<template>
  <div>
    <el-form
      ref="editForm"
      v-loading="saveLoading"
      element-loading-text="儲存中..."
      :inline="true"
      :model="editForm"
      class="demo-form-inline"
      :rules="rule"
    >
      <el-row>
        <el-col :span="8">
          <el-form-item
            label="建物狀態："
            :label-width="formLabelWidth"
            style="width: 100%"
          >
            <el-input
              v-model="editForm.BState"
              autocomplete="off"
              clearable
              disabled
            />
          </el-form-item>
        </el-col>
        <el-col :span="16">
          <el-form-item
            label="管委會："
            :label-width="formLabelWidth"
          >
            <el-radio
              v-model="radioBMFee"
              label="1"
            >有</el-radio>
            <el-radio
              v-model="radioBMFee"
              label="2"
            >無</el-radio>
          </el-form-item>
          <el-form-item
            v-if="editForm.BMFee_Y === '/Yes'"
            label="管理費元："
            label-width="135px"
            prop="BMFee"
          >
            <el-input
              v-model="editForm.BMFee"
              placeholder="請輸入管理費"
              autocomplete="off"
              clearable
            />
          </el-form-item>
          <el-form-item
            v-if="editForm.BMFee_Y === '/Yes'"
            label="每坪金額："
            label-width="100px"
          >
            <el-input
              v-model="editForm.BMFeePing"
              autocomplete="off"
              style="width: 80px"
              disabled
            />
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="8">
          <!-- <el-form-item
            label="屬性："
            :label-width="formLabelWidth"
          >
            <el-select
              v-model="propValue"
              placeholder="請選擇"
            >
              <el-option
                v-for="item in propOptions"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              />
            </el-select>
          </el-form-item> -->
          <el-form-item
            label="建物類型："
            :label-width="formLabelWidth"
          >
            <el-input
              v-model="editForm.BPropoty"
              autocomplete="off"
              clearable
              readonly
            />
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item
            label="車位："
            :label-width="formLabelWidth"
          >
            <el-radio
              v-model="radioBCar"
              label="1"
            >有</el-radio>
            <el-radio
              v-model="radioBCar"
              label="2"
            >無</el-radio>
          </el-form-item>
          <el-form-item
            v-if="editForm.BCar_Y === '/Yes'"
            label="車位租金元/月："
            label-width="135px"
          >
            <el-input
              v-model="editForm.BParkFee"
              placeholder="請輸入車位租金"
              autocomplete="off"
              clearable
            />
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="8">
          <el-form-item
            label="市場租金："
            :label-width="formLabelWidth"
          >
            <el-input
              v-model="editForm.BTRent"
              placeholder="請輸入租金"
              autocomplete="off"
              clearable
            />
          </el-form-item>
        </el-col>
        <!-- <el-col :span="8">
          <el-form-item
            label="出租起訖日期："
            :label-width="formLabelWidth"
          >
            <DatePickerE
              v-model="editForm.BRDStart"
              value-format="yyyy-MM-dd"
              format="yyyy-MM-dd"
              type="date"
              placeholder="選擇日期"
              style="width: 160px"
            /> -
            <DatePickerE
              v-model="editForm.BRDTEnd"
              value-format="yyyy-MM-dd"
              format="yyyy-MM-dd"
              type="date"
              placeholder="選擇日期"
              style="width: 160px"
            />
          </el-form-item>
        </el-col> -->
        <el-col :span="6">
          <el-form-item
            label="公寓大樓規約："
            :label-width="formLabelWidth"
          >
            <el-radio
              v-model="radioBConStature"
              label="1"
            >有</el-radio>
            <el-radio
              v-model="radioBConStature"
              label="2"
            >無</el-radio>
          </el-form-item>
        </el-col>
        <el-col :span="6">
          <el-form-item
            label="電梯："
            :label-width="formLabelWidth"
          >
            <el-radio
              v-model="radioBelevator"
              label="1"
            >有</el-radio>
            <el-radio
              v-model="radioBelevator"
              label="2"
            >無</el-radio>
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="8">
          <el-form-item
            label="合約租金："
            :label-width="formLabelWidth"
          >
            <el-input
              v-model="editForm.BTCRent"
              placeholder="請輸入租金"
              autocomplete="off"
              clearable
              style="width:150px"
            />
            <el-form-item
              label="每坪租金："
              label-width="100px"
            >
              <el-input
                v-model="editForm.BTCRentPing"
                autocomplete="off"
                style="width: 80px"
                disabled
              />
            </el-form-item>
          </el-form-item>
        </el-col>
        <el-col :span="6">
          <el-form-item
            label="定期消安檢查："
            :label-width="formLabelWidth"
          >
            <el-radio
              v-model="radioBFireSaIn"
              label="1"
            >有</el-radio>
            <el-radio
              v-model="radioBFireSaIn"
              label="2"
            >無</el-radio>
          </el-form-item>
        </el-col>
        <el-col :span="6">
          <el-form-item
            label="神明桌："
            :label-width="formLabelWidth"
          >
            <el-radio
              v-model="radioBGodT"
              label="1"
            >可</el-radio>
            <el-radio
              v-model="radioBGodT"
              label="2"
            >否</el-radio>
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="8">
          <el-form-item
            label="租金支付："
            :label-width="formLabelWidth"
          >
            <el-select
              v-model="payValue"
              placeholder="請選擇"
            >
              <el-option
                v-for="item in payOptions"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              />
            </el-select>
          </el-form-item>
        </el-col>
        <el-col :span="6">&nbsp;
        </el-col>
        <el-col :span="6">
          <el-form-item
            label="炊煮："
            :label-width="formLabelWidth"
          >
            <el-radio
              v-model="radioBCook"
              label="1"
            >可</el-radio>
            <el-radio
              v-model="radioBCook"
              label="2"
            >否</el-radio>
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="8">
          <el-form-item
            label="押金："
            :label-width="formLabelWidth"
          >
            <el-input
              v-model="editForm.Bdeposit"
              placeholder="請輸入押金"
              autocomplete="off"
              clearable
              style="width: 150px"
            />
          </el-form-item>
          <el-form-item
            label="為"
            label-width="20px"
          >
            <el-input
              v-model="editForm.BDMon"
              autocomplete="off"
              clearable
              style="width: 80px"
            />
          </el-form-item>
          <el-form-item
            label="個月租金"
            label-width="70px"
          />
        </el-col>
        <el-col :span="6">&nbsp;
        </el-col>
        <el-col :span="6">
          <el-form-item
            label="寵物："
            :label-width="formLabelWidth"
          >
            <el-radio
              v-model="radioBPet"
              label="1"
            >可</el-radio>
            <el-radio
              v-model="radioBPet"
              label="2"
            >否</el-radio>
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="12">
          <h3>本建物(專有部分)是否曾發生兇殺、自殺、一氧化碳中毒或其他非自然死亡情事：</h3>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="12">
          <el-form-item
            label="1.於產權持有期間"
            label-width="1 px"
          >
            <el-radio
              v-model="radio"
              label="1"
            >有</el-radio>
            <el-radio
              v-model="radio"
              label="2"
            >無</el-radio>
          </el-form-item>
          <el-form-item
            label="曾發生上列情事。"
            label-width="1 px"
          />
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="12">
          <el-form-item
            label="2.於產權持有前，出租人 "
            label-width="1 px"
          >
            <el-radio
              v-model="radioMurder"
              label="1"
            >確認無上列情事</el-radio>
            <el-radio
              v-model="radioMurder"
              label="2"
            >知道曾發生上列情事</el-radio>
            <el-radio
              v-model="radioMurder"
              label="3"
            >不知曾否發生上列情事</el-radio>
          </el-form-item>
          <el-form-item
            label="。"
            label-width="1 px"
          />
        </el-col>
      </el-row>
    </el-form>
    <div class="rightbtn">
      <el-button
        v-hasPermi="['Building/Edit']"
        type="primary"
        size="small"
        icon="el-icon-paperclip"
        @click="saveBuildingSituation()"
      >儲存</el-button>
      <el-button
        size="small"
        icon="el-icon-close"
        @click="cancel"
      >關閉</el-button>
    </div>
  </div>
