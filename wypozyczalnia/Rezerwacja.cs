using System;
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
    /// Reprezentuje rezerwację sprzętu narciarskiego przez klienta.
    /// </summary>
    [DataContract]
    public class Rezerwacja : IComparable<Rezerwacja>, IEquatable<Rezerwacja>
    {
        [DataMember]
        public Guid Id { get; private set; }

        [DataMember]
        public Klient Klient { get; private set; }

        [DataMember]
        public SprzetNarciarski Sprzet { get; private set; }

        [DataMember]
        private DateTime dataOd;

        [DataMember]
        private DateTime dataDo;

        /// <summary>
        /// Data rozpoczęcia rezerwacji.
        /// Nie może być wcześniejsza niż dzisiejsza data.
        /// Wyświetla wyjątek gdy data rozpoczęcia jest z przeszłości.
        /// </summary>

        public DateTime DataOd
        {
            get => dataOd;
            private set
            {
                if (value.Date < DateTime.Today)
                    throw new NiepoprawnaRezerwacjaException("Data rozpoczęcia nie może być z przeszłości.");
                dataOd = value;
            }
        }

        /// <summary>
        /// Data zakończenia rezerwacji.
        /// Musi być późniejsza niż data rozpoczęcia.
        /// Wyświetla wyjątek gdy data zakończenia jest wcześniejsza niż data rozpoczęcia.
        /// </summary>

        public DateTime DataDo
        {
            get => dataDo;
            private set
            {
                if (value < DataOd)
                    throw new NiepoprawnaRezerwacjaException("Data zakończenia musi być późniejsza niż rozpoczęcia.");
                dataDo = value;
            }
        }

        /// <summary>
        /// Całkowity koszt rezerwacji.
        /// </summary>

        public decimal Koszt => ObliczKoszt();

        /// <summary>
        /// Tworzy nową rezerwację sprzętu dla danego klienta.
        /// </summary>

        public Rezerwacja(Klient klient, SprzetNarciarski sprzet, DateTime od, DateTime _do)
        {
            Id = Guid.NewGuid();
            Klient = klient ?? throw new ArgumentNullException(nameof(klient));
            Sprzet = sprzet ?? throw new ArgumentNullException(nameof(sprzet));

            DataOd = od;
            DataDo = _do;
        }

        /// <summary>
        /// Oblicza koszt rezerwacji na podstawie długości wypożyczenia i ceny sprzętu.
        /// zwraca całkowity koszt rezerwacji.
        /// </summary>
        public virtual decimal ObliczKoszt()
        {
            int dni = (DataDo - DataOd).Days;
            if (dni <= 0) dni = 1;
            return Sprzet.ObliczKoszt(dni);
        }

        /// <summary>
        /// Porównuje dwie rezerwacje według daty rozpoczęcia.
        /// </summary>
        
        public int CompareTo(Rezerwacja? other)
        {
            if (other == null) return -1;
            return DataOd.CompareTo(other.DataOd);
        }

        /// <summary>
        /// Sprawdza, czy dwie rezerwacje są takie same na podstawie identyfikatora.
        /// </summary>
        
        public bool Equals(Rezerwacja? other)
        {
            if (other == null) return false;
            return Id == other.Id;
        }

        /// <summary>
        /// Zwraca tekstową reprezentację rezerwacji.
        /// </summary>

        public override string ToString()
        {
            return $"[REZERWACJA] {Klient.Imie} {Klient.Nazwisko} | {Sprzet.Opis()} | {DataOd:d} - {DataDo:d}";
        }
    }
}
