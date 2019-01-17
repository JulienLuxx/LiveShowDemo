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
    [Route("API/Role")]
    //[ApiExplorerSettings(IgnoreApi = true)]
    public class RoleController : Controller
    {
        private readonly IRoleSvc _roleSvc;
        public RoleController(IRoleSvc roleSvc)
        {
            _roleSvc = roleSvc;
        }

        [HttpPost("Add")]
        public JsonResult Add(RoleDto dto)
        {
            var result = _roleSvc.Add(dto);
            return Json(result);
        }

        [HttpPost("Edit")]
        public async Task<JsonResult> Edit(RoleDto dto)
        {
            var resultTask = _roleSvc.Edit(dto);
            return Json(await resultTask);
        }

        [HttpGet("Detail")]
        public async Task<JsonResult> GetDetail(int id)
        {
            var resultTask = _roleSvc.GetSingleDataAsync(id);
            return Json(await resultTask);
        }

        [HttpGet("Page")]
        public async Task<JsonResult> GetPageAsync(RoleQueryModel qModel)
        {
            var resultTask = _roleSvc.GetPageDataAsync(qModel);
            return Json(await resultTask);
        }
    }
}