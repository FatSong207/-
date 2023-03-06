/**
 * 小寫轉大寫
 */
export default {
  install(Vue) {
    Vue.directive('upperCase', {
      inserted: function(el, binding, vnode) {
        const handler = function(e) {
          e.target.value = e.target.value.toUpperCase()
        }
        el.addEventListener('input', handler, true)
      }
    })
  }
}
