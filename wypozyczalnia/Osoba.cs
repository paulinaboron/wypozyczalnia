using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace wypozyczalnia
{
    public class NiepoprawnyPeselException : Exception
    {
        public NiepoprawnyPeselException()
            : base("Numer PESEL musi mieć 11 cyfr.") { }
    }

    [DataContract]
    public abstract class Osoba : IEquatable<Osoba>
    {
        [DataMember]
        private string imie;
        [DataMember]
        private string nazwisko;
        [DataMember]
        private string pesel;
        [DataMember]
        private DateTime dataUrodzenia;

        public string Imie
        {
            get => imie;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Musisz wprowadzić imię.");
                imie = value;
            }
        }

        public string Nazwisko
        {
            get => nazwisko;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Musisz wprowadzić nazwisko");
                nazwisko = value;
            }
        }

        public string Pesel
        {
            get => pesel;
            init
            {
                Regex regex = new Regex(@"^\d{11}$");
                if (!regex.IsMatch(value))
                    throw new NiepoprawnyPeselException();
                pesel = value;
            }
        }

        public DateTime DataUrodzenia
        {
            get => dataUrodzenia;
            set
            {
                if (value > DateTime.Now)
                    throw new ArgumentException("Niepoprawna data.");
                dataUrodzenia = value;
            }
        }

        protected Osoba()
        {
            imie = "Brak";
            nazwisko = "Brak";
            pesel = new string('0', 11);
            dataUrodzenia = DateTime.Now;
        }

        protected Osoba(string imie, string nazwisko, string pesel, DateTime dataUrodzenia)
            : this()
        {
            Imie = imie;
            Nazwisko = nazwisko;
            Pesel = pesel;
            DataUrodzenia = dataUrodzenia;
        }

        public int Wiek()
        {
            return (int)((DateTime.Now - DataUrodzenia).TotalDays / 365.25);
        }

        public virtual string Opis()
        {
            return $"{Imie} {Nazwisko}, PESEL: {Pesel}, wiek: {Wiek()}";
        }

        public override string ToString()
        {
            return Opis();
        }

        public bool Equals(Osoba? other)
        {
            if (other is null) return false;
            return Pesel == other.Pesel;
        }
    }
}
