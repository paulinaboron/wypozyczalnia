using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace WypozyczalniaNarciarska
{
    /// <summary>
    /// Wyjątek zgłaszany, gdy podana cena sprzętu jest niepoprawna.
    /// </summary>
    public class NiepoprawnaCenaException : Exception
    {
        public NiepoprawnaCenaException()
            : base("Cena za dzień musi być większa od zera.") { }
    }

    /// <summary>
    /// Abstrakcyjna klasa bazowa reprezentująca sprzęt narciarski.
    /// </summary>

    [DataContract]
    [KnownType(typeof(Narty))]
    [KnownType(typeof(Snowboard))]
    [KnownType(typeof(Buty))]
    public abstract class SprzetNarciarski : ICloneable, IComparable<SprzetNarciarski>, IEquatable<SprzetNarciarski>
    {
        [DataMember]
        private Guid id;
        [DataMember]
        private string producent;
        [DataMember]
        private decimal cenaZaDzien;

        public Guid Id => id;

        /// <summary>
        /// Nazwa producenta sprzętu.
        /// Wyświetla wyjątek gdy nazwa producenta jest pusta.
        /// </summary>
        public string Producent
        {
            get => producent;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Musisz podać nazwę producenta.");
                producent = value;
            }
        }

        /// <summary>
        /// Cena wypożyczenia sprzętu za jeden dzień.
        /// Wyświetla wyjątek gdy cena jest mniejsza lub równa zero.
        /// </summary>
        public decimal CenaZaDzien
        {
            get => cenaZaDzien;
            set
            {
                if (value <= 0)
                    throw new NiepoprawnaCenaException();
                cenaZaDzien = value;
            }
        }

        /// <summary>
        /// Nazwa typu sprzętu (np. Narty, Snowboard, Buty).
        /// </summary>

        public string TypSprzetu => GetType().Name;

        /// <summary>
        /// Nazwa typu sprzętu (np. Narty, Snowboard, Buty).
        /// </summary>
        public virtual string Szczegoly => "";

        /// <summary>
        /// Konstruktor domyślny tworzący sprzęt z wartościami domyślnymi.
        /// </summary>

        protected SprzetNarciarski()
        {
            id = Guid.NewGuid();
            producent = "brak";
            cenaZaDzien = 1;
        }

        /// <summary>
        /// Tworzy sprzęt narciarski z określonym producentem i ceną.
        /// </summary>
        protected SprzetNarciarski(string producent, decimal cenaZaDzien) : this()
        {
            Producent = producent;
            CenaZaDzien = cenaZaDzien;
        }

        /// <summary>
        /// Oblicza koszt wypożyczenia sprzętu na określoną liczbę dni.
        /// Zwraca całkowity koszt wypożyczenia.
        /// Wyświetla błą gdy liczba dni jest mniejsza lub równa zero.
        /// </summary>
        public virtual decimal ObliczKoszt(int liczbaDni)
        {
            if (liczbaDni <= 0)
                throw new ArgumentException("Liczba dni musi być dodatnia.");

            return CenaZaDzien * liczbaDni;
        }

        /// <summary>
        /// Zwraca podstawowy opis sprzętu.
        /// </summary>

        public virtual string Opis()
        {
            return $"{TypSprzetu} | {Producent} | {CenaZaDzien} zł/dzień";
        }

        /// <summary>
        /// Zwraca opis sprzętu.
        /// </summary>
        public override string ToString()
        {
            return Opis();
        }

        /// <summary>
        /// Tworzy kopię obiektu sprzętu.
        /// </summary>
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>
        /// Porównuje dwa obiekty sprzętu według ceny za dzień.
        /// </summary>
        public int CompareTo(SprzetNarciarski? other)
        {
            if (other is null) return -1;
            return CenaZaDzien.CompareTo(other.CenaZaDzien);
        }

        /// <summary>
        /// Sprawdza, czy dwa obiekty sprzętu są identyczne na podstawie identyfikatora.
        /// </summary>
        public bool Equals(SprzetNarciarski? other)
        {
            if (other is null) return false;
            return Id.Equals(other.Id);
        }

    }
}

