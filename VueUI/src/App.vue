<template>
  <div id="app">
    <ModalIdle v-if="isIdle" />
    <router-view />
  </div>
</template>

<script>
import ModalIdle from '@/components/modalIdle/ModalIdle';

export default {
  name: 'App',
  components: {
    ModalIdle
  },
  computed: {
    isIdle() {
      return this.$store.state.idleVue.isIdle;
    }
  }
};
</script>

<style>
.rightbtn {
  display: flex;
  justify-content: flex-end;
  padding-top: 15px;
}
</style>
