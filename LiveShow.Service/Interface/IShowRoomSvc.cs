using LiveShow.Core.Dto;
using LiveShow.Service.Dto;
using LiveShow.Service.QueryModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LiveShow.Service.Interface
{
    public interface IShowRoomSvc
    {
        ResultDto Add(ShowRoomDto dto);

        Task<ResultDto> Activate(int id);

        Task<ResultDto<ShowRoomDto>> GetPageDataAsync(ShowRoomQueryModel qModel);

        Task<ResultDto<ShowRoomDto>> GetSingleDataByUserIdAsync(int userId);

        Task<ResultDto> Shutdown(int id);
    }
}
