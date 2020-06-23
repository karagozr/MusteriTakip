using MusteriTakip.Helpers;
using MusteriTakip.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusteriTakip.Controllers
{
    public class MusteriController : Controller
    {
        public ActionResult Index()
        {
            MusteriAra musteriAra= new MusteriAra();
            return View(musteriAra);
        }
        public ActionResult List(MusteriAra musteriAra)
        {
            List<Musteri> musteriler = FileHelper<Musteri>.DosyaOku();

            if (!string.IsNullOrEmpty(musteriAra.MusteriAdi))
            {
                musteriler = musteriler.Where(x => x.Ad == musteriAra.MusteriAdi).ToList();
            }
            if (!string.IsNullOrEmpty(musteriAra.MusteriNo))
            {
                musteriler = musteriler.Where(x => x.MusteriNo == musteriAra.MusteriNo).ToList();
            }
            return View(musteriler);
        }


        public ActionResult Create()
        {
            Musteri musteri = new Musteri();
            return View(musteri);
        }

        [HttpPost]
        public ActionResult Create(Musteri musteri)
        {
            List<Musteri> musteriler;
            if (FileHelper<Musteri>.DosyaOku() == null) musteriler = new List<Musteri>();
            else musteriler = FileHelper<Musteri>.DosyaOku();
            int kayitSayisi = musteriler == null ? 0 : musteriler.Count;
            musteri.Id = kayitSayisi + 1;
            musteriler.Add(musteri);
            try
            {
                FileHelper<Musteri>.DosyaYaz(musteriler);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            List<Musteri> musteriler = FileHelper<Musteri>.DosyaOku();
            Musteri musteri = musteriler.Where(x => x.Id == id).FirstOrDefault();
            return View(musteri);
        }

        [HttpPost]
        public ActionResult Edit(int id, Musteri musteri)
        {

            List<Musteri> musteriler = FileHelper<Musteri>.DosyaOku();

            foreach (var item in musteriler)
            {
                if (item.Id == id)
                {
                    item.MusteriNo = musteri.MusteriNo;
                    item.Ad = musteri.Ad;
                    item.Soyad = musteri.Soyad;
                    item.TelNo = musteri.TelNo;
                    item.Adres = musteri.Adres;
                    item.Bakiye = musteri.Bakiye;
                }
                
            }
            try
            {
                FileHelper<Musteri>.DosyaYaz(musteriler);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        
    }
}
