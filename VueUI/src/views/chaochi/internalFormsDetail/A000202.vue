<!-- eslint-disable no-irregular-whitespace -->
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
        <div class="text item">
          <span>壹、乙方委託甲方就下列房屋為居間租賃，並同意甲方得於民國
            <el-input
              v-model="editForm.BZ001"
              autocomplete="off"
              clearable
              class="w80px"
              size="mini"
              onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
            />年
            <el-input
              v-model="editForm.BZ002"
              autocomplete="off"
              clearable
              class="w80px"
              size="mini"
              onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
            />月
            <el-input
              v-model="editForm.BZ003"
              autocomplete="off"
              clearable
              class="w80px"
              size="mini"
              onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
            />日起就該房屋為拍照、發行、刊登廣告(含公司網站、租屋網站等)等居間必要行為：
          </span>
        </div>
        <div class="text item">
          <span>　　地址：<b>{{ editForm.BAdd }}</b>。
          </span>
        </div>
        <div class="text item fcRED">
          <span>　　面積：依權狀/騰本登記，共計
            <el-input
              v-model="editForm.BRoom"
              autocomplete="off"
              clearable
              class="w80px"
              size="mini"
              onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
            />房
            <el-input
              v-model="editForm.BLD"
              autocomplete="off"
              clearable
              class="w80px"
              size="mini"
              onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
            />廳
            <el-input
              v-model="editForm.BBath"
              autocomplete="off"
              clearable
              class="w80px"
              size="mini"
              onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
            />衛
            <el-input
              v-model="editForm.BBalcony"
              autocomplete="off"
              clearable
              class="w80px"
              size="mini"
              onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
            />陽台。
          </span>
        </div>
        <div class="text item fcRED">
          <span>　　月租金：新台幣
            <el-input
              v-model="editForm.BTCRent"
              autocomplete="off"
              clearable
              class="w80px"
              size="mini"
              onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
            />元整。　租押金：新台幣
            <el-input
              v-model="editForm.Bdeposit"
              autocomplete="off"
              clearable
              class="w80px"
              size="mini"
              onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
            />元整。
          </span>
        </div>
        <div class="text item fcRED">
          <span>　　使用需求：開伙
            <el-radio
              v-model="BCook"
              label="1"
            >可</el-radio>
            <el-radio
              v-model="BCook"
              label="2"
            >不可</el-radio>；寵物
            <el-radio
              v-model="BPet"
              label="1"
            >可</el-radio>
            <el-radio
              v-model="BPet"
              label="2"
            >不可</el-radio>；神明桌
            <el-radio
              v-model="BGodT"
              label="1"
            >可</el-radio>
            <el-radio
              v-model="BGodT"
              label="2"
            >不可</el-radio>。
          </span>
        </div>
        <div class="text item">
          <span>貳、乙方同意甲方得安排有意承租之承租人以下列方式進入該房屋看屋：
            <el-checkbox
              v-model="editForm.BOpen"
              true-label="/Yes"
              false-label="/Off"
            >乙方親自開門
            </el-checkbox>
            <el-checkbox
              v-model="editForm.BZ004"
              true-label="/Yes"
              false-label="/Off"
            >鑰匙留置大樓管理室
            </el-checkbox>
            <el-checkbox
              v-model="editForm.BZ005"
              true-label="/Yes"
              false-label="/Off"
            >交付鑰匙
              <el-input
                v-model="editForm.BZ006"
                autocomplete="off"
                clearable
                class="w60px"
                size="mini"
              />支
            </el-checkbox>
            <el-checkbox
              v-model="editForm.BZ007"
              true-label="/Yes"
              false-label="/Off"
            >交付感應扣
              <el-input
                v-model="editForm.BZ008"
                autocomplete="off"
                clearable
                class="w60px"
                size="mini"
              />個
            </el-checkbox>
            。
          </span>
        </div>
        <div class="text item">
          <span>參、乙方同意甲方得代為收取承租方定金，並於簽訂租賃契約時將定金轉為租押金之一部。</span>
        </div>
        <div class="text item">
          <span>肆、乙方確實瞭解甲方於收受第壹點社會住宅包租代管計畫之主管機關所給付具本件委託服務報酬性質之補助後，不得重複向乙方收取本件委託服務報酬。</span>
        </div>
      </el-card>
      <el-row :gutter="20">
        <el-col :span="12">
          <h3>受託人</h3>
          <el-card>
            <el-row>
              <el-col :span="12">
                <el-form-item label="公司名稱：">
                  {{ editForm.ROName }}
                  <!-- <el-input
                    v-model="editForm.ROName"
                    autocomplete="off"
                    clearable
                    disabled
                  /> -->
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item label="電話：">
                  {{ editForm.ROTel }}
                  <!-- <el-input
                    v-model="editForm.ROTel"
                    autocomplete="off"
                    clearable
                    disabled
                  /> -->
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col>
                <el-form-item label="地址：">
                  {{ editForm.ROAdd }}
                  <!-- <el-input
                    v-model="editForm.ROAdd"
                    autocomplete="off"
                    clearable
                    disabled
                    style="width: 320px"
                  /> -->
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="12">
                <el-form-item label="不動產經紀人：">
                  {{ editForm.AGName }}
                  <!-- <el-input
                    v-model="editForm.AGName"
                    autocomplete="off"
                    clearable
                    disabled
                  /> -->
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item label="證號：">
                  {{ editForm.AGLRNo }}
                  <!-- <el-input
                    v-model="editForm.AGLRNo"
                    autocomplete="off"
                    clearable
                    disabled
                  /> -->
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="12">
                <el-form-item label="承辦人員：">
                  {{ editForm.ROUserName }}
                  <!-- <el-input
                    v-model="editForm.ROUserName"
                    autocomplete="off"
                    clearable
                    disabled
                  /> -->
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item label="連絡電話：">
                  <el-input
                    v-model="editForm.BZ009"
                    autocomplete="off"
                    clearable
                    class="input-with-select"
                    onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                    :maxlength="10"
                  />
                </el-form-item>
              </el-col>
            </el-row>
          </el-card>
        </el-col>
        <el-col :span="12">
          <h3>委託人</h3>
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
                  <!-- <el-input
                    v-model="editForm.LSName"
                    placeholder="請輸入姓名"
                    autocomplete="off"
                    clearable
                    disabled
                  /> -->
                </el-form-item>
              </el-col>
              <el-col>
                <el-form-item label="身分證字號：">
                  <!-- <el-badge
                    value="(統一編號)"
                    type="info" -->
                  <!-- > -->
                  {{ editForm.LSID }}
                  <!-- <el-input
                      v-model="editForm.LSID"
                      autocomplete="off"
                      clearable
                      disabled
                    /> -->
                  <!-- </el-badge> -->
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
                  <!-- <el-input
                    v-model="editForm.LSTel_2"
                    placeholder="請輸入電話"
                    clearable
                    style="width: 220px"
                    class="input-with-select"
                    onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                  >
                    <el-select
                      slot="prepend"
                      v-model="editForm.LSTel_1"
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
                  </el-input> -->
                </el-form-item>
              </el-col>
            </el-row>
            <div
              class="text item"
              style="font-size: 8px;"
            >
              <div>契約審閱權：本委託書於已由委託人攜回審閱至少3日，並充分瞭解本契約內容。 </div>
              <div>中華民國
                <el-input
                  v-model="editForm.BZ010"
                  autocomplete="off"
                  clearable
                  class="w70px"
                  size="mini"
                  readonly
                  onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                />年
                <el-input
                  v-model="editForm.BZ011"
                  autocomplete="off"
                  clearable
                  class="w60px"
                  size="mini"
                  readonly
                  onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                />月
                <el-input
                  v-model="editForm.BZ012"
                  autocomplete="off"
                  clearable
                  class="w60px"
                  size="mini"
                  readonly
                  onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                />日
              </div>
            </div>
          </el-card>
        </el-col>
      </el-row>
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
import { parseTime } from '@/utils/index';
import { getImg } from '@/api/chaochi/externalform/externalformservice';
import { validateCellReg } from '@/utils/validate';

