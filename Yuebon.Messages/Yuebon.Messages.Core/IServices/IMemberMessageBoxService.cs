using System;
using Yuebon.Commons.IServices;
using Yuebon.Messages.Dtos;
using Yuebon.Messages.Models;

namespace Yuebon.Messages.IServices
{
    /// <summary>
    /// 定義服務接口
    /// </summary>
    public interface IMemberMessageBoxService:IService<MemberMessageBox,MemberMessageBoxOutputDto, string>
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
