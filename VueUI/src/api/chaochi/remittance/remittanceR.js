import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
   * 新增或修改保存
   * @param data
   */
export function saveRemittanceR(data, url) {
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
export function getRemittanceRDetail(id) {
  return http({
    url: 'RemittanceR/GetById',
    method: 'get',
    params: { id: id },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 獲取詳情
   * @param {Id} Id
   */
export function GetRemittancesByIDNoR(id) {
  return http({
    url: 'RemittanceR/GetRemittancesByIDNo',
    method: 'get',
    params: { idNo: id },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 獲取詳情
   * @param {Id} Id
   */
export function GetRemittancesByCustomerRId(id) {
  return http({
    url: 'RemittanceR/GetRemittancesByCustomerRId',
    method: 'get',
    params: { CustomerLId: id },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 批量刪除
   * @param {id集合} ids
   */
export function deleteRemittanceR(data) {
  return http({
    url: 'RemittanceR/DeleteBatchAsync',
    method: 'delete',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
