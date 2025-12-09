using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace veriyapilari4
{
    internal class Program
    {
        public class OyuncuDugumu
        {
            public int FormaNo;
            public string Ad;
            public string Soyadi;

            public OyuncuDugumu Sol;
            public OyuncuDugumu Sag;

            public OyuncuDugumu(int formaNo, string ad, string soyadi)
            {
                FormaNo = formaNo;
                Ad = ad;
                Soyadi = soyadi;
                Sol = null;
                Sag = null;
            }
            public string BilgiYazdir()
            {
                return $"Forma No: {FormaNo}, Ad-Soyad: {Ad} {Soyadi}";
            }
        }
        public class OyuncuBST
        {
            public OyuncuDugumu Kok;
            private bool IsBos = true;

            public OyuncuBST()
            {
                Kok = null;
            }

            public void DegerEkle(int formaNo, string ad, string soyadi, bool kaleciMi = false)
            {
                OyuncuDugumu yeniDugum = new OyuncuDugumu(formaNo, ad, soyadi);

                if (IsBos)
                {
                    if (!kaleciMi)
                    {
                        Console.WriteLine("HATA: Ağaca eklenen ilk oyuncu kaleci olmalıdır.");
                        return;
                    }
                    Kok = yeniDugum;
                    IsBos = false;
                    Console.WriteLine($"EKLENDİ (KÖK): {yeniDugum.BilgiYazdir()}");
                    return;
                }

                // BST ekleme 
                OyuncuDugumu mevcutDugum = Kok;
                OyuncuDugumu oncekiDugum = null;

                while (mevcutDugum != null)
                {
                    oncekiDugum = mevcutDugum;

                    if (formaNo < mevcutDugum.FormaNo)
                    {
                        mevcutDugum = mevcutDugum.Sol;
                    }
                    else if (formaNo > mevcutDugum.FormaNo)
                    {
                        mevcutDugum = mevcutDugum.Sag;
                    }
                    else
                    {
                        Console.WriteLine($"HATA: {formaNo} numaralı forma zaten mevcut.");
                        return;
                    }
                }

                if (formaNo < oncekiDugum.FormaNo)
                {
                    oncekiDugum.Sol = yeniDugum;
                }
                else
                {
                    oncekiDugum.Sag = yeniDugum;
                }
                Console.WriteLine($"EKLENDİ: {yeniDugum.BilgiYazdir()}");
            }
            public OyuncuDugumu Ara(int formaNo)
            {
                OyuncuDugumu mevcutDugum = Kok;
                while (mevcutDugum != null)
                {
                    if (formaNo == mevcutDugum.FormaNo)
                    {
                        return mevcutDugum;
                    }
                    else if (formaNo < mevcutDugum.FormaNo)
                    {
                        mevcutDugum = mevcutDugum.Sol;
                    }
                    else
                    {
                        mevcutDugum = mevcutDugum.Sag;
                    }
                }
                return null; // Bulunamadı
            }
            private OyuncuDugumu EnKucukDugum(OyuncuDugumu dugum)
            {
                OyuncuDugumu mevcut = dugum;
                while (mevcut.Sol != null)
                {
                    mevcut = mevcut.Sol;
                }
                return mevcut;
            }
            public void Sil(int formaNo)
            {
                OyuncuDugumu mevcutDugum = this.Kok;
                OyuncuDugumu ebeveynDugum = null;

                // 1. Silinecek Düğümü Bulma 
                while (mevcutDugum != null && mevcutDugum.FormaNo != formaNo)
                {
                    ebeveynDugum = mevcutDugum;
                    if (formaNo < mevcutDugum.FormaNo)
                    {
                        mevcutDugum = mevcutDugum.Sol;
                    }
                    else
                    {
                        mevcutDugum = mevcutDugum.Sag;
                    }
                }

                if (mevcutDugum == null)
                {
                    Console.WriteLine($"HATA: {formaNo} numaralı oyuncu ağaçta bulunamadı.");
                    return;
                }

                OyuncuDugumu yerineGecenAltDugum;

                // Çocuksuz veya Tek çocuklu ise
                if (mevcutDugum.Sol == null || mevcutDugum.Sag == null)
                {
                    if (mevcutDugum.Sol != null)
                    {
                        yerineGecenAltDugum = mevcutDugum.Sol;
                    }
                    else
                    {
                        yerineGecenAltDugum = mevcutDugum.Sag;
                    }

                    if (ebeveynDugum == null) // Silinen düğüm kök ise
                    {
                        this.Kok = yerineGecenAltDugum;
                    }
                    else if (mevcutDugum.FormaNo < ebeveynDugum.FormaNo)
                    {
                        ebeveynDugum.Sol = yerineGecenAltDugum;
                    }
                    else
                    {
                        ebeveynDugum.Sag = yerineGecenAltDugum;
                    }
                }
                //İki çocuklu ise
                else
                {
                    OyuncuDugumu tempEbeveyn = mevcutDugum;
                    OyuncuDugumu temp = mevcutDugum.Sag;

                    while (temp.Sol != null)
                    {
                        tempEbeveyn = temp;
                        temp = temp.Sol;
                    }

                    //Değerleri silinecek düğüme kopyalar
                    mevcutDugum.FormaNo = temp.FormaNo;
                    mevcutDugum.Ad = temp.Ad;
                    mevcutDugum.Soyadi = temp.Soyadi;

                    //Yedek düğümü (temp) konumundan siler

                    yerineGecenAltDugum = temp.Sag;

                    if (tempEbeveyn == mevcutDugum)
                    {
                        tempEbeveyn.Sag = yerineGecenAltDugum;
                    }
                    else
                    {
                        tempEbeveyn.Sol = yerineGecenAltDugum;
                    }
                }

                Console.WriteLine($"{formaNo} numaralı oyuncu başarıyla silindi.");
            }
            public void LevelorderDolasim()
            {
                Console.Write("Level-order: ");
                if (Kok == null) { Console.WriteLine(); return; }

                Queue<OyuncuDugumu> kuyruk = new Queue<OyuncuDugumu>();
                kuyruk.Enqueue(Kok);

                while (kuyruk.Count > 0)
                {
                    OyuncuDugumu dugum = kuyruk.Dequeue();
                    Console.Write(dugum.FormaNo + " ");

                    if (dugum.Sol != null) kuyruk.Enqueue(dugum.Sol);
                    if (dugum.Sag != null) kuyruk.Enqueue(dugum.Sag);
                }
                Console.WriteLine();
            }

            public void PreorderDolasim(OyuncuDugumu dugum)
            {
                if (dugum == Kok) Console.Write("Preorder: ");
                if (dugum != null)
                {
                    Console.Write(dugum.FormaNo + " ");
                    PreorderDolasim(dugum.Sol);
                    PreorderDolasim(dugum.Sag);
                    if (dugum == Kok) Console.WriteLine();
                }
            }

            public void InorderDolasim(OyuncuDugumu dugum)
            {
                if (dugum == Kok) Console.Write("Inorder: ");
                if (dugum != null)
                {
                    InorderDolasim(dugum.Sol);
                    Console.Write(dugum.FormaNo + " ");
                    InorderDolasim(dugum.Sag);
                    if (dugum == Kok) Console.WriteLine();
                }
            }

            public void PostorderDolasim(OyuncuDugumu dugum)
            {
                if (dugum == Kok) Console.Write("Postorder: ");
                if (dugum != null)
                {
                    PostorderDolasim(dugum.Sol);
                    PostorderDolasim(dugum.Sag);
                    Console.Write(dugum.FormaNo + " ");
                    if (dugum == Kok) Console.WriteLine();
                }
            }

            //Forma numarası en büyük ve en küçük olanı bulma
            public OyuncuDugumu EnBuyukFormaNo()
            {
                if (this.Kok == null) return null;
                OyuncuDugumu mevcut = this.Kok;
                while (mevcut.Sag != null)
                {
                    mevcut = mevcut.Sag;
                }
                return mevcut;
            }

            public OyuncuDugumu EnKucukFormaNo()
            {
                if (this.Kok == null) return null;
                return EnKucukDugum(this.Kok);
            }

            static void Main(string[] args)
            {
                OyuncuBST takimBST = new OyuncuBST();

                Console.WriteLine("### Takım BST Kayıt Sistemi ###\n");

                // Oyuncu Ekleme
                takimBST.DegerEkle(18, "Altay", "Bayındır", kaleciMi: true); // Kök Düğüm (Kaleci)
                takimBST.DegerEkle(7, "Kerem", "Aktürkoğlu");
                takimBST.DegerEkle(23, "Cenk", "Tosun");
                takimBST.DegerEkle(4, "Ozan", "Tufan");
                takimBST.DegerEkle(17, "İrfan Can", "Kahveci");
                takimBST.DegerEkle(14, "Arda", "Güler");
                takimBST.DegerEkle(9, "Burak", "Yılmaz"); ;

                Console.WriteLine();
                Console.WriteLine("Oyuncu Listeleri ");

                
                takimBST.PreorderDolasim(takimBST.Kok);
                takimBST.InorderDolasim(takimBST.Kok);
                takimBST.PostorderDolasim(takimBST.Kok);
                takimBST.LevelorderDolasim();

                Console.WriteLine();
                Console.WriteLine("En Küçük ve En Büyük Bulma");

                
                Console.WriteLine($"En küçük forma no: {takimBST.EnKucukFormaNo()?.BilgiYazdir()}");
                Console.WriteLine($"En büyük forma no: {takimBST.EnBuyukFormaNo()?.BilgiYazdir()}");

                Console.WriteLine();
                Console.WriteLine("Silme İşlemi (11 numarayı silme)");

                takimBST.Sil(11);
                Console.WriteLine("11 numaralı oyuncu silindi.");

                Console.Write("Inorder (Silme Sonrası): ");
                takimBST.InorderDolasim(takimBST.Kok);

                Console.WriteLine();
                Console.ReadKey();
            }
        }
    }
}
