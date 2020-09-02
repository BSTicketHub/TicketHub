using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketHubDataLibrary.Models
{
    public class User
    {
        [Display(Name = "使用者代號")]
        public Guid Id { get; set; }
        [Display(Name = "使用者名稱")]
        [Required]
        public string UserName { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Display(Name = "手機號碼")]
        public string Mobile { get; set; }
        [Display(Name = "電子郵件")]
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<UserLogin> UserLogins { get; set; }
        [ForeignKey("UserId")]
        public ICollection<LoginLog> LoginLogs { get; set; }
        [ForeignKey("UserId")]
        public ICollection<ActionLog> ActionLogs { get; set; }
        [ForeignKey("UserId")]
        public ICollection<Ticket> Tickets { get; set; }
    }
}
