import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
   * 取得所有鄉鎮市區
   */
export function getAllTowns() {
  return http({
    url: 'OpenDataRoad/GetAllTowns',
    method: 'get',
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 取得所有街、路
   */
export function getAllStreets(city, site_id, query) {
  return http({
    url: 'OpenDataRoad/GetAllStreets',
    method: 'get',
    params: { city: city, site_id: site_id, query: query },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 根據縣市+鄉鎮市區取得所有街、路
   */
export function GetStreetsByCityTown(city, town) {
  return http({
    url: 'OpenDataRoad/GetStreetsByCityTown',
    method: 'get',
    params: { city: city, town: town },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
/**
   * 取得當前套用的csv
   */
export function downloadCurrentOpenDataRoadFile() {
  return http({
    url: 'OpenDataRoad/downloadCurrentOpenDataRoadFile',
    method: 'get',
    baseURL: defaultSettings.apiChaochiUrl, // 直接通過覆蓋的方式
    responseType: 'blob'
  })
}
