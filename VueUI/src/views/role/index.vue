<template>
  <div class="app-container">
    <div class="filter-container">
      <el-card>
        <el-form ref="searchform" :inline="true" :model="searchform" class="demo-form-inline" size="small">
          <el-form-item label="角色：">
            <el-input v-model="searchform.name" clearable placeholder="角色名稱或編碼" />
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
            v-hasPermi="['Role/Add']"
            type="primary"
            icon="el-icon-plus"
            size="small"
            @click="ShowEditOrViewDialog()"
          >新增</el-button>
          <el-button
            v-hasPermi="['Role/Edit']"
            type="primary"
            icon="el-icon-edit"
            class="el-button-modify"
            size="small"
            @click="ShowEditOrViewDialog('edit')"
          >修改</el-button>
          <el-button
            v-hasPermi="['Role/Enable']"
            type="info"
            icon="el-icon-video-pause"
            size="small"
            @click="setEnable('0')"
          >禁用</el-button>
          <el-button
            v-hasPermi="['Role/Enable']"
            type="success"
            icon="el-icon-video-play"
            size="small"
            @click="setEnable('1')"
          >啟用</el-button>
          <el-button
            v-hasPermi="['Role/DeleteSoft']"
            type="warning"
            icon="el-icon-delete"
            size="small"
            @click="deleteSoft('0')"
          >軟刪除</el-button>
          <el-button
            v-hasPermi="['Role/Delete']"
            type="danger"
            icon="el-icon-delete"
            size="small"
            @click="deletePhysics()"
          >刪除</el-button>
          <el-button
            v-hasPermi="['Role/SetAuthorize']"
            type="default"
            icon="el-icon-s-custom"
            size="small"
            @click="handleSetAuth()"
          >權限</el-button>
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
        :default-sort="{ prop: 'SortCode', order: 'descending' }"
        @select="handleSelectChange"
        @select-all="handleSelectAllChange"
        @sort-change="handleSortChange"
      >
        <el-table-column type="selection" width="30" />
        <el-table-column prop="FullName" label="角色" sortable="custom" width="180" />
        <el-table-column prop="EnCode" label="角色編碼" sortable="custom" width="180" />
        <el-table-column prop="Type" label="類型" sortable="custom" width="90" align="center">
          <template slot-scope="scope">
            <slot v-if="scope.row.Type === '1'">系統角色</slot>
            <slot v-else-if="scope.row.Type === '2'">業務角色</slot>
            <slot v-else-if="scope.row.Type === '3'">其他角色</slot>
          </template>
        </el-table-column>
        <el-table-column prop="OrganizeName" label="公司/單位" />
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
        <el-table-column prop="CreatorTime" label="建立時間" sortable />
        <el-table-column prop="LastModifyTime" label="更新時間" sortable />
      </el-table>
      <div class="pagination-container">
        <el-pagination background :current-page="pagination.currentPage" :page-sizes="[5, 10, 20, 50, 100, 200, 300, 400]" :page-size="pagination.pagesize" layout="total, sizes, prev, pager, next, jumper" :total="pagination.pageTotal" @size-change="handleSizeChange" @current-change="handleCurrentChange" />
      </div>
    </el-card>
    <el-dialog ref="dialogEditForm" v-el-drag-dialog :title="editFormTitle" :visible.sync="dialogEditFormVisible" width="640px">
      <el-form ref="editFrom" :inline="true" :model="editFrom" :rules="rules" class="demo-form-inline">
        <el-form-item label="角色名稱" :label-width="formLabelWidth" prop="FullName">
          <el-input v-model="editFrom.FullName" placeholder="請輸入角色名稱" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="角色編碼" :label-width="formLabelWidth" prop="EnCode">
          <el-input v-model="editFrom.EnCode" placeholder="請輸入角色編碼" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="角色類型" :label-width="formLabelWidth" prop="RoleType">
          <el-select v-model="editFrom.RoleType" multiple placeholder="請選角色類型">
            <el-option v-for="item in selectRoleType" :key="item.Id" :label="item.ItemName" :value="item.ItemCode" />
          </el-select>
        </el-form-item>
        <el-form-item label="公司/單位" :label-width="formLabelWidth" prop="OrganizeId">
          <el-cascader
            :key="cascaderKey"
            v-model="selectedOrganizeOptions"
            :options="selectOrganize"
            filterable
            :props="{
              label: 'FullName',
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
        <el-form-item label="排序" :label-width="formLabelWidth" prop="SortCode">
          <el-input v-model.number="editFrom.SortCode" placeholder="請輸入排序,默認為99" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="說明" :label-width="formLabelWidth" prop="Description">
          <el-input v-model="editFrom.Description" autocomplete="off" clearable />
        </el-form-item>
        <el-form-item label="是否啟用" :label-width="formLabelWidth" prop="EnabledMark">
          <el-radio-group v-model="editFrom.EnabledMark">
            <el-radio :label="true">是</el-radio>
            <el-radio :label="false">否</el-radio>
          </el-radio-group>
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogEditFormVisible = false">關 閉</el-button>
        <el-button type="primary" @click="saveEditForm()">確 認</el-button>
      </div>
    </el-dialog>

    <el-dialog ref="dialogSetAuthForm" v-el-drag-dialog title="權限" :visible.sync="dialogSetAuthFormVisible" width="70%">
      <el-tabs v-model="ActionName" type="border-card" @tab-click="handleClick">
        <el-tab-pane label="可用系統" name="treeSystem">
          <el-card class="box-card">
            <el-tree ref="treeSystem" :data="treeSystemData" :check-strictly="true" empty-text="加載中，請稍后" show-checkbox default-expand-all node-key="Id" highlight-current :props="{ label: 'FullName', children: 'Children' }" />
          </el-card>
        </el-tab-pane>
        <el-tab-pane label="功能清單" name="treeFunction">
          <el-card class="box-card">
            <el-tree ref="treeFunction" :data="treeFuntionData" :check-strictly="true" empty-text="加載中，請稍后" show-checkbox default-expand-all node-key="Id" highlight-current :props="{ label: 'FullName', children: 'Children',disabled:'IsShow'}" />
          </el-card>
        </el-tab-pane>
        <el-tab-pane label="權限" name="treeOrganize">
          <el-card class="box-card">
            <el-tree ref="treeOrganize" :data="treeOrganizeData" :check-strictly="true" empty-text="加載中，請稍后" show-checkbox default-expand-all node-key="Id" highlight-current :props="{ label: 'FullName', children: 'Children' }" />
          </el-card>
        </el-tab-pane>
      </el-tabs>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogSetAuthFormVisible = false">取 消</el-button>
        <el-button type="primary" @click="saveRoleAuthorize()">保 存</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>

