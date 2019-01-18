using LiveShow.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiveShow.Service.Dto
{
    public class MessageDto:BaseDto
    {
        public int ContentId { get; set; }

        public int Receiver { get; set; }

        public int Status { get; set; }

        public string ErrorMessage { get; set; }
    }
}
