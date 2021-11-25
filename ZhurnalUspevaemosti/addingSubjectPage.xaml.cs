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
        SQLCommands sqlCmds = new SQLCommands();

        public addingSubjectPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataTable table = new DataTable();

            sqlCmds.selectCmd(table, $"SELECT * FROM Subjects WHERE subject_name='{subjectNameTextBox.Text}'");

            if (table.Rows.Count == 0)
            {
                sqlCmds.insertCmd($"INSERT INTO `Subjects` (`subject_name`) VALUES ('{subjectNameTextBox.Text}')");
                sqlCmds.insertCmd($"CREATE TABLE `ayder2s4_zhurnal`.`{subjectNameTextBox.Text}` (`date` date NOT NULL,`lesson` int(11) NOT NULL,`student_id` int(11) NOT NULL,`score` int(1) NOT NULL) ENGINE = InnoDB DEFAULT CHARSET = utf8; ALTER TABLE `ayder2s4_zhurnal`.`{subjectNameTextBox.Text}` ADD KEY `student_id` (`student_id`); ");
                sqlCmds.insertCmd($"ALTER TABLE `{subjectNameTextBox.Text}` ADD FOREIGN KEY (`student_id`) REFERENCES `Students`(`student_id`) ON DELETE RESTRICT ON UPDATE RESTRICT;");
               
            }
            else
            {
                MessageBox.Show("Такой предмет уже существует!");
            }

        }
    }
}
