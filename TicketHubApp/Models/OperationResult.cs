using System;
using TicketHubApp.Interfaces;

namespace TicketHubApp.Models
{
    public class OperationResult : IResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }

        public OperationResult()
        {
            Success = false;
        }
    }
}
