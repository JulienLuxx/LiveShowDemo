using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiveShow.Service.Dto;
using LiveShow.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace LiveShow.MessageCenter.Controllers
{
    [Produces("application/json")]
    [Route("API/MessageCategory")]
    public class MessageCategoryController : Controller
    {
        private readonly IMessageCategorySvc _messageCategorySvc;
        public MessageCategoryController(IMessageCategorySvc messageCategorySvc)
        {
            _messageCategorySvc = messageCategorySvc;
        }

        [HttpPost("Add")]
        public async Task<JsonResult> AddAsync(MessageCategoryDto dto)
        {
            var resultTask = _messageCategorySvc.AddAsync(dto);
            return Json(await resultTask);
        }
    }
}