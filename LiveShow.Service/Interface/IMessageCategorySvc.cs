using LiveShow.Core.Dto;
using LiveShow.Service.Dto;
using LiveShow.Service.QueryModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LiveShow.Service.Interface
{
    public interface IMessageCategorySvc
    {
        Task<ResultDto> AddAsync(MessageCategoryDto dto);

        Task<ResultDto> EditAsync(MessageCategoryDto dto);

        Task<ResultDto<MessageCategoryDto>> GetPageDataAsync(MessageCategoryQueryModel qModel);

        Task<ResultDto<MessageCategoryDto>> GetSingleDataAsync(int id);
    }
}
