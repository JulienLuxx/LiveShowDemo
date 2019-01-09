using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiveShow.Service.Dto;
using LiveShow.Service.Interface;
using LiveShow.Service.QueryModel;
using Microsoft.AspNetCore.Mvc;

namespace LiveShow.Web.Controllers
{
    [Produces("application/json")]
    [Route("API/ShowRoom")]
    public class ShowRoomController : Controller
    {
        private readonly IShowRoomSvc _showRoomSvc;
        public ShowRoomController(IShowRoomSvc showRoomSvc)
        {
            _showRoomSvc = showRoomSvc;
        }

        [HttpPost("Add")]
        public JsonResult Add(ShowRoomDto dto)
        {
            var result = _showRoomSvc.Add(dto);
            return Json(result);
        }

        [HttpPost("Activate")]
        public async Task<JsonResult> Activate(int id)
        {
            var resultTask = _showRoomSvc.Activate(id);
            return Json(await resultTask);
        }

        [HttpGet("Page")]
        public async Task<JsonResult> GetPageAsync(ShowRoomQueryModel qModel)
        {
            var resultTask = _showRoomSvc.GetPageDataAsync(qModel);
            return Json(await resultTask);
        }

        [HttpGet("DetailByUser")]
        public async Task<JsonResult> GetDetailByUserId(int userId)
        {
            var resultTask = _showRoomSvc.GetSingleDataByUserIdAsync(userId);
            return Json(await resultTask);
        }

        [HttpPost("Shutdown")]
        public async Task<JsonResult> Shutdown(int id)
        {
            var resultTask = _showRoomSvc.Shutdown(id);
            return Json(await resultTask);
        }
    }
}