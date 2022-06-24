using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для Edit_Pavilions.xaml
    /// </summary>
    public partial class Edit_Pavilions : Window
    {
        private Pavilion _currentPavilion = new Pavilion();
        private int reg = 0;
        int maxid = Shopping_CenterEntities.GetContext().Pavilion.Max(g => g.ID_Center);
        public Edit_Pavilions(Pavilion selectedPavilion)
        {
            InitializeComponent();
            if (selectedPavilion != null)
            {
                _currentPavilion = selectedPavilion;
                reg = 1;
            }
            else
            {
                _currentPavilion.ID_Center = maxid + 1;
            }

            DataContext = new { currentShop = _currentPavilion, listStatus = Shopping_CenterEntities.GetContext().Pavilion.Select(x => x.Status_Pavilion).Distinct().ToList() };
        }

        private void ButSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentPavilion.Floor.ToString()))
                errors.AppendLine("Укажите номер этажа");
            if (string.IsNullOrWhiteSpace(_currentPavilion.Number_Pavilion.ToString()))
                errors.AppendLine("Укажите номер павилиона");
            if (string.IsNullOrWhiteSpace(_currentPavilion.Square.ToString()))
                errors.AppendLine("Укажите площадь");
            if (string.IsNullOrWhiteSpace(_currentPavilion.Added_value_factor.ToString()))
                errors.AppendLine("Укажите коэф.добав.стоим.");
            if (string.IsNullOrWhiteSpace(_currentPavilion.Cost_per_sq_m.ToString()))
                errors.AppendLine("Укажите стоимость за кв.м");
            if (reg == 0) Shopping_CenterEntities.GetContext().Pavilion.Add(_currentPavilion);

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }


            try
            {
                Shopping_CenterEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена. Обновите таблицу");
                _currentPavilion = new Pavilion();
                ShopsCenters win = new ShopsCenters();
                win.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void ButArenda_Click(object sender, RoutedEventArgs e)
        {
            //if (Shopping_CenterEntities.GetContext().Pavilion.Max(x => x.Number_Pavilion))
            Arenda arenda = new Arenda(_currentPavilion);
            this.Close();
            arenda.Show();
        }
    }
}
