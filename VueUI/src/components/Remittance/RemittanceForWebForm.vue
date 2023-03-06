<template>
  <div>
    <el-form
      ref="editForm"
      :inline="true"
      :model="editForm"
      class="demo-form-inline"
    >
      <el-form-item label="帳戶名稱：">
        <el-input
          v-model="editForm.LAName"
          size="mini"
          :disabled="disabled"
        />
      </el-form-item>
      <br>
      <el-form-item label="金融機構名稱：">
        <el-select
          v-model="editForm.LBankName"
          placeholder=" "
          filterable
          size="mini"
          class="w160px"
          :disabled="disabled"
        >
          <el-option
            v-for="item in bankNameOptions"
            :key="item.Id"
            :label="item.BankName"
            :value="item.BankName"
          />
        </el-select>
        <!-- <el-input
          v-model="editForm.LBankName"
          size="mini"
          :disabled="disabled"
        /> -->
      </el-form-item>
      <el-form-item label="(">
        <el-input
          v-model="editForm.LBankNo"
          size="mini"
          style="width: 85px"
          :maxlength="3"
          disabled
        />
      </el-form-item>
      <el-form-item label=") 金融機構代碼3碼" />
      <br>
      <el-form-item label="分行名稱：">
        <el-select
          v-model="editForm.LBranchName"
          placeholder=" "
          filterable
          size="mini"
          class="w160px"
          :disabled="disabled"
        >
          <el-option
            v-for="item in branchNameOptions"
            :key="item.Id"
            :label="item.BranchName"
            :value="item.BranchName"
          />
        </el-select>
        <!-- <el-input
          v-model="editForm.LBranchName"
          size="mini"
          :disabled="disabled"
        /> -->
      </el-form-item>

      <el-form-item label="(">
        <el-input
          v-model="editForm.LBranchNo"
          size="mini"
          style="width: 85px"
          :maxlength="4"
          disabled
        />
      </el-form-item>
      <el-form-item label=") 分行代碼4碼" />
      <br>
      <el-form-item label="帳戶號碼：">
        <el-input
          v-model="editForm.LANo"
          size="mini"
          :disabled="disabled"
        />
      </el-form-item>
    </el-form>
  </div>
</template>

<script>
import {
  GetAllBanks,
  GetAllBranchByBankName
} from '@/api/chaochi/bankinfo/bankinfo';
export default {
  name: 'RemittanceForWebForm',
  props: {
    sendedform: { type: Object, default: null },
    fielddisabled: { type: Boolean, default: true }
  },
  data() {
    return {
      editForm: { ...this.sendedform },
      bankNameOptions: [],
      branchNameOptions: [],
      disabled: this.fielddisabled
    };
  },
  watch: {
    sendedform: {
      deep: true,
      immediate: true,
      handler() {
        this.editForm = this.sendedform;
        // this.$emit('getremittancebyemit', this.editForm);
      }
    },
    editForm: {
      deep: true,
      handler() {
        this.$emit('getremittancebyemit', this.editForm);
      }
    },
    'editForm.LBankName': {
      handler(a) {
        console.log(a);
        // console.log(this.bankNameOptions.filter(x => x.BankName === a)[0].Id);
        this.editForm.LBankNo = this.bankNameOptions.filter(
          x => x.BankName === a
        )[0].BankNo;
        GetAllBranchByBankName(this.editForm.LBankName).then(res => {
          this.branchNameOptions = res.ResData;
        });
        this.editForm.LBranchName = '';
        this.editForm.LBranchNo = '';
      }
    },
    'editForm.LBranchName': {
      handler(a) {
        if (a) {
          this.editForm.LBranchNo = this.branchNameOptions.filter(
            x => x.BranchName === a
          )[0].BranchNo;
        }
      }
    }
  },
  created() {
    GetAllBanks().then(res => {
      this.bankNameOptions = res.ResData;
    });
  }
};
</script>

<style>
</style>
