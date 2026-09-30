using System;
using System.Collections.Generic;
using System.Text;

namespace Sistem_Inventaris.Models
{
    public class Transaksi
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public int total { get; set; }
        public List<DetailTransaksi> detailTransaction { get; set; } = new List<DetailTransaksi>();

    }
}
