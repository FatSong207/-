import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
   * 分頁查詢
   * @param {查詢條件} data
   */
export function getSendMailInfoListWithPager(data) {
  return http.request({
    url: 'SendMailInfo/FindWithPagerAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}/**
   * 獲取所有可用的
   */
export function getAllSendMailInfoList() {
  return http.request({
    url: 'SendMailInfo/GetAllEnable',
    method: 'get',
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 新增SendMailInfo
   * @param data
   */
export function saveSendMailInfo(data, ccode) {
  return http.request({
    url: 'SendMailInfo/ComplaintInsert',
    method: 'post',
    data: data,
    params: { ccode: ccode },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 獲取詳情
   * @param {Id} Id
   */
export function getSendMailInfoDetail(id) {
  return http({
    url: 'SendMailInfo/GetById',
    method: 'get',
    params: { id: id },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 批量設置啟用狀態
   * @param {id集合} ids
   */
export function setSendMailInfoEnable(data) {
  return http({
    url: 'SendMailInfo/SetEnabledMarktBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 批量軟刪除
   * @param {id集合} ids
   */
export function deleteSoftSendMailInfo(data) {
  return http({
    url: 'SendMailInfo/DeleteSoftBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 批量刪除
   * @param {id集合} ids
   */
export function deleteSendMailInfo(data) {
  return http({
    url: 'SendMailInfo/DeleteBatchAsync',
    method: 'delete',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
