using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBC.ViewModel
{
    public class BTTViewModel
    {
        public BTTViewModel()
        {
            is_delete = false;
        }

        public long id { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "NAME")]
        public string name { get; set; }

        [StringLength(255)]
        [Display(Name = "NOTES")]
        public string notes { get; set; }

        [Display(Name = "CREATED BY")]
        public long created_by { get; set; }

        public DateTime created_on { get; set; }

        public long? modified_by { get; set; }

        public DateTime? modified_on { get; set; }

        public long? deleted_by { get; set; }

        public DateTime? deleted_on { get; set; }

        [Display(Name = "STATUS")]
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