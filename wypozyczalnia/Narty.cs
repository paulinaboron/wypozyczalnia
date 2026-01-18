using System;

namespace WypozyczalniaNarciarska
{
    public class Narty : SprzetNarciarski
    {
        public int Rozmiar { get; }
        public int Dlugosc { get; }
        public TypNart Typ { get; }
        public bool DlaDziecka { get; }
        public int RokProdukcji { get; }

        public Narty(string producent, decimal cenaZaDzien, int rozmiar, int dlugosc, TypNart typ, bool dlaDziecka, int rokProdukcji) : base(producent, cenaZaDzien)
        {
            if (rozmiar <= 0)
                throw new NiepoprawneDaneSprzetuException("Rozmiar nart musi być dodatni.");
            if (dlugosc < 80 || dlugosc > 220)
                throw new NiepoprawneDaneSprzetuException("Długość nart musi być w zakresie od 80 cm do 220 cm.");
            if (rokProdukcji < 1970 || rokProdukcji > DateTime.Now.Year)
                throw new NiepoprawneDaneSprzetuException("Niepoprawny rok produkcji nart");

            Rozmiar = rozmiar;
            Dlugosc = dlugosc;
            Typ = typ;
            DlaDziecka = dlaDziecka;
            RokProdukcji = rokProdukcji;
        }

        public override decimal ObliczKoszt(int liczbaDni)
        {
            decimal koszt = base.ObliczKoszt(liczbaDni);

            if (DlaDziecka)
                koszt *= 0.8m; // 20% zniżki dla dzieci

            return koszt;
        }

        public override string Opis()
        {
            return base.Opis() +
                $" | Narty {Typ}, {Dlugosc} cm, rozmiar {Rozmiar}, " + (DlaDziecka ? "dziecięce (zniżka)" : "dla dorosłych");
        }
    }
}
