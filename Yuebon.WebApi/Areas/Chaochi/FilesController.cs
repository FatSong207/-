using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MiniExcelLibs;
using Senparc.NeuChar.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Yuebon.AspNetCore.Controllers;
using Yuebon.AspNetCore.Models;
using Yuebon.AspNetCore.Mvc.Filter;
using Yuebon.Chaochi.Core.IRepositories;
using Yuebon.Chaochi.Core.IServices;
using Yuebon.Chaochi.Core.Models;
using Yuebon.Chaochi.IRepositories;
using Yuebon.Chaochi.IServices;
using Yuebon.Chaochi.Models;
using Yuebon.Commons.Cache;
using Yuebon.Commons.Extend;
using Yuebon.Commons.Extensions;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.IDbContext;
using Yuebon.Commons.Json;
using Yuebon.Commons.Log;
using Yuebon.Commons.Mapping;
using Yuebon.Commons.Models;
using Yuebon.Security.Application;
using Yuebon.Security.Dtos;
using Yuebon.Security.Models;

namespace Yuebon.WebApi.Areas.Chaochi.Controllers
{
    /// <summary>
    /// 文件上傳
    /// </summary>
    /// 
    //[ApiVersion("1.0")]
    //[ApiVersionNeutral]
    [ApiExplorerSettings(GroupName = "Chaochi")]
    [Route("api/Chaochi/[controller]")]
    [ApiController]
    public class FilesController : ApiController
    {

        private string _filePath;
        private string _dbFilePath;   //數據庫中的文件路徑
        private string _dbThumbnail;   //數據庫中的縮略圖路徑
        private string _belongApp;//所屬應用
        private string _belongAppId;//所屬應用ID 
        private string _fileName;//文件名稱
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IDbContextCore _ybContext;
        private readonly IContractRemittanceService _contractRemittanceService;
        private readonly IBankinfoService _bankinfoService;
        private readonly IHistoryFormBuildingService _historyFormBuildingService;
        private readonly ISatisfactionTopicService _landLordSignContractSatisfactionTopicService;
        private readonly ISatisfactionService _satisfactionService;
        private readonly IEventQuestionnaireTopicService _eventQuestionnaireTopicService;
        private readonly IEventQuestionnaireService _eventQuestionnaireService;
        private readonly IEventService _eventService;
        private readonly IEventGuestService _eventGuestService;
        private IOpenDataRoadService _openDataRoadService;
        private readonly IOpenDataRoadRepository _openDataRoadRepository;
        private ICategoryFormService _categoryFormService;
        private ICategoryFormRepository _categoryFormRepository;
        private ICategoryContractService _categoryContractService;
        private ICategoryContractRepository _categoryContractRepository;
        private IManagementRepository _managementRepository;
        private ISecondLandlordRepository _secondLandlordRepository;
        private ISLMARepository _slmaRepository;
        private readonly IDbContextCore ybContext;


        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="hostingEnvironment"></param>
        /// <param name="ybContext"></param>
        /// <param name="contractRemittanceService"></param>
        /// <param name="bankinfoService"></param>
        /// <param name="historyFormBuildingService"></param>
        /// <param name="landLordSignContractSatisfactionTopicService"></param>
        /// <param name="satisfactionService"></param>
        /// <param name="eventQuestionnaireTopicService"></param>
        /// <param name="eventQuestionnaireService"></param>
        /// <param name="eventService"></param>
        /// <param name="eventGuestService"></param>
        /// <param name="openDataRoadService"></param>
        /// <param name="openDataRoadRepository"></param>
        /// <param name="categoryFormService"></param>
        /// <param name="categoryFormRepository"></param>
        /// <param name="categoryContractService"></param>
        /// <param name="categoryContractRepository"></param>
        /// <param name="managementRepository"></param>
        /// <param name="secondLandlordRepository"></param>
        /// <param name="slmaRepository"></param>
        public FilesController(IWebHostEnvironment hostingEnvironment,
            IDbContextCore ybContext,
            IContractRemittanceService contractRemittanceService,
            IBankinfoService bankinfoService,
            IHistoryFormBuildingService historyFormBuildingService,
            ISatisfactionTopicService landLordSignContractSatisfactionTopicService,
            ISatisfactionService satisfactionService,
            IEventQuestionnaireTopicService eventQuestionnaireTopicService,
            IEventQuestionnaireService eventQuestionnaireService,
            IEventService eventService,
            IEventGuestService eventGuestService,
            IOpenDataRoadService openDataRoadService,
            IOpenDataRoadRepository openDataRoadRepository,
            ICategoryFormService categoryFormService,
            ICategoryFormRepository categoryFormRepository,
            ICategoryContractService categoryContractService,
            ICategoryContractRepository categoryContractRepository,
            IManagementRepository managementRepository,
            ISecondLandlordRepository secondLandlordRepository,
            ISLMARepository slmaRepository)
        {
            _hostingEnvironment = hostingEnvironment;
            _ybContext = ybContext;
            _contractRemittanceService = contractRemittanceService;
            _bankinfoService = bankinfoService;
            _historyFormBuildingService = historyFormBuildingService;
            _landLordSignContractSatisfactionTopicService = landLordSignContractSatisfactionTopicService;
            _satisfactionService = satisfactionService;
            _eventQuestionnaireTopicService = eventQuestionnaireTopicService;
            _eventQuestionnaireService = eventQuestionnaireService;
            _eventService = eventService;
            _eventGuestService = eventGuestService;
            _openDataRoadService = openDataRoadService;
            _openDataRoadRepository = openDataRoadRepository;
            _categoryFormService = categoryFormService;
            _categoryFormRepository = categoryFormRepository;
            _categoryContractService = categoryContractService;
            _categoryContractRepository = categoryContractRepository;
            _managementRepository = managementRepository;
            _secondLandlordRepository = secondLandlordRepository;
            _slmaRepository = slmaRepository;
            this.ybContext = _ybContext;
        }

        /// <summary>
        ///  上傳SLMA資料(CSV格式)
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns>服務器存儲的文件信息</returns>
        [HttpPost("SLMAUpload")]
        [NoSignRequired]
        public IActionResult SLMAUpload([FromForm] IFormCollection formCollection)
        {
            CommonResult result = new CommonResult();
            var files = (FormFileCollection)formCollection.Files;
            var list = new List<SLMA>();
            var currentFile = files[0];

            try {
                using (var stream = new StreamReader(currentFile.OpenReadStream())) {
                    //stream.ReadLine();

                    while (!stream.EndOfStream) {
                        string[] rows = stream.ReadLine().Split(',');
                        if (rows.Length > 0) {
                            list.Add(new SLMA {
                                // 主鍵值
                                Id = GuidUtils.CreateNo(),
                                // 統一編號
                                SLMAID = rows[1],
                                // 公司名稱
                                Name = rows[0],
                                // 負責人姓名
                                Rep = rows[2],
                                // 許可字號/登記證字號
                                LRNo = rows[3],
                                // 營業地址
                                Address = rows[4],
                                // 聯絡電話
                                Tel = rows[5],
                                // 傳真號碼
                                Fax = rows[6],
                                // 電子郵件信箱
                                Email = rows[7],
                                // 租賃住宅管理人員
                                SIName = rows[8],
                                // 租賃住宅管理人員證書字號
                                SILRNo = rows[9],
                                // 租賃住宅管理人員通訊地址
                                SIAdd = rows[10],
                                // 租賃住宅管理人員聯絡電話
                                SITel = rows[11],
                                // 租賃住宅管理人員電子郵件信箱
                                SIEmail = rows[12],
                                // 不動產經紀人
                                AGName = rows[13],
                                // 不動產經紀人統一編號(身分證明文件編號)
                                AGID = rows[14],
                                // 不動產經紀人證書字號
                                AGLRNo = rows[15],
                                // 不動產經紀人通訊地址
                                AGAdd = rows[16],
                                // 不動產經紀人聯絡電話
                                AGTel = rows[17],
                                // 不動產經紀人電子郵件信箱
                                AGEmail = rows[18],

                                CreatorTime = DateTime.Now,
                                CreatorUserId = CurrentUser.UserId
                            });
                        }
                    }

                    var slList = list.GroupBy(x => x.SLMAID).Select(x => new SecondLandlord { SLID = x.First().SLMAID, SLName = x.First().Name, SLRep = x.First().Rep, SLLRNo = x.First().LRNo }).ToList();
                    var maList = list.GroupBy(x => x.SLMAID).Select(x => new Management { MAID = x.First().SLMAID, MName = x.First().Name, MRep = x.First().Rep, MLRNo = x.First().LRNo }).ToList();

                    using IDbConnection conn = ybContext.GetDatabase().GetDbConnection();
                    conn.Open();
                    using var eftran = ybContext.GetDatabase().BeginTransaction();

                    try {
                        _slmaRepository.DeleteBySql("DELETE FROM Chaochi_SLMA");
                        _slmaRepository.AddRange(list);

                        _slmaRepository.DeleteBySql("DELETE FROM Chaochi_SecondLandlord");
                        _secondLandlordRepository.AddRange(slList);

                        _slmaRepository.DeleteBySql("DELETE FROM Chaochi_Management");
                        _managementRepository.AddRange(maList);

                        eftran.Commit();

                        var Addres = csvAdd(files[0], "", "", "SLMA", "");
                        result.ErrCode = ErrCode.successCode;
                        result.ErrMsg = ErrCode.err0;
                        result.Success = true;
                    } catch (Exception ex) {
                        eftran.Rollback();
                        Log4NetHelper.Error("上傳SLMA資料失敗", ex);
                        result.ErrMsg = "上傳SLMA資料失敗";
                        result.ErrCode = ErrCode.err43001;
                    }
                }
            } catch (Exception ex) {
                result.ErrCode = "500";
                result.ErrMsg = ex.Message;
                Log4NetHelper.Error("", ex);
                throw ex;
            }

            return ToJsonContent(result);
        }

