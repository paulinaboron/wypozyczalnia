using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WypozyczalniaNarciarska
{
    public class NiepoprawnaCenaException : Exception
    {
        public NiepoprawnaCenaException()
            : base("Cena za dzień musi być większa od zera.") { }
    }

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
        [DataMember]
        private bool czyDostepny;

        public Guid Id => id;

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

        public bool CzyDostepny
        {
            get => czyDostepny;
            set => czyDostepny = value;
        }
        
        protected SprzetNarciarski()
        {
            id = Guid.NewGuid();
            producent = "brak";
            cenaZaDzien = 1;
            czyDostepny = true;
        }

        protected SprzetNarciarski(string producent, decimal cenaZaDzien) : this()
        {
            Producent = producent;
            CenaZaDzien = cenaZaDzien;
        }

        public virtual decimal ObliczKoszt(int liczbaDni)
        {
            if (liczbaDni <= 0)
                throw new ArgumentException("Liczba dni musi być dodatnia.");

            return CenaZaDzien * liczbaDni;
        }

        public virtual string Opis()
        {
            return $"{GetType().Name} | {Producent} | {CenaZaDzien} zł/dzień";
        }

        public override string ToString()
        {
            return $"{Opis()} | Dostępny: {(CzyDostepny ? "TAK" : "NIE")}";
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public int CompareTo(SprzetNarciarski? other)
        {
            if (other is null) return -1;
            return CenaZaDzien.CompareTo(other.CenaZaDzien);
        }

        public bool Equals(SprzetNarciarski? other)
        {
            if (other is null) return false;
            return Id.Equals(other.Id);
        }

    }
}

