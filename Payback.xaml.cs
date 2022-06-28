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
    /// Логика взаимодействия для Payback.xaml
    /// </summary>
    public partial class Payback : Window
    {
        public List<Shop_Centers> Collection { get; set; }
        public Payback()
        {
            InitializeComponent();
            Collection = Shopping_CenterEntities.GetContext().Shop_Centers.ToList();
            DataContext = this;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var data = Shopping_CenterEntities.GetContext().percent_of_tts((cb.SelectedItem as Shop_Centers).ID_Center).ToList()[0];
            Text1.Text = string.Format("Наименование: {0}", data.Name_Center);
            Text2.Text = string.Format("Город: {0}", data.City);
            Text3.Text = string.Format("Процент: {0:F2}%", data.percents);
        }
    }
}