        /// <summary>
        ///  上傳表單類別(CSV格式)
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns>服務器存儲的文件信息</returns>
        [HttpPost("CategoryFormUpload")]
        [NoSignRequired]
        public IActionResult CategoryFormUpload([FromForm] IFormCollection formCollection)
        {
            CommonResult result = new CommonResult();
            //string belongApp = formCollection["belongApp"].ToString();
            //string belongAppId = formCollection["belongAppId"].ToString();           
            var files = (FormFileCollection)formCollection.Files;
            var list = new List<CategoryForm>();
            var currentFile = files[0];
            var tempData = new HashSet<string>();
            try {
                using (var stream = new StreamReader(currentFile.OpenReadStream())) {
                    //stream.ReadLine();
                    int sortOrder = 1;

                    while (!stream.EndOfStream) {
                        string[] rows = stream.ReadLine().Split(',');
                        if (rows.Length > 0) {
                            string layerLength = rows[0];
                            switch (layerLength) {
                                case "4":
                                    // 第一階
                                    string level1 = rows[1];
                                    // 第二階
                                    string level2 = rows[2];
                                    // 第三階
                                    string level3 = rows[3];
                                    // 第四階
                                    string level4 = rows[4];
                                    // 表單ID
                                    string formID = rows[8];

                                    // 第一階
                                    if (!string.IsNullOrEmpty(level1)) {
                                        if (!tempData.Contains(level1)) {
                                            list.Add(new CategoryForm {
                                                Id = level1,
                                                ParentId = "",
                                                Layers = 1,
                                                Name = level1,
                                                SortOrder = sortOrder,
                                                DeleteMark = false,
                                                EnabledMark = true,
                                                CreatorTime = DateTime.Now,
                                                CreatorUserId = CurrentUser.UserId
                                            });
                                            sortOrder++;
                                        }

                                        tempData.Add(level1);
                                    }

                                    // 第二階
                                    var level2Key = $"{level1}/{level2}";
                                    if (!string.IsNullOrEmpty(level2)) {
                                        if (!tempData.Contains(level2Key)) {
                                            list.Add(new CategoryForm {
                                                Id = level2Key,
                                                ParentId = level1,
                                                Layers = 2,
                                                Name = level2,
                                                DeleteMark = false,
                                                EnabledMark = true,
                                                CreatorTime = DateTime.Now,
                                                CreatorUserId = CurrentUser.UserId
                                            });
                                            tempData.Add(level2Key);
                                        }
                                    }

                                    // 第三階
                                    var level3Key = $"{level2Key}/{level3}";
                                    if (!string.IsNullOrEmpty(level3)) {
                                        if (!tempData.Contains(level3Key)) {
                                            list.Add(new CategoryForm {
                                                Id = level3Key,
                                                ParentId = level2Key,
                                                Layers = 3,
                                                Name = level3,
                                                DeleteMark = false,
                                                EnabledMark = true,
                                                CreatorTime = DateTime.Now,
                                                CreatorUserId = CurrentUser.UserId
                                            });
                                            tempData.Add(level3Key);
                                        }
                                    }

                                    // 第四階
                                    var level4Key = $"{level3Key}/{formID}";
                                    if (!string.IsNullOrEmpty(level4)) {
                                        list.Add(new CategoryForm {
                                            Id = level4Key,
                                            ParentId = level3Key,
                                            Layers = 4,
                                            Name = level4,
                                            DeleteMark = false,
                                            EnabledMark = true,
                                            CreatorTime = DateTime.Now,
                                            CreatorUserId = CurrentUser.UserId
                                        });
                                    }
                                    break;

                                case "5":
                                    // 第一階
                                    level1 = rows[1];
                                    // 第二階
                                    level2 = rows[2];
                                    // 第三階
                                    level3 = rows[3];
                                    // 第四階
                                    level4 = rows[4];
                                    // 第五階
                                    string level5 = rows[5];
                                    // 表單ID
                                    formID = rows[8];

                                    // 第一階
                                    if (!string.IsNullOrEmpty(level1)) {
                                        if (!tempData.Contains(level1)) {
                                            list.Add(new CategoryForm {
                                                Id = level1,
                                                ParentId = "",
                                                Layers = 1,
                                                Name = level1,
                                                SortOrder = sortOrder,
                                                DeleteMark = false,
                                                EnabledMark = true,
                                                CreatorTime = DateTime.Now,
                                                CreatorUserId = CurrentUser.UserId
                                            });
                                            sortOrder++;
                                            tempData.Add(level1);
                                        }
                                    }

                                    // 第二階
                                    level2Key = $"{level1}/{level2}";
                                    if (!string.IsNullOrEmpty(level2)) {
                                        if (!tempData.Contains(level2Key)) {
                                            list.Add(new CategoryForm {
                                                Id = level2Key,
                                                ParentId = level1,
                                                Layers = 2,
                                                Name = level2,
                                                DeleteMark = false,
                                                EnabledMark = true,
                                                CreatorTime = DateTime.Now,
                                                CreatorUserId = CurrentUser.UserId
                                            });
                                            tempData.Add(level2Key);
                                        }
                                    }

                                    // 第三階
                                    level3Key = $"{level2Key}/{level3}";
                                    if (!string.IsNullOrEmpty(level3)) {
                                        if (!tempData.Contains(level3Key)) {
                                            list.Add(new CategoryForm {
                                                Id = level3Key,
                                                ParentId = level2Key,
                                                Layers = 3,
                                                Name = level3,
                                                DeleteMark = false,
                                                EnabledMark = true,
                                                CreatorTime = DateTime.Now,
                                                CreatorUserId = CurrentUser.UserId
                                            });
                                            tempData.Add(level3Key);
                                        }
                                    }

                                    // 第四階
                                    level4Key = $"{level3Key}/{level4}";
                                    if (!string.IsNullOrEmpty(level4)) {
                                        if (!tempData.Contains(level4Key)) {
                                            list.Add(new CategoryForm {
                                                Id = level4Key,
                                                ParentId = level3Key,
                                                Layers = 4,
                                                Name = level4,
                                                DeleteMark = false,
                                                EnabledMark = true,
                                                CreatorTime = DateTime.Now,
                                                CreatorUserId = CurrentUser.UserId
                                            });
                                            tempData.Add(level4Key);
                                        }
                                    }

                                    // 第五階
                                    var level5Key = $"{level4Key}/{formID}";
                                    if (!string.IsNullOrEmpty(level5)) {
                                        list.Add(new CategoryForm {
                                            Id = level5Key,
                                            ParentId = level4Key,
                                            Layers = 5,
                                            Name = level5,
                                            DeleteMark = false,
                                            EnabledMark = true,
                                            CreatorTime = DateTime.Now,
                                            CreatorUserId = CurrentUser.UserId
                                        });
                                    }
                                    break;

                                case "6":
                                    // 第一階
                                    level1 = rows[1];
                                    // 第二階
                                    level2 = rows[2];
                                    // 第三階
                                    level3 = rows[3];
                                    // 第四階
                                    level4 = rows[4];
                                    // 第五階
                                    level5 = rows[5];
                                    // 第六階
                                    var level6 = rows[6];
                                    // 表單ID
                                    formID = rows[8];

                                    // 第一階
                                    if (!string.IsNullOrEmpty(level1)) {
                                        if (!tempData.Contains(level1)) {
                                            list.Add(new CategoryForm {
                                                Id = level1,
                                                ParentId = "",
                                                Layers = 1,
                                                Name = level1,
                                                SortOrder = sortOrder,
                                                DeleteMark = false,
                                                EnabledMark = true,
                                                CreatorTime = DateTime.Now,
                                                CreatorUserId = CurrentUser.UserId
                                            });
                                            sortOrder++;
                                            tempData.Add(level1);

                                        }
                                    }

                                    // 第二階
                                    level2Key = $"{level1}/{level2}";
                                    if (!string.IsNullOrEmpty(level2)) {
                                        if (!tempData.Contains(level2Key)) {
                                            list.Add(new CategoryForm {
                                                Id = level2Key,
                                                ParentId = level1,
                                                Layers = 2,
                                                Name = level2,
                                                DeleteMark = false,
                                                EnabledMark = true,
                                                CreatorTime = DateTime.Now,
                                                CreatorUserId = CurrentUser.UserId
                                            });
                                            tempData.Add(level2Key);
                                        }
                                    }

                                    // 第三階
                                    level3Key = $"{level2Key}/{level3}";
                                    if (!string.IsNullOrEmpty(level3)) {
                                        if (!tempData.Contains(level3Key)) {
                                            list.Add(new CategoryForm {
                                                Id = level3Key,
                                                ParentId = level2Key,
                                                Layers = 3,
                                                Name = level3,
                                                DeleteMark = false,
                                                EnabledMark = true,
                                                CreatorTime = DateTime.Now,
                                                CreatorUserId = CurrentUser.UserId
                                            });
                                            tempData.Add(level3Key);
                                        }
                                    }

                                    // 第四階
                                    level4Key = $"{level3Key}/{level4}";
                                    if (!string.IsNullOrEmpty(level4)) {
                                        if (!tempData.Contains(level4Key)) {
                                            list.Add(new CategoryForm {
                                                Id = level4Key,
                                                ParentId = level3Key,
                                                Layers = 4,
                                                Name = level4,
                                                DeleteMark = false,
                                                EnabledMark = true,
                                                CreatorTime = DateTime.Now,
                                                CreatorUserId = CurrentUser.UserId
                                            });
                                            tempData.Add(level4Key);
                                        }
                                    }

                                    // 第五階
                                    level5Key = $"{level4Key}/{level5}";
                                    if (!string.IsNullOrEmpty(level5)) {
                                        if (!tempData.Contains(level5Key)) {
                                            list.Add(new CategoryForm {
                                                Id = level5Key,
                                                ParentId = level4Key,
                                                Layers = 5,
                                                Name = level5,
                                                DeleteMark = false,
                                                EnabledMark = true,
                                                CreatorTime = DateTime.Now,
                                                CreatorUserId = CurrentUser.UserId
                                            });
                                            tempData.Add(level5Key);
                                        }
                                    }

                                    // 第六階
                                    var level6Key = $"{level5Key}/{formID}";
                                    if (!string.IsNullOrEmpty(level6)) {
                                        list.Add(new CategoryForm {
                                            Id = level6Key,
                                            ParentId = level5Key,
                                            Layers = 6,
                                            Name = level6,
                                            DeleteMark = false,
                                            EnabledMark = true,
                                            CreatorTime = DateTime.Now,
                                            CreatorUserId = CurrentUser.UserId
                                        });
                                    }
                                    break;
                            }
                        }
                    }
                }

                using IDbConnection conn = ybContext.GetDatabase().GetDbConnection();
                conn.Open();
                using var eftran = ybContext.GetDatabase().BeginTransaction();

                try {
                    _categoryFormRepository.DeleteBySql("DELETE FROM Chaochi_CategoryForm");
                    _categoryFormRepository.AddRange(list);

                    eftran.Commit();

                    var Addres = csvAdd(files[0], "", "", "CategoryForm", "");
                    result.ErrCode = ErrCode.successCode;
                    result.ErrMsg = ErrCode.err0;
                    result.Success = true;
                } catch (Exception ex) {
                    eftran.Rollback();
                    Log4NetHelper.Error("上傳表單類別失敗", ex);
                    result.ErrMsg = "上傳表單類別失敗";
                    result.ErrCode = ErrCode.err43001;
                }
            } catch (Exception ex) {
                result.ErrCode = "500";
                result.ErrMsg = ex.Message;
                Log4NetHelper.Error("", ex);
                throw ex;
            }


            return ToJsonContent(result);
        }

