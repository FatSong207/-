import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
   * 取得當前套用的csv
   */
export function downloadCurrentFile() {
  return http({
    url: 'Bankinfo/downloadCurrentFile',
    method: 'get',
    baseURL: defaultSettings.apiChaochiUrl, // 直接通過覆蓋的方式
    responseType: 'blob'
  })
}

/**
 * 獲取所有銀行(用於下拉選單)
*/
export function GetAllBanks() {
  return http.request({
    url: 'Bankinfo/GetAllBanks',
    method: 'get',
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
 * 獲取所有分行(用於下拉選單)
*/
export function GetAllBranchs() {
  return http.request({
    url: 'Bankinfo/GetAllBranchs',
    method: 'get',
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
 * 根據銀行名稱獲取此銀行所有分行(用於下拉選單)
*/
export function GetAllBranchByBankName(bankName) {
  return http.request({
    url: 'Bankinfo/GetAllBranchByBankName',
    method: 'get',
    params: { bankName: bankName },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
