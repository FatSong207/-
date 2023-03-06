<template>
  <div class="registWrapper">

    <div class="header">
      <div class="logo-container">
        <img :src="companyLogo">
      </div>
    </div>
    <div class="register-container">
      <div class="title-container">
        <h3 class="title">歡迎注冊{{ softName }}</h3>
      </div>
      <el-form
        ref="registerForm"
        :model="editFrom"
        :rules="registerRules"
        label-position="right"
        class="registerForm"
      >
        <el-form-item prop="Account">
          <el-input v-model="editFrom.Account" placeholder="請輸入登錄系統帳號" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item prop="Email">
          <el-input v-model="editFrom.Email" placeholder="請輸入Email @" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item prop="Password">
          <el-input v-model="editFrom.Password" type="password" placeholder="請輸入密碼" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item prop="Password2">
          <el-input v-model="editFrom.Password2" type="password" placeholder="請再輸入一遍密碼" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item prop="VerificationCode">
          <el-input v-model="editFrom.VerificationCode" placeholder="請輸入驗證碼" style="width:150px; float:left;" maxlength="4" autocomplete="off" clearable />

          <div style="display:inline; float:left;margin-right:10px;margin-left:10px;">
            <img :src="verifyCodeUrl" style="cursor: pointer;vertical-align: middle;" alt="看不清？點擊更換" @click="getLoginVerifyCode">
          </div>
        </el-form-item>
        <el-form-item prop="checkAgreement">
          <el-checkbox-group v-model="editFrom.checkAgreement">
            <el-checkbox label="true">我已閱讀并同意<a href="#"> 服務協議</a> 、 <a href="#">隱私聲明</a></el-checkbox>
          </el-checkbox-group>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" style="width:200px;" @click="handleLogin()">同意協議并提交</el-button>
        </el-form-item>

        <el-form-item>
          <router-link :to="{path:'/'}">登錄已有帳號</router-link>
        </el-form-item>
      </el-form>
    </div>

    <div id="footer" class="footer" role="contentinfo">
      <div class="footerNodelf text-secondary">
        <span>本軟件使用權屬于：{{ companyName }}</span>
      </div>
      <div class="footerNode text-secondary">
        <span v-html="copyRight">{{ copyRight }}</span>
      </div>
    </div>
  </div>
</template>

<script>
import { setToken } from '@/utils/auth'
import { getToken, getSysSetting, getVerifyCode } from '@/api/basebasic'
import { registerUser } from '@/api/security/userservice'

export default {
  name: 'Register',
  data() {
    const validateUsername = (rule, value, callback) => {
      if (value.length < 1) {
        callback(new Error('請輸入登錄帳號！'))
      } else {
        callback()
      }
    }
    const validatePassword = (rule, value, callback) => {
      if (value.length < 6) {
        callback(new Error('請輸入您的帳號密碼,且不小于6位！'))
      } else {
        callback()
      }
    }
    const validateemail = (rule, value, callback) => {
      const mailReg = /^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+(.[a-zA-Z0-9_-])+/
      if (!value) {
        return callback(new Error('郵箱不能為空'))
      }
      setTimeout(() => {
        if (mailReg.test(value)) {
          callback()
        } else {
          callback(new Error('請輸入正確的郵箱格式'))
        }
      }, 100)
    }
    const validatePass2 = (rule, value, callback) => {
      if (value === '') {
        callback(new Error('請再次輸入密碼'))
      } else if (value !== this.editFrom.Password) {
        callback(new Error('兩次輸入密碼不一致!'))
      } else {
        callback()
      }
    }
    return {
      editFrom: {
        Account: '',
        Email: '',
        Password: '',
        Password2: '',
        VerificationCode: '',
        VerifyCodeKey: '',
        checkAgreement: []
      },
      registerRules: {
        Account: [
          { required: true, trigger: 'blur', validator: validateUsername }
        ],
        Password: [
          { required: true, trigger: 'blur', validator: validatePassword }
        ],
        Password2: [
          { required: true, trigger: 'blur', validator: validatePass2 }
        ],
        Email: [
          { required: true, trigger: 'blur', validator: validateemail }
        ],
        VerificationCode: [
          { required: true, message: '請輸入驗證碼', trigger: 'blur' },
          { min: 4, max: 4, message: '長度4字符', trigger: 'blur' }
        ],
        checkAgreement: [
          { type: 'array', required: true, trigger: 'change', message: '請勾選同意協議內容' }]
      },
      verifyCodeUrl: '',
      formLabelWidth: '100px',
      loading: false,
      softName: '管理系統',
      companyLogo: '/assets/images/login-logo.png',
      companyName: '',
      copyRight: ''
    }
  },
  created() {
    this.loadToken()
    this.getLoginVerifyCode()
  },
  methods: {

    loadToken() {
      getToken().then(response => {
        setToken(response.ResData.AccessToken)
        getSysSetting().then(res => {
          this.softName = res.ResData.SoftName
          this.companyLogo = res.ResData.SysLogo
          this.companyName = res.ResData.CompanyName
          this.copyRight = res.ResData.CopyRight
        })
      })
    },
    handleLogin() {
      this.$refs['registerForm'].validate((valid) => {
        if (valid) {
          if (!this.editFrom.checkAgreement) {
            new Error('請勾選同意協議內容')
            return
          }
          this.loading = true
          const data = this.editFrom
          registerUser(data).then(res => {
            if (res.Success) {
              this.$message({
                message: '恭喜你，注冊成功',
                type: 'success'
              })
              setTimeout(
                this.$router.push('/')
                , 1000)
            } else {
              this.$message({
                message: res.ErrMsg,
                type: 'error'
              })
            }
            this.loading = false
          }).catch(res => {
            this.loading = false
          })
        } else {
          return false
        }
      })
    },
    // 獲取驗證碼
    async getLoginVerifyCode() {
      this.editFrom.VerificationCode = ''
      const res = await getVerifyCode()
      if (res.Success) {
        this.verifyCodeUrl = 'data:image/png;base64,' + res.ResData.Img
        this.editFrom.VerifyCodeKey = res.ResData.Key
      }
    }
  }
}
</script>

<style lang="scss">
$bg: #283443;
$light_gray: rgb(100, 95, 95);
$cursor: #000000;
body{
  background: #f5f6f7;
color: #000;
}
@supports (-webkit-mask: none) and (not (cater-color: $cursor)) {
  .register-container .el-input input {
    color: $cursor;
  }
}
.header{
  width: 100%;
  .logo-container{
  margin-top: 10px;
  float: left;
  }
}
.registWrapper{

.header{
  width: 100%;
  height: 60px;
  border-bottom: 1px solid #999;
  .logo-container{

  margin-top: 10px;
  float: left;
  }
}
}

.register-container {
 width: 800px;
 margin: 30px auto 30px auto;
 background: #ffffff;
 padding: 20px;
 .title-container{
margin: 20px auto;
  text-align: center;
  .title{
    font-size: 28px;
  }
 }
 .registerForm{
   width: 300px;
   margin: 0 auto;
 }
}
</style>

<style lang="scss" scoped>
$dark_gray: #5e6163;
$light_gray: #eee;

  .footer {
    text-align: center;
 width: 800px;
 margin: 30px auto 0 auto;
  }
  div.footerNodelf {
    float: left;
  }

  div.footerNode {
    float: right;
  }
  .text-secondary {
    font-size: 13px;
    line-height: 25px;
  }
</style>
