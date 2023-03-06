<template>
  <div class="app-container">
    <div class="filter-container">
      <el-card>
        <el-form ref="searchform" :inline="true" :model="searchform" class="demo-form-inline" size="small">
          <el-form-item label="任務名稱：">
            <el-input v-model="searchform.name" clearable placeholder="任務名稱或分組" />
          </el-form-item>
          <el-form-item>
            <el-button type="primary" @click="handleSearch()">查詢</el-button>
          </el-form-item>
        </el-form>
      </el-card>
    </div>
    <el-card>
      <div class="list-btn-container">
        <el-button-group>
          <el-button v-hasPermi="['TaskManager/Add']" type="primary" icon="el-icon-plus" size="small" @click="ShowEditOrViewDialog()">新增</el-button>
          <el-button v-hasPermi="['TaskManager/Edit']" type="primary" icon="el-icon-edit" class="el-button-modify" size="small" @click="ShowEditOrViewDialog('edit')">修改</el-button>
          <el-button v-hasPermi="['TaskManager/Enable']" type="info" icon="el-icon-video-pause" size="small" @click="setEnable('0')">禁用</el-button>
          <el-button v-hasPermi="['TaskManager/Enable']" type="success" icon="el-icon-video-play" size="small" @click="setEnable('1')">啟用</el-button>
          <el-button v-hasPermi="['TaskManager/DeleteSoft']" type="warning" icon="el-icon-delete" size="small" @click="deleteSoft('0')">刪除暫存</el-button>
          <el-button v-hasPermi="['TaskManager/Delete']" type="danger" icon="el-icon-delete" size="small" @click="deletePhysics()">刪除</el-button>
          <el-button v-hasPermi="['TaskManager/ChangeStatus']" type="info" icon="el-icon-video-pause" size="small" @click="setStatus('0')">暫停</el-button>
          <el-button v-hasPermi="['TaskManager/ChangeStatus']" type="success" icon="el-icon-video-play" size="small" @click="setStatus('1')">啟動</el-button>
          <el-button type="default" icon="el-icon-refresh" size="small" @click="loadTableData()">更新</el-button>
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
        :default-sort="{ prop: 'Id', order: 'ascending' }"
        @select="handleSelectChange"
        @select-all="handleSelectAllChange"
        @sort-change="handleSortChange"
        @row-click="handleRowClick"
      >
        <el-table-column type="selection" width="30" />
        <el-table-column prop="Id" label="任務ID" sortable="custom" width="150" />
        <el-table-column prop="TaskName" label="任務名稱" sortable="custom" width="150" />
        <el-table-column prop="GroupName" label="分組名稱" sortable="custom" width="150" />
        <el-table-column prop="Cron" label="Cron表達式" sortable="custom" width="130" />
        <el-table-column label="狀態" sortable="custom" width="90" prop="Status" align="center">
          <template slot-scope="scope">
            <el-tag :type="scope.row.Status === 1 ? 'success' : 'danger'" disable-transitions>{{ scope.row.Status === 1 ? "正在運行" : "暫停" }}</el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="Description" label="簡述" sortable="custom" width="110" />
        <el-table-column prop="JobCallAddress" label="任務地址" sortable="custom" width="180" />
        <el-table-column prop="StartTime" label="任務開始時間" sortable width="160" />
        <el-table-column prop="EndTime" label="任務結束時間" sortable width="160" />
        <el-table-column prop="LastRunTime" label="最近執行時間" sortable width="160" />
        <el-table-column prop="NextRunTime" label="下次執行時間" sortable width="160" />
        <el-table-column prop="RunCount" label="執行次數" sortable="custom" width="110" />
        <el-table-column prop="ErrorCount" label="出錯次數" sortable="custom" width="110" />
        <el-table-column prop="LastErrorTime" label="出錯時間" sortable width="160" />
        <el-table-column label="是否啟用" sortable="custom" width="120" prop="EnabledMark" align="center">
          <template slot-scope="scope">
            <el-tag :type="scope.row.EnabledMark === true ? 'success' : 'info'" disable-transitions>{{ scope.row.EnabledMark === true ? "啟用" : "禁用" }}</el-tag>
          </template>
        </el-table-column>
        <el-table-column label="是否刪除" sortable="custom" width="120" prop="DeleteMark" align="center">
          <template slot-scope="scope">
            <el-tag :type="scope.row.DeleteMark === true ? 'danger' : 'success'" disable-transitions>{{ scope.row.DeleteMark === true ? "已刪除" : "否" }}</el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="CreatorTime" label="建立時間" sortable width="160" />
        <el-table-column prop="LastModifyTime" label="更新時間" sortable width="160" />
        <el-table-column fixed="right" label="操作" width="100">
          <template slot-scope="scope">
            <el-button type="text" size="small" @click="handleShowLogs(scope.row)">查看日志</el-button>
          </template>
        </el-table-column>
      </el-table>
      <div class="pagination-container">
        <el-pagination background :current-page="pagination.currentPage" :page-sizes="[5, 10, 20, 50, 100, 200, 300, 400]" :page-size="pagination.pagesize" layout="total, sizes, prev, pager, next, jumper" :total="pagination.pageTotal" @size-change="handleSizeChange" @current-change="handleCurrentChange" />
      </div>
    </el-card>
    <el-dialog ref="dialogEditForm" :title="editFormTitle" :visible.sync="dialogEditFormVisible" width="880px" append-to-body>
      <el-form ref="editFrom" :model="editFrom" :rules="rules">
        <el-row>
          <el-col :span="12">
            <el-form-item label="任務名稱" :label-width="formLabelWidth" prop="TaskName">
              <el-input v-model="editFrom.TaskName" placeholder="請輸入任務名稱" autocomplete="off" clearable />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="分組名稱" :label-width="formLabelWidth" prop="GroupName">
              <el-input v-model="editFrom.GroupName" placeholder="請輸入分組名稱" autocomplete="off" clearable />
            </el-form-item>
          </el-col>
          <el-col :span="24">
            <el-form-item label="起訖時間" :label-width="formLabelWidth" prop="StartEndTime">
              <el-date-picker v-model="editFrom.StartEndTime" type="datetimerange" start-placeholder="開始時間" end-placeholder="結束結束" :default-time="['12:00:00']" />
            </el-form-item>
          </el-col>
          <el-col :span="24">
            <el-form-item label="Cron表達式" :label-width="formLabelWidth" prop="Cron">
              <cron-input v-model="editFrom.Cron" placeholder="請輸入Cron表達式" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="執行方式" :label-width="formLabelWidth" prop="IsLocal">
              <el-radio-group v-model="editFrom.IsLocal" @change="changeIsLocal">
                <el-radio :label="true">本地連結</el-radio>
                <el-radio :label="false">外部連結</el-radio>
              </el-radio-group>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item v-if="isShowSelect" label="任務IP" :label-width="formLabelWidth" prop="JobCallAddress">
              <el-select v-model="editFrom.JobCallAddress" clearable filterable placeholder="請輸入任務IP" style="width: 300px">
                <el-option v-for="item in selectLocalTask" :key="item.FullName" :label="item.FullName" :value="item.FullName" />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item v-if="!isShowSelect" label="任務IP" :label-width="formLabelWidth" prop="JobCallAddress">
              <el-input v-model="editFrom.JobCallAddress" placeholder="請輸入外部任務IP" autocomplete="off" clearable />
            </el-form-item>
          </el-col>
          <el-col :span="24">
            <el-form-item label="任務參數" :label-width="formLabelWidth" prop="JobCallParams">
              <el-input v-model="editFrom.JobCallParams" type="textarea" placeholder="請輸入任務參數，JSON格式,為空時請求訪問方式為get，反之為post" autocomplete="off" clearable />
            </el-form-item>
          </el-col>
          <el-col :span="24">
            <el-form-item label="說明" :label-width="formLabelWidth" prop="Description">
              <el-input v-model="editFrom.Description" type="textarea" autocomplete="off" clearable />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="郵件通知" :label-width="formLabelWidth" prop="IsSendMail">
              <el-radio-group v-model="editFrom.SendMail">
                <el-radio :label="0">不通知</el-radio>
                <el-radio :label="1">異常通知</el-radio>
                <el-radio :label="2">所有通知</el-radio>
              </el-radio-group>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item v-if="editFrom.SendMail != '0'" label="Email地址" :label-width="formLabelWidth" prop="EmailAddress">
              <el-input v-model="editFrom.EmailAddress" placeholder="接收通知郵件多個用英文逗號隔開，為空默認系統配置郵箱" autocomplete="off" clearable />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="是否啟用" :label-width="formLabelWidth" prop="EnabledMark">
              <el-radio-group v-model="editFrom.EnabledMark">
                <el-radio :label="true">是</el-radio>
                <el-radio :label="false">否</el-radio>
              </el-radio-group>
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogEditFormVisible = false">關 閉</el-button>
        <el-button type="primary" @click="saveEditForm()">確 認</el-button>
      </div>
    </el-dialog>

    <el-dialog ref="dialogShowLogForm" :title="editFormTitle + '任務日志最近40條記錄'" :visible.sync="dialogShowLogFormVisible" width="940px">
      <el-timeline>
        <el-timeline-item v-for="(activity, index) in activities" :key="index" :color="activity.Color" :timestamp="activity.CreatorTime">
          {{ activity.Description }}
        </el-timeline-item>
      </el-timeline>
    </el-dialog>
  </div>
