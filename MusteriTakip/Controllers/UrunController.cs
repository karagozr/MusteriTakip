using MusteriTakip.Helpers;
using MusteriTakip.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusteriTakip.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(UrunAra urunAra)
        {
            List<Urun> urunler = FileHelper<Urun>.DosyaOku();

            if (!string.IsNullOrEmpty(urunAra.UrunIsmi))
            {
                urunler = urunler.Where(x => x.UrunIsmi == urunAra.UrunIsmi).ToList();
            }
            if (!string.IsNullOrEmpty(urunAra.UrunKodu))
            {
                urunler = urunler.Where(x => x.UrunKodu == urunAra.UrunKodu).ToList();
            }
            return View(urunler);
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(Urun urun)
        {
            List<Urun> urunler;
            if (FileHelper<Urun>.DosyaOku() == null) urunler = new List<Urun>();
            else urunler = FileHelper<Urun>.DosyaOku();

            int kayitSayisi = urunler == null? 0: urunler.Count;
            urun.Id = kayitSayisi + 1;
            urunler.Add(urun);
            try
            {
                FileHelper<Urun>.DosyaYaz(urunler);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            List<Urun> urunler = FileHelper<Urun>.DosyaOku();
            Urun urun = urunler.Where(x => x.Id == id).FirstOrDefault();
            return View(urun);
        }

        [HttpPost]
        public ActionResult Edit(int id, Urun urun)
        {
            List<Urun> urunler = FileHelper<Urun>.DosyaOku();

            foreach (var item in urunler)
            {
                if (item.Id == id)
                {
                    item.UrunKodu = urun.UrunKodu;
                    item.UrunIsmi = urun.UrunIsmi;
                    item.StokMiktari = urun.StokMiktari;
                    item.Fiyat = urun.Fiyat;
                }

            }
            try
            {
                FileHelper<Urun>.DosyaYaz(urunler);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

       
    }
}
