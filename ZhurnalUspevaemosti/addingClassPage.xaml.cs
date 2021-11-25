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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataTable table = new DataTable();
            SQLCommands sqlcmds = new SQLCommands();
            sqlcmds.selectCmd(table ,$"SELECT * FROM Classes WHERE class_name='{classNameTextBox.Text}'");


            if (table.Rows.Count == 0)
            {
                sqlcmds.insertCmd($"INSERT INTO `Classes` (`class_name`) VALUES ('{classNameTextBox.Text}')");
            }
            else
            {
                MessageBox.Show("Такой класс уже существует!");
            }

            
        }
    }
}
