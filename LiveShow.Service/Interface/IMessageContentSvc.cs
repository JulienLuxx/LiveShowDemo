using LiveShow.Core.Dto;
using LiveShow.Service.Dto;
using LiveShow.Service.QueryModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LiveShow.Service.Interface
{
    public interface IMessageContentSvc
    {
        Task<ResultDto> AddAsync(MessageContentAddDto dto);

        Task<ResultDto<MessageContentDto>> GetPageDataAsync(MessageContentQueryModel qModel);
    }
}
