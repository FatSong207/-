<template>
  <el-form
    class="demo-form-inline"
    size="small"
  >
    <el-form-item label="上傳日期區間：">
      <el-date-picker
        v-model="dateRange"
        type="daterange"
        range-separator="至"
        start-placeholde="起始日期"
        end-placeholde="結束日期"
        value-format="yyyy-MM-dd"
        :picker-options="pickerOptions"
      />
    </el-form-item>
    <el-form-item label=" ">
      <font
        color="red"
      >新增上傳:身分證、存摺、居留證、駕照、健保卡、在職證明、戶籍謄本、資格證明文件、授權書、房客資料卡...等
      </font>
    </el-form-item>
    <el-form-item label=" ">
      <el-button-group>
        <el-button
          type="primary"
          size="small"
          icon="el-icon-search"
          @click="doSearch"
        >查詢</el-button>
        <el-button
          type="primary"
          size="small"
          icon="el-icon-plus"
          @click="historyFormVisible = true"
        >新增</el-button>
        <el-button
          type="success"
          size="small"
          icon="el-icon-download"
          @click="downloadformAsync"
        >下載</el-button>
        <el-button
          type="danger"
          size="small"
          icon="el-icon-delete"
          @click="doDelete"
        >刪除</el-button>
        <el-button
          type="default"
          icon="el-icon-refresh"
          size="small"
          @click="loadTableData()"
        >更新</el-button>
      </el-button-group>
    </el-form-item>
    <el-table
      v-loading="tableloading"
      :data="historytableData"
      @select="handleSelectChange"
      @select-all="handleSelectAllChange"
    >
      <el-table-column
        type="selection"
        width="30"
      />
      <el-table-column
        prop="FormName"
        label="附件名稱"
      />
      <el-table-column
        prop="UploadTime"
        label="上傳日期"
      />
      <el-table-column
        prop="Note"
        label="備註"
      />
      <el-table-column
        prop="CreatorUserId"
        label="異動人員"
      />
    </el-table>
    <div class="pagination-container">
      <el-pagination
        background
        :current-page="pagination.currentPage"
        :page-sizes="[3, 5, 10, 20, 50, 100, 200, 300, 400]"
        :page-size="pagination.pagesize"
        layout="total, sizes, prev, pager, next, jumper"
        :total="pagination.pageTotal"
        @size-change="handleSizeChange"
        @current-change="handleCurrentChange"
      />
    </div>
    <div class="rightbtn">
      <el-button
        size="small" icon="el-icon-close" @click="cancel"
      >關閉</el-button>
    </div>
    <el-dialog
      title="表單"
      :visible.sync="historyFormVisible"
      append-to-body
      width="400px"
      align="center"
    >
      <el-form inline>
        <el-form-item label="附件名稱">
          <el-input v-model="send.formname" />
        </el-form-item>
        <el-form-item label="備註">
          <el-input
            v-model="send.note"
            type="textarea"
          />
        </el-form-item>
        <el-form-item label="上傳檔案">
          <el-upload
            ref="upload"
            class="upload-demo"
            :limit="1"
            :action="httpFileUploadUrl"
            :headers="headers"
            :auto-upload="false"
            :data="send"
            :on-success="onsucess"
            accept=".pdf,.png,.jpg,.gif"
          >
            <el-form-item>
              <el-button
                size="small" type="primary" icon="el-icon-folder-add"
              >選擇文件</el-button>
              <el-button
                size="small"
                type="success"
                icon="el-icon-upload"
                @click.stop="submitUpload"
              >確認上傳</el-button>
            </el-form-item>
            <el-form-item label="*">
              <font color="red">請註意您只能上傳PDF、JPG、JPEG、PNG、GIF</font>
            </el-form-item>
          </el-upload>
        </el-form-item>
      </el-form>
    </el-dialog>
  </el-form>
</template>

