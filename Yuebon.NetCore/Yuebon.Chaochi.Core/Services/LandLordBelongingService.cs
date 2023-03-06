using System;
using System.Data.Common;
using Yuebon.Commons.Encrypt;
using Yuebon.Commons.Repositories;
using Yuebon.Commons.Services;
using Yuebon.Chaochi.IRepositories;
using Yuebon.Chaochi.IServices;
using Yuebon.Chaochi.Models;
using System.Data;
using Yuebon.Chaochi.Dtos;
using System.Collections.Generic;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Mapping;
using System.Threading.Tasks;
using Yuebon.Commons.Extend;
using Yuebon.Commons.Pages;
using Yuebon.Commons.Dtos;
using Yuebon.Commons.Enums;
using Yuebon.Chaochi.Core.Models;
using Yuebon.Chaochi.Core.Dtos;
using Yuebon.Commons.Json;
using System.IO;
using System.Linq;
using Yuebon.Commons.IRepositories;

namespace Yuebon.Chaochi.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class LandLordBelongingService : BaseService<LandLordBelonging, LandLordBelongingOutputDto, string>, ILandLordBelongingService
    {
        public LandLordBelongingService(ILandLordBelongingRepository iRepository) : base(iRepository)
        {
        }
    }
}