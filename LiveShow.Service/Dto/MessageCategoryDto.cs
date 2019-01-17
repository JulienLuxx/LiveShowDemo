using LiveShow.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiveShow.Service.Dto
{
    public class MessageCategoryDto:BaseDto
    {
        public string Name { get; set; }

        public int Sort { get; set; }
    }
}
