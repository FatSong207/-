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
          <el-form-item label="法人名稱：">
            <el-input
              v-model="searchform.LCName"
              clearable
            />
          </el-form-item>
          <el-form-item label="代表人：">
            <el-input
              v-model="searchform.LCRep"
              clearable
            />
          </el-form-item>
          <el-form-item label="統一編號：">
            <el-input
              v-model="searchform.LCID"
              clearable
            />
          </el-form-item>
          <el-form-item label="公司電話：">
            <el-input
              v-model="searchform.LCTel_2"
              placeholder="請輸入電話"
              clearable
              style="width: 220px"
              class="input-with-select"
              :maxlength="8"
              onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
            >
              <el-select
                slot="prepend"
                v-model="searchform.LCTel_1"
                placeholder="區號"
                style="width: 80px"
                clearable
              >
                <el-option
                  v-for="item in zoneOptions"
                  :key="item.value"
                  :label="item.label"
                  :value="item.value"
                />
              </el-select>
            </el-input>
            <!-- <el-input
              v-model="searchform.LCTel"
              clearable
            /> -->
          </el-form-item>
          <el-form-item label="歸屬業務：">
            <el-select
              v-model="searchform.CreatorId"
              placeholder="請選擇"
              clearable
            >
              <el-option
                v-for="item in Sales"
                :key="item.Id"
                :label="item.RealName"
                :value="item.Id"
              />
            </el-select>
          </el-form-item>
          <el-row>
            <el-form-item label="公司登記地址：">
              <el-input
                v-model="searchform.LCAdd"
                clearable
              />
            </el-form-item>
          </el-row>

          <!-- <Address
            title="公司登記地址"
            @getsearchaddress="getsearchaddress"
          /> -->
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
          <el-button
            v-hasPermi="['CustomerLC/Add']"
            type="primary"
            icon="el-icon-plus"
            size="small"
            @click="ShowEditOrViewDialog()"
          >新增</el-button>
          <el-button
            v-hasPermi="['CustomerLC/Edit']"
            type="primary"
            icon="el-icon-edit"
            class="el-button-modify"
            size="small"
            @click="ShowEditOrViewDialog('edit')"
          >修改</el-button>
          <el-button
            v-hasPermi="['CustomerLC/Delete']"
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
          <el-button
            v-hasPermi="['Building/Add']"
            type="primary"
            icon="el-icon-s-home"
            size="small"
            @click="ShowCBBDialog()"
          >新增建物</el-button>
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
        @row-dblclick="rowdblclick"
      >
        <el-table-column
          type="selection"
          width="30"
        />
        <el-table-column
          prop="LCName"
          label="法人名稱"
          sortable="custom"
          width="200"
          fixed
        />
        <el-table-column
          prop="LCRep"
          label="代表人"
          sortable="custom"
          width="120"
          align="center"
        />
        <el-table-column
          prop="LCID"
          label="統一編號"
          sortable="custom"
          width="120"
          align="center"
        />
        <el-table-column
          prop="LCTel"
          label="公司電話"
          sortable="custom"
          width="120"
          align="center"
        />
        <el-table-column
          prop="LCAdd"
          label="法人公司登記地址"
          sortable="custom"
          align="left"
        />
        <!-- <el-table-column prop="Line" label="Line" sortable="custom" width="120" align="center" /> -->
        <el-table-column
          prop="CreatorUserName"
          label="歸屬業務"
          sortable="custom"
          align="left"
          width="320"
        />
        <el-table-column
          prop="LastModifyTime"
          label="更新時間"
          sortable
          width="200"
        />
        <!-- <el-table-column
          prop="CreatorTime"
          label="建立時間"
          sortable
          width="200"
        /> -->
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
    </el-card>
    <el-dialog
      ref="dialogEditForm"
      :show-close="false"
      :close-on-press-escape="false"
      :title="editFormTitle"
      :visible.sync="dialogEditFormVisible"
      width="1040px"
      top="0"
      fullscreen
    >
      <el-tabs
        v-model="activeName"
        v-loading="loading"
        v-loading.lock="loading"
        element-loading-text="載入中..."
        type="border-card"
      >
        <el-tab-pane
          label="出租法人基本資料"
          name="A"
        >
          <el-card>
            <el-form
              ref="editFrom"
              :inline="true"
              :model="editFrom"
              :rules="rules"
              class="demo-form-inline"
            >
              <el-form-item
                label="法人名稱："
                :label-width="formLabelWidth"
                prop="LCName"
              >
                <el-input
                  v-model="editFrom.LCName"
                  placeholder="請輸法入名稱"
                  autocomplete="off"
                  clearable
                />
              </el-form-item>
              <el-form-item
                label="代表人："
                :label-width="formLabelWidth"
                prop="LCRep"
              >
                <el-input
                  v-model="editFrom.LCRep"
                  placeholder="代表人"
                  autocomplete="off"
                  clearable
                />
              </el-form-item>
              <el-form-item
                label="統一編號："
                :label-width="formLabelWidth"
                prop="LCID"
              >
                <el-input
                  v-model="editFrom.LCID"
                  placeholder="請輸入統一編號"
                  autocomplete="off"
                  maxlength="8"
                  clearable
                  :disabled="LCdisabled"
                />
              </el-form-item>
              <el-form-item
                label="公司電話："
                :label-width="formLabelWidth"
                prop="LCTel_2"
              >
                <el-input
                  v-model="editFrom.LCTel_2"
                  placeholder="請輸入電話"
                  clearable
                  style="width: 220px"
                  class="input-with-select"
                  :maxlength="8"
                  onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                >
                  <el-select
                    slot="prepend"
                    v-model="editFrom.LCTel_1"
                    placeholder="區號"
                    style="width: 80px"
                  >
                    <el-option
                      v-for="item in zoneOptions"
                      :key="item.value"
                      :label="item.label"
                      :value="item.value"
                    />
                  </el-select>
                </el-input>
              </el-form-item>
              <!-- <el-form-item
                label="公司電話："
                :label-width="formLabelWidth"
                prop="LCTel"
              >
                <el-input
                  v-model="editFrom.LCTel_1"
                  placeholder="區號"
                  autocomplete="off"
                  style="width: 80px"
                  clearable
                />
                <el-input
                  v-model="editFrom.LCTel_2"
                  placeholder="號碼"
                  autocomplete="off"
                  style="width: 140px"
                  clearable
                />
              </el-form-item> -->
              <el-row>
                <el-form-item
                  label="登記地址："
                  :label-width="formLabelWidth"
                >
                  <Address
                    :sendedform="sendedform"
                    title="公司登記地址"
                    @geteditaddress="geteditaddress"
                  />
                </el-form-item>
              </el-row>
              <el-form-item
                label=" "
                label-width="80px"
              >
                <el-checkbox
                  v-model="LNAddSameCom"
                >登記地址同營業地址</el-checkbox>
              </el-form-item>
              <el-form-item
                v-show="LNAddSameCom === false"
                label="營業地址："
              >
                <Address
                  :sendedform="sendedform1"
                  :resetall="LNAddSameCom"
                  title="公司營業地址"
                  @geteditaddress="geteditaddress"
                />
              </el-form-item>

              <div style="padding-top: 25px">
                <el-divider content-position="left">
                  <h2>代表人/代理人A</h2>
                </el-divider>
              </div>
              <el-row>
                <el-col :span="6">
                  <el-form-item
                    label="姓名："
                    :label-width="formLabelWidth"
                    prop="LCAGName_A"
                  >
                    <el-input
                      v-model="editFrom.LCAGName_A"
                      placeholder="請輸入姓名"
                      autocomplete="off"
                      clearable
                    />
                  </el-form-item>
                </el-col>
                <el-col :span="6">
                  <el-form-item
                    label="身份證字號/居留證號："
                    :label-width="formLabelWidth"
                    prop="LCAGID_A"
                  >
                    <el-input
                      v-model="editFrom.LCAGID_A"
                      v-upperCase
                      placeholder="請輸入身分證字號/居留證號"
                      autocomplete="off"
                      clearable
                      maxlength="10"
                    />
                  </el-form-item>
                </el-col>
                <el-col :span="6">
                  <el-form-item
                    label="手機："
                    :label-width="formLabelWidth"
                    prop="LCAGCell_A"
                  >
                    <el-input
                      v-model="editFrom.LCAGCell_A"
                      placeholder="請輸入手機"
                      autocomplete="off"
                      clearable
                    />
                  </el-form-item>
                </el-col>
                <el-col :span="6">
                  <el-form-item
                    label="電話："
                    :label-width="formLabelWidth"
                  >
                    <el-input
                      v-model="editFrom.LCAGTel_2_A"
                      placeholder="請輸入電話"
                      clearable
                      style="width: 220px"
                      class="input-with-select"
                      :maxlength="8"
                      onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                    >
                      <el-select
                        slot="prepend"
                        v-model="editFrom.LCAGTel_1_A"
                        placeholder="區號"
                        style="width: 80px"
                      >
                        <el-option
                          v-for="item in zoneOptions"
                          :key="item.value"
                          :label="item.label"
                          :value="item.value"
                        />
                      </el-select>
                    </el-input>
                  </el-form-item>
                  <!-- <el-form-item
                    label="電話："
                    :label-width="formLabelWidth"
                    prop="LCAGTel_1_A"
                  >
                    <el-input
                      v-model="editFrom.LCAGTel_1_A"
                      placeholder="區號"
                      autocomplete="off"
                      style="width: 80px"
                      clearable
                    />
                    <el-input
                      v-model="editFrom.LCAGTel_2_A"
                      placeholder="號碼"
                      autocomplete="off"
                      style="width: 140px"
                      clearable
                    />
                  </el-form-item> -->
                </el-col>
              </el-row>
              <el-row>
                <el-col :span="24">
                  <el-form-item
                    label="地址："
                    :label-width="formLabelWidth"
                  >
                    <Address
                      :sendedform="sendedformAGA"
                      title="代A地址"
                      @geteditaddress="geteditaddress"
                    />
                  </el-form-item>
                </el-col>
              </el-row>
              <div style="padding-top: 25px">
                <el-divider content-position="left">
                  <h2>代理人B</h2>
                </el-divider>
              </div>
              <el-row>
                <el-col :span="6">
                  <el-form-item
                    label="姓名："
                    :label-width="formLabelWidth"
                    prop="LCAGName_B"
                  >
                    <el-input
                      v-model="editFrom.LCAGName_B"
                      placeholder="請輸入姓名"
                      autocomplete="off"
                      clearable
                    />
                  </el-form-item>
                </el-col>
                <el-col :span="6">
                  <el-form-item
                    label="身份證字號/居留證號："
                    :label-width="formLabelWidth"
                    prop="LCAGID_B"
                  >
                    <el-input
                      v-model="editFrom.LCAGID_B"
                      v-upperCase
                      placeholder="請輸入身份證字號/居留證號"
                      autocomplete="off"
                      clearable
                    />
                  </el-form-item>
                </el-col>
                <el-col :span="6">
                  <el-form-item
                    label="手機："
                    :label-width="formLabelWidth"
                    prop="LCAGCell_B"
                  >
                    <el-input
                      v-model="editFrom.LCAGCell_B"
                      placeholder="請輸入手機"
                      autocomplete="off"
                      clearable
                    />
                  </el-form-item>
                </el-col>
                <el-col :span="6">
                  <el-form-item
                    label="電話："
                    :label-width="formLabelWidth"
                  >
                    <el-input
                      v-model="editFrom.LCAGTel_2_B"
                      placeholder="請輸入電話"
                      clearable
                      style="width: 220px"
                      class="input-with-select"
                      :maxlength="8"
                      onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                    >
                      <el-select
                        slot="prepend"
                        v-model="editFrom.LCAGTel_1_B"
                        placeholder="區號"
                        style="width: 80px"
                      >
                        <el-option
                          v-for="item in zoneOptions"
                          :key="item.value"
                          :label="item.label"
                          :value="item.value"
                        />
                      </el-select>
                    </el-input>
                  </el-form-item>
                  <!-- <el-form-item
                    label="電話："
                    :label-width="formLabelWidth"
                    prop="LCAGTel_1_B"
                  >
                    <el-input
                      v-model="editFrom.LCAGTel_1_B"
                      placeholder="區號"
                      autocomplete="off"
                      style="width: 80px"
                      clearable
                    />
                    <el-input
                      v-model="editFrom.LCAGTel_2_B"
                      placeholder="號碼"
                      autocomplete="off"
                      style="width: 140px"
                      clearable
                    />
                  </el-form-item> -->
                </el-col>
              </el-row>
              <el-row>
                <el-col :span="24">
                  <el-form-item
                    label="地址："
                    :label-width="formLabelWidth"
                  >
                    <Address
                      :sendedform="sendedformAGB"
                      title="代B地址"
                      @geteditaddress="geteditaddress"
                    />
                  </el-form-item>
                </el-col>
              </el-row>

              <!-- <el-form-item
              label="歸屬業務："
              :label-width="formLabelWidth"
              prop="LNMail"
            >
              <el-input
                placeholder="輸入歸屬業務"
                autocomplete="off"
                clearable
              />
            </el-form-item> -->
            </el-form>
            <div class="rightbtn">
              <el-button
                v-hasPermi="['CustomerLC/Edit']"
                type="primary"
                size="small"
                icon="el-icon-paperclip"
                @click="saveEditForm()"
              >儲存</el-button>
              <el-button
                size="small"
                icon="el-icon-close"
                @click="closeDialogAndReset()"
              >關閉</el-button>
            </div>
          </el-card>
        </el-tab-pane>
        <el-tab-pane
          v-if="editFormTitle === '編輯'"
          label="出租法人匯款資料"
          name="C"
        >
          <el-card>
            <LCRemittance
              :id="currentId"
              :idno="editFrom.LCID"
              :name="editFrom.LCName"
              @cancel="closeDialogAndReset()"
            />
          </el-card>
        </el-tab-pane>
        <el-tab-pane
          v-if="editFormTitle === '編輯'"
          label="出租法人附件"
          name="D"
        >
          <el-card>
            <LCFormHistory
              :id="currentId"
              @cancel="closeDialogAndReset()"
            />
          </el-card>
        </el-tab-pane>
      </el-tabs>
      <!-- <div
        slot="footer"
        class="dialog-footer"
      >
        <el-button @click="closeDialogAndReset()">關 閉</el-button>
        <el-button
          v-if="activeName!=='B'"
          type="primary"
          @click="saveEditForm()"
        >確 認</el-button>
      </div> -->
    </el-dialog>
    <el-dialog
      ref="dialogEditForm"
      title="創建建物"
      :show-close="false"
      :close-on-press-escape="false"
      :visible.sync="dialogEditFormVisibleCBB"
      width="1340px"
    >
      <el-card class="box-card">
        <el-form
          ref="editFrom"
          :inline="true" class="demo-form-inline"
        >
          <el-form-item
            label="建物地址："
            class="el-form-item is-required el-form-item--medium"
          >
            <Address
              :sendedform="sendedformCBB"
              title="新建地址"
              @geteditaddress="geteditaddress"
            />
          </el-form-item>
          <el-form-item
            label="建物類型："
            class="el-form-item is-required el-form-item--medium"
          >
            <el-select
              v-model="bpropoties"
              placeholder="請選擇"
              clearable
              style="width: 200px"
            >
              <el-option
                v-for="item in bpropotiesOptions"
                :key="item.Id"
                :label="item.BuildingPropotyName"
                :value="item.BuildingPropotyName"
              />
            </el-select>
          </el-form-item>
        </el-form>
      </el-card>
      <div
        slot="footer"
        class="dialog-footer"
      >
        <el-button @click="dialogEditFormVisibleCBB = false">關 閉</el-button>
        <el-button
          type="primary" @click="CreateBindBuilding()"
        >創建並綁定</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import {
  getCustomerLCListWithPager,
  getCustomerLCDetail,
  saveCustomerLC,
  setCustomerLNEnable,
  deleteSoftCustomerLN,
  deleteCustomerLC,
  resetPassword
} from '@/api/chaochi/landlordcomapnyperson/customerlcservice';
import { saveBuilding } from '@/api/chaochi/building/buildingservice';
import { validateCellReg } from '@/utils/validate';
import { getSales } from '@/api/security/userservice';
import * as fecha from 'element-ui/lib/utils/date';
import Address from '@/components/Address/Address.vue';
import LCFormHistory from './LCFormHistory.vue';
import LCRemittance from './LCRemittance.vue';
import { mapGetters } from 'vuex';
import { validateIDReg, validateBusinessIDReg } from '@/utils/validate';

