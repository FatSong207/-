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
          <el-form-item label="表單名稱：">
            <el-input
              v-model="searchform.FormName"
              clearable
            />
          </el-form-item>
          <el-form-item label="自訂標籤：">
            <el-input
              v-model="searchform.CustTag"
              clearable
            />
          </el-form-item>
          <el-form-item label="類別：">
            <el-cascader
              ref="categoryPath"
              v-model="searchform.selectedCategoryOptions"
              style="width: 500px"
              :options="selectCategory"
              filterable
              :props="{
                label: 'Name',
                value: 'Id',
                children: 'Children',
                emitPath: false,
                checkStrictly: true,
                expandTrigger: 'hover'
              }"
              clearable
              @change="handleSelectCategoryChange"
            />
          </el-form-item>
          <el-form-item>
            <el-button
              type="primary"
              size="small"
              icon="el-icon-search"
              @click="handleSearch()"
            >查詢</el-button>
          </el-form-item>
        </el-form>
      </el-card>
    </div>
    <el-card>
      <div class="list-btn-container">
        <el-button-group>
          <el-tooltip
            effect="dark"
            content="以PDF內容寫入資料庫"
            placement="top"
            :enterable="false"
          >
            <el-button
              v-hasPermi="['Externalform/UploadPDF']"
              type="primary"
              icon="el-icon-upload2"
              size="small"
              @click="ShowUploadPdfWithDataDialog()"
            >上傳PDF</el-button>
          </el-tooltip>
          <!-- <el-button
            v-hasPermi="['Externalform/Add']"
            type="primary"
            icon="el-icon-plus"
            size="small"
            @click="ShowEditOrViewDialog()"
          >新增</el-button> -->
          <!-- <el-button
            v-hasPermi="['Externalform/Edit']"
            type="primary"
            icon="el-icon-edit"
            class="el-button-modify"
            size="small"
            @click="ShowEditOrViewDialog('edit')"
          >修改</el-button> -->
          <el-button
            v-hasPermi="['Externalform/Delete']"
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
        :key="rerenderGridtable"
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
        <el-table-column
          type="selection"
          width="40" fixed
        />
        <el-table-column
          label="操作"
          width="120" align="center" fixed
        >
          <template slot-scope="scope">
            <!-- 修改按鈕 -->
            <!-- <el-tooltip
              effect="dark"
              content="下載空白PDF"
              placement="top"
              :enterable="false"
            >
              <el-button
                v-if="!cantnullPDF.includes(scope.row.FormId)"
                type="primary"
                icon="el-icon-download"
                size="mini"
                @click="downloadPDFTemplate(scope.row.FormId)"
              />
            </el-tooltip> -->
            <!-- 刪除按鈕 -->
            <el-tooltip
              effect="dark"
              :content="
                scope.row.IsNoKey === 'Y'
                  ? '下載空白無主表單'
                  : '以指定條件內容下載表單'
              "
              placement="top"
              :enterable="false"
            >
              <el-button
                :type="scope.row.IsNoKey === 'Y' ? '' : 'primary'"
                icon="el-icon-download"
                size="mini"
                @click="DownCheck(scope.row)"
              />
              <!-- @click="downloadpdfdialogVisible=true;downloadpdfFormId=scope.row.FormId;downloadFormName=scope.row.FormName;reqb=scope.row.RequiredBuilding;reql=scope.row.RequiredLandlord;reqr=scope.row.RequiredRenter" -->
            </el-tooltip>
          </template>
        </el-table-column>
        <el-table-column
          prop="FormId"
          label="表單代號"
          sortable="custom"
          width="120"
          fixed
        />
        <el-table-column
          prop="FormName"
          label="表單名稱"
          sortable="custom"
          width="300"
        />
        <el-table-column
          prop="CustTag"
          label="自訂標籤"
          sortable="custom"
          width="150"
        />
        <el-table-column
          prop="Vno"
          label="版本"
          sortable="custom"
          width="100"
        />
        <el-table-column
          prop="level"
          label="level"
          sortable="custom"
          width="300"
        />
        <el-table-column
          prop="LastModifyTime"
          label="更新時間"
          width="180"
          sortable
        >
          <template slot-scope="scope">
            {{ scope.row.LastModifyTime }}
          </template>
        </el-table-column>
        <el-table-column
          prop="CreatorTime"
          label="建立時間"
          width="180"
          sortable
        />
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

    <el-dialog
      ref="dialogEditForm"
      :title="editFormTitle"
      :visible.sync="dialogEditFormVisible"
      width="1000px"
    >
      <el-tabs
        v-model="ActionName"
        type="border-card" @tab-click="handleClick"
      >
        <el-tab-pane
          label="外部表單"
          name="A"
        >
          <el-card
            v-loading="loading"
            v-loading.lock="loading"
            element-loading-text="載入中..."
            class="box-card"
          >
            <el-form
              ref="editFrom"
              :inline="true"
              :model="editFrom"
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
                      v-model="editFrom.FormId"
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
                      v-model="editFrom.FormName"
                      placeholder="請輸入表單名稱"
                      autocomplete="off"
                      clearable
                      style="width: 650px"
                    />
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row>
                <el-col :span="12">
                  <el-form-item
                    label="版本號"
                    :label-width="formLabelWidth"
                    prop="Vno"
                  >
                    <el-input
                      v-model="editFrom.Vno"
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
                      v-model="editFrom.TBNO"
                      placeholder="請輸入期數"
                      autocomplete="off"
                      clearable
                    />
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row>
                <el-col :span="24">
                  <el-form-item
                    label="Level"
                    :label-width="formLabelWidth"
                    prop="level"
                  >
                    <!-- <el-input v-model="editFrom.level" placeholder="請輸入Level" autocomplete="off" clearable /> -->
                    <el-cascader
                      ref="categoryPath"
                      v-model="editFrom.level"
                      style="width: 500px"
                      :options="selectCategory"
                      filterable
                      :props="{
                        label: 'Name',
                        value: 'Id',
                        children: 'Children',
                        emitPath: false,
                        checkStrictly: true,
                        expandTrigger: 'hover'
                      }"
                      clearable
                      @change="handleSelectCategoryChange"
                    />
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row>
                <el-col :span="24">
                  <el-form-item
                    label="自訂標籤"
                    :label-width="formLabelWidth"
                    prop="CustTag"
                  >
                    <el-input
                      v-model="editFrom.CustTag"
                      placeholder="請輸入自訂標籤"
                      autocomplete="off"
                      clearable
                    />
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row>
                <el-col :span="24">
                  <el-form-item
                    label="必填檢查欄位"
                    :label-width="formLabelWidth"
                    prop="CustTagX"
                  >
                    <el-checkbox
                      v-model="editFrom.RequiredLandlord"
                      true-label="Y"
                      false-label="N"
                    >出租人</el-checkbox>
                    <el-checkbox
                      v-model="editFrom.RequiredBuilding"
                      true-label="Y"
                      false-label="N"
                    >建物</el-checkbox>
                    <el-checkbox
                      v-model="editFrom.RequiredRenter"
                      true-label="Y"
                      false-label="N"
                    >承租人</el-checkbox>
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row>
                <el-col :span="24">
                  <el-form-item
                    label="必需為已存在值"
                    :label-width="formLabelWidth"
                    prop="CustTagXX"
                  >
                    <el-checkbox
                      v-model="editFrom.MustExistsLandLord"
                      true-label="Y"
                      false-label="N"
                    >出租人</el-checkbox>
                    <el-checkbox
                      v-model="editFrom.MustExistsBuilding"
                      true-label="Y"
                      false-label="N"
                    >建物</el-checkbox>
                    <el-checkbox
                      v-model="editFrom.MustExistsRenter"
                      true-label="Y"
                      false-label="N"
                    >承租人</el-checkbox>
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row>
                <el-col :span="24">
                  <el-form-item
                    label="歷史存檔"
                    :label-width="formLabelWidth"
                    prop="ArchiveLTo"
                  >
                    <el-radio
                      v-model="editFrom.ArchiveLTo" label="出租人"
                    >出租人</el-radio>
                    <el-radio
                      v-model="editFrom.ArchiveLTo" label="建物"
                    >建物</el-radio>
                    <el-radio
                      v-model="editFrom.ArchiveLTo" label="承租人"
                    >承租人</el-radio>
                  </el-form-item>
                </el-col>
              </el-row>
            </el-form>
          </el-card>
        </el-tab-pane>
        <el-tab-pane
          label="空白PDF上傳"
          name="B"
        >
          <el-upload
            ref="newupload"
            drag
            :data="editFrom"
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
            <div class="el-upload__text">
              將文件拖到此處，或<em>點擊上傳</em>
            </div>
            <div
              slot="tip"
              class="el-upload__tip"
            >
              請註意您只能上傳.pdf格式
            </div>
          </el-upload>
        </el-tab-pane>
      </el-tabs>
      <div
        slot="footer"
        class="dialog-footer"
      >
        <el-button
          size="small"
          icon="el-icon-close"
          @click="
            dialogEditFormVisible = false;
            ActionName = 'A';
          "
        >關閉</el-button>
        <el-button
          v-hasPermi="['Building/Edit']"
          type="primary"
          size="small"
          icon="el-icon-video-play"
          @click="saveEditForm()"
        >確認</el-button>
      </div>
    </el-dialog>

    <el-dialog
      ref="dialogUploadPdfWithDataForm"
      :title="editFormTitle"
      :visible.sync="dialogUploadPdfWithDataFormVisible"
      width="440px"
    >
      <el-tabs
        v-model="ActionName"
        v-loading="uploadLoading"
        element-loading-text="上傳中...請勿關閉"
        type="border-card"
        @tab-click="handleClick"
      >
        <el-tab-pane
          label="含資料PDF上傳"
          name="A"
        >
          <el-upload
            ref="newupload"
            drag
            :data="noKeyObj"
            :action="httpUploadPDFWithData"
            :headers="headers"
            :multiple="false"
            :auto-upload="false"
            :show-file-list="false"
            accept=".pdf"
            :on-change="uploadchange"
            :on-success="uploadsuccess"
            :on-error="uploaderror"
            :before-upload="handleUploadBefore"
            :file-list="fileList"
          >
            <em class="el-icon-upload" />
            <div class="el-upload__text">
              將文件拖到此處，或<em>點擊上傳</em>
            </div>
            <div
              slot="tip"
              class="el-upload__tip"
            >
              請註意您只能上傳.pdf格式
            </div>
          </el-upload>
        </el-tab-pane>
      </el-tabs>
      <div
        slot="footer"
        class="dialog-footer"
      >
        <el-button
          size="small"
          icon="el-icon-close"
          @click="dialogUploadPdfWithDataFormVisible = false"
        >關 閉</el-button>
      </div>
    </el-dialog>
    <el-dialog
      ref="downloadpdfdialog"
      title="下載PDF"
      :visible.sync="downloadpdfdialogVisible"
      fullscreen
    >
      <DownLoadPdf
        :formid="downloadpdfFormId"
        :formname="downloadFormName"
        :reqb="reqb"
        :reql="reql"
        :reqr="reqr"
      />
      <div
        slot="footer"
        class="dialog-footer"
      >
        <el-button
          size="small"
          icon="el-icon-close"
          @click="downloadpdfdialogVisible = false"
        >關 閉</el-button>
      </div>
    </el-dialog>
    <el-dialog
      ref="chooseHistoryDst"
      title="偵測到上傳表單為無主表單,請選擇附件位置"
      :visible.sync="chooseHistoryDstVisible"
      :width="chooseHistoryDstWid"
      append-to-body
    >
      <el-form
        ref="noKeyObj"
        :model="noKeyObj" :rules="ruleNokey"
      >
        <el-form-item
          label="目標："
          label-width="80px" prop="achTo"
        >
          <el-select
            v-model="noKeyObj.achTo"
            placeholder="請選擇"
          >
            <el-option
              label="出租自然人"
              value="LN"
            />
            <el-option
              label="出租法人"
              value="LC"
            />
            <el-option
              label="承租自然人"
              value="RN"
            />
            <el-option
              label="承租法人"
              value="RC"
            />
            <el-option
              label="建物"
              value="B"
            />
          </el-select>
        </el-form-item>
        <el-form-item
          v-if="noKeyObj.achTo === 'LN'"
          label-width="180px"
          label="身分證字號/居留證號："
          prop="data"
        >
          <el-input
            v-model="noKeyObj.data"
            v-upperCase
            placeholder="請輸入出租自然人身分證字號/居留證號"
            autocomplete="off"
            clearable
            style="width: 200px"
            maxlength="10"
          />
        </el-form-item>
        <el-form-item
          v-if="noKeyObj.achTo === 'LC'"
          label-width="100px"
          label="統一編號："
          prop="data"
        >
          <el-input
            v-model="noKeyObj.data"
            placeholder="請輸入出租法人統一編號"
            autocomplete="off"
            clearable
            style="width: 200px"
            maxlength="8"
          />
        </el-form-item>
        <el-form-item
          v-if="noKeyObj.achTo === 'RN'"
          label-width="180px"
          label="身分證字號/居留證號："
          prop="data"
        >
          <el-input
            v-model="noKeyObj.data"
            v-upperCase
            placeholder="請輸入承租自然人身分證字號/居留證號"
            autocomplete="off"
            clearable
            style="width: 200px"
            maxlength="10"
          />
        </el-form-item>
        <el-form-item
          v-if="noKeyObj.achTo === 'RC'"
          label-width="100px"
          label="統一編號："
          prop="data"
        >
          <el-input
            v-model="noKeyObj.data"
            placeholder="請輸入承租法人統一編號"
            autocomplete="off"
            clearable
            style="width: 200px"
            maxlength="8"
          />
        </el-form-item>
        <el-form-item
          v-if="noKeyObj.achTo === 'B'"
          label-width="100px"
          label="建物地址："
        >
          <Address
            title="建物地址"
            @getsearchaddress="getsearchaddress"
          />
        </el-form-item>
      </el-form>
      <el-button
        type="primary"
        icon="el-icon-upload"
        size="small"
        @click="SendPDF()"
      >確認並上傳</el-button>
    </el-dialog>
  </div>
