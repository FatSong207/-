import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
   * 建物廣告管理分頁查詢
   * @param {查詢條件} data
   */
export function getBuildingAdvertisementListWithPager(data) {
  return http.request({
    url: 'BuildingAdvertisement/FindWithPagerAsync2',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 獲取所有可用的建物廣告管理
   */
export function getAllBuildingAdvertisementList() {
  return http.request({
    url: 'BuildingAdvertisement/GetAllEnable',
    method: 'get',
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 新增或修改保存建物廣告管理
   * @param data
   */
export function saveBuildingAdvertisement(data, url) {
  return http.request({
    url: url,
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 獲取建物廣告管理詳情
   * @param {Id} 建物廣告管理Id
   */
export function getBuildingAdvertisementDetail(id) {
  return http({
    url: 'BuildingAdvertisement/GetById',
    method: 'get',
    params: { id: id },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 批量設置啟用狀態
   * @param {id集合} ids
   */
export function setBuildingAdvertisementEnable(data) {
  return http({
    url: 'BuildingAdvertisement/SetEnabledMarktBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 批量軟刪除
   * @param {id集合} ids
   */
export function deleteSoftBuildingAdvertisement(data) {
  return http({
    url: 'BuildingAdvertisement/DeleteSoftBatchAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 批量刪除
   * @param {id集合} ids
   */
export function deleteBuildingAdvertisement(data) {
  return http({
    url: 'BuildingAdvertisement/DeleteBatchAsync',
    method: 'delete',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 更新建物廣告狀態
   * @param {建物廣告InputDTO} data
   */
export function updateBAStatus(data) {
  return http({
    url: 'BuildingAdvertisement/UpdateBAStatus',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 更新建物廣告網址
   * @param {建物廣告InputDTO} data
   */
export function updateBAURL(data) {
  return http({
    url: 'BuildingAdvertisement/UpdateBAURL',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
