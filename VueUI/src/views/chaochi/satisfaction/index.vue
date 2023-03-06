<template>
  <div class="app-container">
    <!-- <div class="filter-container">
      <el-card>
        <el-form
          ref="searchform"
          :inline="true"
          :model="searchform"
          class="demo-form-inline"
          size="small"
        >
          <el-form-item label="名稱：">
            <el-input
              v-model="searchform.keywords"
              clearable
              placeholder="名稱"
            />
          </el-form-item>
          <el-form-item>
            <el-button
              type="primary"
              @click="handleSearch()"
            >查詢</el-button>
          </el-form-item>
        </el-form>
      </el-card>
    </div> -->
    <el-card>
      <div class="list-btn-container">
        <el-button-group>
          <el-button
            v-hasPermi="['Satisfaction/Edit']"
            type="primary"
            icon="el-icon-edit"
            class="el-button-modify"
            size="small"
            @click="ShowEditOrViewDialog('edit')"
          >修改</el-button>
        </el-button-group>
      </div>
      <el-table
        ref="gridtable"
        v-loading="tableloading"
        :data="tableData"
        border
        stripe
        highlight-current-row
        style="width: 40%"
        @select="handleSelectChange"
        @select-all="handleSelectAllChange"
      >
        <el-table-column
          type="selection"
          width="40"
        />
        <el-table-column
          prop="Type"
          label="問卷類別"
        />
        <el-table-column
          prop="LastModifyUserName"
          label="最後異動人"
        />
        <el-table-column
          prop="LastModifyTime"
          label="最後異動日期"
        />
      </el-table>
      <!-- <div class="pagination-container">
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
      </div> -->
    </el-card>
    <el-dialog
      ref="dialogEditForm"
      v-el-drag-dialog
      :title="editFormTitle"
      :visible.sync="dialogEditFormVisible"
      fullscreen
      append-to-body
    >
      <el-card v-loading="saveloading">
        <div style="width: 100%; padding-bottom: 15px">
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
            @click="DownQFile()"
          >問卷定義檔下載</el-button>
          <el-button
            type="success"
            size="small"
            icon="el-icon-upload2"
            @click="ShowDialogUploadQFile()"
          >問卷定義檔上傳</el-button>
        </div>
        <el-form
          ref="editForm"
          :model="editForm" :rules="rules"
        >
          <el-form-item
            label="問卷類別："
            :label-width="formLabelWidth"
            prop="Type"
            style="width: 25%"
          >
            <el-input
              v-model="editForm.Type"
              autocomplete="off"
              clearable
              readonly
            />
          </el-form-item>
          <el-form-item
            label="當前問卷題數："
            :label-width="formLabelWidth"
            prop="QuestionCount"
            style="width: 15%"
          >
            <el-input
              v-model="editForm.QuestionCount"
              autocomplete="off"
              clearable
              readonly
            />
          </el-form-item>
          <el-form-item
            label="當前問卷定義檔："
            :label-width="formLabelWidth"
            prop="QFileName"
            style="width: 35%"
            aria-readonly
          >
            <el-input
              v-model="editForm.QFileName"
              autocomplete="off"
              clearable
              readonly
            />
          </el-form-item>
          <el-form-item
            label="填寫網址："
            :label-width="formLabelWidth"
            style="width: 35%"
            aria-readonly
          >
            <el-input
              v-model="editForm.QUrl"
              autocomplete="off"
              clearable
              readonly
            />
          </el-form-item>
          <el-form-item
            label="問卷開頭語："
            :label-width="formLabelWidth"
            prop="QBeginWords"
            style="width: 35%"
          >
            <el-input
              v-model="editForm.QBeginWords"
              placeholder="請輸入問卷開頭語"
              type="textarea"
              :rows="3"
            />
          </el-form-item>
          <el-form-item
            label="問卷結尾語："
            :label-width="formLabelWidth"
            prop="QEndWords"
            style="width: 35%"
          >
            <el-input
              v-model="editForm.QEndWords"
              placeholder="請輸入問卷結尾語"
              type="textarea"
              :rows="3"
            />
          </el-form-item>
        </el-form>

        <div class="rightbtn">
          <!-- <el-button
            type="primary"
            size="small"
            icon="el-icon-paperclip"
            @click="save()"
          >儲存</el-button> -->
          <el-button
            type="primary"
            size="small"
            icon="el-icon-printer"
            @click="GenSighUp()"
          >生成問卷</el-button>
          <el-button
            size="small" icon="el-icon-close" @click="cancel"
          >關閉</el-button>
        </div>
      </el-card>
      <!-- <div
        slot="footer"
        class="dialog-footer"
      >
        <el-button @click="cancel">關 閉</el-button>
      </div> -->
    </el-dialog>

    <el-dialog
      ref="dialogEditFormD"
      title="問卷定義檔上傳"
      :visible.sync="UploadDialogVisible"
      width="480px"
      append-to-body
    >
      <el-upload
        ref="upload"
        class="upload-demo"
        :action="httpImgFileUploadUrl"
        :headers="headers"
        :auto-upload="false"
        drag
        :limit="1"
        accept=".xlsx"
        :data="send"
        :on-success="onsuccess"
        :before-upload="beforeupload"
        :on-change="onchange"
      >
        <i class="el-icon-upload" />
        <div class="el-upload__text">將文件拖到此處，或<em>點擊上傳</em></div>
        <div
          slot="tip"
          class="el-upload__tip"
        >請註意您只能上傳.xlsx格式</div>
      </el-upload>
    </el-dialog>
  </div>
