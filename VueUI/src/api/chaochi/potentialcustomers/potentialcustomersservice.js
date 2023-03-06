import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
   * 潛在客管理分頁查詢
   * @param {查詢條件} data
   */
export function getPotentialCustomersListWithPager(data) {
  return http.request({
    url: 'PotentialCustomers/FindWithPagerAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 新增或修改保存潛在客管理
   * @param data
   */
export function savePotentialCustomers(data, url) {
  return http.request({
    url: url,
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 獲取潛在客管理詳情
   * @param {Id} 潛在客管理Id
   */
export function getPotentialCustomersDetail(id) {
  return http({
    url: 'PotentialCustomers/GetById',
    method: 'get',
    params: { id: id },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 批量刪除
   * @param {id集合} ids
   */
export function deletePotentialCustomers(data) {
  return http({
    url: 'PotentialCustomers/DeleteBatchAsync',
    method: 'delete',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 潛在客轉正式客戶
   * @param data
   */
export function transferClient(data) {
  return http.request({
    url: 'PotentialCustomers/TransferClient',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 指派業務
   * @param data
   */
export function assignSales(data) {
  return http({
    url: 'PotentialCustomers/AssignSales',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 轉店
   * @param data
   */
export function copyPotentialCustomerData(data) {
  return http({
    url: 'PotentialCustomers/CopyPotentialCustomerData',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
