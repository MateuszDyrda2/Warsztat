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
        List<Service.Personel> personels;
        public Admin(Service Service)
        {
            InitializeComponent();

            this.Service = Service;

            if (Service == null)
                throw new NullReferenceException();


            personels = Service.WorkersAndManagers();

            Personels.ItemsSource = personels;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Personels.ItemsSource);
            view.Filter = StatusFilter;

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
            bool isActive = data.Count > 6 ? data[6] == "Active" ? true : false : true;
            Service.Personel? personel = Service.AddPersonel(name, surname, phoneNumber, role, userName, password, _changedItemId, isActive);
            if (personel != null)
            {
                personels.RemoveAll(item => item.Id == _changedItemId);
                personels.Add(personel);
                CollectionViewSource.GetDefaultView(Personels.ItemsSource).Refresh();
            }
            else
            {
                MessageBox.Show("Some fields are empty or incorrect.");
                if (data.Count < 7)
                {
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
                else
                {
                    currentPopup = new MyPopupBuilder()
                    .TextBox("Name", name)
                    .TextBox("Surname", surname)
                    .TextBox("Phone Number", phoneNumber)
                    .TextBox("User Name", userName)
                    .TextBox("Password", password)
                    .ComboBox(new List<string> { "Manager", "Worker" }, "Role", role)
                    .ComboBox(new List<string> { "Active", "Disactive" }, "Status", IsActive ? "Active" : "Disactive")
                    .DataTransfer(transferDelegate)
                    .Build();
                    currentPopup.Show();
                }
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
                         .TextBox("Password")
                         .ComboBox(new List<string> { "Manager", "Worker" }, "Role", chosenPersonel.Role!)
                         .ComboBox(new List<string> { "Active", "Disactive" }, "Status", chosenPersonel.IsActive ? "Active" : "Disactive")
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

        private void Status_CheckedChanged(object sender, RoutedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(Personels.ItemsSource).Refresh();
        }

        private bool StatusFilter(object item)
        {
            return StatusCheckbox.IsChecked == true ? true : (item as Service.Personel)!.IsActive;
        }
    }
}
