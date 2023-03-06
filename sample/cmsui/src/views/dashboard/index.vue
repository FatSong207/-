<template>
  <div class="app-container ">
    <el-row :gutter="40" class="panel-group">
      <el-col :xs="12" :sm="12" :lg="6" class="card-panel-col">
        <div class="card-panel">
          <div class="card-panel-icon-wrapper icon-people">
            <svg-icon icon-class="friend" class="card-panel-icon" />
          </div>
          <div class="card-panel-description">
            <div class="card-panel-text">
              用戶
            </div>
            <countto :start-val="0" :end-val="SysSetting.TotalUser" :duration="2600" class="card-panel-num" />
          </div>
        </div>
      </el-col>
      <el-col :xs="12" :sm="12" :lg="6" class="card-panel-col">
        <div class="card-panel">
          <div class="card-panel-icon-wrapper icon-message">
            <svg-icon icon-class="menu-module" class="card-panel-icon" />
          </div>
          <div class="card-panel-description">
            <div class="card-panel-text">
              模塊
            </div>
            <countto :start-val="0" :end-val="SysSetting.TotalModule" :duration="3000" class="card-panel-num" />
          </div>
        </div>
      </el-col>
      <el-col :xs="12" :sm="12" :lg="6" class="card-panel-col">
        <div class="card-panel">
          <div class="card-panel-icon-wrapper icon-money">
            <svg-icon icon-class="juese" class="card-panel-icon" />
          </div>
          <div class="card-panel-description">
            <div class="card-panel-text">
              崗位角色
            </div>
            <countto :start-val="0" :end-val="SysSetting.TotalRole" :duration="3200" class="card-panel-num" />
          </div>
        </div>
      </el-col>
      <el-col :xs="12" :sm="24" :lg="6" class="card-panel-col">
        <div class="card-panel">
          <div class="card-panel-icon-wrapper icon-shopping">
            <svg-icon icon-class="log" class="card-panel-icon" />
          </div>
          <div class="card-panel-description">
            <div class="card-panel-text">
              日志
            </div>
            <countto :start-val="0" :end-val="SysSetting.TotalLog" :duration="3600" class="card-panel-num" />
          </div>
        </div>
      </el-col>
    </el-row>
    <el-row :gutter="20" class="panel-group">
      <el-col :xs="24" :sm="24" :lg="24">
        <el-card>
          <div slot="header" class="clearfix">
            <span class="iconfont icon-notice">系統公告</span>
          </div>
          <div class="systeminfo">
            <div>煩請各位大神不要修改test用戶密碼</div>
            <div>數據庫數據每小時會重置一次</div>
            <div>開源地址:<a href="https://gitee.com/yuebon/YuebonNetCore" target="_blank">https://gitee.com/yuebon/YuebonNetCore</a></div>
            <div>如果覺得不錯歡迎給個“star”</div>
          </div>
        </el-card>
      </el-col>
    </el-row>

    <el-row :gutter="20" class="panel-group">
      <el-col :span="12" class="card-box">
        <el-card>
          <div slot="header">
            <span class="iconfont icon-about">系統信息</span>
          </div>
          <div class="el-table el-table--enable-row-hover el-table--medium">
            <table cellspacing="0" style="width: 100%;">
              <tbody>
                <tr>
                  <td><div class="cell">系統名稱</div></td>
                  <td><div class="cell">{{ SysSetting.Title }}</div></td>
                  <td><div class="cell">系統版本</div></td>
                  <td><div class="cell">{{ SysSetting.Version }}（<a :href="SysSetting.UpdateUrl" target="_blank">更新</a>）</div></td>
                </tr>
                <tr>
                  <td><div class="cell">域名</div></td>
                  <td><div class="cell">{{ SysSetting.WebUrl }}</div></td>
                  <td><div class="cell">授權方式</div></td>
                  <td><div class="cell">按域名授權給({{ SysSetting.CertificatedCompany }})</div></td>
                </tr>
                <tr>
                  <td><div class="cell">運行時的版本號</div></td>
                  <td><div class="cell">{{ SysSetting.FrameworkDescription }}</div></td>
                  <td><div class="cell">應用端口</div></td>
                  <td><div class="cell">{{ SysSetting.Port }}</div></td>
                </tr>
                <tr>
                  <td><div class="cell">開發團隊</div></td>
                  <td><div class="cell">{{ SysSetting.Manufacturer }}</div></td>
                  <td><div class="cell">官方文檔：</div></td>
                  <td><div class="cell"><a href="http://docs.v.yuebon.com" target="_blank">http://docs.v.yuebon.com</a></div></td>
                </tr>
              </tbody>
            </table>
          </div>
        </el-card>
      </el-col>
      <el-col :span="12" class="card-box">
        <el-card>
          <div slot="header">
            <span>服務器信息</span>
          </div>
          <div class="el-table el-table--enable-row-hover el-table--medium">
            <table cellspacing="0" style="width: 100%;">
              <tbody>
                <tr>
                  <td><div class="cell">服務器名稱</div></td>
                  <td><div v-if="SysSetting.MachineName" class="cell">{{ SysSetting.MachineName }}</div></td>
                  <td><div class="cell">操作系統</div></td>
                  <td><div v-if="SysSetting.OSDescription" class="cell">{{ SysSetting.OSDescription }}</div></td>
                </tr>
                <tr>
                  <td><div class="cell">服務器IP</div></td>
                  <td><div class="cell">{{ SysSetting.IPAdress }}</div></td>
                  <td><div class="cell">CPU數量</div></td>
                  <td><div class="cell">{{ SysSetting.ProcessorCount }}</div></td>
                </tr>
                <tr>
                  <td><div class="cell">內存頁</div></td>
                  <td><div class="cell">{{ SysSetting.SystemPageSize/1024 }}Kb</div></td>
                  <td><div class="cell">應用架構</div></td>
                  <td><div class="cell">{{ SysSetting.ProcessArchitecture }}</div></td>
                </tr>
                <tr>
                  <td><div class="cell">運行時長</div></td>
                  <td><div class="cell">{{ SysSetting.RunTimeLength }}</div></td>
                  <td><div class="cell">使用內存</div></td>
                  <td><div class="cell">{{ SysSetting.WorkingSet/1024 }}Kb</div></td>
                </tr>
              </tbody>
            </table>
          </div>
        </el-card>
      </el-col>
    </el-row>
  </div>
