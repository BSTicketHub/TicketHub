using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketHubDataLibrary.Models
{
    public class ActionLog
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Action { get; set; }
        public string Message { get; set; }
        public DateTime LogedDate { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public TicketHubUser User { get; set; }
    }
}
