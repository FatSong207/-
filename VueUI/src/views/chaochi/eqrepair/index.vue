<template>
  <div class="app-container">
    <div class="filter-container">
      <el-card>
        <el-form
          ref="searchform"
          :model="searchform"
          class="demo-form-inline"
          size="small"
          inline
        >
          <el-form-item label="流水編號：">
            <el-input
              v-model="searchform.Guid"
              clearable
              style="width: 160px"
            />
          </el-form-item>
          <el-form-item label="申請日期：">
            <el-date-picker
              v-model="searchform.ApplicationDate"
              value-format="yyyy-MM-dd"
              format="yyyy-MM-dd"
              clearable
              type="date"
              placeholder="選擇日期"
              style="width: 160px"
            />
          </el-form-item>
          <el-form-item label="結案日期：">
            <el-date-picker
              v-model="searchform.EndCaseDate"
              value-format="yyyy-MM-dd"
              format="yyyy-MM-dd"
              clearable
              type="date"
              placeholder="選擇日期"
              style="width: 160px"
            />
          </el-form-item>
          <el-form-item label="處理狀態：">
            <el-select
              v-model="searchform.State"
              placeholder="請選擇"
              clearable
              style="width: 100px"
            >
              <el-option
                v-for="item in StateOptions"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              />
            </el-select>
            <!-- <el-input
                  v-model="searchform.State"
                  clearable
                  style="width: 160px"
                /> -->
          </el-form-item>
          <el-row>
            <el-col :span="8">
              <el-form-item
                label="建物地址："
                prop="BAdd"
              >
                <el-input
                  v-model="searchform.BAdd"
                  autocomplete="off"
                  style="width: 250px"
                />
              </el-form-item>
            </el-col>
          </el-row>
          <el-form-item>
            <el-button
              type="primary"
              size="small"
              icon="el-icon-search"
              @click="handleSearch()"
            >查詢</el-button>
          </el-form-item>
        </el-form>
      </el-card>
    </div>
    <el-card>
      <div class="list-btn-container">
        <el-button-group>
          <el-button
            v-hasPermi="['EqRepair/Edit']"
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
          >刷新</el-button>
        </el-button-group>
      </div>
      <el-table
        ref="gridtable1"
        v-loading="tableloading"
        :data="tableData"
        border
        stripe
        highlight-current-row
        style="width: 100%"
        :default-sort="{ prop: 'SortCode', order: 'ascending' }"
        @select="handleSelectChange"
        @select-all="handleSelectAllChange"
        @sort-change="handleSortChange"
        @row-dblclick="rowdblclick"
      >
        <el-table-column
          type="selection"
          width="30"
        />
        <el-table-column
          prop="Guid"
          label="流水編號"
          sortable="custom"
          width="180"
        />
        <el-table-column
          prop="BAdd"
          label="建物地址" sortable="custom"
        />
        <el-table-column
          prop="ReporterCell"
          label="連絡電話"
          sortable="custom"
          width="120"
        />
        <el-table-column
          prop="ApplicationDate"
          label="申請日期"
          sortable="custom"
          width="160"
        />
        <el-table-column
          prop="State"
          label="處理狀態"
          sortable="custom"
          width="120"
        />
        <el-table-column
          prop="EndCaseDate"
          label="結案日期"
          sortable="custom"
          width="180"
        />
      </el-table>
      <div class="pagination-container">
        <el-pagination
          background
          :current-page="pagination.currentPage"
          :page-sizes="[5, 10, 20, 50, 100, 200, 300, 400]"
          :page-size="pagination.pagesize"
          layout="total, sizes, prev, pager, next, jumper"
          :total="pagination.pageTotal"
          @size-change="handleSizeChange"
          @current-change="handleCurrentChange"
        />
      </div>
    </el-card>
    <el-dialog
      ref="dialogEditForm"
      v-el-drag-dialog
      :title="editFormTitle"
      :visible.sync="dialogEditFormVisible"
      fullscreen
      append-to-body
    >
      <el-card class="box-card">
        <el-form
          ref="editForm"
          :model="editForm" :rules="rules"
        >
          <el-row>
            <el-col :span="6">
              <el-form-item
                label="甲方姓名："
                :label-width="formLabelWidth"
                prop="LSName"
              >
                <el-input
                  v-model="editForm.LSName"
                  autocomplete="off"
                  readonly
                />
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item
                label="甲方電話："
                :label-width="formLabelWidth"
                prop="LSTel"
              >
                <el-input
                  v-model="editForm.LSTel"
                  autocomplete="off"
                  readonly
                />
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item
                label="報修人姓名："
                :label-width="formLabelWidth"
                prop="ReporterName"
              >
                <el-input
                  v-model="editForm.ReporterName"
                  autocomplete="off"
                  readonly
                />
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item
                label="報修人電話："
                :label-width="formLabelWidth"
                prop="ReporterCell"
              >
                <el-input
                  v-model="editForm.ReporterCell"
                  autocomplete="off"
                  readonly
                />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row>
            <el-col :span="6">
              <el-form-item
                label="流水編號："
                :label-width="formLabelWidth"
                prop="BAdd"
              >
                <el-input
                  v-model="editForm.Guid"
                  autocomplete="off" readonly
                />
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item
                label="建物地址："
                :label-width="formLabelWidth"
                prop="BAdd"
              >
                <el-input
                  v-model="editForm.BAdd"
                  autocomplete="off" readonly
                />
              </el-form-item>
            </el-col>
          </el-row>
        </el-form>
      </el-card>
      <h3>以下是此物件申請的修繕申請項目</h3>
      <div class="list-btn-container">
        <el-button-group>
          <el-button
            v-hasPermi="['EqRepair/Edit']"
            type="primary"
            icon="el-icon-upload2"
            class="el-button-modify"
            size="small"
            @click="ShowImageUploadDialog()"
          >上傳與檢視照片</el-button>
        </el-button-group>
      </div>
      <el-table
        ref="gridtable"
        v-loading="DetailTableloading"
        :data="editForm.eqRepairDetails"
        border
        stripe
        highlight-current-row
        style="width: 100%"
        :default-sort="{ prop: 'SortCode', order: 'ascending' }"
        @select="handleSelectChangeDetail"
        @select-all="handleSelectAllChangeDetail"
        @row-dblclick="rowdblclickDetail"
      >
        <el-table-column
          type="selection"
          width="40"
        />
        <el-table-column
          prop="EqName"
          label="設備名稱" width="180"
        />
        <el-table-column
          prop="EqBrand"
          label="設備規格" width="120"
        />
        <el-table-column
          prop="RepairReason"
          label="修繕原因" width="180"
        />
        <el-table-column
          prop="HandleNote"
          label="處理備註"
        >
          <template slot-scope="scope">
            <el-input v-model="scope.row.HandleNote" />
          </template>
        </el-table-column>
        <el-table-column
          prop="CurrentState"
          label="當前狀態"
          sortable="custom"
          width="120"
        >
          <template slot-scope="scope">
            <el-select
              v-model="scope.row.CurrentState"
              placeholder="請選擇"
            >
              <el-option
                v-for="item in CurrentStateOptions"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              />
            </el-select>
          </template>
        </el-table-column>
        <el-table-column
          prop="Cost"
          label="成本金額" width="100"
        >
          <template slot-scope="scope">
            <el-input
              v-model="scope.row.Cost"
              type="number"
              class="no-number"
              :min="0"
              show-word-limit
              onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
            />
          </template>
        </el-table-column>
        <el-table-column
          prop="Quote"
          label="報價金額" width="100"
        >
          <template slot-scope="scope">
            <el-input
              v-model="scope.row.Quote"
              type="number"
              class="no-number"
              :min="0"
              show-word-limit
              onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
            />
          </template>
        </el-table-column>
        <el-table-column
          prop="RepairCompany"
          label="修繕廠商" width="160"
        >
          <template slot-scope="scope">
            <el-input v-model="scope.row.RepairCompany" />
          </template>
        </el-table-column>
        <el-table-column
          prop="PhotoCount"
          label="照片數量" width="100"
        />
      </el-table>

      <div
        slot="footer"
        class="dialog-footer"
      >
        <el-button
          size="small" icon="el-icon-close" @click="cancel"
        >關閉</el-button>
        <el-button
          type="primary"
          size="small"
          icon="el-icon-paperclip"
          @click="saveEditForm()"
        >儲存</el-button>
      </div>
    </el-dialog>
    <el-dialog
      ref="dialogEditForm"
      v-el-drag-dialog
      title="上傳與檢視"
      :visible.sync="imageUploadDialogVisible"
      append-to-body
      width="65%"
    >
      <imageUpload
        :id="currentIdDetail"
        :guid="currentId"
        :emitflag="emitFlag"
        :emitresetflag="emitResetFlag"
      />
      <div
        slot="footer"
        class="dialog-footer"
      >
        <el-button
          type="primary"
          size="small"
          icon="el-icon-video-play"
          @click="resetImageUpload()"
        >確認</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import {
  getEqRepairListWithPager,
  getEqRepairDetail,
  saveEqRepair
} from '@/api/chaochi/eqrepair/eqrepair';

