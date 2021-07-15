using ScholarshipManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScholarshipManagement.Data.Entities
{
    public class Role : BaseEntity
    {
        public string RoleName { get; set; }

        public string Description { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();

    }
}
