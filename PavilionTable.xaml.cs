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
    /// Логика взаимодействия для PavilionTable.xaml
    /// </summary>
    public partial class PavilionTable : Window
    {
        string _name;
        string _name2;
        int idShop = 0;
        public PavilionTable(Shop_Centers currentShop)
        {
            InitializeComponent();
            idShop = currentShop.ID_Center;
            DGridPavilions.ItemsSource = Shopping_CenterEntities.GetContext().Pavilion.Where(x => x.Added_value_factor > 0.1 && x.ID_Center == idShop).ToList();
            ComboFloor.ItemsSource = Shopping_CenterEntities.GetContext().Pavilion.Select(x => x.Floor).Distinct().ToList();
            ComboStatus.ItemsSource = Shopping_CenterEntities.GetContext().Pavilion.Select(x => x.Status_Pavilion).Distinct().ToList();

        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            ShopsCenters win1 = new ShopsCenters();
            win1.Show();
            this.Close();
        }

        private void ComboFloor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var c = ComboFloor.SelectedItem;
            List<Pavilion> SearchType = null;
            SearchType = Shopping_CenterEntities.GetContext().Pavilion.Where(b => b.Floor.ToString() == c.ToString() && b.Added_value_factor > 0.1 && b.Added_value_factor > 0.1 && b.ID_Center == idShop).ToList();
            DGridPavilions.ItemsSource = SearchType;
        }

        private void ComboStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var c = ComboStatus.SelectedItem;
            List<Pavilion> SearchType = null;
            SearchType = Shopping_CenterEntities.GetContext().Pavilion.Where(b => b.Status_Pavilion.ToString() == c.ToString() && b.Added_value_factor > 0.1 && b.ID_Center == idShop).ToList();
            DGridPavilions.ItemsSource = SearchType;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Edit_Pavilions win = new Edit_Pavilions(null);
            win.Show();
            this.Close();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var PavilionsForRemoving = DGridPavilions.SelectedItems.Cast<Pavilion>().ToList();
            if (MessageBox.Show("Вы точно хотите удалить эти(-от) ТЦ", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    PavilionsForRemoving.ForEach(x => x.Status_Pavilion = "Удален");
                    Shopping_CenterEntities.GetContext().SaveChanges();
                    MessageBox.Show("Записи удалены!");
                    DGridPavilions.ItemsSource = Shopping_CenterEntities.GetContext().Pavilion.Where(b => b.Added_value_factor > 0.1).ToList();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var upd = DGridPavilions.SelectedItems.Cast<Pavilion>().FirstOrDefault();
            Edit_Pavilions win = new Edit_Pavilions(upd);
            win.Show();
            this.Close();
        }


        private void SquareTextTo_TextChanged(object sender, TextChangedEventArgs e)
        {
            _name = SquareTextFrom.Text;
            _name2 = SquareTextTo.Text;
            double num1 = 0;
            double.TryParse(_name, out num1);
            double num2 = 0;
            double.TryParse(_name2, out num2);

            DGridPavilions.ItemsSource = Shopping_CenterEntities.GetContext().Pavilion.Where(b => b.Square > num1 && b.Square < num2 && b.ID_Center == idShop && b.Added_value_factor > 0.1).ToList();
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            _name = SquareTextFrom.Text;
            _name2 = SquareTextTo.Text;
            double num1 = 0;
            double.TryParse(_name, out num1);
            double num2 = 0;
            double.TryParse(_name2, out num2);
            var c = ComboStatus.SelectedItem;
            var a = ComboFloor.SelectedItem;

            try
            {
                if (c == null)
                    DGridPavilions.ItemsSource = Shopping_CenterEntities.GetContext().Pavilion.Where(b => b.Square > num1 && b.Square < num2 && b.ID_Center == idShop && b.Added_value_factor > 0.1 && b.Floor.ToString() == a.ToString()).ToList();
                if (a == null && num1.ToString() != null || num2.ToString() != null)
                    DGridPavilions.ItemsSource = Shopping_CenterEntities.GetContext().Pavilion.Where(b => b.Square > num1 && b.Square < num2 && b.ID_Center == idShop && b.Added_value_factor > 0.1 && b.Status_Pavilion.ToString() == c.ToString()).ToList();
                if (num1 == 0 && num2 == 0)
                    DGridPavilions.ItemsSource = Shopping_CenterEntities.GetContext().Pavilion.Where(b => b.ID_Center == idShop && b.Floor.ToString() == a.ToString() && b.Added_value_factor > 0.1 && b.Status_Pavilion.ToString() == c.ToString()).ToList();
                if (num1.ToString() == null && num2.ToString() == null)
                    DGridPavilions.ItemsSource = Shopping_CenterEntities.GetContext().Pavilion.Where(b => b.ID_Center == idShop && b.Floor.ToString() == a.ToString() && b.Added_value_factor > 0.1 && b.Status_Pavilion.ToString() == c.ToString()).ToList();
                if (num1 != 0 || num2 != 0 && c != null && a != null)
                    DGridPavilions.ItemsSource = Shopping_CenterEntities.GetContext().Pavilion.Where(b => b.Square > num1 && b.Square < num2 && b.ID_Center == idShop && b.Floor.ToString() == a.ToString() && b.Added_value_factor > 0.1 && b.Status_Pavilion.ToString() == c.ToString()).ToList();
                if (a == null && c != null && num1.ToString() != null && num2.ToString() != null)
                    DGridPavilions.ItemsSource = Shopping_CenterEntities.GetContext().Pavilion.Where(b => b.Square > num1 && b.Square < num2 && b.ID_Center == idShop && b.Added_value_factor > 0.1 && b.Status_Pavilion.ToString() == c.ToString()).ToList();
                if (a == null && num1 != 0 && num2 != 0)
                    DGridPavilions.ItemsSource = Shopping_CenterEntities.GetContext().Pavilion.Where(b => b.Square > num1 && b.Square < num2 && b.ID_Center == idShop && b.Added_value_factor > 0.1 && b.Status_Pavilion.ToString() == c.ToString()).ToList();
            }
            catch
            {
            }
        }
    }    
}
