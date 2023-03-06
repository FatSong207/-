import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
   * 文章，通知公告分頁查詢
   * @param {查詢條件} data
   */
export function getArticlenewsListWithPager(data) {
  return http.request({
    url: 'Articlenews/FindWithPagerAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiCMSUrl // 直接通過覆蓋的方式
  })
}/**
   * 獲取所有可用的文章，通知公告
   */
export function getAllArticlenewsList() {
  return http.request({
    url: 'Articlenews/GetAllEnable',
    method: 'get',
    baseURL: defaultSettings.apiCMSUrl // 直接通過覆蓋的方式
  })
}
/**
   * 新增或修改保存文章，通知公告
   * @param data
   */
export function saveArticlenews(data, url) {
  return http.request({
    url: url,
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiCMSUrl // 直接通過覆蓋的方式
  })
}
/**
   * 獲取文章，通知公告詳情
   * @param {Id} 文章，通知公告Id
   */
export function getArticlenewsDetail(id) {
  return http({
    url: 'Articlenews/GetById',
    method: 'get',
    params: { id: id },
    baseURL: defaultSettings.apiCMSUrl // 直接通過覆蓋的方式
  })
}
/**
   * 批量設置啟用狀態
   * @param {id集合} ids
   */
export function setArticlenewsEnable(data) {
  return http({
    url: 'Articlenews/SetEnabledMarktBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiCMSUrl // 直接通過覆蓋的方式
  })
}
/**
   * 批量軟刪除
   * @param {id集合} ids
   */
export function deleteSoftArticlenews(data) {
  return http({
    url: 'Articlenews/DeleteSoftBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiCMSUrl // 直接通過覆蓋的方式
  })
}

/**
   * 批量刪除
   * @param {id集合} ids
   */
export function deleteArticlenews(data) {
  return http({
    url: 'Articlenews/DeleteBatchAsync',
    method: 'delete',
    data: data,
    baseURL: defaultSettings.apiCMSUrl // 直接通過覆蓋的方式
  })
}
