using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warsztat.Services
{
    public partial class Service
    {
        public List<Personel> Workers()
        {
            List<Personel> workers = new();

            var workersDB = context.Personels.Where(p => p.role == "Worker").ToList();

            foreach (var workerDB in workersDB)
            {
                workers.Add(new Personel()
                {
                    Id = workerDB.personelId,
                    Name = workerDB.name,
                    Surname = workerDB.surrname,
                    PhoneNumber = workerDB.phoneNumber,
                    Role = workerDB.role,
                    Username = workerDB.username,
                    Password = workerDB.password
                });
            }

            return workers;
        }
        public List<Client> Clients()
        {
            List<Client> clients = new();

            var clientsDB = context.Clients.ToList();

            foreach (var clientDB in clientsDB)
            {
                clients.Add(new Client()
                {
                    Id = clientDB.clientId,
                    Name = clientDB.name,
                    Surname = clientDB.surrname,
                    PhoneNumber = clientDB.phoneNumber
                });
            }

            return clients;
        }
        public List<Car> Cars(int clientId)
        {
            List<Car> cars = new();

            var carsDB = context.Cars.Where(car => car.clientId == clientId).ToList();

            foreach (var carDB in carsDB)
            {
                cars.Add(new Car()
                {
                    Id = carDB.carId,
                    RegistrationNumber = carDB.registrationNumber,
                    Mark = carDB.carTypeMark,
                    Model = context.CarTypes.Where(c => c.mark == carDB.carTypeMark).First().model
                });
            }

            return cars;
        }
        public List<Request> Requests(int carId, int managerId)
        {
            List<Request> requests = new();

            var requestsDB = context.Requests.Where(r => r.carId == carId && r.personelId == managerId).ToList();

            foreach (var requestDB in requestsDB)
            {
                requests.Add(new Request()
                {
                    Id = requestDB.requestId,
                    Description = requestDB.description,
                    Result = requestDB.result,
                    Status = requestDB.status,
                    Start = requestDB.dateTimeOfRequestStart,
                    End = requestDB.dateTimeOfRequestEnd
                });
            }

            return requests;
        }
        public List<Activity> Activities(int requestId)
        {
            List<Activity> activities = new();

            var activitiesDB = context.Activities.Where(a => a.requestId == requestId).ToList();

            foreach (var activityDB in activitiesDB)
            {
                activities.Add(new Activity()
                {
                    Id = activityDB.activityId,
                    Name = ActivityNameFromDictionary(activityDB.activityType),
                    SequenceNumber = activityDB.sequenceNumber,
                    Description = activityDB.description,
                    Result = activityDB.result,
                    Status = activityDB.status,
                    Start = activityDB.dateTimeOfActivityStart,
                    End = activityDB.dateTimeOfActivityEnd
                });
            }

            return activities;
        }
        public Client? AddNewClient(string name, string surname, string phoneNumber)
        {
            if (name != string.Empty
                && surname != string.Empty
                && phoneNumber != string.Empty
                && phoneNumber.Length == 9)
            {
                Models.Client clientDB = context.Clients.Add(new Models.Client()
                {
                    name = name,
                    surrname = surname,
                    phoneNumber = phoneNumber
                }).Entity;
                context.SaveChanges();

                Client client = new Client()
                {
                    Id = clientDB.clientId,
                    Name = clientDB.name,
                    Surname = clientDB.surrname,
                    PhoneNumber = clientDB.phoneNumber
                };
                return client;
            }

            return null;
        }

        public Car? AddNewCar(string registrationNumber, string mark, int clientId)
        {
            if (registrationNumber != string.Empty
                && registrationNumber.Length == 10
                && mark != string.Empty)
            {
                Models.Car carDB = context.Cars.Add(new Models.Car()
                {
                    registrationNumber = registrationNumber,
                    carTypeMark = mark,
                    clientId = clientId
                }).Entity;
                context.SaveChanges();

                Car car = new()
                {
                    Id = carDB.carId,
                    RegistrationNumber = carDB.registrationNumber,
                    Mark = carDB.carTypeMark,
                    Model = context.CarTypes.Where(c => c.mark == carDB.carTypeMark).First().model
                };
                return car;
            }

            return null;
        }
        public Request? AddNewRequest(string description, string status, DateTime start, int managerId, int carId)
        {
            if (description != string.Empty
                && status != string.Empty
                && status.Length == 3)
            {
                Models.Request requestDB = context.Requests.Add(new Models.Request()
                {
                    description = description,
                    status = status,
                    dateTimeOfRequestStart = start,
                    result = "none",
                    personelId = managerId,
                    carId = carId
                }).Entity;
                context.SaveChanges();

                Request request = new()
                {
                    Id = requestDB.requestId,
                    Description = requestDB.description,
                    Result = requestDB.result,
                    Status = requestDB.status,
                    Start = requestDB.dateTimeOfRequestStart,
                    End = requestDB.dateTimeOfRequestEnd
                };
                return request;
            }

            return null;
        }
        public Activity? AddNewActivity(string sequenceNumber, string description, int status, DateTime start, string type, int workerId, int requestId)
        {
            if (description != string.Empty
                && type != string.Empty)
            {
                int sequenceNumberInt = Int32.Parse(sequenceNumber);
                Models.Activity activityDB = context.Activities.Add(new Models.Activity()
                {
                    sequenceNumber = sequenceNumberInt,
                    description = description,
                    status = status,
                    result = "In progress",
                    dateTimeOfActivityStart = start,
                    activityType = type,
                    personelId = workerId,
                    requestId = requestId
                }).Entity;
                context.SaveChanges();

                Activity activity = new()
                {
                    Id = activityDB.activityId,
                    Name = ActivityNameFromDictionary(activityDB.activityType),
                    SequenceNumber = activityDB.sequenceNumber,
                    Description = activityDB.description,
                    Result = activityDB.result,
                    Status = activityDB.status,
                    Start = activityDB.dateTimeOfActivityStart,
                    End = activityDB.dateTimeOfActivityEnd
                };
                return activity;
            }

            return null;
        }

        public class Client
        {
            public int Id { get; set; }
            public string? Name { get; set; }
            public string? Surname { get; set; }
            public string? PhoneNumber { get; set; }
        }
        public class Car
        {
            public int Id { get; set; }
            public string? RegistrationNumber { get; set; }
            public string? Mark { get; set; }
            public string? Model { get; set; }
        }
        public class Request
        {
            public int Id { get; set; }
            public string? Description { get; set; }
            public string? Result { get; set; }
            public string? Status { get; set; }
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
        }
    }
}
