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
    [DataContract]
    public class Klient : Osoba
    {
        [DataMember]
        private string numerTelefonu = "000000000";
        [DataMember]
        private string email = "brak@brak.pl";
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
        public Klient() : base() {}

        public Klient(string imie, string nazwisko, string pesel, DateTime dataUrodzenia, string numerTelefonu, string email): base(imie, nazwisko, pesel, dataUrodzenia)
        { 
            NumerTelefonu = numerTelefonu;
            Email = email;
        }
        public override string Opis()
        {
            return base.Opis() +
                   $", tel: {NumerTelefonu}, e-mail: {Email}";
        }
    }
}
