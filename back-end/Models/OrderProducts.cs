namespace back_end.Models
{
    public class OrderProduct
    {
        public long orderId{get; set;}    
        public string address {get; set;}
        public string status {get; set;}
        public string product {get; set;}
        public int quantity {get; set;}
        public string barcode {get; set;}
    }
}