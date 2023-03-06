import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
   * LN歷史表單分頁查詢
   * @param {查詢條件} data
   */
export function getLNHistoryFormListWithPager(data) {
  return http.request({
    url: 'HistoryForm/FindLNWithPagerSearchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * LN下載
   * @param {查詢條件} data
   */
export function downLoadLNForm(id, curCid) {
  return http.request({
    url: 'HistoryForm/downloadLNHistoryFormById',
    method: 'get',
    params: { id: id, curCid: curCid },
    baseURL: defaultSettings.apiChaochiUrl, // 直接通過覆蓋的方式
    responseType: 'blob'
  })
}

/**
   * LN歷史表單批量刪除
   * @param {id集合} ids
   */
export function DeleteLNHistoryForm(data) {
  return http({
    url: 'HistoryForm/DeleteLNHistoryForm',
    method: 'delete',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 根據ID獲取LN的檔名
   * @param {查詢條件} data
   */
export function GetLNFileNameById(id) {
  return http.request({
    url: 'HistoryForm/GetLNFileNameById',
    method: 'get',
    params: { id: id },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * LC歷史表單分頁查詢
   * @param {查詢條件} data
   */
export function getLCHistoryFormListWithPager(data) {
  return http.request({
    url: 'HistoryForm/FindLCWithPagerSearchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * LC下載
   * @param {查詢條件} data
   */
export function downLoadLCForm(id, curCid) {
  return http.request({
    url: 'HistoryForm/downloadLcHistoryFormById',
    method: 'get',
    params: { id: id, curCid: curCid },
    baseURL: defaultSettings.apiChaochiUrl, // 直接通過覆蓋的方式
    responseType: 'blob'
  })
}

/**
   * LC歷史表單批量刪除
   * @param {id集合} ids
   */
export function DeleteLCHistoryForm(data) {
  return http({
    url: 'HistoryForm/DeleteLCHistoryForm',
    method: 'delete',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 根據ID獲取LC的檔名
   * @param {查詢條件} data
   */
export function GetLCFileNameById(id) {
  return http.request({
    url: 'HistoryForm/GetLCFileNameById',
    method: 'get',
    params: { id: id },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * RN歷史表單分頁查詢
   * @param {查詢條件} data
   */
export function getRNHistoryFormListWithPager(data) {
  return http.request({
    url: 'HistoryForm/FindRNWithPagerSearchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * RN下載
   * @param {查詢條件} data
   */
export function downLoadRNForm(id, curCid) {
  return http.request({
    url: 'HistoryForm/downloadRNHistoryFormById',
    method: 'get',
    params: { id: id, curCid: curCid },
    baseURL: defaultSettings.apiChaochiUrl, // 直接通過覆蓋的方式
    responseType: 'blob'
  })
}

/**
   * RN歷史表單批量刪除
   * @param {id集合} ids
   */
export function DeleteRNHistoryForm(data) {
  return http({
    url: 'HistoryForm/DeleteRNHistoryForm',
    method: 'delete',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 根據ID獲取RN的檔名
   * @param {查詢條件} data
   */
export function GetRNFileNameById(id) {
  return http.request({
    url: 'HistoryForm/GetRNFileNameById',
    method: 'get',
    params: { id: id },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * RC歷史表單分頁查詢
   * @param {查詢條件} data
   */
export function getRCHistoryFormListWithPager(data) {
  return http.request({
    url: 'HistoryForm/FindRCWithPagerSearchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * RC下載
   * @param {查詢條件} data
   */
export function downLoadRCForm(id, curCid) {
  return http.request({
    url: 'HistoryForm/downloadRCHistoryFormById',
    method: 'get',
    params: { id: id, curCid: curCid },
    baseURL: defaultSettings.apiChaochiUrl, // 直接通過覆蓋的方式
    responseType: 'blob'
  })
}

/**
   * RC歷史表單批量刪除
   * @param {id集合} ids
   */
export function DeleteRCHistoryForm(data) {
  return http({
    url: 'HistoryForm/DeleteRCHistoryForm',
    method: 'delete',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 根據ID獲取RN的檔名
   * @param {查詢條件} data
   */
export function GetRCFileNameById(id) {
  return http.request({
    url: 'HistoryForm/GetRCFileNameById',
    method: 'get',
    params: { id: id },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * Building歷史表單分頁查詢
   * @param {查詢條件} data
   */
export function getBuildingHistoryFormListWithPager(data) {
  return http.request({
    url: 'HistoryForm/FindBuildingWithPagerSearchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * Building歷史表單批量刪除
   * @param {id集合} ids
   */
export function DeleteBuildingHistoryForm(data) {
  return http({
    url: 'HistoryForm/DeleteBuildingHistoryForm',
    method: 'delete',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * Building下載
   * @param {查詢條件} data
   */
export function downLoadBuildingForm(id, curCid) {
  return http.request({
    url: 'HistoryForm/downloadBuildingHistoryFormById',
    method: 'get',
    params: { id: id, curCid: curCid },
    baseURL: defaultSettings.apiChaochiUrl, // 直接通過覆蓋的方式
    responseType: 'blob'
  })
}
/**
   * 檢查某建物是否有建物設備表單檔案
   * @param {查詢條件} data
   */
export function CheckBuildingHasA0000801PDF(badd) {
  return http.request({
    url: 'HistoryForm/CheckBuildingHasA0000801PDF',
    method: 'get',
    params: { badd: badd },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 下載某建物最新的建物設備表單檔案
   * @param {查詢條件} data
   */
export function downloadLatestA0000801PDF(badd) {
  return http.request({
    url: 'HistoryForm/downloadLatestA0000801PDF',
    method: 'get',
    params: { badd: badd },
    baseURL: defaultSettings.apiChaochiUrl, // 直接通過覆蓋的方式
    responseType: 'blob'
  })
}
/**
   * 根據ID獲取Building的檔名
   * @param {查詢條件} data
   */
export function GetBuildingFileNameById(id) {
  return http.request({
    url: 'HistoryForm/GetBuildingFileNameById',
    method: 'get',
    params: { id: id },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
