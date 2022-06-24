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
    /// Логика взаимодействия для Shopping.xaml
    /// </summary>
    public partial class ShopsCenters : Window
    {
        public ShopsCenters()
        {
            InitializeComponent();
            DGridShopping.ItemsSource = Shopping_CenterEntities.GetContext().Shop_Centers.OrderBy(x => x.City).ThenBy(x => x.Status_Center).Where(x => x.Status_Center != "Удален").Where(x => x.Coef_Add_Price > 0.1).ToList();
            ComboCity.ItemsSource = Shopping_CenterEntities.GetContext().Shop_Centers.Select(x => x.City).Distinct().ToList();
            ComboStatus.ItemsSource = Shopping_CenterEntities.GetContext().Shop_Centers.Where(x => x.Status_Center != "Удален").Select(x => x.Status_Center).Distinct().ToList();
        }
        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            if (ComboStatus.SelectedIndex == 0) //первый элемент списка
            {
                DGridShopping.ItemsSource = Shopping_CenterEntities.GetContext().Shop_Centers.Where(x => x.Status_Center == "План").ToList();
            }
            if (ComboStatus.SelectedIndex == 1)//второй элемент списка
            {
                DGridShopping.ItemsSource = Shopping_CenterEntities.GetContext().Shop_Centers.Where(x => x.Status_Center == "Строительсто").ToList();
            }
            if (ComboStatus.SelectedIndex == 2)//третий элемент списка
            {
                DGridShopping.ItemsSource = Shopping_CenterEntities.GetContext().Shop_Centers.Where(x => x.Status_Center == "Реализация").ToList();
            }
        }

        private void ComboCity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var c = ComboCity.SelectedItem;
            List<Shop_Centers> SearchType = null;
            SearchType = Shopping_CenterEntities.GetContext().Shop_Centers.Where(b => b.City == c.ToString() && b.Status_Center != "Удален").ToList();
            DGridShopping.ItemsSource = SearchType;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Transition win1 = new Transition();
            win1.Show();
            this.Close();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Edit_TTs win = new Edit_TTs(null);
            win.Show();
            this.Close();
        }
        private void BEdit_Click(object sender, RoutedEventArgs e)
        {
            var upd = DGridShopping.SelectedItems.Cast<Shop_Centers>().FirstOrDefault();
            Edit_TTs Edit_TTs = new Edit_TTs(upd);
            this.Close();
            Edit_TTs.Show();
        }
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var ShoppingsForRemoving = DGridShopping.SelectedItems.Cast<Shop_Centers>().ToList();
            if (MessageBox.Show("Вы точно хотите Удалить эти(-от) ТЦ", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    ShoppingsForRemoving.ForEach(x => x.Status_Center = "Удален");
                    Shopping_CenterEntities.GetContext().SaveChanges();
                    MessageBox.Show("Записи удалены!");
                    DGridShopping.ItemsSource = Shopping_CenterEntities.GetContext().Shop_Centers.ToList();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
    }
}
