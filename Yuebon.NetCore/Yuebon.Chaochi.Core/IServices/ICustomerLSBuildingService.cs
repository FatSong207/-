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

namespace Yuebon.Chaochi.Core.IServices
{
    public interface ICustomerLSBuildingService:IService<CustomerLSBuilding,CustomerLNOutputDto,string>
    {
        Task<IEnumerable<CustomerLSBuilding>> FindByLSID(string LSID);
    }
}
