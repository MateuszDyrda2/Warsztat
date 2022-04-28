using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warsztat.Models;

namespace Warsztat
{
    public class Service
    {
        private ApplicationContext context;
        public Service()
        {
            context = new ApplicationContext();
        }
        public List<Dictionary<string, dynamic>> WorkerActivitities(int workerID)
        {
            Dictionary<string, dynamic> dictionary = new Dictionary<string, dynamic>();
            List<Dictionary<string, dynamic>> list = new List<Dictionary<string, dynamic>>();

            var activities = context.Personels
                .Where(p => p.personelId == workerID)
                .First().Activities.ToList();

            foreach(var activity in activities)
            {
                dictionary.Add("type", activity.activityType);
                dictionary.Add("sequenceNumber", activity.sequenceNumber);
                dictionary.Add("description", activity.description);
                dictionary.Add("result", activity.result);
                dictionary.Add("status", activity.status);
                dictionary.Add("dateTimeOfActivityStart", activity.dateTimeOfActivityStart);
                dictionary.Add("dateTimeOfActivityEnd", activity.dateTimeOfActivityEnd);
            }
            return list;
        }
        public string ActivityNameFromDictionary(string activityType)
        {
            return context.ActivityDictionaries
                .Where(activity => activity.activityType == activityType)
                .First().activityName;
        }
        public Dictionary<string, dynamic> PursueWorkerActivity(int indexOfActivity, int workerID)
        {
            Activity changedActivity = context.Personels
                .Where(p => p.personelId == workerID)
                .First().Activities.ElementAt(indexOfActivity);
            changedActivity.dateTimeOfActivityEnd = DateTime.Now;
            changedActivity.status = 1;
            context.SaveChanges();

            Dictionary<string, dynamic> dictionary = new Dictionary<string, dynamic>();
            dictionary.Add("type", changedActivity.activityType);
            dictionary.Add("sequenceNumber", changedActivity.sequenceNumber);
            dictionary.Add("description", changedActivity.description);
            dictionary.Add("result", changedActivity.result);
            dictionary.Add("status", changedActivity.status);
            dictionary.Add("dateTimeOfActivityStart", changedActivity.dateTimeOfActivityStart);
            dictionary.Add("dateTimeOfActivityEnd", changedActivity.dateTimeOfActivityEnd);
            return dictionary;
        }
        public Dictionary<string, dynamic> CancelWorkerActivity(int indexOfActivity, int workerID)
        {
            Activity changedActivity = context.Personels
                .Where(p => p.personelId == workerID)
                .First().Activities.ElementAt(indexOfActivity);
            changedActivity.dateTimeOfActivityEnd = DateTime.Now;
            changedActivity.status = 0;
            context.SaveChanges();

            Dictionary<string, dynamic> dictionary = new Dictionary<string, dynamic>();
            dictionary.Add("type", changedActivity.activityType);
            dictionary.Add("sequenceNumber", changedActivity.sequenceNumber);
            dictionary.Add("description", changedActivity.description);
            dictionary.Add("result", changedActivity.result);
            dictionary.Add("status", changedActivity.status);
            dictionary.Add("dateTimeOfActivityStart", changedActivity.dateTimeOfActivityStart);
            dictionary.Add("dateTimeOfActivityEnd", changedActivity.dateTimeOfActivityEnd);
            return dictionary;
        }
    }
}
