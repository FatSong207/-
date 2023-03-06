using System;

namespace Yuebon.Quartz.Models
{
    /// <summary>
    /// 定時任務執行日志，數據實體對象
    /// </summary>
    [Serializable]
    public class User
    {
        public string code { get; set; }
        public string name { get; set; }
        public string ntdnsName { get; set; }
        public string ntdnsAccount { get; set; }
        public string eMail { get; set; }
        public string confidentialCode { get; set; }
        public string innerConfidentialCode { get; set; }
        public string agent { get; set; }
        public string supervisor { get; set; }
        public string eMailName { get; set; }
        public string departmentCode { get; set; }
        public string projectCode { get; set; }
        public string mobilePhone { get; set; }
        public string isSMSNotice { get; set; }
        public string schName { get; set; }
        public string engName { get; set; }
        public string onboardDate { get; set; }
        public string gender { get; set; }
        public string education { get; set; }
        public string officeLocation { get; set; }
        public string status { get; set; }
        public string address { get; set; }
        public string birthDay { get; set; }
        public string seniority { get; set; }
        public string rehabilitativeDate { get; set; }
        public string resignDate { get; set; }
        public string lineID { get; set; }
        public string title { get; set; }
        public string roleCode { get; set; }
        public string position { get; set; }
    }
}
