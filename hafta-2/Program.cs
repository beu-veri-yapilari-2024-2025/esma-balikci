<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace veriyapilari1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] dizi = { 3, 7, 10, 15, 19 };
            int sayi = 7;
            int sonuc = ikiliarama(dizi,dizi.Length,sayi);

            if (sonuc != -1)
            {
                Console.WriteLine($"Sayı {sayi} dizinin {sonuc+1}. elemanıdır.");
            }
            else
            {
                Console.WriteLine("Sayı bulunamadı.");
            }
            Console.ReadKey();
        }
        public static int ikiliarama(int[] A,int n, int sayi)
        {
            int sol = 0;
            int sag = n - 1;

            while (sol <= sag)
            {
                int orta=(sol+sag)/2;
                if (A[orta] == sayi)
                {
                    return orta;
                }
                else if (sayi < A[orta])
                {
                    sag = orta - 1;
                }
                else
                {
                    sol = orta + 1;
                }
            }
            return -1;
        }
    }
}
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bagli_liste
{
    internal class Program
    {
        //Node Sınıfı: Her bir düğüm bir veriye ve bir sonraki düğüme işaret eder
        //veri yapısı tanımladık
        public class Node //Düğüm tanımlama
        {
            public int data; //verimiz
            public Node Next; //Node türünde sonraki pointerı

            public Node(int _data) // gönderilen değeri node a dönüştürür
            {
                data = _data;
                Next = null;
            }
        }

        //Linkedlist sınıfı

        public class BagliList
        {
            private Node head;
            private Node tail;

            public BagliList()
            {
                head = null;
                tail = null;
            }
            //başa eleman ekleme 
            public void BasaEkle(int value)
            {
                Node newNode = new Node(value);//noda dönüştür
                newNode.Next = head;
                head = newNode;
                Console.WriteLine($"{value} başa eklendi.");
            }
            //sona eleman ekleme 
            public void sonaEkle(int value)
            {
                Node newNode = new Node(value);
                if (head == null) // içinde başka eleman yoksa
                {
                    head = newNode;
                    tail= newNode;
                    Console.WriteLine($"{value} sona eklendi");
                    return;
                }
                Node current = head;
                while ( current.Next != null ) // boş değilse devam et
                {
                    current= current.Next;
                }
                current.Next = newNode;
                tail= newNode;
                Console.WriteLine($"{value} sona eklendi.");
            }

            //Eleman arama

            public void ara(int value)
            {
                Node current = head;
                while ( current!=head )
                {
                    if (current.data == value)
                    {
                        Console.WriteLine($"{value} listede bulunuyor.");
                        return;
                    }
                    current = current.Next;
                }
                Console.WriteLine($"{value} listede bulunmuyor.");
            }
            //yazdırma
            public void display()
            {
                if (head == null)
                {
                    Console.WriteLine("Liste boş");
                    return;
                }
                Node current = head;
                Console.Write("Liste: ");
                while ( current != null )
                {
                    Console.Write(current.data+"--->");
                    current= current.Next;
                }
                Console.WriteLine();
            }

            //belirli bir değerin sonrasına eleman ekleme
            public void BelirliDegerSonrasiEkle(int arananDeger, int yeniDeger)
            {
                Node current = head;
                while (current != null)
                {
                    if (current.data == arananDeger)
                    {
                        Node newNode = new Node(yeniDeger);
                        newNode.Next = current.Next;
                        current.Next = newNode;

                        // Eğer son elemandan sonra ekleme yapıldıysa tail'i güncelle
                        if (newNode.Next == null)
                        {
                            tail = newNode;
                        }

                        Console.WriteLine($"{yeniDeger}, {arananDeger} değerinin sonrasına eklendi");
                        return;
                    }
                    current = current.Next;
                }
                Console.WriteLine($"{arananDeger} değeri listede bulunamadı, ekleme yapılamadı");
            }

        }

        static void Main(string[] args)
        {
            BagliList liste = new BagliList();
            int secim;

            do
            {
                Console.WriteLine("\n--- Bağlı Liste İşlemleri ---");
                Console.WriteLine("1. Başa eleman ekle");
                Console.WriteLine("2. Sona eleman ekle");
                Console.WriteLine("3. Belirli değerin sonrasına ekle");
                Console.WriteLine("4. Eleman ara");
                Console.WriteLine("5. Listeyi görüntüle");
                Console.WriteLine("0. Çıkış");
                Console.Write("Seçiminizi yapın: ");

                if (int.TryParse(Console.ReadLine(), out secim))
                {
                    switch (secim)
                    {
                        case 1:
                            Console.Write("Başa eklenecek değeri girin: ");
                            if (int.TryParse(Console.ReadLine(), out int basaDeger))
                            {
                                liste.BasaEkle(basaDeger);
                            }
                            else
                            {
                                Console.WriteLine("Geçersiz değer girdiniz!");
                            }
                            break;

                        case 2:
                            Console.Write("Sona eklenecek değeri girin: ");
                            if (int.TryParse(Console.ReadLine(), out int sonaDeger))
                            {
                                liste.sonaEkle(sonaDeger);
                            }
                            else
                            {
                                Console.WriteLine("Geçersiz değer girdiniz!");
                            }
                            break;

                        case 3:
                            Console.Write("Aranan değeri girin: ");
                            if (int.TryParse(Console.ReadLine(), out int arananDeger))
                            {
                                Console.Write("Eklenecek yeni değeri girin: ");
                                if (int.TryParse(Console.ReadLine(), out int yeniDeger))
                                {
                                    liste.BelirliDegerSonrasiEkle(arananDeger, yeniDeger);
                                }
                                else
                                {
                                    Console.WriteLine("Geçersiz değer girdiniz!");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Geçersiz değer girdiniz!");
                            }
                            break;

                        case 4:
                            Console.Write("Aranacak değeri girin: ");
                            if (int.TryParse(Console.ReadLine(), out int aranacakDeger))
                            {
                                liste.ara(aranacakDeger);
                            }
                            else
                            {
                                Console.WriteLine("Geçersiz değer girdiniz!");
                            }
                            break;

                        case 5:
                            liste.display();
                            break;

                        case 0:
                            Console.WriteLine("Programdan çıkılıyor...");
                            break;

                        default:
                            Console.WriteLine("Geçersiz seçim! Lütfen 0-5 arasında bir değer girin.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Geçersiz giriş! Lütfen sayısal bir değer girin.");
                    secim = -1; // Döngünün devam etmesi için
                }

            } while (secim != 0);
        }
    }
}

>>>>>>> b589167759233ebe661bd2fb99be9fff0039ecb5
