using ScholarshipManagement.Data.Entities;
using ScholarshipManagement.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScholarshipManagement.Data.DTOs
{
    public class StudentDto
    {
        public Guid Id { get; set; }

        public string MemberCode { get; set; }

        public string SurName { get; set; }

        public string FirstName { get; set; }

        public string OtherName { get; set; }

        public string Address { get; set; }

        public Guid JamaatId { get; set; }

        public Guid CircuitId { get; set; }

        public string PhoneNumber { get; set; }

        public string EmailAddress { get; set; }

        public Gender Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public AuxiliaryBody AuxiliaryBody { get; set; }

        public string Guardian { get; set; }

        public string GuardianPhoneNumber { get; set; }

        public string GuardianMemberCode { get; set; }

        public string Photograph { get; set; }

        public Guid UserId { get; set; }

        public IList<ApplicationFormDto> ApplicationForms { get; set; } 
    }
}
