<template>
  <div class="app-container">
    <el-form
      ref="editFrom"
      :model="editFrom" :rules="rules" class="yuebon-setting-form"
    >
      <el-tabs
        v-model="activeName"
        type="border-card"
      >
        <el-tab-pane
          label="基本資料"
          name="first"
        >
          <el-form-item
            label="個人照片"
            :label-width="formLabelWidth" prop="HeadIcon"
          >
            <el-upload
              class="avatar-uploader"
              :action="httpFileUploadUrl"
              :headers="headers"
              :show-file-list="false"
              :on-success="uploadFileSuccess"
            >
              <img
                v-if="editFrom.HeadIcon"
                :src="editFrom.HeadIcon" class="avatar"
              >
              <i
                v-else
                class="el-icon-plus avatar-uploader-icon"
              />
            </el-upload>
            <el-dialog :visible.sync="dialogHeadIconVisible">
              <img
                width="100%"
                :src="dialogHeadIconImageUrl" alt=""
              >
            </el-dialog>
          </el-form-item>
          <el-form-item
            label="帳號"
            :label-width="formLabelWidth" prop="Account"
          >
            <el-input
              v-model="editFrom.Account"
              placeholder="請輸入登錄系統帳號" disabled
            />
          </el-form-item>
          <el-form-item
            label="姓名"
            :label-width="formLabelWidth" prop="RealName"
          >
            <el-input
              v-model="editFrom.RealName"
              placeholder="請輸入姓名" disabled
            />
          </el-form-item>
          <el-form-item
            label="暱稱"
            :label-width="formLabelWidth" prop="NickName"
          >
            <el-input
              v-model="editFrom.NickName"
              placeholder="請輸入暱稱" autocomplete="off" clearable
            />
          </el-form-item>
          <el-form-item
            label="性別"
            :label-width="formLabelWidth" prop="Gender"
          >
            <el-radio-group v-model="editFrom.Gender">
              <el-radio label="1">男</el-radio>
              <el-radio label="0">女</el-radio>
            </el-radio-group>
          </el-form-item>
          <el-form-item
            label="手機號碼"
            :label-width="formLabelWidth" prop="MobilePhone"
          >
            <el-input
              v-model="editFrom.MobilePhone"
              placeholder="請輸入手機號碼" autocomplete="off" clearable
            />
          </el-form-item>
          <el-form-item
            label="LINE ID"
            :label-width="formLabelWidth" prop="WeChat"
          >
            <el-input
              v-model="editFrom.WeChat"
              placeholder="請輸入LINE ID" autocomplete="off" clearable
            />
          </el-form-item>
          <el-form-item
            label="EMAIL"
            :label-width="formLabelWidth" prop="Email"
          >
            <el-input
              v-model="editFrom.Email"
              placeholder="請輸入Email @" autocomplete="off" clearable
            />
          </el-form-item>
          <el-form-item
            label="生日"
            :label-width="formLabelWidth" prop="Birthday"
          >
            <el-date-picker
              v-model="editFrom.Birthday"
              type="date"
              placeholder="選擇日期"
            />
          </el-form-item>
          <el-form-item
            label="選項"
            :label-width="formLabelWidth" prop=""
          >
            <el-checkbox
              v-model="editFrom.EnabledMark"
              disabled
            >啟用</el-checkbox>
            <el-checkbox
              v-model="editFrom.IsAdministrator"
              disabled
            >管理員</el-checkbox>
          </el-form-item>
          <el-form-item
            label="公司/單位"
            style="width:500px" :label-width="formLabelWidth" prop="DepartmentId"
          >
            <el-cascader
              v-model="selectedOrganizeOptions"
              style="width:500px;"
              :options="selectOrganize"
              filterable
              :props="{label:'FullName',value:'Id',children:'Children',emitPath:false, checkStrictly: true,expandTrigger: 'hover' }"
              clearable
              @change="handleSelectOrganizeChange"
            />
          </el-form-item>
          <el-form-item
            label="角色"
            :label-width="formLabelWidth" prop="RoleId"
          >
            <el-select
              v-model="editFrom.RoleId"
              style="width:500px"
              multiple
              clearable
              disabled
              placeholder="請選擇"
            >
              <el-option
                v-for="item in selectRole"
                :key="item.Id"
                :label="item.FullName"
                :value="item.Id"
              />
            </el-select>
          </el-form-item>
          <el-form-item
            label="備註"
            :label-width="formLabelWidth" prop="Description"
          >
            <el-input
              v-model="editFrom.Description"
              style="width:500px" placeholder="" autocomplete="off" clearable
            />
          </el-form-item>
        </el-tab-pane>
      </el-tabs>
      <el-form-item>
        <el-button
          type="primary"
          class="btnset" @click="saveEditForm()"
        >存 檔</el-button>
      </el-form-item>
    </el-form>
  </div>
</template>
<script>

import { mapGetters } from 'vuex'
import defaultSettings from '@/settings'
import { getByUserName, saveUser } from '@/api/security/userservice'
import { getAllRoleList } from '@/api/security/roleservice'
import { getAllOrganizeTreeTable } from '@/api/security/organizeservice'

