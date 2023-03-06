import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
   * 系統分頁查詢
   * @param {查詢條件} data
   */
export function getFilterIPListWithPager(data) {
  return http.request({
    url: 'FilterIP/FindWithPagerAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}/**
   * 獲取所有可用的系統
   */
export function getAllFilterIPList() {
  return http.request({
    url: 'FilterIP/GetAllEnable',
    method: 'get',
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}
/**
   * 新增或修改保存系統
   * @param data
   */
export function saveFilterIP(data, url) {
  return http.request({
    url: url,
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}
/**
   * 獲取系統詳情
   * @param {Id} 系統Id
   */
export function getFilterIPDetail(id) {
  return http({
    url: 'FilterIP/GetById',
    method: 'get',
    params: { id: id },
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}
/**
   * 批量設置啟用狀態
   * @param {id集合} ids
   */
export function setFilterIPEnable(data) {
  return http({
    url: 'FilterIP/SetEnabledMarktBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}
/**
   * 批量軟刪除
   * @param {id集合} ids
   */
export function deleteSoftFilterIP(data) {
  return http({
    url: 'FilterIP/DeleteSoftBatchAsync',
    method: 'post',
    params: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}

/**
   * 批量刪除
   * @param {id集合} ids
   */
export function deleteFilterIP(data) {
  return http({
    url: 'FilterIP/DeleteBatchAsync',
    method: 'delete',
    data: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}

