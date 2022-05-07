using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Warsztat.Services;

namespace Warsztat
{
    public partial class Worker : Page
    {
        public int WorkerId { get; set; }
        public Service Service { get; set; }
        public Worker()
        {
            InitializeComponent();

            //for testing
            Service = new Service();
            WorkerId = 3;

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
        }
    }
}
