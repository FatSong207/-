import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
   * 新增ComplaintNoticeMail
   * @param {查詢條件} data
   */
export function AddNoticeMail(data) {
  return http.request({
    url: 'Complaint/AddNoticeMail',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 獲取ComplaintNoticeMail列表
   * @param {查詢條件} data
   */
export function GetNoticeMailListByCCode(ccode) {
  return http.request({
    url: 'Complaint/GetNoticeMailListByCCode',
    method: 'get',
    params: { ccode: ccode },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 分頁查詢
   * @param {查詢條件} data
   */
export function getComplaintListWithPager(data) {
  return http.request({
    url: 'Complaint/FindWithPagerSearchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 獲取所有可用的
   */
export function getAllComplaintList() {
  return http.request({
    url: 'Complaint/GetAllEnable',
    method: 'get',
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 新增或修改保存
   * @param data
   */
export function saveComplaint(data, url) {
  return http.request({
    url: url,
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 送審
   * @param data
   */
export function SendTrial(data, url) {
  return http.request({
    url: 'Complaint/SendTrial',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 結案
   * @param data
   */
export function EndCase(data, url) {
  return http.request({
    url: 'Complaint/EndCase',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 獲取詳情
   * @param {Id} Id
   */
export function getComplaintDetail(id) {
  return http({
    url: 'Complaint/GetById',
    method: 'get',
    params: { id: id },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 批量刪除
   * @param {id集合} ids
   */
export function deleteComplaint(data) {
  return http({
    url: 'Complaint/DeleteComplaintNoticeMail',
    method: 'delete',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
