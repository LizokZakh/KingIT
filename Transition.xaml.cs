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

namespace KingIT
{
    /// <summary>
    /// Логика взаимодействия для Transition.xaml
    /// </summary>
    public partial class Transition : Window
    {
        public Transition()
        {
            InitializeComponent();
        }

        private void TTs_Click(object sender, RoutedEventArgs e)
        {
            ShopsCenters ShopsCenters = new ShopsCenters();
            this.Close();
            ShopsCenters.Show();
        }

        //private void Users_Click(object sender, RoutedEventArgs e)
        //{
        //    Arenda arenda = new Arenda();
        //    this.Close();
        //    arenda.Show();
        //}
    }
}
