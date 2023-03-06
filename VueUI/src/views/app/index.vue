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
          <el-form-item label="應用名稱：">
            <el-input v-model="searchform.name" clearable placeholder="應用AppId或授權Url" />
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

          <el-button
            v-hasPermi="['APP/Add']"
            type="primary"
            icon="el-icon-plus"
            size="small"
            @click="ShowEditOrViewDialog()"
          >新增</el-button>
          <el-button
            v-hasPermi="['APP/Edit']"
            type="primary"
            icon="el-icon-edit"
            class="el-button-modify"
            size="small"
            @click="ShowEditOrViewDialog('edit')"
          >修改</el-button>
          <el-button
            v-hasPermi="['APP/Enable']"
            type="info"
            icon="el-icon-video-pause"
            size="small"
            @click="setEnable('0')"
          >禁用</el-button>
          <el-button
            v-hasPermi="['APP/Enable']"
            type="success"
            icon="el-icon-video-play"
            size="small"
            @click="setEnable('1')"
          >啟用</el-button>
          <el-button
            v-hasPermi="['APP/DeleteSoft']"
            type="warning"
            icon="el-icon-delete"
            size="small"
            @click="deleteSoft('0')"
          >刪除暫存</el-button>
          <el-button
            v-hasPermi="['APP/Delete']"
            type="danger"
            icon="el-icon-delete"
            size="small"
            @click="deletePhysics()"
          >刪除</el-button>

          <el-button
            v-hasPermi="['APP/ResetAppSecret']"
            type="primary"
            icon="el-icon-refresh-right"
            size="small"
            @click="haddlerResetAppSecret()"
          >重置AppSecret</el-button>

          <el-button
            v-hasPermi="['APP/ResetEncodingAESKey']"
            type="primary"
            icon="el-icon-refresh-right"
            size="small"
            @click="haddlerResetEncodingAESKey()"
          >重置消息密鑰</el-button>
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
        :default-sort="{prop: 'SortCode', order: 'ascending'}"
        @select="handleSelectChange"
        @select-all="handleSelectAllChange"
        @sort-change="handleSortChange"
      >
        <el-table-column type="selection" width="30" />
        <el-table-column prop="AppId" label="應用Id" sortable="custom" width="130" fixed />
        <el-table-column
          prop="AppSecret"
          label="AppSecret"
          sortable="custom"
          width="300"
          fixed
        />
        <el-table-column
          prop="Token"
          label="Token"
          sortable="custom"
          width="150"
        />
        <el-table-column
          label="消息加密"
          sortable="custom"
          width="120"
          prop="IsOpenAEKey"
          align="center"
        >
          <template slot-scope="scope">
            <el-tag
              :type="scope.row.IsOpenAEKey === true ? 'success' : 'info'"
              disable-transitions
            >{{ scope.row.IsOpenAEKey===true?'啟用':'禁用' }}</el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="RequestUrl" label="授權URL" sortable="custom" width="520" />
        <el-table-column
          label="是否啟用"
          sortable="custom"
          width="90"
          prop="EnabledMark"
          align="center"
        >
          <template slot-scope="scope">
            <el-tag
              :type="scope.row.EnabledMark === true ? 'success' : 'info'"
              disable-transitions
            >{{ scope.row.EnabledMark===true?'啟用':'禁用' }}</el-tag>
          </template>
        </el-table-column>
        <el-table-column
          label="是否刪除"
          sortable="custom"
          width="120"
          prop="DeleteMark"
          align="center"
        >
          <template slot-scope="scope">
            <el-tag
              :type="scope.row.DeleteMark === true ? 'danger' : 'success'"
              disable-transitions
            >{{ scope.row.DeleteMark===true?'已刪除':'否' }}</el-tag>
          </template>
        </el-table-column>
        <el-table-column
          prop="CreatorTime"
          label="建立時間"
          sortable
          width="160"
        />
        <el-table-column
          prop="LastModifyTime"
          label="更新時間"
          sortable
          width="160"
        />
      </el-table>
      <div class="pagination-container">
        <el-pagination
          background
          :current-page="pagination.currentPage"
          :page-sizes="[5,10,20,50,100, 200, 300, 400]"
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
      :title="editFormTitle"
      :visible.sync="dialogEditFormVisible"
      width="640px"
    >
      <el-form ref="editFrom" :model="editFrom" :rules="rules">
        <el-form-item label="應用Id" :label-width="formLabelWidth" prop="AppId">
          <el-input v-model="editFrom.AppId" placeholder="請輸入應用Id" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="應用Secret" :label-width="formLabelWidth" prop="AppSecret">
          <el-input v-model="editFrom.AppSecret" placeholder="系統自動生成" readonly autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="消息加密密鑰" :label-width="formLabelWidth" prop="EncodingAESKey">
          <el-input v-model="editFrom.EncodingAESKey" placeholder="系統自動生成" readonly autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="Token" :label-width="formLabelWidth" prop="Token">
          <el-input v-model="editFrom.Token" placeholder="請輸入Token" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="選項" :label-width="formLabelWidth" prop="">
          <el-checkbox v-model="editFrom.EnabledMark">啟用</el-checkbox>
          <el-checkbox v-model="editFrom.IsOpenAEKey">啟用消息加密</el-checkbox>
        </el-form-item>
        <el-form-item label="授權Url" :label-width="formLabelWidth" prop="RequestUrl">
          <el-input v-model="editFrom.RequestUrl" type="textarea" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="說明" :label-width="formLabelWidth" prop="Description">
          <el-input v-model="editFrom.Description" type="textarea" autocomplete="off" clearable />
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogEditFormVisible = false">關 閉</el-button>
        <el-button type="primary" @click="saveEditForm()">確 認</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import { getAPPListWithPager, getAPPDetail, saveAPP, setAPPEnable,
  deleteSoftAPP, deleteAPP, resetAppSecret, resetEncodingAESKey } from '@/api/developers/appservice'

