import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
   * 分頁查詢
   * @param {查詢條件} data
   */
export function getMOBuildingListWithPager(data) {
  return http.request({
    url: 'MOBuilding/FindWithPagerAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 新增或修改保存
   * @param data
   */
export function saveMOBuilding(data, url) {
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
export function getMOBuildingDetail(id) {
  return http({
    url: 'MOBuilding/GetById',
    method: 'get',
    params: { id: id },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

// /**
//    * 刪除
//    * @param {多物件單號} moid
//    * @param {物件地址} badd
//    */
// export function deleteMOBuilding(moid, badd) {
//   return http({
//     url: 'MOBuilding/DeleteAsync',
//     method: 'delete',
//     params: { moid: moid, badd: badd },
//     baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
//   })
// }

/**
   * 批量刪除
   * @param {id集合} ids
   */
export function deleteMOBuilding(data) {
  return http({
    url: 'MOBuilding/DeleteBatchAsync',
    method: 'delete',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
