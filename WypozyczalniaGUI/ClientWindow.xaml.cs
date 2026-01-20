using System;
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
    /// Logika interakcji dla klasy ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        private Wypozyczalnia _wypozyczalnia;
        public ClientWindow(Wypozyczalnia wypozyczalnia)
        {
            _wypozyczalnia = wypozyczalnia;
            InitializeComponent();
            dgKlienci.ItemsSource = _wypozyczalnia.Klienci;
        }

        private void BtnDodajKlienta_Click(object sender, RoutedEventArgs e)
        {
            string imie = txtImie.Text.Trim();
            string nazwisko = txtNazwisko.Text.Trim();
            string pesel = txtPesel.Text.Trim();
            DateOnly dataUrodzenia = DateOnly.FromDateTime(dpDataUrodzenia.SelectedDate ?? DateTime.Now);
            string numerTelefonu = txtTelefon.Text.Trim();
            string email = txtEmail.Text.Trim();
            try
            {
                Klient nowyKlient = new Klient(imie, nazwisko, pesel, dataUrodzenia.ToDateTime(new TimeOnly(0, 0)), numerTelefonu, email);
                _wypozyczalnia.DodajKlienta(nowyKlient);
                MessageBox.Show("Nowy klient został dodany pomyślnie.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd podczas dodawania klienta: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
