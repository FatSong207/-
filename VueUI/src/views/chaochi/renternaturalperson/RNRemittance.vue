<template>
  <div>
    <div class="list-btn-container">
      <el-button-group>
        <el-button
          v-hasPermi="['Building/Add']"
          type="primary"
          icon="el-icon-plus"
          size="small"
          @click="ShowEditOrViewDialog()"
        >新增</el-button>
        <el-button
          v-hasPermi="['Building/Edit']"
          type="primary"
          icon="el-icon-edit"
          size="small"
          @click="ShowEditOrViewDialog('edit')"
        >修改</el-button>
        <el-button
          type="default"
          icon="el-icon-refresh"
          size="small"
          @click="loadTableData()"
        >更新</el-button>
      </el-button-group>
    </div>
    <el-table
      ref="gridtable"
      v-loading="loading"
      :data="tableData"
      border
      stripe
      highlight-current-row
      style="width: 100%"
      @select="handleSelectChange"
    >
      <el-table-column
        type="selection"
        width="30"
      />
      <el-table-column
        prop="RAName"
        label="帳戶名稱"
      />
      <el-table-column
        prop="RAID"
        label="身份證字號" width="150"
      />
      <el-table-column
        prop="RBankName"
        label="金融機構名稱" width="200"
      />
      <el-table-column
        prop="RBankNo"
        label="金融機構代碼" width="150"
      />
      <el-table-column
        prop="RBranchName"
        label="分行名稱" width="200"
      />
      <el-table-column
        prop="RBranchNo"
        label="分行代碼" width="150"
      />
      <el-table-column
        prop="RANo"
        label="帳戶號碼"
      />
    </el-table>
    <div class="rightbtn">
      <el-button
        size="small" icon="el-icon-close" @click="cancel"
      >關閉</el-button>
    </div>
    <el-dialog
      ref="dialogEditForm"
      :title="editFormTitle"
      :visible.sync="dialogEditFormVisible"
      width="880px"
      append-to-body
      :close-on-press-escape="false"
      :show-close="false"
    >
      <el-form
        ref="editForm"
        v-loading="loadingEditForm"
        :inline="true"
        :model="editForm"
        class="demo-form-inline"
        :rules="rule"
      >
        <el-row>
          <el-col :span="12">
            <el-form-item
              label="帳戶名稱："
              :label-width="formLabelWidth"
              prop="RAName"
            >
              <el-input v-model="editForm.RAName" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item
              label="身份證字號："
              :label-width="formLabelWidth"
              prop="RAID"
            >
              <el-input
                v-model="editForm.RAID"
                v-upperCase
                clearable
                :maxlength="10"
              />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="12">
            <el-form-item
              label="金融機構名稱："
              :label-width="formLabelWidth"
              prop="RBankName"
            >
              <el-select
                v-model="editForm.RBankName"
                filterable class="w160px"
              >
                <el-option
                  v-for="item in bankNameOptions"
                  :key="item.Id"
                  :label="item.BankName"
                  :value="item.BankName"
                />
              </el-select>
              <!-- <el-input v-model="editForm.RBankName" /> -->
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item
              label="金融機構代碼："
              :label-width="formLabelWidth"
            >
              <el-input
                v-model="editForm.RBankNo"
                disabled
              />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="12">
            <el-form-item
              label="分行名稱："
              :label-width="formLabelWidth"
              prop="RBranchName"
            >
              <el-select
                v-model="editForm.RBranchName"
                filterable
                class="w160px"
              >
                <el-option
                  v-for="item in branchNameOptions"
                  :key="item.Id"
                  :label="item.BranchName"
                  :value="item.BranchName"
                />
              </el-select>
              <!-- <el-input v-model="editForm.RBranchName" /> -->
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item
              label="分行代碼："
              :label-width="formLabelWidth"
            >
              <el-input
                v-model="editForm.RBranchNo"
                disabled
              />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="24">
            <el-form-item
              label="帳戶號碼："
              :label-width="formLabelWidth"
              prop="RANo"
            >
              <el-input v-model="editForm.RANo" />
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
      <div
        slot="footer"
        class="dialog-footer"
      >
        <el-button
          size="small" icon="el-icon-close" @click="ResetEditDialog()"
        >關閉</el-button>
        <el-button
          type="primary"
          size="small"
          icon="el-icon-video-play"
          @click="saveEditForm()"
        >確認</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
