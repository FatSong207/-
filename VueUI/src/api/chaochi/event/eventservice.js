import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
   * 賓客名單列表
   * @param {查詢條件} data
   */
export function GetGuestList(data) {
  return http.request({
    url: 'Event/GetGuestList',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 賓客清單下載
   * @param {查詢條件} data
   */
export function DownloadGuestListById(id) {
  return http.request({
    url: 'Event/DownloadGuestListById',
    method: 'get',
    params: { id: id },
    baseURL: defaultSettings.apiChaochiUrl, // 直接通過覆蓋的方式
    responseType: 'blob'
  })
}

/**
   * 調查問卷題目定義檔下載
   * @param {查詢條件} data
   */
export function DownloadQuestionnaireTopicByQCode(qcode) {
  return http.request({
    url: 'Event/DownloadQuestionnaireTopicByQCode',
    method: 'get',
    params: { qcode: qcode },
    baseURL: defaultSettings.apiChaochiUrl, // 直接通過覆蓋的方式
    responseType: 'blob'
  })
}

/**
   * 賓客清單下載
   * @param {查詢條件} data
   */
export function DownloadSatisfactionQrCodeByQCode(qcode) {
  return http.request({
    url: 'Event/DownloadSatisfactionQrCodeByQCode',
    method: 'get',
    params: { qcode: qcode },
    baseURL: defaultSettings.apiChaochiUrl, // 直接通過覆蓋的方式
    responseType: 'blob'
  })
}

/**
   * 更新調查問卷
   * @param {查詢條件} data
   */
export function UpdateQuestionnaire(data) {
  return http.request({
    url: 'Event/UpdateQuestionnaire',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 更新調查問卷
   * @param {查詢條件} data
   */
export function GenSighUp(data) {
  return http.request({
    url: 'Event/GenSighUp',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 更新人力與成本
   * @param {查詢條件} data
   */
export function UpdateCostPersonnel(data, eventId) {
  return http.request({
    url: 'Event/UpdateCostPersonnel',
    method: 'post',
    data: data,
    params: { eventId: eventId },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 分頁查詢
   * @param {查詢條件} data
   */
export function getEventListWithPager(data) {
  return http.request({
    url: 'Event/FindWithPagerSearchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 分頁查詢(Eventdashboard)
   * @param {查詢條件} data
   */
export function FindWithPagerSearchAsyncED(data) {
  return http.request({
    url: 'Event/FindWithPagerSearchAsyncED',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 獲取所有可用的
   */
export function getAllEventList() {
  return http.request({
    url: 'Event/GetAllEnable',
    method: 'get',
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 新增或修改保存
   * @param data
   */
export function saveEvent(data, url) {
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
export function getEventDetail(id) {
  return http({
    url: 'Event/GetById',
    method: 'get',
    params: { id: id },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 取得照片
   * @param {userName} 用戶帳號
   */
export function getByFileName(eventId) {
  return http({
    url: 'Event/ShowImg',
    method: 'get',
    params: { eventId: eventId },
    baseURL: defaultSettings.apiChaochiUrl, // 直接通過覆蓋的方式
    responseType: 'blob'
  })
}
/**
   * 批量刪除
   * @param {id集合} ids
   */
export function deleteEvent(data) {
  return http({
    url: 'Event/DeleteBatchAsync',
    method: 'delete',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 根據EventId帶上業務ID組成填寫網址
   * @param {查詢條件} data
   */
export function DirectToSignUpWithSalesId(eventId) {
  return http.request({
    url: 'Event/DirectToSignUpWithSalesId',
    method: 'get',
    params: { eventId: eventId },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 獲取表頭
   * @param {查詢條件} data
   */
export function GenStatistic(eventId) {
  return http.request({
    url: 'Event/GenStatistic',
    method: 'get',
    params: { eventId: eventId },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 獲取資料
   * @param {查詢條件} data
   */
export function GenStatisticTableData(eventId) {
  return http.request({
    url: 'Event/GenStatisticTableData',
    method: 'get',
    params: { eventId: eventId },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 匯出XLSX
   * @param {查詢條件} data
   */
export function ExportToXLSX(eventId) {
  return http.request({
    url: 'Event/ExportToXLSX',
    method: 'get',
    params: { eventId: eventId },
    baseURL: defaultSettings.apiChaochiUrl, // 直接通過覆蓋的方式
    responseType: 'blob'
  })
}
