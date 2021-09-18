using ScholarshipManagement.Data.Entities;
using ScholarshipManagement.Data.Enums;
using System;
using System.Collections.Generic;

namespace ScholarshipManagement.Data.DTOs
{
    public class StudentDto
    {
        public int UserId { get; set; }

        public string MemberCode { get; set; }

        public string PhoneNumber { get; set; }

        public string EmailAddress { get; set; }

        public string SurName { get; set; }

        public string FirstName { get; set; }

        public string OtherName { get; set; }

        public string Address { get; set; }

        public int JamaatId { get; set; }

        public string JamaatName { get; set; }

        public int CircuitId { get; set; }

        public string CircuitName { get; set; }

        public Gender Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public AuxiliaryBody AuxiliaryBody { get; set; }


        public string GuardianFullName { get; set; }

        public string GuardianPhoneNumber { get; set; }

        public string GuardianMemberCode { get; set; }

        public string Photograph { get; set; }

        public IList<ApplicationFormDto> ApplicationForms { get; set; } = new List<ApplicationFormDto>();
    }
}
