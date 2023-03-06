import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
   * 租戶分頁查詢
   * @param {查詢條件} data
   */
export function getTenantListWithPager(data) {
  return http.request({
    url: 'Tenants/Tenant/FindWithPagerAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiHostUrl // 直接通過覆蓋的方式
  })
}/**
   * 獲取所有可用的租戶
   */
export function getAllTenantList() {
  return http.request({
    url: 'Tenants/Tenant/GetAllEnable',
    method: 'get',
    baseURL: defaultSettings.apiHostUrl // 直接通過覆蓋的方式
  })
}
/**
   * 新增或修改保存租戶
   * @param data
   */
export function saveTenant(data, url) {
  return http.request({
    url: url,
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiHostUrl // 直接通過覆蓋的方式
  })
}
/**
   * 獲取租戶詳情
   * @param {Id} 租戶Id
   */
export function getTenantDetail(id) {
  return http({
    url: 'Tenants/Tenant/GetById',
    method: 'get',
    params: { id: id },
    baseURL: defaultSettings.apiHostUrl // 直接通過覆蓋的方式
  })
}
/**
   * 批量設置啟用狀態
   * @param {id集合} ids
   */
export function setTenantEnable(data) {
  return http({
    url: 'Tenants/Tenant/SetEnabledMarktBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiHostUrl // 直接通過覆蓋的方式
  })
}
/**
   * 批量軟刪除
   * @param {id集合} ids
   */
export function deleteSoftTenant(data) {
  return http({
    url: 'Tenants/Tenant/DeleteSoftBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiHostUrl // 直接通過覆蓋的方式
  })
}

/**
   * 批量刪除
   * @param {id集合} ids
   */
export function deleteTenant(data) {
  return http({
    url: 'Tenants/Tenant/DeleteBatchAsync',
    method: 'delete',
    data: data,
    baseURL: defaultSettings.apiHostUrl // 直接通過覆蓋的方式
  })
}
