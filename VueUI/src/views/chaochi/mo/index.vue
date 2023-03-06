<template>
  <div class="app-container">
    <div class="filter-container">
      <el-card>
        <el-form
          ref="searchform"
          :inline="true" :model="searchform" class="demo-form-inline" size="small"
        >
          <el-form-item label="分租物件單號">
            <el-input
              v-model="searchform.MOID"
              clearable placeholder="分租物件單號"
            />
          </el-form-item>
          <el-form-item label="分租物件名稱">
            <el-input
              v-model="searchform.MOName"
              clearable placeholder="分租物件名稱"
            />
          </el-form-item>
          <el-form-item label="分租物件狀態：">
            <el-select
              v-model="searchform.Status"
              placeholder="請選擇"
            >
              <el-option
                v-for="item in MOStatus"
                :key="item.value" :label="item.label" :value="item.value"
              />
            </el-select>
          </el-form-item>
          <br>
          <el-form-item label="包含建物門牌地址：">
            <el-input
              v-model="searchform.BAdd"
              style="width:600px" clearable
            />
          </el-form-item>
          <el-form-item label="建物類型：">
            <el-select
              v-model="searchform.BPropoty"
              placeholder="請選擇" clearable style="width: 200px"
            >
              <el-option
                v-for="item in bpropotiesOptions"
                :key="item.Id"
                :label="item.BuildingPropotyName"
                :value="item.BuildingPropotyName"
              />
            </el-select>
          </el-form-item>
          <el-form-item>
            <el-button
              type="primary"
              icon="el-icon-search" @click="handleSearch()"
            >查詢</el-button>
          </el-form-item>
        </el-form>
      </el-card>
    </div>
    <el-card>
      <div class="list-btn-container">
        <el-button-group>
          <el-button
            v-hasPermi="['MO/Add']"
            type="primary"
            icon="el-icon-plus"
            size="small"
            @click="ShowEditOrViewDialog()"
          >新增</el-button>
          <el-button
            v-hasPermi="['MO/Edit']"
            type="primary"
            icon="el-icon-edit"
            class="el-button-modify"
            size="small"
            @click="ShowEditOrViewDialog('edit')"
          >修改</el-button>
          <el-button
            v-hasPermi="['MO/Delete']"
            type="danger"
            icon="el-icon-delete"
            size="small"
            @click="deletePhysics()"
          >刪除</el-button>
          <el-button
            type="default"
            icon="el-icon-refresh" size="small" @click="loadTableData()"
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
          prop="MOID"
          label="分租物件單號" sortable="custom" width="150"
        />
        <el-table-column
          prop="MOName"
          label="分租物件名稱" sortable="custom" width="250"
        />
        <el-table-column
          prop=""
          label="當前綁定物件數量" sortable="custom" width="160"
        />
        <el-table-column
          prop="LSName"
          label="原房東" sortable="custom" width="120"
        />
        <el-table-column
          prop="SLName"
          label="二房東" sortable="custom" width="120"
        />
        <el-table-column
          prop="RealName"
          label="申請人員" sortable="custom" width="120"
        />
        <el-table-column
          prop="Status"
          label="合約狀態" sortable="custom"
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
      v-el-drag-dialog
      :title="editFormTitle"
      :visible.sync="dialogEditFormVisible"
      append-to-body
      fullscreen
    >
      <el-form
        ref="editFrom"
        :model="editFrom" :rules="rules"
      >
        <el-form-item
          label="分租物件單號"
          :label-width="formLabelWidth" prop="MOID"
        >
          <el-input
            v-model="editFrom.MOID"
            readonly style="width:300px"
          />
        </el-form-item>
        <el-form-item
          label="分租物件名稱"
          :label-width="formLabelWidth" prop="MOName"
        >
          <el-input
            v-model="editFrom.MOName"
            style="width:500px" placeholder="請輸入分租物件名稱" autocomplete="off" clearable :disabled="editFormTitle === '編輯'"
          />
        </el-form-item>
        <el-row>
          <el-col :span="24">
            <el-form-item
              label="原建物地址"
              :label-width="formLabelWidth" prop="OGBuildingName"
            >
              <el-input
                v-model="editFrom.OGBuildingName"
                type="textarea" :rows="4" style="width: 500px" :disabled="editFormTitle === '編輯'"
              />
            </el-form-item>
          </el-col>
          <!-- <el-col :span="14">
            <p>*請用分號令(;)區隔多個原建物地址</p>
          </el-col> -->
        </el-row>
        <el-row>
          <el-col :span="10">
            <el-form-item
              label="出租人-姓名/法人名稱"
              :label-width="formLabelWidth" prop="LSName"
            >
              <el-input
                v-model="editFrom.LSName"
                style="width:300px"
                placeholder="請輸入出租人-姓名/法人名稱"
                autocomplete="off"
                clearable
              />
            </el-form-item>
          </el-col>
          <el-col :span="14">
            <el-form-item
              label="出租人-身份證號或統一編號"
              :label-width="formLabelWidth" prop="LSID"
            >
              <el-input
                v-model="editFrom.LSID"
                style="width:300px"
                placeholder="請輸入出租人-身份證號或統一編號"
                autocomplete="off"
                clearable
              />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="10">
            <el-form-item
              label="二房東"
              :label-width="formLabelWidth" prop="SLName"
            >
              <el-select
                v-model="editFrom.SLName"
                style="width: 300px" placeholder="請選擇" @change="getSLInfo"
              >
                <el-option
                  v-for="item in secondLandlord"
                  :key="item.SLID" :label="item.SLName" :value="item.SLID"
                />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item
              label="二房東統一編號"
              :label-width="formLabelWidth" prop="SLID"
            >
              <el-input
                v-model="editFrom.SLID"
                style="width:300px"
                placeholder="請輸入二房東統一編號"
                autocomplete="off"
                clearable
              />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="24">
            <el-form-item
              label="建物門牌地址："
              :label-width="formLabelWidth"
            >
              <Address
                :sendedform="sendedform"
                @geteditaddress="geteditaddress"
              />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="24">
            <el-form-item
              label="建物類型："
              :label-width="formLabelWidth"
              prop="BPropoty"
            >
              <el-select
                v-model="editFrom.BPropoty"
                placeholder="請選擇" clearable style="width: 200px"
              >
                <el-option
                  v-for="item in bpropotiesOptions"
                  :key="item.Id"
                  :label="item.BuildingPropotyName"
                  :value="item.BuildingPropotyName"
                />
              </el-select>
            </el-form-item>

          </el-col>
        </el-row>
      </el-form>
      <p v-if="currentId !== ''">
        <font color="red">＊已存在建物無法刪除</font>
      </p>
      <div class="list-btn-container">
        <el-button-group>
          <el-button
            v-hasPermi="['MO/Add']"
            type="success" icon="el-icon-plus" size="small" @click="addMOBuilding()"
          >新增
          </el-button>
          <el-button
            v-hasPermi="['MO/Delete']"
            type="danger"
            icon="el-icon-delete"
            size="small"
            @click="removeMOBuilding()"
          >刪除</el-button>
          <el-button
            type="default"
            icon="el-icon-refresh" size="small" @click="loadTableData()"
          >刷新</el-button>
        </el-button-group>
      </div>
      <el-table
        ref="objectTable"
        v-loading="objectTableloading"
        :data="objectTableData"
        border
        highlight-current-row
        style="width: 70%"
        :default-sort="{prop: 'SortCode', order: 'ascending'}"
        @select="handleMOBSelectChange"
        @select-all="handleMOBSelectAllChange"
      >
        <el-table-column
          type="selection"
          width="30"
        />
        <el-table-column
          prop="BAdd"
          label="物件地址" sortable="custom"
        />
        <el-table-column
          prop="BPropoty"
          label="物件屬性" sortable="custom"
        />
      </el-table>
      <div
        slot="footer"
        class="dialog-footer"
      >
        <el-button
          size="small"
          icon="el-icon-close" @click="cancel"
        >取 消</el-button>
        <el-button
          type="primary"
          size="small" icon="el-icon-paperclip" @click="saveEditForm()"
        >儲存</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>

