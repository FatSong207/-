using System;
using Yuebon.Commons.IRepositories;
using Yuebon.Messages.Models;

namespace Yuebon.Messages.IRepositories
{
    /// <summary>
    /// 定義倉儲接口
    /// </summary>
    public interface IMemberMessageBoxRepository:IRepository<MemberMessageBox, string>
    {

        /// <summary>
        /// 更新已讀狀態
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool UpdateIsReadStatus(string id, int isread, string userid);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isread">2:全部，0：未讀，1：已讀</param>
        /// <param name="userid"></param>
        /// <returns></returns>
        int GetTotalCounts(int isread, string userid);
    }
}