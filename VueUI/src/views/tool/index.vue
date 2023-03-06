<template>
  <div class="app-container">
    <div class="filter-container">
      <el-card>
        <el-form ref="searchDbform" :inline="true" :model="searchform" class="demo-form-inline" size="small">
          <el-form-item label="資料庫位置" prop="DbAddress">
            <el-input v-model="searchDbform.DbAddress" placeholder="請輸入資料庫位置" autocomplete="off" clearable />
          </el-form-item>
          <el-form-item label="資料庫名稱" prop="DbName">
            <el-input v-model="searchDbform.DbName" placeholder="請輸入資料庫名稱" autocomplete="off" clearable />
          </el-form-item>
          <el-form-item label="用戶名" prop="DbUserName">
            <el-input v-model="searchDbform.DbUserName" placeholder="請輸入用戶名" autocomplete="off" clearable />
          </el-form-item>
          <el-form-item label="訪問密碼" prop="DbPassword">
            <el-input v-model="searchDbform.DbPassword" placeholder="請輸入訪問密碼" autocomplete="off" clearable />
          </el-form-item>
          <el-form-item label="資料庫類型" prop="DbType">
            <el-select v-model="searchDbform.DbType" clearable placeholder="請選資料庫類型">
              <el-option
                v-for="item in selectDbTypes"
                :key="item.Id"
                :label="item.Title"
                :value="item.Id"
              />
            </el-select>
          </el-form-item>
          <el-form-item label="資料庫PORT號" prop="DbPort">
            <el-input v-model="searchDbform.DbPort" placeholder="請輸入資料庫PORT號" autocomplete="off" clearable />
          </el-form-item>
          <el-form-item>
            <el-button type="primary" @click="handleDbConn()">超連結</el-button>
          </el-form-item>
        </el-form>
      </el-card>
    </div>
    <el-card>
      <div class="list-btn-container">
        <el-form ref="codeform" :inline="true" :rules="rules" :model="codeform" class="demo-form-inline" size="small">
          <el-button type="default" icon="el-icon-refresh" size="small" @click="loadTableData()">更新</el-button>
          <el-form-item label="資料庫">
            <el-tooltip class="item" effect="dark" content="默認為系統訪問資料庫" placement="top">
              <el-select v-model="searchform.DbName" clearable placeholder="請選擇" @change="handleShowTable">
                <el-option
                  v-for="item in selectedDataBase"
                  :key="item.Id"
                  :label="item.DbName"
                  :value="item.DbName"
                />
              </el-select>
            </el-tooltip>
          </el-form-item>
          <el-form-item label="資料庫表名">
            <el-input v-model="searchform.tableName" clearable placeholder="輸入要查詢的表名" />
          </el-form-item>
          <el-form-item>
            <el-button type="primary" @click="handleSearch()">查詢</el-button>
          </el-form-item>
          <el-form-item label="項目命名空間：" prop="baseSpace">
            <el-tooltip class="item" effect="dark" content="系統會根據項目命名空間自動生成IService、Service、Models等子命名空間" placement="top">
              <el-input v-model="codeform.baseSpace" clearable placeholder="如Yuebon.WMS" />
            </el-tooltip>
          </el-form-item>
          <el-form-item label="去掉表名前綴：">
            <el-tooltip class="item" effect="dark" content="表名直接變為類名，去掉表名前綴。多個前綴用“;”隔開和結束" placement="top">
              <el-input v-model="codeform.replaceTableNameStr" clearable width="300" placeholder="多個前綴用“;”隔開" />
            </el-tooltip>
          </el-form-item>
          <el-form-item>
            <el-button type="primary" icon="iconfont icon-code" @click="handleGenerate()">生成代碼</el-button>
          </el-form-item>
        </el-form>
      </div>
      <el-table
        ref="gridtable"
        v-loading="tableloading"
        :data="tableData"
        border
        stripe
        highlight-current-row
        style="width: 100%"
        :default-sort="{prop: 'TableName', order: 'ascending'}"
        @select="handleSelectChange"
        @select-all="handleSelectAllChange"
        @sort-change="handleSortChange"
      >
        <el-table-column
          type="selection"
          width="30"
        />
        <el-table-column
          prop="TableName"
          label="表名"
          sortable="custom"
          width="380"
        />
        <el-table-column
          prop="Description"
          label="表描述"
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
  </div>
</template>

