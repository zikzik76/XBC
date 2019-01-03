using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBC.ViewModel
{
    public class RoomViewModel
    {
        public RoomViewModel()
        {
            is_delete = false;
        }
        public long id { get; set; }

        [Required]
        [StringLength(50)]
        public string code { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        public int capacity { get; set; }

        public bool any_projector { get; set; }

        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        public string notes { get; set; }

        public long office_id { get; set; }

        public bool is_delete { get; set; }
    }
}
