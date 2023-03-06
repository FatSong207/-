import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
 * 獲取token
 * @param query
 */
export function getToken(query) {
  var data = {
    'grant_type': 'client_credential',
    'appid': defaultSettings.appId,
    'secret': defaultSettings.appSecret
  }
  return http({
    url: 'Token',
    method: 'get',
    params: data,
    baseURL: defaultSettings.apiHostUrl // 直接通過覆蓋的方式
  })
}
/**
 *刷新token
 * @param {*} data
 */
export function refreshToken(data) {
  return http({
    url: 'Token/RefreshToken',
    method: 'get',
    params: data,
    baseURL: defaultSettings.apiHostUrl // 直接通過覆蓋的方式
  })
}
/**
 * 獲取系統基礎設置信息
 */
export function getSysSetting() {
  return http({
    url: 'Security/SysSetting/GetInfo',
    method: 'get',
    baseURL: defaultSettings.apiHostUrl // 直接通過覆蓋的方式
  })
}

/**
 * 獲取系統基礎設置信息
 */
export function getAllSysSetting() {
  return http({
    url: 'Security/SysSetting/GetAllInfo',
    method: 'get',
    baseURL: defaultSettings.apiHostUrl // 直接通過覆蓋的方式
  })
}
/**
 * 獲取系統信息
 */
export async function getSysInfo() {
  return http({
    url: 'Security/SysSetting/GetSysInfo',
    method: 'get',
    timeout: 0,
    baseURL: defaultSettings.apiHostUrl // 直接通過覆蓋的方式
  })
}
export function saveSysSetting(data) {
  return http({
    url: 'Security/SysSetting/Save',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiHostUrl // 直接通過覆蓋的方式
  })
}
/**
 * 獲取所有子系統
 */
export function getSubSystemList() {
  return http({
    url: 'Security/SystemType/GetSubSystemList',
    method: 'get',
    baseURL: defaultSettings.apiHostUrl // 直接通過覆蓋的方式
  })
}
/**
   * 登錄
   * @param {*} data
   */
export async function login(data) {
  var query = data
  return http({
    url: 'Login/GetCheckUser',
    method: 'post',
    data: query,
    timeout: 0,
    baseURL: defaultSettings.apiHostUrl // 直接通過覆蓋的方式
  })
}
/**
   * 忘記密碼
   * @param {*} data
   */
export async function ForgetPassword(userAccount) {
  return http({
    url: 'Login/ForgetPassword',
    method: 'get',
    params: { userAccount: userAccount },
    baseURL: defaultSettings.apiHostUrl // 直接通過覆蓋的方式
  })
}
/**
 * 獲取用戶信息
 * @returns
 */
export async function getUserInfo() {
  return http({
    url: 'Login/GetUserInfo',
    method: 'get',
    timeout: 0,
    baseURL: defaultSettings.apiHostUrl // 直接通過覆蓋的方式
  })
}
/**
 * 保存修改密碼
 * @param data 密碼
 * @returns
 */
export function savePassword(data) {
  var query = data
  return http({
    url: 'Security/User/ModifyPassword',
    method: 'post',
    params: query,
    baseURL: defaultSettings.apiHostUrl // 直接通過覆蓋的方式
  })
}
/**
 * 清除緩存
 * @returns
 */
export function clearCache() {
  return http({
    url: 'Security/User/ClearCache',
    method: 'get',
    baseURL: defaultSettings.apiHostUrl // 直接通過覆蓋的方式
  })
}
/**
   * 退出登錄
   */
export function logout() {
  return http({
    url: 'Login/Logout',
    method: 'get',
    baseURL: defaultSettings.apiHostUrl // 直接通過覆蓋的方式
  })
}

/**
   * 系統切換
   * @param ids id集合
   */
export function yuebonConnecSys(data) {
  return http({
    url: 'SystemType/YuebonConnecSys',
    method: 'get',
    params: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}
/**
   * 系統切換sso
   * @param ids id集合
   */
export function sysConnect(data) {
  return http({
    url: 'Login/SysConnect',
    method: 'get',
    params: data,
    baseURL: defaultSettings.apiHostUrl // 直接通過覆蓋的方式
  })
}
/**
 * 根據字典編碼獲取字典內容
 * @param  code 字典編碼
 * @returns
 */
export function getListItemDetailsByCode(code) {
  var data = {
    itemCode: code
  }
  return http({
    url: 'Security/Items/GetListByItemCode',
    method: 'get',
    params: data,
    baseURL: defaultSettings.apiHostUrl // 直接通過覆蓋的方式
  })
}
/**
 * 根據菜單功能編碼查詢該頁面操作功能
 * @param  code 字典編碼
 * @returns
 */
export function getListMeunFuntionBymeunCode(code) {
  return http({
    url: 'Function/GetListByParentEnCode',
    method: 'get',
    params: { enCode: code },
    baseURL: defaultSettings.apiHostUrl
  })
}

/**
   * 獲取微信小程序二維碼
   * @param data 查詢條件
   */
export function getWxAppletQrCode(data) {
  return http.request({
    url: 'WeiXin/WxOpen/ContentWxAppletQrCode',
    method: 'post',
    params: data,
    baseURL: defaultSettings.apiHostUrl// 直接通過覆蓋的方式
  })
}

/**
   * 獲取驗證碼
   * @param data 查詢條件
   */
export function getVerifyCode() {
  return http.request({
    url: 'Captcha',
    method: 'get',
    baseURL: defaultSettings.apiHostUrl// 直接通過覆蓋的方式
  })
}

/**
 * 上傳文件
 * @returns
 */
export function UploadFile(formData) {
  return http.request({
    url: 'Files/Upload',
    method: 'post',
    data: formData,
    baseURL: defaultSettings.apiHostUrl // 直接通過覆蓋的方式

  })
}
