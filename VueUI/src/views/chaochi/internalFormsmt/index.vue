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
          <el-form-item label="表單代號：">
            <el-input
              v-model="searchform.FormId"
              clearable
            />
          </el-form-item>
          <el-form-item label="表單名稱：">
            <el-input
              v-model="searchform.FormName"
              clearable
            />
          </el-form-item>
          <!-- <el-form-item label="部門：">
            <el-select
              v-model="searchform.Dept"
              placeholder="請選擇"
            >
              <el-option
                v-for="item in DeptOptions"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              />
            </el-select>
          </el-form-item>
          <el-form-item label="類別：">
            <el-select
              v-model="searchform.Type"
              placeholder="請選擇"
            >
              <el-option
                v-for="item in TypeOptions"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              />
            </el-select>
          </el-form-item> -->
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
        <!-- <el-form
          ref="searchform"
          :inline="true"
          :model="searchform"
          class="demo-form-inline"
          size="small"
        >
          <el-form-item label="名稱：">
            <el-input v-model="searchform.keywords" clearable placeholder="名稱" />
          </el-form-item>
          <el-form-item>
            <el-button type="primary" @click="handleSearch()">查詢</el-button>
          </el-form-item>
        </el-form> -->
      </el-card>
    </div>
    <el-card>
      <div class="list-btn-container">
        <el-button-group>
          <el-button
            v-hasPermi="['Internalform/Add']"
            type="primary"
            icon="el-icon-plus"
            size="small"
            @click="ShowEditOrViewDialog()"
          >新增</el-button>
          <el-button
            v-hasPermi="['Internalform/Edit']"
            type="primary"
            icon="el-icon-edit"
            class="el-button-modify"
            size="small"
            @click="ShowEditOrViewDialog('edit')"
          >修改</el-button>
          <el-button
            v-hasPermi="['Internalform/Delete']"
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
          width="40"
        />
        <!-- <el-table-column
          label="操作"
          width="120"
          align="center"
          fixed
        >
          <template slot-scope="scope">
            <el-tooltip
              effect="dark"
              content="以表單內容搜尋並下載PDF"
              placement="top"
              :enterable="false"
            >
              <el-button
                type="primary"
                icon="el-icon-download"
                size="mini"
                @click="downloadpdfdialogVisible=true;downloadpdfFormId=scope.row.FormId;downloadFormName=scope.row.FormName;reqb=scope.row.RequiredBuilding;reqln=scope.row.RequiredLandlordN;reqlc=scope.row.RequiredLandlordC;reqrn=scope.row.RequiredRenterN;reqrc=scope.row.RequiredRenterC"
              />
            </el-tooltip>
          </template>
        </el-table-column> -->
        <el-table-column
          prop="FormId"
          label="表單代號"
          sortable="custom"
          width="120"
        />
        <el-table-column
          prop="FormName"
          label="表單名稱"
          sortable="custom"
          width="360"
        />
        <el-table-column
          prop="Vno"
          label="版本"
          sortable="custom"
        />
        <el-table-column
          prop="Dept"
          label="部門"
          sortable="custom"
          width="120"
        />
        <el-table-column
          prop="Type"
          label="類別"
          sortable="custom"
          width="120"
        />
        <el-table-column
          prop="CreatorTime"
          label="創建時間"
          sortable="custom"
          width="180"
        />
        <el-table-column
          prop="LastModifyTime"
          label="更新時間"
          sortable="custom"
          width="180"
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
      :title="editFormTitle"
      :visible.sync="dialogEditFormVisible"
      width="1000px"
      append-to-body
    >
      <el-tabs
        v-model="ActionName"
        v-loading="loading"
        v-loading.lock="loading"
        element-loading-text="載入中..."
        type="border-card"
      >
        <el-tab-pane
          label="WEB表單"
          name="A"
        >
          <el-card class="box-card">
            <el-form
              ref="editForm"
              :inline="true"
              :model="editForm"
              class="demo-form-inline"
            >
              <el-row>
                <el-col :span="24">
                  <el-form-item
                    label="表單代號"
                    :label-width="formLabelWidth"
                    prop="FormId"
                  >
                    <el-input
                      v-model="editForm.FormId"
                      placeholder="請輸入表單代號"
                      autocomplete="off"
                      clearable
                    />
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row>
                <el-col :span="24">
                  <el-form-item
                    label="表單名稱"
                    :label-width="formLabelWidth"
                    prop="FormName"
                  >
                    <el-input
                      v-model="editForm.FormName"
                      placeholder="請輸入表單名稱"
                      autocomplete="off"
                      clearable
                      style="width:650px"
                    />
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row v-if="isSec">
                <el-col :span="24">
                  <el-form-item
                    label="租安表單版本"
                    :label-width="formLabelWidth"
                    prop="FormName"
                  >
                    <el-tooltip
                      class="item"
                      effect="dark"
                      content="請用半形 ',' 做區隔 例:1100531,1100625,1100731"
                      placement="top"
                    >
                      <el-input
                        v-model="editForm.Vno"
                        placeholder="請用半形 ',' 做區隔 例:1100531,1100625,1100731"
                        autocomplete="off"
                        clearable
                        style="width:650px"
                      />
                    </el-tooltip>
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row v-if="!isRec && !isSec">
                <el-col :span="12">
                  <el-form-item
                    label="版本號"
                    :label-width="formLabelWidth"
                    prop="Vno"
                  >
                    <el-input
                      v-model="editForm.Vno"
                      placeholder="請輸入版本號"
                      autocomplete="off"
                      clearable
                    />
                  </el-form-item>
                </el-col>
                <el-col :span="12">
                  <el-form-item
                    label="TBNO"
                    :label-width="formLabelWidth"
                    prop="TBNO"
                  >
                    <el-input
                      v-model="editForm.TBNO"
                      placeholder="請輸入期數"
                      autocomplete="off"
                      clearable
                    />
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row v-if="!isRec && !isSec">
                <el-col :span="24">
                  <el-form-item
                    label="必填檢查欄位"
                    :label-width="formLabelWidth"
                    prop="CustTagX"
                  >
                    <el-checkbox
                      v-model="editForm.RequiredLandlordN"
                      true-label="Y"
                      false-label="N"
                    >出租自然人</el-checkbox>
                    <el-checkbox
                      v-model="editForm.RequiredLandlordC"
                      true-label="Y"
                      false-label="N"
                    >出租法人</el-checkbox>
                    <el-checkbox
                      v-model="editForm.RequiredBuilding"
                      true-label="Y"
                      false-label="N"
                    >建物</el-checkbox>
                    <el-checkbox
                      v-model="editForm.RequiredRenterN"
                      true-label="Y"
                      false-label="N"
                    >承租自然人</el-checkbox>
                    <el-checkbox
                      v-model="editForm.RequiredRenterC"
                      true-label="Y"
                      false-label="N"
                    >承租法人</el-checkbox>
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row v-if="!isRec && !isSec">
                <el-col :span="24">
                  <el-form-item
                    label="必需為已存在值"
                    :label-width="formLabelWidth"
                    prop="CustTagXX"
                  >
                    <el-checkbox
                      v-model="editForm.MustExistsLandLord"
                      true-label="Y"
                      false-label="N"
                    >出租人</el-checkbox>
                    <el-checkbox
                      v-model="editForm.MustExistsBuilding"
                      true-label="Y"
                      false-label="N"
                    >建物</el-checkbox>
                    <el-checkbox
                      v-model="editForm.MustExistsRenter"
                      true-label="Y"
                      false-label="N"
                    >承租人</el-checkbox>
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row v-if="editForm.ArchiveLTo !== ''">
                <el-col :span="24">
                  <el-form-item
                    label="歷史存檔"
                    :label-width="formLabelWidth"
                    prop="ArchiveLTo"
                  >
                    {{ editForm.ArchiveLTo }}
                    <!-- <el-radio
                      v-model="editForm.ArchiveLTo"
                      label="出租人"
                    >出租人</el-radio>
                    <el-radio
                      v-model="editForm.ArchiveLTo"
                      label="建物"
                    >建物</el-radio>
                    <el-radio
                      v-model="editForm.ArchiveLTo"
                      label="承租人"
                    >承租人</el-radio> -->
                  </el-form-item>
                </el-col>
              </el-row>
            </el-form>
          </el-card>
        </el-tab-pane>
        <el-tab-pane
          v-if="editForm.HasPDFTemplate === 'Y'"
          label="空白PDF上傳"
          name="B"
        >
          <el-upload
            ref="newupload"
            drag
            :data="editForm"
            :action="httpUploadPDFTemplateUrl"
            :headers="headers"
            :multiple="false"
            :auto-upload="true"
            :show-file-list="false"
            accept=".pdf"
            :on-success="uploadsuccess"
            :on-error="uploaderror"
            :before-upload="handleUploadBefore"
          >
            <em class="el-icon-upload" />
            <div class="el-upload__text">將文件拖到此處，或<em>點擊上傳</em> </div>
            <div
              slot="tip"
              class="el-upload__tip"
            >請註意您只能上傳.pdf格式</div>
          </el-upload>
        </el-tab-pane>
      </el-tabs>
      <!-- <el-form
        ref="editForm"
        :model="editForm"
        :rules="rules"
      >
        <el-form-item
          label="表單代號"
          :label-width="formLabelWidth"
          prop="FormId"
        >
          <el-input
            v-model="editForm.FormId"
            placeholder="請輸入 "
            autocomplete="off"
            clearable
          />
        </el-form-item>
        <el-form-item
          label=" "
          :label-width="formLabelWidth"
          prop="Vno"
        >
          <el-input
            v-model="editForm.Vno"
            placeholder="請輸入 "
            autocomplete="off"
            clearable
          />
        </el-form-item>
        <el-form-item
          label=" "
          :label-width="formLabelWidth"
          prop="FormName"
        >
          <el-input
            v-model="editForm.FormName"
            placeholder="請輸入 "
            autocomplete="off"
            clearable
          />
        </el-form-item>
        <el-form-item
          label=" "
          :label-width="formLabelWidth"
          prop="TBNO"
        >
          <el-input
            v-model="editForm.TBNO"
            placeholder="請輸入 "
            autocomplete="off"
            clearable
          />
        </el-form-item>
        <el-form-item
          label=" "
          :label-width="formLabelWidth"
          prop="Dept"
        >
          <el-input
            v-model="editForm.Dept"
            placeholder="請輸入 "
            autocomplete="off"
            clearable
          />
        </el-form-item>
        <el-form-item
          label=" "
          :label-width="formLabelWidth"
          prop="Type"
        >
          <el-input
            v-model="editForm.Type"
            placeholder="請輸入 "
            autocomplete="off"
            clearable
          />
        </el-form-item>
        <el-form-item
          label=" "
          :label-width="formLabelWidth"
          prop="CreatorTime"
        >
          <el-input
            v-model="editForm.CreatorTime"
            placeholder="請輸入 "
            autocomplete="off"
            clearable
          />
        </el-form-item>
        <el-form-item
          label=" "
          :label-width="formLabelWidth"
          prop="CreatorUserId"
        >
          <el-input
            v-model="editForm.CreatorUserId"
            placeholder="請輸入 "
            autocomplete="off"
            clearable
          />
        </el-form-item>
        <el-form-item
          label=" "
          :label-width="formLabelWidth"
          prop="LastModifyTime"
        >
          <el-input
            v-model="editForm.LastModifyTime"
            placeholder="請輸入 "
            autocomplete="off"
            clearable
          />
        </el-form-item>
        <el-form-item
          label=" "
          :label-width="formLabelWidth"
          prop="LastModifyUserId"
        >
          <el-input
            v-model="editForm.LastModifyUserId"
            placeholder="請輸入 "
            autocomplete="off"
            clearable
          />
        </el-form-item>

      </el-form> -->
      <div
        slot="footer"
        class="dialog-footer"
      >
        <el-button
          size="small"
          icon="el-icon-close"
          @click="cancel"
        >關閉</el-button>
        <el-button
          type="primary"
          size="small"
          icon="el-icon-video-play"
          @click="saveEditForm()"
        >確定</el-button>
      </div>
    </el-dialog>
    <el-dialog
      ref="downloadpdfdialog"
      title="下載PDF"
      :visible.sync="downloadpdfdialogVisible"
      fullscreen
    >
      <DownLoadPdfin
        :formid="downloadpdfFormId"
        :formname="downloadFormName"
        :reqb="reqb"
        :reqln="reqln"
        :reqrn="reqrn"
        :reqlc="reqlc"
        :reqrc="reqrc"
        @closedownloadpdfdialog="closeDownLoadPdfDialog()"
      />
      <div
        slot="footer"
        class="dialog-footer"
      >
        <el-button @click="downloadpdfdialogVisible = false">關 閉</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import {
  getInternalformListWithPager,
  getInternalformDetail,
  saveInternalform,
  deleteInternalform
} from '@/api/chaochi/internalform/internalform';
import defaultSettings from '@/settings';
import { getToken } from '@/utils/auth';
import elDragDialog from '@/directive/el-drag-dialog'; // 彈窗可移動
import DownLoadPdfin from './downloadpdf.vue';
export default {
  name: 'Internalformmt', // 需與菜單的功能編碼一致
  directives: { elDragDialog },
  components: { DownLoadPdfin },
  data() {
    return {
      searchform: {},
      tableData: [],
      ActionName: 'A',
      tableloading: true,
      loading: false,
      pagination: {
        currentPage: 1,
        pagesize: 20,
        pageTotal: 0
      },
      sortableData: {
        order: 'desc',
        sort: 'CreatorTime'
      },
      TypeOptions: [
        {
          value: '',
          label: '全部'
        },
        {
          value: '委託類',
          label: '委託類'
        },
        {
          value: '授權類',
          label: '授權類'
        },
        {
          value: '領據類',
          label: '領據類'
        },
        {
          value: '切結類',
          label: '切結類'
        },
        {
          value: '調查類',
          label: '調查類'
        },
        {
          value: '終止類',
          label: '終止類'
        }
      ],
      DeptOptions: [
        {
          value: '',
          label: '共用'
        },
        {
          value: '社宅處',
          label: '社宅處'
        },
        {
          value: '物管處',
          label: '物管處'
        },
        {
          value: '商仲部',
          label: '商仲部'
        },
        {
          value: '捷運部',
          label: '捷運部'
        },
        {
          value: '商務中心',
          label: '商務中心'
        }
      ],
      dialogEditFormVisible: false,
      downloadpdfdialogVisible: false,
      downloadFormName: '',
      downloadpdfFormId: '',
      reqb: '',
      reqln: '',
      reqlc: '',
      reqrn: '',
      reqrc: '',
      editFormTitle: '',
      editForm: {},
      rules: {},
      formLabelWidth: '165px',
      httpUploadPDFTemplateUrl:
        defaultSettings.apiChaochiUrl + 'Internalform/UploadPDFTemplate',
      headers: [],
      currentId: '', // 當前操作對象的ID值，主要用于修改
      currentSelected: []
    };
  },
  computed: {
    isRec() {
      const result =
        this.editForm.FormId === 'B031201' ||
        this.editForm.FormId === 'B031301' ||
        this.editForm.FormId === 'A000501' ||
        this.editForm.FormId === 'A000601';
      return result;
    },
    isSec() {
      const result =
        this.editForm.FormId === 'A000301' ||
        this.editForm.FormId === 'A000401';
      return result;
    }
  },
  created() {
    this.pagination.currentPage = 1;
    this.InitDictItem();
    this.loadTableData();
    this.headers = { Authorization: 'Bearer ' + (getToken() || '') };
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
      this.editForm = {
        FormId: '',
        Vno: '',
        FormName: '',
        TBNO: '',
        Dept: '',
        Type: '',
        RequiredLandlord: 'N',
        RequiredBuilding: 'N',
        RequiredRenter: 'N',
        MustExistsLandLord: 'N',
        MustExistsBuilding: 'N',
        MustExistsRenter: 'N',
        ArchiveLTo: '',
        LastModifyTime: '',
        LastModifyUserId: ''

        // 需個性化處理內容
      };
      this.resetForm('editForm');
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
        Keywords: 'Y',
        Order: this.sortableData.order,
        Sort: this.sortableData.sort
      };
      getInternalformListWithPager(seachdata).then(res => {
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
    rowdblclick(row, column, event) {
      this.$refs.gridtable.clearSelection();
      this.currentSelected = '';
      this.rerenderGridtable = Date.now(); // 強制重繪 https://bbchin.com/archives/el-table-rerender
      this.$nextTick(function() {
        // https://ithelp.ithome.com.tw/articles/10240669
        this.$refs.gridtable.toggleRowSelection(row, true);
        this.currentSelected = this.$refs.gridtable.selection;
        this.ShowEditOrViewDialog('edit');
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
          this.loading = true;
          this.bindEditInfo();
        }
      } else {
        this.editFormTitle = '新增';
        this.currentId = '';
        this.dialogEditFormVisible = true;
      }
    },
    bindEditInfo: function() {
      getInternalformDetail(this.currentId)
        .then(res => {
          this.editForm = res.ResData;
          // 需個性化處理內容
        })
        .finally(() => {
          this.loading = false;
        });
    },
    /**
     * 新增/修改保存
     */
    saveEditForm() {
      this.$refs['editForm'].validate(valid => {
        if (valid) {
          var url = 'Internalform/Insert';
          if (this.currentId !== '') {
            url = 'Internalform/Update';
          }
          console.log('14');
          saveInternalform(this.editForm, url).then(res => {
            if (res.Success) {
              this.$message({
                message: '恭喜你，操作成功',
                type: 'success'
              });
              this.dialogEditFormVisible = false;
              this.currentSelected = '';
              this.loadTableData();
              this.InitDictItem();
            } else {
              this.$message({
                message: res.ErrMsg,
                type: 'error'
              });
            }
          });
        } else {
          return false;
        }
      });
    },
    uploadsuccess: function(response, file, fileList) {
      this.uploadLoading = false;
      if (response.Success) {
        this.$message({
          message: file.name + ' 上傳成功',
          type: 'success'
        });
        this.$store.state.tagsView.cachedViews = [];
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
        );
      }
      // this.loadTableDataD()
    },
    uploaderror: function(err, file, fileList) {
      this.uploadLoading = false;
      console.log('uploaderror');
      // console.log(file)
      console.log(err);
      console.log(file);
      console.log(fileList);
      this.$message.error('開發中');
      // this.loadTableDataD()
    },
    handleUploadBefore(file) {
      console.log(file.name);
      this.uploadLoading = true;
      // file.name = this.editFrom.FormId + 'pdf'
    },
    closeDownLoadPdfDialog() {
      this.downloadpdfdialogVisible = false;
    },
    Jump: function(formid, formname) {
      var p = {
        FormId: formid,
        FormName: formname,
        IsNew: true
      };
      // this.$emit('closeDownLoadPdfDialog');
      this.$router.push({
        // path: '/chaochi/internalFormsDetail/index',
        name: 'InternalformsDetail',
        params: p
      });
    },
    deletePhysics: function() {
      if (this.currentSelected.length === 0) {
        this.$alert('請先選擇要操作的數據', '提示');
        return false;
      } else {
        const that = this;
        this.$confirm('是否確認刪除所選的數據項?', '警告', {
          confirmButtonText: '確定',
          cancelButtonText: '取消',
          type: 'warning'
        })
          .then(function() {
            var currentIds = [];
            that.currentSelected.forEach(element => {
              currentIds.push(element.Id);
            });
            const data = {
              Ids: currentIds
            };
            return deleteInternalform(data);
          })
          .then(res => {
            if (res.Success) {
              that.$message({
                message: '恭喜你，刪除成功',
                type: 'success'
              });
              that.currentSelected = '';
              that.loadTableData();
            } else {
              this.$message({
                message: res.ErrMsg,
                type: 'error'
              });
            }
          });
      }
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

<style>
</style>
