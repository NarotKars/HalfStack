using System;

namespace back_end.Models
{
    public class Order
    {
        public long Order_Id {get; set;}
        public int Customer_Id {get; set;}
        public DateTime Order_Date {get; set;}
        public string City {get; set;}
        public string Street {get; set;}
        public string Number {get; set;}
        public string Status {get; set;}
    }
}
