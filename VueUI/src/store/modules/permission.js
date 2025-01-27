﻿import { constantRoutes } from '@/router'
import { GetBPropoties } from '@/api/chaochi/building/buildingservice'
import Layout from '@/layout/index'
import ParentView from '@/components/ParentView'

const permission = {
  state: {
    routes: [],
    addRoutes: [],
    sidebarRouters: [],
    BPropoties: []
  },
  mutations: {
    SET_ROUTES: (state, routes) => {
      state.addRoutes = routes
      state.routes = constantRoutes.concat(routes)
    },
    SET_SIDEBAR_ROUTERS: (state, routers) => {
      state.sidebarRouters = constantRoutes.concat(routers)
    },
    SET_BPropoties: (state, data) => {
      state.BPropoties = data
    }
  },
  actions: {
    GetBPropoties({ commit }) {
      return new Promise(resolve => {
        GetBPropoties().then(res => {
          if (res.Success) {
            commit('SET_BPropoties', res.ResData)
          }
        })
      })
    },
    // 生成路由
    GenerateRoutes({ commit }, meuns) {
      return new Promise(resolve => {
        var menuList = JSON.stringify(meuns)
        const sdata = JSON.parse(menuList)
        const rdata = JSON.parse(menuList)
        const sidebarRoutes = filterAsyncRouter(sdata)
        const rewriteRoutes = filterAsyncRouter(rdata, true)
        rewriteRoutes.push({ path: '*', redirect: '/404', hidden: true })
        commit('SET_ROUTES', rewriteRoutes)
        commit('SET_SIDEBAR_ROUTERS', sidebarRoutes)
        resolve(rewriteRoutes)
      })
    }
  }
}
// 遍歷后臺傳來的路由字符串，轉換為組件對象
function filterAsyncRouter(asyncRouterMap, isRewrite = false) {
  return asyncRouterMap.filter(route => {
    if (isRewrite && route.children) {
      route.children = filterChildren(route.children)
    }
    if (route.component) {
      if (route.component === 'Layout') {
        route.component = Layout
      } else if (route.component === 'ParentView') { // Layout ParentView 組件特殊處理
        route.component = ParentView
      } else {
        route.component = loadView(route.component)
      }
    }
    if (route.children != null && route.children && route.children.length) {
      route.children = filterAsyncRouter(route.children, route, isRewrite)
    }
    return true
  })
}

function filterChildren(childrenMap) {
  var children = []
  childrenMap.forEach((el, index) => {
    if (el.children && el.children.length) {
      if (el.component === 'ParentView') {
        el.children.forEach(c => {
          if (c.children && c.children.length) {
            children = children.concat(filterChildren(c.children, c))
            return
          }
          children.push(c)
        })
        return
      }
    }
    children = children.concat(el)
  })
  return children
}

export const loadView = (view) => { // 路由懶加載
  return (resolve) => require([`@/views/${view}`], resolve)
}

export default permission
