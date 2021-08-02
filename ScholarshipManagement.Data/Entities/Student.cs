using ScholarshipManagement.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScholarshipManagement.Data.Entities
{
    public class Student : BaseEntity
    {
        public int UserId { get; set; }

        public User User { get; set; }

        [Required, MaxLength(50)]
        public string SurName { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(30)]
        public string OtherName { get; set; }

        [Required, MaxLength(250)]
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string MemberCode { get; set; }

        public int? JamaatId { get; set; }

        public Jamaat Jamaat { get; set; }

        public int? CircuitId { get; set; }

        public Circuit Circuit  { get; set; }

        public Gender Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public AuxiliaryBody AuxiliaryBody { get; set; }

        [Required, MaxLength(150)]
        public string GuardianFullName { get; set; }

        [Required, MaxLength(11)]
        public string GuardianPhoneNumber { get; set; }

        public string GuardianMemberCode { get; set; }

        
        public string Photograph { get; set; }

        public ICollection<ApplicationForm> ApplicationForms { get; set; } = new HashSet<ApplicationForm>();
    }
}
