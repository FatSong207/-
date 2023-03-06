using System;
using Yuebon.Commons.IServices;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;

namespace Yuebon.Chaochi.IServices
{
    /// <summary>
    /// 定義服務接口
    /// </summary>
    public interface IComplaintNoticeMailService:IService<ComplaintNoticeMail,ComplaintNoticeMailOutputDto, string>
    {
    }
}
