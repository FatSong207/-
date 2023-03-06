import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
   * 系統分頁查詢
   * @param {查詢條件} data
   */
export function getUploadFileListWithPager(data) {
  return http.request({
    url: 'UploadFile/FindWithPagerAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}

/**
   * 批量刪除
   * @param {id集合} ids
   */
export function deleteUploadFile(data) {
  return http({
    url: 'UploadFile/DeleteBatchAsync',
    method: 'delete',
    data: data,
    baseURL: defaultSettings.apiSecurityUrl // 直接通過覆蓋的方式
  })
}