        /// <summary>
        ///  上傳合約類別(CSV格式)
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns>服務器存儲的文件信息</returns>
        [HttpPost("CategoryContractUpload")]
        [NoSignRequired]
        public IActionResult CategoryContractUpload([FromForm] IFormCollection formCollection)
        {
            CommonResult result = new CommonResult();
            //string belongApp = formCollection["belongApp"].ToString();
            //string belongAppId = formCollection["belongAppId"].ToString();           
            var files = (FormFileCollection)formCollection.Files;
            var list = new List<CategoryContract>();
            var currentFile = files[0];
            var tempData = new HashSet<string>();
            try {
                using (var stream = new StreamReader(currentFile.OpenReadStream())) {
                    //stream.ReadLine();
                    int sortOrder = 1;
                    int sortOrder_lv2 = 1;
                    int sortOrder_lv5 = 1;

                    while (!stream.EndOfStream) {
                        string[] rows = stream.ReadLine().Split(',');
                        if (rows.Length > 0) {
                            string layerLength = rows[0];

                            switch (layerLength) {
                                case "5":
                                    // 第一階
                                    var level1 = rows[1];
                                    // 第二階
                                    var level2 = rows[2];
                                    // 第三階
                                    var level3 = rows[3];
                                    // 第四階
                                    var level4 = rows[4];
                                    // 第五階
                                    var level5 = rows[5];
                                    // 表單ID
                                    var formID = rows[8];
                                    // 合約種類
                                    var type = rows[9];
                                    // 歸類
                                    var archiveLTo = rows[10];
                                    // 業務是否要簽核
                                    var needSalesSign = rows[11];
                                    // 主管是否要簽核
                                    var needSupervisorSign = rows[12];
                                    // 業務是否要簽核
                                    var needSignOnline = rows[13];
                                    // 合約子類別
                                    var subType = rows[15];

                                    // 第一階
                                    if (!string.IsNullOrEmpty(level1)) {
                                        if (!tempData.Contains(level1)) {
                                            list.Add(new CategoryContract {
                                                Id = level1,
                                                ParentId = "",
                                                Layers = 1,
                                                Name = level1,
                                                SortOrder = sortOrder,
                                                DeleteMark = false,
                                                EnabledMark = true,
                                                CreatorTime = DateTime.Now,
                                                CreatorUserId = CurrentUser.UserId
                                            });
                                            sortOrder++;
                                            tempData.Add(level1);

                                        }
                                    }

                                    // 第二階
                                    var level2Key = $"{level1}/{level2}";
                                    if (!string.IsNullOrEmpty(level2)) {
                                        if (!tempData.Contains(level2Key)) {
                                            list.Add(new CategoryContract {
                                                Id = level2Key,
                                                ParentId = level1,
                                                Layers = 2,
                                                Name = level2,
                                                DeleteMark = false,
                                                EnabledMark = true,
                                                CreatorTime = DateTime.Now,
                                                CreatorUserId = CurrentUser.UserId
                                            });
                                            tempData.Add(level2Key);
                                        }
                                    }

                                    // 第三階
                                    var level3Key = $"{level2Key}/{level3}";
                                    if (!string.IsNullOrEmpty(level3)) {
                                        if (!tempData.Contains(level3Key)) {
                                            list.Add(new CategoryContract {
                                                Id = level3Key,
                                                ParentId = level2Key,
                                                Layers = 3,
                                                Name = level3,
                                                DeleteMark = false,
                                                EnabledMark = true,
                                                CreatorTime = DateTime.Now,
                                                CreatorUserId = CurrentUser.UserId
                                            });
                                            tempData.Add(level3Key);
                                        }
                                    }

                                    // 第四階
                                    var level4Key = $"{level3Key}/{level4}";
                                    if (!string.IsNullOrEmpty(level4)) {
                                        if (!tempData.Contains(level4Key)) {
                                            list.Add(new CategoryContract {
                                                Id = level4Key,
                                                ParentId = level3Key,
                                                Layers = 4,
                                                Name = level4,
                                                DeleteMark = false,
                                                EnabledMark = true,
                                                CreatorTime = DateTime.Now,
                                                CreatorUserId = CurrentUser.UserId
                                            });
                                            tempData.Add(level4Key);
                                        }
                                    }

                                    // 第五階
                                    //var level5Key = $"{level4Key}/{formID}";
                                    var level5Key = $"{formID}";
                                    if (!string.IsNullOrEmpty(level5)) {
                                        list.Add(new CategoryContract {
                                            Id = level5Key,
                                            ParentId = (string.IsNullOrEmpty(archiveLTo)) ? level4Key : formID.Substring(0, formID.Length - 1),
                                            Layers = 5,
                                            Name = level5,
                                            ArchiveLTo = archiveLTo,
                                            Type = (string.IsNullOrEmpty(archiveLTo)) ? type : "",
                                            SubType = (string.IsNullOrEmpty(subType)) ? subType : "",
                                            NeedSalesSign = (string.IsNullOrEmpty(archiveLTo)) ? needSalesSign : "",
                                            NeedSupervisorSign = (string.IsNullOrEmpty(archiveLTo)) ? needSupervisorSign : "",
                                            NeedSignOnline = (string.IsNullOrEmpty(archiveLTo)) ? needSignOnline : "",
                                            DeleteMark = false,
                                            EnabledMark = (string.IsNullOrEmpty(archiveLTo)) ? true : false,
                                            CreatorTime = DateTime.Now,
                                            CreatorUserId = CurrentUser.UserId
                                        });
                                    }
                                    break;

                                case "6":
                                    // 第一階
                                    level1 = rows[1];
                                    // 第二階
                                    level2 = rows[2];
                                    // 第三階
                                    level3 = rows[3];
                                    // 第四階
                                    level4 = rows[4];
                                    // 第五階
                                    level5 = rows[5];
                                    // 第六階
                                    var level6 = rows[6];
                                    // 表單ID                                    
                                    formID = rows[8];
                                    // 合約種類
                                    type = rows[9];
                                    // 歸類
                                    archiveLTo = rows[10];
                                    // 業務是否要簽核
                                    needSalesSign = rows[11];
                                    // 主管是否要簽核
                                    needSupervisorSign = rows[12];
                                    // 業務是否要簽核
                                    needSignOnline = rows[13];
                                    // 合約子類別
                                    subType = rows[15];

                                    // 第一階
                                    if (!string.IsNullOrEmpty(level1)) {
                                        if (!tempData.Contains(level1)) {
                                            list.Add(new CategoryContract {
                                                Id = level1,
                                                ParentId = "",
                                                Layers = 1,
                                                Name = level1,
                                                SortOrder = sortOrder,
                                                DeleteMark = false,
                                                EnabledMark = true,
                                                CreatorTime = DateTime.Now,
                                                CreatorUserId = CurrentUser.UserId
                                            });
                                            sortOrder++;
                                            tempData.Add(level1);

                                        }
                                    }

                                    // 第二階
                                    level2Key = $"{level1}/{level2}";
                                    if (!string.IsNullOrEmpty(level2)) {
                                        if (!tempData.Contains(level2Key)) {
                                            list.Add(new CategoryContract {
                                                Id = level2Key,
                                                ParentId = level1,
                                                Layers = 2,
                                                Name = level2,
                                                SortOrder = sortOrder_lv2,
                                                DeleteMark = false,
                                                EnabledMark = true,
                                                CreatorTime = DateTime.Now,
                                                CreatorUserId = CurrentUser.UserId
                                            });
                                            sortOrder_lv2++;
                                            tempData.Add(level2Key);
                                        }
                                    }

                                    // 第三階
                                    level3Key = $"{level2Key}/{level3}";
                                    if (!string.IsNullOrEmpty(level3)) {
                                        if (!tempData.Contains(level3Key)) {
                                            list.Add(new CategoryContract {
                                                Id = level3Key,
                                                ParentId = level2Key,
                                                Layers = 3,
                                                Name = level3,
                                                DeleteMark = false,
                                                EnabledMark = true,
                                                CreatorTime = DateTime.Now,
                                                CreatorUserId = CurrentUser.UserId
                                            });
                                            tempData.Add(level3Key);
                                        }
                                    }

                                    // 第四階
                                    level4Key = $"{level3Key}/{level4}";
                                    if (!string.IsNullOrEmpty(level4)) {
                                        if (!tempData.Contains(level4Key)) {
                                            list.Add(new CategoryContract {
                                                Id = level4Key,
                                                ParentId = level3Key,
                                                Layers = 4,
                                                Name = level4,
                                                DeleteMark = false,
                                                EnabledMark = true,
                                                CreatorTime = DateTime.Now,
                                                CreatorUserId = CurrentUser.UserId
                                            });
                                            tempData.Add(level4Key);
                                        }
                                    }

                                    // 第五階
                                    level5Key = $"{level4Key}/{level5}";
                                    if (!string.IsNullOrEmpty(level5)) {
                                        if (!tempData.Contains(level5Key)) {
                                            list.Add(new CategoryContract {
                                                Id = level5Key,
                                                ParentId = level4Key,
                                                Layers = 5,
                                                Name = level5,
                                                SortOrder = sortOrder_lv5,
                                                DeleteMark = false,
                                                EnabledMark = true,
                                                CreatorTime = DateTime.Now,
                                                CreatorUserId = CurrentUser.UserId
                                            });
                                            sortOrder_lv5++;
                                            tempData.Add(level5Key);
                                        }
                                    }

                                    // 第六階(附件)
                                    //var level6Key = $"{level5Key}/{formID}";
                                    var level6Key = $"{formID}";
                                    //if (!string.IsNullOrEmpty(level6)) {
                                    if (!tempData.Contains(level6Key)) {
                                        list.Add(new CategoryContract {
                                            Id = level6Key,
                                            ParentId = (string.IsNullOrEmpty(archiveLTo)) ? level5Key : formID.Substring(0, formID.Length - 1),
                                            Layers = 6,
                                            Name = level6,
                                            ArchiveLTo = archiveLTo,
                                            NeedSalesSign = (string.IsNullOrEmpty(archiveLTo)) ? needSalesSign : "",
                                            NeedSupervisorSign = (string.IsNullOrEmpty(archiveLTo)) ? needSupervisorSign : "",
                                            NeedSignOnline = (string.IsNullOrEmpty(archiveLTo)) ? needSignOnline : "",
                                            Type = (string.IsNullOrEmpty(archiveLTo)) ? type : "",
                                            SubType = (string.IsNullOrEmpty(subType)) ? subType : "",
                                            DeleteMark = false,
                                            EnabledMark = (string.IsNullOrEmpty(archiveLTo)) ? true : false,
                                            CreatorTime = DateTime.Now,
                                            CreatorUserId = CurrentUser.UserId
                                        }); ;
                                        //    tempData.Add(level6Key);
                                        //}
                                    }
                                    break;

                                case "7":
                                    // 第一階
                                    level1 = rows[1];
                                    // 第二階
                                    level2 = rows[2];
                                    // 第三階
                                    level3 = rows[3];
                                    // 第四階
                                    level4 = rows[4];
                                    // 第五階
                                    level5 = rows[5];
                                    // 第六階
                                    level6 = rows[6];
                                    // 第七階
                                    var level7 = rows[7];
                                    // 表單ID                                    
                                    formID = rows[8];
                                    // 合約種類
                                    type = rows[9];
                                    // 歸類
                                    archiveLTo = rows[10];
                                    // 業務是否要簽核
                                    needSalesSign = rows[11];
                                    // 主管是否要簽核
                                    needSupervisorSign = rows[12];
                                    // 業務是否要簽核
                                    needSignOnline = rows[13];
                                    // 合約子類別
                                    subType = rows[15];


                                    // 第一階
                                    if (!string.IsNullOrEmpty(level1)) {
                                        if (!tempData.Contains(level1)) {
                                            list.Add(new CategoryContract {
                                                Id = level1,
                                                ParentId = "",
                                                Layers = 1,
                                                Name = level1,
                                                SortOrder = sortOrder,
                                                DeleteMark = false,
                                                EnabledMark = true,
                                                CreatorTime = DateTime.Now,
                                                CreatorUserId = CurrentUser.UserId
                                            });
                                            sortOrder++;
                                            tempData.Add(level1);

                                        }
                                    }

                                    // 第二階
                                    level2Key = $"{level1}/{level2}";
                                    if (!string.IsNullOrEmpty(level2)) {
                                        if (!tempData.Contains(level2Key)) {
                                            list.Add(new CategoryContract {
                                                Id = level2Key,
                                                ParentId = level1,
                                                Layers = 2,
                                                Name = level2,
                                                DeleteMark = false,
                                                EnabledMark = true,
                                                CreatorTime = DateTime.Now,
                                                CreatorUserId = CurrentUser.UserId
                                            });
                                            tempData.Add(level2Key);
                                        }
                                    }

                                    // 第三階
                                    level3Key = $"{level2Key}/{level3}";
                                    if (!string.IsNullOrEmpty(level3)) {
                                        if (!tempData.Contains(level3Key)) {
                                            list.Add(new CategoryContract {
                                                Id = level3Key,
                                                ParentId = level2Key,
                                                Layers = 3,
                                                Name = level3,
                                                DeleteMark = false,
                                                EnabledMark = true,
                                                CreatorTime = DateTime.Now,
                                                CreatorUserId = CurrentUser.UserId
                                            });
                                            tempData.Add(level3Key);
                                        }
                                    }

                                    // 第四階
                                    level4Key = $"{level3Key}/{level4}";
                                    if (!string.IsNullOrEmpty(level4)) {
                                        if (!tempData.Contains(level4Key)) {
                                            list.Add(new CategoryContract {
                                                Id = level4Key,
                                                ParentId = level3Key,
                                                Layers = 4,
                                                Name = level4,
                                                DeleteMark = false,
                                                EnabledMark = true,
                                                CreatorTime = DateTime.Now,
                                                CreatorUserId = CurrentUser.UserId
                                            });
                                            tempData.Add(level4Key);
                                        }
                                    }

                                    // 第五階
                                    level5Key = $"{level4Key}/{level5}";
                                    if (!string.IsNullOrEmpty(level5)) {
                                        if (!tempData.Contains(level5Key)) {
                                            list.Add(new CategoryContract {
                                                Id = level5Key,
                                                ParentId = level4Key,
                                                Layers = 5,
                                                Name = level5,
                                                DeleteMark = false,
                                                EnabledMark = true,
                                                CreatorTime = DateTime.Now,
                                                CreatorUserId = CurrentUser.UserId
                                            });
                                            tempData.Add(level5Key);
                                        }
                                    }

                                    // 第六階
                                    level6Key = $"{level5Key}/{level6}";
                                    if (!string.IsNullOrEmpty(level6)) {
                                        if (!tempData.Contains(level6Key)) {
                                            list.Add(new CategoryContract {
                                                Id = level6Key,
                                                ParentId = level5Key,
                                                Layers = 6,
                                                Name = level6,
                                                DeleteMark = false,
                                                EnabledMark = true,
                                                CreatorTime = DateTime.Now,
                                                CreatorUserId = CurrentUser.UserId
                                            });
                                            tempData.Add(level6Key);
                                        }
                                    }

                                    // 第七階
                                    //var level7Key = $"{level6Key}/{formID}";
                                    var level7Key = $"{formID}";
                                    if (!string.IsNullOrEmpty(level7)) {
                                        list.Add(new CategoryContract {
                                            Id = level7Key,
                                            ParentId = (string.IsNullOrEmpty(archiveLTo)) ? level6Key : formID.Substring(0, formID.Length - 1),
                                            Layers = 7,
                                            Name = level7,
                                            ArchiveLTo = archiveLTo,
                                            Type = (string.IsNullOrEmpty(archiveLTo)) ? type : "",
                                            SubType = (string.IsNullOrEmpty(subType)) ? subType : "",
                                            NeedSalesSign = (string.IsNullOrEmpty(archiveLTo)) ? needSalesSign : "",
                                            NeedSupervisorSign = (string.IsNullOrEmpty(archiveLTo)) ? needSupervisorSign : "",
                                            NeedSignOnline = (string.IsNullOrEmpty(archiveLTo)) ? needSignOnline : "",
                                            DeleteMark = false,
                                            EnabledMark = (string.IsNullOrEmpty(archiveLTo)) ? true : false,
                                            CreatorTime = DateTime.Now,
                                            CreatorUserId = CurrentUser.UserId
                                        });
                                    }
                                    break;
                            }
                        }
                    }
                }

                using IDbConnection conn = ybContext.GetDatabase().GetDbConnection();
                conn.Open();
                using var eftran = ybContext.GetDatabase().BeginTransaction();

                try {
                    _categoryContractRepository.DeleteBySql("DELETE FROM Chaochi_CategoryContract");
                    _categoryContractRepository.AddRange(list);

                    eftran.Commit();

                    var Addres = csvAdd(files[0], "", "", "CategoryContract", "");
                    result.ErrCode = ErrCode.successCode;
                    result.ErrMsg = ErrCode.err0;
                    result.Success = true;
                } catch (Exception ex) {
                    eftran.Rollback();
                    Log4NetHelper.Error("上傳合約類別失敗", ex);
                    result.ErrMsg = "上傳合約類別失敗";
                    result.ErrCode = ErrCode.err43001;
                }
            } catch (Exception ex) {
                result.ErrCode = "500";
                result.ErrMsg = ex.Message;
                Log4NetHelper.Error("", ex);
                throw ex;
            }


            return ToJsonContent(result);
        }

