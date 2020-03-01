namespace back_end.Models
{
    public class OrderDetails
    {
        public int id {get; set;}
        public string product {get; set;}
        public int quantity {get; set;}
        public int customerId {get;set;}
        public long orderId {get; set;}
    }
}