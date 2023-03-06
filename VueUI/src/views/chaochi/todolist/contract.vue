<template>
  <el-card class="box-card">
    <el-tabs
      v-model="TabAlias"
      type="border-card"
    >
      <el-tab-pane
        label="基本資料"
        name="CT1"
      >
        <el-card class="box-card">
          <el-form
            ref="form"
            :inline="true" :model="form" class="demo-form-inline"
          >
            <el-row>
              <el-col :span="24">
                <el-form-item
                  label="契約類別"
                  :label-width="contractLabelWidth" prop="CCateFullPath"
                >
                  <el-input
                    v-model="categoryFullPath"
                    style="width:650px" readonly
                  />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="24">
                <el-form-item
                  label="分租物件單號"
                  :label-width="contractLabelWidth" prop="MOID"
                >
                  <el-input
                    v-model="form.MOID"
                    style="width:400px" readonly
                  />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="24">
                <el-form-item
                  label="分租物件名稱"
                  :label-width="contractLabelWidth" prop="MOName"
                >
                  <el-input
                    v-model="form.MOName"
                    style="width:400px" readonly
                  />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="24">
                <el-form-item
                  label="合約編號"
                  :label-width="contractLabelWidth" prop="CID"
                >
                  <el-input
                    v-model="form.CID"
                    style="width:400px" readonly
                  />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="24">
                <el-form-item
                  label="負責業務"
                  :label-width="contractLabelWidth" prop="CID"
                >
                  <el-input
                    v-model="form.SalesName"
                    style="width:400px" readonly
                  />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="12">
                <el-form-item
                  v-if="contractType1 || contractType3"
                  label="出租人姓名/法人名稱"
                  :label-width="contractLabelWidth"
                  prop="LSName"
                >
                  <el-input
                    v-model="form.LSInfo.LSName"
                    style="width:250px" readonly
                  />
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item
                  v-if="contractType1 || contractType3"
                  label="出租人身份證號/統一編號"
                  :label-width="contractLabelWidth"
                  prop="LSID"
                >
                  <el-input
                    v-model="form.LSInfo.LSID"
                    style="width:250px" readonly
                  />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="12">
                <el-form-item
                  label="二房東名稱"
                  :label-width="contractLabelWidth" prop="SLName"
                >
                  <el-input
                    v-model="form.SLName"
                    style="width:400px" readonly
                  />
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item
                  label="二房東統一編號"
                  :label-width="contractLabelWidth" prop="SLID"
                >
                  <el-input
                    v-model="form.SLID"
                    style="width:250px" readonly
                  />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="12">
                <el-form-item
                  label="管理方名稱"
                  :label-width="contractLabelWidth" prop="MName"
                >
                  <el-input
                    v-model="form.MName"
                    style="width:400px" readonly
                  />
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item
                  label="管理方統一編號"
                  :label-width="contractLabelWidth" prop="MAID"
                >
                  <el-input
                    v-model="form.MAID"
                    style="width:250px" readonly
                  />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="12">
                <el-form-item
                  v-if="contractType2 || contractType3"
                  label="承租人姓名/法人名稱"
                  :label-width="contractLabelWidth"
                  prop="RName"
                >
                  <el-input
                    v-model="form.RNInfo.RNName"
                    style="width:250px" readonly
                  />
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item
                  v-if="contractType2 || contractType3"
                  label="承租人身份證號/統一編號"
                  :label-width="contractLabelWidth"
                  prop="RNID"
                >
                  <el-input
                    v-model="form.RNInfo.RNID"
                    style="width:250px" readonly
                  />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="24">
                <el-form-item
                  label="建物門牌地址"
                  :label-width="contractLabelWidth" prop="BAdd"
                >
                  <el-input
                    v-model="form.BuildingInfo.BAdd"
                    style="width:800px" readonly
                  />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="24">
                <el-form-item
                  label="合約狀態"
                  :label-width="contractLabelWidth" prop="CStatus"
                >
                  <el-input
                    v-model="form.CStatus"
                    readonly
                  />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="12">
                <el-form-item
                  label="社宅物件編號"
                  :label-width="contractLabelWidth" prop="ObjectNo"
                >
                  <el-input
                    v-model="form.ObjectNo"
                    readonly
                  />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="12">
                <el-form-item
                  label="兆基物件編號"
                  :label-width="contractLabelWidth" prop="CCObjectNo"
                >
                  <el-input
                    v-model="form.CCObjectNo"
                    readonly
                  />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="24">
                <el-form-item
                  label="媒合編號"
                  :label-width="contractLabelWidth" prop="MID"
                >
                  <el-input
                    v-model="form.MID"
                    readonly
                  />
                </el-form-item>
              </el-col>
            </el-row>
          </el-form>
        </el-card>
      </el-tab-pane>
      <el-tab-pane
        label="合約內容"
        name="CT2"
      >
        <el-card class="box-card">
          <el-form
            ref="form"
            :inline="true" :model="form" class="demo-form-inline"
          >
            <el-row>
              <el-col :span="12">
                <el-form-item
                  label="簽約日"
                  :label-width="contractLabelWidth" prop="ContractDate"
                >
                  <el-input
                    v-model="form.ContractDate"
                    readonly
                  />
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item
                  label="是否同意公證:"
                  :label-width="contractLabelWidth" prop="notary"
                >
                  <el-input
                    v-model="form.BZOutputDto.BZ059060"
                    readonly
                  />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="12">
                <el-form-item
                  label="租賃起日"
                  :label-width="contractLabelWidth" prop="BRDStart"
                >
                  <el-input
                    v-model="form.BuildingInfo.BRDStart"
                    readonly
                  />
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item
                  label="租賃迄日"
                  :label-width="contractLabelWidth" prop="BRDTEnd"
                >
                  <el-input
                    v-model="form.BuildingInfo.BRDTEnd"
                    readonly
                  />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="12">
                <el-form-item
                  label="每月租金"
                  :label-width="contractLabelWidth" prop="BTCRent"
                >
                  <el-input
                    v-model="form.BuildingInfo.BTCRent"
                    readonly
                  />
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item
                  label="管理費"
                  :label-width="contractLabelWidth" prop="BZ029"
                >
                  <el-input
                    v-model="form.BuildingInfo.BMFee"
                    readonly
                  />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="12">
                <el-form-item
                  label="合計"
                  :label-width="contractLabelWidth" prop="BZ009"
                >
                  <el-input
                    v-model="form.BZOutputDto.BZ009"
                    readonly
                  />
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item
                  label="每月"
                  :label-width="contractLabelWidth" prop="BTRent_Day"
                >
                  <el-input
                    v-model="form.BuildingInfo.BTRent_Day"
                    readonly
                  />
                </el-form-item>
                日前支付
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="24">
                <el-form-item
                  label="租金支付方式"
                  :label-width="contractLabelWidth" prop="PaymentMethod"
                >
                  <el-input
                    v-model="form.PaymentMethod"
                    style="width:500px" readonly
                  />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="12">
                <el-form-item
                  label="銀行"
                  :label-width="contractLabelWidth" prop="BankName"
                >
                  <el-input
                    v-model="form.BankName"
                    readonly
                  />
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item
                  label="銀行代號"
                  :label-width="contractLabelWidth" prop="BankNo"
                >
                  <el-input
                    v-model="form.BankNo"
                    readonly
                  />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="12">
                <el-form-item
                  label="分行"
                  :label-width="contractLabelWidth" prop="BranchName"
                >
                  <el-input
                    v-model="form.BranchName"
                    readonly
                  />
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item
                  label="分行代號"
                  :label-width="contractLabelWidth" prop="BranchNo"
                >
                  <el-input
                    v-model="form.BranchNo"
                    readonly
                  />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="12">
                <el-form-item
                  label="戶名"
                  :label-width="contractLabelWidth" prop="AccountName"
                >
                  <el-input
                    v-model="form.AccountName"
                    style="width:400px" readonly
                  />
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item
                  label="帳號"
                  :label-width="contractLabelWidth" prop="AccountNo"
                >
                  <el-input
                    v-model="form.AccountNo"
                    readonly
                  />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="12">
                <el-form-item
                  label="車位"
                  :label-width="contractLabelWidth" prop="BCar"
                >
                  <el-input
                    v-model="bcar"
                    readonly
                  />
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item
                  label="車位編號"
                  :label-width="contractLabelWidth" prop="BCarNo"
                >
                  <el-input
                    v-model="form.BuildingInfo.BCarNo"
                    readonly
                  />
                </el-form-item>
              </el-col>
              <!-- <el-col :span="12">
                          <el-form-item
                            label="租賃附屬設備"
                            :label-width="contractLabelWidth"
                            prop="BZ006007"
                          >
                            <el-input
                              v-model="editFrom.BZOutputDto.BZ006007"
                              readonly
                            />
                          </el-form-item>
                        </el-col> -->
            </el-row>
            <el-row>
              <el-col :span="12">
                <el-form-item
                  label="車位管理費 每月另付:"
                  :label-width="contractLabelWidth" prop="BZ030"
                >
                  <el-input
                    v-model="form.BuildingInfo.BParkFee"
                    readonly
                  />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="12">
                <el-form-item
                  label="押金為"
                  :label-width="contractLabelWidth" prop="BDMon"
                >
                  <el-input
                    v-model="form.BuildingInfo.BDMon"
                    readonly
                  />
                </el-form-item>個月租金
              </el-col>
              <el-col :span="12">
                <el-form-item
                  label="押金金額"
                  :label-width="contractLabelWidth" prop="Bdeposit"
                >
                  <el-input
                    v-model="form.BuildingInfo.Bdeposit"
                    readonly
                  />
                </el-form-item>
              </el-col>
            </el-row>
          </el-form>
        </el-card>
      </el-tab-pane>
      <el-tab-pane
        label="附件綁定"
        name="CT3"
      >
        <el-card class="box-card">
          <el-form
            ref="form"
            :inline="true" :model="form" class="demo-form-inline"
          >
            <el-row>
              <el-col :span="12">
                <el-table
                  ref="majorAttachmentTable"
                  v-loading="majorAttachmentTableloading"
                  :data="form.MajorAttachmentList"
                  stripe
                  highlight-current-row
                  style="width: 100%"
                  :default-sort="{prop: 'SortCode', order: 'ascending'}"
                  :header-cell-style="{
                    'background-color': '#728FCE',
                    'color': '#fff',
                    'font-weight': '400'
                  }"
                >
                  <el-table-column
                    prop="FormName"
                    label="要件表單名稱" sortable="custom" width="160"
                  />
                  <el-table-column
                    prop="ArchiveTo"
                    label="表單歸屬" sortable="custom" width="120"
                  />
                  <el-table-column
                    prop="Status"
                    label="附件狀況" sortable="custom" width="120"
                  />
                  <el-table-column
                    prop="UploadTime"
                    label="綁定日期" sortable="custom" width="120"
                  />
                  <el-table-column
                    prop="UploadUserId"
                    label="異動人員" sortable="custom" width="120"
                  />
                </el-table>
              </el-col>
              <el-col :span="12">
                <el-table
                  ref="minorAttachmentTable"
                  v-loading="minorAttachmentTableloading"
                  :data="form.MinorAttachmentList"
                  stripe
                  highlight-current-row
                  style="width: 100%"
                  :default-sort="{prop: 'SortCode', order: 'ascending'}"
                  :header-cell-style="{
                    'background-color': '#728FCE',
                    'color': '#fff',
                    'font-weight': '400'
                  }"
                >
                  <el-table-column
                    prop="FormName"
                    label="附件名稱" sortable="custom" width="120"
                  />
                  <el-table-column
                    prop="UploadTime"
                    label="上傳日期" sortable="custom" width="120"
                  />
                  <el-table-column
                    prop="UploadUserId"
                    label="異動人員" sortable="custom" width="120"
                  />
                </el-table>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="24">&nbsp;</el-col>
            </el-row>
          </el-form>
        </el-card>
      </el-tab-pane>
      <el-tab-pane
        label="歷史版本"
        name="CT4"
      >
        <el-card class="box-card">
          <el-form
            ref="form"
            :inline="true" :model="form" class="demo-form-inline"
          >
            <el-table
              ref="contractHistoryTable"
              v-loading="contractHistoryTableloading"
              :data="form.ContractHistoryList"
              stripe
              highlight-current-row
              style="width: 100%"
              :default-sort="{prop: 'SortCode', order: 'ascending'}"
              :header-cell-style="{
                'background-color': '#728FCE',
                'color': '#fff',
                'font-weight': '400'
              }"
            >
              <el-table-column
                prop="Version"
                label="版號" sortable="custom" width="120"
              />
              <el-table-column
                prop="CName"
                label="合約名稱" sortable="custom" width="120"
              >
                <template slot-scope="scope">
                  <a
                    href="#"
                    style="text-decoration: underline;"
                    @click="downloadFinalizedPDF(scope.row.Version, scope.row.CName)"
                  >{{
                    scope.row.CName }}</a>
                </template>
              </el-table-column>
              <el-table-column
                prop="Status"
                label="簽約掃描檔" sortable="custom" width="120"
              >
                <template slot-scope="scope">
                  <a
                    v-if="scope.row.Status === '已存在'"
                    href="#"
                    style="text-decoration: underline;"
                    @click="downloadSignedContract(scope.row.Version, scope.row.CName)"
                  >{{
                    scope.row.Status }}</a>
                  <span v-else>{{ scope.row.Status }}</span>

                </template>
              </el-table-column>
              <el-table-column
                prop="UploadTime"
                label="產生日期" sortable="custom" width="120"
              />
              <el-table-column
                prop="Note"
                label="備註" sortable="custom" width="120"
              />
              <el-table-column
                prop="UploadUser"
                label="異動人員" sortable="custom" width="120"
              />
            </el-table>
            <el-row>&nbsp;</el-row>
          </el-form>
        </el-card>
      </el-tab-pane>
      <el-tab-pane
        label="審核記錄"
        name="CT6"
      >
        <el-card class="box-card">
          <el-table
            ref="contractFlowLogTable"
            v-loading="contractFlowLogTableloading"
            :data="form.ContractFlowLogList"
            stripe
            highlight-current-row
            style="width: 100%"
            :default-sort="{prop: 'ApplyTime', order: 'ascending'}"
            :header-cell-style="{
              'background-color': '#728FCE',
              'color': '#fff',
              'font-weight': '400'
            }"
          >
            <el-table-column
              prop="CID"
              label="審核單號" sortable="custom" width="200"
            />
            <el-table-column
              prop="Applicant"
              label="當關者" sortable="custom" width="150"
            />
            <el-table-column
              prop="Auditor"
              label="下一關" sortable="custom" width="150"
            />
            <el-table-column
              prop="Action"
              label="同意/退回" sortable="custom" width="150"
            />
            <el-table-column
              prop="Comment"
              label="審核意見" sortable="custom" width="150"
            />
            <el-table-column
              prop="ApplyTime"
              label="審核時間" sortable="custom" width="150"
            />
          </el-table>
        </el-card>
      </el-tab-pane>
    </el-tabs>
    <div class="flowSubmit">
      <el-form
        ref="flowSubmitForm"
        :model="flowSubmitForm" :rules="flowSubmitRules"
      >
        <el-form-item
          label="動作"
          prop="Action"
        >
          <el-select
            v-model="flowSubmitForm.Action"
            placeholder="請選擇"
          >
            <el-option
              v-for="item in action"
              :key="item.value" :label="item.label" :value="item.value"
            />
          </el-select>
        </el-form-item>
        <el-form-item
          label="審核意見"
          prop="Memo"
        >
          <el-input
            v-model="flowSubmitForm.Memo"
            type="textarea" :rows="4" style="width: 500px"
          />
        </el-form-item>
      </el-form>
    </div>
    <div class="flowSubmit">
      <el-button
        size="small"
        icon="el-icon-close" @click="cancel"
      >關閉</el-button>
      <el-button
        v-preventReClick
        size="small" type="primary" icon="el-icon-paperclip" @click="flowSubmit"
      >送出</el-button>
    </div>
  </el-card>
