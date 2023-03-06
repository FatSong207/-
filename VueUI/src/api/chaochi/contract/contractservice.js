import http from '@/utils/request'
import defaultSettings from '@/settings'

/**
   * 分頁查詢
   * @param {查詢條件} data
   */
export function getContractListWithPager(data) {
  return http.request({
    url: 'Contract/FindWithPagerAsync2',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 新增合約
   * @param data
   */
export function saveContract(data, url) {
  return http.request({
    url: url,
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 獲取合約詳細資訊
   * @param {cid} cid
   */
export function getContract(cid) {
  return http({
    url: 'Contract/GetContract',
    method: 'get',
    params: { cid: cid },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 獲取合約主要附件
   * @param {cid} cid
   */
export function getMajorAttachments(cid) {
  return http({
    url: 'Contract/GetMajorAttachments',
    method: 'get',
    params: { cid: cid },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 獲取合約次要附件
   * @param {cid} cid
   */
export function getMinorAttachments(cid) {
  return http({
    url: 'Contract/GetMinorAttachments',
    method: 'get',
    params: { cid: cid },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 獲取合約歷史
   * @param {cid} cid
   */
export function getContractHistory(cid) {
  return http({
    url: 'Contract/GetContractHistory',
    method: 'get',
    params: { cid: cid },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 下載主約表單
   * @param data
   */
export function downloadContract(data) {
  return http({
    url: 'Contract/DownloadContract',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl, // 直接通過覆蓋的方式
    responseType: 'blob'
  })
}

/**
   * 下載主約表單
   * @param data
   */
export function uploadContract(data) {
  return http({
    url: 'Contract/UploadContract',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 終止合約
   * @param data
   */
export function contractInvalid(data) {
  return http({
    url: 'Contract/ContractInvalid',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 合約解約送審
   * @param data
   */
export function submitTerminateContract(data) {
  return http({
    url: 'Contract/SubmitTerminateContract',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 綁定主約必要附件
   * @param data
   */
export function bindAttachment(data) {
  return http({
    url: 'Contract/BindAttachment',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 清空綁定主約必要附件
   * @param data
   */
export function unbindAttachment(data) {
  return http({
    url: 'Contract/UnbindAttachment',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 刪除合約次要附件
   * @param data
   */
export function deleteMinorAttachment(data) {
  return http({
    url: 'Contract/DeleteMinorAttachment',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 批量刪除
   * @param {id集合} ids
   */
export function deleteContract(data) {
  return http({
    url: 'Contract/DeleteBatchAsync',
    method: 'delete',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 合約定稿送審
   * @param data
   */
export function initialContract(data) {
  return http({
    url: 'Contract/InitialContract',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 下載定稿合約
   * @param {合約資料} data
   */
export function downloadFinalizedPDF(data) {
  return http({
    url: 'Contract/DownloadFinalizedPDF',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl, // 直接通過覆蓋的方式
    responseType: 'blob'
  })
}

/**
   * 下載已簽約合約
   * @param {合約資料} data
   */
export function downloadSignedContract(data) {
  return http({
    url: 'Contract/DownloadSignedContract',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl, // 直接通過覆蓋的方式
    responseType: 'blob'
  })
}

/**
   * 合約生效
   * @param {合約資料} data
   */
export function contractEffiective(data) {
  return http({
    url: 'Contract/ContractEffiective',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 更新媒合編號
   * @param cid
   * @param mid
   */
export function updateMID(cid, mid) {
  return http({
    url: 'Contract/UpdateMID',
    method: 'post',
    params: { cid: cid, mid: mid },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 合約送審
   * @param cid
   * @param cname
   */
export function submitSignedContract(data) {
  return http({
    url: 'Contract/SubmitSignedContract',
    method: 'post',
    data: data,
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}

/**
   * 根據出租建物地址查詢分租物件單號
   * @param badd
   */
export function getMOIDByBAdd(badd) {
  return http({
    url: 'Contract/GetMOIDByBAdd',
    method: 'post',
    params: { BAdd: badd },
    baseURL: defaultSettings.apiChaochiUrl // 直接通過覆蓋的方式
  })
}
