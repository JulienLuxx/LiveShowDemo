using System;
using System.Collections.Generic;
using System.Text;

namespace LiveShow.Domain.Entitis
{
    public class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }

        public DateTime CreateTime { get; set; }

        public string Name { get; set; }

        public int Status { get; set; }

        public bool IsDeleted { get; set; }

        public byte[] TimeStamp { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
