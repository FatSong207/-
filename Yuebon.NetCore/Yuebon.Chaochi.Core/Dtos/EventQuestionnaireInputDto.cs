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
    [AutoMap(typeof(EventQuestionnaire))]
    [Serializable]
    public class EventQuestionnaireInputDto: IInputDto<string>
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 活動Id，用來查詢是否有存在於QuestTopic
        /// </summary>

        public string EId { get; set; }

        #region 唯讀不可更新之欄位

        ///// <summary>
        ///// 設置或獲取活動Id
        ///// </summary>
        //public string EventId { get; set; }

        ///// <summary>
        ///// 設置或獲取問卷類型
        ///// </summary>
        //public string QType { get; set; }

        ///// <summary>
        ///// 設置或獲取問卷代號
        ///// </summary>
        //public string QCode { get; set; }

        ///// <summary>
        ///// 設置或獲取問卷連結
        ///// </summary>
        //public string QUrl { get; set; }

        ///// <summary>
        ///// 設置或獲取所屬活動
        ///// </summary>
        //public string EventName { get; set; }

        ///// <summary>
        ///// 設置或獲取所屬公司
        ///// </summary>
        //public string BelongCompany { get; set; }

        #endregion

        /// <summary>
        /// 設置或獲取調查問卷名稱
        /// </summary>
        public string QName { get; set; }
        /// <summary>
        /// 設置或獲取問卷開頭語
        /// </summary>
        public string QBegingWords { get; set; }
        /// <summary>
        /// 設置或獲取問卷結尾語
        /// </summary>
        public string QEndWords { get; set; }
        /// <summary>
        /// 設置或獲取當前問卷題數
        /// </summary>
        public string QuestionCount { get; set; }
        /// <summary>
        /// 設置或獲取當前問卷定義檔
        /// </summary>
        public string QFileName { get; set; }

    }
}
