using System;
namespace back_end.Models
{
    public class Transaction
    {
        public long transactionId {get; set;}
        public DateTime orderDate {get; set;}
        public decimal amount {get;set;}
    }
}