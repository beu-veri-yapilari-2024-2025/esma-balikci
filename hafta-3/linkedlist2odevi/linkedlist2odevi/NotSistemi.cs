using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linkedlist2odevi
{
    public class NotSistemi
    {
        private OgrenciBaslikDugumu ogrenciListesiBasi = null;
        private DersBaslikDugumu dersListesiBasi = null;
        private OgrenciBaslikDugumu OgrenciBulVeyaOlustur(int ogrenciNo)
        {
            OgrenciBaslikDugumu temp = ogrenciListesiBasi;
            OgrenciBaslikDugumu onceki = null;

            while (temp != null && temp.OgrenciNumarasi < ogrenciNo)
            {
                onceki = temp;
                temp = temp.SonrakiOgrenci;
            }

            if (temp != null && temp.OgrenciNumarasi == ogrenciNo)
            {
                return temp;
            }

            OgrenciBaslikDugumu yeniOgrenciBaslik = new
    OgrenciBaslikDugumu(ogrenciNo);

            if (onceki == null)
            {
                yeniOgrenciBaslik.SonrakiOgrenci = ogrenciListesiBasi;
                ogrenciListesiBasi = yeniOgrenciBaslik;
            }
            else
            {
                yeniOgrenciBaslik.SonrakiOgrenci = temp;
                onceki.SonrakiOgrenci = yeniOgrenciBaslik;
            }

            return yeniOgrenciBaslik;
        }

        private DersBaslikDugumu DersBulVeyaOlustur(string dersKodu)
        {
            DersBaslikDugumu temp = dersListesiBasi;
            DersBaslikDugumu onceki = null;

            while (temp != null && string.Compare(temp.DersKodu, dersKodu) < 0)
            {
                onceki = temp;
                temp = temp.SonrakiDers;
            }

            if (temp != null && temp.DersKodu == dersKodu)
            {
                return temp;
            }
            DersBaslikDugumu yeniDersBaslik = new DersBaslikDugumu(dersKodu);

            if (onceki == null)
            {
                yeniDersBaslik.SonrakiDers = dersListesiBasi;
                dersListesiBasi = yeniDersBaslik;
            }
            else
            {
                yeniDersBaslik.SonrakiDers = temp;
                onceki.SonrakiDers = yeniDersBaslik;
            }

            return yeniDersBaslik;
        }
        public bool NotEkle(int ogrenciNo, string dersKodu, string harfNotu)
        {
            OgrenciBaslikDugumu ogrenciBaslik = OgrenciBulVeyaOlustur(ogrenciNo);
            DersBaslikDugumu dersBaslik = DersBulVeyaOlustur(dersKodu);
            NotDugumu yeniNot = new NotDugumu(ogrenciNo, dersKodu, harfNotu);

            NotDugumu tempYatay = ogrenciBaslik.IlkDers;
            NotDugumu oncekiYatay = null;
            while (tempYatay != null && string.Compare(tempYatay.DersKodu, dersKodu)
    < 0)
            {
                oncekiYatay = tempYatay;
                tempYatay = tempYatay.OgrencininSonrakiDersi;
            }

            if (tempYatay != null && tempYatay.DersKodu == dersKodu)
            {
                return false;
            }

            if (oncekiYatay == null)
            {
                yeniNot.OgrencininSonrakiDersi = ogrenciBaslik.IlkDers;
                ogrenciBaslik.IlkDers = yeniNot;
            }
            else
            {
                yeniNot.OgrencininSonrakiDersi = tempYatay;
                oncekiYatay.OgrencininSonrakiDersi = yeniNot;
            }

            NotDugumu tempDikey = dersBaslik.IlkOgrenci;
            NotDugumu oncekiDikey = null;
            while (tempDikey != null && tempDikey.OgrenciNumarasi < ogrenciNo)
            {
                oncekiDikey = tempDikey;
                tempDikey = tempDikey.DerstekiSonrakiOgrenci;
            }

            if (tempDikey != null && tempDikey.OgrenciNumarasi == ogrenciNo)
            {
                return false;
            }

            if (oncekiDikey == null)
            {
                yeniNot.DerstekiSonrakiOgrenci = dersBaslik.IlkOgrenci;
                dersBaslik.IlkOgrenci = yeniNot;
            }
            else
            {
                yeniNot.DerstekiSonrakiOgrenci = tempDikey;
                oncekiDikey.DerstekiSonrakiOgrenci = yeniNot;
            }

            return true;
        }


        public bool NotSil(int ogrenciNo, string dersKodu)
        {
            OgrenciBaslikDugumu ogrenciBaslik = OgrenciBulVeyaOlustur(ogrenciNo);
            if (ogrenciBaslik == null) return false;

            NotDugumu tempYatay = ogrenciBaslik.IlkDers;
            NotDugumu oncekiYatay = null;
            while (tempYatay != null && tempYatay.DersKodu != dersKodu)
            {
                oncekiYatay = tempYatay;
                tempYatay = tempYatay.OgrencininSonrakiDersi;
            }

            if (tempYatay == null) return false;

            if (oncekiYatay == null)
            {
                ogrenciBaslik.IlkDers = tempYatay.OgrencininSonrakiDersi;
            }
            else
            {
                oncekiYatay.OgrencininSonrakiDersi =
    tempYatay.OgrencininSonrakiDersi;
            }

            DersBaslikDugumu dersBaslik = DersBulVeyaOlustur(dersKodu);
            if (dersBaslik == null) return false;

            NotDugumu tempDikey = dersBaslik.IlkOgrenci;
            NotDugumu oncekiDikey = null;
            while (tempDikey != null && tempDikey.OgrenciNumarasi != ogrenciNo)
            {
                oncekiDikey = tempDikey;
                tempDikey = tempDikey.DerstekiSonrakiOgrenci;
            }

            if (tempDikey == null) return false;

            if (oncekiDikey == null)
            {
                dersBaslik.IlkOgrenci = tempDikey.DerstekiSonrakiOgrenci;
            }
            else
            {
                oncekiDikey.DerstekiSonrakiOgrenci =
    tempDikey.DerstekiSonrakiOgrenci;
            }

            return true;
        }


        public List<string> DerstekiOgrencileriListele(string dersKodu)
        {
            List<string> sonuc = new List<string>();
            DersBaslikDugumu dersBaslik = DersBulVeyaOlustur(dersKodu);

            if (dersBaslik == null || dersBaslik.IlkOgrenci == null)
            {
                sonuc.Add("Bu dersi alan öğrenci bulunamadı.");
                return sonuc;
            }


            NotDugumu temp = dersBaslik.IlkOgrenci;
            while (temp != null)
            {
                sonuc.Add($"Öğrenci No: {temp.OgrenciNumarasi} - Harf Notu: {temp.HarfNotu}");
                temp = temp.DerstekiSonrakiOgrenci;
            }

            return sonuc;
        }

        public List<string> OgrencininDersleriniListele(int ogrenciNo)
        {
            List<string> sonuc = new List<string>();
            OgrenciBaslikDugumu ogrenciBaslik = OgrenciBulVeyaOlustur(ogrenciNo);

            if (ogrenciBaslik == null || ogrenciBaslik.IlkDers == null)
            {
                sonuc.Add("Bu öğrencinin aldığı ders bulunamadı.");
                return sonuc;
            }


            NotDugumu temp = ogrenciBaslik.IlkDers;
            while (temp != null)
            {
                sonuc.Add($"Ders Kodu: {temp.DersKodu} - Harf Notu: { temp.HarfNotu}"); 
                temp = temp.OgrencininSonrakiDersi;
            }

            return sonuc;
        }
    }
}
