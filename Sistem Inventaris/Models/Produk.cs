using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace Sistem_Inventaris.Models
{
    public class Produk
    {
        public int id { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        public int stock { get; set; }
    }
}
