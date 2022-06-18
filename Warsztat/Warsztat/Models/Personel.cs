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

        [Required]
        [MaxLength(40)]
        public string name { get; set; }

        public string surrname { get; set; }

        [StringLength(9)]
        public string phoneNumber { get; set; }

        public string role { get; set; }

        public string username { get; set; }

        public string password { get; set; }

        public bool isActive { get; set; }

        public ICollection<Request> requests { get; set; }

        public ICollection<Activity> Activities { get; set; }
    }
}
