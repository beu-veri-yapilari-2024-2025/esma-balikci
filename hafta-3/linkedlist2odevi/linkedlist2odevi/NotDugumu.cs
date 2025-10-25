using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linkedlist2odevi
{
    public class NotDugumu
    {
        public int OgrenciNumarasi;
        public string DersKodu;
        public string HarfNotu;

        public NotDugumu OgrencininSonrakiDersi;

        public NotDugumu DerstekiSonrakiOgrenci;

        public NotDugumu(int ogrenciNo, string dersKodu, string harfNotu)
        {
            this.OgrenciNumarasi = ogrenciNo;
            this.DersKodu = dersKodu;
            this.HarfNotu = harfNotu;
            this.OgrencininSonrakiDersi = null;
            this.DerstekiSonrakiOgrenci = null;
        }
    }
}
