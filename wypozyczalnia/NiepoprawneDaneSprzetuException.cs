using System;

namespace WypozyczalniaNarciarska
{
    /// <summary>
    /// Wyj¹tek zg³aszany w przypadku podania niepoprawnych danych sprzêtu narciarskiego.
    /// </summary>
    public class NiepoprawneDaneSprzetuException : ArgumentException
    {
        /// <summary>
        /// Tworzy wyj¹tek z domyœlnym komunikatem o niepoprawnych danych sprzêtu.
        /// </summary>
        public NiepoprawneDaneSprzetuException() : base("Niepoprawne dane sprzêtu.") { }

        /// <summary>
        /// Tworzy wyj¹tek z podanym komunikatem opisuj¹cym b³¹d.
        /// </summary>
        public NiepoprawneDaneSprzetuException(string message) : base(message) { }

        /// <summary>
        /// Tworzy wyj¹tek z podanym komunikatem oraz wyj¹tkiem wewnêtrznym.
        /// </summary>
        public NiepoprawneDaneSprzetuException(string message, Exception inner) : base(message, inner) { }
    }
}