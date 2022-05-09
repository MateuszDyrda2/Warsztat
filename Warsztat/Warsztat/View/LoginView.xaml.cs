using System;
using System.Collections.Generic;
using System.Windows;
using Warsztat.Services;
using Warsztat.View;

namespace Warsztat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public Service Service { get; set; }
        public DataTransfer transferDelegate;
        private MyPopup? currentPopup;
        public LoginView()
        {
            InitializeComponent();

            //for testing
            Service = new();

            if (Service == null)
                throw new NullReferenceException();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(usernameTextbox.Text.Length == 0 || passwordTextbox.Password.Length == 0)
            {
                MessageBox.Show("Field with username or password is empty!");
            }
            else
            {

                List<Service.Personel> personels = Service.checkAllPersonel();

                foreach (Service.Personel personel in personels)
                {
                    if(personel.Username == usernameTextbox.Text && personel.Password == passwordTextbox.Password)
                    {
                        MessageBox.Show("Logged as " + personel.Username + " !");
                        if(personel.Role=="Admin")
                        {
                            Admin adminView = new Admin();
                            this.Close();
                            adminView.Show();
                        }
                        else if (personel.Role == "Manager")
                        {
                            Manager managerView = new Manager();
                            this.Close();
                            managerView.Show();
                        }
                        else
                        {
                            Worker workerView = new Worker();
                            this.Close();
                            //workerView.Show();        //O co mu tu chodzi??? XD
                        }
                    }
                    else
                    {
                        MessageBox.Show("There isn't such user! Try again");
                    }
                }
            }

        }

        //private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        //{

        //}
    }
}
