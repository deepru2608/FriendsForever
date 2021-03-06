﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsForever_App.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Remote(action: "IsEmailInUse", controller: "Account")]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [Phone]
        [Remote(action: "IsMobileInUse", controller: "Account")]
        public string MobileNo { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public DateTime Dob { get; set; }
        [Required]
        public string Username { get; set; }
    }
}
