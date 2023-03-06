import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
 * 獲取樹形表單分類
*/
export function getAllCategoryTreeTable() {
  return http.request({
    url: 'CategoryForm/GetAllCategoryTreeTable',
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
    url: 'CategoryForm/FindWithPagerAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 新增或修改表單分類
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
   * 獲取表單分類詳情
   * @param {Id} 分類Id
   */
export function getCategoryDetail(id) {
  return http({
    url: 'CategoryForm/GetById',
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
    url: 'CategoryForm/SetEnabledMarktBatchAsync',
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
    url: 'CategoryForm/DeleteSoftBatchAsync',
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
    url: 'CategoryForm/DeleteBatchAsync',
    method: 'delete',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 取得當前套用的csv
   */
export function downloadCurrentCategoryFormFile() {
  return http({
    url: 'CategoryForm/downloadCurrentCategoryFormFile',
    method: 'get',
    baseURL: defaultSettings.apiChaochiUrl, // 直接通過覆蓋的方式
    responseType: 'blob'
  })
}
