using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakk
{
    class Program
    {
        public static Jatek AktualisJatek;
        static void Main(string[] args)
        {
            AktualisJatek = new Jatek();
            AktualisJatek.Kezd();
            AktualisJatek.JatekFuttatas();

            Console.ReadLine();
        }
    }
}
