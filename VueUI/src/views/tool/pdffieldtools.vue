<template>
  <div v-loading="loading">
    <el-upload
      ref="newupload"
      drag
      :data="send"
      :action="httpUploadPDFTemplateUrl"
      :headers="headers"
      :multiple="false"
      :auto-upload="true"
      :show-file-list="false"
      accept=".pdf"
      :on-success="uploadsuccess"
      :before-upload="handleUploadBefore"
    >
      <!-- :on-success="uploadsuccess"
      :on-error="uploaderror"
      :before-upload="handleUploadBefore" -->
      <em class="el-icon-upload" />
      <div class="el-upload__text">將文件拖到此處，或<em>點擊上傳</em> </div>
      <div
        slot="tip"
        class="el-upload__tip"
      >請註意您只能上傳.pdf格式</div>
    </el-upload>
    <h4>{{ formName }}</h4>
    <h4>新欄位：</h4>
    <ul>
      <li
        v-for="(item,index) of resultArr"
        :key="index"
      >{{ item }}</li>
    </ul>
  </div>
</template>

<script>
import defaultSettings from '@/settings';
import { getToken } from '@/utils/auth';
export default {
  name: 'PdfFielsTools',
  data() {
    return {
      send: {},
      headers: [],
      httpUploadPDFTemplateUrl:
        defaultSettings.apiHostUrl + 'Tools/UploadPDFTemplate',
      formName: '',
      resultArr: [],
      loading: false
    };
  },
  created() {
    this.headers = { Authorization: 'Bearer ' + (getToken() || '') };
  },
  methods: {
    uploadsuccess(response, file) {
      this.formName = file.name;
      if (response.Success) {
        if (response.ResData !== '') {
          this.resultArr = response.ResData.split('，');
        } else {
          this.resultArr = [];
        }
      }
      this.loading = false;
    },
    handleUploadBefore() {
      this.loading = true;
    }
  }
};
</script>

<style>
</style>
