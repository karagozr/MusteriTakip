using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusteriTakip.Models
{
    [Serializable]
    public class Siparis
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Siparis Kodu")]
        public int SiparisKodu { get; set; }

        [Display(Name = "Müşteri")]
        public int MusteriId { get; set; }

        [Display(Name = "Ürün")]
        public int UrunId { get; set; }

        [Display(Name = "Miktar")]
        public int Miktar { get; set; }

        [Display(Name = "Tutar")]
        public double Tutar { get; set; }

        [Display(Name = "Durum")]
        public bool Durum { get; set; }
    }
}