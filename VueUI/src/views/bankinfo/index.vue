<template>
  <div style="width:40%">
    <el-upload
      ref="upload"
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
import { downloadCurrentFile } from '@/api/chaochi/bankinfo/bankinfo'
import defaultSettings from '@/settings';
import { getToken } from '@/utils/auth';
export default {
  name: 'Bankinfomt',
  data() {
    return {
      msg: '測試中',
      httpImgFileUploadUrl:
        defaultSettings.apiChaochiUrl + 'Files/BankinfoUpload',
      headers: []
    };
  },
  created() {
    this.headers = { Authorization: 'Bearer ' + (getToken() || '') };
  },
  methods: {
    onsuccess(res) {
      if (res.Success === false) {
        this.$message.error(res.ErrMsg);
      } else {
        this.$message({
          message: '恭喜你，上傳成功',
          type: 'success'
        });
      }
      this.$refs.upload.clearFiles();
    },
    onprogress() {
      this.$message({
        message: '上傳中請稍後...',
        type: 'loading'
      });
    },
    download() {
      downloadCurrentFile().then(res => {
        const blob = res; // new Blob([response.data], { type: 'image/jpeg' })
        const link = document.createElement('a');
        link.href = URL.createObjectURL(blob);
        link.download = '銀行資訊維護';
        link.click();
        URL.revokeObjectURL(link.href);
      });
    }
  }
};
</script>

<style>
.btn {
  margin: 10px;
}
</style>
