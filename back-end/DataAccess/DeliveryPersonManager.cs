using back_end.Models;
using System.Data;

namespace dbSettings.DataAccess
{
    public class DeliveryPersonManager
    {
        private SqlConnection connection;
        public DeliveryPersonManager()
        {
            connection = new SqlConnection(AppSettings.ConnectionString);
        }

        public DeliveryPersonModel Get(int id)
        {
            var item = new DeliveryPersonModel();
            try
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = string.Format("select Workers.Name, Users.Email, Addresses.City, Addresses.Street, Addresses.Number  from Delivery_Person join Workers on Delivery_Person.Delivery_Id = Workers.Worker_Id join Users on Workers.Worker_Id = Users.Id join Addresses on Delivery_Person.Address_ID = Addresses.ID where Delivery_Person.Delivery_Id = {0}", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            item.Name = reader.GetString(reader.GetOrdinal("Name"));
                            item.Email = reader.GetString(reader.GetOrdinal("Name"));
                            item.Address = reader.GetString(reader.GetOrdinal("Number")) +
                                           reader.GetString(reader.GetOrdinal("Street")) +
                                            reader.GetString(reader.GetOrdinal("City"));
                        }
                    };
                };
            }
            catch
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
            return item;
        }
    }
}
