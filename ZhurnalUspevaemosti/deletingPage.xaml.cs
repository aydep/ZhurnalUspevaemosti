using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
    /// Логика взаимодействия для deletingPage.xaml
    /// </summary>
    public partial class deletingPage : Page
    {
        SQLCommands sqlCmds = new SQLCommands();
        DataTable table = new DataTable();


        public deletingPage()
        {
            InitializeComponent();
        }

        private void ComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            table.Clear();
            objectBox.Items.Clear();
            switch (roleBox.Text)
            {
                case "Ученика":
                    sqlCmds.selectCmd(table, "Select * FROM Students");
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        objectBox.Items.Add(table.Rows[i][2].ToString() + " " + table.Rows[i][3]);
                    }
                    break;

                case "Учителя":
                    sqlCmds.selectCmd(table, "Select * FROM Teachers");
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        objectBox.Items.Add(table.Rows[i][2].ToString() + " " + table.Rows[i][3]);
                    }
                    break;

                case "Предмет":
                    sqlCmds.selectCmd(table, "Select * FROM Subjects");
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        objectBox.Items.Add(table.Rows[i][1].ToString());
                    }
                    break;

                default:
                    break;
            }

            
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            switch (roleBox.Text)
            {
                case "Ученика":
                    sqlCmds.insertCmd($"DELETE FROM `Students` WHERE `Students`.`student_id` = {table.Rows[objectBox.SelectedIndex][0]}");
                    break;

                case "Учителя":
                    sqlCmds.insertCmd($"DELETE FROM `Teachers` WHERE `Teachers`.`teacher_id` = {table.Rows[objectBox.SelectedIndex][0]}");
                    break;

                case "Предмет":
                    sqlCmds.insertCmd($"DELETE FROM `Subjects` WHERE `Subjects`.`subject_id` = {table.Rows[objectBox.SelectedIndex][0]}");
                    sqlCmds.insertCmd($"DROP TABLE `{table.Rows[objectBox.SelectedIndex][1]}`");
                    break;

                default:
                    break;
            }
        }
    }
}
