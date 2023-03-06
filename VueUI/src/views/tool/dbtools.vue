<template>
  <div class="app-container">
    <el-card>
      <div class="yuebon-app-leftcontainer">
        <el-form ref="editFrom" :model="editFrom" :rules="rules" label-position="right" class="yuebon-db-editform">
          <el-form-item label="資料庫位置" :label-width="formLabelWidth" prop="DbAddress">
            <el-input v-model="editFrom.DbAddress" placeholder="請輸入資料庫位置" autocomplete="off" clearable />
          </el-form-item>
          <el-form-item label="資料庫名稱" :label-width="formLabelWidth" prop="DbName">
            <el-input v-model="editFrom.DbName" placeholder="請輸入資料庫名稱" autocomplete="off" clearable />
          </el-form-item>
          <el-form-item label="用戶名" :label-width="formLabelWidth" prop="DbUserName">
            <el-input v-model="editFrom.DbUserName" placeholder="請輸入用戶名" autocomplete="off" clearable />
          </el-form-item>
          <el-form-item label="訪問密碼" :label-width="formLabelWidth" prop="DbPassword">
            <el-input v-model="editFrom.DbPassword" placeholder="請輸入訪問密碼" autocomplete="off" clearable />
          </el-form-item>
          <el-form-item label="資料庫類型" :label-width="formLabelWidth" prop="DbType">
            <el-select v-model="editFrom.DbType" clearable placeholder="請選資料庫類型">
              <el-option
                v-for="item in selectDbTypes"
                :key="item.Id"
                :label="item.Title"
                :value="item.Id"
              />
            </el-select>
          </el-form-item>
          <el-form-item>
            <el-button type="primary" @click="handleEncrypt()">生成連線字串</el-button>
          </el-form-item>
        </el-form>
      </div>

      <div class="yuebon-app-rightcontainer">
        <el-form ref="resultFrom" :model="resultFrom" :rules="rules" label-position="top" class="yuebon-db-editform">
          <el-form-item label="資料庫連線字串" :label-width="formLabelWidth" prop="ConnStr">
            <el-input v-model="resultFrom.ConnStr" type="textarea" :autosize="{ minRows: 2, maxRows: 4}" placeholder="資料庫連線字串" autocomplete="off" clearable />
          </el-form-item>
          <el-form-item label="加密資料庫連線字串" :label-width="formLabelWidth" prop="EncryptConnStr">
            <el-input v-model="resultFrom.EncryptConnStr" type="textarea" :autosize="{ minRows: 2, maxRows: 4}" placeholder="資料庫連線字串" autocomplete="off" clearable />
          </el-form-item>
          <el-form-item>
            <el-button type="primary" @click="handleDecrypt()">解密資料庫連線字串</el-button>
          </el-form-item>
        </el-form>
      </div>
    </el-card>
  </div>
</template>

<script>

import { dbtoolsConnStrDecrypt, dbtoolsConnStrEncrypt } from '@/api/developers/toolsservice'
export default {
  name: 'DbTools',
  data() {
    return {
      selectDbTypes: [{
        Id: 'SqlServer',
        Title: 'SqlServer'
      }, {
        Id: 'MySql',
        Title: 'MySql'
      }],
      editFrom: {
        DbAddress: '',
        DbName: '',
        DbUserName: '',
        DbPassword: '',
        DbType: ''
      },
      rules: {
        DbAddress: [
          { required: true, message: '請輸入數據庫地址', trigger: 'blur' },
          { min: 1, max: 50, message: '長度在 2 到 50 個字符', trigger: 'blur' }
        ],
        DbName: [
          { required: true, message: '請輸入數據庫名稱', trigger: 'blur' },
          { min: 2, max: 50, message: '長度在 2 到 50 個字符', trigger: 'blur' }
        ],
        DbUserName: [
          { required: true, message: '請輸入用戶名', trigger: 'blur' },
          { min: 1, max: 50, message: '長度在 1 到 50 個字符', trigger: 'blur' }
        ],
        DbPassword: [
          { required: true, message: '請輸入訪問密碼', trigger: 'blur' },
          { min: 1, max: 50, message: '長度在 1 到 50 個字符', trigger: 'blur' }
        ],
        DbType: [
          { required: true, message: '請選數據庫類型', trigger: 'blur' }
        ]
      },
      resultFrom: {
        ConnStr: '',
        EncryptConnStr: ''
      },
      rulesresultFrom: {
        EncryptConnStr: [
          { required: true, message: '加密數據庫連接字符串', trigger: 'blur' },
          { min: 5, max: 50, message: '長度在 5 到 50 個字符', trigger: 'blur' }
        ]
      },
      formLabelWidth: '120px'
    }
  },
  created() {
  },
  methods: {
    /**
     * 解密數據庫
     */
    handleDecrypt: function() {
      this.$refs['resultFrom'].validate((valid) => {
        if (valid) {
          var dataInfo = {
            strConn: this.resultFrom.EncryptConnStr
          }
          dbtoolsConnStrDecrypt(dataInfo).then(res => {
            if (res.Success) {
              this.resultFrom.ConnStr = res.ResData.ConnStr

              this.$message({
                message: '恭喜你，操作成功',
                type: 'success'
              })
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
    /**
     * 生成連接字符串
     */
    handleEncrypt: function() {
      this.$refs['editFrom'].validate((valid) => {
        if (valid) {
          var dataInfo = {
            DbAddress: this.editFrom.DbAddress,
            DbName: this.editFrom.DbName,
            DbUserName: this.editFrom.DbUserName,
            DbPassword: this.editFrom.DbPassword,
            DbType: this.editFrom.DbType
          }
          dbtoolsConnStrEncrypt(dataInfo).then(res => {
            if (res.Success) {
              this.resultFrom.ConnStr = res.ResData.ConnStr
              this.resultFrom.EncryptConnStr = res.ResData.EncryptConnStr

              this.$message({
                message: '恭喜你，操作成功',
                type: 'success'
              })
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
