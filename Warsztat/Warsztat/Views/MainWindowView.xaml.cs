using System.Windows;

namespace Warsztat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void openLoginWindow(object sender, RoutedEventArgs e)
        {
            LoginView loginView = new LoginView();
            //this.Visibility = Visibility.Hidden;
            this.Close();
            loginView.Show();
        }

        private void exitApp(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
