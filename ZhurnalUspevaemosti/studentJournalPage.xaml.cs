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
    /// Логика взаимодействия для studentJournalPage.xaml
    /// </summary>
    public partial class studentJournalPage : Page
    {
        SQLCommands sqlCmds = new SQLCommands();

        private bool IsEven(int a)
        {
            return (a % 2) == 0;
        }

        public studentJournalPage()
        {
            InitializeComponent();

            DataTable subjectsTable = new DataTable();
            DataTable scoreTable = new DataTable();

            sqlCmds.selectCmd(subjectsTable, "Select * FROM Subjects");

            for (int i = 0; i < subjectsTable.Rows.Count; i++)
            {
               
                TextBlock textBlock = new TextBlock();
                ColumnDefinition col = new ColumnDefinition();

                sqlCmds.selectCmd(scoreTable, $"Select date, score FROM {subjectsTable.Rows[i][1]} WHERE student_id = 17");

                board.ColumnDefinitions.Add(col);
                board.Children.Add(textBlock);
                Grid.SetColumn(textBlock, i);

                textBlock.FontSize = 16;
                textBlock.Padding = new Thickness(20);
                textBlock.Background = Brushes.LightGray;
                textBlock.Width = 150;
                textBlock.Height = 200;

                textBlock.Text = subjectsTable.Rows[i][1].ToString();
                

                for (int j = 0; j < scoreTable.Rows.Count; j++)
                { 
                    textBlock.Text += System.Environment.NewLine;
                    textBlock.Text += scoreTable.Rows[j][0].ToString().Remove(5) + " - " + scoreTable.Rows[j][1].ToString() + " ";

                }
                scoreTable.Clear();
            }
        }
    }
}
