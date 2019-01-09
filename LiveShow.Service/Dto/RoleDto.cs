using LiveShow.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiveShow.Service.Dto
{
    public class RoleDto : BaseDto
    {
        public string Name { get; set; }

        public int Status { get; set; }
    }
}
