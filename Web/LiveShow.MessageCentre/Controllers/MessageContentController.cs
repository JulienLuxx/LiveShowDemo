using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiveShow.Service.Dto;
using LiveShow.Service.Interface;
using LiveShow.Service.QueryModel;
using Microsoft.AspNetCore.Mvc;

namespace LiveShow.MessageCenter.Controllers
{
    [Produces("application/json")]
    [Route("API/MessageContent")]
    public class MessageContentController : Controller
    {
        private readonly IMessageContentSvc _messageContentSvc;
        public MessageContentController(IMessageContentSvc messageContentSvc)
        {
            _messageContentSvc = messageContentSvc;
        }

        [HttpPost("Add")]
        public async Task<JsonResult> AddAsync(MessageContentAddDto dto)
        {
            var resultTask = _messageContentSvc.AddAsync(dto);
            return Json(await resultTask);
        }

        [HttpGet("Page")]
        public async Task<JsonResult> GetPageAsync(MessageContentQueryModel qModel)
        {
            var resultTask = _messageContentSvc.GetPageDataAsync(qModel);
            return Json(await resultTask);
        }
    }
}