using System;
using System.Collections.Generic;
using Yuebon.Commons.IServices;
using Yuebon.Security.Dtos;
using Yuebon.Security.Models;

namespace Yuebon.Security.IServices
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAreaService:IService<Area, AreaOutputDto, string>
    {

        #region 用于uniapp下拉選項
        /// <summary>
        /// 獲取所有可用的地區，用于uniapp下拉選項
        /// </summary>
        /// <returns></returns>
        List<AreaPickerOutputDto> GetAllByEnable();
        /// <summary>
        /// 獲取省、市、縣/區三級可用的地區，用于uniapp下拉選項
        /// </summary>
        /// <returns></returns>
        List<AreaPickerOutputDto> GetProvinceToAreaByEnable();
        #endregion
    }
}
