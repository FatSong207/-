using System;

namespace Yuebon.Quartz.Models
{
    /// <summary>
    /// 定時任務執行日志，數據實體對象
    /// </summary>
    [Serializable]
    public class Dept
    {
        public string code { get; set; }
        public string shortName { get; set; }
        public string name { get; set; }
        public string upperDeptCode { get; set; }
        public int deptMidLevelCode { get; set; }
        public string deptLevelCode { get; set; }
        public string deptManager { get; set; }
        public string deptReceiver { get; set; }
        public string scnDeptNamr { get; set; }
        public string engDeptName { get; set; }
        public string deptLoginLimitCount { get; set; }
        public string hrGuid { get; set; }
        public string efnetCode { get; set; }
        public string isFunctionEnabled { get; set; }
        public string desc { get; set; }
        public string levelCode { get; set; }
        public string returnWorkDate { get; set; }
        public string disabledDate { get; set; }
        public string status { get; set; }
    }
}
