using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBC.ViewModel
{
    public class TechnologyViewModel
    {
        public TechnologyViewModel()
        {
            is_delete = false;
        }
        public long id { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Technology Name")]
        public string name { get; set; }

        [StringLength(255)]
        [Display(Name = "Notes")]
        public string notes { get; set; }

        [Display(Name = "Created By")]
        public long created_by { get; set; }

        public bool is_delete { get; set; }

        //public List<TechTrainerViewModel> TechTrainers { get; set; }
        public List<TrainerViewModel> trainers { get; set; }
    }
}
