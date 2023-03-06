/**
 * 角色權限處理
 */

import store from '@/store'
import { addClass } from 'element-ui/src/utils/dom';

export default {
  inserted(el, binding, vnode) {
    const { value } = binding
    const super_admin = 'administrators'
    const roles = store.getters && store.getters.roles
    // console.log(value)
    // console.log(roles)
    if (value && value instanceof Array && value.length > 0) {
      const roleFlag = value

      const hasRole = roles.split(',').some(role => {
        return super_admin === role || roleFlag.includes(role)
      })

      // const hasRole = roles.some(role => {
      //   return super_admin === role || roleFlag.includes(role)
      // })

      if (!hasRole) {
        // el.parentNode && el.parentNode.removeChild(el)
        addClass(el, 'is-disabled');// 遮罩層的外觀
        el.children[0].disabled = 'disabled'// 實際設定disabled進去
      }
    } else {
      throw new Error(`請設置角色權限標簽值"`)
    }
  }
}
