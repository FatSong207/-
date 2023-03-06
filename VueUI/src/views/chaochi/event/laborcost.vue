<template>
  <el-card
    v-loading="saveloading"
    class="box-card"
  >
    <div style="float: left;width: 70%">
      <h3>活動成本設定</h3>
      <div style="float: left;width: 48%;">
        <el-form ref="editForm">
          <el-row>
            <el-col :span="12">
              <el-form-item label="費用類別">
                <el-select
                  v-model="costType"
                  placeholder="請選擇"
                  clearable
                  style="width: 140px"
                >
                  <el-option
                    v-for="item in costTypeOptions"
                    :key="item.value"
                    :label="item.label"
                    :value="item.value"
                  />
                </el-select>
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="費用金額">
                <el-input
                  v-model="amount"
                  placeholder="請輸入金額"
                  autocomplete="off"
                  clearable
                  style="width: 120px"
                  class="no-number"
                  onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row>
            <div class="list-btn-container">
              <el-button-group>
                <el-button
                  type="success"
                  size="small"
                  icon="el-icon-plus"
                  @click="AddTempTableData()"
                >加 入</el-button>
                <el-button
                  type="danger"
                  size="small"
                  icon="el-icon-delete"
                  @click="RemoveTempTableData()"
                >刪 除</el-button>
              </el-button-group>
            </div>
          </el-row>
          <el-row>
            <el-col :span="24">
              <el-table
                ref="gridtable"
                :data="tableData"
                border
                stripe
                highlight-current-row
                style="width: 90%"
                @select="handleSelectChange"
                @select-all="handleSelectAllChange"
              >
                <el-table-column
                  type="selection"
                  width="40"
                />
                <el-table-column
                  prop="sort"
                  type="index"
                  label="序位"
                  width="50"
                  align="center"
                />
                <el-table-column
                  prop="Type"
                  label="費用類別"
                />
                <el-table-column
                  prop="Amount"
                  label="費用金額"
                  width="180"
                />
              </el-table>
            </el-col>
          </el-row>
        </el-form>
      </div>
      <div style="float: left;width: 48%;padding-left: 20px">
        <el-form ref="editForm">
          <el-row>
            <el-col :span="12">
              <el-form-item label="收入類別">
                <el-select
                  v-model="incomeType"
                  placeholder="請選擇"
                  clearable
                  style="width: 140px"
                >
                  <el-option
                    v-for="item in incomeTypeOptions"
                    :key="item.value"
                    :label="item.label"
                    :value="item.value"
                  />
                </el-select>
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="收入金額">
                <el-input
                  v-model="income"
                  placeholder="請輸入金額"
                  autocomplete="off"
                  clearable
                  style="width: 120px"
                  class="no-number"
                  onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row>
            <div class="list-btn-container">
              <el-button-group>
                <el-button
                  type="success"
                  size="small"
                  icon="el-icon-plus"
                  @click="AddTempTableDataIncome()"
                >加 入</el-button>
                <el-button
                  type="danger"
                  size="small"
                  icon="el-icon-delete"
                  @click="RemoveTempTableDataIncome()"
                >刪 除</el-button>
              </el-button-group>
            </div>
          </el-row>
          <el-row>
            <el-col :span="24">
              <el-table
                ref="gridtable"
                :data="tableDataincome"
                border
                stripe
                highlight-current-row
                style="width: 90%"
                @select="handleSelectChangeincome"
                @select-all="handleSelectAllChangeincome"
              >
                <el-table-column
                  type="selection"
                  width="40"
                />
                <el-table-column
                  prop="sort"
                  type="index"
                  label="序位"
                  width="50"
                  align="center"
                />
                <el-table-column
                  prop="Type"
                  label="收入類別"
                />
                <el-table-column
                  prop="Amount"
                  label="收入金額"
                  width="180"
                />
              </el-table>
            </el-col>
          </el-row>
        </el-form>
      </div>
    </div>
    <div style="float: left;width: 30%;padding-left: 20px">
      <h3>投入活動人員</h3>
      <div style="float: left;width: 100%">
        <!-- <h3>活動人力設定</h3> -->
        <el-form ref="editForm">
          <el-row>
            <el-col :span="24">
              <el-form-item label="部門">
                <el-select
                  v-model="dept"
                  placeholder="請選擇"
                  clearable
                  filterable
                  style="width: 330px"
                >
                  <el-option
                    v-for="item in deptOptions"
                    :key="item.Id"
                    :label="item.FullName"
                    :value="item.Id"
                  />
                </el-select>
              </el-form-item>
            </el-col>
          </el-row>
          <el-row>
            <el-col :span="24">
              <el-form-item label="人員">
                <el-select
                  v-model="personnel"
                  placeholder="請選擇"
                  multiple
                  style="width: 380px;"
                >
                  <el-option
                    v-for="item in personnelOptions"
                    :key="item.Id"
                    :label="item.RealName"
                    :value="item.Id"
                  />
                </el-select>

              </el-form-item>
            </el-col>
          </el-row>
        </el-form>
      </div>
      <div style="float: left;width: 100%;padding-bottom: 20px;">
        <el-card>
          <el-form ref="editForm">
            <el-row>
              <el-col :span="12">
                <el-form-item
                  label="合計費用"
                  label-width="85px"
                >
                  <el-input
                    v-model="totalCost"
                    placeholder="合計費用"
                    autocomplete="off"
                    style="width: 100px"
                    readonly
                  />
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item
                  label="人員數量"
                  label-width="85px"
                >
                  <el-input
                    v-model="totalPerson"
                    placeholder="人員數量"
                    autocomplete="off"
                    style="width: 80px"
                    readonly
                  />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="12">
                <el-form-item
                  label="合計收入"
                  label-width="85px"
                >
                  <el-input
                    v-model="totalIncome"
                    placeholder="合計收入"
                    autocomplete="off"
                    style="width: 100px"
                    readonly
                  />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="12">
                <hr>
                <el-form-item
                  label="收支小計"
                  label-width="85px"
                >
                  <el-input
                    v-model="subTotal"
                    placeholder="收支小計"
                    autocomplete="off"
                    style="width: 100px"
                    readonly
                  />
                </el-form-item>
              </el-col>
            </el-row>
          </el-form>
        </el-card>
        <div class="rightbtn">
          <el-button
            type="primary"
            size="small"
            icon="el-icon-paperclip"
            @click="save()"
          >儲存</el-button>
          <el-button size="small" icon="el-icon-close" @click="cancel">關閉</el-button>
        </div>
      </div>
    </div>
  </el-card>
