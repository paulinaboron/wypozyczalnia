using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WypozyczalniaNarciarska
{
    /// <summary>
    /// Wyjątek zgłaszany, gdy numer PESEL jest niepoprawny.
    /// </summary>
    public class NiepoprawnyPeselException : Exception
    {
        public NiepoprawnyPeselException()
            : base("Numer PESEL musi mieć 11 cyfr.") { }
    }

    /// <summary>
    /// Abstrakcyjna klasa bazowa reprezentująca osobę.
    /// Zawiera podstawowe dane imię, nazwisko, PESEL i data urodzenia.
    /// </summary>

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

        /// <summary>
        /// Pobiera lub ustawia imię osoby.
        /// Wyjątek gdy imię jest puste lub zawiera tylko białe znaki.
        /// </summary>


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

        /// <summary>
        /// Pobiera lub ustawia nazwisko osoby.
        /// Wyjątek gdy nazwisko jest puste lub zawiera tylko białe znaki.
        /// </summary>


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

        /// <summary>
        /// Pobiera numer PESEL osoby.
        /// Może zostać ustawiony tylko podczas inicjalizacji obiektu.
        /// </summary>

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


        /// <summary>
        /// Pobiera lub ustawia datę urodzenia osoby.
        /// Wyjątek jeśli data jest późniejsza niż aktualna.
        /// </summary>


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

        /// <summary>
        /// Konstruktor domyślny inicjalizujący.
        /// </summary>

        protected Osoba()
        {
            imie = "Brak";
            nazwisko = "Brak";
            pesel = new string('0', 11);
            dataUrodzenia = DateTime.Now;
        }

        /// <summary>
        /// Tworzy obiekt osoby na podstawie podanych danych.
        /// </summary>
        /// 
        protected Osoba(string imie, string nazwisko, string pesel, DateTime dataUrodzenia)
            : this()
        {
            Imie = imie;
            Nazwisko = nazwisko;
            Pesel = pesel;
            DataUrodzenia = dataUrodzenia;
        }

        /// <summary>
        /// Oblicza wiek osoby na podstawie daty urodzenia.
        /// </summary>

        public int Wiek()
        {
            return (int)((DateTime.Now - DataUrodzenia).TotalDays / 365.25);
        }

        /// <summary>
        /// Zwraca krótki opis osoby.
        /// </summary>

        public virtual string Opis()
        {
            return $"{Imie} {Nazwisko}, PESEL: {Pesel}, wiek: {Wiek()}";
        }

        /// <summary>
        /// Zwraca tekstową reprezentację obiektu osoby.
        /// </summary>
       
        public override string ToString()
        {
            return Opis();
        }

        /// <summary>
        /// Porównuje dwie osoby na podstawie numeru PESEL.
        /// </summary>
        
        public bool Equals(Osoba? other)
        {
            if (other is null) return false;
            return Pesel == other.Pesel;
        }
    }
}
