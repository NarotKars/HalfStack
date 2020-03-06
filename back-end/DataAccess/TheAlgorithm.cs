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
        private const string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Supermarket_DB;Integrated Security=True";
        private SqlConnection connection;
        public theAlgorithm()
        {
            connection = new SqlConnection(AppSettings.ConnectionString);
        }

        List<List<int>> current = new List<List<int>> { new List<int> { 1, 2, 1 }, new List<int> { 4, 8, 2 }, new List<int> { 7, 5, 3 }, new List<int> { 6, 6, 4 } };
        double[,] distance = new double[4, 4];
        double x = 5, y = 5;

        public int[,] algorithm(int id)
        {
            List<OrderProduct> orders = GetAllProductsAsGenericList(id);

            List<string> products = new List<string>();
            for (int i = 0; i < orders.ToArray().Length; i++)
            {
                products.Add(orders.ToArray()[i].product);
            }


            List<int> items = GetAllBraches(products);

            for (int i = 0; i < items.Count; i++)
            {
                if (items.Contains(current.ToArray()[i].ToArray()[2]))
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


            for (int i = 0; i < branches.Length; i++)
            {
                for (int j = i + 1; j < branches.Length; j++)
                {
                    distance[i, j] = Math.Sqrt(((branches[i, 0] - branches[j, 0]) * (branches[i, 0] - branches[j, 0]) + (branches[i, 1] - branches[j, 1]) * (branches[i, 1] - branches[j, 1])));
                    distance[j, i] = distance[i, j];
                }
            }

            int customer_min_distance1 = -1, customer_min_distance2 = -1, customer_min_distance3 = -1;
            double imin1 = 100, imin2 = 100, imin3 = 100;
            double[] cbd = new double[10]; //cdb-customer-branch distance
            for (int i = 0; i < 10; i++)
            {
                cbd[i] = Math.Sqrt((branches[i, 0] - x) * (branches[i, 0] - x) + (branches[i, 1] - y) * (branches[i, 1] - y));
                if (cbd[i] < imin1)
                {
                    imin3 = imin2;
                    customer_min_distance3 = customer_min_distance2;
                    imin2 = imin1;
                    customer_min_distance2 = customer_min_distance1;
                    imin1 = cbd[i];
                    customer_min_distance1 = i;
                }
                else if (cbd[i] < imin2)
                {
                    imin3 = imin2;
                    customer_min_distance3 = customer_min_distance2;
                    imin2 = cbd[i];
                    customer_min_distance2 = i;
                }
                else if (cbd[i] < imin3)
                {
                    imin3 = cbd[i];
                    customer_min_distance3 = i;
                }
            }
            bool[] visited = { false, false, false, false };
            nextStep(visited, distance, customer_min_distance1);

            return branches;
        }

        public int nextStep(bool[] visited, double[,] distance, int worker) //worker-i gtnvelu branchi kortinatnery
        {
            visited[worker] = true;
            int min = 100;
            int nextBranch = -1;
            for (int j = 0; j < distance.Length; j++)
            {
                if (distance[worker, j] != 0 && visited[worker] == false && distance[worker, j] < min)
                {
                    //min = distance[worker, j];
                    nextBranch = j;
                }
            }
            return nextBranch;
        }


        public List<OrderProduct> GetAllProductsAsGenericList(int id)
        {
            List<OrderProduct> orders = new List<OrderProduct>();
            string sql = "select Orders_Products.Order_Id, Orders_Products.Quantity, Products.Name, Addresses.City, Addresses.Street, Addresses.Number, Orders.Status from Orders_Products join Products on Products.Barcode=Orders_Products.Product_code join Customers on Customers.User_Id=Orders_Products.Customer_Id join Addresses on Addresses.ID=Customers.Address_ID join Orders on Orders.Order_Id=Orders_Products.Order_Id where Orders_Products.Order_Id='{0}'";
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
                                quantity = dataReader.GetInt32(dataReader.GetOrdinal("Quantity"))
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
                products.Add(orders.ToArray()[i].product);
            }


            List<int> branches = GetAllBraches(products);

            return orders;
        }
  }
}