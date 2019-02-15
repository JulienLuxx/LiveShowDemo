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
    /// <summary>
    /// 房间管理
    /// </summary>
    [Produces("application/json")]
    [Route("API/ShowRoom")]
    public class ShowRoomController : Controller
    {
        private readonly IShowRoomSvc _showRoomSvc;
        public ShowRoomController(IShowRoomSvc showRoomSvc)
        {
            _showRoomSvc = showRoomSvc;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public JsonResult Add(ShowRoomDto dto)
        {
            var result = _showRoomSvc.Add(dto);
            return Json(result);
        }

        /// <summary>
        /// 启用
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("Activate")]
        public async Task<JsonResult> Activate(int id)
        {
            var resultTask = _showRoomSvc.Activate(id);
            return Json(await resultTask);
        }

        /// <summary>
        /// 获取页面列表
        /// </summary>
        /// <param name="qModel"></param>
        /// <returns></returns>
        [HttpGet("Page")]
        public async Task<JsonResult> GetPageAsync(ShowRoomQueryModel qModel)
        {
            var resultTask = _showRoomSvc.GetPageDataAsync(qModel);
            return Json(await resultTask);
        }

        /// <summary>
        /// 根据用户编号获取详情
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("DetailByUser")]
        public async Task<JsonResult> GetDetailByUserId(int userId)
        {
            var resultTask = _showRoomSvc.GetSingleDataByUserIdAsync(userId);
            return Json(await resultTask);
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("Shutdown")]
        public async Task<JsonResult> Shutdown(int id)
        {
            var resultTask = _showRoomSvc.Shutdown(id);
            return Json(await resultTask);
        }
    }
}