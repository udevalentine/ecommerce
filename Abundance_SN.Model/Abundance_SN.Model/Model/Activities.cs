using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abundance_SN.Model.Model
{
    public class Activities
    {
        public long Id { get; set; }
        public int? SubjectId { get; set; }
        public string SubjectType { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public System.DateTime UpdatedAt { get; set; }

        public User User { get; set; }
    }
}
