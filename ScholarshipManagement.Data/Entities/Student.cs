using ScholarshipManagement.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScholarshipManagement.Data.Entities
{
    public class Student : BaseEntity
    {
        [Required, MaxLength(10)]
        public string MemberCode { get; set; }

        [Required, MaxLength(50)]
        public string SurName { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(30)]
        public string OtherName { get; set; }

        [Required, MaxLength(250)]
        public string Address { get; set; }

        
        public Guid JamaatId { get; set; }

        [Required, MaxLength(50)]
        public Guid CircuitId { get; set; }

        [MaxLength(30)]
        public string PhoneNumber { get; set; }

        [Required, MaxLength(50)]
        public string EmailAddress { get; set; }

        [Required, MaxLength(10)]
        public Gender Gender { get; set; }

        [Required, MaxLength(10)]
        public DateTime DateOfBirth { get; set; }

        [Required, MaxLength(10)]
        public AuxiliaryBody AuxiliaryBody { get; set; }

        [Required, MaxLength(150)]
        public string Guardian { get; set; }

        [Required, MaxLength(11)]
        public string GuardianPhoneNumber { get; set; }

        [MaxLength(11)]
        public string GuardianMemberCode { get; set; }

        [Required, MaxLength(30), StringLength(1024)]
        public string Photograph { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }

        public ICollection<ApplicationForm> ApplicationForms { get; set; } = new HashSet<ApplicationForm>();
    }
}
