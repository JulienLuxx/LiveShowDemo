using LiveShow.Core.Dto;
using LiveShow.Service.Dto;
using LiveShow.Service.QueryModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LiveShow.Service.Interface
{
    public interface IRoleSvc
    {
        ResultDto Add(RoleDto dto);

        Task<ResultDto> Edit(RoleDto dto);

        Task<ResultDto<RoleDto>> GetPageDataAsync(RoleQueryModel qModel);

        Task<ResultDto<RoleDto>> GetSingleDataAsync(int id);
    }
}
