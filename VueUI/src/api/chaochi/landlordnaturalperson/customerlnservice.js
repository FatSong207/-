import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
   * 用戶分頁查詢
   * @param {查詢條件} data
   */
export function getCustomerLNListWithPager(data) {
  return http.request({
    url: 'CustomerLN/FindWithPagerSearchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 新增或修改保存用戶
   * @param data
   */
export function saveCustomerLN(data, url) {
  return http.request({
    url: url,
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 獲取房東自然人詳情
   * @param {Id} 用戶Id
   */
export function getCustomerLNDetail(id) {
  return http({
    url: 'CustomerLN/GetById',
    method: 'get',
    params: { id: id },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 獲取房東自然人匯款資訊
   * @param {Id} 用戶Id
   */
export function getLNRemittances(data) {
  return http({
    url: 'Remittance/FindRemittanceByBIDs',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 獲取房東自然人單一匯款資訊
   * @param {Id} 用戶Id
   */
export function getLNRemittance(id) {
  return http({
    url: 'Remittance/GetById',
    method: 'get',
    params: { id: id },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 獲取某業務的所有房東
   * @param {Id} 用戶Id
   */
export function GetlnListByCreatorUserId(creatorUserId) {
  return http({
    url: 'CustomerLN/GetListByCreatorUserId',
    method: 'get',
    params: { creatorUserId: creatorUserId },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 獲取用戶詳情
   * @param {userName} 用戶帳號
   */
export function getByUserName(userName) {
  return http({
    url: 'CustomerLN/GetByUserName',
    method: 'get',
    params: { userName: userName },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 批量設置啟用狀態
   * @param {id集合} ids
   */
export function setCustomerLNEnable(data) {
  return http({
    url: 'CustomerLN/SetEnabledMarktBatchAsync',
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
export function deleteCustomerLN(data) {
  return http({
    url: 'CustomerLN/DeleteBatchAsync',
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
   * 獲取用戶詳情
   * @param {userName} 用戶帳號
   */
export function GetPermissionUserList(name) {
  return http({
    url: 'CustomerLN/GetPermissionUserList',
    method: 'get',
    params: { name: name },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

export function GetByLNID(LNID) {
  return http({
    url: 'CustomerLN/GetByLNID',
    method: 'post',
    params: { LNID: LNID },
    baseURL: defaultSettings.apiChaochiUrl
  })
}
