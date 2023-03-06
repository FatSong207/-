using System;
using Yuebon.Commons.Services;
using Yuebon.Security.IServices;
using Yuebon.Security.IRepositories;
using Yuebon.Security.Dtos;
using Yuebon.Security.Models;
using System.Threading.Tasks;
using Yuebon.Commons.Models;
using System.Collections.Generic;
using System.Linq;
using Yuebon.Commons.Extensions;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Pages;
using Yuebon.Commons.Dtos;
using Yuebon.Commons.Mapping;
using Yuebon.Commons.Log;
using System.Data;

namespace Yuebon.Security.Services
{
    /// <summary>
    /// 單據編碼服務接口實現
    /// </summary>
    public class SequenceService : BaseService<Sequence, SequenceOutputDto, string>, ISequenceService
    {
        private readonly ISequenceRepository _repository;
        private readonly ISequenceRuleRepository _repositoryRule;
        private readonly ILogService _logService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="repositoryRule"></param>
        /// <param name="logService"></param>
        public SequenceService(ISequenceRepository repository, ISequenceRuleRepository repositoryRule, ILogService logService) : base(repository)
        {
            _repository = repository;
            _logService = logService;
            _repositoryRule = repositoryRule;
        }

        /// <summary>
        /// 獲取最新業務單據編碼
        /// </summary>
        /// <param name="sequenceName">業務單據編碼名稱</param>
        /// <returns></returns>
        public async Task<CommonResult> GetSequenceNextTask(string sequenceName)
        {

            CommonResult result = new CommonResult();
            //生成編號   
            string sequenceNewNo = "";
            #region 獲取序號生成器屬性
            if (string.IsNullOrWhiteSpace(sequenceName))
            {
                result.ErrMsg = "參數錯誤：業務編碼編號";
                return result;
            }
            //獲取序號生成器屬性
            Sequence sequence = _repository.GetWhere("SequenceName='" + sequenceName + "'");
            if (sequence != null)
            {
                IEnumerable<SequenceRule> list = _repositoryRule.GetListWhere("SequenceName='" + sequenceName + "' order by RuleOrder asc");
                if (list.Any())
                {
                    int delimiterNum = 0;
                    foreach (SequenceRule item in list)
                    {
                        delimiterNum++;

                        switch (item.RuleType)
                        {
                            case "const"://常量方式
                                sequenceNewNo += item.RuleValue;
                                break;
                            case "shortdate"://短日期 年2位月2位日期2位
                                sequenceNewNo += DateTime.Now.ToString("yyyyMMdd").Substring(2);
                                break;
                            case "date"://日期，年4位
                                sequenceNewNo += DateTime.Now.ToString("yyyyMMdd");
                                break;
                            case "ydate"://年月，年4位月2位
                                sequenceNewNo += DateTime.Now.ToString("yyyyMMdd").Substring(0,6);
                                break;
                            case "timestamp"://日期時間精確到毫秒
                                sequenceNewNo += DateTime.Now.ToString("yyyyMMddHHmmssffff");
                                break;
                            case "number"://計數，流水號
                                int num = CurrentReset(sequence, item);
                                //計數拼接
                                sequenceNewNo += NumberingSeqRule(item, num).ToString();
                                //更新當前序號
                                sequence.CurrentNo =num;
                                break;
                            case "guid"://Guid
                                sequenceNewNo += GuidUtils.NewGuidFormatN();
                                break;
                            case "random"://隨機數
                                Random random = new Random();
                                string strMax = "9".ToString().PadLeft(item.RuleValue.Length - 1, '9');
                                string strRandom = random.Next(item.RuleValue.ToInt(), strMax.ToInt()).ToString(); //生成隨機編號 
                                sequenceNewNo += strRandom;
                                break;
                        }
                        if (!string.IsNullOrEmpty(sequence.SequenceDelimiter)&& delimiterNum!= list.Count())
                        {
                            sequenceNewNo += sequence.SequenceDelimiter;
                        }

                    }
                    //當前編號
                    sequence.CurrentCode = sequenceNewNo;
                    sequence.CurrentReset = DateTime.Now.ToString("yyyyMMdd");
                    await _repository.UpdateAsync(sequence, sequence.Id);
                    result.ResData = sequenceNewNo;
                    result.Success = true;
                }
                else
                {
                    result.Success = false;
                    result.ErrMsg = "未查詢到業務編碼對應的編碼規則配置, 請檢查編碼規則配置";
                    return result;
                }
            }
            else
            {
                result.Success = false;
                result.ErrMsg = "請定義" + sequenceName + "的單據編碼！";
                return result;
            }
            #endregion
            return result;
        }

