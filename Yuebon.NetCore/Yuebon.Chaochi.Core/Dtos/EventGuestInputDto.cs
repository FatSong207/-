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
    [AutoMap(typeof(EventGuest))]
    [Serializable]
    public class EventGuestInputDto: IInputDto<string>
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string EventId { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string GuestName { get; set; }

        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string GusetCell { get; set; }


    }
}
