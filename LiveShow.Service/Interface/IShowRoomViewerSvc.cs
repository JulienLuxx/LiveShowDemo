using LiveShow.Core.Dto;
using LiveShow.Service.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LiveShow.Service.Interface
{
    public interface IShowRoomViewerSvc
    {
        Task<ResultDto> Add(ShowRoomViewerDto dto);

        Task<ResultDto> Remove(ShowRoomViewerDto dto);
    }
}
