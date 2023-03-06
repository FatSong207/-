using System;
using Yuebon.Commons.IRepositories;
using Yuebon.Chaochi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Yuebon.Chaochi.IRepositories
{
    /// <summary>
    /// 定義合約附件倉儲接口
    /// </summary>
    public interface IContractAttachmentRepository:IRepository<ContractAttachment, string>
    {
        ///// <summary>
        ///// 根據合約編號+合約版號查詢合約附件
        ///// 
        ///// </summary>
        ///// <param name="cid">合約編號</param>
        ///// <param name="version">合約版號</param>
        ///// <returns></returns>    
        //Task<List<ContractAttachment>> GetByVersion(string cid, string version);
    }
}