using back_end.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace dbSettings.DataAccess
{
    public class theAlgorithm
    {
        long ORDERID;
        double minDistance=0;
        private const string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Supermarket_DB;Integrated Security=True";
        private SqlConnection connection;
        public theAlgorithm()
        {
            connection = new SqlConnection(AppSettings.ConnectionString);
        }

        List<List<int>> current = new List<List<int>> { new List<int> { 1, 2, 1 }, new List<int> { 4, 8, 2 }, new List<int> { 7, 5, 3 }, new List<int> { 6, 6, 4 } };
        
        List<List<int>> delivery=new List<List<int>>();
        double[,] distance = new double[4, 4];
        double x = 5, y = 5;
        List<int> path=new List<int>();
        List<BranchProducts> bb = new List<BranchProducts>();
        List<OrderProduct> orders = new List<OrderProduct>();

        List<BranchProducts> currentbranchproducts = new List<BranchProducts>();
        public int[,] algorithm(long id)
        {
            ORDERID=id;
            List<OrderProduct> orders = GetAllProductsAsGenericList(id);

            List<string> products = new List<string>();
            //products of current order
            for (int i = 0; i < orders.ToArray().Length; i++)
            {
                products.Add(orders.ToArray()[i].barcode);
            }
             int[,] branchesFake = new int[current.Count, 3];
            for (int i = 0; i < current.Count; i++)
                for (int j = 0; j < 3; j++)
                {
                    branchesFake[i, j] = current.ToArray()[i].ToArray()[j];
                }
            for (int i = 0; i < branchesFake.GetLength(0); i++)
            {
                for (int j = i + 1; j <branchesFake.GetLength(1); j++)
                {
                    distance[i, j] = Math.Sqrt(((branchesFake[i, 0] - branchesFake[j, 0]) * (branchesFake[i, 0] - branchesFake[j, 0]) + (branchesFake[i, 1] - branchesFake[j, 1]) * (branchesFake[i, 1] - branchesFake[j, 1])));
                    distance[j, i] = distance[i, j];
                }
            }
            List<int> availableBranches = GetAllBraches(products);

            //current available branches
            for (int i = 0; i < availableBranches.Count; i++)
            {
                if (availableBranches.Contains(current.ToArray()[i].ToArray()[2]))
                    continue;
                else
                    current.RemoveAt(i);
            }


            int[,] branches = new int[current.Count, 3];
            for (int i = 0; i < current.Count; i++)
                for (int j = 0; j < 3; j++)
                {
                    branches[i, j] = current.ToArray()[i].ToArray()[j];
                }

            

            int customer_min_distance1 = -1, customer_min_distance2 = -1, customer_min_distance3 = -1;
            double imin1 = 100, imin2 = 100, imin3 = 100;
            double[] cbd = new double[availableBranches.Count]; //cdb-customer-branch distance
            for (int i = 0; i < availableBranches.Count; i++)
            {
                cbd[i] = Math.Sqrt((branches[i, 0] -x ) * (branches[i, 0] - x) + (branches[i, 1] - y) * (branches[i, 1] - y));
                if (cbd[i] < imin1)
                {
                    imin3 = imin2;
                    customer_min_distance3 = customer_min_distance2;
                    imin2 = imin1;
                    customer_min_distance2 = customer_min_distance1;
                    imin1 = cbd[i];
                    customer_min_distance1 = availableBranches[i];
                }
                else if (cbd[i] < imin2)
                {
                    imin3 = imin2;
                    customer_min_distance3 = customer_min_distance2;
                    imin2 = cbd[i];
                    customer_min_distance2 = availableBranches[i];
                }
                else if (cbd[i] < imin3)
                {
                    imin3 = cbd[i];
                    customer_min_distance3 = availableBranches[i];
                }
            }
            bool[] visited = { false, false, false, false };
            minDistance+=imin1;
            int nextBranch = customer_min_distance1-1;

           
            while (orders.Count!=0)
            {
                
                visited[nextBranch]=true;
                path.Add(nextBranch+1);
                double min = 100;
                nextStep(visited, distance, nextBranch+1);
                if(orders.Count!=0)
                {
                for(int i=0;i<4;i++)
                {
                for (int j = 0; j < 4; j++)
                {
                    if (distance[i, j] != 0 && visited[j] == false && distance[i, j] < min)
                    {
                        min = distance[i, j];
                        nextBranch = j;
                    }
                }
                }
                minDistance+=min;
                }
            }
            minDistance*=5;
            minDistance=Convert.ToInt32(minDistance);
            minDistance+=15;
            int hours=0,minutes=0;
            if(minDistance>=120)
            {
                hours=2;
                minDistance-=120;
            }
            else if(minDistance>=60)
            {
                hours=1;
                minDistance-=60;
            }
            minutes=Convert.ToInt32(minDistance);
            DateTime confirm_later=new DateTime(2020, 3, 12, hours,minutes,0);
            
            
            Console.WriteLine(confirm_later);
            int[] deliveryworkers=deliveryWorkers(nextBranch+1);
            try
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = string.Format("INSERT INTO Orders_DeliveryWorkers(Id1,Id2,Id3,Order_Id) values('{0}', '{1}', '{2}','{3}')", deliveryworkers[0], deliveryworkers[1], deliveryworkers[2], ORDERID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                connection.Close();
            }



            try
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = string.Format("UPDATE Orders SET confirm_later='{0}' WHERE Order_Id='{1}'", confirm_later, ORDERID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                connection.Close();
            }

            return branches;
        }

        public void nextStep(bool[] visited, double[,] distance, int nextBranch)
        {
              for(int i=0;i<bb.Count;i++)
                    {
                        
                        if(bb[i].BranchId==nextBranch)
                        {
                    for(int k=0;k<orders.Count;k++)
                    {
                        
                        for(int j=0;j<bb[i].items.Count;j++)
                        {
                        if(orders.Count!=0 && orders[k].barcode==bb[i].items[j].Product)
                        {
                           
                            BranchesAndProducts branchesAndProducts = new BranchesAndProducts();
                            branchesAndProducts.Barcode=bb[i].items[j].Product;
                            branchesAndProducts.BranchName=getBranchName(bb[i].BranchId);

                            if(orders[k].quantity-bb[i].items[j].Count<=0)
                            {
                                branchesAndProducts.Quantity=orders[k].quantity;
                                orders[k].quantity = 0;
                                    orders.RemoveAt(k);
                                   
                                    k--;
                            }
                            else
                                {
                                    orders[k].quantity = orders[j].quantity - bb[i].items[j].Count;
                                    branchesAndProducts.Quantity=orders[k].quantity;
                                }
                            InsertBranchesAndProducts(branchesAndProducts,ORDERID);

                        }
                        }
                        }
                    }

                    }
            
        }


        public List<OrderProduct> GetAllProductsAsGenericList(long id)
        {

            string sql = "select Products.Barcode, Orders_Products.Order_Id, Orders_Products.Quantity, Products.Name, Addresses.City, Addresses.Street, Addresses.Number, Orders.Status from Orders_Products join Products on Products.Barcode=Orders_Products.Product_code join Orders on Orders.Order_Id=Orders_Products.Order_Id join Customers on Customers.User_Id=Orders.Customer_Id join Addresses on Addresses.ID=Customers.Address_ID  where Orders_Products.Order_Id='{0}'";
            StringBuilder errorMessages = new StringBuilder();
            try
            {
                using (SqlCommand command = new SqlCommand(string.Format(sql, id), connection))
                {
                    connection.Open();
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            orders.Add(new OrderProduct
                            {
                                orderId = dataReader.GetInt64(dataReader.GetOrdinal("Order_Id")),
                                status = dataReader.GetString(dataReader.GetOrdinal("Status")),
                                address = dataReader.GetString(dataReader.GetOrdinal("City")) +
                                          dataReader.GetString(dataReader.GetOrdinal("Street")) +
                                          dataReader.GetString(dataReader.GetOrdinal("Number")),
                                product = dataReader.GetString(dataReader.GetOrdinal("Name")),
                                quantity = dataReader.GetInt32(dataReader.GetOrdinal("Quantity")),
                                barcode=dataReader.GetString(dataReader.GetOrdinal("Barcode"))
                            });
                        }
                    }
                }

            }
            catch (SqlException ex)
            {
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    errorMessages.Append("Index #" + i + "\n" +
                   "Message: " + ex.Errors[i].Message + "\n" +
                   "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                   "Source: " + ex.Errors[i].Source + "\n" +
                   "Procedure: " + ex.Errors[i].Procedure + "\n");
                }
                Console.WriteLine(errorMessages.ToString());
            }
            finally
            {
                connection.Close();
            }

            List<string> products = new List<string>();
            for (int i = 0; i < orders.ToArray().Length; i++)
            {
                products.Add(orders.ToArray()[i].barcode);
            }

            
            /*List<int> branches = GetAllBraches(products);
            Console.WriteLine(branches.Count);
            for(int i=0;i<branches.Count;i++)
            {
                Console.WriteLine("b");
                Console.WriteLine(branches[i]);
            }*/
            return orders;
        }


        public List<int> GetAllBraches(List<string> orderproducts)
        {
            var branchNames = new List<string>();
            //List<BranchProducts> bb = new List<BranchProducts>();

            try
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = string.Format("select Branch_Name from Branches");

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            branchNames.Add(reader.GetString(reader.GetOrdinal("Branch_Name")));
                        }
                    };
                    var barnches = branchNames.ToArray();
                    for (int i = 0; i < branchNames.Count; i++)
                    {
                        cmd.CommandText = string.Format("select Product_code, Count from Products_in_Branches where Branch_name='{0}'", branchNames[i]);
                        var some = new List<ProductsCount>();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                some.Add(new ProductsCount
                                {
                                    Product = reader.GetString(reader.GetOrdinal("Product_code")),
                                    Count = reader.GetInt32(reader.GetOrdinal("Count"))
                                });
                            }
                        }
                        bb.Add(new BranchProducts
                        {
                            BranchId = getBranchId(barnches[i]),
                            items = some
                        });
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
            var brancs = new List<int>();
            var filterbranhces = new bool[branchNames.Count];
            var currentbranch = branchNames.ToArray();
            for (int i = 0; i < currentbranch.Length; i++)
            {
                filterbranhces[i] = false;
            }
            string[] p=new string[4];
            for(int i=0;i<4;i++)
            {
                for(int j=0;j<bb[i].items.Count;j++)
                {
                    p[i]+=bb[i].items[j].Product+' ';
                }
            }
            for (int i = 0; i < currentbranch.Length; i++)
            {
                for (int j = 0; j < orderproducts.ToArray().Length; j++)
                {
                    if (p[i].Contains(orderproducts.ToArray()[j]))
                    {
                        filterbranhces[i] = true;
                        brancs.Add(bb[i].BranchId);
                        currentbranchproducts.Add(new BranchProducts
                        {
                            BranchId = bb[i].BranchId,
                        });
                        break;
                    }
                }
            }
            return brancs;
        }

        public int getBranchId(string BranchName)
        {
            if(BranchName=="Branch of Corsa Ave.") return 4;
            else if(BranchName=="Branch of Gold") return 3;
            else if(BranchName=="Branch of N Oxford") return 2;
            else return 1;
        }
        public string getBranchName(int branchId)
        {
            if(branchId==4) return "Branch of Corsa Ave.";
            else if(branchId==3) return "Branch of Gold";
            else if(branchId==2) return "Branch of N Oxford";
            else return "Branch of Navy";
        }


        public void InsertBranchesAndProducts(BranchesAndProducts orderBranches, long id)
        {
            string sql = "INSERT INTO OrderProducts_Branches(Order_Id, Branch_Name,Product_Code,Quantity)";
            sql += $" VALUES('{id}' , '{orderBranches.BranchName}', '{orderBranches.Barcode}', '{orderBranches.Quantity}')";
            StringBuilder errorMessages = new StringBuilder();
            using (SqlConnection connection = new SqlConnection(AppSettings.ConnectionString))
            {
                using (SqlCommand command= new SqlCommand(sql, connection))
                {
                    try
                    {
                        command.CommandType = CommandType.Text;
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch(SqlException ex)
                    {
                        for (int i = 0; i < ex.Errors.Count; i++)
                        {
                             errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                            "Source: " + ex.Errors[i].Source + "\n" +
                            "Procedure: " + ex.Errors[i].Procedure + "\n");
                        }
                        Console.WriteLine(errorMessages.ToString());
                    }
                }
            }
        }


        public  int[] deliveryWorkers(int branchId)
        {
            List<int> delivery_Id=new List<int>();
            try
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = string.Format("select Delivery_Id from Delivery_Person where Status='free'");

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            delivery_Id.Add(reader.GetInt32(reader.GetOrdinal("Delivery_Id")));
                        }
                    };
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                connection.Close();
            }

            Random random=new Random();
            for(int i=0;i<delivery_Id.Count;i++)
                delivery.Add(new List<int>(){random.Next(0,11),random.Next(0,11),delivery_Id[i]});
            
            int customer_min_distance1 = -1, customer_min_distance2 = -1, customer_min_distance3 = -1;
            double imin1 = 100, imin2 = 100, imin3 = 100;
            double[] dbd = new double[4]; //dbd-dilvery-branch distance
    
            for (int i = 0; i < delivery.Count; i++)
            {
                for(int m=0;m<current.Count;m++)
                {
                    if(current.ToArray()[m].ToArray()[2] == branchId)
                    {
                    
                       
                        dbd[i]=Math.Sqrt((delivery.ToArray()[i].ToArray()[0] - current.ToArray()[m].ToArray()[0]) * (delivery.ToArray()[i].ToArray()[0] - current.ToArray()[m].ToArray()[0]) + (delivery.ToArray()[i].ToArray()[1] - current.ToArray()[m].ToArray()[1]) * (delivery.ToArray()[i].ToArray()[1] - current.ToArray()[m].ToArray()[1]));
                        
                        if(dbd[i] < imin1)
                        {
                            imin3 = imin2;
                            customer_min_distance3 = customer_min_distance2;
                            imin2 = imin1;
                            customer_min_distance2 = customer_min_distance1;
                            imin1 = dbd[i];
                            customer_min_distance1 =delivery.ToArray()[i].ToArray()[2];
                        }
                        else if (dbd[i] < imin2)
                        {
                            imin3 = imin2;
                            customer_min_distance3 = customer_min_distance2;
                            imin2 = dbd[i];
                            customer_min_distance2 = delivery.ToArray()[i].ToArray()[2] ;
                        }
                        else if (dbd[i] < imin3)
                        {
                            imin3 = dbd[i];
                            customer_min_distance3 = delivery.ToArray()[i].ToArray()[2];
                        }
                    }
                }
            }
                int[] del=new int[]{customer_min_distance1,customer_min_distance2,customer_min_distance3};
                return del;
        }
      

    }
}
