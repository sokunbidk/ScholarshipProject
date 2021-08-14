using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScholarshipManagement.Data.Entities
{
    public class Circuit : BaseEntity
    {
       
        public string CircuitName { get; set; }
        public string Email { get; set; }
        public int PresidentId { get; set; }

        public ICollection<Jamaat> Jamaats { get; set; } = new HashSet<Jamaat>();
    }
}
