using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ZhurnalUspevaemosti
{
    /// <summary>
    /// Логика взаимодействия для journalPage.xaml
    /// </summary>
    public partial class journalPage : Page
    {
        SQLCommands sqlCmds = new SQLCommands();

        public journalPage()
        {
            InitializeComponent();
            DataTable classesTable = new DataTable();

            sqlCmds.selectCmd(classesTable, "Select * FROM ayder2s4_zhurnal.Classes");

            for (int i = 0; i < classesTable.Rows.Count; i++)
            {
                classComboBox.Items.Add(classesTable.Rows[i][0]);
                classScoreComboBox.Items.Add(classesTable.Rows[i][0]);
            }

            if (currentUser.Role == "Teachers")
            {
                DataTable subjectTable = new DataTable();

                sqlCmds.selectCmd(subjectTable, $"Select teacher_subject FROM Teachers WHERE teacher_id='{currentUser.Id}'");

                for (int i = 0; i < subjectTable.Rows.Count; i++)
                {
                    subjectComboBox.Items.Add(subjectTable.Rows[i][0]);
                    subjectScoreComboBox.Items.Add(subjectTable.Rows[i][0]);
                }
            }
            else
            {
                DataTable subjectTable = new DataTable();

                sqlCmds.selectCmd(subjectTable, $"Select * FROM Subjects");

                for (int i = 0; i < subjectTable.Rows.Count; i++)
                {
                    subjectComboBox.Items.Add(subjectTable.Rows[i][1]);
                    subjectScoreComboBox.Items.Add(subjectTable.Rows[i][1]);
                }
            }

            if (currentUser.Role == "Student")
            {
                addingScorePannel.Visibility = Visibility.Hidden;
            }
            
        }

        private void showButton_Click(object sender, RoutedEventArgs e)
        {
            subjectComboBox.ToolTip = "";
            subjectComboBox.Background = Brushes.Transparent;

            if (subjectComboBox.Text != "")
            {
                

                DataTable subjectsTable = new DataTable();
                DataTable studTable = new DataTable();

                sqlCmds.selectCmd(studTable, $"SELECT Students.student_id, Students.name FROM Students,Classes WHERE Classes.class_name='{classComboBox.Text}'");
                
                /*  Пример запроса
                    SELECT English.date, English.lesson,
                        MAX(CASE WHEN student_id = 17 THEN score END) AS S1,
                        MAX(CASE WHEN student_id = 18 THEN score END) AS S2
                        ...
                        MAX(CASE WHEN student_id = n  THEN score END) AS Sm,
                    FROM English GROUP BY English.lesson
                */

                string preparedCommand = $"SELECT {subjectComboBox.Text}.date, {subjectComboBox.Text}.lesson, ";

                for (int i = 0; i < studTable.Rows.Count; i++)
                {
                    preparedCommand += $"MAX(CASE WHEN student_id = {studTable.Rows[i][0]} THEN score END) AS {studTable.Rows[i][1]}, ";
                }
                                               
                preparedCommand = preparedCommand.Substring(0, preparedCommand.Length - 2);
                preparedCommand += $" FROM {subjectComboBox.Text} GROUP BY {subjectComboBox.Text}.lesson";

                sqlCmds.selectCmd(subjectsTable, preparedCommand);

                dataGrid.DataContext = subjectsTable;
            } else
            {
                subjectComboBox.ToolTip = "Выберите предмет";
                subjectComboBox.Background = Brushes.IndianRed;
            }
            
           
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void scoreAddButton_Click(object sender, RoutedEventArgs e)
        {
            String[] student = studentScoreComboBox.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            sqlCmds.insertCmd($"INSERT INTO `{subjectScoreComboBox.Text}` (`date`, `lesson`, `student_id`, `score`) VALUES (CURRENT_DATE(), '{lessonComboBox.Text.ToString()}', (SELECT student_id FROM Students WHERE name='{student[1]}' AND surname='{student[0]}'), '{ScoreComboBox.Text}')");       
        }

        private void classScoreComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            DataTable table = new DataTable();
            
            sqlCmds.selectCmd(table, $"SELECT DISTINCT name, surname FROM Students WHERE class_name ='{classScoreComboBox.Text}' GROUP BY surname ");
            
            studentScoreComboBox.Items.Clear();

            for (int i = 0; i < table.Rows.Count; i++)
            {
                studentScoreComboBox.Items.Add(table.Rows[i][1].ToString() + " " + table.Rows[i][0].ToString());
            }
        }
    }
}
