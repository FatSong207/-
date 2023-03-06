<template>
  <div class="app-container">
    <component :is="currentComponent" />
  </div>
</template>
<script>
export default {
  name: 'ToDoListDetail',
  props: {
    info: { type: Object, default: null }
  },
  data() {
    return {
      moduleName: null,
      currentComponent: null
    };
  },
  //   computed: {
  //     c_moduleName() {
  //       if (this.$route.params.moduleName) {
  //       // eslint-disable-next-line vue/no-side-effects-in-computed-properties
  //         this.moduleName = this.$route.params.moduleName
  //       }

  //       switch (this.moduleName) {
  //         case '合約':
  //         // eslint-disable-next-line vue/no-side-effects-in-computed-properties
  //           this.currentComponent = () => import('./contract.vue')
  //           break
  //         // case '評分管理':
  //         //   // eslint-disable-next-line vue/no-side-effects-in-computed-properties
  //         //   this.currentComponent = () => import('./score.vue')
  //         //   break
  //         // case '客訴管理':
  //         //   // eslint-disable-next-line vue/no-side-effects-in-computed-properties
  //         //   this.currentComponent = () => import('./complaint.vue')
  //         //   break
  //         // case '黑名單':
  //         //   // eslint-disable-next-line vue/no-side-effects-in-computed-properties
  //         //   this.currentComponent = () => import('./blacklist.vue')
  //         //   break
  //         default:
  //         // eslint-disable-next-line vue/no-side-effects-in-computed-properties
  //           this.currentComponent = () => import('./NotFound.vue')
  //       }
  //       return this.$route.params.moduleName
  //     }
  //   },
  //   watch: {
  //     c_moduleName: function(newValue, oldValue) {
  //       // console.log(oldValue + '-myFormId->' + newValue + ' FormId:' + this.$route.params.FormId)
  //       // console.log(oldValue + '-c_FormId->' + newValue)
  //     }
  //   },
  mounted() {
    this.load();
  },
  methods: {
    load: function() {
      if (this.$route.params.moduleName) {
        // eslint-disable-next-line vue/no-side-effects-in-computed-properties
        this.moduleName = this.$route.params.moduleName;
      }

      switch (this.moduleName) {
        case '合約':
          // eslint-disable-next-line vue/no-side-effects-in-computed-properties
          this.currentComponent = () => import('./contract.vue');
          break;
        case '入居者審查':
          // eslint-disable-next-line vue/no-side-effects-in-computed-properties
          this.currentComponent = () => import('./blackList.vue');
          break;
        // case '評分管理':
        //   // eslint-disable-next-line vue/no-side-effects-in-computed-properties
        //   this.currentComponent = () => import('./score.vue')
        //   break
        // case '客訴管理':
        //   // eslint-disable-next-line vue/no-side-effects-in-computed-properties
        //   this.currentComponent = () => import('./complaint.vue')
        //   break
        // case '黑名單':
        //   // eslint-disable-next-line vue/no-side-effects-in-computed-properties
        //   this.currentComponent = () => import('./blacklist.vue')
        //   break
        default:
          // eslint-disable-next-line vue/no-side-effects-in-computed-properties
          this.currentComponent = () => import('./NotFound.vue');
      }
    }
  }
};
</script>
