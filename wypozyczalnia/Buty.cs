using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WypozyczalniaNarciarska
{
    /// <summary>
    /// Reprezentuje buty narciarskie dostępne w wypożyczalni.
    /// </summary>

    [DataContract]
    public class Buty : SprzetNarciarski
    {
        [DataMember]
        public int Rozmiar { get; set; }
        [DataMember]
        public bool DlaDziecka { get; set; }
        [DataMember]
        public int RokProdukcji { get; set; }

        /// <summary>
        /// Tworzy nowe buty narciarskie z podanymi parametrami.
        /// Wyświetla błąd gdy rozmiar lub rok produkcji są poza dozwolonym zakresem.
        /// </summary>

        public Buty(string producent, decimal cenaZaDzien, int rozmiar, bool dlaDziecka, int rokProdukcji) : base(producent, cenaZaDzien)
        {
            if (rozmiar < 20 || rozmiar > 50)
                throw new NiepoprawneDaneSprzetuException("Rozmiar butów musi być w zakresie od 20 do 50.");
            
            if (rokProdukcji < 1970 || rokProdukcji > DateTime.Now.Year)
                throw new NiepoprawneDaneSprzetuException("Niepoprawny rok produkcji butów");
            
            Rozmiar = rozmiar;
            DlaDziecka = dlaDziecka;
            RokProdukcji = rokProdukcji;
        }

        /// <summary>
        /// Oblicza koszt wypożyczenia butów na określoną liczbę dni.
        /// Uwzględnia zniżkę 10% dla butów dziecięcych.
        /// Zwraca całkowity koszt wypożyczenia.
        /// </summary>

        public override decimal ObliczKoszt(int liczbaDni)
        {
            decimal koszt = base.ObliczKoszt(liczbaDni);
            
            if (DlaDziecka)
                koszt *= 0.9m; // 10% zniżki dla dzieci
            return koszt;
        }

        /// <summary>
        /// Zwraca szczegółowe informacje o butach narciarskich.
        /// </summary>
        public override string Szczegoly => $"Rozmiar {Rozmiar}, " + (DlaDziecka ? "dziecięce (zniżka)" : "dla dorosłych");

        /// <summary>
        /// Zwraca pełny opis butów narciarskich.
        /// </summary>
        public override string Opis()
        {
            return base.Opis() + $" | {Szczegoly}";
        }
    }
}
