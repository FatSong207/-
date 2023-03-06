/*******************************************************************************
 * Copyright © 2017-2020 Yuebon.Framework 版權所有
 * Author: Yuebon
 * Description: Yuebon快速開發平臺
 * Website：http://www.yuebon.com
*********************************************************************************/
using System;

namespace Yuebon.Commons.Extend
{
    /// <summary>
    /// 日期擴展方法
    /// </summary>
    public static class ExtDate
    {
        /// <summary>
        /// 格式：剛剛、幾分鐘前、幾小時前、幾天前、yyyy/MM/dd
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToEasyString(this DateTime value)
        {
            DateTime now = DateTime.Now;
            if (now < value) return value.ToString("yyyy/MM/dd");
            TimeSpan dep = now - value;
            if (dep.TotalMinutes < 30)
            {
                return "剛剛";
            }
            else if (dep.TotalMinutes >= 30 && dep.TotalMinutes < 60)
            {
                return (int)dep.TotalMinutes + " 分鐘前";
            }
            else if (dep.TotalHours < 24)
            {
                return (int)dep.TotalHours + " 小時前";
            }
            else if (dep.TotalDays <= 7)
            {
                return (int)dep.TotalDays + " 天前";
            }
            else if (dep.TotalDays > 7 && dep.TotalDays <= 14)
            {
                int defautlWeek = 0;
                defautlWeek = (int)dep.TotalDays / 7;
                if ((int)dep.TotalDays % 7 > 0)
                {
                    defautlWeek++;
                }
                return defautlWeek + " 周前";
            }
            else if (dep.TotalDays > 14 && dep.TotalDays < 365)
            {
                return value.Month.ToString().PadLeft(2, '0') + "-" +
                    value.Day.ToString().PadLeft(2, '0') + " " + value.Hour + ":" + value.Minute;
            }
            else return (now.Year - value.Year) + " 年前";
        }


        /// <summary>
        /// 格式：即將、幾分鐘后、幾小時后、幾天后、yyyy/MM/dd
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToEasyStringDQ(this DateTime value)
        {
            DateTime now = DateTime.Now;
            if (value < now) return "已過期";
            TimeSpan dep = value - now;

            if (dep.TotalMinutes < 60)
            {
                return (int)dep.TotalMinutes + " 分鐘后";
            }
            else if (dep.TotalHours < 24)
            {
                return (int)dep.TotalHours + " 小時后";
            }
            else if (dep.TotalDays <= 30)
            {
                return (int)dep.TotalDays + " 天后";
            }
            else if (dep.TotalDays > 30 && dep.TotalDays <= 90)
            {
                int defautlWeek = 0;
                defautlWeek = (int)dep.TotalDays / 30;
                if ((int)dep.TotalDays % 30 > 0)
                {
                    defautlWeek++;
                }
                return defautlWeek + " 個月后";
            }
            else if (dep.TotalDays > 90 && dep.TotalDays < 365)
            {
                return value.Month.ToString().PadLeft(2, '0') + "-" +
                    value.Day.ToString().PadLeft(2, '0') + " " + value.Hour + ":" + value.Minute;
            }
            else return " 長期";
        }

        /// <summary>
        /// 格式：幾秒、幾分鐘幾秒、幾小時幾分鐘幾秒
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToBrowseTime(this int value)
        {
            if (value > 0)
            {
                TimeSpan ts = new TimeSpan(0, 0, value);
                if (ts.Days > 0)
                {
                    if (ts.Hours > 0)
                    {
                        return ts.Days + "天" + ts.Hours + "小時" + ts.Minutes + "分鐘" + ts.Seconds + "秒";
                    }
                    else
                    {
                        return ts.Hours + "小時" + ts.Minutes + "分鐘";
                    }
                }
                else if (ts.Hours > 0)
                {
                    if (ts.Seconds > 0)
                    {
                        return ts.Hours + "小時" + ts.Minutes + "分鐘" + ts.Seconds + "秒";
                    }
                    else
                    {
                        return ts.Hours + "小時" + ts.Minutes + "分鐘";
                    }
                }
                else
                {
                    if (ts.Minutes > 0)
                    {
                        return ts.Minutes + "分鐘" + ts.Seconds + "秒";
                    }
                    else
                    {
                        return ts.Seconds + "秒";
                    }
                }
            }
            else
            {
                return "1秒";
            }



        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToEasyString(this DateTime? value)
        {
            if (value.HasValue) return value.Value.ToEasyString();
            else return string.Empty;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToEasyStringDQ(this DateTime? value)
        {
            if (value.HasValue) return value.Value.ToEasyStringDQ();
            else return string.Empty;
        }

        /// <summary>
        /// 計算日期間隔
        /// </summary>
        /// <param name="d1">要參與計算的其中一個日期</param>
        /// <param name="d2">要參與計算的另一個日期</param>
        /// <param name="drf">決定返回值形式的枚舉</param>
        /// <returns>一個代表年月日的int數組，具體數組長度與枚舉參數drf有關</returns>
        public static int[] ToDiffResult(DateTime d1, DateTime d2, DiffResultFormat drf)
        {
            #region 數據初始化
            DateTime max;
            DateTime min;
            int year;
            int month;
            int tempYear, tempMonth;
            if (d1 > d2)
            {
                max = d1;
                min = d2;
            }
            else
            {
                max = d2;
                min = d1;
            }
            tempYear = max.Year;
            tempMonth = max.Month;
            if (max.Month < min.Month)
            {
                tempYear--;
                tempMonth = tempMonth + 12;
            }
            year = tempYear - min.Year;
            month = tempMonth - min.Month;
            #endregion
            #region 按條件計算
            if (drf == DiffResultFormat.dd)
            {
                TimeSpan ts = max - min;
                return new int[] { ts.Days };
            }
            if (drf == DiffResultFormat.mm)
            {
                return new int[] { month + year * 12 };
            }
            if (drf == DiffResultFormat.yy)
            {
                return new int[] { year };
            }
            return new int[] { year, month };
            #endregion
        }
    }

    /// <summary>
    /// 關于返回值形式的枚舉
    /// </summary>
    public enum DiffResultFormat
    {
        /// <summary>
        /// 年數和月數
        /// </summary>
        yymm,
        /// <summary>
        /// 年數
        /// </summary>
        yy,
        /// <summary>
        /// 月數
        /// </summary>
        mm,
        /// <summary>
        /// 天數
        /// </summary>
        dd,
    }
}
