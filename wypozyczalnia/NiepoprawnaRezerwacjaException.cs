using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WypozyczalniaNarciarska
{
    /// <summary>
    /// Wyjątek zgłaszany w przypadku wykrycia niepoprawnych danych rezerwacji.
    /// </summary>
    public class NiepoprawnaRezerwacjaException : Exception
    {
        /// <summary>
        /// Tworzy nowy wyjątek związany z błędną rezerwacją.
        /// </summary>
        public NiepoprawnaRezerwacjaException(string message) : base(message) { }
    }
}

