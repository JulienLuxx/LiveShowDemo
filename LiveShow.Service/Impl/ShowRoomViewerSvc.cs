using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LiveShow.Core.Dto;
using LiveShow.Domain;
using LiveShow.Domain.Entitis;
using LiveShow.Domain.Enum;
using LiveShow.Service.Dto;
using LiveShow.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace LiveShow.Service.Impl
{
    public class ShowRoomViewerSvc : BaseSvc, IShowRoomViewerSvc 
    {
        public ShowRoomViewerSvc(
            IMapper mapper, 
            LiveShowDBContext liveShowDB
            ) : base(mapper, liveShowDB)
        {
        }

        public async Task<ResultDto> Add(ShowRoomViewerDto dto)
        {
            var result = new ResultDto();
            dto.CreateTime = DateTime.Now;
            try
            {
                if (await _liveShowDB.ShowRoom.AsNoTracking().Where(x => (x.IsDeleted || (x.Status == ShowRoomStatusEnum.Ban.GetHashCode() || x.Status == ShowRoomStatusEnum.Disable.GetHashCode())) && x.Id == dto.UserId).AnyAsync())
                {
                    return result;
                }
                if (await _liveShowDB.ShowRoom.AsNoTracking().Include(x => x.ShowRoomVlewers).Where(x => x.ShowRoomVlewers.Count >= 10000 && x.Id == dto.ShowRoomId).AnyAsync())
                {
                    result.Message = "Over Size";
                    return result;
                }
                var data = await _liveShowDB.ShowRoomViewer.Where(x => x.UserId == dto.UserId).FirstOrDefaultAsync();
                if (null == data)
                {
                    //if (!await _liveShowDB.ShowRoomViewer.AsNoTracking().Where(x => x.UserId == dto.UserId && x.ShowRoomId == dto.ShowRoomId).AnyAsync())
                    _liveShowDB.ShowRoomViewer.Add(new ShowRoomVlewer()
                    {
                        ShowRoomId = dto.ShowRoomId,
                        UserId = dto.UserId
                    });
                }
                else if (data.ShowRoomId != dto.ShowRoomId) 
                {
                    _liveShowDB.Remove(data);
                    _liveShowDB.ShowRoomViewer.Add(new ShowRoomVlewer()
                    {
                        ShowRoomId = dto.ShowRoomId,
                        UserId = dto.UserId
                    });
                    //data.ShowRoomId = dto.ShowRoomId;
                    //_liveShowDB.ShowRoomViewer.Update(data);
                }
                var flag = _liveShowDB.SaveChanges();
                //if (flag > 0)
                //{
                result.ActionResult = true;
                result.Message = "Success";
                //}
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<ResultDto> Remove(ShowRoomViewerDto dto)
        {
            var result = new ResultDto();
            try
            {
                var data = await _liveShowDB.ShowRoomViewer.AsNoTracking().Where(x => x.UserId == dto.UserId && x.ShowRoomId == dto.ShowRoomId).FirstOrDefaultAsync();
                if (null == data)
                {
                    return result;
                }
                _liveShowDB.ShowRoomViewer.Remove(data);
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

        public async Task<ResultDto<ShowRoomViewerDto>> GetList()
        {
            var result = new ResultDto<ShowRoomViewerDto>();
            try
            {
                var dataList = await _liveShowDB.ShowRoomViewer.AsNoTracking().ToListAsync();
                var dtoList = _mapper.Map<List<ShowRoomViewerDto>>(dataList);
                result.ActionResult = true;
                result.Message = "Success";
                result.List = dtoList;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
