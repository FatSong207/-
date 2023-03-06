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
          <el-form-item label="日誌日期區間：">
            <el-date-picker
              v-model="searchform.logDateRange"
              value-format="yyyy-MM-dd"
              type="daterange"
              range-separator="至"
            />
            <!-- <el-input
              v-model="searchform.keywords"
              clearable
              placeholder="名稱"
            /> -->
          </el-form-item>
          <el-form-item label="審核日期區間：">
            <el-date-picker
              v-model="searchform.AuditDateRange"
              value-format="yyyy-MM-dd"
              type="daterange"
              range-separator="至"
            />
            <!-- <el-input
              v-model="searchform.keywords"
              clearable
              placeholder="名稱"
            /> -->
          </el-form-item>
          <el-form-item label="主管備註：">
            <el-input
              v-model="searchform.StoreManagerNote"
              clearable
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
    </div>
    <el-card>
      <div class="list-btn-container">
        <el-button-group>
          <el-button
            v-hasPermi="['SalesWorkLog/Add']"
            type="primary"
            icon="el-icon-plus"
            size="small"
            @click="ShowEditOrViewDialog()"
          >新增</el-button>
          <el-button
            v-hasPermi="['SalesWorkLog/Edit']"
            type="primary"
            icon="el-icon-view"
            class="el-button-modify"
            size="small"
            @click="ShowEditOrViewDialog('edit')"
          >檢視</el-button>
          <el-button
            v-hasPermi="['SalesWorkLog/Delete']"
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
      >
        <el-table-column
          type="selection"
          width="40"
        />
        <el-table-column
          prop="BelongDeptName"
          label="隸屬部門"
          sortable="custom"
          width="220"
        />
        <el-table-column
          prop="SalesName"
          label="業務姓名"
          sortable="custom"
          width="120"
        />
        <!-- <el-table-column
          prop="SalesAccount"
          label="業務帳號"
          sortable="custom"
          width="120"
        /> -->
        <el-table-column
          prop="LogDate"
          label="日誌日期"
          sortable="custom"
          width="200"
        />
        <el-table-column
          prop="AuditSupervisorName"
          label="審核主管"
          sortable="custom"
          width="120"
        />
        <el-table-column
          prop="AuditTime"
          label="審核日期"
          sortable="custom"
          width="200"
        />
        <el-table-column
          prop="StoreManagerNote"
          label="主管備註"
          sortable="custom"
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
      v-el-drag-dialog
      :title="editFormTitle"
      :visible.sync="dialogEditFormVisible"
      append-to-body
      fullscreen
    >
      <el-form
        ref="editForm"
        :model="editForm"
        :rules="rules"
        inline
      >
        <el-row>
          <el-col :span="6">
            <el-form-item
              label="業務姓名："
              :label-width="formLabelWidth"
              prop="SalesName"
            >
              <el-input
                v-model="currentUser.RealName"
                disabled
              />
            </el-form-item>
          </el-col>
          <el-col :span="6">
            <el-form-item
              label="業務帳號："
              :label-width="formLabelWidth"
              prop="SalesAccount"
            >
              <el-input
                v-model="currentUser.Account"
                disabled
              />
            </el-form-item>
          </el-col>
          <el-col :span="6">
            <el-form-item
              label="隸屬部門："
              :label-width="formLabelWidth"
              prop="BelongDept"
            >
              <el-input
                v-model="currentUser.DepartmentId"
                disabled
              />
            </el-form-item>
          </el-col>
          <el-col :span="6">
            <el-form-item
              label="日誌日期："
              :label-width="formLabelWidth"
              prop="LogDate"
            >
              <el-input
                v-model="editForm.LogDate"
                disabled
              />
            </el-form-item>
          </el-col>
        </el-row>
        <h3>今日行程績效</h3>
        <el-row :gutter="20">
          <el-col :span="8">
            <el-card>
              <el-form-item
                label="聯繫開發："
                :label-width="formLabelWidth"
                prop="ContactDevelope"
              >
                <el-input
                  v-model="editForm.ContactDevelope"
                  placeholder="聯繫開發"
                  style="width:100px"
                  type="number"
                  class="no-number"
                  :min="0"
                  onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                  :readonly="!PermiToEdit"
                />
              </el-form-item>
              <el-form-item label="人" />
              <el-form-item
                label="招租來電："
                :label-width="formLabelWidth"
                prop="CallForRent"
              >
                <el-input
                  v-model="editForm.CallForRent"
                  placeholder="招租來電"
                  style="width:100px"
                  type="number"
                  class="no-number"
                  :min="0"
                  onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                  :readonly="!PermiToEdit"
                />
              </el-form-item>
              <el-form-item label="通" />
              <el-form-item
                label="房東場勘："
                :label-width="formLabelWidth"
                prop="LandlordSurvey"
              >
                <el-input
                  v-model="editForm.LandlordSurvey"
                  placeholder="房東場勘"
                  style="width:100px"
                  type="number"
                  class="no-number"
                  :min="0"
                  onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                  :readonly="!PermiToEdit"
                />
              </el-form-item>
              <el-form-item label="間" />
              <el-form-item
                label="房客帶看："
                :label-width="formLabelWidth"
                prop="RenterSurvey"
              >
                <el-input
                  v-model="editForm.RenterSurvey"
                  placeholder="房客帶看"
                  style="width:100px"
                  type="number"
                  class="no-number"
                  :min="0"
                  onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                  :readonly="!PermiToEdit"
                />
              </el-form-item>
              <el-form-item label="組" />
              <el-form-item
                label="房東委託："
                :label-width="formLabelWidth"
                prop="LandlordDelegate"
              >
                <el-input
                  v-model="editForm.LandlordDelegate"
                  placeholder="房東委託"
                  style="width:100px"
                  type="number"
                  class="no-number"
                  :min="0"
                  onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                  :readonly="!PermiToEdit"
                />
              </el-form-item>
              <el-form-item label="件" />
              <el-form-item
                label="房客收訂："
                :label-width="formLabelWidth"
                prop="RenterDeposit"
              >
                <el-input
                  v-model="editForm.RenterDeposit"
                  placeholder="房客收訂"
                  style="width:100px"
                  type="number"
                  class="no-number"
                  :min="0"
                  onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                  :readonly="!PermiToEdit"
                />
              </el-form-item>
              <el-form-item label="間" />
            </el-card>
          </el-col>
          <el-col :span="8">
            <el-card>
              <el-form-item
                label="維修派工："
                :label-width="formLabelWidth"
                prop="RepairDispatch"
              >
                <el-input
                  v-model="editForm.RepairDispatch"
                  placeholder="維修派工"
                  style="width:100px"
                  type="number"
                  class="no-number"
                  :min="0"
                  onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                  :readonly="!PermiToEdit"
                />
              </el-form-item>
              <el-form-item label="筆" />
              <el-form-item
                label="入住點交："
                :label-width="formLabelWidth"
                prop="CheckIn"
              >
                <el-input
                  v-model="editForm.CheckIn"
                  placeholder="入住點交"
                  style="width:100px"
                  type="number"
                  class="no-number"
                  :min="0"
                  onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                  :readonly="!PermiToEdit"
                />
              </el-form-item>
              <el-form-item label="間" />
              <el-form-item
                label="催收聯繫："
                :label-width="formLabelWidth"
                prop="CollectionContact"
              >
                <el-input
                  v-model="editForm.CollectionContact"
                  placeholder="催收聯繫"
                  style="width:100px"
                  type="number"
                  class="no-number"
                  :min="0"
                  onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                  :readonly="!PermiToEdit"
                />
              </el-form-item>
              <el-form-item label="筆" />
              <el-form-item
                label="退租點退："
                :label-width="formLabelWidth"
                prop="LeaseCheck"
              >
                <el-input
                  v-model="editForm.LeaseCheck"
                  placeholder="退租點退"
                  style="width:100px"
                  type="number"
                  class="no-number"
                  :min="0"
                  onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                  :readonly="!PermiToEdit"
                />
              </el-form-item>
              <el-form-item label="間" />
              <el-form-item
                label="清潔維護："
                :label-width="formLabelWidth"
                prop="Clean"
              >
                <el-input
                  v-model="editForm.Clean"
                  placeholder="清潔維護"
                  style="width:100px"
                  type="number"
                  class="no-number"
                  :min="0"
                  onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                  :readonly="!PermiToEdit"
                />
              </el-form-item>
              <el-form-item label="處" />
              <el-form-item
                label="水電抄表："
                :label-width="formLabelWidth"
                prop="Hydropwer"
              >
                <el-input
                  v-model="editForm.Hydropwer"
                  placeholder="水電抄表"
                  style="width:100px"
                  type="number"
                  class="no-number"
                  :min="0"
                  onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                  :readonly="!PermiToEdit"
                />
              </el-form-item>
              <el-form-item label="間" />
            </el-card>
          </el-col>
        </el-row>
        <h3>本日簽約成交</h3>
        <el-card>
          <span v-if="editFormTitle === '新增'||(editForm.States === 'first' && editForm.CreatorUserId === userId)">
            <el-form-item
              label="品項"
              prop="SupervisorNote"
            >
              <el-select
                v-model="deal.item"
                style="width: 120px"
              >
                <el-option
                  key="社會宅"
                  value="社會宅"
                >社會宅</el-option>
                <el-option
                  key="一般宅"
                  value="一般宅"
                >一般宅</el-option>
                <el-option
                  key="商辦"
                  value="商辦"
                >商辦</el-option>
                <el-option
                  key="買賣"
                  value="買賣"
                >買賣</el-option>
              </el-select>
              <!-- <el-input
              v-model="report.important"
              placeholder="請輸入本日重點工作事項"
              autocomplete="off"
              clearable
            /> -->
            </el-form-item>
            <el-form-item
              label="型態"
              prop="SupervisorNote"
            >
              <el-select
                v-model="deal.type"
                style="width: 120px"
              >
                <el-option
                  key="新成交"
                  value="新成交"
                >新成交</el-option>
                <el-option
                  key="再媒合"
                  value="再媒合"
                >再媒合</el-option>
                <el-option
                  key="續約"
                  value="續約"
                >續約</el-option>
                <el-option
                  key="轉期"
                  value="轉期"
                >轉期</el-option>
                <el-option
                  key="沒定"
                  value="沒定"
                >沒定</el-option>
              </el-select>
              <!-- <el-input
              v-model="report.finishState"
              placeholder="完成度"
              autocomplete="off"
              clearable
            /> -->
            </el-form-item>
            <el-form-item
              label="業績額"
              prop="SupervisorNote"
            >
              <el-input
                v-model="deal.performance"
                type="number"
                class="no-number"
                :min="0"
                style="width: 180px"
                show-word-limit
                onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
              />
            </el-form-item>
            <el-form-item
              label="地址"
              prop="SupervisorNote"
            >
              <Address
                :sendedform="sendedform"
                title="地址"
                @geteditaddress="geteditaddress"
              />
              <!-- <el-input
              v-model="deal.address"
              placeholder="地址"
              autocomplete="off"
              clearable
            /> -->
            </el-form-item>
            <div class="list-btn-container">
              <el-button-group>
                <el-button
                  type="primary"
                  size="small"
                  icon="el-icon-plus"
                  @click="AdddetailTableData()"
                >增加</el-button>
                <el-button
                  type="danger"
                  icon="el-icon-delete"
                  size="small"
                  @click="RemovedetailTableData()"
                >刪除</el-button>
              </el-button-group>
            </div>
          </span>
          <!-- <el-divider /> -->
          <el-row>
            <el-col :span="24">
              <el-table
                ref="detailTable"
                :data="detailTableData"
                border
                style="width: 70%"
                highlight-current-row
                @select="handleSelectChangeAdd"
                @select-all="handleSelectAllChangeAdd"
              >
                <el-table-column
                  type="selection"
                  width="40"
                />
                <el-table-column
                  prop="Item"
                  label="品項"
                  width="100"
                />
                <el-table-column
                  prop="Type"
                  label="型態"
                  width="100"
                />
                <el-table-column
                  prop="Performance"
                  label="業績額"
                  width="160"
                />
                <el-table-column
                  prop="Address"
                  label="地址"
                />
              </el-table>
            </el-col>
          </el-row>
        </el-card>
        <h3>本月總累積業績</h3>
        <el-card>
          <div
            v-if="PermiToEdit"
            class="list-btn-container"
          >
            <el-button-group>
              <el-button
                type="warning"
                size="small"
                icon="el-icon-refresh-left"
                @click="ResetLastData()"
              >累積業績歸零</el-button>
            </el-button-group>
          </div>
          <el-row :gutter="20">
            <el-col :span="9">
              <el-card>
                <el-form-item
                  label="招租庫存_社會宅："
                  :label-width="formLabelWidth"
                  prop="Storage_SH"
                >
                  <el-input
                    v-model="editForm.Storage_SH"
                    placeholder="社會宅"
                    style="width:100px"
                    type="number"
                    class="no-number"
                    :min="0"
                    onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                    :readonly="!PermiToEdit"
                  />
                </el-form-item>
                <el-form-item label="間" />
                <el-form-item
                  label="招租庫存_一般宅："
                  :label-width="formLabelWidth"
                  prop="Storage_NH"
                >
                  <el-input
                    v-model="editForm.Storage_NH"
                    placeholder="一般宅"
                    style="width:100px"
                    type="number"
                    class="no-number"
                    :min="0"
                    onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                    :readonly="!PermiToEdit"
                  />
                </el-form-item>
                <el-form-item label="間" />
                <el-form-item
                  label="房東場勘："
                  :label-width="formLabelWidth"
                  prop="Last_LandlordSurvey"
                >
                  <el-input
                    v-model="editForm.Last_LandlordSurvey"
                    placeholder="房東場勘"
                    style="width:100px"
                    type="number"
                    class="no-number"
                    :min="0"
                    onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                    :readonly="!PermiToEdit"
                  />
                </el-form-item>
                <el-form-item label="間" />
                <el-form-item
                  label="房東委託："
                  :label-width="formLabelWidth"
                  prop="Last_LandlordDelegate"
                >
                  <el-input
                    v-model="editForm.Last_LandlordDelegate"
                    placeholder="房東委託"
                    style="width:100px"
                    type="number"
                    class="no-number"
                    :min="0"
                    onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                    :readonly="!PermiToEdit"
                  />
                </el-form-item>
                <el-form-item label="間" />
                <el-form-item
                  label="房客帶看："
                  :label-width="formLabelWidth"
                  prop="Last_RenterSurvey"
                >
                  <el-input
                    v-model="editForm.Last_RenterSurvey"
                    placeholder="房客帶看"
                    style="width:100px"
                    type="number"
                    class="no-number"
                    :min="0"
                    onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                    :readonly="!PermiToEdit"
                  />
                </el-form-item>
                <el-form-item label="組" />
                <el-form-item
                  label="房客收訂："
                  :label-width="formLabelWidth"
                  prop="Last_RenterDeposit"
                >
                  <el-input
                    v-model="editForm.Last_RenterDeposit"
                    placeholder="房客收訂"
                    style="width:100px"
                    type="number"
                    class="no-number"
                    :min="0"
                    onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                    :readonly="!PermiToEdit"
                  />
                </el-form-item>
                <el-form-item label="間" />
              </el-card>
            </el-col>
            <el-col :span="9">
              <el-card>
                <el-form-item
                  label="維修派工："
                  :label-width="formLabelWidth"
                  prop="Last_RepairDispatch"
                >
                  <el-input
                    v-model="editForm.Last_RepairDispatch"
                    placeholder="維修派工"
                    style="width:100px"
                    type="number"
                    class="no-number"
                    :min="0"
                    onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                    :readonly="!PermiToEdit"
                  />
                </el-form-item>
                <el-form-item label="筆" />
                <el-form-item
                  label="入住點交："
                  :label-width="formLabelWidth"
                  prop="Last_CheckIn"
                >
                  <el-input
                    v-model="editForm.Last_CheckIn"
                    placeholder="入住點交"
                    style="width:100px"
                    type="number"
                    class="no-number"
                    :min="0"
                    onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                    :readonly="!PermiToEdit"
                  />
                </el-form-item>
                <el-form-item label="間" />
                <el-form-item
                  label="催收聯繫："
                  :label-width="formLabelWidth"
                  prop="Last_CollectionContact"
                >
                  <el-input
                    v-model="editForm.Last_CollectionContact"
                    placeholder="催收聯繫"
                    style="width:100px"
                    type="number"
                    class="no-number"
                    :min="0"
                    onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                    :readonly="!PermiToEdit"
                  />
                </el-form-item>
                <el-form-item label="筆" />
                <el-form-item
                  label="退租點退："
                  :label-width="formLabelWidth"
                  prop="Last_LeaseCheck"
                >
                  <el-input
                    v-model="editForm.Last_LeaseCheck"
                    placeholder="退租點退"
                    style="width:100px"
                    type="number"
                    class="no-number"
                    :min="0"
                    onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                    :readonly="!PermiToEdit"
                  />
                </el-form-item>
                <el-form-item label="間" />
                <el-form-item
                  label="清潔維護："
                  :label-width="formLabelWidth"
                  prop="Last_Clean"
                >
                  <el-input
                    v-model="editForm.Last_Clean"
                    placeholder="清潔維護"
                    style="width:100px"
                    type="number"
                    class="no-number"
                    :min="0"
                    onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                    :readonly="!PermiToEdit"
                  />
                </el-form-item>
                <el-form-item label="處" />
                <el-form-item
                  label="水電抄表："
                  :label-width="formLabelWidth"
                  prop="Last_Hydropwer"
                >
                  <el-input
                    v-model="editForm.Last_Hydropwer"
                    placeholder="水電抄表"
                    style="width:100px"
                    type="number"
                    class="no-number"
                    :min="0"
                    onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                    :readonly="!PermiToEdit"
                  />
                </el-form-item>
                <el-form-item label="間" />
              </el-card>
            </el-col>
          </el-row>
          <br>
          <el-table
            :data="dealList"
            border
            stripe
            style="width:50%"
          >
            <el-table-column
              prop="Title"
              :resizable="false"
              label="成交列表"
            />
            <el-table-column
              prop="Performance"
              :resizable="false"
              label="業績額"
              width="160"
            >
              <template slot-scope="scope">
                <el-input
                  v-model="scope.row.Performance"
                  type="number"
                  class="no-number"
                  :min="0"
                  style="width: 100%"
                  show-word-limit
                  onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                  :readonly="!PermiToEdit"
                  placeholder="元"
                />
              </template>
            </el-table-column>
            <el-table-column
              prop="New"
              :resizable="false"
              label="新成交"
              width="120"
            >
              <template slot-scope="scope">
                <el-input
                  v-model="scope.row.New"
                  style="width: 100%"
                  :readonly="!PermiToEdit"
                  placeholder="件"
                />
              </template>
            </el-table-column>
            <el-table-column
              prop="New"
              :resizable="false"
              label="再媒合"
              width="120"
            >
              <template slot-scope="scope">
                <el-input
                  v-model="scope.row.Match"
                  style="width: 100%"
                  :readonly="!PermiToEdit"
                  placeholder="件"
                />
              </template>
            </el-table-column>
            <el-table-column
              prop="New"
              :resizable="false"
              label="續約"
              width="120"
            >
              <template slot-scope="scope">
                <el-input
                  v-model="scope.row.Continue"
                  style="width: 100%"
                  :readonly="!PermiToEdit"
                  placeholder="件"
                />
              </template>
            </el-table-column>
            <el-table-column
              prop="New"
              :resizable="false"
              label="沒定"
              width="120"
            >
              <template slot-scope="scope">
                <el-input
                  v-model="scope.row.Nothing"
                  type="number"
                  class="no-number"
                  :min="0"
                  style="width: 100%"
                  show-word-limit
                  onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                  :readonly="!PermiToEdit"
                  placeholder="元"
                />
              </template>
            </el-table-column>
            <el-table-column
              prop="New"
              :resizable="false"
              label="轉期"
              width="120"
            >
              <template slot-scope="scope">
                <el-input
                  v-model="scope.row.Transfer"
                  style="width: 100%"
                  :readonly="!PermiToEdit"
                  placeholder="件"
                />
              </template>
            </el-table-column>
          </el-table>
        </el-card>
        <br>
        <el-row :gutter="14">
          <el-col :span="8">
            <el-card>
              <el-form-item
                label="行程回報："
                prop="ScheduleReport"
              >
                <el-input
                  v-model="editForm.ScheduleReport"
                  type="textarea"
                  :rows="5"
                  style="width:400px"
                  placeholder="請輸入內容"
                  :readonly="!PermiToEdit"
                  :disabled="!(editFormTitle === '新增'||(editForm.States === 'first' && editForm.CreatorUserId === userId))"
                />
              </el-form-item>
            </el-card>
          </el-col>
          <el-col :span="8">
            <el-card>
              <el-form-item
                label="店長備註："
                prop="StoreManagerNote"
              >
                <el-input
                  v-model="editForm.StoreManagerNote"
                  type="textarea"
                  :rows="5"
                  style="width:400px"
                  placeholder="請輸入內容"
                  :disabled="!IsShowApprovalBtn"
                />
              </el-form-item>
            </el-card>
          </el-col>
          <el-col :span="8">
            <el-card>
              <el-form-item
                label="待辦事項："
                prop="TodoNote"
              >
                <el-input
                  v-model="editForm.TodoNote"
                  type="textarea"
                  :rows="5"
                  style="width:400px"
                  placeholder="請輸入內容"
                  :readonly="!PermiToEdit"
                  :disabled="!(editFormTitle === '新增'||(editForm.States === 'first' && editForm.CreatorUserId === userId))"
                />
              </el-form-item>
            </el-card>
          </el-col>
        </el-row>
      </el-form>
      <div
        slot="footer"
        class="dialog-footer"
      >
        <el-button
          v-if="editFormTitle === '新增'||(editForm.States === 'first' && editForm.CreatorUserId === userId)"
          type="primary"
          size="small"
          icon="el-icon-paperclip"
          @click="saveEditForm()"
        >儲存</el-button>
        <el-button
          v-if="editFormTitle === '新增'||(editForm.States === 'first' && editForm.CreatorUserId === userId)"
          type="primary"
          size="small"
          icon="el-icon-s-promotion"
          @click="saveEditForm('sendtrail')"
        >送審</el-button>
        <el-button
          v-if="IsShowApprovalBtn"
          type="primary"
          size="small"
          icon="el-icon-close"
          @click="saveEditForm('approval')"
        >放行</el-button>
        <el-button
          size="small"
          icon="el-icon-close"
          @click="cancel"
        >關閉</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import {
  getSalesWorkLogListWithPager,
  getSalesWorkLogDetail,
  saveSalesWorkLog,
  deleteSalesWorkLog,
  GetLastDeals
} from '@/api/chaochi/salesworklog/salesworklog';
import { getUserDetail } from '@/api/security/userservice';
import { mergerAddress } from '@/utils/index';

