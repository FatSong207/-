import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
   * 新增或修改保存
   * @param data
   */
export function saveRemittanceL(data, url) {
  return http.request({
    url: url,
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 獲取詳情
   * @param {Id} Id
   */
export function getRemittanceLDetail(id) {
  return http({
    url: 'RemittanceL/GetById',
    method: 'get',
    params: { id: id },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 獲取詳情
   * @param {Id} Id
   */
export function GetRemittancesByIDNo(id) {
  return http({
    url: 'RemittanceL/GetRemittancesByIDNo',
    method: 'get',
    params: { idNo: id },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 獲取詳情
   * @param {Id} Id
   */
export function GetRemittancesByCustomerLId(id) {
  return http({
    url: 'RemittanceL/GetRemittancesByCustomerLId',
    method: 'get',
    params: { CustomerLId: id },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 批量刪除
   * @param {id集合} ids
   */
export function deleteRemittanceL(data) {
  return http({
    url: 'RemittanceL/DeleteBatchAsync',
    method: 'delete',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
