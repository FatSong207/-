<template>
  <div class="app-container">
    {{ myFormId }} {{ myFormName }}
    <component :is="currentComponent" />
  </div>
</template>
<script>
import Address from '@/components/Address/Address.vue'
import A000201 from './A000201'
import NotFound from './NotFound'

export default {
  name: 'InternalformsDetailSign',
  components: {
    Address,
    A000201,
    NotFound
  },
  props: {
    searchPDF: { type: Object, default: null }
  },
  data() {
    return {
      myFormId: null,
      myFormName: null,
      currentComponent: null
    }
  },
  computed: {
    // https://stackoverflow.com/questions/42133894/vue-js-how-to-properly-watch-for-nested-data
    c_FormId() {
      if (this.$route.params.FormId) {
        // eslint-disable-next-line vue/no-side-effects-in-computed-properties
        this.myFormId = this.$route.params.FormId
        // eslint-disable-next-line vue/no-side-effects-in-computed-properties
        this.myFormName = this.$route.params.FormName
      }

      // eslint-disable-next-line vue/no-side-effects-in-computed-properties
      switch (this.myFormId) {
        case 'A000201':
          // eslint-disable-next-line vue/no-side-effects-in-computed-properties
          this.currentComponent = A000201
          break
        // case 'A00101':
        //   // eslint-disable-next-line vue/no-side-effects-in-computed-properties
        //   this.currentComponent = A00101
        //   break
        default:
          // eslint-disable-next-line vue/no-side-effects-in-computed-properties
          this.currentComponent = NotFound
      }
      // console.log('Sc_FormId:' + this.$route.params.FormId + ' myFormId:' + this.myFormId)
      // console.log('c_FormId:' + this.$route.params.FormId)
      return this.$route.params.FormId
    }
  },
  watch: {
    c_FormId: function(newValue, oldValue) {
      // console.log(oldValue + '-myFormId->' + newValue + ' FormId:' + this.$route.params.FormId)
      // console.log(oldValue + '-c_FormId->' + newValue)
    }
  },
  methods: {}
}
</script>
