using WypozyczalniaNarciarska;
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
    /// Reprezentuje klienta wypożyczalni narciarskiej.
    /// Dziedziczy po klasie Osoba i rozszerza ją o dane kontaktowe.
    /// </summary>
    [DataContract]
    public class Klient : Osoba
    {
        [DataMember]
        private string numerTelefonu = "000000000";
        [DataMember]
        private string email = "brak@brak.pl";

        /// <summary>
        /// Pobiera lub ustawia numer telefonu klienta.
        /// Wyrzuca wyjątek kiedy numer telefonu jest pusty lub składa się tylko z białych znaków.
        /// </summary>

        public string NumerTelefonu
        {
            get => numerTelefonu;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Numer telefonu nie może być pusty");
                numerTelefonu = value;
            }
        }

        /// <summary>
        /// Pobiera lub ustawia adres e-mail klienta.
        /// Wyrzuca wyjątek kiedy nie wprowadzono znaku @.
        /// </summary>
        public string Email
        {
            get => email;
            set
            {
                if (!value.Contains("@"))
                    throw new ArgumentException("Niepoprawny adres e-mail");
                email = value;
            }
        }

        /// <summary>
        /// Tworzy nową instancję klienta z domyślnymi wartościami.
        /// </summary>
        /// 
        public Klient() : base() {}


        /// <summary>
        /// Tworzy nową instancję klienta z pełnymi danymi.
        /// </summary>
        /// 
        public Klient(string imie, string nazwisko, string pesel, DateTime dataUrodzenia, string numerTelefonu, string email): base(imie, nazwisko, pesel, dataUrodzenia)
        { 
            NumerTelefonu = numerTelefonu;
            Email = email;
        }

        /// <summary>
        /// Zwraca tekstowy opis klienta wraz z danymi kontaktowymi.
        /// </summary>
        public override string Opis()
        {
            return base.Opis() +
                   $", tel: {NumerTelefonu}, e-mail: {Email}";
        }
    }
}
