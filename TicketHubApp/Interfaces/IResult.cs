﻿using System;

namespace TicketHubApp.Interfaces
{
    public interface IResult
    {
        bool Success { get; set; }
        string Message { get; set; }
        Exception Exception { get; set; }
    }
}