</template>

<script>

import {
  getTaskManagerListWithPager, getTaskManagerDetail,
  saveTaskManager, setTaskManagerEnable, deleteSoftTaskManager,
  deleteTaskManager, changeStatus, getLocalTaskJobs, getTaskJobLogListWithPager
} from '@/api/security/taskmanager'

import CronInput from '@/components/cron/cron-input'

export default {
  name: 'TaskManager',
  components: {
    CronInput
  },
  data() {
    return {
      searchform: {
        name: ''
      },
      loadBtnFunc: [],
      selectLocalTask: [],
      tableData: [],
      tableloading: true,
      pagination: {
        currentPage: 1,
        pagesize: 20,
        pageTotal: 0
      },
      sortableData: {
        order: 'desc',
        sort: 'Id'
      },
      dialogEditFormVisible: false,
      dialogShowLogFormVisible: false,
      editFormTitle: '',
      editFrom: {},
      rules: {
        TaskName: [
          { required: true, message: '請輸入任務名稱', trigger: 'blur' },
          { min: 2, max: 50, message: '長度在 2 到 50 個字符', trigger: 'blur' }
        ],
        GroupName: [
          { required: true, message: '請輸入分組名稱', trigger: 'blur' },
          { min: 2, max: 50, message: '長度在 2 到 50 個字符', trigger: 'blur' }
        ],
        StartEndTime: [
          { required: true, message: '請設置任務起止時間', trigger: 'blur' }
        ],
        Cron: [
          { required: true, message: '請輸入Cron表達式', trigger: 'blur' },
          { min: 1, max: 50, message: '長度在 1 到 50 個字符', trigger: 'blur' }
        ],
        JobCallAddress: [
          { required: true, message: '請輸入遠程接口地址', trigger: 'blur' },
          { min: 1, max: 250, message: '長度在 1 到 50 個字符', trigger: 'blur' }
        ]
      },
      formLabelWidth: '120px',
      currentId: '', // 當前操作對象的ID值，主要用于修改
      currentSelected: [],
      isShowSelect: true,
      activities: [],
      reverse: true
    }
  },
  created() {
    this.pagination.currentPage = 1
    this.InitDictItem()
    this.loadTableData()
  },
  methods: {
    /**
     * 初始化數據
     */
    InitDictItem() {
      getLocalTaskJobs().then(res => {
        this.selectLocalTask = res.ResData
      })
    },
    /**
     * 加載頁面table數據
     */
    loadTableData: function() {
      this.tableloading = true
      var seachdata = {
        CurrenetPageIndex: this.pagination.currentPage,
        PageSize: this.pagination.pagesize,
        Keywords: this.searchform.name,
        Order: this.sortableData.order,
        Sort: this.sortableData.sort
      }
      getTaskManagerListWithPager(seachdata).then(res => {
        this.tableData = res.ResData.Items
        this.pagination.pageTotal = res.ResData.TotalItems
        this.tableloading = false
      })
    },
    /**
     * 點擊查詢
     */
    handleSearch: function() {
      this.pagination.currentPage = 1
      this.loadTableData()
    },
    changeIsLocal: function() {
      this.isShowSelect = this.editFrom.IsLocal
      this.editFrom.JobCallAddress = ''
    },
    handleShowLogs: function(row) {
      this.dialogShowLogFormVisible = true
      this.loadJobLogData(row)
    },
    /**
     * 加載頁面table數據
     */
    loadJobLogData: function(row) {
      var seachdata = {
        'Keywords': row.Id
      }
      getTaskJobLogListWithPager(seachdata).then(res => {
        this.activities = res.ResData
      })
    },
    // 表單重置
    reset() {
      if (!this.currentId) {
        this.editFrom = {
          TaskName: '',
          GroupName: '',
          Cron: '',
          JobCallAddress: '',
          JobCallParams: '',
          EnabledMark: true,
          IsLocal: true,
          Description: '',
          SendMail: 1,
          EmailAddress: '',
          StartTime: '',
          EndTime: '',
          StartEndTime: ''
        }
        this.resetForm('editFrom')
      } else {
        this.bindEditInfo()
      }
    },

    /**
     * 新增、修改或查看明細信息（綁定顯示數據）     *
     */
    ShowEditOrViewDialog: function(view) {
      if (view !== undefined) {
        if (this.currentId.length === 0) {
          this.$alert('請選擇一條數據進行編輯/修改', '提示')
        } else {
          this.editFormTitle = '編輯'
          this.dialogEditFormVisible = true
        }
      } else {
        this.editFormTitle = '新增'
        this.currentId = ''
        this.dialogEditFormVisible = true
      }
      this.reset()
    },
    bindEditInfo: function() {
      getTaskManagerDetail(this.currentId).then(res => {
        this.editFrom = res.ResData
        this.isShowSelect = res.ResData.IsLocal
        this.editFrom.StartEndTime = [res.ResData.StartTime, res.ResData.EndTime]
      })
    },
    /**
     * 新增/修改保存
     */
    saveEditForm() {
      this.$refs['editFrom'].validate((valid) => {
        if (valid) {
          const data = {
            'TaskName': this.editFrom.TaskName,
            'GroupName': this.editFrom.GroupName,
            'Cron': this.editFrom.Cron,
            'JobCallAddress': this.editFrom.JobCallAddress,
            'JobCallParams': this.editFrom.JobCallParams,
            'StartTime': this.editFrom.StartEndTime[0],
            'EndTime': this.editFrom.StartEndTime[1],
            'IsLocal': this.editFrom.IsLocal,
            'Description': this.editFrom.Description,
            'SendMail': this.editFrom.SendMail,
            'EmailAddress': this.editFrom.EmailAddress,
            'EnabledMark': this.editFrom.EnabledMark,
            'Id': this.currentId
          }
          var url = 'TaskManager/Insert'
          if (this.currentId !== '') {
            url = 'TaskManager/Update'
          }
          saveTaskManager(data, url).then(res => {
            if (res.Success) {
              this.$message({
                message: '恭喜你，操作成功',
                type: 'success'
              })
              this.dialogEditFormVisible = false
              this.currentSelected = ''
              this.editFrom.StartEndTime = ''
              this.resetForm('editFrom')
              this.loadTableData()
              this.InitDictItem()
            } else {
              this.$message({
                message: res.ErrMsg,
                type: 'error'
              })
            }
          })
        } else {
          return false
        }
      })
    },
    setEnable: function(val) {
      if (this.currentSelected.length === 0) {
        this.$alert('請先選擇要操作的數據', '提示')
        return false
      } else {
        var currentIds = []
        this.currentSelected.forEach(element => {
          currentIds.push(element.Id)
        })
        const data = {
          Ids: currentIds,
          Flag: val
        }
        setTaskManagerEnable(data).then(res => {
          if (res.Success) {
            this.$message({
              message: '恭喜你，操作成功',
              type: 'success'
            })
            this.currentSelected = ''
            this.loadTableData()
          } else {
            this.$message({
              message: res.ErrMsg,
              type: 'error'
            })
          }
        })
      }
    },
    deleteSoft: function(val) {
      if (this.currentSelected.length === 0) {
        this.$alert('請先選擇要操作的數據', '提示')
        return false
      } else {
        var currentIds = []
        this.currentSelected.forEach(element => {
          currentIds.push(element.Id)
        })
        const data = {
          Ids: currentIds,
          Flag: val
        }
        deleteSoftTaskManager(data).then(res => {
          if (res.Success) {
            this.$message({
              message: '恭喜你，操作成功',
              type: 'success'
            })
            this.currentSelected = ''
            this.loadTableData()
          } else {
            this.$message({
              message: res.ErrMsg,
              type: 'error'
            })
          }
        })
      }
    },
    deletePhysics: function() {
      if (this.currentSelected.length === 0) {
        this.$alert('請先選擇要操作的數據', '提示')
        return false
      } else {
        var currentIds = []
        this.currentSelected.forEach(element => {
          currentIds.push(element.Id)
        })
        this.$confirm('是否確認刪除所選的數據項?', '警告', {
          confirmButtonText: '確定',
          cancelButtonText: '取消',
          type: 'warning'
        }).then(function() {
          const data = {
            Ids: currentIds
          }
          return deleteTaskManager(data)
        }).then(res => {
          if (res.Success) {
            this.$message({
              message: '恭喜你，刪除成功',
              type: 'success'
            })
            this.currentSelected = ''
            this.loadTableData()
          } else {
            this.$message({
              message: res.ErrMsg,
              type: 'error'
            })
          }
        })
      }
    },
    // 啟動/暫停
    setStatus: function(val) {
      if (this.currentSelected.length === 0) {
        this.$alert('請先選擇要操作的數據', '提示')
        return false
      } else {
        var currentIds = this.currentSelected[0].Id
        const data = {
          Id: currentIds,
          Status: val
        }
        changeStatus(data).then(res => {
          if (res.Success) {
            this.$message({
              message: '恭喜你，操作成功',
              type: 'success'
            })
            this.currentSelected = ''
            this.loadTableData()
          } else {
            this.$message({
              message: res.ErrMsg,
              type: 'error'
            })
          }
        })
      }
    },
    /**
     * 當表格的排序條件發生變化的時候會觸發該事件
     */
    handleSortChange: function(column) {
      this.sortableData.sort = column.prop
      if (column.order === 'ascending') {
        this.sortableData.order = 'asc'
      } else {
        this.sortableData.order = 'desc'
      }
      this.loadTableData()
    },
    /**
     * 當用戶手動勾選checkbox數據行事件
     */
    handleSelectChange: function(selection, row) {
      this.currentSelected = selection
      this.currentId = row.Id
    },
    /**
     * 當用戶手動勾選全選checkbox事件
     */
    handleSelectAllChange: function(selection) {
      this.currentSelected = selection
    },
    /**
     * 單擊行
     */
    handleRowClick: function(row, column, event) {
      this.currentId = row.Id
    },
    /**
     * 選擇每頁顯示數量
     */
    handleSizeChange(val) {
      this.pagination.pagesize = val
      this.pagination.currentPage = 1
      this.loadTableData()
    },
    /**
     * 選擇當頁面
     */
    handleCurrentChange(val) {
      this.pagination.currentPage = val
      this.loadTableData()
    }
  }
}
</script>

<style>
</style>
