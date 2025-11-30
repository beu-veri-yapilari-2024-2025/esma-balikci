using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace veriyapilari3
{
    internal class Program
    {
        public class Dugum
        {
            public int Veri;
            public Dugum Sol;
            public Dugum Sag;

            public Dugum(int deger)
            {
                Veri = deger;
                Sol = null;
                Sag = null;
            }
        }
        public class IkiliAgac
        {
            public Dugum Kok;

            public IkiliAgac()
            {
                Kok = null;
            }

            public void DegerEkle(int veri)
            {
                // 1. Ağaç boşsa yeni düğümü kök yapar
                if (Kok == null)
                {
                    Kok = new Dugum(veri);
                    return;
                }

                Dugum mevcutDugum = Kok;
                Dugum oncekiDugum = null;

                while (mevcutDugum != null)
                {
                    oncekiDugum = mevcutDugum; 

                    if (veri < mevcutDugum.Veri)
                    {
                        // Yeni veri küçükse sola ilerler
                        mevcutDugum = mevcutDugum.Sol;
                    }
                    else if (veri > mevcutDugum.Veri)
                    {
                        // Yeni veri büyükse sağa ilerler
                        mevcutDugum = mevcutDugum.Sag;
                    }
                    else
                    {
                        // Değer zaten ağaçta varsa ekleme yapmaz
                        Console.WriteLine("Eleman zaten mevcut.");
                        return;
                    }
                }

                // 2. Yeni düğümü oluştur ve ebeveynine bağlar
                if (veri < oncekiDugum.Veri)
                {
                    oncekiDugum.Sol = new Dugum(veri);
                }
                else
                {
                    oncekiDugum.Sag = new Dugum(veri);
                }
            }

            public void LevelorderDolasim()
            {
                Console.Write("Level-order: ");

                // Ağaç boşsa, işlemi bitir
                if (Kok == null)
                {
                    Console.WriteLine();
                    return;
                }

                //Kök kuyruğa eklenir
                Queue<Dugum> kuyruk = new Queue<Dugum>();
                kuyruk.Enqueue(Kok);

                // Kuyruk boşalana kadar döner
                while (kuyruk.Count > 0)
                {
                    // Kuyruktan bir sonraki düğümü alır
                    Dugum dugum = kuyruk.Dequeue();
                    Console.Write(dugum.Veri + " ");

                    // Eğer sol çocuğu varsa kuyruğa ekler
                    if (dugum.Sol != null)
                    {
                        kuyruk.Enqueue(dugum.Sol);
                    }

                    // Eğer sağ çocuğu varsa kuyruğa ekler
                    if (dugum.Sag != null)
                    {
                        kuyruk.Enqueue(dugum.Sag);
                    }
                }
                Console.WriteLine();
            }

            public void PreorderDolasim(Dugum dugum)
            {
                // Kök kontrolü
                if (dugum == this.Kok)
                {
                    Console.Write("Preorder: ");
                }

                if (dugum != null)
                {
                    Console.Write(dugum.Veri + " ");        // Kök
                    PreorderDolasim(dugum.Sol);        // Sol
                    PreorderDolasim(dugum.Sag);         // Sağ

                    // Son kontrol
                    if (dugum == this.Kok)
                    {
                        Console.WriteLine();
                    }
                }
            }

            public void InorderDolasim(Dugum dugum)
            {
                if (dugum == this.Kok)
                {
                    Console.Write("Inorder: ");
                }

                if (dugum != null)
                {
                    InorderDolasim(dugum.Sol);      // Sol
                    Console.Write(dugum.Veri + " ");    // Kök
                    InorderDolasim(dugum.Sag);       // Sağ

                    if (dugum == this.Kok)
                    {
                        Console.WriteLine();
                    }
                }
            }

            public void PostorderDolasim(Dugum dugum)
            {
                if (dugum == this.Kok)
                {
                    Console.Write("Postorder: ");
                }

                if (dugum != null)
                {
                    PostorderDolasim(dugum.Sol);     // Sol
                    PostorderDolasim(dugum.Sag);      // Sağ
                    Console.Write(dugum.Veri + " ");    // Kök

                    if (dugum == this.Kok)
                    {
                        Console.WriteLine();
                    }
                }
            }
        }
        static void Main(string[] args)
            {
            // 1. Ağacı oluşturma
            IkiliAgac agac = new IkiliAgac();
            int[] girilenDegerler = { 10, 6, 15, 3, 8, 20 };

            Console.WriteLine("Ağaç oluşturuluyor. Girilen değerler: 10, 6, 15, 3, 8, 20\n");

            for (int i = 0; i < girilenDegerler.Length; i++)
            {
                int deger = girilenDegerler[i];
                agac.DegerEkle(deger);
            }

            agac.PreorderDolasim(agac.Kok);
            agac.InorderDolasim(agac.Kok);
            agac.PostorderDolasim(agac.Kok);
            agac.LevelorderDolasim();
            Console.ReadKey();
        }
    }
}