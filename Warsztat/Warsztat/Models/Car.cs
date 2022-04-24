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

        [StringLength(10)]
        public string registrationNumber { get; set; }

        //Foreign Key
        public int clientId { get; set; }

        [Required]
        public Client client { get; set; }

        //Foreign Key
        public string carTypeMark { get; set; }

        [Required]
        public CarType carType { get; set; }


        public ICollection<Request> requests { get; set; }

        public int requestId { get; set; }
    }
}
