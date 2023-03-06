<template>
  <div class="app-container">
    {{ myFormId }} {{ myFormName }}
    <component :is="currentComponent" />
    <!-- <el-tooltip
      effect="dark"
      content="簽名"
      placement="top"
      :enterable="false"
    >
      <el-button
        v-hasPermi="['Building/Add']"
        type="primary"
        icon="el-icon-upload2"
        size="small"
        @click="Jump2(myFormId,myFormName)"
      >簽名</el-button>
    </el-tooltip> -->
  </div>
</template>
<script>
import Address from '@/components/Address/Address.vue'
// import A000401 from './A000401.vue'
// import A000301 from './A000301.vue'
// import A000201 from './A000201.vue'
// import NotFound from './NotFound'

export default {
  name: 'SecurityFormDetail',
  components: {
    Address
    // A000301,
    // A000401,
    // A000201,
    // NotFound
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
        case 'A000401':
          // eslint-disable-next-line vue/no-side-effects-in-computed-properties
          this.currentComponent = () => import('./A000401.vue')
          break
        case 'A000301':
          // eslint-disable-next-line vue/no-side-effects-in-computed-properties
          this.currentComponent = () => import('./A000301.vue')
          break
        case 'A000201':
          // eslint-disable-next-line vue/no-side-effects-in-computed-properties
          this.currentComponent = () => import('./A000201.vue')
          break
        default:
          // eslint-disable-next-line vue/no-side-effects-in-computed-properties
          this.currentComponent = () => import('./NotFound.vue')
      }
      // console.log('c_FormId:' + this.$route.params.FormId + ' myFormId:' + this.myFormId)
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
  methods: {
    Jump2: function(FormId, FormName) {
      // console.log(row.FormId)
      this.$router.push({
        // path: '/chaochi/internalFormsDetail/index',
        name: 'InternalformsDetailSign',
        params: {
          FormId: FormId,
          FormName: FormName
        }
      })
    }
  }
}
</script>
