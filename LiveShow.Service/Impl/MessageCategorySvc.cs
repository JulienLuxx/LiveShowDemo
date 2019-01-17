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
    public class MessageCategorySvc : BaseSvc, IMessageCategorySvc 
    {
        public MessageCategorySvc(LiveShowDBContext liveShowDB) : base(liveShowDB)
        {
        }

        public async Task<ResultDto> AddAsync(MessageCategoryDto dto)
        {
            var result = new ResultDto();
            try
            {
                dto.SetDefaultValue();
                var data = Mapper.Map<MessageCategory>(dto);
                await _liveShowDB.MessageCategory.AddAsync(data);
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

        public async Task<ResultDto> EditAsync(MessageCategoryDto dto)
        {
            var result = new ResultDto();
            try
            {
                var data = await _liveShowDB.MessageCategory.Where(x => !x.IsDeleted && x.Id == dto.Id).FirstOrDefaultAsync();
                data.Name = dto.Name;
                data.Sort = dto.Sort;
                _liveShowDB.MessageCategory.Update(data);
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

        public async Task<ResultDto<MessageCategoryDto>> GetPageDataAsync(MessageCategoryQueryModel qModel)
        {
            var result = new ResultDto<MessageCategoryDto>();
            try
            {
                var query = _liveShowDB.MessageCategory.AsNoTracking().Where(x => !x.IsDeleted);
                var queryData = query.Select(s => new MessageCategoryDto()
                {
                    Id=s.Id,
                    Name=s.Name,
                    Sort=s.Sort,
                    CreateTime=s.CreateTime,
                });
                queryData = queryData.OrderBy(o => o.Sort);
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

        public async Task<ResultDto<MessageCategoryDto>> GetSingleDataAsync(int id)
        {
            var result = new ResultDto<MessageCategoryDto>();
            try
            {
                var data = await _liveShowDB.MessageCategory.AsNoTracking().Where(x => !x.IsDeleted && x.Id == id).FirstOrDefaultAsync();
                if (null != data)
                {
                    var dto = Mapper.Map<MessageCategoryDto>(data);
                    result.ActionResult = true;
                    result.Message = "Success";
                    result.Data = dto;
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