export default {
  name: 'A000202',
  props: {
    editform: { type: Object, default: null }
  },
  data() {
    return {
      editForm: { ...this.editform },
      rule: {
        LNCell: [
          { required: true, trigger: 'change', validator: validateCellReg }
        ]
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
      src_1: '',
      BCook: '',
      BPet: '',
      BGodT: '',
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
          this.BCook =
            this.editForm.BCook_Y === '/Yes'
              ? '1'
              : this.editForm.BCook_N === '/Yes'
                ? '2'
                : '';
          this.BPet =
            this.editForm.BPet_Y === '/Yes'
              ? '1'
              : this.editForm.BPet_N === '/Yes'
                ? '2'
                : '';
          this.BGodT =
            this.editForm.BGodT_Y === '/Yes'
              ? '1'
              : this.editForm.BGodT_N === '/Yes'
                ? '2'
                : '';

          this.GetSig();
        }
      }
    },
    BCook: {
      handler() {
        switch (this.BCook) {
          case '1':
            this.editForm.BCook_Y = '/Yes';
            this.editForm.BCook_N = '/Off';
            break;
          case '2':
            this.editForm.BCook_Y = '/Off';
            this.editForm.BCook_N = '/Yes';
            break;
        }
      }
    },
    BPet: {
      handler() {
        switch (this.BPet) {
          case '1':
            this.editForm.BPet_Y = '/Yes';
            this.editForm.BPet_N = '/Off';
            break;
          case '2':
            this.editForm.BPet_Y = '/Off';
            this.editForm.BPet_N = '/Yes';
            break;
        }
      }
    },
    BGodT: {
      handler() {
        switch (this.BGodT) {
          case '1':
            this.editForm.BGodT_Y = '/Yes';
            this.editForm.BGodT_N = '/Off';
            break;
          case '2':
            this.editForm.BGodT_Y = '/Off';
            this.editForm.BGodT_N = '/Yes';
            break;
        }
      }
    }
  },
  created() {
    this.editForm.BZ010 = parseTime(new Date(), '{c}');
    this.editForm.BZ011 = parseTime(new Date(), '{m}');
    this.editForm.BZ012 = parseTime(new Date(), '{d}');
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
                  link.download = '社會住宅委託書.pdf';
                  link.click();
                  URL.revokeObjectURL(link.href);
                  const formData = new FormData();
                  var file = new File([blob], '社會住宅委託書' + '.pdf', {
                    type: 'pdf'
                  });
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
    }
  }
};
</script>

<style scoped>
.el-radio {
  margin-right: 5px !important;
  color: #e24e4e !important;
}
.el-checkbox {
  margin-right: 15px !important;
}
.el-checkbox__label {
  padding-left: 2px !important;
}
</style>

