import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
   * 分頁查詢
   * @param {查詢條件} data
   */
export function getContractformListWithPager(data) {
  return http.request({
    url: 'Contractform/FindWithPagerAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 新增或修改保存
   * @param data
   */
export function saveContractform(data, url) {
  return http.request({
    url: url,
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 獲取詳情
   * @param {Id} Id
   */
export function getContractformDetail(id) {
  return http({
    url: 'Contractform/GetById',
    method: 'get',
    params: { id: id },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 批量刪除
   * @param {id集合} ids
   */
export function deleteContractform(data) {
  return http({
    url: 'Contractform/DeleteBatchAsync',
    method: 'delete',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 獲取用戶詳情
   * @param {userName} 用戶帳號
   */
export function downloadPDFTemplateByFormId(FormId) {
  return http({
    url: 'Contractform/downloadPDFTemplateByFormId',
    method: 'get',
    params: { FormId: FormId },
    baseURL: defaultSettings.apiChaochiUrl, // 直接通過覆蓋的方式
    responseType: 'blob'
  })
}

