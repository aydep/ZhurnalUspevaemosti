using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;
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
    /// Логика взаимодействия для addingPage.xaml
    /// </summary>
    public partial class addingPage : Page
    {
        public addingPage()
        {
            InitializeComponent();
            addingPageFrame.Content = new addingClassPage();
        }

        private void studentRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            addingPageFrame.Navigate(new addingStudentPage());
        }

        private void teacherRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            addingPageFrame.Navigate(new addingTeacherPage());
        }

        private void classRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            addingPageFrame.Navigate(new addingClassPage());
        }

        private void adminRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            addingPageFrame.Navigate(new addingAdminPage());
        }

        private void subjectRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            addingPageFrame.Navigate(new addingSubjectPage());
        }
    }
}
