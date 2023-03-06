<template>
  <div>
    <h3>{{ title }}</h3>
    <el-card
      class="box-card"
      style="width: 520px"
    >
      <el-form
        ref="form"
        :model="formData"
        label-width="120px"
      >
        <el-form-item
          label="身分別："
          prop="Role"
        >
          <el-radio
            v-model="formData.Role"
            label="LandLord"
          >出租人</el-radio>
          <el-radio
            v-model="formData.Role"
            label="Renter"
          >承租人</el-radio>
        </el-form-item>
        <el-form-item
          v-if="formData.Role !== ''"
          prop="NC"
        >
          <el-radio
            v-model="formData.NC"
            label="Nature"
          >自然人</el-radio>
          <el-radio
            v-model="formData.NC"
            label="Company"
          >法人</el-radio>
        </el-form-item>
        <template
          v-if="formData.NC === 'Nature'"
          ref="自然人"
        >
          <el-form-item
            label="姓名："
            prop="Name"
          >
            <el-input
              v-model="formData.Name"
              clearable
              style="width: 220px"
            />
          </el-form-item>
          <el-form-item
            label="身分證字號："
            prop="ID"
          >
            <el-input
              v-model="formData.ID"
              v-upperCase
              clearable
              style="width: 220px"
            />
          </el-form-item>
          <el-form-item
            label="生日："
            prop="BirthDay"
          >
            <DatePickerE
              v-model="formData.BirthDay"
              value-format="yyyy-MM-dd"
              format="yyyy-MM-dd"
              type="date"
              placeholder="選擇日期"
            />
          </el-form-item>
          <el-form-item label="手機：">
            <el-input
              v-model="formData.Cell"
              clearable
              style="width: 220px"
            />
          </el-form-item>
          <el-form-item label="電話：">
            <el-input
              v-model="formData.Tel1"
              clearable
              style="width: 80px"
            /> -
            <el-input
              v-model="formData.Tel2"
              clearable
              style="width: 130px"
            />
          </el-form-item>
        </template>
        <template
          v-if="formData.NC === 'Company'"
          ref="法人"
        >
          <el-form-item
            label="法人名稱："
            prop="Name"
          >
            <el-input
              v-model="formData.Name"
              clearable
              style="width: 220px"
            />
          </el-form-item>
          <el-form-item
            label="統一編號："
            prop="ID"
          >
            <el-input
              v-model="formData.ID"
              clearable
              style="width: 220px"
            />
          </el-form-item>
          <el-form-item
            label="代表人："
            prop="Rep"
          >
            <el-input
              v-model="formData.Rep"
              clearable
              style="width: 220px"
            />
          </el-form-item>
          <el-form-item label="公司電話：">
            <el-input
              v-model="formData.Tel1"
              clearable
              style="width: 80px"
            /> -
            <el-input
              v-model="formData.Tel2"
              clearable
              style="width: 130px"
            />
          </el-form-item>
        </template>
        <div class="rightbtn">
          <el-button
            :disabled="disBtn"
            type="primary"
            size="small"
            icon="el-icon-paperclip"
            @click="RequestAccess()"
          >送出</el-button>
        </div>
      </el-form>
    </el-card>
  </div>
</template>

<script>
import DatePickerE from '@/components/RocDatepickerE';
import { RequestAccess } from '@/api/chaochi/RequestAccess/buildingservice';
export default {
  name: 'RequestAccess',
  components: { DatePickerE },
  data() {
    return {
      formData: {
        Role: '',
        NC: '',
        Name: '',
        ID: '',
        BirthDay: '',
        Cell: '',
        Tel1: '',
        Tel2: '',
        Rep: ''
      },
      title: '請先選擇身分別',
      disBtn: true
    };
  },
  watch: {
    'formData.NC': {
      handler() {
        this.title =
          this.formData.NC === 'Nature'
            ? '請輸入完整資訊，手機及電話二擇一填寫，其餘欄位必填'
            : this.formData.NC === 'Company'
              ? '請輸入完整資訊，所有欄位必填'
              : '請先選擇身分別';
      }
    },
    formData: {
      deep: true,
      handler() {
        if (this.formData.NC === 'Nature') {
          this.disBtn =
            this.formData.Name === '' ||
            this.formData.ID === '' ||
            this.formData.BirthDay === '' ||
            this.formData.BirthDay === null ||
            (this.formData.Cell === '' &&
              (this.formData.Tel1 === '' || this.formData.Tel2 === ''));
        } else if (this.formData.NC === 'Company') {
          this.disBtn =
            this.formData.Name === '' ||
            this.formData.ID === '' ||
            this.formData.Rep === '' ||
            this.formData.Tel1 === '' ||
            this.formData.Tel2 === '';
        }
      }
    }
  },
  methods: {
    RequestAccess() {
      RequestAccess(this.formData).then(res => {
        if (res.Success) {
          this.$message.success('成功!');
        } else {
          this.$message.error('失敗!');
        }
      });
    }
  }
};
</script>

<style>
</style>
