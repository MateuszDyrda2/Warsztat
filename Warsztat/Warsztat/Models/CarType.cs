using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warsztat.Models
{
    class CarType
    {
        public string mark { get; set; }

        [Key]
        public string model { get; set; }

        public ICollection<Car> Cars { get; set; }
    }
}
