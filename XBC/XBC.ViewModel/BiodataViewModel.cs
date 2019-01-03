using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBC.ViewModel
{
    public class BiodataViewModel
    {
        public BiodataViewModel()
        {
            isDelete = false;
        }
        public long id { get; set; }

        [Required]
        [StringLength(255)]
        public string name { get; set; }

        [StringLength(5)]
        public string gender { get; set; }

        [Required]
        [StringLength(100)]
        public string lastEducation { get; set; }

        [Required]
        [StringLength(5)]
        public string graduationYear { get; set; }

        [Required]
        [StringLength(5)]
        public string educationalLevel { get; set; }

        [Required]
        [StringLength(100)]
        public string majors { get; set; }

        [Required]
        [StringLength(5)]
        public string gpa { get; set; }

        public long? bootcampTestType { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid Numeric")]
        public int? iq { get; set; }

        [Required]
        [RegularExpression("^[0-9]{1,12}$", ErrorMessage = "DU Must Be Numeric")]
        public string du { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid Numeric")]
        public int? arithmetic { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid Numeric")]
        public int? nestedLogic { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid Numeric")]
        public int? joinTable { get; set; }

        [StringLength(50)]
        public string tro { get; set; }

        [StringLength(100)]
        public string notes { get; set; }

        [StringLength(100)]
        public string interviewer { get; set; }

        public long createdBy { get; set; }

        public DateTime createdOn { get; set; }
        public long? modified_by { get; set; }
        public DateTime? modified_on { get; set; }

        public long? deleted_by { get; set; }

        public DateTime? deleted_on { get; set; }

        public bool isDelete { get; set; }

    }
}
