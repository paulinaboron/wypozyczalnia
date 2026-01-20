using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WypozyczalniaNarciarska
{

    [DataContract]
    public class Snowboard : SprzetNarciarski
    {
        public int Rozmiar { get; }
        public int Dlugosc { get; }
        public bool DlaDziecka { get; }
        public int RokProdukcji { get; }

        public Snowboard(string producent, decimal cenaZaDzien, int rozmiar, int dlugosc, bool dlaDziecka, int rokProdukcji) : base(producent, cenaZaDzien)
        {
            if (rozmiar <= 0)
                throw new NiepoprawneDaneSprzetuException("Rozmiar snowboardu musi być dodatni.");
            if (dlugosc < 90 || dlugosc > 180)
                throw new NiepoprawneDaneSprzetuException("Długość snowboardu musi być w zakresie od 90 cm do 180 cm.");
            if (rokProdukcji < 1970 || rokProdukcji > DateTime.Now.Year)
                throw new NiepoprawneDaneSprzetuException("Niepoprawny rok produkcji snowboardu");

            Rozmiar = rozmiar;
            Dlugosc = dlugosc;
            DlaDziecka = dlaDziecka;
            RokProdukcji = rokProdukcji;
        }

        public override decimal ObliczKoszt(int liczbaDni)
        {
            decimal koszt = base.ObliczKoszt(liczbaDni);

            if (DlaDziecka)
                koszt *= 0.85m; // 15% zniżki dla dzieci

            return koszt;
        }

        public override string Opis()
        {
            return base.Opis() +
                $" | Snowboard, {Dlugosc} cm, rozmiar {Rozmiar}, " + (DlaDziecka ? "dziecięcy (zniżka)" : "dla dorosłych");
        }
    }
}
