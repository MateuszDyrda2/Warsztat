﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warsztat.Models
{
    class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int clientId { get; set; }

        public string name { get; set; }

        public string surrname { get; set; }

        public long number { get; set; }

        public int carId { get; set; }

        public ICollection<Car> Cars { get; set; }

    }
}
