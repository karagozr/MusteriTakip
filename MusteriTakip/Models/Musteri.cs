using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusteriTakip.Models
{
    [Serializable]
    public class Musteri
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Müşteri No")]
        public string MusteriNo { get; set; }

        [Display(Name = "Ad")]
        public string Ad { get; set; }

        [Display(Name = "Soyad")]
        public string Soyad { get; set; }

        [Display(Name = "Telefon No")]
        public string TelNo { get; set; }

        [Display(Name = "Adres")]
        public string Adres { get; set; }

        [Display(Name = "Bakiye")]
        public double Bakiye { get; set; }
    }
}