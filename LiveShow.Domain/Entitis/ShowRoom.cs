using LiveShow.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiveShow.Domain.Entitis
{
    public class ShowRoom
    {
        public ShowRoom()
        {
            //ShowRoomVlewers = new HashSet<ShowRoomVlewer>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public int Status { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime LastActivateTime { get; set; }

        public bool IsDeleted { get; set; }

        //public int UserId { get; set; }

        public byte[] TimeStamp { get; set; }

        //public virtual User User { get; set; }

        //public virtual ICollection<ShowRoomVlewer> ShowRoomVlewers { get; set; }

        //public virtual void Activate()
        //{
        //    LastActivateTime = DateTime.Now;
        //    Status = ShowRoomStatusEnum.Activate.GetHashCode();
        //}

        //public virtual void Disable()
        //{
        //    Status = ShowRoomStatusEnum.Disable.GetHashCode();
        //    if (ShowRoomVlewers.Count > 0)
        //    {
        //        ShowRoomVlewers.Clear();
        //    }
        //}
    }
}
