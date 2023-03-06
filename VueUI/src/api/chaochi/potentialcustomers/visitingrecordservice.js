import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
   * 業務拜訪記錄分頁查詢
   * @param {查詢條件} data
   */
export function getVisitingRecordListWithPager(data) {
  return http.request({
    url: 'VisitingRecord/FindWithPagerAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 新增或修改保存業務拜訪記錄
   * @param data
   */
export function saveVisitingRecord(data, url) {
  return http.request({
    url: url,
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 獲取業務拜訪記錄詳情
   * @param {Id} 業務拜訪記錄Id
   */
export function getVisitingRecordDetail(id) {
  return http({
    url: 'VisitingRecord/GetById',
    method: 'get',
    params: { id: id },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 批量刪除
   * @param {id集合} ids
   */
export function deleteVisitingRecord(data) {
  return http({
    url: 'PotentialCustomers/DeleteVRBatchAsync',
    method: 'delete',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
