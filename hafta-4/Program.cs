using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kuyrukodevi
{
    internal class Program
    {
        static class DiziKuyrugu
        {
            static List<string>[] kuyruklar = new List<string>[3]
            {
            new List<string>(), 
            new List<string>(), 
            new List<string>()
            };
            public static void Enqueue(string isim, int oncelik)
            {
                kuyruklar[oncelik - 1].Add(isim);
            }
            public static string Dequeue()
            {
                for (int i = 0; i < 3; i++)
                {
                    if (kuyruklar[i].Count > 0)
                    {
                        string kisi = kuyruklar[i][0];
                        kuyruklar[i].RemoveAt(0);
                        return kisi + ($" öncelik {i + 1}");
                    }
                }
                return "Kuyruk boş.";
            }
            public static void Yazdir()
            {
                
                for (int i = 0; i < 3; i++)
                {
                    Console.WriteLine($"{i + 1}. grup: {string.Join(", ", kuyruklar[i])}");
                }
            }
            class Kisi
            {
                public string Ad;
                public int Oncelik;
                public Kisi Next;
                public Kisi(string ad, int oncelik)
                {
                    Ad = ad; Oncelik = oncelik; Next = null;
                }
            }
            static class LinkedKuyruk
            {
                static Kisi[] baslar = new Kisi[3];
                static Kisi[] sonlar = new Kisi[3];

                public static void Enqueue(string isim, int oncelik)
                {
                    Kisi yeni = new Kisi(isim, oncelik);
                    int index = oncelik - 1;
                    if (baslar[index] == null)
                        baslar[index] = sonlar[index] = yeni;
                    else
                    {
                        sonlar[index].Next = yeni;
                        sonlar[index] = yeni;
                    }
                }

                public static string Dequeue()
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (baslar[i] != null)
                        {
                            string kisi = baslar[i].Ad;
                            baslar[i] = baslar[i].Next;
                            if (baslar[i] == null) sonlar[i] = null;
                            return kisi + ($" öncelik {i + 1}");
                        }
                    }
                    return "Kuyruk boş.";
                }

                public static void Yazdir()
                {
                    
                    for (int i = 0; i < 3; i++)
                    {
                        Console.Write($"{i + 1}. grup: ");
                        Kisi temp = baslar[i];
                        while (temp != null)
                        {
                            Console.Write(temp.Ad + " ");
                            temp = temp.Next;
                        }
                        Console.WriteLine();
                    }
                }
            }
            static void Main(string[] args)
            {
                DiziKuyrugu.Enqueue("Esma", 2);
                DiziKuyrugu.Enqueue("Ayşe", 1);
                DiziKuyrugu.Enqueue("Samet", 3);
                DiziKuyrugu.Enqueue("Zeynep", 1);
                DiziKuyrugu.Yazdir();

                Console.WriteLine("\nİşlem yapılıyor: " + DiziKuyrugu.Dequeue());
                DiziKuyrugu.Yazdir();

                LinkedKuyruk.Enqueue("Furkan", 3);
                LinkedKuyruk.Enqueue("Nazar", 1);
                LinkedKuyruk.Enqueue("Can", 2);
                LinkedKuyruk.Enqueue("Deniz", 1);
                LinkedKuyruk.Yazdir();

                Console.WriteLine("\nİşlem yapılıyor: " + LinkedKuyruk.Dequeue());
                LinkedKuyruk.Yazdir();

                Console.ReadKey();
            }
        }
    }
}
