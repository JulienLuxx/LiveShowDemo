using System;
using System.Collections.Generic;
using System.Text;

namespace LiveShow.Domain.Entitis
{
    public class User
    {
        public User()
        {
            ShowRoomVlewers = new HashSet<ShowRoomViewer>();
        }
        public int Id { get; set; }

        public DateTime CreateTime { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string MailBox { get; set; }

        public string Mobile { get; set; }

        public int Status { get; set; }

        public string SaltValue { get; set; }

        public bool IsDeleted { get; set; }

        public byte[] TimeStamp { get; set; }

        public int RoleId { get; set; }

        //public int? ShowRoomId { get; set; }

        public virtual Role Role { get; set; }

        public virtual ICollection<ShowRoomViewer> ShowRoomVlewers { get; set; }
    }
}
