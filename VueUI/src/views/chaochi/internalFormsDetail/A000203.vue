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
          <el-col>
            <el-form-item label="委託人：">
              <el-input
                v-model="editForm.LSName"
                placeholder="請輸入姓名"
                autocomplete="off"
                disabled
              />
            </el-form-item>
            <el-form-item label="(以下簡稱甲方)" />
          </el-col>
          <el-col>
            <el-form-item label="受託人：">
              <el-input
                v-model="editForm.ROName"
                autocomplete="off"
                disabled
                style="width: 320px"
              />
            </el-form-item>
            <el-form-item label="(以下簡稱乙方)" />
          </el-col>
        </el-row>
        <div class="text item">
          <span>雙方秉持誠信原則同意於民國
            <el-input
              v-model="editForm.BZ001"
              autocomplete="off"
              clearable
              class="w70px"
              size="mini"
              onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
            />年
            <el-input
              v-model="editForm.BZ002"
              autocomplete="off"
              clearable
              class="w60px"
              size="mini"
              onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
            />月
            <el-input
              v-model="editForm.BZ003"
              autocomplete="off"
              clearable
              class="w60px"
              size="mini"
              onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
            />日
          </span> 簽定本契約：
        </div>
        <div class="text item">
          <span>1. 乙方授權甲方得於民國
            <el-input
              v-model="editForm.BZ004"
              autocomplete="off"
              clearable
              class="w70px"
              size="mini"
              onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
            />年
            <el-input
              v-model="editForm.BZ005"
              autocomplete="off"
              clearable
              class="w60px"
              size="mini"
              onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
            />月
            <el-input
              v-model="editForm.BZ006"
              autocomplete="off"
              clearable
              class="w60px"
              size="mini"
              onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
            />日至民國
            <el-input
              v-model="editForm.BZ007"
              autocomplete="off"
              clearable
              class="w70px"
              size="mini"
              onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
            />年
            <el-input
              v-model="editForm.BZ008"
              autocomplete="off"
              clearable
              class="w60px"
              size="mini"
              onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
            />月
            <el-input
              v-model="editForm.BZ009"
              autocomplete="off"
              clearable
              class="w60px"
              size="mini"
              onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
            />日
          </span>止為下列房屋為居間仲介行為
        </div>
        <div class="text item">
          <span>
            &nbsp;&nbsp;&nbsp;（包含收取返還定金、拍照、刊登廣告等；房屋資訊與委託出租條件內容以謄本及下列現況為主）。
          </span>
        </div>
        <div class="text item pad">
          <span>&nbsp;&nbsp;<b>房屋地址如下：{{ editForm.BAdd }}</b>
          </span>
        </div>
        <div class="text item">
          <span>2. 以下為房屋資訊與委託出租條件
          </span>
        </div>
        <div class="text item">
          <el-row>
            <el-col :span="8">
              &nbsp;&nbsp;&nbsp;&nbsp;使用（權狀）面積：
              <el-input
                v-model="editForm.BRealPING"
                autocomplete="off"
                clearable
                class="w80px"
                size="mini"
              />坪
            </el-col>
            <el-col :span="6">
              面寬：
              <el-input
                v-model="editForm.BZ010"
                autocomplete="off"
                clearable
                class="w80px"
                size="mini"
              />米
            </el-col>
          </el-row>
        </div>
        <div class="text item">
          <el-row>
            <el-col :span="8">
              &nbsp;&nbsp;&nbsp;&nbsp;月租金：新台幣
              <el-input
                v-model="editForm.BTCRent"
                autocomplete="off"
                clearable
                class="w100px"
                size="mini"
              />元（ <el-radio
                v-model="BZ011012"
                label="1"
              >未稅</el-radio>
              <el-radio
                v-model="BZ011012"
                label="2"
              >含稅</el-radio>）
            </el-col>
            <el-col :span="6">
              現況：<el-radio
                v-model="BZ013014"
                label="1"
              >空屋</el-radio>
              <el-radio
                v-model="BZ013014"
                label="2"
              >原</el-radio>
              <el-input
                v-model="editForm.BZ015"
                autocomplete="off"
                clearable
                class="w100px"
                size="mini"
              />
            </el-col>
          </el-row>
        </div>
        <div class="text item">
          <el-row>
            <el-col :span="8">
              &nbsp;&nbsp;&nbsp;&nbsp;押&nbsp;&nbsp;&nbsp;金：新台幣
              <el-input
                v-model="editForm.Bdeposit"
                autocomplete="off"
                clearable
                class="w100px"
                size="mini"
              />元
            </el-col>
            <el-col :span="6">
              裝修期：
              <el-input
                v-model="editForm.BZ016"
                autocomplete="off"
                clearable
                class="w140px"
                size="mini"
              />
            </el-col>
          </el-row>
        </div>
        <div class="text item">
          <el-row>
            <el-col :span="8">
              &nbsp;&nbsp;&nbsp;&nbsp;管理費：新台幣
              <el-input
                v-model="editForm.BMFee"
                autocomplete="off"
                clearable
                class="w100px"
                size="mini"
              />元
            </el-col>
            <el-col :span="6">
              物件來源：
              <el-input
                v-model="editForm.BZ017"
                autocomplete="off"
                clearable
                class="w140px"
                size="mini"
              />
            </el-col>
          </el-row>
        </div>
        <div class="text item">
          <span>&nbsp;&nbsp;&nbsp;&nbsp;備註事項：
            <el-input
              v-model="editForm.BZ017"
              autocomplete="off"
              clearable
              class="w600px"
              size="mini"
            />
          </span>
        </div>
        <div class="text item">
          <span>3. 乙方同意甲方得以（
            <el-checkbox
              v-model="editForm.BOpen"
              true-label="/Yes"
              false-label="/Off"
            >親自開門
            </el-checkbox>
            <el-checkbox
              v-model="editForm.BZ020"
              true-label="/Yes"
              false-label="/Off"
            >鑰匙留置管理室
            </el-checkbox>
            <el-checkbox
              v-model="editForm.BZ021"
              true-label="/Yes"
              false-label="/Off"
            >給付甲方
              <el-checkbox
                v-model="editForm.BZ022"
                true-label="/Yes"
                false-label="/Off"
              >
                鑰匙
              </el-checkbox>
              <b style="color:black;margin-right:15px">/</b>
              <el-checkbox
                v-model="editForm.BZ023"
                true-label="/Yes"
                false-label="/Off"
              >
                磁扣</el-checkbox>
              <el-input
                v-model="editForm.BZ024"
                autocomplete="off"
                clearable
                class="w80px"
                size="mini"
              />支
            </el-checkbox>）之方式，安排有意承租之人進入本房屋參觀，並同意乙方得於本房屋張貼出租廣告。
          </span>
        </div>
        <div class="text item">
          <span>
            4. 本契約為一般委託租賃契約書；乙方尚未與甲方所仲介之第三人簽訂房屋租賃契約書前，<u><b>可隨時租售予他人，無需支付受託人(甲方)任何費用</b></u>。
          </span>
        </div>
        <div class="text item">
          <span>5. 乙方同意於甲方居間仲介完成，簽訂房屋租賃契約時，支付報酬如下：</span>
        </div>
        <div class="text item">
          <span>&nbsp;&nbsp;&nbsp;&nbsp;(1) 簽立租賃期間一年之房屋租賃契約，應支付相當於半個月租金額為仲介費。</span>
        </div>
        <div class="text item">
          <span>&nbsp;&nbsp;&nbsp;&nbsp;(2) 簽立租賃期間兩年以上之房屋租賃契約，應支付相當於一個月租金額為仲介費。</span>
        </div>
        <div class="text item">
          <span>6. 乙方應提示有權出租本房屋之證明文件。</span>
        </div>
        <div class="text item">
          <span>7. 委託人為代理人時，應出具授權書正本。</span>
        </div>
        <div class="text item">
          <span>8. 因本契約所生之法律糾紛，雙方同意以臺灣臺北地方法院為管轄法院。</span>
        </div>
        <el-row>
          <el-col>
            <h3>委 託 人 （ 乙 方 ）</h3>
            <el-card>
              <el-row>
                <el-col>
                  <el-form-item
                    label="姓名："
                    class="nameformlabel"
                  >
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
                  </el-form-item>
                </el-col>
                <el-col>
                  <el-form-item label="身分證字號：">
                    <el-badge
                      value="(統一編號)"
                      type="info"
                    >{{ editForm.LSID }}
                    </el-badge>
                  </el-form-item>
                </el-col>
                <el-col>
                  <el-form-item
                    label="電話："
                    prop="LNCell"
                  >
                    <el-input
                      v-model="editForm.LNCell"
                      placeholder="請輸入電話"
                      clearable
                      style="width: 220px"
                      class="input-with-select"
                      onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                      :maxlength="10"
                    />
                  </el-form-item>
                </el-col>
                <el-col>
                  <el-form-item label="地址：">
                    <Address
                      :sendedform="sendedform"
                      title="戶籍地址"
                      @geteditaddress="geteditaddress"
                    />
                  </el-form-item>
                </el-col>
              </el-row>
              <div
                class="text item"
                style="font-size: 8px;"
              >
                <span>契約審閱權，本房屋委託租賃契約書已由委託人攜回審閱至少三日，並充分瞭解本契約內容。</span>
              </div>
            </el-card>
          </el-col>
        </el-row>
        <h3>受 託 人 (甲 方 )</h3>
        <el-row :gutter="20">
          <el-col :span="8">
            <el-card>
              <el-form-item label="經紀業者：">
                {{ editForm.ROName }}
              </el-form-item>
            </el-card>
          </el-col>
          <el-col :span="10">
            <el-card>
              <el-row>
                <el-col :span="12">
                  <el-form-item label="不動產經紀人：">
                    {{ editForm.AGName }}
                  </el-form-item>
                </el-col>
                <el-col :span="12">
                  <el-form-item label="證號：">
                    {{ editForm.AGLRNo }}
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row>
                <el-col :span="12">
                  <el-form-item label="聯絡電話：">
                    {{ editForm.ROTel }}
                  </el-form-item>
                </el-col>
                <el-col :span="12">
                  <el-form-item label="承辦人：">
                    {{ editForm.ROUserName }}
                  </el-form-item>
                </el-col>
              </el-row>

            </el-card>
          </el-col>
        </el-row>
      </el-card>
    </el-form>
    <div style="margin-top: 20px; text-align: center">
      <el-button
        size="small"
        icon="el-icon-paperclip"
        type="primary"
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
      <!-- <el-button
        type="success"
        size="small"
        icon="el-icon-download"
        @click="downloadLatestA0000801PDF()"
      >下載此建物最新設備表單</el-button> -->
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
import {
  UploadWithData,
  downloadPDFWithDataByFormId,
  ImgUpload
} from '@/api/chaochi/internalform/internalform';
import { getImg } from '@/api/chaochi/externalform/externalformservice';
import Address from '@/components/Address/Address.vue';
import { validateCellReg } from '@/utils/validate';
export default {
  name: 'A000203',
  components: { Address },
  props: {
    editform: { type: Object, default: null }
  },
  data() {
    return {
      editForm: { ...this.editform },
      rule: {
        LNCell: [
          { required: false, trigger: 'change', validator: validateCellReg }
        ]
      },
      sendedform: {},
      BZ011012: '',
      BZ013014: '',
      src_1: '',
      centerDialogVisible: false
    };
  },
  watch: {
    editform: {
      immediate: true,
      handler() {
        if (Object.keys(this.editform).length !== 0) {
          this.$emit('openpageloading');
          this.editForm = this.editform;
          this.sendedform = {
            Add_1: this.editForm.LSAdd_1,
            Add_1_1: this.editForm.LSAdd_1_1,
            Add_1_2: this.editForm.LSAdd_1_2,
            Add_2: this.editForm.LSAdd_2,
            Add_2_1: this.editForm.LSAdd_2_1,
            Add_2_2: this.editForm.LSAdd_2_2,
            Add_2_3: this.editForm.LSAdd_2_3,
            Add_2_4: this.editForm.LSAdd_2_4,
            Add_3: this.editForm.LSAdd_3,
            Add_3_1: this.editForm.LSAdd_3_1,
            Add_3_2: this.editForm.LSAdd_3_2,
            Add_4: this.editForm.LSAdd_4,
            Add_5: this.editForm.LSAdd_5,
            Add_6: this.editForm.LSAdd_6,
            Add_7: this.editForm.LSAdd_7,
            Add_8: this.editForm.LSAdd_8,
            Add_9: this.editForm.LSAdd_9
          };
          this.BZ011012 =
            this.editForm.BZ011 === '/Yes'
              ? '1'
              : this.editForm.BZ012 === '/Yes'
                ? '2'
                : '';
          this.BZ013014 =
            this.editForm.BZ013 === '/Yes'
              ? '1'
              : this.editForm.BZ014 === '/Yes'
                ? '2'
                : '';
          this.GetSig();
        }
      }
    },
    BZ011012: {
      handler() {
        switch (this.BZ011012) {
          case '1':
            this.editForm.BZ011 = '/Yes';
            this.editForm.BZ012 = '/Off';
            break;
          case '2':
            this.editForm.BZ011 = '/Off';
            this.editForm.BZ012 = '/Yes';
            break;
        }
      }
    },
    BZ013014: {
      handler() {
        switch (this.BZ013014) {
          case '1':
            this.editForm.BZ013 = '/Yes';
            this.editForm.BZ014 = '/Off';
            break;
          case '2':
            this.editForm.BZ013 = '/Off';
            this.editForm.BZ014 = '/Yes';
            break;
        }
      }
    }
  },
  methods: {
    cancel() {
      this.$emit('cancel');
    },
    upload() {
      this.$refs['editForm'].validate(valid => {
        if (valid) {
          this.UploadWithData();
        } else {
          return false;
        }
      });
    },
    UploadWithData() {
      this.$emit('openpageloading');
      // this.editForm.LCanModify = !this.editForm.disabled; //A000203沒有匯款資料
      this.editForm.IsGenPDF = 'N';
      // // 時間額外處理 2022/09/13 決議丟置後端處理
      // if (this.editForm.BZ035) {
      //   this.editForm.BZ036 = this.editForm.BZ035.split('-')[1];
      //   this.editForm.BZ037 = this.editForm.BZ035.split('-')[2];
      //   this.editForm.BZ035 = this.editForm.BZ035.split('-')[0];
      // }
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
    },
    SaveAndUploadAndDownoadPDF() {
      this.$refs['editForm'].validate(valid => {
        if (valid) {
          this.$emit('openpageloading');
          this.editForm.LCanModify = !this.editForm.disabled;
          this.editForm.IsGenPDF = 'Y';
          UploadWithData(this.editForm.FormId, this.editForm).then(res => {
            if (res.Success) {
              const data = {
                FormId: this.editForm.FormId,
                BAdd: this.editForm.BAdd
              };
              if (this.editForm.LSID) {
                switch (this.editForm.LSID.length) {
                  case 10:
                    data.LNID = this.editForm.LSID;
                    break;
                  case 8:
                    data.LCID = this.editForm.LSID;
                    break;
                }
              }
              downloadPDFWithDataByFormId(data)
                .then(res => {
                  const blob = res; // new Blob([response.data], { type: 'image/jpeg' })
                  const link = document.createElement('a');
                  link.href = URL.createObjectURL(blob);
                  link.download = 'F-LAW111-15-A(店辦)委託租賃契約書.pdf';
                  link.click();
                  URL.revokeObjectURL(link.href);
                  const formData = new FormData();
                  var file = new File(
                    [blob],
                    'F-LAW111-15-A(店辦)委託租賃契約書' + '.pdf',
                    {
                      type: 'pdf'
                    }
                  );
                  formData.append('File', file);
                  // formData.append('id', fileName);
                  formData.append('role', 'Building');
                  formData.append('formname', this.editForm.FormName);
                  formData.append('BAdd', this.editForm.BAdd);
                  ImgUpload(formData)
                    .then(res => {
                      if (res.Success) {
                        this.$message.success('上傳並儲存成功');
                        this.cancel();
                      } else {
                        this.$message.error('上傳失敗');
                      }
                    })
                    .finally(() => {
                      this.$emit('closepageloading');
                    });
                })
                // .finally(() => {
                //   this.$emit('closepageloading');
                // });
            } else {
              this.$message.error('失敗');
            }
          });
          // .finally(() => {
          //   this.$emit('closepageloading');
          // });
        } else {
          return false;
        }
      });
    },
    geteditaddress(addressData, title) {
      if (title === '戶籍地址') {
        this.editForm.LSAdd_1 = addressData.Add_1;
        this.editForm.LSAdd_1_1 = addressData.Add_1_1;
        this.editForm.LSAdd_1_2 = addressData.Add_1_2;
        this.editForm.LSAdd_2 = addressData.Add_2;
        this.editForm.LSAdd_2_1 = addressData.Add_2_1;
        this.editForm.LSAdd_2_2 = addressData.Add_2_2;
        this.editForm.LSAdd_2_3 = addressData.Add_2_3;
        this.editForm.LSAdd_2_4 = addressData.Add_2_4;
        this.editForm.LSAdd_3 = addressData.Add_3;
        this.editForm.LSAdd_3_1 = addressData.Add_3_1;
        this.editForm.LSAdd_3_2 = addressData.Add_3_2;
        this.editForm.LSAdd_4 = addressData.Add_4;
        this.editForm.LSAdd_5 = addressData.Add_5;
        this.editForm.LSAdd_6 = addressData.Add_6;
        this.editForm.LSAdd_7 = addressData.Add_7;
        this.editForm.LSAdd_8 = addressData.Add_8;
        this.editForm.LSAdd_9 = addressData.Add_9;
      }
    }
  }
};
</script>

<style>
.el-radio {
  margin-right: 5px !important;
}
.el-checkbox {
  margin-right: 15px !important;
}
.el-checkbox__label {
  padding-left: 2px !important;
}
</style>
