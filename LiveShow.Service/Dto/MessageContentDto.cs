using LiveShow.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiveShow.Service.Dto
{
    public class MessageContentDto:BaseDto
    {
        /// <summary>
        /// 类型编号
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 发送者编号
        /// </summary>
        public int? SenderId { get; set; }

        /// <summary>
        /// 发送者名称
        /// </summary>
        public string SenderName { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime SendTime { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 接收对象类型
        /// </summary>
        public int ReceiveObjectType { get; set; }

        /// <summary>
        /// 接收对象编号
        /// </summary>
        public string ReceiveObjectIds { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }
    }

    public class MessageContentAddDto:MessageContentDto
    {
        public List<MessageDto> Messages { get; set; }
    }
}
