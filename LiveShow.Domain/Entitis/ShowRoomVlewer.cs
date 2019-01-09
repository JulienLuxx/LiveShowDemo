using System;
using System.Collections.Generic;
using System.Text;

namespace LiveShow.Domain.Entitis
{
    public class ShowRoomVlewer
    {
        //public int Id { get; set; }

        public DateTime CreateTime { get; set; }

        public byte[] TimeStamp { get; set; }

        public int? UserId { get; set; }

        public int? ShowRoomId { get; set; }

        public virtual User User { get; set; }

        public virtual ShowRoom ShowRoom { get; set; }

    }
}
