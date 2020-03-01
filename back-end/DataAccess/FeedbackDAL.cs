using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using back_end.Models;
using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace dbSettings.DataAccess
{
    public class FeedbackDAL
    {
        public void InsertFeedback(Feedback feedback)
        {
            string sql = "INSERT INTO Feedback(User_Id,Text)";
            sql += $" VALUES('{feedback.User_Id}' , '{feedback.Text}')";

            StringBuilder errorMessages = new StringBuilder();
            using (SqlConnection connection = new SqlConnection(AppSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        command.CommandType = CommandType.Text;
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        for (int i = 0; i < ex.Errors.Count; i++)
                        {
                            errorMessages.Append("Index #" + i + "\n" +
                           "Message: " + ex.Errors[i].Message + "\n" +
                           "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                           "Source: " + ex.Errors[i].Source + "\n" +
                           "Procedure: " + ex.Errors[i].Procedure + "\n");
                        }
                        Console.WriteLine(errorMessages.ToString());
                    }
                }
            }
        }
    }
}

