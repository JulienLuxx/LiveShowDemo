using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LiveShow.Core.Dto;
using LiveShow.Domain;
using LiveShow.Domain.Entitis;
using LiveShow.Service.Dto;
using LiveShow.Service.Interface;

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
    }
}
