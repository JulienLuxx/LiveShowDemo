using System;
using System.Collections.Generic;
using System.Text;

namespace LiveShow.Domain.Entitis
{
    public class Message
    {
        public int Id { get; set; }

        public int ContentId { get; set; }

        public int Receiver { get; set; }

        public int Status { get; set; }

        public string ErrorMessage { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreateTime { get; set; }

        public byte[] TimeStamp { get; set; }

        public virtual MessageContent MessageContent { get; set; }
    }
}
