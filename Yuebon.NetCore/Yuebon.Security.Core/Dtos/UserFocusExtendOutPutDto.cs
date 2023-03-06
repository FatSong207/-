using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Yuebon.Commons.Models;
using Yuebon.Commons.Mapping;
using Yuebon.Security.Models;
using Yuebon.Commons.Dtos;

namespace Yuebon.Security.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMap(typeof(User))]
    [Serializable]
    public class UserFocusExtendOutPutDto : IOutputDto
    {

        #region Property Members

        /// <summary>
        /// 用戶主鍵
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 關注的用戶ID
        /// </summary>
        public virtual string FocusUserId { get; set; }

        /// <summary>
        /// 關注人
        /// </summary>
        public virtual string CreatorUserId { get; set; }

        /// <summary>
        /// 關注時間
        /// </summary>
        public virtual DateTime? CreatorTime { get; set; }

        /// <summary>
        /// 關注的用戶昵稱
        /// </summary>
        public virtual string FUserNickName { get; set; }

        /// <summary>
        /// 關注的用戶頭像
        /// </summary>
        public virtual string FUserHeadIcon { get; set; }

        /// <summary>
        /// 關注的用戶手機
        /// </summary>
        public virtual string FUserMobilePhone { get; set; }

        /// <summary>
        /// 關注的用戶資料開放程序
        /// </summary>
        public virtual string FUserOpenType { get; set; }

        /// <summary>
        /// 記錄數
        /// </summary>
        public virtual int RecordCount { get; set; }


        /// <summary>
        /// 關注時間
        /// </summary>
        public virtual string ShowAddTime { get; set; }
        #endregion
    }
}
