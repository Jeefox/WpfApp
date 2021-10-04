using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;


namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void HamburgerMenuNavigationButton_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Page2());
            MainWindow.AddDataToTable();
        }
    }
}
