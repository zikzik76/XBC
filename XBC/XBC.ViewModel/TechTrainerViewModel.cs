using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBC.ViewModel
{
    public class TechTrainerViewModel
    {
        public long id { get; set; }

        public long technology_id { get; set; }

        [Display(Name="Trainer")]
        public long trainer_id { get; set; }
        public long created_by { get; set; }

        public string name { get; set; }

        //public virtual t_technology t_technology { get; set; }

        //public virtual t_trainer t_trainer { get; set; }
    }
}
