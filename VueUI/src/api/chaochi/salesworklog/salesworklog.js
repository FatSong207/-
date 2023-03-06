import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
   * 業務工作日誌分頁查詢
   * @param {查詢條件} data
   */
export function getSalesWorkLogListWithPager(data) {
  return http.request({
    url: 'SalesWorkLog/FindWithPagerSearchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}/**
   * 獲取最近一次的資料
   */
export function GetLastDeals() {
  return http.request({
    url: 'SalesWorkLog/GetLastDeals',
    method: 'get',
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 新增或修改保存業務工作日誌
   * @param data
   */
export function saveSalesWorkLog(data, url) {
  return http.request({
    url: url,
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 獲取業務工作日誌詳情
   * @param {Id} 業務工作日誌Id
   */
export function getSalesWorkLogDetail(id) {
  return http({
    url: 'SalesWorkLog/GetById',
    method: 'get',
    params: { id: id },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 批量設置啟用狀態
   * @param {id集合} ids
   */
export function setSalesWorkLogEnable(data) {
  return http({
    url: 'SalesWorkLog/SetEnabledMarktBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 批量軟刪除
   * @param {id集合} ids
   */
export function deleteSoftSalesWorkLog(data) {
  return http({
    url: 'SalesWorkLog/DeleteSoftBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 批量刪除
   * @param {id集合} ids
   */
export function deleteSalesWorkLog(data) {
  return http({
    url: 'SalesWorkLog/DeleteBatchAsync',
    method: 'delete',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
