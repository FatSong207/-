using Yuebon.Commons.IServices;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;

namespace Yuebon.Chaochi.IServices
{
    /// <summary>
    /// 定義合約附件服務接口
    /// </summary>
    public interface IContractAttachmentService:IService<ContractAttachment,ContractAttachmentOutputDto, string>
    {
        /// <summary>
        /// 根據合約編號查詢合約必要附件
        /// 
        /// </summary>
        /// <param name="cid">合約編號</param>
        /// <returns></returns>    
        Task<List<ContractAttachmentOutputDto>> GetMajorByCID(string cid);

        /// <summary>
        /// 根據合約編號查詢合約其他附件
        /// 
        /// </summary>
        /// <param name="cid">合約編號</param>
        /// <returns></returns>    
        Task<List<ContractAttachmentOutputDto>> GetMinorByCID(string cid);

        /// <summary>
        /// 根據合約必要附件主鍵值查詢合約必要附件狀態
        /// 
        /// </summary>
        /// <param name="id">合約必要附件主鍵值</param>
        /// <returns></returns>   
        Task<string> GetStatusById(string id);

        /// <summary>
        /// 更新主約附件狀態
        /// 
        /// </summary>
        /// <param name="userId">使用者Id</param>
        /// <param name="cid">合約編號</param>
        /// <param name="cname">表單名稱</param>
        /// <param name="status">附件狀態</param>
        /// <returns></returns> 
        Task<bool> UpdateContractAttachmentStatus(string userId, string cid, string cname, string status, IDbConnection conn = null, IDbTransaction trans = null);
    }
}
