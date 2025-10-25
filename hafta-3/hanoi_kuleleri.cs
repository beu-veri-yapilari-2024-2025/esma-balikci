using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hanoikuleleri
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("n değerini giriniz : ");
            int n = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"\n{n} disk için çözüm adımları (A -> C):");
            Tasi(n, 'A', 'C', 'B');
            Console.ReadKey();
        }
        public static void Tasi(int n, char kaynak, char hedef, char yardimci)
        {
            if (n == 1)
            {
                Console.WriteLine($"Diski {kaynak} çubuğundan {hedef} çubuğuna taşı.");
            }
            else
            {
                Tasi(n - 1, kaynak, yardimci, hedef);

                Console.WriteLine($"Diski {kaynak} çubuğundan {hedef} çubuğuna taşı.");

                Tasi(n - 1, yardimci, hedef, kaynak);
            }
        }
    }
}
