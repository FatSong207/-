import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
   * 角色分頁查詢
   * @param {查詢條件} data
   */
export function getRoleListWithPager(data) {
  return http.request({
    url: 'Role/FindWithPagerAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}/**
   * 獲取所有可用的角色
   */
export function getAllRoleList() {
  return http.request({
    url: 'Role/GetAllEnable',
    method: 'get',
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}
/**
   * 新增或修改保存角色
   * @param data
   */
export function saveRole(data, url) {
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
export function getRoleDetail(id) {
  return http({
    url: 'Role/GetById',
    method: 'get',
    params: { id: id },
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}
/**
   * 批量設置啟用狀態
   * @param {id集合} ids
   */
export function setRoleEnable(data) {
  return http({
    url: 'Role/SetEnabledMarktBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}
/**
   * 批量軟刪除
   * @param {id集合} ids
   */
export function deleteSoftRole(data) {
  return http({
    url: 'Role/DeleteSoftBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}

/**
   * 批量刪除
   * @param {id集合} ids
   */
export function deleteRole(data) {
  return http({
    url: 'Role/DeleteBatchAsync',
    method: 'delete',
    data: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}

/**
 *獲取功能菜單樹
 * @param {roleId:角色Id} data
 */
export function getAllFunctionTree() {
  return http({
    url: 'RoleAuthorize/GetAllFunctionTree',
    method: 'get',
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}
/**
 *獲取功能菜單樹
 * @param {roleId:角色Id} data
 */
export function getRoleAuthorizeFunction(data) {
  return http({
    url: 'RoleAuthorize/GetRoleAuthorizeFunction',
    method: 'get',
    params: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}

/**
 *獲取角色可以訪問數據
 * @param {roleId:角色Id} data
 */
export function getAllRoleDataByRoleId(data) {
  return http({
    url: 'RoleData/GetAllRoleDataByRoleId',
    method: 'get',
    params: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}
/**
 * 保存角色權限
 * @param {} data
 */
export function saveRoleAuthorize(data) {
  return http.request({
    url: 'RoleAuthorize/SaveRoleAuthorize',
    method: 'post',
    data: data,
    timeout: 0,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}
