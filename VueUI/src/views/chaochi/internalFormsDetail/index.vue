<template>
  <div class="app-container">
    <h3>{{ myFormId }} {{ myFormName }}</h3>
    <component
      :is="currentComponent"
      v-loading="pageLoading"
      v-loading.fullscreen.lock="pageLoading"
      element-loading-text="請稍後..."
      element-loading-spinner="el-icon-loading"
      element-loading-background="rgba(0, 0, 0, 0.8)"
      :editform="editForm"
      @closepageloading="ClosePageLoading"
      @openpageloading="OpenPageLoading"
      @upload="upload"
      @cancel="cancel"
    />
    <!-- <div style="margin-top: 20px; text-align: center">
      <el-button
        size="small"
        icon="el-icon-close"
        @click="cancel"
      >關閉</el-button>
    </div> -->
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
import { UploadWithData } from '@/api/chaochi/internalform/internalform';
import Address from '@/components/Address/Address.vue';
import A000201 from './A000201.vue';
import A000202 from './A000202.vue';
import A000203 from './A000203.vue';
import A000003 from './A000003.vue';
import A000002 from './A000002.vue';
import A000001 from './A000001.vue';
import A000004 from './A000004.vue';
import NotFound from './NotFound';

export default {
  name: 'InternalformsDetail',
  components: {
    Address,
    A000201,
    A000202,
    A000003,
    A000002,
    A000001,
    A000004,
    NotFound
  },
  props: {
    searchPDF: { type: Object, default: null }
  },
  data() {
    return {
      myFormId: null,
      myFormName: null,
      currentComponent: null,
      pageLoading: false
    };
  },
  computed: {
    // https://stackoverflow.com/questions/42133894/vue-js-how-to-properly-watch-for-nested-data
    c_FormId() {
      if (this.$route.params.FormId) {
        // eslint-disable-next-line vue/no-side-effects-in-computed-properties
        this.myFormId = this.$route.params.FormId;
        // eslint-disable-next-line vue/no-side-effects-in-computed-properties
        this.myFormName = this.$route.params.FormName;
      }

      // eslint-disable-next-line vue/no-side-effects-in-computed-properties
      switch (this.myFormId) {
        case 'A000201':
          // eslint-disable-next-line vue/no-side-effects-in-computed-properties
          this.currentComponent = A000201;
          break;
        case 'A000202':
          // eslint-disable-next-line vue/no-side-effects-in-computed-properties
          this.currentComponent = A000202;
          break;
        case 'A000003':
          // eslint-disable-next-line vue/no-side-effects-in-computed-properties
          this.currentComponent = A000003;
          break;
        case 'A000002':
          // eslint-disable-next-line vue/no-side-effects-in-computed-properties
          this.currentComponent = A000002;
          break;
        case 'A000001':
          // eslint-disable-next-line vue/no-side-effects-in-computed-properties
          this.currentComponent = A000001;
          break;
        case 'A000004':
          // eslint-disable-next-line vue/no-side-effects-in-computed-properties
          this.currentComponent = A000004;
          break;
        case 'A000203':
          // eslint-disable-next-line vue/no-side-effects-in-computed-properties
          this.currentComponent = A000203;
          break;
        default:
          // eslint-disable-next-line vue/no-side-effects-in-computed-properties
          this.currentComponent = NotFound;
      }
      return this.$route.params.FormId;
    },
    editForm() {
      return this.$route.params;
    },
    visitedViews() {
      return this.$store.state.tagsView.visitedViews;
    }
  },
  watch: {
    c_FormId: {
      immediate: true,
      handler() {
        // this.pageLoading = true;
      }
    }
  },
  methods: {
    ClosePageLoading() {
      this.pageLoading = false;
    },
    OpenPageLoading() {
      this.pageLoading = true;
    },
    upload(formid, editform) {
      console.log(formid, editform);
      this.OpenPageLoading();
      UploadWithData(formid, editform)
        .then(res => {
          if (res.Success) {
            this.$message.success('儲存成功!');
          } else {
            this.$message.error('失敗');
          }
        })
        .finally(() => {
          this.ClosePageLoading();
        });
    },
    cancel() {
      const view = this.visitedViews.slice(-1)[0];
      if (view) {
        this.$store
          .dispatch('tagsView/delView', view)
          .then(({ visitedViews }) => {
            if (this.isActive(view)) {
              this.toLastView(visitedViews, view);
            }
          });
      }
    },
    isActive(route) {
      return route.path === this.$route.path;
    },
    toLastView(visitedViews, view) {
      const latestView = visitedViews.slice(-1)[0];
      if (latestView) {
        this.$router.push(latestView.fullPath);
      } else {
        // now the default is to redirect to the home page if there is no tags-view,
        // you can adjust it according to your needs.
        if (view.name === 'Dashboard') {
          // to reload home page
          this.$router.replace({ path: '/redirect' + view.fullPath });
        } else {
          this.$router.push('/');
        }
      }
    },

    Jump2: function(FormId, FormName) {
      this.$router.push({
        name: 'InternalformsDetailSign',
        params: {
          FormId: FormId,
          FormName: FormName
        }
      });
    }
  }
};
</script>
<style>
.w600px {
  width: 600px !important;
}
.w300px {
  width: 300px !important;
}
.w280px {
  width: 280px !important;
}
.w260px {
  width: 260px !important;
}
.w240px {
  width: 240px !important;
}
.w220px {
  width: 220px !important;
}
.w200px {
  width: 200px !important;
}
.w180px {
  width: 180px !important;
}
.w160px {
  width: 160px !important;
}
.w140px {
  width: 140px !important;
}
.w120px {
  width: 120px !important;
}
.w100px {
  width: 100px !important;
}
.w80px {
  width: 80px !important;
}
.w70px {
  width: 70px !important;
}
.w60px {
  width: 60px !important;
}
.text {
  font-size: 14px !important;
  letter-spacing: 1px !important;
  margin-bottom: 5px !important;
}
.mb-0 {
  margin-bottom: 0px !important;
}
.tmb {
  height: 216px;
  display: inline-block;
  background-color: #eff6ff;
  vertical-align: middle;
  width: 532px;
  line-height: 72px;
  text-align: center;
}
.fcRED {
  color: #e24e4e;
}
.pad {
  padding: 10px;
  border: 1px solid grey;
  width: 55%;
}
</style>
