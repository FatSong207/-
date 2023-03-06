<template>
  <div>
    <div style="width: 100%; padding-bottom:15px">
      <div>
        <!-- <el-button
          type="primary"
          size="small"
          icon="el-icon-paperclip"
          @click="save()"
        >儲 存</el-button> -->
        <el-button
          type="success"
          size="small"
          icon="el-icon-download"
          @click="DownloadGuestListById()"
        >下載賓客名單</el-button>
        <el-button
          type="success"
          size="small"
          icon="el-icon-upload2"
          @click="ShowUploadDialog()"
        >上傳賓客名單</el-button>
      </div>
    </div>
    <el-form>
      <el-row>
        <el-col :span="8">
          <el-form-item>
            <el-table
              ref="gridtable"
              v-loading="tableloading"
              :data="tableData"
              border
              stripe
              highlight-current-row
              style="width: 80%"
            >
              <el-table-column
                prop="sort"
                type="index"
                label="序位"
                width="50"
                align="center"
              />
              <el-table-column
                prop="GuestName"
                label="賓客姓名"
                width="120"
              />
              <el-table-column
                prop="GusetCell"
                label="賓客手機"
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
          </el-form-item>
        </el-col>
        <el-col :span="8">
          <el-form-item label="當前賓客人數">
            <el-input
              v-model="guestCount"
              readonly
              style="width: 120px"
            />
          </el-form-item>
        </el-col>
      </el-row>

      <div class="rightbtn">
        <el-button size="small" icon="el-icon-close" @click="cancel">關閉</el-button>
      </div>
    </el-form>
    <el-dialog
      ref="dialogEditFormD"
      title="上傳賓客名單"
      :visible.sync="UploadDialogVisible"
      width="480px"
      append-to-body
    >
      <el-upload
        ref="upload"
        class="upload-demo"
        :action="httpImgFileUploadUrl"
        :headers="headers"
        drag
        :limit="1"
        accept=".csv"
        :data="send"
        :on-success="onsuccess"
        :before-upload="beforeupload"
      >
        <i class="el-icon-upload" />
        <div class="el-upload__text">將文件拖到此處，或<em>點擊上傳</em></div>
        <div
          slot="tip"
          class="el-upload__tip"
        >請註意您只能上傳.csv格式</div>
      </el-upload>
    </el-dialog>
  </div>
</template>

<script>
import {
  GetGuestList,
  DownloadGuestListById
} from '@/api/chaochi/event/eventservice';
import defaultSettings from '@/settings';
import { getToken } from '@/utils/auth';
export default {
  name: 'EventGuest',
  props: {
    id: { type: String, default: null }
  },
  data() {
    return {
      tableData: [],
      guestCount: 0,
      send: {
        id: ''
      },
      pagination: {
        currentPage: 1,
        pagesize: 20,
        pageTotal: 0
      },
      sortableData: {
        order: 'desc',
        sort: 'GuestName'
      },
      headers: [],
      httpImgFileUploadUrl:
        defaultSettings.apiChaochiUrl + 'Files/EventGuestUpload',
      UploadDialogVisible: false,
      tableloading: false
    };
  },
  watch: {
    id: {
      handler() {
        this.loadlist();
      }
    },
    tableData: {
      handler(a) {
        if (a !== undefined) {
          this.guestCount = this.tableData.length;
        }
      }
    }
  },
  created() {
    this.headers = { Authorization: 'Bearer ' + (getToken() || '') };
    this.pagination.currentPage = 1;
    this.loadlist();
  },
  methods: {
    cancel() {
      this.$emit('cancel');
    },
    beforeupload() {
      this.send.id = this.id;
    },
    onsuccess(res) {
      if (res.Success === false) {
        this.$message.error('上傳失敗');
        this.$refs.upload.clearFiles();
      } else {
        this.$message.success('恭喜你，上傳成功');
        this.$refs.upload.clearFiles();
        this.UploadDialogVisible = false;
        this.loadlist();
      }
    },
    DownloadGuestListById() {
      DownloadGuestListById(this.id).then(res => {
        const blob = res; // new Blob([response.data], { type: 'image/jpeg' })
        const link = document.createElement('a');
        link.href = URL.createObjectURL(blob);
        link.download = '賓客清單';
        link.click();
        URL.revokeObjectURL(link.href);
      });
    },
    ShowUploadDialog() {
      this.UploadDialogVisible = true;
    },
    loadlist: function() {
      this.tableloading = true;
      var seachdata = {
        CurrenetPageIndex: this.pagination.currentPage,
        PageSize: this.pagination.pagesize,
        Keywords: this.id,
        Order: this.sortableData.order,
        Sort: this.sortableData.sort
      };
      GetGuestList(seachdata).then(res => {
        this.tableData = res.ResData.Items;
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
      this.loadlist();
    },
    /**
     * 選擇當頁面
     */
    handleCurrentChange(val) {
      this.pagination.currentPage = val;
      this.loadlist();
    }
  }
};
</script>

<style>
</style>
