using LiveShow.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiveShow.Service.Dto
{
    public class ShowRoomDto:BaseDto
    {
        public string Name { get; set; }

        public string Title { get; set; }

        public int Status { get; set; }

        public DateTime LastActivateTime { get; set; }

        public string StatusName { get; set; }
    }
}
