using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warsztat.Models
{
    class Request
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int requestId { get; set; }

        public string description { get; set; }

        public string result { get; set; }

        public int status { get; set; }

        //Foreign Key
        public int carId { get; set; }

        public Car car { get; set; }

        //Foreign Key
        public int personelId { get; set; }

        public Personel personel { get; set; }

    }
}
