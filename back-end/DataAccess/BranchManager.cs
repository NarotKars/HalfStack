using System.Data.SqlClient;
using back_end.Models;
using System.Collections.Generic;
namespace dbSettings.DataAccess
{
    public class BranchManager
    {
        private SqlConnection connection;

        public BranchManager()
        {
            connection = new SqlConnection(AppSettings.ConnectionString);
        }

        public List<int> GetAllBraches(List<string> orderproducts)
        {
            var item = new List<int>();
            var bb = new List<BranchProducts>();
            try
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = string.Format("select Id from Branches");

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            item.Add(reader.GetInt32(reader.GetOrdinal("Id")));
                        }
                    };
                    var barnches = item.ToArray();
                    for (int i = 0; i < barnches.Length; i++)
                    {
                        cmd.CommandText = string.Format("select Product_code from Products_in_Branches where Branch_Id={0}", barnches[i]);
                        var some = new List<string>();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                some.Add(reader.GetString(reader.GetOrdinal("Product_code"))
                                //Count = reader.GetInt32(reader.GetOrdinal("Count"))
                                );

                            }
                        }
                        bb.Add(new BranchProducts
                        {
                            BranchId = barnches[i],
                            items = some
                        });
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

            var filterbranhces = new bool[item.ToArray().Length];
            var currentbranch = item.ToArray();
            for (int i = 0; i < currentbranch.Length; i++)
            {
                filterbranhces[i] = false;
            }
            var brancs = new List<int>();

            for (int i = 0; i < currentbranch.Length; i++)
            {
                for (int j = 0; j < orderproducts.ToArray().Length; j++)
                    if (bb[i].items.Contains((orderproducts.ToArray()[j])))
                    {
                        filterbranhces[i] = true;
                        brancs.Add(bb[i].BranchId);
                        break;
                    }
            }

            return brancs;
        }
    }
}
