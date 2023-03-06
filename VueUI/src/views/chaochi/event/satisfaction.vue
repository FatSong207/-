<template>
  <el-card class="box-card">
    <div>
      <!-- <div style="width: 100%; padding-bottom:15px">
        <el-button
          type="primary"
          size="small"
          icon="el-icon-paperclip"
          @click="save()"
        >儲 存</el-button>
        <el-button
          type="success"
          size="small"
          icon="el-icon-upload2"
          @click="DownloadQrCodeImg()"
        >問卷二維碼圖片下載</el-button>
      </div> -->
      <el-form
        ref="editForm"
        :model="editForm"
      >
        <el-form-item
          label="問卷類別："
          :label-width="formLabelWidth"
          style="width: 20%"
        >
          <el-input
            v-model="editForm.QType"
            readonly
          />
        </el-form-item>
        <el-form-item
          label="問卷代號："
          :label-width="formLabelWidth"
          style="width: 25%"
        >
          <el-input
            v-model="editForm.QCode"
            autocomplete="off" readonly
          />
        </el-form-item>
        <el-form-item
          label="問卷連結："
          :label-width="formLabelWidth"
          style="width: 35%"
        >
          <el-input
            v-model="editForm.QUrl"
            readonly
          />
        </el-form-item>
        <el-form-item
          label="所屬活動："
          :label-width="formLabelWidth"
          style="width: 25%"
        >
          <el-input
            v-model="editForm.EventName"
            readonly
          />
        </el-form-item>
        <el-form-item
          label="所屬公司："
          :label-width="formLabelWidth"
          style="width: 20%"
        >
          <el-input
            v-model="editForm.BelongCompany"
            readonly
          />
        </el-form-item>
        <el-form-item
          label="問卷QrCode："
          :label-width="formLabelWidth"
          style="width: 30%"
        >
          <!-- <img :src="scope.row.ImgUrl==null?'XX':scope.row.ImgUrl" min-width="70" height="70">{{ scope.row.FileName }} -->
          <el-card style="width: 80%; text-align: center">
            <expandable-image
              :src="editForm.QRcodeImg == null ? '#/XX' : editForm.QRcodeImg"
            />
            <el-button
              type="success"
              size="small"
              icon="el-icon-download"
              @click="DownloadQrCodeImg()"
            >問卷二維碼圖片下載</el-button>
          </el-card>
        </el-form-item>
      </el-form>
      <div class="rightbtn">
        <el-button
          size="small" icon="el-icon-close" @click="cancel"
        >關閉</el-button>
      </div>
    </div>
  </el-card>
</template>

<script>
import {
  getByFileName,
  DownloadSatisfactionQrCodeByQCode
} from '@/api/chaochi/event/eventservice';
export default {
  name: 'Satisfaction',
  props: { satisfaction: { type: Object, default: null }},
  data() {
    return {
      editForm: { ...this.satisfaction },
      formLabelWidth: '135px'
    };
  },
  watch: {
    editForm: {
      handler() {
        getByFileName(this.editForm.EventId).then((response) => {
          this.editForm.QRcodeImg = URL.createObjectURL(response);
        });
      }
    }
  },
  created() {
    getByFileName(this.editForm.EventId).then((response) => {
      this.editForm.QRcodeImg = URL.createObjectURL(response);
    });
  },
  methods: {
    cancel() {
      this.$emit('cancel');
    },
    DownloadQrCodeImg() {
      DownloadSatisfactionQrCodeByQCode(this.editForm.QCode).then((res) => {
        const blob = res; // new Blob([response.data], { type: 'image/jpeg' })
        const link = document.createElement('a');
        link.href = URL.createObjectURL(blob);
        link.download = this.editForm.QCode;
        link.click();
        URL.revokeObjectURL(link.href);
      });
    }
  }
};
</script>

<style></style>
