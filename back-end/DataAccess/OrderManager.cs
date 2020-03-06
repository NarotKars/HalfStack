using System.Data.SqlClient;
using back_end.Models;
using System.Collections.Generic;
namespace dbSettings.DataAccess
{
    public class OrderManager
    {
        private SqlConnection connection;

        public OrderManager()
        {
            connection = new SqlConnection(AppSettings.ConnectionString);
        }

           public List<Order> GetOrdersAsGenericList(string status)
        {
            List<Order> orders = new List<Order>();
            string sql = "SELECT Order_Id, Customer_Id, Order_Date, City, Street, Number FROM Orders join Addresses on Orders.Address_ID=Addresses.ID WHERE  Status='{0}'";

            try
            {
                using (SqlCommand command = new SqlCommand(string.Format(sql, status), connection))
                {
                    connection.Open();
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            orders.Add(new Order
                            {
                                orderId = dataReader.GetInt64(dataReader.GetOrdinal("Order_Id")),
                                customerId = dataReader.GetInt32(dataReader.GetOrdinal("Customer_Id")),
                                orderDate = dataReader.GetDateTime(dataReader.GetOrdinal("Order_Date")),
                                address = dataReader.GetString(dataReader.GetOrdinal("City")) +
                                         dataReader.GetString(dataReader.GetOrdinal("Street")) +
                                         dataReader.GetString(dataReader.GetOrdinal("Number"))
                            });
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
                connection.Close();
            }
            return orders;
        }
        
        public string Get(int id)
        {
            string feedback = "";
            try
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = string.Format("Select  Orders.Feedback from Orders where Order_Id = {0}", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            feedback = reader.GetString(reader.GetOrdinal("Feedback"));
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

            return feedback;
        }

        public string PutFeedback(OrderFeedbackModel model)
        {
            string feedback = model.Feedback;
            try
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = string.Format("Update Orders set Feedback='{0}' where Order_Id= {1}", model.Feedback, model.Id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                connection.Close();
            }

            return feedback;
        }
    }
}
