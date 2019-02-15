using LiveShow.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiveShow.Service.Dto
{
    public class MessageDto:BaseDto
    {
        /// <summary>
        /// 内容编号
        /// </summary>
        public int ContentId { get; set; }

        /// <summary>
        /// 接收者编号
        /// </summary>
        public int Receiver { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
