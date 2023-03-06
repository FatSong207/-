import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
 * 根據公司名稱+地址+電話獲取SLMA資料
*/
// export function GetDataByCAT(data) {
//   return http.request({
//     url: 'SLMA/GetDataByCAT',
//     method: 'post',
//     data: data,
//     baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
//   })
// }

export function GetDataByCAT(companyName, address, tel) {
  return http.request({
    url: 'SLMA/GetDataByCAT',
    method: 'get',
    params: { companyName: companyName, address: address, tel: tel },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
 * 根據公司名稱跟地址獲取電話資料(用於下拉選單)
*/
export function GetTelByCompanyNameAndAddress(companyName, address) {
  return http.request({
    url: 'SLMA/GetTelByCompanyNameAndAddress',
    method: 'get',
    params: { companyName: companyName, address: address },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
 * 根據公司名稱獲取地址資料(用於下拉選單)
*/
export function GetAddressByCompanyName(companyName) {
  return http.request({
    url: 'SLMA/GetAddressByCompanyName',
    method: 'get',
    params: { companyName: companyName },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
 * 獲取SLMA所有不重複的公司名稱(用於下拉選單)
*/
export function GetAllCompanyName() {
  return http.request({
    url: 'SLMA/GetAllCompanyName',
    method: 'get',
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
 * 獲取獲取二房東/管理方資訊
*/
export function getSLMAList(slid) {
  return http.request({
    url: 'SLMA/GetSLMAList',
    method: 'get',
    params: { slid: slid },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
 * 獲取獲取二房東/管理方電話
*/
export function getSLMATel(slid, address) {
  return http.request({
    url: 'SLMA/GetSLMATel',
    method: 'get',
    params: { slid: slid, address: address },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
 * 獲取租賃住宅管理人員資訊清單
*/
export function getSIList(slid, address) {
  return http.request({
    url: 'SLMA/GetSIList',
    method: 'get',
    params: { slid: slid, address: address },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
 * 獲取租賃住宅管理人員/不動產經紀人資訊清單
*/
export function getAGList(slid, address) {
  return http.request({
    url: 'SLMA/GetAGList',
    method: 'get',
    params: { slid: slid, address: address },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
 * 獲取租賃住宅管理人員資訊
*/
export function getSuperintendent(mname) {
  return http.request({
    url: 'SLMA/GetSuperintendent',
    method: 'get',
    params: { mname: mname },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
 * 獲取不動產經紀人資訊
*/
export function getAgent(sname) {
  return http.request({
    url: 'SLMA/GetAgent',
    method: 'get',
    params: { sname: sname },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 取得當前套用的csv
   */
export function downloadCurrentSLMAFile() {
  return http({
    url: 'SLMA/DownloadCurrentSLMAFile',
    method: 'get',
    baseURL: defaultSettings.apiChaochiUrl, // 直接通過覆蓋的方式
    responseType: 'blob'
  })
}
