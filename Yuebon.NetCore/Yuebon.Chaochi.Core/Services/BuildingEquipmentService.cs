using System;
using System.Data.Common;
using System.Linq;
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
using System.IO;
using Dapper;
using Yuebon.Commons.Json;

namespace Yuebon.Chaochi.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class BuildingEquipmentService : BaseService<BuildingEquipment, BuildingEquipment, string>, IBuildingEquipmentService
    {
        private readonly IBuildingEquipmentRepository _Repository;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="userLogOnRepository"></param>
        /// <param name="logService"></param>
        /// <param name="roleService"></param>
        /// <param name="organizeService"></param>
        public BuildingEquipmentService(IBuildingEquipmentRepository repository) : base(repository)
        {
            _Repository = repository;
        }

        /// <summary>
        /// 根據BAdd取得Id
        /// </summary>
        /// <param name="BAdd"></param>
        /// <returns></returns>
        public string GetIdByBAdd(string BAdd)
        {
            
            var inst = repository.GetWhere($"BAdd='{BAdd}'");
            if (inst is not null) {
                return inst.Id;
            } else {
                return "";
            }
        }
    }
}