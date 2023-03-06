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
      :rules="rule"
    >
      <el-row v-if="reql === 'Y'">
        <el-form-item
          label="出租人身分證字號/居留證號："
          prop="LNID"
        >
          <el-input
            v-model="searchPDF.LNID"
            v-upperCase
            clearable
            :disabled="LNIDdisable"
            :maxlength="10"
          />
        </el-form-item>
        <el-form-item
          label="出租人統一編號："
          prop="LCID"
        >
          <el-input
            v-model="searchPDF.LCID"
            clearable
            :disabled="LCIDdisable"
            :maxlength="8"
          />
        </el-form-item>
      </el-row>
      <el-row v-if="reqr === 'Y'">
        <el-form-item
          label="承租人身分證字號/居留證號："
          prop="RNID"
        >
          <el-input
            v-model="searchPDF.RNID"
            v-upperCase
            clearable
            :disabled="RNIDdisable"
            :maxlength="10"
          />
        </el-form-item>
        <el-form-item
          label="承租人統一編號："
          prop="RCID"
        >
          <el-input
            v-model="searchPDF.RCID"
            clearable
            :disabled="RCIDdisable"
            :maxlength="8"
          />
        </el-form-item>
      </el-row>
      <el-row v-if="reqb === 'Y'">
        <el-form-item label="建物地址：">
          <Address
            :sendedform="sendedform"
            title="建物地址"
            @getsearchaddress="getsearchaddress"
          />
        </el-form-item>
      </el-row>
      <el-row v-if="reql === 'Y' || reqr === 'Y'">
        <el-form-item>
          <el-checkbox v-model="searchPDF.RemittanceCheck">指定匯款資訊</el-checkbox>
        </el-form-item>
      </el-row>
      <div v-show="searchPDF.RemittanceCheck">
        <div v-if="reql === 'Y'">
          <el-form-item label="出租人匯款資訊：">
            <el-select
              v-model="searchPDF.LRemittanceId"
              placeholder="請選擇"
              style="width: 250px"
              clearable
            >
              <el-option
                v-for="item in LRemittanceArr"
                :key="item.Id"
                :label="item.LAName+'    '+item.LAID"
                :value="item.Id"
              >
                <span style="float: left">{{ item.LAName }}</span>
                <span style="float: right; color: #8492a6; font-size: 13px">{{ item.LBankNo }}－{{ item.LANo }}</span>
              </el-option>
            </el-select>
          </el-form-item>
        </div>
        <div v-if="reqr === 'Y'">
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
                :label="item.RAName+'    '+item.RAID"
                :value="item.Id"
              >
                <span style="float: left">{{ item.RAName }}</span>
                <span style="float: right; color: #8492a6; font-size: 13px">{{ item.RBankNo }}－{{ item.RANo }}</span>
              </el-option>
            </el-select>
          </el-form-item>
        </div>
      </div>
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
                v-model="realname"
                style="width: 150px"
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
            <el-form-item label="經紀人連絡電話：">
              <el-input
                v-model="searchPDF.AGTel"
                style="width: 150px"
                readonly
                clearable
              />
            </el-form-item>
          </el-row>
        </el-row>
      </div>
      <el-row>
        <el-button
          type="primary"
          icon="el-icon-download"
          size="small"
          @click="downloadPDFWithData(formid,searchPDF)"
        >下載PDF</el-button>

      </el-row>
    </el-form>
  </el-card>
</template>

