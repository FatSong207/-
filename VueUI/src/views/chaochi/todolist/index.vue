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
          <el-form-item label="待辦事項名稱：">
            <el-input
              v-model="searchform.Name"
              clearable
              placeholder="待辦事項名稱"
            />
          </el-form-item>
          <el-form-item label="功能模組：">
            <el-select
              v-model="searchform.Type"
              placeholder="請選擇"
            >
              <el-option
                v-for="item in Type"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              />
            </el-select>
          </el-form-item>
          <!-- <el-form-item label="狀態：">
            <el-select
              v-model="searchform.Status"
              placeholder="請選擇"
            >
              <el-option
                v-for="item in Status"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              />
            </el-select>
          </el-form-item> -->
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
      <!-- <div class="list-btn-container">
        <el-button-group>
          <el-button v-hasPermi="['ToDoList/Add']" type="primary" icon="el-icon-plus" size="small" @click="ShowEditOrViewDialog()">新增</el-button>
          <el-button v-hasPermi="['ToDoList/Edit']" type="primary" icon="el-icon-edit" class="el-button-modify" size="small" @click="ShowEditOrViewDialog('edit')">修改</el-button>
          <el-button v-hasPermi="['ToDoList/Enable']" type="info" icon="el-icon-video-pause" size="small" @click="setEnable('0')">禁用</el-button>
          <el-button v-hasPermi="['ToDoList/Enable']" type="success" icon="el-icon-video-play" size="small" @click="setEnable('1')">啟用</el-button>
          <el-button v-hasPermi="['ToDoList/DeleteSoft']" type="warning" icon="el-icon-delete" size="small" @click="deleteSoft('0')">軟刪除</el-button>
          <el-button v-hasPermi="['ToDoList/Delete']" type="danger" icon="el-icon-delete" size="small" @click="deletePhysics()">刪除</el-button>
          <el-button type="default" icon="el-icon-refresh" size="small" @click="loadTableData()">刷新</el-button>
        </el-button-group>
      </div> -->
      <el-table
        ref="gridtable"
        v-loading="tableloading"
        :data="tableData"
        border
        stripe
        highlight-current-row
        style="width: 100%"
        :default-sort="{ prop: 'SortCode', order: 'ascending' }"
        @select="handleSelectChange"
        @select-all="handleSelectAllChange"
        @sort-change="handleSortChange"
        @row-dblclick="rowdblclick"
      >
        <!-- <el-table-column type="selection" width="50" /> -->
        <el-table-column
          prop="Type"
          label="功能類型" sortable="custom"
        />
        <el-table-column
          prop="TypeId"
          label="功能模組ID" sortable="custom"
        />
        <!-- <el-table-column prop="TypeId" label="審核單號" sortable="custom" /> -->
        <el-table-column
          prop="Name"
          label="待辧事項名稱" sortable="custom"
        />
        <!-- <el-table-column
          prop="Status"
          label="狀態" sortable="custom"
        /> -->
      </el-table>
      <div class="pagination-container">
        <el-pagination
          background
          :current-page="pagination.currentPage"
          :page-sizes="[5, 10, 20, 50, 100, 200, 300, 400]"
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
import {
  getToDoListListWithPager,
  getToDoListDetail
} from '@/api/chaochi/todolist/todolistservice';
import elDragDialog from '@/directive/el-drag-dialog'; // 彈窗可移動
import { mapGetters } from 'vuex';
export default {
  name: 'ToDoList', // 需與菜單的功能編碼一致
  directives: { elDragDialog },
  data() {
    return {
      searchform: {
        keywords: '',
        Type: '',
        Status: ''
      },
      Type: [
        { value: '', label: '不指定' },
        { value: '合約', label: '合約' },
        // { value: '評分管理', label: '評分管理' },
        // { value: '客訴管理', label: '客訴管理' },
        // { value: '黑名單', label: '黑名單' }
        { value: '入居者審查', label: '入居者審查' }
      ],
      // Status: [
      //   { value: '', label: '不指定' },
      //   { value: '待審核', label: '待審核' },
      //   { value: '已審核', label: '已審核' }
      // ],
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
      editFrom: {},
      rules: {},
      formLabelWidth: '80px',
      currentId: '', // 當前操作對象的ID值，主要用于修改
      currentSelected: [],
      typeId: '',
      typeData: ''
    };
  },
  computed: {
    ...mapGetters(['name']),
    ...mapGetters(['todolistcount'])
  },
  watch: {
    todolistcount: {
      handler() {
        console.log('todolistcountChange');
        this.loadTableData();
      }
    }
  },
  created() {
    this.pagination.currentPage = 1;
    this.InitDictItem();
    this.loadTableData();
  },
  methods: {
    /**
     * 初始化數據
     */
    InitDictItem() {},

    // 取消按鈕
    cancel() {
      this.dialogEditFormVisible = false;
      this.reset();
    },
    // 表單重置
    reset() {
      this.editFrom = {
        Type: '',
        TypeId: '',
        Name: '',
        Status: '',
        CreatorTime: '',
        CreatorUserId: '',
        LastModifyTime: '',
        LastModifyUserId: ''

        // 需個性化處理內容
      };
      this.resetForm('editFrom');
    },
    /**
     * 加載頁面table數據
     */
    loadTableData: function() {
      this.tableloading = true;
      var seachdata = {
        CurrenetPageIndex: this.pagination.currentPage,
        PageSize: this.pagination.pagesize,
        Keywords: this.name,
        Order: this.sortableData.order,
        Sort: this.sortableData.sort
      };
      getToDoListListWithPager(seachdata).then((res) => {
        this.tableData = res.ResData.Items;
        this.pagination.pageTotal = res.ResData.TotalItems;
        this.tableloading = false;
      });
    },
    /**
     * 點擊查詢
     */
    handleSearch: function() {
      this.pagination.currentPage = 1;
      this.loadTableData();
    },

    /**
     * 新增、修改或查看明細信息（綁定顯示數據）     *
     */
    ShowEditOrViewDialog: function(view, row, column, event) {
      this.reset();
      if (view !== undefined) {
        if (
          this.currentSelected.length > 1 ||
          this.currentSelected.length === 0
        ) {
          this.$alert('請選擇一條數據進行編輯/修改', '提示');
        } else {
          this.currentId = this.currentSelected[0].Id;
          this.typeId = this.currentSelected[0].TypeId;
          this.typeData = this.currentSelected[0].TypeData;
          this.editFormTitle = '編輯';
          // this.dialogEditFormVisible = true
          this.bindEditInfo(row, column, event);
        }
      } else {
        this.editFormTitle = '新增';
        this.currentId = '';
        this.dialogEditFormVisible = true;
      }
    },
    bindEditInfo: function(row, column, event) {
      console.log(row, column, event);

      getToDoListDetail(this.currentId).then((res) => {
        var p = {
          moduleName: '',
          info: res.ResData,
          todoListItemId: this.currentId,
          typeId: this.typeId,
          typeData: this.typeData
        };
        switch (row.Type) {
          case '合約':
            p.moduleName = '合約';
            this.$router.push({
              name: 'ToDoListDetail',
              params: p
            });
            break;
          case '入居者審查':
            p.moduleName = '入居者審查';
            this.$router.push({
              name: 'ToDoListDetail',
              params: p
            });
            break;
          default:
            break;
        }
        console.log(res.ResData);

        // 需個性化處理內容
      });
    },
    /**
     *雙擊開啟明細
     */
    rowdblclick(row, column, event) {
      this.$refs.gridtable.clearSelection();
      this.currentSelected = '';
      this.rerenderGridtable = Date.now(); // 強制重繪 https://bbchin.com/archives/el-table-rerender
      this.$nextTick(function() {
        // https://ithelp.ithome.com.tw/articles/10240669
        this.$refs.gridtable.toggleRowSelection(row, true);
        this.currentSelected = this.$refs.gridtable.selection;
        this.ShowEditOrViewDialog('edit', row, column, event);
      });
    },
    /**
     * 當表格的排序條件發生變化的時候會觸發該事件
     */
    handleSortChange: function(column) {
      this.sortableData.sort = column.prop;
      if (column.order === 'ascending') {
        this.sortableData.order = 'asc';
      } else {
        this.sortableData.order = 'desc';
      }
      this.loadTableData();
    },
    /**
     * 當用戶手動勾選checkbox數據行事件
     */
    handleSelectChange: function(selection, row) {
      this.currentSelected = selection;
    },
    /**
     * 當用戶手動勾選全選checkbox事件
     */
    handleSelectAllChange: function(selection) {
      this.currentSelected = selection;
    },
    /**
     * 選擇每頁顯示數量
     */
    handleSizeChange(val) {
      this.pagination.pagesize = val;
      this.pagination.currentPage = 1;
      this.loadTableData();
    },
    /**
     * 選擇當頁面
     */
    handleCurrentChange(val) {
      this.pagination.currentPage = val;
      this.loadTableData();
    }
  }
};
</script>

<style></style>
