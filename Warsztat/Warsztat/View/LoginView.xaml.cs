using System;
using System.Collections.Generic;
using System.Windows;
using Warsztat.Services;
using Warsztat.View;

namespace Warsztat
{
    public partial class LoginView : Window
    {
        public Service Service { get; set; }
        public LoginView()
        {
            InitializeComponent();

            Service = new();
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
                            Admin adminView = new Admin(Service);
                            this.Close();
                            adminView.Show();
                            return;
                        }
                        else if (personel.Role == "Manager")
                        {
                            Manager managerView = new Manager(personel.Id, Service);
                            this.Close();
                            managerView.Show();
                            return;
                        }
                        else
                        {
                            Worker workerView = new Worker(personel.Id, Service);
                            this.Close();
                            workerView.Show();
                            return;
                        }
                    }
                }
                MessageBox.Show("There isn't such user! Try again");
            }

        }
    }
}