</template>

<script>
import {
  getExternalformListWithPager,
  getExternalformDetail,
  saveExternalform,
  deleteExternalform,
  downloadPDFTemplateByFormId,
  UploadPDFWithDataAsync
} from '@/api/chaochi/externalform/externalformservice';
import { getAllCategoryTreeTable } from '@/api/chaochi/category/categoryformservice';
import defaultSettings from '@/settings';
import { getToken } from '@/utils/auth';
import DownLoadPdf from './downloadpdf.vue';
import Address from '@/components/Address/Address.vue';
import { validateIDCombo } from '@/utils/validate';
import { mergerAddress } from '@/utils/index';

export default {
  name: 'ExternalForm',
  components: { DownLoadPdf, Address },
  data() {
    return {
      noKeyObj: {
        isNokey: '',
        isSpecific: '',
        achTo: '',
        data: ''
      },
      ruleNokey: {
        achTo: [
          { required: true, trigger: 'change', message: '請選擇目標角色' }
        ],
        data: [
          { required: true, trigger: 'change', validator: validateIDCombo }
        ]
      },
      searchPDF: {},
      chooseHistoryDstWid: '600px',
      downloadFormName: '',
      downloadpdfFormId: '',
      reqb: '',
      reql: '',
      reqr: '',
      downloadpdfdialogVisible: false,
      selectedCategoryOptions: [],
      selectCategory: [],
      selectedCategoryFullPath: '',
      rerenderGridtable: Date.now(),
      searchform: {
        RoleId: '',
        Keywords: '',
        CreateTime: '',
        FormId: '',
        FormName: '',
        GuildCountyCity: '',
        Period: '',
        CustTag: '',
        Type: '',
        Path: '',
        selectedCategoryOptions: ''
      },
      tableData: [],
      tableloading: true,
      pagination: {
        currentPage: 1,
        pagesize: 10,
        pageTotal: 0
      },
      paginationF: {
        currentPage: 1,
        pagesize: 20,
        pageTotal: 0
      },
      sortableData: {
        order: 'asc',
        sort: 'FormId'
      },
      dialogEditFormVisible: false,
      dialogUploadPdfWithDataFormVisible: false,
      dialogEditFormVisibleD: false,
      dialogEditFormVisibleF: false,
      chooseHistoryDstVisible: false,
      editFormTitle: '',
      editFormTitleD: '新增',
      editFormTitleF: '',
      editFrom: {},
      formLabelWidth: '165px',
      currentId: '', // 當前操作對象的ID值，主要用于修改
      currentSelected: [],
      currentSelectedD: [],
      currentIdF: '', // 當前操作對象的ID值，主要用于修改
      currentSelectedF: [],
      ActionName: 'A',
      httpUploadPDFTemplateUrl:
        defaultSettings.apiChaochiUrl + 'Externalform/UploadPDFTemplate',
      httpUploadPDFWithData:
        defaultSettings.apiChaochiUrl + 'Externalform/UploadPDFWithData',
      httpImgFileUploadUrl:
        defaultSettings.apiChaochiUrl + 'Building/ImgUpload',
      headers: [],
      uploadLoading: false,
      loading: false,
      fileList: []
    };
  },
  watch: {
    'noKeyObj.achTo': {
      handler(a) {
        console.log(a);
        if (a === 'B') {
          this.chooseHistoryDstWid = '1400px';
        } else {
          this.chooseHistoryDstWid = '600px';
        }
      }
    }
  },
  created() {
    this.pagination.currentPage = 1;
    this.paginationF.currentPage = 1;
    this.InitDictItem();
    this.loadTableData();
    this.headers = { Authorization: 'Bearer ' + (getToken() || '') };
  },
  methods: {
    SendPDF() {
      this.$refs['noKeyObj'].validate((valid) => {
        if (valid) {
          this.chooseHistoryDstVisible = false;
          this.noKeyObj.isNokey = 'Y';
          this.uploadLoading = true;
          // this.$refs['newupload'].submit();

          const formData = new FormData();
          var files = new File([this.fileList[0].raw], this.fileList[0].name, {
            type: 'application/pdf'
          });
          formData.append('File', files);
          formData.append('isNokey', this.noKeyObj.isNokey);
          formData.append('achTo', this.noKeyObj.achTo);
          if (this.noKeyObj.achTo === 'B') {
            const addarray = [
              this.searchPDF.BAdd_1,
              this.searchPDF.BAdd_1_1,
              this.searchPDF.BAdd_1_2,
              this.searchPDF.BAdd_2,
              this.searchPDF.BAdd_2_1,
              this.searchPDF.BAdd_2_2,
              this.searchPDF.BAdd_2_3,
              this.searchPDF.BAdd_2_4,
              this.searchPDF.BAdd_3,
              this.searchPDF.BAdd_3_1,
              this.searchPDF.BAdd_3_2,
              this.searchPDF.BAdd_4,
              this.searchPDF.BAdd_5,
              this.searchPDF.BAdd_6,
              this.searchPDF.BAdd_7,
              this.searchPDF.BAdd_8,
              this.searchPDF.BAdd_9
            ];
            this.noKeyObj.data = mergerAddress(addarray);
          }
          formData.append('data', this.noKeyObj.data);
          UploadPDFWithDataAsync(formData)
            .then((response) => {
              if (response.Success) {
                this.$message({
                  message: ' 上傳成功',
                  type: 'success'
                });

                // this.chooseHistoryDstVisible = false;
              } else {
                this.$alert(
                  '上傳但解析失敗<br/>' + '<b>' + response.ErrMsg + '<b/>',
                  '解析失敗',
                  {
                    confirmButtonText: '確定',
                    dangerouslyUseHTMLString: true
                  }
                );
                // this.chooseHistoryDstVisible = false;
              }
            })
            .finally(() => {
              this.fileList = [];
              this.$refs.newupload.clearFiles();
              this.uploadLoading = false;
            });
          this.noKeyObj = {
            isNokey: '',
            isSpecific: '',
            achTo: '',
            data: ''
          };
          // this.chooseHistoryDstVisible = false;
        } else {
          return false;
        }
      });
    },
    /**
     * 初始化數據
     */
    InitDictItem() {
      getAllCategoryTreeTable().then((res) => {
        this.selectCategory = res.ResData;
      });
    },
    handleUploadBefore(file) {
      console.log(file.name);
      this.uploadLoading = true;
      // file.name = this.editFrom.FormId + 'pdf'
    },
    queryByLNID: function() {
      this.tableloading = true;
      var seachdata = {
        CurrenetPageIndex: this.pagination.currentPage,
        PageSize: this.pagination.pagesize,
        Keywords: this.searchform.name,
        Order: this.sortableData.order,
        Sort: this.sortableData.sort,
        CreatorTime1:
          this.searchform.CreateTime !== ''
            ? this.searchform.CreateTime[0]
            : '',
        CreatorTime2:
          this.searchform.CreateTime !== ''
            ? this.searchform.CreateTime[1]
            : '',
        RoleId: this.searchform.RoleId,
        FormId: this.searchform.FormId,
        FormName: this.searchform.FormName,
        GuildCountyCity: this.searchform.GuildCountyCity,
        Period: this.searchform.Period,
        CustTag: this.searchform.CustTag,
        Type: this.searchform.Type,
        selectedCategoryOptions: this.searchform.selectedCategoryOptions
      };
      getExternalformListWithPager(seachdata).then((res) => {
        this.tableData = res.ResData.Items;
        this.pagination.pageTotal = res.ResData.TotalItems;
        this.tableloading = false;
      });
    },
    /**
     * 加載頁面table數據
     */
    loadTableData: function() {
      this.tableloading = true;
      var seachdata = {
        CurrenetPageIndex: this.pagination.currentPage,
        PageSize: this.pagination.pagesize,
        Keywords: this.searchform.name,
        Order: this.sortableData.order,
        Sort: this.sortableData.sort,
        CreatorTime1:
          this.searchform.CreateTime !== ''
            ? this.searchform.CreateTime[0]
            : '',
        CreatorTime2:
          this.searchform.CreateTime !== ''
            ? this.searchform.CreateTime[1]
            : '',
        RoleId: this.searchform.RoleId,
        FormId: this.searchform.FormId,
        FormName: this.searchform.FormName,
        GuildCountyCity: this.searchform.GuildCountyCity,
        Period: this.searchform.Period,
        CustTag: this.searchform.CustTag,
        Type: this.searchform.Type,
        level: this.searchform.level,
        selectedCategoryOptions: this.searchform.selectedCategoryOptions
      };
      getExternalformListWithPager(seachdata).then((res) => {
        this.tableData = res.ResData.Items;
        this.pagination.pageTotal = res.ResData.TotalItems;
        this.tableloading = false;
      });
    },

    loadTableDataF: function() {
      this.tableloading = true;
      this.paginationF.pageTotal = this.editFrom.BuildingBelongings.length;
      this.tableloading = false;
    },
    /**
     * 點擊查詢
     */
    handleSearch: function() {
      this.pagination.currentPage = 1;
      this.loadTableData();
    },
    handleClick: function() {},
    // 表單重置
    reset() {
      this.editFrom = {
        Account: '',
        RealName: '',
        NickName: '',
        Gender: 1,
        Birthday: '',
        MobilePhone: '',
        Email: '',
        WeChat: '',
        DepartmentId: '',
        RoleId: '',
        IsAdministrator: true,
        EnabledMark: true,
        Description: ''
      };
      this.selectedOrganizeOptions = '';
      this.resetForm('editFrom');
    },
    resetF() {
      this.editFromF = {};
    },
    DownCheck(row) {
      console.log(row);
      if (row.IsNoKey === 'Y') {
        this.downloadPDFTemplate(row.FormId);
      } else {
        this.downloadpdfdialogVisible = true;
        this.downloadpdfFormId = row.FormId;
        this.downloadFormName = row.FormName;
        this.reqb = row.RequiredBuilding;
        this.reql = row.RequiredLandlord;
        this.reqr = row.RequiredRenter;
      }
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
          // this.$refs.gridtableImg.doLayout()
        }
      } else {
        this.editFormTitle = '新增';
        this.currentId = '';
        this.selectedOrganizeOptions = '';
        this.dialogEditFormVisible = true;
      }
    },
    /**
     * ShowUploadPdfWithDataDialog     *
     */
    ShowUploadPdfWithDataDialog: function() {
      this.ActionName = 'A'; // 這樣切換才不會在 A以外 然後就不畫
      this.dialogUploadPdfWithDataFormVisible = true;
    },
    bindEditInfo: function() {
      getExternalformDetail(this.currentId)
        .then((res) => {
          this.editFrom = res.ResData;
        })
        .finally(() => {
          this.loading = false;
        });
    },
    bindEditInfoF: function() {
      this.editFromF = this.currentSelectedF[0];
    },
    /**
     * 新增/修改保存
     */
    saveEditForm() {
      // console.log(this.editFrom.RoleId)
      this.$refs['editFrom'].validate((valid) => {
        if (valid) {
          const data = {
            FormId: this.editFrom.FormId,
            FormName: this.editFrom.FormName,
            GuildCountyCity: this.editFrom.GuildCountyCity,
            CompletionDate: this.editFrom.CompletionDate,
            Period: this.editFrom.Period,
            Type: this.editFrom.Type,
            Vno: this.editFrom.Vno,
            TBNO: this.editFrom.TBNO,
            CustTag: this.editFrom.CustTag,
            RequiredLandlord: this.editFrom.RequiredLandlord,
            RequiredBuilding: this.editFrom.RequiredBuilding,
            RequiredRenter: this.editFrom.RequiredRenter,
            MustExistsLandLord: this.editFrom.MustExistsLandLord,
            MustExistsBuilding: this.editFrom.MustExistsBuilding,
            MustExistsRenter: this.editFrom.MustExistsRenter,
            ArchiveLTo: this.editFrom.ArchiveLTo,
            level: this.editFrom.level,
            Id: this.currentId
          };

          var url = 'Externalform/Insert';
          if (this.currentId !== '') {
            url = 'Externalform/Update';
          }
          saveExternalform(data, url).then((res) => {
            if (res.Success) {
              this.$message({
                message: '恭喜你，操作成功',
                type: 'success'
              });
              this.dialogEditFormVisible = false;
              this.currentSelected = '';
              this.selectedOrganizeOptions = '';
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
    deletePhysics: function() {
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
          .then(function() {
            const data = {
              Ids: currentIds
            };
            return deleteExternalform(data);
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
    handleSortChangeF: function(column) {
      this.sortableData.sort = column.prop;
      if (column.order === 'ascending') {
        this.sortableData.order = 'asc';
      } else {
        this.sortableData.order = 'desc';
      }
      // this.loadTableData()
    },
    /**
     * 當用戶手動勾選checkbox數據行事件
     */
    handleSelectChange: function(selection, row) {
      this.currentSelected = selection;
    },
    handleSelectChangeD: function(selection, row) {
      this.currentSelectedD = selection;
    },
    handleSelectChangeF: function(selection, row) {
      this.currentSelectedF = selection;
    },
    /**
     * 當用戶手動勾選全選checkbox事件
     */
    handleSelectAllChange: function(selection) {
      this.currentSelected = selection;
    },
    handleSelectAllChangeD: function(selection) {
      this.currentSelectedD = selection;
    },
    handleSelectAllChangeF: function(selection) {
      this.currentSelectedF = selection;
    },
    /**
     * 選擇每頁顯示數量
     */
    handleSizeChange(val) {
      this.pagination.pagesize = val;
      this.pagination.currentPage = 1;
      this.loadTableData();
    },
    // handleSizeChangeF(val) {
    //   this.paginationF.pagesize = val
    //   this.paginationF.currentPage = 1
    //   this.loadTableDataF()
    // },
    /**
     * 選擇當頁面
     */
    handleCurrentChange(val) {
      this.pagination.currentPage = val;
      this.loadTableData();
    },

    uploadsuccess: function(response, file, fileList) {
      this.uploadLoading = false;
      if (response.Success) {
        this.$message({
          message: file.name + ' 上傳成功',
          type: 'success'
        });
        this.chooseHistoryDstVisible = false;
        // }
        // this.$store.state.tagsView.cachedViews = [];
      } else {
        if (response.ResData === 'IsNoKey') {
          this.chooseHistoryDstVisible = true;
        } else {
          this.$alert(
            file.name +
              '上傳但解析失敗<br/>' +
              '<b>' +
              response.ErrMsg +
              '<b/>',
            '解析失敗',
            {
              confirmButtonText: '確定',
              dangerouslyUseHTMLString: true
            }
          );

          this.fileList = [];
          this.$refs.newupload.clearFiles();
          this.chooseHistoryDstVisible = false;
          this.noKeyObj = {
            isNokey: '',
            isSpecific: '',
            achTo: '',
            data: ''
          };
        }
      }

      this.fileList = [];
      this.$refs.newupload.clearFiles();
    },
    uploaderror: function(err, file, fileList) {
      this.uploadLoading = false;
      console.log('uploaderror');
      // console.log(file)
      console.log(err);
      console.log(file);
      console.log(fileList);
      this.$message({
        message: file.name + ' 上傳失敗',
        type: 'error'
      });
      // this.loadTableDataD()
    },
    uploadchange: function(file) {
      this.$refs['newupload'].submit();
      if (this.fileList[0] !== file) {
        this.fileList.push(file);
      }
      // console.log('uploadchange', file);
      // const formData = new FormData();
      // var files = new File([file.raw], file.name, {
      //   type: 'application/pdf'
      // });
      // formData.append('File', files);
      // this.uploadLoading = true;
      // CheckIsNoKeyForm(formData)
      //   .then(res => {
      //     if (res.ResData) {
      //       this.chooseHistoryDstVisible = true;
      //     } else if (res.ResData === false) {
      //       this.$refs['newupload'].submit();
      //     }
      //   })
      //   .finally(() => {
      //     this.uploadLoading = false;
      //   });
    },
    downloadPDFTemplate: function(label) {
      // console.log(url)
      // https://stackoverflow.com/questions/53772331/vue-html-js-how-to-download-a-file-to-browser-using-the-download-tag
      this.tableloading = true;

      downloadPDFTemplateByFormId(label)
        .then((response) => {
          if (response == null) {
            // this.$message({
            //   message: '下載失敗',
            //   type: 'error'
            // })
            this.$alert('下載失敗', '下載失敗', {
              confirmButtonText: '確定'
            });
            return;
          }
          if (response.type !== 'application/pdf') {
            console.log(response.type);
            var reader = new FileReader();
            reader.onload = (e) => {
              const msg = JSON.parse(e.target.result);
              console.log(msg);
              // this.$message({
              //   message: '下載失敗, 請確認空白PDF(' + label + ')已上傳' + msg,
              //   type: 'error'
              // })
              this.$alert('下載失敗', '下載失敗', {
                confirmButtonText: '確定'
              });
            };
            reader.readAsText(response);
            return;
          }
          // item.ImgUrl = URL.createObjectURL(response)
          // this.tableloading = false
          const blob = response; // new Blob([response.data], { type: 'image/jpeg' })
          const link = document.createElement('a');
          link.href = URL.createObjectURL(blob);
          link.download = label;
          link.click();
          URL.revokeObjectURL(link.href);
        })
        .catch((error) => {
          console.log(error);
        })
        .finally(() => {
          this.tableloading = false;
        });
    },
    // rowdblclick(row, column, event) {
    //   this.$refs.gridtable.clearSelection();
    //   this.currentSelected = '';
    //   this.rerenderGridtable = Date.now(); // 強制重繪 https://bbchin.com/archives/el-table-rerender
    //   this.$nextTick(function() {
    //     // https://ithelp.ithome.com.tw/articles/10240669
    //     this.$refs.gridtable.toggleRowSelection(row, true);
    //     this.currentSelected = this.$refs.gridtable.selection;
    //     this.ShowEditOrViewDialog('edit');
    //   });
    // },
    /**
     *選擇表單分類
     */
    handleSelectCategoryChange: function() {
      const category = this.$refs['categoryPath'].getCheckedNodes()[0];
      if (category) {
        this.selectedCategoryFullPath = category.pathLabels;
      } else {
        this.selectedCategoryFullPath = '';
      }
    },
    getsearchaddress(addressData) {
      this.searchPDF.BAdd_1 = addressData.Add_1;
      this.searchPDF.BAdd_1_1 = addressData.Add_1_1;
      this.searchPDF.BAdd_1_2 = addressData.Add_1_2;
      this.searchPDF.BAdd_2 = addressData.Add_2;
      this.searchPDF.BAdd_2_1 = addressData.Add_2_1;
      this.searchPDF.BAdd_2_2 = addressData.Add_2_2;
      this.searchPDF.BAdd_2_3 = addressData.Add_2_3;
      this.searchPDF.BAdd_2_4 = addressData.Add_2_4;
      this.searchPDF.BAdd_3 = addressData.Add_3;
      this.searchPDF.BAdd_3_1 = addressData.Add_3_1;
      this.searchPDF.BAdd_3_2 = addressData.Add_3_2;
      this.searchPDF.BAdd_4 = addressData.Add_4;
      this.searchPDF.BAdd_5 = addressData.Add_5;
      this.searchPDF.BAdd_6 = addressData.Add_6;
      this.searchPDF.BAdd_7 = addressData.Add_7;
      this.searchPDF.BAdd_8 = addressData.Add_8;
      this.searchPDF.BAdd_9 = addressData.Add_9;
    }
  }
};
</script>