</template>

<script>
import countto from 'vue-count-to'
import { getSysInfo } from '@/api/basebasic'
import { getUrlKey } from '@/utils/index'
import defaultSettings from '@/settings'

export default {
  name: 'Dashboard',
  components: { countto },
  data() {
    return {
      SysSetting: [],
      syskey: ''
    }
  },
  created() {
    this.syskey = getUrlKey('openmf')
    if (this.syskey !== '' && this.syskey !== null && this.syskey !== 'null' && this.syskey !== undefined) {
      this.loadsysType()
    }
    this.InitDictItem()
  },
  methods: {
    /**
     * 初始化數據
     */
    InitDictItem() {
      getSysInfo().then(res => {
        this.SysSetting = res.ResData
      })
    },
    loadsysType() {
      var data = {
        openmf: this.syskey,
        appId: defaultSettings.appId,
        systemCode: defaultSettings.activeSystemCode
      }
      this.loading = true
      this.$store
        .dispatch('user/sysConnetLogin', data)
        .then(res => {
          // window.location.href = res.ResData.ActiveSystemUrl
          this.loading = false
        })
        .catch(res => {
          this.loading = false
        })
    }
  }
}
</script>

<style lang="scss" scoped>
.panel-group {
  margin-top: 18px;
  .card-panel-col {
    margin-bottom: 32px;
  }
  .card-panel {
    height: 108px;
    cursor: pointer;
    font-size: 12px;
    position: relative;
    overflow: hidden;
    color: #666;
    background: #fff;
    box-shadow: 4px 4px 40px rgba(0, 0, 0, .05);
    border-color: rgba(0, 0, 0, .05);
    &:hover {
      .card-panel-icon-wrapper {
        color: #fff;
      }
      .icon-people {
        background: #40c9c6;
      }
      .icon-message {
        background: #36a3f7;
      }
      .icon-money {
        background: #f4516c;
      }
      .icon-shopping {
        background: #34bfa3
      }
    }
    .icon-people {
      color: #40c9c6;
    }
    .icon-message {
      color: #36a3f7;
    }
    .icon-money {
      color: #f4516c;
    }
    .icon-shopping {
      color: #34bfa3
    }
    .card-panel-icon-wrapper {
      float: left;
      margin: 14px 0 0 14px;
      padding: 16px;
      transition: all 0.38s ease-out;
      border-radius: 6px;
    }
    .card-panel-icon {
      float: left;
      font-size: 48px;
    }
    .card-panel-description {
      float: right;
      font-weight: bold;
      margin: 26px;
      margin-left: 0px;
      .card-panel-text {
        line-height: 18px;
        color: rgba(0, 0, 0, 0.45);
        font-size: 16px;
        margin-bottom: 12px;
      }
      .card-panel-num {
        font-size: 20px;
      }
    }
  }
}
@media (max-width:550px) {
  .card-panel-description {
    display: none;
  }
  .card-panel-icon-wrapper {
    float: none !important;
    width: 100%;
    height: 100%;
    margin: 0 !important;
    .svg-icon {
      display: block;
      margin: 14px auto !important;
      float: none !important;
    }
  }
}
.systeminfo{
  line-height: 25px;
    float: left;
    width: 50%;
    margin-bottom: 20px;
  .lidiv{
  line-height: 25px;
  }
}
</style>
