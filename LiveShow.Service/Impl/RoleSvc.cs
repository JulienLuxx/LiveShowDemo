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
    public class RoleSvc : BaseSvc, IRoleSvc 
    {
        public RoleSvc(
            IMapper mapper, 
            LiveShowDBContext liveShowDB
            ) : base(mapper, liveShowDB)
        {
        }

        public ResultDto Add(RoleDto dto)
        {
            var result = new ResultDto();
            try
            {
                dto.SetDefaultValue();
                var data = _mapper.Map<Role>(dto);
                _liveShowDB.Add(data);
                var flag = _liveShowDB.SaveChanges();
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

        public async Task<ResultDto> Edit(RoleDto dto)
        {
            var result = new ResultDto();
            try
            {
                var data = await _liveShowDB.Role.Where(x => !x.IsDeleted && x.Id == dto.Id).FirstOrDefaultAsync();
                data.Name = dto.Name;
                data.Status = dto.Status;
                _liveShowDB.Role.Update(data);
                var flag = _liveShowDB.SaveChanges();
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

        public async Task<ResultDto<RoleDto>> GetPageDataAsync(RoleQueryModel qModel)
        {
            var result = new ResultDto<RoleDto>();
            try
            {
                var query = _liveShowDB.Role.AsNoTracking().Where(x => !x.IsDeleted);
                var queryData = query.Select(s => new RoleDto()
                {
                    Id = s.Id,
                    Name = s.Name,
                    Status = s.Status,
                    CreateTime=s.CreateTime
                });
                queryData = queryData.OrderBy(o => o.Id);
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

        public async Task<ResultDto<RoleDto>> GetSingleDataAsync(int id)
        {
            var result = new ResultDto<RoleDto>();
            try
            {
                var data = await _liveShowDB.Role.AsNoTracking().Where(x => !x.IsDeleted && x.Id == id).FirstOrDefaultAsync();
                if (null != data)
                {
                    var dto = _mapper.Map<RoleDto>(data);
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
