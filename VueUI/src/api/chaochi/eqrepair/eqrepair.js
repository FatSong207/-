import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
   * 分頁查詢
   * @param {查詢條件} data
   */
export function getEqRepairListWithPager(data) {
  return http.request({
    url: 'EqRepair/FindWithPagerAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 新增或修改保存
   * @param data
   */
export function saveEqRepair(data, url) {
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
export function getEqRepairDetail(id) {
  return http({
    url: 'EqRepair/GetById',
    method: 'get',
    params: { id: id },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 批量刪除
   * @param {id集合} ids
   */
export function deleteEqRepair(data) {
  return http({
    url: 'EqRepair/DeleteBatchAsync',
    method: 'delete',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 展示圖檔(單一)
   * @param {userName} 用戶帳號
   */
export function getByFileName(fileName, id, guid) {
  return http({
    url: 'EqRepair/ShowImg',
    method: 'get',
    params: { fileName: fileName, id: id, guid: guid },
    baseURL: defaultSettings.apiChaochiUrl, // 直接通過覆蓋的方式
    responseType: 'blob'
  })
}

/**
   * 獲取圖片列表
   * @param {Id} 用戶Id
   */
export function Getimgslist(id) {
  return http({
    url: 'EqRepair/Getimgslist',
    method: 'get',
    params: { id: id },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
