﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TestingMVC.ViewModels
{
    public class UserRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}