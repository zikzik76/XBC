using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBC.ViewModel
{
    public class BatchViewModel
    {
        public BatchViewModel()
        {
            isDelete = false;
        }
        public long id { get; set; }
        public long batchTestId { get; set; }

        [Display(Name = "TECHNOLOGY")]
        public long technologyId { get; set; }

        [Display(Name = "TECHNOLOGY")]
        public string technologyName { get; set; }

        [Display(Name = "TRAINER")]
        public long trainerId { get; set; }

        [Display(Name = "TRAINER")]
        public string trainerName { get; set; }

        [Display(Name = "Participant")]
        public long biodataId { get; set; }

        [Display(Name = "Participant")]
        public string biodataName { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "NAME")]
        public string name { get; set; }

        [Required]
        [Display(Name = "Period From")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? periodFrom { get; set; }

        [Required]
        [Display(Name = "Period To")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? periodTo { get; set; }

        public long? roomId { get; set; }

        public long? bootcampTypeId { get; set; }

        [StringLength(255)]
        public string notes { get; set; }

        public long createdBy { get; set; }

        public DateTime createdOn { get; set; }

        public long? modifiedBy { get; set; }

        public DateTime? modifiedOn { get; set; }

        public long? deletedBy { get; set; }

        public DateTime? deletedOn { get; set; }

        public bool isDelete { get; set; }
    }
}
