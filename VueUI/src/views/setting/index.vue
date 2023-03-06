<template>
  <div class="app-container">
    <el-form ref="editFrom" :model="editFrom" :rules="rules" class="yuebon-setting-form">
      <el-tabs v-model="activeName" type="border-card">
        <el-tab-pane label="基本資訊" name="first">
          <el-form-item label="系統名稱" :label-width="formLabelWidth" prop="SoftName">
            <el-input v-model="editFrom.SoftName" placeholder="請輸入系統名稱" autocomplete="off" clearable />
            *系統名稱最多20個字符
          </el-form-item>
          <el-form-item label="系統簡介" :label-width="formLabelWidth" prop="SoftSummary">
            <el-input v-model="editFrom.SoftSummary" placeholder="請輸入系統簡介" autocomplete="off" clearable />
          </el-form-item>
          <el-form-item label="訪問域名" :label-width="formLabelWidth" prop="WebUrl">
            <el-input v-model="editFrom.WebUrl" placeholder="請輸入訪問域名" autocomplete="off" clearable />
            <!-- *域名授權請聯系<a href="http://www.yuebon.com/" target="_blank">越邦客服</a> -->
          </el-form-item>
          <el-form-item label="Logo" :label-width="formLabelWidth" prop="SysLogo">
            <el-upload
              class="avatar-uploader"
              :action="httpFileUploadUrl"
              :headers="headers"
              :show-file-list="false"
              :on-success="uploadFileSuccess"
            >
              <img v-if="editFrom.SysLogo" :src="editFrom.SysLogo" class="avatar">
              <i v-else class="el-icon-plus avatar-uploader-icon" />
            </el-upload>
            <el-dialog :visible.sync="dialogVisible">
              <img width="100%" :src="dialogImageUrl" alt="">
            </el-dialog>
          </el-form-item>
          <el-form-item label="公司名稱" :label-width="formLabelWidth" prop="CompanyName">
            <el-input v-model="editFrom.CompanyName" placeholder="請輸入公司名稱" autocomplete="off" clearable />
          </el-form-item>
          <el-form-item label="通訊地址" :label-width="formLabelWidth" prop="Address">
            <el-input v-model="editFrom.Address" placeholder="請輸入通訊地址" autocomplete="off" clearable />
          </el-form-item>
          <el-form-item label="電話" :label-width="formLabelWidth" prop="Telphone">
            <el-input v-model="editFrom.Telphone" placeholder="請輸入電話" autocomplete="off" clearable />
          </el-form-item>
          <el-form-item label="Email" :label-width="formLabelWidth" prop="Email">
            <el-input v-model="editFrom.Email" placeholder="請輸入Email" autocomplete="off" clearable />
          </el-form-item>
          <!-- <el-form-item label="ICP備案號" :label-width="formLabelWidth" prop="ICPCode">
            <el-input v-model="editFrom.ICPCode" placeholder="請輸入ICP備案號" autocomplete="off" clearable />
            請到工業和信息化部http://beian.miit.gov.cn網站查詢
          </el-form-item>
          <el-form-item label="公安備案號" :label-width="formLabelWidth" prop="PublicSecurityCode">
            <el-input v-model="editFrom.PublicSecurityCode" placeholder="請輸入公安備案號" autocomplete="off" clearable />
            請到全國互聯網安全管理服務平臺http://www.beian.gov.cn網站備案查詢
          </el-form-item> -->
        </el-tab-pane>
        <el-tab-pane label="功能權限" name="second">
          <el-form-item label="是否開啟系統" :label-width="formLabelWidth" prop="Webstatus">
            <el-radio-group v-model="editFrom.Webstatus">
              <el-radio label="0">是</el-radio>
              <el-radio label="1">否</el-radio>
            </el-radio-group>
          </el-form-item>
          <el-form-item label="系統關閉原因" :label-width="formLabelWidth" prop="Webclosereason">
            <el-input v-model="editFrom.Webclosereason" type="textarea" placeholder="請輸入系統關閉原因" autocomplete="off" clearable />
          </el-form-item>
          <el-form-item label="系統統計代碼" :label-width="formLabelWidth" prop="Webcountcode">
            <el-input v-model="editFrom.Webcountcode" type="textarea" placeholder="請輸入系統統計代碼" autocomplete="off" clearable />
          </el-form-item>
        </el-tab-pane>
        <el-tab-pane label="短信設置" name="three">
          <el-form-item label="短信API地址" :label-width="formLabelWidth" prop="Smsapiurl">
            <el-input v-model="editFrom.Smsapiurl" placeholder="請輸入短信API地址" autocomplete="off" clearable />
          </el-form-item>
          <el-form-item label="平臺登錄賬戶" :label-width="formLabelWidth" prop="Smsusername">
            <el-input v-model="editFrom.Smsusername" placeholder="請輸入平臺登錄賬戶" autocomplete="off" clearable />
          </el-form-item>
          <el-form-item label="平臺通信密鑰" :label-width="formLabelWidth" prop="Smspassword">
            <el-input v-model="editFrom.Smspassword" placeholder="請輸入平臺通信密鑰" autocomplete="off" clearable />
          </el-form-item>
          <el-form-item label="簽名" :label-width="formLabelWidth" prop="SmsSignName">
            <el-input v-model="editFrom.SmsSignName" placeholder="請輸入短信簽名" autocomplete="off" clearable />
          </el-form-item>
          <el-form-item label="短信平臺說明" :label-width="formLabelWidth">
            請不要使用系統默認賬戶test，因為它是公用的測試帳號；
            請在短信平臺修改賬戶資料中綁定簽名方可使用短信功能；
            如果您尚未申請開通，請點擊這里註冊成功后填寫您的用戶名和通訊密鑰均可正常使用。
            目前實現了阿里云短信和助通科技短信接口。
          </el-form-item>
        </el-tab-pane>
        <el-tab-pane label="郵件設置" name="four">
          <el-form-item label="SMTP服務器" :label-width="formLabelWidth" prop="Emailsmtp">
            <el-input v-model="editFrom.Emailsmtp" placeholder="請輸入SMTP服務器" autocomplete="off" clearable />
          </el-form-item>
          <el-form-item label="SSL加密連接" :label-width="formLabelWidth" prop="Emailssl">
            <el-radio-group v-model="editFrom.Emailssl">
              <el-radio label="true">是</el-radio>
              <el-radio label="false">否</el-radio>
            </el-radio-group>
          </el-form-item>
          <el-form-item label="SMTP端口" :label-width="formLabelWidth" prop="Emailport">
            <el-input v-model="editFrom.Emailport" placeholder="請輸入SMTP端口" autocomplete="off" clearable />
          </el-form-item>
          <el-form-item label="發件人地址" :label-width="formLabelWidth" prop="Emailfrom">
            <el-input v-model="editFrom.Emailfrom" placeholder="請輸入發件人地址" autocomplete="off" clearable />
          </el-form-item>
          <el-form-item label="郵箱帳號" :label-width="formLabelWidth" prop="Emailusername">
            <el-input v-model="editFrom.Emailusername" placeholder="請輸入郵箱帳號" autocomplete="off" clearable />
          </el-form-item>
          <el-form-item label="郵箱密碼" :label-width="formLabelWidth" prop="Emailpassword">
            <el-input v-model="editFrom.Emailpassword" placeholder="請輸入郵箱密碼" type="password" autocomplete="off" clearable />
          </el-form-item>
          <el-form-item label="發件人暱稱" :label-width="formLabelWidth" prop="Emailnickname">
            <el-input v-model="editFrom.Emailnickname" placeholder="請輸入發件人暱稱" autocomplete="off" clearable />
          </el-form-item>
        </el-tab-pane>
        <el-tab-pane label="文件上傳" name="five">
          <el-form-item label="伺服器" :label-width="formLabelWidth" prop="Fileserver">
            <el-select v-model="editFrom.Fileserver" placeholder="請選擇">
              <el-option label="本地服務器" value="localhost" />
              <el-option label="阿里云OSS" value="alioss" />
              <el-option label="騰訊云OSS" value="tengxunoss" />
              <el-option label="七牛云" value="qiniu" />
            </el-select>
          </el-form-item>
          <el-form-item label="文件上傳目錄" :label-width="formLabelWidth" prop="Filepath">
            <el-input v-model="editFrom.Filepath" placeholder="請輸入文件上傳目錄" autocomplete="off" clearable />
          </el-form-item>
          <el-form-item label="文件保存方式" :label-width="formLabelWidth" prop="Filesave">
            <el-select v-model="editFrom.Filesave" placeholder="請選擇">
              <el-option label="按年月日每天一個目錄" value="1" />
              <el-option label="按年月日存入不同目錄" value="2" />
            </el-select>
          </el-form-item>
          <el-form-item label="文件上傳類型" :label-width="formLabelWidth" prop="Fileextension">
            <el-input v-model="editFrom.Fileextension" placeholder="請輸入文件上傳類型" autocomplete="off" clearable />
          </el-form-item>
          <el-form-item label="圖片大小" :label-width="formLabelWidth" prop="Thumbnailheight">
            寬<el-input v-model="editFrom.Thumbnailwidth" placeholder="圖片寬度" autocomplete="off" style="width:150px;" clearable />px
            *高<el-input v-model="editFrom.Thumbnailheight" placeholder="圖片高度" autocomplete="off" style="width:150px;" clearable />px

          </el-form-item>
        </el-tab-pane>
      </el-tabs>
      <el-form-item>
        <el-button type="primary" class="btnset" @click="saveEditForm()">存 檔</el-button>
      </el-form-item>
    </el-form>
  </div>
