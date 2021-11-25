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
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        SQLCommands sqlCmds = new SQLCommands();

        public AuthWindow()
        {
            InitializeComponent();
        }

        AdminWindow admWin;

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

                DataTable Table = new DataTable();

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

                //Запрос
                sqlCmds.selectCmd(Table, $"SELECT * FROM ayder2s4_zhurnal.{currentUser.Role} WHERE `login` = '{Login}' AND `password` = '{Pass}';");

                if (Table.Rows.Count > 0) //Передача данных в класс curentUser и открытие основного окна
                {
                    currentUser.Name = Table.Rows[0][2].ToString();
                    currentUser.Surename = Table.Rows[0][3].ToString();
                    currentUser.Id = int.Parse(Table.Rows[0][0].ToString());

                    admWin = new AdminWindow();
                    admWin.Show();

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Проверьте корректность данных!");
                }
            }
        }
    }
}