import Address from '@/components/Address/Address.vue';

import elDragDialog from '@/directive/el-drag-dialog'; // 彈窗可移動
import { mapGetters } from 'vuex';

export default {
  name: 'SalesWorkLog', // 需與菜單的功能編碼一致
  components: { Address },
  directives: { elDragDialog },
  data() {
    return {
      deal: {},
      editForm: {},
      rules: {},
      currentUser: {},
      sendedform: {},
      pagination: {
        currentPage: 1,
        pagesize: 20,
        pageTotal: 0
      },
      searchform: {
        keywords: '',
        logDateRange: [],
        AuditDateRange: []
      },
      sortableData: {
        order: 'desc',
        sort: 'CreatorTime'
      },
      tableData: [],
      currentSelected: [],
      detailTableData: [],
      currentSelectedAdd: [],
      dealList: [
        {
          Title: '社會宅',
          Category: 'SH',
          Performance: '',
          New: '',
          Match: '',
          Continue: '',
          Nothing: '',
          Transfer: ''
        },
        {
          Title: '一般宅',
          Category: 'NH',
          Performance: '',
          New: '',
          Match: '',
          Continue: '',
          Nothing: '',
          Transfer: ''
        },
        {
          Title: '商辦/買賣',
          Category: 'ST',
          Performance: '',
          New: '',
          Match: '',
          Continue: '',
          Nothing: '',
          Transfer: ''
        }
      ],
      editFormTitle: '',
      formLabelWidth: '135px',
      currentId: '', // 當前操作對象的ID值，主要用于修改
      tableloading: true,
      dialogEditFormVisible: false
    };
  },
  computed: {
    ...mapGetters(['userId']),
    IsShowApprovalBtn() {
      return (
        this.userId === this.editForm.AuditSupervisor &&
        this.editForm.States !== 'approval'
      );
    },
    PermiToEdit() {
      return (
        this.editFormTitle === '新增' ||
        (this.editForm.CreatorUserId === this.userId &&
          this.editForm.States === 'first')
      );
    }
  },
  created() {
    this.pagination.currentPage = 1;
    getUserDetail(this.userId).then(res => {
      if (res.Success) {
        this.currentUser = res.ResData;
      }
    });
    this.InitDictItem();
    this.loadTableData();
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
    AdddetailTableData() {
      if (!this.deal.item) {
        this.$message.error('請選擇品項');
        return;
      }
      if (!this.deal.type) {
        this.$message.error('請選擇型態');
        return;
      }
      if (!this.deal.performance) {
        this.$message.error('請輸入業績額');
        return;
      }
      if (!this.editForm.Add_1) {
        this.$message.error('請輸入地址');
        return;
      }
      const addarray = [
        this.editForm.Add_1,
        this.editForm.Add_1_1,
        this.editForm.Add_1_2,
        this.editForm.Add_2,
        this.editForm.Add_2_1,
        this.editForm.Add_2_2,
        this.editForm.Add_2_3,
        this.editForm.Add_2_4,
        this.editForm.Add_3,
        this.editForm.Add_3_1,
        this.editForm.Add_3_2,
        this.editForm.Add_4,
        this.editForm.Add_5,
        this.editForm.Add_6,
        this.editForm.Add_7,
        this.editForm.Add_8,
        this.editForm.Add_9
      ];
      var data = {
        Item: this.deal.item,
        Type: this.deal.type,
        Performance: this.deal.performance,
        Address: mergerAddress(addarray)
      };
      this.detailTableData.push(data);
      this.deal.item = '';
      this.deal.type = '';
      this.deal.performance = '';
      this.sendedform = {};
    },
    RemovedetailTableData() {
      var currentAdds = [];
      this.currentSelectedAdd.forEach(element => {
        currentAdds.push(element);
      });
      this.detailTableData = this.detailTableData.filter(
        el => !currentAdds.includes(el)
      );
      this.currentSelectedAdd = [];
    },
    ResetLastData() {
      this.$confirm('此操作將歸零下方累積業績，是否繼續?', '警告', {
        confirmButtonText: '確定',
        cancelButtonText: '取消',
        type: 'warning'
      })
        .then(() => {
          this.editForm = {
            Storage_SH: 0,
            Storage_NH: 0,
            Last_LandlordSurvey: 0,
            Last_LandlordDelegate: 0,
            Last_RenterSurvey: 0,
            Last_RenterDeposit: 0,
            Last_RepairDispatch: 0,
            Last_CheckIn: 0,
            Last_CollectionContact: 0,
            Last_LeaseCheck: 0,
            Last_Clean: 0,
            Last_Hydropwer: 0
          };
          this.dealList = [
            {
              Title: '社會宅',
              Category: 'SH',
              Performance: '',
              New: '',
              Match: '',
              Continue: '',
              Nothing: '',
              Transfer: ''
            },
            {
              Title: '一般宅',
              Category: 'NH',
              Performance: '',
              New: '',
              Match: '',
              Continue: '',
              Nothing: '',
              Transfer: ''
            },
            {
              Title: '商辦/買賣',
              Category: 'ST',
              Performance: '',
              New: '',
              Match: '',
              Continue: '',
              Nothing: '',
              Transfer: ''
            }
          ];
        })
        .catch(() => {});
    },
    // 表單重置
    reset() {
      this.editForm = {
        BelongDept: '',
        SalesName: '',
        SalesAccount: '',
        LogDate: '',
        AuditSupervisor: '',
        AuditTime: '',
        StoreManagerNote: '',
        ContactDevelope: '',
        CallForRent: '',
        LandlordSurvey: '',
        RenterSurvey: '',
        LandlordDelegate: '',
        RenterDeposit: '',
        RepairDispatch: '',
        CheckIn: '',
        CollectionContact: '',
        LeaseCheck: '',
        Clean: '',
        Hydropwer: '',
        Storage_SH: '',
        Storage_NH: '',
        Last_LandlordSurvey: '',
        Last_LandlordDelegate: '',
        Last_RenterSurvey: '',
        Last_RenterDeposit: '',
        Last_RepairDispatch: '',
        Last_CheckIn: '',
        Last_CollectionContact: '',
        Last_LeaseCheck: '',
        Last_Clean: '',
        Last_Hydropwer: '',
        SH_Performance: '',
        SH_New: '',
        SH_Match: '',
        SH_Continue: '',
        SH_Nothing: '',
        SH_Transfer: '',
        NH_Performance: '',
        NH_New: '',
        NH_Match: '',
        NH_Continue: '',
        NH_Nothing: '',
        NH_Transfer: '',
        ST_Performance: '',
        ST_New: '',
        ST_Match: '',
        ST_Continue: '',
        ST_Nothing: '',
        ST_Transfer: '',
        ScheduleReport: '',
        TodoNote: '',
        CreatorTime: '',
        CreatorUserId: '',
        CreatorUserDeptId: '',
        LastModifyTime: '',
        LastModifyUserId: ''

        // 需個性化處理內容
      };
      this.dealList = [
        {
          Title: '社會宅',
          Category: 'SH',
          Performance: '',
          New: '',
          Match: '',
          Continue: '',
          Nothing: '',
          Transfer: ''
        },
        {
          Title: '一般宅',
          Category: 'NH',
          Performance: '',
          New: '',
          Match: '',
          Continue: '',
          Nothing: '',
          Transfer: ''
        },
        {
          Title: '商辦/買賣',
          Category: 'ST',
          Performance: '',
          New: '',
          Match: '',
          Continue: '',
          Nothing: '',
          Transfer: ''
        }
      ];

      this.detailTableData = [];
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
        logDateRange:
          this.searchform.logDateRange.length > 0
            ? this.searchform.logDateRange[0] +
              ',' +
              this.searchform.logDateRange[1]
            : '',
        AuditDateRange:
          this.searchform.AuditDateRange.length > 0
            ? this.searchform.AuditDateRange[0] +
              ',' +
              this.searchform.AuditDateRange[1]
            : '',
        StoreManagerNote: this.searchform.StoreManagerNote,
        Order: this.sortableData.order,
        Sort: this.sortableData.sort
      };
      getSalesWorkLogListWithPager(seachdata).then(res => {
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
        // 新增時要先去獲取此用戶最近一筆的資料
        GetLastDeals().then(res => {
          if (res.ResData) {
            this.editForm.Storage_SH = res.ResData.Storage_SH;
            this.editForm.Storage_NH = res.ResData.Storage_NH;
            this.editForm.Last_LandlordSurvey = res.ResData.Last_LandlordSurvey;
            this.editForm.Last_LandlordDelegate =
              res.ResData.Last_LandlordDelegate;
            this.editForm.Last_RenterSurvey = res.ResData.Last_RenterSurvey;
            this.editForm.Last_RenterDeposit = res.ResData.Last_RenterSurvey;
            this.editForm.Last_RepairDispatch = res.ResData.Last_RepairDispatch;
            this.editForm.Last_CheckIn = res.ResData.Last_CheckIn;
            this.editForm.Last_CollectionContact =
              res.ResData.Last_CollectionContact;
            this.editForm.Last_LeaseCheck = res.ResData.Last_LeaseCheck;
            this.editForm.Last_Clean = res.ResData.Last_Clean;
            this.editForm.Last_Hydropwer = res.ResData.Last_Hydropwer;
            this.dealList.forEach(deal => {
              switch (deal.Category) {
                case 'SH':
                  deal.Performance = res.ResData.SH_Performance;
                  deal.New = res.ResData.SH_New;
                  deal.Match = res.ResData.SH_Match;
                  deal.Continue = res.ResData.SH_Continue;
                  deal.Nothing = res.ResData.SH_Nothing;
                  deal.Transfer = res.ResData.SH_Transfer;
                  break;
                case 'NH':
                  deal.Performance = res.ResData.NH_Performance;
                  deal.New = res.ResData.NH_New;
                  deal.Match = res.ResData.NH_Match;
                  deal.Continue = res.ResData.NH_Continue;
                  deal.Nothing = res.ResData.NH_Nothing;
                  deal.Transfer = res.ResData.NH_Transfer;
                  break;
                case 'ST':
                  deal.Performance = res.ResData.ST_Performance;
                  deal.New = res.ResData.ST_New;
                  deal.Match = res.ResData.ST_Match;
                  deal.Continue = res.ResData.ST_Continue;
                  deal.Nothing = res.ResData.ST_Nothing;
                  deal.Transfer = res.ResData.ST_Transfer;
                  break;
              }
            });
          }
        });
        this.currentId = '';
        this.dialogEditFormVisible = true;
        this.sendedform = {};
      }
    },
    bindEditInfo: function() {
      getSalesWorkLogDetail(this.currentId).then(res => {
        this.editForm = res.ResData;
        this.detailTableData = res.ResData.SalesWorkLogDetails;
        this.dealList.forEach(deal => {
          switch (deal.Category) {
            case 'SH':
              deal.Performance = res.ResData.SH_Performance;
              deal.New = res.ResData.SH_New;
              deal.Match = res.ResData.SH_Match;
              deal.Continue = res.ResData.SH_Continue;
              deal.Nothing = res.ResData.SH_Nothing;
              deal.Transfer = res.ResData.SH_Transfer;
              break;
            case 'NH':
              deal.Performance = res.ResData.NH_Performance;
              deal.New = res.ResData.NH_New;
              deal.Match = res.ResData.NH_Match;
              deal.Continue = res.ResData.NH_Continue;
              deal.Nothing = res.ResData.NH_Nothing;
              deal.Transfer = res.ResData.NH_Transfer;
              break;
            case 'ST':
              deal.Performance = res.ResData.ST_Performance;
              deal.New = res.ResData.ST_New;
              deal.Match = res.ResData.ST_Match;
              deal.Continue = res.ResData.ST_Continue;
              deal.Nothing = res.ResData.ST_Nothing;
              deal.Transfer = res.ResData.ST_Transfer;
              break;
          }
        });
        this.currentUser.RealName = res.ResData.SalesName;
        this.currentUser.Account = res.ResData.SalesAccount;
        this.currentUser.DepartmentId = res.ResData.BelongDept;

        this.sendedform = {
          Add_1: this.editForm.Add_1,
          Add_1_1: this.editForm.Add_1_1,
          Add_1_2: this.editForm.Add_1_2,
          Add_2: this.editForm.Add_2,
          Add_2_1: this.editForm.Add_2_1,
          Add_2_2: this.editForm.Add_2_2,
          Add_2_3: this.editForm.Add_2_3,
          Add_2_4: this.editForm.Add_2_4,
          Add_3: this.editForm.Add_3,
          Add_3_1: this.editForm.Add_3_1,
          Add_3_2: this.editForm.Add_3_2,
          Add_4: this.editForm.Add_4,
          Add_5: this.editForm.Add_5,
          Add_6: this.editForm.Add_6,
          Add_7: this.editForm.Add_7,
          Add_8: this.editForm.Add_8,
          Add_9: this.editForm.Add_9
        };
      });
    },
    /**
     * 新增/修改保存
     */
    saveEditForm(action) {
      this.$refs['editForm'].validate(valid => {
        if (valid) {
          const data = {
            BelongDept: this.editForm.BelongDept,
            SalesName: this.editForm.SalesName,
            SalesAccount: this.editForm.SalesAccount,
            LogDate: this.editForm.LogDate,
            AuditSupervisor: this.editForm.AuditSupervisor,
            AuditTime: this.editForm.AuditTime,
            StoreManagerNote: this.editForm.StoreManagerNote,
            ContactDevelope: this.editForm.ContactDevelope,
            CallForRent: this.editForm.CallForRent,
            LandlordSurvey: this.editForm.LandlordSurvey,
            RenterSurvey: this.editForm.RenterSurvey,
            LandlordDelegate: this.editForm.LandlordDelegate,
            RenterDeposit: this.editForm.RenterDeposit,
            RepairDispatch: this.editForm.RepairDispatch,
            CheckIn: this.editForm.CheckIn,
            CollectionContact: this.editForm.CollectionContact,
            LeaseCheck: this.editForm.LeaseCheck,
            Clean: this.editForm.Clean,
            Hydropwer: this.editForm.Hydropwer,
            Storage_SH: this.editForm.Storage_SH,
            Storage_NH: this.editForm.Storage_NH,
            Last_LandlordSurvey: this.editForm.Last_LandlordSurvey,
            Last_LandlordDelegate: this.editForm.Last_LandlordDelegate,
            Last_RenterSurvey: this.editForm.Last_RenterSurvey,
            Last_RenterDeposit: this.editForm.Last_RenterDeposit,
            Last_RepairDispatch: this.editForm.Last_RepairDispatch,
            Last_CheckIn: this.editForm.Last_CheckIn,
            Last_CollectionContact: this.editForm.Last_CollectionContact,
            Last_LeaseCheck: this.editForm.Last_LeaseCheck,
            Last_Clean: this.editForm.Last_Clean,
            Last_Hydropwer: this.editForm.Last_Hydropwer,
            SH_Performance: this.editForm.SH_Performance,
            SH_New: this.editForm.SH_New,
            SH_Match: this.editForm.SH_Match,
            SH_Continue: this.editForm.SH_Continue,
            SH_Nothing: this.editForm.SH_Nothing,
            SH_Transfer: this.editForm.SH_Transfer,
            NH_Performance: this.editForm.NH_Performance,
            NH_New: this.editForm.NH_New,
            NH_Match: this.editForm.NH_Match,
            NH_Continue: this.editForm.NH_Continue,
            NH_Nothing: this.editForm.NH_Nothing,
            NH_Transfer: this.editForm.NH_Transfer,
            ST_Performance: this.editForm.ST_Performance,
            ST_New: this.editForm.ST_New,
            ST_Match: this.editForm.ST_Match,
            ST_Continue: this.editForm.ST_Continue,
            ST_Nothing: this.editForm.ST_Nothing,
            ST_Transfer: this.editForm.ST_Transfer,
            ScheduleReport: this.editForm.ScheduleReport,
            TodoNote: this.editForm.TodoNote,
            States: this.editForm.States,
            CreatorTime: this.editForm.CreatorTime,
            CreatorUserId: this.editForm.CreatorUserId,
            CreatorUserDeptId: this.editForm.CreatorUserDeptId,
            LastModifyTime: this.editForm.LastModifyTime,
            LastModifyUserId: this.editForm.LastModifyUserId,

            SalesWorkLogDetails: this.detailTableData,
            Deals: this.dealList,

            Id: this.currentId
          };

          if (action) {
            switch (action) {
              case 'sendtrail':
                console.log('states');
                data.States = 'sendtrail';
                break;
              case 'approval':
                data.States = 'approval';
                break;
            }
          }

          var url = 'SalesWorkLog/Insert';
          if (this.currentId !== '') {
            url = 'SalesWorkLog/UpdateAsync';
          }
          console.log(data);
          saveSalesWorkLog(data, url).then(res => {
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
            return deleteSalesWorkLog(data);
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
    handleSelectChangeAdd: function(selection, row) {
      this.currentSelectedAdd = selection;
    },
    handleSelectAllChangeAdd: function(selection) {
      this.currentSelectedAdd = selection;
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
    geteditaddress(addressData, title) {
      if (title === '地址') {
        this.editForm.Add_1 = addressData.Add_1;
        this.editForm.Add_1_1 = addressData.Add_1_1;
        this.editForm.Add_1_2 = addressData.Add_1_2;
        this.editForm.Add_2 = addressData.Add_2;
        this.editForm.Add_2_1 = addressData.Add_2_1;
        this.editForm.Add_2_2 = addressData.Add_2_2;
        this.editForm.Add_2_3 = addressData.Add_2_3;
        this.editForm.Add_2_4 = addressData.Add_2_4;
        this.editForm.Add_3 = addressData.Add_3;
        this.editForm.Add_3_1 = addressData.Add_3_1;
        this.editForm.Add_3_2 = addressData.Add_3_2;
        this.editForm.Add_4 = addressData.Add_4;
        this.editForm.Add_5 = addressData.Add_5;
        this.editForm.Add_6 = addressData.Add_6;
        this.editForm.Add_7 = addressData.Add_7;
        this.editForm.Add_8 = addressData.Add_8;
        this.editForm.Add_9 = addressData.Add_9;
      }
    }
  }
};
</script>

<style>
</style>
