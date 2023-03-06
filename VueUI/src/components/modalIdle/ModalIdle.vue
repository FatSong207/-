<template>
  <div class="overlay">
    <div class="el-message-box">
      <div class="el-message-box__header">
        <div class="el-message-box__title">
          <span>自動登出</span>
        </div>
        <button type="button" aria-label="Close" class="el-message-box__headerbtn">
          <i class="el-message-box__close el-icon-close" />
        </button>
      </div>
      <div class="el-message-box__content">
        <div class="el-message-box__container">
          <div class="el-message-box__message">
            <p>
              <span>{{ IDLE_MIN }}分鐘內無任何動作</span>
            </p>
            <p>
              <i style="color: teal;">{{ time/1000 }} 秒後自動登出 回到首頁</i>
            </p>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>

import { removeToken } from '@/utils/auth'
import { resetRouter } from '@/router'

export default {
  data() {
    return {
      time: 10000,
      IDLE_MIN: process.env.VUE_APP_IDLE_MIN
    }
  },

  created() {
    const timerId = setInterval(() => {
      this.time -= 1000

      if (!this.$store.state.idleVue.isIdle) clearInterval(timerId)

      if (this.time < 1) {
        clearInterval(timerId)
        this.logout()
      }
    }, 1000)
  },
  methods: {
    logout() {
    //   this.$Spin.show()
      this.$store.dispatch('user/logout').then(() => {
        this.$router.push(`/login?redirect=${this.$route.fullPath}`)
        removeToken() // must remove  token  first
        resetRouter()
        location.reload()
      })
    }
  }
}
</script>

<style scoped>
.overlay {
  position: fixed;
  top: 0;
  right: 0;
  bottom: 0;
  left: 0;
  background-color: rgba(0, 0, 0, 0.3);
  z-index: 9999;
  display: flex;
  align-items: center;
  justify-content: center;
}
</style>
