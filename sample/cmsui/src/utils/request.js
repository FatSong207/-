import axios from 'axios'
import router from '../router'

import { MessageBox, Message } from 'element-ui'
import store from '@/store'
import { getToken } from '@/utils/auth'

// create an axios instance
const service = axios.create({
  baseURL: process.env.VUE_APP_BASE_API,
  timeout: 5000 // request timeout
})

service.defaults.headers.post['Content-Type'] = 'application/json;charset=UTF-8'
// request interceptor
service.interceptors.request.use(
  config => {
    config.headers['Content-Type'] = 'application/json;charset=UTF-8'
    const token = getToken()
    if (token) {
      config.headers['Authorization'] = 'Bearer ' + token
    }
    return config
  },
  error => {
    // do something with request error
    return Promise.reject(error)
  }
)

// response interceptor
service.interceptors.response.use(
  response => {
    const res = response.data
    if (res.ErrCode !== '0') {
      if (res.ErrCode === '40005') { // 超時自動刷新token
        store.dispatch('user/resetToken').then((res) => {
          location.reload()
        })
      } else if (res.ErrCode === '40006') {
        router.push({
          path: '/403',
          query: {
            redirect: router.currentRoute.fullPath
          }
        }).catch(() => { })
      } else if (res.ErrCode === '40000' || res.ErrCode === '40008') {
      // to re-login
        MessageBox.confirm('登錄狀態已過期，您可以繼續留在該頁面，或者重新登錄', '系統提示', {
          confirmButtonText: '重新登錄',
          cancelButtonText: '取消',
          type: 'warning'
        }).then((res) => {
          store.dispatch('user/resetToken').then((res) => {
            location.reload()
          })
        })
      } else {
        Message({
          message: res.ErrMsg || 'Error',
          type: 'error',
          duration: 5 * 1000
        })
        return Promise.reject(new Error(res.ErrMsg))
      }
    } else {
      return res
    }
  },
  error => {
    console.log('err' + error)
    let { message } = error
    if (message === 'Network Error') {
      message = '后端接口連接異常'
    } else if (message.includes('timeout')) {
      message = '系統接口請求超時'
    } else if (message.includes('Request failed with status code')) {
      message = '系統接口' + message.substr(message.length - 3) + '異常'
    }
    Message({
      message: error.message,
      type: 'error',
      duration: 5 * 1000
    })
    return Promise.reject(error)
  }
)

export default service
