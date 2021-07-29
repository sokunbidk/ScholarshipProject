using ScholarshipManagement.Data.Enums;

namespace ScholarshipManagement.Data.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string MemberCode { get; set; }

        public string PhoneNumber { get; set; }

        public UserType UserType { get; set; }
    }
}
