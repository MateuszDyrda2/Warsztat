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
        private readonly ApplicationContext context;
        public Service()
        {
            context = new ApplicationContext();
        }
    }
}
