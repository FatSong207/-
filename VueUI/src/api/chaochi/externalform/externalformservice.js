import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
   * 獲取用戶詳情
   * @param {userName} 用戶帳號
   */
export function BeqExportToPDF(bAdd) {
  return http({
    url: 'Externalform/BeqExportToPDF',
    method: 'get',
    params: { bAdd: bAdd },
    baseURL: defaultSettings.apiChaochiUrl, // 直接通過覆蓋的方式
    responseType: 'blob'
  })
}
/**
   * 用戶分頁查詢
   * @param {查詢條件} data
   */
export function getExternalformListWithPager(data) {
  return http.request({
    url: 'Externalform/FindWithPagerSearchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 新增或修改保存用戶
   * @param data
   */
export function saveExternalform(data, url) {
  return http.request({
    url: url,
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 獲取用戶詳情
   * @param {Id} 用戶Id
   */
export function getExternalformDetail(id) {
  return http({
    url: 'Externalform/GetById',
    method: 'get',
    params: { id: id },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 獲取用戶詳情
   * @param {userName} 用戶帳號
   */
export function getByUserName(userName) {
  return http({
    url: 'Externalform/GetByUserName',
    method: 'get',
    params: { userName: userName },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 批量設置啟用狀態
   * @param {id集合} ids
   */
export function setExternalformEnable(data) {
  return http({
    url: 'Externalform/SetEnabledMarktBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 批量軟刪除
   * @param {id集合} ids
   */
export function deleteSoftUser(data) {
  return http({
    url: 'User/DeleteSoftBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 批量刪除
   * @param {id集合} ids
   */
export function deleteExternalform(data) {
  return http({
    url: 'Externalform/DeleteBatchAsync',
    method: 'delete',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 重置密碼
   * @param {userId:用戶id} data
   */
export function resetPassword(data) {
  return http({
    url: 'User/ResetPassword',
    method: 'post',
    params: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 修改密碼
   * @param {password:新密碼,password2:重復新密碼} data
   */
export function modifyPassword(data) {
  return http({
    url: 'User/ModifyPassword',
    method: 'post',
    params: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 註冊新用戶
   * @param data
   */
export function registerUser(data, url) {
  return http.request({
    url: 'User/Register',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 保存用戶主題配置
   * @param data
   */
export function saveThemeConfig(data) {
  return http.request({
    url: 'User/SaveUserTheme',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 上傳檔案
   * @param 檔案內容
   */
export function upload(data) {
  return http({
    url: 'Files/Upload',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 上傳檔案
   * @param 檔案內容
   */
export function imgUpload(data) {
  return http({
    url: 'Externalform/ImgUpload',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 檢查是否為無主表單
   * @param 檔案內容
   */
export function UploadPDFWithDataAsync(data) {
  return http({
    url: 'Externalform/UploadPDFWithData',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl, // 直接通過覆蓋的方式
    timeout: 60000
  })
}

/**
   * 獲取用戶詳情
   * @param {userName} 用戶帳號
   */
export function getByFileName(fileName) {
  return http({
    url: 'Externalform/ShowImg',
    method: 'get',
    params: { fileName: fileName },
    baseURL: defaultSettings.apiChaochiUrl, // 直接通過覆蓋的方式
    responseType: 'blob'
  })
}

/**
   * 獲取用戶詳情
   * @param {userName} 用戶帳號
   */
export function downloadPDFTemplateByFormId(FormId) {
  return http({
    url: 'Externalform/downloadPDFTemplateByFormId',
    method: 'get',
    params: { FormId: FormId },
    baseURL: defaultSettings.apiChaochiUrl, // 直接通過覆蓋的方式
    responseType: 'blob'
  })
}

/**
   * 獲取用戶詳情
   * @param {userName} 用戶帳號
   */
export function downloadPDFWithDataByFormId(searchPDF) {
  return http({
    url: 'Externalform/downloadPDFWithDataByFormId',
    method: 'post',
    data: searchPDF,
    baseURL: defaultSettings.apiChaochiUrl, // 直接通過覆蓋的方式
    responseType: 'blob'
  })
}

/**
   * 獲取用戶詳情
   * @param {Id} 用戶Id
   */
export function getImgInfo(id) {
  return http({
    url: 'Externalform/GetImgById',
    method: 'get',
    params: { id: id },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 獲取用戶詳情
   * @param {Id} 用戶Id
   */
export function delImg(fileName) {
  return http({
    url: 'Externalform/DeleteImgFile',
    method: 'get',
    params: { fileName: fileName },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   *
   * 內部表單
   *
   */
export function findFormList(data) {
  return http.request({
    url: 'Externalform/FindFormList',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

export function findFormData(json) {
  return http({
    url: 'Externalform/FindFormData',
    method: 'post',
    data: json,
    baseURL: defaultSettings.apiChaochiUrl, // 直接通過覆蓋的方式
    responseType: 'string'
  })
}

export function saveFomrData(json) {
  return http({
    url: 'Externalform/SaveFomrData',
    method: 'post',
    data: json,
    baseURL: defaultSettings.apiChaochiUrl, // 直接通過覆蓋的方式
    responseType: 'string'
  })
}

export function findSignature(json) {
  return http({
    url: 'Externalform/FindSignature',
    method: 'post',
    data: json,
    baseURL: defaultSettings.apiChaochiUrl, // 直接通過覆蓋的方式
    responseType: 'string'
  })
}

export function uploadSignatureImg(json) {
  return http({
    url: 'Externalform/UploadSignature',
    method: 'post',
    data: json,
    baseURL: defaultSettings.apiChaochiUrl, // 直接通過覆蓋的方式
    responseType: 'string'
  })
}

export function getImg(filePath) {
  return http({
    url: 'Externalform/GetImg',
    method: 'get',
    params: { filePath: filePath },
    baseURL: defaultSettings.apiChaochiUrl, // 直接通過覆蓋的方式
    responseType: 'blob'
  })
}

export function fileingForm(data) {
  return http.request({
    url: 'Externalform/FileingForm',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 根據ID獲取實體
   * @param {Id} 用戶Id
   */
export function GetInstance(FormId) {
  return http({
    url: 'Externalform/GetInstance',
    method: 'get',
    params: { FormId: FormId },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 取得不能顯示"下載空白表單" 的PDF FormID
   * @param {Id} 用戶Id
   */
export function GetCantNullPDF() {
  return http({
    url: 'Externalform/GetCantNullPDF',
    method: 'get',
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
// Internal form method

