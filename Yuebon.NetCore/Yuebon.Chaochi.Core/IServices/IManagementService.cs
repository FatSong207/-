using System;
using Yuebon.Commons.IServices;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Yuebon.Chaochi.IServices
{
    /// <summary>
    /// 定義管理方服務接口
    /// </summary>
    public interface IManagementService:IService<Management,ManagementOutputDto, string>
    {
        /// <summary>
        /// 根據統一編號查詢管理方
        /// </summary>
        /// <param name="maid">統一編號</param>
        /// <returns></returns>
        Task<ManagementOutputDto> GetById(string maid);
    }
}
