<template>
  <el-card
    v-loading="loading"
    v-loading.lock="loading"
    element-loading-text="下載中..."
    element-loading-spinner="el-icon-loading"
  >
    <h2>{{ formid }}_{{ formname }}</h2>
    <el-form
      ref="searchform"
      :inline="true"
      :model="searchPDF"
      class="demo-form-inline"
      size="small"
    >
      <el-row>
        <el-form-item
          v-if="reqln === 'Y'"
          label="出租人身分證字號/居留證號："
        >
          <el-input
            v-model="searchPDF.LNID"
            clearable
            :disabled="LNIDdisable"
            maxlength="10"
          />
        </el-form-item>
        <el-form-item
          v-if="reqlc === 'Y'"
          label="出租人統一編號："
        >
          <el-input
            v-model="searchPDF.LCID"
            clearable
            :disabled="LCIDdisable"
            maxlength="8"
          />
        </el-form-item>
        <el-form-item
          v-if="checkResult"
          label=""
          style="color: red"
        >
          <b>*{{ checkResultText }}<el-button
            v-if="checkResultText !== '你沒有操作此出租人的權限'"
            size="small"
            type="success"
            @click="InserCustomerL()"
          >新增此出租人</el-button></b>
        </el-form-item>
      </el-row>
      <el-row>
        <el-form-item
          v-if="reqrn === 'Y'"
          label="承租人身分證字號/居留證號："
        >
          <el-input
            v-model="searchPDF.RNID"
            clearable
            :disabled="RNIDdisable"
            maxlength="10"
          />
        </el-form-item>
        <el-form-item
          v-if="reqrc === 'Y'"
          label="承租人統一編號："
        >
          <el-input
            v-model="searchPDF.RCID"
            clearable
            :disabled="RCIDdisable"
            maxlength="8"
          />
        </el-form-item>
        <el-form-item
          v-if="checkResult2"
          label=""
          style="color: red"
        >
          <b>*{{ checkResult2Text }} <el-button
            v-if="checkResult2Text !== '你沒有操作此承租人的權限'"
            size="small"
            type="success"
            @click="InserCustomerR()"
          >新增此承租人</el-button></b>
        </el-form-item>
      </el-row>
      <el-row v-if="reqb === 'Y'">
        <el-form-item label="建物地址：">
          <Address
            title="建物地址"
            @getsearchaddress="getsearchaddress"
          />
        </el-form-item>
      </el-row>
      <!-- <el-row v-if="reqln === 'Y' || reqrn === 'Y' || reqlc === 'Y' || reqrc === 'Y'">
        <el-form-item>
          <el-checkbox v-model="searchPDF.RemittanceCheck">指定匯款資訊</el-checkbox>
        </el-form-item>
      </el-row> -->
      <!-- <div v-show="searchPDF.RemittanceCheck"> -->
      <div v-if="reqln === 'Y' || reqlc === 'Y'">
        <el-form-item label="出租人匯款資訊：">
          <el-select
            v-model="searchPDF.LRemittanceId"
            placeholder="請選擇"
            style="width: 250px"
            clearable
          >
            <el-option
              value="new"
              label="新增匯款資料"
            >
              <span style="float: left">新增匯款資料</span>
              <span style="float: right; color: #8492a6; font-size: 13px" />
            </el-option>
            <el-option
              v-for="item in LRemittanceArr"
              :key="item.Id"
              :label="item.LAName+' _ '+(item.LAID ===null?'':item.LAID)"
              :value="item.Id"
            >
              <span style="float: left">{{ item.LAName }}</span>
              <span style="float: right; color: #8492a6; font-size: 13px">{{ item.LBankNo }}－{{ item.LANo }}</span>
            </el-option>
          </el-select>
        </el-form-item>
      </div>
      <div v-if="reqrn === 'Y'">
        <el-form-item label="承租人匯款資訊：">
          <el-select
            v-model="searchPDF.RRemittanceId"
            placeholder="請選擇"
            style="width: 250px"
            clearable
          >
            <el-option
              v-for="item in RRemittanceArr"
              :key="item.Id"
              :label="item.RAName+' _ '+item.RAID"
              :value="item.Id"
            >
              <span style="float: left">{{ item.RAName }}</span>
              <span style="float: right; color: #8492a6; font-size: 13px">{{ item.RBankNo }}－{{ item.RANo }}</span>
            </el-option>
          </el-select>
        </el-form-item>
      </div>
      <!-- </div> -->
      <el-row>
        <el-form-item>
          <el-checkbox v-model="searchPDF.ReplaceROFieldCheck">以下列指定資訊，取代此表單欄位內容</el-checkbox>
        </el-form-item>
      </el-row>
      <div v-show="searchPDF.ReplaceROFieldCheck">
        <el-row>
          <h2>選擇租屋公司</h2>
        </el-row>
        <el-row>
          <el-form-item label="名稱：">
            <el-select
              v-model="searchPDF.ROName"
              style="width: 300px"
              placeholder="請選擇公司名稱"
              @change="RONameChange()"
            >
              <el-option
                v-for="item in allCompanyName"
                :key="item.Id"
                :label="item.Name"
                :value="item.Name"
              />
            </el-select>
          </el-form-item>
        </el-row>
        <el-row>
          <el-form-item label="地址：">
            <el-select
              v-model="searchPDF.ROAdd"
              style="width: 300px"
              placeholder="請選擇公司地址"
              @change="ROAddChange()"
            >
              <el-option
                v-for="item in address"
                :key="item.Id"
                :label="item.Address"
                :value="item.Address"
              />
            </el-select>
          </el-form-item>
        </el-row>
        <el-row>
          <el-form-item label="電話：">
            <el-select
              v-model="searchPDF.ROTel"
              style="width: 300px"
              placeholder="請選擇公司電話"
              @change="ROTelChange()"
            >
              <el-option
                v-for="item in Tel"
                :key="item.Id"
                :label="item.Tel"
                :value="item.Tel"
              />
            </el-select>
          </el-form-item>
        </el-row>
        <el-row>
          <el-form-item label="傳真：">
            <el-input
              v-model="searchPDF.ROFax"
              style="width: 120px"
              placeholder="自動帶入"
              readonly
              clearable
            />
          </el-form-item>
          <el-form-item label="統一編號：">
            <el-input
              v-model="searchPDF.ROID"
              style="width: 120px"
              placeholder="自動帶入"
              readonly
              clearable
            />
          </el-form-item>
          <el-row>
            <el-form-item label="負責人：">
              <el-input
                v-model="searchPDF.RORep"
                style="width: 100px"
                placeholder="自動帶入"
                readonly
                clearable
              />
            </el-form-item>
            <el-form-item label="營業登記證：">
              <el-input
                v-model="searchPDF.ROLRNo"
                style="width: 240px"
                placeholder="自動帶入"
                readonly
                clearable
              />
            </el-form-item>
          </el-row>
          <el-row>
            <el-form-item label="經紀人：">
              <el-input
                v-model="searchPDF.AGName"
                style="width: 100px"
                placeholder="自動帶入"
                readonly
                clearable
              />
            </el-form-item>
            <el-form-item label="經紀人證書字號：">
              <el-input
                v-model="searchPDF.AGLRNo"
                style="width: 240px"
                placeholder="自動帶入"
                readonly
                clearable
              />
            </el-form-item>
          </el-row>
          <el-row>
            <el-form-item label="住宅管理人：">
              <el-input
                v-model="searchPDF.RHMName"
                style="width: 100px"
                placeholder="自動帶入"
                readonly
                clearable
              />
            </el-form-item>
            <el-form-item label="住宅管理人證書字號：">
              <el-input
                v-model="searchPDF.RHMRNo"
                style="width: 240px"
                placeholder="自動帶入"
                readonly
                clearable
              />
            </el-form-item>
            <el-form-item label="事業服務人員：">
              <el-input
                v-model="name"
                style="width: 100px"
                readonly
                clearable
              />
            </el-form-item>
          </el-row>
        </el-row>
        <!-- <el-row>
        <el-form-item label="名稱：">
          <el-select
            v-model="searchPDF.ROName"
            style="width: 300px"
            placeholder="請選擇公司名稱"
            @change="RONameChange()"
          >
            <el-option
              v-for="item in allCompanyName"
              :key="item.Id"
              :label="item.Name"
              :value="item.Name"
            />
          </el-select>
        </el-form-item>
      </el-row>
      <el-row>
        <el-form-item label="地址：">
          <el-select
            v-model="searchPDF.ROAdd"
            style="width: 300px"
            placeholder="請選擇公司地址"
            @change="ROAddChange()"
          >
            <el-option
              v-for="item in address"
              :key="item.Id"
              :label="item.Address"
              :value="item.Address"
            />
          </el-select>
        </el-form-item>
      </el-row>
      <el-row>
        <el-form-item label="電話：">
          <el-select
            v-model="searchPDF.ROTel"
            style="width: 300px"
            placeholder="請選擇公司電話"
            @change="ROTelChange()"
          >
            <el-option
              v-for="item in Tel"
              :key="item.Id"
              :label="item.Tel"
              :value="item.Tel"
            />
          </el-select>
        </el-form-item>
      </el-row>
      <el-row>
        <el-form-item label="負責人：">
          <el-input
            v-model="searchPDF.RORep"
            style="width: 100px"
            placeholder="自動帶入"
            readonly
            clearable
          />
        </el-form-item>
        <el-form-item label="營業登記證：">
          <el-input
            v-model="searchPDF.ROLRNo"
            style="width: 240px"
            placeholder="自動帶入"
            readonly
            clearable
          />
        </el-form-item>
        <el-row>
          <el-form-item label="統一編號：">
            <el-input
              v-model="searchPDF.ROID"
              style="width: 120px"
              placeholder="自動帶入"
              readonly
              clearable
            />
          </el-form-item>
          <el-form-item label="住宅管理人員：">
            <el-input
              v-model="searchPDF.RHMName"
              style="width: 120px"
              placeholder="自動帶入"
              readonly
              clearable
            />
          </el-form-item>
          <el-form-item label="事業服務人員：">
            <el-input
              v-model="name"
              style="width: 100px"
              readonly
              clearable
            />
          </el-form-item>
        </el-row>
      </el-row> -->
      </div>
      <el-button
        type="primary"
        icon="el-icon-download"
        size="small"
        @click="Jump()"
      >確 認</el-button>
    </el-form>
    <el-dialog
      ref="dialogEditForm"
      title="請指定建物屬性"
      :show-close="false"
      :close-on-press-escape="false"
      :visible.sync="dialogEditFormVisible"
      width="640px"
      append-to-body
    >
      <el-card class="box-card">
        <el-form
          ref="editFrom"
          :inline="true"
          class="demo-form-inline"
        >
          <el-form-item label="出租人身份證字號/統一編號：">
            <el-input
              v-model="LNIDLCID"
              disabled
            />
          </el-form-item>
          <el-form-item label="建物類型：">
            <el-select
              v-model="select"
              placeholder="請選擇"
              clearable
              style="width: 200px"
            >
              <el-option
                v-for="item in bpropotiesOptions"
                :key="item.Id"
                :label="item.BuildingPropotyName"
                :value="item.BuildingPropotyName"
              />
            </el-select>
          </el-form-item>
        </el-form>
      </el-card>
      <div
        slot="footer"
        class="dialog-footer"
      >
        <el-button @click="dialogEditFormVisible = false">關 閉</el-button>
        <el-button
          type="primary"
          @click="CreateBindBuilding()"
        >創建並綁定</el-button>
      </div>
    </el-dialog>
  </el-card>