import elDragDialog from '@/directive/el-drag-dialog'; // 彈窗可移動
import imageUpload from '@/views/chaochi/eqrepair/imageupload.vue';
export default {
  name: 'EqRepair', // 需與菜單的功能編碼一致
  directives: { elDragDialog },
  components: { imageUpload },
  data() {
    return {
      searchform: {
        Guid: '',
        State: '',
        ApplicationDate: '',
        EndCaseDate: '',
        BAdd: ''
      },
      tableData: [],
      tableloading: true,
      DetailTableloading: false,
      pagination: {
        currentPage: 1,
        pagesize: 20,
        pageTotal: 0
      },
      sortableData: {
        order: 'desc',
        sort: 'ApplicationDate'
      },
      dialogEditFormVisible: false,
      imageUploadDialogVisible: false,
      editFormTitle: '',
      editForm: {
        eqRepairDetails: []
      },
      rules: {},
      formLabelWidth: '135px',
      currentId: '', // 當前操作對象的ID值，主要用于修改
      currentSelected: [],
      currentIdDetail: '',
      currentSelectedDetail: [],
      CurrentStateOptions: [
        {
          value: '未處理',
          label: '未處理'
        },
        {
          value: '已派工',
          label: '已派工'
        },
        {
          value: '已完成',
          label: '已完成'
        }
      ],
      StateOptions: [
        {
          value: '申請中',
          label: '申請中'
        },
        {
          value: '派工中',
          label: '派工中'
        },
        {
          value: '已結案',
          label: '已結案'
        }
      ],
      emitFlag: false,
      emitResetFlag: false
    };
  },
  // computed: {
  //   photoCount(){
  //     var i = 0;
  //     if(){

  //     }
  //   }
  // },
  created() {
    this.pagination.currentPage = 1;
    this.InitDictItem();
    this.loadTableData();
  },
  methods: {
    /**
     * 初始化數據
     */
    InitDictItem() {},
    // 取消按鈕
    cancel() {
      this.dialogEditFormVisible = false;
      this.reset();
    },
    resetImageUpload() {
      this.imageUploadDialogVisible = false;
      this.bindEditInfo();
      this.emitResetFlag = !this.emitResetFlag;
    },
    rowdblclick(row, column, event) {
      this.$refs.gridtable1.clearSelection();
      this.currentSelected = '';
      // this.rerenderGridtable1 = Date.now(); // 強制重繪 https://bbchin.com/archives/el-table-rerender
      this.$nextTick(function() {
        // https://ithelp.ithome.com.tw/articles/10240669
        this.$refs.gridtable1.toggleRowSelection(row, true);
        this.currentSelected = this.$refs.gridtable1.selection;
        this.ShowEditOrViewDialog('edit');
      });
    },
    rowdblclickDetail(row, column, event) {
      this.$refs.gridtable.clearSelection();
      this.currentSelectedDetail = '';
      // this.rerenderGridtable = Date.now(); // 強制重繪 https://bbchin.com/archives/el-table-rerender
      this.$nextTick(function() {
        // https://ithelp.ithome.com.tw/articles/10240669
        this.$refs.gridtable.toggleRowSelection(row, true);
        this.currentSelectedDetail = this.$refs.gridtable.selection;
        this.ShowImageUploadDialog();
      });
    },
    // 表單重置
    reset() {
      this.editForm = {
        Guid: '',
        CustomerID: '',
        BAdd: '',
        ReporterName: '',
        ReporterCell: '',
        RepairDateTime: '',
        EqName: '',
        RepairReason: '',
        State: '',
        ApplicationDate: '',
        EndCaseDate: '',
        HandleNote: ''

        // 需個性化處理內容
      };
      this.resetForm('editForm');
    },
    /**
     * 加載頁面table數據
     */
    loadTableData: function() {
      this.tableloading = true;
      var filter = this.searchform;
      var seachdata = {
        CurrenetPageIndex: this.pagination.currentPage,
        PageSize: this.pagination.pagesize,
        Keywords: this.searchform.keywords,
        Filter: filter,
        Order: this.sortableData.order,
        Sort: this.sortableData.sort
      };
      getEqRepairListWithPager(seachdata).then((res) => {
        this.tableData = res.ResData.Items;
        this.pagination.pageTotal = res.ResData.TotalItems;
        this.tableloading = false;
      });
    },
    /**
     * 點擊查詢
     */
    handleSearch: function() {
      this.pagination.currentPage = 1;
      this.loadTableData();
    },

    /**
     * 新增、修改或查看明細信息（綁定顯示數據）     *
     */
    ShowEditOrViewDialog: function(view) {
      this.reset();
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
          this.DetailTableloading = true;
          this.bindEditInfo();
        }
      } else {
        this.editFormTitle = '新增';
        this.currentId = '';
        this.dialogEditFormVisible = true;
      }
    },
    ShowImageUploadDialog() {
      if (
        this.currentSelectedDetail.length > 1 ||
        this.currentSelectedDetail.length === 0
      ) {
        this.$alert('請選擇一條數據進行編輯/修改', '提示');
      } else {
        this.emitFlag = !this.emitFlag;
        this.currentIdDetail = this.currentSelectedDetail[0].Id;
        this.imageUploadDialogVisible = true;
      }
    },
    bindEditInfo: function() {
      getEqRepairDetail(this.currentId)
        .then((res) => {
          this.editForm = res.ResData;
          // 需個性化處理內容
          this.DetailTableloading = false;
        })
        .finally(() => {
          this.DetailTableloading = false;
        });
    },
    /**
     * 新增/修改保存
     */
    saveEditForm() {
      this.$refs['editForm'].validate((valid) => {
        if (valid) {
          // const data = {
          //   Guid: this.editForm.Guid,
          //   CustomerID: this.editForm.CustomerID,
          //   BAdd: this.editForm.BAdd,
          //   ReporterName: this.editForm.ReporterName,
          //   ReporterCell: this.editForm.ReporterCell,
          //   RepairDateTime: this.editForm.RepairDateTime,
          //   EqName: this.editForm.EqName,
          //   RepairReason: this.editForm.RepairReason,
          //   State: this.editForm.State,
          //   ApplicationDate: this.editForm.ApplicationDate,
          //   EndCaseDate: this.editForm.EndCaseDate,
          //   HandleNote: this.editForm.HandleNote,

          //   Id: this.currentId
          // };
          var url = 'EqRepair/Insert';
          if (this.currentId !== '') {
            url = 'EqRepair/Update';
          }
          saveEqRepair(this.editForm, url).then((res) => {
            if (res.Success) {
              this.$message({
                message: '恭喜你，操作成功',
                type: 'success'
              });
              this.dialogEditFormVisible = false;
              this.currentSelected = '';
              this.loadTableData();
              this.InitDictItem();
            } else {
              this.$message({
                message: res.ErrMsg,
                type: 'error'
              });
            }
          });
        } else {
          return false;
        }
      });
    },
    /**
     * 當表格的排序條件發生變化的時候會觸發該事件
     */
    handleSortChange: function(column) {
      this.sortableData.sort = column.prop;
      if (column.order === 'ascending') {
        this.sortableData.order = 'asc';
      } else {
        this.sortableData.order = 'desc';
      }
      this.loadTableData();
    },
    /**
     * 當用戶手動勾選checkbox數據行事件
     */
    handleSelectChange: function(selection, row) {
      this.currentSelected = selection;
    },
    /**
     * 當用戶手動勾選全選checkbox事件
     */
    handleSelectAllChange: function(selection) {
      this.currentSelected = selection;
    },
    /**
     * 當用戶手動勾選checkbox數據行事件(明細頁內)
     */
    handleSelectChangeDetail: function(selection, row) {
      this.currentSelectedDetail = selection;
    },
    /**
     * 當用戶手動勾選全選checkbox事件(明細頁內)
     */
    handleSelectAllChangeDetail: function(selection) {
      this.currentSelectedDetail = selection;
    },
    /**
     * 選擇每頁顯示數量
     */
    handleSizeChange(val) {
      this.pagination.pagesize = val;
      this.pagination.currentPage = 1;
      this.loadTableData();
    },
    /**
     * 選擇當頁面
     */
    handleCurrentChange(val) {
      this.pagination.currentPage = val;
      this.loadTableData();
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

.no-number input[type='number']::-webkit-outer-spin-button,
.no-number input[type='number']::-webkit-inner-spin-button {
  margin: 0;
  -webkit-appearance: none !important;
}

/*在firefox下移除input[number]的上下箭头*/
.no-number {
  -moz-appearance: textfield;
}

.no-number input[type='number'] {
  -moz-appearance: textfield;
}
</style>
