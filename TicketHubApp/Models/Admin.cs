using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketHubApp.Models
{
    public class Admin
    {
        [Display(Name = "管理者代號")]
        public Guid Id { get; set; }
        [Display(Name = "管理者名稱")]
        [Required]
        public string UserName { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Display(Name = "電子郵件")]
        public string Email { get; set; }
        [ForeignKey("UserId")]
        public ICollection<LoginLog> LoginLogs { get; set; }
        [ForeignKey("UserId")]
        public ICollection<ActionLog> ActionLogs { get; set; }
    }
}
