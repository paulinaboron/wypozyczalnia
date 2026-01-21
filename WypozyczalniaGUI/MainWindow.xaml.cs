using Microsoft.Win32;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WypozyczalniaNarciarska;

namespace WypozyczalniaGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Wypozyczalnia _wypozyczalnia;
        public MainWindow()
        {
            InitializeComponent();
            _wypozyczalnia = new Wypozyczalnia();
            dgRezerwacje.ItemsSource = _wypozyczalnia.Rezerwacje;
            dgWypozyczenia.ItemsSource = _wypozyczalnia.Wypozyczenia;
        }

        private void BtnKlienci_Click(object sender, RoutedEventArgs e)
        {
            ClientWindow oknoKlientow = new ClientWindow(_wypozyczalnia);
            oknoKlientow.Owner = this;
            oknoKlientow.ShowDialog();
            dgRezerwacje.Items.Refresh();
        }

        private void BtnDodajRezerwacje_Click(object sender, RoutedEventArgs e)
        {
            NewReservationWindow oknoNowejRezerwacji = new NewReservationWindow(_wypozyczalnia);
            oknoNowejRezerwacji.Owner = this;
            oknoNowejRezerwacji.ShowDialog();
            dgRezerwacje.Items.Refresh();
        }

        private void BtnSprzet_Click(object sender, RoutedEventArgs e)
        {
            EquipmentWindow oknoSprzetu = new EquipmentWindow(_wypozyczalnia);
            oknoSprzetu.Owner = this;
            oknoSprzetu.ShowDialog();
            dgRezerwacje.Items.Refresh();
        }

        private void BtnZapisz_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Pliki XML (*.xml)|*.xml";
            saveFileDialog.FileName = "wypozyczalnia.xml";

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    _wypozyczalnia.ZapiszDoPliku(saveFileDialog.FileName);

                    txtStatusPliku.Text = $"Plik: {saveFileDialog.FileName}";
                    MessageBox.Show("Dane zostały pomyślnie zapisane.", "Zapis", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Błąd zapisu: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnWczytaj_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Pliki XML (*.xml)|*.xml";

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    _wypozyczalnia = Wypozyczalnia.WczytajZPliku(openFileDialog.FileName);

                    dgRezerwacje.ItemsSource = _wypozyczalnia.Rezerwacje;
                    dgWypozyczenia.ItemsSource = _wypozyczalnia.Wypozyczenia;

                    txtStatusPliku.Text = $"Plik: {openFileDialog.FileName}";
                    MessageBox.Show("Dane zostały pomyślnie wczytane.", "Wczytywanie", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Błąd wczytywania: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnRealizuj_Click(object sender, RoutedEventArgs e)
        {

            if (dgRezerwacje.SelectedItem is Rezerwacja wybranaRez)
            {
                try
                {
                    Wypozyczenie noweWyp = new Wypozyczenie(
                        wybranaRez.Klient,
                        wybranaRez.Sprzet,
                        wybranaRez.DataOd,
                        wybranaRez.DataDo
                    );

                    _wypozyczalnia.DodajWypozyczenie(noweWyp);
                    _wypozyczalnia.Rezerwacje.Remove(wybranaRez);

                    dgRezerwacje.Items.Refresh();
                    dgWypozyczenia.Items.Refresh();

                    MessageBox.Show("Rezerwacja została pomyślnie zmieniona w wypożyczenie.", "Sukces");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Błąd podczas realizacji: {ex.Message}", "Błąd");
                }
            }
            else
            {
                MessageBox.Show("Proszę najpierw wybrać rezerwację z listy.");
            }
        }

        private void BtnAnuluj_Click(object sender, RoutedEventArgs e)
        {
            Rezerwacja wybranaRezerwacja = (Rezerwacja)dgRezerwacje.SelectedItem;
            if (wybranaRezerwacja != null)
            {
                MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz anulować wybraną rezerwację?", "Potwierdzenie anulowania", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    _wypozyczalnia.UsunRezerwacje(wybranaRezerwacja);
                    dgRezerwacje.Items.Refresh();
                }
            }
            else
            {
                MessageBox.Show("Proszę wybrać rezerwację do anulowania.", "Brak wyboru", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnPrzyjmijZwrot_Click(object sender, RoutedEventArgs e)
        {
            if (dgWypozyczenia.SelectedItem is Wypozyczenie wybrane)
            {
                try
                {
                    wybrane.ZwrocSprzet(DateTime.Now);
                    dgWypozyczenia.Items.Refresh();

                    MessageBox.Show($"Sprzęt zwrócony. Koszt całkowity: {wybrane.ObliczKoszt():C}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}