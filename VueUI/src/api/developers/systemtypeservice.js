import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
   * 系統分頁查詢
   * @param {查詢條件} data
   */
export function getSystemTypeListWithPager(data) {
  return http.request({
    url: 'SystemType/FindWithPagerAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}/**
   * 獲取所有可用的系統
   */
export function getAllSystemTypeList() {
  return http.request({
    url: 'SystemType/GetAllEnable',
    method: 'get',
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}
/**
   * 新增或修改保存系統
   * @param data
   */
export function saveSystemType(data, url) {
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
export function getSystemTypeDetail(id) {
  return http({
    url: 'SystemType/GetById',
    method: 'get',
    params: { id: id },
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}
/**
   * 批量設置啟用狀態
   * @param {id集合} ids
   */
export function setSystemTypeEnable(data) {
  return http({
    url: 'SystemType/SetEnabledMarktBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}
/**
   * 批量軟刪除
   * @param {id集合} ids
   */
export function deleteSoftSystemType(data) {
  return http({
    url: 'SystemType/DeleteSoftBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}

/**
   * 批量刪除
   * @param {id集合} ids
   */
export function deleteSystemType(data) {
  return http({
    url: 'SystemType/DeleteBatchAsync',
    method: 'delete',
    data: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}

