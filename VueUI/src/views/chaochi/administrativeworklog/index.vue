<template>
  <div class="app-container">
    <div class="filter-container">
      <el-card>
        <el-form
          ref="searchform"
          :inline="true"
          :model="searchform"
          class="demo-form-inline"
          size="small"
        >
          <el-row>
            <el-form-item label="所屬部門：">
              <!-- <el-cascader
                v-model="searchform.BelongDept"
                :options="selectOrganize"
                filterable
                :props="{label: 'FullName',value: 'Id',children: 'Children',emitPath: false,checkStrictly: true,expandTrigger: 'hover',}"
                clearable
                style="width: 700px"
              /> -->
              <el-select
                v-model="searchform.BelongDept"
                filterable clearable
              >
                <el-option
                  v-for="item in selectOrganize"
                  :key="item.id"
                  :label="item.name"
                  :value="item.id"
                />
              </el-select>
            </el-form-item>
          </el-row>
          <el-form-item label="填表人姓名：">
            <el-select
              v-model="searchform.UserName"
              filterable clearable
            >
              <el-option
                v-for="item in selectUsers"
                :key="item.Id"
                :label="item.RealName"
                :value="item.RealName"
              />
            </el-select>
            <!-- <el-input
              v-model="searchform.UserName"
              clearable
              placeholder="名稱"
            /> -->
          </el-form-item>
          <el-form-item label="填表人帳號：">
            <el-input
              v-model="searchform.UserAccount"
              disabled
            />
          </el-form-item>
          <el-form-item label="日誌日期：">
            <el-date-picker
              v-model="dateRange"
              type="daterange"
              range-separator="至"
              start-placeholde="起始日期"
              end-placeholde="結束日期"
              value-format="yyyy-MM-dd"
            />
            <!-- <el-date-picker
              v-model="searchform.LogDate"
              type="date"
              value-format="yyyy-MM-dd"
              placeholder="選擇日期"
            /> -->
          </el-form-item>
          <el-form-item>
            <el-button
              type="primary"
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
            v-hasPermi="['AdministrativeWorkLog/Add']"
            type="primary"
            icon="el-icon-plus"
            size="small"
            @click="ShowEditOrViewDialog()"
          >新增</el-button>
          <el-button
            v-hasPermi="['AdministrativeWorkLog/Edit']"
            type="primary"
            icon="el-icon-view"
            class="el-button-modify"
            size="small"
            @click="ShowViewDialog()"
          >檢視</el-button>
          <el-button
            type="default"
            icon="el-icon-refresh"
            size="small"
            @click="loadTableData()"
          >刷新</el-button>
        </el-button-group>
      </div>
      <el-table
        ref="gridtable"
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
      >
        <el-table-column
          type="selection"
          width="40"
        />
        <el-table-column
          prop="BelongDeptName"
          label="隸屬部門"
          sortable="custom"
          width="220"
        />
        <el-table-column
          prop="UserName"
          label="填表人姓名"
          sortable="custom"
          width="120"
        />
        <!-- <el-table-column
          prop="UserAccount"
          label="填表人帳號"
          sortable="custom"
          width="160"
        /> -->
        <el-table-column
          prop="LogDate"
          label="日誌日期"
          sortable="custom"
          width="200"
        />
        <el-table-column
          prop="AuditSupervisorName"
          label="審核主管"
          sortable="custom"
          width="120"
        />
        <el-table-column
          prop="AuditTime"
          label="審核日期"
          sortable="custom"
          width="200"
        />
        <el-table-column
          prop="SupervisorNote"
          label="主管備註"
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
      <el-form
        ref="editForm"
        :model="editForm" :rules="rules" inline
      >
        <el-row>
          <el-col :span="6">
            <el-form-item
              label="填表人姓名："
              :label-width="formLabelWidth"
              prop="UserName"
              style="width: 100%"
            >
              <el-input
                v-model="currentUser.RealName"
                disabled
              />
            </el-form-item>
          </el-col>
          <el-col :span="6">
            <el-form-item
              label="填表人帳號："
              :label-width="formLabelWidth"
              prop="UserAccount"
            >
              <el-input
                v-model="currentUser.Account"
                disabled
              />
            </el-form-item>
          </el-col>
          <el-col :span="6">
            <el-form-item
              label="隸屬部門："
              :label-width="formLabelWidth"
              prop="BelongDept"
            >
              <el-input
                v-model="currentUser.DepartmentId"
                disabled
              />
            </el-form-item>
          </el-col>
          <el-col :span="6">
            <el-form-item
              label="日誌日期："
              :label-width="formLabelWidth"
              prop="CreatorTime"
            >
              <el-input
                v-model="readonlyLogDate"
                disabled
              />
            </el-form-item>
          </el-col>
        </el-row>
        <h3>行程回報</h3>
        <el-card>
          <el-form-item
            label="本日重點工作事項"
            prop="SupervisorNote"
          >
            <el-input
              v-model="report.important"
              placeholder="請輸入本日重點工作事項"
              autocomplete="off"
              clearable
            />
          </el-form-item>
          <el-form-item
            label="完成度"
            prop="SupervisorNote"
          >
            <el-select v-model="report.finishState">
              <el-option
                key="未開始"
                value="未開始"
              >未開始</el-option>
              <el-option
                key="處理中"
                value="處理中"
              >處理中</el-option>
              <el-option
                key="已完成"
                value="已完成"
              >已完成</el-option>
            </el-select>
            <!-- <el-input
              v-model="report.finishState"
              placeholder="完成度"
              autocomplete="off"
              clearable
            /> -->
          </el-form-item>
          <el-form-item
            label="備註"
            prop="SupervisorNote"
          >
            <el-input
              v-model="report.note"
              placeholder="請輸入備註"
              autocomplete="off"
              clearable
              style="width: 650px"
            />
          </el-form-item>
          <div class="list-btn-container">
            <el-button-group>
              <el-button
                type="primary"
                size="small"
                icon="el-icon-plus"
                @click="AdddetailTableData()"
              >增加</el-button>
              <el-button
                type="danger"
                icon="el-icon-edit"
                class="el-button-modify"
                size="small"
                @click="RemovedetailTableData()"
              >刪除</el-button>
            </el-button-group>
          </div>
          <el-row>
            <el-col :span="24">
              <el-table
                ref="detailTable"
                :data="detailTableData"
                border
                style="width: 70%"
                highlight-current-row
                @select="handleSelectChangeAdd"
                @select-all="handleSelectAllChangeAdd"
              >
                <el-table-column
                  type="selection"
                  width="40"
                />
                <el-table-column
                  prop="Important"
                  label="本日重點工作事項"
                  sortable="custom"
                  width="240"
                />
                <el-table-column
                  prop="FinishState"
                  label="完成度"
                  sortable="custom"
                  width="120"
                />
                <el-table-column
                  prop="Note"
                  label="備註" sortable="custom"
                />
              </el-table>
            </el-col>
          </el-row>
          <br>
          <el-form-item
            label="待辦工作事項："
            label-width="130px"
          >
            <el-input
              v-model="editForm.TodoNote"
              type="textarea"
              :rows="5"
              style="width: 400px"
              placeholder="請輸入內容"
            />
          </el-form-item>
          <el-form-item
            label="其他事項："
            label-width="130px"
          >
            <el-input
              v-model="editForm.OtherNote"
              type="textarea"
              :rows="5"
              style="width: 400px"
              placeholder="請輸入內容"
            />
          </el-form-item>
          <br>
          <!-- <el-form-item
            label="主管備註："
            label-width="130px"
          >
            <el-input
              v-model="textarea"
              type="textarea"
              :rows="5"
              style="width:800px"
              placeholder="請輸入內容"
            />
          </el-form-item> -->
        </el-card>
      </el-form>
      <div
        slot="footer"
        class="dialog-footer"
      >
        <el-button
          type="primary"
          size="small"
          icon="el-icon-paperclip"
          @click="saveEditForm()"
        >儲存</el-button>
        <el-button
          type="primary"
          size="small"
          icon="el-icon-s-promotion"
          @click="saveEditForm('sendtrail')"
        >送審</el-button>
        <el-button
          size="small" icon="el-icon-close" @click="cancel"
        >關閉</el-button>
      </div>
    </el-dialog>
    <el-dialog
      v-el-drag-dialog
      title="檢視"
      :visible.sync="dialogViewVisible"
      fullscreen
      append-to-body
    >
      <el-form
        :model="editForm"
        inline
      >
        <el-row>
          <el-col :span="6">
            <el-form-item
              label="填表人姓名："
              :label-width="formLabelWidth"
              prop="UserName"
              style="width: 100%"
            >
              <el-input
                v-model="editForm.UserName"
                disabled
              />
            </el-form-item>
          </el-col>
          <el-col :span="6">
            <el-form-item
              label="填表人帳號："
              :label-width="formLabelWidth"
              prop="UserAccount"
            >
              <el-input
                v-model="editForm.UserAccount"
                disabled
              />
            </el-form-item>
          </el-col>
          <el-col :span="6">
            <el-form-item
              label="隸屬部門："
              :label-width="formLabelWidth"
              prop="BelongDept"
            >
              <el-input
                v-model="editForm.BelongDept"
                disabled
              />
            </el-form-item>
          </el-col>
          <el-col :span="6">
            <el-form-item
              label="日誌日期："
              :label-width="formLabelWidth"
              prop="CreatorTime"
            >
              <el-input
                v-model="editForm.LogDate"
                disabled
              />
            </el-form-item>
          </el-col>
        </el-row>
        <h3>行程回報</h3>
        <el-card
          v-loading="loading"
          v-loading.lock="loading"
          element-loading-text="載入中..."
        >
          <el-form-item
            label="本日重點工作事項"
            prop="SupervisorNote"
          >
            <el-input
              v-model="report.important"
              placeholder="請輸入本日重點工作事項"
              autocomplete="off"
              clearable
            />
          </el-form-item>
          <el-form-item
            label="完成度"
            prop="SupervisorNote"
          >
            <el-select v-model="report.finishState">
              <el-option
                key="未開始"
                value="未開始"
              >未開始</el-option>
              <el-option
                key="處理中"
                value="處理中"
              >處理中</el-option>
              <el-option
                key="已完成"
                value="已完成"
              >已完成</el-option>
            </el-select>
            <!-- <el-input
              v-model="report.finishState"
              placeholder="完成度"
              autocomplete="off"
              clearable
            /> -->
          </el-form-item>
          <el-form-item
            label="備註"
            prop="SupervisorNote"
          >
            <el-input
              v-model="report.note"
              placeholder="請輸入備註"
              autocomplete="off"
              clearable
              style="width: 650px"
            />
          </el-form-item>
          <div class="list-btn-container">
            <el-button-group
              v-if="
                editForm.States === 'first' &&
                  editForm.CreatorUserId === currentUser.Id
              "
            >
              <el-button
                type="primary"
                size="small"
                icon="el-icon-plus"
                @click="AdddetailTableData()"
              >增加</el-button>
              <el-button
                type="danger"
                icon="el-icon-edit"
                class="el-button-modify"
                size="small"
                @click="RemovedetailTableData()"
              >刪除</el-button>
            </el-button-group>
          </div>
          <el-row>
            <el-col :span="24">
              <el-table
                ref="detailTable"
                :data="detailTableData"
                border
                style="width: 70%"
                highlight-current-row
                @select="handleSelectChangeAdd"
                @select-all="handleSelectAllChangeAdd"
              >
                <el-table-column
                  type="selection"
                  width="40"
                />
                <el-table-column
                  prop="Important"
                  label="本日重點工作事項"
                  sortable="custom"
                  width="240"
                />
                <el-table-column
                  prop="FinishState"
                  label="完成度"
                  sortable="custom"
                  width="120"
                />
                <el-table-column
                  prop="Note"
                  label="備註" sortable="custom"
                />
              </el-table>
            </el-col>
          </el-row>
          <br>
          <el-form-item
            label="待辦工作事項："
            label-width="130px"
          >
            <el-input
              v-model="editForm.TodoNote"
              type="textarea"
              :rows="5"
              style="width: 400px"
              placeholder="請輸入內容"
              :disabled="
                !(
                  editForm.States === 'first' &&
                  editForm.CreatorUserId === currentUser.Id
                )
              "
            />
          </el-form-item>
          <el-form-item
            label="其他事項："
            label-width="130px"
          >
            <el-input
              v-model="editForm.OtherNote"
              type="textarea"
              :rows="5"
              style="width: 400px"
              placeholder="請輸入內容"
              :disabled="
                !(
                  editForm.States === 'first' &&
                  editForm.CreatorUserId === currentUser.Id
                )
              "
            />
          </el-form-item>
          <br>
          <el-form-item
            label="主管備註："
            label-width="130px"
          >
            <el-input
              v-model="editForm.SupervisorNote"
              type="textarea"
              :rows="5"
              style="width: 800px"
              :disabled="!IsShowApprovalBtn"
            />
          </el-form-item>
        </el-card>
      </el-form>
      <div
        slot="footer"
        class="dialog-footer"
      >
        <el-button
          v-if="
            editForm.States === 'first' &&
              editForm.CreatorUserId === currentUser.Id
          "
          type="primary"
          size="small"
          icon="el-icon-paperclip"
          @click="saveEditForm()"
        >儲存</el-button>
        <el-button
          v-if="
            editForm.States === 'first' &&
              editForm.CreatorUserId === currentUser.Id
          "
          type="primary"
          size="small"
          icon="el-icon-s-promotion"
          @click="saveEditForm('sendtrail')"
        >送審</el-button>
        <el-button
          v-if="IsShowApprovalBtn"
          type="primary"
          size="small"
          icon="el-icon-close"
          @click="saveEditForm('approval')"
        >放行</el-button>
        <el-button
          size="small" icon="el-icon-close" @click="cancel"
        >關閉</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import {
  getAdministrativeWorkLogListWithPager,
  getAdministrativeWorkLogDetail,
  saveAdministrativeWorkLog,
  deleteAdministrativeWorkLog
} from '@/api/chaochi/administrativeworklog/administrativeworklog';
import { getUserDetail, GetUserListByDept } from '@/api/security/userservice';
import { GetPermissionOrganizeSelect } from '@/api/security/organizeservice';
import { parseTime } from '@/utils/index';

