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
    /// Логика взаимодействия для Edit_TTs.xaml
    /// </summary>
    public partial class Edit_TTs : Window
    {
        private Shop_Centers _currentShop = new Shop_Centers();
        private int reg = 0;
        int maxid = Shopping_CenterEntities.GetContext().Shop_Centers.Max(g => g.ID_Center);
        public Edit_TTs(Shop_Centers selectedShop)
        {
            InitializeComponent();
            if (selectedShop != null)
            {
                _currentShop = selectedShop;
                reg = 1;
            }
            else
            {
                _currentShop.ID_Center = maxid + 1;
            }

            DataContext = new { currentShop = _currentShop, listStatus = Shopping_CenterEntities.GetContext().Shop_Centers.Select(x => x.Status_Center).Distinct().ToList() };
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image Files | *.BMP;*.JPG;*.PNG";
            fileDialog.InitialDirectory = @"C:\Users\Zakha\OneDrive\Рабочий стол\ТЦ\Image ТЦ";

            fileDialog.Title = "Выбор фото ТЦ";

            if (fileDialog.ShowDialog() == true)
            {

                _currentShop.Photo = File.ReadAllBytes(fileDialog.FileName);
            }
            MessageBox.Show(" Файл выбран ");
        }

        private void ButSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentShop.Name_Center.ToString()))
                errors.AppendLine("Укажите название");
            if (string.IsNullOrWhiteSpace(_currentShop.City.ToString()))
                errors.AppendLine("Укажите город");
            if (string.IsNullOrWhiteSpace(_currentShop.Quantity_pavilions.ToString()))
                errors.AppendLine("Укажите количество павильонов");
            if (string.IsNullOrWhiteSpace(_currentShop.Cost.ToString()))
                errors.AppendLine("Укажите стоимость тц");
            if (string.IsNullOrWhiteSpace(_currentShop.Coef_Add_Price.ToString()))
                errors.AppendLine("Укажите коэф.добав.стоим.");
            if (string.IsNullOrWhiteSpace(_currentShop.Number_floors.ToString()))
                errors.AppendLine("Укажите этажность");
            if (reg == 0) Shopping_CenterEntities.GetContext().Shop_Centers.Add(_currentShop);

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }


            try
            {
                Shopping_CenterEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена. Обновите таблицу");
                _currentShop = new Shop_Centers();
                ShopsCenters win = new ShopsCenters();
                win.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void ButPavilion_Click(object sender, RoutedEventArgs e)
        {
            var currentShop = _currentShop;
            PavilionTable PavilionTable = new PavilionTable(currentShop);
            this.Close();
            PavilionTable.Show();
        }
    }
}
