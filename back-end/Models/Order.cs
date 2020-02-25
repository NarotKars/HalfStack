using System;
namespace back_end.Models
{
    public class Order
    {
        public long orderId {get; set;}
        public int customerId {get; set;}
        public DateTime orderDate {get; set;}
        public string address {get; set;}
        public string status {get; set;}
    }
}
