import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
   * 分頁查詢
   * @param {查詢條件} data
   */
export function getReceiptListWithPager(data) {
  return http.request({
    // url: 'Receipt/FindWithPagerAsync',
    url: 'Receipt/FindWithPagerSearchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 獲取所有可用的
   */
export function getAllReceiptList() {
  return http.request({
    url: 'Receipt/GetAllEnable',
    method: 'get',
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 新增或修改保存
   * @param data
   */
export function saveReceipt(data, url) {
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
export function getReceiptDetail(id) {
  return http({
    url: 'Receipt/GetById',
    method: 'get',
    params: { id: id },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 批量設置啟用狀態
   * @param {id集合} ids
   */
export function setReceiptEnable(data) {
  return http({
    url: 'Receipt/SetEnabledMarktBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 批量軟刪除
   * @param {id集合} ids
   */
export function deleteSoftReceipt(data) {
  return http({
    url: 'Receipt/DeleteSoftBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 批量刪除
   * @param {id集合} ids
   */
export function deleteReceipt(data) {
  return http({
    url: 'Receipt/DeleteBatchAsync',
    method: 'delete',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 批量設置啟用狀態
   * @param {主鍵} id
   * @param {領收據狀態} status
   */
export function setReceiptStatus(id, status) {
  return http({
    url: 'Receipt/SetReceiptStatusAsync',
    method: 'post',
    params: { id: id, status: status },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 取簽名圖檔
   * @param {簽名檔路徑} filePath
   */
export function getImg(filePath) {
  return http({
    url: 'Receipt/GetImg',
    method: 'get',
    params: { filePath: filePath },
    baseURL: defaultSettings.apiChaochiUrl, // 直接通過覆蓋的方式
    responseType: 'blob'
  })
}

/**
   * 產生公會包租收據(B031301)
   * @param {領收據資料} data
   */
export function downloadGuildPDF1(data) {
  return http({
    url: 'Receipt/DownloadGuildPDF1',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl, // 直接通過覆蓋的方式
    responseType: 'blob'
  })
}

/**
   * 產生公會包租收據(B031301)
   * @param {領收據資料} data
   */
export function downloadGuildPDF2(data) {
  return http({
    url: 'Receipt/DownloadGuildPDF2',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl, // 直接通過覆蓋的方式
    responseType: 'blob'
  })
}
