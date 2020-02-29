using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using backend.Models;

namespace backend.Managers
{
    public class UserManagerDB
    {
        private const string _coneectionString = @"Data Source=.\SQLEXPRESS1;Initial Catalog=PersonalInfo_DB;Integrated Security=True";


        private SqlConnection _connnection;

        public UserManagerDB()
        {
            _connnection = new SqlConnection(_coneectionString);
        }


        public UserManager ReadById(int id)
        {
            UserManager item = new UserManager();

            try
            {
                _connnection.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = _connnection;
                    cmd.CommandText = string.Format("select Users.Id, Workers.Name,Users.Email from Workers join Users on Workers.Worker_Id=Users.Id where Workers.Worker_Id={0}", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            item.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                            item.Name = reader.GetString(reader.GetOrdinal("Name"));
                            item.Email = reader.GetString(reader.GetOrdinal("Email"));
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
        public UserManager Updatet(UserManager user)
        {
            try
            {
                _connnection.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = _connnection;
                    cmd.CommandText = string.Format("update  Users set username='{0}',Email='{1}' from Users inner join Workers on Workers.Worker_Id=Users.id where Workers.Worker_Id='{2}'", user.Name, user.Email, user.Id);

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

