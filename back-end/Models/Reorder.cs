using System;
namespace back_end.Models
{
    public class Reorder
    {
        public long orderId {get; set;}
        public int customerId {get; set;}
        public DateTime orderDate {get; set;}
        public Address Address { get; set; }
        public string status {get; set;}
        public decimal amount {get; set;}
    }
}
