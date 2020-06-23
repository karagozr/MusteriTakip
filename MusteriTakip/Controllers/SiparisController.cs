using MusteriTakip.Helpers;
using MusteriTakip.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusteriTakip.Controllers
{
    public class SiparisController : Controller
    {
        public ActionResult Create(int id)
        {
            List<Urun> urunler = FileHelper<Urun>.DosyaOku();
            ViewBag.Musteriler = FileHelper<Musteri>.DosyaOku().Where(x=>x.Id== id);
            ViewBag.Urunler = urunler;
            if (FileHelper<Siparis>.DosyaOku() == null)
            {
                ViewBag.MusteriAktifSiparileri = null;
            }
            else
            {
                ViewBag.MusteriAktifSiparileri = FileHelper<Siparis>.DosyaOku().Where(x => x.MusteriId == id && x.Durum == true) == null ? null :
                FileHelper<Siparis>.DosyaOku().Where(x => x.MusteriId == id && x.Durum == true).Select(x => new MusteriSiparis
                {
                    UrunIsmi = urunler.Where(z => z.Id == x.UrunId).FirstOrDefault().UrunIsmi,
                    Miktar = x.Miktar,
                    Fiyat = urunler.Where(z => z.Id == x.UrunId).FirstOrDefault().Fiyat,
                    Tutar = x.Tutar
                });
            }
            
            return View();
        }

        [HttpPost]
        public ActionResult Create(SiparisGiris siparisGiris)
        {
            List<Siparis> siparisler;
            if (FileHelper<Siparis>.DosyaOku() == null) siparisler = new List<Siparis>();
            else siparisler = FileHelper<Siparis>.DosyaOku();

            int kayitSayisi = siparisler == null ? 0 : siparisler.Count;

            //daha önce tamamlanmaış siaprişi varmı bak
            int siparisNo = 0;
            Siparis oncekiSiparis = siparisler.Where(x => x.MusteriId == siparisGiris.MusteriId && x.Durum == true).FirstOrDefault();
            if (oncekiSiparis == null)
            {
                if (siparisler.Count == 0)
                {
                    siparisNo = 1;
                }
                else
                {
                    siparisNo = siparisler.Last().SiparisKodu+1;
                }
            }
            else
            {
                siparisNo = oncekiSiparis.SiparisKodu;
            }
                

            Urun urun = FileHelper<Urun>.DosyaOku().Where(x => x.Id == siparisGiris.UrunId).FirstOrDefault();

            Siparis siparis = new Siparis
            {
                Id = kayitSayisi,
                SiparisKodu = siparisNo,
                MusteriId = siparisGiris.MusteriId,
                UrunId = siparisGiris.UrunId,
                Miktar = siparisGiris.Miktar,
                Tutar = siparisGiris.Miktar * urun.Fiyat,
                Durum = true
            };

            siparisler.Add(siparis);
            try
            {
                FileHelper<Siparis>.DosyaYaz(siparisler);
                return RedirectToAction("Create",siparisGiris.MusteriId);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public ActionResult SiparisKaydet(int id)
        {
            List<Siparis> siparisler;
            if (FileHelper<Siparis>.DosyaOku() == null) siparisler = new List<Siparis>();
            else siparisler = FileHelper<Siparis>.DosyaOku();

            

            //daha önce tamamlanmaış siaprişi varmı bak
            List<Siparis> musteriAktifSiparisler = siparisler.Where(x => x.MusteriId == id && x.Durum == true).ToList();
            if (musteriAktifSiparisler.Count == 0)
            {
                TempData["Mesaj"] = "En az bir siapriş kalemi giriniz";
                return RedirectToAction("Create", id);
            }
            else
            {
                if(musteriAktifSiparisler.Sum(x=>x.Tutar)> FileHelper<Musteri>.DosyaOku().FirstOrDefault().Bakiye)
                {
                    TempData["Mesaj"] = "Siapriş Tutarı bakiyenizden fazla";
                    return RedirectToAction("Create/"+id);
                }
                else
                {
                    List<Urun> urunler = FileHelper<Urun>.DosyaOku();
                    foreach (var item in urunler)
                    {
                        if (musteriAktifSiparisler.Where(x => x.UrunId == item.Id) != null)
                            item.StokMiktari = item.StokMiktari - musteriAktifSiparisler.Where(x => x.UrunId == item.Id).Sum(x => x.Miktar);
                    }

                    foreach (var item in siparisler)
                    {
                        if (item.MusteriId == id)
                            item.Durum = false;
                    }

                    try
                    {
                        FileHelper<Urun>.DosyaYaz(urunler);
                        FileHelper<Siparis>.DosyaYaz(siparisler);
                        TempData["Mesaj"] = "Sipariş başarıyla kaydedildi ve fatura çıktısı verildi";
                        return GetTestFile(musteriAktifSiparisler);
                    }
                    catch
                    {
                        TempData["Mesaj"] = "İşlem sırasında hata oluştu";
                        return RedirectToAction("Create", id);
                    }
                }
            }

           
        }

        [HttpGet]
        public FileContentResult GetTestFile(List<Siparis> siparisler)
        {
            List<Musteri> musteriler = FileHelper<Musteri>.DosyaOku();
            List<Urun> urunler = FileHelper<Urun>.DosyaOku();
            MemoryStream memoryStream = new MemoryStream();
            TextWriter tw = new StreamWriter(memoryStream);
            int i = 1;
            Musteri musteri = musteriler.Where(x => x.Id == siparisler.FirstOrDefault().MusteriId).FirstOrDefault();
            tw.WriteLine("              FATURA BİLGİLERİ");
            tw.WriteLine($"Müşteri Adı - Soyadı : {musteri.Ad} {musteri.Soyad}");
            tw.WriteLine($"Telefon No           : {musteri.TelNo} ");
            tw.WriteLine($"Adres                : {musteri.Adres} ");
            tw.WriteLine("------------------------------------------------------------");
            tw.WriteLine("              FATURA KALEMLERİ                              ");
            foreach (var item in siparisler)
            {
                tw.WriteLine($"{i++} - {urunler.Where(x=>x.Id==item.UrunId).FirstOrDefault().UrunIsmi} - {item.Miktar} - {item.Tutar}");
            }
            tw.WriteLine($"Fatura Tutarı        : {siparisler.Sum(x=>x.Tutar)} ");
            tw.Flush();
            tw.Close();

            return File(memoryStream.GetBuffer(), "text/plain",  musteri.Ad+"_"+musteri.Soyad+".txt");
        }

    }
}
