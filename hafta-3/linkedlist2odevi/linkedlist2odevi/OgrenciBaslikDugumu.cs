using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linkedlist2odevi
{
    public class OgrenciBaslikDugumu
    {
        public int OgrenciNumarasi;
        public OgrenciBaslikDugumu SonrakiOgrenci;
        public NotDugumu IlkDers;


        public OgrenciBaslikDugumu(int ogrenciNo)
        {
            this.OgrenciNumarasi = ogrenciNo;
            this.SonrakiOgrenci = null;
            this.IlkDers = null;
        }
    }
}
