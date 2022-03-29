using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warsztat.Models
{
    class Car
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int carId { get; set; }

        public long registrationNumber { get; set; }

        public int clientId { get; set; }

        public Client client { get; set; }

    }
}
