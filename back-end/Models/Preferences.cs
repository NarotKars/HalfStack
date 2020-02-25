using System;
namespace back_end.Models
{
    public class Preference
    {
        public int preferenceId {get; set;}
        public int customerId {get; set;}
        public string text {get; set;}
    }
}