        /// <summary>
        ///  上船活動賓客名單
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns>服務器存儲的文件信息</returns>
        [HttpPost("EventGuestUpload")]
        [NoSignRequired]
        public async Task<IActionResult> EventGuestUpload([FromForm] IFormCollection formCollection)
        {
            CommonResult result = new CommonResult();
            string belongApp = formCollection["belongApp"].ToString();
            string belongAppId = formCollection["belongAppId"].ToString();
            string eventId = formCollection["id"].ToString();
            var files = (FormFileCollection)formCollection.Files;
            var list = new List<EventGuest>();
            var currentFile = files[0];
            int guestCount = 0;
            using (var stream = new StreamReader(currentFile.OpenReadStream())) {
                stream.ReadLine();
                while (!stream.EndOfStream) {
                    string[] rows = stream.ReadLine().Split(',');
                    string name = rows[0];
                    string cell = rows[1];
                    if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(cell)) {
                        break;
                    } else {
                        guestCount++;
                        list.Add(new EventGuest {
                            GuestName = name,
                            EventId = eventId,
                            GusetCell = cell,
                        });
                    }
                }
            }
            try {
                using IDbConnection conn = _ybContext.GetDatabase().GetDbConnection();
                conn.Open();
                using var eftran = _ybContext.GetDatabase().BeginTransaction();
                var tran = eftran.GetDbTransaction();
                try {
                    await _eventGuestService.DeleteConnWhereAsync($" EventId = '{eventId}' ", conn, eftran.GetDbTransaction());
                    _eventGuestService.InsertRange(list);

                    //更新Event主表
                    await _eventService.UpdateTableFieldAsync("GuestCount", guestCount, $" Id = '{eventId}' ");
                    var Addres = csvAdd(files[0], belongApp, belongAppId, "EventGuest", eventId);
                    //await _eventCostService.DeleteConnWhereAsync($" EventId = '{eventId}' ", conn, eftran.GetDbTransaction());
                    //_eventCostService.InsertRange(eventCosts);

                    eftran.Commit();
                    result.Success = true;
                    result.ErrCode = ErrCode.successCode;
                } catch (Exception ex) {
                    eftran.Rollback();
                    Log4NetHelper.Error("更新失敗", ex);
                    result.ErrMsg = "更新失敗";
                    result.ErrCode = ErrCode.err43001;
                    throw;
                }
            } catch (Exception ex) {
                Log4NetHelper.Error("TransactionFailed", ex);
                result.ErrMsg = "TransactionFailed";
                result.ErrCode = ErrCode.err43001;
            }
            //try {
            //    _eventGuestService.DeleteConnWhereAsync();
            //        //_eventGuestService.DeleteBySql("delete from OpenDataRoad");
            //        _openDataRoadService.InsertRange(list);
            //        var Addres = csvAdd(files[0], belongApp, belongAppId, "OpenDataRoad");
            //        result.ErrCode = ErrCode.successCode;
            //        result.Success = true;
            //    } catch (Exception ex) {
            //        result.ErrCode = "500";
            //        result.ErrMsg = ex.Message;
            //        Log4NetHelper.Error("", ex);
            //        throw ex;
            //    }


