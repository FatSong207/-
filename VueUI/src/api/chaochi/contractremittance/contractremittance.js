import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
   * 取得當前套用的csv
   */
export function downloadCurrentFile() {
  return http({
    url: 'ContractRemittance/downloadCurrentFile',
    method: 'get',
    baseURL: defaultSettings.apiChaochiUrl, // 直接通過覆蓋的方式
    responseType: 'blob'
  })
}

/**
   * 獲取戶名
   * @param {type} 社宅或一般宅
   */
export function getAccountNameByType(type) {
  return http({
    url: 'ContractRemittance/GetAccountNameByType',
    method: 'get',
    params: { type: type },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 獲取使用單位
   * @param {type} 社宅或一般宅
   * @param {accountName} 戶名
   */
export function getUseCountyByAccountName(type, accountName) {
  return http({
    url: 'ContractRemittance/GetUseCountyByAccountName',
    method: 'get',
    params: { type: type, accountName: accountName },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 獲取銀行名稱
   * @param {type} 社宅或一般宅
   * @param {accountName} 戶名
   * @param {useCounty} 使用單位
   */
export function getBankNameByUseCounty(type, accountName, useCounty) {
  return http({
    url: 'ContractRemittance/GetBankNameByUseCounty',
    method: 'get',
    params: { type: type, accountName: accountName, useCounty: useCounty },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