import { getToken } from '@/utils/auth'
export default {
  name: 'Usercenter',
  data() {
    return {
      activeName: 'first',
      selectRole: [],
      selectedOrganizeOptions: '',
      selectOrganize: [],
      editFrom: {
        Account: '',
        HeadIcon: '',
        RealName: '',
        NickName: '',
        Gender: '1',
        Birthday: '',
        MobilePhone: '',
        Email: '',
        WeChat: '',
        DepartmentId: '',
        RoleId: '',
        IsAdministrator: true,
        EnabledMark: true,
        Description: ''
      },
      rules: {
        RealName: [
          { required: true, message: '請輸入系統名稱', trigger: 'blur' },
          { min: 2, max: 50, message: '長度在 6 到 32 個字符', trigger: 'blur' }
        ],
        MobilePhone: [
          { required: true, message: '請輸入訪問地址', trigger: 'blur' },
          { min: 2, max: 50, message: '長度在 6 到 32 個字符', trigger: 'blur' }
        ]
      },
      httpFileUploadUrl: defaultSettings.fileUploadUrl,
      dialogHeadIconVisible: false,
      dialogHeadIconImageUrl: '',
      filelist: [],
      formLabelWidth: '100px',
      headers: []
    }
  },
  computed: {
    ...mapGetters([
      'name'
    ])
  },
  created() {
    this.InitDictItem()
    this.bindEditInfo()
    this.headers = { Authorization: 'Bearer ' + (getToken() || '') }
  },
  methods: {
    /**
     * 初始化數據
     */
    InitDictItem() {
      getAllRoleList().then(res => {
        this.selectRole = res.ResData
      })
      getAllOrganizeTreeTable().then(res => {
        this.selectOrganize = res.ResData
      })
    },
    handleClick(tab, event) {
      console.log(tab, event)
    },

    /**
     *選擇組織
     */
    handleSelectOrganizeChange: function() {
      this.editFrom.OrganizeId = this.selectedOrganizeOptions
    },
    handleRemove(file, fileList) {
      this.editFrom.SysLogo = file.url
    },
    handlePictureCardPreview(file) {
      this.dialogImageUrl = file.url
      this.dialogVisible = true
    },
    bindEditInfo: function() {
      getByUserName(this.name).then(res => {
        this.editFrom.Account = res.ResData.Account
        this.editFrom.RealName = res.ResData.RealName
        this.editFrom.NickName = res.ResData.NickName
        this.editFrom.Gender = res.ResData.Gender + ''
        this.editFrom.Birthday = res.ResData.Birthday
        this.editFrom.MobilePhone = res.ResData.MobilePhone
        this.editFrom.Email = res.ResData.Email
        this.editFrom.WeChat = res.ResData.WeChat
        this.editFrom.DepartmentId = res.ResData.DepartmentId
        this.editFrom.IsAdministrator = res.ResData.IsAdministrator
        this.editFrom.EnabledMark = res.ResData.EnabledMark
        this.editFrom.RoleId = res.ResData.RoleId.split(',')
        this.editFrom.Description = res.ResData.Description
        this.selectedOrganizeOptions = res.ResData.DepartmentId
        this.currentId = res.ResData.Id
        this.editFrom.HeadIcon = res.ResData.HeadIcon
        this.filelist = [{ name: res.ResData.HeadIcon, url: res.ResData.HeadIcon }]
      })
    },
    /**
     * 新增/修改保存
     */
    saveEditForm() {
      console.log(this.editFrom.RoleId)
      this.$refs['editFrom'].validate((valid) => {
        if (valid) {
          const data = {
            Account: this.editFrom.Account,
            RealName: this.editFrom.RealName,
            NickName: this.editFrom.NickName,
            Gender: this.editFrom.Gender,
            Birthday: this.editFrom.Birthday,
            MobilePhone: this.editFrom.MobilePhone,
            Email: this.editFrom.Email,
            WeChat: this.editFrom.WeChat,
            DepartmentId: this.editFrom.DepartmentId,
            IsAdministrator: this.editFrom.IsAdministrator,
            EnabledMark: this.editFrom.EnabledMark,
            RoleId: this.editFrom.RoleId.join(','),
            Description: this.editFrom.Description,
            HeadIcon: this.editFrom.HeadIcon,
            Id: this.currentId
          }
          var url = 'User/Update'
          saveUser(data, url).then(res => {
            if (res.Success) {
              this.$message({
                message: '恭喜你，操作成功',
                type: 'success'
              })
              this.selectedOrganizeOptions = ''
              this.$refs['editFrom'].resetFields()
              this.bindEditInfo()
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
    },
    uploadFileSuccess: function(response, file, fileList) {
      this.editFrom.HeadIcon = defaultSettings.fileUrl + response.ResData.FilePath
    }
  }
}
</script>
<style lang="scss" scoped>
.yuebon-setting-form .el-tab-pane{
  width: 40%;
}
.yuebon-setting-form .el-input{
width: 100%;
}
.yuebon-setting-form .el-select .el-input,.yuebon-setting-form .el-cascader .el-input{
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
