using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace back_end.Models
{
    public class Feedback
    {
        public int ID { get; set; }
        public int User_Id { get; set; }
        public string Text { get; set; }

    }
}