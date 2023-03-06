import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
   * 行政工作日誌分頁查詢
   * @param {查詢條件} data
   */
export function getAdministrativeWorkLogListWithPager(data) {
  return http.request({
    url: 'AdministrativeWorkLog/FindWithPagerAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}/**
   * 獲取所有可用的行政工作日誌
   */
export function getAllAdministrativeWorkLogList() {
  return http.request({
    url: 'AdministrativeWorkLog/GetAllEnable',
    method: 'get',
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 新增或修改保存行政工作日誌
   * @param data
   */
export function saveAdministrativeWorkLog(data, url) {
  return http.request({
    url: url,
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 獲取行政工作日誌詳情
   * @param {Id} 行政工作日誌Id
   */
export function getAdministrativeWorkLogDetail(id) {
  return http({
    url: 'AdministrativeWorkLog/GetById',
    method: 'get',
    params: { id: id },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 批量設置啟用狀態
   * @param {id集合} ids
   */
export function setAdministrativeWorkLogEnable(data) {
  return http({
    url: 'AdministrativeWorkLog/SetEnabledMarktBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 批量軟刪除
   * @param {id集合} ids
   */
export function deleteSoftAdministrativeWorkLog(data) {
  return http({
    url: 'AdministrativeWorkLog/DeleteSoftBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 批量刪除
   * @param {id集合} ids
   */
export function deleteAdministrativeWorkLog(data) {
  return http({
    url: 'AdministrativeWorkLog/DeleteBatchAsync',
    method: 'delete',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
