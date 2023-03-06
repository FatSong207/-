import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
   * 房客-自然人分頁查詢
   * @param {查詢條件} data
   */
export function getCustomerRNListWithPager(data) {
  return http.request({
    url: 'CustomerRN/FindWithPagerSearchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 新增或修改房客
   * @param data
   */
export function saveCustomerRN(data, url) {
  return http.request({
    url: url,
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 修改同住人
   * @param data
   */
export function UpdateRNFAsync(data) {
  return http.request({
    url: 'CustomerRN/UpdateRNFAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 獲取房客自然人詳情
   * @param {Id} 用戶Id
   */
export function getCustomerRNDetail(id) {
  return http({
    url: 'CustomerRN/GetById',
    method: 'get',
    params: { id: id },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 獲取房客自然人匯款資訊
   * @param {Id} 用戶Id
   */
export function getRNRemittances(data) {
  return http({
    url: 'Remittance/FindRemittanceByBIDs',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 獲取房客自然人單一匯款資訊
   * @param {Id} 用戶Id
   */
export function getRNRemittance(id) {
  return http({
    url: 'Remittance/GetById',
    method: 'get',
    params: { id: id },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 批量設置啟用狀態
   * @param {id集合} ids
   */
export function setCustomerRNEnable(data) {
  return http({
    url: 'CustomerRN/SetEnabledMarktBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 批次軟刪除
   * @param {id集合} ids
   */
export function deleteSoftCustomerRN(data) {
  return http({
    url: 'CustomerRN/DeleteSoftBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 批量刪除
   * @param {id集合} ids
   */
export function deleteCustomerRN(data) {
  return http({
    url: 'CustomerRN/DeleteBatchAsync',
    method: 'delete',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
export function GetByRNID(RNID) {
  return http({
    url: 'CustomerRN/GetByRNID',
    method: 'post',
    params: { RNID: RNID },
    baseURL: defaultSettings.apiChaochiUrl
  })
}
