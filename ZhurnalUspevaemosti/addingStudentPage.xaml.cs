using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ZhurnalUspevaemosti
{
    /// <summary>
    /// Логика взаимодействия для addingStudentPage.xaml
    /// </summary>
    public partial class addingStudentPage : Page
    {
        SQLCommands sqlCmds = new SQLCommands();

        public addingStudentPage()
        {
            InitializeComponent();
            
            DataTable table = new DataTable();

            sqlCmds.selectCmd(table, "SELECT `class_name` FROM Classes");

            if (table.Rows.Count < 1)
            {
                classComboBox.ToolTip = "Нет добавленных классов!";
            }
            else
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    classComboBox.Items.Add(table.Rows[i][0].ToString());
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string Login = loginTextBox.Text.Trim(); 
            string name = nameTextBox.Text.Trim(); 
            string surname = surnameTextBox.Text.Trim(); 
            string Pass1 = passwordBox1.Password.Trim();
            string Pass2 = passwordBox2.Password.Trim();

            DataTable table = new DataTable();

            sqlCmds.selectCmd(table, $"SELECT `login` FROM Students WHERE login='{Login}'");

            if (nameTextBox.Text == "" || surnameTextBox.Text == "" || datePicker.SelectedDate.Value.ToString() == "" || classComboBox.Text == "" || loginTextBox.Text == "" || passwordBox1.Password == "" || passwordBox2.Password == "") //Проверка корректности данных
            {
                MessageBox.Show("Заполните все поля!");
            }
            else if (table.Rows.Count > 0)
            {
                MessageBox.Show("Такой логин уже существует!");
            }
            else if (Login.Length < 6)
            {
                MessageBox.Show("Слишком короткий логин!");
            }
            else if (Pass1.Length < 6)
            {
                MessageBox.Show("Слишком короткий пароль!");
            }
            else if (Pass2 != Pass1)
            {
                MessageBox.Show("Пароли не совпадают");
            }
            else
            {
                string date = datePicker.SelectedDate.Value.Year.ToString() + "." + datePicker.SelectedDate.Value.Month.ToString() + "." + datePicker.SelectedDate.Value.Day.ToString();

                sqlCmds.insertCmd($"INSERT INTO `Students` (`class_name`, `name`, `surname`, `birth_date`, `avtar`, `login`, `password`) VALUES ('{classComboBox.Text}', '{name}', '{surname}', '{date}', '', '{Login}', '{Pass1}')");

                MessageBox.Show("Ученик успешно добавлен!");
            }

        }
    }
}
