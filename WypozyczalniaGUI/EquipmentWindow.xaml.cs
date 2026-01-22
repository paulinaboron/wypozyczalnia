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
    /// Klasa odpowiedzialna za zarządzanie asortymentem sprzętu narciarskiego.
    /// 
    /// Prezentacja danych: ładuje listę dostępnego sprzętu w kontrolce DataGrid oraz umożliwia filtrowanie według frazy wyszukiwania.
    /// Dodawanie sprzętu: pobiera dane z formularza  i tworzy różne typy sprzętu (narty, snowboard, buty) z odpowiednimi właściwościami i walidacją danych wejściowych.
    /// </summary>
    public partial class EquipmentWindow : Window
    {
        private Wypozyczalnia _wypozyczalnia;
        public EquipmentWindow(Wypozyczalnia wypozyczalnia)
        {
            _wypozyczalnia = wypozyczalnia;
            InitializeComponent();
            dgSprzet.ItemsSource = _wypozyczalnia.ListaSprzetu;
            cbTypNart.ItemsSource = Enum.GetValues(typeof(TypNart));
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string fraza = txtSearch.Text.ToLower();
            dgSprzet.ItemsSource = _wypozyczalnia.ListaSprzetu.Where(s => s.Opis().ToLower().Contains(fraza)).ToList();
        }

        /// Dynamika Formularza
        /// Metoda steruje widocznością pól wejściowych (np. długość, typ nart) w zależności od wybranego rodzaju sprzętu
        private void CbTypSprzetu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var wybranyRodzaj = (cbTypSprzetu.SelectedItem as ComboBoxItem)?.Content.ToString();
            panelWymiary.Visibility = Visibility.Collapsed;
            panelTypNart.Visibility = Visibility.Collapsed;
            switch (wybranyRodzaj)
            {
                case "Narty":
                    panelWymiary.Visibility = Visibility.Visible;
                    panelTypNart.Visibility = Visibility.Visible;
                    break;
                case "Snowboard":
                    panelWymiary.Visibility = Visibility.Visible;
                    break;
                case "Buty":
                    break;
            }
        }

        private void BtnDodaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var wybranyRodzaj = (cbTypSprzetu.SelectedItem as ComboBoxItem)?.Content.ToString();
                string producent = txtProducent.Text;
                decimal cenaZaDzien = decimal.Parse(txtCena.Text);
                int rokProdukcji = int.Parse(txtRok.Text);
                int rozmiar = int.Parse(txtRozmiar.Text);
                bool dlaDziecka = (bool)chkDlaDziecka.IsChecked;

                switch (wybranyRodzaj)
                {
                case "Narty":
                    int dlugosc = int.Parse(txtDlugosc.Text);
                    TypNart typNart = (TypNart)cbTypNart.SelectedItem;
                    Narty narty = new Narty(producent, cenaZaDzien, rozmiar, dlugosc, typNart, dlaDziecka, rokProdukcji);
                    _wypozyczalnia.DodajSprzet(narty);
                    break;
                case "Snowboard":
                    int dlugoscSb = int.Parse(txtDlugosc.Text);
                    Snowboard snowboard = new Snowboard(producent, cenaZaDzien, rozmiar, dlugoscSb, dlaDziecka, rokProdukcji);
                    _wypozyczalnia.DodajSprzet(snowboard);
                    break;
                case "Buty":
                    Buty buty = new Buty(producent, cenaZaDzien, rozmiar, dlaDziecka, rokProdukcji);
                    _wypozyczalnia.DodajSprzet(buty);
                    break;

            }
                MessageBox.Show("Nowy sprzęt został dodany pomyślnie.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd podczas dodawania sprzętu: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }


            dgSprzet.Items.Refresh();

        }
    }
}
