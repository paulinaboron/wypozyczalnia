using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;
using System.Runtime.Serialization;
using System.IO;
using System.Collections.ObjectModel;

namespace WypozyczalniaNarciarska
{
    [DataContract]
    public class Wypozyczalnia
    {
        [DataMember]
        public ObservableCollection<Rezerwacja> Rezerwacje { get; private set; } = new();

        [DataMember]
        public ObservableCollection<Wypozyczenie> Wypozyczenia { get; private set; } = new();

        [DataMember]
        public ObservableCollection<Klient> Klienci { get; private set; } = new();

        [DataMember]
        public ObservableCollection<SprzetNarciarski> ListaSprzetu { get; private set; } = new();

        public void DodajRezerwacje(Rezerwacja r)
        {
            Rezerwacje.Add(r);
        }

        public void DodajWypozyczenie(Wypozyczenie w)
        {
            Wypozyczenia.Add(w);
        }

        public void DodajKlienta(Klient k)
        {
            Klienci.Add(k);
        }

        public void DodajSprzet(SprzetNarciarski s)
        {
            ListaSprzetu.Add(s);
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
            DataContractSerializer serializer = new DataContractSerializer(
                typeof(Wypozyczalnia),
                new Type[] { typeof(Narty), typeof(Snowboard), typeof(Buty) }
            );

            using FileStream fs = new FileStream(nazwa, FileMode.Open);
            return (Wypozyczalnia)serializer.ReadObject(fs);
        }
    }

    
}
