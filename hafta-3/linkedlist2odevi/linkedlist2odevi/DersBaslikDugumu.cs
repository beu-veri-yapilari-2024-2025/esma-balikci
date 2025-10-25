using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linkedlist2odevi
{
    public class DersBaslikDugumu 
    {
        public string DersKodu;
        public DersBaslikDugumu SonrakiDers;
        public NotDugumu IlkOgrenci;
        public DersBaslikDugumu(string dersKodu)
        {
            this.DersKodu = dersKodu;
            this.SonrakiDers = null;
            this.IlkOgrenci = null;
        }
    }
}
