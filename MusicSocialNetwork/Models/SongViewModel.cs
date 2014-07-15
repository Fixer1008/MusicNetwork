﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicSocialNetwork.Models
{
    public class SongViewModel
    {
        [Required]
        public string SongName { get; set; }
        [Required]
        public string Artist { get; set; }
        [Required]
        public int Duration { get; set; }
        [Url]
        [Required]
        public string Url { get; set; }
    }
}