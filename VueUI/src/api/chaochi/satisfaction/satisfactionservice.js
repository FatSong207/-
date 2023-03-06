import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
   * 分頁查詢
   * @param {查詢條件} data
   */
export function getSatisfactionListWithPager(data) {
  return http.request({
    url: 'Satisfaction/FindWithPagerAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 新增或修改保存
   * @param data
   */
export function saveSatisfaction(data, url) {
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
export function getSatisfactionDetail(id) {
  return http({
    url: 'Satisfaction/GetById',
    method: 'get',
    params: { id: id },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 批量刪除
   * @param {id集合} ids
   */
export function deleteSatisfaction(data) {
  return http({
    url: 'Satisfaction/DeleteBatchAsync',
    method: 'delete',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 滿意度問卷題目定義檔下載
   * @param {查詢條件} data
   */
export function DownloadSatisfactionTopicById(id) {
  return http.request({
    url: 'Satisfaction/DownloadSatisfactionTopicById',
    method: 'get',
    params: { id: id },
    baseURL: defaultSettings.apiChaochiUrl, // 直接通過覆蓋的方式
    responseType: 'blob'
  })
}

/**
   * 生成滿意度問卷
   * @param {查詢條件} data
   */
export function GenSignUpForSatisfaction(data) {
  return http.request({
    url: 'Satisfaction/GenSignUpForSatisfaction',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 匯出XLSX
   * @param {查詢條件} data
   */
export function ExportToXLSX(eventId) {
  return http.request({
    url: 'Satisfaction/ExportToXLSX',
    method: 'get',
    params: { eventId: eventId },
    baseURL: defaultSettings.apiChaochiUrl, // 直接通過覆蓋的方式
    responseType: 'blob'
  })
}
