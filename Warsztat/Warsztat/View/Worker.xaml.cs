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
        public Worker(int WorkerId, Service Service)
        {
            InitializeComponent();

            this.WorkerId = WorkerId;
            this.Service = Service;

            if (Service == null)
                throw new NullReferenceException();

            List<Service.Activity> activities = Service.WorkerActivitities(WorkerId);
            foreach (Service.Activity activity in activities)
            {
                Activities.Items.Add(activity);
            }

            /*CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Activities.ItemsSource);
            view.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            view.SortDescriptions.Add(new SortDescription("SequenceNumber", ListSortDirection.Ascending));
            view.SortDescriptions.Add(new SortDescription("Description", ListSortDirection.Ascending));
            view.SortDescriptions.Add(new SortDescription("Result", ListSortDirection.Ascending));
            view.SortDescriptions.Add(new SortDescription("Status", ListSortDirection.Ascending));
            view.SortDescriptions.Add(new SortDescription("Start", ListSortDirection.Ascending));
            view.SortDescriptions.Add(new SortDescription("End", ListSortDirection.Ascending));*/

            transferDelegate += new DataTransfer(ReceiveInputFromPopup);
        }

        public void ReceiveInputFromPopup(List<string> data)
        {
            if (_activityStatus != null)
            {
                Service.Activity activity = Service.FinishOrCloseActivityStatus(_activityStatus, data[0], _changedItemId);
                Activities.Items.Add(activity);
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
                if (chosenActivity.Status != "Finished" && chosenActivity.Status != "Canceled")
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
    }
}
