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
    public partial class Worker : Window
    {
        public int WorkerId { get; set; }
        public Service Service { get; set; }
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
        }

        private void PursueButton_Click(object sender, RoutedEventArgs e)
        {
            int numberOfSelectedItems = Activities.SelectedItems.Count;
            for (int i = 0; i < numberOfSelectedItems; i++)
            {
                //index is 0 becouse RemoveAt removes Selected Item, so next item will have index 0
                Service.Activity activity = Activities.SelectedItems[0] as Service.Activity ?? throw new InvalidCastException();
                int changedItemIndex = Activities.Items.IndexOf(activity);
                Activities.Items.RemoveAt(changedItemIndex);
                Service.Activity changedActivity = Service.PursueWorkerActivity(activity.Id, WorkerId);

                Activities.Items.Insert(changedItemIndex, changedActivity);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            int numberOfSelectedItems = Activities.SelectedItems.Count;
            for (int i = 0; i < numberOfSelectedItems; i++)
            {
                //index is 0 becouse RemoveAt removes Selected Item, so next item will have index 0
                Service.Activity activity = Activities.SelectedItems[0] as Service.Activity ?? throw new InvalidCastException();
                int changedItemIndex = Activities.Items.IndexOf(activity);
                Activities.Items.RemoveAt(changedItemIndex);
                Service.Activity changedActivity = Service.CancelWorkerActivity(activity.Id, WorkerId);

                Activities.Items.Insert(changedItemIndex, changedActivity);
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
