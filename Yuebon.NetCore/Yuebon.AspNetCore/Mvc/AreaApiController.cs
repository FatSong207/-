using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Yuebon.AspNetCore.Models;
using Yuebon.AspNetCore.Mvc;
using Yuebon.AspNetCore.Mvc.Filter;
using Yuebon.AspNetCore.ViewModel;
using Yuebon.Commons.Core.Dtos;
using Yuebon.Commons.Dtos;
using Yuebon.Commons.Extensions;
using Yuebon.Commons.IServices;
using Yuebon.Commons.Log;
using Yuebon.Commons.Mapping;
using Yuebon.Commons.Models;
using Yuebon.Commons.Pages;

namespace Yuebon.AspNetCore.Controllers
{
    /// <summary>
    /// 基本控制器，增刪改查
    /// </summary>
    /// <typeparam name="T">實體類型</typeparam>
    /// <typeparam name="TODto">數據輸出實體類型</typeparam>
    /// <typeparam name="TIDto">數據輸入實體類型</typeparam>
    /// <typeparam name="TService">Service類型</typeparam>
    /// <typeparam name="TKey">主鍵數據類型</typeparam>
    [ApiController]
    public abstract class AreaApiController<T,TODto, TIDto, TService, TKey> : ApiController
        where T : Entity
        where TService : IService<T, TODto, TKey>
        where TODto : class
        where TIDto : class
        where TKey : IEquatable<TKey>
    {

        #region 屬性變量


        /// <summary>
        /// 服務接口
        /// </summary>
        public TService iService;

        #endregion


        
        #region 構造函數及常用

        /// <summary>
        /// 構造方法
        /// </summary>
        /// <param name="_iService"></param>
        public AreaApiController(TService _iService)
        {
            iService = _iService;
        }

        #endregion

        #region 公共添加、修改、刪除、軟刪除接口


        /// <summary>
        /// 在插入數據前對數據的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected virtual void OnBeforeInsert(T info)
        {
            //留給子類對參數對象進行修改
        }

        /// <summary>
        /// 在更新數據前對數據的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected virtual void OnBeforeUpdate(T info)
        {
            //留給子類對參數對象進行修改
        }

        /// <summary>
        /// 在軟刪除數據前對數據的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected virtual void OnBeforeSoftDelete(T info)
        {
            //留給子類對參數對象進行修改
        }

        /// <summary>
        /// 異步新增數據
        /// </summary>
        /// <param name="tinfo"></param>
        /// <returns></returns>
        [HttpPost("Insert")]
        [YuebonAuthorize("Add")]
        public virtual async Task<IActionResult> InsertAsync(TIDto tinfo)
        {
            CommonResult result = new CommonResult();
            T info = tinfo.MapTo<T>();
            OnBeforeInsert(info);
            long ln = await iService.InsertAsync(info).ConfigureAwait(false);
            if (ln > 0)
            {
                result.ErrCode = ErrCode.successCode;
                result.ErrMsg = ErrCode.err0;
            }
            else
            {
                result.ErrMsg = ErrCode.err43001;
                result.ErrCode = "43001";
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 異步更新數據，需要在業務模塊控制器重寫該方法,否則更新無效
        /// </summary>
        /// <param name="inInfo"></param>
        /// <returns></returns>
        [HttpPost("Update")]
        [YuebonAuthorize("Edit")]
        public virtual async Task<IActionResult> UpdateAsync(TIDto inInfo)
        {
            CommonResult result = new CommonResult();
            return ToJsonContent(result);
        }
        /// <summary>
        /// 物理刪除
        /// </summary>
        /// <param name="id">主鍵Id</param>
        [HttpDelete("Delete")]
        [YuebonAuthorize("Delete")]
        public virtual IActionResult Delete(TKey id)
        {
            CommonResult result = new CommonResult();
            bool bl = iService.Delete(id);
            if (bl)
            {
                result.ErrCode = ErrCode.successCode;
                result.ErrMsg = ErrCode.err0;
            }
            else
            {
                result.ErrMsg = ErrCode.err43003;
                result.ErrCode = "43003";
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 異步物理刪除
        /// </summary>
        /// <param name="id">主鍵Id</param>
        [HttpDelete("DeleteAsync")]
        [YuebonAuthorize("Delete")]
        public virtual async Task<IActionResult> DeleteAsync(TKey id)
        {
            CommonResult result = new CommonResult();
                bool bl = await iService.DeleteAsync(id).ConfigureAwait(false);
                if (bl)
                {
                    result.ErrCode = ErrCode.successCode;
                    result.ErrMsg = ErrCode.err0;
                }
                else
                {
                    result.ErrMsg = ErrCode.err43003;
                    result.ErrCode = "43003";
                }
            return ToJsonContent(result);
        }
        /// <summary>
        /// 異步批量物理刪除
        /// </summary>
        /// <param name="info"></param>
        [HttpDelete("DeleteBatchAsync")]
        [YuebonAuthorize("Delete")]
        public virtual async Task<IActionResult> DeleteBatchAsync(DeletesInputDto info)
        {
            CommonResult result = new CommonResult();
            string where = string.Empty;
            if (typeof(TKey) == typeof(string))
            {
                where = "id in ('" + info.Ids.Join(",").Trim(',').Replace(",", "','") + "')";
            }
            else if (typeof(TKey) == typeof(int))
            {
                where = "id in (" + info.Ids.Join(",") + ")";
            }
            if (!string.IsNullOrEmpty(where))
            {
                bool bl = await iService.DeleteBatchWhereAsync(where).ConfigureAwait(false);
                if (bl)
                {
                    result.ErrCode = ErrCode.successCode;
                    result.ErrMsg = ErrCode.err0;
                }
                else
                {
                    result.ErrMsg = ErrCode.err43003;
                    result.ErrCode = "43003";
                }
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 軟刪除信息
        /// </summary>
        /// <param name="id">主鍵Id</param>
        /// <param name="bltag">刪除標識，默認為1：即設為刪除,0：未刪除</param>
        [HttpPost("DeleteSoft")]
        [YuebonAuthorize("DeleteSoft")]
        public virtual IActionResult DeleteSoft(TKey id, string bltag = "1")
        {
            CommonResult result = new CommonResult();
            bool bl = false;
            if (bltag == "0")
            {
                bl = true;
            }
            bool blResult = iService.DeleteSoft(bl, id, CurrentUser.UserId);
            if (blResult)
            {
                result.ErrCode = ErrCode.successCode;
                result.ErrMsg = ErrCode.err0;
            }
            else
            {
                result.ErrMsg = ErrCode.err43002;
                result.ErrCode = "43002";
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 異步軟刪除信息
        /// </summary>
        /// <param name="id">主鍵Id</param>
        /// <param name="bltag">刪除標識，默認為1：即設為刪除,0：未刪除</param>
        [HttpPost("DeleteSoftAsync")]
        [YuebonAuthorize("DeleteSoft")]
        public virtual async Task<IActionResult> DeleteSoftAsync(TKey id, string bltag = "1")
        {
            CommonResult result = new CommonResult();
            bool bl = false;
            if (bltag == "0")
            {
                bl = true;
            }
            bool blResult = await iService.DeleteSoftAsync(bl, id, CurrentUser.UserId);
            if (blResult)
            {
                result.ErrCode = ErrCode.successCode;
                result.ErrMsg = ErrCode.err0;
            }
            else
            {
                result.ErrMsg = ErrCode.err43002;
                result.ErrCode = "43002";
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 異步批量軟刪除信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost("DeleteSoftBatchAsync")]
        [YuebonAuthorize("DeleteSoft")]
        public virtual async Task<IActionResult> DeleteSoftBatchAsync(UpdateEnableViewModel info)
        {
            CommonResult result = new CommonResult();
            string where = string.Empty;
            if (typeof(TKey) == typeof(string))
            {
                where = "id in ('" + info.Ids.Join(",").Trim(',').Replace(",", "','") + "')";
            }
            else if (typeof(TKey) == typeof(int))
            {
                where = "id in (" + info.Ids.Join(",") + ")";
            }
            if (!string.IsNullOrEmpty(where))
            {
                bool bl = false;
                if (info.Flag == "1")
                {
                    bl = true;
                }
                bool blResult = await iService.DeleteSoftBatchAsync(bl, where, CurrentUser.UserId);
                if (blResult)
                {
                    result.ErrCode = ErrCode.successCode;
                    result.ErrMsg = ErrCode.err0;
                }
                else
                {
                    result.ErrMsg = ErrCode.err43002;
                    result.ErrCode = "43002";
                }
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 設為數據有效性
        /// </summary>
        /// <param name="id">主鍵Id</param>
        /// <param name="bltag">有效標識，默認為1：即設為無效,0：有效</param>
        [HttpPost("SetEnabledMark")]
        [YuebonAuthorize("Enable")]
        public virtual IActionResult SetEnabledMark(TKey id, string bltag="1")
        {
            CommonResult result = new CommonResult();
            bool bl = false;
            if (bltag == "1")
            {
                bl = true;
            }
            bool blresut = iService.SetEnabledMark(bl, id, CurrentUser.UserId);
            if (blresut)
            {
                result.ErrCode = ErrCode.successCode;
                result.ErrMsg = ErrCode.err0;
            }
            else
            {
                result.ErrMsg = ErrCode.err43002;
                result.ErrCode = "43002";
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 異步設為數據有效性
        /// </summary>
        /// <param name="id">主鍵Id</param>
        /// <param name="bltag">有效標識，默認為1：即設為無效,0：有效</param>
        [HttpPost("SetEnabledMarkAsync")]
        [YuebonAuthorize("Enable")]
        public virtual async Task<IActionResult> SetEnabledMarkAsync(TKey id, string bltag = "1")
        {
            CommonResult result = new CommonResult();
            bool bl = false;
            if (bltag == "1")
            {
                bl = true;
            }
            bool blresut = await iService.SetEnabledMarkAsync(bl, id, CurrentUser.UserId);
            if (blresut)
            {
                result.ErrCode = ErrCode.successCode;
                result.ErrMsg = ErrCode.err0;
            }
            else
            {
                result.ErrMsg = ErrCode.err43002;
                result.ErrCode = "43002";
            }
            return ToJsonContent(result);
        }


        /// <summary>
        /// 異步批量設為數據有效性
        /// </summary>
        /// <param name="info"></param>
        [HttpPost("SetEnabledMarktBatchAsync")]
        [YuebonAuthorize("Enable")]
        public virtual async Task<IActionResult> SetEnabledMarktBatchAsync(UpdateEnableViewModel info)
        {
            CommonResult result = new CommonResult();
            bool bl = false;
            if (info.Flag == "1")
            {
                bl = true;
            }
            string where = string.Empty;

            if (typeof(TKey) == typeof(string))
            {
                where = "id in ('" + info.Ids.Join(",").Replace(",", "','") + "')";
            }
            else if (typeof(TKey) == typeof(int))
            {
                where = "id in (" + info.Ids.Join(",") + ")";
            }
            if (!string.IsNullOrEmpty(where))
            {
                bool blresut = await iService.SetEnabledMarkByWhereAsync(bl,where,CurrentUser.UserId);
                if (blresut)
                {
                    result.ErrCode = ErrCode.successCode;
                    result.ErrMsg = ErrCode.err0;
                }
                else
                {
                    result.ErrMsg = ErrCode.err43002;
                    result.ErrCode = "43002";
                }
            }
            return ToJsonContent(result);
        }

        #endregion
        #region 查詢單個實體
        /// <summary>
        /// 根據主鍵Id獲取一個對象信息
        /// </summary>
        /// <param name="id">主鍵Id</param>
        /// <returns></returns>
        [HttpGet("GetById")]
        [YuebonAuthorize("List")]
        //[NoPermissionRequired]
        public virtual async Task<CommonResult<TODto>> GetById(TKey id)
        {
            CommonResult<TODto> result = new CommonResult<TODto>();
            TODto info = await iService.GetOutDtoAsync(id);
            if (info != null)
            {
                result.ErrCode = ErrCode.successCode;
                result.ResData = info;
            }
            else
            {
                result.ErrMsg = ErrCode.err50001;
                result.ErrCode = "50001";
            }
            return result;
        }
        #endregion

        #region 返回集合的接口
        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="search">查詢條件</param>
        /// <returns>指定對象的集合</returns>
        [HttpPost("FindWithPager")]
        [YuebonAuthorize("List")]
        public virtual CommonResult<PageResult<TODto>> FindWithPager(SearchInputDto<T> search)
        {
            CommonResult<PageResult<TODto>> result = new CommonResult<PageResult<TODto>>();
            result.ResData = iService.FindWithPager(search);
            result.ErrCode = ErrCode.successCode;
            return result;
        }



        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// 
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost("FindWithPagerAsync")]
        [YuebonAuthorize("List")]
        public virtual async Task<CommonResult<PageResult<TODto>>> FindWithPagerAsync(SearchInputDto<T> search)
        {
            CommonResult<PageResult<TODto>> result = new CommonResult<PageResult<TODto>>();
            result.ResData = await iService.FindWithPagerAsync(search);
            result.ErrCode = ErrCode.successCode;
            return result;
        }


        /// <summary>
        /// 獲取所有可用的
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllEnable")]
        [YuebonAuthorize("List")]
        public virtual async Task<CommonResult<List<TODto>>> GetAllEnable()
        {
            CommonResult<List<TODto>> result = new CommonResult<List<TODto>>();
            IEnumerable<T> list = await iService.GetAllByIsNotDeleteAndEnabledMarkAsync();
            List<TODto> resultList = list.MapTo<TODto>();
            result.ResData = resultList;
            result.ErrCode = ErrCode.successCode;
            result.ErrMsg = ErrCode.err0;

            return result;
        }

        #endregion


        #region 輔助方法

        #endregion

    }
}
