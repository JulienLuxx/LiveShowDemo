using LiveShow.Core.Dto;
using LiveShow.Service.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LiveShow.Service.Interface
{
    public interface IUserSvc
    {
        ResultDto Add(UserDto dto);

        Task<ResultDto> ChangePasswordAsync(ChangePasswordDto dto);

        Task<ResultDto<LoginUserDto>> LoginAsync(LoginDto dto);

        Task<ResultDto> RegisterAsync(RegisterDto dto);
    }
}
