<template>
  <div style="width:40%">
    <el-upload
      class="upload-demo"
      :action="httpImgFileUploadUrl"
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
      >請註意您只能上傳.csv格式</div>
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
import { downloadCurrentOpenDataRoadFile } from '@/api/chaochi/OpenDataRoad/OpenDataRoadservice'
import defaultSettings from '@/settings'
import { getToken } from '@/utils/auth'
export default {
  name: 'Addressmt',
  data() {
    return {
      msg: '測試中',
      httpImgFileUploadUrl: defaultSettings.apiChaochiUrl + 'Files/CsvUpload',
      headers: []
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
      downloadCurrentOpenDataRoadFile().then(res => {
        const blob = res // new Blob([response.data], { type: 'image/jpeg' })
        const link = document.createElement('a')
        link.href = URL.createObjectURL(blob)
        link.download = 'OpenDataRoad'
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
