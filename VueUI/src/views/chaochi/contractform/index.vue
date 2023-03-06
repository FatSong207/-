<template>
  <div class="app-container">
    <div class="filter-container">
      <el-card>
        <el-form ref="searchform" :inline="true" :model="searchform" class="demo-form-inline" size="small">
          <el-form-item label="表單名稱：">
            <el-input v-model="searchform.FormName" clearable />
          </el-form-item>
          <el-form-item label="類別：">
            <el-cascader
              ref="categoryPath"
              v-model="searchform.CateId"
              style="width:600px;"
              :options="selectCategory"
              filterable
              :props="{ label: 'Name',value:'Id',children:'Children',emitPath:false, checkStrictly: true,expandTrigger: 'hover' }"
              clearable
            />
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
            v-hasPermi="['Contractform/Add']"
            type="primary"
            icon="el-icon-plus"
            size="small"
            @click="ShowEditOrViewDialog()"
          >新增</el-button>
          <el-button
            v-hasPermi="['Contractform/Edit']"
            type="primary"
            icon="el-icon-edit"
            class="el-button-modify"
            size="small"
            @click="ShowEditOrViewDialog('edit')"
          >修改</el-button>
          <el-button
            v-hasPermi="['Contractform/Delete']"
            type="danger"
            icon="el-icon-delete"
            size="small"
            @click="deletePhysics()"
          >刪除</el-button>
          <el-button type="default" icon="el-icon-refresh" size="small" @click="loadTableData()">更新</el-button>
        </el-button-group>
      </div>
      <el-table
        :key="rerenderGridtable"
        ref="gridtable"
        v-loading="tableloading"
        :data="tableData"
        border
        stripe
        highlight-current-row
        style="width: 100%"
        :default-sort="{prop: 'SortCode', order: 'descending'}"
        @select="handleSelectChange"
        @select-all="handleSelectAllChange"
        @sort-change="handleSortChange"
        @row-dblclick="rowdblclick"
      >
        <el-table-column type="selection" width="40" fixed />
        <el-table-column prop="FormId" label="表單代號" sortable="custom" width="120" fixed />
        <el-table-column prop="FormName" label="表單名稱" sortable="custom" width="300" />
        <el-table-column prop="Vno" label="版本" sortable="custom" width="100" />
        <el-table-column prop="Level" label="Level" sortable="custom" width="300" />
        <el-table-column prop="LastModifyTime" label="更新時間" width="180" sortable>
          <template slot-scope="scope">
            {{ scope.row.LastModifyTime }}
          </template>
        </el-table-column>
        <el-table-column prop="CreatorTime" label="建立時間" width="180" sortable />
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

    <el-dialog ref="dialogEditForm" :title="editFormTitle" :visible.sync="dialogEditFormVisible" width="1000px">
      <el-tabs v-model="ActionName" type="border-card" @tab-click="handleClick">
        <el-tab-pane label="合約表單" name="A">
          <el-card class="box-card">
            <el-form ref="editFrom" :inline="true" :model="editFrom" class="demo-form-inline">
              <el-row>
                <el-col :span="24">
                  <el-form-item label="表單代號" :label-width="formLabelWidth" prop="FormId">
                    <el-input v-model="editFrom.FormId" placeholder="請輸入表單代號" autocomplete="off" clearable />
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row>
                <el-col :span="24">
                  <el-form-item label="表單名稱" :label-width="formLabelWidth" prop="FormName">
                    <el-input
                      v-model="editFrom.FormName"
                      placeholder="請輸入表單名稱"
                      autocomplete="off"
                      clearable
                      style="width:500px"
                    />
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row>
                <el-col :span="24">
                  <el-form-item label="版本號" :label-width="formLabelWidth" prop="Vno">
                    <el-input v-model="editFrom.Vno" placeholder="請輸入版本號" autocomplete="off" clearable />
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row>
                <el-col :span="24">
                  <el-form-item label="Level" :label-width="formLabelWidth" prop="Level">
                    <el-cascader
                      ref="categoryPath"
                      v-model="editFrom.CateId"
                      style="width:600px;"
                      :options="selectCategory"
                      filterable
                      :props="{label:'Name',value:'Id',children:'Children',emitPath:false, checkStrictly: true,expandTrigger: 'hover' }"
                      clearable
                      @change="handleSelectCategoryChange"
                    />
                  </el-form-item>
                </el-col>
              </el-row>
            </el-form>
          </el-card>
        </el-tab-pane>
        <el-tab-pane label="空白PDF上傳" name="B">
          <el-upload
            ref="newupload"
            drag
            :data="editFrom"
            :action="httpUploadPDFTemplateUrl"
            :headers="headers"
            :multiple="false"
            :auto-upload="true"
            :show-file-list="false"
            accept=".pdf"
            :on-change="uploadchange"
            :on-success="uploadsuccess"
            :on-error="uploaderror"
            :before-upload="handleUploadBefore"
          >
            <em class="el-icon-upload" />
            <div class="el-upload__text">將文件拖到此處，或<em>點擊上傳</em> </div>
            <div slot="tip" class="el-upload__tip">請註意您只能上傳.pdf格式</div>
          </el-upload>
        </el-tab-pane>
      </el-tabs>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogEditFormVisible = false">關 閉</el-button>
        <el-button v-hasPermi="['Building/Edit']" type="primary" @click="saveEditForm()">確 認</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import {
  getContractformListWithPager,
  getContractformDetail,
  saveContractform,
  deleteContractform,
  downloadPDFTemplateByFormId
} from '@/api/chaochi/contractform/contractformservice'
import defaultSettings from '@/settings'
import { getToken } from '@/utils/auth'
import { getAllCategoryTreeTable } from '@/api/chaochi/category/categorycontractservice'