        /// <summary>
        /// 獲取最新業務單據編碼
        /// </summary>
        /// <param name="sequenceName">業務單據編碼名稱</param>
        /// <returns></returns>
        public CommonResult GetSequenceNext(string sequenceName)
        {

            CommonResult result = new CommonResult();
            //生成編號   
            string sequenceNewNo = "";
            #region 獲取序號生成器屬性
            if (string.IsNullOrWhiteSpace(sequenceName))
            {
                result.ErrMsg = "參數錯誤：業務編碼編號";
                return result;
            }
            //獲取序號生成器屬性
            Sequence sequence = _repository.GetWhere("SequenceName='" + sequenceName + "'");
            if (sequence != null)
            {
                IEnumerable<SequenceRule> list = _repositoryRule.GetListWhere("SequenceName='" + sequenceName + "' order by RuleOrder asc");
                if (list.Any())
                {
                    int delimiterNum = 0;
                    foreach (SequenceRule item in list)
                    {
                        delimiterNum++;

                        switch (item.RuleType)
                        {
                            case "const"://常量方式
                                sequenceNewNo += item.RuleValue;
                                break;
                            case "shortdate"://短日期 年2位月2位日期2位
                                sequenceNewNo += DateTime.Now.ToString("yyyyMMdd").Substring(2);
                                break;
                            case "date"://日期，年4位
                                sequenceNewNo += DateTime.Now.ToString("yyyyMMdd");
                                break;
                            case "ydate"://年月，年4位月2位
                                sequenceNewNo += DateTime.Now.ToString("yyyyMMdd").Substring(0, 6);
                                break;
                            case "sydate"://年月，年2位月2位
                                sequenceNewNo += DateTime.Now.ToString("yyyyMMdd").Substring(2, 4);
                                break;
                            case "timestamp"://日期時間精確到毫秒
                                sequenceNewNo += DateTime.Now.ToString("yyyyMMddHHmmssffff");
                                break;
                            case "number"://計數，流水號
                                int num = CurrentReset(sequence, item);
                                //計數拼接
                                sequenceNewNo += NumberingSeqRule(item, num).ToString();
                                //更新當前序號, 
                                sequence.CurrentNo = num;
                                break;
                            case "guid"://Guid
                                sequenceNewNo += GuidUtils.NewGuidFormatN();
                                break;
                            case "random"://隨機數
                                Random random = new Random();
                                string strMax="9".ToString().PadLeft(item.RuleValue.Length, '9');
                                string strRandom = random.Next(item.RuleValue.ToInt(), strMax.ToInt()).ToString(); //生成隨機編號 
                                sequenceNewNo += strRandom; 
                                break;
                        }
                        if (!string.IsNullOrEmpty(sequence.SequenceDelimiter) && delimiterNum != list.Count())
                        {
                            sequenceNewNo += sequence.SequenceDelimiter;
                        }

                    }
                    //當前編號
                    sequence.CurrentCode = sequenceNewNo;
                    sequence.CurrentReset = DateTime.Now.ToString("yyyyMMdd");
                   _repository.Update(sequence, sequence.Id);
                    result.ResData = sequenceNewNo;
                    result.Success = true;
                }
                else
                {
                    result.Success = false;
                    result.ErrMsg = "未查詢到業務編碼對應的編碼規則配置, 請檢查編碼規則配置";
                    return result;
                }
            }
            else
            {
                result.Success = false;
                result.ErrMsg = "請定義" + sequenceName + "的單據編碼！";
                return result;
            }
            #endregion
            return result;
        }
        /// <summary>
        /// 計數 方式 重置規則
        /// </summary>
        /// <param name="seq"></param>
        /// <param name="seqRule"></param>
        /// <returns></returns>
        private static int CurrentReset(Sequence seq, SequenceRule seqRule)
        {
            int newNo = 0, ruleNo = 0;
            try
            {
                ruleNo = seqRule.RuleValue.ToInt();
            }
            catch (Exception ex)
            {
                newNo = 1;
                Log4NetHelper.Error(ex.Message, ex);
            }

            switch (seq.SequenceReset)
            {
                case "D"://每天重置
                    if (!string.IsNullOrEmpty(seq.CurrentReset) && seq.CurrentReset != DateTime.Now.ToString("yyyyMMdd"))
                    {
                        newNo = 1;
                    }
                    break;
                case "M"://每月重置
                    if (!string.IsNullOrWhiteSpace(seq.CurrentReset)) {
                        if (!seq.CurrentReset.Contains(DateTime.Now.ToString("yyyyMM")))
                        {
                            newNo = ruleNo;
                        }
                    }
                    else
                    {
                        newNo = 1;
                    }
                    break;
                case "Y"://每年重置
                    if (!string.IsNullOrWhiteSpace(seq.CurrentReset))
                    {
                        if (!seq.CurrentReset.Contains(DateTime.Now.ToString("yyyy")))
                        {
                            newNo = ruleNo;
                        }
                    }
                    else
                    {
                        newNo = 1;
                    }
                    break;
            }
            if (newNo == 0)
            {
                if (seq.CurrentNo == 0)
                {
                    newNo = ruleNo;
                }
                else
                {
                    //當前序號+步長 
                    newNo = seq.CurrentNo + seq.Step;
                }
            }
            return newNo;

        }
        /// <summary>
        /// 計數規則
        /// </summary>
        /// <param name="seqRule"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        private static string NumberingSeqRule(SequenceRule seqRule, int code)
        {
            string str = "";
            if (seqRule.PaddingSide == "Left")
            {
                str += code.ToString().PadLeft(seqRule.PaddingWidth,seqRule.PaddingChar.ToChar());
            }
            if (seqRule.PaddingSide == "Right")
            {
                str += code.ToString().PadRight(seqRule.PaddingWidth, seqRule.PaddingChar.ToChar());
            }
            return str;
        }


