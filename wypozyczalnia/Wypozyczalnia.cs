using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.IO;
using System.Collections.ObjectModel;

namespace WypozyczalniaNarciarska
{
    /// <summary>
    /// Reprezentuje wypożyczalnię narciarską.
    /// Przechowuje klientów, sprzęt, rezerwacje oraz wypożyczenia.
    /// </summary>
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

        /// <summary>
        /// Dodaje nową rezerwację do systemu.
        /// </summary>
        public void DodajRezerwacje(Rezerwacja r)
        {
            Rezerwacje.Add(r);
        }

        /// <summary>
        /// Usuwa rezerwację z systemu.
        /// </summary>

        public void UsunRezerwacje(Rezerwacja r)
        {
            Rezerwacje.Remove(r);
        }

        /// <summary>
        /// Dodaje nowe wypożyczenie.
        /// </summary>
        public void DodajWypozyczenie(Wypozyczenie w)
        {
            Wypozyczenia.Add(w);
        }

        /// <summary>
        /// Dodaje nowego klienta do wypożyczalni.
        /// </summary>
        public void DodajKlienta(Klient k)
        {
            Klienci.Add(k);
        }

        /// <summary>
        /// Dodaje sprzęt do listy dostępnego wyposażenia.
        /// </summary>
        public void DodajSprzet(SprzetNarciarski s)
        {
            ListaSprzetu.Add(s);
        }

        /// <summary>
        /// Sprawdza, czy dany sprzęt jest dostępny w podanym terminie.
        /// Zwraca prawdę jeśli sprzęt nie jest zarezerwowany ani wypożyczony.
        /// </summary>
        public bool CzyDostepnyWTerminie(SprzetNarciarski s, DateTime od, DateTime _do)
        {
            bool zarezerwowane = Rezerwacje.Any(r => r.Sprzet.Id == s.Id &&
                                           od < r.DataDo && _do > r.DataOd);
            bool wypozyczone = Wypozyczenia.Any(w => w.Sprzet.Id == s.Id &&
                                              od < w.DataDo && _do > w.DataOd);

            return !zarezerwowane && !wypozyczone;
        }

        /// <summary>
        /// Zapisuje stan wypożyczalni do pliku.
        /// </summary>
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
        /// <summary>
        /// Wczytuje stan wypożyczalni z pliku.
        /// Zwraca obiekt odtworzony z pliku.
        /// </summary>

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
