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
    public partial class Manager : Window
    {
        public Service Service { get; set; }
        public int ManagerId { get; set; }
        private int Page = 1;

        private Service.Client? currentClient;
        private Service.Car? currentCar;
        private Service.Request? currentRequest;

        public DataTransfer transferDelegate;
        private MyPopup? currentPopup;
        public Manager()
        {
            InitializeComponent();
            
            Service = new();
            ManagerId = 2;

            if (Service == null)
                throw new NullReferenceException();

            List<Service.Client> clients = Service.Clients();
            foreach (Service.Client client in clients)
                Clients.Items.Add(client);

            Cars.Visibility = Visibility.Hidden;
            Requests.Visibility = Visibility.Hidden;
            Activities.Visibility = Visibility.Hidden;

            CurrentPage.Text = "Clients";

            transferDelegate += new DataTransfer(ReceiveInputFromPopup);
        }
        private void ListViewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            switch (Page)
            {
                case 1:
                    if (Clients.SelectedItems.Count < 1)
                        break;
                    Service.Client chosenClient = Clients.SelectedItems[0] as Service.Client ?? throw new InvalidCastException();
                    List<Service.Car> cars = Service.Cars(chosenClient.Id);
                    Cars.Items.Clear();
                    foreach (Service.Car car in cars)
                        Cars.Items.Add(car);
                    CurrentPage.Text = $"{chosenClient.Name} {chosenClient.Surname} - Cars";

                    Clients.Visibility = Visibility.Hidden;
                    Cars.Visibility = Visibility.Visible;

                    currentClient = chosenClient;
                    Page++;
                    break;
                case 2:
                    if (Cars.SelectedItems.Count < 1)
                        break;
                    Service.Car chosenCar = Cars.SelectedItems[0] as Service.Car ?? throw new InvalidCastException();
                    List<Service.Request> requests = Service.Requests(chosenCar.Id, ManagerId);
                    Requests.Items.Clear();
                    foreach (Service.Request request in requests)
                        Requests.Items.Add(request);
                    CurrentPage.Text = CurrentPage.Text.Split('-').First();
                    CurrentPage.Text += $"- {chosenCar.Model} {chosenCar.Mark} - Requests";

                    Cars.Visibility = Visibility.Hidden;
                    Requests.Visibility = Visibility.Visible;

                    currentCar = chosenCar;
                    Page++;
                    break;
                case 3:
                    if (Requests.SelectedItems.Count < 1)
                        break;
                    Service.Request chosenRequest = Requests.SelectedItems[0] as Service.Request ?? throw new InvalidCastException();
                    List<Service.Activity> activities = Service.Activities(chosenRequest.Id);
                    Activities.Items.Clear();
                    foreach (Service.Activity activity in activities)
                        Activities.Items.Add(activity);
                    CurrentPage.Text = CurrentPage.Text.Split('-')[0] + '-' + CurrentPage.Text.Split('-')[1];
                    CurrentPage.Text += $"- Request {chosenRequest.Id} - Activities";

                    Requests.Visibility = Visibility.Hidden;
                    Activities.Visibility = Visibility.Visible;

                    currentRequest = chosenRequest;
                    Page++;
                    break;
                default:
                    Page = 4;
                    break;
            }
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            switch (Page)
            {
                case 2:
                    Cars.Visibility = Visibility.Hidden;
                    Clients.Visibility = Visibility.Visible;

                    CurrentPage.Text = "Clients";
                    Page--;
                    break;
                case 3:
                    Requests.Visibility = Visibility.Hidden;
                    Cars.Visibility = Visibility.Visible;

                    CurrentPage.Text = CurrentPage.Text.Split('-').First();
                    CurrentPage.Text += $"- Cars";
                    Page--;
                    break;
                case 4:
                    Activities.Visibility = Visibility.Hidden;
                    Requests.Visibility = Visibility.Visible;

                    CurrentPage.Text = CurrentPage.Text.Split('-')[0] + '-' + CurrentPage.Text.Split('-')[1];
                    CurrentPage.Text += $"- Requests";
                    Page--;
                    break;
                default:
                    Page = 1;
                    break;
            }
        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            switch (Page)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                default:
                    break;
            }
        }
        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            currentPopup?.Close();
            switch (Page)
            {
                case 1:
                    currentPopup = new MyPopupBuilder()
                        .TextBox("Name")
                        .TextBox("Surname")
                        .TextBox("Phone Number")
                        .DataTransfer(transferDelegate)
                        .Build();
                    currentPopup.Show();
                    break;
                case 2:
                    currentPopup = new MyPopupBuilder()
                       .TextBox("Registration Number")
                       .TextBox("Car Mark")
                       .DataTransfer(transferDelegate)
                       .Build();
                    currentPopup.Show();
                    break;
                case 3:
                    currentPopup = new MyPopupBuilder()
                       .TextBox("Description")
                       
                       .DataTransfer(transferDelegate)
                       .Build();
                    currentPopup.Show();
                    break;
                case 4:
                    currentPopup = new MyPopupBuilder()
                       .TextBox("Description")
                       .TextBox("Sequence Number")
                       .TextBox("Type")
                       .ComboBox(Service.Workers())
                       .DataTransfer(transferDelegate)
                       .Build();
                    currentPopup.Show();
                    break;
                default:
                    break;
            }
        }
        public void ReceiveInputFromPopup(List<string> data)
        {
            switch (Page)
            {
                case 1:
                    string name = data[0];
                    string surname = data[1];
                    string phoneNumber = data[2];
                    Service.Client? client = Service.AddNewClient(name, surname, phoneNumber);
                    if (client != null)
                    {
                        Clients.Items.Add(client);
                    }
                    else
                        MessageBox.Show("Some fields are empty or incorrect.");
                    break;
                case 2:
                    string registrationNumber = data[0];
                    string mark = data[1];
                    Service.Car? car = Service.AddNewCar(registrationNumber, mark, currentClient!.Id);
                    if (car != null)
                    {
                        Cars.Items.Add(car);
                    }
                    else
                        MessageBox.Show("Some fields are empty or incorrect.");
                    break;
                case 3:
                    string description = data[0];
                    Service.Request? request = Service.AddNewRequest(description, "OPN", DateTime.Now, ManagerId, currentCar!.Id);
                    if (request != null)
                    {
                        Requests.Items.Add(request);
                    }
                    else
                        MessageBox.Show("Some fields are empty or incorrect.");
                    break;
                case 4:
                    try
                    {
                        string activityDescription = data[0];
                        string sequenceNumber = data[1];
                        string type = data[2];
                        int workerId = data[3] != String.Empty ? Int32.Parse(data[3]) : -1;
                        Service.Activity? activity = Service.AddNewActivity(sequenceNumber, activityDescription, 1, DateTime.Now, type, workerId, currentRequest!.Id);
                        if (activity != null)
                        {
                            Activities.Items.Add(activity);
                        }
                        else
                            MessageBox.Show("Some fields are empty or incorrect.");
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("Some fields are empty or incorrect.");
                    }
                    break;
                default:
                    break;
            }
        }
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
