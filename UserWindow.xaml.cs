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
    /// Логика взаимодействия для Users.xaml
    /// </summary>
    public partial class Users : Window
    {
        public List<Employees> EmployeesList { get; set; }
        public Employees CurrentEmployee { get; set; }
        public Users()
        {
            InitializeComponent();
            EmployeesList = Shopping_CenterEntities.GetContext().Employees.ToList();
            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentEmployee != null)
                MoveOnTheNextWindow(CurrentEmployee);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MoveOnTheNextWindow(null);
        }

        private void MoveOnTheNextWindow(Employees emp)
        {
            EditUserWindow win = new EditUserWindow(emp);
            win.Show();
            this.Close();
            //Создать окно
            //открыть окно и т.п. (передавать emp, если null значит новый)
        }
    }
}
