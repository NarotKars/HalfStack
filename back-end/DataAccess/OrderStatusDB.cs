
﻿using back_end.Models;
using System.Data.SqlClient;

namespace dbSettings.DataAccess
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