import { getMOListWithPager, getMODetail,
  saveMO, deleteMO, isBuildingExist } from '@/api/chaochi/mo/moservice'
import { getMOBuildingListWithPager, deleteMOBuilding } from '@/api/chaochi/mo/mobuildingservice'
import { getAllSecondLandlordList } from '@/api/chaochi/secondlandlord/secondlandlordservice'
import { mergerAddress } from '@/utils/index'
import { mapGetters } from 'vuex'
import Address from '@/components/Address/Address.vue'
import elDragDialog from '@/directive/el-drag-dialog' // 彈窗可移動
export default {
  name: 'MO', // 需與菜單的功能編碼一致
  directives: { elDragDialog },
  components: {
    Address
  },
  data() {
    return {
      searchform: {
        MOID: '',
        MOName: '',
        Status: '',
        BAdd: '',
        BPropoty: ''
      },
      MOStatus: [
        { value: '', label: '不指定' },
        { value: '待綁定合約', label: '待綁定合約' },
        { value: '已綁定合約', label: '已綁定合約' }
      ],
      tableData: [],
      objectTableData: [],
      tableloading: true,
      objectTableloading: true,
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
      editFrom: {},
      rules: {
        MOName: [
          { required: true, message: '請輸入分租物件名稱', trigger: 'blur' },
          { min: 2, max: 50, message: '長度在 2 到 50 個字符', trigger: 'blur' }
        ],
        OGBuildingName: [
          { required: true, message: '請輸入原建物地址', trigger: 'blur' }
        ],
        LSName: [
          { required: true, message: '請輸入出租人姓名或法人名稱', trigger: 'blur' }
        ],
        LSID: [
          { required: true, message: '請輸入身份證號或統一編號', trigger: 'blur' }
        ],
        SLName: [
          { required: true, message: '請選擇二房東', trigger: 'change' }
        ],
        SLID: [
          { required: true, message: '請輸入二房東統一編號', trigger: 'blur' }
        ],
        BPropoty: [
          { required: true, message: '請選擇建物類型', trigger: 'change' }
        ]

      },
      formLabelWidth: '220px',
      currentId: '', // 當前操作對象的ID值，主要用于修改
      currentSelected: [],
      currentMOBId: '', // 當前操作物件的ID值，主要用于修改
      currentMOBSelected: [],
      secondLandlord: [],
      sendedform: {}
    }
  },
  computed: {
    ...mapGetters(['bpropotiesOptions'])
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
      getAllSecondLandlordList().then(res => {
        this.secondLandlord = res.ResData
      })
    },

    // 取消按鈕
    cancel() {
      this.dialogEditFormVisible = false
      this.reset()
    },
    // 表單重置
    reset() {
      this.editFrom = {
        MOID: '',
        MOName: '',
        OGBuildingName: '',
        LSID: '',
        LSName: '',
        SLID: '',
        SLName: '',
        Status: '',
        BAdd: '',
        BAdd_1: '',
        BAdd_1_1: '',
        BAdd_1_2: '',
        BAdd_2: '',
        BAdd_2_1: '',
        BAdd_2_2: '',
        BAdd_2_3: '',
        BAdd_2_4: '',
        BAdd_3: '',
        BAdd_3_1: '',
        BAdd_3_2: '',
        BAdd_4: '',
        BAdd_5: '',
        BAdd_6: '',
        BAdd_7: '',
        BAdd_8: '',
        BAdd_9: '',
        BPropoty: ''
        // 需個性化處理內容
      }
      this.objectTableData = []
      this.resetForm('editFrom')
    },
    /**
     * 加載頁面table數據
     */
    loadTableData: function() {
      this.tableloading = true
      var t = {
        MOID: this.searchform.MOID,
        MOName: this.searchform.MOName,
        Status: this.searchform.Status
      }
      var seachdata = {
        CurrenetPageIndex: this.pagination.currentPage,
        PageSize: this.pagination.pagesize,
        Keywords: this.searchform.keywords,
        Order: this.sortableData.order,
        Sort: 't1.CreatorTime',
        BAdd: this.searchform.BAdd,
        BPropoty: this.searchform.BPropoty,
        Filter: t
      }
      getMOListWithPager(seachdata).then(res => {
        this.tableData = res.ResData.Items
        this.pagination.pageTotal = res.ResData.TotalItems
        this.tableloading = false
      })
    },
    /**
     * 加載建物頁面table數據
     */
    loadMOBTableData: function() {
      this.objectTableloading = true
      var t = {
        MOID: this.editFrom.MOID
      }
      var seachdata = {
        CurrenetPageIndex: this.pagination.currentPage,
        PageSize: this.pagination.pagesize,
        Keywords: this.searchform.keywords,
        Order: this.sortableData.order,
        Sort: this.sortableData.sort,
        Filter: t
      }

      getMOBuildingListWithPager(seachdata).then(res => {
        this.objectTableData = res.ResData.Items
        // this.pagination.pageTotal = res.ResData.TotalItems
        this.objectTableloading = false
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
      this.reset()
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
        this.objectTableloading = false
        this.editFrom.MOID = this.generateId()
      }
    },
    bindEditInfo: function() {
      getMODetail(this.currentId).then(res => {
        this.editFrom = res.ResData
        // 需個性化處理內容

        // 處理地址
        this.sendedform = {
          Add_1: this.editFrom.BAdd_1,
          Add_1_1: this.editFrom.BAdd_1_1,
          Add_1_2: this.editFrom.BAdd_1_2,
          Add_2: this.editFrom.BAdd_2,
          Add_2_1: this.editFrom.BAdd_2_1,
          Add_2_2: this.editFrom.BAdd_2_2,
          Add_2_3: this.editFrom.BAdd_2_3,
          Add_2_4: this.editFrom.BAdd_2_4,
          Add_3: this.editFrom.BAdd_3,
          Add_3_1: this.editFrom.BAdd_3_1,
          Add_3_2: this.editFrom.BAdd_3_2,
          Add_4: this.editFrom.BAdd_4,
          Add_5: this.editFrom.BAdd_5,
          Add_6: this.editFrom.BAdd_6,
          Add_7: this.editFrom.BAdd_7,
          Add_8: this.editFrom.BAdd_8,
          Add_9: this.editFrom.BAdd_9
        }
        this.loadMOBTableData()
      })
    },
    /**
     * 新增/修改保存
     */
    saveEditForm() {
      this.$refs['editFrom'].validate((valid) => {
        if (valid) {
          const data = {
            'MOID': this.editFrom.MOID,
            'MOName': this.editFrom.MOName,
            'OGBuildingName': this.editFrom.OGBuildingName,
            'LSID': this.editFrom.LSID,
            'LSName': this.editFrom.LSName,
            'SLID': this.editFrom.SLID,
            'SLName': this.editFrom.SLName,
            'Status': '待綁定合約',
            'BuildingList': this.objectTableData,
            'Id': this.currentId
          }

          var url = 'MO/Insert'
          if (this.currentId !== '') {
            url = 'MO/Update'
          }
          saveMO(data, url).then(res => {
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
        const that = this
        this.$confirm('是否確認刪除所選的數據項?', '警告', {
          confirmButtonText: '確定',
          cancelButtonText: '取消',
          type: 'warning'
        }).then(function() {
          var currentIds = []
          that.currentSelected.forEach(element => {
            currentIds.push(element.MOID)
          })
          const data = {
            Ids: currentIds
          }
          return deleteMO(data)
        }).then(res => {
          if (res.Success) {
            that.$message({
              message: '恭喜你，刪除成功',
              type: 'success'
            })
            that.currentSelected = ''
            that.loadTableData()
          } else {
            this.$message({
              message: res.ErrMsg,
              type: 'error'
            })
          }
        })
      }
    },
    deleteMOBuilding: function() {
      if (this.currentMOBSelected.length === 0) {
        this.$alert('請先選擇要操作的物件', '提示')
        return false
      } else {
        const that = this
        this.$confirm('是否確認刪除所選的物件?', '警告', {
          confirmButtonText: '確定',
          cancelButtonText: '取消',
          type: 'warning'
        }).then(function() {
          var currentIds = []
          that.currentMOBSelected.forEach(element => {
            currentIds.push(element.Id)
          })
          const data = {
            Ids: currentIds
          }
          return deleteMOBuilding(data)
        }).then(res => {
          if (res.Success) {
            that.$message({
              message: '恭喜你，刪除成功',
              type: 'success'
            })
            that.currentMOBSelected = []
            that.loadMOBTableData()
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
    /**
     * 當用戶手動勾選checkbox數據行事件
     */
    handleMOBSelectChange: function(selection, row) {
      this.currentMOBSelected = selection
    },
    /**
     * 當用戶手動勾選全選checkbox事件
     */
    handleMOBSelectAllChange: function(selection) {
      this.currentMOBSelected = selection
    },
    /**
     *產生分租物件單號
     */
    generateId() {
      const ms = new Date().getMilliseconds()
      return 'MO-' + Math.ceil(Math.random() * 10000000) + ms
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
    getSLInfo(value) {
      let obj = {}
      obj = this.secondLandlord.find((item) => {
        return item.SLID === value
      })

      this.editFrom.SLID = value
      this.editFrom.SLName = obj.SLName
    },
    geteditaddress(addressData) {
      this.editFrom.BAdd_1 = addressData.Add_1
      this.editFrom.BAdd_1_1 = addressData.Add_1_1
      this.editFrom.BAdd_1_2 = addressData.Add_1_2
      this.editFrom.BAdd_2 = addressData.Add_2
      this.editFrom.BAdd_2_1 = addressData.Add_2_1
      this.editFrom.BAdd_2_2 = addressData.Add_2_2
      this.editFrom.BAdd_2_3 = addressData.Add_2_3
      this.editFrom.BAdd_2_4 = addressData.Add_2_4
      this.editFrom.BAdd_3 = addressData.Add_3
      this.editFrom.BAdd_3_1 = addressData.Add_3_1
      this.editFrom.BAdd_3_2 = addressData.Add_3_2
      this.editFrom.BAdd_4 = addressData.Add_4
      this.editFrom.BAdd_5 = addressData.Add_5
      this.editFrom.BAdd_6 = addressData.Add_6
      this.editFrom.BAdd_7 = addressData.Add_7
      this.editFrom.BAdd_8 = addressData.Add_8
      this.editFrom.BAdd_9 = addressData.Add_9
    },
    addMOBuilding() {
      const addarray = [
        this.editFrom.BAdd_1,
        this.editFrom.BAdd_1_1,
        this.editFrom.BAdd_1_2,
        this.editFrom.BAdd_2,
        this.editFrom.BAdd_2_1,
        this.editFrom.BAdd_2_2,
        this.editFrom.BAdd_2_3,
        this.editFrom.BAdd_2_4,
        this.editFrom.BAdd_3,
        this.editFrom.BAdd_3_1,
        this.editFrom.BAdd_3_2,
        this.editFrom.BAdd_4,
        this.editFrom.BAdd_5,
        this.editFrom.BAdd_6,
        this.editFrom.BAdd_7,
        this.editFrom.BAdd_8,
        this.editFrom.BAdd_9

      ]

      const data = {
        'MOID': this.editFrom.MOID,
        'BAdd': mergerAddress(addarray),
        'BAdd_1': this.editFrom.BAdd_1,
        'BAdd_1_1': this.editFrom.BAdd_1_1,
        'BAdd_1_2': this.editFrom.BAdd_1_2,
        'BAdd_2': this.editFrom.BAdd_2,
        'BAdd_2_1': this.editFrom.BAdd_2_1,
        'BAdd_2_2': this.editFrom.BAdd_2_2,
        'BAdd_2_3': this.editFrom.BAdd_2_3,
        'BAdd_2_4': this.editFrom.BAdd_2_4,
        'BAdd_3': this.editFrom.BAdd_3,
        'BAdd_3_1': this.editFrom.BAdd_3_1,
        'BAdd_3_2': this.editFrom.BAdd_3_2,
        'BAdd_4': this.editFrom.BAdd_4,
        'BAdd_5': this.editFrom.BAdd_5,
        'BAdd_6': this.editFrom.BAdd_6,
        'BAdd_7': this.editFrom.BAdd_7,
        'BAdd_8': this.editFrom.BAdd_8,
        'BAdd_9': this.editFrom.BAdd_9,
        'BPropoty': this.editFrom.BPropoty
      }

      if (data.BAdd && data.BAdd !== '陳列不能為空值' && data.BAdd !== '地址陣列不為17') {
        const obj2 = this.objectTableData.filter((el) => el.BAdd === data.BAdd);
        if (obj2.length > 1) {
          this.$alert('物件地址不能重複', '提示')
        } else {
          const Adds = []
          Adds.push(data.BAdd)
          const checkData = {
            Ids: Adds
          }
          isBuildingExist(checkData).then(res => {
            if (res.ResData) {
              this.$alert('建物[' + data.BAdd + ']已存在', '警告')
              return false
            } else {
              this.objectTableData.push(data);
            }
          })
        }
      }

      // var url = 'MOBuilding/Insert'
      // saveMOBuilding(data, url).then(res => {
      //   if (res.Success) {
      //     this.$message({
      //       message: '恭喜你，操作成功',
      //       type: 'success'
      //     })
      //     // this.dialogEditFormVisible = false
      //     this.currentMOBSelected = ''
      //     this.loadMOBTableData()
      //   } else {
      //     this.$message({
      //       message: res.ErrMsg,
      //       type: 'error'
      //     })
      //   }
      // })
    },
    removeMOBuilding() {
      var currentAdds = []
      this.currentMOBSelected.forEach(element => {
        currentAdds.push(element.BAdd)
      })

      // 編輯時,已存在建物無法刪除
      if (this.currentAdds !== '') {
        const data = {
          Ids: currentAdds
        }
        isBuildingExist(data).then(res => {
          if (res.ResData) {
            this.$alert('已存在建物無法刪除', '警告')
            return false
          } else {
            this.objectTableData = this.objectTableData.filter((el) => !currentAdds.includes(el.BAdd));
          }
        })
      } else {
        this.objectTableData = this.objectTableData.filter((el) => !currentAdds.includes(el.BAdd));
      }
    }
  }
}
</script>

<style>
</style>
