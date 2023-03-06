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
          class="el-button-modify"
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
        prop="LAName"
        label="帳戶名稱"
      />
      <el-table-column
        prop="LAID"
        label="身份證字號" width="150"
      />
      <el-table-column
        prop="LBankName"
        label="金融機構名稱" width="200"
      />
      <el-table-column
        prop="LBankNo"
        label="金融機構代碼" width="150"
      />
      <el-table-column
        prop="LBranchName"
        label="分行名稱" width="200"
      />
      <el-table-column
        prop="LBranchNo"
        label="分行代碼" width="150"
      />
      <el-table-column
        prop="LANo"
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
              prop="LAName"
            >
              <el-input v-model="editForm.LAName" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item
              label="身份證字號："
              :label-width="formLabelWidth"
              prop="LAID"
            >
              <el-input
                v-model="editForm.LAID"
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
              prop="LBankName"
            >
              <el-select
                v-model="editForm.LBankName"
                filterable class="w160px"
              >
                <el-option
                  v-for="item in bankNameOptions"
                  :key="item.Id"
                  :label="item.BankName"
                  :value="item.BankName"
                />
              </el-select>
              <!-- <el-input v-model="editForm.LBankName" /> -->
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item
              label="金融機構代碼："
              :label-width="formLabelWidth"
            >
              <el-input
                v-model="editForm.LBankNo"
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
              prop="LBranchName"
            >
              <el-select
                v-model="editForm.LBranchName"
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
              <!-- <el-input v-model="editForm.LBranchName" /> -->
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item
              label="分行代碼："
              :label-width="formLabelWidth"
            >
              <el-input
                v-model="editForm.LBranchNo"
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
              prop="LANo"
            >
              <el-input v-model="editForm.LANo" />
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
      <div
        slot="footer"
        class="dialog-footer"
      >
        <el-button
          size="small"
          icon="el-icon-close"
          @click="
            dialogEditFormVisible = false;
            reseteditForm();
          "
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
  saveRemittanceL,
  getRemittanceLDetail,
  GetRemittancesByCustomerLId
} from '@/api/chaochi/remittance/remittancel';
import { GetAllBanks, GetAllBranchs } from '@/api/chaochi/bankinfo/bankinfo';
import { validateIDCombo } from '@/utils/validate';
export default {
  name: 'LNRemittance',
  props: {
    id: { type: String, default: null },
    idno: { type: String, default: null },
    name: { type: String, default: null }
  },
  data() {
    return {
      testmsg: '',
      rule: {
        LAID: [
          { required: false, trigger: 'blur', validator: validateIDCombo }
        ],
        LAName: [
          { required: true, trigger: 'blur', message: '請輸入帳戶名稱' }
        ],
        LBankName: [
          { required: true, trigger: 'change', message: '請選擇金融機構名稱' }
        ],
        LBranchName: [
          { required: true, trigger: 'change', message: '請選擇分行名稱' }
        ],
        LANo: [{ required: true, trigger: 'blur', message: '請輸入帳戶號碼' }]
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
        LAName: this.name,
        LAID: this.idno,
        LBankName: '',
        LBranchName: '',
        LBranchNo: '',
        LBankNo: ''
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
    'editForm.LBankName': {
      handler(a, b) {
        if (a) {
          this.editForm.LBankNo = this.bankNameOptions.filter(
            (x) => x.BankName === a
          )[0].BankNo;
          this.branchNameOptions = this.allbranch.filter(
            (x) => x.BankNo === this.editForm.LBankNo
          );
          if (this.editForm.LBankName !== a || b !== '') {
            this.editForm.LBranchName = '';
            this.editForm.LBranchNo = '';
          }
        }
      }
    },
    'editForm.LBranchName': {
      handler(a) {
        if (a) {
          this.editForm.LBranchNo = this.allbranch.filter(
            (x) => x.BranchName === a && x.BankNo === this.editForm.LBankNo
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
    reseteditForm() {
      this.editForm = {
        LBankName: '',
        LBankNo: '',
        LBranchName: '',
        LBranchNo: ''
      };
    },
    cancel() {
      this.$emit('cancel');
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
          getRemittanceLDetail(this.currentId).then((res) => {
            this.editForm = res.ResData;
            this.loadingEditForm = false;
          });
        }
      } else {
        this.editFormTitle = '新增';
        this.currentId = '';
        this.dialogEditFormVisible = true;
        this.editForm = {
          LAName: this.name,
          LAID: this.idno,
          LBankName: '',
          LBranchName: '',
          LBranchNo: '',
          LBankNo: ''
        };
      }
    },
    saveEditForm() {
      this.$refs['editForm'].validate((valid) => {
        if (valid) {
          this.editForm.CustomerLId = this.id;
          this.editForm.IDNo = this.idno;
          const data = this.editForm;
          var url = 'RemittanceL/Insert';
          if (this.currentId !== '') {
            url = 'RemittanceL/Update';
          }
          saveRemittanceL(data, url).then((res) => {
            if (res.Success) {
              this.$message.success('成功!');
              this.dialogEditFormVisible = false;
              this.loadTableData();
              this.reseteditForm();
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
      GetRemittancesByCustomerLId(this.id).then((res) => {
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
