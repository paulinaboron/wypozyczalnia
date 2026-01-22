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
    /// Klasa zapewnia interfejs do zarządzania bazą klientów.
    /// 
    /// Wyświetlanie Listy: Pobiera kolekcję Klienci z głównego obiektu wypożyczalni i wyświetla ją w DataGrid.
    /// Rejestracja Nowych Klientów: Zbiera dane z pól tekstowych (w tym PESEL i e-mail) i tworzy nowy obiekt klasy Klient.
    /// Obsługa Wyjątków: Przechwytuje błędy rzucane przez logikę klasy Klient (np. błędny format PESEL) i wyświetla je użytkownikowi w formie komunikatu.
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
