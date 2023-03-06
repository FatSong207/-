import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
   * 應用分頁查詢
   * @param {查詢條件} data
   */
export function getAPPListWithPager(data) {
  return http.request({
    url: 'APP/FindWithPagerAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}/**
   * 獲取所有可用的應用
   */
export function getAllAPPList() {
  return http.request({
    url: 'APP/GetAllEnable',
    method: 'get',
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}
/**
   * 新增或修改保存應用
   * @param data
   */
export function saveAPP(data, url) {
  return http.request({
    url: url,
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}
/**
   * 獲取應用詳情
   * @param {Id} 應用Id
   */
export function getAPPDetail(id) {
  return http({
    url: 'APP/GetById',
    method: 'get',
    params: { id: id },
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}
/**
   * 批量設置啟用狀態
   * @param {id集合} ids
   */
export function setAPPEnable(data) {
  return http({
    url: 'APP/SetEnabledMarktBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}
/**
   * 批量軟刪除
   * @param {id集合} ids
   */
export function deleteSoftAPP(data) {
  return http({
    url: 'APP/DeleteSoftBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}

/**
   * 批量刪除
   * @param {id集合} ids
   */
export function deleteAPP(data) {
  return http({
    url: 'APP/DeleteBatchAsync',
    method: 'delete',
    data: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}
/**
 * 重置應用密鑰
 * @param {id} data
 */
export function resetAppSecret(data) {
  return http({
    url: 'APP/ResetAppSecret',
    method: 'get',
    params: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}

/**
 * 重置消息加密密鑰
 * @param {id} data
 */
export function resetEncodingAESKey(data) {
  return http({
    url: 'APP/ResetEncodingAESKey',
    method: 'get',
    params: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}
