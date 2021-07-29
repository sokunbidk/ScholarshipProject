using ScholarshipManagement.Data.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScholarshipManagement.Data.Entities
{
    public class User : BaseEntity
    {
        [Required, MaxLength(50)]
        [Display(Name = "User Email")]
        public string Email { get; set; }

        [Required, MaxLength(50)]
        public string PasswordHash { get; set; }

        public string HashSalt { get; set; }

        public UserType UserType { get; set; }

        public string MemberCode { get; set; }

        public string PhoneNumber { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
    }
}
