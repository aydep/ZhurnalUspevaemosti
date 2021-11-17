using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ZhurnalUspevaemosti
{
    /// <summary>
    /// Логика взаимодействия для addingClassPage.xaml
    /// </summary>
    public partial class addingClassPage : Page
    {
        public addingClassPage()
        {
            InitializeComponent();
        }

        DB db = new DB();
        MySqlDataAdapter adapter = new MySqlDataAdapter();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataTable table = new DataTable();

            MySqlCommand checkCommands = new MySqlCommand($"SELECT * FROM Classes WHERE class_name='{classNameTextBox.Text}'", db.getConnection());

            adapter.SelectCommand = checkCommands;
            adapter.Fill(table);

            if (table.Rows.Count == 0)
            {
                MySqlCommand insertCommand = new MySqlCommand($"INSERT INTO `Classes` (`class_name`) VALUES ('{classNameTextBox.Text}')", db.getConnection());
                db.openConnection();
                insertCommand.ExecuteNonQuery();
                db.closeConnection();
            }
            else
            {
                MessageBox.Show("Такой класс уже существует!");
            }

            
        }
    }
}
