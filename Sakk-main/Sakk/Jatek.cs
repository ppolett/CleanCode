using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Jatek
    {
        public Tabla ChessBoard;
        public ColorEnum KiLep = ColorEnum.feher;
        public bool vege = false;

        public Jatek()
        {
            ChessBoard = new Tabla();
        }

        public void Kezd()
        {
            //fehér
            ChessBoard.BabuAdasaMezohoz(new Babu(ColorEnum.white, BabuTipusEnum.bastya), 1, 1);
            ChessBoard.BabuAdasaMezohoz(new Babu(ColorEnum.white, BabuTipusEnum.lo), 2, 1);
            ChessBoard.BabuAdasaMezohoz(new Babu(ColorEnum.white, BabuTipusEnum.futo), 3, 1);
            ChessBoard.BabuAdasaMezohoz(new Babu(ColorEnum.white, BabuTipusEnum.vezer), 4, 1);
            ChessBoard.BabuAdasaMezohoz(new Babu(ColorEnum.white, BabuTipusEnum.kiralyno), 5, 1);
            ChessBoard.BabuAdasaMezohoz(new Babu(ColorEnum.white, BabuTipusEnum.futo), 6, 1); ;
            ChessBoard.BabuAdasaMezohoz(new Babu(ColorEnum.white, BabuTipusEnum.lo), 7, 1);
            ChessBoard.BabuAdasaMezohoz(new Babu(ColorEnum.white, BabuTipusEnum.bastya), 8, 1);

            for (int i = 1; i <= 8; i++)
            {
                ChessBoard.BabuAdasaMezohoz(new Babu(ColorEnum.white, BabuTipusEnum.gyalog), i, 2);
            }

            //fekete
            ChessBoard.BabuAdasaMezohoz(new Babu(ColorEnum.black, BabuTipusEnum.bastya), 1, 8);
            ChessBoard.BabuAdasaMezohoz(new Babu(ColorEnum.black, BabuTipusEnum.lo), 2, 8);
            ChessBoard.BabuAdasaMezohoz(new Babu(ColorEnum.black, BabuTipusEnum.futo), 3, 8);
            ChessBoard.BabuAdasaMezohoz(new Babu(ColorEnum.black, BabuTipusEnum.kiralyno), 4, 8);
            ChessBoard.BabuAdasaMezohoz(new Babu(ColorEnum.black, BabuTipusEnum.vezer), 5, 8);
            ChessBoard.BabuAdasaMezohoz(new Babu(ColorEnum.black, BabuTipusEnum.futo), 6, 8); ;
            ChessBoard.BabuAdasaMezohoz(new Babu(ColorEnum.black, BabuTipusEnum.lo), 7, 8);
            ChessBoard.BabuAdasaMezohoz(new Babu(ColorEnum.black, BabuTipusEnum.bastya), 8, 8);

            for (int i = 1; i <= 8; i++)
            {
                ChessBoard.BabuAdasaMezohoz(new Babu(ColorEnum.black, BabuTipusEnum.gyalog), i, 7);
            }

            ChessBoard.Kirajzol();
        }

        public void JatekFuttatas() 
        {
            while (vege == false)
	        {
                string hibauzenet;
                Console.WriteLine("");
                if (KiLep == ColorEnum.feher)
	            {
                    Console.WriteLine("A FEHÉR LÉP");
	            }
                else
	            {
                    Console.WriteLine("A FEKETE LÉP");
	            }
                bool vegrehajthatoe = true;
                Console.Write("Adja meg a lépés kordinátáit(x1y1x2y2): ");
                string kordinatak = Console.ReadLine();
                if (kordinatak.Length == 4)
	            {
                    int kezx = KoordinataFordito(kordinatak[0].ToString());
                    int kezy = int.Parse(kordinatak[1].ToString());
                    int vegx = KoordinataFordito(kordinatak[2].ToString());
                    int vegy = int.Parse(kordinatak[3].ToString());
                    hibauzenet = Lepes(kezx, kezy, vegx, vegy);
                    if (hibauzenet != null)
	                {
                        vegrehajthatoe = false;
	                }
	            }
                else 
                { 
                    hibauzenet = "Hibás kordináták!";
                    vegrehajthatoe = false;
                }
                
                if (vegrehajthatoe == true)
	            {
                    if (KiLep == ColorEnum.feher)
	                {
                        KiLep = ColorEnum.fekete;
	                }
                    else
	                {
                        KiLep = ColorEnum.feher;
	                }
	            }
                ChessBoard.Kirajzol(hibauzenet);
	        }
        }

        public int KoordinataFordito(string x)
        {
            switch(x)
            {
                case "A": return 1;
                case "B": return 2;
                case "C": return 3;
                case "D": return 4;
                case "E": return 5;
                case "F": return 6;
                case "G": return 7;
                case "H": return 8;
                default: return 0;
            }
        }
        public string Lepes(int kezdox, int kezdoy, int vegx, int vegy)
        {
            Mezo kiindulomezo = ChessTable.KeresKoordinataAlapjan(kezdox, kezdoy);
            if (kiindulomezo.babu != null)
            {
                List<Mezo> idelephet = HovaLephetABabu(kiindulomezo);
                bool lephet = false;
                foreach (Mezo item in idelephet)
                {
                    if (item.X == vegx && item.Y == vegy)
                    {
                        lephet = true;
                    }
                }
                if (lephet == true)
                {
                    Babu elvettbabu = ChessBoard.BabuElvetelAMezobol(kezdox, kezdoy);
                    if (elvettbabu != null)
                    {
                        ChessBoard.BabuAdasaMezohoz(elvettbabu, vegx, vegy);
                        return null;
                    }
                    else
                    {
                        return "Nincs ott babu, a lepes nem lehetséges!";
                    }
                }
                else
                {
                    return "A babu nem léphet a megadott mezőre!";
                }
            }
            else
            {
                return "A kiinduló mezőn nincs bábu!";
            }
            
        }

        void Bastya(Mezo kiinduloMezo, int deltaX, int deltaY) { 
            bool validMezo = true;
            int startX = kiinduloMezo.X;
            int startY = kiinduloMezo.Y;
            while (validMezo)
            {
                startX += deltaX;
                startY += deltaY;
                if (startX < 9 && startX > 0 && startY < 9 && startY > 0)
                {
                    Mezo adottMezo = ChessBoard.KeresKoordinataAlapjan(startX, startY);
                    if(adottMezo.babu == null)
                    {
                        ideLephet.Add(adottMezo);
                    } 
                    else
                    {
                        if(adottMezo.babu.Color != kiinduloMezo.babu.Color)
                        {
                            ideLephet.Add(adottMezo);
                        }

                        validMezo = false;
                    }
                }
                else
                {
                    validMezo = false;
                }
            }
        }

        void Futo(Mezo kiindulomezo, int deltaX, int deltaY)
        {
            bool validMezo = true;
            int startX = kiinduloMezo.X;
            int startY = kiinduloMezo.Y;
            while (validMezo)
            {
            startX++;
            startY++;
            if (startX < 9 && startY < 9)
            {
                Mezo adottMezo = ChessBoard.KeresKoordinataAlapjan(startX, startY);
                if (adottMezo.babu == null)
                {
                    ideLephet.Add(adottMezo);
                }
                else
                {
                    if (adottMezo.babu.Color != kiinduloMezo.babu.Color)
                    {
                        ideLephet.Add(adottMezo);
                    }

                    validMezo = false;
                }
            }
            else 
            {
                validMezo = false;
            }
            }
        }
        
        void Gyalog(Mezo kiindulomezo, int deltaX, int deltaY)
        {
            if (kiinduloMezo.babu.Color == ColorEnum.feher)
                        {
                            if (kiinduloMezo.Y == 2)
                            {
                                ideLephet.Add(ChessBoard.KeresKoordinataAlapjan(kiinduloMezo.X, kiinduloMezo.Y + 2));
                                ideLephet.Add(ChessBoard.KeresKoordinataAlapjan(kiinduloMezo.X, kiinduloMezo.Y + 1));
                            }
                            else
                            {
                                if (kiinduloMezo.Y < 8)
                                {
                                    ideLephet.Add(ChessBoard.KeresKoordinataAlapjan(kiinduloMezo.X, kiinduloMezo.Y + 1));
                                }
                            }
                            if (kiinduloMezo.X > 1 && kiinduloMezo.Y < 8)
                            {
                                Mezo adottMezo = ChessBoard.KeresKoordinataAlapjan(kiinduloMezo.X - 1, kiinduloMezo.Y + 1);
                                if (adottMezo.babu != null && adottMezo.babu.Color != kiinduloMezo.babu.Color)
                                {
                                    ideLephet.Add(adottMezo);
                                }
                            }
                            if (kiinduloMezo.X < 8 && kiinduloMezo.Y < 8)
                            {
                                Mezo adottMezo = ChessBoard.KeresKoordinataAlapjan(kiinduloMezo.X + 1, kiinduloMezo.Y + 1);
                                if (adottMezo.babu != null && adottMezo.babu.Color != kiinduloMezo.babu.Color)
                                {
                                    ideLephet.Add(adottMezo);
                                }
                            }
                        }
                        else
                        {
                            if (kiinduloMezo.Y == 7)
                            {
                                ideLephet.Add(ChessBoard.KeresKoordinataAlapjan(kiinduloMezo.X, kiinduloMezo.Y - 2));
                                ideLephet.Add(ChessBoard.KeresKoordinataAlapjan(kiinduloMezo.X, kiinduloMezo.Y - 1));
                            }
                            else
                            {
                                if (kiinduloMezo.Y > 1)
                                {
                                    ideLephet.Add(ChessBoard.KeresKoordinataAlapjan(kiinduloMezo.X, kiinduloMezo.Y - 1));
                                }
                            }
                            if (kiinduloMezo.X > 1 && kiinduloMezo.Y > 1)
                            {
                                Mezo adottMezo = ChessBoard.KeresKoordinataAlapjan(kiinduloMezo.X - 1, kiinduloMezo.Y - 1);
                                if (adottMezo.babu != null && adottMezo.babu.ColorkiinduloMezo.babu.Color)
                                {
                                    ideLephet.Add(adottMezo);
                                }
                            }
                            if (kiinduloMezo.X < 8 && kiinduloMezo.Y > 1)
                            {
                                Mezo adottMezo = ChessBoard.KeresKoordinataAlapjan(kiinduloMezo.X + 1, kiinduloMezo.Y - 1);
                                if (adottMezo.babu != null && adottMezo.babu.Color != kiinduloMezo.babu.Color)
                                {
                                    ideLephet.Add(adottMezo);
                                }
                            }
                        }
        }
        
        public List<Mezo> HovaLephetABabu(Mezo kiinduloMezo)
        {
            List<Mezo> ideLephet = new List<Mezo>();
            switch (kiinduloMezo.babu.BabuTipus)
            {
                case BabuTipusEnum.bastya:
                    {
                        Bastya(1,0);
                        Bastya(-1,0);
                        Bastya(0,-1);
                        Bastya(0,1);
                        break;
                    }
                case BabuTipusEnum.futo:
                    {
                        Futo(1,0);
                        Futo(-1,0);
                        Futo(0,-1);
                        Futo(0,1);
                    }
                case BabuTipusEnum.gyalog:
                    {
                        Gyalog(1,0);
                        Gyalog(-1,0);
                        Gyalog(0,-1);
                        Gyalog(0,1);
                        break;
                    }
                case BabuTipusEnum.kiralyno:
                    {
                        Futo(1,0);
                        Futo(-1,0);
                        Futo(0,-1);
                        Futo(0,1);
                        Bastya(1,0);
                        Bastya(-1,0);
                        Bastya(0,-1);
                        Bastya(0,1);
                        break;
                    }
                case BabuTipusEnum.lo:
                    {
                        if (kiinduloMezo.X < 7)
                        {
                            if (kiinduloMezo.Y > 1)
                            {
                                Mezo adottMezo = ChessBoard.KeresKoordinataAlapjan(kiinduloMezo.X + 2, kiinduloMezo.Y - 1);
                                if (adottMezo.babu == null || adottMezo.babu.Color != kiinduloMezo.babu.Color)
                                {
                                    ideLephet.Add(adottMezo);
                                }
                            }
                            if (kiinduloMezo.Y < 8)
                            {
                                Mezo adottMezo = ChessBoard.KeresKoordinataAlapjan(kiinduloMezo.X + 2, kiinduloMezo.Y + 1);
                                if (adottMezo.babu == null || adottMezo.babu.Color != kiinduloMezo.babu.Color)
                                {
                                    ideLephet.Add(adottMezo);
                                }
                            }
                        }
                        if (kiinduloMezo.X > 2)
                        {
                            if (kiinduloMezo.Y > 1)
                            {
                                Mezo adottMezo = ChessBoard.KeresKoordinataAlapjan(kiinduloMezo.X - 2, kiinduloMezo.Y - 1);
                                if (adottMezo.babu == null || adottMezo.babu.Color != kiinduloMezo.babu.Color)
                                {
                                    ideLephet.Add(adottMezo);
                                }
                            }
                            if (kiinduloMezo.Y < 8)
                            {
                                Mezo adottMezo = ChessBoard.KeresKoordinataAlapjan(kiinduloMezo.X - 2, kiinduloMezo.Y + 1);
                                if (adottMezo.babu == null || adottMezo.babu.Color != kiinduloMezo.babu.Color)
                                {
                                    ideLephet.Add(adottMezo);
                                }
                            }
                        }
                        if (kiinduloMezo.Y < 7)
                        {
                            if (kiinduloMezo.X > 1)
                            {
                                Mezo adottMezo = ChessBoard.KeresKoordinataAlapjan(kiinduloMezo.X - 1, kiinduloMezo.Y + 2);
                                if (adottMezo.babu == null || adottMezo.babu.Color != kiinduloMezo.babu.Color)
                                {
                                    ideLephet.Add(adottMezo);
                                }
                            }
                            if (kiinduloMezo.X < 8)
                            {
                                Mezo adottMezo = ChessBoard.KeresKoordinataAlapjan(kiinduloMezo.X + 1, kiinduloMezo.Y + 2);
                                if (adottMezo.babu == null || adottMezo.babu.Color != kiinduloMezo.babu.Color)
                                {
                                    ideLephet.Add(adottMezo);
                                }
                            }
                        }
                        if (kiinduloMezo.Y > 2)
                        {
                            if (kiinduloMezo.X > 1)
                            {
                                Mezo adottMezo = ChessBoard.KeresKoordinataAlapjan(kiinduloMezo.X - 1, kiinduloMezo.Y - 2);
                                if (adottMezo.babu == null || adottMezo.babu.Color != kiinduloMezo.babu.Color)
                                {
                                    ideLephet.Add(adottMezo);
                                }
                            }
                            if (kiinduloMezo.X < 8)
                            {
                                Mezo adottMezo = ChessBoard.KeresKoordinataAlapjan(kiinduloMezo.X + 1, kiinduloMezo.Y - 2);
                                if (adottMezo.babu == null || adottMezo.babu.Color != kiinduloMezo.babu.Color)
                                {
                                    ideLephet.Add(adottMezo);
                                }
                            }
                        }
                        break;
                    }
                case BabuTipusEnum.vezer:
                    {
                        if (kiinduloMezo.X > 1)
                        {
                            Mezo adottMezo = ChessBoard.KeresKoordinataAlapjan(kiinduloMezo.X - 1, kiinduloMezo.Y);
                            if (adottMezo.babu == null || adottMezo.babu.Color != kiinduloMezo.babu.Color)
                            {
                                ideLephet.Add(adottMezo);
                            }
                            if (kiinduloMezo.Y > 1)
                            {
                                adottMezo = ChessBoard.KeresKoordinataAlapjan(kiinduloMezo.X - 1, kiinduloMezo.Y -1);
                                if (adottMezo.babu == null || adottMezo.babu.Color != kiinduloMezo.babu.Color)
                                {
                                    ideLephet.Add(adottMezo);
                                }
                            }
                            if (kiinduloMezo.Y < 8)
                            {
                                adottMezo = ChessBoard.KeresKoordinataAlapjan(kiinduloMezo.X - 1, kiinduloMezo.Y +1);
                                if (adottMezo.babu == null || adottMezo.babu.Color != kiinduloMezo.babu.Color)
                                {
                                    ideLephet.Add(adottMezo);
                                }
                            }
                        }
                        if (kiinduloMezo.Y > 1)
                        {
                            Mezo adottMezo = ChessBoard.KeresKoordinataAlapjan(kiinduloMezo.X, kiinduloMezo.Y - 1);
                            if (adottMezo.babu == null || adottMezo.babu.Color != kiinduloMezo.babu.Color)
                            {
                                ideLephet.Add(adottMezo);
                            }
                        }
                        if (kiinduloMezo.Y < 8)
                        {
                            Mezo adottMezo = ChessBoard.KeresKoordinataAlapjan(kiinduloMezo.X, kiinduloMezo.Y + 1);
                            if (adottMezo.babu == null || adottMezo.babu.Color != kiinduloMezo.babu.Color)
                            {
                                ideLephet.Add(adottMezo);
                            }
                        }
                        if (kiinduloMezo.X < 8)
                        {
                            Mezo adottMezo = ChessBoard.KeresKoordinataAlapjan(kiinduloMezo.X + 1, kiinduloMezo.Y);
                            if (adottMezo.babu == null || adottMezo.babu.Color != kiinduloMezo.babu.Color)
                            {
                                ideLephet.Add(adottMezo);
                            }
                            if (kiinduloMezo.Y > 1)
                            {
                                adottMezo = ChessBoard.KeresKoordinataAlapjan(kiinduloMezo.X + 1, kiinduloMezo.Y - 1);
                                if (adottMezo.babu == null || adottMezo.babu.Color != kiinduloMezo.babu.Color)
                                {
                                    ideLephet.Add(adottMezo);
                                }
                            }
                            if (kiinduloMezo.Y < 8)
                            {
                                adottMezo = ChessBoard.KeresKoordinataAlapjan(kiinduloMezo.X + 1, kiinduloMezo.Y + 1);
                                if (adottMezo.babu == null || adottMezo.babu.Color != kiinduloMezo.babu.Color)
                                {
                                    ideLephet.Add(adottMezo);
                                }
                            }
                        }
                        break;
                    }
            }

            foreach(Mezo item in ideLephet)
            {
                Console.Write(item.X.ToString() + "." + item.Y.ToString());
            }

            return ideLephet;
        }
    }
}
