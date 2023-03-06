using Yuebon.Commons.Enums;
using Yuebon.Commons.IServices;
using Yuebon.Quartz.Dtos;
using Yuebon.Quartz.Models;
using Yuebon.Security.Dtos;
using Yuebon.Security.Models;

namespace Yuebon.Quartz.IServices
{
    /// <summary>
    /// 定義定時任務服務接口
    /// </summary>
    public interface ITaskManagerService:IService<TaskManager,TaskManagerOutputDto, string>
    {
        /// <summary>
        /// 記錄任務運行結果
        /// </summary>
        /// <param name="jobId">任務Id</param>
        /// <param name="jobAction">任務執行動作</param>
        /// <param name="blresultTag">任務執行結果表示，true成功，false失敗，初始執行為true</param>
        /// <param name="msg">任務記錄描述</param>
        void RecordRun(string jobId, JobAction jobAction, bool blresultTag = true, string msg = "");

    }
}
