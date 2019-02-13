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
using LiveShow.Service.QueryModel;
using Microsoft.EntityFrameworkCore;

namespace LiveShow.Service.Impl
{
    public class ShowRoomSvc : BaseSvc, IShowRoomSvc 
    {
        public ShowRoomSvc(
            LiveShowDBContext liveShowDB
            ) : base( liveShowDB)
        {
        }

        public ResultDto Add(ShowRoomDto dto)
        {
            var result = new ResultDto();
            try
            {
                dto.SetDefaultValue();
                var data = Mapper.Map<ShowRoom>(dto);
                data.Disable();
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

        public async Task<ResultDto> AddViewer(ShowRoomViewerDto dto)
        {
            var result = new ResultDto();
            try
            {
                if (await _liveShowDB.ShowRoom.AsNoTracking().Where(x => (x.IsDeleted || (x.Status == ShowRoomStatusEnum.Ban.GetHashCode() || x.Status == ShowRoomStatusEnum.Disable.GetHashCode()))&&x.Id==dto.UserId).AnyAsync())
                {
                    return result;
                }
                if (await _liveShowDB.ShowRoom.AsNoTracking().Include(x => x.ShowRoomVlewers).Where(x => x.ShowRoomVlewers.Count >= 10000 && x.Id == dto.ShowRoomId).AnyAsync())
                {
                    result.Message = "Over Size";
                    return result;
                }
                var viewerData = await _liveShowDB.ShowRoomViewer.Where(x => x.UserId == dto.UserId).FirstOrDefaultAsync();
                if (null==viewerData)
                {
                    if(await _liveShowDB.ShowRoomViewer.AsNoTracking().Where(x=>x.UserId==dto.UserId&&x.ShowRoomId==dto.ShowRoomId).AnyAsync())
                    _liveShowDB.ShowRoomViewer.Add(new ShowRoomVlewer()
                    {
                        ShowRoomId = dto.ShowRoomId,
                        UserId = dto.UserId
                    });
                }
                else
                {
                    viewerData.ShowRoomId = dto.ShowRoomId;
                    _liveShowDB.ShowRoomViewer.Update(viewerData);
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

        public async Task<ResultDto> RemoveViewer(ShowRoomViewerDto dto)
        {
            var result = new ResultDto();
            try
            {
                var viewData = await _liveShowDB.ShowRoomViewer.AsNoTracking().Where(x => x.UserId == dto.UserId && x.ShowRoomId == dto.ShowRoomId).FirstOrDefaultAsync();
                if (null == viewData)
                {
                    return result;
                }
                _liveShowDB.ShowRoomViewer.Remove(viewData);
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

        /// <summary>
        /// 启用房间
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ResultDto> Activate(int id)
        {
            var result = new ResultDto();
            try
            {
                var data = await _liveShowDB.ShowRoom.Where(x => !x.IsDeleted && x.Id == id && x.Status == ShowRoomStatusEnum.Disable.GetHashCode()).FirstOrDefaultAsync();
                if (null == data)
                {
                    result.Message = "Not Found";
                    return result;
                }
                if (ShowRoomStatusEnum.Ban.GetHashCode() == data.Status)
                {
                    result.Message = "This Room has been ban";
                    return result;
                }
                data.Activate();
                var flag= _liveShowDB.SaveChanges();
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

        /// <summary>
        /// 获取页面数据
        /// </summary>
        /// <param name="qModel"></param>
        /// <returns></returns>
        public async Task<ResultDto<ShowRoomDto>> GetPageDataAsync(ShowRoomQueryModel qModel)
        {
            var result = new ResultDto<ShowRoomDto>();
            var query = _liveShowDB.ShowRoom.AsNoTracking()/*.Include(x => x.User)*/.Where(x => !x.IsDeleted);
            try
            {
                query = qModel.Status.HasValue && ShowRoomStatusEnum.Default.GetHashCode() != qModel.Status ? query.Where(x => x.Status == qModel.Status) : query;
                var queryData = query.Select(s => new ShowRoomDto()
                {
                    Id = s.Id,
                    Name = s.Name,
                    Title = s.Title,
                    Status = s.Status,
                    //StatusName= 
                });
                queryData = queryData.OrderBy(o => o.LastActivateTime);
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

        /// <summary>
        /// 根据用户编号取单条数据
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ResultDto<ShowRoomDto>> GetSingleDataByUserIdAsync(int userId)
        {
            var result = new ResultDto<ShowRoomDto>();
            try
            {
                var data = await _liveShowDB.ShowRoom.Include(x => x.ShowRoomVlewers).Where(x => !x.IsDeleted && x.ShowRoomVlewers.Where(y => y.UserId == userId).Any()).FirstOrDefaultAsync();
                var dto = Mapper.Map<ShowRoomDto>(data);
                result.ActionResult = true;
                result.Message = "Success";
                result.Data = dto;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 关闭房间
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ResultDto> Shutdown(int id)
        {
            var result = new ResultDto();
            try
            {
                var data = await _liveShowDB.ShowRoom.Where(x => !x.IsDeleted && x.Id == id && x.Status == ShowRoomStatusEnum.Activate.GetHashCode()).Include(x => x.ShowRoomVlewers).FirstOrDefaultAsync();
                if (null == data)
                {
                    result.Message = "Not Found";
                    return result;
                }
                if (ShowRoomStatusEnum.Ban.GetHashCode() == data.Status)
                {
                    result.Message = "This Room has been ban";
                    return result;
                }
                data.Disable();
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


    }
}
