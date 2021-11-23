using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakk
{
    public class Babu: IBabu
    {
        public SzinEnum Szin;
        public BabuTipusEnum BabuTipus;

        public Babu(SzinEnum szin, BabuTipusEnum babuTipus)
        {
            Szin = szin;
            BabuTipus = babuTipus;
        }


    }
}
