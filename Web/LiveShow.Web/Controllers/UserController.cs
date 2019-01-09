﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiveShow.Service.Dto;
using LiveShow.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace LiveShow.Web.Controllers
{
    [Produces("application/json")]
    [Route("API/User")]
    public class UserController : Controller
    {
        private readonly IUserSvc _userSvc;
        public UserController(IUserSvc userSvc)
        {
            _userSvc = userSvc;
        }

        //[Authorize]
        [HttpPost("ChangePassword")]
        public async Task<JsonResult> ChangePassword([FromBody]ChangePasswordDto dto)
        {
            var resultTask = _userSvc.ChangePasswordAsync(dto);
            return Json(await resultTask);
        }

        [HttpPost("Register")]
        public async Task<JsonResult> Register(RegisterDto dto)
        {
            var resultTask = _userSvc.RegisterAsync(dto);
            return Json(await resultTask);
        }

        [HttpPost("Login")]
        public async Task<JsonResult> Login(LoginDto dto)
        {
            var resultTask = _userSvc.LoginAsync(dto);
            return Json(await resultTask);
        }
    }
}