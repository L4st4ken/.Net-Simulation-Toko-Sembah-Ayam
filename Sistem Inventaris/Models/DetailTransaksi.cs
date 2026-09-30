using System;
using System.Collections.Generic;
using System.Text;

namespace Sistem_Inventaris.Models
{
    public class DetailTransaksi
    {
        public int id { get; set; }
        public int transactionId { get; set; }
        public Produk product { get; set; }
        public int qty { get; set; }
        public int subtotal { get; set; }
    }
}
