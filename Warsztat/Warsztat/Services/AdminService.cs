using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warsztat
{
    public partial class Service
    {
        public List<Dictionary<string, dynamic>> WorkersAndManagers()
        {
            Dictionary<string, dynamic> dictionary = new Dictionary<string, dynamic>();
            List<Dictionary<string, dynamic>> list = new List<Dictionary<string, dynamic>>();

            var personels = context.Personels.ToList();

            foreach (var personel in personels)
            {
                if (personel.role != "Admin")
                {
                    dictionary.Add("id", personel.personelId);
                    dictionary.Add("name", personel.name);
                    dictionary.Add("surname", personel.surrname);
                    dictionary.Add("phoneNumber", personel.phoneNumber);
                    dictionary.Add("role", personel.role);
                    dictionary.Add("username", personel.username);
                    dictionary.Add("password", personel.password);
                }
            }

            return list;
        }
    }
}
