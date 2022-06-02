using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Warsztat.Services
{
    public partial class Service
    {
        public List<Personel> WorkersAndManagers()
        {
            List<Personel> personels = new();

            var personelsDB = context.Personels.ToList();

            foreach (var personelDB in personelsDB)
            {
                if (personelDB.role != "Admin")
                {
                    SHA512 sha512Hash = SHA512.Create();
                    //From String to byte array
                    byte[] sourceBytes = Encoding.UTF8.GetBytes(personelDB.password);
                    byte[] hashBytes = sha512Hash.ComputeHash(sourceBytes);
                    string hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);
                    personels.Add(new Personel()
                    {
                        Id = personelDB.personelId,
                        Name = personelDB.name,
                        Surname = personelDB.surrname,
                        PhoneNumber = personelDB.phoneNumber,
                        Role = personelDB.role,
                        Username = personelDB.username,
                        Password = hash
                    });
                }
            }

            return personels;
        }

        public Personel? AddPersonel(string name, string surname, string phoneNumber, string role, string username, string password, int? id)
        {
            if (name != string.Empty 
                && surname != string.Empty 
                && phoneNumber != string.Empty 
                && phoneNumber.Length == 9
                && role != string.Empty 
                && username != string.Empty 
                && password != string.Empty)
            {
                Models.Personel? personelDB = null;

                SHA512 sha512Hash = SHA512.Create();
                //From String to byte array
                byte[] sourceBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha512Hash.ComputeHash(sourceBytes);
                string hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);
                if (id == null)
                {
                    personelDB = context.Personels.Add(new Models.Personel()
                    {
                        name = name,
                        surrname = surname,
                        phoneNumber = phoneNumber,
                        role = role,
                        username = username,
                        password = hash
                    }).Entity;
                    context.SaveChanges();
                }
                else
                {
                    personelDB = context.Personels
                    .Where(p => p.personelId == id).First();

                    personelDB.name = name;
                    personelDB.surrname = surname;
                    personelDB.phoneNumber = phoneNumber;
                    personelDB.role = role;
                    personelDB.username = username;
                    personelDB.password = hash;
                    context.SaveChanges();
                }

                Personel personel = new Personel()
                {
                    Id = personelDB!.personelId,
                    Name = personelDB.name,
                    Surname = personelDB.surrname,
                    PhoneNumber = personelDB.phoneNumber,
                    Role = personelDB.role,
                    Username = personelDB.username,
                    Password = hash
                };
                return personel;
            }

            return null;
        }
        public Personel? ModifyPersonel(string name, string surname, string phoneNumber, string role, string username, string password, int personelId)
        {
            if (name != string.Empty
                && surname != string.Empty
                && phoneNumber != string.Empty
                && phoneNumber.Length == 9
                && role != string.Empty
                && username != string.Empty
                && password != string.Empty)
            {
                Models.Personel changedPersonel = context.Personels
                .Where(p => p.personelId == personelId).First();

                changedPersonel.name = name;
                changedPersonel.surrname = surname;
                changedPersonel.phoneNumber = phoneNumber;
                changedPersonel.role = role;
                changedPersonel.username = username;
                changedPersonel.password = password;
                context.SaveChanges();

                Personel personel = new Personel()
                {
                    Id = changedPersonel.personelId,
                    Name = changedPersonel.name,
                    Surname = changedPersonel.surrname,
                    PhoneNumber = changedPersonel.phoneNumber,
                    Role = changedPersonel.role,
                    Username = changedPersonel.username,
                    Password = changedPersonel.password
                };
                return personel;
            }

            return null;
        }
        public void DeletePersonel(int personelId)
        {
            Models.Personel personel = context.Personels
            .Where(p => p.personelId == personelId)
            .First();

            context.Personels.Remove(personel);
            context.SaveChanges();
        }

        public class Personel
        {
            public int Id { get; set; }
            public string? Name { get; set; }
            public string? Surname { get; set; }
            public string? PhoneNumber { get; set; }
            public string? Role { get; set; }
            public string? Username { get; set; }
            public string? Password { get; set; }
            public override string ToString()
            {
                return $"{Name} {Surname}";
            }
        }
    }
}
