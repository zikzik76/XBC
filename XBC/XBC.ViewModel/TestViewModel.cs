using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBC.ViewModel
{
    public class TestViewModel
    {
        public TestViewModel()
        {
            is_delete = false;
        }

        public long id { get; set; }

        public long batchId { get; set; }

        public long batchTestId { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "NAME")]
        public string name { get; set; }

        [Display(Name = "Is Bootcamp Test?")]
        public bool is_bootcamp_test { get; set; }

        [StringLength(255)]
        [DataType(DataType.MultilineText)]
        public string notes { get; set; }
        [Display(Name = "CREATED BY")]
        public long created_by { get; set; }
        public DateTime created_on { get; set; }

        public long? modified_by { get; set; }

        public DateTime? modified_on { get; set; }

        public long? deleted_by { get; set; }

        public DateTime? deleted_on { get; set; }

        public bool is_delete { get; set; }
    }
}
