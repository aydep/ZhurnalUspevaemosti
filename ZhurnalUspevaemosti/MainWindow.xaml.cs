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
            string Login = loginTextBox.Text.Trim(); //Корректировка введеных данных
            string Pass = passwordBox.Password.Trim();

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
            } else if (Login.Length < 5)
            {
                loginTextBox.ToolTip = "Слишком коротко";
                loginTextBox.Background = Brushes.IndianRed;
            } else if(Pass.Length < 6) 
            {
                passwordBox.ToolTip = "Слишком коротко";
                passwordBox.Background = Brushes.IndianRed;
            } else {
                DB db = new DB(); //Назначение локальной таблицы и адаптера 

                DataTable Table = new DataTable();

                MySqlDataAdapter adapter = new MySqlDataAdapter();

                switch (roleBox.Text) //Назначение роли
                {
                    case "Ученик":
                        currentUser.Role = "Students";
                        break;

                    case "Учитель":
                        currentUser.Role = "Teachers";
                        break;

                    case "Администратор":
                        currentUser.Role = "Admins";
                        break;

                    default:
                        MessageBox.Show("Не выбрана роль");
                        break;
                }

                //Формирование запоса
                MySqlCommand command = new MySqlCommand($"SELECT * FROM ayder2s4_zhurnal.{currentUser.Role} WHERE `login` = @uL AND `password` = @uP", db.getConnection());
                command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = Login;
                command.Parameters.Add("@uP", MySqlDbType.VarChar).Value = Pass;

                

                db.openConnection();//Открытие подключения к БД
                MessageBox.Show(roleBox.Text);
                adapter.SelectCommand = command;
                adapter.Fill(Table);

                if (Table.Rows.Count > 0) //Передача данных в класс curentUser и открытие основного окна
                {
                    currentUser.Name = Table.Rows[0][2].ToString();
                    currentUser.Surename = Table.Rows[0][3].ToString();
                    currentUser.Id = int.Parse(Table.Rows[0][0].ToString());
                    currentUser.ClassId = int.Parse(Table.Rows[0][1].ToString());

                    this.Close();
                    MessageBox.Show("Yes " + currentUser.Name + " " + currentUser.Surename + " " + currentUser.Role + " " + currentUser.Id);
                }
                else
                {
                    MessageBox.Show("No");

                }

                db.closeConnection();//Закрытие подключения к БД


                //MySqlCommand command = new MySqlCommand("INSERT INTO `users` (`id`, `Login`, `password`, `email`) VALUES (NULL, @uL, @uP, @uE)", db.getConnection()); //Формирование текста запроса
                //command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = Login;
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

    }
}
