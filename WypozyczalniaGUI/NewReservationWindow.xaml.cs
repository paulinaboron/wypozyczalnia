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
    /// Logika interakcji dla klasy NewReservationWindow.xaml
    /// </summary>
    public partial class NewReservationWindow : Window
    {
        private Wypozyczalnia _wypozyczalnia;
        public NewReservationWindow(Wypozyczalnia wypozyczalnia)
        {
            _wypozyczalnia = wypozyczalnia;
            InitializeComponent();
        }
    }
}
