﻿using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.Models
{
    public class Setting : BaseEntity
    {

        [Required]
        public string Key { get; set; }

        public string Value { get; set; }
    }
}