<script>
import { downloadPDFWithDataByFormId } from '@/api/chaochi/externalform/externalformservice';
import { GetRemittancesByIDNo } from '@/api/chaochi/remittance/remittancel';
import { GetRemittancesByIDNoR } from '@/api/chaochi/remittance/remittanceR';
// import { getAllSecondLandlordList } from '@/api/chaochi/secondlandlord/secondlandlordservice'
import {
  GetAllCompanyName,
  GetAddressByCompanyName,
  GetTelByCompanyNameAndAddress,
  GetDataByCAT
  // getSLMAList
  // getSIList,
  // getSLMATel
} from '@/api/chaochi/slma/slmaservice';
import Address from '@/components/Address/Address.vue';
import { mapGetters } from 'vuex';
import { validateIDReg, validateBusinessIDReg } from '@/utils/validate';
export default {
  name: 'DownLoadPdf',
  components: { Address },
  props: {
    formid: { type: String, default: '' },
    formname: { type: String, default: '' },
    reqb: { type: String, default: 'Y' },
    reql: { type: String, default: 'Y' },
    reqr: { type: String, default: 'Y' }
  },
  data() {
    return {
      searchPDF: {
        testName: '',
        LRemittanceId: '',
        RRemittanceId: '',
        LNID: '',
        LCID: '',
        RNID: '',
        RCID: '',
        SLID: '',
        BAdd: '',
        BAdd_1: '',
        BAdd_1_1: '',
        BAdd_1_2: '',
        BAdd_2: '',
        BAdd_2_1: '',
        BAdd_2_2: '',
        BAdd_2_3: '',
        BAdd_2_4: '',
        ReplaceROFieldCheck: false,
        ROName: '',
        ROAdd: '',
        ROTel: '',
        ROFax: '',
        RORep: '',
        ROLRNo: '',
        ROID: '',
        RHMName: '',
        RHMRNo: '',
        FormId: ''
      },
      sendedform: {},
      rule: {
        RNID: [
          { required: false, trigger: 'change', validator: validateIDReg }
        ],
        RCID: [
          {
            required: false,
            trigger: 'change',
            validator: validateBusinessIDReg
          }
        ],
        LNID: [
          { required: false, trigger: 'change', validator: validateIDReg }
        ],
        LCID: [
          {
            required: false,
            trigger: 'change',
            validator: validateBusinessIDReg
          }
        ]
      },
      allCompanyName: [],
      address: [],
      Tel: [],
      slmaid: '',
      RemittanceCheck: false,
      LCIDdisable: false,
      LNIDdisable: false,
      RNIDdisable: false,
      RCIDdisable: false,
      loading: false,
      LRemittanceArr: [],
      RRemittanceArr: []
    };
  },
  computed: {
    ...mapGetters(['realname'])
  },
  watch: {
    formid: {
      handler() {
        // Object.keys(this.searchPDF).forEach(k => (this.searchPDF[k] = ''));
        if (this.reqb === 'Y') {
          this.sendedform = {
            Add_1: this.searchPDF.BAdd_1,
            Add_1_1: this.searchPDF.BAdd_1_1,
            Add_1_2: this.searchPDF.BAdd_1_2,
            Add_2: this.searchPDF.BAdd_2,
            Add_2_1: this.searchPDF.BAdd_2_1,
            Add_2_2: this.searchPDF.BAdd_2_2,
            Add_2_3: this.searchPDF.BAdd_2_3,
            Add_2_4: this.searchPDF.BAdd_2_4,
            Add_3: this.searchPDF.BAdd_3,
            Add_3_1: this.searchPDF.BAdd_3_1,
            Add_3_2: this.searchPDF.BAdd_3_2,
            Add_4: this.searchPDF.BAdd_4,
            Add_5: this.searchPDF.BAdd_5,
            Add_6: this.searchPDF.BAdd_6,
            Add_7: this.searchPDF.BAdd_7,
            Add_8: this.searchPDF.BAdd_8,
            Add_9: this.searchPDF.BAdd_9
          };
        }
      }
    },
    'searchPDF.LNID': {
      handler() {
        this.searchPDF.LRemittanceId = '';
        this.LRemittanceArr = [];
        if (this.searchPDF.LNID.length > 0) {
          if (this.searchPDF.LNID.length === 10) {
            GetRemittancesByIDNo(this.searchPDF.LNID).then(res => {
              this.LRemittanceArr = res.ResData;
            });
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
            GetRemittancesByIDNo(this.searchPDF.LCID).then(res => {
              this.LRemittanceArr = res.ResData;
            });
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
            GetRemittancesByIDNoR(this.searchPDF.RNID).then(res => {
              this.RRemittanceArr = res.ResData;
            });
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
    downloadPDFWithData: function(label, searchPDF) {
      this.$refs['searchform'].validate(valid => {
        if (valid) {
          searchPDF.FormId = label;
          if (
            searchPDF.LNID === '' &&
            searchPDF.LCID === '' &&
            searchPDF.RNID === '' &&
            searchPDF.RCID === '' &&
            searchPDF.BAdd_1 === ''
          ) {
            this.$message.error('請選擇已存在的下載條件');
            return;
          }
          searchPDF.ROUserName = this.realname;
          this.loading = true;
          downloadPDFWithDataByFormId(searchPDF)
            .then(response => {
              console.log(response.type);
              this.loading = false;
              if (response == null) {
                // this.$message({
                //   message: '下載失敗1',
                //   type: 'error'
                // })
                this.$alert('下載失敗', '下載失敗', {
                  confirmButtonText: '確定'
                });
                return;
              }
              if (response.type !== 'application/pdf') {
                var reader = new FileReader();
                reader.onload = e => {
                  const msg = JSON.parse(e.target.result);
                  this.$alert('<b>' + msg.ErrMsg + '<b/>', '下載失敗', {
                    confirmButtonText: '確定',
                    dangerouslyUseHTMLString: true
                  });
                };
                reader.readAsText(response);
                return;
              }
              const blob = response; // new Blob([response.data], { type: 'image/jpeg' })
              const link = document.createElement('a');
              link.href = URL.createObjectURL(blob);
              link.download = label;
              link.click();
              URL.revokeObjectURL(link.href);
            })
            .catch(error => {
              console.log(error);
            });

          this.tableloading = false;
          // console.log(url)
          // https://stackoverflow.com/questions/53772331/vue-html-js-how-to-download-a-file-to-browser-using-the-download-tag
          // 檢查步驟二查詢條件
          // GetInstance(label).then(res => {
          //   console.log(res, searchPDF)
          //   let msg = ''
          //   if (res.ResData.RequiredLandlord === 'Y') {
          //     if (searchPDF.LNID === '' && searchPDF.LCID === '') {
          //       msg += '請輸入房東身分證字號／居留證號／統一編號<br/>'
          //     }
          //   }
          //   if (res.ResData.RequiredBuilding === 'Y' && searchPDF.BAdd_1 === '') {
          //     msg += '請輸入建物地址<br/>'
          //   }
          //   if (res.ResData.RequiredRenter === 'Y') {
          //     if (searchPDF.RNID === '') {
          //       msg += '請輸入房客身分證字號／居留證號／統一編號<br/>'
          //     }
          //   }
          //   if (msg.length > 0) {
          //     this.$alert(msg, `提示__${label}`, {
          //       dangerouslyUseHTMLString: true
          //     })
          //   } else {
          //   }
          // })
        }
      });
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
        this.searchPDF.AGTel = res.ResData.AGTel;
      });
    },
    ResetRepLRNo() {
      this.searchPDF.ROFax = '';
      this.searchPDF.RORep = '';
      this.searchPDF.ROLRNo = '';
      this.searchPDF.ROID = '';
      this.searchPDF.RHMName = '';
      this.searchPDF.RHMRNo = '';
      this.searchPDF.AGName = '';
      this.searchPDF.AGLRNo = '';
      this.searchPDF.AGTel = '';
    }
  }
};
</script>

<style>
</style>