</template>

<script>

// import { updateContractStatus } from '@/api/chaochi/contract/contractservice'
import {
  downloadFinalizedPDF,
  downloadSignedContract
} from '@/api/chaochi/contract/contractservice'
import { getCategoryDetail } from '@/api/chaochi/category/categorycontractservice'
import { flowSubmit } from '@/api/chaochi/todolist/todolistservice'
import { mapGetters } from 'vuex';
export default {
  name: 'ToDoListContract', // 需與菜單的功能編碼一致
  props: {
    info: { type: Object, default: null },
    todoListItemId: { type: String, default: '' }
  },
  data() {
    return {
      categoryFullPath: '',
      BID: '',
      TabAlias: 'CT1',
      form: {
        BZOutputDto: {
          BZ029: '',
          BZ009: '',
          // BZ111213: '',
          BZ006007: '',
          BZ059060: '',
          BZ030: ''
        }
      },
      action: [
        { value: '同意', label: '同意' },
        { value: '退回', label: '退回' }
      ],
      flowSubmitRules: {
        Action: [
          { required: true, trigger: 'change', message: '請選擇動作' }
        ],
        Memo: [
          { required: true, message: '請輸入審核意見', trigger: 'blur' }
        ]

      },
      flowSubmitForm: {},
      formLabelWidth: '140px',
      dialogLabelWidth: '110px',
      contractLabelWidth: '180px',
      majorAttachmentTableloading: true,
      minorAttachmentTableloading: true,
      contractHistoryTableloading: true,
      contractFlowLogTableloading: true,
      currentId: '',
      typeId: '',
      typeData: ''
    }
  },
  computed: {
    bcar() {
      if (this.form.BuildingInfo.BCar_Y) {
        if (this.form.BuildingInfo.BCar_Y === '/Yes') {
          return '有';
        } else if (this.form.BuildingInfo.BCar_Y === '/Off') {
          return '無';
        } else {
          return '無';
        }
      } else {
        return '無';
      }
    },
    contractType1() {
      let type = ''
      if (this.form.CType) {
        type = this.form.CType.trim()
      }
      return type === '1'
    },
    contractType2() {
      let type = ''
      if (this.form.CType) {
        type = this.form.CType.trim()
      }
      return type === '2'
    },
    contractType3() {
      let type = ''
      if (this.form.CType) {
        type = this.form.CType.trim()
      }
      return type === '3'
    },
    visitedViews() {
      return this.$store.state.tagsView.visitedViews
    },
    ...mapGetters(['userId'])
  },
  created() {
    this.loaddata();
  },
  methods: {
    isActive(route) {
      return route.path === this.$route.path
    },
    toLastView(visitedViews, view) {
      const latestView = visitedViews.slice(-1)[0]
      if (latestView) {
        this.$router.push(latestView.fullPath)
      } else {
        // now the default is to redirect to the home page if there is no tags-view,
        // you can adjust it according to your needs.
        if (view.name === 'Dashboard') {
          // to reload home page
          this.$router.replace({ path: '/redirect' + view.fullPath })
        } else {
          this.$router.push('/')
        }
      }
    },
    loaddata: function() {
      this.majorAttachmentTableloading = true
      this.minorAttachmentTableloading = true
      this.contractHistoryTableloading = true
      this.contractFlowLogTableloading = true
      this.form = this.$route.params.info
      this.currentId = this.$route.params.todoListItemId
      this.typeId = this.$route.params.typeId
      this.typeData = this.$route.params.typeData

      if (this.form.CCate) {
        getCategoryDetail(this.form.CCate).then(res => {
          const category = res.ResData
          this.categoryFullPath = category.ParentId + '/' + category.Name
        })
      }
      this.majorAttachmentTableloading = false
      this.minorAttachmentTableloading = false
      this.contractHistoryTableloading = false
      this.contractFlowLogTableloading = false
    },
    downloadFinalizedPDF(version, fileName) {
      // console.log(row.CName)
      const data = {
        'CID': this.form.CID,
        'Version': version,
        'CCate': this.form.CCate,
        'CType': this.form.CType
      }

      downloadFinalizedPDF(data).then(res => {
        if (res == null) {
          this.$alert('下載失敗', '下載失敗', {
            confirmButtonText: '確定'
          })
          return
        }
        if (res.type !== 'application/pdf') {
          // console.log(res.type)
          var reader = new FileReader()
          reader.onload = e => {
            const msg = JSON.parse(e.target.result)
            // console.log(msg)
            // this.$message({
            //   message: '下載失敗, 請確認空白PDF(' + label + ')已上傳 ' + msg,
            //   type: 'error'
            // })
            this.$alert('請確認合約(' + fileName + ')是否已定稿 ' + msg.ErrMsg, '下載失敗', {
              confirmButtonText: '確定'
            })
          }
          reader.readAsText(res)
          return
        }

        const blob = res// new Blob([response.data], { type: 'image/jpeg' })
        const link = document.createElement('a')
        link.href = URL.createObjectURL(blob)
        link.download = fileName.replace('.', '_')
        link.click()
        URL.revokeObjectURL(link.href)
      }).catch(error => {
        console.log(error)
      })
    },
    downloadSignedContract(version, fileName) {
      // console.log(row.CName)
      const data = {
        'CID': this.form.CID,
        'Version': version,
        'CCate': this.form.CCate,
        'CType': this.form.CType
      }

      downloadSignedContract(data).then(res => {
        if (res == null) {
          this.$alert('下載失敗', '下載失敗', {
            confirmButtonText: '確定'
          })
          return
        }
        if (res.type !== 'application/pdf') {
          // console.log(res.type)
          var reader = new FileReader()
          reader.onload = e => {
            const msg = JSON.parse(e.target.result)
            // console.log(msg)
            // this.$message({
            //   message: '下載失敗, 請確認空白PDF(' + label + ')已上傳 ' + msg,
            //   type: 'error'
            // })
            this.$alert('請確認合約(' + fileName + ')是否已定稿 ' + msg.ErrMsg, '下載失敗', {
              confirmButtonText: '確定'
            })
          }
          reader.readAsText(res)
          return
        }
        const blob = res// new Blob([response.data], { type: 'image/jpeg' })
        const link = document.createElement('a')
        link.href = URL.createObjectURL(blob)
        link.download = fileName.replace('.', '_') + '_' + version
        link.click()
        URL.revokeObjectURL(link.href)
      }).catch(error => {
        console.log(error)
      })
    },
    // 取消按鈕
    cancel() {
      this.$refs['flowSubmitForm'].resetFields()
      const view = this.visitedViews.slice(-1)[0]
      if (view) {
        this.$store.dispatch('tagsView/delView', view).then(({ visitedViews }) => {
          if (this.isActive(view)) {
            this.toLastView(visitedViews, view)
          }
        })
      }
    },
    flowSubmit() {
      this.$refs['flowSubmitForm'].validate((valid) => {
        if (valid) {
          const data = {
            'Id': this.currentId,
            'UserId': this.userId,
            'TypeId': this.typeId,
            'TypeData': this.typeData,
            'Action': this.flowSubmitForm.Action,
            'Memo': this.flowSubmitForm.Memo
          }
          flowSubmit(data).then(res => {
            if (res.Success) {
              this.$message({
                message: '已審核',
                type: 'success'
              })
              this.cancel()
            } else {
              this.$message({
                message: res.ErrMsg,
                type: 'error'
              })
            }
          })
        } else {
          return false;
        }
      })
    }
    // disapproval() {
    //   const data = {
    //     'UserId': this.userId,
    //     'Id': this.currentId
    //   }
    //   disapproval(data).then(res => {
    //     if (res.Success) {
    //       this.$message({
    //         message: '已駁回',
    //         type: 'success'
    //       })
    //       this.cancel()
    //     } else {
    //       this.$message({
    //         message: res.ErrMsg,
    //         type: 'error'
    //       })
    //     }
    //   })
    // }
  }
}
</script>

<style>
.flowSubmit {
  display: flex;
  justify-content: flex-start;
  padding-top: 15px;
}
</style>
