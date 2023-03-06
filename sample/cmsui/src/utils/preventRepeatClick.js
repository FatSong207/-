/**
 * 防止重復點擊
 * @author yuebon.com chenqingwen
 */
export default {
  install(Vue) {
    // 防止重復點擊
    Vue.directive('preventReClick', {
      inserted(el, binding) {
        el.addEventListener('click', () => {
          if (!el.disabled) {
            el.disabled = true
            setTimeout(() => {
              el.disabled = false
            }, binding.value || 1000)
          }
        })
      }
    })
  }
}
