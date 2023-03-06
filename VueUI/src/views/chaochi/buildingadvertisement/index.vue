<template>
  <div class="app-container">
    <div class="filter-container">
      <el-card>
        <el-form
          ref="searchform"
          :inline="true" :model="searchform" class="demo-form-inline" size="small"
        >
          <el-form-item label="建物類型：">
            <el-select
              v-model="searchform.BPropoty"
              placeholder="請選擇" clearable style="width: 120px"
            >
              <el-option
                v-for="item in bpropotiesOptions"
                :key="item.Id"
                :label="item.BuildingPropotyName"
                :value="item.BuildingPropotyName"
              />
            </el-select>
          </el-form-item>
          <el-form-item label="建物狀態：">
            <el-select
              v-model="searchform.BState"
              placeholder="請選擇" clearable style="width: 100px"
            >
              <el-option
                v-for="item in BState"
                :key="item.value" :label="item.label" :value="item.value"
              />
            </el-select>
          </el-form-item>
          <el-form-item label="建物廣告狀態：">
            <el-select
              v-model="searchform.BAStatus"
              placeholder="請選擇" clearable style="width: 120px"
            >
              <el-option
                v-for="item in BAStatus"
                :key="item.value" :label="item.label" :value="item.value"
              />
            </el-select>
          </el-form-item>
          <el-form-item label="上架天數：">
            <el-input
              v-model="searchform.BADays"
              clearable placeholder="上架天數"
            />
          </el-form-item>
          <el-form-item label="招租天數：">
            <el-input
              v-model="searchform.BRDays"
              clearable placeholder="招租天數"
            />
          </el-form-item>
          <br>
          <el-form-item label="坪數：">
            <el-input
              v-model="searchform.BRealPINGMax"
              clearable placeholder="最大坪數" style="width: 100px"
            />
            --
            <el-input
              v-model="searchform.BRealPINGMin"
              clearable placeholder="最小坪數" style="width: 100px"
            />
          </el-form-item>
          <el-form-item label="租金：">
            <el-input
              v-model="searchform.BTRentMax"
              clearable placeholder="租金上限" style="width: 100px"
            />--
            <el-input
              v-model="searchform.BTRentMin"
              clearable placeholder="租金下限" style="width: 100px"
            />
          </el-form-item>
          <el-form-item label="格局：">
            <el-select
              v-model="searchform.BPatten"
              placeholder="請選擇" clearable style="width: 120px"
            >
              <el-option
                v-for="item in BPatten"
                :key="item.value" :label="item.label" :value="item.value"
              />
            </el-select>
          </el-form-item>
          <el-form-item label="電梯：">
            <el-select
              v-model="searchform.BElevator"
              placeholder="請選擇" clearable style="width: 100px"
            >
              <el-option
                v-for="item in Options"
                :key="item.value" :label="item.label" :value="item.value"
              />
            </el-select>
          </el-form-item>
          <br>
          <el-form-item label="建物區域：">
            <el-input
              v-model="searchform.BAdd"
              style="width:600px" clearable
            />
          </el-form-item>
          <br>
          <el-form-item label="歸屬店：">
            <el-input
              v-model="searchform.CreatorUserDeptId"
              style="width:300px" clearable
            />
            <!-- <el-cascader
              v-model="searchform.CreatorUserDeptId"
              :options="selectOrganize"
              filterable
              :props="{label: 'FullName',value: 'Id',children: 'Children',emitPath: false,checkStrictly: true,expandTrigger: 'hover',}"
              clearable
              style="width: 700px"
            /> -->
          </el-form-item>
          <el-form-item>
            <el-button
              type="primary"
              @click="handleSearch()"
            >查詢</el-button>
          </el-form-item>
        </el-form>
      </el-card>
    </div>
    <el-card>
      <div class="filter-container">
        <el-form
          ref="editFrom1"
          :inline="true" class="demo-form-inline" size="small" :model="editFrom" :rules="rules1"
        >
          <el-form-item
            v-hasPermi="['BuildingAdvertisement/BA']"
            label="建物狀態"
            :label-width="formLabelWidth"
            prop="BState"
          >
            <el-select
              v-model="editFrom.BState"
              placeholder="請選擇" clearable style="width: 180px"
            >
              <el-option
                v-for="item in BStateEdit"
                :key="item.value" :label="item.label" :value="item.value"
              />
            </el-select>
          </el-form-item>
          <el-button
            v-hasPermi="['BuildingAdvertisement/BA']"
            type="primary"
            icon="el-icon-edit"
            class="el-button-modify"
            size="small"
            @click="updateBState()"
          >更新建物狀態</el-button>
        </el-form>
        <el-form
          ref="editFrom2"
          :inline="true" class="demo-form-inline" size="small" :model="editFrom" :rules="rules2"
        >
          <el-form-item
            v-hasPermi="['BuildingAdvertisement/BA']"
            label="廣告網址"
            :label-width="formLabelWidth"
            prop="BAURL"
          >
            <el-input
              v-model="editFrom.BAURL"
              placeholder="請輸入廣告網址"
              autocomplete="off"
              clearable
              style="width: 500px"
            />
          </el-form-item>
          <el-button
            v-hasPermi="['BuildingAdvertisement/BA']"
            type="primary"
            icon="el-icon-edit"
            class="el-button-modify"
            size="small"
            @click="updateBAURL()"
          >刊登廣告</el-button>
        </el-form>
        <el-button
          v-hasPermi="['BuildingAdvertisement/BA']"
          type="danger"
          icon="el-icon-warning"
          class="el-button-modify"
          size="small"
          @click="updateBAStatus('廣告已下架')"
        >廣告下架</el-button>
        <el-button
          v-hasPermi="['BuildingAdvertisement/BA']"
          type="success"
          icon="el-icon-video-play"
          class="el-button-modify"
          size="small"
          @click="updateBAStatus('廣告已上架')"
        >重新招租</el-button>
        <el-button
          type="default"
          icon="el-icon-refresh" size="small" @click="loadTableData()"
        >刷新</el-button>
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
        @row-dblclick="rowdblclick"
      >
        <el-table-column
          type="selection"
          width="30"
        />
        <el-table-column
          prop="BPropoty"
          label="建物類型" sortable="custom" width="100"
        />
        <el-table-column
          prop="BState"
          label="建物狀態" sortable="custom" width="100"
        />
        <el-table-column
          prop="BAStatus"
          label="建物廣告狀態" sortable="custom" width="120"
        />
        <el-table-column
          prop="BAdd"
          label="建物門牌地址" sortable="custom" width="250"
        />
        <el-table-column
          prop="BInfo"
          label="房/廳/衛" width="120"
        />
        <el-table-column
          prop="BRealPING"
          label="坪數" sortable="custom" width="100"
        />
        <el-table-column
          prop="BTRent"
          label="租金" sortable="custom" width="100"
        />
        <el-table-column
          prop="CreatorUserDeptName"
          label="歸屬店" width="180"
        />
        <el-table-column
          prop="Sales"
          label="所屬業務" width="120"
        />
        <el-table-column
          prop="BADays"
          label="上架天數" sortable="custom" width="120"
        />
        <el-table-column
          prop="BRDays"
          label="招租天數" sortable="custom" width="120"
        />
        <el-table-column
          prop="BAURL"
          label="廣告網址" sortable="custom"
        >
          <template slot-scope="scope">
            <a
              target="_blank"
              :href="scope.row.BAURL"
            >{{ scope.row.BAURL }}</a>
          </template>
        </el-table-column>
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
    <!-- <el-dialog
      ref="dialogEditForm"
      v-el-drag-dialog
      :title="editFormTitle+'建物廣告'"
      :visible.sync="dialogEditFormVisible"
      append-to-body
      fullscreen
    >
      <el-form ref="editFrom" :model="editFrom" :rules="rules">
        <el-form-item label="建物狀態" :label-width="formLabelWidth" prop="BState">
          <el-select v-model="editFrom.BState" placeholder="請選擇" clearable style="width: 200px">
            <el-option v-for="item in BStateEdit" :key="item.value" :label="item.label" :value="item.value" />
          </el-select>
        </el-form-item>
        <el-form-item label="廣告網址" :label-width="formLabelWidth" prop="BAURL">
          <el-input v-model="editFrom.BAURL" placeholder="請輸入廣告網址" autocomplete="off" clearable />
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="cancel">取 消</el-button>
        <el-button type="primary" @click="saveEditForm()">確 定</el-button>
      </div>
    </el-dialog> -->

  </div>
