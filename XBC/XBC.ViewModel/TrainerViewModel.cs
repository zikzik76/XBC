using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBC.ViewModel
{
    public class TrainerViewModel
    {
        public TrainerViewModel()
        {
            is_delete = false;
        }
        public long id { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name= "Trainer Name")]
        public string name { get; set; }

        [StringLength(255)]
        [Display(Name = "Notes")]
        public string notes { get; set; }
        public bool is_delete { get; set; }

        [Display(Name = "Created By")]
        public long created_by { get; set; }

        public string created_by_name { get; set; }
    }
}
