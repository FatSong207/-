<template>
  <div class="app-container">
    <div class="filter-container">
      <!-- <el-collapse v-model="activeNames"> -->
      <!-- <el-collapse-item
          title="步驟一:表單代號搜尋"
          name="1"
        >
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
              <el-form-item label="自訂標籤：">
                <el-input
                  v-model="searchform.CustTag"
                  clearable
                />
              </el-form-item>
              <el-form-item label="部門：">
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
              </el-form-item>
              <el-form-item>
                <el-button
                  type="primary"
                  @click="handleSearch()"
                >查詢</el-button>
              </el-form-item>
            </el-form>
          </el-card>
        </el-collapse-item> -->
      <!-- <el-collapse-item
          title="步驟二:表單內容搜尋"
          name="2"
        > -->
      <el-card>
        <el-form
          ref="searchform"
          :inline="true"
          :model="searchPDF"
          class="demo-form-inline"
          size="small"
        >
          <!-- <el-row>
            <el-form-item label="房東身分證字號/居留證號：">
              <el-input
                v-model="searchPDF.LNID"
                clearable
              >
                <em
                  slot="suffix"
                  class="el-input__icon el-icon-search"
                />
              </el-input>
            </el-form-item>
            <el-form-item label="房東統一編號：">
              <el-input
                v-model="searchPDF.LCID"
                clearable
              />
            </el-form-item>
            <el-form-item label="物件編號：">
                  <el-input
                    v-model="searchPDF.BID"
                    clearable
                  />
                </el-form-item>
          </el-row> -->
          <el-row>
            <h3>請指定建物地址</h3>
            <el-form-item label="建物門牌地址：">
              <Address
                title="建物地址"
                :sendedform="sendedform"
                @geteditaddress="geteditaddress"
              />
            </el-form-item>
          </el-row>
        </el-form>
      </el-card>
      <!-- </el-collapse-item> -->
      <!-- </el-collapse> -->
    </div>
    <!-- <internalForm
      v-show="internalFormPage"
      ref="internalForm"
      :searchPDF="searchPDF"
    />
    <internalSignForm
      v-show="signFormPage"
      ref="internalSignForm"
      :searchPDF="searchPDF"
    /> -->

    <el-card>
      <!-- <div class="list-btn-container">
        <el-button-group>
          <el-button
            v-hasPermi="['Building/Add']"
            type="primary"
            icon="el-icon-plus"
            size="small"
          >新增</el-button>
          <el-button
            v-hasPermi="['Building/Edit']"
            type="primary"
            icon="el-icon-edit"
            class="el-button-modify"
            size="small"
          >修改</el-button>
          <el-button
            v-hasPermi="['Building/Delete']"
            type="danger"
            icon="el-icon-delete"
            size="small"
            @click="deletePhysics()"
          >刪除</el-button>
          <el-button
            type="default"
            icon="el-icon-refresh"
            size="small"
            @click="searchFormList()"
          >更新</el-button>
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
        :default-sort="{ prop: 'SortCode', order: 'descending' }"
        @sort-change="handleSortChange"
        @row-dblclick="rowdblclick"
      >
        <!-- <el-table-column
          type="selection"
          width="40"
        /> -->
        <el-table-column
          label="操作"
          width="150"
          align="center"
        >
          <!-- <router-link to="/chaochi/internalFormsDetail/index" >跳轉</router-link> -->
          <template slot-scope="scope">
            <!-- <el-tooltip
              effect="dark"
              content="打開空白表單"
              placement="top"
              :enterable="false"
            >
              <el-button
                v-hasPermi="['Building/Add']"
                type="primary"
                icon="el-icon-download"
                size="mini"
                @click="Jump(scope.row,searchPDF)"
                v-if="scope.row.FormId!=='A000301' && scope.row.FormId!=='A000401'"
              ></el-button>
            </el-tooltip> -->
            <el-tooltip
              effect="dark"
              content="根據查詢條件打開表單"
              placement="top"
              :enterable="false"
            >
              <el-button
                type="primary"
                icon="el-icon-download"
                size="mini"
                @click="Jump(scope.row,searchPDF)"
              />
            </el-tooltip>
          </template>
        </el-table-column>
        <el-table-column
          prop="FormId"
          label="表單代號"
          width="120"
        />
        <el-table-column
          prop="FormName"
          label="表單名稱"
          width="240"
        />
        <el-table-column
          prop="Vno"
          label="版本"
        />
        <el-table-column
          prop="Dept"
          label="部門"
          width="120"
        />
        <el-table-column
          prop="Type"
          label="類別"
          width="120"
        />
        <el-table-column
          prop="LastModifyTime"
          label="更新時間"
          width="180"
          sortable="custom"
        >
          <!-- <template slot-scope="scope">
            {{ scope.row.LastModifyTime }}
          </template> -->
        </el-table-column>
        <!-- <el-table-column
          prop="CreatorTime"
          label="建立時間"
          width="180"
          sortable
        /> -->
        <!-- <el-table-column fixed="right" label="操作" width="100">
          <template slot-scope="scope">
            <el-button
              @click="advanceSearch(scope.row, '瀏覽')"
              type="text"
              size="small"
              v-show="scope.row.IsSign"
              >瀏覽</el-button
            >
            <el-button
              @click="advanceSearch(scope.row, '编辑')"
              type="text"
              size="small"
              v-show="!scope.row.IsSign"
              >编辑</el-button
            >
            <el-button
              @click="advanceSearch(scope.row, '簽名')"
              type="text"
              size="small"
              v-show="scope.row.DataExist && !scope.row.IsSign && !scope.row.IsFileing"
              >簽名</el-button
            >
            <el-button
              @click="advanceSearch(scope.row, '歸檔')"
              type="text"
              size="small"
              v-show="scope.row.IsSign && !scope.row.IsFileing"
              >歸檔</el-button
            >
            <el-button
              @click="advanceSearch(scope.row, '下載')"
              type="text"
              size="small"
              v-show="scope.row.IsFileing"
              >下載</el-button
            >
          </template>
        </el-table-column> -->
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
// import axios from 'axios'
// import {
//   findFormList
// } from '@/api/chaochi/externalform/externalformservice';
import {
  handelAdd,
  checkExists
} from '@/api/chaochi/SecurityForm/securityformService';
import { getInternalformListWithPager } from '@/api/chaochi/internalform/internalform';
// import internalForm from './form.vue'
// import internalSignForm from './signFormA000201.vue'
import { getToken } from '@/utils/auth';
import Address from '@/components/Address/Address.vue';
export default {
  name: 'SecurityForm',
  components: {
    Address
  },
  data() {
    return {
      // btnGroup: ['瀏覽', '编辑', '簽名', '歸檔', '下載'],
      // isSignFormPage: false,
      // isFileFormPage: false,
      // isInternalFormPage: true,
      sendedform: {},
      // formInline: {
      //   user: '',
      //   region: ''
      // },
      // dialogVisible: false,
      // rerenderGridtable: Date.now(),
      // internalFormPage: false,
      // signFormPage: false,
      // tableDisplay: true,
      // TypeOptions: [
      //   {
      //     value: '',
      //     label: '全部'
      //   },
      //   {
      //     value: '委託類',
      //     label: '委託類'
      //   },
      //   {
      //     value: '授權類',
      //     label: '授權類'
      //   },
      //   {
      //     value: '領據類',
      //     label: '領據類'
      //   },
      //   {
      //     value: '切結類',
      //     label: '切結類'
      //   },
      //   {
      //     value: '調查類',
      //     label: '調查類'
      //   },
      //   {
      //     value: '終止類',
      //     label: '終止類'
      //   }
      // ],
      // DeptOptions: [
      //   {
      //     value: '',
      //     label: '共用'
      //   },
      //   {
      //     value: '社宅處',
      //     label: '社宅處'
      //   },
      //   {
      //     value: '物管處',
      //     label: '物管處'
      //   },
      //   {
      //     value: '商仲部',
      //     label: '商仲部'
      //   },
      //   {
      //     value: '捷運部',
      //     label: '捷運部'
      //   },
      //   {
      //     value: '商務中心',
      //     label: '商務中心'
      //   }
      // ],
      // activeNames: ['1', '2'],
      searchform: {
        RoleId: '',
        Keywords: '',
        CreateTime: '',
        FormId: '',
        FormName: '',
        Dept: '',
        Type: ''
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
      searchPDF: {
        testName: '',
        LNID: '',
        LCID: '',
        TBNO: '',
        Vno: '',
        BID: '',
        FName: '',
        FormId: '',
        CurrenetPageIndex: '',
        PageSize: '',
        Order: '',
        Sort: '',
        FormName: '',
        Type: '',
        Dept: '',
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
        BAdd_9: ''
      }
    };
  },
  created() {
    // console.log('created')
    this.pagination.currentPage = 1;
    this.paginationF.currentPage = 1;
    this.FindWithPagerSearchAsync();
    this.headers = { Authorization: 'Bearer ' + (getToken() || '') };
  },
  methods: {
    /**
     * 點擊查詢
     */
    handleSearch: function() {
      this.pagination.currentPage = 1;
      // this.loadTableData()
      this.FindWithPagerSearchAsync();
    },
    /**
     * 加載頁面table數據
     */
    // loadTableData: function() {
    //   this.tableloading = true;
    //   var seachdata = {
    //     CurrenetPageIndex: this.pagination.currentPage,
    //     PageSize: this.pagination.pagesize,
    //     Keywords: this.searchform.name,
    //     Order: this.sortableData.order,
    //     Sort: this.sortableData.sort,
    //     CreatorTime1:
    //       this.searchform.CreateTime !== ''
    //         ? this.searchform.CreateTime[0]
    //         : '',
    //     CreatorTime2:
    //       this.searchform.CreateTime !== ''
    //         ? this.searchform.CreateTime[1]
    //         : '',
    //     RoleId: this.searchform.RoleId,
    //     FormId: this.searchform.FormId,
    //     FormName: this.searchform.FormName,
    //     GuildCountyCity: this.searchform.GuildCountyCity,
    //     Period: this.searchform.Period,
    //     CustTag: this.searchform.CustTag,
    //     Type: this.searchform.Type
    //   };
    //   getExternalformListWithPager(seachdata).then(res => {
    //     this.tableData = res.ResData.Items;
    //     this.pagination.pageTotal = res.ResData.TotalItems;
    //     this.tableloading = false;
    //   });
    // },
    geteditaddress(addressData) {
      this.searchPDF.BAdd = addressData.Address;
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
    },
    handleClose() {
      this.dialogVisible = false;
    },
    // sample: function() {
    //   this.sendedform = {
    //     Add_1: '桃園',
    //     Add_1_1: '/Off',
    //     Add_1_2: '/Yes',
    //     Add_2: '桃園',
    //     Add_2_1: '/Off',
    //     Add_2_2: '/Off',
    //     Add_2_3: '/Off',
    //     Add_2_4: '/Yes',
    //     Add_3: '中山',
    //     Add_3_1: '/Off',
    //     Add_3_2: '/Yes',
    //     Add_4: '1',
    //     Add_5: '1',
    //     Add_6: '1',
    //     Add_7: '1',
    //     Add_8: '1',
    //     Add_9: '1'
    //   }
    // },
    // advanceSearch: function(row, pageType) {
    //   // this.dialogVisible = true;
    //   this.searchPDF.FormId = row.FormId
    //   this.searchPDF.FName = row.FormId
    //   this.searchPDF.TBNO = row.TBNO
    //   this.searchPDF.Vno = row.Vno
    //   switch (pageType) {
    //     case '编辑':
    //       this.isInternalFormPage = true
    //       this.isFileFormPage = this.isSignFormPage = false
    //       this.searchForm()
    //       break
    //     case '歸檔':
    //       this.fileingForm()
    //       break
    //     case '下載':
    //       this.download()
    //       break
    //     case '簽名':
    //     case '瀏覽':
    //       this.isSignFormPage = true
    //       this.isFileFormPage = this.isInternalFormPage = false
    //       this.signForm()
    //       break
    //   }
    // },
    // searchForm: function() {
    //   this.internalFormPage = this.isInternalFormPage = true
    //   this.signFormPage = this.tableDisplay = this.dialogVisible = this.isFileFormPage = this.isSignFormPage = false
    //   this.$refs.internalForm.findForm()
    // },
    // signForm: function() {
    //   this.signFormPage = this.isSignFormPage = true
    //   this.internalFormPage = this.tableDisplay = this.dialogVisible = this.isFileFormPage = this.isInternalFormPage = false
    //   this.$refs.internalSignForm.findForm()
    // },
    handleSortChange: function(column) {
      this.sortableData.sort = column.prop;
      if (column.order === 'ascending') {
        this.sortableData.order = 'asc';
      } else {
        this.sortableData.order = 'desc';
      }
      this.FindWithPagerSearchAsync();
    },
    // handleSelectChange: function(selection, row) {
    //   this.currentSelected = selection
    // },
    // handleSelectChangeD: function(selection, row) {
    //   this.currentSelectedD = selection
    // },
    // handleSelectChangeF: function(selection, row) {
    //   this.currentSelectedF = selection
    // },
    // handleSelectAllChange: function(selection) {
    //   this.currentSelected = selection
    // },
    // handleSelectAllChangeD: function(selection) {
    //   this.currentSelectedD = selection
    // },
    // handleSelectAllChangeF: function(selection) {
    //   this.currentSelectedF = selection
    // },
    handleCurrentChange(val) {
      this.pagination.currentPage = val;
      this.FindWithPagerSearchAsync();
    },
    handleSizeChange(val) {
      this.pagination.pagesize = val;
      this.pagination.currentPage = 1;
      this.FindWithPagerSearchAsync();
    },
    rowdblclick(row, column, event) {
      /*
      this.$refs.gridtable.clearSelection();
      this.currentSelected = "";
      this.rerenderGridtable = Date.now(); // 強制重繪 https://bbchin.com/archives/el-table-rerender
      this.$nextTick(function () {
        // https://ithelp.ithome.com.tw/articles/10240669
        this.$refs.gridtable.toggleRowSelection(row, true);
        this.currentSelected = this.$refs.gridtable.selection;
        this.ShowEditOrViewDialog("edit");
      });
      */
    },
    // searchFormList: function() {
    //   this.pagination.currentPage = 1
    //   this.tableloading = true
    //   this.tableDisplay = true
    //   this.signFormPage = this.internalFormPage = this.dialogVisible = false
    //   this.FindWithPagerSearchAsync()
    // },
    FindWithPagerSearchAsync: function() {
      this.tableloading = true;
      var filter = this.searchform;
      var seachdata = {
        CurrenetPageIndex: this.pagination.currentPage,
        PageSize: this.pagination.pagesize,
        Filter: filter,
        Keywords: 'Security',
        Order: this.sortableData.order,
        Sort: this.sortableData.sort
      };
      getInternalformListWithPager(seachdata).then(res => {
        this.tableData = res.ResData.Items;
        this.pagination.pageTotal = res.ResData.TotalItems;
        this.tableloading = false;
      });
    },
    // fileingForm: function() {
    //   fileingForm(this.searchPDF).then(res => {
    //     // if (res.ResData.ErrCode == "0") {
    //     this.$message({
    //       message: '已歸檔 !',
    //       type: 'success'
    //     })
    //     // }
    //   })
    // },
    // download: function() {
    //   this.$message({
    //     message: '開發中',
    //     type: 'warging'
    //   })
    //   return false
    //   const label = this.searchPDF.FormId
    //   downloadPDFWithDataByFormId(this.searchPDF)
    //     .then(response => {
    //       if (response == null) {
    //         this.$message({
    //           message: '下載失敗',
    //           type: 'error'
    //         })
    //         return
    //       }
    //       if (response.type !== 'application/pdf') {
    //         var reader = new FileReader()
    //         reader.onload = e => {
    //           const msg = JSON.parse(e.target.result)
    //           console.log(msg)
    //           this.$message({
    //             message: '下載失敗, 請確認空白PDF(' + label + ')已上傳',
    //             type: 'error'
    //           })
    //         }
    //         reader.readAsText(response)
    //         return
    //       }
    //       const blob = response
    //       const link = document.createElement('a')
    //       link.href = URL.createObjectURL(blob)
    //       link.download = label
    //       link.click()
    //       URL.revokeObjectURL(link.href)
    //     })
    //     .catch(error => {
    //       console.log(error)
    //     })
    // },
    getsearchaddress(addressData) {
      /*
      this.searchform.LNAdd_1 = addressData.Add_1
      this.searchform.LNAdd_1_1 = addressData.Add_1_1
      this.searchform.LNAdd_1_2 = addressData.Add_1_2
      this.searchform.LNAdd_2 = addressData.Add_2
      this.searchform.LNAdd_2_1 = addressData.Add_2_1
      this.searchform.LNAdd_2_2 = addressData.Add_2_2
      this.searchform.LNAdd_2_3 = addressData.Add_2_3
      this.searchform.LNAdd_2_4 = addressData.Add_2_4
      this.searchform.LNAdd_3 = addressData.Add_3
      this.searchform.LNAdd_3_1 = addressData.Add_3_1
      this.searchform.LNAdd_3_2 = addressData.Add_3_2
      this.searchform.LNAdd_4 = addressData.Add_4
      this.searchform.LNAdd_5 = addressData.Add_5
      this.searchform.LNAdd_6 = addressData.Add_6
      this.searchform.LNAdd_7 = addressData.Add_7
      this.searchform.LNAdd_8 = addressData.Add_8
      this.searchform.LNAdd_9 = addressData.Add_9
      */
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
    },
    Jump: function(row, searchPDF) {
      if (searchPDF.BAdd_1_1 === '') {
        this.$message.error('請指定建物地址');
      } else {
        handelAdd(searchPDF).then(res => {
          checkExists(res).then(res => {
            if (res.ResData === true) {
              // 判斷第二步驟是否有輸入地址
              if (searchPDF.BAdd_1 !== '') {
                var p = {
                  FormId: row.FormId,
                  FormName: row.FormName,
                  searchPDF: searchPDF
                };
              } else {
                p = {
                  FormId: row.FormId,
                  FormName: row.FormName
                };
              }
              this.$router.push({
                // path: '/chaochi/internalFormsDetail/index',
                name: 'SecurityFormDetail',
                params: p
              });
            } else {
              this.$message.error('查無此建物或無權操作此建物!');
            }
          });
        });
      }
    },
    Jump2: function(row) {
      // console.log(row.FormId)
      this.$router.push({
        // path: '/chaochi/internalFormsDetail/index',
        name: 'InternalformsDetailSign',
        params: {
          FormId: row.FormId,
          FormName: row.FormName
        }
      });
    }
  }
};
</script>
<style>
</style>
