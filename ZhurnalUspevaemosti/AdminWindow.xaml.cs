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
using System.Windows.Shapes;

namespace ZhurnalUspevaemosti
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
            user_name.Text = currentUser.Surename + " " + currentUser.Name.Substring(0, 1) + ".";
            //mainFrame.Content = new journalPage();

            if (currentUser.Role != "Admins")
            {
                addButton.Visibility = Visibility.Hidden;
            }

            if (currentUser.Role == "Students")
            {
                mainFrame.Content = new studentJournalPage();
            }
            else
            {
                mainFrame.Content = new journalPage();
            }
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void journalButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentUser.Role == "Students")
            {
                mainFrame.Navigate(new studentJournalPage());
            }
            else
            {
                mainFrame.Navigate(new journalPage());
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new addingPage());
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new deletingPage());
        }
    }
}
