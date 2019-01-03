using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBC.ViewModel
{
    public class OfficeViewModel
    {
        public OfficeViewModel()
        {
            is_delete = false;
        }

        public long id { get; set; }

        public long roomId { get; set; }

        [Required]
        [StringLength(50)]

        [Display(Name = "NAME")]
        public string name { get; set; }

        [StringLength(50)]
        public string phone { get; set; }

        [StringLength(255)]
        public string email { get; set; }

        [Display(Name = "CONTACT")]
        public string PhoneEmail
        {
            get
            {
                return phone + "/" + email;
            }
        }

        [StringLength(500)]
        public string address { get; set; }

        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        public string notes { get; set; }

        public long created_by { get; set; }

        public DateTime created_on { get; set; }

        public bool is_delete { get; set; }

        public List<RoomViewModel> Details { get; set; }

        //ignore
        //[Required]
        //[StringLength(50)]
        //public string codeRoom { get; set; }

        //[Required]
        //[StringLength(50)]
        //public string nameRoom { get; set; }

        //public int capacity { get; set; }

        //public bool any_projector { get; set; }

        //[StringLength(500)]
        //[DataType(DataType.MultilineText)]
        //public string notesRoom { get; set; }
    }
}
