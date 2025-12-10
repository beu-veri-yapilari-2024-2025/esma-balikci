using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructuresProject
{
    // Düğüm Sınıfı
    public class Node
    {
        public char Data;
        public Node Left, Right;
        public int Height;
        public int Frequency; 

        public Node(char data)
        {
            Data = data;
            Height = 1;
            Frequency = 0;
            Left = null;
            Right = null;
        }
    }

    public class AdvancedTree
    {
        public Node Root;

        private int CompareChars(char a, char b)
        {
            string alphabet = "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZ";
            int indexA = alphabet.IndexOf(char.ToUpper(a));
            int indexB = alphabet.IndexOf(char.ToUpper(b));
            return indexA.CompareTo(indexB);
        }

        // --- AVL FONKSİYONLARI ---

        public int GetHeight(Node n) => n == null ? 0 : n.Height;

        public int GetBalance(Node n) => n == null ? 0 : GetHeight(n.Left) - GetHeight(n.Right);

        // Sağa Döndürme (LL Durumu için)
        public Node RotateRight(Node y)
        {
            Node x = y.Left;
            Node T2 = x.Right;

            // Döndürme
            x.Right = y;
            y.Left = T2;

            y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;
            x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;

            return x;
        }

        // Sola Döndürme (RR Durumu için)
        public Node RotateLeft(Node x)
        {
            Node y = x.Right;
            Node T2 = y.Left;

            // Döndürme
            y.Left = x;
            x.Right = T2;

            x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;
            y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;

            return y;
        }

        public void Insert(char data)
        {
            Root = InsertRec(Root, data);
        }

        private Node InsertRec(Node node, char data)
        {
            if (node == null) return new Node(data);

            if (CompareChars(data, node.Data) < 0)
                node.Left = InsertRec(node.Left, data);
            else if (CompareChars(data, node.Data) > 0)
                node.Right = InsertRec(node.Right, data);
            else
                return node; // Aynı veri eklenmez

            node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));

            // Denge Faktörü
            int balance = GetBalance(node);

            // 1. Sol Sol Durumu (LL) -> Sağa döndür
            if (balance > 1 && CompareChars(data, node.Left.Data) < 0)
            {
                Console.WriteLine($"AVL Dengeleme: {node.Data} üzerinde Sağa Döndürme (LL Durumu)");
                return RotateRight(node);
            }

            // 2. Sağ Sağ Durumu (RR) -> Sola döndür
            if (balance < -1 && CompareChars(data, node.Right.Data) > 0)
            {
                Console.WriteLine($"AVL Dengeleme: {node.Data} üzerinde Sola Döndürme (RR Durumu)");
                return RotateLeft(node);
            }

            // 3. Sol Sağ Durumu (LR) -> Önce Sola, Sonra Sağa
            if (balance > 1 && CompareChars(data, node.Left.Data) > 0)
            {
                Console.WriteLine($"AVL Dengeleme: {node.Left.Data} üzerinde Sola, sonra {node.Data} üzerinde Sağa (LR Durumu)");
                node.Left = RotateLeft(node.Left);
                return RotateRight(node);
            }

            // 4. Sağ Sol Durumu (RL) -> Önce Sağa, Sonra Sola
            if (balance < -1 && CompareChars(data, node.Right.Data) < 0)
            {
                Console.WriteLine($"AVL Dengeleme: {node.Right.Data} üzerinde Sağa, sonra {node.Data} üzerinde Sola (RL Durumu)");
                node.Right = RotateRight(node.Right);
                return RotateLeft(node);
            }

            return node;
        }

        // --- DSW ALGORİTMASI FONKSİYONLARI ---

        public void ApplyDSW()
        {
            Console.WriteLine("\n--- DSW Algoritması Başlatılıyor ---");
            CreateBackbone();
            Console.WriteLine("Backbone oluşturuldu (Ağaç sağa yatık çizgi haline geldi).");
            BalanceBackbone();
            Console.WriteLine("BalanceBackbone tamamlandı (Ağaç yeniden dengelendi).");
        }

        public void CreateBackbone()
        {
            Node grandParent = null;
            Node temp = Root;
            Node leftChild;

            while (temp != null)
            {
                leftChild = temp.Left;
                if (leftChild != null)
                {
                    // Sağa döndürme işlemi 
                    Node oldTemp = temp;
                    temp = leftChild;
                    oldTemp.Left = temp.Right;
                    temp.Right = oldTemp;

                    if (grandParent != null)
                        grandParent.Right = temp;
                    else
                        Root = temp; // Yeni root
                }
                else
                {
                    grandParent = temp;
                    temp = temp.Right;
                }
            }
        }

        public void BalanceBackbone()
        {
            int nodeCount = 0;
            Node temp = Root;
            while (temp != null)
            {
                nodeCount++;
                temp = temp.Right;
            }

            // M = 2^floor(log2(N+1)) - 1
            int m = (int)Math.Pow(2, Math.Floor(Math.Log(nodeCount + 1, 2))) - 1;

            Compress(nodeCount - m);

            while (m > 1)
            {
                m = m / 2;
                Compress(m);
            }
        }

        private void Compress(int count)
        {
            Node grandParent = null;
            Node temp = Root;

            for (int i = 0; i < count; i++)
            {
                if (temp == null || temp.Right == null) break;

                Node child = temp.Right;
                temp.Right = child.Right;
                child.Right = temp;
                child.Left = temp.Left; 
                temp.Left = null;

                if (grandParent != null)
                    grandParent.Right = child;
                else
                    Root = child;

                grandParent = child;
                temp = child.Right;
            }
        }

        // --- SELF-ADJUSTING TREE FONKSİYONLARI ---

        public void SearchWithFrequency(char key)
        {
            Console.WriteLine($"\n'{key}' aranıyor...");
            Root = SearchAndAdjust(Root, key);
        }

        private Node SearchAndAdjust(Node node, char key)
        {
            if (node == null) return null;

            int compare = CompareChars(key, node.Data);

            if (compare == 0)
            {
                node.Frequency++; // Frekansı artır
                Console.WriteLine($"Bulundu: {node.Data}, Yeni Frekans: {node.Frequency}");
                return node;
            }
            else if (compare < 0)
            {
                node.Left = SearchAndAdjust(node.Left, key);
                // Priority Rotation: Eğer çocuğun frekansı ebeveyninden büyükse yukarı taşı
                if (node.Left != null && node.Left.Frequency > node.Frequency)
                {
                    Console.WriteLine($"Priority Rotate: {node.Left.Data} ({node.Left.Frequency}) > {node.Data} ({node.Frequency}) -> Sağa Dönüş");
                    return RotateRight(node);
                }
            }
            else
            {
                node.Right = SearchAndAdjust(node.Right, key);
                if (node.Right != null && node.Right.Frequency > node.Frequency)
                {
                    Console.WriteLine($"Priority Rotate: {node.Right.Data} ({node.Right.Frequency}) > {node.Data} ({node.Frequency}) -> Sola Dönüş");
                    return RotateLeft(node);
                }
            }
            return node;
        }

        // --- YAZDIRMA FONKSİYONLARI ---

        public void PrintLevelOrder()
        {
            if (Root == null) return;
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(Root);
            while (queue.Count > 0)
            {
                Node current = queue.Dequeue();
                Console.Write($"{current.Data}(F:{current.Frequency}) ");
                if (current.Left != null) queue.Enqueue(current.Left);
                if (current.Right != null) queue.Enqueue(current.Right);
            }
            Console.WriteLine();
        }

        public void PrintTreePretty(string indent, Node last, bool isLeft)
        {
            if (last != null)
            {
                Console.Write(indent);
                if (isLeft)
                {
                    Console.Write("L---- ");
                    indent += "|     ";
                }
                else
                {
                    Console.Write("R---- ");
                    indent += "      ";
                }
                Console.WriteLine($"{last.Data} (Freq: {last.Frequency})");
                PrintTreePretty(indent, last.Left, true);
                PrintTreePretty(indent, last.Right, false);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            AdvancedTree tree = new AdvancedTree();
            char[] inputs = { 'S', 'E', 'L', 'İ', 'M', 'K', 'A', 'Ç', 'T', 'I' };

            Console.WriteLine("1. ADIM: AVL AĞACI OLUŞTURULUYOR...");
            Console.WriteLine("-------------------------------------");
            foreach (char c in inputs)
            {
                tree.Insert(c);
            }

            Console.WriteLine("\n[AVL AĞACI SON HALİ]");
            tree.PrintTreePretty("", tree.Root, false);
            Console.WriteLine("Kök Düğüm: " + tree.Root.Data + " (L olmalı)");

            Console.WriteLine("\n2. ADIM: DSW METODU UYGULANIYOR...");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("Bilgi: DSW, ağacı önce Backbone (sağa yatık) hale getirir, sonra mükemmel dengeye kavuşturur.");

            tree.ApplyDSW();

            Console.WriteLine("\n[DSW SONRASI AĞAÇ]");
            tree.PrintTreePretty("", tree.Root, false);

            Console.WriteLine("\n3. ADIM: SELF-ADJUSTING (FREQUENCY) TESTİ");
            Console.WriteLine("-------------------------------------");

            Console.WriteLine("Senaryo: 'A' karakterine 5 kez, 'K' karakterine 2 kez erişiliyor.");

            for (int i = 0; i < 5; i++) tree.SearchWithFrequency('A');
            for (int i = 0; i < 2; i++) tree.SearchWithFrequency('K');

            Console.WriteLine("\n[SELF-ADJUSTING SONRASI AĞAÇ]");
            tree.PrintTreePretty("", tree.Root, false);

            Console.WriteLine("\nProgram sonlandı. Çıkmak için bir tuşa basın.");
            Console.ReadKey();
        }
    }
}