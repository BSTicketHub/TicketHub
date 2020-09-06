using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketHubDataLibrary.Models
{
    public class Role
    {
        [Display(Name = "角色代號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Display(Name = "角色名稱")]
        [Required]
        public string Name { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
