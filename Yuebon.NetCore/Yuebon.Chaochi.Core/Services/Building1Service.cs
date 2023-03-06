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
    public class Building1Service : BaseService<Building1, Building1OutputDto, string>, IBuilding1Service
    {
        private readonly IBuilding1Repository _Repository;
        private readonly IBuildingBelongingRepository _buildingBelongingRepository;
        private readonly Security.IRepositories.IUserLogOnRepository _userSigninRepository;
        private readonly Security.IServices.ILogService _logService;
        private readonly Security.IServices.IRoleService _roleService;
        private Security.IServices.IOrganizeService _organizeService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="userLogOnRepository"></param>
        /// <param name="logService"></param>
        /// <param name="roleService"></param>
        /// <param name="organizeService"></param>
        public Building1Service(IBuilding1Repository repository, IBuildingBelongingRepository buildingBelongingRepository, Security.IRepositories.IUserLogOnRepository userLogOnRepository, Security.IServices.ILogService logService, Security.IServices.IRoleService roleService, Security.IServices.IOrganizeService organizeService) : base(repository)
        {
            _Repository = repository;
            _buildingBelongingRepository = buildingBelongingRepository;
            _userSigninRepository = userLogOnRepository;
            _logService = logService;
            _roleService = roleService;
            _organizeService = organizeService;
        }

        /// <summary>
        /// 註冊用戶
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userLogOnEntity"></param>
        /// <param name="trans"></param>
        public bool Insert(Building1 entity, Security.Models.UserLogOn userLogOnEntity, IDbTransaction trans = null)
        {
            return _Repository.Insert(entity, userLogOnEntity, trans);
        }

        /// <summary>
        /// 註冊用戶
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userLogOnEntity"></param>
        /// <param name="trans"></param>
        public async Task<bool> InsertAsync(Building1 entity, IDbTransaction trans = null)
        {
            return await _Repository.InsertAsync(entity, trans);
        }
        /// <summary>
        /// 註冊用戶,第三方平臺
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userLogOnEntity"></param>
        /// <param name="trans"></param>
        public bool Insert(Building1 entity, Security.Models.UserLogOn userLogOnEntity, Security.Models.UserOpenIds userOpenIds, IDbTransaction trans = null)
        {
            return _Repository.Insert(entity, userLogOnEntity, userOpenIds, trans);
        }


        public string GetIdByBAdd(string BAdd)
        {
           var inst=repository.GetWhere($"BAdd='{BAdd}'");
            if (inst is not null) {
                return inst.Id;
            } else {
                return "";
            }         
        }

        public Task<Building1> GetByBAdd(string BAdd)
        {
            return _Repository.GetByBAdd(BAdd);
        }

        public int Add(Building1 entity)
        {
            return _Repository.Add(entity);
        }
    }
}