using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScholarshipManagement.Data.Entities
{
    public class Jamaat : BaseEntity
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public int CircuitId { get; set; } //reference property

        public Circuit Circuit { get; set; } ///navigation prop

    }
}
