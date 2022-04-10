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
        public int activityID { get; set; }

        public string description { get; set; }

        public string result { get; set; }

        public int status { get; set; }

        //Foreign Key
        public int requestId { get; set; }

        public Request request{ get; set; }

        //Foreign Key
        public int personelId { get; set; }

        public Personel personel { get; set; }

        //Foreign Key
        public string activityType { get; set; }

        public ActivityDictionary activityDictionary { get; set; }
    }
}
