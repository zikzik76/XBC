using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBC.ViewModel
{
    public class MonitoringViewModel
    {
        public MonitoringViewModel()
        {
            is_delete = false;
        }

        public long id { get; set; }

        [Display(Name = "Name")]
        public long biodata_id { get; set; }
        [Display(Name = "Name")]
        public string biodataName { get; set; }

        [Display(Name = "Idle Date")]
        //[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime idle_date { get; set; }

        [StringLength(50)]
        public string last_project { get; set; }

        [StringLength(255)]
        public string idle_reason { get; set; }

        [Display(Name = "Placement Date")]
        //[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? placement_date { get; set; }

        [Display(Name = "Placement at")]
        [StringLength(50)]
        public string placement_at { get; set; }

        [Display(Name = "Notes")]
        [StringLength(255)]
        public string notes { get; set; }

        public long created_by { get; set; }

        public DateTime created_on { get; set; }

        public long? modified_by { get; set; }

        public DateTime? modified_on { get; set; }

        public long? deleted_by { get; set; }

        public DateTime? deleted_on { get; set; }

        public bool is_delete { get; set; }

        public string status_is_delete
        {
            get
            {
                if (is_delete)
                {
                    return "Non Active";

                }
                else
                {
                    return "Active";
                }
            }
            set { }
        }
    }
}
