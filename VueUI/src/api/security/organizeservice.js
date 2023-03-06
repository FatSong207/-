import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
 * 獲取組織下拉選單
*/
export function GetAllOrgForSelect() {
  return http.request({
    url: 'Organize/GetAllForSelect',
    method: 'get',
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}
/**
 * 獲取樹形組織機構
*/
export function getAllOrganizeTreeTable() {
  return http.request({
    url: 'Organize/GetAllOrganizeTreeTable',
    method: 'get',
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}
/**
 * 獲取當前使用者可觸碰ㄉ樹形組織機構
*/
export function GetPermissionOrganizeTreeTable() {
  return http.request({
    url: 'Organize/GetPermissionOrganizeTreeTable',
    method: 'get',
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}
/**
 * 獲取當前使用者可觸碰ㄉ組織機構適用于Select
*/
export function GetPermissionOrganizeSelect() {
  return http.request({
    url: 'Organize/GetPermissionOrganizeSelect',
    method: 'get',
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}
/**
 * 獲取當前使用者可觸碰ㄉ樹形組織機構
*/
export function GetPermissionOrganizeTreeTableBA() {
  return http.request({
    url: 'Organize/GetPermissionOrganizeTreeTableBA',
    method: 'get',
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}
/**
 * 獲取當前使用者可觸碰ㄉ樹形組織機構
*/
export function getPermissionOrganizeTreeTablePC() {
  return http.request({
    url: 'Organize/GetPermissionOrganizeTreeTablePC',
    method: 'get',
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}
/**
   * 角色分頁查詢
   * @param {查詢條件} data
   */
export function getOrganizeListWithPager(data) {
  return http.request({
    url: 'Organize/FindWithPagerAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}
/**
   * 新增或修改保存角色
   * @param data
   */
export function saveOrganize(data, url) {
  return http.request({
    url: url,
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}
/**
   * 獲取角色詳情
   * @param {Id} 角色Id
   */
export function getOrganizeDetail(id) {
  return http({
    url: 'Organize/GetById',
    method: 'get',
    params: { id: id },
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}
/**
   * 批量設置啟用狀態
   * @param {id集合} ids
   */
export function setOrganizeEnable(data) {
  return http({
    url: 'Organize/SetEnabledMarktBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}
/**
   * 批量軟刪除
   * @param {id集合} ids
   */
export function deleteSoftOrganize(data) {
  return http({
    url: 'Organize/DeleteSoftBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}

/**
   * 批量刪除
   * @param {id集合} ids
   */
export function deleteOrganize(data) {
  return http({
    url: 'Organize/DeleteBatchAsync',
    method: 'delete',
    data: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}

