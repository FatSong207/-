import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
   * 分頁查詢
   * @param {查詢條件} data
   */
export function getSecondLandlordListWithPager(data) {
  return http.request({
    url: 'SecondLandlord/FindWithPagerAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 獲取所有可用的
   */
export function getAllSecondLandlordList() {
  return http.request({
    url: 'SecondLandlord/GetAll',
    method: 'get',
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 新增或修改保存
   * @param data
   */
export function saveSecondLandlord(data, url) {
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
export function getSecondLandlordDetail(id) {
  return http({
    url: 'SecondLandlord/GetById',
    method: 'get',
    params: { id: id },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 批量設置啟用狀態
   * @param {id集合} ids
   */
export function setSecondLandlordEnable(data) {
  return http({
    url: 'SecondLandlord/SetEnabledMarktBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 批量軟刪除
   * @param {id集合} ids
   */
export function deleteSoftSecondLandlord(data) {
  return http({
    url: 'SecondLandlord/DeleteSoftBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 批量刪除
   * @param {id集合} ids
   */
export function deleteSecondLandlord(data) {
  return http({
    url: 'SecondLandlord/DeleteBatchAsync',
    method: 'delete',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