</template>

<script>
import { GetAllOrgForSelect } from '@/api/security/organizeservice';
import { GetUserListByDept, getAllUserList } from '@/api/security/userservice';
import { UpdateCostPersonnel } from '@/api/chaochi/event/eventservice';
export default {
  name: 'LaborCost',
  props: {
    id: { type: String, default: null },
    eventcost: { type: Array, default: null },
    eventpersonnel: { type: Array, default: null },
    resetflag: { type: Boolean, default: false }
  },
  data() {
    return {
      saveloading: false,
      costType: '',
      incomeType: '',
      dept: '',
      personnel: [],
      amount: '',
      income: '',
      totalCost: 0,
      totalIncome: 0,
      subTotal: 0,
      costTypeOptions: [
        {
          value: '設計費用',
          label: '設計費用'
        },
        {
          value: '廣告行銷費用',
          label: '廣告行銷費用'
        },
        {
          value: '文宣品費用',
          label: '文宣品費用'
        },
        {
          value: '贈品費用',
          label: '贈品費用'
        },
        {
          value: '場地費用',
          label: '場地費用'
        },
        {
          value: '設備費用',
          label: '設備費用'
        },
        {
          value: '交通運費',
          label: '交通運費'
        },
        {
          value: '人事費用',
          label: '人事費用'
        },
        {
          value: '公關費用',
          label: '公關費用'
        },
        {
          value: '雜項費用',
          label: '雜項費用'
        }
      ],
      incomeTypeOptions: [
        {
          value: '門票費用',
          label: '門票費用'
        },
        {
          value: '廣告贊助',
          label: '廣告贊助'
        },
        {
          value: '其他收入',
          label: '其他收入'
        }
      ],
      deptOptions: [],
      personnelOptions: [],
      currentSelected: [],
      currentSelectedincome: [],
      tableData: [],
      tableDataincome: []
    };
  },
  computed: {
    totalPerson() {
      return this.personnel.length;
    }
  },
  watch: {
    eventcost: {
      handler() {
        this.Reload();
        this.InitDictItem();
      }
    },
    resetflag: {
      handler() {
        this.costType = '';
        this.incomeType = '';
        this.dept = '';
        this.personnel = [];
        this.amount = '';
        this.income = '';
        this.totalCost = 0;
        this.totalIncome = 0;
        this.subTotal = 0;
        this.tableData = [];
        this.tableDataincome = [];
      }
    },
    tableData: {
      handler(a, b) {
        var total = 0;
        a.forEach(x => {
          total += parseInt(x.Amount);
        });
        this.totalCost = parseInt(total);
      }
    },
    tableDataincome: {
      handler(a, b) {
        var total = 0;
        a.forEach(x => {
          total += parseInt(x.Amount);
        });
        this.totalIncome = parseInt(total);
      }
    },
    totalCost: {
      handler() {
        this.subTotal = this.totalIncome - this.totalCost;
      }
    },
    totalIncome: {
      handler() {
        this.subTotal = this.totalIncome - this.totalCost;
      }
    },
    dept: {
      handler(a) {
        GetUserListByDept(a).then(res => {
          this.personnelOptions = res.ResData;
        });
      }
    }
  },
  created() {
    this.InitDictItem();
  },
  methods: {
    InitDictItem() {
      GetAllOrgForSelect().then(res => {
        this.deptOptions = res.ResData;
        this.Reload();
      });
      getAllUserList().then(res => {
        this.personnelOptions = res.ResData;
      });
    },
    cancel() {
      this.$emit('cancel');
    },
    save() {
      this.saveloading = true;
      var updateObj = {};
      var list = [];
      var personlist = [];
      list = this.tableData.concat(this.tableDataincome);
      personlist = this.personnel;
      updateObj = {
        eventCosts: list,
        eventPersonnels: personlist,
        TotalCost: this.totalCost,
        TotalIncome: this.totalIncome,
        SubTotal: this.subTotal,
        TotalPerson: this.totalPerson
      };
      UpdateCostPersonnel(updateObj, this.id).then(res => {
        if (res.Success) {
          this.$message.success('儲存成功!');
        } else {
          this.$message.error('儲存失敗!');
        }
        this.saveloading = false;
      });
    },
    AddTempTableData() {
      if (this.amount === '') {
        this.$alert('請輸入金額', '提示');
      } else {
        var data = {
          Type: this.costType,
          Category: '成本',
          Amount: this.amount
        };
        this.tableData.push(data);
      }
    },

    RemoveTempTableData() {
      var currentAdds = [];
      this.currentSelected.forEach(element => {
        currentAdds.push(element);
      });
      this.tableData = this.tableData.filter(el => !currentAdds.includes(el));
      this.currentSelected = [];
    },
    AddTempTableDataIncome() {
      if (this.income === '') {
        this.$alert('請輸入金額', '提示');
      } else {
        var data = {
          Type: this.incomeType,
          Category: '收入',
          Amount: this.income
        };
        this.tableDataincome.push(data);
      }
    },

    RemoveTempTableDataIncome() {
      var currentAdds = [];
      this.currentSelectedincome.forEach(element => {
        currentAdds.push(element);
      });
      this.tableDataincome = this.tableDataincome.filter(
        el => !currentAdds.includes(el)
      );
      this.currentSelectedincome = [];
    },
    handleSelectChange: function(selection, row) {
      this.currentSelected = selection;
    },
    handleSelectAllChange: function(selection) {
      this.currentSelected = selection;
    },
    handleSelectChangeincome: function(selection, row) {
      this.currentSelectedincome = selection;
    },
    handleSelectAllChangeincome: function(selection) {
      this.currentSelectedincome = selection;
    },
    Reload: function() {
      if (this.eventcost !== null) {
        if (this.eventcost.filter(x => x.Category === '成本').length !== 0) {
          this.tableData = this.eventcost.filter(x => x.Category === '成本');
        }
        if (this.eventcost.filter(x => x.Category === '收入').length !== 0) {
          this.tableDataincome = this.eventcost.filter(
            x => x.Category === '收入'
          );
        }
      }
      if (this.eventpersonnel !== null) {
        this.personnel = this.eventpersonnel;
      }
    }
  }
};
</script>

<style>
.no-number::-webkit-outer-spin-button,
.no-number::-webkit-inner-spin-button {
  margin: 0;
  -webkit-appearance: none !important;
}

.no-number input[type="number"]::-webkit-outer-spin-button,
.no-number input[type="number"]::-webkit-inner-spin-button {
  margin: 0;
  -webkit-appearance: none !important;
}

/*在firefox下移除input[number]的上下箭头*/
.no-number {
  -moz-appearance: textfield;
}

.no-number input[type="number"] {
  -moz-appearance: textfield;
}
</style>
