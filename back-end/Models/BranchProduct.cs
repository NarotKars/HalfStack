using System;
using System.Collections.Generic;
namespace back_end.Models
{
    public class BranchProducts
    {
        public int BranchId { set; get; }
        public List<ProductsCount> items;
    }

    public class ProductsCount
    {
        public string Product { set; get; }
        public int Count { set; get; }
    }

    public class BranchesAndProducts
    {
        public string BranchName {set; get;}
        public string Barcode {get;set;}
        public int Quantity {get; set;}
    }
}