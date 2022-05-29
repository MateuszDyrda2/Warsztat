using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warsztat.Models
{
    class Activity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int activityId { get; set; }

        public int? sequenceNumber { get; set; }

        public string description { get; set; }

        public string? result { get; set; }

        [Column(TypeName = "varchar(3)")]
        public string status { get; set; }

        public DateTime dateTimeOfActivityStart { get; set; }

        public DateTime? dateTimeOfActivityEnd { get; set; }

        //Foreign Key
        public int requestId { get; set; }

        [Required]
        public Request request{ get; set; }

        //Foreign Key
        public int? personelId { get; set; }

        public Personel personel { get; set; }

        //Foreign Key
        public string activityType { get; set; }

        [Required]
        public ActivityDictionary activityDictionary { get; set; }
    }
}