export default {
  name: 'ContractForm',
  data() {
    return {
      selectCategory: [],
      rerenderGridtable: Date.now(),
      searchform: {
        FormId: '',
        FormName: '',
        CateId: ''
      },
      tableData: [],
      tableloading: true,
      pagination: {
        currentPage: 1,
        pagesize: 10,
        pageTotal: 0
      },
      sortableData: {
        order: 'asc',
        sort: 'FormId'
      },
      dialogEditFormVisible: false,
      editFormTitle: '',
      editFrom: {},
      formLabelWidth: '165px',
      currentId: '', // 當前操作對象的ID值，主要用于修改
      currentSelected: [],
      ActionName: 'A',
      httpUploadPDFTemplateUrl:
        defaultSettings.apiChaochiUrl + 'Contractform/UploadPDFTemplate',
      headers: []
    }
  },
  created() {
    this.pagination.currentPage = 1
    this.InitDictItem()
    this.loadTableData()
    this.headers = { Authorization: 'Bearer ' + (getToken() || '') }
  },
  methods: {
    /**
     * 初始化數據
     */
    InitDictItem() {
      getAllCategoryTreeTable().then(res => {
        this.selectCategory = res.ResData
      })
    },
    handleUploadBefore(file) {
      console.log(file.name)
      // file.name = this.editFrom.FormId + 'pdf'
    },
    /**
     * 加載頁面table數據
     */
    loadTableData: function() {
      this.tableloading = true
      var t = {
        FormId: this.searchform.FormId,
        FormName: this.searchform.FormName,
        CateId: this.searchform.CateId
      }
      var seachdata = {
        CurrenetPageIndex: this.pagination.currentPage,
        PageSize: this.pagination.pagesize,
        Order: this.sortableData.order,
        Sort: this.sortableData.sort,
        Filter: t
      }
      getContractformListWithPager(seachdata).then(res => {
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
    handleClick: function() {},
    // 表單重置
    reset() {
      this.editFrom = {
        FormId: '',
        FormName: '',
        Vno: '',
        Level: ''
      }
      this.resetForm('editFrom')
    },
    /**
     * 新增、修改或查看明細信息（綁定顯示數據）     *
     */
    ShowEditOrViewDialog: function(view) {
      this.reset()
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
          // this.$refs.gridtableImg.doLayout()
        }
      } else {
        this.editFormTitle = '新增'
        this.currentId = ''
        this.dialogEditFormVisible = true
      }
    },
    bindEditInfo: function() {
      getContractformDetail(this.currentId).then(res => {
        this.editFrom = res.ResData
      })
    },
    /**
     * 新增/修改保存
     */
    saveEditForm() {
      // console.log(this.editFrom.RoleId)
      this.$refs['editFrom'].validate(valid => {
        if (valid) {
          const data = {
            FormId: this.editFrom.FormId,
            FormName: this.editFrom.FormName,
            Vno: this.editFrom.Vno,
            CateId: this.editFrom.CateId,
            Level: this.editFrom.Level,
            Id: this.currentId
          }

          var url = 'Contractform/Insert'
          if (this.currentId !== '') {
            url = 'Contractform/Update'
          }
          saveContractform(data, url).then(res => {
            if (res.Success) {
              this.$message({
                message: '恭喜你，操作成功',
                type: 'success'
              })
              this.dialogEditFormVisible = false
              this.currentSelected = ''
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
        })
          .then(function() {
            const data = {
              Ids: currentIds
            }
            return deleteContractform(data)
          })
          .then(res => {
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

    uploadsuccess: function(response, file, fileList) {
      if (response.Success) {
        this.$message({
          message: file.name + ' 上傳成功',
          type: 'success'
        })
        this.$store.state.tagsView.cachedViews = []
      } else {
        // this.$message({
        //   message: file.name + ' 上傳但解析失敗,' + response.ErrMsg,
        //   type: 'error'
        // })
        this.$alert(
          file.name + '上傳但解析失敗<br/>' + '<b>' + response.ErrMsg + '<b/>',
          '解析失敗',
          {
            confirmButtonText: '確定',
            dangerouslyUseHTMLString: true
          }
        )
      }
      // this.loadTableDataD()
    },
    uploaderror: function(err, file, fileList) {
      console.log('uploaderror')
      // console.log(file)
      console.log(err)
      console.log(file)
      console.log(fileList)
      this.$message({
        message: file.name + ' 上傳失敗',
        type: 'error'
      })
      // this.loadTableDataD()
    },
    uploadchange: function() {
      // console.log('uploadchange')
    },
    downloadPDFTemplate: function(label) {
      // console.log(url)
      // https://stackoverflow.com/questions/53772331/vue-html-js-how-to-download-a-file-to-browser-using-the-download-tag
      this.tableloading = true

      downloadPDFTemplateByFormId(label)
        .then(response => {
          if (response == null) {
            // this.$message({
            //   message: '下載失敗',
            //   type: 'error'
            // })
            this.$alert('下載失敗', '下載失敗', {
              confirmButtonText: '確定'
            })
            return
          }
          if (response.type !== 'application/pdf') {
            console.log(response.type)
            var reader = new FileReader()
            reader.onload = e => {
              const msg = JSON.parse(e.target.result)
              console.log(msg)
              // this.$message({
              //   message: '下載失敗, 請確認空白PDF(' + label + ')已上傳' + msg,
              //   type: 'error'
              // })
              this.$alert('下載失敗', '下載失敗', {
                confirmButtonText: '確定'
              })
            }
            reader.readAsText(response)
            return
          }
          // item.ImgUrl = URL.createObjectURL(response)
          // this.tableloading = false
          const blob = response // new Blob([response.data], { type: 'image/jpeg' })
          const link = document.createElement('a')
          link.href = URL.createObjectURL(blob)
          link.download = label
          link.click()
          URL.revokeObjectURL(link.href)
        })
        .catch(error => {
          console.log(error)
        })

      this.tableloading = false
    },
    rowdblclick(row, column, event) {
      this.$refs.gridtable.clearSelection()
      this.currentSelected = ''
      this.rerenderGridtable = Date.now() // 強制重繪 https://bbchin.com/archives/el-table-rerender
      this.$nextTick(function() {
        // https://ithelp.ithome.com.tw/articles/10240669
        this.$refs.gridtable.toggleRowSelection(row, true)
        this.currentSelected = this.$refs.gridtable.selection
        this.ShowEditOrViewDialog('edit')
      })
    },
    /**
     *選擇表單分類
     */
    handleSelectCategoryChange: function() {
      const category = this.$refs['categoryPath'].getCheckedNodes()[0]
      if (category) {
        this.editFrom.Level = category.pathLabels.join('/')
      } else {
        this.editFrom.Level = ''
      }
    }
  }
}
</script>
