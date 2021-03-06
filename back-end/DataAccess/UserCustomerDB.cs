﻿using back_end.Models;
using System.Data.SqlClient;

namespace dbSettings.DataAccess
{
    public class UserCustomerDB
    {
        private SqlConnection _connnection;

        public UserCustomerDB()
        {
            _connnection = new SqlConnection(AppSettings.ConnectionString);
        }

        public UserCustomer ReadById(int id)
        {
            UserCustomer item = new UserCustomer();

            try
            {
                _connnection.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = _connnection;
                    cmd.CommandText = string.Format("select Customers.Phone_Number,ISNULL(Customers.Name,'unknown') as Name,ISNULL(Users.Email,'unknown') as Email,Addresses.City,Addresses.Street,Addresses.Number from Customers left join Addresses on Customers.Address_ID=Addresses.ID  left join Users on Customers.User_ID=Users.Id where Customers.User_ID ='{0}'", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            item.Phone_Number = reader.GetString(reader.GetOrdinal("Phone_Number"));
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
                    cmd.CommandText = string.Format("update Customers set Name='{0}' where Customers.User_Id='{1}'", user.Name, user.Id);

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


public Update updateAddress(Update user)
{
    try
        {
            _connnection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = _connnection;
                    cmd.CommandText = string.Format("if(select addresses.id from addresses where  addresses.city='{0}' and addresses.street='{1}' and addresses.number='{2}' ) is not null update customers set address_id = (select id from addresses where addresses.city = '{0}' and addresses.street = '{1}' and addresses.number = '{2}') where customers.user_id = '{3}' else begin insert into addresses(city, street, number) values('{0}', '{1}', '{2}') update customers set address_id = (select scope_identity() as id) where customers.user_id = '{3}' end", user.Address.City, user.Address.Street, user.Address.Number, user.Id);
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

    }
}