            return ToJsonContent(result);
        }

        /// <summary>
        ///  上船政府公開路名資料 限定CSV
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns>服務器存儲的文件信息</returns>
        [HttpPost("CsvUpload")]
        [NoSignRequired]
        public IActionResult CsvUpload([FromForm] IFormCollection formCollection)
        {
            CommonResult result = new CommonResult();
            string belongApp = formCollection["belongApp"].ToString();
            string belongAppId = formCollection["belongAppId"].ToString();
            var files = (FormFileCollection)formCollection.Files;
            var list = new List<OpenDataRoad>();
            var currentFile = files[0];
            using (var stream = new StreamReader(currentFile.OpenReadStream())) {
                stream.ReadLine();
                stream.ReadLine();
                while (!stream.EndOfStream) {
                    string[] rows = stream.ReadLine().Split(',');
                    string city = rows[0];
                    string town = rows[1];
                    string street = rows[2];
                    if (!string.IsNullOrEmpty(city) && !string.IsNullOrEmpty(town) && !string.IsNullOrEmpty(street)) {
                        list.Add(new OpenDataRoad {
                            Id = city + town.Remove(0, 3) + street,
                            city = city,
                            site_id = town.Remove(0, 3),
                            road = street,
                        });
                    }
                }
            }
            //list = list.Where(x => x.city != "" && x.site_id != "" && x.road != "").ToList();
            var count = list.Count > 30000;
            if (count) {
                try {
                    _openDataRoadRepository.DeleteBySql("delete from OpenDataRoad");
                    _openDataRoadService.InsertRange(list);
                    var Addres = csvAdd(files[0], belongApp, belongAppId, "OpenDataRoad", "");
                    result.ErrCode = ErrCode.successCode;
                    result.Success = true;
                } catch (Exception ex) {
                    result.ErrCode = "500";
                    result.ErrMsg = ex.Message;
                    Log4NetHelper.Error("", ex);
                    throw ex;
                }
            } else {
                result.Success = false;
                result.ErrMsg = ErrCode.err43001;
                result.ErrCode = "43001";
            }


            return ToJsonContent(result);
        }
        private Yuebon.Security.Dtos.UploadFileResultOuputDto csvAdd(IFormFile file, string belongApp, string belongAppId, string uploadType, string eventId)
        {
            //_belongApp = belongApp;
            //_belongAppId = belongAppId;
            if (file != null && file.Length > 0 && file.Length < 10485760) {
                using (var binaryReader = new BinaryReader(file.OpenReadStream())) {
                    var fileName = string.Empty;
                    fileName = file.FileName;

                    var data = binaryReader.ReadBytes((int)file.Length);
                    var filenameExt = csvUploadFile(fileName, data, uploadType, eventId);

                    Yuebon.Security.Dtos.UploadFileResultOuputDto uploadFileResultOuputDto = new Yuebon.Security.Dtos.UploadFileResultOuputDto {
                        FileName = filenameExt,
                    };
                    return uploadFileResultOuputDto;
                }
            } else {
                Log4NetHelper.Info("文件過大");
                throw new Exception("文件過大");
            }
        }

