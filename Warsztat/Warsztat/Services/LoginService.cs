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
                   Username = personelDB.username,
                   Password = personelDB.password
               });
            }

            return personels;
        }
    }
}
