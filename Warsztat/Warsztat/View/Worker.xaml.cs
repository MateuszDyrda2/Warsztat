using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class Worker : Window
    {
        public int WorkerId { get; set; }
        public Service Service { get; set; }

        public DataTransfer transferDelegate;
        private MyPopup? currentPopup;

        private int? _changedItemId;
        private string? _activityStatus = null;

        List<Service.Activity> activities;
        public Worker(int WorkerId, Service Service)
        {
            InitializeComponent();

            this.WorkerId = WorkerId;
            this.Service = Service;

            if (Service == null)
                throw new NullReferenceException();

            activities = Service.WorkerActivitities(WorkerId);
            Activities.ItemsSource = activities;

            requestFilter.Items.Add(string.Empty);
            foreach (Service.Activity activity in activities)
            {
                requestFilter.Items.Add(activity.ParentRequestName);
            }

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Activities.ItemsSource);
            view.Filter = Filter;

            transferDelegate += new DataTransfer(ReceiveInputFromPopup);
        }

        public void ReceiveInputFromPopup(List<string> data)
        {
            if (_activityStatus != null)
            {
                Service.Activity activity = Service.FinishOrCloseActivityStatus(_activityStatus, data[0], _changedItemId);
                activities.Add(activity);
            }
        }

        private void PursueButton_Click(object sender, RoutedEventArgs e)
        {
            if (Activities.SelectedItems.Count > 0)
            {
                currentPopup?.Close();
                Service.Activity chosenActivity = Activities.SelectedItems[0] as Service.Activity ?? throw new InvalidCastException();
                if (chosenActivity.Status != "Finished" && chosenActivity.Status != "Canceled")
                {
                    _changedItemId = chosenActivity.Id;
                    _activityStatus = "FIN";
                    currentPopup = new MyPopupBuilder()
                        .TextBox("Result")
                        .DataTransfer(transferDelegate)
                        .Build();
                    currentPopup.Show();
                }
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (Activities.SelectedItems.Count > 0)
            {
                currentPopup?.Close();
                Service.Activity chosenActivity = Activities.SelectedItems[0] as Service.Activity ?? throw new InvalidCastException();
                if (chosenActivity.Status != "Finished" && chosenActivity.Status != "Cancelled")
                {
                    _changedItemId = chosenActivity.Id;
                    _activityStatus = "CAN";
                    currentPopup = new MyPopupBuilder()
                        .TextBox("Result")
                        .DataTransfer(transferDelegate)
                        .Build();
                    currentPopup.Show();
                }
            }
        }
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            LoginView loginView = new LoginView();
            this.Close();
            loginView.Show();
            return;
        }

        private void FilterChanged(object sender, RoutedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(Activities.ItemsSource)?.Refresh();
        }

        private bool Filter(object item)
        {
            return (OpenFilter(item) || InProgressFilter(item) || FinishedFilter(item) || CancelledFilter(item)) && DescriptionFilter(item) && RequestFilter(item);
        }

        private bool RequestFilter(object item)
        {
            if (requestFilter.SelectedItem == null || requestFilter.SelectedItem == string.Empty)
                return true;
            else
                return (item as Service.Activity)!.ParentRequestName == requestFilter.SelectedItem as string;
        }

        private bool DescriptionFilter(object item)
        {
            if (string.IsNullOrEmpty(descriptionFilter.Text)) 
                return true;
            else
                return (item as Service.Activity)!.Description!.Contains(descriptionFilter.Text);
        }

        private bool OpenFilter(object item)
        {
            if (OpenCheckbox.IsChecked == true)
                return (item as Service.Activity)!.Status == "Open";
            else
                return false;
        }

        private bool InProgressFilter(object item)
        {
            if (InProgressCheckbox.IsChecked == true)
                return (item as Service.Activity)!.Status == "InProgress";
            else
                return false;
        }

        private bool FinishedFilter(object item)
        {
            if (FinishedCheckbox.IsChecked == true)
                return (item as Service.Activity)!.Status == "Finished";
            else
                return false;
        }

        private bool CancelledFilter(object item)
        {
            if (CancelledCheckbox.IsChecked == true)
                return (item as Service.Activity)!.Status == "Canceled";
            else
                return false;
        }

        private void requestFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(Activities.ItemsSource)?.Refresh();
        }

        private void descriptionFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(Activities.ItemsSource)?.Refresh();
        }
    }
}
