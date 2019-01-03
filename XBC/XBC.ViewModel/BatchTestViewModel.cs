using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBC.ViewModel
{
    public class BatchTestViewModel
    {
        public long id { get; set; }

        public long? batchId { get; set; }

        public long? testId { get; set; }

        public long? created_by { get; set; }

        public DateTime? created_on { get; set; }
    }
}
