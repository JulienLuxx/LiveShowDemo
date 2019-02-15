using LiveShow.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiveShow.Service.Dto
{
    public class ShowRoomDto:BaseDto
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 最后启用时间
        /// </summary>
        public DateTime LastActivateTime { get; set; }

        /// <summary>
        /// 状态说明
        /// </summary>
        public string StatusName { get; set; }
    }
}
