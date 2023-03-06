<template>
  <div class="app-container">
    <div class="filter-container">
      <el-card shadow="hover">
        <el-form
          ref="searchform"
          :inline="true"
          :model="searchform"
          class="demo-form-inline"
          size="small"
        >
          <el-form-item label="姓名：">
            <el-input
              v-model="searchform.Name"
              clearable placeholder="姓名"
            />
          </el-form-item>
          <el-form-item label="身分證字號/居留證號：">
            <el-input
              v-model="searchform.IDNo"
              v-upperCase
              clearable
              placeholder="身分證字號/居留證號"
            />
          </el-form-item>
          <el-form-item label="當前狀態：">
            <el-select
              v-model="searchform.ResultState"
              placeholder="請選擇"
              clearable
            >
              <el-option value="資料蒐集中">資料蒐集中</el-option>
              <el-option value="灰名單">灰名單</el-option>
              <el-option value="白名單">白名單</el-option>
              <el-option value="黑名單">黑名單</el-option>
            </el-select>
            <!-- <el-input
              v-model="searchform.ResultState"
              clearable
              placeholder="當前狀態"
            /> -->
          </el-form-item>
          <el-form-item label="名單來源：">
            <el-select
              v-model="searchform.ReportDept"
              placeholder="請選擇"
              filterable
              clearable
            >
              <el-option
                v-for="item in orgList"
                :key="item.Id"
                :label="item.FullName"
                :value="item.Id"
              />
            </el-select>
            <!-- <el-input
              v-model="searchform.ReportDept"
              clearable
              placeholder="名單來源"
            /> -->
          </el-form-item>
          <el-row>
            <el-form-item>
              <el-button
                type="primary"
                @click="handleSearch()"
              >查詢</el-button>
            </el-form-item>
          </el-row>
        </el-form>
      </el-card>
    </div>
    <el-card>
      <div class="list-btn-container">
        <el-button-group>
          <el-button
            v-hasPermi="['BlackList/Add']"
            type="primary"
            icon="el-icon-plus"
            size="small"
            @click="ShowAddDialog()"
          >新增</el-button>
          <el-button
            v-hasPermi="['BlackList/Edit']"
            type="primary"
            icon="el-icon-view"
            class="el-button-modify"
            size="small"
            @click="ShowEditOrViewDialog('edit')"
          >檢視</el-button>
          <el-button
            v-hasPermi="['BlackList/Delete']"
            type="danger"
            icon="el-icon-delete"
            size="small"
            @click="deletePhysics()"
          >刪除</el-button>
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
        @row-dblclick="rowdblclick"
      >
        <el-table-column
          type="selection"
          width="40"
        />
        <el-table-column
          prop="Name"
          label="姓名"
          sortable="custom"
          width="120"
          fixed
        />
        <el-table-column
          prop="IDNo"
          label="身分證字號/居留證號"
          width="170"
          fixed
        />
        <el-table-column
          prop="Age"
          label="年齡"
          width="60"
          align="center"
          fixed
        />
        <!-- <el-table-column
          prop="Reporter"
          label="回報者"
          sortable="custom"
          width="120"
        /> -->
        <el-table-column
          prop="ReportDept"
          label="名單來源"
          sortable="custom"
          width="120"
        />
        <el-table-column
          prop="CreatorUserName"
          label="隸屬業務"
          sortable="custom"
          width="120"
        />
        <el-table-column
          prop="CreatorTime"
          label="生成日期"
          sortable="custom"
          width="160"
        />
        <el-table-column
          prop="ResultState"
          label="當前狀態"
          sortable="custom"
          width="120"
        />
        <el-table-column
          prop="Rating"
          label="綜合評分" width="220"
        />
        <el-table-column
          prop="Note"
          label="備註" width="220"
        >
          <!-- <template slot-scope="scope">
            <div v-html="scope.row.Note" />
          </template> -->
        </el-table-column>
        <el-table-column
          prop="TrialNote"
          label="審核說明" width="220"
        />
        <el-table-column
          prop="LastModifyTime"
          label="異動日期"
          sortable="custom"
          width="160"
        />
        <el-table-column
          prop="LastModifyUserId"
          label="異動人員" width="120"
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
      v-loading="loading"
      v-loading.lock="loading"
      element-loading-text="載入中..."
      :title="editFormTitle"
      :visible.sync="dialogEditFormVisible"
      fullscreen
      append-to-body
    >
      <el-form
        ref="editForm"
        :model="editForm" :rules="rules" inline
      >
        <el-card>
          <el-row>
            <el-form-item
              label="名稱："
              :label-width="formLabelWidth2"
              prop="Name"
            >
              <el-input
                v-model="editForm.Name"
                readonly
                autocomplete="off"
                clearable
              />
            </el-form-item>
            <el-form-item
              label="身分證字號/居留證號："
              label-width="160px"
              prop="IDNo"
            >
              <el-input
                v-model="editForm.IDNo"
                readonly
                autocomplete="off"
                clearable
              />
            </el-form-item>
            <el-form-item
              label="生日："
              :label-width="formLabelWidth2"
              prop="Birthday"
            >
              <el-input
                v-model="editForm.Birthday"
                readonly
                autocomplete="off"
                clearable
              />
            </el-form-item>
            <el-form-item
              label="身分別："
              :label-width="formLabelWidth2"
              prop="Birthday"
            >
              <el-input
                v-model="editForm.IDentity"
                readonly
                autocomplete="off"
                clearable
              />
            </el-form-item>
          </el-row>
          <el-row>
            <el-col :span="6">
              <div class="recordbox">
                <el-divider content-position="left">
                  <h2>犯罪紀錄</h2>
                </el-divider>
              </div>
              <el-form-item
                label="刑事通緝犯"
                :label-width="formLabelWidth2"
                prop="Rating"
              >
                <el-input
                  v-model="editForm.CriminalWanted"
                  type="textarea"
                  :rows="3"
                  style="width: 300px"
                  readonly
                  autocomplete="off"
                  clearable
                />
              </el-form-item>
              <el-form-item
                label="查捕中逃犯"
                prop="Rating"
                :label-width="formLabelWidth2"
              >
                <el-input
                  v-model="editForm.CatchingFugitives"
                  type="textarea"
                  :rows="3"
                  style="width: 300px"
                  readonly
                  autocomplete="off"
                  clearable
                />
              </el-form-item>
              <el-form-item
                label="查緝專刊"
                :label-width="formLabelWidth2"
                prop="Rating"
              >
                <el-input
                  v-model="editForm.CriminalLib"
                  type="textarea"
                  :rows="3"
                  style="width: 300px"
                  readonly
                  autocomplete="off"
                  clearable
                />
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <div class="recordbox">
                <el-divider content-position="left">
                  <h2>欠債紀錄</h2>
                </el-divider>
              </div>
              <el-form-item
                label="交通罰鍰"
                :label-width="formLabelWidth2"
                prop="Rating"
              >
                <el-input
                  v-model="editForm.TraficFines"
                  type="textarea"
                  :rows="3"
                  style="width: 300px"
                  readonly
                  autocomplete="off"
                  clearable
                />
              </el-form-item>
              <el-form-item
                label="汽機車燃料稅"
                prop="Rating"
                :label-width="formLabelWidth2"
              >
                <el-input
                  v-model="editForm.FuelPenaltyExpireds"
                  type="textarea"
                  :rows="3"
                  style="width: 300px"
                  readonly
                  autocomplete="off"
                  clearable
                />
              </el-form-item>
              <el-form-item
                label="消債事件"
                :label-width="formLabelWidth2"
                prop="Rating"
              >
                <el-input
                  v-model="editForm.ConsumerDebt"
                  type="textarea"
                  :rows="3"
                  style="width: 300px"
                  readonly
                  autocomplete="off"
                  clearable
                />
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <div class="recordbox">
                <el-divider content-position="left">
                  <h2>行為能力</h2>
                </el-divider>
              </div>
              <el-form-item
                label="監護補助"
                :label-width="formLabelWidth2"
                prop="Rating"
              >
                <el-input
                  v-model="editForm.Domestic"
                  type="textarea"
                  :rows="3"
                  style="width: 300px"
                  readonly
                  autocomplete="off"
                  clearable
                />
              </el-form-item>
              <div class="recordbox">
                <el-divider content-position="left">
                  <h2>租金補貼</h2>
                </el-divider>
              </div>
              <el-form-item
                label="縣市/公會版"
                :label-width="formLabelWidth2"
                prop="Rating"
              >
                <el-input
                  v-model="editForm.RentSubsidy"
                  type="textarea"
                  :rows="3"
                  style="width: 300px"
                  readonly
                  autocomplete="off"
                  clearable
                />
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <div class="recordbox">
                <el-divider content-position="left">
                  <h2>黑名單</h2>
                </el-divider>
              </div>
              <el-form-item
                label="法源網"
                :label-width="formLabelWidth2"
                prop="Rating"
              >
                <el-input
                  v-model="editForm.LawBank"
                  type="textarea"
                  :rows="3"
                  style="width: 300px"
                  readonly
                  autocomplete="off"
                  clearable
                />
              </el-form-item>
            </el-col>
          </el-row>
          <el-divider content-position="left">
            <h2>狀態判斷</h2>
          </el-divider>
          <el-row>
            <el-form-item
              label="綜合評分："
              :label-width="formLabelWidth2"
              prop="Rating"
            >
              <el-input
                v-model="editForm.Rating"
                readonly
                autocomplete="off"
                clearable
              />
            </el-form-item>
          </el-row>
          <el-row>
            <el-col :span="12">
              <el-form-item
                label="備註："
                :label-width="formLabelWidth2"
                prop="Note"
              >
                <el-input
                  v-model="editForm.Note"
                  type="textarea"
                  autocomplete="off"
                  style="width: 420px"
                  :rows="6"
                  maxlength="500"
                  show-word-limit
                />
              </el-form-item>
            </el-col>
            <!-- <el-col :span="6">
            <el-form-item
              label="回報者："
              :label-width="formLabelWidth2"
              prop="Reporter"
              style="width: 100%"
            >
              <el-input
                v-model="editForm.Reporter"
                readonly
                autocomplete="off"
                clearable
              />
            </el-form-item>
          </el-col> -->
          </el-row>
          <el-row>
            <el-col :span="6">
              <el-form-item
                label="名單來源："
                :label-width="formLabelWidth2"
                prop="ReportDept"
              >
                <el-input
                  v-model="editForm.ReportDept"
                  readonly
                  autocomplete="off"
                  clearable
                />
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item
                label="隸屬業務："
                :label-width="formLabelWidth2"
                prop="CreatorUserId"
              >
                <el-input
                  v-model="editForm.CreatorUserName"
                  readonly
                  autocomplete="off"
                  clearable
                />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row>
            <el-col :span="12">
              <el-form-item
                label="審核說明："
                :label-width="formLabelWidth2"
                prop="TrialNote"
              >
                <el-input
                  v-model="editForm.TrialNote"
                  type="textarea"
                  autocomplete="off"
                  style="width: 420px"
                  :rows="6"
                  maxlength="500"
                  show-word-limit
                />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row>
            <el-col :span="6">
              <el-form-item
                label="當前狀態："
                :label-width="formLabelWidth2"
                prop="ResultState"
              >
                <el-input
                  v-model="editForm.ResultState"
                  readonly
                  autocomplete="off"
                  clearable
                />
              </el-form-item>
            </el-col>
            <!-- <el-col :span="6">
              <el-form-item
                v-hasPermi="['BlackList/resultState']"
                label="主管判定："
                :label-width="formLabelWidth2"
                prop="newResultState"
              >
                <el-select
                  v-model="newResultState"
                  placeholder="請選擇"
                  clearable
                  style="width: 120px"
                  :disabled="!hasPermission"
                >
                  <el-option
                    v-for="item in StateOptions"
                    :key="item.value"
                    :label="item.label"
                    :value="item.value"
                  />
                </el-select>
              </el-form-item>
            </el-col> -->
          </el-row>
        </el-card>
      </el-form>
      <div
        slot="footer"
        class="dialog-footer"
      >
        <el-button
          size="small" icon="el-icon-close" @click="cancel"
        >關閉</el-button>
        <!-- <el-button
          v-if="hasPermission"
          v-hasPermi="['BlackList/Edit']"
          type="primary"
          size="small"
          icon="el-icon-paperclip"
          @click="saveEditForm('update')"
        >儲存</el-button> -->
      </div>
    </el-dialog>
    <el-dialog
      v-el-drag-dialog
      title="新增"
      :visible.sync="addDialogVisible"
      append-to-body
      width="500px"
    >
      <el-form
        ref="AddForm"
        :model="addForm" :rules="addRule" inline
      >
        <el-row>
          <el-form-item
            label="姓名："
            prop="Name" style="width: 100%"
          >
            <el-input
              v-model="addForm.Name"
              autocomplete="off" clearable
            />
          </el-form-item>
          <el-form-item
            label="身分證字號/居留證號："
            prop="IDNo"
            style="width: 100%"
          >
            <el-input
              v-model="addForm.IDNo"
              v-upperCase
              autocomplete="off"
              :maxlength="10"
              clearable
            />
          </el-form-item>
          <el-form-item
            label="生日："
            prop="Birthday" style="width: 100%"
          >
            <DatePickerE
              v-model="addForm.Birthday"
              value-format="yyyy-MM-dd"
              format="yyyy-MM-dd"
              type="date"
              placeholder="選擇日期"
            />
            <!-- <el-input
              v-model="addForm.Name"
              readonly
              autocomplete="off"
              clearable
            /> -->
          </el-form-item>
          <el-form-item
            v-if="roles.indexOf('Business assistance') > -1"
            label="身分別："
            prop="Name"
            style="width: 100%"
          >
            <el-select
              v-model="addForm.IDentity"
              autocomplete="off" clearable
            >
              <el-option value="承租人">承租人</el-option>
              <el-option value="保證人">保證人</el-option>
              <el-option value="同住人">同住人</el-option>
            </el-select>
          </el-form-item>
        </el-row>
      </el-form>
      <div
        slot="footer"
        class="dialog-footer"
      >
        <el-button
          type="primary"
          size="small"
          icon="el-icon-paperclip"
          @click="saveEditForm('Insert')"
        >儲存</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import {
  getBlackListListWithPager,
  getBlackListDetail,
  saveBlackList,
  deleteBlackList
} from '@/api/chaochi/blacklist/blacklistservice';
import { GetAllOrgForSelect } from '@/api/security/organizeservice';
import DatePickerE from '@/components/RocDatepickerE';

