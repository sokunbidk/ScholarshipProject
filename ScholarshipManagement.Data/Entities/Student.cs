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

        [Required(ErrorMessage = "Surname is required")]
        [MaxLength(50)]
        public string SurName { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(30)]
        public string OtherName { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [MaxLength(250)]
        public string Address { get; set; }

        [Required(ErrorMessage = "PhoneNumber is required")]
        [MaxLength(11)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "EmailAddress is required")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "MemberCode is required")]
        public string MemberCode { get; set; }
        public int CircuitId { get; set; }
        public int JamaatId { get; set; }
        public Jamaat Jamaat { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "DOB is required")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "AuxiliaryBody is required")]
        public AuxiliaryBody AuxiliaryBody { get; set; }

        [Required(ErrorMessage = "GuardianFullName is required")]
        [MaxLength(150)]
        public string GuardianFullName { get; set; }

        [Required(ErrorMessage = "GuardianPhoneNumber is required")]
        [MaxLength(11)]
        public string GuardianPhoneNumber { get; set; }

        [Required(ErrorMessage = "GuardianMemberCode is required")]
        public string GuardianMemberCode { get; set; }

        
        public string Photograph { get; set; }

        public ICollection<ApplicationForm> ApplicationForms { get; set; } = new HashSet<ApplicationForm>();
    }
}
