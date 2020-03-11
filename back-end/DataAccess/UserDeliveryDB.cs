using back_end.Models;
using System.Data.SqlClient;
using System.Text;
using System;
using System.Collections.Generic;
using System.Data;

namespace dbSettings.DataAccess

{
    public class UserDeliveryDB
    {
        private const string _coneectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Supermarket_DB;Integrated Security=True";


        private SqlConnection _connnection;

        public UserDeliveryDB()
        {
            _connnection = new SqlConnection(AppSettings.ConnectionString);
        }


        public UserDelivery ReadById(int id)
        {
            UserDelivery item = new UserDelivery();

            try
            {
                _connnection.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = _connnection;
                    cmd.CommandText = string.Format("select Workers.Name, Users.Email, Addresses.City, Addresses.Street, Addresses.Number  from Delivery_Person join Workers on Delivery_Person.Delivery_Id = Workers.Worker_Id join Users on Workers.Worker_Id = Users.Id join Addresses on Delivery_Person.Address_ID = Addresses.ID where Delivery_Person.Delivery_Id = {0}", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            item.Name = reader.GetString(reader.GetOrdinal("Name"));
                            item.Email = reader.GetString(reader.GetOrdinal("Email"));
                            item.Address = reader.GetString(reader.GetOrdinal("City")) + reader.GetString(reader.GetOrdinal("Street")) + reader.GetString(reader.GetOrdinal("Number"));

                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                _connnection.Close();
            }

            return item;
        }
        public Update Updatet(Update user)
        {
            try
            {
                _connnection.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = _connnection;
                    cmd.CommandText = string.Format("update Workers Set Name='{0}' from Workers join Delivery_Person on Workers.Worker_Id=Delivery_Person.Delivery_Id where Delivery_Id='{1}'", user.Name, user.Id);

                    cmd.ExecuteNonQuery();

                }

            }
            catch
            {
                throw;
            }
            finally
            {
                _connnection.Close();
            }

            try
            {
                _connnection.Open();

                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.Connection = _connnection;
                    cmd.CommandText = string.Format("if(select addresses.id from addresses where  addresses.city='{0}' and addresses.street='{1}' and addresses.number='{2}' ) is not null update Delivery_Person set  Address_ID = (select id from addresses where addresses.city ='{0}' and addresses.street ='{1}' and addresses.number ='{2}') from Delivery_Person  join Workers on Delivery_Person.Delivery_Id=Workers.Worker_Id join  Users on Workers.Worker_Id=Users.Id where Delivery_Person.Delivery_Id='{3}' else begin insert into addresses(city, street, number) values('{0}', '{1}', '{2}') update Delivery_Person set Address_ID = (select scope_identity() as id)from Delivery_Person  join Workers on Delivery_Person.Delivery_Id=Workers.Worker_Id join  Users on Workers.Worker_Id=Users.Id where Delivery_Person.Delivery_Id='{3}' end", user.Address.City, user.Address.Street, user.Address.Number, user.Id);

                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {

                throw;
            }
            finally
            {
                _connnection.Close();
            }

            return user;
        }

        public List<Order> GetToBeAcceptedOrders(int id)
        {
            List<Order> orders=new List<Order>();
            string sql = "select Orders.Order_Id, Customer_Id from Orders_DeliveryWorkers join Orders on Orders_DeliveryWorkers.Order_Id=Orders.Order_Id where  (Id1='{0}' or Id2='{0}' or Id3='{0}') and Accepted_Id is null";
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
                                    orderId= dataReader.GetInt64(dataReader.GetOrdinal("Order_Id")),
                                    customerId=dataReader.GetInt32(dataReader.GetOrdinal("Customer_Id"))
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
