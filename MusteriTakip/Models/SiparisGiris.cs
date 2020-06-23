using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusteriTakip.Models
{
    [Serializable]
    public class SiparisGiris
    {

        [Display(Name = "Müşteri")]
        public int MusteriId { get; set; }

        [Display(Name = "Ürün")]
        public int UrunId { get; set; }

        [Display(Name = "Miktar")]
        public int Miktar { get; set; }

    }
}