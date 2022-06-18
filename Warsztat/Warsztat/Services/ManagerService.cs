using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warsztat.Services
{
    public partial class Service
    {
        public List<Personel> ActiveWorkers()
        {
            List<Personel> workers = new();

            var workersDB = context.Personels.Where(p => p.role == "Worker" && p.isActive == true).ToList();

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
        public string WorkerName(int workerId)
        {
            var workerDB = context.Personels.Where(p => p.personelId == workerId).FirstOrDefault();
            if (workerDB != null)
                return $"{workerDB.name} {workerDB.surrname}";
            else
                return string.Empty;
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
                    Model = context.CarTypes.Where(c => c.model == carDB.carTypeMark).First().mark
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
        public List<string> AllCars()
        {
            List<string> carModels = context.CarTypes
                .Select(car => car.model).ToList();

            List<string> allCars = new();

            foreach(string carModel in carModels)
            {
                string? carMark = context.CarTypes.Where(carType => carType.model == carModel).Select(carType => carType.mark).FirstOrDefault();
                allCars.Add($"{carMark} {carModel}");
            }

            return allCars;
        }
        public int? HighestSequenceNumber(int requestId)
        {
            List<int?> sequenceNumber = context.Activities.Where(a => a.requestId == requestId).Select(a => a.sequenceNumber).ToList();

            return sequenceNumber.Count != 0 ? sequenceNumber.Max() : null;
        }
        public List<string> AllActivityTypes()
        {
            return context.ActivityDictionaries.Select(a => a.activityName).ToList();
        }
        public string AcitivtyTypeByName(string activityName)
        {
            return context.ActivityDictionaries.Where(a => a.activityName == activityName).Select(a => a.activityType).FirstOrDefault()!;
        }
        public Client? AddNewClient(string name, string surname, string phoneNumber, int? id)
        {
            if (name != string.Empty
                && surname != string.Empty
                && phoneNumber != string.Empty
                && phoneNumber.Length == 9)
            {
                Models.Client? clientDB = null;

                if (id == null)
                {
                    clientDB = context.Clients.Add(new Models.Client()
                    {
                        name = name,
                        surrname = surname,
                        phoneNumber = phoneNumber
                    }).Entity;
                    context.SaveChanges();
                }
                else
                {
                    clientDB = context.Clients
                    .Where(p => p.clientId == id).First();

                    clientDB.name = name;
                    clientDB.surrname = surname;
                    clientDB.phoneNumber = phoneNumber;
                    context.SaveChanges();
                }
               

                Client client = new Client()
                {
                    Id = clientDB!.clientId,
                    Name = clientDB.name,
                    Surname = clientDB.surrname,
                    PhoneNumber = clientDB.phoneNumber
                };
                return client;
            }

            return null;
        }

        public Car? AddNewCar(string registrationNumber, string model, int clientId, int?id )
        {
            if (registrationNumber != string.Empty
                && registrationNumber.Length == 10
                && model != string.Empty)
            {
                Models.Car? carDB = null;

                if (id == null)
                {
                    carDB = context.Cars.Add(new Models.Car()
                    {
                        registrationNumber = registrationNumber,
                        carTypeMark = model,
                        clientId = clientId
                    }).Entity;
                    context.SaveChanges();
                }
                else
                {
                    carDB = context.Cars
                    .Where(p => p.carId == id).First();

                    carDB.registrationNumber = registrationNumber;
                    carDB.carTypeMark = model;
                    carDB.clientId = clientId;
                    context.SaveChanges();
                }

                Car car = new()
                {
                    Id = carDB!.carId,
                    RegistrationNumber = carDB.registrationNumber,
                    Mark = carDB.carTypeMark,
                    Model = context.CarTypes.Where(c => c.model == carDB.carTypeMark).First().mark
                };
                return car;
            }

            return null;
        }
        public Request? AddNewRequest(string description, int managerId, int carId, int? id)
        {
            string status = "OPN";
            DateTime start = DateTime.Now;

            if (description != string.Empty)
            {
                Models.Request? requestDB = null;

                if (id == null)
                {
                    requestDB = context.Requests.Add(new Models.Request()
                    {
                        description = description,
                        status = status,
                        dateTimeOfRequestStart = start,
                        personelId = managerId,
                        carId = carId
                    }).Entity;
                    context.SaveChanges();
                }
                else
                {
                    requestDB = context.Requests
                   .Where(p => p.carId == id).First();

                    requestDB.description = description;
                    requestDB.status = status;
                    requestDB.dateTimeOfRequestStart = start;
                    requestDB.personelId = managerId;
                    requestDB.carId = carId;
                    context.SaveChanges();
                }

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
        public Activity? AddNewActivity(string sequenceNumber, string description, string type, int? workerId, int requestId, int? id)
        {
            string status = "OPN";
            DateTime start = DateTime.Now;

            if (description != string.Empty
                && type != string.Empty)
            {
                int? sequenceNumberInt = null;
                if (sequenceNumber != string.Empty)
                {
                    sequenceNumberInt = Int32.Parse(sequenceNumber);
                    _updateAllActivitySequenceNumbers(sequenceNumberInt, requestId);
                }

                Models.Activity? activityDB = null;

                if (id == null)
                {
                    activityDB = context.Activities.Add(new Models.Activity()
                    {
                        sequenceNumber = sequenceNumberInt,
                        description = description,
                        status = status,
                        dateTimeOfActivityStart = start,
                        activityType = type,
                        requestId = requestId,
                        personelId = workerId
                    }).Entity;
                    context.SaveChanges();
                }
                else
                {
                    activityDB = context.Activities
                        .Where(p => p.activityId == id).First();

                    activityDB.sequenceNumber = sequenceNumberInt;
                    activityDB.description = description;
                    activityDB.status = status;
                    activityDB.activityType = type;
                    activityDB.requestId = requestId;
                    activityDB.personelId = workerId;
                    context.SaveChanges();
                }

                Activity activity = new()
                {
                    Id = activityDB!.activityId,
                    Name = ActivityNameFromDictionary(activityDB.activityType),
                    SequenceNumber = activityDB.sequenceNumber,
                    Description = activityDB.description,
                    Result = activityDB.result,
                    Status = activityDB.status,
                    Start = activityDB.dateTimeOfActivityStart
                };
                return activity;
            }

            return null;
        }
        public Request UpdateRequestStatus(string status, int id)
        {
            Models.Request requestDB = context.Requests
                  .Where(p => p.requestId == id).First();

            requestDB.status = status;
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

        public Request FinishOrCloseRequestStatus(string status, string result, int? id)
        {
            Models.Request requestDB = context.Requests
                  .Where(r => r.requestId == id).First();

            requestDB.status = status;
            requestDB.result = result;
            context.SaveChanges();

            Request request = new()
            {
                Id = requestDB.requestId,
                Description = requestDB.description,
                Result = requestDB.result,
                Status = requestDB.status,
                Start = requestDB.dateTimeOfRequestStart,
                End = DateTime.Now
            };
            return request;
        }

        private void _updateAllActivitySequenceNumbers(int? sequenceNumber, int requestId)
        {
            List<Activity> activities = new();

            var activitiesDB = context.Activities.Where(a => a.requestId == requestId).ToList();

            foreach (var activityDB in activitiesDB)
            {
                if (activityDB.sequenceNumber >= sequenceNumber)
                    activityDB.sequenceNumber++;
            }
            context.SaveChanges();
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
            private string? _status;
            public int Id { get; set; }
            public string? Description { get; set; }
            public string? Result { get; set; }
            public string Status
            {

                get => _status!;

                set
                {
                    switch (value)
                    {
                        case "CAN":
                            _status = "Cancelled";
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