<script>
import {
  getRNHistoryFormListWithPager,
  downLoadRNForm,
  GetRNFileNameById,
  DeleteRNHistoryForm
} from '@/api/chaochi/historyform/historyformservice';
import defaultSettings from '@/settings';
import { getToken } from '@/utils/auth';
export default {
  name: 'RNFormHistory',
  props: {
    id: { type: String, default: null }
  },
  data() {
    return {
      httpFileUploadUrl: defaultSettings.apiChaochiUrl + 'CustomerRN/ImgUpload',
      historytableData: [],
      historyFormVisible: false,
      headers: [],
      send: {
        sid: this.id,
        formname: '',
        note: ''
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
        sort: 'UploadTime'
      },
      searchform: {
        test: ''
      },
      currentSelected: [],
      currentId: '',
      filename: '',
      dateRange: [],
      pickerOptions: {
        shortcuts: [
          {
            text: '最近一周',
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 7);
              picker.$emit('pick', [start, end]);
            }
          },
          {
            text: '最近一個月',
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 30);
              picker.$emit('pick', [start, end]);
            }
          }
        ]
      }
    };
  },
  watch: {
    id: {
      handler() {
        this.send.sid = this.id;
        this.loadTableData();
        this.dateRange = [];
      }
    }
  },
  created() {
    this.headers = { Authorization: 'Bearer ' + (getToken() || '') };
    this.pagination.currentPage = 1;
    this.loadTableData();
  },
  methods: {
    cancel() {
      this.$emit('cancel');
    },
    InitDictItem() {},
    submitUpload() {
      this.$refs.upload.submit();
    },
    doDelete() {
      if (this.currentSelected.length === 0) {
        this.$alert('請先選擇要操作的數據', '提示');
        return false;
      } else {
        var currentIds = [];
        this.currentSelected.forEach((element) => {
          currentIds.push(element.Id);
        });

        this.$confirm('是否確認刪除所選的數據項?', '警告', {
          confirmButtonText: '確定',
          cancelButtonText: '取消',
          type: 'warning'
        })
          .then(() => {
            this.tableloading = true;
            const data = {
              Ids: currentIds
            };
            return DeleteRNHistoryForm(data);
          })
          .then((res) => {
            if (res.Success) {
              this.$message({
                message: '恭喜你，刪除成功',
                type: 'success'
              });
              this.currentSelected = '';
              this.loadTableData();
            } else {
              this.$message({
                message: res.ErrMsg,
                type: 'error'
              });
            }
          });
      }
    },
    onsucess() {
      this.$message({
        message: '恭喜你，上傳成功',
        type: 'success'
      });
      this.historyFormVisible = false;
      this.$refs.upload.clearFiles();
      this.loadTableData();
      this.send.formname = '';
      this.send.note = '';
    },
    async downloadformAsync() {
      if (
        this.currentSelected.length > 1 ||
        this.currentSelected.length === 0
      ) {
        this.$alert('請選擇一條數據進行下載', '提示');
      } else {
        this.currentId = this.currentSelected[0].Id;
        await GetRNFileNameById(this.currentId).then((res) => {
          this.filename = res.ResData;
          var reg = new RegExp('^([0-9]{14})');
          this.filename = this.filename.replace(reg, '');
        });
        downLoadRNForm(this.currentId, this.id).then((res) => {
          const blob = res; // new Blob([response.data], { type: 'image/jpeg' })
          const link = document.createElement('a');
          link.href = URL.createObjectURL(blob);
          link.download = this.filename;
          link.click();
          URL.revokeObjectURL(link.href);
        });
      }
    },
    doSearch() {
      this.tableloading = true;
      var tes = '';
      if (this.dateRange.length < 1) {
        tes = '';
      } else {
        tes = this.dateRange[0] + ',' + this.dateRange[1];
      }
      var seachHistoryFormdata = {
        CurrenetPageIndex: this.pagination.currentPage,
        PageSize: this.pagination.pagesize,
        Filter: {
          Note: tes
        },
        Keywords: this.send.sid,
        Order: this.sortableData.order,
        Sort: this.sortableData.sort
      };
      getRNHistoryFormListWithPager(seachHistoryFormdata).then((res) => {
        this.historytableData = res.ResData.Items;
        this.pagination.pageTotal = res.ResData.TotalItems;
        this.tableloading = false;
      });
    },
    loadTableData: function() {
      this.tableloading = true;
      var seachHistoryFormdata = {
        CurrenetPageIndex: this.pagination.currentPage,
        PageSize: this.pagination.pagesize,
        Keywords: this.send.sid,
        Order: this.sortableData.order,
        Sort: this.sortableData.sort
      };
      getRNHistoryFormListWithPager(seachHistoryFormdata).then((res) => {
        this.historytableData = res.ResData.Items;
        this.pagination.pageTotal = res.ResData.TotalItems;
        this.tableloading = false;
      });
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
    }
  }
};
</script>

<style></style>
