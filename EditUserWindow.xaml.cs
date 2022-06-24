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
    /// Логика взаимодействия для EditUserWindow.xaml
    /// </summary>
    public partial class EditUserWindow : Window
    {
        public Employees CurrentEmployee { get; set; }
        public List<string> RoleCollection { get; set; }
        public List<string> GenderCollection { get; set; }

        public EditUserWindow(Employees emp)
        {
            InitializeComponent();
            if (emp != null)
                CurrentEmployee = emp;
            else
                CurrentEmployee = new Employees();
            RoleCollection = Shopping_CenterEntities.GetContext().Employees.Select(x => x.Role).Distinct().ToList();
            GenderCollection = Shopping_CenterEntities.GetContext().Employees.Select(x => x.Gender).Distinct().ToList();
            DataContext = this;
        }

        private void LoadPicture_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image Files | *.BMP;*.JPG;*.PNG";
            fileDialog.InitialDirectory = @"C:\Users\Zakha\OneDrive\Рабочий стол\ТЦ";

            fileDialog.Title = "Выбор фото сотрудника";

            if (fileDialog.ShowDialog() == true)
            {
                CurrentEmployee.Photo = File.ReadAllBytes(fileDialog.FileName);
                MessageBox.Show(" Файл выбран ");
                DataContext = null;
                DataContext = this;
            }
        }

        private void ButBack_Click(object sender, RoutedEventArgs e)
        {
            Shopping_CenterEntities.GetContext().Entry(CurrentEmployee).Reload();
            BackStep();
        }

        private void ButSave_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(CurrentEmployee.Surname) &&
                !string.IsNullOrWhiteSpace(CurrentEmployee.Name) &&
                !string.IsNullOrWhiteSpace(CurrentEmployee.Secondname) &&
                !string.IsNullOrWhiteSpace(CurrentEmployee.Login) &&
                !string.IsNullOrWhiteSpace(CurrentEmployee.Password) &&
                !string.IsNullOrWhiteSpace(CurrentEmployee.Gender) &&
                !string.IsNullOrWhiteSpace(CurrentEmployee.Role) &&
                !string.IsNullOrWhiteSpace(CurrentEmployee.Telephone) &&
                CurrentEmployee.Photo != null)
            {
                if (CurrentEmployee.ID_Employees == 0)
                {
                    CurrentEmployee.ID_Employees = Shopping_CenterEntities.GetContext().Employees.Max(x => x.ID_Employees) + 1;
                    Shopping_CenterEntities.GetContext().Employees.Add(CurrentEmployee);
                }
                Shopping_CenterEntities.GetContext().SaveChanges();
                BackStep();
            }
            else MessageBox.Show("Не удалось произвести сохранение");
        }

        private void BackStep()
        {
            Users win1 = new Users();
            win1.Show();
            this.Close();
        }
    }
}
