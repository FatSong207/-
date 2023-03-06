using Yuebon.Commons.IServices;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;
using System.Threading.Tasks;
using System.Data;

namespace Yuebon.Chaochi.IServices
{
    /// <summary>
    /// 定義業務拜訪記錄服務接口
    /// </summary>
    public interface IVisitingRecordService:IService<VisitingRecord,VisitingRecordOutputDto, string>
    {
        /// <summary>
        /// 根據潛在客編號查詢最新訪談記錄
        /// </summary>
        /// <param name="pid">潛在客編號</param>
        /// <param name="conn">DB連線</param>
        /// <param name="trans">DB交易</param>
        /// <returns></returns>
        Task<VisitingRecordOutputDto> GetLatestByPID(string pid, IDbConnection conn = null, IDbTransaction trans = null);
    }
}
