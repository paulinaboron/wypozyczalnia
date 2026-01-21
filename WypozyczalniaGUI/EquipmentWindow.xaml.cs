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
    /// Logika interakcji dla klasy EquipmentWindow.xaml
    /// </summary>
    public partial class EquipmentWindow : Window
    {
        private Wypozyczalnia _wypozyczalnia;
        public EquipmentWindow(Wypozyczalnia wypozyczalnia)
        {
            _wypozyczalnia = wypozyczalnia;
            InitializeComponent();
            lbSprzet.ItemsSource = _wypozyczalnia.ListaSprzetu;
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string fraza = txtSearch.Text.ToLower();
            lbSprzet.ItemsSource = _wypozyczalnia.ListaSprzetu.Where(s => s.Opis().ToLower().Contains(fraza)).ToList();
        }

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
            var wybranyRodzaj = (cbTypSprzetu.SelectedItem as ComboBoxItem)?.Content.ToString();

            string producent = txtProducent.Text;
            decimal cenaZaDzien = decimal.Parse(txtCena.Text);
            int rokProdukcji = int.Parse(txtRok.Text);
            int rozmiar = int.Parse(txtRozmiar.Text);
            bool dlaDziecka = (bool)chkDlaDziecka.IsChecked;

            try
            {
                switch (wybranyRodzaj)
                {
                case "Narty":
                    int dlugosc = int.Parse(txtDlugosc.Text);
                    TypNart typNart = TypNart.Biegowe;
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


            lbSprzet.Items.Refresh();

        }
    }
}
