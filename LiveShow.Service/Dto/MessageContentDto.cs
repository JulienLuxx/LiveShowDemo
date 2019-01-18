using LiveShow.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiveShow.Service.Dto
{
    public class MessageContentDto:BaseDto
    {
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
    }

    public class MessageContentAddDto:MessageContentDto
    {
        public List<MessageDto> Messages { get; set; }
    }
}
