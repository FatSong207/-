<template>
  <div class="app-container">
    <div class="filter-container">
      <el-card>
        <el-form ref="searchform" :inline="true" :model="searchform" class="demo-form-inline" size="small">
          <el-form-item label="任務名稱：">
            <el-input v-model="searchform.name" clearable placeholder="任務Id或任務名稱" />
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
          <el-button v-hasPermi="['TaskJobsLog/Delete']" type="danger" icon="el-icon-delete" size="small" @click="deletePhysics()">刪除</el-button>
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
        :default-sort="{ prop: 'CreatorTime', order: 'ascending' }"
        @select="handleSelectChange"
        @select-all="handleSelectAllChange"
        @sort-change="handleSortChange"
      >
        <el-table-column type="selection" width="30" />
        <el-table-column prop="TaskId" label="任務Id" sortable="custom" width="150" />
        <el-table-column prop="TaskName" label="任務名稱" sortable="custom" width="220" />
        <el-table-column prop="JobAction" label="執行動作" sortable="custom" width="120" />
        <el-table-column prop="Status" label="執行狀態" sortable="custom" width="120">
          <template slot-scope="scope">
            <el-tag :type="scope.row.Status === true ? 'success' : 'info'" disable-transitions>{{ scope.row.Status === true ? "正常" : "異常" }}</el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="CreatorTime" label="建立時間" sortable="custom" width="160" />
        <el-table-column prop="Description" label="結果描述" sortable="custom" />
      </el-table>
      <div class="pagination-container">
        <el-pagination background :current-page="pagination.currentPage" :page-sizes="[5, 10, 20, 50, 100, 200, 300, 400]" :page-size="pagination.pagesize" layout="total, sizes, prev, pager, next, jumper" :total="pagination.pageTotal" @size-change="handleSizeChange" @current-change="handleCurrentChange" />
      </div>
    </el-card>
    <el-dialog ref="dialogEditForm" :title="editFormTitle + '{TableNameDesc}'" :visible.sync="dialogEditFormVisible" width="640px">
      <el-form ref="editFrom" :model="editFrom" :rules="rules">
        <el-form-item label="任務Id" :label-width="formLabelWidth" prop="TaskId">
          <el-input v-model="editFrom.TaskId" placeholder="請輸入任務Id" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="任務名稱" :label-width="formLabelWidth" prop="TaskName">
          <el-input v-model="editFrom.TaskName" placeholder="請輸入任務名稱" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="執行動作" :label-width="formLabelWidth" prop="JobAction">
          <el-input v-model="editFrom.JobAction" placeholder="請輸入執行動作" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="執行狀態" :label-width="formLabelWidth" prop="Status">
          <el-radio-group v-model="editFrom.Status">
            <el-radio label="true">是</el-radio>
            <el-radio label="false">否</el-radio>
          </el-radio-group>
        </el-form-item>
        <el-form-item label="結果描述" :label-width="formLabelWidth" prop="Description">
          <el-input v-model="editFrom.Description" placeholder="請輸入結果描述" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="創建時間" :label-width="formLabelWidth" prop="CreatorTime">
          <el-input v-model="editFrom.CreatorTime" placeholder="請輸入創建時間" autocomplete="off" clearable />
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogEditFormVisible = false">取 消</el-button>
        <el-button type="primary" @click="saveEditForm()">確 定</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import {
  getTaskJobsLogListWithPager,
  getTaskJobsLogDetail,
  saveTaskJobsLog,
  setTaskJobsLogEnable,
  deleteSoftTaskJobsLog,
  deleteTaskJobsLog
} from '@/api/security/taskjobslog'

export default {
  name: 'TaskJobsLog',
  data() {
    return {
      searchform: {
        name: ''
      },
      loadBtnFunc: [],
      tableData: [],
      tableloading: true,
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
      editFrom: {
        TaskId: '',
        TaskName: '',
        JobAction: '',
        Status: 'true',
        Description: '',
        CreatorTime: ''
      },
      rules: {
        TaskId: [
          { required: true, message: '請輸入任務Id', trigger: 'blur' },
          {
            min: 2,
            max: 50,
            message: '長度在 2 到 50 個字符',
            trigger: 'blur'
          }
        ],
        Status: [
          {
            required: true,
            message: '請輸入執行狀態 正常、異常',
            trigger: 'blur'
          },
          {
            min: 2,
            max: 50,
            message: '長度在 2 到 50 個字符',
            trigger: 'blur'
          }
        ],
        CreatorTime: [
          { required: true, message: '請輸入創建時間', trigger: 'blur' },
          {
            min: 2,
            max: 50,
            message: '長度在 2 到 50 個字符',
            trigger: 'blur'
          }
        ]
      },
      formLabelWidth: '80px',
      currentId: '', // 當前操作對象的ID值，主要用于修改
      currentSelected: []
    }
  },
  created() {
    this.pagination.currentPage = 1
    this.InitDictItem()
    this.loadTableData()
    this.loadBtnFunc = JSON.parse(localStorage.getItem('yueboncurrentfuns'))
  },
  methods: {
    /**
     * 初始化數據
     */
    InitDictItem() { },
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
      getTaskJobsLogListWithPager(seachdata).then((res) => {
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

    /**
     * 新增、修改或查看明細信息（綁定顯示數據）     *
     */
    ShowEditOrViewDialog: function(view) {
      if (view !== undefined) {
        if (
          this.currentSelected.length > 1 ||
          this.currentSelected.length === 0
        ) {
          this.$alert('請選擇一條數據進行編輯/修改', '提示')
        } else {
          this.currentId = this.currentSelected[0].Id
          this.editFormTitle = '編輯'
          this.dialogEditFormVisible = true
          this.bindEditInfo()
        }
      } else {
        this.editFormTitle = '新增'
        this.currentId = ''
        this.dialogEditFormVisible = true
        this.$refs['editFrom'].resetFields()
      }
    },
    bindEditInfo: function() {
      getTaskJobsLogDetail(this.currentId).then((res) => {
        this.editFrom.TaskId = res.ResData.TaskId
        this.editFrom.TaskName = res.ResData.TaskName
        this.editFrom.JobAction = res.ResData.JobAction
        this.editFrom.Status = res.ResData.Status + ''
        this.editFrom.Description = res.ResData.Description
        this.editFrom.CreatorTime = res.ResData.CreatorTime
      })
    },
    /**
     * 新增/修改保存
     */
    saveEditForm() {
      this.$refs['editFrom'].validate((valid) => {
        if (valid) {
          const data = {
            TaskId: this.editFrom.TaskId,
            TaskName: this.editFrom.TaskName,
            JobAction: this.editFrom.JobAction,
            Status: this.editFrom.Status,
            Description: this.editFrom.Description,
            CreatorTime: this.editFrom.CreatorTime,

            Id: this.currentId
          }
          saveTaskJobsLog(data).then((res) => {
            if (res.Success) {
              this.$message({
                message: '恭喜你，操作成功',
                type: 'success'
              })
              this.dialogEditFormVisible = false
              this.currentSelected = ''
              this.$refs['editFrom'].resetFields()
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
        var currentIds = ''
        this.currentSelected.forEach((element) => {
          currentIds += element.Id + ','
        })
        const data = {
          ids: currentIds,
          bltag: val
        }
        setTaskJobsLogEnable(data).then((res) => {
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
        deleteSoftTaskJobsLog(data).then((res) => {
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
          return deleteTaskJobsLog(data)
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
    },
    /**
     * 當用戶手動勾選全選checkbox事件
     */
    handleSelectAllChange: function(selection) {
      this.currentSelected = selection
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
