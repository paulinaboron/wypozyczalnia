using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WypozyczalniaNarciarska;

namespace WypozyczalniaGUI
{
    /// <summary>
    /// Obsługa procesu tworzenia nowej rezerwacji sprzętu
    /// 
    /// Kontrola Dostępności: Dynamicznie filtruje listę sprzętu w metodzie Termin_Changed, wyświetlając tylko te pozycje, które są wolne w wybranym przedziale czasowym
    /// Kalkulacja Kosztu: Metoda LbDostepnySprzet_SelectionChanged na bieżąco oblicza szacowany koszt wynajmu po wybraniu konkretnego przedmiotu i określeniu liczby dni.
    /// Tworzenie Obiektu: Po zatwierdzeniu tworzy nową instancję klasy Rezerwacja, łącząc wybranego klienta, sprzęt oraz ramy czasowe.
    /// </summary>
    public partial class NewReservationWindow : Window
    {
        private Wypozyczalnia _wypozyczalnia;
        public NewReservationWindow(Wypozyczalnia wypozyczalnia)
        {
            _wypozyczalnia = wypozyczalnia;
            InitializeComponent();
            lbDostepnySprzet.ItemsSource = _wypozyczalnia.ListaSprzetu
                .Where(s => _wypozyczalnia.CzyDostepnyWTerminie(s, DateTime.Now, DateTime.Now))
                .ToList();
            cbKlienci.ItemsSource = _wypozyczalnia.Klienci;
        }

        private void BtnNowyKlient_Click(object sender, RoutedEventArgs e)
        {
            ClientWindow oknoKlientow = new ClientWindow(_wypozyczalnia);
            oknoKlientow.Owner = this;
            oknoKlientow.ShowDialog();
        }

        private void BtnZatwierdz_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                Klient wybranyKlient = (Klient)cbKlienci.SelectedItem;
                DateTime? dataRozpoczecia = dpDataOd.SelectedDate;
                DateTime? dataZakonczenia = dpDataDo.SelectedDate;
                SprzetNarciarski wybranySprzet = (SprzetNarciarski)lbDostepnySprzet.SelectedItem;
                Rezerwacja nowaRezerwacja = new Rezerwacja(wybranyKlient, wybranySprzet, dataRozpoczecia ?? DateTime.Now, dataZakonczenia ?? DateTime.Now.AddDays(1));
                _wypozyczalnia.DodajRezerwacje(nowaRezerwacja);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd podczas tworzenia rezerwacji: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBox.Show("Nowa rezerwacja została dodana pomyślnie.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);


        }

        private void Termin_Changed(object sender, SelectionChangedEventArgs e)
        {
            DateTime start = dpDataOd.SelectedDate ?? DateTime.Now;
            DateTime koniec = dpDataDo.SelectedDate ?? DateTime.Now.AddDays(1);

            lbDostepnySprzet.ItemsSource = _wypozyczalnia.ListaSprzetu
                .Where(s => _wypozyczalnia.CzyDostepnyWTerminie(s, start, koniec))
                .ToList();
        }

        private void LbDostepnySprzet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbDostepnySprzet.SelectedItem == null)
            {
                lblSuma.Text = "0.00 zł";
                return;
            }
            SprzetNarciarski wybranySprzet = (SprzetNarciarski)lbDostepnySprzet.SelectedItem;
            DateTime start = dpDataOd.SelectedDate ?? DateTime.Now;
            DateTime koniec = dpDataDo.SelectedDate ?? DateTime.Now.AddDays(1);

            int dni = (koniec - start).Days +1;
            if (dni <= 0) dni = 1;
            lblSuma.Text = $"{wybranySprzet.ObliczKoszt(dni)} zł";
        }
    }
}
