<template>
  <div style="width:40%">
    <el-upload
      class="upload-demo"
      :action="httpImgFileUploadUrl"
      :data="uploadData"
      :headers="headers"
      drag
      :limit="1"
      accept=".csv"
      :on-success="onsuccess"
      :on-progress="onprogress"
    >
      <i class="el-icon-upload" />
      <div class="el-upload__text">將文件拖到此處，或<em>點擊上傳</em></div>
      <div
        slot="tip"
        class="el-upload__tip"
      >＊<font color="red">請註意您只能上傳.csv格式</font><br>＊<font color="red">表單分類層數請勿超過7層</font><br>＊<font color="red">請將表單編號放在CSV的I欄</font></div>
    </el-upload>
    <el-button
      type="primary"
      size="small"
      class="btn"
      @click="download"
    >下載當前套用的.csv</el-button>
  </div>
</template>

<script>
import { downloadCurrentCategoryFormFile } from '@/api/chaochi/category/categoryformservice'
import defaultSettings from '@/settings'
import { getToken } from '@/utils/auth'
export default {
  name: 'CategoryForm',
  data() {
    return {
      msg: '測試中',
      httpImgFileUploadUrl: defaultSettings.apiChaochiUrl + 'Files/CategoryFormUpload',
      headers: [],
      uploadData: {
        categoryType: 'CategoryForm'
      }
    }
  },
  created() {
    this.headers = { Authorization: 'Bearer ' + (getToken() || '') }
  },
  methods: {
    onsuccess(res) {
      if (res.Success === false) {
        this.$message.error('上傳失敗')
      } else {
        this.$message({
          message: '恭喜你，上傳成功',
          type: 'success'
        })
      }
    },
    onprogress() {
      this.$message({
        message: '上傳中請稍後...',
        type: 'loading'
      })
    },
    download() {
      downloadCurrentCategoryFormFile().then(res => {
        const blob = res // new Blob([response.data], { type: 'image/jpeg' })
        const link = document.createElement('a')
        link.href = URL.createObjectURL(blob)
        link.download = '表單分類維護'
        link.click()
        URL.revokeObjectURL(link.href)
      })
    }
  }
}
</script>

<style>
.btn {
  margin: 10px;
}
</style>
