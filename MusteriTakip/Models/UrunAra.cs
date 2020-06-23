using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusteriTakip.Models
{
    public class UrunAra
    {
        [Display(Name = "Ürün Kodu")]
        public string UrunKodu { get; set; }

        [Display(Name = "Ürün İsmi")]
        public string UrunIsmi { get; set; }
    }
}