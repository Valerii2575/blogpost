using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blogpost.Application.DTOs
{
    public class EmailSendDto
    {
        public string? To { get; set; }
        public string? From { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }

        public EmailSendDto(string to, string from, string subject, string body) 
        {
            To = to;
            From = from;
            Subject = subject;
            Body = body;
        }
    }
}
