using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using back_end.Models;

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
                    cmd.CommandText = string.Format("select Customers.User_ID, Customers.Name,Customers.Phone_Number,Users.Email,Addresses.City,Addresses.Street,Addresses.Number from Customers inner join Addresses on Customers.Address_ID=Addresses.ID Inner join Users on Customers.User_ID=Users.Id where Customers.User_ID ='{0}'", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            item.Id = reader.GetInt32(reader.GetOrdinal("User_Id"));
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


    }
}
