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
          <el-form-item label="名稱：">
            <el-input v-model="searchform.keywords" clearable placeholder="名稱" />
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
            v-hasPermi="['Articlecategory/Add']"
            type="primary"
            icon="el-icon-plus"
            size="small"
            @click="ShowEditOrViewDialog()"
          >新增</el-button>
          <el-button
            v-hasPermi="['Articlecategory/Edit']"
            type="primary"
            icon="el-icon-edit"
            class="el-button-modify"
            size="small"
            @click="ShowEditOrViewDialog('edit')"
          >修改</el-button>
          <el-button
            v-hasPermi="['Articlecategory/Enable']"
            type="info"
            icon="el-icon-video-pause"
            size="small"
            @click="setEnable('0')"
          >禁用</el-button>
          <el-button
            v-hasPermi="['Articlecategory/Enable']"
            type="success"
            icon="el-icon-video-play"
            size="small"
            @click="setEnable('1')"
          >啟用</el-button>
          <el-button
            v-hasPermi="['Articlecategory/DeleteSoft']"
            type="warning"
            icon="el-icon-delete"
            size="small"
            @click="deleteSoft('0')"
          >軟刪除</el-button>
          <el-button
            v-hasPermi="['Articlecategory/Delete']"
            type="danger"
            icon="el-icon-delete"
            size="small"
            @click="deletePhysics()"
          >刪除</el-button>
          <el-button type="default" icon="el-icon-refresh" size="small" @click="loadTableData()">更新</el-button>
        </el-button-group>
      </div>
      <el-table
        ref="gridtable"
        v-loading="tableloading"
        :data="tableData"
        row-key="Id"
        border
        stripe
        highlight-current-row
        style="width: 100%"
        default-expand-all
        :tree-props="{ children: 'Children' }"
        @select="handleSelectChange"
        @select-all="handleSelectAllChange"
        @sort-change="handleSortChange"
      >
        <el-table-column type="selection" width="30" />
        <el-table-column prop="Title" label="標題" sortable="custom" />
        <el-table-column prop="SortCode" label="排序" sortable="custom" width="120" />
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
        <el-table-column prop="CreatorTime" label="創建時間" sortable="custom" width="180" />

      </el-table>
    </el-card>
    <el-dialog
      ref="dialogEditForm"
      v-el-drag-dialog
      :title="editFormTitle"
      :visible.sync="dialogEditFormVisible"
      width="660px"
    >
      <el-form ref="editFrom" :model="editFrom" :rules="rules">
        <el-form-item label="標題" :label-width="formLabelWidth" prop="Title">
          <el-input v-model="editFrom.Title" placeholder="請輸入標題" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="上級分類" :label-width="formLabelWidth" prop="ParentId">
          <el-cascader
            v-model="selectedCategoryOptions"
            style="width: 500px"
            :options="selectCategory"
            filterable
            :props="{label: 'Title',
                     value: 'Id',
                     children: 'Children',
                     emitPath: false,
                     checkStrictly: true,
                     expandTrigger: 'hover',
            }"
            clearable
            @change="handleSelectCategoryChange"
          />
        </el-form-item>
        <el-form-item label="外網網址" :label-width="formLabelWidth" prop="LinkUrl">
          <el-input v-model="editFrom.LinkUrl" placeholder="請輸入外網網址" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="排序" :label-width="formLabelWidth" prop="SortCode">
          <el-input v-model.number="editFrom.SortCode" placeholder="請輸入排序,默認為99" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="說明" :label-width="formLabelWidth" prop="Description">
          <el-input v-model="editFrom.Description" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="是否啟用" :label-width="formLabelWidth" prop="EnabledMark">
          <el-radio-group v-model="editFrom.EnabledMark">
            <el-radio label="true">是</el-radio>
            <el-radio label="false">否</el-radio>
          </el-radio-group>
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

import { GetAllCategoryTreeTable, getArticlecategoryDetail,
  saveArticlecategory, setArticlecategoryEnable, deleteSoftArticlecategory,
  deleteArticlecategory } from '@/api/cms/articlecategory'

import elDragDialog from '@/directive/el-drag-dialog' // base on element-ui
export default {
  name: 'Articlecategory',
  directives: { elDragDialog },
  data() {
    return {
      searchform: {
        keywords: ''
      },
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
        Description: '',
        EnabledMark: 'true',
        LinkUrl: '',
        ParentId: '',
        SortCode: 99,
        Title: ''
      },
      rules: {
        Title: [
          { required: true, message: '請輸入名稱', trigger: 'blur' },
          { min: 2, max: 50, message: '長度在 2 到 50 個字符', trigger: 'blur' }
        ]
      },
      formLabelWidth: '120px',
      currentId: '', // 當前操作對象的ID值，主要用于修改
      currentSelected: [],
      selectedCategoryOptions: '',
      selectCategory: []
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
    },
    /**
     * 加載頁面table數據
     */
    loadTableData: function() {
      this.tableloading = true
      var seachdata = {
        keyword: this.searchform.keywords
      }
      GetAllCategoryTreeTable(seachdata).then(res => {
        this.tableData = res.ResData
        this.selectCategory = res.ResData
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
     * 選擇分類
     */
    handleSelectCategoryChange: function() {
      this.editFrom.ParentId = this.selectedCategoryOptions
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
          this.bindEditInfo()
        }
      } else {
        this.editFormTitle = '新增'
        this.currentId = ''
        this.dialogEditFormVisible = true
        this.selectedCategoryOptions = ''
        this.$refs['editFrom'].resetFields()
      }
    },
    bindEditInfo: function() {
      getArticlecategoryDetail(this.currentId).then(res => {
        this.editFrom.Description = res.ResData.Description
        this.editFrom.EnabledMark = res.ResData.EnabledMark + ''
        this.editFrom.LinkUrl = res.ResData.LinkUrl
        this.editFrom.ParentId = res.ResData.ParentId
        this.editFrom.SortCode = res.ResData.SortCode
        this.editFrom.Title = res.ResData.Title
        this.selectedCategoryOptions = res.ResData.ParentId
      })
    },
    /**
     * 新增/修改保存
     */
    saveEditForm() {
      this.$refs['editFrom'].validate((valid) => {
        if (valid) {
          const data = {
            'Description': this.editFrom.Description,
            'EnabledMark': this.editFrom.EnabledMark,
            'LinkUrl': this.editFrom.LinkUrl,
            'ParentId': this.editFrom.ParentId,
            'SortCode': this.editFrom.SortCode,
            'Title': this.editFrom.Title,
            'Id': this.currentId
          }

          var url = 'Articlecategory/Insert'
          if (this.currentId !== '') {
            url = 'Articlecategory/Update'
          }
          saveArticlecategory(data, url).then(res => {
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
        var currentIds = []
        this.currentSelected.forEach(element => {
          currentIds.push(element.Id)
        })
        const data = {
          Ids: currentIds,
          Flag: val
        }
        setArticlecategoryEnable(data).then(res => {
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
        deleteSoftArticlecategory(data).then(res => {
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
          return deleteArticlecategory(data)
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
