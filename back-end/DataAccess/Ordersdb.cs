using back_end.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace dbSettings.DataAccess
{
    public class Orderdb
    {
        public List<Order> GetOrdersAsGenericList(int id)
        {
            List<Order> orders = new List<Order>();
            string sql = "SELECT Order_Id, Customer_Id, Order_Date, City, Street, Number, Status FROM Orders join Addresses on Orders.Address_ID=Addresses.ID WHERE Customer_Id = '{0}'";
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
                                orders.Add(new Order
                                {
                                    Order_Id=dataReader.GetInt64(dataReader.GetOrdinal("Order_Id")),
                                    Customer_Id=dataReader.GetInt32(dataReader.GetOrdinal("Customer_Id")),
                                    Order_Date=dataReader.GetDateTime(dataReader.GetOrdinal("Order_Date")),
                                    City = dataReader.GetString(dataReader.GetOrdinal("City")),
                                    Street = dataReader.GetString(dataReader.GetOrdinal("Street")),
                                    Number = dataReader.GetString(dataReader.GetOrdinal("Number")),
                                    Status = dataReader.GetString(dataReader.GetOrdinal("Status")),
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