</template>

<script>
import {
  DownloadWithData,
  CheckCustomerLNExists,
  CheckCustomerRNExists,
  CheckCustomerBbuildingExists
} from '@/api/chaochi/internalform/internalform';
import { GetRemittancesByIDNo } from '@/api/chaochi/remittance/remittancel';
import { GetRemittancesByIDNoR } from '@/api/chaochi/remittance/remittanceR';
import {
  GetAllCompanyName,
  GetAddressByCompanyName,
  GetTelByCompanyNameAndAddress,
  GetDataByCAT
} from '@/api/chaochi/slma/slmaservice';
import { saveCustomerLN } from '@/api/chaochi/landlordnaturalperson/customerlnservice';
import { saveCustomerRN } from '@/api/chaochi/renter/customerrnservice';
import { saveCustomerLC } from '@/api/chaochi/landlordcomapnyperson/customerlcservice';
import { InsertFromWebForm } from '@/api/chaochi/building/buildingservice';
import { mergerAddress } from '@/utils/index';
import Address from '@/components/Address/Address.vue';
import { mapGetters } from 'vuex';
export default {
  name: 'DownLoadPdfin',
  components: { Address },
  props: {
    formid: { type: String, default: '' },
    formname: { type: String, default: '' },
    reqb: { type: String, default: 'Y' },
    reqln: { type: String, default: 'Y' },
    reqrn: { type: String, default: 'Y' },
    reqlc: { type: String, default: 'Y' },
    reqrc: { type: String, default: 'Y' }
  },
  data() {
    return {
      searchPDF: {
        RemittanceCheck: false,
        ReplaceROFieldCheck: false,
        LRemittanceId: '',
        RRemittanceId: '',
        LNID: '',
        LCID: '',
        RNID: '',
        RCID: '',
        SLID: '',
        ROName: '',
        ROAdd: '',
        ROTel: '',
        RORep: '',
        ROLRNo: '',
        ROID: '',
        RHMName: '',
        FormId: '',
        ROFax: '',
        RHMRNo: ''
      },
      allCompanyName: [],
      address: [],
      Tel: [],
      slmaid: '',
      select: '',
      checkResultText: '',
      checkResult2Text: '',
      LCIDdisable: false,
      LNIDdisable: false,
      RNIDdisable: false,
      RCIDdisable: false,
      loading: false,
      checkResult: false,
      checkResult2: false,
      dialogEditFormVisible: false,
      LRemittanceArr: [],
      RRemittanceArr: []
    };
  },
  computed: {
    ...mapGetters(['name']),
    ...mapGetters(['bpropotiesOptions']),
    LNIDLCID() {
      if (this.searchPDF.LNID) {
        return this.searchPDF.LNID;
      } else if (this.searchPDF.LCID) {
        return this.searchPDF.LCID;
      } else {
        return '';
      }
    }
  },
  watch: {
    formid: {
      handler() {
        Object.keys(this.searchPDF).forEach(k => {
          if (this.reqb === 'Y') {
            if (k.indexOf('BAdd') === -1) {
              this.searchPDF[k] = '';
            }
          } else {
            this.searchPDF[k] = '';
          }
        });
        // this.searchPDF.LNID = '';
        // this.searchPDF.LCID = '';
        // this.searchPDF.RNID = '';
        // this.searchPDF.RCID = '';

        this.searchPDF.RemittanceCheck = false;
        this.searchPDF.ReplaceROFieldCheck = false;
      }
    },
    'searchPDF.LNID': {
      handler() {
        this.searchPDF.LRemittanceId = '';
        this.LRemittanceArr = [];
        if (this.searchPDF.LNID.length > 0) {
          if (this.searchPDF.LNID.length === 10) {
            if (this.formid === 'A000001') {
              CheckCustomerLNExists(this.searchPDF.LNID).then(res => {
                if (res.ResData !== 'deny') {
                  this.checkResult = !res.ResData;
                  if (this.checkResult) {
                    this.checkResultText = '查無此出租人';
                  } else {
                    GetRemittancesByIDNo(this.searchPDF.LNID).then(res => {
                      this.LRemittanceArr = res.ResData;
                    });
                  }
                } else {
                  this.checkResult = true;
                  this.checkResultText = '你沒有操作此出租人的權限';
                }
              });
            }
          }
          this.LCIDdisable = true;
        } else {
          this.LCIDdisable = false;
        }
      }
    },
    'searchPDF.LCID': {
      handler() {
        this.searchPDF.LRemittanceId = '';
        this.LRemittanceArr = [];
        if (this.searchPDF.LCID.length > 0) {
          if (this.searchPDF.LCID.length === 8) {
            if (this.formid === 'A000003') {
              CheckCustomerLNExists(this.searchPDF.LCID).then(res => {
                if (res.ResData !== 'deny') {
                  this.checkResult = !res.ResData;
                  if (this.checkResult) {
                    this.checkResultText = '查無此出租人';
                  } else {
                    GetRemittancesByIDNo(this.searchPDF.LCID).then(res => {
                      this.LRemittanceArr = res.ResData;
                    });
                  }
                } else {
                  this.checkResult = true;
                  this.checkResultText = '你沒有操作此出租人的權限';
                }
              });
            }
          }
          this.LNIDdisable = true;
        } else {
          this.LNIDdisable = false;
        }
      }
    },
    'searchPDF.RNID': {
      handler() {
        this.searchPDF.RRemittanceId = '';
        this.RRemittanceArr = [];
        if (this.searchPDF.RNID.length > 0) {
          if (this.searchPDF.RNID.length === 10) {
            if (this.formid === 'A000002') {
              CheckCustomerRNExists(this.searchPDF.RNID).then(res => {
                if (res.ResData !== 'deny') {
                  this.checkResult2 = !res.ResData;
                  if (this.checkResult2) {
                    this.checkResult2Text = '查無此承租人';
                  } else {
                    GetRemittancesByIDNoR(this.searchPDF.RNID).then(res => {
                      this.RRemittanceArr = res.ResData;
                    });
                  }
                } else {
                  this.checkResult2 = true;
                  this.checkResult2Text = '你沒有操作此承租人的權限';
                }
              });
            }
          }
          this.RCIDdisable = true;
        } else {
          this.RCIDdisable = false;
        }
      }
    },
    'searchPDF.RCID': {
      handler() {
        if (this.searchPDF.RCID.length > 0) {
          this.RNIDdisable = true;
        } else {
          this.RNIDdisable = false;
        }
      }
    }
  },
  created() {
    GetAllCompanyName().then(res => {
      this.allCompanyName = res.ResData;
    });
  },
  methods: {
    getsearchaddress(addressData) {
      this.searchPDF.BAdd_1 = addressData.Add_1;
      this.searchPDF.BAdd_1_1 = addressData.Add_1_1;
      this.searchPDF.BAdd_1_2 = addressData.Add_1_2;
      this.searchPDF.BAdd_2 = addressData.Add_2;
      this.searchPDF.BAdd_2_1 = addressData.Add_2_1;
      this.searchPDF.BAdd_2_2 = addressData.Add_2_2;
      this.searchPDF.BAdd_2_3 = addressData.Add_2_3;
      this.searchPDF.BAdd_2_4 = addressData.Add_2_4;
      this.searchPDF.BAdd_3 = addressData.Add_3;
      this.searchPDF.BAdd_3_1 = addressData.Add_3_1;
      this.searchPDF.BAdd_3_2 = addressData.Add_3_2;
      this.searchPDF.BAdd_4 = addressData.Add_4;
      this.searchPDF.BAdd_5 = addressData.Add_5;
      this.searchPDF.BAdd_6 = addressData.Add_6;
      this.searchPDF.BAdd_7 = addressData.Add_7;
      this.searchPDF.BAdd_8 = addressData.Add_8;
      this.searchPDF.BAdd_9 = addressData.Add_9;
    },
    RONameChange() {
      this.ResetRepLRNo();
      this.searchPDF.ROAdd = '';
      this.searchPDF.ROTel = '';
      GetAddressByCompanyName(this.searchPDF.ROName).then(res => {
        this.address = res.ResData;
      });
    },
    ROAddChange() {
      this.ResetRepLRNo();
      this.searchPDF.ROTel = '';
      GetTelByCompanyNameAndAddress(
        this.searchPDF.ROName,
        this.searchPDF.ROAdd
      ).then(res => {
        this.Tel = res.ResData;
      });
    },
    ROTelChange() {
      // const data = {
      //   companyName: this.searchPDF.ROName,
      //   address: this.searchPDF.ROAdd,
      //   Tel: this.searchPDF.ROTel
      // }
      // GetDataByCAT(data).then(res => {
      //   console.log('122')
      //   console.log(res.ResData)
      // })
      this.ResetRepLRNo();
      GetDataByCAT(
        this.searchPDF.ROName,
        this.searchPDF.ROAdd,
        this.searchPDF.ROTel
      ).then(res => {
        this.searchPDF.ROFax = res.ResData.Fax;
        this.searchPDF.RORep = res.ResData.Rep;
        this.searchPDF.ROLRNo = res.ResData.LRNo;
        this.searchPDF.ROID = res.ResData.SLMAID;
        this.searchPDF.RHMName = res.ResData.SIName;
        this.searchPDF.RHMRNo = res.ResData.SILRNo;
        this.searchPDF.AGName = res.ResData.AGName;
        this.searchPDF.AGLRNo = res.ResData.AGLRNo;
      });
    },
    ResetRepLRNo() {
      this.searchPDF.RORep = '';
      this.searchPDF.ROLRNo = '';
      this.searchPDF.ROID = '';
      this.searchPDF.RHMName = '';
      this.searchPDF.RHMRNo = '';
      this.searchPDF.AGName = '';
      this.searchPDF.AGLRNo = '';
    },
    InserCustomerL() {
      this.$prompt('請輸入出租人名稱', '提示', {
        confirmButtonText: '確定',
        cancelButtonText: '取消'
      }).then(val => {
        const data = {};
        if (this.searchPDF.LNID) {
          data.LNName = val.value;
          data.LNID = this.searchPDF.LNID;
          this.loading = true;
          saveCustomerLN(data, 'CustomerLN/Insert')
            .then(res => {
              if (res.Success) {
                this.$message.success('新增成功');
                this.checkResult = false;
              } else {
                this.$message.error('失敗!');
              }
            })
            .finally(() => {
              this.loading = false;
            });
        } else if (this.searchPDF.LCID) {
          data.LCName = val.value;
          data.LCID = this.searchPDF.LCID;
          this.loading = true;
          saveCustomerLC(data, 'CustomerLC/Insert')
            .then(res => {
              if (res.Success) {
                this.$message.success('新增成功');
                this.checkResult = false;
              } else {
                this.$message.error('失敗!');
              }
            })
            .finally(() => {
              this.loading = false;
            });
        }
      });
    },
    InserCustomerR() {
      this.$prompt('請輸入承租人姓名', '提示', {
        confirmButtonText: '確定',
        cancelButtonText: '取消'
      }).then(val => {
        const data = {};
        if (this.searchPDF.RNID) {
          data.RNName = val.value;
          data.RNID = this.searchPDF.RNID;
          this.loading = true;
          saveCustomerRN(data, 'CustomerRN/Insert')
            .then(res => {
              if (res.Success) {
                this.$message.success('新增成功');
                this.checkResult2 = false;
              } else {
                this.$message.error('失敗!');
              }
            })
            .finally(() => {
              this.loading = false;
            });
        } else if (this.searchPDF.RCID) {
          data.RCName = val.value;
          data.RCID = this.searchPDF.RCID;
        }
      });
    },
    Jump: function() {
      this.searchPDF.FormId = this.formid;
      if (this.formid === 'A000001') {
        const addarray = [
          this.searchPDF.BAdd_1,
          this.searchPDF.BAdd_1_1,
          this.searchPDF.BAdd_1_2,
          this.searchPDF.BAdd_2,
          this.searchPDF.BAdd_2_1,
          this.searchPDF.BAdd_2_2,
          this.searchPDF.BAdd_2_3,
          this.searchPDF.BAdd_2_4,
          this.searchPDF.BAdd_3,
          this.searchPDF.BAdd_3_1,
          this.searchPDF.BAdd_3_2,
          this.searchPDF.BAdd_4,
          this.searchPDF.BAdd_5,
          this.searchPDF.BAdd_6,
          this.searchPDF.BAdd_7,
          this.searchPDF.BAdd_8,
          this.searchPDF.BAdd_9
        ];
        var badd = mergerAddress(addarray);
        if (badd) {
          CheckCustomerBbuildingExists(badd).then(res => {
            if (!res.ResData) {
              this.$confirm('查無此建物地址，是否新增此建物?', '提示', {
                confirmButtonText: '確定',
                cancelButtonText: '取消',
                type: 'warning'
              }).then(() => {
                this.dialogEditFormVisible = true;
              });
            } else {
              this.PushRoute();
            }
          });
        } else {
          this.$message.error('請輸入建物地址');
        }
      } else {
        this.PushRoute();
      }
    },
    CreateBindBuilding() {
      if (!this.searchPDF.LNID && !this.searchPDF.LCID) {
        this.$message.error('請先指定出租人身分證字號/統一編號');
        return false;
      }
      const data = {
        BAdd_1: this.searchPDF.BAdd_1,
        BAdd_1_1: this.searchPDF.BAdd_1_1,
        BAdd_1_2: this.searchPDF.BAdd_1_2,
        BAdd_2: this.searchPDF.BAdd_2,
        BAdd_2_1: this.searchPDF.BAdd_2_1,
        BAdd_2_2: this.searchPDF.BAdd_2_2,
        BAdd_2_3: this.searchPDF.BAdd_2_3,
        BAdd_2_4: this.searchPDF.BAdd_2_4,
        BAdd_3: this.searchPDF.BAdd_3,
        BAdd_3_1: this.searchPDF.BAdd_3_1,
        BAdd_3_2: this.searchPDF.BAdd_3_2,
        BAdd_4: this.searchPDF.BAdd_4,
        BAdd_5: this.searchPDF.BAdd_5,
        BAdd_6: this.searchPDF.BAdd_6,
        BAdd_7: this.searchPDF.BAdd_7,
        BAdd_8: this.searchPDF.BAdd_8,
        BAdd_9: this.searchPDF.BAdd_9
      };
      data.BPropoty = this.select;
      var belongId = this.searchPDF.LNID
        ? this.searchPDF.LNID
        : this.searchPDF.LCID;
      InsertFromWebForm(data, belongId, 'Building/InsertFromWebForm').then(
        res => {
          if (res.Success) {
            this.$message.success('新增成功');
            this.dialogEditFormVisible = false;
            this.PushRoute();
          } else {
            this.$message.error('失敗');
          }
        }
      );
    },
    PushRoute() {
      this.loading = true;
      this.searchPDF.ROUserName = this.name;
      var disabled =
        this.searchPDF.LRemittanceId !== 'new' &&
        this.searchPDF.LRemittanceId !== '';
      DownloadWithData(this.searchPDF)
        .then(res => {
          if (res.Success) {
            var formdata = res.ResData;
            var p1 = {
              FormId: this.formid,
              FormName: this.formname,
              disabled: disabled,
              IsFiled: false
            };
            if (!res.ResData.IsFiled) {
              console.log(res.ResData.IsFiled);
              p1.IsFiled = false;
            } else {
              p1.IsFiled = true;
              // var fileName = res.ResData.id + '.pdf';
              // this.$confirm(
              //   '此表單簽名檔已歸檔無法再開啟<br/>要直接下載歷史存檔(.pdf)嗎?',
              //   '提示',
              //   {
              //     cancelButtonText: '取消',
              //     confirmButtonText: '確定',
              //     type: 'error',
              //     dangerouslyUseHTMLString: true
              //   }
              // )
              //   .then(() => {
              //     downloadWebFormHistoryFormById(this.searchPDF).then(res => {
              //       console.log(res);
              //       const blob = res;
              //       const link = document.createElement('a');
              //       link.href = URL.createObjectURL(blob);
              //       link.download = fileName;
              //       link.click();
              //       URL.revokeObjectURL(link.href);
              //     });
              //   })
              //   .catch(() => {});
            }
            console.log(formdata, p1);
            var p = Object.assign(p1, formdata);
            console.log(p);
            this.loading = false;
            this.$emit('closedownloadpdfdialog');
            this.$router.push({
              // path: '/chaochi/internalFormsDetail/index',
              name: 'InternalformsDetail',
              params: p
            });
          }
        })
        .finally(() => {
          this.loading = false;
        });
    }
  }
};
</script>

<style>
.formlabeltip {
  color: red !important;
}
</style>
