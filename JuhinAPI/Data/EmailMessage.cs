using System;
using System.Collections.Generic;


namespace JuhinAPI.Data
{
    public class EmailMessage
    {
        public EmailMessage()
        {
            ToAddresses = new List<EmailAddress>();
            FromAddress = new EmailAddress();
        }

        public List<EmailAddress> ToAddresses { get; set; }
        public EmailAddress FromAddress { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

    }
}
