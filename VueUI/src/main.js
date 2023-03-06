import Vue from 'vue'

import Cookies from 'js-cookie'
import 'normalize.css/normalize.css' // A modern alternative to CSS resets

import ElementUI from 'element-ui'
import locale from 'element-ui/lib/locale/lang/zh-TW'
import 'element-ui/lib/theme-chalk/index.css'
// import locale from 'element-ui/lib/locale/lang/en' // lang i18n
import '@/styles/index.scss' // global css
import App from './App'
import store from './store'
import router from './router'
import permission from './directive/permission'

import '@/icons' // icon
import '@/permission' // permission control
import { resetForm } from '@/utils/index'
import preventReClick from '@/utils/preventRepeatClick.js'
import upperCase from '@/utils/upperCase.js'
import VueExpandableImage from 'vue-expandable-image'
import IdleVue from 'idle-vue'
import VueSignaturePad from 'vue-signature-pad'

const eventsHub = new Vue()

Vue.use(VueSignaturePad)

Vue.use(IdleVue, {
  eventEmitter: eventsHub,
  store,
  // idleTime: 3000, // 3秒,
  // idleTime: 60 * 60 * 1000, // 3秒,
  idleTime: process.env.VUE_APP_IDLE,
  startAtIdle: false
})

// 全局方法掛載
Vue.prototype.resetForm = resetForm

Vue.use(permission)
Vue.use(preventReClick)
Vue.use(upperCase)
Vue.use(VueExpandableImage)
/**
 * If you don't want to use mock-server
 * you want to use MockJs for mock api
 * you can execute: mockXHR()
 *
 * Currently MockJs will be used in the production environment,
 * please remove it before going online ! ! !
 */
if (process.env.NODE_ENV === 'production') {
  // const { mockXHR } = require('../mock')
  // mockXHR()
}

// set ElementUI lang to EN
// Vue.use(ElementUI, { locale })
// 如果想要中文版 element-ui，按如下方式聲明

Vue.use(ElementUI, {
  size: Cookies.get('size') || 'medium', // set element-ui default size
  locale
})
Vue.config.productionTip = false
new Vue({
  el: '#app',
  router,
  store,
  render: h => h(App)
})
