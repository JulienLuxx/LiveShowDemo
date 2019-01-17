using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LiveShow.Core.Dto;
using LiveShow.Domain;
using LiveShow.Domain.Entitis;
using LiveShow.Service.Dto;
using LiveShow.Service.Interface;
using Microsoft.EntityFrameworkCore;
using LiveShow.Core.Encrypt;

namespace LiveShow.Service.Impl
{
    public class UserSvc : BaseSvc, IUserSvc 
    {
        private IEncryptUtil _encryptUtil { get; set; }
        public UserSvc(
            LiveShowDBContext liveShowDB,
            IEncryptUtil encryptUtil
            ) : base( liveShowDB)
        {
            _encryptUtil = encryptUtil;
        }

        public ResultDto Add(UserDto dto)
        {
            var result = new ResultDto();
            try
            {
                dto.SetDefaultValue();
                var randomStr = new Random().Next(100000).ToString();
                dto.Password = _encryptUtil.GetMd5By32(dto.Password + randomStr);
                var data = Mapper.Map<User>(dto);
                data.SaltValue = randomStr;
                _liveShowDB.Add(data);
                var flag = _liveShowDB.SaveChanges();
                if (flag > 0)
                {
                    result.ActionResult = true;
                    result.Message = "Success";
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<ResultDto> ChangePasswordAsync(ChangePasswordDto dto)
        {
            var result = new ResultDto();
            try
            {
                if (!dto.NewPassword.Equals(dto.ConfirmPassword))
                {
                    result.Message = "UnConfirm";
                    return result;
                }
                var data = await _liveShowDB.User.FindAsync(dto.Id);
                if (null != data)
                {
                    dto.OrigPassword = _encryptUtil.GetMd5By32(dto.OrigPassword + data.SaltValue);
                    if (string.IsNullOrEmpty(data.Password))
                    {
                        data.Password = _encryptUtil.GetMd5By32(dto.NewPassword + data.SaltValue);
                    }
                    else
                    {
                        if (!dto.OrigPassword.Equals(data.Password))
                        {
                            result.Message = "OrigPassword error";
                            return result;
                        }
                        else
                        {
                            data.Password = _encryptUtil.GetMd5By32(dto.NewPassword + data.SaltValue);
                        }
                    }


                    var flag = _liveShowDB.SaveChanges();
                    if (flag > 0)
                    {
                        result.ActionResult = true;
                        result.Message = "Success";
                    }

                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<ResultDto<LoginUserDto>> LoginAsync(LoginDto dto)
        {
            var result = new ResultDto<LoginUserDto>();
            try
            {
                var data = await _liveShowDB.User.AsNoTracking().Where(x => x.Name.Equals(dto.UserName) || x.Mobile.Equals(dto.UserName)).Select(s => new LoginUserDto()
                {
                    Id = s.Id,
                    Name = s.Name,
                    Password = s.Password,
                    Status = s.Status,
                    SaltValue = s.SaltValue
                }).FirstOrDefaultAsync();

                if (null == data)
                {
                    result.Message = "User does not exist";
                    return result;
                }
                else if (!data.Password.Equals(_encryptUtil.GetMd5By32(dto.Password + data.SaltValue)))
                {
                    result.Message = "UserNameOrPassword error";
                    return result;
                }

                result.ActionResult = true;
                result.Message = "success";
                result.Data = data;
                return result;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<ResultDto> RegisterAsync(RegisterDto dto)
        {
            var result = new ResultDto();
            try
            {
                var mobileTask = _liveShowDB.User.AsNoTracking().Where(x => x.Mobile.Equals(dto.Mobile)).AnyAsync();
                var mailBoxTask = _liveShowDB.User.AsNoTracking().Where(x => x.MailBox.Equals(dto.MailBox)).AnyAsync();
                if (await mobileTask)
                {
                    result.Message = "MobileNumber already exist!";
                    return result;
                }
                if (await mailBoxTask)
                {
                    result.Message = "MailBox already exist!";
                    return result;
                }
                dto.RoleId =await _liveShowDB.Role.Where(x => !x.IsDeleted && x.Name.Equals("user")).Select(s => s.Id).FirstOrDefaultAsync();
                var userDto = Mapper.Map<UserDto>(dto);
                result = Add(userDto);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
