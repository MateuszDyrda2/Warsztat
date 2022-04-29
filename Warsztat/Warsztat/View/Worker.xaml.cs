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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Warsztat.Models;

namespace Warsztat
{
    public partial class Worker : Page
    {
        private int _workerId;
        private WorkerService _service;
        public Worker(int workerId, WorkerService service)
        {
            InitializeComponent();
            _workerId = workerId;
            _service = service;
            List<Dictionary<string, dynamic>> activities = _service.WorkerActivitities(_workerId);
            for (int i = 0; i < activities.Count; i++)
            {
                string name = _service.ActivityNameFromDictionary(activities[i]["activityType"]);
                int sequenceNumber = activities[i]["sequenceNumber"];
                string description = activities[i]["description"];
                string result = activities[i]["result"];
                int status = activities[i]["status"];
                DateTime start = activities[i]["dateTimeOfActivityStart"];
                DateTime end = activities[i]["dateTimeOfActivityEnd"];
                Activities.Items.Add($"{name}\n" +
                    $"{description}\n" +
                    $"Sequence Number: {sequenceNumber}\n" +
                    $"Status: {status}\n" +
                    $"Result: {result}\n" +
                    $"{start} - {end}");
            }     
        }

        private void PursueButton_Click(object sender, RoutedEventArgs e)
        {
            int numberOfSelectedItems = Activities.SelectedItems.Count;
            for (int i = 0; i < numberOfSelectedItems; i++)
            {
                //index is 0 becouse RemoveAt removes Selected Item, so next item will have index 0
                int changedItemIndex = Activities.Items.IndexOf(Activities.SelectedItems[0]);
                Activities.Items.RemoveAt(changedItemIndex);
                Dictionary<string, dynamic> activity = _service.PursueWorkerActivity(changedItemIndex, _workerId);

                string name = _service.ActivityNameFromDictionary(activity["activityType"]);
                int sequenceNumber = activity["sequenceNumber"];
                string description = activity["description"];
                string result = activity["result"];
                int status = activity["status"];
                DateTime start = activity["dateTimeOfActivityStart"];
                DateTime end = activity["dateTimeOfActivityEnd"];
                Activities.Items.Add($"{name}\n" +
                    $"{description}\n" +
                    $"Sequence Number: {sequenceNumber}\n" +
                    $"Status: {status}\n" +
                    $"Result: {result}\n" +
                    $"{start} - {end}");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            int numberOfSelectedItems = Activities.SelectedItems.Count;
            for (int i = 0; i < numberOfSelectedItems; i++)
            {
                //index is 0 becouse RemoveAt removes Selected Item, so next item will have index 0
                int changedItemIndex = Activities.Items.IndexOf(Activities.SelectedItems[0]);
                Activities.Items.RemoveAt(changedItemIndex);
                Dictionary<string, dynamic> activity = _service.CancelWorkerActivity(changedItemIndex, _workerId);

                string name = _service.ActivityNameFromDictionary(activity["activityType"]);
                int sequenceNumber = activity["sequenceNumber"];
                string description = activity["description"];
                string result = activity["result"];
                int status = activity["status"];
                DateTime start = activity["dateTimeOfActivityStart"];
                DateTime end = activity["dateTimeOfActivityEnd"];
                Activities.Items.Add($"{name}\n" +
                    $"{description}\n" +
                    $"Sequence Number: {sequenceNumber}\n" +
                    $"Status: {status}\n" +
                    $"Result: {result}\n" +
                    $"{start} - {end}");
            }
        }
    }
}
