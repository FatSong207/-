import axios from 'axios'
import router from '../router'

import { MessageBox, Message } from 'element-ui'
import store from '@/store'
import { getToken, removeToken } from '@/utils/auth'
import { resetRouter } from '@/router'
// import { sign, GetRandomString } from '@/utils/yuebon'

// create an axios instance
const service = axios.create({
  baseURL: process.env.VUE_APP_BASE_API,
  timeout: 60000 // request timeout
})

service.defaults.headers.post['Content-Type'] = 'application/json;charset=UTF-8'
// request攔截器
service.interceptors.request.use(
  config => {
    store.commit('loading/SET_TRUE')
    if (config.headers['Content-Type'] === undefined) { config.headers['Content-Type'] = 'application/json;charset=UTF-8' }
    const token = getToken()
    if (token) {
      config.headers['Authorization'] = 'Bearer ' + token
    }
    // 如果接口需要簽名, 則通過請求時,headers中傳遞sign參數true
    // const iSSign = config.headers['sign']
    // if (iSSign || iSSign === undefined) {
    //   const timeStamp = new Date().getTime().toString().substr(0, 10)
    //   const nonce = GetRandomString()
    //   config.headers['appId'] = store.getters.appId
    //   config.headers['nonce'] = nonce
    //   config.headers['timeStamp'] = timeStamp
    //   config.headers['signature'] = sign(config, nonce, timeStamp, store.getters.appSecret)
    // }
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
    store.commit('loading/SET_FALSE')
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
        return res
      } else if (res.ErrCode === '44001') {
        MessageBox.alert(res.ErrMsg, '系統提示', {
          confirmButtonText: '重新登入',
          type: 'error'
        }).then(() => {
          store.dispatch('user/logout').then(() => {
            router.push(`/login`)
            removeToken() // must remove  token  first
            resetRouter()
            location.reload()
          })
        }).catch(() => {
          store.dispatch('user/logout').then(() => {
            router.push(`/login`)
            removeToken() // must remove  token  first
            resetRouter()
            location.reload()
          })
        })
        return res
      } else if (res.ErrCode === '44002') {
        store.dispatch('user/logout').then(() => {
          router.push(`/login`)
          removeToken() // must remove  token  first
          resetRouter()
          location.reload()
          Message({
            message: res.ErrMsg || 'Error',
            type: 'error',
            duration: 5 * 1000
          })
        })
        return res
      } else {
        // console.log(res.ErrCode)
        if (!res.ErrCode) { // 傳送二進位檔案時候
          // console.log(res)
          return res
        }

        Message({
          message: res.ErrMsg || 'Error',
          type: 'error',
          duration: 5 * 1000
        })
        return Promise.reject(new Error(res.ErrMsg))
        // return res
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