export default {
  name: 'CustomerLC',
  components: { Address, LCFormHistory, LCRemittance },
  data() {
    return {
      bpropoties: '',
      sendedform: {},
      sendedform1: {},
      sendedformAGA: {},
      sendedformAGB: {},
      sendedformCBB: {},
      activeName: 'A',
      searchform: {
        LCName: '',
        LCRep: '',
        LCID: '',
        LCTel: '',
        LCAdd: '',
        CreateTime: ''
      },
      selectRole: [],
      selectedOrganizeOptions: '',
      selectOrganize: [],
      loadBtnFunc: [],
      tableData: [],
      historytableData: [],
      Sales: [],
      zoneOptions: [
        {
          label: '02',
          value: '02'
        },
        {
          label: '03',
          value: '03'
        },
        {
          label: '037',
          value: '037'
        },
        {
          label: '04',
          value: '04'
        },
        {
          label: '049',
          value: '049'
        },
        {
          label: '05',
          value: '05'
        },
        {
          label: '06',
          value: '06'
        },
        {
          label: '07',
          value: '07'
        },
        {
          label: '08',
          value: '08'
        },
        {
          label: '089',
          value: '089'
        },
        {
          label: '082',
          value: '082'
        },
        {
          label: '0826',
          value: '0826'
        },
        {
          label: '0836',
          value: '0836'
        }
      ],
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
      dialogEditFormVisible: false,
      historyFormVisible: false,
      LCdisabled: false,
      editFormTitle: '',
      editFrom: {},
      rules: {
        LCID: [
          {
            required: true,
            trigger: 'blur',
            validator: validateBusinessIDReg
          }
        ],
        LCRep: [{ required: true, message: '請輸入代表人', trigger: 'blur' }],
        LCName: [{ required: true, message: '請輸入名稱', trigger: 'blur' }],
        LCTel_2: [{ required: true, message: '請輸入電話', trigger: 'blur' }],
        LCAGID_A: [
          {
            required: false,
            trigger: 'blur',
            validator: validateIDReg
          }
        ],
        LCAGID_B: [
          {
            required: false,
            trigger: 'blur',
            validator: validateIDReg
          }
        ],
        LCAGCell_A: [
          { required: false, trigger: 'change', validator: validateCellReg }
        ],
        LCAGCell_B: [
          { required: false, trigger: 'change', validator: validateCellReg }
        ]
      },
      formLabelWidth: '165px',
      currentId: '', // 當前操作對象的ID值，主要用于修改
      currentSelected: [],
      LNAddSameCom: false,
      dialogEditFormVisibleCBB: false
    };
  },
  computed: {
    ...mapGetters(['bpropotiesOptions'])
  },
  watch: {
    'editFrom.LCAddSame': {
      handler(a) {
        if (a === '/Yes') {
          this.LNAddSameCom = true;
        } else {
          this.LNAddSameCom = false;
        }
      }
    },
    LNAddSameCom: {
      handler(a) {
        if (a === true) {
          this.editFrom.LCAddSame = '/Yes';
        }
        if (a === false) {
          this.editFrom.LCAddSame = '/Off';
        }
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
    InitDictItem() {
      getSales().then((res) => {
        this.Sales = res.ResData;
        if (this.Sales.length > 0) {
          if (this.Sales[0].RealName === '待指派') {
            this.Sales.shift();
          }
        }
      });
    },
    // 雙擊row
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
     * 加載頁面table數據
     */
    loadTableData: function() {
      this.tableloading = true;
      var t = {
        LCName: this.searchform.LCName,
        LCRep: this.searchform.LCRep,
        LCID: this.searchform.LCID,
        LCTel_1: this.searchform.LCTel_1,
        LCTel_2: this.searchform.LCTel_2,
        LCAdd: this.searchform.LCAdd,
        CreatorUserId: this.searchform.CreatorId
      };
      var searchdata = {
        CurrenetPageIndex: this.pagination.currentPage,
        PageSize: this.pagination.pagesize,
        Filter: t,
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
        RoleId: this.searchform.RoleId
      };
      getCustomerLCListWithPager(searchdata).then((res) => {
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
    // 表單重置
    reset() {
      this.editFrom = {};
      this.selectedOrganizeOptions = '';
      this.resetForm('editFrom');
    },
    ShowCBBDialog() {
      if (
        this.currentSelected.length > 1 ||
        this.currentSelected.length === 0
      ) {
        this.$alert('請選擇一條數據進行', '提示');
      } else {
        this.currentId = this.currentSelected[0].Id;
        this.dialogEditFormVisibleCBB = true;
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
          this.LCdisabled = true;
          this.dialogEditFormVisible = true;
          this.loading = true;
          this.bindEditInfo();
        }
      } else {
        this.editFormTitle = '新增';
        this.LCdisabled = false;
        this.LNAddSameCom = false;
        this.currentId = '';
        this.selectedOrganizeOptions = '';
        this.dialogEditFormVisible = true;
        this.sendedform = {};
        this.sendedform1 = {};
        this.sendedformAGA = {};
        this.sendedformAGB = {};
      }
    },
    bindEditInfo: function() {
      getCustomerLCDetail(this.currentId)
        .then((res) => {
          this.editFrom = res.ResData;
          this.sendedform = {
            Add_1: this.editFrom.LCAdd_1,
            Add_1_1: this.editFrom.LCAdd_1_1,
            Add_1_2: this.editFrom.LCAdd_1_2,
            Add_2: this.editFrom.LCAdd_2,
            Add_2_1: this.editFrom.LCAdd_2_1,
            Add_2_2: this.editFrom.LCAdd_2_2,
            Add_2_3: this.editFrom.LCAdd_2_3,
            Add_2_4: this.editFrom.LCAdd_2_4,
            Add_3: this.editFrom.LCAdd_3,
            Add_3_1: this.editFrom.LCAdd_3_1,
            Add_3_2: this.editFrom.LCAdd_3_2,
            Add_4: this.editFrom.LCAdd_4,
            Add_5: this.editFrom.LCAdd_5,
            Add_6: this.editFrom.LCAdd_6,
            Add_7: this.editFrom.LCAdd_7,
            Add_8: this.editFrom.LCAdd_8,
            Add_9: this.editFrom.LCAdd_9
          };
          this.sendedform1 = {
            Add_1: this.editFrom.LCAddC_1,
            Add_1_1: this.editFrom.LCAddC_1_1,
            Add_1_2: this.editFrom.LCAddC_1_2,
            Add_2: this.editFrom.LCAddC_2,
            Add_2_1: this.editFrom.LCAddC_2_1,
            Add_2_2: this.editFrom.LCAddC_2_2,
            Add_2_3: this.editFrom.LCAddC_2_3,
            Add_2_4: this.editFrom.LCAddC_2_4,
            Add_3: this.editFrom.LCAddC_3,
            Add_3_1: this.editFrom.LCAddC_3_1,
            Add_3_2: this.editFrom.LCAddC_3_2,
            Add_4: this.editFrom.LCAddC_4,
            Add_5: this.editFrom.LCAddC_5,
            Add_6: this.editFrom.LCAddC_6,
            Add_7: this.editFrom.LCAddC_7,
            Add_8: this.editFrom.LCAddC_8,
            Add_9: this.editFrom.LCAddC_9
          };
          this.sendedformAGA = {
            Add_1: this.editFrom.LCAGAdd_1_A,
            Add_1_1: this.editFrom.LCAGAdd_1_1_A,
            Add_1_2: this.editFrom.LCAGAdd_1_2_A,
            Add_2: this.editFrom.LCAGAdd_2_A,
            Add_2_1: this.editFrom.LCAGAdd_2_1_A,
            Add_2_2: this.editFrom.LCAGAdd_2_2_A,
            Add_2_3: this.editFrom.LCAGAdd_2_3_A,
            Add_2_4: this.editFrom.LCAGAdd_2_4_A,
            Add_3: this.editFrom.LCAGAdd_3_A,
            Add_3_1: this.editFrom.LCAGAdd_3_1_A,
            Add_3_2: this.editFrom.LCAGAdd_3_2_A,
            Add_4: this.editFrom.LCAGAdd_4_A,
            Add_5: this.editFrom.LCAGAdd_5_A,
            Add_6: this.editFrom.LCAGAdd_6_A,
            Add_7: this.editFrom.LCAGAdd_7_A,
            Add_8: this.editFrom.LCAGAdd_8_A,
            Add_9: this.editFrom.LCAGAdd_9_A
          };
          this.sendedformAGB = {
            Add_1: this.editFrom.LCAGAdd_1_B,
            Add_1_1: this.editFrom.LCAGAdd_1_1_B,
            Add_1_2: this.editFrom.LCAGAdd_1_2_B,
            Add_2: this.editFrom.LCAGAdd_2_B,
            Add_2_1: this.editFrom.LCAGAdd_2_1_B,
            Add_2_2: this.editFrom.LCAGAdd_2_2_B,
            Add_2_3: this.editFrom.LCAGAdd_2_3_B,
            Add_2_4: this.editFrom.LCAGAdd_2_4_B,
            Add_3: this.editFrom.LCAGAdd_3_B,
            Add_3_1: this.editFrom.LCAGAdd_3_1_B,
            Add_3_2: this.editFrom.LCAGAdd_3_2_B,
            Add_4: this.editFrom.LCAGAdd_4_B,
            Add_5: this.editFrom.LCAGAdd_5_B,
            Add_6: this.editFrom.LCAGAdd_6_B,
            Add_7: this.editFrom.LCAGAdd_7_B,
            Add_8: this.editFrom.LCAGAdd_8_B,
            Add_9: this.editFrom.LCAGAdd_9_B
          };
        })
        .finally(() => {
          this.loading = false;
        });
    },
    /**
     * 新增/修改保存
     */
    saveEditForm() {
      this.$refs['editFrom'].validate((valid) => {
        if (valid) {
          // const data = {
          //   Account: this.editFrom.Account,
          //   RealName: this.editFrom.RealName,
          //   Gender: this.editFrom.Gender,
          //   Birthday: this.editFrom.Birthday,
          //   MobilePhone: this.editFrom.MobilePhone,
          //   Telephone: this.editFrom.Telephone,
          //   Email: this.editFrom.Email,
          //   Line: this.editFrom.Line,
          //   DepartmentId: this.editFrom.DepartmentId,
          //   Id: this.currentId
          // }
          const data = this.editFrom;
          var url = 'CustomerLC/Insert';
          if (this.currentId !== '') {
            url = 'CustomerLC/Update';
          }
          this.loading = true;
          saveCustomerLC(data, url)
            .then((res) => {
              if (res.Success) {
                this.$message({
                  message: '恭喜你，操作成功',
                  type: 'success'
                });
                if (url === 'CustomerLC/Insert') {
                  this.closeDialogAndReset();
                }
                // this.dialogEditFormVisible = false;
                // this.currentSelected = '';
                // this.selectedOrganizeOptions = '';
                // this.loadTableData();
                // this.InitDictItem();
              } else {
                this.$message({
                  message: res.ErrMsg,
                  type: 'error'
                });
              }
            })
            .finally(() => {
              this.loading = false;
            });
        } else {
          return false;
        }
      });
    },
    CreateBindBuilding() {
      const data = this.sendedformCBB;
      data.BPropoty = this.bpropoties;
      var url = 'Building/InsertAs';
      saveBuilding(data, this.currentId, url).then((res) => {
        console.log(res.Success);
        if (res.Success) {
          this.$message.success('成功新增建物!');
          this.dialogEditFormVisibleCBB = false;
          this.currentSelected = '';
          this.sendedformCBB = {};
          this.loadTableData();
        } else {
          this.$message.error('新增失敗!', res.ErrMsg);
        }
      });
    },
    closeDialogAndReset() {
      this.dialogEditFormVisible = false;
      this.activeName = 'A';
      this.currentSelected = '';
      this.selectedOrganizeOptions = '';
      this.loadTableData();
      this.InitDictItem();
      this.editFrom = {};
      this.sendedform = {};
      this.sendedform1 = {};
      this.sendedformAGA = {};
      this.sendedformAGB = {};
      this.sendedformCBB = {};
    },
    setEnable: function(val) {
      if (this.currentSelected.length === 0) {
        this.$alert('請先選擇要操作的數據', '提示');
        return false;
      } else {
        var currentIds = [];
        this.currentSelected.forEach((element) => {
          currentIds.push(element.Id);
        });
        const data = {
          Ids: currentIds,
          Flag: val
        };
        setCustomerLNEnable(data).then((res) => {
          if (res.Success) {
            this.$message({
              message: '恭喜你，操作成功',
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
    deleteSoft: function(val) {
      if (this.currentSelected.length === 0) {
        this.$alert('請先選擇要操作的數據', '提示');
        return false;
      } else {
        var currentIds = [];
        this.currentSelected.forEach((element) => {
          currentIds.push(element.Id);
        });
        const data = {
          Ids: currentIds,
          Flag: val
        };
        deleteSoftCustomerLN(data).then((res) => {
          if (res.Success) {
            this.$message({
              message: '恭喜你，操作成功',
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
            return deleteCustomerLC(data);
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
    },

    /**
     *選擇組織
     */
    handleSelectOrganizeChange: function() {
      this.editFrom.DepartmentId = this.selectedOrganizeOptions;
    },
    dateformatter(row, column, cellValue, index) {
      var date = row[column.property];
      return cellValue ? fecha.format(new Date(date), 'yyyy-MM-dd') : '';
    },
    handleResetPassword: function(val) {
      if (
        this.currentSelected.length > 1 ||
        this.currentSelected.length === 0
      ) {
        this.$alert('請先選擇要操作的數據', '提示');
        return false;
      } else {
        const data = {
          userId: this.currentSelected[0].Id
        };
        resetPassword(data).then((res) => {
          if (res.Success) {
            this.$alert('重置密碼成功，新密為' + res.ErrMsg, '提醒', {
              confirmButtonText: '確定',
              callback: (action) => {}
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
    handleClick(a, b) {
      console.log(a, b);
    },
    geteditaddress(addressData, title) {
      if (title === '公司登記地址') {
        this.editFrom.LCAdd_1 = addressData.Add_1;
        this.editFrom.LCAdd_1_1 = addressData.Add_1_1;
        this.editFrom.LCAdd_1_2 = addressData.Add_1_2;
        this.editFrom.LCAdd_2 = addressData.Add_2;
        this.editFrom.LCAdd_2_1 = addressData.Add_2_1;
        this.editFrom.LCAdd_2_2 = addressData.Add_2_2;
        this.editFrom.LCAdd_2_3 = addressData.Add_2_3;
        this.editFrom.LCAdd_2_4 = addressData.Add_2_4;
        this.editFrom.LCAdd_3 = addressData.Add_3;
        this.editFrom.LCAdd_3_1 = addressData.Add_3_1;
        this.editFrom.LCAdd_3_2 = addressData.Add_3_2;
        this.editFrom.LCAdd_4 = addressData.Add_4;
        this.editFrom.LCAdd_5 = addressData.Add_5;
        this.editFrom.LCAdd_6 = addressData.Add_6;
        this.editFrom.LCAdd_7 = addressData.Add_7;
        this.editFrom.LCAdd_8 = addressData.Add_8;
        this.editFrom.LCAdd_9 = addressData.Add_9;
      }
      if (title === '公司營業地址') {
        this.editFrom.LCAddC_1 = addressData.Add_1;
        this.editFrom.LCAddC_1_1 = addressData.Add_1_1;
        this.editFrom.LCAddC_1_2 = addressData.Add_1_2;
        this.editFrom.LCAddC_2 = addressData.Add_2;
        this.editFrom.LCAddC_2_1 = addressData.Add_2_1;
        this.editFrom.LCAddC_2_2 = addressData.Add_2_2;
        this.editFrom.LCAddC_2_3 = addressData.Add_2_3;
        this.editFrom.LCAddC_2_4 = addressData.Add_2_4;
        this.editFrom.LCAddC_3 = addressData.Add_3;
        this.editFrom.LCAddC_3_1 = addressData.Add_3_1;
        this.editFrom.LCAddC_3_2 = addressData.Add_3_2;
        this.editFrom.LCAddC_4 = addressData.Add_4;
        this.editFrom.LCAddC_5 = addressData.Add_5;
        this.editFrom.LCAddC_6 = addressData.Add_6;
        this.editFrom.LCAddC_7 = addressData.Add_7;
        this.editFrom.LCAddC_8 = addressData.Add_8;
        this.editFrom.LCAddC_9 = addressData.Add_9;
      }
      if (title === '代A地址') {
        this.editFrom.LCAGAdd_1_A = addressData.Add_1;
        this.editFrom.LCAGAdd_1_1_A = addressData.Add_1_1;
        this.editFrom.LCAGAdd_1_2_A = addressData.Add_1_2;
        this.editFrom.LCAGAdd_2_A = addressData.Add_2;
        this.editFrom.LCAGAdd_2_1_A = addressData.Add_2_1;
        this.editFrom.LCAGAdd_2_2_A = addressData.Add_2_2;
        this.editFrom.LCAGAdd_2_3_A = addressData.Add_2_3;
        this.editFrom.LCAGAdd_2_4_A = addressData.Add_2_4;
        this.editFrom.LCAGAdd_3_A = addressData.Add_3;
        this.editFrom.LCAGAdd_3_1_A = addressData.Add_3_1;
        this.editFrom.LCAGAdd_3_2_A = addressData.Add_3_2;
        this.editFrom.LCAGAdd_4_A = addressData.Add_4;
        this.editFrom.LCAGAdd_5_A = addressData.Add_5;
        this.editFrom.LCAGAdd_6_A = addressData.Add_6;
        this.editFrom.LCAGAdd_7_A = addressData.Add_7;
        this.editFrom.LCAGAdd_8_A = addressData.Add_8;
        this.editFrom.LCAGAdd_9_A = addressData.Add_9;
      }
      if (title === '代B地址') {
        this.editFrom.LCAGAdd_1_B = addressData.Add_1;
        this.editFrom.LCAGAdd_1_1_B = addressData.Add_1_1;
        this.editFrom.LCAGAdd_1_2_B = addressData.Add_1_2;
        this.editFrom.LCAGAdd_2_B = addressData.Add_2;
        this.editFrom.LCAGAdd_2_1_B = addressData.Add_2_1;
        this.editFrom.LCAGAdd_2_2_B = addressData.Add_2_2;
        this.editFrom.LCAGAdd_2_3_B = addressData.Add_2_3;
        this.editFrom.LCAGAdd_2_4_B = addressData.Add_2_4;
        this.editFrom.LCAGAdd_3_B = addressData.Add_3;
        this.editFrom.LCAGAdd_3_1_B = addressData.Add_3_1;
        this.editFrom.LCAGAdd_3_2_B = addressData.Add_3_2;
        this.editFrom.LCAGAdd_4_B = addressData.Add_4;
        this.editFrom.LCAGAdd_5_B = addressData.Add_5;
        this.editFrom.LCAGAdd_6_B = addressData.Add_6;
        this.editFrom.LCAGAdd_7_B = addressData.Add_7;
        this.editFrom.LCAGAdd_8_B = addressData.Add_8;
        this.editFrom.LCAGAdd_9_B = addressData.Add_9;
      }
      if (title === '新建地址') {
        this.sendedformCBB.BAdd_1 = addressData.Add_1;
        this.sendedformCBB.BAdd_1_1 = addressData.Add_1_1;
        this.sendedformCBB.BAdd_1_2 = addressData.Add_1_2;
        this.sendedformCBB.BAdd_2 = addressData.Add_2;
        this.sendedformCBB.BAdd_2_1 = addressData.Add_2_1;
        this.sendedformCBB.BAdd_2_2 = addressData.Add_2_2;
        this.sendedformCBB.BAdd_2_3 = addressData.Add_2_3;
        this.sendedformCBB.BAdd_2_4 = addressData.Add_2_4;
        this.sendedformCBB.BAdd_3 = addressData.Add_3;
        this.sendedformCBB.BAdd_3_1 = addressData.Add_3_1;
        this.sendedformCBB.BAdd_3_2 = addressData.Add_3_2;
        this.sendedformCBB.BAdd_4 = addressData.Add_4;
        this.sendedformCBB.BAdd_5 = addressData.Add_5;
        this.sendedformCBB.BAdd_6 = addressData.Add_6;
        this.sendedformCBB.BAdd_7 = addressData.Add_7;
        this.sendedformCBB.BAdd_8 = addressData.Add_8;
        this.sendedformCBB.BAdd_9 = addressData.Add_9;
      }
    }
  }
};
</script>
