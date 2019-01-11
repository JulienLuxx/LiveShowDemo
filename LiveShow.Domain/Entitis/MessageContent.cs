using System;
using System.Collections.Generic;
using System.Text;

namespace LiveShow.Domain.Entitis
{
    public class MessageContent
    {
        public MessageContent()
        {
            Messages = new HashSet<Message>();
        }

        public int Id { get; set; }

        public int CategoryId { get; set; }

        public string Title { get; set; }

        public int? SenderId { get; set; }

        public string SenderName { get; set; }

        public DateTime SendTime { get; set; }

        public string Content { get; set; }

        public int ReceiveObjectType { get; set; }

        public string ReceiveObjectIds { get; set; }

        public int Status { get; set; }

        public string ErrorMessage { get; set; }

        public bool IsDeleted { get; set; }

        public byte[] TimeStamp { get; set; }

        public virtual MessageCategory MessageCategory { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}
