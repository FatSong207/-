<template>
  <div class="app-container">
    <div class="filter-container">
      <el-card>
        <el-form
          ref="searchform"
          inline
          :model="searchform"
          class="demo-form-inline"
          size="small"
        >
          <el-form-item label="名單來源：">
            <el-select
              v-model="searchform.BelongCompany"
              placeholder="請選擇"
              filterable
              clearable
            >
              <el-option
                v-for="item in orgList"
                :key="item.Id"
                :label="item.FullName"
                :value="item.Id"
              />
            </el-select>
            <!-- <el-input
              v-model="searchform.ReportDept"
              clearable
              placeholder="名單來源"
            /> -->
          </el-form-item>
          <el-form-item label="活動代號：">
            <el-input
              v-model="searchform.EventId"
              clearable
              placeholder="名稱"
              style="width: 160px"
            />
          </el-form-item>
          <el-form-item label="活動名稱：">
            <el-input
              v-model="searchform.EventName"
              clearable
              placeholder="名稱"
              style="width: 160px"
            />
          </el-form-item>
          <el-row>
            <el-form-item>
              <el-button
                type="primary"
                size="small"
                icon="el-icon-search"
                @click="handleSearch()"
              >查詢</el-button>
            </el-form-item>
          </el-row>
        </el-form>
      </el-card>
    </div>
    <el-card>
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
      >
        <el-table-column
          prop="EventId"
          label="活動代號"
          sortable="custom"
          width="160"
        />
        <el-table-column
          prop="EventName"
          label="活動名稱" sortable="custom"
        />
        <el-table-column
          prop="StartDate"
          label="活動起始"
          sortable="custom"
          width="120"
        />
        <el-table-column
          prop="EndDate"
          label="活動結束"
          sortable="custom"
          width="120"
        />
        <el-table-column
          prop="BelongCompanyName"
          label="活動所屬公司"
          sortable="custom"
        />
        <el-table-column
          prop="Manager"
          label="活動管理人"
          sortable="custom"
          width="120"
        />
        <el-table-column
          prop="EventType"
          label="活動類型"
          sortable="custom"
          width="120"
        />
        <el-table-column
          label="操作"
          width="110"
        >
          <template slot-scope="scope">
            <el-button
              type="text" @click="handleClick(scope.row)"
            >填寫問卷</el-button>
          </template>
        </el-table-column>
        <!-- <el-table-column
          prop="LastModifyTime"
          label="最後異動日期"
          sortable="custom"
          width="160"
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
  FindWithPagerSearchAsyncED,
  DirectToSignUpWithSalesId
} from '@/api/chaochi/event/eventservice';
import { GetAllOrgForSelect } from '@/api/security/organizeservice';
import { mapGetters } from 'vuex';
import elDragDialog from '@/directive/el-drag-dialog'; // 彈窗可移動
export default {
  name: 'EventDashboard', // 需與菜單的功能編碼一致
  directives: { elDragDialog },
  data() {
    return {
      searchform: {
        EventId: '',
        EventName: ''
      },
      tableData: [],
      orgList: [],
      loading: false,
      tableloading: true,
      saveloading: false,
      pagination: {
        currentPage: 1,
        pagesize: 20,
        pageTotal: 0
      },
      sortableData: {
        order: 'desc',
        sort: 'CreatorTime'
      },
      currentId: '', // 當前操作對象的ID值，主要用于修改
      currentType: '',
      currentSelected: []
    };
  },
  computed: {
    ...mapGetters(['name', 'dept'])
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
    InitDictItem() {
      GetAllOrgForSelect().then((res) => {
        this.orgList = res.ResData;
      });
    },

    // 取消按鈕
    cancel() {
      this.dialogEditFormVisible = false;
      this.currentSelected = '';
      this.loadTableData();
    },
    handleClick(row) {
      // console.log(row);
      DirectToSignUpWithSalesId(row.EventId).then((res) => {
        // console.log(res.ResData);
        // this.$message.success(`${row.EventName}--即將導向至填寫問卷網!`);
        var newTab = window.open(res.ResData, '_blank');
        newTab.location;
      });
    },
    /**
     * 加載頁面table數據
     */
    loadTableData: function() {
      this.tableloading = true;
      var filter = this.searchform;
      var seachdata = {
        CurrenetPageIndex: this.pagination.currentPage,
        PageSize: this.pagination.pagesize,
        Filter: filter,
        Keywords: this.searchform.keywords,
        Order: this.sortableData.order,
        Sort: this.sortableData.sort
      };
      FindWithPagerSearchAsyncED(seachdata).then((res) => {
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
