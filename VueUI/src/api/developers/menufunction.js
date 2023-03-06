import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
   * 獲取功能菜單
   * @param {查詢條件} data
   */
export function getAllMenuTreeTable(data) {
  return http.request({
    url: 'Menu/GetAllMenuTreeTable',
    method: 'get',
    params: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}
/**
   * 新增或修改保存
   * @param data
   */
export function saveMenu(data, url) {
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
export function getMenuDetail(id) {
  return http({
    url: 'Menu/GetById',
    method: 'get',
    params: { id: id },
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}
/**
   * 批量設置啟用狀態
   * @param {id集合} ids
   */
export function setMenuEnable(data) {
  return http({
    url: 'Menu/SetEnabledMarktBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}
/**
   * 批量軟刪除
   * @param {id集合} ids
   */
export function deleteSoftMenu(data) {
  return http({
    url: 'Menu/DeleteSoftBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}

/**
   * 批量刪除
   * @param {id集合} ids
   */
export function deleteMenu(data) {
  return http({
    url: 'Menu/DeleteBatchAsync',
    method: 'delete',
    data: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}

/**
   * 查詢所有可用的
   * @param {查詢條件} data
   */
export function getMenuAllEnable() {
  return http.request({
    url: 'Menu/GetAllEnable',
    method: 'get',
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}

/**
   * 獲取功能菜單
   * @param {查詢條件} data
   */
export function getFunctionListWithPager(data) {
  return http.request({
    url: 'Function/FindWithPagerAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}
/**
   * 新增或修改保存
   * @param data
   */
export function saveFunction(data, url) {
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
export function getFunctionDetail(id) {
  return http({
    url: 'Function/GetById',
    method: 'get',
    params: { id: id },
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}
/**
   * 批量設置啟用狀態
   * @param {id集合} ids
   */
export function setFunctionEnable(data) {
  return http({
    url: 'Function/SetEnabledMarktBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}
/**
   * 批量軟刪除
   * @param {id集合} ids
   */
export function deleteSoftFunction(data) {
  return http({
    url: 'Function/DeleteSoftBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}

/**
   * 批量刪除
   * @param {id集合} ids
   */
export function deleteFunction(data) {
  return http({
    url: 'Function/DeleteBatchAsync',
    method: 'delete',
    data: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}

/**
   * 查詢所有可用的
   * @param {查詢條件} data
   */
export function getFunctionAllEnable() {
  return http.request({
    url: 'Function/GetAllEnable',
    method: 'get',
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}
/**
 * 根據子系統查詢所有功能
 * @param {} data
 */
export function getAllFunctionTreeTable(data) {
  return http.request({
    url: 'Function/GetAllFunctionTreeTable',
    method: 'get',
    params: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}
