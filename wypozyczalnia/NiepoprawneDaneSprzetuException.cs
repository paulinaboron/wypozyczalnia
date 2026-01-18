using System;

namespace WypozyczalniaNarciarska
{
    public class NiepoprawneDaneSprzetuException : ArgumentException
    {
        public NiepoprawneDaneSprzetuException() : base("Niepoprawne dane sprzêtu.") { }
        public NiepoprawneDaneSprzetuException(string message) : base(message) { }
        public NiepoprawneDaneSprzetuException(string message, Exception inner) : base(message, inner) { }
    }
}