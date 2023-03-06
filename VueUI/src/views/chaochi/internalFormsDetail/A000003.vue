<template>
  <div>
    <el-form
      ref="editForm"
      :inline="true"
      :model="editForm"
      class="demo-form-inline"
      :rules="rule"
    >
      <el-card>
        <el-row>
          <el-col :span="8">
            <el-form-item
              label="客戶編號："
              :label-width="formLabelWidth"
            >
              <el-input
                v-model="editForm.BZ001"
                placeholder="請輸入客戶編號"
                autocomplete="off"
                clearable
              />
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item
              label="公司名稱(中文)："
              :label-width="formLabelWidth"
              prop="LCName"
            >
              <el-input
                v-model="editForm.LCName"
                placeholder="公司名稱(中文)"
                autocomplete="off"
                clearable
              />
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item
              label="公司名稱(英文)："
              :label-width="formLabelWidth"
            >
              <el-input
                v-model="editForm.BZ002"
                placeholder="公司名稱(英文)"
                autocomplete="off"
                clearable
                onkeyup="this.value=this.value.replace(/[\u4E00-\u9FA5]/g,'')"
              />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col>
            <el-form-item
              label="公司組織型態："
              :label-width="formLabelWidth"
            >
              <el-checkbox
                v-model="editForm.BZ003"
                true-label="/Yes"
                false-label="/Off"
              >上市公司，股票代號
                <el-input
                  v-model="editForm.BZ023"
                  autocomplete="off"
                  clearable
                  size="mini"
                  class="w80px"
                />
              </el-checkbox>
              &nbsp;&nbsp;&nbsp;&nbsp;
              <el-checkbox
                v-model="editForm.BZ004"
                true-label="/Yes"
                false-label="/Off"
              >上櫃公司，股票代號
                <el-input
                  v-model="editForm.BZ024"
                  autocomplete="off"
                  clearable
                  size="mini"
                  class="w80px"
                />
              </el-checkbox>
              &nbsp;&nbsp;&nbsp;&nbsp;
              <el-checkbox
                v-model="editForm.BZ005"
                true-label="/Yes"
                false-label="/Off"
              >(公司是否有集團或相關企業)
              </el-checkbox>
              &nbsp;&nbsp;&nbsp;&nbsp;
              <el-checkbox
                v-model="editForm.BZ006"
                true-label="/Yes"
                false-label="/Off"
              >其他
              </el-checkbox>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="24">
            <el-form-item
              label="戶籍地址："
              :label-width="formLabelWidth"
            >
              <Address
                :sendedform="sendedform"
                title="公司地址"
                @geteditaddress="geteditaddress"
              />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="24">
            <el-form-item
              label=" "
              :label-width="formLabelWidth"
            >
              <el-checkbox v-model="LCAddSameCom">發票地址同公司地址
              </el-checkbox>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="24">
            <el-form-item
              v-show="LCAddSameCom===false"
              label="發票地址："
              :label-width="formLabelWidth"
            >
              <Address
                :sendedform="sendedform1"
                :resetall="LCAddSameCom"
                title="發票地址"
                @geteditaddress="geteditaddress"
              />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="6">
            <el-form-item
              label="統一編號："
              :label-width="formLabelWidth"
              prop="LCID"
            >
              <el-input
                v-model="editForm.LCID"
                autocomplete="off"
                clearable
                disabled
              />
            </el-form-item>
          </el-col>
          <el-col :span="6">
            <el-form-item
              label="負責人："
              :label-width="formLabelWidth"
              prop="LCRep"
            >
              <el-input
                v-model="editForm.LCRep"
                placeholder="請輸入負責人"
                autocomplete="off"
                clearable
              />
            </el-form-item>
          </el-col>
          <el-col :span="6">
            <el-form-item
              label="電話："
              :label-width="formLabelWidth"
              prop="LCTel_2"
            >
              <el-input
                v-model="editForm.LCTel_2"
                placeholder="請輸入電話"
                clearable
                style="width: 200px"
                class="input-with-select"
              >
                <el-select
                  slot="prepend"
                  v-model="editForm.LCTel_1"
                  placeholder="區號"
                  style="width: 80px"
                >
                  <el-option
                    v-for="item in zoneOptions"
                    :key="item.value"
                    :label="item.label"
                    :value="item.value"
                  />
                </el-select>
              </el-input>
            </el-form-item>
          </el-col>
          <el-col :span="6">
            <el-form-item
              label="傳真："
              :label-width="formLabelWidth"
            >
              <el-input
                v-model="editForm.BZ007"
                placeholder="請輸入傳真"
                autocomplete="off"
                clearable
              />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="6">
            <el-form-item
              label="成立日期："
              :label-width="formLabelWidth"
            >
              <DatePickerE
                v-model="editForm.BZ008"
                value-format="yyyy-MM-dd"
                format="yyyy-MM-dd"
                type="date"
                placeholder="選擇日期"
                style="width: 200px"
              />
            </el-form-item>
          </el-col>
          <el-col :span="6">
            <el-form-item
              label="行業別："
              :label-width="formLabelWidth"
            >
              <el-input
                v-model="editForm.BZ009"
                placeholder="請輸入行業別"
                autocomplete="off"
                clearable
              />
            </el-form-item>
          </el-col>
          <el-col :span="6">
            <el-form-item
              label="實收資本額："
              :label-width="formLabelWidth"
            >
              <el-input
                v-model="editForm.BZ010"
                placeholder="請輸入實收資本額"
                autocomplete="off"
                clearable
              />
            </el-form-item>
          </el-col>
          <el-col :span="6">
            <el-form-item
              label="員工人數："
              :label-width="formLabelWidth"
            >
              <el-input
                v-model="editForm.BZ011"
                type="number"
                autocomplete="off"
                :min="0"
                class="w100px"
                onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
              />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="6">
            <el-form-item
              label="聯絡人："
              :label-width="formLabelWidth"
            >
              <el-input
                v-model="editForm.LCCP"
                placeholder="請輸入聯絡人"
                autocomplete="off"
                clearable
              />
            </el-form-item>
          </el-col>
          <el-col :span="6">
            <el-form-item
              label="聯絡人電話："
              :label-width="formLabelWidth"
            >
              <el-input
                v-model="editForm.LCCPCell"
                placeholder="請輸入聯絡人電話"
                autocomplete="off"
                clearable
              />
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item
              label="聯絡人信箱："
              :label-width="formLabelWidth"
              prop="LCCPEMail"
            >
              <el-input
                v-model="editForm.LCCPEMail"
                placeholder="請輸入聯絡人信箱"
                autocomplete="off"
                clearable
                class="w300px"
              />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="6">
            <el-form-item
              label="會計部聯絡人："
              :label-width="formLabelWidth"
            >
              <el-input
                v-model="editForm.BZ012"
                placeholder="請輸入聯絡人"
                autocomplete="off"
                clearable
              />
            </el-form-item>
          </el-col>
          <el-col :span="6">
            <el-form-item
              label="聯絡人電話："
              :label-width="formLabelWidth"
            >
              <el-input
                v-model="editForm.BZ013"
                placeholder="請輸入會計部聯絡人電話"
                autocomplete="off"
                clearable
              />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col>
            <el-form-item
              label="付款方式："
              :label-width="formLabelWidth"
            >
              <el-checkbox
                v-model="editForm.BZ014"
                true-label="/Yes"
                false-label="/Off"
              >月結60天
              </el-checkbox>
              <el-checkbox
                v-model="editForm.BZ015"
                true-label="/Yes"
                false-label="/Off"
              >現金
              </el-checkbox>
              <el-checkbox
                v-model="editForm.BZ016"
                true-label="/Yes"
                false-label="/Off"
              >信用卡
              </el-checkbox>
              <el-checkbox
                v-model="editForm.BZ017"
                true-label="/Yes"
                false-label="/Off"
              >匯款
              </el-checkbox>
              <el-checkbox
                v-model="editForm.BZ018"
                true-label="/Yes"
                false-label="/Off"
              >支票
              </el-checkbox>
              <el-checkbox
                v-model="editForm.BZ019"
                true-label="/Yes"
                false-label="/Off"
              >其他
                <el-input
                  v-model="editForm.BZ020"
                  autocomplete="off"
                  clearable
                  size="mini"
                  class="w80px"
                />
              </el-checkbox>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="6">
            <el-form-item
              label="結帳日期："
              :label-width="formLabelWidth"
            >
              <DatePickerE
                v-model="editForm.BZ021"
                value-format="yyyy-MM-dd"
                format="yyyy-MM-dd"
                type="date"
                placeholder="選擇日期"
                style="width: 200px"
              />
            </el-form-item>
          </el-col>
          <el-col :span="6">
            <el-form-item
              label="請款日期："
              :label-width="formLabelWidth"
            >
              <DatePickerE
                v-model="editForm.BZ022"
                value-format="yyyy-MM-dd"
                format="yyyy-MM-dd"
                type="date"
                placeholder="選擇日期"
                style="width: 200px"
              />
            </el-form-item>
          </el-col>
        </el-row>
      </el-card>

      <el-row :gutter="20">
        <el-col :span="12">
          <h3>匯款資料</h3>
          <el-card>
            <RemittanceWebForm
              :sendedform="sendRemittanceform"
              :fielddisabled="editForm.disabled"
              @getremittancebyemit="GetRemittanceByEmit"
            />
          </el-card>
        </el-col>
        <el-col :span="12">
          <h3>立書同意書人</h3>
          <div class="text item">
            <span>
              <el-image
                :src="src_1"
                class="tmb"
              >
                <div slot="error">
                  <div class="tmb"><i class="el-icon-picture-outline" /></div>
                </div>
              </el-image>
              <el-button
                type="success"
                icon="el-icon-edit"
                size="mini"
                round
                @click="centerDialogVisible = true"
              >簽名</el-button>
            </span>
          </div>
        </el-col>
      </el-row>
    </el-form>
    <div style="margin-top: 20px; text-align: center">
      <el-button
        type="primary"
        size="small"
        icon="el-icon-paperclip"
        @click="upload()"
      >儲存</el-button>
      <el-button
        size="small"
        icon="el-icon-close"
        @click="cancel"
      >關閉</el-button>
      <el-button
        v-if="src_1"
        type="success"
        size="small"
        icon="el-icon-download"
        @click="SaveAndUploadAndDownoadPDF()"
      >下載並匯出PDF</el-button>
    </div>
    <el-dialog
      title="簽名"
      :visible.sync="centerDialogVisible"
      width="850px"
      :close-on-click-modal="false"
    >
      <div
        id="signDiv"
        style="border: 1px solid black"
      >
        <VueSignaturePad
          ref="signaturePad"
          width="800px"
          height="500px"
        />
      </div>
      <span
        slot="footer"
        class="dialog-footer"
      >
        <el-button
          type="primary"
          @click="save('1')"
        >儲存</el-button>
        <el-button @click="clear">清除</el-button>
        <el-button
          type="info"
          @click="centerDialogVisible = false"
        >關閉</el-button>
      </span>
    </el-dialog>
  </div>
