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
          <el-form-item label="客訴單號：">
            <el-input
              v-model="searchform.CCode"
              clearable
              placeholder="名稱"
              style="width: 160px"
            />
          </el-form-item>
          <el-form-item label="填單日期：">
            <DatePickerE
              v-model="searchform.ComplaintCreatorTime"
              value-format="yyyy-MM-dd"
              format="yyyy-MM-dd"
              type="date"
              placeholder="選擇日期"
              style="width: 160px"
            />
          </el-form-item>
          <el-form-item label="單據狀態：">
            <el-select
              v-model="searchform.State"
              placeholder="請選擇"
              clearable
              style="width: 160px"
            >
              <el-option
                v-for="item in StateOptions"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              />
            </el-select>
          </el-form-item>
          <el-row>
            <el-form-item label="客訴類別：">
              <el-select
                v-model="searchform.ComplaintType"
                placeholder="請選擇"
                clearable
                style="width: 160px"
              >
                <el-option
                  v-for="item in ComplaintTypeOptions"
                  :key="item.value"
                  :label="item.label"
                  :value="item.value"
                />
              </el-select>
            </el-form-item>
            <el-form-item label="被投訴公司/部門：">
              <el-input
                v-model="searchform.ComplaintDept"
                clearable
                placeholder="名稱"
                style="width: 160px"
              />
            </el-form-item>
            <el-form-item label="被投訴對象：">
              <el-input
                v-model="searchform.Complainee"
                clearable
                placeholder="名稱"
                style="width: 160px"
              />
            </el-form-item>
            <el-form-item label="糾紛合約號碼：">
              <el-input
                v-model="searchform.CId"
                clearable
                placeholder="名稱"
                style="width: 160px"
              />
            </el-form-item>
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
            v-hasPermi="['Complaint/Edit']"
            type="primary"
            icon="el-icon-view"
            size="small"
            @click="ShowEditOrViewDialog('edit')"
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
          prop="CCode"
          label="客訴單編號" width="220"
        />
        <el-table-column
          prop="Name"
          label="客訴人姓名" width="120"
        />
        <el-table-column
          prop="ContactPhone"
          label="聯絡電話" width="160"
        />
        <el-table-column
          prop="ComplaintType"
          label="客訴類別"
          sortable="custom"
          width="120"
        />
        <el-table-column
          prop="State"
          label="單據狀態"
          sortable="custom"
          width="120"
        />
        <el-table-column
          prop="HandleUserName"
          label="客服人員"
          sortable="custom"
          width="120"
        />
        <el-table-column
          prop="ComplaintCreatorTime"
          label="填單日期"
          sortable="custom"
          width="120"
        />
        <el-table-column
          prop="SendTrialTime"
          label="送審日期"
          sortable="custom"
          width="120"
        />
        <el-table-column
          prop="EndCaseTime"
          label="結案日期"
          sortable="custom"
          width="120"
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
      <!-- <div style="width: 100%">
        <h3>處理提示：<br>
        1. 請客服人員將處理結果填入相關欄位，按下送審鍵後請主管結案。<br>
        2. 請注意處理時間<br></h3>
      </div> -->
      <div style="display: flex">
        <div style="float: left; width: 40%">
          <el-form
            ref="editForm"
            :model="editForm" :rules="rules"
          >
            <el-form-item
              label="投訴人姓名："
              :label-width="formLabelWidth"
              prop="Name"
              style="width: 50%"
            >
              <el-input
                v-model="editForm.Name"
                autocomplete="off"
                clearable
                readonly
              />
            </el-form-item>
            <el-form-item
              label="投訴人聯絡電話："
              :label-width="formLabelWidth"
              prop="ContactPhone"
              style="width: 50%"
            >
              <el-input
                v-model="editForm.ContactPhone"
                autocomplete="off"
                clearable
                readonly
              />
            </el-form-item>
            <el-form-item
              label="投訴人聯絡信箱："
              :label-width="formLabelWidth"
              prop="ContactMail"
              style="width: 50%"
            >
              <el-input
                v-model="editForm.ContactMail"
                autocomplete="off"
                clearable
                readonly
              />
            </el-form-item>
            <el-form-item
              label="投訴人可連絡時段："
              :label-width="formLabelWidth"
              prop="ContactDatetime"
              style="width: 50%"
            >
              <el-input
                v-model="editForm.ContactDatetime"
                autocomplete="off"
                clearable
                readonly
              />
            </el-form-item>
            <el-form-item
              label="客訴類別："
              :label-width="formLabelWidth"
              prop="ComplaintType"
              style="width: 50%"
            >
              <el-input
                v-model="editForm.ComplaintType"
                autocomplete="off"
                clearable
                readonly
              />
            </el-form-item>
            <el-row>
              <el-col :span="12">
                <el-form-item
                  label="投訴部門/公司："
                  :label-width="formLabelWidth"
                  prop="ComplaintDept"
                  style="width: 100%"
                >
                  <el-input
                    v-model="editForm.ComplaintDept"
                    autocomplete="off"
                    clearable
                    readonly
                  />
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item
                  label="投訴對象姓名："
                  :label-width="formLabelWidth"
                  prop="Complainee"
                  style="width: 90%"
                >
                  <el-input
                    v-model="editForm.Complainee"
                    autocomplete="off"
                    clearable
                    readonly
                  />
                </el-form-item>
              </el-col>
            </el-row>
            <el-form-item
              label="有糾紛的合約編號："
              :label-width="formLabelWidth"
              prop="CId"
              style="width: 50%"
            >
              <el-input
                v-model="editForm.CId"
                autocomplete="off"
                clearable
                readonly
              />
            </el-form-item>
            <el-form-item
              label="客訴事由："
              :label-width="formLabelWidth"
              prop="ComplaintReason"
              style="width: 95%"
            >
              <el-input
                v-model="editForm.ComplaintReason"
                autocomplete="off"
                type="textarea"
                :rows="3"
                readonly
              />
            </el-form-item>
            <el-form-item
              label="投訴人意見提供："
              :label-width="formLabelWidth"
              prop="ProvideAdvice"
              style="width: 95%"
            >
              <el-input
                v-model="editForm.ProvideAdvice"
                autocomplete="off"
                type="textarea"
                :rows="3"
                readonly
              />
            </el-form-item>
            <el-form-item
              label="累積發信次數："
              :label-width="formLabelWidth"
              prop="SendMailCount"
              style="width: 35%"
            >
              <el-input
                v-model="editForm.SendMailCount"
                autocomplete="off"
                clearable
                readonly
              />
            </el-form-item>
          </el-form>
        </div>
        <el-card
          v-loading="loading"
          v-loading.lock="loading"
          element-loading-text="載入中..."
          style="float: left; width: 60%"
        >
          <el-form
            ref="editForm"
            :model="editForm"
          >
            <el-form-item
              label="客服人員處理備註："
              :label-width="formLabelWidth"
              prop="HandleNote"
            >
              <el-input
                v-model="editForm.HandleNote"
                placeholder="請輸入客服人員處理備註"
                type="textarea"
                :rows="8"
                clearable
              />
            </el-form-item>
            <el-form-item
              label="指定通知對象郵箱："
              :label-width="formLabelWidth"
              prop="HandleNote"
              style="width: 70%"
            />
            <el-row>
              <el-col :span="14">
                <el-form-item
                  label="部門："
                  label-width="80px"
                >
                  <el-cascader
                    :key="cascaderKey"
                    v-model="dept"
                    :options="selectOrganize"
                    filterable
                    :props="{
                      label: 'FullName',
                      value: 'Id',
                      children: 'Children',
                      emitPath: false,
                      checkStrictly: true,
                      expandTrigger: 'hover'
                    }"
                    clearable
                    style="width: 100%"
                    @change="handleSelectOrganizeChange"
                  />
                </el-form-item>
              </el-col>
              <el-col :span="6">
                <el-form-item
                  label="人員："
                  label-width="80px"
                >
                  <el-select
                    v-model="personnel"
                    placeholder="請選擇"
                    style="width: 120px"
                    filterable
                    searchable
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
            <el-row>
              <el-form-item label-width="80px">
                <el-button-group>
                  <el-button
                    type="success"
                    size="small"
                    icon="el-icon-plus"
                    @click="AddNoticeMail()"
                  >加 入</el-button>
                  <el-button
                    type="danger"
                    size="small"
                    icon="el-icon-delete"
                    @click="deletePhysics()"
                  >刪 除</el-button>
                </el-button-group>
              </el-form-item>
            </el-row>
            <el-form-item label-width="80px">
              <el-table
                ref="gridtable"
                v-loading="noticeLoading"
                :data="noticeMailTableData"
                border
                stripe
                highlight-current-row
                style="width: 55%"
                @select="handleSelectChangeNoticeMail"
                @select-all="handleSelectAllChangeNoticeMail"
              >
                <el-table-column
                  type="selection"
                  width="40"
                />
                <el-table-column
                  prop="DeptName"
                  label="部門" align="center"
                />
                <el-table-column
                  prop="UserName"
                  label="姓名" align="center"
                />
              </el-table>
            </el-form-item>
          </el-form>
        </el-card>
      </div>
      <div class="rightbtn">
        <el-button
          type="primary"
          size="small"
          icon="el-icon-paperclip"
          @click="saveEditForm()"
        >儲存</el-button>
        <el-button
          type="primary"
          size="small"
          icon="el-icon-message"
          @click="AddSendMailInfo()"
        >通知</el-button>
        <el-button
          type="primary"
          size="small"
          icon="el-icon-s-promotion"
          @click="SendTrail()"
        >送審</el-button>
        <el-button
          type="primary"
          size="small"
          icon="el-icon-s-check"
          @click="EndCase()"
        >結案</el-button>
        <el-button
          size="small" icon="el-icon-close" @click="cancel"
        >關閉</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import {
  getComplaintListWithPager,
  AddNoticeMail,
  GetNoticeMailListByCCode,
  getComplaintDetail,
  saveComplaint,
  SendTrial,
  EndCase,
  deleteComplaint
} from '@/api/chaochi/complaint/complaintservice';
import { saveSendMailInfo } from '@/api/chaochi/sendmailinfo/sendmailinfoservice';
import { GetAllOrgForSelect } from '@/api/security/organizeservice';
import { GetUserListByDept, getAllUserList } from '@/api/security/userservice';
import { getAllOrganizeTreeTable } from '@/api/security/organizeservice';
import elDragDialog from '@/directive/el-drag-dialog'; // 彈窗可移動
import DatePickerE from '@/components/RocDatepickerE';
export default {
  name: 'Complaint', // 需與菜單的功能編碼一致
  directives: { elDragDialog },
  components: { DatePickerE },
  data() {
    return {
      deptOptions: [],
      dept: '',
      personnelOptions: [],
      personnel: '',
      searchform: {
        CCode: '',
        ComplaintCreatorTime: '',
        State: '',
        ComplaintType: '',
        ComplaintDept: '',
        Complainee: '',
        CId: ''
      },
      tableData: [],
      noticeMailTableData: [],
      tableloading: true,
      loading: false,
      noticeLoading: false,
      StateOptions: [
        {
          value: '待處理',
          label: '待處理'
        },
        {
          value: '處理中',
          label: '處理中'
        },
        {
          value: '已處理',
          label: '已處理'
        },
        {
          value: '已結案',
          label: '已結案'
        }
      ],
      ComplaintTypeOptions: [
        {
          value: '服務不周',
          label: '服務不周'
        },
        {
          value: '合約糾紛',
          label: '合約糾紛'
        },
        {
          value: '提供意見',
          label: '提供意見'
        },
        {
          value: '尋求資訊援助',
          label: '尋求資訊援助'
        },
        {
          value: '其它',
          label: '其它'
        }
      ],
      pagination: {
        currentPage: 1,
        pagesize: 20,
        pageTotal: 0
      },
      sortableData: {
        order: 'desc',
        sort: 'CreatorTime'
      },
      dialogEditFormVisible: false,
      editFormTitle: '',
      editForm: {},
      rules: {},
      formLabelWidth: '145px',
      currentId: '',
      currentSelected: [],
      currentIdNoticeMail: '',
      currentSelectedNoticeMail: [],
      selectOrganize: [],
      cascaderKey: 0,
      selectedOrganizeOptions: ''
    };
  },
  watch: {
    dept: {
      handler(a) {
        if (a === '' || a === null) {
          getAllUserList().then((res) => {
            this.personnelOptions = res.ResData;
          });
        } else {
          GetUserListByDept(a).then((res) => {
            this.personnelOptions = res.ResData;
          });
        }
      }
    }
  },
  created() {
    this.pagination.currentPage = 1;
    this.InitDictItem();
    this.loadTableData();
  },
  methods: {
    /**
     * 初始化數據
     */
    InitDictItem() {
      GetAllOrgForSelect().then((res) => {
        this.deptOptions = res.ResData;
        // this.Reload();
      });
      getAllUserList().then((res) => {
        this.personnelOptions = res.ResData;
      });
      getAllOrganizeTreeTable().then((res) => {
        ++this.cascaderKey;
        this.selectOrganize = res.ResData;
      });
      getAllOrganizeTreeTable().then((res) => {
        this.treeOrganizeData = res.ResData;
      });
    },
    SendTrail() {
      SendTrial(this.editForm).then((res) => {
        if (res.Success) {
          this.$message.success('送審成功');
        } else {
          this.$message.error('失敗');
        }
      });
    },
    EndCase() {
      EndCase(this.editForm).then((res) => {
        if (res.Success) {
          this.$message.success('結案成功');
        } else {
          this.$message.error('失敗');
        }
      });
    },
    AddSendMailInfo() {
      const data = {
        CCode: this.editForm.CCode,
        Name: this.editForm.Name,
        ContactPhone: this.editForm.ContactPhone,
        ContactMail: this.editForm.ContactMail,
        ContactDatetime: this.editForm.ContactDatetime,
        ComplaintType: this.editForm.ComplaintType,
        ComplaintDept: this.editForm.ComplaintDept,
        Complainee: this.editForm.Complainee,
        CId: this.editForm.CId,
        ComplaintReason: this.editForm.ComplaintReason,
        ProvideAdvice: this.editForm.ProvideAdvice,
        State: this.editForm.State,
        HandleUser: this.editForm.HandleUser,
        ComplaintCreatorTime: this.editForm.ComplaintCreatorTime,
        SendTrialTime: this.editForm.SendTrialTime,
        EndCaseTime: this.editForm.EndCaseTime,
        HandleNote: this.editForm.HandleNote,
        SendMailCount: this.editForm.SendMailCount,
        CreatorTime: this.editForm.CreatorTime,
        CreatorUserId: this.editForm.CreatorUserId,
        LastModifyTime: this.editForm.LastModifyTime,
        LastModifyUserId: this.editForm.LastModifyUserId,

        Id: this.currentId
      };
      var url = 'Complaint/Update';
      this.loading = true;
      saveComplaint(data, url)
        .then((res) => {
          if (res.Success) {
            const sendMailInfo = {
              Subject: `客訴單通知 單號${this.editForm.CCode}`,
              Body: this.editForm.HandleNote
            };
            saveSendMailInfo(sendMailInfo, this.editForm.CCode).then((res) => {
              if (res.Success) {
                this.$message.success('已成功通知該對象');
              } else {
                this.$message.error('失敗');
              }
            });
          } else {
            this.$message.error('失敗');
          }
        })
        .finally(() => {
          this.loading = false;
        });
    },
    AddNoticeMail() {
      if (this.personnel === '' || this.personnel === null) {
        this.$message.error('請選擇人員');
        return;
      }
      const data = {
        CCode: this.editForm.CCode,
        UserId: this.personnel
      };
      this.noticeLoading = true;
      AddNoticeMail(data)
        .then((res) => {
          if (res.Success) {
            this.$message.success('新增成功');
            this.personnel = '';
          } else {
            this.$message.error('失敗');
          }
        })
        .finally(() => {
          GetNoticeMailListByCCode(this.editForm.CCode)
            .then((res) => {
              this.noticeMailTableData = res.ResData;
            })
            .finally(() => {
              this.noticeLoading = false;
            });
        });
    },
    // 取消按鈕
    cancel() {
      this.dialogEditFormVisible = false;
      this.currentSelected = '';
      this.loadTableData();
      this.InitDictItem();
      this.reset();
    },
    // 表單重置
    reset() {
      this.editForm = {
        CCode: '',
        Name: '',
        ContactPhone: '',
        ContactMail: '',
        ContactDatetime: '',
        ComplaintType: '',
        ComplaintDept: '',
        Complainee: '',
        CId: '',
        ComplaintReason: '',
        ProvideAdvice: '',
        State: '',
        HandleUserName: '',
        ComplaintCreatorTime: '',
        SendTrialTime: '',
        EndCaseTime: '',
        HandleNote: '',
        SendMailCount: '',
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
      getComplaintListWithPager(seachdata).then((res) => {
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
      getComplaintDetail(this.currentId).then((res) => {
        this.editForm = res.ResData;
        GetNoticeMailListByCCode(this.editForm.CCode)
          .then((res) => {
            this.noticeMailTableData = res.ResData;
          })
          .finally(() => {
            this.loading = false;
          });
      });
    },
    /**
     * 新增/修改保存
     */
    saveEditForm() {
      this.$refs['editForm'].validate((valid) => {
        if (valid) {
          const data = {
            CCode: this.editForm.CCode,
            Name: this.editForm.Name,
            ContactPhone: this.editForm.ContactPhone,
            ContactMail: this.editForm.ContactMail,
            ContactDatetime: this.editForm.ContactDatetime,
            ComplaintType: this.editForm.ComplaintType,
            ComplaintDept: this.editForm.ComplaintDept,
            Complainee: this.editForm.Complainee,
            CId: this.editForm.CId,
            ComplaintReason: this.editForm.ComplaintReason,
            ProvideAdvice: this.editForm.ProvideAdvice,
            State: this.editForm.State,
            HandleUser: this.editForm.HandleUser,
            ComplaintCreatorTime: this.editForm.ComplaintCreatorTime,
            SendTrialTime: this.editForm.SendTrialTime,
            EndCaseTime: this.editForm.EndCaseTime,
            HandleNote: this.editForm.HandleNote,
            SendMailCount: this.editForm.SendMailCount,
            CreatorTime: this.editForm.CreatorTime,
            CreatorUserId: this.editForm.CreatorUserId,
            LastModifyTime: this.editForm.LastModifyTime,
            LastModifyUserId: this.editForm.LastModifyUserId,

            Id: this.currentId
          };

          var url = 'Complaint/Insert';
          if (this.currentId !== '') {
            url = 'Complaint/Update';
          }
          this.loading = true;
          saveComplaint(data, url)
            .then((res) => {
              if (res.Success) {
                this.$message({
                  message: '儲存成功',
                  type: 'success'
                });
                // this.dialogEditFormVisible = false;
                // this.currentSelected = '';
                // this.loadTableData();
                // this.InitDictItem();
              } else {
                this.$message({
                  message: res.ErrMsg,
                  type: 'error'
                });
              }
            })
            .finally(() => {
              this.loading = false;
            });
        } else {
          return false;
        }
      });
    },
    deletePhysics: function() {
      if (this.currentSelectedNoticeMail.length === 0) {
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
            that.noticeLoading = true;
            var currentIds = [];
            that.currentSelectedNoticeMail.forEach((element) => {
              currentIds.push(element.Id);
            });
            const data = {
              Ids: currentIds
            };
            return deleteComplaint(data);
          })
          .then((res) => {
            if (res.Success) {
              that.$message({
                message: '恭喜你，刪除成功',
                type: 'success'
              });
              that.currentSelectedNoticeMail = '';
              GetNoticeMailListByCCode(that.editForm.CCode)
                .then((res) => {
                  that.noticeMailTableData = res.ResData;
                })
                .finally(() => {
                  that.noticeLoading = false;
                });
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
     *選擇組織
     */
    handleSelectOrganizeChange: function() {
      console.log(this.selectedOrganizeOptions);
      this.editFrom.OrganizeId = this.selectedOrganizeOptions;
    },
    handleSelectChange: function(selection, row) {
      this.currentSelected = selection;
    },
    handleSelectAllChange: function(selection) {
      this.currentSelected = selection;
    },
    handleSelectChangeNoticeMail: function(selection, row) {
      this.currentSelectedNoticeMail = selection;
    },
    handleSelectAllChangeNoticeMail: function(selection) {
      this.currentSelectedNoticeMail = selection;
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
