using System;
using System.Collections.Generic;
using System.Text;

namespace LiveShow.Domain.Entitis
{
    public class MessageCategory
    {
        public MessageCategory()
        {
            MessageContents = new HashSet<MessageContent>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int Sort { get; set; }

        public DateTime CreateTime { get; set; }

        public bool IsDeleted { get; set; }

        public byte[] TimeStamp { get; set; }

        public virtual ICollection<MessageContent> MessageContents { get; set; }
    }
}