// import {
//   SaveRemittance,
//   GetRemittanceByLNID,
//   GetRemittanceById
// } from '@/api/chaochi/remittance/remittanceservice';
import {
  saveRemittanceR,
  getRemittanceRDetail,
  GetRemittancesByCustomerRId
} from '@/api/chaochi/remittance/remittanceR';
import { GetAllBanks, GetAllBranchs } from '@/api/chaochi/bankinfo/bankinfo';
import { validateIDCombo } from '@/utils/validate';
export default {
  name: 'RNRemittance',
  props: {
    id: { type: String, default: null },
    idno: { type: String, default: null },
    name: { type: String, default: null }
  },
  data() {
    return {
      testmsg: '開發中..',
      rule: {
        RAID: [
          { required: false, trigger: 'blur', validator: validateIDCombo }
        ],
        RAName: [
          { required: true, trigger: 'blur', message: '請輸入帳戶名稱' }
        ],
        RBankName: [
          { required: true, trigger: 'change', message: '請選擇金融機構名稱' }
        ],
        RBranchName: [
          { required: true, trigger: 'change', message: '請選擇分行名稱' }
        ],
        RANo: [{ required: true, trigger: 'blur', message: '請輸入帳戶號碼' }]
      },
      loading: false,
      loadingEditForm: false,
      data: [],
      bankNameOptions: [],
      allbranch: [],
      branchNameOptions: [],
      currentId: '',
      currentSelected: [],
      editFormTitle: '',
      dialogEditFormVisible: false,
      formLabelWidth: '165px',
      editForm: {
        RAName: this.name,
        RAID: this.idno,
        RBankName: '',
        RBranchName: ''
      },
      tableData: []
    };
  },
  computed: {
    tableDataCount() {
      return this.tableData.length;
    }
  },
  watch: {
    id: {
      handler() {
        this.loadTableData();
      }
    },
    'editForm.RBankName': {
      handler(a, b) {
        if (a) {
          this.editForm.RBankNo = this.bankNameOptions.filter(
            (x) => x.BankName === a
          )[0].BankNo;
          this.branchNameOptions = this.allbranch.filter(
            (x) => x.BankNo === this.editForm.RBankNo
          );
          if (this.editForm.RBankName !== a || b !== '') {
            this.editForm.RBranchName = '';
            this.editForm.RBranchNo = '';
          }
        }
      }
    },
    'editForm.RBranchName': {
      handler(a) {
        console.log(a);
        if (a) {
          console.log(a);
          this.editForm.RBranchNo = this.allbranch.filter(
            (x) => x.BranchName === a && x.BankNo === this.editForm.RBankNo
          )[0].BranchNo;
        }
      }
    }
  },
  created() {
    this.loadTableData();
    GetAllBanks().then((res) => {
      this.bankNameOptions = res.ResData;
    });
    GetAllBranchs().then((res) => {
      this.allbranch = res.ResData;
    });
  },
  methods: {
    cancel() {
      this.$emit('cancel');
    },
    ResetEditDialog() {
      this.dialogEditFormVisible = false;
      this.editForm = {
        RBankName: '',
        RBranchName: '',
        RBankNo: '',
        RBranchNo: ''
      };
    },
    ShowEditOrViewDialog: function(view) {
      if (view !== undefined) {
        if (
          this.currentSelected.length > 1 ||
          this.currentSelected.length === 0
        ) {
          this.$alert('請選擇一條數據進行編輯/修改', '提示');
        } else {
          this.currentId = this.currentSelected[0].Id;
          this.editFormTitle = '編輯';
          this.dialogEditFormVisible = true;
          this.loadingEditForm = true;
          getRemittanceRDetail(this.currentId).then((res) => {
            this.editForm = res.ResData;
            this.loadingEditForm = false;
          });
        }
      } else {
        this.editFormTitle = '新增';
        this.currentId = '';
        this.dialogEditFormVisible = true;
        this.editForm = {
          RAName: this.name,
          RAID: this.idno,
          RBankName: '',
          RBranchName: ''
        };
      }
    },
    saveEditForm() {
      this.$refs['editForm'].validate((valid) => {
        if (valid) {
          this.editForm.CustomerRId = this.id;
          this.editForm.IDNo = this.idno;
          const data = this.editForm;
          var url = 'RemittanceR/Insert';
          if (this.currentId !== '') {
            url = 'RemittanceR/Update';
          }
          saveRemittanceR(data, url).then((res) => {
            if (res.Success) {
              this.$message.success('成功!');
              this.dialogEditFormVisible = false;
              this.loadTableData();
              this.editForm = {
                RBankName: '',
                RBankNo: '',
                RBranchName: '',
                RBranchNo: ''
              };
            }
          });
        }
      });
    },
    handleSelectChange: function(selection, row) {
      this.currentSelected = selection;
    },
    handleSelectAllChange: function(selection) {
      this.currentSelected = selection;
    },
    loadTableData: function() {
      this.loading = true;
      GetRemittancesByCustomerRId(this.id).then((res) => {
        this.tableData = res.ResData;
        this.currentId = '';
        this.currentSelected = [];
        this.loading = false;
      });
    }
  }
};
</script>

<style></style>
