using Yuebon.Commons.IRepositories;
using Yuebon.Chaochi.Models;

namespace Yuebon.Chaochi.IRepositories
{
    /// <summary>
    /// 定義業務拜訪記錄倉儲接口
    /// </summary>
    public interface IVisitingRecordRepository:IRepository<VisitingRecord, string>
    {
    }
}