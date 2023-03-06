<template>
  <el-card
    v-loading="saveloading"
    class="box-card"
  >
    <div>
      <div style="width: 100%; padding-bottom: 15px">
        <!-- <el-button
          type="primary"
          size="small"
          icon="el-icon-paperclip"
          @click="save()"
        >儲 存</el-button> -->
        <el-button
          type="success"
          size="small"
          icon="el-icon-download"
          @click="DownQFile()"
        >問卷定義檔下載</el-button>
        <el-button
          type="success"
          size="small"
          icon="el-icon-upload2"
          @click="ShowDialogUploadQFile()"
        >問卷定義檔上傳</el-button>
      </div>
      <!-- <el-form :model="read">
      <el-form-item
        label="123："
        :label-width="formLabelWidth"
        style="width: 20%"
      >
        <el-input v-model="read.EventName" />
      </el-form-item>
    </el-form> -->
      <el-form
        ref="editForm"
        :model="editForm" inline :rules="rules"
      >
        <el-row>
          <el-form-item
            label="問卷類別："
            :label-width="formLabelWidth"
          >
            <el-input
              v-model="editForm.QType"
              readonly
            />
          </el-form-item>
          <el-form-item
            label="問卷代號："
            :label-width="formLabelWidth"
          >
            <el-input
              v-model="editForm.QCode"
              autocomplete="off" readonly
            />
          </el-form-item>
        </el-row>
        <!-- <el-row>
          <el-form-item
            label="問卷連結："
            :label-width="formLabelWidth"
          >
            <el-input
              v-model="editForm.QUrl"
              readonly style="width: 600px"
            />
          </el-form-item>
        </el-row> -->
        <el-row>
          <el-form-item
            label="所屬活動："
            :label-width="formLabelWidth"
          >
            <el-input
              v-model="editForm.EventName"
              readonly
              style="width: 600px"
            />
          </el-form-item>
        </el-row>
        <el-row>
          <el-form-item
            label="所屬公司："
            :label-width="formLabelWidth"
          >
            <el-input
              v-model="editForm.BelongCompany"
              readonly
              style="width: 600px"
            />
          </el-form-item>
        </el-row>

        <el-form-item
          label="當前問卷題數："
          :label-width="formLabelWidth"
        >
          <el-input
            v-model="editForm.QuestionCount"
            readonly
          />
        </el-form-item>
        <el-form-item
          label="當前問卷定義檔："
          :label-width="formLabelWidth"
        >
          <el-input
            v-model="editForm.QFileName"
            readonly
            style="width: 300px"
          />
        </el-form-item>
        <el-card
          v-if="editForm.QFileName"
          style="width: 40%"
        >
          <el-form-item
            label="調查問卷名稱："
            :label-width="formLabelWidth"
            prop="QName"
          >
            <el-input
              v-model="editForm.QName"
              placeholder="請輸入調查問卷名稱"
            />
          </el-form-item>
          <el-row>
            <el-form-item
              label="問卷開頭語："
              :label-width="formLabelWidth"
              prop="QBegingWords"
            >
              <el-input
                v-model="editForm.QBegingWords"
                placeholder="請輸入問卷開頭語"
                type="textarea"
                :rows="3"
                style="width: 300px"
              />
            </el-form-item>
            <el-form-item
              label="問卷結尾語："
              :label-width="formLabelWidth"
              prop="QEndWords"
            >
              <el-input
                v-model="editForm.QEndWords"
                placeholder="請輸入問卷結尾語"
                type="textarea"
                :rows="3"
                style="width: 300px"
              />
            </el-form-item>
          </el-row>
          <div class="rightbtn">
            <el-button
              v-if="editForm.HasGened === 'Y'"
              type="primary"
              size="small"
              icon="el-icon-paperclip"
              @click="UpdateQuestionnaire()"
            >儲存</el-button>
          </div>
        </el-card>
        <!-- <el-form-item
          v-if="editForm.NewQFileName"
          label="新問卷定義檔："
          label-width="200px"
          style="width: 35%"
          class="item"
        >
          <el-input
            v-model="editForm.NewQFileName"
            readonly
          />
        </el-form-item> -->
      </el-form>
      <div class="rightbtn">
        <el-button
          type="primary"
          size="small"
          icon="el-icon-printer"
          @click="GenSighUp()"
        >生成問卷</el-button>
        <el-button
          size="small" icon="el-icon-close" @click="cancel"
        >關閉</el-button>
      </div>
    </div>
    <el-dialog
      ref="dialogEditFormD"
      title="問卷定義檔上傳"
      :visible.sync="UploadDialogVisible"
      width="480px"
      append-to-body
    >
      <el-upload
        ref="upload"
        class="upload-demo"
        :action="httpImgFileUploadUrl"
        :headers="headers"
        :auto-upload="false"
        drag
        :limit="1"
        accept=".xlsx"
        :data="send"
        :on-success="onsuccess"
        :before-upload="beforeupload"
        :on-change="onchange"
      >
        <i class="el-icon-upload" />
        <div class="el-upload__text">將文件拖到此處，或<em>點擊上傳</em></div>
        <div
          slot="tip"
          class="el-upload__tip"
        >請註意您只能上傳.xlsx格式</div>
      </el-upload>
    </el-dialog>
  </el-card>
