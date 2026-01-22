using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WypozyczalniaNarciarska
{
    /// <summary>
    /// Reprezentuje snowboard dostępny w wypożyczalni.
    /// </summary>

    [DataContract]
    public class Snowboard : SprzetNarciarski
    {
        [DataMember]
        public int Rozmiar { get; set; }
        [DataMember]
        public int Dlugosc { get; set; }
        [DataMember]
        public bool DlaDziecka { get; set; }
        [DataMember]
        public int RokProdukcji { get; set; }

        /// <summary>
        /// Tworzy nowy snowboard z podanymi parametrami.
        /// Wyświetla wyjątek gdy którykolwiek z parametrów jest poza dozwolonym zakresem.
        /// </summary>
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

        /// <summary>
        /// Oblicza koszt wypożyczenia snowboardu na daną liczbę dni.
        /// Uwzględnia 15% zniżki dla snowboardów dziecięcych.
        /// Zwraca Całkowity koszt wypożyczenia.
        /// </summary>

        public override decimal ObliczKoszt(int liczbaDni)
        {
            decimal koszt = base.ObliczKoszt(liczbaDni);

            if (DlaDziecka)
                koszt *= 0.85m; // 15% zniżki dla dzieci

            return koszt;
        }

        /// <summary>
        /// Zwraca szczegółowe informacje o snowboardzie.
        /// </summary>
        public override string Szczegoly => $"{Dlugosc} cm, rozmiar {Rozmiar}, " + (DlaDziecka ? "dziecięcy (zniżka)" : "dla dorosłych");

        /// <summary>
        /// Zwraca pełny opis snowboardu.
        /// </summary>
        public override string Opis()
        {
            return base.Opis() + $" | {Szczegoly}";
        }
    }
}
