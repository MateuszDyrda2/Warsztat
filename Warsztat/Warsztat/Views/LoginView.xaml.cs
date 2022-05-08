using System.Windows;

namespace Warsztat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(usernameTextbox.Text.Length == 0)
            {
                MessageBox.Show("Field with username is empty!");
            }
            if(passwordTextbox.Password.Length == 0)
            {
                MessageBox.Show("Field with password is empty!");
            }

        }

        //private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        //{

        //}
    }
}
