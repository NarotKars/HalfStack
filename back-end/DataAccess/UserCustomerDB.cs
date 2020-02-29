using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using backend.Models;

namespace backend.Managers
{
    public class UserCustomerDB
    {
        private const string _coneectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Supermarket_DB; Security=True";


        private SqlConnection _connnection;

        public UserCustomerDB()
        {
            _connnection = new SqlConnection(_coneectionString);
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
                    cmd.CommandText = string.Format("select  Customers.Name,Customers.Phone_Number,Users.Email,Addresses.City,Addresses.Street,Addresses.Number from Customers join Addresses on Customers.Address_ID=Addresses.ID  join Users on Customers.User_ID=Users.Id where Customers.User_ID ='{0}'", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            item.Name = reader.GetString(reader.GetOrdinal("Name"));
                            item.Phone_Number = reader.GetString(reader.GetOrdinal("Phone_Number"));
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
                    cmd.CommandText = string.Format("update  Users set Username='{0}',Email='{1}' from Users join Customers on Customers.User_Id=Users.Id where Customers.User_Id='{2}'", user.Name, user.Email, user.Id);

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
