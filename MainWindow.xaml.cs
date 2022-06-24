using System.Windows;
using System.Linq;
using System.Windows.Controls;

namespace KingIT
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static int employeNumber;
        static int counterButton; //считает, сколько раз была нажата кнопка входа
        public MainWindow()
        {
            InitializeComponent();
            Login.MaxLength = 60;
            Password.MaxLength = 6;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (Shopping_CenterEntities db = new Shopping_CenterEntities())
            {
                var sqlUser = (from employee in db.Employees
                               where employee.Login.ToLower() == Login.Text.ToLower() && employee.Password == Password.Password
                               select employee).FirstOrDefault();

                if (sqlUser != null)
                {
                    employeNumber = sqlUser.ID_Employees;
                    if (sqlUser.Role == "Администратор")
                    {
                        AppInfo.SetEmployee(sqlUser.ID_Employees);

                        Users users = new Users();
                        this.Close();
                        users.Show();
                    }
                    else if (sqlUser.Role == "Менеджер А" || sqlUser.Role == "Менеджер С")
                    {
                        Transition Transition = new Transition();
                        this.Close();
                        Transition.Show();
                    }    
                   
                }
                else
                {
                    MessageBox.Show("Пользователь не найден", "", MessageBoxButton.OK);
                }
            }
            counterButton++;

            if (counterButton >= 3)
            {
                Captcha captcha = new Captcha();
                captcha.Show();
            }
        }
    }
}
