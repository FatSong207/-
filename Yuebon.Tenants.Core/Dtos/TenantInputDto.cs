using AutoMapper;
using System;
using Yuebon.Commons.Dtos;
using Yuebon.Tenants.Models;

namespace Yuebon.Tenants.Dtos
{
    /// <summary>
    /// 租戶輸入對象模型
    /// </summary>
    [AutoMap(typeof(Tenant))]
    [Serializable]
    public class TenantInputDto: IInputDto<string>
    {
        /// <summary>
        /// 設置或獲取 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 設置或獲取租戶名稱
        /// </summary>
        public string TenantName { get; set; }

        /// <summary>
        /// 設置或獲取公司名稱
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 設置或獲取訪問域名
        /// </summary>
        public string HostDomain { get; set; }

        /// <summary>
        /// 設置或獲取聯系人
        /// </summary>
        public string LinkMan { get; set; }

        /// <summary>
        /// 設置或獲取聯系電話
        /// </summary>
        public string Telphone { get; set; }

        /// <summary>
        /// 設置或獲取數據源，分庫使用
        /// </summary>
        public string DataSource { get; set; }

        /// <summary>
        /// 設置或獲取租戶介紹
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 設置或獲取是否可用
        /// </summary>
        public bool EnabledMark { get; set; }


    }
}
