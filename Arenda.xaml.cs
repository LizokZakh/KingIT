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
    /// Логика взаимодействия для Arenda.xaml
    /// </summary>
    public partial class Arenda : Window
    {
        private Pavilion pavilion;
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
        public List<Tenants> tenantsCollection { get; set; }
        public Tenants currentTenants { get; set; }
        public Arenda(Pavilion pavilion)
        {
            InitializeComponent();
            this.pavilion = pavilion;
            Start = DateTime.Today;
            Stop = DateTime.Today;
            tenantsCollection = Shopping_CenterEntities.GetContext().Tenants.ToList();
            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Start <= Stop && Start >= DateTime.Today) 
            {
                bool stat = Start == DateTime.Today;
                try
                {
                    Shopping_CenterEntities.GetContext().RentOrBookPavilionInMall(!stat, pavilion.Number_Pavilion, pavilion.ID_Center, Start, Stop, currentTenants.ID_Renters, MainWindow.employeNumber);
                    if (stat == true)
                    {
                        MessageBox.Show("Арендовано");
                    }
                    else
                    {
                        MessageBox.Show("Забронировано");
                    }
                    //MessageBox.Show(stat ? "Арендовано" : "Забронировано");
                }
                    catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }

        }

        private void ButBack_Click(object sender, RoutedEventArgs e)
        {
            Edit_Pavilions win1 = new Edit_Pavilions(pavilion);
            win1.Show();
            this.Close();
        }
    }
}
