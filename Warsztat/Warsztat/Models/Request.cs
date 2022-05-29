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

        public string? result { get; set; }

        [Column(TypeName ="varchar(3)")]
        public string status { get; set; }

        public DateTime dateTimeOfRequestStart { get; set; }

        public DateTime? dateTimeOfRequestEnd { get; set; }

        //Foreign Key
        public int carId { get; set; }

        [Required]//Bo nie ma requestu bez samochodu
        public Car car { get; set; }

        //Foreign Key
        public int personelId { get; set; }

        [Required]//Bo nie ma requestu bez personelu
        public Personel personel { get; set; }


        public int activityId { get; set; }

        public ICollection<Activity> activities { get; set; }

    }
}
