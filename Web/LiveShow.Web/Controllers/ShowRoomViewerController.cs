using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiveShow.Service.Dto;
using LiveShow.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace LiveShow.Web.Controllers
{
    [Produces("application/json")]
    [Route("API/ShowRoomViewer")]
    public class ShowRoomViewerController : Controller
    {
        private readonly IShowRoomViewerSvc _showRoomViewerSvc;
        public ShowRoomViewerController(IShowRoomViewerSvc showRoomViewerSvc)
        {
            _showRoomViewerSvc = showRoomViewerSvc;
        }

        [HttpPost("Add")]
        public async Task<JsonResult> Add(ShowRoomViewerDto dto)
        {
            var resultTask = _showRoomViewerSvc.Add(dto);
            return Json(await resultTask);
        }

        [HttpPost("Remove")]
        public async Task<JsonResult> Remove(ShowRoomViewerDto dto)
        {
            var resultTask = _showRoomViewerSvc.Remove(dto);
            return Json(await resultTask);
        }

        [HttpPost("List")]
        public async Task<JsonResult> GetList()
        {
            var resultTask = _showRoomViewerSvc.GetList();
            return Json(await resultTask);
        }
    }
}