</template>

<script>

// import { getBuildingAdvertisementListWithPager, updateBAStatus, updateBAURL } from '@/api/chaochi/buildingadvertisement/buildingadvertisementservice'
import { updateBState } from '@/api/chaochi/building/buildingservice'
import { getBuildingAdvertisementListWithPager, getBuildingAdvertisementDetail, saveBuildingAdvertisement, updateBAStatus, updateBAURL } from '@/api/chaochi/buildingadvertisement/buildingadvertisementservice'
// import { GetPermissionOrganizeTreeTableBA } from '@/api/security/organizeservice'
import { mapGetters } from 'vuex'
import elDragDialog from '@/directive/el-drag-dialog' // 彈窗可移動
export default {
  name: 'BuildingAdvertisement', // 需與菜單的功能編碼一致
  directives: { elDragDialog },
  data() {
    const validateURL = (rule, value, callback) => {
      const URLReg = /^(https?|chrome):\/\/[^\s$.?#].[^\s]*$/gm
      if (!value) {
        return callback(new Error('請輸入廣告網址'))
      }
      setTimeout(() => {
        if (URLReg.test(value)) {
          callback()
        } else {
          callback(new Error('請輸入正確的網址格式'))
        }
      }, 100)
    }
    return {
      searchform: {
        BPropoty: '',
        BState: '',
        BAStatus: '',
        BADays: '',
        BRDays: '',
        BRealPINGMax: '',
        BRealPINGMin: '',
        BTRentMax: '',
        BTRentMin: '',
        BPatten: '',
        BElevator: '',
        BAdd: ''
      },
      BState: [],
      BStateEdit: [
        { value: '招租中', label: '招租中' },
        { value: '已收訂', label: '已收訂' },
        { value: '無委託', label: '無委託' }
      ],
      BAStatus: [
        { value: '', label: '不指定' },
        { value: '廣告未上架', label: '廣告未上架' },
        { value: '廣告已上架', label: '廣告已上架' },
        { value: '廣告已下架', label: '廣告已下架' }
      ],
      BPatten: [
        { value: '', label: '不指定' },
        { value: '套房', label: '套房' },
        { value: '雅房', label: '雅房' },
        { value: '一房', label: '一房' },
        { value: '二房', label: '二房' },
        { value: '三房', label: '三房' },
        { value: '四房以上', label: '四房以上' },
        { value: '開放式空間', label: '開放式空間' }
      ],
      Options: [
        { value: '', label: '不指定' },
        { value: '有', label: '有' },
        { value: '無', label: '無' }
      ],
      tableData: [],
      tableloading: true,
      pagination: {
        currentPage: 1,
        pagesize: 20,
        pageTotal: 0
      },
      sortableData: {
        order: 'desc',
        sort: 't1.CreatorTime'
      },
      dialogEditFormVisible: false,
      editFormTitle: '',
      editFrom: {},
      rules1: {
        BState: [
          { required: true, trigger: 'change', message: '請選擇建物狀態' }
        ]
      },
      rules2: {
        BAURL: [
          { required: true, trigger: 'change', validator: validateURL }
        ]
      },
      formLabelWidth: '80px',
      currentId: '', // 當前操作對象的ID值，主要用于修改
      currentSelected: [],
      cascaderKey: 0,
      selectOrganize: []
    }
  },
  computed: {
    ...mapGetters(['bpropotiesOptions']),
    ...mapGetters(['roles'])
  },
  watch: {
    roles: {
      handler(val) {
        if (val.includes('sales')) {
          this.BState = [
            { value: '', label: '不指定' },
            { value: '招租中', label: '招租中' },
            { value: '已收訂', label: '已收訂' }
          ]
        } else {
          this.BState = [
            { value: '', label: '不指定' },
            { value: '待招租', label: '待招租' },
            { value: '招租中', label: '招租中' },
            { value: '已收訂', label: '已收訂' },
            { value: '無委託', label: '無委託' }
          ]
        }
      },
      immediate: true
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
      // GetPermissionOrganizeTreeTableBA().then(res => {
      //   ++this.cascaderKey;
      //   this.selectOrganize = res.ResData;
      // });
    },

    // 取消按鈕
    cancel() {
      this.dialogEditFormVisible = false
      this.reset()
    },
    // 表單重置
    reset() {
      this.editFrom = {
        BState: '',
        BAURL: ''
        // 需個性化處理內容
      }
      this.resetForm('editFrom')
    },
    /**
     * 加載頁面table數據
     */
    loadTableData: function() {
      this.tableloading = true
      var t = {
        BAStatus: this.searchform.BAStatus,
        BADays: this.searchform.BADays,
        BRDays: this.searchform.BRDays,
        BAdd: this.searchform.BAdd,
        CreatorUserDeptId: this.searchform.CreatorUserDeptId
      }
      var seachdata = {
        BPropoty: this.searchform.BPropoty,
        BState: this.searchform.BState,
        BRealPINGMax: this.searchform.BRealPINGMax,
        BRealPINGMin: this.searchform.BRealPINGMin,
        BTRentMax: this.searchform.BTRentMax,
        BTRentMin: this.searchform.BTRentMin,
        BPatten: this.searchform.BPatten,
        BElevator: this.searchform.BElevator,
        CurrenetPageIndex: this.pagination.currentPage,
        PageSize: this.pagination.pagesize,
        Keywords: this.searchform.keywords,
        Order: this.sortableData.order,
        Sort: this.sortableData.sort,
        Filter: t
      }

      getBuildingAdvertisementListWithPager(seachdata).then(res => {
        this.tableData = res.ResData.Items
        this.pagination.pageTotal = res.ResData.TotalItems
        this.tableloading = false
      }).finally(
        this.tableloading = false
      )
    },
    /**
     * 點擊查詢
     */
    handleSearch: function() {
      this.pagination.currentPage = 1
      this.loadTableData()
    },
    /**
     * 更新建物狀態
     */
    updateBState: function() {
      if (this.currentSelected.length === 0) {
        this.$alert('請選擇一份資料進行編輯/修改', '提示')
      } else {
        this.$refs['editFrom1'].validate((valid) => {
          if (valid) {
            var selectedIds = new Array(this.currentSelected.length);
            this.currentSelected.every(item => {
              const bastatus = item.BAStatus;
              if (bastatus === '廣告已下架') {
                this.$alert('無法編輯已下架建物廣告', '提示')
                return false
              } else {
                selectedIds.push(item.Id)
              }
            })
            selectedIds = selectedIds.filter(function() { return true });
            if (selectedIds.length > 0) {
              var data = {
                'ids': selectedIds,
                'BState': this.editFrom.BState
              }
              updateBState(data).then(res => {
                if (res.Success) {
                  this.$message({
                    message: '恭喜你，操作成功',
                    type: 'success'
                  })
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
            }
          }
        })
      }
    },
    /**
     * 更新建物廣告網址
     */
    updateBAURL: function() {
      if (this.currentSelected.length > 1 || this.currentSelected.length === 0) {
        this.$alert('請選擇一份資料進行編輯/修改', '提示')
      } else {
        this.$refs['editFrom2'].validate((valid) => {
          if (valid) {
            const bastatus = this.currentSelected[0].BAStatus;
            if (bastatus === '廣告已下架') {
              this.$alert('無法更新已下架建物廣告的網址', '提示')
              // } else if (!this.editFrom.BAURL) {
              //   this.$alert('請輸入建物廣告網址', '提示')
            } else {
              this.currentId = this.currentSelected[0].Id
              var data = {
                'Id': this.currentId,
                'BAURL': this.editFrom.BAURL
              }
              updateBAURL(data).then(res => {
                if (res.Success) {
                  this.$message({
                    message: '恭喜你，操作成功',
                    type: 'success'
                  })
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
            }
          }
        })
      }
    },
    /**
     * 新增、修改或查看明細信息（綁定顯示數據）     *
     */
    ShowEditOrViewDialog: function(view) {
      this.reset()
      if (view !== undefined) {
        const bastatus = this.currentSelected[0].BAStatus;
        if (this.currentSelected.length > 1 || this.currentSelected.length === 0) {
          this.$alert('請選擇一份資料進行編輯/修改', '提示')
        } else if (bastatus === '廣告已下架') {
          this.$alert('無法編輯已下架建物廣告', '提示')
        } else {
          this.currentId = this.currentSelected[0].Id
          this.editFormTitle = '編輯'
          this.bindEditInfo()
        }
      } else {
        this.editFormTitle = '新增'
        this.currentId = ''
        this.dialogEditFormVisible = true
      }
    },
    bindEditInfo: function() {
      getBuildingAdvertisementDetail(this.currentId).then(res => {
        this.editForm = res.ResData;
        // 需個性化處理內容
        this.dialogEditFormVisible = true
      })
    },
    /**
     * 新增/修改保存
     */
    saveEditForm() {
      this.$refs['editFrom'].validate((valid) => {
        if (valid) {
          const data = {
            'BState': this.editFrom.BState,
            'BAURL': this.editFrom.BAURL,
            'Id': this.currentId
          }

          var url = 'BuildingAdvertisement/Insert'
          if (this.currentId !== '') {
            url = 'BuildingAdvertisement/Update'
          }
          saveBuildingAdvertisement(data, url).then(res => {
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
    /**
     * 當表格的排序條件發生變化的時候會觸發該事件
     */
    handleSortChange: function(column) {
      this.sortableData.sort = column.prop
      if (this.sortableData.sort === 'BAdd') {
        this.sortableData.sort = 't1.BAdd'
      }
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
    /**
     *雙擊開啟明細
     */
    rowdblclick(row, column, event) {
      this.$refs.gridtable.clearSelection()
      this.currentSelected = ''
      this.rerenderGridtable = Date.now() // 強制重繪 https://bbchin.com/archives/el-table-rerender
      this.$nextTick(function() {
        // https://ithelp.ithome.com.tw/articles/10240669
        this.$refs.gridtable.toggleRowSelection(row, true)
        this.currentSelected = this.$refs.gridtable.selection
        this.ShowEditOrViewDialog('edit');
      })
    },
    updateBAStatus: function(status) {
      const bastatus = this.currentSelected[0].BAStatus;
      if (this.currentSelected.length > 1 || this.currentSelected.length === 0) {
        this.$alert('請選擇一份資料進行編輯/修改', '提示')
      } else if (status === '廣告已下架' && bastatus === '廣告已下架') {
        this.$alert('建物廣告無法重複下架', '提示')
      } else if (status === '廣告已上架' && (bastatus === '廣告未上架' || bastatus === '廣告已上架')) {
        this.$alert('只能重新招租己下架的廣告', '提示')
      } else {
        this.currentId = this.currentSelected[0].Id
        const data = {
          'BAStatus': status,
          'Id': this.currentId
        }

        updateBAStatus(data).then(res => {
          if (res.Success) {
            this.$message({
              message: '恭喜你，操作成功',
              type: 'success'
            })
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
      }
    }
  }
}
</script>

<style lang="less" scoped>
/deep/ .el-form-item__error {
  Color: #f56c6c;
  position: relative;
  left: 0;
  top: 100%;
}
</style>
