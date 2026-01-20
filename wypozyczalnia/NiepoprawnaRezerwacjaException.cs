using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WypozyczalniaNarciarska
{
    public class NiepoprawnaRezerwacjaException : Exception
    {
        public NiepoprawnaRezerwacjaException(string message) : base(message) { }
    }
}

