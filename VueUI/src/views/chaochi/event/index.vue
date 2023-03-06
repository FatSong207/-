<template>
  <div class="app-container">
    <div class="filter-container">
      <el-card>
        <el-form
          ref="searchform"
          inline
          :model="searchform"
          class="demo-form-inline"
          size="small"
        >
          <el-form-item label="活動代號：">
            <el-input
              v-model="searchform.EventId"
              clearable
              placeholder="名稱"
              style="width: 160px"
            />
          </el-form-item>
          <el-form-item label="活動名稱：">
            <el-input
              v-model="searchform.EventName"
              clearable
              placeholder="名稱"
              style="width: 160px"
            />
          </el-form-item>
          <el-form-item label="起始日期：">
            <DatePickerE
              v-model="searchform.StartDate"
              value-format="yyyy-MM-dd"
              format="yyyy-MM-dd"
              type="date"
              placeholder="選擇日期"
              style="width: 160px"
            />
          </el-form-item>
          <el-form-item label="結束日期：">
            <DatePickerE
              v-model="searchform.EndDate"
              value-format="yyyy-MM-dd"
              format="yyyy-MM-dd"
              type="date"
              :picker-options="pickerOptions"
              placeholder="選擇日期"
              style="width: 160px"
            />
          </el-form-item>
          <el-form-item label="活動類型：">
            <el-select
              v-model="searchform.EventType"
              placeholder="請選擇"
              clearable
              style="width: 140px"
            >
              <el-option
                v-for="item in TypeOptions"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              />
            </el-select>
          </el-form-item>
          <el-row>
            <el-form-item>
              <el-button
                type="primary"
                size="small"
                icon="el-icon-search"
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
            v-hasPermi="['Event/Add']"
            type="primary"
            icon="el-icon-plus"
            size="small"
            @click="ShowEditOrViewDialog()"
          >新增</el-button>
          <el-button
            v-hasPermi="['Event/Edit']"
            type="primary"
            icon="el-icon-edit"
            class="el-button-modify"
            size="small"
            @click="ShowEditOrViewDialog('edit')"
          >修改</el-button>
          <el-button
            v-hasPermi="['Event/Delete']"
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
          <!-- <el-button
            v-hasPermi="['Event/Statistic']"
            type="success"
            icon="el-icon-s-data"
            size="small"
            @click="loadTableData()"
          >滿意度問卷統計</el-button> -->
          <!-- <el-button
            v-hasPermi="['Event/Statistic']"
            type="success"
            icon="el-icon-s-data"
            size="small"
            @click="loadTableData()"
          >調查問卷統計</el-button> -->
        </el-button-group>
      </div>
      <el-table
        ref="gridtable"
        v-loading="tableloading"
        :data="tableData"
        border
        stripe
        highlight-current-row
        style="width: 1200"
        :default-sort="{ prop: 'SortCode', order: 'ascending' }"
        @select="handleSelectChange"
        @select-all="handleSelectAllChange"
        @sort-change="handleSortChange"
      >
        <el-table-column
          type="selection"
          width="30"
        />
        <el-table-column
          label="操作"
          width="160" align="center" fixed
        >
          <template slot-scope="scope">
            <el-button
              v-if="
                scope.row.EventType === '指定來賓' && scope.row.FinishCount > 0
              "
              type="success"
              icon="el-icon-s-data"
              size="small"
              @click="onStatistic(scope.row)"
            >滿意度問卷統計</el-button>
            <el-button
              v-else-if="
                scope.row.EventType === '開放參加' && scope.row.FinishCount > 0
              "
              type="success"
              icon="el-icon-s-data"
              size="small"
              @click="onStatistic(scope.row)"
            >調查問卷統計</el-button>
            <el-tag
              v-else
              type="info"
            >尚未有人填寫</el-tag>
          </template>
        </el-table-column>
        <el-table-column
          prop="EventId"
          label="活動代號"
          sortable="custom"
          width="160"
        />
        <el-table-column
          prop="EventName"
          label="活動名稱"
          sortable="custom"
          width="200"
          fixed
        />
        <el-table-column
          prop="StartDate"
          label="活動起始"
          sortable="custom"
          width="120"
        />
        <el-table-column
          prop="EndDate"
          label="活動結束"
          sortable="custom"
          width="120"
        />
        <el-table-column
          prop="FinishCount"
          label="當前總份數"
          sortable="custom"
          width="120"
        />
        <el-table-column
          prop="BelongCompany"
          label="活動所屬公司"
          sortable="custom"
          width="250"
        />
        <el-table-column
          prop="Manager"
          label="活動管理人"
          sortable="custom"
          width="120"
        />
        <el-table-column
          prop="EventType"
          label="活動類型"
          sortable="custom"
          width="120"
        />
        <el-table-column
          prop="LastModifyTime"
          label="最後異動日期"
          sortable="custom"
          width="160"
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
      <el-tabs
        v-model="activeName"
        type="border-card"
      >
        <el-tab-pane
          label="活動設定"
          name="A"
        >
          <el-card
            v-loading="loading"
            v-loading.lock="loading"
            element-loading-text="載入中..."
            class="box-card"
          >
            <el-form
              ref="editForm"
              v-loading="saveloading"
              :model="editForm"
              :rules="rules"
            >
              <el-form-item
                label="活動名稱："
                :label-width="formLabelWidth"
                prop="EventName"
              >
                <el-input
                  v-model="editForm.EventName"
                  placeholder="請輸入活動名稱"
                  autocomplete="off"
                  clearable
                  style="width: 600px"
                />
              </el-form-item>
              <el-form-item
                label="活動代號："
                :label-width="formLabelWidth"
                prop="EventId"
                style="width: 20%"
              >
                <el-input
                  v-model="editForm.EventId"
                  placeholder="請輸入活動代號"
                  autocomplete="off"
                  clearable
                  readonly
                  style="width: 220px"
                />
              </el-form-item>
              <el-form-item
                label="活動類型："
                :label-width="formLabelWidth"
                prop="EventType"
                style="width: 20%"
              >
                <el-select
                  v-model="editForm.EventType"
                  placeholder="請選擇"
                  clearable
                  style="width: 220px"
                  :disabled="editFormTitle === '編輯'"
                >
                  <el-option
                    v-for="item in TypeOptions"
                    :key="item.value"
                    :label="item.label"
                    :value="item.value"
                  />
                </el-select>
                <!-- <el-input
                  v-model="editForm.EventType"
                  autocomplete="off"
                  clearable
                  readonly
                /> -->
              </el-form-item>
              <el-form-item
                label="活動所屬公司："
                :label-width="formLabelWidth"
                prop="BelongCompany"
                style="width: 20%"
              >
                <el-input
                  v-model="editForm.BelongCompany"
                  placeholder="請輸入活動所屬公司"
                  autocomplete="off"
                  clearable
                  readonly
                  style="width: 600px"
                />
              </el-form-item>
              <el-row>
                <el-col :span="12">
                  <el-form-item
                    label="活動起訖日期："
                    :label-width="formLabelWidth"
                    prop="StartDate"
                  >
                    <DatePickerE
                      v-model="editForm.StartDate"
                      value-format="yyyy-MM-dd"
                      format="yyyy-MM-dd"
                      type="date"
                      placeholder="選擇日期"
                      style="width: 160px"
                    />
                    至
                    <DatePickerE
                      v-model="editForm.EndDate"
                      value-format="yyyy-MM-dd"
                      format="yyyy-MM-dd"
                      :picker-options="editFormpickerOptions"
                      type="date"
                      placeholder="選擇日期"
                      style="width: 160px"
                    />
                  </el-form-item>
                </el-col>
              </el-row>
              <el-form-item
                label="活動管理人員："
                :label-width="formLabelWidth"
                prop="Manager"
                style="width: 20%"
              >
                <el-input
                  v-model="editForm.Manager"
                  placeholder="請輸入活動管理人員"
                  autocomplete="off"
                  readonly
                />
              </el-form-item>
              <div
                v-if="editFormTitle === '編輯'"
                style="width: 100%"
              >
                <div class="rightbtn">
                  <el-button
                    type="primary"
                    size="small"
                    icon="el-icon-paperclip"
                    @click="saveEditForm()"
                  >儲存</el-button>
                  <el-button
                    size="small" icon="el-icon-close" @click="cancel"
                  >關閉</el-button>
                </div>
              </div>
            </el-form>
          </el-card>
        </el-tab-pane>
        <el-tab-pane
          v-if="editFormTitle === '編輯'"
          label="人力與成本"
          name="B"
        >
          <LaborCost
            :id="currentId"
            :eventcost="editForm.eventCosts"
            :eventpersonnel="editForm.eventPersonnels"
            :resetflag="emitResetFlag"
            @cancel="cancel"
          />
        </el-tab-pane>
        <el-tab-pane
          v-if="editFormTitle === '編輯' && currentType === '指定來賓'"
          label="活動賓客維護"
          name="C"
        >
          <el-card class="box-card">
            <EventGuest
              :id="currentId"
              @cancel="cancel"
            />
          </el-card>
        </el-tab-pane>
        <el-tab-pane
          v-if="editFormTitle === '編輯' && currentType === '開放參加'"
          label="調查問卷設定"
          name="D"
        >
          <Questionnaire
            v-if="activeName === 'D'"
            :questionnaire="editForm.EventQuestionnaire"
            :startdate="editForm.StartDate"
            :idd="editForm.Id"
            @bindinfo="bindEditInfo"
            @cancel="cancel"
          />
        </el-tab-pane>
        <el-tab-pane
          v-if="editFormTitle === '編輯' && currentType === '指定來賓'"
          label="滿意度問卷設定"
          name="E"
        >
          <Satisfaction
            v-if="activeName === 'E'"
            :satisfaction="editForm.EventSatisfaction"
            @cancel="cancel"
          />
        </el-tab-pane>
      </el-tabs>
      <div
        slot="footer"
        class="dialog-footer"
      >
        <!-- <el-button @click="cancel">關 閉</el-button> -->
        <el-button
          v-if="editFormTitle === '新增'"
          size="small"
          type="primary"
          @click="saveEditForm()"
        >確定</el-button>
      </div>
    </el-dialog>
    <el-dialog
      ref="dialogStatisticVisible"
      v-el-drag-dialog
      title="統計"
      :visible.sync="dialogStatisticVisible"
      fullscreen
      append-to-body
    >
      <el-card v-loading="dialogStatisticloading">
        <el-form :model="descriptionForm">
          <el-row>
            <el-col :span="6">
              <el-form-item
                v-if="descriptionForm.EventSatisfaction"
                label="問卷代號："
                :label-width="formLabelWidth"
              >
                <el-input
                  v-model="descriptionForm.EventSatisfaction.QCode"
                  autocomplete="off"
                  readonly
                />
              </el-form-item>
              <el-form-item
                v-else
                label="問卷代號："
                :label-width="formLabelWidth"
              >
                <el-input
                  v-model="descriptionForm.EventQuestionnaire.QCode"
                  autocomplete="off"
                  readonly
                />
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item
                label="活動代號："
                :label-width="formLabelWidth"
              >
                <el-input
                  v-model="descriptionForm.EventId"
                  autocomplete="off"
                  readonly
                />
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item
                label="活動名稱："
                :label-width="formLabelWidth"
              >
                <el-input
                  v-model="descriptionForm.EventName"
                  autocomplete="off"
                  readonly
                />
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item
                label="活動所屬公司："
                :label-width="formLabelWidth"
              >
                <el-input
                  v-model="descriptionForm.BelongCompany"
                  autocomplete="off"
                  readonly
                />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row>
            <el-col :span="6">
              <el-form-item
                label="當前總份數："
                :label-width="formLabelWidth"
              >
                <el-input
                  v-model="descriptionForm.FinishCount"
                  autocomplete="off"
                  readonly
                />
              </el-form-item>
            </el-col>
          </el-row>
        </el-form>
        <div class="list-btn-container">
          <el-button-group>
            <el-button
              type="success"
              icon="el-icon-download"
              size="small"
              :loading="btnloading"
              @click="ExportToXLSX1()"
            >匯出問卷統計</el-button>
          </el-button-group>
        </div>
        <!-- <div style="margin-bottom: 10px">
          指定分派店：<el-select
            v-model="ReportDept"
            placeholder="請選擇"
            filterable
            clearable
            style="margin-right: 10px"
          >
            <el-option
              v-for="item in orgList"
              :key="item.Id"
              :label="item.FullName"
              :value="item.Id"
            />
          </el-select>
          <el-button-group>
            <el-button
              type="primary" icon="el-icon-place" size="small"
            >分派勾選名單(開發中)</el-button>
          </el-button-group>
        </div> -->
        <el-table
          ref="gridtable"
          :data="statisticTableData"
          border
          stripe
          highlight-current-row
        >
          <el-table-column
            type="selection"
            width="40" fixed
          />
          <el-table-column
            prop="UserName"
            label="填寫人姓名"
            width="100"
            fixed
          />
          <el-table-column
            prop="Sex"
            label="性別" width="80" fixed
          >
            <template slot-scope="scope">
              <span>{{
                scope.row.Sex === 'M' ? '男' : scope.row.Sex === 'F' ? '女' : ''
              }}</span>
            </template>
          </el-table-column>
          <el-table-column
            prop="Phone"
            label="手機號碼" width="100" fixed
          />
          <el-table-column
            v-if="descriptionForm.EventType === '開放參加'"
            prop="RealName"
            label="執行業務"
            width="100"
            fixed
          />
          <el-table-column
            prop="FullName"
            label="負責人所屬店" fixed
          />
          <el-table-column
            v-for="(item, index) in tableTitle"
            :key="index"
            label-class-name="testclass"
            class-name="row"
            :prop="item"
            :label="item"
            width="500"
          />
        </el-table>
      </el-card>
      <div
        slot="footer"
        class="dialog-footer"
      >
        <!-- <el-button @click="cancel">關 閉</el-button> -->
        <el-button
          size="small"
          icon="el-icon-close"
          @click="dialogStatisticVisible = false"
        >關閉</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import {
  getEventListWithPager,
  getEventDetail,
  saveEvent,
  deleteEvent,
  GenStatistic,
  GenStatisticTableData,
  ExportToXLSX
} from '@/api/chaochi/event/eventservice';
import { parseTime, ROCDateToDate } from '@/utils/index';
import { GetAllOrgForSelect } from '@/api/security/organizeservice';
import { mapGetters } from 'vuex';
import elDragDialog from '@/directive/el-drag-dialog'; // 彈窗可移動
import DatePickerE from '@/components/RocDatepickerE';
import LaborCost from './laborcost.vue';
import EventGuest from './eventguest.vue';
import Questionnaire from './questionnaire.vue';
import Satisfaction from './satisfaction.vue';
export default {
  name: 'Event', // 需與菜單的功能編碼一致
  directives: { elDragDialog },
  components: {
    DatePickerE,
    LaborCost,
    EventGuest,
    Questionnaire,
    Satisfaction
  },
  data() {
    const valTowVal = (rule, value, callback) => {
      if (value === '' || value === null) {
        callback(new Error('請選擇日期!'));
      } else if (
        this.editForm.StartDate === '' ||
        this.editForm.EndDate === '' ||
        this.editForm.StartDate === null ||
        this.editForm.EndDate === null
      ) {
        callback(new Error());
      } else {
        callback();
      }
    };
    return {
      editForm: {},
      descriptionForm: {
        EventSatisfaction: {},
        EventQuestionnaire: {}
      },
      rules: {
        EventName: [
          { required: true, trigger: 'blur', message: '請輸入活動名稱' }
        ],
        EventType: [
          {
            required: true,
            trigger: ['blur', 'change'],
            message: '請選擇活動類型'
          }
        ],
        StartDate: [
          {
            required: true,
            validator: valTowVal,
            message: '請選擇日期',
            trigger: 'change'
          }
        ]
      },
      searchform: {
        EventId: '',
        EventName: '',
        StartDate: '',
        EndDate: '',
        EventType: ''
      },
      pickerOptions: {
        disabledDate: (time) => {
          var cur = ROCDateToDate(this.searchform.StartDate);
          return time.getTime() < new Date(cur).getTime();
        }
      },
      editFormpickerOptions: {
        disabledDate: (time) => {
          return (
            time.getTime() <
            new Date(ROCDateToDate(this.editForm.StartDate)).getTime()
          );
        }
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
      statisticTableData: [],
      currentSelected: [],
      tableTitle: [],
      orgList: [],
      TypeOptions: [
        {
          value: '指定來賓',
          label: '指定來賓'
        },
        {
          value: '開放參加',
          label: '開放參加'
        }
      ],
      dialogEditFormVisible: false,
      dialogStatisticVisible: false,
      loading: false,
      tableloading: true,
      saveloading: false,
      btnloading: false,
      dialogStatisticloading: false,
      emitResetFlag: false,
      editFormTitle: '',
      formLabelWidth: '135px',
      ReportDept: '',
      currentId: '', // 當前操作對象的ID值，主要用于修改
      currentType: '',
      activeName: 'A'
    };
  },
  computed: {
    ...mapGetters(['name', 'dept'])
  },
  created() {
    this.pagination.currentPage = 1;
    this.InitDictItem();
    this.loadTableData();
    GetAllOrgForSelect().then((res) => {
      this.orgList = res.ResData;
    });
  },
  methods: {
    /**
     * 初始化數據
     */
    InitDictItem() {},

    ExportToXLSX1() {
      this.btnloading = true;
      ExportToXLSX(this.descriptionForm.Id)
        .then((res) => {
          const blob = res; // new Blob([response.data], { type: 'image/jpeg' })
          const link = document.createElement('a');
          link.href = URL.createObjectURL(blob);
          link.download = this.descriptionForm.EventName + '_問卷統計.xlsx';
          link.click();
          URL.revokeObjectURL(link.href);
        })
        .finally(() => {
          this.btnloading = false;
        });
    },

    onStatistic(row) {
      this.dialogStatisticVisible = true;
      this.dialogStatisticloading = true;
      GenStatistic(row.Id).then((res) => {
        // console.log(res);
        this.tableTitle = res.ResData;
        GenStatisticTableData(row.Id)
          .then((res) => {
            // console.log(res);
            this.statisticTableData = res.ResData;
          })
          .finally(() => {
            this.dialogStatisticloading = false;
          });
      });
      getEventDetail(row.Id).then((res) => {
        console.log(res.ResData);
        this.descriptionForm = res.ResData;
      });
      // console.log(row);
    },
    // 取消按鈕
    cancel() {
      this.dialogEditFormVisible = false;
      this.currentSelected = '';
      this.loadTableData();
      this.reset();
      this.emitResetFlag = !this.emitResetFlag;
      this.activeName = 'A';
    },
    // 表單重置
    reset() {
      this.editForm = {
        EventId: '',
        EventName: '',
        StartDate: '',
        EndDate: '',
        BelongCompany: '',
        Manager: '',
        EventType: '',
        CreatorTime: '',
        CreatorUserId: '',
        LastModifyTime: '',
        LastModifyUserId: ''

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
        Filter: filter,
        Keywords: this.searchform.keywords,
        Order: this.sortableData.order,
        Sort: this.sortableData.sort
      };
      getEventListWithPager(seachdata).then((res) => {
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
      this.loading = true;
      this.reset();
      if (view !== undefined) {
        if (
          this.currentSelected.length > 1 ||
          this.currentSelected.length === 0
        ) {
          this.$alert('請選擇一條數據進行編輯/修改', '提示');
        } else {
          this.currentId = this.currentSelected[0].Id;
          this.currentType = this.currentSelected[0].EventType;
          this.editFormTitle = '編輯';
          this.dialogEditFormVisible = true;
          this.bindEditInfo();
        }
      } else {
        this.editFormTitle = '新增';
        this.editForm.EventId = parseTime(new Date(), '{c}{m}{d}{h}{i}{s}{ms}');
        this.editForm.BelongCompany = this.dept;
        this.editForm.Manager = this.name;
        this.currentId = '';
        this.dialogEditFormVisible = true;
        this.loading = false;
      }
    },
    bindEditInfo: function() {
      getEventDetail(this.currentId).then((res) => {
        this.editForm = res.ResData;
        this.loading = false;
        // 需個性化處理內容
      });
    },
    /**
     * 新增/修改保存
     */
    saveEditForm() {
      this.$refs['editForm'].validate((valid) => {
        if (valid) {
          this.saveloading = true;
          const data = {
            EventId: this.editForm.EventId,
            EventName: this.editForm.EventName,
            StartDate: this.editForm.StartDate,
            EndDate: this.editForm.EndDate,
            BelongCompany: this.editForm.BelongCompany,
            Manager: this.editForm.Manager,
            EventType: this.editForm.EventType,
            CreatorTime: this.editForm.CreatorTime,
            CreatorUserId: this.editForm.CreatorUserId,
            LastModifyTime: this.editForm.LastModifyTime,
            LastModifyUserId: this.editForm.LastModifyUserId,

            Id: this.currentId
          };

          var url = 'Event/Insert';
          if (this.currentId !== '') {
            url = 'Event/Update';
          }
          saveEvent(data, url)
            .then((res) => {
              if (res.Success) {
                this.$message({
                  message: '恭喜你，操作成功',
                  type: 'success'
                });
                if (this.editFormTitle === '新增') {
                  this.dialogEditFormVisible = false;
                  this.currentSelected = '';
                  this.loadTableData();
                  this.InitDictItem();
                }
              } else {
                this.$message({
                  message: res.ErrMsg,
                  type: 'error'
                });
              }
            })
            .finally((res) => {
              this.saveloading = false;
            });
        } else {
          return false;
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
            return deleteEvent(data);
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
    }
  }
};
</script>

<style>
.testclass {
  color: rgb(78, 155, 165);
}
.row {
  color: rgb(78, 155, 165);
}
</style>
