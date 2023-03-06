import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
   * 分頁查詢
   * @param {查詢條件} data
   */
export function getBlackListListWithPager(data) {
  return http.request({
    url: 'BlackList/FindWithPagerAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}/**
   * 獲取所有可用的
   */
export function getAllBlackListList() {
  return http.request({
    url: 'BlackList/GetAllEnable',
    method: 'get',
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 新增或修改保存
   * @param data
   */
export function saveBlackList(data, url) {
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
export function getBlackListDetail(id) {
  return http({
    url: 'BlackList/GetById',
    method: 'get',
    params: { id: id },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 批量設置啟用狀態
   * @param {id集合} ids
   */
export function setBlackListEnable(data) {
  return http({
    url: 'BlackList/SetEnabledMarktBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 批量軟刪除
   * @param {id集合} ids
   */
export function deleteSoftBlackList(data) {
  return http({
    url: 'BlackList/DeleteSoftBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 批量刪除
   * @param {id集合} ids
   */
export function deleteBlackList(data) {
  return http({
    url: 'BlackList/DeleteBatchAsync',
    method: 'delete',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
