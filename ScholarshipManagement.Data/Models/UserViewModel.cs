using System;
using ScholarshipManagement.Data.DTOs;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ScholarshipManagement.Data.Enums;

namespace ScholarshipManagement.Data
{
    public class UserViewModel
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string MemberCode { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime CreatedAt { get; set; }
    }

    public class CreateUserRequestModel
    {
        [Display(Name = "Member Code")]
        public string MemberCode { get; set; }

        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        public UserType UserType { get; set; } = UserType.Student;
    }

    public class LoginUserRequestModel
    {
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        public string Password { get; set; }
    }

    public class UpdateUserRequestModel
    {
        public int Id { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }

    public class UsersResponseModel : BaseResponse
    {
        public IEnumerable<UserDto> Data { get; set; } = new List<UserDto>();
    }

    public class UserResponseModel : BaseResponse
    {
        public UserDto Data { get; set; }
    }
}

