using back_end.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace dbSettings.DataAccess
{
    public class Transactiondb
    {
        public Transaction CreateTransaction(decimal total,DateTime orderDate)
        {
            string sql = "CreateTransaction";
            Transaction newTransaction=new Transaction();
            StringBuilder errorMessages = new StringBuilder();
            using (SqlConnection connection = new SqlConnection(AppSettings.ConnectionString))
            {
                using (SqlCommand command= new SqlCommand(sql, connection))
                {
                    try
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();

                        SqlParameter amount=command.CreateParameter();
                        amount.ParameterName="@Amount";
                        amount.Value=total;
                        command.Parameters.Add(amount);

                        SqlParameter date=command.CreateParameter();
                        date.ParameterName="@Date";
                        date.Value=orderDate;
                        command.Parameters.Add(date);

                        SqlParameter status=command.CreateParameter();
                        status.ParameterName="@Status";
                        status.Value="new";
                        command.Parameters.Add(status);

                        SqlParameter newtransactionId= command.CreateParameter();
                        newtransactionId.ParameterName="@id";
                        newtransactionId.Direction=System.Data.ParameterDirection.Output;
                        newtransactionId.DbType= System.Data.DbType.Int64;
                        command.Parameters.Add(newtransactionId);
                        command.ExecuteNonQuery();
                        newTransaction.amount=total;
                        newTransaction.orderDate=orderDate;
                        if(newtransactionId is null)
                            newTransaction.transactionId=-1;
                        else
                            newTransaction.transactionId=(long)newtransactionId.Value;
                    }
                    catch(SqlException ex)
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
            return newTransaction;
        }
    }
}