import elDragDialog from '@/directive/el-drag-dialog'; // 彈窗可移動
import { mapGetters } from 'vuex';

export default {
  name: 'AdministrativeWorkLog', // 需與菜單的功能編碼一致
  directives: { elDragDialog },
  data() {
    return {
      searchform: {},
      editForm: {},
      detailEditForm: {},
      rules: {},
      currentUser: {},
      report: {
        important: '',
        finishState: '',
        note: ''
      },
      pagination: {
        currentPage: 1,
        pagesize: 20,
        pageTotal: 0
      },
      sortableData: {
        order: 'desc',
        sort: 'CreatorTime'
      },
      tableData: [],
      currentSelected: [],
      currentSelectedAdd: [],
      detailTableData: [],
      selectOrganize: [],
      selectUsers: [],
      dateRange: [],
      selectedOrganizeOptions: '',
      editFormTitle: '',
      formLabelWidth: '135px',
      currentId: '',
      cascaderKey: 0,
      tableloading: true,
      loading: false,
      dialogEditFormVisible: false,
      dialogViewVisible: false
    };
  },
  computed: {
    ...mapGetters(['userId']),
    readonlyLogDate() {
      return parseTime(new Date(), '{y}-{m}-{d} {h}:{i}:{s}');
    },
    IsShowApprovalBtn() {
      return (
        this.currentUser.Id === this.editForm.AuditSupervisor &&
        this.editForm.States !== 'approval'
      );
    }
  },
  watch: {
    'searchform.BelongDept': {
      handler() {
        GetUserListByDept(this.searchform.BelongDept).then((res) => {
          this.selectUsers = res.ResData;
        });
      }
    },
    'searchform.UserName': {
      handler() {
        this.searchform.UserAccount = this.selectUsers.filter(
          (u) => u.RealName === this.searchform.UserName
        )[0].Account;
        // console.log(
        //   this.selectUsers.filter(
        //     (u) => u.RealName === this.searchform.UserName
        //   )[0].Account
        // );
      }
    }
  },
  created() {
    this.pagination.currentPage = 1;
    this.InitDictItem();
    getUserDetail(this.userId).then((res) => {
      if (res.Success) {
        this.currentUser = res.ResData;
      }
    });
    this.loadTableData();
  },
  methods: {
    /**
     * 初始化數據
     */
    InitDictItem() {
      GetPermissionOrganizeSelect().then((res) => {
        this.selectOrganize = res.ResData;
      });
      this.detailTableData = [];
      this.editForm.TodoNote = '';
      this.editForm.OtherNote = '';
    },

    // 取消按鈕
    cancel() {
      this.dialogEditFormVisible = false;
      this.dialogViewVisible = false;
      this.reset();
    },
    // 表單重置
    reset() {
      this.editForm = {
        UserName: '',
        UserAccount: '',
        LogDate: '',
        AuditSupervisor: '',
        AuditTime: '',
        SupervisorNote: '',
        BelongDept: '',
        CreatorTime: '',
        CreatorUserId: '',
        LastModifyTime: '',
        LastModifyUserId: '',
        TodoNote: '',
        OtherNote: ''

        // 需個性化處理內容
      };
      this.detailTableData = [];

      this.resetForm('editForm');
    },
    /**
     * 加載頁面table數據
     */
    loadTableData: function() {
      var logDateRange = '';
      if (this.dateRange.length < 1) {
        logDateRange = '';
      } else {
        logDateRange = this.dateRange[0] + ',' + this.dateRange[1];
      }
      this.tableloading = true;
      var filter = this.searchform;
      filter.LogDate = logDateRange;
      var seachdata = {
        CurrenetPageIndex: this.pagination.currentPage,
        PageSize: this.pagination.pagesize,
        Filter: filter,
        Keywords: this.searchform.keywords,
        Order: this.sortableData.order,
        Sort: this.sortableData.sort
      };
      getAdministrativeWorkLogListWithPager(seachdata).then((res) => {
        this.tableData = res.ResData.Items;
        this.pagination.pageTotal = res.ResData.TotalItems;
        this.tableloading = false;
      });
    },
    AdddetailTableData() {
      if (!this.report.important) {
        this.$message.error('請輸入重點工作事項');
        return;
      }
      if (!this.report.finishState) {
        this.$message.error('請選擇完成度');
        return;
      }
      // if (!this.report.note) {
      //   this.$message.error('請輸入備註');
      //   return;
      // }
      var data = {
        Important: this.report.important,
        FinishState: this.report.finishState,
        Note: this.report.note
      };
      this.detailTableData.push(data);
      this.report.important = '';
      this.report.finishState = '';
      this.report.note = '';
    },
    RemovedetailTableData() {
      var currentAdds = [];
      this.currentSelectedAdd.forEach((element) => {
        currentAdds.push(element);
      });
      this.detailTableData = this.detailTableData.filter(
        (el) => !currentAdds.includes(el)
      );
      this.currentSelectedAdd = [];
    },
    /**
     * 點擊查詢
     */
    handleSearch: function() {
      this.pagination.currentPage = 1;
      this.loadTableData();
    },

    ShowViewDialog() {
      this.reset();
      if (
        this.currentSelected.length > 1 ||
        this.currentSelected.length === 0
      ) {
        this.$alert('請選擇一條數據進行編輯/修改', '提示');
      } else {
        this.currentId = this.currentSelected[0].Id;
        this.dialogViewVisible = true;
        this.bindEditInfo();
      }
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
          this.bindEditInfo();
        }
      } else {
        this.editFormTitle = '新增';
        this.currentId = '';
        this.dialogEditFormVisible = true;
      }
    },
    bindEditInfo: function() {
      this.loading = true;
      getAdministrativeWorkLogDetail(this.currentId)
        .then((res) => {
          this.editForm = res.ResData;
          this.detailTableData = res.ResData.AdministrativeWorkLogDetails;
          // 需個性化處理內容
        })
        .then(() => {
          this.loading = false;
        });
    },
    /**
     * 新增/修改保存
     */
    saveEditForm(action) {
      const data = {
        // UserName: this.editForm.UserName,
        // UserAccount: this.editForm.UserAccount,
        // LogDate: this.editForm.LogDate,
        // AuditSupervisor: this.editForm.AuditSupervisor,
        // AuditTime: this.editForm.AuditTime,
        // SupervisorNote: this.editForm.SupervisorNote,
        // BelongDept: this.editForm.BelongDept,
        // CreatorTime: this.editForm.CreatorTime,
        // CreatorUserId: this.editForm.CreatorUserId,
        // LastModifyTime: this.editForm.LastModifyTime,
        // LastModifyUserId: this.editForm.LastModifyUserId,
        AdministrativeWorkLogDetails: this.detailTableData,
        TodoNote: this.editForm.TodoNote,
        OtherNote: this.editForm.OtherNote,
        SupervisorNote: this.editForm.SupervisorNote,
        States: this.editForm.States,

        Id: this.currentId
      };
      if (action) {
        switch (action) {
          case 'sendtrail':
            data.States = 'sendtrail';
            break;
          case 'approval':
            data.States = 'approval';
            break;
        }
      }

      var url = 'AdministrativeWorkLog/Insert';
      if (this.currentId !== '') {
        url = 'AdministrativeWorkLog/UpdateAsync';
      }
      saveAdministrativeWorkLog(data, url).then((res) => {
        if (res.Success) {
          this.$message({
            message: '恭喜你，操作成功',
            type: 'success'
          });
          this.dialogEditFormVisible = false;
          this.dialogViewVisible = false;
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
    },
    deletePhysics: function() {
      if (this.currentSelected.length === 0) {
        this.$alert('請先選擇要操作的數據', '提示');
        return false;
      } else {
        const that = this;
        this.$confirm('是否確認刪除所選的數據項?', '警告', {
          confirmButtonText: '確定',
          cancelButtonText: '取消',
          type: 'warning'
        })
          .then(function() {
            var currentIds = [];
            that.currentSelected.forEach((element) => {
              currentIds.push(element.Id);
            });
            const data = {
              Ids: currentIds
            };
            return deleteAdministrativeWorkLog(data);
          })
          .then((res) => {
            if (res.Success) {
              that.$message({
                message: '恭喜你，刪除成功',
                type: 'success'
              });
              that.currentSelected = '';
              that.loadTableData();
            } else {
              this.$message({
                message: res.ErrMsg,
                type: 'error'
              });
            }
          });
      }
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
    handleSelectChange: function(selection, row) {
      this.currentSelected = selection;
    },
    handleSelectAllChange: function(selection) {
      this.currentSelected = selection;
    },
    handleSelectChangeAdd: function(selection, row) {
      this.currentSelectedAdd = selection;
    },
    handleSelectAllChangeAdd: function(selection) {
      this.currentSelectedAdd = selection;
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

<style></style>
