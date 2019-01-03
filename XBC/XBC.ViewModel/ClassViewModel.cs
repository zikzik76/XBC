using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBC.ViewModel
{
    public class ClassViewModel
    {
        public long id { get; set; }

        [Display(Name = "BATCH")]
        public long batchId { get; set; }

        [Display(Name = "BATCH")]
        public string batchName { get; set; }

        [Display(Name = "NAME")]
        public long biodataId { get; set; }

        [Display(Name = "NAME")]
        public string biodataName { get; set; }

        public long technologyId { get; set; }
        public string technologyName { get; set; }

        [Display(Name = "BATCH")]
        public string TechnologyBatch
        {
            get
            {
                return technologyName + " " + batchName;
            }
        }

        public long created_by { get; set; }

        public DateTime created_on { get; set; }


    }
}
