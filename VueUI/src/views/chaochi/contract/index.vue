<template>
  <div class="app-container">
    <div class="filter-container">
      <el-card>
        <el-form
          ref="searchform"
          :inline="true" :model="searchform" class="demo-form-inline" size="small"
        >
          <el-form-item label="出租人姓名/法人名稱：">
            <el-input
              v-model="searchform.LSName"
              clearable placeholder="出租人姓名/名稱"
            />
          </el-form-item>
          <el-form-item label="出租人身份證字號/統一編號：">
            <el-input
              v-model="searchform.LSID"
              clearable placeholder="出租人身份證字號/統一編號"
            />
          </el-form-item>
          <el-form-item label="承租人姓名/法人名稱：">
            <el-input
              v-model="searchform.RNName"
              clearable placeholder="承租人姓名/名稱"
            />
          </el-form-item>
          <el-form-item label="承租人身份證字號/統一編號：">
            <el-input
              v-model="searchform.RNID"
              clearable placeholder="承租人身份證字號/統一編號"
            />
          </el-form-item>
          <br>
          <el-form-item
            label="二房東："
            prop="SecondLord"
          >
            <el-select
              v-model="searchform.SLID"
              placeholder="請選擇" clearable
            >
              <el-option
                v-for="item in secondLandlord"
                :key="item.SLID" :label="item.SLName" :value="item.SLID"
              />
            </el-select>
          </el-form-item>
          <el-form-item
            label="所屬業務："
            prop="Sales"
          >
            <el-select
              v-model="searchform.Sales"
              placeholder="請選擇" clearable
            >
              <el-option
                v-for="item in sales"
                :key="item.Account" :label="item.RealName" :value="item.Account"
              />
            </el-select>
          </el-form-item>
          <el-form-item
            label="管理方："
            prop="Management"
          >
            <el-select
              v-model="searchform.MAID"
              placeholder="請選擇" clearable
            >
              <el-option
                v-for="item in management"
                :key="item.MAID" :label="item.MName" :value="item.MAID"
              />
            </el-select>
          </el-form-item>
          <el-form-item label="多物件單編號：">
            <el-input
              v-model="searchform.MOID"
              clearable placeholder="多物件單編號"
            />
          </el-form-item>
          <br>
          <el-row>
            <el-form-item label="建物門牌地址：">
              <el-input
                v-model="searchform.BAdd"
                style="width:600px" clearable
              />
            </el-form-item>
          </el-row>
          <!-- <Address title="建物門牌地址" @getsearchaddress="getsearchaddress" /> -->
          <br>
          <el-form-item label="合約編號：">
            <el-input
              v-model="searchform.CID"
              clearable placeholder="合約編號"
            />
          </el-form-item>
          <el-form-item label="媒合編號：">
            <el-input
              v-model="searchform.MID"
              clearable placeholder="媒合編號"
            />
          </el-form-item>
          <el-form-item
            label="合約狀態："
            prop="Status"
          >
            <el-select
              v-model="searchform.CStatus"
              placeholder="請選擇" clearable
            >
              <el-option
                v-for="item in status"
                :key="item.value" :label="item.label" :value="item.value"
              />
            </el-select>
          </el-form-item>
          <el-form-item>
            <el-button
              type="primary"
              icon="el-icon-search" @click="handleSearch()"
            >查詢</el-button>
          </el-form-item>
          <br>
        </el-form>
      </el-card>
    </div>
    <el-card>
      <div class="list-btn-container">
        <el-button-group>
          <el-button
            v-hasPermi="['Contract/Add']"
            type="primary"
            icon="el-icon-plus"
            size="small"
            @click="ShowEditOrViewDialog()"
          >新增</el-button>
          <el-button
            v-hasPermi="['Contract/Edit']"
            type="primary"
            icon="el-icon-edit"
            class="el-button-modify"
            size="small"
            @click="ShowEditOrViewDialog('CT1')"
          >修改</el-button>
          <el-button
            v-hasPermi="['Contract/Delete']"
            type="danger"
            icon="el-icon-delete"
            size="small"
            @click="deletePhysics()"
          >刪除</el-button>
          <el-button
            v-hasPermi="['Contract/Invalid']"
            type="danger"
            icon="el-icon-close"
            class="el-button-modify"
            size="small"
            @click="contractInvalid()"
          >合約作廢</el-button>
          <el-button
            v-hasPermi="['Contract/Terminate']"
            type="danger"
            icon="el-icon-close"
            class="el-button-modify"
            size="small"
            @click="terminateContract()"
          >合約解約</el-button>
          <el-button
            type="default"
            icon="el-icon-refresh" size="small" @click="loadTableData()"
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
          width="30"
        />
        <el-table-column
          prop="CID"
          label="合約編號" sortable="custom" width="120"
        />
        <el-table-column
          prop="Version"
          label="合約版號" sortable="custom" width="120" align="center"
        />
        <el-table-column
          prop="LSName"
          label="出租人-姓名/名稱" sortable="custom" width="160"
        />
        <el-table-column
          prop="SLName"
          label="二房東名稱" sortable="custom" width="120"
        />
        <el-table-column
          prop="RNName"
          label="承租人-姓名/名稱" sortable="custom" width="160"
        />
        <el-table-column
          prop="MName"
          label="管理方名稱" sortable="custom" width="120"
        />
        <el-table-column
          prop="MOID"
          label="多物件" sortable="custom" width="120"
        />
        <el-table-column
          prop="BAdd"
          label="建物門牌地址" sortable="custom" width="150"
        />
        <el-table-column
          prop="BRDStart"
          label="合約起日" sortable="custom" width="120"
        />
        <el-table-column
          prop="BRDTEnd"
          label="合約迄日" sortable="custom" width="120"
        />
        <el-table-column
          prop="BTCRent"
          label="租金" sortable="custom" width="120" :formatter="currencyFormatter"
        />
        <el-table-column
          prop="SalesName"
          label="所屬業務" sortable="custom" width="120"
        />
        <el-table-column
          prop="CStatus"
          label="合約狀態" sortable="custom" width="120"
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
      title="新增合約" :visible.sync="dialogEditFormVisible" fullscreen
    >
      <el-form
        ref="editFrom"
        :model="editFrom" :label-width="formLabelWidth" :rules="rules"
      >
        <!-- <el-form-item label="指定負責業務" prop="Sales">
          <el-select v-model="editFrom.Sales" placeholder="請選擇" @change="getSalesName">
            <el-option v-for="item in sales2" :key="item.Account" :label="item.RealName" :value="item.Account" />
          </el-select>
        </el-form-item> -->
        <el-form-item
          label="選擇契約類別"
          :label-width="formLabelWidth" prop="CCate"
        >
          <el-cascader
            ref="categoryPath"
            v-model="editFrom.CCate"
            style="width:500px;"
            :options="selectCategory"
            filterable
            :props="{label:'Name',value:'Id',children:'Children',emitPath:false, checkStrictly: true,expandTrigger: 'hover' }"
            clearable
            @change="handleSelectCategoryChange"
          />
        </el-form-item>
        <el-form-item
          label="合約編號"
          :label-width="formLabelWidth" prop="CID"
        >
          <el-input
            v-model="editFrom.CID"
            style="width:500px" readonly
          />
        </el-form-item>
        <el-form-item
          label="租賃起日："
          prop="BRDStart"
        >
          <DatePickerE
            v-model="editFrom.BRDStart"
            value-format="yyyy-MM-dd"
            format="yyyy-MM-dd"
            type="date"
            placeholder="選擇租賃起日"
            style="width: 200px"
          />
        </el-form-item>

        <el-form-item
          label="租賃迄日："
          prop="BRDTEnd"
        >
          <DatePickerE
            v-model="editFrom.BRDTEnd"
            value-format="yyyy-MM-dd"
            format="yyyy-MM-dd"
            type="date"
            placeholder="選擇租賃迄日"
            style="width: 200px"
          />
        </el-form-item>
        <el-form-item
          label="出租建物門牌地址" prop="BAdd"
        >
          <!-- <el-input
            v-model="editFrom.BAdd"
            type="textarea" :rows="4" style="width: 500px"
          />
          <p>多地址請用分號(;)區隔</p> -->
          <Address
            title="出租建物門牌地址"
            @geteditaddress="geteditaddress"
          />
        </el-form-item>
        <el-form-item
          ref="MOID"
          label="分租物件單號" :label-width="formLabelWidth" prop="MOID"
        >
          <el-input
            v-model="editFrom.MOID"
            style="width:500px" placeholder="請輸入分租物件單號" autocomplete="off"
          />
        </el-form-item>
        <el-form-item
          label="分租物件名稱"
          :label-width="formLabelWidth" prop="MOName"
        >
          <el-input
            v-model="editFrom.MOName"
            style="width:500px" readonly
          />
        </el-form-item>
        <el-form-item
          label="社宅物件編號"
          :label-width="formLabelWidth" prop="ObjectNo"
        >
          <el-input
            v-model="editFrom.ObjectNo"
            style="width:500px"
          />
        </el-form-item>
        <el-form-item
          label="兆基物件編號"
          :label-width="formLabelWidth" prop="CCObjectNo"
        >
          <el-input
            v-model="editFrom.CCObjectNo"
            style="width:500px"
          />
        </el-form-item>
        <el-form-item
          v-show="contractType1 || contractType3"
          label="出租人身份證號" :label-width="formLabelWidth" prop="LSID"
        >
          <el-input
            v-model="editFrom.LSID"
            v-upperCase
            style="width:500px"
            placeholder="請輸入出租人身份證號"
            autocomplete="off"
            maxlength="10"
            clearable
            @blur="getRemittanceTarget(editFrom.RemittanceTarget)"
          />
        </el-form-item>
        <el-form-item
          v-show="contractType2 || contractType3"
          label="承租人身份證號" :label-width="formLabelWidth" prop="RNID"
        >
          <el-input
            v-model="editFrom.RNID"
            v-upperCase style="width:500px" placeholder="請輸入承租人身份證號" autocomplete="off" clearable @blur="getRemittanceTarget(editFrom.RemittanceTarget)"
          />
        </el-form-item>
        <el-form-item
          v-if="contractType2"
          label="來源包租合約編號" :label-width="formLabelWidth" prop="SCID"
        >
          <el-input
            v-model="editFrom.SCID"
            style="width:500px"
            placeholder="請輸入來源包租合約編號"
            autocomplete="off"
            maxlength="40"
            clearable
          />
        </el-form-item>
        <el-form-item
          label="合約帳號對象"
          prop="RemittanceTarget"
        >
          <el-select
            v-model="editFrom.RemittanceTarget"
            placeholder="請選擇" @change="getRemittanceTarget"
          >
            <el-option
              v-for="item in RemittanceTarget"
              :key="item.value" :label="item.label" :value="item.value"
            />
          </el-select>
        </el-form-item>
        <el-form-item
          v-if="RemittanceTargetL && editFrom.LSID"
          label="出租人匯款資訊" prop="AccountNameL"
        >
          <el-select
            v-model="editFrom.AccountNameL"
            placeholder="請選擇"
          >
            <el-option
              v-for="item in RemittanceL"
              :key="item.Id"
              :label="item.LAName+' ' + item.LBankNo + '-' + item.LBranchNo + '-' + item.LANo"
              :value="item.Id"
            />
          </el-select>
        </el-form-item>
        <el-form-item
          v-if="RemittanceTargetR && editFrom.RNID"
          label="承租人匯款資訊" prop="AccountNameR"
        >
          <el-select
            v-model="editFrom.AccountNameR"
            placeholder="請選擇"
          >
            <el-option
              v-for="item in RemittanceR"
              :key="item.Id"
              :label="item.RAName+' ' + item.RBankNo + '-' + item.RBranchNo + '-' + item.RANo"
              :value="item.Id"
            />
          </el-select>
        </el-form-item>
        <el-form-item
          v-if="RemittanceTargetC"
          label="社宅/一般宅" prop="RemittanceType"
        >
          <el-select
            v-model="editFrom.RemittanceType"
            placeholder="請選擇" @change="getAccountNameByType"
          >
            <el-option
              v-for="item in RemittanceType"
              :key="item.value" :label="item.label" :value="item.value"
            />
          </el-select>
        </el-form-item>
        <el-form-item
          v-if="RemittanceTargetC"
          label="戶名" prop="AccountNameC"
        >
          <el-select
            v-model="editFrom.AccountNameC"
            placeholder="請選擇" @change="getUseCountyByAccountName"
          >
            <el-option
              v-for="item in AccountName"
              :key="item.AccountName"
              :label="item.AccountName"
              :value="item.AccountName"
            />
          </el-select>
        </el-form-item>
        <el-form-item
          v-if="RemittanceTargetC"
          label="使用單位" prop="UseCounty"
        >
          <el-select
            v-model="editFrom.UseCounty"
            placeholder="請選擇" @change="getBankNameByUseCounty"
          >
            <el-option
              v-for="item in UseCounty"
              :key="item.UseCounty"
              :label="item.UseCounty"
              :value="item.UseCounty"
            />
          </el-select>
        </el-form-item>
        <el-form-item
          v-if="RemittanceTargetC"
          label="銀行" prop="BankName"
        >
          <el-select
            v-model="editFrom.BankName"
            placeholder="請選擇"
          >
            <el-option
              v-for="item in BankName"
              :key="item.BankName" :label="item.BankName" :value="item.BankName"
            />
          </el-select>
        </el-form-item>
        <el-form-item
          v-if="contractType1 || contractType2"
          label="二房東" prop="SecondLord"
        >
          <el-select
            v-model="editFrom.SLID"
            placeholder="請選擇" style="width:500px" @change="getSLMAList"
          >
            <el-option
              v-for="item in secondLandlord2"
              :key="item.SLID" :label="item.SLName" :value="item.SLID"
            />
          </el-select>
        </el-form-item>
        <el-form-item
          v-if="contractType3"
          label="管理方" prop="Management"
        >
          <el-select
            v-model="editFrom.MAID"
            placeholder="請選擇" style="width:500px" @change="getSLMAList"
          >
            <el-option
              v-for="item in management"
              :key="item.MAID" :label="item.MName" :value="item.MAID"
            />
          </el-select>
        </el-form-item>
        <el-form-item
          v-if="contractType1 || contractType2 || contractType3"
          label="營業地址" prop="SIAdd"
        >
          <el-select
            v-model="editFrom.SIAdd"
            placeholder="請選擇" @change="getSIList"
          >
            <el-option
              v-for="item in slma"
              :key="item.Address" :label="item.Address" :value="item.Address"
            />
          </el-select>
        </el-form-item>
        <el-form-item
          v-if="contractType1 || contractType2 || contractType3"
          label="聯絡電話" prop="SITel"
        >
          <el-select
            v-model="editFrom.SITel"
            placeholder="請選擇"
          >
            <el-option
              v-for="item in slmatel"
              :key="item.Tel" :label="item.Tel" :value="item.Tel"
            />
          </el-select>
        </el-form-item>
        <el-form-item
          v-if="contractType1 || contractType2 || contractType3"
          label="指定住管員" prop="Superintendent"
        >
          <el-select
            v-model="editFrom.SILRNo"
            placeholder="請選擇"
          >
            <el-option
              v-for="item in superintendent"
              :key="item.SILRNo" :label="item.SIName" :value="item.SILRNo"
            />
          </el-select>
        </el-form-item>
        <el-form-item
          v-if="contractType1 || contractType2 || contractType3"
          label="對應經紀人" prop="Agent"
        >
          <el-select
            v-model="editFrom.AGLRNo"
            placeholder="請選擇"
          >
            <el-option
              v-for="item in agent"
              :key="item.AGLRNo" :label="item.AGName" :value="item.AGLRNo"
            />
          </el-select>
        </el-form-item>
      </el-form>
      <div
        slot="footer"
        class="dialog-footer"
      >
        <el-button
          size="small"
          icon="el-icon-close" @click="cancel"
        >放 棄</el-button>
        <el-button
          v-preventReClick
          v-hasPermi="['Contract/Add']"
          size="small"
          icon="el-icon-document"
          type="primary"
          @click="saveEditForm()"
        >產生合約</el-button>
      </div>
    </el-dialog>
    <el-dialog
      ref="dialogContractForm"
      title="合約資料"
      :visible.sync="dialogContractFormVisible"
      width="1040px"
      top="0"
      fullscreen
    >
      <el-tabs
        v-model="TabAlias"
        type="border-card"
      >
        <el-tab-pane
          label="基本資料"
          name="CT1"
        >
          <el-card
            v-loading="CT1Loading"
            class="box-card"
          >
            <el-form
              ref="editFrom"
              :inline="true" :model="editFrom" class="demo-form-inline"
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
                      v-model="editFrom.MOID"
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
                      v-model="editFrom.MOName"
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
                      v-model="editFrom.CID"
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
                      v-model="editFrom.SalesName"
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
                      v-model="editFrom.LSInfo.LSName"
                      style="width:250px" readonly
                    />
                  </el-form-item>
                </el-col>
                <el-col :span="12">
                  <el-form-item
                    v-if="contractType1 || contractType3"
                    label="出租人身份證號/統一編號"
                    :label-width="contractLabelWidth"
                  >
                    <el-input
                      v-model="editFrom.LSInfo.LSID"
                      style="width:250px" readonly
                    />
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row>
                <el-col :span="12">
                  <el-form-item
                    v-show="contractType1 || contractType2"
                    label="二房東名稱" :label-width="contractLabelWidth" prop="SLName"
                  >
                    <el-input
                      v-model="editFrom.SLName"
                      style="width:400px" readonly
                    />
                  </el-form-item>
                </el-col>
                <el-col :span="12">
                  <el-form-item
                    v-show="contractType1 || contractType2"
                    label="二房東統一編號" :label-width="contractLabelWidth" prop="SLID"
                  >
                    <el-input
                      v-model="editFrom.SLID"
                      style="width:250px" readonly
                    />
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row>
                <el-col :span="12">
                  <el-form-item
                    v-show="contractType3"
                    label="管理方名稱" :label-width="contractLabelWidth" prop="MName"
                  >
                    <el-input
                      v-model="editFrom.MName"
                      style="width:400px" readonly
                    />
                  </el-form-item>
                </el-col>
                <el-col :span="12">
                  <el-form-item
                    v-show="contractType3"
                    label="管理方統一編號" :label-width="contractLabelWidth" prop="MAID"
                  >
                    <el-input
                      v-model="editFrom.MAID"
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
                      v-model="editFrom.RNInfo.RNName"
                      style="width:250px" readonly
                    />
                  </el-form-item>
                </el-col>
                <el-col :span="12">
                  <el-form-item
                    v-if="contractType2 || contractType3"
                    label="承租人身份證號/統一編號"
                    :label-width="contractLabelWidth"
                  >
                    <el-input
                      v-model="editFrom.RNInfo.RNID"
                      style="width:250px" readonly
                    />
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row>
                <el-col :span="24">
                  <el-form-item
                    v-if="contractType2"
                    label="來源包租合約編號" :label-width="contractLabelWidth" prop="SCID"
                  >
                    <el-input
                      v-model="editFrom.SCID"
                      style="width:500px" readonly
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
                      v-model="editFrom.BuildingInfo.BAdd"
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
                      v-model="editFrom.CStatus"
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
                      v-model="editFrom.ObjectNo"
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
                      v-model="editFrom.CCObjectNo"
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
                      v-model="editFrom.MID"
                      readonly
                    />
                  </el-form-item>
                </el-col>
              </el-row>
              <!-- <el-row>
                <el-col :span="7">&nbsp;</el-col>
                <el-col :span="7">&nbsp;</el-col>
                <el-col :span="10"> -->
              <div
                v-if="showUnsignedBtn"
                class="rightbtn"
              >
                <el-button
                  size="small"
                  icon="el-icon-close" @click="cancel"
                >關 閉</el-button>
                <el-button
                  v-hasPermi="['Contract/Edit']"
                  type="primary"
                  size="small"
                  icon="el-icon-download"
                  @click="downloadContract()"
                >下載主約表單
                </el-button>
                <el-button
                  v-preventReClick
                  v-hasPermi="['Contract/Edit']"
                  type="primary"
                  size="small"
                  icon="el-icon-upload"
                  @click="dialogUploadContractVisible = true"
                >
                  更新主約表單</el-button>
              </div>
              <!-- </el-col>
              </el-row> -->
            </el-form>
          </el-card>
        </el-tab-pane>
        <el-tab-pane
          label="合約內容"
          name="CT2"
        >
          <el-card class="box-card">
            <el-form
              ref="editFrom"
              :inline="true" :model="editFrom" class="demo-form-inline"
            >
              <el-row>
                <el-col :span="12">
                  <el-form-item
                    label="簽約日"
                    :label-width="contractLabelWidth"
                  >
                    <el-input
                      v-model="editFrom.ContractDate"
                      readonly
                    />
                  </el-form-item>
                </el-col>
                <el-col :span="12">
                  <el-form-item
                    label="是否同意公證"
                    :label-width="contractLabelWidth" prop="Notarization"
                  >
                    <el-input
                      v-model="editFrom.Notarization"
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
                      v-model="editFrom.BuildingInfo.BRDStart"
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
                      v-model="editFrom.BuildingInfo.BRDTEnd"
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
                      v-model="editFrom.BuildingInfo.BTCRent"
                      readonly
                    />
                  </el-form-item>
                </el-col>
                <el-col :span="12">
                  <el-form-item
                    label="車位月租金:"
                    :label-width="contractLabelWidth" prop="BZ030"
                  >
                    <el-input
                      v-model="editFrom.BuildingInfo.BParkFee"
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
                      v-model="editFrom.BTRPFee"
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
                      v-model="editFrom.BuildingInfo.BTRent_Day"
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
                      v-model="editFrom.BuildingInfo.PaymentMethod"
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
                      v-model="editFrom.BankName"
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
                      v-model="editFrom.BankNo"
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
                      v-model="editFrom.BranchName"
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
                      v-model="editFrom.BranchNo"
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
                      v-model="editFrom.AccountName"
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
                      v-model="editFrom.AccountNo"
                      readonly
                    />
                  </el-form-item>
                </el-col>
                <!-- <el-col :span="12">
                  <el-form-item
                    label="其他"
                    :label-width="contractLabelWidth"
                    prop="BZ014"
                  >
                    <el-input
                      value=""
                      readonly
                    />
                  </el-form-item>
                </el-col> -->
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
                      v-model="editFrom.BuildingInfo.BCarNo"
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
                    label="大樓管理費"
                    :label-width="contractLabelWidth" prop="BZ029"
                  >
                    <el-input
                      v-model="editFrom.BuildingInfo.BMFee"
                      readonly
                    />
                  </el-form-item>
                </el-col>
                <!-- <el-col :span="12">
                  <el-form-item label="車位租金 每月另付:" :label-width="contractLabelWidth" prop="BParkFee">
                    <el-input v-model="editFrom.BuildingInfo.BParkFee" readonly />
                  </el-form-item>
                </el-col> -->
              </el-row>
              <el-row>
                <el-col :span="12">
                  <el-form-item
                    label="押金為"
                    :label-width="contractLabelWidth" prop="BDMon"
                  >
                    <el-input
                      v-model="editFrom.BuildingInfo.BDMon"
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
                      v-model="editFrom.BuildingInfo.Bdeposit"
                      readonly
                    />
                  </el-form-item>
                </el-col>
              </el-row>
              <!-- <el-row> -->
              <div class="rightbtn">
                <el-button
                  size="small"
                  icon="el-icon-close" @click="cancel"
                >關 閉</el-button>
              </div>
              <!-- </el-col>
              </el-row> -->
            </el-form>
          </el-card>
        </el-tab-pane>

        <el-tab-pane
          label="相關資料"
          name="CT5"
        >
          <el-card class="box-card">
            <el-form
              ref="contractRelevantForm"
              :inline="true" :model="contractRelevantForm" class="demo-form-inline"
            >
              <el-row>
                <el-col :span="24">
                  <el-form-item
                    label="稅籍編號"
                    :label-width="contractLabelWidth" prop="B1TaxID"
                  >
                    <el-input
                      v-model="contractRelevantForm.B1TaxID"
                      style="width:400px"
                    />
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row>
                <el-col :span="24">
                  <el-form-item
                    label="租金補助"
                    :label-width="contractLabelWidth" prop="RNRentSUBAFee"
                  >
                    <el-input
                      v-model="contractRelevantForm.RNRentSUBAFee"
                      style="width:400px"
                    />
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row>
                <el-col :span="24">
                  <el-form-item
                    label="身分資格"
                    :label-width="contractLabelWidth" prop="RNQualify"
                  >
                    <el-select
                      v-model="contractRelevantForm.RNQualify"
                      placeholder="請選擇"
                    >
                      <el-option
                        v-for="item in RNQualify"
                        :key="item.value" :label="item.label" :value="item.value"
                      />
                    </el-select>
                  </el-form-item>
                </el-col>
              </el-row>
              <div class="rightbtn">
                <el-button
                  size="small"
                  icon="el-icon-close" @click="cancel"
                >關 閉</el-button>
                <el-button
                  v-preventReClick
                  v-hasPermi="['Contract/Edit']"
                  type="primary"
                  size="small"
                  icon="el-icon-paperclip"
                  @click="saveContractRelevantForm()"
                >
                  更新合約相關資料</el-button>
              </div>
            </el-form>
          </el-card>
        </el-tab-pane>

        <el-tab-pane
          label="附件綁定"
          name="CT3"
        >
          <el-card class="box-card">
            <el-form
              ref="editFrom"
              :inline="true" :model="editFrom" class="demo-form-inline"
            >
              <el-row>
                <el-col :span="12">
                  <!-- <div class="list-btn-container">
                    <el-button-group>
                      <el-button v-hasPermi="['User/Add']" type="success" icon="el-icon-plus" size="small" @click="bindAttachment()">綁 定</el-button>
                      <el-button v-hasPermi="['User/Edit']" type="danger" icon="el-icon-minus" size="small" @click="unBindAttachment()">清除綁定</el-button>
                    </el-button-group>
                  </div> -->
                  <el-table
                    ref="majorAttachmentTable"
                    v-loading="majorAttachmentTableloading"
                    :data="editFrom.MajorAttachmentList"
                    stripe
                    highlight-current-row
                    style="width: 100%"
                    :default-sort="{prop: 'SortCode', order: 'ascending'}"
                    :header-cell-style="{
                      'background-color': '#728FCE',
                      'color': '#fff',
                      'font-weight': '400'
                    }"
                    @row-dblclick="openBindAttachmentDialog"
                    @sort-change="handleSortChange"
                  >
                    <!-- <el-table-column type="selection" :selectable="disabledCheckBox" width="30" /> -->
                    <el-table-column
                      label="選擇"
                      width="60px" align="center" header-align="center"
                    >
                      <template slot-scope="scope">
                        <el-radio
                          v-model="archiveOption"
                          :label="scope.$index"
                          style="margin-left: 10px;"
                          :disabled="disableRadioButton(scope.$index, scope.row)"
                          @change="openBindAttachmentDialog(scope.row)"
                        >&nbsp;</el-radio>
                      </template>
                    </el-table-column>
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
                  <div class="list-btn-container">
                    <el-button-group>
                      <el-button
                        v-hasPermi="['Contract/Edit']"
                        type="success"
                        icon="el-icon-plus"
                        size="small"
                        @click="dialogUploadAttachmentVisible = true"
                      >新 增</el-button>
                      <el-button
                        v-hasPermi="['Contract/Edit']"
                        type="danger"
                        icon="el-icon-minus"
                        size="small"
                        @click="deleteAttachment()"
                      >刪 除</el-button>
                    </el-button-group>
                  </div>
                  <el-table
                    ref="minorAttachmentTable"
                    v-loading="minorAttachmentTableloading"
                    :data="editFrom.MinorAttachmentList"
                    stripe
                    highlight-current-row
                    style="width: 100%"
                    :default-sort="{prop: 'SortCode', order: 'ascending'}"
                    :header-cell-style="{
                      'background-color': '#728FCE',
                      'color': '#fff',
                      'font-weight': '400'
                    }"
                    @select="handleMinorAttachmentSelectChange"
                    @select-all="handleMinorAttachmentSelectAllChange"
                    @sort-change="handleSortChange"
                  >
                    <el-table-column
                      type="selection"
                      width="30"
                    />
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
              <el-row>
                <!-- <el-col :span="20">&nbsp;</el-col>
                <el-col :span="4"> -->
                <div class="rightbtn">
                  <el-button
                    size="small"
                    icon="el-icon-close" @click="cancel"
                  >關 閉</el-button>
                  <!-- <el-button
                    v-hasPermi="['Contract/Edit']"
                    type="primary"
                    size="small"
                    icon="el-icon-edit"
                    @click="finalized"
                  >合約定稿</el-button> -->
                  <el-button
                    v-if="showUnsignedBtn"
                    v-preventReClick
                    v-hasPermi="['Contract/Edit']"
                    type="primary"
                    size="small"
                    icon="el-icon-submit"
                    @click="finalized"
                  >合約送審</el-button>
                </div>
                <!-- </el-col>
              </el-row> -->
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
              ref="editFrom"
              :inline="true" :model="editFrom" class="demo-form-inline"
            >
              <div
                v-if="showUploadSignedContractButton"
                class="list-btn-container"
              >
                <el-button-group>
                  <el-button
                    v-hasPermi="['Contract/Edit']"
                    type="success"
                    icon="el-icon-upload"
                    size="small"
                    @click="dialogUploadSignedContractVisible = true"
                  >上傳最新版掃描檔</el-button>
                  <el-button
                    v-preventReClick
                    v-hasPermi="['Contract/Edit']"
                    type="success"
                    icon="el-icon-edit"
                    class="el-button-modify"
                    size="small"
                    @click="dialogUpdateMIDVisible = true"
                  >更新媒合編號</el-button>
                  <el-button
                    v-preventReClick
                    v-hasPermi="['Contract/Edit']"
                    type="success"
                    icon="el-icon-edit"
                    class="el-button-modify"
                    size="small"
                    @click="openSubmitSignedContract(editFrom.CID, editFrom.Version)"
                  >送 審</el-button>
                </el-button-group>
              </div>
              <el-table
                ref="contractHistoryTable"
                v-loading="contractHistoryTableloading"
                :data="editFrom.ContractHistoryList"
                stripe
                highlight-current-row
                style="width: 100%"
                :default-sort="{prop: 'SortCode', order: 'ascending'}"
                :header-cell-style="{
                  'background-color': '#728FCE',
                  'color': '#fff',
                  'font-weight': '400'
                }"
                @sort-change="handleSortChange"
              >
                <el-table-column
                  prop="Version"
                  label="版號" sortable="custom" width="120"
                />
                <el-table-column
                  prop="CName"
                  label="合約名稱" sortable="custom" width="250"
                >
                  <template slot-scope="scope">
                    <a
                      href="#"
                      style="text-decoration: underline; cursor: pointer"
                      @click="downloadFinalizedPDF(scope.row.Version, scope.row.CName)"
                    >{{ scope.row.CName }}</a>
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
                    >{{ scope.row.Status }}</a>
                    <span v-else>{{ scope.row.Status }}</span>
                  </template>
                </el-table-column>
                <el-table-column
                  prop="UploadTime"
                  label="產生日期" sortable="custom" width="200"
                />
                <el-table-column
                  prop="Note"
                  label="備註" sortable="custom" width="120"
                />
                <el-table-column
                  prop="UploadUser"
                  label="異動人員" sortable="custom" width="120"
                />
                <!-- <el-table-column label="" width="120">
                  <template slot-scope="scope">
                    <el-button
                      @click="openSubmitSignedContract(scope.row.CID, scope.row.Version)"
                    >送 審</el-button>
                  </template>
                </el-table-column> -->
              </el-table>
              <el-row>&nbsp;</el-row>
              <!-- <el-row>
                <el-col :span="4">&nbsp;</el-col>
                <el-col :span="4">&nbsp;</el-col>
                <el-col :span="16"> -->
              <div class="rightbtn">
                <el-button
                  size="small"
                  icon="el-icon-close" @click="cancel"
                >關閉</el-button>
                <!-- <el-button
                  v-if="showContractEffiectiveButton"
                  v-hasPermi="['Contract/Edit']"
                  type="primary"
                  size="small"
                  icon="el-icon-video-play"
                  @click="contractEffiective"
                >放行
                </el-button>
                <el-button
                  v-if="showContractEffiectiveButton2"
                  v-hasPermi="['Contract/Edit']"
                  type="primary"
                  size="small"
                  icon="el-icon-document"
                  @click="contractEffiective"
                >正式進版
                </el-button> -->
              </div>
              <!-- </el-col>
              </el-row> -->
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
              :data="editFrom.ContractFlowLogList"
              stripe
              highlight-current-row
              style="width: 100%"
              :default-sort="{prop: 'ApplyTime', order: 'ascending'}"
              :header-cell-style="{
                'background-color': '#728FCE',
                'color': '#fff',
                'font-weight': '400'
              }"
              @sort-change="handleSortChange"
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
            <el-row>&nbsp;</el-row>
            <div class="rightbtn">
              <el-button
                size="small"
                icon="el-icon-close" @click="cancel"
              >關閉</el-button>
            </div>
          </el-card>
        </el-tab-pane>
      </el-tabs>
    </el-dialog>
    <el-dialog
      ref="dialogBindContractAttachment"
      v-el-drag-dialog
      title="挑選綁定表單/文件"
      :visible.sync="dialogBindContractAttachmentVisible"
      width="480px"
      :close-on-click-modal="false"
      append-to-body
    >
      <!-- <BuildingFormHistory
        :id="BID"
        ref="buildingformhistory"
        :for-contract="true"
      /> -->
      <el-form
        class="demo-form-inline"
        size="small"
      >
        <el-form-item label=" ">
          <el-button-group>
            <el-button
              v-hasPermi="['Contract/Edit']"
              type="success"
              size="small"
              icon="el-icon-plus"
              @click="bindAttachment"
            >綁定</el-button>
            <el-button
              v-hasPermi="['Contract/Edit']"
              type="danger"
              size="small"
              icon="el-icon-minus"
              @click="unBindAttachment"
            >清除綁定
            </el-button>
          </el-button-group>
        </el-form-item>
        <el-table
          v-loading="bindAttachmentFormLoading"
          :data="bindAttachmentFormData"
          border
          @current-change="handleBindAttachmentSelectChange"
        >
          <el-table-column
            label="選擇"
            width="60px" align="center" header-align="center"
          >
            <template slot-scope="scope">
              <el-radio
                v-model="bindOption"
                :label="scope.$index" style="margin-left: 10px;"
              >&nbsp;</el-radio>
            </template>
          </el-table-column>
          <el-table-column
            prop="FormName"
            label="表單名稱"
          />
          <el-table-column
            prop="UploadTime"
            label="上傳日期"
          />
        </el-table>
      </el-form>
    </el-dialog>
    <el-dialog
      ref="dialogUploadContract"
      v-el-drag-dialog title="上傳主約" :visible.sync="dialogUploadContractVisible" width="480px" :close-on-click-modal="false" append-to-body
    >
      <el-form
        :inline="true"
        class="demo-form-inline"
      >
        <el-row>
          <el-upload
            ref="uploadContract"
            drag
            :data="editFrom"
            :action="httpUploadContractUrl"
            :headers="headers"
            :multiple="false"
            :auto-upload="true"
            :show-file-list="false"
            accept=".pdf"
            :on-change="uploadchange"
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
        </el-row>
      </el-form>
      <div
        slot="footer"
        class="dialog-footer"
      >
        <el-button
          size="small"
          icon="el-icon-paperclip" @click="dialogUploadContractVisible = false"
        >關 閉</el-button>
      </div>
    </el-dialog>
    <el-dialog
      ref="dialogUploadAttachment"
      v-el-drag-dialog
      title="上傳合約其他附件"
      :visible.sync="dialogUploadAttachmentVisible"
      width="480px"
      :close-on-click-modal="false"
      append-to-body
    >
      <el-form
        :inline="true"
        class="demo-form-inline"
      >
        <el-row>
          <el-upload
            ref="uploadAttachment"
            drag
            :data="editFrom"
            :action="httpUploadAttachmentUrl"
            :headers="headers"
            :multiple="true"
            :auto-upload="true"
            :show-file-list="false"
            accept=".bmp,.png,.jpg,.gif,.pdf,"
            :on-change="uploadchange"
            :on-success="uploadsuccess2"
            :on-error="uploaderror"
          >
            <em class="el-icon-upload" />
            <div class="el-upload__text">將文件拖到此處，或<em>點擊上傳</em> </div>
            <div
              slot="tip"
              class="el-upload__tip"
            >請註意您只能上傳PDF和圖檔</div>
          </el-upload>
        </el-row>
      </el-form>
      <div
        slot="footer"
        class="dialog-footer"
      >
        <el-button
          size="small"
          icon="el-icon-close" @click="dialogUploadAttachmentVisible = false"
        >關 閉</el-button>
      </div>
    </el-dialog>
    <el-dialog
      ref="dialogUploadSignedContract"
      v-el-drag-dialog
      title="上傳最新版掃描檔"
      :visible.sync="dialogUploadSignedContractVisible"
      width="480px"
      :close-on-click-modal="false"
      append-to-body
    >
      <el-form
        :inline="true"
        class="demo-form-inline"
      >
        <el-row>
          <el-upload
            ref="uploadSignedContract"
            drag
            :data="editFrom"
            :action="httpUploadSignedContractUrl"
            :headers="headers"
            :multiple="false"
            :auto-upload="true"
            :show-file-list="false"
            accept=".pdf"
            :on-change="uploadchange"
            :on-success="uploadsuccess3"
            :on-error="uploaderror"
            :before-upload="handleUploadBefore3"
          >
            <em class="el-icon-upload" />
            <div class="el-upload__text">將文件拖到此處，或<em>點擊上傳</em> </div>
            <div
              slot="tip"
              class="el-upload__tip"
            >請註意您只能上傳PDF</div>
          </el-upload>
        </el-row>
        <!-- <el-row>
          <el-form-item label="送審意見" prop="SignedFlowComment">
            <el-input v-model="editFrom.Comment" type="textarea" :rows="5" style="width: 400px" />
          </el-form-item>
        </el-row> -->
      </el-form>
      <div
        slot="footer"
        class="dialog-footer"
      >
        <el-button
          size="small"
          icon="el-icon-close" @click="dialogUploadSignedContractVisible = false"
        >關 閉</el-button>
      </div>
    </el-dialog>
    <el-dialog
      ref="dialogNote"
      v-el-drag-dialog v-loading="dialogNoteLoading" title="請填寫送審意見" :visible.sync="dialogNoteVisible" width="600px" :close-on-click-modal="false" append-to-body
    >
      <el-form
        ref="noteForm"
        :model="editFrom" :rules="noteRules"
      >
        <el-form-item
          label="歷史版本備註"
          prop="Note"
        >
          <el-input
            v-model="editFrom.Note"
            type="textarea" :rows="4" style="width: 500px"
          />
        </el-form-item>
        <el-form-item
          label="送審意見"
          prop="Comment"
        >
          <el-input
            v-model="editFrom.Comment"
            type="textarea" :rows="4" style="width: 500px"
          />
        </el-form-item>
      </el-form>
      <div
        slot="footer"
        class="dialog-footer"
      >
        <el-button
          size="small"
          icon="el-icon-close" @click="cancelComment"
        >關閉</el-button>
        <el-button
          v-preventReClick
          v-hasPermi="['Contract/Edit']"
          type="primary"
          size="small"
          icon="el-icon-edit"
          @click="contractFinalized"
        >合約定稿</el-button>
      </div>
    </el-dialog>
    <el-dialog
      ref="dialogUpdateMID"
      v-el-drag-dialog title="請填寫媒合編號" :visible.sync="dialogUpdateMIDVisible" width="600px" :close-on-click-modal="false" append-to-body
    >
      <el-form
        ref="editFrom"
        :model="editFrom"
      >
        <el-form-item
          label="媒合編號"
          prop="MID"
        >
          <el-input
            v-model="editFrom.MID"
            style="width: 500px"
          />
        </el-form-item>
      </el-form>
      <div
        slot="footer"
        class="dialog-footer"
      >
        <el-button
          size="small"
          icon="el-icon-close" @click="cancelMID"
        >關閉</el-button>
        <el-button
          v-preventReClick
          v-hasPermi="['Contract/Edit']"
          type="primary"
          size="small"
          icon="el-icon-document"
          @click="updateMID"
        >
          更新媒合編號</el-button>
      </div>
    </el-dialog>
    <el-dialog
      ref="dialogTerminateContract"
      v-el-drag-dialog title="請填寫送審意見" :visible.sync="dialogTerminateContractVisible" width="600px" :close-on-click-modal="false" append-to-body
    >
      <el-form
        ref="terminateContractForm"
        :model="editFrom" :rules="signedFlowRules"
      >
        <el-form-item
          label="送審意見"
          prop="Comment"
        >
          <el-input
            v-model="editFrom.Comment"
            type="textarea" :rows="4" style="width: 500px"
          />
        </el-form-item>
      </el-form>
      <div
        slot="footer"
        class="dialog-footer"
      >
        <el-button
          size="small"
          icon="el-icon-close" @click="cancelTerminateComment"
        >關閉</el-button>
        <el-button
          v-preventReClick
          v-hasPermi="['Contract/Terminate']"
          type="primary"
          size="small"
          icon="el-icon-edit"
          @click="submitTerminateContract"
        >合約解約送審</el-button>
      </div>
    </el-dialog>
    <el-dialog
      ref="dialogSignedContract"
      v-el-drag-dialog title="請填寫送審意見" :visible.sync="dialogSignedContractVisible" width="600px" :close-on-click-modal="false" append-to-body
    >
      <el-form
        ref="signedContractForm"
        :model="editFrom" :rules="submitSignedContractRules"
      >
        <el-form-item
          label="簽約日："
          prop="ContractDate"
        >
          <DatePickerE
            v-model="editFrom.ContractDate"
            value-format="yyyy-MM-dd"
            format="yyyy-MM-dd"
            type="date"
            placeholder="選擇簽約日"
            style="width: 200px"
          />
        </el-form-item>
        <el-form-item
          label="送審意見"
          prop="Comment"
        >
          <el-input
            v-model="editFrom.Comment"
            type="textarea" :rows="4" style="width: 500px"
          />
        </el-form-item>
      </el-form>
      <div
        slot="footer"
        class="dialog-footer"
      >
        <el-button
          size="small"
          icon="el-icon-close" @click="cancelSignedComment"
        >關閉</el-button>
        <el-button
          v-preventReClick
          v-hasPermi="['Contract/Edit']"
          type="primary"
          size="small"
          icon="el-icon-edit"
          @click="submitSignedContract"
        >已簽名合約送審</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>