        /// <summary>
        /// 實現文件上傳到服務器保存，并生成縮略圖
        /// </summary>
        /// <param name="fileName">文件名稱</param>
        /// <param name="fileBuffers">文件字節流</param>
        /// <param name="uploadType"></param>
        /// <param name="eventId"></param>
        private string csvUploadFile(string fileName, byte[] fileBuffers, string uploadType, string eventId)
        {

            //判斷文件是否為空
            if (string.IsNullOrEmpty(fileName)) {
                Log4NetHelper.Info("文件名不能為空");
                throw new Exception("文件名不能為空");
            }

            //判斷文件是否為空
            if (fileBuffers.Length < 1) {
                Log4NetHelper.Info("文件不能為空");
                throw new Exception("文件不能為空");
            }

            Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
            Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();
            var path = $"{sysSetting.ChaochiFilepath}";
            string uploadPath = "";
            //string uploadPath = @$"{path}OpenDataRoad\";
            if (uploadType == "EventGuest") {
                uploadPath = @$"{path}{uploadType}\{eventId}\";
                fileName = "賓客清單.csv";
            } else {
                uploadPath = @$"{path}{uploadType}\";
            }

            if (!(System.IO.File.Exists(uploadPath))) {
                Directory.CreateDirectory(@$"{uploadPath}\");
            }
            //string ext = Path.GetExtension(fileName).ToLower();
            //string newName = GuidUtils.CreateNo();
            //string newfileName = newName + ext;

            using (var fs = new FileStream(uploadPath + fileName, FileMode.Create)) {
                fs.Write(fileBuffers, 0, fileBuffers.Length);
                fs.Close();
            }
            return fileName;
        }

        /// <summary>
        ///  上傳調查問卷題目定義檔
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns>服務器存儲的文件信息</returns>
        [HttpPost("EventQuestionnaireUpload")]
        [NoSignRequired]
        public async Task<IActionResult> EventQuestionnaireUpload([FromForm] IFormCollection formCollection)
        {
            CommonResult result = new CommonResult();
            string qCode = formCollection["id"].ToString();
            var files = (FormFileCollection)formCollection.Files;
            var list = new List<EventQuestionnaireTopic>();
            var currentFile = files[0];
            int questionCount = 0;

            using (var stream = currentFile.OpenReadStream()) {
                var rows = stream.Query().ToList();
                questionCount = rows.Count - 1;
                //stream.ReadLine();
                //while (!stream.EndOfStream) {
                //    string[] rows = stream.ReadLine().Split(',');
                //    int sequence = rows[0].ToInt();
                //    string topic = rows[1];
                //    string type = rows[2];
                //    string required = rows[3];
                //    string last = rows[4];
                //    string options = rows[5];
                //    if (sequence == 0 && string.IsNullOrEmpty(topic)) {
                //        break;
                //    } else {
                //        questionCount++;
                //        list.Add(new EventQuestionnaireTopic {
                //            QCode = qCode,
                //            Sequence = sequence,
                //            Topic = topic,
                //            Type = type,
                //            Required = required,
                //            Last = last,
                //            Options = options
                //        });
                //    }
                //}
            }
            try {
                using IDbConnection conn = _ybContext.GetDatabase().GetDbConnection();
                conn.Open();
                using var eftran = _ybContext.GetDatabase().BeginTransaction();
                var tran = eftran.GetDbTransaction();
                try {
                    //await _eventQuestionnaireTopicService.DeleteConnWhereAsync($" QCode = '{qCode}' ", conn, eftran.GetDbTransaction());
                    //_eventQuestionnaireTopicService.InsertRange(list);

                    //更新EventQuestionnaire主表
                    var eq =await _eventQuestionnaireService.GetWhereAsync($" QCode = '{qCode}' ");
                    eq.QuestionCount = questionCount.ToString();
                    eq.QFileName = currentFile.FileName;
                    eq.QName = "";
                    eq.QBegingWords = "";
                    eq.QEndWords = "";
                    eq.HasGened = "N";
                    await _eventQuestionnaireService.UpdateAsync(eq,eq.Id);


                    //await _eventQuestionnaireService.UpdateTableFieldAsync("QuestionCount", questionCount, $" QCode = '{qCode}' ");
                    //await _eventQuestionnaireService.UpdateTableFieldAsync("QFileName", currentFile.FileName, $" QCode = '{qCode}' ");
                    var Addres = EventQuestAdd(files[0], qCode);

                    //eftran.Commit();
                    result.Success = true;
                    result.ErrCode = ErrCode.successCode;
                } catch (Exception ex) {
                    eftran.Rollback();
                    Log4NetHelper.Error("更新失敗", ex);
                    result.ErrMsg = "更新失敗";
                    result.ErrCode = ErrCode.err43001;
                    throw;
                }
            } catch (Exception ex) {
                Log4NetHelper.Error("TransactionFailed", ex);
                result.ErrMsg = "TransactionFailed";
                result.ErrCode = ErrCode.err43001;
            }
            return ToJsonContent(result);
        }

        private Yuebon.Security.Dtos.UploadFileResultOuputDto EventQuestAdd(IFormFile file, string qCode)
        {
            //_belongApp = belongApp;
            //_belongAppId = belongAppId;
            if (file != null && file.Length > 0 && file.Length < 10485760) {
                using (var binaryReader = new BinaryReader(file.OpenReadStream())) {
                    var fileName = string.Empty;
                    fileName = file.FileName;

                    var data = binaryReader.ReadBytes((int)file.Length);
                    var filenameExt = EventQuestUploadFile(fileName, data, qCode);

                    Yuebon.Security.Dtos.UploadFileResultOuputDto uploadFileResultOuputDto = new Yuebon.Security.Dtos.UploadFileResultOuputDto {
                        FileName = filenameExt,
                    };
                    return uploadFileResultOuputDto;
                }
            } else {
                Log4NetHelper.Info("文件過大");
                throw new Exception("文件過大");
            }
        }

        /// <summary>
        /// 實現文件上傳到服務器保存，并生成縮略圖
        /// </summary>
        /// <param name="fileName">文件名稱</param>
        /// <param name="fileBuffers">文件字節流</param>
        /// <param name="qCode"></param>
        private string EventQuestUploadFile(string fileName, byte[] fileBuffers, string qCode)
        {

            //判斷文件是否為空
            if (string.IsNullOrEmpty(fileName)) {
                Log4NetHelper.Info("文件名不能為空");
                throw new Exception("文件名不能為空");
            }

            //判斷文件是否為空
            if (fileBuffers.Length < 1) {
                Log4NetHelper.Info("文件不能為空");
                throw new Exception("文件不能為空");
            }

            Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
            Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();
            var path = $"{sysSetting.ChaochiFilepath}";
            string uploadPath = "";
            uploadPath = @$"{path}EventQuestionnaireTopic\{qCode}\";
            //fileName = "調查問卷題目定義檔.csv";

            if (!(System.IO.File.Exists(uploadPath))) {
                Directory.CreateDirectory(@$"{uploadPath}\");
            }
            //string ext = Path.GetExtension(fileName).ToLower();
            //string newName = GuidUtils.CreateNo();
            //string newfileName = newName + ext;

            using (var fs = new FileStream(uploadPath + fileName, FileMode.Create)) {
                fs.Write(fileBuffers, 0, fileBuffers.Length);
                fs.Close();
            }
            return fileName;
        }

        /// <summary>
        ///  上傳滿意度問卷題目定義檔
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns>服務器存儲的文件信息</returns>
        [HttpPost("SatisfactionUpload")]
        [NoSignRequired]
        public async Task<IActionResult> SatisfactionUpload([FromForm] IFormCollection formCollection)
        {
            CommonResult result = new CommonResult();
            string satisfactionId = formCollection["id"].ToString();
            var files = (FormFileCollection)formCollection.Files;
            var list = new List<SatisfactionTopic>();
            var currentFile = files[0];
            int questionCount = 0;
            using (var stream = currentFile.OpenReadStream()) {
                var rows = stream.Query().ToList();
                questionCount = rows.Count - 1;
                var aaa = rows;
                //stream.ReadLine();
                //while (!stream.EndOfStream) {
                //    string[] rows = stream.ReadLine().Split(',');
                //    int sequence = rows[0].ToInt();
                //    string topic = rows[1];
                //    string type = rows[2];
                //    string required = rows[3];
                //    string last = rows[4];
                //    string options = rows[5];
                //    if (sequence == 0 && string.IsNullOrEmpty(topic)) {
                //        break;
                //    } else {
                //        questionCount++;
                //        list.Add(new SatisfactionTopic {
                //            SatisfactionId = satisfactionId,
                //            Sequence = sequence,
                //            Topic = topic,
                //            Type = type,
                //            Required = required,
                //            Last = last,
                //            Options = options
                //        });
                //    }
                //}
            }
            try {
                using IDbConnection conn = _ybContext.GetDatabase().GetDbConnection();
                conn.Open();
                using var eftran = _ybContext.GetDatabase().BeginTransaction();
                var tran = eftran.GetDbTransaction();
                try {
                    //await _landLordSignContractSatisfactionTopicService.DeleteConnWhereAsync($" SatisfactionId = '{satisfactionId}' ", conn, eftran.GetDbTransaction());
                    //_landLordSignContractSatisfactionTopicService.InsertRange(list);

                    var i = await _satisfactionService.GetAsync(satisfactionId);
                    i.QuestionCount = questionCount.ToString();
                    i.QFileName = i.Type + "定義檔.xlsx";
                    i.LastModifyTime = DateTime.Now;
                    i.LastModifyUserId = CurrentUser.UserId;
                    var success = await _satisfactionService.UpdateAsync(i, satisfactionId, conn, tran);


                    //更新Satisfaction主表
                    //await _satisfactionService.UpdateTableFieldAsync("QuestionCount", questionCount, $" Id = '{satisfactionId}' ",conn,tran);
                    //await _satisfactionService.UpdateTableFieldAsync("QFileName", i.Type+"定義檔.xlsx", $" Id = '{satisfactionId}' ", conn, tran);
                    var Addres = SatisfactionAdd(files[0], i.Type + "定義檔.xlsx");

                    eftran.Commit();
                    result.Success = true;
                    result.ErrCode = ErrCode.successCode;
                } catch (Exception ex) {
                    eftran.Rollback();
                    Log4NetHelper.Error("更新失敗", ex);
                    result.ErrMsg = "更新失敗";
                    result.ErrCode = ErrCode.err43001;
                    throw;
                }
            } catch (Exception ex) {
                Log4NetHelper.Error("TransactionFailed", ex);
                result.ErrMsg = "TransactionFailed";
                result.ErrCode = ErrCode.err43001;
            }


            return ToJsonContent(result);
        }

        private Yuebon.Security.Dtos.UploadFileResultOuputDto SatisfactionAdd(IFormFile file, string fileName)
        {
            //_belongApp = belongApp;
            //_belongAppId = belongAppId;
            if (file != null && file.Length > 0 && file.Length < 10485760) {
                using (var binaryReader = new BinaryReader(file.OpenReadStream())) {
                    //var fileName = string.Empty;
                    //fileName = file.FileName;

                    var data = binaryReader.ReadBytes((int)file.Length);
                    var filenameExt = SatisfactionUploadFile(fileName, data);

                    Yuebon.Security.Dtos.UploadFileResultOuputDto uploadFileResultOuputDto = new Yuebon.Security.Dtos.UploadFileResultOuputDto {
                        FileName = filenameExt,
                    };
                    return uploadFileResultOuputDto;
                }
            } else {
                Log4NetHelper.Info("文件過大");
                throw new Exception("文件過大");
            }
        }

        /// <summary>
        /// 實現文件上傳到服務器保存，并生成縮略圖
        /// </summary>
        /// <param name="fileName">文件名稱</param>
        /// <param name="fileBuffers">文件字節流</param>
        private string SatisfactionUploadFile(string fileName, byte[] fileBuffers)
        {

            //判斷文件是否為空
            if (string.IsNullOrEmpty(fileName)) {
                Log4NetHelper.Info("文件名不能為空");
                throw new Exception("文件名不能為空");
            }

            //判斷文件是否為空
            if (fileBuffers.Length < 1) {
                Log4NetHelper.Info("文件不能為空");
                throw new Exception("文件不能為空");
            }

            Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
            Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();
            var path = $"{sysSetting.ChaochiFilepath}";
            string uploadPath = "";
            uploadPath = @$"{path}SatisfactionTopic\";
            //fileName = "調查問卷題目定義檔.csv";

            if (!(System.IO.File.Exists(uploadPath))) {
                Directory.CreateDirectory(@$"{uploadPath}\");
            }
            //string ext = Path.GetExtension(fileName).ToLower();
            //string newName = GuidUtils.CreateNo();
            //string newfileName = newName + ext;

            using (var fs = new FileStream(uploadPath + fileName, FileMode.Create)) {
                fs.Write(fileBuffers, 0, fileBuffers.Length);
                fs.Close();
            }
            return fileName;
        }


        /// <summary>
        ///  上傳建物/設備照片(只接收壓縮檔)
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns>服務器存儲的文件信息</returns>
        [HttpPost("BuildingImgZipUpload")]
        [NoSignRequired]
        public async Task<IActionResult> BuildingImgZipUpload([FromForm] IFormCollection formCollection)
        {
            CommonResult result = new CommonResult();
            string buildingId = formCollection["sid"].ToString();
            string formName = formCollection["formname"].ToString();
            string note = formCollection["note"].ToString();
            var files = (FormFileCollection)formCollection.Files;
            var currentFile = files[0];
            try {
                var Addres = BuildingImgZipAdd(files[0], buildingId);
                using IDbConnection conn = _ybContext.GetDatabase().GetDbConnection();
                conn.Open();
                using var eftran = _ybContext.GetDatabase().BeginTransaction();
                var tran = eftran.GetDbTransaction();
                try {
                    var histBuilding = new HistoryFormBuilding() {
                        FileName = Addres.FileName,
                        UploadTime = DateTime.Now,
                        IDNo = buildingId,
                        FormName = string.IsNullOrEmpty(formName) ? currentFile.FileName : formName,
                        Note = note,
                        CreatorUserId = CurrentUser.UserId
                    };
                    await _historyFormBuildingService.InsertAsync(histBuilding);



                    eftran.Commit();
                    result.Success = true;
                    result.ErrCode = ErrCode.successCode;
                } catch (Exception ex) {
                    eftran.Rollback();
                    Log4NetHelper.Error("更新失敗", ex);
                    result.ErrMsg = "更新失敗";
                    result.ErrCode = ErrCode.err43001;
                    throw;
                }
            } catch (Exception ex) {
                Log4NetHelper.Error("TransactionFailed", ex);
                result.ErrMsg = "TransactionFailed";
                result.ErrCode = ErrCode.err43001;
            }


            return ToJsonContent(result);
        }

        /// <summary>
        /// 上傳建物/設備照片(只接收壓縮檔)
        /// </summary>
        /// <param name="file"></param>
        /// <param name="buildingId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private Yuebon.Security.Dtos.UploadFileResultOuputDto BuildingImgZipAdd(IFormFile file, string buildingId)
        {
            //_belongApp = belongApp;
            //_belongAppId = belongAppId;
            if (file != null && file.Length > 0 && file.Length < 10485760) {
                using (var binaryReader = new BinaryReader(file.OpenReadStream())) {
                    var fileName = string.Empty;
                    fileName = file.FileName;

                    var data = binaryReader.ReadBytes((int)file.Length);
                    var filenameExt = BuildingImgZipUploadFile(fileName, data, buildingId);

                    Yuebon.Security.Dtos.UploadFileResultOuputDto uploadFileResultOuputDto = new Yuebon.Security.Dtos.UploadFileResultOuputDto {
                        FileName = filenameExt,
                    };
                    return uploadFileResultOuputDto;
                }
            } else {
                Log4NetHelper.Info("文件過大");
                throw new Exception("文件過大");
            }
        }

        /// <summary>
        /// 上傳建物/設備照片(只接收壓縮檔)
        /// </summary>
        /// <param name="fileName">文件名稱</param>
        /// <param name="fileBuffers">文件字節流</param>
        /// <param name="buildingId"></param>
        private string BuildingImgZipUploadFile(string fileName, byte[] fileBuffers, string buildingId)
        {

            //判斷文件是否為空
            if (string.IsNullOrEmpty(fileName)) {
                Log4NetHelper.Info("文件名不能為空");
                throw new Exception("文件名不能為空");
            }

            //判斷文件是否為空
            if (fileBuffers.Length < 1) {
                Log4NetHelper.Info("文件不能為空");
                throw new Exception("文件不能為空");
            }

            fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + fileName;

            Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
            Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();
            var path = $"{sysSetting.ChaochiFilepath}";
            string uploadPath = "";
            uploadPath = @$"{path}historyPDF\Building\{buildingId}\";

            if (!Directory.Exists(uploadPath)) {
                Directory.CreateDirectory(@$"{uploadPath}\");
            }
            //string ext = Path.GetExtension(fileName).ToLower();
            //string newName = GuidUtils.CreateNo();
            //string newfileName = newName + ext;

            using (var fs = new FileStream(uploadPath + fileName, FileMode.Create)) {
                fs.Write(fileBuffers, 0, fileBuffers.Length);
                fs.Close();
            }
            return fileName;
        }

        /// <summary>
        ///  銀行資訊資料 限定CSV
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns>服務器存儲的文件信息</returns>
        [HttpPost("BankinfoUpload")]
        [NoSignRequired]
        public IActionResult BankinfoUpload([FromForm] IFormCollection formCollection)
        {
            CommonResult result = new CommonResult();
            string belongApp = formCollection["belongApp"].ToString();
            string belongAppId = formCollection["belongAppId"].ToString();
            var files = (FormFileCollection)formCollection.Files;
            var list = new List<Bankinfo>();
            var currentFile = files[0];
            using (var stream = new StreamReader(currentFile.OpenReadStream())) {
                //stream.ReadLine();
                stream.ReadLine();
                while (!stream.EndOfStream) {
                    string row = stream.ReadLine();
                    string[] data = row.Split(',');
                    string bankNo = data[0];
                    if (!bankNo.Contains("\"")) {
                        result.Success = false;
                        result.ErrCode = ErrCode.err40110;
                        result.ErrMsg = "請在銀行代碼前加上'  \"  '";
                        return ToJsonContent(result);
                    }
                    bankNo = data[0].Replace("\"", "");
                    string bankName = data[1];
                    string branchNo = data[2];
                    if (!string.IsNullOrEmpty(branchNo)) {
                        branchNo = branchNo.Replace("\"", "");
                    }
                    string branchName = data[3];
                    list.Add(new Bankinfo {
                        Id = GuidUtils.CreateNo(),
                        BankNo = bankNo,
                        BankName = bankName,
                        BranchNo = branchNo,
                        BranchName = branchName
                    });

                }
            }
            try {
                _bankinfoService.DeleteConnWhereAsync("");
                _bankinfoService.InsertRange(list);
                var Addres = BankinfoAdd(files[0], belongApp, belongAppId);
                result.ErrCode = ErrCode.successCode;
                result.Success = true;
            } catch (Exception ex) {
                result.ErrCode = "500";
                result.ErrMsg = ex.Message;
                Console.WriteLine(ex.InnerException.Message);
                Log4NetHelper.Error("", ex);
                throw;
            }

            return ToJsonContent(result);
        }
        private Yuebon.Security.Dtos.UploadFileResultOuputDto BankinfoAdd(IFormFile file, string belongApp, string belongAppId)
        {
            //_belongApp = belongApp;
            //_belongAppId = belongAppId;
            if (file != null && file.Length > 0 && file.Length < 10485760) {
                using (var binaryReader = new BinaryReader(file.OpenReadStream())) {
                    var fileName = string.Empty;
                    fileName = file.FileName;

                    var data = binaryReader.ReadBytes((int)file.Length);
                    var filenameExt = BankinfoUploadFile(fileName, data);

                    Yuebon.Security.Dtos.UploadFileResultOuputDto uploadFileResultOuputDto = new Yuebon.Security.Dtos.UploadFileResultOuputDto {
                        FileName = filenameExt,
                    };
                    return uploadFileResultOuputDto;
                }
            } else {
                Log4NetHelper.Info("文件過大");
                throw new Exception("文件過大");
            }
        }

        /// <summary>
        /// 實現文件上傳到服務器保存，并生成縮略圖
        /// </summary>
        /// <param name="fileName">文件名稱</param>
        /// <param name="fileBuffers">文件字節流</param>
        private string BankinfoUploadFile(string fileName, byte[] fileBuffers)
        {

            //判斷文件是否為空
            if (string.IsNullOrEmpty(fileName)) {
                Log4NetHelper.Info("文件名不能為空");
                throw new Exception("文件名不能為空");
            }

            //判斷文件是否為空
            if (fileBuffers.Length < 1) {
                Log4NetHelper.Info("文件不能為空");
                throw new Exception("文件不能為空");
            }

            Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
            Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();
            var path = $"{sysSetting.ChaochiFilepath}";
            string uploadPath = @$"{path}Bankinfo\";

            if (!Directory.Exists(uploadPath)) {
                Directory.CreateDirectory(@$"{uploadPath}");
            }

            using (var fs = new FileStream(uploadPath + fileName, FileMode.Create)) {
                fs.Write(fileBuffers, 0, fileBuffers.Length);
                fs.Close();
            }
            return fileName;
        }

        /// <summary>
        /// 合約匯款資料 限定CSV
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns>服務器存儲的文件信息</returns>
        [HttpPost("ContractRemittanceUpload")]
        [NoSignRequired]
        public IActionResult ContractRemittanceUpload([FromForm] IFormCollection formCollection)
        {
            CommonResult result = new CommonResult();
            string belongApp = formCollection["belongApp"].ToString();
            string belongAppId = formCollection["belongAppId"].ToString();
            var files = (FormFileCollection)formCollection.Files;
            var list = new List<ContractRemittance>();
            var currentFile = files[0];
            using (var stream = new StreamReader(currentFile.OpenReadStream())) {
                //stream.ReadLine();
                stream.ReadLine();
                while (!stream.EndOfStream) {
                    string row = stream.ReadLine();
                    string[] data = row.Split(',');
                    string type = data[0];
                    string accountName = data[1];
                    string useCounty = data[2];
                    string bankName = data[3];
                    string bankNo = data[4];
                    string branchName = data[5];
                    string branchNo = data[6].Trim();
                    if (!bankNo.Contains("\"")) {
                        result.Success = false;
                        result.ErrCode = ErrCode.err40110;
                        result.ErrMsg = "請在銀行代碼前加上'  \"  '";
                        return ToJsonContent(result);
                    }
                    bankNo = data[4].Replace("\"", "");
                    if (!string.IsNullOrEmpty(branchNo)) {
                        branchNo = branchNo.Replace("\"", "").Trim();
                    }
                    list.Add(new ContractRemittance {
                        Id = GuidUtils.CreateNo(),
                        Type = type,
                        AccountName = accountName,
                        UseCounty = useCounty,
                        BankNo = bankNo,
                        BankName = bankName,
                        BranchNo = branchNo,
                        BranchName = branchName
                    });

                }
            }
            try {
                _contractRemittanceService.DeleteConnWhereAsync("");
                _contractRemittanceService.InsertRange(list);
                var Addres = ContractRemittanceAdd(files[0], belongApp, belongAppId);
                result.ErrCode = ErrCode.successCode;
                result.Success = true;
            } catch (Exception ex) {
                result.ErrCode = "500";
                result.ErrMsg = ex.Message;
                Log4NetHelper.Error("", ex);
                throw;
            }

            return ToJsonContent(result);
        }
        private Yuebon.Security.Dtos.UploadFileResultOuputDto ContractRemittanceAdd(IFormFile file, string belongApp, string belongAppId)
        {
            //_belongApp = belongApp;
            //_belongAppId = belongAppId;
            if (file != null && file.Length > 0 && file.Length < 10485760) {
                using (var binaryReader = new BinaryReader(file.OpenReadStream())) {
                    var fileName = string.Empty;
                    fileName = file.FileName;

                    var data = binaryReader.ReadBytes((int)file.Length);
                    var filenameExt = ContractRemittanceUploadFile(fileName, data);

                    Yuebon.Security.Dtos.UploadFileResultOuputDto uploadFileResultOuputDto = new Yuebon.Security.Dtos.UploadFileResultOuputDto {
                        FileName = filenameExt,
                    };
                    return uploadFileResultOuputDto;
                }
            } else {
                Log4NetHelper.Info("文件過大");
                throw new Exception("文件過大");
            }
        }

        /// <summary>
        /// 實現文件上傳到服務器保存，并生成縮略圖
        /// </summary>
        /// <param name="fileName">文件名稱</param>
        /// <param name="fileBuffers">文件字節流</param>
        private string ContractRemittanceUploadFile(string fileName, byte[] fileBuffers)
        {

            //判斷文件是否為空
            if (string.IsNullOrEmpty(fileName)) {
                Log4NetHelper.Info("文件名不能為空");
                throw new Exception("文件名不能為空");
            }

            //判斷文件是否為空
            if (fileBuffers.Length < 1) {
                Log4NetHelper.Info("文件不能為空");
                throw new Exception("文件不能為空");
            }

            Commons.Cache.YuebonCacheHelper yuebonCacheHelper = new Commons.Cache.YuebonCacheHelper();
            Yuebon.Security.Models.SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<Yuebon.Security.Models.SysSetting>();
            var path = $"{sysSetting.ChaochiFilepath}";
            string uploadPath = @$"{path}ContrantRemittance\";

            if (!Directory.Exists(uploadPath)) {
                Directory.CreateDirectory(@$"{uploadPath}");
            }

            using (var fs = new FileStream(uploadPath + fileName, FileMode.Create)) {
                fs.Write(fileBuffers, 0, fileBuffers.Length);
                fs.Close();
            }
            return fileName;
        }


        /// <summary>
        ///  單文件上傳接口
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns>服務器存儲的文件信息</returns>
        [HttpPost("Upload")]
        [NoSignRequired]
        public IActionResult Upload([FromForm] IFormCollection formCollection)
        {
            CommonResult result = new CommonResult();

            FormFileCollection filelist = (FormFileCollection)formCollection.Files;
            string belongApp = formCollection["belongApp"].ToString();
            string belongAppId = formCollection["belongAppId"].ToString();
            _fileName = filelist[0].FileName;
            try {
                result.ResData = Add(filelist[0], belongApp, belongAppId);
                result.ErrCode = ErrCode.successCode;
                result.Success = true;
            } catch (Exception ex) {
                result.ErrCode = "500";
                result.ErrMsg = ex.Message;
                Log4NetHelper.Error("", ex);
                throw ex;
            }
            return ToJsonContent(result);
        }
        /// <summary>
        ///  批量上傳文件接口
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns>服務器存儲的文件信息</returns>
        [HttpPost("Uploads")]
        public IActionResult Uploads([FromForm] IFormCollection formCollection)
        {
            CommonResult result = new CommonResult();
            FormFileCollection filelist = (FormFileCollection)formCollection.Files;
            string belongApp = formCollection["belongApp"].ToString();
            string belongAppId = formCollection["belongAppId"].ToString();
            try {
                result.ResData = Adds(filelist, belongApp, belongAppId);
            } catch (Exception ex) {
                Log4NetHelper.Error("", ex);
                result.ErrCode = "500";
                result.ErrMsg = ex.Message;
            }

            return ToJsonContent(result);
        }
        /// <summary>
        /// 刪除文件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("DeleteFile")]
        public IActionResult DeleteFile(string id)
        {
            CommonResult result = new CommonResult();
            try {
                UploadFile uploadFile = new UploadFileApp().Get(id);

                YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
                SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<SysSetting>();
                string localpath = _hostingEnvironment.WebRootPath;
                if (uploadFile != null) {
                    string filepath = (localpath + "/" + uploadFile.FilePath).ToFilePath();
                    if (System.IO.File.Exists(filepath))
                        System.IO.File.Delete(filepath);
                    string filepathThu = (localpath + "/" + uploadFile.Thumbnail).ToFilePath();
                    if (System.IO.File.Exists(filepathThu))
                        System.IO.File.Delete(filepathThu);

                    result.ErrCode = ErrCode.successCode;
                    result.Success = true;
                } else {
                    result.ErrCode = ErrCode.failCode;
                    result.Success = false;
                }

            } catch (Exception ex) {
                Log4NetHelper.Error("", ex);
                result.ErrCode = "500";
                result.ErrMsg = ex.Message;
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 批量上傳文件
        /// </summary>
        /// <param name="files">文件</param>
        /// <param name="belongApp">所屬應用，如文章article</param>
        /// <param name="belongAppId">所屬應用ID，如文章id</param>
        /// <returns></returns>
        private List<UploadFileResultOuputDto> Adds(IFormFileCollection files, string belongApp, string belongAppId)
        {
            List<UploadFileResultOuputDto> result = new List<UploadFileResultOuputDto>();
            foreach (var file in files) {
                if (file != null) {
                    result.Add(Add(file, belongApp, belongAppId));
                }
            }
            return result;
        }
        /// <summary>
        /// 單個上傳文件
        /// </summary>
        /// <param name="file"></param>
        /// <param name="belongApp"></param>
        /// <param name="belongAppId"></param>
        /// <returns></returns>
        private UploadFileResultOuputDto Add(IFormFile file, string belongApp, string belongAppId)
        {
            _belongApp = belongApp;
            _belongAppId = belongAppId;
            if (file != null && file.Length > 0 && file.Length < 10485760) {
                using (var binaryReader = new BinaryReader(file.OpenReadStream())) {
                    var fileName = string.Empty;
                    fileName = _fileName;

                    var data = binaryReader.ReadBytes((int)file.Length);
                    UploadFile(fileName, data);
                    ;
                    UploadFile filedb = new UploadFile {
                        Id = GuidUtils.CreateNo(),
                        FilePath = _dbFilePath,
                        Thumbnail = _dbThumbnail,
                        FileName = fileName,
                        FileSize = file.Length.ToInt(),
                        FileType = Path.GetExtension(fileName),
                        Extension = Path.GetExtension(fileName),
                        BelongApp = _belongApp,
                        BelongAppId = _belongAppId
                    };
                    new UploadFileApp().Insert(filedb);
                    UploadFileResultOuputDto uploadFileResultOuputDto = filedb.MapTo<UploadFileResultOuputDto>();
                    uploadFileResultOuputDto.PhysicsFilePath = (_hostingEnvironment.WebRootPath + "/" + _dbThumbnail).ToFilePath(); ;
                    return uploadFileResultOuputDto;
                }
            } else {
                Log4NetHelper.Info("文件過大");
                throw new Exception("文件過大");
            }
        }
        /// <summary>
        /// 實現文件上傳到服務器保存，并生成縮略圖
        /// </summary>
        /// <param name="fileName">文件名稱</param>
        /// <param name="fileBuffers">文件字節流</param>
        private void UploadFile(string fileName, byte[] fileBuffers)
        {

            //判斷文件是否為空
            if (string.IsNullOrEmpty(fileName)) {
                Log4NetHelper.Info("文件名不能為空");
                throw new Exception("文件名不能為空");
            }

            //判斷文件是否為空
            if (fileBuffers.Length < 1) {
                Log4NetHelper.Info("文件不能為空");
                throw new Exception("文件不能為空");
            }

            YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
            SysSetting sysSetting = yuebonCacheHelper.Get("SysSetting").ToJson().ToObject<SysSetting>();
            string folder = DateTime.Now.ToString("yyyyMMdd");
            _filePath = _hostingEnvironment.WebRootPath;
            var _tempfilepath = sysSetting.Filepath;

            if (!string.IsNullOrEmpty(_belongApp)) {
                _tempfilepath += "/" + _belongApp;
            }
            if (!string.IsNullOrEmpty(_belongAppId)) {
                _tempfilepath += "/" + _belongAppId;
            }
            if (sysSetting.Filesave == "1") {
                _tempfilepath = _tempfilepath + "/" + folder + "/";
            }
            if (sysSetting.Filesave == "2") {
                DateTime date = DateTime.Now;
                _tempfilepath = _tempfilepath + "/" + date.Year + "/" + date.Month + "/" + date.Day + "/";
            }

            var uploadPath = _filePath + "/" + _tempfilepath;
            if (sysSetting.Fileserver == "localhost") {
                if (!Directory.Exists(uploadPath)) {
                    Directory.CreateDirectory(uploadPath);
                }
            }
            string ext = Path.GetExtension(fileName).ToLower();
            string newName = GuidUtils.CreateNo();
            string newfileName = newName + ext;

            using (var fs = new FileStream(uploadPath + newfileName, FileMode.Create)) {
                fs.Write(fileBuffers, 0, fileBuffers.Length);
                fs.Close();
                //生成縮略圖
                if (ext.Contains(".jpg") || ext.Contains(".jpeg") || ext.Contains(".png") || ext.Contains(".bmp") || ext.Contains(".gif")) {
                    string thumbnailName = newName + "_" + sysSetting.Thumbnailwidth + "x" + sysSetting.Thumbnailheight + ext;
                    ImgHelper.MakeThumbnail(uploadPath + newfileName, uploadPath + thumbnailName, sysSetting.Thumbnailwidth.ToInt(), sysSetting.Thumbnailheight.ToInt());
                    _dbThumbnail = _tempfilepath + thumbnailName;
                }
                _dbFilePath = _tempfilepath + newfileName;
            }
        }
    }
}
