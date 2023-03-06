import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
   * 取得此建物的所有Eq
   * @param {查詢條件} data
   */
export function GetEq(bAdd) {
  return http.request({
    url: 'Building/GetEq',
    method: 'get',
    params: { bAdd: bAdd },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 新增or更新建物設備
   * @param {查詢條件} data
   */
export function SaveBEq(data) {
  return http.request({
    url: 'Building/SaveBEq',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 產生PDF
   * @param {查詢條件} data
   */
export function GenPDF(id) {
  return http.request({
    url: 'Building/GenPDF',
    method: 'get',
    params: { id: id },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 用戶分頁查詢
   * @param {查詢條件} data
   */
export function getBuildingListWithPager(data) {
  return http.request({
    url: 'Building/FindWithPagerSearchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 新增或修改保存建物
   * @param data
   */
export function saveBuilding(data, landlordId, url) {
  return http.request({
    url: url,
    method: 'post',
    data: data,
    params: { landlordId: landlordId },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 新增或修改保存建物(從web表單介面新增)
   * @param data
   */
export function InsertFromWebForm(data, idNo, url) {
  return http.request({
    url: url,
    method: 'post',
    data: data,
    params: { idNo: idNo },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 修改保存出租住宅基本資料
   * @param data
   */
export function UpdateBuildingRentBasic(data) {
  return http.request({
    url: 'Building/UpdateBuildingRentBasic',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 移轉建物所屬權
   * @param data
   */
export function UpdateBuildingBelonging(data) {
  return http.request({
    url: 'Building/UpdateBuildingBelonging',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 移除建物所屬權
   * @param data
   */
export function RemoveBuildingBelonging(data) {
  return http.request({
    url: 'Building/RemoveBuildingBelonging',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 修改保存建物現況
   * @param data
   */
export function UpdateBuildingSituation(data) {
  return http.request({
    url: 'Building/UpdateBuildingSituation',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 獲取用戶詳情
   * @param {Id} 用戶Id
   */
export function getBuildingDetail(id) {
  return http({
    url: 'Building/GetById',
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
    url: 'Building/GetByUserName',
    method: 'get',
    params: { userName: userName },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 批量設置啟用狀態
   * @param {id集合} ids
   */
export function setBuildingEnable(data) {
  return http({
    url: 'Building/SetEnabledMarktBatchAsync',
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
export function deleteBuilding(data) {
  return http({
    url: 'Building/DeleteBatchAsync',
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
    url: 'Building/ImgUpload',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 獲取用戶詳情
   * @param {userName} 用戶帳號
   */
export function getByFileName(fileName, id) {
  return http({
    url: 'Building/ShowImg',
    method: 'get',
    params: { fileName: fileName, id: id },
    baseURL: defaultSettings.apiChaochiUrl, // 直接通過覆蓋的方式
    responseType: 'blob',
    timeout: 50000
  })
}

/**
   * 獲取用戶詳情
   * @param {Id} 用戶Id
   */
export function getImgInfo(data) {
  return http({
    url: 'Building/GetImgById',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 獲取用戶詳情
   * @param {Id} 用戶Id
   */
export function delImg(fileNames, bid) {
  return http({
    url: 'Building/DeleteImgFile',
    method: 'post',
    data: fileNames,
    params: { bid: bid },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 上傳pdf
   * @param {查詢條件} data
   */
export function ImgUpload(data) {
  return http.request({
    url: 'Building/ImgUpload',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 獲取建物屬性下拉
   * @param {Id} 用戶Id
   */
export function GetBPropoties() {
  return http({
    url: 'Building/GetBPropoties',
    method: 'get',
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 更新建物狀態
   * @param {主鍵} id
   * @param {建物狀態} state
   */
export function updateBState(data) {
  return http({
    url: 'Building/UpdateBState',
    method: 'post',
    // params: { ids: ids, state: state },
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