</template>

<script>
import { UpdateBuildingSituation } from '@/api/chaochi/building/buildingservice';
// import DatePickerE from '@/components/RocDatepickerE';
export default {
  name: 'BuildingSituation',
  // components: { DatePickerE },
  props: {
    editform: { type: Object, default: null },
    emitbping: { type: String, default: '' }
  },
  data() {
    return {
      testMsg: '開發中..',
      saveLoading: false,
      editForm: {
        BRDStart: '',
        BRDTEnd: ''
      },
      rule: {
        BMFee: []
        // LCAGCell_B: [
        //   { required: false, trigger: 'change', validator: validateCellReg }
        // ]
      },
      formLabelWidth: '135px',
      propOptions: [
        {
          value: 'BCharter',
          label: '包租案'
        },
        {
          value: 'BChangeRent',
          label: '代租案'
        },
        {
          value: 'BSecondLandlord',
          label: '服務事業轉租'
        },
        {
          value: 'BManagement',
          label: '服務事業協助出租'
        }
      ],
      propValue: '',
      payValue: '',
      payOptions: [
        {
          value: 'BTransfer',
          label: '轉帳'
        },
        {
          value: 'BBill',
          label: '票據'
        },
        {
          value: 'BCash',
          label: '現金'
        },
        {
          value: 'BCreditCard',
          label: '信用卡'
        }
      ],
      radio: '',
      radioMurder: '',
      radioBConStature: '',
      // radioBSuper: '',
      radioBFireSaIn: '',
      radioBMFee: '',
      radioBCar: '',
      radioBCook: '',
      radioBPet: '',
      radioBGodT: '',
      radioBelevator: ''
    };
  },
  watch: {
    editform: {
      handler() {
        this.editForm = this.editform;
        this.propValue =
          this.editForm.BCharter === '/Yes'
            ? 'BCharter'
            : this.editForm.BChangeRent === '/Yes'
              ? 'BChangeRent'
              : this.editForm.BSecondLandlord === '/Yes'
                ? 'BSecondLandlord'
                : this.editForm.BManagement === '/Yes'
                  ? 'BManagement'
                  : '';
        this.radio =
          this.editForm.BMurder_Y === '/Yes'
            ? '1'
            : this.editForm.BMurder_N === '/Yes'
              ? '2'
              : '';
        this.radioMurder =
          this.editForm.BMurder_No_Check === '/Yes'
            ? '1'
            : this.editForm.BMurder_Yes_Check === '/Yes'
              ? '2'
              : this.editForm.BMurder_Yes_Dont === '/Yes'
                ? '3'
                : '';
        this.payValue =
          this.editForm.BTransfer === '/Yes'
            ? 'BTransfer'
            : this.editForm.BBill === '/Yes'
              ? 'BBill'
              : this.editForm.BCash === '/Yes'
                ? 'BCash'
                : this.editForm.BCreditCard === '/Yes'
                  ? 'BCreditCard'
                  : '';
        this.radioBConStature =
          this.editForm.BConStature_Y === '/Yes'
            ? '1'
            : this.editForm.BConStature_N === '/Yes'
              ? '2'
              : '';
        // this.radioBSuper =
        //   this.editForm.BSuper_Y === '/Yes'
        //     ? '1'
        //     : this.editForm.BSuper_N === '/Yes'
        //       ? '2'
        //       : '';
        this.radioBFireSaIn =
          this.editForm.BFireSaIn_Y === '/Yes'
            ? '1'
            : this.editForm.BFireSaIn_N === '/Yes'
              ? '2'
              : '';
        this.radioBMFee =
          this.editForm.BMFee_Y === '/Yes'
            ? '1'
            : this.editForm.BMFee_N === '/Yes'
              ? '2'
              : '';
        this.radioBCar =
          this.editForm.BCar_Y === '/Yes'
            ? '1'
            : this.editForm.BCar_N === '/Yes'
              ? '2'
              : '';
        this.radioBCook =
          this.editForm.BCook_Y === '/Yes'
            ? '1'
            : this.editForm.BCook_N === '/Yes'
              ? '2'
              : '';
        this.radioBPet =
          this.editForm.BPet_Y === '/Yes'
            ? '1'
            : this.editForm.BPet_N === '/Yes'
              ? '2'
              : '';
        this.radioBGodT =
          this.editForm.BGodT_Y === '/Yes'
            ? '1'
            : this.editForm.BGodT_N === '/Yes'
              ? '2'
              : '';
        this.radioBelevator =
          this.editForm.Belevator_Y === '/Yes'
            ? '1'
            : this.editForm.Belevator_N === '/Yes'
              ? '2'
              : '';
      }
    },
    propValue: {
      handler() {
        switch (this.propValue) {
          case 'BCharter':
            this.editForm.BCharter = '/Yes';
            this.editForm.BChangeRent = '/Off';
            this.editForm.BSecondLandlord = '/Off';
            this.editForm.BManagement = '/Off';
            break;
          case 'BChangeRent':
            this.editForm.BCharter = '/Off';
            this.editForm.BChangeRent = '/Yes';
            this.editForm.BSecondLandlord = '/Off';
            this.editForm.BManagement = '/Off';
            break;
          case 'BSecondLandlord':
            this.editForm.BCharter = '/Off';
            this.editForm.BChangeRent = '/Off';
            this.editForm.BSecondLandlord = '/Yes';
            this.editForm.BManagement = '/Off';
            break;
          case 'BManagement':
            this.editForm.BCharter = '/Off';
            this.editForm.BChangeRent = '/Off';
            this.editForm.BSecondLandlord = '/Off';
            this.editForm.BManagement = '/Yes';
            break;
        }
      }
    },
    radio: {
      handler() {
        switch (this.radio) {
          case '1':
            this.editForm.BMurder_Y = '/Yes';
            this.editForm.BMurder_N = '/Off';
            break;
          case '2':
            this.editForm.BMurder_Y = '/Off';
            this.editForm.BMurder_N = '/Yes';
            break;
        }
      }
    },
    radioMurder: {
      handler() {
        switch (this.radioMurder) {
          case '1':
            this.editForm.BMurder_No_Check = '/Yes';
            this.editForm.BMurder_Yes_Check = '/Off';
            this.editForm.BMurder_Yes_Dont = '/Off';
            break;
          case '2':
            this.editForm.BMurder_No_Check = '/Off';
            this.editForm.BMurder_Yes_Check = '/Yes';
            this.editForm.BMurder_Yes_Dont = '/Off';
            break;
          case '3':
            this.editForm.BMurder_No_Check = '/Off';
            this.editForm.BMurder_Yes_Check = '/Off';
            this.editForm.BMurder_Yes_Dont = '/Yes';
            break;
        }
      }
    },
    payValue: {
      handler() {
        switch (this.payValue) {
          case 'BTransfer':
            this.editForm.BTransfer = '/Yes';
            this.editForm.BBill = '/Off';
            this.editForm.BCash = '/Off';
            this.editForm.BCreditCard = '/Off';
            break;
          case 'BBill':
            this.editForm.BTransfer = '/Off';
            this.editForm.BBill = '/Yes';
            this.editForm.BCash = '/Off';
            this.editForm.BCreditCard = '/Off';
            break;
          case 'BCash':
            this.editForm.BTransfer = '/Off';
            this.editForm.BBill = '/Off';
            this.editForm.BCash = '/Yes';
            this.editForm.BCreditCard = '/Off';
            break;
          case 'BCreditCard':
            this.editForm.BTransfer = '/Off';
            this.editForm.BBill = '/Off';
            this.editForm.BCash = '/Off';
            this.editForm.BCreditCard = '/Yes';
            break;
        }
      }
    },
    radioBConStature: {
      handler() {
        switch (this.radioBConStature) {
          case '1':
            this.editForm.BConStature_Y = '/Yes';
            this.editForm.BConStature_N = '/Off';
            break;
          case '2':
            this.editForm.BConStature_Y = '/Off';
            this.editForm.BConStature_N = '/Yes';
            break;
        }
      }
    },
    // radioBSuper: {
    //   handler() {
    //     switch (this.radioBSuper) {
    //       case '1':
    //         this.editForm.BSuper_Y = '/Yes';
    //         this.editForm.BSuper_N = '/Off';
    //         break;
    //       case '2':
    //         this.editForm.BSuper_Y = '/Off';
    //         this.editForm.BSuper_N = '/Yes';
    //         break;
    //     }
    //   }
    // },
    radioBFireSaIn: {
      handler() {
        switch (this.radioBFireSaIn) {
          case '1':
            this.editForm.BFireSaIn_Y = '/Yes';
            this.editForm.BFireSaIn_N = '/Off';
            break;
          case '2':
            this.editForm.BFireSaIn_Y = '/Off';
            this.editForm.BFireSaIn_N = '/Yes';
            break;
        }
      }
    },
    radioBMFee: {
      handler() {
        switch (this.radioBMFee) {
          case '1':
            this.editForm.BMFee_Y = '/Yes';
            this.editForm.BMFee_N = '/Off';
            this.rule.BMFee.push({
              required: true,
              message: '請輸入金額',
              trigger: 'blur'
            });
            break;
          case '2':
            this.editForm.BMFee_Y = '/Off';
            this.editForm.BMFee_N = '/Yes';
            this.rule.BMFee = [];
            this.editForm.BMFee = '';
            break;
        }
      }
    },
    radioBCar: {
      handler() {
        switch (this.radioBCar) {
          case '1':
            this.editForm.BCar_Y = '/Yes';
            this.editForm.BCar_N = '/Off';
            break;
          case '2':
            this.editForm.BCar_Y = '/Off';
            this.editForm.BCar_N = '/Yes';
            break;
        }
      }
    },
    radioBCook: {
      handler() {
        switch (this.radioBCook) {
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
    radioBPet: {
      handler() {
        switch (this.radioBPet) {
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
    radioBGodT: {
      handler() {
        switch (this.radioBGodT) {
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
    },
    radioBelevator: {
      handler() {
        switch (this.radioBelevator) {
          case '1':
            this.editForm.Belevator_Y = '/Yes';
            this.editForm.Belevator_N = '/Off';
            break;
          case '2':
            this.editForm.Belevator_Y = '/Off';
            this.editForm.Belevator_N = '/Yes';
            break;
        }
      }
    },
    emitbping: {
      handler(a) {
        this.editForm.BPing = a;
        if (a && a > 0) {
          if (this.editForm.BMFee) {
            this.editForm.BMFeePing = Math.round(
              parseFloat(this.editForm.BMFee) / parseFloat(this.editForm.BPing)
            ).toString();
            this.editForm.BTCRentPing = Math.round(
              parseFloat(this.editForm.BTCRent) /
                parseFloat(this.editForm.BPing)
            ).toString();
          } else {
            this.editForm.BMFeePing = '0';
            this.editForm.BTCRentPing = '0';
          }
        } else {
          this.editForm.BMFeePing = '0';
          this.editForm.BTCRentPing = '0';
        }
      }
    },
    'editForm.BMFee': {
      handler(a) {
        if (this.editForm.BPing) {
          this.editForm.BMFeePing = Math.round(
            parseFloat(this.editForm.BMFee) / parseFloat(this.editForm.BPing)
          ).toString();
          if (parseFloat(a) > 0 && parseInt(this.editForm.BMFeePing) > 0) {
            this.editForm.BMFeePing = Math.round(
              parseFloat(this.editForm.BMFee) / parseFloat(this.editForm.BPing)
            ).toString();
          } else {
            this.editForm.BMFeePing = '0';
          }
        } else {
          this.editForm.BMFeePing = '0';
        }
      }
    },
    'editForm.BTCRent': {
      handler(a) {
        if (this.editForm.BPing) {
          this.editForm.BTCRentPing = Math.round(
            parseFloat(this.editForm.BTCRent) / parseFloat(this.editForm.BPing)
          ).toString();
          if (parseFloat(a) > 0 && parseInt(this.editForm.BTCRentPing) > 0) {
            this.editForm.BTCRentPing = Math.round(
              parseFloat(this.editForm.BTCRent) /
                parseFloat(this.editForm.BPing)
            ).toString();
          } else {
            this.editForm.BTCRentPing = '0';
          }
        } else {
          this.editForm.BTCRentPing = '0';
        }
      }
    }
  },

  methods: {
    cancel() {
      this.$emit('cancel');
    },
    saveBuildingSituation() {
      this.$refs['editForm'].validate(valid => {
        if (valid) {
          this.saveLoading = true;
          UpdateBuildingSituation(this.editForm)
            .then(res => {
              if (res.Success) {
                this.$message.success('儲存成功!');
              } else {
                this.$message.error('失敗!');
              }
            })
            .finally(() => {
              this.saveLoading = false;
            });
        } else {
          return false;
        }
      });
    }
  }
};
</script>

<style>
</style>
