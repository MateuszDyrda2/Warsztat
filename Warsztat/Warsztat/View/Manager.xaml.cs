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

        private int? _changedItemId;
        private string? _requestStatus = null;
        public Manager(int ManagerId, Service Service)
        {
            InitializeComponent();

            this.ManagerId = ManagerId;
            this.Service = Service;

            if (Service == null)
                throw new NullReferenceException();

            List<Service.Client> clients = Service.Clients();
            foreach (Service.Client client in clients)
                Clients.Items.Add(client);

            Cars.Visibility = Visibility.Hidden;
            Requests.Visibility = Visibility.Hidden;
            Activities.Visibility = Visibility.Hidden;

            UpdateRequestButton.Visibility = Visibility.Hidden;
            FinishRequestButton.Visibility = Visibility.Hidden;
            CloseRequestButton.Visibility = Visibility.Hidden;

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

                    UpdateRequestButton.Visibility = Visibility.Visible;
                    FinishRequestButton.Visibility = Visibility.Visible;
                    CloseRequestButton.Visibility = Visibility.Visible;

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

                    UpdateRequestButton.Visibility = Visibility.Hidden;
                    FinishRequestButton.Visibility = Visibility.Hidden;
                    CloseRequestButton.Visibility = Visibility.Hidden;

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

                    UpdateRequestButton.Visibility = Visibility.Hidden;
                    FinishRequestButton.Visibility = Visibility.Hidden;
                    CloseRequestButton.Visibility = Visibility.Hidden;

                    CurrentPage.Text = CurrentPage.Text.Split('-').First();
                    CurrentPage.Text += $"- Cars";
                    Page--;
                    break;
                case 4:
                    Activities.Visibility = Visibility.Hidden;
                    Requests.Visibility = Visibility.Visible;

                    UpdateRequestButton.Visibility = Visibility.Visible;
                    FinishRequestButton.Visibility = Visibility.Visible;
                    CloseRequestButton.Visibility = Visibility.Visible;

                    CurrentPage.Text = CurrentPage.Text.Split('-')[0] + '-' + CurrentPage.Text.Split('-')[1];
                    CurrentPage.Text += $"- Requests";
                    Page--;
                    break;
                default:
                    Page = 1;
                    break;
            }
        }
        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            currentPopup?.Close();
            _changedItemId = null;
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
                       .ComboBox(Service.AllCars(), "Car")
                       .DataTransfer(transferDelegate)
                       .Build();
                    currentPopup.Show();
                    break;
                case 3:
                    _requestStatus = null;
                    currentPopup = new MyPopupBuilder()
                       .TextBox("Description")
                       .DataTransfer(transferDelegate)
                       .Build();
                    currentPopup.Show();
                    break;
                case 4:
                    List<string> sequenceNumbers = new List<string>();
                    int? highestSequenceNumber = Service.HighestSequenceNumber(currentRequest!.Id);
                    if (highestSequenceNumber != null)
                    {
                        for (int i = 1; i <= highestSequenceNumber.Value; i++)
                            sequenceNumbers.Add((i).ToString());
                    }
                    else
                    {
                        sequenceNumbers.Add("1");
                    }
                    currentPopup = new MyPopupBuilder()
                       .TextBox("Description")
                       .ComboBox(Service.AllActivityTypes(), "Activity Type")
                       .ComboBox(sequenceNumbers, "Sequence Number")
                       .ComboBox(Service.Workers())
                       .DataTransfer(transferDelegate)
                       .Build();
                    currentPopup.Show();
                    break;
                default:
                    break;
            }
        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            switch (Page)
            {
                case 1:
                    if (Clients.SelectedItems.Count > 0)
                    {
                        currentPopup?.Close();
                        Service.Client chosenClient = Clients.SelectedItems[0] as Service.Client ?? throw new InvalidCastException();
                        _changedItemId = chosenClient.Id;

                        currentPopup = new MyPopupBuilder()
                            .TextBox("Name", chosenClient.Name!)
                            .TextBox("Surname", chosenClient.Surname!)
                            .TextBox("Phone Number", chosenClient.PhoneNumber!)
                            .DataTransfer(transferDelegate)
                            .Build();
                        currentPopup.Show();
                    }
                    break;
                case 2:
                    if (Cars.SelectedItems.Count > 0)
                    {
                        currentPopup?.Close();
                        Service.Car chosenCar = Cars.SelectedItems[0] as Service.Car ?? throw new InvalidCastException();
                        _changedItemId = chosenCar.Id;

                        currentPopup = new MyPopupBuilder()
                            .TextBox("Registration Number", chosenCar.RegistrationNumber!)
                            .ComboBox(Service.AllCars(), "Car", $"{chosenCar.Mark} {chosenCar.Model}")
                            .DataTransfer(transferDelegate)
                            .Build();
                        currentPopup.Show();
                    }
                    break;
                case 3:
                    _requestStatus = null;
                    break;
                case 4:
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
                    Service.Client? client = Service.AddNewClient(name, surname, phoneNumber, _changedItemId);
                    if (client != null)
                    {
                        foreach (Service.Client changedClient in Clients.Items)
                            if (changedClient.Id == _changedItemId)
                            {
                                Clients.Items.Remove(changedClient);
                                break;
                            }

                        Clients.Items.Add(client);
                    }
                    else
                        MessageBox.Show("Some fields are empty or incorrect.");
                    break;
                case 2:
                    string registrationNumber = data[0];
                    string model = data[1].Split(' ').Last();
                    Service.Car? car = Service.AddNewCar(registrationNumber, model, currentClient!.Id, _changedItemId);
                    if (car != null)
                    {
                        foreach (Service.Car changedCar in Cars.Items)
                            if (changedCar.Id == _changedItemId)
                            {
                                Cars.Items.Remove(changedCar);
                                break;
                            }

                        Cars.Items.Add(car);
                    }
                    else
                        MessageBox.Show("Some fields are empty or incorrect.");
                    break;
                case 3:
                    string description = data[0];
                    Service.Request? request;
                    if (_requestStatus == null)
                    {
                        request = Service.AddNewRequest(description, ManagerId, currentCar!.Id, _changedItemId);
                    }
                    else
                    {
                        request = Service.FinishOrCloseRequestStatus(_requestStatus, data[0], _changedItemId);
                    }
                         
                    if (request != null)
                    {
                        foreach (Service.Request changedRequest in Requests.Items)
                        {
                            if (changedRequest.Id == _changedItemId)
                            {
                                Requests.Items.Remove(changedRequest);
                                break;
                            }
                        }

                        Requests.Items.Add(request);
                    }
                    else
                        MessageBox.Show("Some fields are empty or incorrect.");
                    break;
                case 4:
                    try
                    {
                        string activityDescription = data[0];
                        string type = string.Empty;
                        if (data[1] != string.Empty)
                            type = Service.AcitivtyTypeByName(data[1]);
                        string sequenceNumber = data[2];
                        int workerId = data[3] != String.Empty ? Int32.Parse(data[3]) : -1;
                        Service.Activity? activity = Service.AddNewActivity(sequenceNumber, activityDescription, type, workerId, currentRequest!.Id, _changedItemId);
                        if (activity != null)
                        {
                            foreach (Service.Activity changedActivity in Activities.Items)
                                if (changedActivity.Id == _changedItemId)
                                {
                                    Activities.Items.Remove(changedActivity);
                                    break;
                                }

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
            LoginView loginView = new LoginView();
            this.Close();
            loginView.Show();
            return;
        }
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (Requests.SelectedItems.Count > 0)
            {
                currentPopup?.Close();
                Service.Request chosenRequest = Requests.SelectedItems[0] as Service.Request ?? throw new InvalidCastException();
                if (chosenRequest.Status != "Finished" && chosenRequest.Status != "Canceled")
                {
                    Service.Request request = Service.UpdateRequestStatus("PRO", chosenRequest.Id);

                    foreach (Service.Request changedRequest in Requests.Items)
                        if (changedRequest.Id == chosenRequest.Id)
                        {
                            Requests.Items.Remove(changedRequest);
                            break;
                        }

                    Requests.Items.Add(request);
                }
            }
        }
        private void FinishButton_Click(object sender, RoutedEventArgs e)
        {
            if (Requests.SelectedItems.Count > 0)
            {
                currentPopup?.Close();
                Service.Request chosenRequest = Requests.SelectedItems[0] as Service.Request ?? throw new InvalidCastException();
                if (chosenRequest.Status != "Finished" && chosenRequest.Status != "Canceled")
                {
                    _changedItemId = chosenRequest.Id;
                    _requestStatus = "FIN";
                    currentPopup = new MyPopupBuilder()
                               .TextBox("Result")
                               .DataTransfer(transferDelegate)
                               .Build();
                    currentPopup.Show();
                }
            }
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            if (Requests.SelectedItems.Count > 0)
            {
                currentPopup?.Close();
                Service.Request chosenRequest = Requests.SelectedItems[0] as Service.Request ?? throw new InvalidCastException();
                if (chosenRequest.Status != "Finished" && chosenRequest.Status != "Canceled")
                {
                    _changedItemId = chosenRequest.Id;
                    _requestStatus = "CAN";
                    currentPopup = new MyPopupBuilder()
                               .TextBox("Result")
                               .DataTransfer(transferDelegate)
                               .Build();
                    currentPopup.Show();
                }
            }
        }
    }
}
