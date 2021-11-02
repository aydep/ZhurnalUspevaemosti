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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using System.Data;

namespace ZhurnalUspevaemosti
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Sign_Click(object sender, RoutedEventArgs e)
        {
            string login = loginTextBox.Text.Trim(); //Корректировка введеных данных
            string pass = passwordBox.Password.Trim();

            roleBox.ToolTip = ""; //Очистка непраильных полей
            roleBox.Background = Brushes.Transparent;
            loginTextBox.ToolTip = "";
            loginTextBox.Background = Brushes.Transparent;
            passwordBox.ToolTip = "";
            passwordBox.Background = Brushes.Transparent;

            if (roleBox.Text == "") //Проверка корректности данных
            {
                roleBox.ToolTip = "Выберите роль";
                roleBox.Background = Brushes.IndianRed;
            } else if (login.Length < 5)
            {
                loginTextBox.ToolTip = "Слишком коротко";
                loginTextBox.Background = Brushes.IndianRed;
            } else if(pass.Length < 6) 
            {
                passwordBox.ToolTip = "Слишком коротко";
                passwordBox.Background = Brushes.IndianRed;
            } else {
                DB db = new DB(); //Назначение локальной таблицы и адаптера 

                DataTable table = new DataTable();

                MySqlDataAdapter adapter = new MySqlDataAdapter();

                MySqlCommand command = new MySqlCommand("SELECT * FROM users WHERE `login` = @uL AND `password` = @uP AND `role`=@uR", db.getConnection());
                command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = login;
                command.Parameters.Add("@uP", MySqlDbType.VarChar).Value = pass;
                command.Parameters.Add("@uR", MySqlDbType.VarChar).Value = roleBox.Text;

                db.openConnection();//Открытие подключения к БД

                adapter.SelectCommand = command;
                adapter.Fill(table);

                if (table.Rows.Count > 0)
                {
                    this.Close();
                    MessageBox.Show("Yes");
                }
                else
                {
                    MessageBox.Show("No");

                }

                db.closeConnection();//Закрытие подключения к БД


                //MySqlCommand command = new MySqlCommand("INSERT INTO `users` (`id`, `login`, `password`, `email`) VALUES (NULL, @uL, @uP, @uE)", db.getConnection()); //Формирование текста запроса
                //command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = login;
                //command.Parameters.Add("@uP", MySqlDbType.VarChar).Value = pass_1;
                //command.Parameters.Add("@uE", MySqlDbType.VarChar).Value = email;


                //if (command.ExecuteNonQuery() == 1)
                //{
                //    MessageBox.Show("Yes");
                //}
                //else
                //{
                //    MessageBox.Show("No");
                //}

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
