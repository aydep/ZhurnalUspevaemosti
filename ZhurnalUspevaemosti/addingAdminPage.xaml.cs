using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Controls;
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
    /// Логика взаимодействия для addingAdminPage.xaml
    /// </summary>
    public partial class addingAdminPage : Page
    {
        public addingAdminPage()
        {
            InitializeComponent();
        }

        DB db = new DB();
        MySqlDataAdapter adapter = new MySqlDataAdapter();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string Login = loginTextBox.Text.Trim();
            string name = nameTextBox.Text.Trim();
            string surname = surnameTextBox.Text.Trim();
            string Pass1 = passwordBox1.Password.Trim();
            string Pass2 = passwordBox2.Password.Trim();

            DataTable table = new DataTable();

            MySqlCommand loginCommand = new MySqlCommand($"SELECT `login` FROM Admins WHERE login='{Login}'", db.getConnection());

            adapter.SelectCommand = loginCommand;
            adapter.Fill(table);

            if (nameTextBox.Text == "" || surnameTextBox.Text == "" || loginTextBox.Text == "" || passwordBox1.Password == "" || passwordBox2.Password == "") //Проверка корректности данных
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
                MySqlCommand command = new MySqlCommand($"INSERT INTO `Admins` (`name`, `surname`, `avatar`, `login`, `password`) VALUES ('{name}', '{surname}', '', '{Login}', '{Pass1}')", db.getConnection());

                db.openConnection();
                command.ExecuteNonQuery();
                db.closeConnection();

                MessageBox.Show("Администратор успешно добавлен!");
            }
        }
    }
}
