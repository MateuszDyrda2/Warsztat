using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warsztat.Models
{
    class Personel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int personelId { get; set; }

        public string name { get; set; }

        public string surrname { get; set; }

        public long number { get; set; }

        public ICollection<Request> requests { get; set; }

        public int requestId { get; set; }
    }
}
