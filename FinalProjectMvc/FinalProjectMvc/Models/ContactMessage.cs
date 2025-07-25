﻿namespace FinalProjectMvc.Models
{
    public class ContactMessage :BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }
        public string? Reply { get; set; }
        public DateTime? ReplyDate { get; set; } 
        public bool IsReplied { get; set; } = false; 
    }
}
