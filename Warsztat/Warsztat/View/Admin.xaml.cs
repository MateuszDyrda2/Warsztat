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
        public Admin()
        {
            InitializeComponent();

            //for testing
            Service = new();

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
            currentPopup = new MyPopupBuilder()
                       .TextBox("Name")
                       .TextBox("Surname")
                       .TextBox("Phone Number")
                       .TextBox("User Name")
                       .TextBox("Password")
                       .ComboBox(new List<string> { "Manager", "Worker" })
                       .DataTransfer(transferDelegate)
                       .Build();
            currentPopup.Show();
        }
        public void ReceiveInputFromPopup(List<string> data)
        {
            string name = data[0];
            string surname = data[1];
            string phoneNumber = data[2];
            string password = data[3];
            string userName = data[4];
            string role = data[5];
            Service.Personel? personel = Service.AddNewPersonel(name, surname, phoneNumber, role, userName, password);
            if (personel != null)
            {
                Personels.Items.Add(personel);
            }
            else
                MessageBox.Show("Some fields are empty or incorrect.");
        }
        private void ModifyChosenUserButton_Click(object sender, RoutedEventArgs e)
        {
        }
        private void DeleteChosenUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (Personels.SelectedItems.Count > 0)
            {
                Service.Personel personel = Personels.SelectedItems[0] as Service.Personel ?? throw new InvalidCastException();
                int deletedItemIndex = Personels.Items.IndexOf(personel);
                Service.DeletePersonel(personel.Id);
                if (personel != null)
                {
                    Personels.Items.RemoveAt(deletedItemIndex);
                    return;
                }
                return;
            }
            MessageBox.Show("No user has been chosen.");
        }
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