</template>
<script>
import { getAllSysSetting, saveSysSetting } from '@/api/basebasic'
import defaultSettings from '@/settings'
import { getToken } from '@/utils/auth'
export default {
  name: 'SysSetting',
  data() {
    return {
      activeName: 'first',
      editFrom: {
        SoftName: '',
        SoftSummary: '',
        WebUrl: '',
        SysLogo: '',
        CompanyName: '',
        Address: '',
        Telphone: '',
        Email: '',
        ICPCode: '',
        PublicSecurityCode: '',
        Webstatus: '0',
        Webclosereason: '',
        Webcountcode: '',
        Smsapiurl: '',
        Smsusername: '',
        Smspassword: '',
        SmsSignName: '',
        Emailsmtp: '',
        Emailssl: '',
        Emailport: '',
        Emailfrom: '',
        Emailusername: '',
        Emailpassword: '',
        Emailnickname: '',
        Fileserver: '',
        Filepath: '',
        Filesave: '',
        Fileextension: ''
      },
      rules: {
        SoftName: [
          { required: true, message: '請輸入系統名稱', trigger: 'blur' },
          { min: 2, max: 50, message: '長度在 6 到 32 個字符', trigger: 'blur' }
        ],
        WebUrl: [
          { required: true, message: '請輸入訪問地址', trigger: 'blur' },
          { min: 2, max: 50, message: '長度在 6 到 32 個字符', trigger: 'blur' }
        ]
      },
      httpFileUploadUrl: defaultSettings.fileUploadUrl,
      dialogVisible: false,
      dialogImageUrl: '',
      filelist: [],
      formLabelWidth: '150px',
      headers: []
    }
  },
  created() {
    this.loadSettingData()
    this.headers = { Authorization: 'Bearer ' + (getToken() || '') }
  },
  methods: {
    handleRemove(file, fileList) {
      this.editFrom.SysLogo = file.url
    },
    handlePictureCardPreview(file) {
      this.dialogImageUrl = file.url
      this.dialogVisible = true
    },
    loadSettingData: function() {
      getAllSysSetting().then(res => {
        this.editFrom = res.ResData
        this.editFrom.Webstatus = res.ResData.Webstatus + ''
        this.editFrom.Emailssl = res.ResData.Emailssl + ''
      })
    },
    uploadFileSuccess: function(response, file, fileList) {
      this.editFrom.SysLogo = defaultSettings.fileUrl + response.ResData.FilePath
    },
    saveEditForm() {
      this.$refs['editFrom'].validate((valid) => {
        if (valid) {
          const data = this.editFrom
          saveSysSetting(data).then(res => {
            if (res.Success) {
              this.$message({
                message: '恭喜你，操作成功',
                type: 'success'
              })
              this.dialogEditFormVisible = false
              this.currentSelected = ''
              this.$refs['editFrom'].resetFields()
              this.loadSettingData()
            } else {
              this.$message({
                message: res.ErrMsg,
                type: 'error'
              })
            }
          })
        } else {
          return false
        }
      })
    }
  }
}
</script>
<style lang="scss" scoped>
.yuebon-setting-form .el-form-item{
  width: 100%;
}
.yuebon-setting-form .el-input{
width: 40%;
}
.yuebon-setting-form .el-select .el-input{
        width: 100%;
}
.yuebon-setting-form .btnset{
    float:right;
    margin-right: 30px;
    margin-top: 20px;
}

.avatar-uploader .el-upload {
    border: 1px solid #ccc;
    border-radius: 6px;
    cursor: pointer;
    position: relative;
    overflow: hidden;
  }
  .avatar-uploader .el-upload:hover {
    border-color: #409EFF;
  }
  .avatar-uploader-icon {
    font-size: 28px;
    color: #8c939d;
    width: 176px;
    height: 176px;
    line-height: 176px;
    border: 1px solid #ccc;
    text-align: center;
  }
  .avatar {
    width: 176px;
    height: 176px;
    display: block;
    border: 1px solid #ccc;
  }
</style>
