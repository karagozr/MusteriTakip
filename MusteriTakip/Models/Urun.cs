using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusteriTakip.Models
{
    [Serializable]
    public class Urun
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Ürün Kodu")]
        public string UrunKodu { get; set; }

        [Display(Name = "Ürün İsmi")]
        public string UrunIsmi { get; set; }

        [Display(Name = "Fiyat")]
        public double Fiyat { get; set; }

        [Display(Name = "Stok Miktarı")]
        public double StokMiktari { get; set; }
    }
}