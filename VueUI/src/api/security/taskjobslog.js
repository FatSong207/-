import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
   * 定時任務執行日志分頁查詢
   * @param {查詢條件} data
   */
export function getTaskJobsLogListWithPager(data) {
  return http.request({
    url: 'TaskJobsLog/FindWithPagerAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}/**
   * 獲取所有可用的定時任務執行日志
   */
export function getAllTaskJobsLogList() {
  return http.request({
    url: 'TaskJobsLog/GetAllEnable',
    method: 'get',
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}
/**
   * 新增或修改保存定時任務執行日志
   * @param data
   */
export function saveTaskJobsLog(data, url) {
  return http.request({
    url: url,
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}
/**
   * 獲取定時任務執行日志詳情
   * @param {Id} 定時任務執行日志Id
   */
export function getTaskJobsLogDetail(id) {
  return http({
    url: 'TaskJobsLog/GetById',
    method: 'get',
    params: { id: id },
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}
/**
   * 批量設置啟用狀態
   * @param {id集合} ids
   */
export function setTaskJobsLogEnable(data) {
  return http({
    url: 'TaskJobsLog/SetEnabledMarktBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}
/**
   * 批量軟刪除
   * @param {id集合} ids
   */
export function deleteSoftTaskJobsLog(data) {
  return http({
    url: 'TaskJobsLog/DeleteSoftBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}

/**
   * 批量刪除
   * @param {id集合} ids
   */
export function deleteTaskJobsLog(data) {
  return http({
    url: 'TaskJobsLog/DeleteBatchAsync',
    method: 'delete',
    data: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}
