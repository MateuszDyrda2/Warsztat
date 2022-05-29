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
using Warsztat.Services;

namespace Warsztat.View
{
    public partial class Admin : Window
    {
        public Service Service { get; set; }
        public DataTransfer transferDelegate;
        private MyPopup? currentPopup;
        private int? _changedItemId;
        public Admin(Service Service)
        {
            InitializeComponent();

            this.Service = Service;

            if (Service == null)
                throw new NullReferenceException();


            List<Service.Personel> personels = Service.WorkersAndManagers();

            foreach (Service.Personel personel in personels)
            {
                Personels.Items.Add(personel);
            }

            transferDelegate += new DataTransfer(ReceiveInputFromPopup);
        }

        private void AddNewUserButton_Click(object sender, RoutedEventArgs e)
        {
            currentPopup?.Close();
            _changedItemId = null;
            currentPopup = new MyPopupBuilder()
                       .TextBox("Name", String.Empty)
                       .TextBox("Surname")
                       .TextBox("Phone Number")
                       .TextBox("User Name")
                       .TextBox("Password")
                       .ComboBox(new List<string> { "Manager", "Worker" }, "Role")
                       .DataTransfer(transferDelegate)
                       .Build();
            currentPopup.Show();
        }
        public void ReceiveInputFromPopup(List<string> data)
        {
            string name = data[0];
            string surname = data[1];
            string phoneNumber = data[2];
            string userName = data[3];
            string password = data[4];
            string role = data[5];
            Service.Personel? personel = Service.AddPersonel(name, surname, phoneNumber, role, userName, password, _changedItemId);
            if (personel != null)
            {
                foreach (Service.Personel changedPersonel in Personels.Items)
                    if (changedPersonel.Id == _changedItemId)
                    {
                        Personels.Items.Remove(changedPersonel);
                        break;
                    }
                       

                Personels.Items.Add(personel);
            }
            else
            {
                MessageBox.Show("Some fields are empty or incorrect.");
                currentPopup = new MyPopupBuilder()
                      .TextBox("Name", name)
                      .TextBox("Surname", surname)
                      .TextBox("Phone Number", phoneNumber)
                      .TextBox("User Name", userName)
                      .TextBox("Password", password)
                      .ComboBox(new List<string> { "Manager", "Worker" }, "Role", role)
                      .DataTransfer(transferDelegate)
                      .Build();
                currentPopup.Show();
            }
                
        }
        private void ModifyChosenUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (Personels.SelectedItems.Count > 0)
            {
                currentPopup?.Close();
                Service.Personel chosenPersonel = Personels.SelectedItems[0] as Service.Personel ?? throw new InvalidCastException();
                _changedItemId = chosenPersonel.Id;

                currentPopup = new MyPopupBuilder()
                         .TextBox("Name", chosenPersonel.Name!)
                         .TextBox("Surname", chosenPersonel.Surname!)
                         .TextBox("Phone Number", chosenPersonel.PhoneNumber!)
                         .TextBox("User Name", chosenPersonel.Username!)
                         .TextBox("Password", chosenPersonel.Password!)
                         .ComboBox(new List<string> { "Manager", "Worker" }, "Role", chosenPersonel.Role!)
                         .DataTransfer(transferDelegate)
                         .Build();
                currentPopup.Show();
            }
        }
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            LoginView loginView = new LoginView();
            this.Close();
            loginView.Show();
            return;
        }
    }
}
