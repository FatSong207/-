import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
   * 系統分頁查詢
   * @param {查詢條件} data
   */
export function getLogListWithPager(data) {
  return http.request({
    url: 'Log/FindWithPagerSearchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}
/**
   * 獲取系統詳情
   * @param {Id} 系統Id
   */
export function getLogDetail(id) {
  return http({
    url: 'Log/GetById',
    method: 'get',
    params: { id: id },
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}
/**
   * 批量刪除
   * @param {id集合} ids
   */
export function deleteLog(data) {
  return http({
    url: 'Log/DeleteBatchAsync',
    method: 'delete',
    data: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}

