import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
   * 要求存取權
   * @param {查詢條件} data
   */
export function RequestAccess(data) {
  return http.request({
    url: 'RequestAccess/Request',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
