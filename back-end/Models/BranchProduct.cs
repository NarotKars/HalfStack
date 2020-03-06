using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_end.Models
{
    public class BranchProducts
    {
        public int BranchId { set; get; }
        public List<string> items;
    }

    public class Products
    {
        public string Product { set; get; }
        // public int Count { set; get; }
    }
}
