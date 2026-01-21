using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using WypozyczalniaNarciarska;

namespace WypozyczalniaNarciarska
{
    [DataContract]
    public class Rezerwacja : IComparable<Rezerwacja>, IEquatable<Rezerwacja>
    {
        [DataMember]
        public Guid Id { get; private set; }

        [DataMember]
        public Klient Klient { get; private set; }

        [DataMember]
        public SprzetNarciarski Sprzet { get; private set; }

        [DataMember]
        private DateTime dataOd;

        [DataMember]
        private DateTime dataDo;

        public DateTime DataOd
        {
            get => dataOd;
            private set
            {
                if (value.Date < DateTime.Today)
                    throw new NiepoprawnaRezerwacjaException("Data rozpoczęcia nie może być z przeszłości.");
                dataOd = value;
            }
        }

        public DateTime DataDo
        {
            get => dataDo;
            private set
            {
                if (value <= DataOd)
                    throw new NiepoprawnaRezerwacjaException("Data zakończenia musi być późniejsza niż rozpoczęcia.");
                dataDo = value;
            }
        }

        public decimal Koszt => ObliczKoszt();

        public Rezerwacja(Klient klient, SprzetNarciarski sprzet, DateTime od, DateTime _do)
        {
            Id = Guid.NewGuid();
            Klient = klient ?? throw new ArgumentNullException(nameof(klient));
            Sprzet = sprzet ?? throw new ArgumentNullException(nameof(sprzet));

            DataOd = od;
            DataDo = _do;
        }

        public virtual decimal ObliczKoszt()
        {
            int dni = (DataDo - DataOd).Days;
            if (dni <= 0) dni = 1;
            return Sprzet.ObliczKoszt(dni);
        }

        public int CompareTo(Rezerwacja? other)
        {
            if (other == null) return -1;
            return DataOd.CompareTo(other.DataOd);
        }

        public bool Equals(Rezerwacja? other)
        {
            if (other == null) return false;
            return Id == other.Id;
        }

        public override string ToString()
        {
            return $"[REZERWACJA] {Klient.Imie} {Klient.Nazwisko} | {Sprzet.Opis()} | {DataOd:d} - {DataDo:d}";
        }
    }
}
