<template>
  <div class="app-container">
    <el-card>
      <div class="list-btn-container">
        <el-button-group>
          <el-button
            v-hasPermi="['Organize/Add']"
            type="primary"
            icon="el-icon-plus"
            size="small"
            @click="ShowEditOrViewDialog()"
          >新增</el-button>
          <el-button
            v-hasPermi="['Organize/Edit']"
            type="primary"
            icon="el-icon-edit"
            class="el-button-modify"
            size="small"
            @click="ShowEditOrViewDialog('edit')"
          >修改</el-button>
          <el-button
            v-hasPermi="['Organize/Enable']"
            type="info"
            icon="el-icon-video-pause"
            size="small"
            @click="setEnable('0')"
          >禁用</el-button>
          <el-button
            v-hasPermi="['Organize/Enable']"
            type="success"
            icon="el-icon-video-play"
            size="small"
            @click="setEnable('1')"
          >啟用</el-button>
          <el-button
            v-hasPermi="['Organize/DeleteSoft']"
            type="warning"
            icon="el-icon-delete"
            size="small"
            @click="deleteSoft('0')"
          >軟刪除</el-button>
          <el-button
            v-hasPermi="['Organize/Delete']"
            type="danger"
            icon="el-icon-delete"
            size="small"
            @click="deletePhysics()"
          >刪除</el-button>
          <el-button
            type="default"
            icon="el-icon-refresh"
            size="small"
            @click="loadTableData()"
          >更新</el-button>
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
        <el-table-column
          type="selection"
          width="30"
        />
        <el-table-column
          prop="FullName"
          label="公司名稱"
          sortable="custom"
          width="380"
        />
        <!-- <el-table-column prop="EnCode" label="編碼" sortable="custom" width="180" /> -->
        <!-- <el-table-column
          prop="CategoryId"
          label="單位"
          sortable="custom"
          width="90"
          align="center"
        >
          <template slot-scope="scope">
            <slot v-if="scope.row.CategoryId === 'Group'">集團</slot>
            <slot v-if="scope.row.CategoryId === 'Area'">區域</slot>
            <slot v-if="scope.row.CategoryId === 'Company'">公司</slot>
            <slot v-else-if="scope.row.CategoryId === 'SubCompany'">子公司</slot>
            <slot v-else-if="scope.row.CategoryId === 'Department'">部門</slot>
            <slot v-else-if="scope.row.CategoryId === 'SubDepartment'">子部門</slot>
            <slot v-else-if="scope.row.CategoryId === 'WorkGroup'">工作組</slot>
          </template>
        </el-table-column> -->
        <el-table-column
          prop="ManagerId"
          label="負責人"
          sortable="custom"
          width="90"
        />
        <!-- <el-table-column prop="TelePhone" label="電話" sortable="custom" width="120" />
        <el-table-column prop="MobilePhone" label="手機" sortable="custom" width="120" /> -->
        <el-table-column
          label="是否啟用"
          sortable="custom"
          width="120"
          prop="EnabledMark"
          align="center"
        >
          <template slot-scope="scope">
            <el-tag
              :type="scope.row.EnabledMark === true ? 'success' : 'info'"
              disable-transitions
            >{{ scope.row.EnabledMark === true ? "啟用" : "停用" }}</el-tag>
          </template>
        </el-table-column>
        <!-- <el-table-column label="是否刪除" sortable="custom" width="120" prop="DeleteMark" align="center">
          <template slot-scope="scope">
            <el-tag :type="scope.row.DeleteMark === true ? 'danger' : 'success'" disable-transitions>{{ scope.row.DeleteMark === true ? "已刪除" : "否" }}</el-tag>
          </template>
        </el-table-column> -->
        <el-table-column
          prop="CreatorTime"
          label="建立時間"
          sortable
        />
        <el-table-column
          prop="LastModifyTime"
          label="更新時間"
          sortable
        />
      </el-table>
    </el-card>
    <el-dialog
      ref="dialogEditForm"
      v-el-drag-dialog
      :title="editFormTitle"
      :visible.sync="dialogEditFormVisible"
      width="660px"
    >
      <el-form
        ref="editFrom"
        :inline="true"
        :model="editFrom"
        :rules="rules"
        class="demo-form-inline"
      >
        <el-form-item
          label="上級組織"
          :label-width="formLabelWidth"
          prop="ParentId"
        >
          <el-cascader
            v-model="selectedOrganizeOptions"
            style="width: 500px"
            :options="selectOrganize"
            filterable
            :props="{label: 'FullName',
                     value: 'Id',
                     children: 'Children',
                     emitPath: false,
                     checkStrictly: true,
                     expandTrigger: 'hover',
            }"
            clearable
            @change="handleSelectOrganizeChange"
          />
        </el-form-item>
        <el-form-item
          label="公司名稱"
          :label-width="formLabelWidth"
          prop="FullName"
        >
          <el-input
            v-model="editFrom.FullName"
            style="width: 500px"
            placeholder="請輸入公司名稱"
            autocomplete="off"
            clearable
          />
        </el-form-item>
        <!-- <el-form-item label="代碼" :label-width="formLabelWidth" prop="EnCode">
          <el-input v-model="editFrom.EnCode" placeholder="請輸入機構代碼" autocomplete="off" clearable />
        </el-form-item> -->
        <!-- <el-form-item
          label="單位"
          :label-width="formLabelWidth"
          prop="CategoryId"
        >
          <el-select
            v-model="editFrom.CategoryId"
            clearable
            placeholder="請選單位"
          >
            <el-option
              v-for="item in selectOrganizeType"
              :key="item.Id"
              :label="item.ItemName"
              :value="item.ItemCode"
            />
          </el-select>
        </el-form-item> -->
        <el-form-item
          label="簡稱"
          :label-width="formLabelWidth"
          prop="ShortName"
        >
          <el-input
            v-model="editFrom.ShortName"
            placeholder="請輸入簡稱"
            autocomplete="off"
            clearable
          />
        </el-form-item>
        <!-- <el-form-item label="負責人" :label-width="formLabelWidth" prop="ManagerId">
          <el-input v-model="editFrom.ManagerId" placeholder="請輸入負責人" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="手機" :label-width="formLabelWidth" prop="MobilePhone">
          <el-input v-model="editFrom.MobilePhone" placeholder="請輸入手機" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="公司電話" :label-width="formLabelWidth" prop="TelePhone">
          <el-input v-model="editFrom.TelePhone" placeholder="請輸入公司電話" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="Line ID" :label-width="formLabelWidth" prop="WeChat">
          <el-input v-model="editFrom.WeChat" placeholder="請輸入Line ID" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="Email" :label-width="formLabelWidth" prop="Email">
          <el-input v-model="editFrom.Email" placeholder="請輸入Email" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="公司傳真" :label-width="formLabelWidth" prop="Fax">
          <el-input v-model="editFrom.Fax" placeholder="請輸入公司傳真" autocomplete="off" clearable />
        </el-form-item> -->
        <el-form-item
          label="排序"
          :label-width="formLabelWidth"
          prop="SortCode"
        >
          <el-input
            v-model.number="editFrom.SortCode"
            placeholder="請輸入排序,默認為99"
            autocomplete="off"
            clearable
          />
        </el-form-item>
        <el-form-item
          label="選項"
          :label-width="formLabelWidth"
          prop="EnabledMark"
        >
          <el-switch
            v-model="enabledMark"
            active-text="啟用"
            inactive-text="停用"
          />
          <!-- <el-checkbox v-model="editFrom.DeleteMark">刪除</el-checkbox> -->
        </el-form-item>
        <!-- <el-form-item label="公司地址" :label-width="formLabelWidth" prop="Address">
          <el-input v-model="editFrom.Address" style="width: 500px" placeholder="請輸入公司地址" autocomplete="off" clearable />
        </el-form-item> -->
        <el-form-item
          label="說明"
          :label-width="formLabelWidth"
          prop="Description"
        >
          <el-input
            v-model="editFrom.Description"
            style="width: 500px"
            autocomplete="off"
            clearable
          />
        </el-form-item>
      </el-form>
      <div
        slot="footer"
        class="dialog-footer"
      >
        <el-button @click="dialogEditFormVisible = false">關 閉</el-button>
        <el-button
          type="primary"
          @click="saveEditForm()"
        >確 認</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import { getListItemDetailsByCode } from '@/api/basebasic'
