const state = {
  loading: false
}

const mutations = {
  SET_TRUE: (state, view) => {
    state.loading = true
  },
  SET_FALSE: (state, view) => {
    state.loading = false
  }
}
export default {
  namespaced: true,
  state,
  mutations
}
