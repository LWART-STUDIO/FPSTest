using System;
using UnityEngine;

namespace CodeBase.Logger
{
    public class LogMessage
    {
        public LogType Type { get; set; }
        public DateTime Time { get; set; }
        public string Message { get; set; }

        public LogMessage(LogType type, string message)
        {
            Type = type;
            Message = message;
            Time=DateTime.UtcNow;
        }
    }
}