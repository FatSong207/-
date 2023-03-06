import http from '@/utils/request';
import defaultSettings from '@/settings';

/**
 * 待辦事項分頁查詢
 * @param {查詢條件} data
 */
export function getToDoListListWithPager(data) {
  return http.request({
    url: 'ToDoList/FindWithPagerAsync',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  });
}

/**
 * 新增或修改保存待辦事項
 * @param data
 */
export function saveToDoList(data, url) {
  return http.request({
    url: url,
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  });
}

/**
 * 獲取待辦事項詳情
 * @param {Id} 待辦事項Id
 */
export function getToDoListDetail(id) {
  return http({
    url: 'ToDoList/GetById2',
    method: 'get',
    params: { id: id },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  });
}

/**
 * 獲取待辦事項詳情
 * @param {typeId} 功能模組Id
 * @param {typeData} 功能模組資料
 */
export function getToDoListDetail2(typeId, typeData) {
  return http({
    url: 'ToDoList/GetByTypeData',
    method: 'get',
    params: { typeId: typeId, typeData: typeData },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  });
}

/**
 * 獲取待辦事項詳情
 * @param {typeId} 功能模組Id
 */
export function getToDoListDetail3(typeId) {
  return http({
    url: 'ToDoList/GetByTypeID',
    method: 'get',
    params: { typeId: typeId },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  });
}

/**
 * 批量刪除
 * @param data
 */
export function deleteToDoList(data) {
  return http({
    url: 'ToDoList/DeleteBatchAsync',
    method: 'delete',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  });
}
/**
 * 獲取當前用戶待辦事項數
 * @param {typeId} 功能模組Id
 */
export function GetPersonalTodoListCount(account) {
  return http({
    url: 'ToDoList/GetPersonalTodoListCount',
    method: 'get',
    params: { account: account },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  });
}

/**
 * 核准放行
 * @param data
 */
export function flowSubmit(data) {
  return http({
    url: 'ToDoList/FlowSubmit',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  });
}

/**
 * 審核黑名單
 * @param data
 */
export function BlackListSubmit(data) {
  return http({
    url: 'ToDoList/BlackListSubmit',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  });
}

// /**
//    * 駁回
//    * @param data
//    */
// export function disapproval(data) {
//   return http({
//     url: 'ToDoList/Disapproval',
//     method: 'post',
//     data: data,
//     baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
//   })
// }
