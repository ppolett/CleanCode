using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Tabla
    {
        private readonly int xMax = 8;
        private readonly int yMax = 8;

        private readonly List<Mezo> mezok = new List<Mezo>();

        private readonly ConsoleColor alapHatterColor;
        private readonly ConsoleColor alapSzovegColor;

        public Tabla() 
        {
            for (int x = 1; x <= xMax; x++)
            {
                for (int y = 1; y <= yMax; y++)
                {
                    mezok.Add(new Mezo(x, y));
                }
            }

            alapHatterColor = Console.BackgroundColor;
            alapSzovegColor = Console.ForegroundColor;
        }

        public void Kirajzol(string hibauzenet = null)
        {
            Console.Clear();

            Console.WriteLine("  X A   B   C   D   E   F   G   H  ");
            Console.WriteLine("Y =================================");
            for (int x = 1; x <= xMax; x++)
            {
                Console.Write(x.ToString() + " |");
                for (int y = 1; y <= yMax; y++)
                {
                    Mezo jelenlegiMezo = KeresKoordinataAlapjan(y, x);

                    jelenlegiMezo.Kirajzol();

                    Console.BackgroundColor = alapHatterColor;
                    Console.ForegroundColor = alapSzovegColor;

                    Console.Write("|");
                }
                Console.WriteLine("");
                Console.WriteLine("  =================================");
            }
            if (hibauzenet != null )
	        {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("");
                Console.WriteLine(hibauzenet);
                Console.WriteLine("");
                Console.BackgroundColor = alapHatterColor;
                Console.ForegroundColor = alapSzovegColor;
	        }
        }

        public Mezo KeresKoordinataAlapjan(int x, int y)
        {
            foreach(Mezo mezo in mezok)
            {
                if(mezo.X == x && mezo.Y == y)
                {
                    return mezo;
                }
            }
            return null;
        }

        public void BabuAdasaMezohoz(Babu babu, int x, int y)
        {
            KeresKoordinataAlapjan(x,y).babu = babu;
        }

        public Babu BabuElvetelAMezobol(int x, int y) 
        {
            Babu result = KeresKoordinataAlapjan(x,y).babu;
            KeresKoordinataAlapjan(x,y).babu = null;
            return result;
        }
    }
}
