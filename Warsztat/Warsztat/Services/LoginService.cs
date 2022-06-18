using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Warsztat.Models;

namespace Warsztat.Services
{
    public partial class Service
    {
        public List<Personel> checkAllPersonel()
        {
            List<Personel> personels = new();

            var personelsDB = context.Personels.ToList();

            foreach (var personelDB in personelsDB)
            {  
               personels.Add(new Personel()
               {
                   Id = personelDB.personelId,
                   Name = personelDB.name,
                   Surname = personelDB.surrname,
                   PhoneNumber = personelDB.phoneNumber,
                   Role = personelDB.role,
                   IsActive = personelDB.isActive,
                   Username = personelDB.username,
                   Password = personelDB.password
               });
            }

            return personels;
        }

        public void AddAdmin()
        {
            var personelsDB = context.Personels.Where(p => p.role == "admin").ToList();

            SHA512 sha512Hash = SHA512.Create();
            //From String to byte array
            byte[] sourceBytes = Encoding.UTF8.GetBytes("admin");
            byte[] hashBytes = sha512Hash.ComputeHash(sourceBytes);
            string hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);

            if (personelsDB.Count == 0)
            {
                var personelDB = context.Personels.Add(new Models.Personel()
                {
                    name = "admin",
                    surrname = "admin",
                    phoneNumber = "000000000",
                    role = "Admin",
                    username = "admin",
                    password = hash,
                    isActive = true,
                }).Entity;
                context.SaveChanges();
            }
        }
    }
}