export default {
  name: 'APP',
  data() {
    return {
      searchform: {
        name: ''
      },
      loadBtnFunc: [],
      selectedOrganizeOptions: '',
      selectOrganize: [],
      selectAPPType: [],
      tableData: [],
      tableloading: true,
      pagination: {
        currentPage: 1,
        pagesize: 20,
        pageTotal: 0
      },
      sortableData: {
        order: 'desc',
        sort: 'AppId'
      },
      dialogEditFormVisible: false,
      editFormTitle: '',
      editFrom: {},
      rules: {
        AppId: [
          { required: true, message: '請輸入應用AppId', trigger: 'blur' },
          { min: 2, max: 50, message: '長度在 6 到 32 個字符', trigger: 'blur' }
        ],
        Token: [
          { required: true, message: '請輸入Token', trigger: 'blur' },
          { min: 2, max: 50, message: '長度在 6 到 32 個字符', trigger: 'blur' }
        ]
      },
      formLabelWidth: '100px',
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
    InitDictItem() {
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
      getAPPListWithPager(seachdata).then(res => {
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

    // 表單重置
    reset() {
      if (!this.currentId) {
        this.editFrom = {
          AppId: '',
          AppSecret: '',
          EncodingAESKey: '',
          RequestUrl: '',
          Token: '',
          IsOpenAEKey: false,
          EnabledMark: true,
          Description: ''
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
        if (this.currentSelected.length > 1 || this.currentSelected.length === 0) {
          this.$alert('請選擇一條數據進行編輯/修改', '提示')
        } else {
          this.currentId = this.currentSelected[0].Id
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
      getAPPDetail(this.currentId).then(res => {
        this.editFrom = res.ResData
      })
    },
    /**
     * 新增/修改保存
     */
    saveEditForm() {
      this.$refs['editFrom'].validate((valid) => {
        if (valid) {
          const data = {
            'AppId': this.editFrom.AppId,
            'AppSecret': this.editFrom.AppSecret,
            'EncodingAESKey': this.editFrom.EncodingAESKey,
            'RequestUrl': this.editFrom.RequestUrl,
            'Token': this.editFrom.Token,
            'IsOpenAEKey': this.editFrom.IsOpenAEKey,
            'EnabledMark': this.editFrom.EnabledMark,
            'Description': this.editFrom.Description,
            'Id': this.currentId
          }
          var url = 'APP/Insert'
          if (this.currentId !== '') {
            url = 'APP/Update'
          }
          saveAPP(data, url).then(res => {
            if (res.Success) {
              this.$message({
                message: '恭喜你，操作成功',
                type: 'success'
              })
              this.dialogEditFormVisible = false
              this.currentSelected = ''
              this.reset()
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
        setAPPEnable(data).then(res => {
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
        this.$confirm('是否確認刪除所選的數據項?', '警告', {
          confirmButtonText: '確定',
          cancelButtonText: '取消',
          type: 'warning'
        }).then(function() {
          const data = {
            Ids: currentIds,
            Flag: val
          }
          return deleteSoftAPP(data)
        }).then(res => {
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
          return deleteAPP(data)
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
    },
    haddlerResetAppSecret: function() {
      if (this.currentSelected.length === 0 || this.currentSelected.length > 1) {
        this.$alert('請選擇一條數據進行重置', '提示')
        return false
      } else {
        const data = {
          id: this.currentSelected[0].Id
        }
        resetAppSecret(data).then(res => {
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
    haddlerResetEncodingAESKey: function() {
      if (this.currentSelected.length === 0 || this.currentSelected.length > 1) {
        this.$alert('請選擇一條數據進行重置', '提示')
        return false
      } else {
        const data = {
          id: this.currentSelected[0].Id
        }
        resetEncodingAESKey(data).then(res => {
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
    }
  }
}
</script>
