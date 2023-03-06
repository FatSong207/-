using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Yuebon.Commons.Models;
using Yuebon.Commons.Dtos;
using Yuebon.Chaochi.Models;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 輸入對象模型
    /// </summary>
    [AutoMap(typeof(ComplaintNoticeMail))]
    [Serializable]
    public class ComplaintNoticeMailInputDto: IInputDto<string>
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 設置或獲取投訴單編號
        /// </summary>
        public string CCode { get; set; }


        /// <summary>
        /// 設置或獲取UserId
        /// </summary>
        public string UserId { get; set; }
    }
}