</template>

<script>
import { getImg } from '@/api/chaochi/externalform/externalformservice';
import {
  UploadWithData,
  ImgUpload
} from '@/api/chaochi/internalform/internalform';
import { validateEamilReg, validateBusinessIDReg } from '@/utils/validate';
import Address from '@/components/Address/Address.vue';
import DatePickerE from '@/components/RocDatepickerE';
import RemittanceWebForm from '@/components/Remittance/RemittanceForWebForm.vue';
import JsPDF from 'jspdf';
import html2canvas from 'html2canvas';
export default {
  name: 'A000003',
  components: { Address, DatePickerE, RemittanceWebForm },
  props: {
    editform: { type: Object, default: null }
  },
  data() {
    return {
      editForm: { ...this.editform },
      sendedform: {},
      sendedform1: {},
      sendRemittanceform: {},
      rule: {
        LCID: [
          { required: true, trigger: 'blur', validator: validateBusinessIDReg }
        ],
        LCCPEMail: [
          { required: false, trigger: 'change', validator: validateEamilReg }
        ],
        LCRep: [{ required: true, message: '請輸入代表人', trigger: 'blur' }],
        LCName: [{ required: true, message: '請輸入名稱', trigger: 'blur' }],
        LCTel_2: [{ required: true, message: '請輸入電話', trigger: 'blur' }]
      },
      zoneOptions: [
        {
          label: '02',
          value: '02'
        },
        {
          label: '03',
          value: '03'
        },
        {
          label: '037',
          value: '037'
        },
        {
          label: '04',
          value: '04'
        },
        {
          label: '049',
          value: '049'
        },
        {
          label: '05',
          value: '05'
        },
        {
          label: '06',
          value: '06'
        },
        {
          label: '07',
          value: '07'
        },
        {
          label: '08',
          value: '08'
        },
        {
          label: '089',
          value: '089'
        },
        {
          label: '082',
          value: '082'
        },
        {
          label: '0826',
          value: '0826'
        },
        {
          label: '0836',
          value: '0836'
        }
      ],
      formLabelWidth: '130px',
      src_1: '',
      centerDialogVisible: false,
      LCAddSameCom: false
    };
  },
  watch: {
    editform: {
      immediate: true,
      handler() {
        console.log('editformch');
        if (Object.keys(this.editform).length !== 0) {
          this.editForm = this.editform;
          this.$emit('openpageloading');
          this.sendedform = {
            Add_1: this.editForm.LCAdd_1,
            Add_1_1: this.editForm.LCAdd_1_1,
            Add_1_2: this.editForm.LCAdd_1_2,
            Add_2: this.editForm.LCAdd_2,
            Add_2_1: this.editForm.LCAdd_2_1,
            Add_2_2: this.editForm.LCAdd_2_2,
            Add_2_3: this.editForm.LCAdd_2_3,
            Add_2_4: this.editForm.LCAdd_2_4,
            Add_3: this.editForm.LCAdd_3,
            Add_3_1: this.editForm.LCAdd_3_1,
            Add_3_2: this.editForm.LCAdd_3_2,
            Add_4: this.editForm.LCAdd_4,
            Add_5: this.editForm.LCAdd_5,
            Add_6: this.editForm.LCAdd_6,
            Add_7: this.editForm.LCAdd_7,
            Add_8: this.editForm.LCAdd_8,
            Add_9: this.editForm.LCAdd_9
          };
          this.sendedform1 = {
            Add_1: this.editForm.LCAddC_1,
            Add_1_1: this.editForm.LCAddC_1_1,
            Add_1_2: this.editForm.LCAddC_1_2,
            Add_2: this.editForm.LCAddC_2,
            Add_2_1: this.editForm.LCAddC_2_1,
            Add_2_2: this.editForm.LCAddC_2_2,
            Add_2_3: this.editForm.LCAddC_2_3,
            Add_2_4: this.editForm.LCAddC_2_4,
            Add_3: this.editForm.LCAddC_3,
            Add_3_1: this.editForm.LCAddC_3_1,
            Add_3_2: this.editForm.LCAddC_3_2,
            Add_4: this.editForm.LCAddC_4,
            Add_5: this.editForm.LCAddC_5,
            Add_6: this.editForm.LCAddC_6,
            Add_7: this.editForm.LCAddC_7,
            Add_8: this.editForm.LCAddC_8,
            Add_9: this.editForm.LCAddC_9
          };
          this.sendRemittanceform = {
            LAName: this.editForm.LCName,
            LBankName: this.editForm.LBankName,
            LBankNo: this.editForm.LBankNo,
            LBranchName: this.editForm.LBranchName,
            LBranchNo: this.editForm.LBranchNo,
            LANo: this.editForm.LANo
          };
          this.LCAddSameCom = this.editForm.LCAddSame === '/Yes';
          this.GetSig();
        }
      }
    },
    LCAddSameCom: {
      handler(a) {
        if (a) {
          this.editForm.LCAddSame = '/Yes';
        } else {
          this.editForm.LCAddSame = '/Off';
        }
      }
    }
  },
  methods: {
    cancel() {
      this.$emit('cancel');
    },
    upload() {
      this.$refs['editForm'].validate((valid, err) => {
        if (valid) {
          this.UploadWithData();
        } else {
          var msg = '';
          var errArr = Object.keys(err);
          errArr.forEach(x => {
            switch (x) {
              case 'LCName':
                msg += '*公司名稱<br/>';
                break;
              case 'LCID':
                msg += '*統一編號<br/>';
                break;
              case 'LCRep':
                msg += '*負責人<br/>';
                break;
              case 'LCTel_2':
                msg += '*電話<br/>';
                break;
            }
          });
          this.$alert(
            `必填欄位未填：<br/><div style="color:#e24e4e"><i><b>${msg}<b/><i/><div/>`,
            '提示',
            {
              confirmButtonText: '確定',
              dangerouslyUseHTMLString: true
            }
          );
          return false;
        }
      });
    },
    UploadWithData() {
      this.$emit('openpageloading');
      this.editForm.LCanModify = !this.editForm.disabled;
      this.editForm.IsGenPDF = 'N';
      UploadWithData(this.editForm.FormId, this.editForm)
        .then(res => {
          if (res.Success) {
            this.$message.success('儲存成功!');
          } else {
            this.$message.error('失敗');
          }
        })
        .finally(() => {
          this.$emit('closepageloading');
        });
    },

    clear() {
      this.$refs.signaturePad.clearSignature();
    },
    save(params) {
      const { data } = this.$refs.signaturePad.saveSignature();
      switch (params) {
        case '1':
          this.editForm.SignatureBase64_1 = data;
          this.src_1 = data;
          break;
      }
      this.centerDialogVisible = false;
      this.$refs.signaturePad.clearSignature();
    },
    GetSig() {
      if (this.editForm.SignatureImgPath_1) {
        getImg(this.editForm.SignatureImgPath_1)
          .then(response => {
            this.src_1 = URL.createObjectURL(response);
          })
          .finally(() => {
            this.$emit('closepageloading');
          });
      } else {
        this.src_1 = null;
        this.$emit('closepageloading');
      }

      //   if (this.editForm.SignatureImgPath_2) {
      //     getImg(this.editForm.SignatureImgPath_2).then(response => {
      //       this.editForm.SignatureImgPath_2 = URL.createObjectURL(response);
      //     });
      //   }
      //   if (this.editForm.SignatureImgPath_3) {
      //     getImg(this.editForm.SignatureImgPath_3).then(response => {
      //       this.editForm.SignatureImgPath_3 = URL.createObjectURL(response);
      //     });
      //   }
      //   if (this.editForm.SignatureImgPath_4) {
      //     getImg(this.editForm.SignatureImgPath_4).then(response => {
      //       this.editForm.SignatureImgPath_4 = URL.createObjectURL(response);
      //     });
      //   }
      //   if (this.editForm.SignatureImgPath_5) {
      //     getImg(this.editForm.SignatureImgPath_5).then(response => {
      //       this.editForm.SignatureImgPath_5 = URL.createObjectURL(response);
      //     });
      //   }
    },
    geteditaddress(addressData, title) {
      if (title === '公司地址') {
        this.editForm.LCAdd_1 = addressData.Add_1;
        this.editForm.LCAdd_1_1 = addressData.Add_1_1;
        this.editForm.LCAdd_1_2 = addressData.Add_1_2;
        this.editForm.LCAdd_2 = addressData.Add_2;
        this.editForm.LCAdd_2_1 = addressData.Add_2_1;
        this.editForm.LCAdd_2_2 = addressData.Add_2_2;
        this.editForm.LCAdd_2_3 = addressData.Add_2_3;
        this.editForm.LCAdd_2_4 = addressData.Add_2_4;
        this.editForm.LCAdd_3 = addressData.Add_3;
        this.editForm.LCAdd_3_1 = addressData.Add_3_1;
        this.editForm.LCAdd_3_2 = addressData.Add_3_2;
        this.editForm.LCAdd_4 = addressData.Add_4;
        this.editForm.LCAdd_5 = addressData.Add_5;
        this.editForm.LCAdd_6 = addressData.Add_6;
        this.editForm.LCAdd_7 = addressData.Add_7;
        this.editForm.LCAdd_8 = addressData.Add_8;
        this.editForm.LCAdd_9 = addressData.Add_9;
      }
      if (title === '發票地址') {
        this.editForm.LCAddC_1 = addressData.Add_1;
        this.editForm.LCAddC_1_1 = addressData.Add_1_1;
        this.editForm.LCAddC_1_2 = addressData.Add_1_2;
        this.editForm.LCAddC_2 = addressData.Add_2;
        this.editForm.LCAddC_2_1 = addressData.Add_2_1;
        this.editForm.LCAddC_2_2 = addressData.Add_2_2;
        this.editForm.LCAddC_2_3 = addressData.Add_2_3;
        this.editForm.LCAddC_2_4 = addressData.Add_2_4;
        this.editForm.LCAddC_3 = addressData.Add_3;
        this.editForm.LCAddC_3_1 = addressData.Add_3_1;
        this.editForm.LCAddC_3_2 = addressData.Add_3_2;
        this.editForm.LCAddC_4 = addressData.Add_4;
        this.editForm.LCAddC_5 = addressData.Add_5;
        this.editForm.LCAddC_6 = addressData.Add_6;
        this.editForm.LCAddC_7 = addressData.Add_7;
        this.editForm.LCAddC_8 = addressData.Add_8;
        this.editForm.LCAddC_9 = addressData.Add_9;
      }
    },
    GetRemittanceByEmit(remittance) {
      this.editForm.LAName = remittance.LAName;
      this.editForm.LANo = remittance.LANo;
      this.editForm.LBankName = remittance.LBankName;
      this.editForm.LBankNo = remittance.LBankNo;
      this.editForm.LBranchName = remittance.LBranchName;
      this.editForm.LBranchNo = remittance.LBranchNo;
    },
    SaveAndUploadAndDownoadPDF() {
      this.$refs['editForm'].validate((valid, err) => {
        if (valid) {
          this.$emit('openpageloading');
          this.editForm.LCanModify = !this.editForm.disabled;
          this.editForm.IsGenPDF = 'Y';
          UploadWithData(this.editForm.FormId, this.editForm).then(res => {
            if (res.Success) {
              // this.$message.success('儲存成功!');
              var w = this.$refs.editForm.$el.clientWidth;
              var h = this.$refs.editForm.$el.clientHeight;
              var fileName = res.ResData;
              const doc = new JsPDF('l', 'pt', [h, w]);
              var test = this.$refs.editForm.$el;
              test.forEach(el => {
                el.removeAttribute('placeholder'); // 把placeholder拿掉
              });
              html2canvas(this.$refs.editForm.$el, { scale: 1.25 }).then(
                canvas => {
                  const img = canvas.toDataURL('image/jpeg');
                  doc.addImage(img, 'JPEG', 10, 10);
                  const blob = doc.output('blob');
                  var file = new File([blob], '法人房東資料卡.pdf', {
                    type: 'pdf'
                  });
                  const formData = new FormData();
                  formData.append('File', file);
                  formData.append('id', fileName);
                  formData.append('role', 'LC');
                  formData.append('IDNo', this.editForm.LCID);
                  formData.append('formname', this.editForm.FormName);
                  ImgUpload(formData)
                    .then(res => {
                      if (res.Success) {
                        this.$message.success('上傳並儲存成功');
                        const link = document.createElement('a');
                        link.href = URL.createObjectURL(blob);
                        link.download = '法人房東資料卡.pdf';
                        link.click();
                        URL.revokeObjectURL(link.href);
                        this.cancel();
                      } else {
                        this.$message.error('上傳失敗');
                      }
                    })
                    .finally(() => {
                      this.$emit('closepageloading');
                    });
                }
              );
            } else {
              this.$message.error('失敗');
            }
          });
        } else {
          var msg = '';
          var errArr = Object.keys(err);
          errArr.forEach(x => {
            switch (x) {
              case 'LCName':
                msg += '*公司名稱<br/>';
                break;
              case 'LCID':
                msg += '*統一編號<br/>';
                break;
              case 'LCRep':
                msg += '*負責人<br/>';
                break;
              case 'LCTel_2':
                msg += '*電話<br/>';
                break;
            }
          });
          this.$alert(
            `必填欄位未填：<br/><div style="color:#e24e4e"><i><b>${msg}<b/><i/><div/>`,
            '提示',
            {
              confirmButtonText: '確定',
              dangerouslyUseHTMLString: true
            }
          );
          return false;
        }
      });
    }
  }
};
</script>

<style>
</style>
