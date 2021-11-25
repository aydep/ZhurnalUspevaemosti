using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows;
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
    /// Логика взаимодействия для addingSubjectPage.xaml
    /// </summary>
    public partial class addingSubjectPage : Page
    {
        public addingSubjectPage()
        {
            InitializeComponent();
        }

        DB db = new DB();
        MySqlDataAdapter adapter = new MySqlDataAdapter();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataTable table = new DataTable();

            MySqlCommand checkCommands = new MySqlCommand($"SELECT * FROM Subjects WHERE subject_name='{subjectNameTextBox.Text}'", db.getConnection());

            adapter.SelectCommand = checkCommands;
            adapter.Fill(table);

            if (table.Rows.Count == 0)
            {
                MySqlCommand insertCommand = new MySqlCommand($"INSERT INTO `Subjects` (`subject_name`) VALUES ('{subjectNameTextBox.Text}')", db.getConnection());
                db.openConnection();
                insertCommand.ExecuteNonQuery();
                db.closeConnection();
            }
            else
            {
                MessageBox.Show("Такой предмет уже существует!");
            }

        }
    }
}
