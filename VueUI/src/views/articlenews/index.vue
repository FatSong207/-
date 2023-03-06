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
          <el-button v-hasPermi="['Articlenews/Add']" type="primary" icon="el-icon-plus" size="small" @click="handleEditDataFrom('add')">新增</el-button>
          <el-button v-hasPermi="['Articlenews/Edit']" type="primary" icon="el-icon-edit" class="el-button-modify" size="small" @click="handleEditDataFrom('edit')">修改</el-button>
          <el-button v-hasPermi="['Articlenews/List']" type="primary" icon="el-icon-view" size="small" @click="handleEditDataFrom('show')">查看</el-button>
          <el-button v-hasPermi="['Articlenews/Enable']" type="info" icon="el-icon-video-pause" size="small" @click="setEnable('0')">禁用</el-button>
          <el-button v-hasPermi="['Articlenews/Enable']" type="success" icon="el-icon-video-play" size="small" @click="setEnable('1')">啟用</el-button>
          <el-button v-hasPermi="['Articlenews/DeleteSoft']" type="warning" icon="el-icon-delete" size="small" @click="deleteSoft('0')">刪除暫存</el-button>
          <el-button v-hasPermi="['Articlenews/Delete']" type="danger" icon="el-icon-delete" size="small" @click="deletePhysics()">刪除</el-button>
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
        <el-table-column prop="Title" label="文章標題" sortable="custom" />
        <el-table-column prop="CategoryName" label="所屬分類" sortable="custom" width="120" />
        <el-table-column prop="SortCode" label="排序" sortable="custom" width="120" />
        <el-table-column label="屬性" sortable="custom" width="230" prop="" align="center">
          <template slot-scope="scope">
            <el-tag v-if="scope.row.IsTop === true" disable-transitions>置頂</el-tag>
            <el-tag v-if="scope.row.IsRed === true" type="success" disable-transitions>推薦</el-tag>
            <el-tag v-if="scope.row.IsHot === true" type="danger" disable-transitions>熱門</el-tag>
            <el-tag v-if="scope.row.IsNew === true" type="warning" disable-transitions>最新</el-tag>
          </template>
        </el-table-column>
        <el-table-column label="是否發布" sortable="custom" width="120" prop="EnabledMark" align="center">
          <template slot-scope="scope">
            <el-tag :type="scope.row.EnabledMark === true ? 'success' : 'info'" disable-transitions>{{ scope.row.EnabledMark === true ? "已發布" : "未發布" }}</el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="CreatorTime" label="創建時間" sortable="custom" width="160" />

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
  </div>
</template>

<script>

import { getArticlenewsListWithPager, setArticlenewsEnable, deleteSoftArticlenews, deleteArticlenews } from '@/api/cms/articlenews'
import { GetAllCategoryTreeTable } from '@/api/cms/articlecategory'
export default {
  name: 'Articlenews',
  data() {
    return {
      searchform: {
        keywords: ''
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
      selectedCategoryOptions: '',
      selectCategory: [],
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
    InitDictItem() {
      GetAllCategoryTreeTable().then(res => {
        this.selectCategory = res.ResData
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
        Keywords: this.searchform.keywords,
        Order: this.sortableData.order,
        Sort: this.sortableData.sort
      }
      getArticlenewsListWithPager(seachdata).then(res => {
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
        setArticlenewsEnable(data).then(res => {
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
        deleteSoftArticlenews(data).then(res => {
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
          return deleteArticlenews(data)
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
    handleEditDataFrom(view) {
      if (view !== undefined && view !== 'add') {
        if (this.currentSelected.length > 1 || this.currentSelected.length === 0) {
          this.$alert('請選擇一條數據進行編輯/修改', '提示')
        } else {
          this.currentId = this.currentSelected[0].Id
          this.$router.push({ name: 'EditArticle', params: { id: this.currentId, showtype: view }})
        }
      } else {
        this.$router.push({ name: 'EditArticle', params: { id: 'null', showtype: 'add' }})
      }
    }

  }
}
</script>

<style lang="scss" scoped>
</style>
