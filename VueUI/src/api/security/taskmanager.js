import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
   * 分頁查詢
   * @param {查詢條件} data
   */
export function getTaskManagerListWithPager(data) {
  return http.request({
    url: 'TaskManager/FindWithPagerAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}/**
   * 獲取所有可用的
   */
export function getAllTaskManagerList() {
  return http.request({
    url: 'TaskManager/GetAllEnable',
    method: 'get',
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}
/**
   * 新增或修改保存
   * @param data
   */
export function saveTaskManager(data, url) {
  return http.request({
    url: url,
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}
/**
   * 獲取詳情
   * @param {Id} Id
   */
export function getTaskManagerDetail(id) {
  return http({
    url: 'TaskManager/GetById',
    method: 'get',
    params: { id: id },
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}
/**
   * 批量設置啟用狀態
   * @param {id集合} ids
   */
export function setTaskManagerEnable(data) {
  return http({
    url: 'TaskManager/SetEnabledMarktBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}
/**
   * 批量軟刪除
   * @param {id集合} ids
   */
export function deleteSoftTaskManager(data) {
  return http({
    url: 'TaskManager/DeleteSoftBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}

/**
   * 批量刪除
   * @param {id集合} ids
   */
export function deleteTaskManager(data) {
  return http({
    url: 'TaskManager/DeleteBatchAsync',
    method: 'delete',
    data: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}

/**
 * 啟動/暫停
 * @param  data
 */
export function changeStatus(data) {
  return http({
    url: 'TaskManager/ChangeStatus',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}

/**
   * 獲取本地任務
   */
export function getLocalTaskJobs() {
  return http({
    url: 'TaskManager/GetLocalHandlers',
    method: 'get',
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}

/**
   * 分頁查詢
   * @param {查詢條件} data
   */
export function getTaskJobLogListWithPager(data) {
  return http.request({
    url: 'TaskJobsLog/FindWithByTaskIdAsync',
    method: 'get',
    params: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}
