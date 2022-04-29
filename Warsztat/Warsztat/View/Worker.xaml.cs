using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Warsztat
{
    public partial class Worker : Page
    {
        public int WorkerId { get; set; }
        public Service Service { get; set; }
        public Worker()
        {
            if (Service == null)
                throw new NullReferenceException();

            List<Dictionary<string, dynamic>> activities = Service.WorkerActivitities(WorkerId);
            for (int i = 0; i < activities.Count; i++)
            {
                Activities.Items.Add(new Activity()
                { 
                    Id = activities[i]["id"],
                    Name = Service.ActivityNameFromDictionary(activities[i]["activityType"]),
                    SequenceNumber = activities[i]["sequenceNumber"],
                    Description = activities[i]["description"],
                    Result = activities[i]["result"],
                    Status = activities[i]["status"],
                    Start = activities[i]["dateTimeOfActivityStart"],
                    End = activities[i]["dateTimeOfActivityEnd"]
                });
            }
        }

        private void PursueButton_Click(object sender, RoutedEventArgs e)
        {
            int numberOfSelectedItems = Activities.SelectedItems.Count;
            for (int i = 0; i < numberOfSelectedItems; i++)
            {
                //index is 0 becouse RemoveAt removes Selected Item, so next item will have index 0
                Activity changedActivity = Activities.SelectedItems[0] as Activity ?? throw new InvalidCastException();
                int changedItemIndex = Activities.Items.IndexOf(changedActivity);
                Activities.Items.RemoveAt(changedItemIndex);
                Dictionary<string, dynamic> activity = Service.PursueWorkerActivity(changedActivity.Id, WorkerId);

                Activities.Items.Add(new Activity()
                {
                    Id = activity["id"],
                    Name = Service.ActivityNameFromDictionary(activity["activityType"]),
                    SequenceNumber = activity["sequenceNumber"],
                    Description = activity["description"],
                    Result = activity["result"],
                    Status = activity["status"],
                    Start = activity["dateTimeOfActivityStart"],
                    End = activity["dateTimeOfActivityEnd"]
                });
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            int numberOfSelectedItems = Activities.SelectedItems.Count;
            for (int i = 0; i < numberOfSelectedItems; i++)
            {
                //index is 0 becouse RemoveAt removes Selected Item, so next item will have index 0
                Activity changedActivity = Activities.SelectedItems[0] as Activity ?? throw new InvalidCastException();
                int changedItemIndex = Activities.Items.IndexOf(changedActivity);
                Activities.Items.RemoveAt(changedItemIndex);
                Dictionary<string, dynamic> activity = Service.CancelWorkerActivity(changedActivity.Id, WorkerId);

                Activities.Items.Add(new Activity()
                {
                    Id = activity["id"],
                    Name = Service.ActivityNameFromDictionary(activity["activityType"]),
                    SequenceNumber = activity["sequenceNumber"],
                    Description = activity["description"],
                    Result = activity["result"],
                    Status = activity["status"],
                    Start = activity["dateTimeOfActivityStart"],
                    End = activity["dateTimeOfActivityEnd"]
                });
            }
        }

        class Activity
        {
            public int Id { get; set; }
            public string? Name { get; set; }
            public int SequenceNumber { get; set; }
            public string? Description { get; set; }
            public string? Result { get; set; }
            public int Status { get; set; }
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
            public override string ToString()
            {
                return $"{Name}\n" +
                        $"{Description}\n" +
                        $"Sequence Number: {SequenceNumber}\n" +
                        $"Status: {Status}\n" +
                        $"Result: {Result}\n" +
                        $"{Start} - {End}";
            }
        }
    }
}
