import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
 * 獲取token
 * @param {} query
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
    method: 'get',
    params: query,
    timeout: 0,
    baseURL: defaultSettings.apiHostUrl // 直接通過覆蓋的方式
  })
}
export async function getUserInfo() {
  return http({
    url: 'Login/GetUserInfo',
    method: 'get',
    timeout: 0,
    baseURL: defaultSettings.apiHostUrl // 直接通過覆蓋的方式
  })
}
export function savePassword(data) {
  var query = data
  return http({
    url: 'Security/User/ModifyPassword',
    method: 'post',
    params: query,
    baseURL: defaultSettings.apiHostUrl // 直接通過覆蓋的方式
  })
}
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
   * @param {id集合} ids
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
   * @param {id集合} ids
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
   */
export function getListItemDetailsByCode(code) {
  return http({
    url: 'Security/Items/GetListByItemCode?itemCode=' + code,
    method: 'get',
    baseURL: defaultSettings.apiHostUrl // 直接通過覆蓋的方式
  })
}
/**
   * 根據菜單功能編碼查詢該頁面操作功能
   */
export function getListMeunFuntionBymeunCode(code) {
  return http({
    url: 'Function/GetListByParentEnCode?enCode=' + code,
    method: 'get',
    baseURL: defaultSettings.apiHostUrl
  })
}

/**
   * 獲取微信小程序二維碼
   * @param {查詢條件} data
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
   * @param {查詢條件} data
   */
export function getVerifyCode() {
  return http.request({
    url: 'Captcha',
    method: 'get',
    baseURL: defaultSettings.apiHostUrl// 直接通過覆蓋的方式
  })
}