<script>
import { createGetDBConn, codeGetDBList, codeGetTableList, codeGenerator } from '@/api/developers/toolsservice'
import { downloadFile } from '@/utils/index'
import { Loading } from 'element-ui'

import defaultSettings from '@/settings'
export default {
  name: 'CodeGenerator',
  data() {
    return {
      searchDbform: {
        DbName: '',
        DbAddress: '',
        DbPort: '1433',
        DbUserName: '',
        DbPassword: '',
        DbType: ''
      },
      searchform: {
        DbName: '',
        tableName: ''
      },
      codeform: {
        baseSpace: '',
        replaceTableNameStr: ''
      },
      rules: {
        baseSpace: [
          { required: true, message: '請輸入項目名稱', trigger: 'blur' },
          { min: 2, max: 50, message: '長度在 2 到 50 個字符', trigger: 'blur' }
        ],
        replaceTableNameStr: [
          { min: 0, max: 50, message: '長度小于50個字符', trigger: 'blur' }
        ]
      },
      selectDbTypes: [{
        Id: 'SqlServer',
        Title: 'SqlServer'
      }, {
        Id: 'MySql',
        Title: 'MySql'
      }],
      tableData: [],
      tableloading: true,
      pagination: {
        currentPage: 1,
        pagesize: 20,
        pageTotal: 0
      },
      sortableData: {
        order: '',
        sort: ''
      },
      currentSelected: [],
      selectedDataBase: []
    }
  },
  created() {
    this.pagination.currentPage = 1
    this.loadData()
    this.loadTableData()
  },
  methods: {
    loadData: function() {
      codeGetDBList().then(res => {
        this.selectedDataBase = res.ResData
      })
    },
    /**
     * 加載頁面table數據
     */
    loadTableData: function() {
      if (this.searchform.dataBaseName !== '') {
        this.tableloading = true
        var seachdata = {
          'CurrenetPageIndex': this.pagination.currentPage,
          'PageSize': this.pagination.pagesize,
          'Keywords': this.searchform.tableName,
          'EnCode': this.searchform.DbName,
          'Order': this.sortableData.order,
          'Sort': this.sortableData.sort
        }
        codeGetTableList(seachdata).then(res => {
          this.tableData = res.ResData.Items
          this.pagination.pageTotal = res.ResData.TotalItems
          this.tableloading = false
        })
      }
    },
    /**
     * 點擊查詢
     */
    handleSearch: function() {
      this.pagination.currentPage = 1
      this.loadTableData()
    },
    handleShowTable: function() {
      this.pagination.currentPage = 1
      this.loadTableData()
    },
    handleDbConn: function() {
      var dataInfo = {
        DbAddress: this.searchDbform.DbAddress,
        DbPort: this.searchDbform.DbPort,
        DbName: this.searchDbform.DbName,
        DbUserName: this.searchDbform.DbUserName,
        DbPassword: this.searchDbform.DbPassword,
        DbType: this.searchDbform.DbType
      }
      createGetDBConn(dataInfo).then(res => {
        this.selectedDataBase = res.ResData
        this.searchform.DbName = this.searchDbform.DbName
      })
      this.pagination.currentPage = 1
      this.loadTableData()
    },
    /**
     * 點擊生成服務端代碼
     */
    handleGenerate: async function() {
      if (this.currentSelected.length === 0) {
        this.$alert('請先選擇要生成代碼的數據表', '提示')
        return false
      } else {
        this.$refs['codeform'].validate((valid) => {
          if (valid) {
            var loadop = {
              lock: true,
              text: '正在生成代碼...',
              spinner: 'el-icon-loading',
              background: 'rgba(0, 0, 0, 0.7)'
            }
            const pageLoading = Loading.service(loadop)
            var currentTables = ''
            this.currentSelected.forEach(element => {
              currentTables += element.TableName + ','
            })
            var seachdata = {
              'tables': currentTables,
              'baseSpace': this.codeform.baseSpace,
              'replaceTableNameStr': this.codeform.replaceTableNameStr
            }
            codeGenerator(seachdata).then(res => {
              if (res.Success) {
                downloadFile(defaultSettings.fileUrl + res.ResData[0], res.ResData[1])
                this.$message({
                  message: '恭喜你，代碼生成完成！',
                  type: 'success'
                })
              } else {
                this.$message({
                  message: res.ErrMsg,
                  type: 'error'
                })
              }
              pageLoading.close()
            }).catch(erre => {
              pageLoading.close()
            })
          } else {
            return false
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
