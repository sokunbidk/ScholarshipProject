using ScholarshipManagement.Data.Entities;
using ScholarshipManagement.Data.Enums;

namespace ScholarshipManagement.Data.DTOs
{
    public class UserDto : BaseEntity
    {
        public int Id { get; set; }
        public string UserFullName { get; set; }
        public int CircuitId { get; set; }
        public int JamaatId { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string HashSalt { get; set; }

        public string MemberCode { get; set; }

        public string PhoneNumber { get; set; }

        public UserType UserType { get; set; }
        public string CreatedBy { get; set; }
    }
}
