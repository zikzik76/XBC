using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBC.ViewModel
{
    public class TestimonyViewModel
    {
        public TestimonyViewModel()
        {
            is_delete = false;
        }

        public long id { get; set; }

        [Required]
        [StringLength(255)]

        [Display(Name ="TITLE")]
        public string title { get; set; }

        [StringLength(5000)]
        [DataType(DataType.MultilineText)]
        public string content { get; set; }

        public bool is_delete { get; set; }
    }
}
