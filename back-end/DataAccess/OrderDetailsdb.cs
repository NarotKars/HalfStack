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
        public List<OrderDetails> GetOrderDetailsAsGenericList(int id)
        {
            List<OrderDetails> orders = new List<OrderDetails>();
            string sql = "select Orders_Products.Id, Customer_Id, Orders_Products.Order_Id, Products.Name, Orders_Products.quantity from Products join Orders_Products on Orders_Products.Product_code=Products.Barcode where Orders_Products.Customer_Id='{0}'";
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
                                    id = dataReader.GetInt32(dataReader.GetOrdinal("Id")),
                                    orderId= dataReader.GetInt64(dataReader.GetOrdinal("Order_Id")),
                                    customerId = dataReader.GetInt32(dataReader.GetOrdinal("Customer_Id")),
                                    product = dataReader.GetString(dataReader.GetOrdinal("Name")),
                                    quantity = dataReader.GetInt32(dataReader.GetOrdinal("Quantity")),
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