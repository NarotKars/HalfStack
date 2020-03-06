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
            string sql = "select Transaction_Products.ProductsCode, Transaction_Products.[Count], Orders.Customer_Id, Products.Name, Products.Selling_Price, Orders.Order_Id, Products.Category_Name from Transaction_Products join Orders on Transaction_Products.TransactionID=Orders.Order_Id join Products on Products.Barcode=Transaction_Products.ProductsCode where Orders.Customer_Id='{0}'";
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
                                    quantity = dataReader.GetInt32(dataReader.GetOrdinal("Count")),
                                    categoryName=dataReader.GetString(dataReader.GetOrdinal("Category_Name")),
                                    barcode=dataReader.GetString(dataReader.GetOrdinal("ProductsCode")),
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
            string sql = "select Transaction_Products.ProductsCode, Transaction_Products.[Count], Transaction_Products.TransactionID from Transaction_Products where Transaction_Products.TransactionID='{0}'";
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
                                    orderId= dataReader.GetInt64(dataReader.GetOrdinal("TransactionId")),
                                    quantity = dataReader.GetInt32(dataReader.GetOrdinal("Count")),
                                    barcode=dataReader.GetString(dataReader.GetOrdinal("ProductsCode")),
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
            string sql = "INSERT INTO Transaction_Products(TransactionID, ProductsCode, [Count])";
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

    }
}