</template>

<script>
import {
  getSatisfactionListWithPager,
  DownloadSatisfactionTopicById,
  getSatisfactionDetail,
  GenSignUpForSatisfaction,
  ExportToXLSX
} from '@/api/chaochi/satisfaction/satisfactionservice';
import defaultSettings from '@/settings';
import { getToken } from '@/utils/auth';

import elDragDialog from '@/directive/el-drag-dialog'; // 彈窗可移動
export default {
  name: 'Satisfaction', // 需與菜單的功能編碼一致
  directives: { elDragDialog },
  data() {
    return {
      searchform: {
        keywords: ''
      },
      send: {
        id: ''
      },
      headers: [],
      httpImgFileUploadUrl:
        defaultSettings.apiChaochiUrl + 'Files/SatisfactionUpload',
      tableData: [],
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
      dialogEditFormVisible: false,
      UploadDialogVisible: false,
      editFormTitle: '',
      editForm: {},
      rules: {
        QBeginWords: [
          { required: true, trigger: 'change', message: '請輸入開頭語' }
        ],
        QEndWords: [
          { required: true, trigger: 'change', message: '請輸入結尾語' }
        ]
      },
      formLabelWidth: '135px',
      currentId: '', // 當前操作對象的ID值，主要用于修改
      currentSelected: []
    };
  },
  created() {
    this.pagination.currentPage = 1;
    this.headers = { Authorization: 'Bearer ' + (getToken() || '') };
    this.InitDictItem();
    this.loadTableData();
  },
  methods: {
    /**
     * 初始化數據
     */
    InitDictItem() {},
    onchange(file) {
      if (file.status !== 'ready') return;
      this.$confirm(
        '改變調查問卷題目，系統將<br/><b style="color:red">1.自動匯出原題目對應之統計報表<br/>2.初始化此調查問卷(這意味著您將失去原題目之定義檔)</b><br/>確認上傳定義更新檔案？',
        '警告',
        {
          confirmButtonText: '確定',
          cancelButtonText: '取消',

          dangerouslyUseHTMLString: true,
          type: 'warning'
        }
      )
        .then(() => {
          this.send.id = this.editForm.Id;
          ExportToXLSX(this.send.id)
            .then((res) => {
              const blob = res; // new Blob([response.data], { type: 'image/jpeg' })
              const link = document.createElement('a');
              link.href = URL.createObjectURL(blob);
              link.download = this.editForm.Type + '_問卷統計.xlsx';
              link.click();
              URL.revokeObjectURL(link.href);
              this.$refs.upload.submit();
            })
            .catch(() => {
              this.$message.error('產生統計報表失敗');
            });
        })
        .catch(() => {
          this.$refs.upload.clearFiles();
        });
    },
    beforeupload() {},
    onsuccess(res) {
      if (res.Success === false) {
        this.$message.error('上傳失敗');
        this.$refs.upload.clearFiles();
      } else {
        this.$message.success('恭喜你，上傳成功');
        this.$refs.upload.clearFiles();
        this.bindEditInfo();
        this.UploadDialogVisible = false;
      }
    },
    DownQFile() {
      DownloadSatisfactionTopicById(this.currentId).then((res) => {
        const blob = res; // new Blob([response.data], { type: 'image/jpeg' })
        const link = document.createElement('a');
        link.href = URL.createObjectURL(blob);
        link.download = this.editForm.QFileName;
        link.click();
        URL.revokeObjectURL(link.href);
      });
    },
    ShowDialogUploadQFile() {
      this.UploadDialogVisible = true;
    },
    // 取消按鈕
    cancel() {
      this.dialogEditFormVisible = false;
      this.reset();
      this.currentSelected = '';
      this.loadTableData();
      this.InitDictItem();
    },
    // 表單重置
    reset() {
      this.editForm = {
        Type: '',
        QuestionCount: '',
        QFileName: '',
        QBeginWords: '',
        QEndWords: '',
        CreatorUserId: '',
        CreatorTime: '',
        LastModifyUserId: '',
        LastModifyTime: ''

        // 需個性化處理內容
      };
      this.resetForm('editForm');
    },
    /**
     * 加載頁面table數據
     */
    loadTableData: function() {
      this.tableloading = true;
      var seachdata = {
        CurrenetPageIndex: this.pagination.currentPage,
        PageSize: this.pagination.pagesize,
        Keywords: this.searchform.keywords,
        Order: this.sortableData.order,
        Sort: this.sortableData.sort
      };
      getSatisfactionListWithPager(seachdata).then((res) => {
        this.tableData = res.ResData.Items;
        this.pagination.pageTotal = res.ResData.TotalItems;
        this.tableloading = false;
      });
    },

    /**
     * 新增、修改或查看明細信息（綁定顯示數據）     *
     */
    ShowEditOrViewDialog: function(view) {
      this.reset();
      if (view !== undefined) {
        if (
          this.currentSelected.length > 1 ||
          this.currentSelected.length === 0
        ) {
          this.$alert('請選擇一條數據進行編輯/修改', '提示');
        } else {
          this.currentId = this.currentSelected[0].Id;
          this.editFormTitle = '編輯';
          this.dialogEditFormVisible = true;
          this.bindEditInfo();
        }
      } else {
        this.editFormTitle = '新增';
        this.currentId = '';
        this.dialogEditFormVisible = true;
      }
    },
    bindEditInfo: function() {
      getSatisfactionDetail(this.currentId).then((res) => {
        this.editForm = res.ResData;
        // 需個性化處理內容
      });
    },
    // /**
    //  * 新增/修改保存
    //  */
    // save() {
    //   this.saveloading = true;
    //   this.$refs['editForm'].validate((valid) => {
    //     if (valid) {
    //       const data = {
    //         Type: this.editForm.Type,
    //         QuestionCount: this.editForm.QuestionCount,
    //         QFileName: this.editForm.QFileName,
    //         QBeginWords: this.editForm.QBeginWords,
    //         QEndWords: this.editForm.QEndWords,
    //         CreatorUserId: this.editForm.CreatorUserId,
    //         CreatorTime: this.editForm.CreatorTime,
    //         LastModifyUserId: this.editForm.LastModifyUserId,
    //         LastModifyTime: this.editForm.LastModifyTime,

    //         Id: this.currentId
    //       };

    //       var url = 'Satisfaction/Insert';
    //       if (this.currentId !== '') {
    //         url = 'Satisfaction/Update';
    //       }
    //       saveSatisfaction(data, url)
    //         .then((res) => {
    //           if (res.Success) {
    //             this.$message.success('儲存成功');
    //           } else {
    //             this.$message.error(res.ErrMsg);
    //           }
    //         })
    //         .finally((res) => {
    //           this.saveloading = false;
    //         });
    //     } else {
    //       return false;
    //     }
    //   });
    // },
    GenSighUp() {
      // this.$confirm('', '警告', {
      //   confirmButtonText: '確定',
      //   cancelButtonText: '取消',
      //   type: 'warning'
      // }).then(function() {
      //   const data = {
      //     Ids: currentIds
      //   };
      //   return deleteSystemType(data);
      // });
      if (!this.editForm.QFileName) {
        this.$message.error('請先上傳題目定義檔!');
        return;
      } else {
        this.$refs['editForm'].validate((valid) => {
          if (valid) {
            this.$confirm(
              '您即將產生一份滿意度問卷，請注意：<br/><b style="color:red">若您是針對已存在之滿意度問卷做題目的更改，您將失去原題目之填寫資料，請確保已將原題目之統計報表保存妥善!<b/>',
              '警告',
              {
                confirmButtonText: '確定',
                cancelButtonText: '取消',
                dangerouslyUseHTMLString: true,
                type: 'warning'
              }
            )
              .then(() => {
                this.saveloading = true;
                // this.editForm.Eid = this.idd;
                const data = {
                  Id: this.editForm.Id,
                  QBeginWords: this.editForm.QBeginWords,
                  QEndWords: this.editForm.QEndWords
                };
                GenSignUpForSatisfaction(data)
                  .then((res) => {
                    if (res.Success) {
                      this.$message.success('儲存成功');
                      this.bindEditInfo();
                      // this.$emit('bindinfo');
                    } else {
                      this.$message.error('失敗');
                    }
                  })
                  .finally(() => {
                    this.saveloading = false;
                  });
              })
              .catch(() => {
                return;
              });
          } else {
            return false;
          }
        });
      }
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
