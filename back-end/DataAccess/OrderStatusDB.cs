using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using backend.Models;
namespace backend.Managers
{
    public class OrderStatusDB
    {
        private const string _coneectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Supermarket_DB;Integrated Security=True";


        private SqlConnection _connnection;

        public OrderStatusDB()
        {
            _connnection = new SqlConnection(_coneectionString);
        }
        public OrderStatus Update(OrderStatus order)
        {
            try
            {
                _connnection.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = _connnection;
                    cmd.CommandText = string.Format("update Orders set Status='{0}' where Order_Id='{1}'", order.Status, order.OrderId);

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
            return order;
        }
    }
}