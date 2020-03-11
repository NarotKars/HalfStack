namespace back_end.Models
{
    public class OrderDetails
    {
        public string categoryName {get; set;}
        public string product {get; set;}
        public int quantity {get; set;}
        public int customerId {get;set;}
        public long orderId {get; set;}
        public decimal price {get;set;}
        public string barcode {get;set;}
    }

    public class OrderDelivery
    {
        public long orderId{get; set;}
        public int deliveryId{get; set;}
    }
}