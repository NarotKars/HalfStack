
﻿using back_end.Models;
using System.Data.SqlClient;

namespace dbSettings.DataAccess

{
    public class UserDeliveryDB
    {
        private const string _coneectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Supermarket_DB;Integrated Security=True";


        private SqlConnection _connnection;

        public UserDeliveryDB()
        {
            _connnection = new SqlConnection(_coneectionString);
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
                    cmd.CommandText = string.Format("select Workers.Name,Users.Username, Users.Email, Addresses.City, Addresses.Street, Addresses.Number  from Delivery_Person join Workers on Delivery_Person.Delivery_Id = Workers.Worker_Id join Users on Workers.Worker_Id = Users.Id join Addresses on Delivery_Person.Address_ID = Addresses.ID where Delivery_Person.Delivery_Id = {0}", id);

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



    }
}
