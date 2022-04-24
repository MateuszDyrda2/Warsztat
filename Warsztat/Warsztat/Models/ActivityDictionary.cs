using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warsztat.Models
{
    class ActivityDictionary
    {
        [Key]
        public string activityType { get; set; }

        public string activityName { get; set; }

        public ICollection<Activity> Activities { get; set; }
    }
}
