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

        private void Button_Registr_Click(object sender, RoutedEventArgs e)
        {
            string login = loginTextBox.Text.Trim(); //Корректировка введеных данных
            string pass_1 = passwordBox1.Password.Trim();
            string pass_2 = passwordBox2.Password.Trim();
            string email = emailTextBox.Text.ToLower().ToLower();

            loginTextBox.ToolTip = ""; //Очистка непраильных полей
            loginTextBox.Background = Brushes.Transparent;
            passwordBox1.ToolTip = "";
            passwordBox1.Background = Brushes.Transparent;
            passwordBox2.ToolTip = "";
            passwordBox2.Background = Brushes.Transparent;
            emailTextBox.ToolTip = "";
            emailTextBox.Background = Brushes.Transparent;


            if (login.Length < 5) //Проверки корректности данных
            {
                loginTextBox.ToolTip = "Слишком коротко";
                loginTextBox.Background = Brushes.IndianRed;
            } else if(pass_1.Length < 6) 
            {
                passwordBox1.ToolTip = "Слишком коротко";
                passwordBox1.Background = Brushes.IndianRed;
            } else if(pass_1 != pass_2)
            {   
                passwordBox2.ToolTip = "Пароли не одинаковы";
                passwordBox2.Background = Brushes.IndianRed;
            } else if(email.Length < 5 || !email.Contains("@") || !email.Contains("."))
            {   
                emailTextBox.ToolTip = "Не корректно";
                emailTextBox.Background = Brushes.IndianRed;
            } else {
                DB db = new DB(); //Назначение локальной таблицы и адаптера 

                DataTable table = new DataTable();

                MySqlDataAdapter adapter = new MySqlDataAdapter();

                //MySqlCommand command = new MySqlCommand("SELECT * FROM users WHERE `login` = @uL AND `password` = @uP", db.getConnection());
                //command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = login;
                //command.Parameters.Add("@uP", MySqlDbType.VarChar).Value = pass_1;
                ////command.Parameters.Add("@uE", MySqlDbType.VarChar).Value = email;

                //adapter.SelectCommand = command;
                //adapter.Fill(table);

                //if (table.Rows.Count > 0)
                //{
                //    MessageBox.Show("Yes");
                //}
                //else
                //{
                //    MessageBox.Show("No");
                //}

                MySqlCommand command = new MySqlCommand("INSERT INTO `users` (`id`, `login`, `password`, `email`) VALUES (NULL, @uL, @uP, @uE)", db.getConnection()); //Формирование текста запроса
                command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = login;
                command.Parameters.Add("@uP", MySqlDbType.VarChar).Value = pass_1;
                command.Parameters.Add("@uE", MySqlDbType.VarChar).Value = email;

                db.openConnection();//Открытие подключения к БД

                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Yes");
                }
                else
                {
                    MessageBox.Show("No");
                }

                db.closeConnection();//Закрытие подключения к БД
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
