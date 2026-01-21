using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WypozyczalniaNarciarska;

namespace WypozyczalniaNarciarska
{
    public class Wypozyczenie : Rezerwacja
    {
        public bool Zakonczone { get; private set; }
        public DateTime? DataZwrotu { get; private set; }

        public Wypozyczenie(Klient klient, SprzetNarciarski sprzet, DateTime od, DateTime _do)
            : base(klient, sprzet, od, _do)
        {
            Zakonczone = false;
        }

        public void ZwrocSprzet(DateTime dataZwrotu)
        {
            if (dataZwrotu < DataOd)
                throw new NiepoprawnaRezerwacjaException("Data zwrotu nie może być wcześniejsza niż data wypożyczenia.");

            DataZwrotu = dataZwrotu;
            Zakonczone = true;
        }

        public override decimal ObliczKoszt()
        {
            if (!Zakonczone || DataZwrotu == null)
                return base.ObliczKoszt();

            int dni = (DataZwrotu.Value - DataOd).Days;
            if (dni <= 0) dni = 1;
            return Sprzet.ObliczKoszt(dni);
        }
    }
}
