using System.Data.SqlClient;
using back_end.Models;
using dbSettings.DataAccess;

namespace back_end.Managers
{
    public class OrderManager
    {
        private SqlConnection connection;

        public OrderManager()
        {
            connection = new SqlConnection(AppSettings.ConnectionString);
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
