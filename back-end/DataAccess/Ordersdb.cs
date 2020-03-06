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
            string sql = "SELECT Order_Id, Customer_Id, Order_Date, City, Street, Number, Orders.Status, Transactions.Amount FROM Orders join Addresses on Orders.Address_ID=Addresses.ID join Transactions on Transactions.Id=Orders.Order_Id WHERE Customer_Id = '{0}'";
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
                                    orderId=dataReader.GetInt64(dataReader.GetOrdinal("Order_Id")),
                                    customerId=dataReader.GetInt32(dataReader.GetOrdinal("Customer_Id")),
                                    orderDate=dataReader.GetDateTime(dataReader.GetOrdinal("Order_Date")),
                                    address= dataReader.GetString(dataReader.GetOrdinal("City")) +
                                             dataReader.GetString(dataReader.GetOrdinal("Street")) +
                                             dataReader.GetString(dataReader.GetOrdinal("Number")),
                                    status = dataReader.GetString(dataReader.GetOrdinal("Status")),
                                    amount=dataReader.GetDecimal(dataReader.GetOrdinal("Amount"))
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


        public List<Order> GetAllOrdersAsGenericList()
        {
            List<Order> orders = new List<Order>();
            string sql = "SELECT Order_Id, Customer_Id, Order_Date, City, Street, Number, Status FROM Orders join Addresses on Orders.Address_ID=Addresses.ID";
            StringBuilder errorMessages = new StringBuilder();
            try
            {
                using (SqlConnection connection = new SqlConnection(AppSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(string.Format(sql), connection))
                    {
                        connection.Open();
                        using (SqlDataReader dataReader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (dataReader.Read())
                            {
                                orders.Add(new Order
                                {
                                    orderId=dataReader.GetInt64(dataReader.GetOrdinal("Order_Id")),
                                    customerId=dataReader.GetInt32(dataReader.GetOrdinal("Customer_Id")),
                                    orderDate=dataReader.GetDateTime(dataReader.GetOrdinal("Order_Date")),
                                    address= dataReader.GetString(dataReader.GetOrdinal("City")) +
                                             dataReader.GetString(dataReader.GetOrdinal("Street")) +
                                             dataReader.GetString(dataReader.GetOrdinal("Number")),
                                    status = dataReader.GetString(dataReader.GetOrdinal("Status")),
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

        public void InsertOrder(Reorder order, long id)
        {
            string sql="insert into Orders(Order_Id,Customer_Id, Order_Date, Status)";
                   sql+= $" Values ('{id}', '{order.customerId}','{order.orderDate}', 'new')";
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

        public void updateOrderAddress(Address address, long id)
        {   
            string sql="if(select addresses.id from addresses where  addresses.city='{0}' and addresses.street='{1}' and addresses.number='{2}' ) is not null update Orders set Address_ID = (select id from addresses where addresses.city = '{0}' and addresses.street = '{1}' and addresses.number = '{2}') where Orders.Order_Id = '{3}' else begin insert into addresses(city, street, number) values('{0}', '{1}', '{2}') update Orders set Address_ID = (select scope_identity() as id) where Orders.Order_Id = '{3}' end";
            StringBuilder errorMessages = new StringBuilder();
            using (SqlConnection connection = new SqlConnection(AppSettings.ConnectionString))
            {
                using (SqlCommand command= new SqlCommand(string.Format(sql,address.City,address.Street,address.Number,id), connection))
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