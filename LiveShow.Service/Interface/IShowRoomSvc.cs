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

        /// <summary>
        /// 启用房间
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ResultDto> Activate(int id);

        /// <summary>
        /// 获取页面数据
        /// </summary>
        /// <param name="qModel"></param>
        /// <returns></returns>
        Task<ResultDto<ShowRoomDto>> GetPageDataAsync(ShowRoomQueryModel qModel);

        /// <summary>
        /// 根据用户编号取单条数据
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<ResultDto<ShowRoomDto>> GetSingleDataByUserIdAsync(int userId);

        /// <summary>
        /// 关闭房间
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ResultDto> Shutdown(int id);
    }
}
