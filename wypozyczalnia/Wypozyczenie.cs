using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using WypozyczalniaNarciarska;

namespace WypozyczalniaNarciarska
{
    /// <summary>
    /// Reprezentuje faktyczne wypożyczenie sprzętu przez klienta.
    /// Dziedziczy po klasie Rezerwacja.
    /// </summary>
    [DataContract]
    public class Wypozyczenie : Rezerwacja
    {
        [DataMember]
        public bool Zakonczone { get; private set; }
        [DataMember]
        public DateTime? DataZwrotu { get; private set; }

        /// <summary>
        /// Tworzy nowe wypożyczenie sprzętu dla danego klienta.
        /// </summary>
        public Wypozyczenie(Klient klient, SprzetNarciarski sprzet, DateTime od, DateTime _do)
            : base(klient, sprzet, od, _do)
        {
            Zakonczone = false;
        }

        /// <summary>
        /// Oznacza wypożyczenie jako zakończone i ustawia datę zwrotu sprzętu.
        /// Wyświetla błąd gdy data zwrotu jest wcześniejsza niż data rozpoczęcia wypożyczenia.
        /// </summary>

        public void ZwrocSprzet(DateTime dataZwrotu)
        {
            if (dataZwrotu < DataOd)
                throw new NiepoprawnaRezerwacjaException("Data zwrotu nie może być wcześniejsza niż data wypożyczenia.");

            DataZwrotu = dataZwrotu;
            Zakonczone = true;
        }

        /// <summary>
        /// Oblicza koszt wypożyczenia.
        /// Jeśli sprzęt został zwrócony, koszt jest liczony na podstawie faktycznej daty zwrotu.
        /// W przeciwnym wypadku używana jest planowana data zakończenia.
        /// Zwraca całkowity koszt wypożyczenia.
        /// </summary>

        public override decimal ObliczKoszt()
        {
            if (!Zakonczone || DataZwrotu == null)
                return base.ObliczKoszt();

            int dni = (DataZwrotu.Value - DataOd).Days;
            if (dni <= 0) dni = 1;
            return Sprzet.ObliczKoszt(dni);
        }
    }
}