import { getListItemDetailsByCode } from '@/api/basebasic'
import {
  getRoleListWithPager, getRoleDetail, saveRole,
  setRoleEnable, deleteSoftRole, deleteRole, getRoleAuthorizeFunction,
  getAllFunctionTree, getAllRoleDataByRoleId, saveRoleAuthorize
} from '@/api/security/roleservice'

import { getAllOrganizeTreeTable } from '@/api/security/organizeservice'
import { getAllSystemTypeList } from '@/api/developers/systemtypeservice'
import { Loading } from 'element-ui'
import elDragDialog from '@/directive/el-drag-dialog' // base on element-ui
export default {
  name: 'Role',
  directives: { elDragDialog },
  data() {
    return {
      searchform: {
        name: ''
      },
      loadBtnFunc: [],
      selectedOrganizeOptions: '',
      selectOrganize: [],
      selectRoleType: [],
      tableData: [],
      tableloading: true,
      pagination: {
        currentPage: 1,
        pagesize: 20,
        pageTotal: 0
      },
      sortableData: {
        order: 'desc',
        sort: 'SortCode'
      },
      dialogEditFormVisible: false,
      editFormTitle: '',
      editFrom: {
        RoleType: []
      },
      rules: {
        FullName: [
          { required: true, message: '請輸入角色名稱', trigger: 'blur' },
          { min: 2, max: 50, message: '長度在 2 到 50 個字符', trigger: 'blur' }
        ],
        EnCode: [
          { required: true, message: '請輸入角色編碼', trigger: 'blur' },
          { min: 2, max: 50, message: '長度在 2 到 50 個字符', trigger: 'blur' }
        ],
        RoleType: [
          { required: true, message: '請角色類型', trigger: 'blur' }
        ],
        OrganizeId: [
          { required: true, message: '請選擇所屬部門', trigger: 'blur' }
        ]
      },
      formLabelWidth: '80px',
      currentId: '', // 當前操作對象的ID值，主要用于修改
      currentSelected: [],
      pageLoading: '',
      dialogSetAuthFormVisible: false,
      treeFuntionData: [],
      default_select: [],
      defaultOrganize_select: [],
      treeOrganizeData: [],
      defaultSystem_select: [],
      treeSystemData: [],
      cascaderKey: 0,
      ActionName: 'treeSystem'
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
      getListItemDetailsByCode('RoleType').then(res => {
        this.selectRoleType = res.ResData
      })

      getAllOrganizeTreeTable().then(res => {
        ++this.cascaderKey
        this.selectOrganize = res.ResData
      })

      getAllFunctionTree().then(res => {
        this.treeFuntionData = res.ResData
      })
      getAllOrganizeTreeTable().then(res => {
        this.treeOrganizeData = res.ResData
      })
      getAllSystemTypeList().then(res => {
        this.treeSystemData = res.ResData
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
      getRoleListWithPager(seachdata).then(res => {
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
     *選擇組織
     */
    handleSelectOrganizeChange: function() {
      console.log(this.selectedOrganizeOptions)
      this.editFrom.OrganizeId = this.selectedOrganizeOptions
    },
    // 表單重置
    reset() {
      this.editFrom = {
        FullName: '',
        EnCode: '',
        RoleType: [],
        OrganizeId: '',
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
      }
    },
    bindEditInfo: function() {
      getRoleDetail(this.currentId).then(res => {
        this.editFrom = res.ResData
        this.selectedOrganizeOptions = res.ResData.OrganizeId
      })
    },
    /**
     * 新增/修改保存
     */
    saveEditForm() {
      this.$refs['editFrom'].validate((valid) => {
        if (valid) {
          var loadop = {
            lock: true,
            text: '正在保存數據，請耐心等待...',
            spinner: 'el-icon-loading',
            background: 'rgba(0, 0, 0, 0.7)'
          }
          this.pageLoading = Loading.service(loadop)
          const data = {
            'OrganizeId': this.editFrom.OrganizeId,
            'EnCode': this.editFrom.EnCode,
            'FullName': this.editFrom.FullName,
            'Type': this.editFrom.RoleType,
            'SortCode': this.editFrom.SortCode,
            'EnabledMark': this.editFrom.EnabledMark,
            'Description': this.editFrom.Description,
            'Id': this.currentId
          }
          var url = 'Role/Insert'
          if (this.currentId !== '') {
            url = 'Role/Update'
          }
          saveRole(data, url).then(res => {
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
            this.pageLoading.close()
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
        setRoleEnable(data).then(res => {
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
        deleteSoftRole(data).then(res => {
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
          return deleteRole(data)
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
    /**
     * 設置權限
     */
    handleSetAuth: function() {
      if (this.currentSelected.length === 0) {
        this.$alert('請先選擇要操作的數據', '提示')
        return false
      } else {
        this.currentId = this.currentSelected[0].Id
        this.dialogSetAuthFormVisible = true

        this.ActionName = 'treeSystem'
        this.default_select = []
        this.defaultOrganize_select = []
        this.defaultSystem_select = []

        const datar = {
          roleId: this.currentId
        }
        getAllRoleDataByRoleId(datar).then(res => {
          this.defaultOrganize_select = res.ResData
          setTimeout(this.restFrom(), 500)
        })

        const data = {
          roleId: this.currentId,
          itemType: '1,2'
        }
        getRoleAuthorizeFunction(data).then(res => {
          this.default_select = res.ResData
          setTimeout(this.restFrom(), 500)
        })

        const datas = {
          roleId: this.currentId,
          itemType: '0'
        }
        getRoleAuthorizeFunction(datas).then(res => {
          this.defaultSystem_select = res.ResData
          setTimeout(this.restFrom(), 500)
        })
      }
    },
    handleClick: function() {
      // this.restFrom()
    },
    // 重置
    restFrom: function() {
      var that = this
      this.$nextTick(() => {
        this.$refs.treeFunction.setCheckedKeys(that.default_select)
        this.$refs.treeSystem.setCheckedKeys(that.defaultSystem_select)
        this.$refs.treeOrganize.setCheckedKeys(that.defaultOrganize_select)
      })
    },
    /**
     * 保存權限
     */
    saveRoleAuthorize: function() {
      var loadop = {
        lock: true,
        text: '正在保存數據，請耐心等待...',
        spinner: 'el-icon-loading',
        background: 'rgba(0, 0, 0, 0.7)'
      }
      this.pageLoading = Loading.service(loadop)
      // 目前被選中的菜單節點
      const checkedKeysTreeFunction = this.$refs.treeFunction.getCheckedKeys()
      // 半選中的菜單節點
      const halfCheckedKeysTreeFunction = this.$refs.treeFunction.getHalfCheckedKeys()
      checkedKeysTreeFunction.unshift.apply(checkedKeysTreeFunction, halfCheckedKeysTreeFunction)

      const checkedKeysTreeOrganize = this.$refs.treeOrganize.getCheckedKeys()
      const halfCheckedKeysTreeOrganize = this.$refs.treeOrganize.getHalfCheckedKeys()
      checkedKeysTreeOrganize.unshift.apply(checkedKeysTreeOrganize, halfCheckedKeysTreeOrganize)

      const checkedKeysTreeSystem = this.$refs.treeSystem.getCheckedKeys()
      const halfCheckedKeysTreeSystem = this.$refs.treeSystem.getHalfCheckedKeys()
      checkedKeysTreeSystem.unshift.apply(checkedKeysTreeSystem, halfCheckedKeysTreeSystem)

      var data = {
        'RoleFunctios': checkedKeysTreeFunction,
        'RoleData': checkedKeysTreeOrganize,
        'RoleSystem': checkedKeysTreeSystem,
        'RoleId': this.currentId
      }
      saveRoleAuthorize(data).then(res => {
        if (res.Success) {
          this.$message({
            message: '恭喜你，操作成功',
            type: 'success'
          })
          this.currentSelected = ''
          // this.default_select = []
          // this.defaultOrganize_select = []
          // this.defaultSystem_select = []
          this.dialogSetAuthFormVisible = false
        } else {
          this.$message({
            message: res.ErrMsg,
            type: 'error'
          })
        }
        this.pageLoading.close()
      })
    }
  }
}
</script>

<style>
.el-cascader {
  width: 100%;
}
.box-card {
  max-height: 600px;
  overflow-y: scroll;
}
</style>
