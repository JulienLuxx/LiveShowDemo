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
using LiveShow.Service.QueryModel;
using Microsoft.EntityFrameworkCore;

namespace LiveShow.Service.Impl
{
    public class MessageContentSvc : BaseSvc, IMessageContentSvc 
    {
        public MessageContentSvc(LiveShowDBContext liveShowDB) : base(liveShowDB)
        {
        }

        public async Task<ResultDto> AddAsync(MessageContentAddDto dto)
        {
            var result = new ResultDto();
            try
            {
                dto.SetDefaultValue();
                var data = Mapper.Map<MessageContent>(dto);
                await _liveShowDB.MessageContent.AddAsync(data);
                var flag = await _liveShowDB.SaveChangesAsync();
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

        public async Task<ResultDto<MessageContentDto>> GetPageDataAsync(MessageContentQueryModel qModel)
        {
            var result = new ResultDto<MessageContentDto>();
            try
            {
                var query = _liveShowDB.MessageContent.AsNoTracking().Where(x => !x.IsDeleted);
                var queryData = query.Select(s => new MessageContentDto()
                {
                    Id = s.Id,
                    CategoryId = s.CategoryId,
                    Title = s.Title,
                    SenderId = s.SenderId,
                    SenderName = s.SenderName,
                    SendTime = s.SendTime,
                    Content = s.Content,
                    ReceiveObjectType = s.ReceiveObjectType,
                    ReceiveObjectIds = s.ReceiveObjectIds,
                    Status = s.Status,
                    ErrorMessage = s.ErrorMessage
                });
                queryData = queryData.OrderByDescending(o => o.CreateTime);
                queryData = queryData.Skip((qModel.Page - 1) * qModel.PageSize).Take(qModel.PageSize);
                result.ActionResult = true;
                result.Message = "Success";
                result.List = await queryData.ToListAsync();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
