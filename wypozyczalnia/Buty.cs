using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WypozyczalniaNarciarska
{

    [DataContract]
    public class Buty : SprzetNarciarski
    {
        public int Rozmiar { get; }
        public bool DlaDziecka { get; }
        public int RokProdukcji { get; }
        
        public Buty(string producent, decimal cenaZaDzien, int rozmiar, bool dlaDziecka, int rokProdukcji) : base(producent, cenaZaDzien)
        {
            if (rozmiar < 20 || rozmiar > 50)
                throw new NiepoprawneDaneSprzetuException("Rozmiar butów musi być w zakresie od 20 do 50.");
            
            if (rokProdukcji < 1970 || rokProdukcji > DateTime.Now.Year)
                throw new NiepoprawneDaneSprzetuException("Niepoprawny rok produkcji butów");
            
            Rozmiar = rozmiar;
            DlaDziecka = dlaDziecka;
            RokProdukcji = rokProdukcji;
        }
        
        public override decimal ObliczKoszt(int liczbaDni)
        {
            decimal koszt = base.ObliczKoszt(liczbaDni);
            
            if (DlaDziecka)
                koszt *= 0.9m; // 10% zniżki dla dzieci
            return koszt;
        }
        
        public override string Opis()
        {
            return base.Opis() +
                $" | Rozmiar {Rozmiar}, " + (DlaDziecka ? "dziecięce (zniżka)" : "dla dorosłych");
        }
    }
}
