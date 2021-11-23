using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Babu: IBabu
    {
        public ColorEnum Color;
        public BabuTipusEnum BabuTipus;

        public Babu(SzinEnum color, BabuTipusEnum babuTipus)
        {
            Color = color;
            BabuTipus = babuTipus;
        }
    }
}
