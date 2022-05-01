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
        public Admin()
        {
            InitializeComponent();
            Service = new();

            if (Service == null)
                throw new NullReferenceException();

            List<Service.Personel> personels = Service.WorkersAndManagers();

            foreach (Service.Personel personel in personels)
            {
                Personels.Items.Add(personel);
            }
        }

        private void AddNewUserButton_Click(object sender, RoutedEventArgs e)
        {
            string name = Name.Text;
            string surname = Surname.Text;
            string phoneNumber = PhoneNumber.Text;
            string role = Role.Text;
            string username = Username.Text;
            string password = Password.Text;
            Service.Personel? personel = Service.AddNewPersonel(name, surname, phoneNumber, role, username, password);
            if (personel != null)
                Personels.Items.Add(personel); 
            else
                MessageBox.Show("Some fields are empty or incorrect.");
        }

        private void ModifyChosenUserButton_Click(object sender, RoutedEventArgs e)
        {
            string name = Name.Text;
            string surname = Surname.Text;
            string phoneNumber = PhoneNumber.Text;
            string role = Role.Text;
            string username = Username.Text;
            string password = Password.Text;

            if (Personels.SelectedItems.Count > 0)
            {
                Service.Personel changedPersonel = Personels.SelectedItems[0] as Service.Personel ?? throw new InvalidCastException();
                int changedItemIndex = Personels.Items.IndexOf(changedPersonel);
                Service.Personel? personel = Service.ModifyPersonel(name, surname, phoneNumber, role, username, password, changedPersonel.Id);
                if (personel != null)
                {
                    Personels.Items.RemoveAt(changedItemIndex);
                    Personels.Items.Insert(changedItemIndex, personel);
                    return;
                }
                MessageBox.Show("Some fields are empty or incorrect.");
                return;
            }
            MessageBox.Show("No user has been chosen.");
        }

        private void SelectedPersonelChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Personels.SelectedItems.Count > 0)
            {
                Service.Personel personel = Personels.SelectedItems[0] as Service.Personel ?? throw new InvalidCastException();
                Name.Text = personel.Name;
                Surname.Text = personel.Surname;
                PhoneNumber.Text = personel.PhoneNumber;
                Role.Text = personel.Role;
                Username.Text = personel.Username;
                Password.Text = personel.Password;
            }
        }
    }
}
