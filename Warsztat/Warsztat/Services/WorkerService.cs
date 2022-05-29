using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warsztat.Models;

namespace Warsztat.Services
{
    public partial class Service
    {
        public List<Activity> WorkerActivitities(int workerID)
        {
            List<Activity> activities = new();

            var activitiesDB = context.Activities
                .Where(a => a.personelId == workerID)
                .ToList();

            foreach (var activity in activitiesDB)
            {
                activities.Add(new Activity()
                {
                    Id = activity.activityId,
                    Name = ActivityNameFromDictionary(activity.activityType),
                    SequenceNumber = activity.sequenceNumber,
                    Description = activity.description,
                    Result = activity.result,
                    Status = activity.status,
                    Start = activity.dateTimeOfActivityStart,
                    End = activity.dateTimeOfActivityEnd
                });
            }

            return activities;
        }
        public String ActivityNameFromDictionary(String activityType)
        {
            return context.ActivityDictionaries
                .Where(activity => activity.activityType == activityType)
                .First().activityName;
        }
        public Activity PursueWorkerActivity(int activityID, int workerID)
        {
            Models.Activity changedActivity = context.Personels
                .Where(p => p.personelId == workerID)
                .First().Activities.Where(a => a.activityId == activityID).First();
            changedActivity.dateTimeOfActivityEnd = DateTime.Now;
            changedActivity.status = "FIN";
            context.SaveChanges();

            Activity activity = new()
            {
                Id = changedActivity.activityId,
                Name = ActivityNameFromDictionary(changedActivity.activityType),
                SequenceNumber = changedActivity.sequenceNumber,
                Description = changedActivity.description,
                Result = changedActivity.result,
                Status = changedActivity.status,
                Start = changedActivity.dateTimeOfActivityStart,
                End = changedActivity.dateTimeOfActivityEnd
            };
            return activity;
        }
        public Activity CancelWorkerActivity(int activityID, int workerID)
        {
            Models.Activity changedActivity = context.Personels
               .Where(p => p.personelId == workerID)
               .First().Activities.Where(a => a.activityId == activityID).First();
            changedActivity.dateTimeOfActivityEnd = DateTime.Now;
            changedActivity.status = "CAN";
            context.SaveChanges();

            Activity activity = new()
            {
                Id = changedActivity.activityId,
                Name = ActivityNameFromDictionary(changedActivity.activityType),
                SequenceNumber = changedActivity.sequenceNumber,
                Description = changedActivity.description,
                Result = changedActivity.result,
                Status = changedActivity.status,
                Start = changedActivity.dateTimeOfActivityStart,
                End = changedActivity.dateTimeOfActivityEnd
            };
            return activity;
        }
        public class Activity
        {
            private string? _status;
            public int Id { get; set; }
            public string? Name { get; set; }
            public int? SequenceNumber { get; set; }
            public string? Description { get; set; }
            public string? Result { get; set; }
            public string Status { 

                get => _status!;

                set
                {
                    switch (value)
                    {
                        case "CAN":
                            _status = "Canceled";
                            break;
                        case "FIN":
                            _status = "Finished";
                            break;
                        case "OPN":
                            _status = "Open";
                            break;
                        case "PRO":
                            _status = "In progress";
                            break;
                        default:
                            _status = "Unknown";
                            break;
                    }
                }
            }
            public DateTime Start { get; set; }
            public DateTime? End { get; set; }
        }
    }
}
