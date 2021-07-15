using System;
using ScholarshipManagement.Data.DTOs;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace ScholarshipManagement.Data
{
    public class UserViewModel
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime CreatedAt { get; set; }

        public string PhoneNo { get; set; }
    }

    public class CreateUserRequestModel
    {
        [Required(ErrorMessage = "User name is required"), Display(Name = "User Name")]
        public string UserName { get; set; }

        public string MemberCode { get; set; }

        [Display(Name = "User Description")]
        public string Email { get; set; }

        public string Password { get; set; }
        public string ConfirmPassword { get; set; }


        public string PhoneNumber { get; set; }
    }

    public class UpdateUserRequestModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "User name is required"), Display(Name = "User Name")]
        public string UserName { get; set; }

        public string PhoneNumber { get; set; }

        [Display(Name = "Phone number Description")]
        public string PhoneNo { get; set; }
        public string Email { get; set; }
    }
    public class UsersResponseModel : BaseResponse
    {
        public  IEnumerable<UserDto> Data { get; set; } = new List<UserDto>();
    }

    public class UserResponseModel : BaseResponse
    {
        public UserDto Data { get; set; }
    }
}

