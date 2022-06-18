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
                    ParentRequestName = "Request " + activity.requestId.ToString(),
                    WorkerId = activity.personelId,
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

        public Activity FinishOrCloseActivityStatus(string status, string result, int? id)
        {
            Models.Activity activityDB = context.Activities
                .Where(a => a.activityId == id).First();

            activityDB.status = status;
            activityDB.result = result;
            context.SaveChanges();

            Activity activity = new()
            {
                Id = activityDB.activityId,
                Name = ActivityNameFromDictionary(activityDB.activityType),
                SequenceNumber = activityDB.sequenceNumber,
                Description = activityDB.description,
                Result = activityDB.result,
                WorkerId = activityDB.personelId,
                Status = activityDB.status,
                Start = activityDB.dateTimeOfActivityStart,
                End = DateTime.Now
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
            public string? ParentRequestName { get; set; }
            public int? WorkerId { get; set; }
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
