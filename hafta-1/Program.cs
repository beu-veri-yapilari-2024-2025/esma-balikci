using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace veriyapilariödevi
{
    internal class Program
    {
        //Recursive ikili arama: 
        public static int Search(int[] dizi, int aradigimizSayi, int sol, int sag) 
        {
            if (sag >= sol)
            {
                int orta = sol + (sag - sol) / 2; 
                if (dizi[orta] == aradigimizSayi) 
                {
                    return orta;
                }
                else if (dizi[orta] > aradigimizSayi)  
                {
                    return Search(dizi, aradigimizSayi, sol, orta - 1);
                }
                else
                {
                    return Search(dizi, aradigimizSayi, orta + 1, sag); 
                }
            }
            return -1; 
        }
        static void Main(string[] args)
        {
            // Dizideki sayıların toplamını bulma

            int[] dizi2 = { 2, 5, 6, 8, 11 };
            int toplam = 0;
            for (int i = 0; i < dizi2.Length; i++)
            {
                toplam += dizi2[i];
            }
            Console.WriteLine($"Dizideki sayıların toplamı: {toplam}");

            //Matris çarpımı

            int[,] matris1 = { { 2, 5, 1 }, { 3, 2, 4 }, { 1, 3, 5 } };
            int[,] matris2 = { { 2, 5, 3 }, { 4, 2, 4 }, { 3, 4, 1 } };
            int[,] carpim = new int[3, 3];

            for (int i = 0; i < carpim.GetLength(0); i++)
            {
                for (int j = 0; j < carpim.GetLength(1); j++)
                {
                    carpim[i, j] += 0;
                    for (int k = 0; k < 3; k++)
                    {
                        carpim[i, j] += matris1[i, k] * matris2[k, j];
                    }
                }
            }
            for (int i = 0; i < matris1.GetLength(0); i++)
            {
                for (int j = 0; j < matris2.GetLength(1); j++)
                {
                    Console.Write(carpim[i, j] + " ");
                }
                Console.WriteLine();
            }

            //Dizideki elemanların aranması

            int[] dizi = { 3, 7, 10, 15, 19 };
            int sayi = 7;
            int sonuc = ikiliarama(dizi, dizi.Length, sayi);

            if (sonuc != -1)
            {
                Console.WriteLine($"Sayı {sayi} dizinin {sonuc + 1}. elemanıdır.");
            }
            else
            {
                Console.WriteLine("Sayı bulunamadı.");
            }
            Console.ReadKey();

        }
    public static int ikiliarama(int[] A, int n, int sayi)
    {
        int sol = 0;
        int sag = n - 1;

        while (sol <= sag)
        {
            int orta = (sol + sag) / 2;
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
