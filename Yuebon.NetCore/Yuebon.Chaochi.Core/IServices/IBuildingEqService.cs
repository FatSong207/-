using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Yuebon.Commons.Enums;
using Yuebon.Commons.IServices;
using Yuebon.Commons.Pages;
using Yuebon.Chaochi.Dtos;
using Yuebon.Chaochi.Models;

namespace Yuebon.Chaochi.IServices
{
    /// <summary>
    /// 用戶服務接口
    /// </summary>
    public interface IBuildingEqService : IService<BuildingEq, BuildingEq, string>
    {
        string GetIdByBAdd(string BAdd);
        void InsertRange(List<BuildingEq> buildingEqs);
    }
}
