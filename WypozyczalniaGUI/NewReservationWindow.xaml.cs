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
    /// Logika interakcji dla klasy NewReservationWindow.xaml
    /// </summary>
    public partial class NewReservationWindow : Window
    {
        private Wypozyczalnia _wypozyczalnia;
        public NewReservationWindow(Wypozyczalnia wypozyczalnia)
        {
            _wypozyczalnia = wypozyczalnia;
            InitializeComponent();
            lbDostepnySprzet.ItemsSource = _wypozyczalnia.ListaSprzetu
                .Where(s => s.CzyWolnyWTerminie(DateTime.Now, DateTime.Now, _wypozyczalnia.Rezerwacje))
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
                .Where(s => s.CzyWolnyWTerminie(start, koniec, _wypozyczalnia.Rezerwacje))
                .ToList();
        }

        private void LbDostepnySprzet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime start = dpDataOd.SelectedDate ?? DateTime.Now;
            DateTime koniec = dpDataDo.SelectedDate ?? DateTime.Now.AddDays(1);

            decimal cenaZadzien = lbDostepnySprzet.SelectedItem is SprzetNarciarski sprzet ? sprzet.CenaZaDzien : 0;

            int dni = (koniec - start).Days;
            if (dni <= 0) dni = 1;
            decimal calkowityKoszt = dni * cenaZadzien;
            lblSuma.Text = $"{calkowityKoszt} zł";
        }
    }
}
