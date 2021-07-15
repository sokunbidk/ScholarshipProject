using ScholarshipManagement.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScholarshipManagement.Data.Entities
{
    public class User : BaseEntity
    {
        [Required, MaxLength(50)]
        public string UserName { get; set; }

        [Required, MaxLength(50)]
        public string PasswordHash { get; set; }

        public string HashSalt { get; set; }

        public UserType UserType { get; set; }
        
        public Student Student { get; set; }

        public string MemberCode { get; set; }

        [Display(Name = "User Description")]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
    }
}
