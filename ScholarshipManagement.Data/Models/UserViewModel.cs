using System;
using ScholarshipManagement.Data.DTOs;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ScholarshipManagement.Data.Enums;
using ScholarshipManagement.Data.Entities;

namespace ScholarshipManagement.Data
{
    public class UserViewModel
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string MemberCode { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime CreatedAt { get; set; }
        public string FullName { get; set; }
    }

    public class CreateUserRequestModel
    {
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Jamaat is required")]
        [Display(Name = "Select Jamaat")]
        public int JamaatId { get; set; }

        [Required(ErrorMessage = "Circuit is required")]
        [Display(Name = "Select Circuit")]
        public int CircuitId { get; set; }

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

        public UserType UserType { get; set; } 
    }

    public class LoginUserRequestModel
    {
        [Display(Name = "User Full Name")]
        public string UserFullName { get; set; }

        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        public string Password { get; set; }


        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Member Code")]
        public string MemberCode { get; set; }

        [Display(Name = "User Type")]
        public UserType UserType { get; set; }

    }

    public class UpdateUserRequestModel
    {
        public int Id { get; set; }

        [Display(Name = "User Full Name")]
        public string UserFullName { get; set; }

        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Member Code")]
        public string MemberCode { get; set; }

        [Display(Name = "User Type")]
        public UserType UserType { get; set; }
    }

    public class UsersResponseModel : BaseResponse
    {
        public IEnumerable<UserDto> Data { get; set; } = new List<UserDto>();
    }

    public class UserResponseModel : BaseResponse
    {
        public UserDto Data { get; set; }
    }
    public class UserEntityResponseModel : BaseResponse
    {
        public User Data { get; set; }
    }
}

