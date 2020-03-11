using back_end.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace dbSettings.DataAccess
{
    public class OrderDetailsdb
    {
        public List<OrderDetails> GetOrderDetailsOfOneCustomerAsGenericList(int id)
        {
            List<OrderDetails> orders = new List<OrderDetails>();
            string sql = "select Products.Barcode,Orders.Order_Id,Quantity,Products.Name,Products.Category_Name,Products.Selling_Price,Orders.Customer_Id from Orders_Products join Products on Orders_Products.Product_code=Products.Barcode join Orders on Orders.Order_Id=Orders_Products.Order_Id where Orders.Customer_Id='{0}'";
            StringBuilder errorMessages = new StringBuilder();
            try
            {
                using (SqlConnection connection = new SqlConnection(AppSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(string.Format(sql,id), connection))
                    {
                        connection.Open();
                        using (SqlDataReader dataReader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (dataReader.Read())
                            {
                                orders.Add(new OrderDetails
                                {
                                    orderId= dataReader.GetInt64(dataReader.GetOrdinal("Order_Id")),
                                    customerId = dataReader.GetInt32(dataReader.GetOrdinal("Customer_Id")),
                                    product = dataReader.GetString(dataReader.GetOrdinal("Name")),
                                    price=dataReader.GetDecimal(dataReader.GetOrdinal("Selling_Price")),
                                    quantity = dataReader.GetInt32(dataReader.GetOrdinal("Quantity")),
                                    categoryName=dataReader.GetString(dataReader.GetOrdinal("Category_Name")),
                                    barcode=dataReader.GetString(dataReader.GetOrdinal("Barcode")),
                                });
                            }
                        }
                    }
                }
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
            return orders;
        }



        public List<OrderDetails> GetOrderDetailsAsGenericListByOrderId(long id)
        {
            List<OrderDetails> orders = new List<OrderDetails>();
            string sql = "select Order_Id,Quantity,Product_code from Orders_Products where Order_Id='{0}'";
            StringBuilder errorMessages = new StringBuilder();
            try
            {
                using (SqlConnection connection = new SqlConnection(AppSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(string.Format(sql,id), connection))
                    {
                        connection.Open();
                        using (SqlDataReader dataReader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (dataReader.Read())
                            {
                                orders.Add(new OrderDetails
                                {
                                    orderId= dataReader.GetInt64(dataReader.GetOrdinal("Order_Id")),
                                    quantity = dataReader.GetInt32(dataReader.GetOrdinal("Quantity")),
                                    barcode=dataReader.GetString(dataReader.GetOrdinal("Product_code")),
                                });
                            }
                        }
                    }
                }
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
            return orders;
        }
        

        public void InsertProduct(OrderDetails order, long id)
        {
            string sql = "insert into Orders_Products(Order_Id, Product_code, Quantity)";
            sql += $" VALUES('{id}' , '{order.barcode}', '{order.quantity}')";
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

        public List<BranchesAndProducts> GetOrderDetailsWithPathAsGenericList(int id)
        {
            List<BranchesAndProducts> orders = new List<BranchesAndProducts>();
            string sql = "select Branch_Name, Products.Name, Quantity from OrderProducts_Branches join Products on Products.Barcode=OrderProducts_Branches.Product_code where Order_Id='{0}'";
            StringBuilder errorMessages = new StringBuilder();
            try
            {
                using (SqlConnection connection = new SqlConnection(AppSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(string.Format(sql,id), connection))
                    {
                        connection.Open();
                        using (SqlDataReader dataReader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (dataReader.Read())
                            {
                                orders.Add(new BranchesAndProducts
                                {
                                    BranchName= dataReader.GetString(dataReader.GetOrdinal("Branch_Name")),
                                    Barcode = dataReader.GetString(dataReader.GetOrdinal("Name")),
                                    Quantity = dataReader.GetInt32(dataReader.GetOrdinal("Quantity")),
                                });
                            }
                        }
                    }
                }
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
            return orders;
        }


    }
}