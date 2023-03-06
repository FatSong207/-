import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
 * 獲取樹形契約分類
*/
export function getAllCategoryTreeTable() {
  return http.request({
    url: 'CategoryContract/GetAllCategoryTreeTable',
    method: 'get',
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 分類分頁查詢
   * @param {查詢條件} data
   */
export function getCategoryListWithPager(data) {
  return http.request({
    url: 'CategoryContract/FindWithPagerAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 新增或修改契約分類
   * @param data
   */
export function saveCategory(data, url) {
  return http.request({
    url: url,
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 獲取契約分類詳情
   * @param {Id} 分類Id
   */
export function getCategoryDetail(id) {
  return http({
    url: 'CategoryContract/GetById',
    method: 'get',
    params: { id: id },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 批次設置啟用狀態
   * @param {id集合} ids
   */
export function setCategoryEnable(data) {
  return http({
    url: 'CategoryContract/SetEnabledMarktBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 批次軟刪除
   * @param {id集合} ids
   */
export function deleteSoftCategory(data) {
  return http({
    url: 'CategoryContract/DeleteSoftBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 批次刪除
   * @param {id集合} ids
   */
export function deleteCategory(data) {
  return http({
    url: 'CategoryContract/DeleteBatchAsync',
    method: 'delete',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 取得當前套用的csv
   */
export function downloadCurrentCategoryContractFile() {
  return http({
    url: 'CategoryContract/downloadCurrentCategoryContractFile',
    method: 'get',
    baseURL: defaultSettings.apiChaochiUrl, // 直接通過覆蓋的方式
    responseType: 'blob'
  })
}
