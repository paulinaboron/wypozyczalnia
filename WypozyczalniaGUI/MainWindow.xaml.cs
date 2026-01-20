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

        }

        private void BtnAnuluj_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}