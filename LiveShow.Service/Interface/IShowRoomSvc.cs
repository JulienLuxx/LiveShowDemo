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

        Task<ResultDto> AddViewer(ShowRoomViewerDto dto);

        Task<ResultDto> RemoveViewer(ShowRoomViewerDto dto);

        Task<ResultDto> Activate(int userId);

        Task<ResultDto<ShowRoomDto>> GetPageDataAsync(ShowRoomQueryModel qModel);

        Task<ResultDto> Shutdown(int userId);
    }
}
