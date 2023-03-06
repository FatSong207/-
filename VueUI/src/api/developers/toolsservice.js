import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
   * 創建數據庫連接
   */
export function createGetDBConn(data) {
  return http({
    url: 'CodeGenerator/CreateDBConn',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiHostUrl // 直接通過覆蓋的方式
  })
}
/**
   * 獲取數據庫
   */
export function codeGetDBList() {
  return http({
    url: 'CodeGenerator/GetListDataBase',
    method: 'get',
    baseURL: defaultSettings.apiHostUrl // 直接通過覆蓋的方式
  })
}
/**
   * 獲取數據庫表
   */
export function codeGetTableList(data) {
  return http({
    url: 'CodeGenerator/FindListTable',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiHostUrl // 直接通過覆蓋的方式
  })
}
/**
   * 生成代碼
   */
export async function codeGenerator(data) {
  return await http({
    url: 'CodeGenerator/Generate',
    method: 'get',
    params: data,
    timeout: 0,
    baseURL: defaultSettings.apiHostUrl // 直接通過覆蓋的方式
  })
}
/**
 *
* 數據庫解密
*/
export function dbtoolsConnStrDecrypt(data) {
  return http({
    url: 'DbTools/ConnStrDecrypt',
    method: 'post',
    params: data,
    baseURL: defaultSettings.apiHostUrl // 直接通過覆蓋的方式
  })
}
/**
   * 數據庫加密
   */
export function dbtoolsConnStrEncrypt(data) {
  return http({
    url: 'DbTools/ConnStrEncrypt',
    method: 'post',
    params: data,
    baseURL: defaultSettings.apiHostUrl // 直接通過覆蓋的方式
  })
}