import elDragDialog from '@/directive/el-drag-dialog'; // 彈窗可移動
import { mapGetters } from 'vuex';
import { validateIDCombo } from '@/utils/validate';
export default {
  name: 'BlackList', // 需與菜單的功能編碼一致
  components: { DatePickerE },
  directives: { elDragDialog },
  data() {
    return {
      editForm: {},
      addForm: {},
      rules: {},
      pagination: {
        currentPage: 1,
        pagesize: 20,
        pageTotal: 0
      },
      searchform: {
        keywords: ''
      },
      sortableData: {
        order: 'desc',
        sort: 'CreatorTime'
      },
      tableData: [],
      currentSelected: [],
      orgList: [],
      StateOptions: [
        {
          label: '白名單',
          value: '白名單'
        },
        {
          label: '黑名單',
          value: '黑名單'
        }
      ],
      addRule: {
        IDNo: [
          { required: true, trigger: 'blur', validator: validateIDCombo }
          // { min: 10, max: 10, message: '請輸入10位', trigger: 'change' }
        ],
        Name: [{ required: true, message: '請輸入姓名', trigger: 'blur' }],
        Birthday: [{ required: true, message: '請選擇生日', trigger: 'blur' }]
      },
      tableloading: true,
      loading: false,
      dialogEditFormVisible: false,
      addDialogVisible: false,
      editFormTitle: '',
      newResultState: '',
      formLabelWidth2: '100px',
      currentId: ''
    };
  },
  computed: {
    ...mapGetters(['userId', 'dept', 'roles']),
    hasPermission() {
      return this.editForm.TrialUserId === this.userId;
    }
  },
  created() {
    GetAllOrgForSelect().then((res) => {
      this.orgList = res.ResData;
    });
    this.pagination.currentPage = 1;
    this.InitDictItem();
    this.$set(this.searchform, 'ReportDept', this.dept);
    // this.searchform.ReportDept = 'Root';
    this.loadTableData();
  },
  methods: {
    /**
     * 初始化數據
     */
    InitDictItem() {
      this.addForm = {
        Name: '',
        IDNo: '',
        Birthday: ''
      };
    },

    // 取消按鈕
    cancel() {
      this.dialogEditFormVisible = false;
      this.reset();
    },
    // 表單重置
    reset() {
      this.editForm = {
        Name: '',
        IDNo: '',
        Birthday: '',
        Gender: '',
        Age: '',
        ContactPhone: '',
        Reporter: '',
        ReportDept: '',
        ResultState: '',
        Rating: '',
        Note: '',
        TrialNote: '',
        CreatorTime: '',
        CreatorUserId: '',
        LastModifyTime: '',
        LastModifyUserId: ''

        // 需個性化處理內容
      };
      this.resetForm('editForm');
    },
    ShowAddDialog() {
      this.addDialogVisible = true;
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
        Filter: filter,
        Keywords: this.searchform.keywords,
        Order: this.sortableData.order,
        Sort: this.sortableData.sort
      };
      getBlackListListWithPager(seachdata).then((res) => {
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
      getBlackListDetail(this.currentId)
        .then((res) => {
          this.editForm = res.ResData;
          // 需個性化處理內容
        })
        .finally(() => {
          this.loading = false;
        });
    },
    /**
     * 新增/修改保存
     */
    saveEditForm(action) {
      // this.$refs['editForm'].validate(valid => {
      //   if (valid) {
      var data = {};
      var moveOn = true;
      if (action === 'Insert') {
        this.$refs['AddForm'].validate((valid) => {
          if (valid) {
            console.log(valid);
            data = {
              Name: this.addForm.Name,
              idno: this.addForm.IDNo,
              Birthday: this.addForm.Birthday,
              IDentity: this.addForm.IDentity
            };
            moveOn = true;
          } else {
            console.log(valid);
            moveOn = false;
            return false;
          }
        });
        if (!moveOn) {
          return false;
        }
      } else {
        data = {
          Name: this.editForm.Name,
          IDNo: this.editForm.IDNo,
          Birthday: this.editForm.Birthday,
          Gender: this.editForm.Gender,
          Age: this.editForm.Age,
          ContactPhone: this.editForm.ContactPhone,
          Reporter: this.editForm.Reporter,
          ReportDept: this.editForm.ReportDept,
          ResultState: this.editForm.ResultState,
          Rating: this.editForm.Rating,
          Note: this.editForm.Note,
          TrialNote: this.editForm.TrialNote,
          CreatorTime: this.editForm.CreatorTime,
          CreatorUserId: this.editForm.CreatorUserId,
          LastModifyTime: this.editForm.LastModifyTime,
          LastModifyUserId: this.editForm.LastModifyUserId,

          Id: this.currentId
        };
      }
      if (this.newResultState !== '') {
        data.ResultState = this.newResultState;
      }
      saveBlackList(data, `BlackList/${action}`).then((res) => {
        if (res.Success) {
          this.$message.success('儲存成功');
          this.dialogEditFormVisible = false;
          this.addDialogVisible = false;
          this.currentSelected = '';
          this.loadTableData();
          this.InitDictItem();
        } else {
          this.$message.error('失敗');
        }
      });
      // } else {
      //   return false;
      // }
      // });
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
            return deleteBlackList(data);
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
    },
    // 雙擊row
    rowdblclick(row, column, event) {
      this.$refs.gridtable.clearSelection();
      this.currentSelected = '';
      this.rerenderGridtable = Date.now(); // 強制重繪 https://bbchin.com/archives/el-table-rerender
      this.$nextTick(function() {
        // https://ithelp.ithome.com.tw/articles/10240669
        this.$refs.gridtable.toggleRowSelection(row, true);
        this.currentSelected = this.$refs.gridtable.selection;
        this.ShowEditOrViewDialog('edit');
      });
    }
  }
};
</script>

<style>
.recordbox {
  margin-right: 50px;
}
</style>
