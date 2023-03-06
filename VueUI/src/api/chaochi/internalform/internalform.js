import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
   * 下載
   * @param {查詢條件} data
   */
export function DownloadWithData(data) {
  return http.request({
    url: 'Internalform/DownloadWithData',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 上傳
   * @param {查詢條件} data
   */
export function UploadWithData(formId, data) {
  return http.request({
    url: 'Internalform/UploadWithData',
    method: 'post',
    params: { formId: formId },
    data: data,
    baseURL: defaultSettings.apiChaochiUrl, // 直接通過覆蓋的方式
    timeout: 50000
  })
}

/**
   * 上傳
   * @param {查詢條件} data
   */
export function downloadPDFWithDataByFormId(data) {
  return http.request({
    url: 'Internalform/downloadPDFWithDataByFormId',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl, // 直接通過覆蓋的方式
    responseType: 'blob'
  })
}
/**
   * 檢查承租人是否存在
   * @param {查詢條件} data
   */
export function CheckCustomerRNExists(rId) {
  return http.request({
    url: 'Internalform/CheckCustomerRNExists',
    method: 'post',
    params: { rId: rId },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 檢查出租人是否存在
   * @param {查詢條件} data
   */
export function CheckCustomerLNExists(lId) {
  return http.request({
    url: 'Internalform/CheckCustomerLNExists',
    method: 'post',
    params: { lId: lId },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 檢查建物是否存在
   * @param {查詢條件} data
   */
export function CheckCustomerBbuildingExists(bAdd) {
  return http.request({
    url: 'Internalform/CheckCustomerBbuildingExists',
    method: 'post',
    params: { bAdd: bAdd },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 上傳pdf
   * @param {查詢條件} data
   */
export function ImgUpload(data) {
  return http.request({
    url: 'Internalform/ImgUpload',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 下載PDF(簽名檔已歸檔只能下載歷史檔案)
   * @param {查詢條件} data
   */
export function downloadWebFormHistoryFormById(data) {
  return http.request({
    url: 'Internalform/downloadWebFormHistoryFormById',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl, // 直接通過覆蓋的方式
    responseType: 'blob'
  })
}
/**
   * 內部表單分頁查詢
   * @param {查詢條件} data
   */
export function getInternalformListWithPager(data) {
  return http.request({
    url: 'Internalform/FindFormList',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 獲取所有可用的內部表單
   */
export function getAllInternalformList() {
  return http.request({
    url: 'Internalform/GetAllEnable',
    method: 'get',
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 新增或修改保存內部表單
   * @param data
   */
export function saveInternalform(data, url) {
  return http.request({
    url: url,
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 獲取內部表單詳情
   * @param {Id} 內部表單Id
   */
export function getInternalformDetail(id) {
  return http({
    url: 'Internalform/GetById',
    method: 'get',
    params: { id: id },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 批量設置啟用狀態
   * @param {id集合} ids
   */
export function setInternalformEnable(data) {
  return http({
    url: 'Internalform/SetEnabledMarktBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 批量軟刪除
   * @param {id集合} ids
   */
export function deleteSoftInternalform(data) {
  return http({
    url: 'Internalform/DeleteSoftBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 批量刪除
   * @param {id集合} ids
   */
export function deleteInternalform(data) {
  return http({
    url: 'Internalform/DeleteBatchAsync',
    method: 'delete',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
