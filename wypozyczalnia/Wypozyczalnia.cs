using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;
using System.Runtime.Serialization;
using System.IO;

namespace WypozyczalniaNarciarska
{
    [DataContract]
    public class Wypozyczalnia
    {
        [DataMember]
        public List<Rezerwacja> Rezerwacje { get; private set; } = new();

        [DataMember]
        public List<Wypozyczenie> Wypozyczenia { get; private set; } = new();

        public void DodajRezerwacje(Rezerwacja r)
        {
            Rezerwacje.Add(r);
        }

        public void DodajWypozyczenie(Wypozyczenie w)
        {
            Wypozyczenia.Add(w);
        }

        public void ZapiszDoPliku(string nazwa)
        {
            DataContractSerializer serializer = new(
    typeof(Wypozyczalnia),
    new Type[]
    {
        typeof(Narty),
        typeof(Snowboard),
        typeof(Buty)
    });

           
            using FileStream fs = new(nazwa, FileMode.Create);
            serializer.WriteObject(fs, this);
        }

        public static Wypozyczalnia WczytajZPliku(string nazwa)
        {
            DataContractSerializer serializer = new(typeof(Wypozyczalnia));
            using FileStream fs = new(nazwa, FileMode.Open);
            return (Wypozyczalnia)serializer.ReadObject(fs);
        }
    }

    
}
