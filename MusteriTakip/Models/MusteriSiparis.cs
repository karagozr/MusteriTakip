using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusteriTakip.Models
{
    public class MusteriSiparis
    {
        public string UrunIsmi { get; set; }

        public double Miktar { get; set; }

        public double Fiyat { get; set; }

        public double Tutar { get; set; }
    }
}