        /// <summary>
        /// 根據條件查詢數據庫,并返回對象集合(用于分頁數據顯示)
        /// </summary>
        /// <param name="search">查詢的條件</param>
        /// <returns>指定對象的集合</returns>
        public override async Task<PageResult<SequenceOutputDto>> FindWithPagerAsync(SearchInputDto<Sequence> search, IDbConnection conn = null, IDbTransaction trans = null)
        {
            bool order = search.Order == "asc" ? false : true;
            string where = GetDataPrivilege();
            if (!string.IsNullOrEmpty(search.Keywords))
            {
                where += string.Format(" and SequenceName like '%{0}%' ", search.Keywords);
            };
            PagerInfo pagerInfo = new PagerInfo
            {
                CurrenetPageIndex = search.CurrenetPageIndex,
                PageSize = search.PageSize
            };
            List<Sequence> list = await repository.FindWithPagerAsync(where, pagerInfo, search.Sort, order);
            PageResult<SequenceOutputDto> pageResult = new PageResult<SequenceOutputDto>
            {
                CurrentPage = pagerInfo.CurrenetPageIndex,
                Items = list.MapTo<SequenceOutputDto>(),
                ItemsPerPage = pagerInfo.PageSize,
                TotalItems = pagerInfo.RecordCount
            };
            return pageResult;
        }
    }
}