using AutoMapper;
using System;
using Yuebon.Commons.Dtos;
using Yuebon.Chaochi.Models;

namespace Yuebon.Chaochi.Dtos
{
    /// <summary>
    /// 輸入對象模型
    /// </summary>
    [AutoMap(typeof(MOBuilding))]
    [Serializable]
    public class MOBuildingInputDto: IInputDto<string>
    {
        /// <summary>
        /// 設置或獲取主鍵值
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 設置或獲取多物件單編號
        /// </summary>
        public string MOID { get; set; }
        /// <summary>
        /// 設置或獲取物件完整地址
        /// </summary>
        public string BAdd { get; set; }
        /// <summary>
        /// 設置或獲取物件地址_縣/市
        /// </summary>
        public string BAdd_1 { get; set; }
        /// <summary>
        /// 設置或獲取物件地址_縣
        /// </summary>
        public string BAdd_1_1 { get; set; }
        /// <summary>
        /// 設置或獲取物件地址_市
        /// </summary>
        public string BAdd_1_2 { get; set; }
        /// <summary>
        /// 設置或獲取物件地址_鄉/鎮/市/區
        /// </summary>
        public string BAdd_2 { get; set; }
        /// <summary>
        /// 設置或獲取物件地址_鄉
        /// </summary>
        public string BAdd_2_1 { get; set; }
        /// <summary>
        /// 設置或獲取物件地址_鎮
        /// </summary>
        public string BAdd_2_2 { get; set; }
        /// <summary>
        /// 設置或獲取物件地址_市
        /// </summary>
        public string BAdd_2_3 { get; set; }
        /// <summary>
        /// 設置或獲取物件地址_區
        /// </summary>
        public string BAdd_2_4 { get; set; }
        /// <summary>
        /// 設置或獲取物件地址_街/路
        /// </summary>
        public string BAdd_3 { get; set; }
        /// <summary>
        /// 設置或獲取物件地址_街
        /// </summary>
        public string BAdd_3_1 { get; set; }
        /// <summary>
        /// 設置或獲取物件地址_路
        /// </summary>
        public string BAdd_3_2 { get; set; }
        /// <summary>
        /// 設置或獲取物件地址_段
        /// </summary>
        public string BAdd_4 { get; set; }
        /// <summary>
        /// 設置或獲取物件地址_巷
        /// </summary>
        public string BAdd_5 { get; set; }
        /// <summary>
        /// 設置或獲取物件地址_弄
        /// </summary>
        public string BAdd_6 { get; set; }
        /// <summary>
        /// 設置或獲取物件地址_號
        /// </summary>
        public string BAdd_7 { get; set; }
        /// <summary>
        /// 設置或獲取物件地址_樓
        /// </summary>
        public string BAdd_8 { get; set; }
        /// <summary>
        /// 設置或獲取物件地址_樓
        /// </summary>
        public string BAdd_9 { get; set; }

        /// <summary>
        /// 設置或獲取建物屬性
        /// </summary>
        public string BPropoty { get; set; }
        public string LSID { get; set; }
    }
}
