using System.Windows.Controls;



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

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ButClick1(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new Page2());
        }
    }
}
