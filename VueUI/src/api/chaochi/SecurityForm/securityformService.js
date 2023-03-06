import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
   *
   * 內部表單
   *
   */
export function FindWithPagerSearchAsync(data) {
  return http.request({
    url: 'SecurityForm/FindWithPagerSearchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 處理BAdd
   * @param {查詢條件} data
   */
export function handelAdd(data) {
  return http.request({
    url: 'SecurityForm/handelAdd',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 檢查此BAdd是否存在
   * @param {查詢條件} data
   */
export function checkExists(BAdd) {
  return http.request({
    url: 'SecurityForm/checkExistsAndPermission',
    method: 'get',
    params: { BAdd: BAdd },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 圖片列表
   * @param {查詢條件} data
   */
export function Getimgslist(BAdd) {
  return http.request({
    url: 'SecurityForm/GetimgslistC',
    method: 'get',
    params: { BAdd: BAdd },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 根據租安表單代號查詢版本號並形成list(用於下拉選單)
   * @param {查詢條件} data
   */
export function GetVNoListByFormId(formId) {
  return http.request({
    url: 'SecurityForm/GetVNoListByFormId',
    method: 'post',
    params: { formId: formId },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 圖片列表
   * @param {查詢條件} data
   */
export function GetimgslistG(BAdd) {
  return http.request({
    url: 'SecurityForm/GetimgslistG',
    method: 'get',
    params: { BAdd: BAdd },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 取得照片
   * @param {userName} 用戶帳號
   */
export function getByFileName(fileName, BAdd) {
  return http({
    url: 'SecurityForm/ShowImgC',
    method: 'get',
    params: { fileName: fileName, BAdd: BAdd },
    baseURL: defaultSettings.apiChaochiUrl, // 直接通過覆蓋的方式
    responseType: 'blob'
  })
}

/**
   * 取得照片
   * @param {userName} 用戶帳號
   */
export function getByFileNameG(fileName, BAdd) {
  return http({
    url: 'SecurityForm/ShowImgG',
    method: 'get',
    params: { fileName: fileName, BAdd: BAdd },
    baseURL: defaultSettings.apiChaochiUrl, // 直接通過覆蓋的方式
    responseType: 'blob'
  })
}

/**
   * 批量上傳照片
   * @param {userName} 用戶帳號
   */
export function ImgUpload(data) {
  return http({
    url: 'SecurityForm/ImgUpload',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 批量上傳照片
   * @param {userName} 用戶帳號
   */
export function ImgUploadG(data) {
  return http({
    url: 'SecurityForm/ImgUploadG',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 預覽PDF
   * @param {userName} 用戶帳號
   */
export function PreviewPDF(BAdd, Vno, data) {
  return http({
    url: 'SecurityForm/PreviewPDFC',
    method: 'post',
    params: { BAdd: BAdd, Vno: Vno },
    data: data,
    baseURL: defaultSettings.apiChaochiUrl, // 直接通過覆蓋的方式
    responseType: 'blob'
  })
}

/**
   * 預覽PDF
   * @param {userName} 用戶帳號
   */
export function PreviewPDFG(BAdd, Vno, data) {
  return http({
    url: 'SecurityForm/PreviewPDFG',
    method: 'post',
    params: { BAdd: BAdd, Vno: Vno },
    data: data,
    baseURL: defaultSettings.apiChaochiUrl, // 直接通過覆蓋的方式
    responseType: 'blob'
  })
}
/**
   * 產生PDF
   * @param {userName} 用戶帳號
   */
export function GenPDF(BAdd, Vno, data) {
  return http({
    url: 'SecurityForm/GenPDFC',
    method: 'post',
    params: { BAdd: BAdd, Vno: Vno },
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 產生PDF
   * @param {userName} 用戶帳號
   */
export function GenPDFG(BAdd, Vno, data) {
  return http({
    url: 'SecurityForm/GenPDFG',
    method: 'post',
    params: { BAdd: BAdd, Vno: Vno },
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 刪除圖檔
   * @param {Id} 用戶Id
   */
export function delImg(BAdd, myFileInfo) {
  return http({
    url: 'SecurityForm/DeleteImgFileC',
    method: 'post',
    params: { BAdd: BAdd },
    data: myFileInfo,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 刪除圖檔
   * @param {Id} 用戶Id
   */
export function delImgG(BAdd, myFileInfo) {
  return http({
    url: 'SecurityForm/DeleteImgFileG',
    method: 'post',
    params: { BAdd: BAdd },
    data: myFileInfo,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
