﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicSocialNetwork.Models
{
    [Serializable]
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Pass { get; set; }
    }
}