import {
  getContractListWithPager,
  getContract,
  getMajorAttachments,
  getMinorAttachments,
  getContractHistory,
  saveContract,
  downloadContract,
  contractInvalid,
  bindAttachment,
  unbindAttachment,
  downloadFinalizedPDF,
  downloadSignedContract,
  // contractEffiective,
  submitSignedContract,
  deleteMinorAttachment,
  deleteContract,
  updateMID,
  submitTerminateContract,
  initialContract,
  getMOIDByBAdd
} from '@/api/chaochi/contract/contractservice'
import { getToDoListDetail2, getToDoListDetail3 } from '@/api/chaochi/todolist/todolistservice'
import { getBuildingHistoryFormListWithPager, getRNHistoryFormListWithPager, getLNHistoryFormListWithPager } from '@/api/chaochi/historyform/historyformservice'
import { getAllSecondLandlordList } from '@/api/chaochi/secondlandlord/secondlandlordservice'
import { getAllManagementList } from '@/api/chaochi/management/managementservice'
import { getSLMAList, getSIList, getAGList, getSLMATel } from '@/api/chaochi/slma/slmaservice'
import { getAllUserList } from '@/api/security/userservice'
import { getAllCategoryTreeTable, getCategoryDetail } from '@/api/chaochi/category/categorycontractservice'
import { GetRemittancesByIDNo } from '@/api/chaochi/remittance/remittancel'
import { GetRemittancesByIDNoR } from '@/api/chaochi/remittance/remittanceR'
import { getAccountNameByType, getUseCountyByAccountName, getBankNameByUseCounty } from '@/api/chaochi/contractremittance/contractremittance'
import { validateIDCombo } from '@/utils/validate'
import Address from '@/components/Address/Address.vue'
import DatePickerE from '@/components/RocDatepickerE';
import elDragDialog from '@/directive/el-drag-dialog' // 彈窗可移動
import defaultSettings from '@/settings'
import { getToken } from '@/utils/auth'
import { mergerAddress } from '@/utils/index';
export default {
  name: 'Contract', // 需與菜單的功能編碼一致
  directives: { elDragDialog },
  components: {
    DatePickerE,
    Address
    // BuildingFormHistory
  },
  data() {
    return {
      searchform: {
        LSName: '',
        LSID: '',
        RNName: '',
        RNID: '',
        SLID: '',
        Sales: '',
        MAID: '',
        MOID: '',
        CID: '',
        MID: '',
        CStatus: '',
        BAdd: ''
      },
      bindAttachmentFormLoading: true,
      bindAttachmentFormData: [],
      BID: '',
      archiveOption: -1,
      bindOption: -1,
      secondLandlord: [],
      secondLandlord2: [],
      sales: [],
      management: [],
      slmaid: '',
      slma: [],
      slmatel: [],
      superintendent: [],
      agent: [],
      status: [
        { value: '', label: '無指定' },
        { value: '備審未簽名', label: '備審未簽名' },
        { value: '已簽名待放行', label: '已簽名待放行' },
        { value: '待生效', label: '待生效' },
        { value: '效期中', label: '效期中' },
        { value: '已到期', label: '已到期' },
        { value: '已解約', label: '已解約' },
        { value: '已作廢', label: '已作廢' }
      ],
      RNQualify: [
        { value: '', label: '無指定' },
        { value: '一般戶', label: '一般戶' },
        { value: '第一類弱勢戶', label: '第一類弱勢戶' },
        { value: '第二類弱勢戶', label: '第二類弱勢戶' }
      ],
      RemittanceTarget: [
        // { value: '出租人', label: '出租人' },
        // { value: '公司', label: '' }
      ],
      RemittanceL: [],
      RemittanceR: [],
      RemittanceType: [
        { value: '社宅', label: '社宅' },
        { value: '一般宅', label: '一般宅' }
      ],
      AccountName: [],
      UseCounty: [],
      BankName: [],
      selectCategory: [],
      categoryFullPath: '',
      TabAlias: 'CT1',
      tableData: [],
      tableloading: true,
      CT1Loading: true,
      majorAttachmentTableloading: true,
      minorAttachmentTableloading: true,
      contractHistoryTableloading: true,
      contractFlowLogTableloading: true,
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
      dialogContractFormVisible: false,
      editFrom: {
        BZOutputDto: {
          BZ029: '',
          BZ009: '',
          // BZ111213: '',
          BZ006007: '',
          BZ059060: '',
          BZ030: ''
        },
        LSInfo: {},
        RNInfo: {},
        BuildingInfo: {}
      },
      contractFrom: {},
      contractRelevantForm: {},
      rules: {
        LSID: [
          // { required: true, trigger: 'change', validator: validateIDCombo }
        ],
        RNID: [
          // { required: true, trigger: 'change', validator: validateIDCombo }
        ]
        // BAdd: [
        //   { required: true, message: '請輸入建物地址', trigger: 'change' }
        // ]
      },
      noteRules: {
        Comment: [
          { required: true, message: '請輸入審核意見', trigger: 'blur' }
        ]
      },
      signedFlowRules: {
        Comment: [
          { required: true, message: '請輸入解約審核意見', trigger: 'blur' }
        ]
      },
      submitSignedContractRules: {
        Comment: [
          { required: true, message: '請輸入審核意見', trigger: 'blur' }
        ],
        ContractDate: [
          { required: true, message: '請輸入簽約日', trigger: 'blur' }
        ]
      },
      formLabelWidth: '140px',
      dialogLabelWidth: '110px',
      contractLabelWidth: '180px',
      currentId: '', // 當前操作對象的ID值，主要用于修改
      currentCID: '', // 當前操作對象的合約編號，主要用於修改
      currentVersion: '',
      currentSelected: [],
      currentMinorAttachmentSelected: [],
      currentMajorAttachmentSelected: [],
      currentBindAttachmentSelected: [],
      httpUploadContractUrl:
        defaultSettings.apiChaochiUrl + 'Contract/UploadContract',
      httpUploadAttachmentUrl:
        defaultSettings.apiChaochiUrl + 'ContractAttachment/UploadMinorAttachment',
      httpUploadSignedContractUrl:
        defaultSettings.apiChaochiUrl + 'Contract/UploadSignedContract',
      headers: [],
      dialogUploadContractVisible: false,
      dialogUploadAttachmentVisible: false,
      dialogUploadSignedContractVisible: false,
      dialogBindContractAttachmentVisible: false,
      dialogNoteVisible: false,
      dialogNoteLoading: false,
      dialogUpdateMIDVisible: false,
      dialogTerminateContractVisible: false,
      dialogSignedContractVisible: false,
      showUploadSignedContractButton: false,
      showUnsignedBtn: false
    }
  },
  computed: {
    bcar() {
      if (this.editFrom.BuildingInfo.BCar_Y) {
        if (this.editFrom.BuildingInfo.BCar_Y === '/Yes') {
          return '有';
        } else if (this.editFrom.BuildingInfo.BCar_Y === '/Off') {
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
      if (this.editFrom.CType) {
        type = this.editFrom.CType.trim()
      }
      return type === '1'
    },
    contractType2() {
      let type = ''
      if (this.editFrom.CType) {
        type = this.editFrom.CType.trim()
      }
      return type === '2'
    },
    contractType3() {
      let type = ''
      if (this.editFrom.CType) {
        type = this.editFrom.CType.trim()
      }
      return type === '3'
    },
    RemittanceTargetL() {
      return this.editFrom.RemittanceTarget === '出租人'
    },
    RemittanceTargetR() {
      return this.editFrom.RemittanceTarget === '承租人'
    },
    RemittanceTargetC() {
      return this.editFrom.RemittanceTarget === '公司'
    },
    showSubmitButton() {
      let status = ''
      if (this.editFrom.CStatus) {
        status = this.editFrom.CStatus.trim()
      }
      return status === '備審未簽名' || status === '已簽名待放行'
    }
    // showContractEffiectiveButton() {
    //   let status = ''
    //   if (this.editFrom.CStatus) {
    //     status = this.editFrom.CStatus.trim()
    //   }
    //   return status === '已簽約待進版'
    // },
    // showContractEffiectiveButton2() {
    //   let status = ''
    //   if (this.editFrom.CStatus) {
    //     status = this.editFrom.CStatus.trim()
    //   }
    //   return status === '已簽約待審核'
    // }
  },
  watch: {
    contractType1: {
      immediate: true,
      flush: 'post',
      handler(value) {
        this.rules.LSID = []
        this.rules.RNID = []
        this.rules.LSID.push({ required: true, trigger: 'change', validator: validateIDCombo })
        if (value) {
          this.RemittanceTarget = []
          this.RemittanceTarget.push({ value: '出租人', label: '出租人' }, { value: '公司', label: '' })
        }
      }
    },
    contractType2: {
      immediate: true,
      flush: 'post',
      handler(value) {
        this.rules.LSID = []
        this.rules.RNID = []
        this.rules.RNID.push({ required: true, trigger: 'change', validator: validateIDCombo })
        if (value) {
          this.RemittanceTarget = []
          this.RemittanceTarget.push({ value: '承租人', label: '承租人' }, { value: '公司', label: '' })
        }
      }
    },
    contractType3: {
      // deep: true,
      immediate: true,
      flush: 'post',
      handler(value) {
        this.rules.LSID = []
        this.rules.RNID = []
        this.rules.LSID.push({ required: true, trigger: 'change', validator: validateIDCombo })
        this.rules.RNID.push({ required: true, trigger: 'change', validator: validateIDCombo })
        if (value) {
          this.RemittanceTarget = []
          this.RemittanceTarget.push({ value: '出租人', label: '出租人' }, { value: '承租人', label: '承租人' }, { value: '公司', label: '' })
        }
      }
    }
  },
  created() {
    this.pagination.currentPage = 1
    this.InitDictItem()
    this.loadTableData()
    this.headers = { Authorization: 'Bearer ' + (getToken() || '') }
  },
  methods: {
    /**
     * 初始化數據
     */
    InitDictItem() {
      getAllSecondLandlordList().then(res => {
        this.secondLandlord = res.ResData
        // const sl = {
        //   SLID: '',
        //   SLName: '無指定'
        // }
        // this.secondLandlord.splice(0, 0, sl)
      })
      getAllSecondLandlordList().then(res => {
        this.secondLandlord2 = res.ResData
      })
      getAllUserList().then(res => {
        this.sales = res.ResData
        // const sa = {
        //   Account: '',
        //   RealName: '無指定'
        // }
        // this.sales.splice(0, 0, sa)
      })
      getAllManagementList().then(res => {
        this.management = res.ResData
        // const ma = {
        //   MAID: '',
        //   MName: '請選擇'
        // }
        // this.management.splice(0, 0, ma)
      })
      getAllCategoryTreeTable().then(res => {
        this.selectCategory = res.ResData
      })
    },

    // 取消按鈕
    cancel() {
      this.dialogEditFormVisible = false
      this.dialogContractFormVisible = false
      this.dialogBindContractAttachmentVisible = false
      this.dialogNoteVisible = false
      this.dialogUploadSignedContractVisible = false
      this.slmaname = ''
      this.reset()
    },
    // 取消按鈕
    cancelTerminateComment() {
      this.dialogTerminateContractVisible = false
      this.editFrom.Comment = ''
    },
    // 取消按鈕
    cancelSignedComment() {
      this.dialogSignedContractVisible = false
      this.editFrom.Comment = ''
    },
    // 取消按鈕
    cancelComment() {
      this.dialogNoteVisible = false
      this.editFrom.Note = ''
      this.editFrom.Comment = ''
    },
    // 取消按鈕
    cancelMID() {
      this.dialogUpdateMIDVisible = false
      this.editFrom.MID = ''
    },
    // 表單重置
    reset() {
      this.editFrom = {
        CID: '',
        Version: '',
        CName: '',
        CType: '',
        CCate: '',
        NeedSalesSign: '',
        NeedSupervisorSign: '',
        NeedSignOnline: '',
        CStatus: '',
        ContractDate: '',
        Contract_Y: '',
        Contract_M: '',
        Contract_D: '',
        ObjectNo: '',
        CCObjectNo: '',
        Comment: '',
        Note: '',
        MID: '',
        MOID: '',
        MOName: '',
        MID1: '',
        SCID: '',
        LSID: '',
        LSName: '',
        LSRep: '',
        LSTel: '',
        LSCell: '',
        LSAdd: '',
        LSAdd_1: '',
        LSAdd_1_1: '',
        LSAdd_1_2: '',
        LSAdd_2: '',
        LSAdd_2_1: '',
        LSAdd_2_2: '',
        LSAdd_2_3: '',
        LSAdd_2_4: '',
        LSAdd_3: '',
        LSAdd_3_1: '',
        LSAdd_3_2: '',
        LSAdd_4: '',
        LSAdd_5: '',
        LSAdd_6: '',
        LSAdd_7: '',
        LSAdd_8: '',
        LSAdd_9: '',
        LSAddC: '',
        LSAddC_1: '',
        LSAddC_1_1: '',
        LSAddC_1_2: '',
        LSAddC_2: '',
        LSAddC_2_1: '',
        LSAddC_2_2: '',
        LSAddC_2_3: '',
        LSAddC_2_4: '',
        LSAddC_3: '',
        LSAddC_3_1: '',
        LSAddC_3_2: '',
        LSAddC_4: '',
        LSAddC_5: '',
        LSAddC_6: '',
        LSAddC_7: '',
        LSAddC_8: '',
        LSAddC_9: '',
        RNID: '',
        RNName: '',
        RNTel: '',
        RNAdd: '',
        RNAdd_1: '',
        RNAdd_1_1: '',
        RNAdd_1_2: '',
        RNAdd_2: '',
        RNAdd_2_1: '',
        RNAdd_2_2: '',
        RNAdd_2_3: '',
        RNAdd_2_4: '',
        RNAdd_3: '',
        RNAdd_3_1: '',
        RNAdd_3_2: '',
        RNAdd_4: '',
        RNAdd_5: '',
        RNAdd_6: '',
        RNAdd_7: '',
        RNAdd_8: '',
        RNAdd_9: '',
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
        BAdd_9: '',
        BNo_1: '',
        BNo_2: '',
        BNo_3: '',
        // BPNo_1: '',
        // BPNo_2: '',
        // BPNo_3: '',
        BPNo_1_A: '',
        BPNo_2_A: '',
        BPNo_3_A: '',
        BPNo_1_B: '',
        BPNo_2_B: '',
        BPNo_3_B: '',
        BPNo_1_C: '',
        BPNo_2_C: '',
        BPNo_3_C: '',
        BCar: '',
        BCar_Y: '',
        BCar_N: '',
        BCarNo: '',
        BArea: '',
        BRealArea: '',
        BTCRent: '',
        BTRent: '',
        BTRent_Day: '',
        BRDStart: '',
        BRDStart_Y: '',
        BRDStart_M: '',
        BRDStart_D: '',
        BRDTEnd: '',
        BRDTEnd_Y: '',
        BRDTEnd_M: '',
        BRDTEnd_D: '',
        BParkFee: '',
        BTransfer: '',
        BBill: '',
        BCash: '',
        PaymentMethod: '',
        BDMon: '',
        Bdeposit: '',
        SLID: '',
        SLName: '',
        SLRep: '',
        SLLRNo: '',
        SLAdd: '',
        SLTel: '',
        MAID: '',
        MName: '',
        MRep: '',
        MLRNo: '',
        MAdd: '',
        MTel: '',
        MEmail: '',
        SIName: '',
        SILRNo: '',
        SIAdd: '',
        SITel: '',
        SIEmail: '',
        Sales: '',
        SalesName: '',
        AGID: '',
        AGName: '',
        AGLRNo: '',
        AGAdd: '',
        AGTel: '',
        AGEmail: '',
        RemittanceTarget: '',
        RemittanceType: '',
        AccountNameL: '',
        AccountNameC: '',
        UseCounty: '',
        BankName: '',
        BZOutputDto: {
          BZ029: '',
          BZ009: '',
          // BZ111213: '',
          BZ006007: '',
          BZ059060: '',
          BZ030: ''
        },
        LSInfo: {},
        RNInfo: {},
        BuildingInfo: {},
        ContractRelevantInfo: {},
        MajorAttachmentList: [],
        MinorAttachmentList: [],
        ContractHistoryList: [],
        Id: '',
        archiveOption: -1,
        bindOption: -1,
        currentMinorAttachmentSelected: [],
        currentMajorAttachmentSelected: [],
        currentBindAttachmentSelected: []
        // 需個性化處理內容
      }
      // this.rules =
      // this.RemittanceTarget = []
      this.agent = []
      this.resetForm('editFrom')
      this.resetForm('contractRelevantForm')
    },
    // 表單重置
    resetAddData() {
      this.editFrom.LSID = ''
      this.editFrom.RNID = ''
      this.editFrom.SCID = ''
      this.editFrom.SLID = ''
      this.editFrom.MAID = ''
      this.editFrom.SIAdd = ''
      this.editFrom.SITel = ''
      this.editFrom.SILRNo = ''
      this.editFrom.AGLRNo = ''
    },
    /**
     * 加載頁面table數據
     */
    loadTableData: function() {
      this.tableloading = true
      var t = {
        LSName: this.searchform.LSName,
        LSID: this.searchform.LSID,
        RNName: this.searchform.RNName,
        RNID: this.searchform.RNID,
        SLID: this.searchform.SLID,
        Sales: this.searchform.Sales,
        MAID: this.searchform.MAID,
        MOID: this.searchform.MOID,
        CID: this.searchform.CID,
        MID: this.searchform.MID,
        CStatus: this.searchform.CStatus,
        BAdd: this.searchform.BAdd
      }
      var seachdata = {
        CurrenetPageIndex: this.pagination.currentPage,
        PageSize: this.pagination.pagesize,
        Keywords: this.searchform.keywords,
        Order: this.sortableData.order,
        Sort: this.sortableData.sort,
        Filter: t
      }
      getContractListWithPager(seachdata).then(res => {
        this.tableData = res.ResData.Items
        this.pagination.pageTotal = res.ResData.TotalItems
        this.tableloading = false
      })
    },
    /**
     * 點擊查詢
     */
    handleSearch: function() {
      this.pagination.currentPage = 1
      this.loadTableData()
    },

    /**
     * 新增、修改或查看明細信息（綁定顯示數據）     *
     */
    ShowEditOrViewDialog: function(view) {
      this.dialogEditFormVisible = false
      this.dialogContractFormVisible = false
      this.dialogBindContractAttachmentVisible = false
      this.reset()
      if (view !== undefined) {
        if (this.currentSelected.length > 1 || this.currentSelected.length === 0) {
          this.$alert('請選擇一條數據進行編輯/修改', '提示')
        } else {
          // console.log(this.currentSelected[0])
          this.currentId = this.currentSelected[0].Id
          this.currentCID = this.currentSelected[0].CID
          this.dialogContractFormVisible = true
          this.CT1Loading = true
          this.bindEditInfo(view)
        }
      } else {
        this.currentId = ''
        this.dialogEditFormVisible = true
      }
    },
    bindEditInfo: function(view) {
      this.TabAlias = view
      getContract(this.currentCID).then(res => {
        this.CT1Loading = false
        this.majorAttachmentTableloading = true
        this.minorAttachmentTableloading = true
        this.contractHistoryTableloading = true
        this.contractFlowLogTableloading = true
        this.editFrom = res.ResData
        if (this.editFrom.CCate) {
          getCategoryDetail(this.editFrom.CCate).then(res => {
            const category = res.ResData
            this.categoryFullPath = category.ParentId + '/' + category.Name
          })
        }

        this.editFrom.Id = this.currentId
        this.contractRelevantForm = this.editFrom.ContractRelevantInfo
        this.majorAttachmentTableloading = false
        this.minorAttachmentTableloading = false
        this.contractHistoryTableloading = false
        this.contractFlowLogTableloading = false

        var typeId = this.editFrom.CID + '-' + this.editFrom.Version
        getToDoListDetail2(typeId, this.editFrom.CStatus).then(res => {
          if (res.Success) {
            if (res.ErrMsg === 'Empty') {
              switch (this.editFrom.CStatus) {
                case '備審未簽名':
                  this.showUnsignedBtn = true
                  break;
                case '已簽名待放行':
                  this.showUnsignedBtn = true
                  this.showUploadSignedContractButton = true
                  break;
              }
            }
            // if (this.editFrom.CStatus === '已簽名待放行') {
            //   this.showUploadSignedContractButton = true
            // } else if () {

            // }
          }
        })
      })
    },
    /**
     * 新增/修改保存
     */
    saveEditForm() {
      this.$refs['editFrom'].validate((valid) => {
        if (valid) {
          if (this.editFrom.BAdd === '') {
            this.$alert('請填寫出租建物地址', '提示')
            return false
          }
          var url = 'Contract/Insert'
          if (this.currentId !== '') {
            url = 'Contract/Update'
          }
          saveContract(this.editFrom, url).then(res => {
            if (res.Success) {
              this.$message({
                message: '恭喜你，新增合約成功',
                type: 'success'
              })
              this.dialogEditFormVisible = false
              this.dialogContractFormVisible = false
              this.type1Visible = false
              this.currentSelected = ''
              this.loadTableData()
              this.InitDictItem()
            } else {
              this.$message({
                message: res.ErrMsg,
                type: 'error'
              })
            }
          })
        } else {
          return false
        }
      })
    },
    deletePhysics: function() {
      if (this.currentSelected.length === 0) {
        this.$alert('請先選擇要操作的數據', '提示')
        return false
      } else {
        var currentIds = []
        var unableIds = []
        this.currentSelected.forEach(element => {
          if (element.CStatus === '備審未簽名') {
            currentIds.push(element.Id)
          } else {
            unableIds.push(element.Id)
          }
        })

        if (unableIds.length > 0) {
          this.$alert('您只能刪除[備審未簽名]的合約，請確認。', '提示')
          return false
        } else if (currentIds.length > 0) {
          this.$confirm('是否確認刪除所選的數據項?', '警告', {
            confirmButtonText: '確定',
            cancelButtonText: '取消',
            type: 'warning'
          })
            .then(function() {
              const data = {
                Ids: currentIds
              }
              return deleteContract(data)
            })
            .then(res => {
              if (res.Success) {
                this.$message({
                  message: '恭喜你，刪除成功',
                  type: 'success'
                })
                this.currentSelected = ''
                this.loadTableData()
              } else {
                this.$message({
                  message: res.ErrMsg,
                  type: 'error'
                })
              }
            })
        }
      }
    },
    /**
     * 當表格的排序條件發生變化的時候會觸發該事件
     */
    handleSortChange: function(column) {
      this.sortableData.sort = column.prop
      if (column.order === 'ascending') {
        this.sortableData.order = 'asc'
      } else {
        this.sortableData.order = 'desc'
      }
      this.loadTableData()
    },
    /**
     * 當用戶手動勾選checkbox數據行事件
     */
    handleSelectChange: function(selection, row) {
      this.currentSelected = selection
    },
    /**
     * 當用戶手動勾選全選checkbox事件
     */
    handleSelectAllChange: function(selection) {
      this.currentSelected = selection
    },
    /**
     * 當用戶手動勾選checkbox數據行事件
     */
    handleMinorAttachmentSelectChange: function(selection, row) {
      this.currentMinorAttachmentSelected = selection
    },
    /**
     * 當用戶手動勾選全選checkbox事件
     */
    handleMinorAttachmentSelectAllChange: function(selection) {
      this.currentMinorAttachmentSelected = selection
    },
    /**
     * 當用戶手動勾選radio button數據行事件
     */
    handleBindAttachmentSelectChange: function(selection, row) {
      this.currentBindAttachmentSelected = selection
    },
    /**
     * 選擇每頁顯示數量
     */
    handleSizeChange(val) {
      this.pagination.pagesize = val
      this.pagination.currentPage = 1
      this.loadTableData()
    },
    /**
     * 選擇當頁面
     */
    handleCurrentChange(val) {
      this.pagination.currentPage = val
      this.loadTableData()
    },
    /**
     * 建物門牌地址
     */
    getsearchaddress(addressData) {
      this.searchform.BAdd_1 = addressData.Add_1
      this.searchform.BAdd_1_1 = addressData.Add_1_1
      this.searchform.BAdd_1_2 = addressData.Add_1_2
      this.searchform.BAdd_2 = addressData.Add_2
      this.searchform.BAdd_2_1 = addressData.Add_2_1
      this.searchform.BAdd_2_2 = addressData.Add_2_2
      this.searchform.BAdd_2_3 = addressData.Add_2_3
      this.searchform.BAdd_2_4 = addressData.Add_2_4
      this.searchform.BAdd_3 = addressData.Add_3
      this.searchform.BAdd_3_1 = addressData.Add_3_1
      this.searchform.BAdd_3_2 = addressData.Add_3_2
      this.searchform.BAdd_4 = addressData.Add_4
      this.searchform.BAdd_5 = addressData.Add_5
      this.searchform.BAdd_6 = addressData.Add_6
      this.searchform.BAdd_7 = addressData.Add_7
      this.searchform.BAdd_8 = addressData.Add_8
      this.searchform.BAdd_9 = addressData.Add_9
    },
    geteditaddress(addressData) {
      this.editFrom.BAdd_1 = addressData.Add_1
      this.editFrom.BAdd_1_1 = addressData.Add_1_1
      this.editFrom.BAdd_1_2 = addressData.Add_1_2
      this.editFrom.BAdd_2 = addressData.Add_2
      this.editFrom.BAdd_2_1 = addressData.Add_2_1
      this.editFrom.BAdd_2_2 = addressData.Add_2_2
      this.editFrom.BAdd_2_3 = addressData.Add_2_3
      this.editFrom.BAdd_2_4 = addressData.Add_2_4
      this.editFrom.BAdd_3 = addressData.Add_3
      this.editFrom.BAdd_3_1 = addressData.Add_3_1
      this.editFrom.BAdd_3_2 = addressData.Add_3_2
      this.editFrom.BAdd_4 = addressData.Add_4
      this.editFrom.BAdd_5 = addressData.Add_5
      this.editFrom.BAdd_6 = addressData.Add_6
      this.editFrom.BAdd_7 = addressData.Add_7
      this.editFrom.BAdd_8 = addressData.Add_8
      this.editFrom.BAdd_9 = addressData.Add_9
      const addarray = [
        this.editFrom.BAdd_1,
        this.editFrom.BAdd_1_1,
        this.editFrom.BAdd_1_2,
        this.editFrom.BAdd_2,
        this.editFrom.BAdd_2_1,
        this.editFrom.BAdd_2_2,
        this.editFrom.BAdd_2_3,
        this.editFrom.BAdd_2_4,
        this.editFrom.BAdd_3,
        this.editFrom.BAdd_3_1,
        this.editFrom.BAdd_3_2,
        this.editFrom.BAdd_4,
        this.editFrom.BAdd_5,
        this.editFrom.BAdd_6,
        this.editFrom.BAdd_7,
        this.editFrom.BAdd_8,
        this.editFrom.BAdd_9
      ]
      this.editFrom.BAdd = mergerAddress(addarray)
      getMOIDByBAdd(this.editFrom.BAdd).then(res => {
        if (res.Success) {
          const moInfo = res.ResData
          if (moInfo) {
            this.editFrom.MOID = moInfo.MOID
            this.$refs['MOID'].$el.querySelector('input').setAttribute('readonly', true)
            this.$refs['MOID'].$el.querySelector('input').removeAttribute('clearable')
            this.editFrom.MOName = moInfo.MOName
          } else {
            this.editFrom.MOID = ''
            this.$refs['MOID'].$el.querySelector('input').removeAttribute('readonly')
            this.editFrom.MOName = ''
          }
        }
      })
    },
    /**
     *選擇契約分類
     */
    handleSelectCategoryChange: function(value) {
      this.resetAddData()
      getCategoryDetail(value).then(res => {
        const category = res.ResData
        if (!category.ArchiveTo && category.Type) {
          if (this.editFrom.CID === '') {
            this.editFrom.CID = this.generateId(category.Id)
          }
        } else {
          this.$alert('請選擇正確的合約分類', '提示')
          this.editFrom.CCate = ''
          this.editFrom.CID = ''
          return false
        }

        // this.editFrom.CID = this.generateId(category.Id)
        this.editFrom.CName = category.Name
        this.editFrom.CCate = category.Id
        this.editFrom.CType = category.Type
        this.editFrom.NeedSalesSign = category.NeedSalesSign
        this.editFrom.NeedSupervisorSign = category.NeedSupervisorSign
        this.editFrom.NeedSignOnline = category.NeedSignOnline
      })
    },
    /**
     *產生合約編號
     */
    generateId(categoryId) {
      const ms = new Date().getMilliseconds()
      return categoryId + '-' + Math.ceil(Math.random() * 10000000) + ms
    },
    /**
     *雙擊開啟明細
     */
    rowdblclick(row, column, event) {
      this.$refs.gridtable.clearSelection()
      this.currentSelected = ''
      this.rerenderGridtable = Date.now() // 強制重繪 https://bbchin.com/archives/el-table-rerender
      this.$nextTick(function() {
        // https://ithelp.ithome.com.tw/articles/10240669
        this.$refs.gridtable.toggleRowSelection(row, true)
        this.currentSelected = this.$refs.gridtable.selection
        this.ShowEditOrViewDialog('CT1')
      })
      this.TabAlias = 'CT1'
    },
    downloadContract: function() {
      const data = {
        'CCate': this.editFrom.CCate,
        'CID': this.editFrom.CID,
        'Id': this.currentId
      }
      const label = data.CID
      // this.tableloading = true

      downloadContract(data).then(res => {
        if (res == null) {
          // this.$message({
          //   message: '下載失敗',
          //   type: 'error'
          // })
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
            this.$alert('請確認空白PDF(' + label + ')已上傳 ' + msg.ErrMsg, '下載失敗', {
              confirmButtonText: '確定'
            })
          }
          reader.readAsText(res)
          return
        }
        // item.ImgUrl = URL.createObjectURL(response)
        // this.tableloading = false
        const blob = res// new Blob([response.data], { type: 'image/jpeg' })
        const link = document.createElement('a')
        link.href = URL.createObjectURL(blob)
        link.download = label
        link.click()
        URL.revokeObjectURL(link.href)
      }).catch(error => {
        console.log(error)
      })
    },
    uploadsuccess: function(response, file, fileList) {
      if (response.Success) {
        this.dialogUploadContractVisible = false
        this.dialogUploadAttachmentVisible = false
        this.dialogUploadSignedContractVisible = false
        this.$message({
          message: file.name + ' 上傳成功',
          type: 'success'
        })
        // this.cancel()
        this.ShowEditOrViewDialog('CT1')
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
        )
      }
    },
    uploadsuccess2: function(response, file, fileList) {
      if (response.Success) {
        this.dialogUploadContractVisible = false
        this.dialogUploadAttachmentVisible = false
        this.dialogUploadSignedContractVisible = false
        this.minorAttachmentTableloading = true
        // this.cancel()
        // this.ShowEditOrViewDialog('CT3')
        getMinorAttachments(this.editFrom.CID).then(res => {
          if (res.Success) {
            this.editFrom.MinorAttachmentList = res.ResData
            this.minorAttachmentTableloading = false
            this.$message({
              message: file.name + ' 上傳成功',
              type: 'success'
            })
          }
        })
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
        )
      }
    },
    uploadsuccess3: function(response, file, fileList) {
      if (response.Success) {
        this.dialogUploadContractVisible = false
        this.dialogUploadAttachmentVisible = false
        this.dialogUploadSignedContractVisible = false
        this.contractHistoryTableloading = true
        // this.cancel()
        // this.ShowEditOrViewDialog('CT4')
        getContractHistory(this.editFrom.CID).then(res => {
          if (res.Success) {
            this.editFrom.ContractHistoryList = res.ResData
            this.contractHistoryTableloading = false
            this.$message({
              message: file.name + ' 上傳成功',
              type: 'success'
            })
          }
        })
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
        )
      }
    },
    uploaderror: function(err, file, fileList) {
      // console.log('uploaderror')
      // console.log(file)
      console.log(err)
      // console.log(file)
      // console.log(fileList)
      this.$message({
        message: file.name + ' 上傳失敗',
        type: 'error'
      })
    },
    uploadchange: function() {
      // console.log('uploadchange')
    },
    handleUploadBefore(file) {
      // console.log(file.name)
      // file.name = this.editFrom.FormId + 'pdf'
    },
    handleUploadBefore3(file) {

    },
    // /**
    //  *獲取二房東/管理方資料清單
    //  */
    getRemittanceTarget(value) {
      if (value) {
        switch (value) {
          case '出租人':
            this.editFrom.RemittanceType = ''
            this.AccountName = []
            this.editFrom.AccountName = ''
            this.UseCounty = []
            this.editFrom.UseCounty = ''
            this.BankName = []
            this.editFrom.BankName = ''
            GetRemittancesByIDNo(this.editFrom.LSID).then(res => {
              this.RemittanceL = res.ResData
            })
            break;
          case '承租人':
            this.editFrom.RemittanceType = ''
            this.AccountName = []
            this.editFrom.AccountName = ''
            this.UseCounty = []
            this.editFrom.UseCounty = ''
            this.BankName = []
            this.editFrom.BankName = ''
            GetRemittancesByIDNoR(this.editFrom.RNID).then(res => {
              this.RemittanceR = res.ResData
            })
            break;
          case '公司':
            this.RemittanceL = []
            this.RemittanceR = []
            this.editFrom.LAName = ''
            this.editFrom.RAName = ''
            //
            break;
        }
      }
    },
    getAccountNameByType(value) {
      if (value) {
        this.UseCounty = []
        this.editFrom.UseCounty = ''
        this.BankName = []
        this.editFrom.BankName = ''
        getAccountNameByType(value).then(res => {
          this.AccountName = res.ResData
        })
      }
    },
    getUseCountyByAccountName(value) {
      if (this.editFrom.RemittanceType && value) {
        this.BankName = []
        this.editFrom.BankName = ''
        getUseCountyByAccountName(this.editFrom.RemittanceType, value).then(res => {
          this.UseCounty = res.ResData
        })
      }
    },
    getBankNameByUseCounty(value) {
      if (this.editFrom.RemittanceType && this.editFrom.AccountNameC && value) {
        getBankNameByUseCounty(this.editFrom.RemittanceType, this.editFrom.AccountNameC, value).then(res => {
          this.BankName = res.ResData
        })
      }
    },
    /**
     *獲取二房東/管理方資料清單
     */
    getSLMAList(value) {
      this.editFrom.SIAdd = ''
      this.editFrom.SITel = ''
      this.editFrom.SILRNo = ''
      this.editFrom.AGLRNo = ''
      this.slmaid = value
      getSLMAList(value).then(res => {
        this.slma = res.ResData
      })
    },
    /**
     *獲取租賃住宅管理人員資料清單
     */
    getSIList(value) {
      this.editFrom.SITel = ''
      this.editFrom.SILRNo = ''
      this.editFrom.AGLRNo = ''
      this.agent = []

      getSLMATel(this.slmaid, value).then(res => {
        this.slmatel = res.ResData
      })
      getSIList(this.slmaid, value).then(res => {
        this.superintendent = res.ResData
      })
      getAGList(this.slmaid, value).then(res => {
        this.agent = res.ResData
      })
    },
    contractInvalid() {
      if (this.currentSelected.length > 1 || this.currentSelected.length === 0) {
        this.$alert('請選擇一份合約', '提示')
      } else {
        this.currentId = this.currentSelected[0].Id
        this.currentCID = this.currentSelected[0].CID
        getToDoListDetail3(this.currentCID).then(res => {
          if (res.Success) {
            if (res.ErrMsg === 'Empty') {
              if (this.currentSelected[0].CStatus === '備審未簽名') {
                const data = {
                  'CID': this.currentCID,
                  'BAdd': this.currentSelected[0].BAdd
                }
                contractInvalid(data).then(res => {
                  if (res.Success) {
                    this.$message({
                      message: '合約已作廢',
                      type: 'success'
                    })
                    this.loadTableData()
                    // this.InitDictItem()
                  } else {
                    this.$message({
                      message: res.ErrMsg,
                      type: 'error'
                    })
                  }
                })
              } else {
                // this.$alert('您只能作[廢備審未簽名]的合約，請確認。', '提示')
                this.$message({
                  message: '您只能作[廢備審未簽名]的合約，請確認。',
                  type: 'warning'
                })
                // return false
              }
            } else {
              this.$message({
                message: '合約待審中，無法作廢',
                type: 'warning'
              })
            }
          }
        })

        // if (this.currentSelected[0].CStatus === '備審未簽名') {

        // } else {
        //   this.$alert('您只能作[廢備審未簽名]的合約，請確認。', '提示')
        //   return false
        // }
      }
    },
    disableRadioButton(index, row) {
      if (row.ArchiveTo === '主約') {
        return true
      } else {
        return !this.showUnsignedBtn
      }
    },
    openBindAttachmentDialog(selection) {
      this.bindOption = -1
      if (selection && selection.ArchiveTo !== '主約') {
        this.currentMajorAttachmentSelected = selection
        this.dialogBindContractAttachmentVisible = true
        this.bindAttachmentFormLoading = true
        var tes = ''
        var seachFormdata = {
          CurrenetPageIndex: this.pagination.currentPage,
          PageSize: this.pagination.pagesize,
          Filter: {
            Note: tes
          },
          // Keywords: selection.ArchiveID,
          Order: this.sortableData.order,
          Sort: 'FileName'
        }

        switch (selection.ArchiveTo) {
          case '建物':
            // this.BID = selection.ArchiveID
            seachFormdata.Keywords = selection.ArchiveID
            getBuildingHistoryFormListWithPager(seachFormdata).then(res => {
              this.bindAttachmentFormData = res.ResData.Items
              this.bindAttachmentFormLoading = false
            })
            break
          case '出租人':
            seachFormdata.Filter.IDNo = selection.ArchiveID
            getLNHistoryFormListWithPager(seachFormdata).then(res => {
              this.bindAttachmentFormData = res.ResData.Items
              this.bindAttachmentFormLoading = false
            })
            break
          case '承租人':
            seachFormdata.Filter.IDNo = selection.ArchiveID
            getRNHistoryFormListWithPager(seachFormdata).then(res => {
              this.bindAttachmentFormData = res.ResData.Items
              this.bindAttachmentFormLoading = false
            })
            break
        }
      }
    },
    bindAttachment() {
      if (this.currentBindAttachmentSelected.length > 1 || this.currentBindAttachmentSelected.length === 0) {
        this.$alert('請選擇一份要件', '提示')
      } else {
        // const historyFormName = this.currentBindAttachmentSelected.FormName
        const attachmentFormName = this.currentMajorAttachmentSelected.FormName
        if (this.currentMajorAttachmentSelected.Status === '已綁定') {
          this.$message({
            message: '要件已綁定',
            type: 'error'
          })
          // } else if (historyFormName !== attachmentFormName) {
          //   this.$message({
          //     message: '綁定要件名稱不一致，請確認',
          //     type: 'error'
          //   })
        } else {
          const data = {
            'CID': this.editFrom.CID,
            'CType': this.editFrom.CType,
            'CCate': this.editFrom.CCate,
            'FileName': this.currentBindAttachmentSelected.FileName,
            'FormName': attachmentFormName,
            'ArchiveTo': this.currentMajorAttachmentSelected.ArchiveTo,
            'ArchiveID': this.currentMajorAttachmentSelected.ArchiveID
          }
          bindAttachment(data).then(res => {
            if (res.Success) {
              // this.cancel()
              // this.ShowEditOrViewDialog('CT3')
              // this.majorAttachmentTableloading = false;
              this.dialogBindContractAttachmentVisible = false
              this.majorAttachmentTableloading = true
              getMajorAttachments(this.editFrom.CID).then(res => {
                if (res.Success) {
                  this.editFrom.MajorAttachmentList = res.ResData
                  this.majorAttachmentTableloading = false
                  this.$message({
                    message: '綁定成功',
                    type: 'success'
                  })
                }
              })
            } else {
              this.$message({
                message: res.ErrMsg,
                type: 'error'
              })
            }
          })
        }
      }
    },
    unBindAttachment() {
      if (this.currentBindAttachmentSelected.length > 1 || this.currentBindAttachmentSelected.length === 0) {
        this.$alert('請選擇一份要件', '提示')
      } else {
        const attachmentFormName = this.currentMajorAttachmentSelected.FormName
        if (this.currentMajorAttachmentSelected.Status === '未綁定') {
          this.$message({
            message: '要件未綁定',
            type: 'error'
          })
        } else {
          const data = {
            'CID': this.editFrom.CID,
            'CType': this.editFrom.CType,
            'CCate': this.editFrom.CCate,
            'FormName': attachmentFormName
          }
          unbindAttachment(data).then(res => {
            if (res.Success) {
              // this.ShowEditOrViewDialog('CT3')
              this.dialogBindContractAttachmentVisible = false
              this.majorAttachmentTableloading = true
              getMajorAttachments(this.editFrom.CID).then(res => {
                if (res.Success) {
                  this.editFrom.MajorAttachmentList = res.ResData
                  this.majorAttachmentTableloading = false
                  this.$message({
                    message: '清除綁定成功',
                    type: 'success'
                  })
                }
              })
            } else {
              this.$message({
                message: res.ErrMsg,
                type: 'error'
              })
            }
          })
        }
      }
    },
    deleteAttachment() {
      if (this.currentMinorAttachmentSelected.length > 1 || this.currentMinorAttachmentSelected.length === 0) {
        this.$alert('請選擇一份次要附件', '提示')
      } else {
        const aid = this.currentMinorAttachmentSelected[0].Id
        const attachmentFormName = this.currentMinorAttachmentSelected[0].FormName

        const data = {
          'AID': aid,
          'CID': this.editFrom.CID,
          'CType': this.editFrom.CType,
          'CCate': this.editFrom.CCate,
          'AttachmentFormName': attachmentFormName
        }

        deleteMinorAttachment(data).then(res => {
          if (res.Success) {
            // this.ShowEditOrViewDialog('CT3')
            this.dialogUploadAttachmentVisible = false
            this.minorAttachmentTableloading = true
            getMinorAttachments(this.editFrom.CID).then(res => {
              if (res.Success) {
                this.editFrom.MinorAttachmentList = res.ResData
                this.minorAttachmentTableloading = false
                this.$message({
                  message: '次要附件刪除成功',
                  type: 'success'
                })
              }
            })
          } else {
            this.$message({
              message: res.ErrMsg,
              type: 'error'
            })
          }
        })
      }
    },
    finalized() {
      const checkStatus = this.editFrom.MajorAttachmentList.some(x => x.Status === '未綁定')
      if (checkStatus) {
        // this.$message({
        //   // message: '請確認所有要件是否綁定',
        //   message: '合約指定要件未全數綁定，無法送審或定稿！',
        //   type: 'error'
        // })
        this.$alert('合約指定要件未全數綁定，無法送審！', {
          confirmButtonText: '關閉'
        })
      } else {
        var typeId = this.editFrom.CID + '-' + this.editFrom.Version
        // getToDoListDetail2(typeId, this.editFrom.CStatus).then(res => {
        getToDoListDetail3(typeId).then(res => {
          if (res.Success) {
            if (res.ErrMsg === 'Empty' && this.editFrom.CStatus === '備審未簽名') {
              this.dialogNoteVisible = true
            } else if (this.editFrom.CStatus === '已簽名待放行') {
              this.$message({
                message: '狀態為[已簽名待放行]的合約無法送審',
                type: 'warning'
              })
            } else {
              this.$message({
                message: '合約待審中，無法重複送審',
                type: 'warning'
              })
            }
          }
        })
      }
    },
    contractFinalized() {
      this.$refs['noteForm'].validate((valid) => {
        if (valid) {
          this.dialogNoteLoading = true
          const data = {
            'BAdd': this.editFrom.BAdd,
            'CID': this.editFrom.CID,
            'CCObjectNo': this.editFrom.CCObjectNo,
            'CCate': this.editFrom.CCate,
            'CType': this.editFrom.CType,
            'CStatus': this.editFrom.CStatus,
            'Sales': this.editFrom.Sales,
            'SalesName': this.editFrom.SalesName,
            'Note': this.editFrom.Note,
            'Comment': this.editFrom.Comment
          }
          initialContract(data).then(res => {
            if (res.Success) {
              this.dialogNoteLoading = false
              this.dialogNoteVisible = false
              // this.ShowEditOrViewDialog('CT4')
              getContractHistory(this.editFrom.CID).then(res => {
                if (res.Success) {
                  this.editFrom.ContractHistoryList = res.ResData
                  this.contractHistoryTableloading = false
                  this.showUnsignedBtn = false
                  this.showUploadSignedContractButton = false
                  this.$message({
                    message: '合約已送審',
                    type: 'success'
                  })
                }
              })
            } else {
              this.$message({
                message: res.ErrMsg,
                type: 'error'
              })
            }
          })
        } else {
          return false
        }
      })
    },
    downloadFinalizedPDF(version, fileName) {
      // console.log(row.CName)
      const data = {
        'CID': this.editFrom.CID,
        'Version': version,
        'CCate': this.editFrom.CCate,
        'CType': this.editFrom.CType
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
        'CID': this.editFrom.CID,
        'Version': version,
        'CCate': this.editFrom.CCate,
        'CType': this.editFrom.CType
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
    // contractEffiective() {
    //   const data = {
    //     'CID': this.editFrom.CID,
    //     'CCate': this.editFrom.CCate,
    //     'CType': this.editFrom.CType
    //   }

    //   contractEffiective(data).then(res => {
    //     if (res.Success) {
    //       this.$message({
    //         message: '合約已生效',
    //         type: 'success'
    //       })
    //       this.loadTableData()
    //       // this.InitDictItem()
    //     } else {
    //       this.$message({
    //         message: res.ErrMsg,
    //         type: 'error'
    //       })
    //     }
    //   })
    // },
    updateMID() {
      updateMID(this.editFrom.CID, this.editFrom.MID).then(res => {
        if (res.Success) {
          this.$message({
            message: '媒合編號更新成功',
            type: 'success'
          })
          this.dialogUpdateMIDVisible = false
          this.ShowEditOrViewDialog('CT4')
        } else {
          this.$message({
            message: res.ErrMsg,
            type: 'error'
          })
        }
      })
    },
    terminateContract() {
      if (this.currentSelected.length > 1 || this.currentSelected.length === 0) {
        this.$alert('請選擇一份合約進行解約', '提示')
      } else {
        this.reset();
        this.currentId = this.currentSelected[0].Id
        this.currentCID = this.currentSelected[0].CID

        var typeId = this.currentCID + '-' + this.currentVersion
        getToDoListDetail3(typeId).then(res => {
          if (res.Success) {
            if (res.ErrMsg === 'Empty' && this.currentSelected[0].CStatus === '效期中') {
              this.dialogTerminateContractVisible = true
            } else {
              this.$message({
                message: '合約未生效或解約待審中，無法重複送審',
                type: 'warning'
              })
            }
          }
        })
      }
    },
    submitTerminateContract() {
      this.$refs['terminateContractForm'].validate((valid) => {
        if (valid) {
          var comment = this.editFrom.Comment
          getContract(this.currentCID).then(res => {
            this.editFrom = res.ResData
            this.editFrom.Comment = comment
            submitTerminateContract(this.editFrom).then(res => {
              if (res.Success) {
                this.$message({
                  message: '合約解約送審成功',
                  type: 'success'
                })
              } else {
                this.$message({
                  message: res.ErrMsg,
                  type: 'error'
                })
              }
            })
          })
        } else {
          return false
        }
      })
    },
    saveContractRelevantForm() {
      this.$refs['contractRelevantForm'].validate((valid) => {
        if (valid) {
          const data = {
            'Id': this.currentId,
            'CID': this.editFrom.CID,
            'B1TaxID': this.contractRelevantForm.B1TaxID,
            'RNRentSUBAFee': this.contractRelevantForm.RNRentSUBAFee,
            'RNQualify': this.contractRelevantForm.RNQualify
          }

          var url = 'Contract/UpdateRelevant'

          saveContract(data, url).then(res => {
            if (res.Success) {
              this.$message({
                message: '恭喜你，更新合約相關資料成功',
                type: 'success'
              })
            } else {
              this.$message({
                message: res.ErrMsg,
                type: 'error'
              })
            }
          })
        } else {
          return false
        }
      })
    },
    openSubmitSignedContract(cid, version) {
      this.currentCID = cid;
      this.currentVersion = (version) || '1';
      this.dialogSignedContractVisible = true;
    },
    submitSignedContract() {
      this.$refs['signedContractForm'].validate((valid) => {
        if (valid) {
          // console.log(cid, version, this.editFrom.CStatus, this.editFrom.CCObjectNo, this.editFrom.BAdd)
          const data = {
            'CID': this.currentCID,
            'Version': this.currentVersion,
            'CStatus': this.editFrom.CStatus,
            'Comment': this.editFrom.Comment,
            'CCObjectNo': this.editFrom.CCObjectNo,
            'BAdd': this.editFrom.BAdd,
            'ContractDate': this.editFrom.ContractDate
          }
          submitSignedContract(data).then(res => {
            if (res.Success) {
              this.dialogSignedContractVisible = false
              // this.ShowEditOrViewDialog('CT4')
              getContractHistory(this.editFrom.CID).then(res => {
                if (res.Success) {
                  this.editFrom.ContractHistoryList = res.ResData
                  this.contractHistoryTableloading = false
                  this.showUnsignedBtn = false
                  this.showUploadSignedContractButton = false
                  this.$message({
                    message: '合約已送審',
                    type: 'success'
                  })
                }
              })
            } else {
              this.$message({
                message: res.ErrMsg,
                type: 'error'
              })
            }
          })
        } else {
          return false
        }
      })
    },
    currencyFormatter(row, column, cellValue, index) {
      return cellValue ? new Intl.NumberFormat('en').format(cellValue) : '';
    }
  }
}
</script>

<style>

</style>
