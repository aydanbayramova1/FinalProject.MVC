﻿namespace FinalProjectMvc.Models
{
    public class ContactUs : BaseEntity
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string MessageTitle { get; set; }
        public string MessageDescription { get; set; }
    }
}
