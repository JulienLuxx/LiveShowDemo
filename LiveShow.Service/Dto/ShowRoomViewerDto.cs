using LiveShow.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiveShow.Service.Dto
{
    public class ShowRoomViewerDto:BaseDto
    {
        public int? ShowRoomId { get; set; }

        public int? UserId { get; set; }
    }
}