import {
  getAllOrganizeTreeTable,
  getOrganizeDetail,
  saveOrganize,
  setOrganizeEnable,
  deleteSoftOrganize,
  deleteOrganize
} from '@/api/security/organizeservice'

import elDragDialog from '@/directive/el-drag-dialog' // base on element-ui
export default {
  name: 'Organize',
  directives: { elDragDialog },
  data() {
    return {
      enabledMark: false,
      loadBtnFunc: [],
      selectedOrganizeOptions: '',
      selectOrganize: [],
      selectOrganizeType: [],
      tableData: [],
      tableloading: true,
      dialogEditFormVisible: false,
      editFormTitle: '',
      editFrom: {},
      rules: {
        FullName: [
          { required: true, message: '請輸入名稱', trigger: 'blur' },
          { min: 2, max: 50, message: '長度在 2 到 50 個字符', trigger: 'blur' }
        ]
        // CategoryId: [{ required: true, message: '請選擇單位', trigger: 'blur' }]
      },
      formLabelWidth: '80px',
      currentId: '', // 當前操作對象的ID值，主要用于修改
      currentSelected: []
    }
  },
  created() {
    this.InitDictItem()
    this.loadTableData()
  },
  methods: {
    /**
     * 初始化數據
     */
    InitDictItem() {
      getListItemDetailsByCode('OrganizeCategory').then(res => {
        this.selectOrganizeType = res.ResData
      })
    },
    /**
     * 加載頁面table數據
     */
    loadTableData: function() {
      this.tableloading = true
      getAllOrganizeTreeTable().then(res => {
        this.tableData = res.ResData
        this.selectOrganize = res.ResData
        this.tableloading = false
        console.log(res.ResData)
      })
    },

    /**
     *選擇組織
     */
    handleSelectOrganizeChange: function() {
      this.editFrom.ParentId = this.selectedOrganizeOptions
    },
    // 表單重置
    reset() {
      this.editFrom = {
        FullName: '',
        ParentId: '',
        ShortName: '',
        // CategoryId: '',
        ManagerId: '',
        // AllowEdit: true,
        SortCode: 99,
        EnabledMark: true,
        Description: ''
      }
      this.selectedOrganizeOptions = ''
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
        }
      } else {
        this.editFormTitle = '新增'
        this.currentId = ''
        this.dialogEditFormVisible = true
      }
    },
    bindEditInfo: function() {
      getOrganizeDetail(this.currentId).then(res => {
        this.editFrom = res.ResData
        if (
          this.editFrom.EnabledMark === true
        ) {
          this.enabledMark = true
        } else {
          this.enabledMark = false
        }
        this.selectedOrganizeOptions = res.ResData.ParentId
      })
    },
    /**
     * 新增/修改保存
     */
    saveEditForm() {
      this.$refs['editFrom'].validate(valid => {
        if (valid) {
          const data = {
            ParentId: this.editFrom.ParentId,
            FullName: this.editFrom.FullName,
            ShortName: this.editFrom.ShortName,
            // CategoryId: this.editFrom.CategoryId,
            ManagerId: this.editFrom.ManagerId,
            // AllowEdit: this.editFrom.AllowEdit,
            SortCode: this.editFrom.SortCode,
            EnabledMark: this.enabledMark,
            Description: this.editFrom.Description,
            Id: this.currentId
          }
          console.log(data)
          var url = 'Organize/Insert'
          if (this.currentId !== '') {
            url = 'Organize/Update'
          }
          saveOrganize(data, url).then(res => {
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
        setOrganizeEnable(data).then(res => {
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
        deleteSoftOrganize(data).then(res => {
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
        })
          .then(function() {
            const data = {
              Ids: currentIds
            }
            return deleteOrganize(data)
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
    }
  }
}
</script>
