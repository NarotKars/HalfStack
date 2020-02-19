using dbSettings.DataAccess;
using back_end.Models;
using System;
using System.Collections.Generic;

namespace ADONETConnectedDEMO
{
    class Program
    {
        static void Main(string[] args)
        {
             Orderdb productDAL = new Orderdb();

            // GetProductsCountScalar
            //Console.WriteLine($"\n\nProducts count is: {productDAL.GetProductsCountScalar()}");

            // GetProductsAsGenericList
            List<Order> products = productDAL.GetToDosAsGenericList(1);

            // 'for' loop used here for demonstrate how use index in the list structure
            for (int i = 0; i < products.Count; i++)
            {
                Console.WriteLine($"  {products[i].Order_Id}\t\t{products[i].Order_Date.ToShortDateString()}\t{products[i].City}\t{products[i].Street}\t{products[i].Number}\t{products[i].Status}");
            }
            
            Console.ReadLine();
        }        
    }
}