</template>

<script>
import {
  GenSighUp,
  UpdateQuestionnaire,
  DownloadQuestionnaireTopicByQCode,
  ExportToXLSX
} from '@/api/chaochi/event/eventservice';
import defaultSettings from '@/settings';
import { getToken } from '@/utils/auth';
import moment from 'moment';
export default {
  name: 'Questionnaire',
  props: {
    questionnaire: { type: Object, default: null },
    startdate: { type: String, default: '' },
    idd: { type: String, default: '' }
  },
  data() {
    return {
      editForm: { ...this.questionnaire },
      StartDate: this.startdate,
      send: {
        id: ''
      },
      rules: {
        QName: [
          { required: true, message: '請輸入調查問卷名稱', trigger: 'change' }
        ],
        QBegingWords: [
          { required: true, message: '請輸入開頭語', trigger: 'change' }
        ],
        QEndWords: [
          { required: true, message: '請輸入結尾語', trigger: 'change' }
        ]
      },
      headers: [],
      httpImgFileUploadUrl:
        defaultSettings.apiChaochiUrl + 'Files/EventQuestionnaireUpload',
      formLabelWidth: '135px',
      saveloading: false,
      UploadDialogVisible: false
    };
  },
  computed: {
    BtnShow() {
      var year;
      if (this.StartDate !== null) {
        if (this.StartDate.indexOf('-') !== -1) {
          year = this.StartDate.split('-')[0];
          var month = this.StartDate.split('-')[1];
          var day = this.StartDate.split('-')[2];
          year = (parseInt(year) + 1911).toString() + month + day;
          // var datetime = new Date('2022-07-11');
          var startdate = moment(year).format('YYYY-MM-DD HH:mm:SS');
          var thisday = moment(new Date()).format('YYYY-MM-DD HH:mm:SS');
          console.log(startdate, thisday);
          return startdate > thisday;
        } else {
          return false;
        }
      } else {
        return false;
      }
    }
  },
  watch: {
    questionnaire: {
      deep: true,
      handler() {
        console.log('1');
        this.editForm = { ...this.questionnaire };
      }
    }
  },
  created() {
    this.headers = { Authorization: 'Bearer ' + (getToken() || '') };
  },
  methods: {
    cancel() {
      this.$emit('cancel');
    },
    UpdateQuestionnaire() {
      const dataf = {
        Id: this.editForm.Id,
        EId: this.idd,
        QName: this.editForm.QName,
        QBegingWords: this.editForm.QBegingWords,
        QEndWords: this.editForm.QEndWords
      };
      UpdateQuestionnaire(dataf).then((res) => {
        console.log(res);
      });
    },
    GenSighUp() {
      // this.$confirm('', '警告', {
      //   confirmButtonText: '確定',
      //   cancelButtonText: '取消',
      //   type: 'warning'
      // }).then(function() {
      //   const data = {
      //     Ids: currentIds
      //   };
      //   return deleteSystemType(data);
      // });
      if (!this.editForm.QFileName) {
        this.$message.error('請先上傳題目定義檔!');
        return;
      } else {
        this.$refs['editForm'].validate((valid) => {
          if (valid) {
            this.$confirm(
              '您即將產生一份新調查問卷，請注意：<br/><b style="color:red">若您是針對已存在之調查問卷作題目的更改，您將失去原題目之填寫資料，請確保已將原題目之統計報表保存妥善!<b/>',
              '警告',
              {
                confirmButtonText: '確定',
                cancelButtonText: '取消',
                dangerouslyUseHTMLString: true,
                type: 'warning'
              }
            )
              .then(() => {
                this.saveloading = true;
                this.editForm.Eid = this.idd;
                GenSighUp(this.editForm)
                  .then((res) => {
                    if (res.Success) {
                      this.$message.success('儲存成功');
                      this.$emit('bindinfo');
                    } else {
                      this.$message.error('失敗');
                    }
                  })
                  .finally(() => {
                    this.saveloading = false;
                  });
              })
              .catch(() => {
                return;
              });
          } else {
            return false;
          }
        });
      }
    },
    onchange(file) {
      console.log(file);
      if (file.status !== 'ready' || !file) return;
      let msg = '';
      if (this.editForm.HasGened === 'N') {
        msg = '確定上傳?';
      } else {
        msg =
          '改變調查問卷題目，系統將<br/><b style="color:red">1.自動匯出原題目對應之統計報表<br/>2.初始化此調查問卷(這意味著您將失去原題目之定義檔)</b><br/>確認上傳定義更新檔案？';
      }
      this.$confirm(msg, '警告', {
        confirmButtonText: '確定',
        cancelButtonText: '取消',
        dangerouslyUseHTMLString: true,
        type: 'warning'
      })
        .then(() => {
          this.send.id = this.editForm.QCode;
          if (this.editForm.HasGened === 'N') {
            this.$refs.upload.submit();
          } else {
            ExportToXLSX(this.idd)
              .then((res) => {
                const blob = res; // new Blob([response.data], { type: 'image/jpeg' })
                const link = document.createElement('a');
                link.href = URL.createObjectURL(blob);
                link.download = this.editForm.EventName + '_問卷統計.xlsx';
                link.click();
                URL.revokeObjectURL(link.href);
                this.$refs.upload.submit();
                this.editForm.QName = '';
                this.editForm.QBegingWords = '';
                this.editForm.QEndWords = '';
                this.editForm.HasGened = 'N';
              })
              .catch(() => {
                this.$message.error('產生統計報表失敗');
              });
          }
        })
        .catch(() => {
          this.$refs.upload.clearFiles();
        });
    },
    beforeupload() {
      // this.send.id = this.editForm.QCode;
    },
    onsuccess(res, file) {
      if (res.Success === false) {
        this.$message.error('上傳失敗');
        this.$refs.upload.clearFiles();
      } else {
        this.$message.success('恭喜你，上傳成功');
        console.log(file);
        this.$refs.upload.clearFiles();
        this.UploadDialogVisible = false;
        this.$emit('bindinfo');
        // this.$refs.editForm.$forceUpdate();
      }
    },
    DownQFile() {
      DownloadQuestionnaireTopicByQCode(this.editForm.QCode).then((res) => {
        const blob = res; // new Blob([response.data], { type: 'image/jpeg' })
        const link = document.createElement('a');
        link.href = URL.createObjectURL(blob);
        link.download =
          this.editForm.QFileName === null || this.editForm.QFileName === ''
            ? '調查問卷題目定義檔'
            : this.editForm.QFileName;
        link.click();
        URL.revokeObjectURL(link.href);
      });
    },
    ShowDialogUploadQFile() {
      this.UploadDialogVisible = true;
    }
  }
};
</script>

<style>
.item .el-form-item__label {
  color: red;
  font-weight: bold;
  font-size: 20px;
}
</style>
