<template>
  <div class="app-container">
    <div class="filter-container">
      <el-card>
        <el-form
          ref="searchform"
          :inline="true" :model="searchform" class="demo-form-inline" size="small"
        >
          <el-form-item label="姓名：">
            <el-input
              v-model="searchform.Name"
              clearable placeholder="姓名"
            />
          </el-form-item>
          <el-form-item label="手機號碼：">
            <el-input
              v-model="searchform.Cell"
              clearable placeholder="手機號碼"
            />
          </el-form-item>
          <el-form-item label="身份：">
            <el-select
              v-model="searchform.Identity"
              placeholder="請選擇" clearable
            >
              <el-option
                v-for="item in Identity"
                :key="item.value" :label="item.label" :value="item.value"
              />
            </el-select>
          </el-form-item>
          <el-form-item label="自然人或法人：">
            <el-select
              v-model="searchform.Identity2"
              placeholder="請選擇" clearable
            >
              <el-option
                v-for="item in Identity2"
                :key="item.value" :label="item.label" :value="item.value"
              />
            </el-select>
          </el-form-item>
          <br>
          <el-form-item label="來源：">
            <el-select
              v-model="searchform.Source"
              placeholder="請選擇" clearable
            >
              <el-option
                v-for="item in SourceSearch"
                :key="item.value" :label="item.label" :value="item.value"
              />
            </el-select>
          </el-form-item>
          <el-form-item label="當前狀態：">
            <el-select
              v-model="searchform.Status"
              placeholder="請選擇" clearable
            >
              <el-option
                v-for="item in StatusSearch"
                :key="item.value" :label="item.label" :value="item.value"
              />
            </el-select>
          </el-form-item>
          <el-form-item label="回報次數：">
            <el-input
              v-model="searchform.ReportBackCounts"
              placeholder="回報次數" clearable
            />
          </el-form-item>
          <el-form-item label="建立日期：">
            <DatePickerE
              v-model="searchform.CreateDate"
              value-format="yyyy-MM-dd"
              format="yyyy-MM-dd"
              type="date"
              placeholder="選擇建立日期"
            />
          </el-form-item>
          <br>
          <el-form-item label="隸屬區/店：">
            <el-cascader
              ref="AreaSearch"
              v-model="searchform.Area"
              :options="selectArea"
              filterable
              :props="{label:'FullName',value:'Id',children:'Children',emitPath:false, checkStrictly: true,expandTrigger: 'hover' }"
              clearable
              style="width:300px;"
              @change="handleSelectAreaSearchChange"
            />
          </el-form-item>
          <el-form-item label="業務：">
            <el-select
              v-model="searchform.Sales"
              placeholder="請選擇" clearable
            >
              <el-option
                v-for="item in SSales"
                :key="item.Account" :label="item.RealName" :value="item.Account"
              />
            </el-select>
          </el-form-item>
          <el-form-item label="結案：">
            <el-select
              v-model="searchform.IsClosed"
              placeholder="結案" clearable
            >
              <el-option
                v-for="item in Options"
                :key="item.value" :label="item.label" :value="item.value"
              />
            </el-select>
          </el-form-item>
          <el-form-item label="轉正：">
            <el-select
              v-model="searchform.IsTransfer"
              placeholder="轉正" clearable
            >
              <el-option
                v-for="item in Options"
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
        </el-form>
      </el-card>
    </div>
    <el-card>
      <div class="list-btn-container">
        <el-form
          ref="editFrom1"
          :inline="true" class="demo-form-inline" size="small" :model="editFrom"
        >
          <el-form-item
            v-hasPermi="['PotentialCustomers/StoreManager']"
            label="指派業務" label-width="80px"
          >
            <el-select
              v-model="editFrom.ASales"
              placeholder="請選擇" clearable style="width: 180px"
            >
              <el-option
                v-for="item in Sales"
                :key="item.Account" :label="item.RealName" :value="item.Account"
              />
            </el-select>
          </el-form-item>
          <el-button
            v-hasPermi="['PotentialCustomers/StoreManager']"
            type="primary"
            icon="el-icon-video-play"
            size="small"
            @click="assignSales()"
          >指派</el-button>
        </el-form>
        <el-form
          ref="editFrom2"
          :inline="true" class="demo-form-inline" size="small" :model="editFrom"
        >
          <el-form-item
            v-hasPermi="['PotentialCustomers/StoreManager']"
            label="目標區/店" label-width="80px"
          >
            <el-cascader
              ref="AreaEdit"
              v-model="editFrom.AArea_2"
              :options="selectAreaAll"
              filterable
              :props="{label:'FullName',value:'FullName',children:'Children',emitPath:false, checkStrictly: true,expandTrigger: 'hover' }"
              clearable
              style="width:300px;"
              @change="handleSelectAreaAllChange"
            />
          </el-form-item>
          <el-button
            v-hasPermi="['PotentialCustomers/StoreManager']"
            type="primary"
            icon="el-icon-video-play"
            size="small"
            @click="copyPotentialCustomerData()"
          >轉店</el-button>
        </el-form>

        <el-button-group>
          <el-button
            v-hasPermi="['PotentialCustomers/Add']"
            type="primary"
            icon="el-icon-plus"
            size="small"
            @click="ShowEditOrViewDialog()"
          >新增</el-button>
          <el-button
            v-hasPermi="['PotentialCustomers/Edit']"
            type="primary"
            icon="el-icon-edit"
            class="el-button-modify"
            size="small"
            @click="ShowEditOrViewDialog('edit')"
          >修改</el-button>
          <!-- <el-button
            v-hasPermi="['PotentialCustomers/StoreManager']"
            type="primary"
            icon="el-icon-video-play"
            size="small"
            @click="assignSales()"
          >指派</el-button> -->
          <el-button
            v-hasPermi="['PotentialCustomers/Edit']"
            type="primary"
            icon="el-icon-video-play"
            size="small"
            @click="transferClient()"
          >轉正式客戶</el-button>
          <el-button
            v-hasPermi="['PotentialCustomers/StoreManager']"
            type="danger"
            icon="el-icon-delete"
            size="small"
            @click="deletePhysics()"
          >刪除</el-button>
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
      >
        <el-table-column
          type="selection"
          width="30"
        />
        <el-table-column
          prop="Name"
          label="姓名" sortable="custom" width="120"
        />
        <el-table-column
          prop="[Identity]"
          label="身份" sortable="custom" width="120"
        />
        <el-table-column
          prop="Cell"
          label="手機號碼" sortable="custom" width="120"
        />
        <el-table-column
          prop="Area"
          label="隸屬區/店" sortable="custom" width="120"
        />
        <el-table-column
          prop="SalesName"
          label="指派業務" width="120"
        />
        <el-table-column
          prop="Source"
          label="來源" sortable="custom" width="120"
        />
        <el-table-column
          prop="CreateDate"
          label="建立日期" sortable="custom" width="120"
        />
        <el-table-column
          prop="ReportBackCounts"
          label="回報次數" sortable="custom" width="120"
        />
        <el-table-column
          prop="Status"
          label="當前狀態" sortable="custom" width="120"
        />
        <el-table-column
          prop="IsClosed"
          label="結案" sortable="custom" width="120"
        />
        <el-table-column
          prop="IsTransfer"
          label="轉正" sortable="custom"
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
      fullscreen
      :title="editFormTitle+'潛在客'"
      :visible.sync="dialogEditFormVisible"
      width="660px"
      append-to-body
    >
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
              ref="editFrom"
              :model="editFrom" :rules="rules"
            >
              <el-row>
                <el-col :span="6">
                  <el-form-item
                    label="身份"
                    :label-width="formLabelWidth" prop="Identity"
                  >
                    <el-select
                      v-model="editFrom.Identity"
                      placeholder="請選擇" :disabled="isReadonly || isReadonly2" @change="resetData"
                    >
                      <el-option
                        v-for="item in IdentityEdit"
                        :key="item.value"
                        :label="item.label"
                        :value="item.value"
                      />
                    </el-select>
                  </el-form-item>
                </el-col>
                <el-col :span="6">
                  <el-form-item
                    label="自然人或法人"
                    :label-width="formLabelWidth" prop="Identity2"
                  >
                    <el-select
                      v-model="editFrom.Identity2"
                      placeholder="請選擇" :disabled="isReadonly || isReadonly2"
                    >
                      <el-option
                        v-for="item in Identity2Edit"
                        :key="item.value"
                        :label="item.label"
                        :value="item.value"
                      />
                    </el-select>
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row>
                <el-col :span="6">
                  <el-form-item
                    v-if="landlord"
                    label="所在縣市" :label-width="formLabelWidth" prop="CountyCity"
                  >
                    <el-select
                      v-model="editFrom.CountyCity"
                      placeholder="請選擇" :disabled="isReadonly"
                    >
                      <el-option
                        v-for="item in CountyCity"
                        :key="item.value" :label="item.label" :value="item.value"
                      />
                    </el-select>
                  </el-form-item>
                </el-col>
                <el-col :span="6">
                  <el-form-item
                    label="隸屬區/店"
                    :label-width="formLabelWidth" prop="Area"
                  >
                    <el-cascader
                      ref="AreaEdit"
                      v-model="editFrom.Area"
                      :options="selectArea"
                      filterable
                      :props="{label:'FullName',value:'FullName',children:'Children',emitPath:false, checkStrictly: true,expandTrigger: 'hover' }"
                      clearable
                      style="width:300px;"
                      :disabled="isReadonly || isReadonly2"
                      @change="handleSelectAreaChange"
                    />
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row>
                <el-col :span="6">
                  <el-form-item
                    label="姓名"
                    :label-width="formLabelWidth" prop="Name"
                  >
                    <el-input
                      v-model="editFrom.Name"
                      clearable
                      placeholder="姓名"
                      style="width:215px"
                      :readonly="isReadonly || isReadonly2"
                    />
                  </el-form-item>
                </el-col>
                <el-col :span="6">
                  <el-form-item
                    label="電話"
                    :label-width="formLabelWidth" prop="Tel"
                  >
                    <el-input
                      v-model="editFrom.Tel_1"
                      clearable
                      placeholder="區碼"
                      style="width:75px"
                      :readonly="isReadonly"
                    /> -
                    <el-input
                      v-model="editFrom.Tel_2"
                      clearable
                      placeholder="號碼"
                      style="width:150px"
                      :readonly="isReadonly"
                    />
                  </el-form-item>
                </el-col>
                <el-col :span="6">
                  <el-form-item
                    label="手機"
                    :label-width="formLabelWidth" prop="Cell"
                  >
                    <el-input
                      v-model="editFrom.Cell"
                      clearable
                      placeholder="手機"
                      style="width:215px"
                      :readonly="isReadonly || isReadonly2"
                    />
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row>
                <el-col :span="6">
                  <el-form-item
                    :label="houseTypeLabel"
                    :label-width="formLabelWidth" prop="HouseType"
                  >
                    <el-select
                      v-model="editFrom.HouseType"
                      placeholder="請選擇" :disabled="isReadonly"
                    >
                      <el-option
                        v-for="item in HouseType"
                        :key="item.value" :label="item.label" :value="item.value"
                      />
                    </el-select>
                  </el-form-item>
                </el-col>
                <el-col :span="6">
                  <el-form-item
                    :label="houseInteriorLabel"
                    :label-width="formLabelWidth" prop="HouseInterior"
                  >
                    <el-select
                      v-model="editFrom.HouseInterior"
                      placeholder="請選擇" :disabled="isReadonly"
                    >
                      <el-option
                        v-for="item in HouseInterior"
                        :key="item.value"
                        :label="item.label"
                        :value="item.value"
                      />
                    </el-select>
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row>
                <el-col :span="6">
                  <el-form-item
                    label="來源"
                    :label-width="formLabelWidth" prop="Source"
                  >
                    <el-select
                      v-model="editFrom.Source"
                      placeholder="請選擇" :disabled="isReadonly"
                    >
                      <el-option
                        v-for="item in Source"
                        :key="item.value" :label="item.label" :value="item.value"
                      />
                    </el-select>
                  </el-form-item>
                </el-col>
                <!-- <el-col :span="6">
                  <el-form-item v-if="landlord" label="出租人來源" :label-width="formLabelWidth" prop="LSource">
                    <el-select v-model="editFrom.Source" placeholder="請選擇" :disabled="isReadonly">
                      <el-option v-for="item in LSource" :key="item.value" :label="item.label" :value="item.value" />
                    </el-select>
                  </el-form-item>
                </el-col> -->
                <el-col :span="6">
                  <el-form-item
                    v-if="landlord"
                    label="期待租金" :label-width="formLabelWidth" prop="ExpectRent"
                  >
                    <el-input
                      v-model="editFrom.ExpectRent"
                      clearable
                      placeholder="期待租金"
                      style="width:215px"
                      :readonly="isReadonly"
                    />
                  </el-form-item>
                </el-col>
                <el-col :span="6">
                  <el-form-item
                    v-if="landlord"
                    label="可否寵物" :label-width="formLabelWidth" prop="AllowPet"
                  >
                    <el-select
                      v-model="editFrom.AllowPet"
                      placeholder="請選擇" :disabled="isReadonly"
                    >
                      <el-option
                        v-for="item in Options2"
                        :key="item.value" :label="item.label" :value="item.value"
                      />
                    </el-select>
                  </el-form-item>
                </el-col>
                <!-- </el-row> -->
                <!-- <el-row> -->
                <!-- <el-col :span="6">
                  <el-form-item v-if="renter" label="承租人來源" :label-width="formLabelWidth" prop="RSource">
                    <el-select v-model="editFrom.Source" placeholder="請選擇" :disabled="isReadonly">
                      <el-option v-for="item in RSource" :key="item.value" :label="item.label" :value="item.value" />
                    </el-select>
                  </el-form-item>
                </el-col> -->
                <el-col :span="6">
                  <el-form-item
                    v-if="renter"
                    label="租金預算" :label-width="formLabelWidth" prop="AnticipateRent"
                  >
                    <el-input
                      v-model="editFrom.AnticipateRent"
                      clearable
                      placeholder="租金預算"
                      style="width:215px"
                      :readonly="isReadonly"
                    />
                  </el-form-item>
                </el-col>
                <el-col :span="6">
                  <el-form-item
                    v-if="renter"
                    label="有無寵物" :label-width="formLabelWidth" prop="HavePet"
                  >
                    <el-select
                      v-model="editFrom.HavePet"
                      placeholder="請選擇" :disabled="isReadonly"
                    >
                      <el-option
                        v-for="item in Options3"
                        :key="item.value" :label="item.label" :value="item.value"
                      />
                    </el-select>
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row>
                <el-col :span="24">
                  <el-form-item
                    v-show="landlord"
                    label="出租房屋地址" :label-width="formLabelWidth"
                  >
                    <Address
                      :sendedform="sendedformL"
                      title="出租房屋地址" @geteditaddress="geteditaddress"
                    />
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row>
                <el-col :span="24">
                  <el-form-item
                    v-show="renter"
                    label="房屋戶籍地址" :label-width="formLabelWidth"
                  >
                    <Address
                      :sendedform="sendedformR"
                      title="房屋戶籍地址" @geteditaddress="geteditaddress"
                    />
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row>
                <el-col :span="24">
                  <el-form-item
                    :label-width="formLabelWidth"
                    label="客戶備註" prop="Note"
                  >
                    <el-input
                      v-model="editFrom.Note"
                      type="textarea"
                      :rows="4"
                      style="width: 670px"
                      :disabled="isReadonly"
                    />
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row>
                <el-col :span="1">&nbsp;</el-col>
                <el-col :span="23">
                  <el-checkbox
                    v-if="editFormTitle !== '編輯' && !isBA"
                    v-model="editFrom.HavePrivilege"
                    true-label="Y"
                    false-label="N"
                  >
                    我是該客戶開發業務
                  </el-checkbox>
                </el-col>
              </el-row>

              <div class="rightbtn">
                <el-button
                  size="small"
                  icon="el-icon-close" @click="cancel"
                >關閉</el-button>
                <el-button
                  v-if="editFormTitle === '新增'"
                  v-hasPermi="['PotentialCustomers/Add']"
                  type="primary"
                  size="small"
                  icon="el-icon-paperclip"
                  @click="saveEditForm()"
                >儲存
                </el-button>
                <el-button
                  v-if="notTransfer && editFormTitle === '編輯'"
                  v-hasPermi="['PotentialCustomers/Edit']"
                  type="primary"
                  size="small"
                  icon="el-icon-paperclip"
                  @click="saveEditForm()"
                >儲存
                </el-button>
              </div>
            </el-form>
          </el-card>
        </el-tab-pane>
        <el-tab-pane
          v-if="editFormTitle === '編輯'"
          label="拜訪記錄" name="CT2"
        >
          <el-card class="box-card">
            <div class="list-btn-container">
              <el-button-group>
                <el-button
                  v-if="notTransfer"
                  v-hasPermi="['PotentialCustomers/Add']"
                  type="primary"
                  icon="el-icon-plus"
                  size="small"
                  @click="ShowVREditOrViewDialog()"
                >新增</el-button>
                <el-button
                  v-if="notTransfer"
                  v-hasPermi="['PotentialCustomers/Edit']"
                  type="primary"
                  icon="el-icon-edit"
                  class="el-button-modify"
                  size="small"
                  @click="ShowVREditOrViewDialog('edit')"
                >修改
                </el-button>
                <el-button
                  v-if="notTransfer"
                  v-hasPermi="['PotentialCustomers/Delete']"
                  type="danger"
                  icon="el-icon-delete"
                  size="small"
                  @click="deleteVRPhysics()"
                >刪除</el-button>
                <el-button
                  type="default"
                  icon="el-icon-refresh" size="small" @click="loadVRTableData()"
                >刷新</el-button>
              </el-button-group>
            </div>
            <el-table
              ref="vrgridtable"
              v-loading="vrtableloading"
              :data="vrTableData"
              border
              stripe
              highlight-current-row
              style="width: 100%"
              :default-sort="{prop: 'SortCode', order: 'ascending'}"
              @select="handleVRSelectChange"
              @select-all="handleVRSelectAllChange"
              @sort-change="handleVRSortChange"
              @row-dblclick="vrrowdblclick"
            >
              <el-table-column
                type="selection"
                width="30"
              />
              <el-table-column
                prop="Status"
                label="當前狀態" sortable="custom" width="120"
              />
              <el-table-column
                prop="SalesName"
                label="當前業務" sortable="custom" width="120"
              />
              <el-table-column
                prop="VisitTime"
                label="拜訪時間" sortable="custom" width="150"
              />
              <el-table-column
                prop="Note"
                label="備註" sortable="custom"
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
            <div class="rightbtn">
              <el-button
                size="small"
                icon="el-icon-close" @click="cancelVR2"
              >關閉</el-button>
            </div>
          </el-card>
        </el-tab-pane>
      </el-tabs>
    </el-dialog>
    <el-dialog
      ref="dialogVREditForm"
      v-el-drag-dialog
      :title="vrEditFormTitle+'拜訪記錄'"
      :visible.sync="dialogVREditFormVisible"
      append-to-body
      fullscreen
    >
      <el-form
        ref="vrEditFrom"
        :model="vrEditFrom"
      >
        <el-form-item
          label="拜訪時間"
          :label-width="formLabelWidth" prop="VisitTime"
        >
          <DatePickerE
            v-model="vrEditFrom.VisitTime"
            value-format="yyyy-MM-dd HH:mm:ss"
            format="yyyy-MM-dd HH:mm:ss"
            type="datetime"
            placeholder="選擇建立日期"
          />
        </el-form-item>
        <el-form-item
          v-if="landlord"
          label="出租人當前狀態" :label-width="formLabelWidth" prop="LStatus"
        >
          <el-select
            v-model="vrEditFrom.Status"
            placeholder="請選擇" :disabled="isReadonly"
          >
            <el-option
              v-for="item in LStatus"
              :key="item.value" :label="item.label" :value="item.value"
            />
          </el-select>
        </el-form-item>
        <el-form-item
          v-if="renter"
          label="承租人當前狀態" :label-width="formLabelWidth" prop="RStatus"
        >
          <el-select
            v-model="vrEditFrom.Status"
            placeholder="請選擇" :disabled="isReadonly"
          >
            <el-option
              v-for="item in RStatus"
              :key="item.value" :label="item.label" :value="item.value"
            />
          </el-select>
        </el-form-item>
        <el-form-item
          label="備註"
          :label-width="formLabelWidth" prop="Note"
        >
          <el-input
            v-model="vrEditFrom.Note"
            type="textarea" :rows="4" style="width: 500px" :readonly="isReadonly"
          />
        </el-form-item>
      </el-form>
      <div
        slot="footer"
        class="dialog-footer"
      >
        <el-button
          size="small"
          icon="el-icon-close" @click="cancelVR"
        >關閉</el-button>
        <el-button
          v-if="notTransfer"
          type="primary" size="small" icon="el-icon-video-play" @click="saveVREditForm()"
        >儲存
        </el-button>
      </div>
    </el-dialog>
    <el-dialog
      ref="dialogTransferClientForm"
      v-el-drag-dialog
      title="轉正式客戶"
      :visible.sync="dialogTransferClientFormVisible"
      append-to-body
      fullscreen
    >
      <el-form
        ref="transferFrom"
        :model="editFrom" :rules="transferRules"
      >
        <el-row>
          <el-col :span="8">
            <el-form-item
              label="姓名"
              :label-width="formLabelWidth" prop="TCName"
            >
              <el-input
                v-model="editFrom.Name"
                style="width:300px"
              />
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item
              label="身份證號/統編"
              :label-width="formLabelWidth" prop="LRID"
            >
              <el-input
                v-model="editFrom.LRID"
                style="width:300px"
              />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="8">
            <el-form-item
              label="身份別"
              :label-width="formLabelWidth" prop="TCIdentity"
            >
              <el-input
                v-model="editFrom.Identity"
                style="width:300px" readonly
              />
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item
              label="自然人/法人"
              :label-width="formLabelWidth" prop="TCIdentity2"
            >
              <el-input
                v-model="editFrom.Identity2"
                style="width:300px" readonly
              />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="8">
            <el-form-item
              label="手機號碼"
              :label-width="formLabelWidth" prop="TCCell"
            >
              <el-input
                v-model="editFrom.Cell"
                style="width:300px" readonly
              />
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item
              label="建物類型"
              :label-width="formLabelWidth" prop="BPropoty"
            >
              <el-select
                v-model="editFrom.BPropoty"
                placeholder="請選擇" clearable style="width: 120px"
              >
                <el-option
                  v-for="item in bpropotiesOptions"
                  :key="item.Id"
                  :label="item.BuildingPropotyName"
                  :value="item.BuildingPropotyName"
                />
              </el-select>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="24">
            <el-form-item
              v-show="landlord"
              label="出租房屋地址" :label-width="formLabelWidth" class="el-form-item is-required el-form-item--medium"
            >
              <Address
                :sendedform="sendedformL"
                title="出租房屋地址" @geteditaddress="geteditaddress"
              />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="24">
            <el-form-item
              v-show="renter"
              label="房屋戶籍地址" :label-width="formLabelWidth"
            >
              <Address
                :sendedform="sendedformR"
                title="房屋戶籍地址" @geteditaddress="geteditaddress"
              />
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
      <p
        v-show="landlord"
        style="color:red;font-weight: bold;padding-left: 25px;"
      >地址請填寫正確，轉正後無法修改</p>
      <div
        slot="footer"
        class="dialog-footer"
      >
        <el-button
          size="small"
          icon="el-icon-close" @click="cancelTC"
        >關閉</el-button>
        <el-button
          type="primary"
          icon="el-icon-video-play" @click="doTransferClient()"
        >轉正</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>

import { getPotentialCustomersListWithPager, getPotentialCustomersDetail,
  savePotentialCustomers, deletePotentialCustomers, transferClient, assignSales, copyPotentialCustomerData
} from '@/api/chaochi/potentialcustomers/potentialcustomersservice'
import {
  getVisitingRecordListWithPager, getVisitingRecordDetail,
  saveVisitingRecord, deleteVisitingRecord
} from '@/api/chaochi/potentialcustomers/visitingrecordservice'
import { getPermissionOrganizeTreeTablePC, getAllOrganizeTreeTable } from '@/api/security/organizeservice';
import { getSales, getSalesByDepartmentId } from '@/api/security/userservice';
import { mapGetters } from 'vuex'
import elDragDialog from '@/directive/el-drag-dialog' // 彈窗可移動
import Address from '@/components/Address/Address.vue'
import DatePickerE from '@/components/RocDatepickerE'
import moment from 'moment'
import { validateIDCombo, validateCellReg } from '@/utils/validate'
// import { isNumber } from '@/components/cron/util/tools'
export default {
  name: 'PotentialCustomers', // 需與菜單的功能編碼一致
  directives: { elDragDialog },
  components: {
    Address,
    DatePickerE
  },
  data() {
    return {
      searchform: {
        Name: '',
        Cell: '',
        Identity: '',
        Identity2: '',
        Source: '',
        Status: '',
        ReportBackCounts: '',
        CreateDate: '',
        Area: '',
        IsClosed: '',
        IsTransfer: ''
      },
      houseTypeLabel: '',
      houseInteriorLabel: '',
      Identity: [
        { value: '', label: '不指定' },
        { value: '出租人', label: '出租人' },
        { value: '承租人', label: '承租人' }
      ],
      Identity2: [
        { value: '', label: '不指定' },
        { value: '自然人', label: '自然人' },
        { value: '法人', label: '法人' }
      ],
      IdentityEdit: [
        { value: '出租人', label: '出租人' },
        { value: '承租人', label: '承租人' }
      ],
      Identity2Edit: [
        { value: '自然人', label: '自然人' },
        { value: '法人', label: '法人' }
      ],
      Source: [],
      SourceSearch: [
        { value: '', label: '不指定' },
        { value: '業務開發', label: '業務開發' },
        { value: '門市洽詢', label: '門市洽詢' },
        { value: '網路社群', label: '網路社群' },
        { value: '電銷開發', label: '電銷開發' },
        { value: '0800資源', label: '0800資源' },
        { value: '現有承租人', label: '現有承租人' },
        { value: '跨區轉介', label: '跨區轉介' },
        { value: '公部門轉介', label: '公部門轉介' },
        { value: '活動問卷', label: '活動問卷' }
      ],
      LSource: [
        { value: '業務開發', label: '業務開發' },
        { value: '門市洽詢', label: '門市洽詢' },
        { value: '網路社群', label: '網路社群' },
        { value: '電銷開發', label: '電銷開發' },
        { value: '0800資源', label: '0800資源' },
        { value: '跨區轉介', label: '跨區轉介' },
        { value: '公部門轉介', label: '公部門轉介' },
        { value: '活動問卷', label: '活動問卷' }
      ],
      RSource: [
        { value: '業務開發', label: '業務開發' },
        { value: '現有承租人', label: '現有承租人' },
        { value: '0800資源', label: '0800資源' },
        { value: '網路社群', label: '網路社群' },
        { value: '電銷開發', label: '電銷開發' },
        { value: '門市洽詢', label: '門市洽詢' },
        { value: '跨區轉介', label: '跨區轉介' },
        { value: '公部門轉介', label: '公部門轉介' },
        { value: '活動問卷', label: '活動問卷' }
      ],
      Status: [],
      StatusSearch: [
        { value: '', label: '不指定' },
        { value: '約定場勘', label: '約定場勘' },
        { value: '已簽委託/申請書', label: '已簽委託/申請書' },
        { value: '約定帶看', label: '約定帶看' },
        { value: '已填申請書', label: '已填申請書' },
        { value: '轉派至其他單位', label: '轉派至其他單位' },
        { value: '再追蹤', label: '再追蹤' },
        { value: '無意願', label: '無意願' },
        { value: '已出租', label: '已出租' },
        { value: '待簽約', label: '待簽約' },
        { value: '已重複開發', label: '已重複開發' }
      ],
      LStatus: [
        { value: '約定場勘', label: '約定場勘' },
        { value: '已簽委託/申請書', label: '已簽委託/申請書' },
        { value: '轉派至其他單位', label: '轉派至其他單位' },
        { value: '再追蹤', label: '再追蹤' },
        { value: '無意願', label: '無意願' },
        { value: '已出租', label: '已出租' },
        { value: '已重複開發', label: '已重複開發' }
      ],
      RStatus: [
        { value: '約定帶看', label: '約定帶看' },
        { value: '已填申請書', label: '已填申請書' },
        { value: '轉派至其他單位', label: '轉派至其他單位' },
        { value: '再追蹤', label: '再追蹤' },
        { value: '無意願', label: '無意願' },
        { value: '待簽約', label: '待簽約' },
        { value: '已重複開發', label: '已重複開發' }
      ],
      Sales: [],
      SSales: [],
      Options: [
        { value: '', label: '不指定' },
        { value: '是', label: '是' },
        { value: '否', label: '否' }
      ],
      Options2: [
        { value: '可', label: '可' },
        { value: '否', label: '否' }
      ],
      Options3: [
        { value: '有', label: '有' },
        { value: '無', label: '無' }
      ],
      CountyCity: [
        { label: '臺北市', value: '臺北市' },
        { label: '新北市', value: '新北市' },
        { label: '桃園市', value: '桃園市' },
        { label: '臺中市', value: '臺中市' },
        { label: '臺南市', value: '臺南市' },
        { label: '高雄市', value: '高雄市' },
        { label: '基隆市', value: '基隆市' },
        { label: '新竹市', value: '新竹市' },
        { label: '嘉義市', value: '嘉義市' },
        { label: '新竹縣', value: '新竹縣' },
        { label: '苗栗縣', value: '苗栗縣' },
        { label: '彰化縣', value: '彰化縣' },
        { label: '南投縣', value: '南投縣' },
        { label: '雲林縣', value: '雲林縣' },
        { label: '嘉義縣', value: '嘉義縣' },
        { label: '屏東縣', value: '屏東縣' },
        { label: '宜蘭縣', value: '宜蘭縣' },
        { label: '花蓮縣', value: '花蓮縣' },
        { label: '臺東縣', value: '臺東縣' },
        { label: '澎湖縣', value: '澎湖縣' },
        { label: '金門縣', value: '金門縣' },
        { label: '連江縣', value: '連江縣' }
      ],
      HouseType: [
        { value: '公寓', label: '公寓' },
        { value: '華廈', label: '華廈' },
        { value: '電梯大樓', label: '電梯大樓' },
        { value: '透天厝', label: '透天厝' },
        { value: '農舍', label: '農舍' },
        { value: '廠房', label: '廠房' }
      ],
      HouseInterior: [
        { value: '套房', label: '套房' },
        { value: '雅房', label: '雅房' },
        { value: '1房', label: '1房' },
        { value: '2房', label: '2房' },
        { value: '3房', label: '3房' },
        { value: '4房以上', label: '4房以上' },
        { value: '開放式空間', label: '開放式空間' }
      ],
      selectArea: [],
      selectAreaAll: [],
      tableData: [],
      vrTableData: [],
      tableloading: true,
      vrtableloading: true,
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
      dialogVREditFormVisible: false,
      dialogTransferClientFormVisible: false,
      editFormTitle: '',
      vrEditFormTitle: '',
      editFrom: {},
      vrEditFrom: {},
      transferFrom: {},
      rules: {
        Name: [
          { required: true, message: '請輸入姓名', trigger: 'blur' }
        ],
        Identity: [
          { required: true, trigger: 'change', message: '請選擇身份' }
        ],
        Identity2: [
          { required: true, trigger: 'change', message: '請選擇自然人或法人' }
        ],
        CountyCity: [
          { required: true, trigger: 'change', message: '請選擇所在縣市' }
        ],
        Area: [
          { required: true, trigger: 'change', message: '請選擇隸屬區/店' }
        ],
        Cell: [
          { required: true, trigger: 'change', validator: validateCellReg }
        ],
        Source: [
          { required: true, trigger: 'change', message: '請選擇來源' }
        ]
        // LSource: [
        //   { required: true, trigger: 'change', message: '請選擇出租人來源' }
        // ],
        // RSource: [
        //   { required: true, trigger: 'change', message: '請選擇承租人來源' }
        // ]
      },
      transferRules: {
        LRID: [
          { required: true, trigger: 'change', validator: validateIDCombo }
          // { required: true, message: '請輸入身份證號/統編', trigger: 'blur' },
          // { type: 'string', min: 8, max: 10, message: '身份證號為10碼/統編為8碼', trigger: 'blur' }
        ],
        BPropoty: [
          { required: true, trigger: 'change', message: '請選擇建物類型' }
        ]
      },
      formLabelWidth: '120px',
      currentId: '', // 當前操作對象的ID值，主要用于修改
      currentSelected: [],
      vrCurrentId: '', // 當前操作對象的ID值，主要用于修改
      vrCurrentSelected: [],
      TabAlias: 'CT1',
      sendedformL: {},
      sendedformR: {}
    }
  },
  computed: {
    ...mapGetters(['name']),
    ...mapGetters(['roles']),
    ...mapGetters(['userId']),
    ...mapGetters(['bpropotiesOptions']),
    isBA() {
      return this.roles.includes('Business assistance')
    },
    isSales() {
      return this.roles.includes('sales')
    },
    isStoreManager() {
      return this.roles.includes('Business storemanager')
    },
    landlord() {
      let identity = ''
      if (this.editFrom.Identity) {
        identity = this.editFrom.Identity.trim()
      }
      return identity === '出租人'
    },
    renter() {
      let identity = ''
      if (this.editFrom.Identity) {
        identity = this.editFrom.Identity.trim()
      }
      return identity === '承租人'
    },
    isReadonly() {
      return this.editFrom.IsTransfer === '是'
    },
    isReadonly2() {
      return this.editFormTitle === '編輯' && this.editFrom.CreatorUserId !== this.userId && this.editFrom.Sales !== this.name
    },
    notTransfer() {
      return this.editFrom.IsTransfer !== '是'
    }
    // isStoreManager() {
    //   return this.roles.includes('storemanager')
    // }
  },
  watch: {
    'editFrom.Identity': {
      handler(val) {
        this.houseTypeLabel = (val === '出租人') ? '房屋型態' : '房屋需求'
        this.houseInteriorLabel = (val === '出租人') ? '房屋格局' : '格局需求'
      }
    }
  },
  created() {
    this.pagination.currentPage = 1
    this.InitDictItem()
    this.loadTableData()
  },
  methods: {
    /**
     * 初始化數據
     */
    InitDictItem() {
      getPermissionOrganizeTreeTablePC().then(res => {
        ++this.cascaderKey
        this.selectArea = res.ResData
      });
      getAllOrganizeTreeTable().then(res => {
        this.selectAreaAll = res.ResData
      })
      getSales().then(res => {
        this.Sales = res.ResData
      })
    },

    // 取消按鈕
    cancel() {
      this.dialogEditFormVisible = false
      this.reset()
    },
    // 訪談記錄取消按鈕
    cancelVR() {
      this.dialogVREditFormVisible = false
      this.resetVR()
    },
    // 訪談記錄取消按鈕
    cancelVR2() {
      this.dialogEditFormVisible = false
      this.reset()
      this.loadTableData()
    },
    // 轉正式客戶取消按鈕
    cancelTC() {
      this.dialogTransferClientFormVisible = false
      this.reset()
    },
    // 表單重置
    reset() {
      this.editFrom = {
        PID: '',
        Identity: '',
        Identity2: '',
        Source: '',
        Status: '',
        IsClosed: '',
        IsTransfer: '',
        ReportBackCounts: '',
        Sales: '',
        Area: '',
        Area_1: '',
        Area_2: '',
        HouseType: '',
        HouseInterior: '',
        Name: '',
        roles: '',
        Tel: '',
        Tel_1: '',
        Tel_2: '',
        Extension: '',
        CountyCity: '',
        ResidentCount: '',
        RAdd: '',
        RAdd_1: '',
        RAdd_1_1: '',
        RAdd_1_2: '',
        RAdd_2: '',
        RAdd_2_1: '',
        RAdd_2_2: '',
        RAdd_2_3: '',
        RAdd_2_4: '',
        RAdd_3: '',
        RAdd_3_1: '',
        RAdd_3_2: '',
        RAdd_4: '',
        RAdd_5: '',
        RAdd_6: '',
        RAdd_7: '',
        RAdd_8: '',
        RAdd_9: '',
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
        ExpectRent: '',
        AnticipateRent: '',
        HavePet: '',
        AllowPet: '',
        Note: '',
        CreatorTime: '',
        CreatorUserId: '',
        CreatorUserOrgId: '',
        CreatorUserDeptId: '',
        DeleteMark: '',
        DeleteTime: '',
        DeleteUserId: '',
        LastModifyTime: '',
        LastModifyUserId: '',

        // 需個性化處理內容

        // 身份證號/統編
        LRID: '',
        // 是否擁有客戶服務權
        HavePrivilege: ''
      }
      this.sendedformL = {}
      this.sendedformR = {}
      this.TabAlias = 'CT1'
      this.resetForm('editFrom')
    },
    resetVR() {
      this.vrEditFrom = {
        // 拜訪記錄
        VistiTime: '',
        Status: '',
        Sales: '',
        Note: ''
      }
      this.resetForm('vrEditFrom')
    },
    /**
     * 加載頁面table數據
     */
    loadTableData: function() {
      this.tableloading = true
      var t = {
        Name: this.searchform.Name,
        Cell: this.searchform.Cell,
        Identity: this.searchform.Identity,
        Identity2: this.searchform.Identity2,
        Source: this.searchform.Source,
        Status: this.searchform.Status,
        ReportBackCounts: this.searchform.ReportBackCounts,
        CreateDate: this.searchform.CreateDate,
        Area: this.searchform.Area,
        Sales: this.searchform.Sales,
        IsClosed: this.searchform.IsClosed,
        IsTransfer: this.searchform.IsTransfer
      }
      var seachdata = {
        CurrenetPageIndex: this.pagination.currentPage,
        PageSize: this.pagination.pagesize,
        Keywords: this.searchform.keywords,
        Order: this.sortableData.order,
        Sort: this.sortableData.sort,
        Filter: t
      }
      getPotentialCustomersListWithPager(seachdata).then(res => {
        this.tableData = res.ResData.Items
        this.pagination.pageTotal = res.ResData.TotalItems
        this.tableloading = false
      })
    },
    /**
     * 加載頁面table數據
     */
    loadVRTableData: function() {
      this.vrtableloading = true
      var t = {
        PID: this.editFrom.PID
      }
      var seachdata = {
        CurrenetPageIndex: this.pagination.currentPage,
        PageSize: this.pagination.pagesize,
        Keywords: this.searchform.keywords,
        Order: this.sortableData.order,
        Sort: this.sortableData.sort,
        Filter: t
      }
      getVisitingRecordListWithPager(seachdata).then(res => {
        this.vrTableData = res.ResData.Items
        this.pagination.pageTotal = res.ResData.TotalItems
        this.vrtableloading = false
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
      this.reset()
      if (view !== undefined) {
        if (this.currentSelected.length > 1 || this.currentSelected.length === 0) {
          this.$alert('請選擇一條數據進行編輯/修改', '提示')
        } else {
          const sales = this.currentSelected[0].Sales
          if (sales) {
            this.currentId = this.currentSelected[0].Id
            this.editFormTitle = '編輯'
            this.bindEditInfo()
            // this.dialogEditFormVisible = true
          } else {
            this.$alert('請先指派業務', '警告')
            return false
          }
        }
      } else {
        this.editFormTitle = '新增'
        this.currentId = ''
        this.dialogEditFormVisible = true
        this.sendedformL = { }
        this.sendedformR = { }
      }
    },
    ShowVREditOrViewDialog: function(view) {
      this.resetVR()
      if (view !== undefined) {
        if (this.currentVRSelected.length > 1 || this.currentVRSelected.length === 0) {
          this.$alert('請選擇一條數據進行編輯/修改', '提示')
        } else {
          this.currentVRId = this.currentVRSelected[0].Id
          this.vrEditFormTitle = '編輯'
          this.dialogVREditFormVisible = true
          this.bindVREditInfo()
        }
      } else {
        this.vrEditFormTitle = '新增'
        this.currentVRId = ''
        this.dialogVREditFormVisible = true
      }
    },
    bindEditInfo: function() {
      getPotentialCustomersDetail(this.currentId).then(res => {
        this.editFrom = res.ResData
        // 需個性化處理內容

        // 處理出租房屋地址
        this.sendedformL = {
          Add_1: this.editFrom.BAdd_1,
          Add_1_1: this.editFrom.BAdd_1_1,
          Add_1_2: this.editFrom.BAdd_1_2,
          Add_2: this.editFrom.BAdd_2,
          Add_2_1: this.editFrom.BAdd_2_1,
          Add_2_2: this.editFrom.BAdd_2_2,
          Add_2_3: this.editFrom.BAdd_2_3,
          Add_2_4: this.editFrom.BAdd_2_4,
          Add_3: this.editFrom.BAdd_3,
          Add_3_1: this.editFrom.BAdd_3_1,
          Add_3_2: this.editFrom.BAdd_3_2,
          Add_4: this.editFrom.BAdd_4,
          Add_5: this.editFrom.BAdd_5,
          Add_6: this.editFrom.BAdd_6,
          Add_7: this.editFrom.BAdd_7,
          Add_8: this.editFrom.BAdd_8,
          Add_9: this.editFrom.BAdd_9
        }

        // 處理房屋戶籍地址
        this.sendedformR = {
          Add_1: this.editFrom.RAdd_1,
          Add_1_1: this.editFrom.RAdd_1_1,
          Add_1_2: this.editFrom.RAdd_1_2,
          Add_2: this.editFrom.RAdd_2,
          Add_2_1: this.editFrom.RAdd_2_1,
          Add_2_2: this.editFrom.RAdd_2_2,
          Add_2_3: this.editFrom.RAdd_2_3,
          Add_2_4: this.editFrom.RAdd_2_4,
          Add_3: this.editFrom.RAdd_3,
          Add_3_1: this.editFrom.RAdd_3_1,
          Add_3_2: this.editFrom.RAdd_3_2,
          Add_4: this.editFrom.RAdd_4,
          Add_5: this.editFrom.RAdd_5,
          Add_6: this.editFrom.RAdd_6,
          Add_7: this.editFrom.RAdd_7,
          Add_8: this.editFrom.RAdd_8,
          Add_9: this.editFrom.RAdd_9
        }

        this.loadVRTableData();
        this.dialogEditFormVisible = true
      })
    },
    bindVREditInfo: function() {
      getVisitingRecordDetail(this.currentVRId).then(res => {
        this.vrEditFrom = res.ResData
        // 需個性化處理內容
      })
    },
    /**
     * 新增/修改保存
     */
    saveEditForm() {
      this.$refs['editFrom'].validate((valid) => {
        if (valid) {
          const data = {
            'PID': this.editFrom.PID,
            'Identity': this.editFrom.Identity,
            'Identity2': this.editFrom.Identity2,
            // 'Source': (this.editFrom.Identity === '出租人') ? this.editFrom.LSource : this.editFrom.RSource,
            'Source': this.editFrom.Source,
            'Status': this.editFrom.Status,
            'IsClosed': this.editFrom.IsClosed,
            'IsTransfer': this.editFrom.IsTransfer,
            'ReportBackCounts': this.editFrom.ReportBackCounts,
            'Sales': this.editFrom.Sales,
            'Area': this.editFrom.Area,
            'Area_1': this.editFrom.Area_1,
            'Area_2': this.editFrom.Area_2,
            'HouseType': this.editFrom.HouseType,
            'HouseInterior': this.editFrom.HouseInterior,
            'Name': this.editFrom.Name,
            'Tel': this.editFrom.Tel,
            'Tel_1': this.editFrom.Tel_1,
            'Tel_2': this.editFrom.Tel_2,
            'Cell': this.editFrom.Cell,
            'CountyCity': this.editFrom.CountyCity,
            'ResidentCount': this.editFrom.ResidentCount,
            'RAdd': this.editFrom.RAdd,
            'RAdd_1': this.editFrom.RAdd_1,
            'RAdd_1_1': this.editFrom.RAdd_1_1,
            'RAdd_1_2': this.editFrom.RAdd_1_2,
            'RAdd_2': this.editFrom.RAdd_2,
            'RAdd_2_1': this.editFrom.RAdd_2_1,
            'RAdd_2_2': this.editFrom.RAdd_2_2,
            'RAdd_2_3': this.editFrom.RAdd_2_3,
            'RAdd_2_4': this.editFrom.RAdd_2_4,
            'RAdd_3': this.editFrom.RAdd_3,
            'RAdd_3_1': this.editFrom.RAdd_3_1,
            'RAdd_3_2': this.editFrom.RAdd_3_2,
            'RAdd_4': this.editFrom.RAdd_4,
            'RAdd_5': this.editFrom.RAdd_5,
            'RAdd_6': this.editFrom.RAdd_6,
            'RAdd_7': this.editFrom.RAdd_7,
            'RAdd_8': this.editFrom.RAdd_8,
            'RAdd_9': this.editFrom.RAdd_9,
            'BAdd': this.editFrom.BAdd,
            'BAdd_1': this.editFrom.BAdd_1,
            'BAdd_1_1': this.editFrom.BAdd_1_1,
            'BAdd_1_2': this.editFrom.BAdd_1_2,
            'BAdd_2': this.editFrom.BAdd_2,
            'BAdd_2_1': this.editFrom.BAdd_2_1,
            'BAdd_2_2': this.editFrom.BAdd_2_2,
            'BAdd_2_3': this.editFrom.BAdd_2_3,
            'BAdd_2_4': this.editFrom.BAdd_2_4,
            'BAdd_3': this.editFrom.BAdd_3,
            'BAdd_3_1': this.editFrom.BAdd_3_1,
            'BAdd_3_2': this.editFrom.BAdd_3_2,
            'BAdd_4': this.editFrom.BAdd_4,
            'BAdd_5': this.editFrom.BAdd_5,
            'BAdd_6': this.editFrom.BAdd_6,
            'BAdd_7': this.editFrom.BAdd_7,
            'BAdd_8': this.editFrom.BAdd_8,
            'BAdd_9': this.editFrom.BAdd_9,
            'ExpectRent': this.editFrom.ExpectRent,
            'AnticipateRent': this.editFrom.AnticipateRent,
            'HavePet': this.editFrom.HavePet,
            'AllowPet': this.editFrom.AllowPet,
            'Note': this.editFrom.Note,
            'HavePrivilege': this.editFrom.HavePrivilege,
            'Id': this.currentId
          }
          var url = 'PotentialCustomers/Insert'
          if (this.currentId !== '') {
            url = 'PotentialCustomers/Update'
          }
          savePotentialCustomers(data, url).then(res => {
            if (res.Success) {
              this.$message({
                message: '恭喜你，操作成功',
                type: 'success'
              })
              this.dialogEditFormVisible = false
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
    saveVREditForm() {
      this.$refs['vrEditFrom'].validate((valid) => {
        if (valid) {
          const data = {
            'PID': this.editFrom.PID,
            'VisitTime': this.vrEditFrom.VisitTime,
            'Status': this.vrEditFrom.Status,
            'Sales': this.editFrom.Sales,
            'Note': this.vrEditFrom.Note,
            'CreatorTime': this.vrEditFrom.CreatorTime,
            'CreatorUserId': this.vrEditFrom.CreatorUserId,
            'CreatorUserOrgId': this.vrEditFrom.CreatorUserOrgId,
            'CreatorUserDeptId': this.vrEditFrom.CreatorUserDeptId,
            'DeleteMark': this.vrEditFrom.DeleteMark,
            'DeleteTime': this.vrEditFrom.DeleteTime,
            'DeleteUserId': this.vrEditFrom.DeleteUserId,
            'LastModifyTime': this.vrEditFrom.LastModifyTime,
            'LastModifyUserId': this.vrEditFrom.LastModifyUserId,

            'Id': this.currentVRId
          }

          var url = 'VisitingRecord/Insert'
          if (this.currentVRId !== '') {
            url = 'VisitingRecord/Update'
          }
          saveVisitingRecord(data, url).then(res => {
            if (res.Success) {
              this.$message({
                message: '恭喜你，操作成功',
                type: 'success'
              })
              this.dialogVREditFormVisible = false
              this.currentVRSelected = ''
              this.loadVRTableData()
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
        // 系統日期大於建立日期，不可刪除
        const cDateString = this.currentSelected[0].CreatorTime
        if (cDateString) {
          if (this.compareCreateDate(cDateString)) {
            this.$alert('隔日資料無法刪除', '警告')
            return false
          }
        }

        const that = this
        this.$confirm('是否確認刪除所選的數據項?', '警告', {
          confirmButtonText: '確定',
          cancelButtonText: '取消',
          type: 'warning'
        }).then(function() {
          var currentIds = []
          that.currentSelected.forEach(element => {
            currentIds.push(element.PID)
          })
          const data = {
            Ids: currentIds
          }
          return deletePotentialCustomers(data)
        }).then(res => {
          if (res.Success) {
            that.$message({
              message: '恭喜你，刪除成功',
              type: 'success'
            })
            that.currentSelected = ''
            that.loadTableData()
          } else {
            this.$message({
              message: res.ErrMsg,
              type: 'error'
            })
          }
        })
      }
    },
    deleteVRPhysics: function() {
      if (this.currentVRSelected.length === 0) {
        this.$alert('請先選擇要操作的數據', '提示')
        return false
      } else {
        // 系統日期大於建立日期，不可刪除
        const cDateString = this.currentVRSelected[0].CreatorTime
        if (cDateString) {
          if (this.compareCreateDate(cDateString)) {
            this.$alert('隔日訪談記錄無法刪除', '警告')
            return false
          }
        }

        const that = this
        this.$confirm('是否確認刪除所選的數據項?', '警告', {
          confirmButtonText: '確定',
          cancelButtonText: '取消',
          type: 'warning'
        }).then(function() {
          var currentVRIds = []
          var pid = ''
          var status = ''
          that.currentVRSelected.forEach(element => {
            currentVRIds.push(element.Id)
            pid = element.PID
            status = element.Status
          })
          const data = {
            Ids: currentVRIds,
            Id: pid,
            Status: status
          }
          return deleteVisitingRecord(data)
        }).then(res => {
          if (res.Success) {
            that.$message({
              message: '恭喜你，刪除成功',
              type: 'success'
            })
            that.currentVRSelected = ''
            that.loadVRTableData()
          } else {
            this.$message({
              message: res.ErrMsg,
              type: 'error'
            })
          }
        })
      }
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
        this.ShowEditOrViewDialog('edit');
      })
    },
    /**
     *雙擊開啟明細
     */
    vrrowdblclick(row, column, event) {
      this.$refs.vrgridtable.clearSelection()
      this.currentVRSelected = ''
      this.rerenderGridtable = Date.now() // 強制重繪 https://bbchin.com/archives/el-table-rerender
      this.$nextTick(function() {
        // https://ithelp.ithome.com.tw/articles/10240669
        this.$refs.vrgridtable.toggleRowSelection(row, true)
        this.currentVRSelected = this.$refs.vrgridtable.selection
        this.ShowVREditOrViewDialog('edit');
      })
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
     * 當表格的排序條件發生變化的時候會觸發該事件
     */
    handleVRSortChange: function(column) {
      this.sortableData.sort = column.prop
      if (column.order === 'ascending') {
        this.sortableData.order = 'asc'
      } else {
        this.sortableData.order = 'desc'
      }
      this.loadVRTableData()
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
    handleVRSelectChange: function(selection, row) {
      this.currentVRSelected = selection
    },
    /**
     * 當用戶手動勾選全選checkbox事件
     */
    handleVRSelectAllChange: function(selection) {
      this.currentVRSelected = selection
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
    assignSales() {
      if (this.currentSelected.length === 0) {
        this.$alert('請先選擇要操作的數據', '提示')
        return false
      } else {
        var pIds = []
        this.currentSelected.forEach(element => {
          pIds.push(element.PID)
        })
        const data = {
          Ids: pIds,
          Sales: this.editFrom.ASales
        }
        assignSales(data).then(res => {
          if (res.Success) {
            this.$message({
              message: '恭喜你，操作成功',
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
    },
    copyPotentialCustomerData() {
      if (this.currentSelected.length === 0) {
        this.$alert('請先選擇要操作的數據', '提示')
        return false
      } else {
        var pIds = []
        var allowCopy = []
        var cells = []
        this.currentSelected.forEach(element => {
          pIds.push(element.PID)
          allowCopy.push((element.IsClosed === '是' || element.IsTransfer === '是') ? 'N' : 'Y')
          cells.push(element.Cell)
        })
        const data = {
          Ids: pIds,
          AllowCopy: allowCopy,
          Cells: cells,
          AArea_1: this.editFrom.AArea_1,
          AArea_2: this.editFrom.AArea_2
        }
        copyPotentialCustomerData(data).then(res => {
          if (res.Success) {
            this.$message({
              // message: '恭喜你，操作成功',
              message: res.ErrMsg,
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
    },
    transferClient() {
      if (this.currentSelected.length > 1 || this.currentSelected.length === 0) {
        this.$alert('請選擇一位潛在客進行轉正', '提示')
        return false
      } else {
        const IsTransfer = this.currentSelected[0].IsTransfer
        // const salesName = this.currentSelected[0].SalesName
        const sales = this.currentSelected[0].Sales

        if (IsTransfer === '是') {
          this.$alert('此潛在客已轉正', '警告')
          return false
        } else if (!sales) {
          this.$alert('未指派業務,無法轉正', '警告')
          return false
        } else if (this.name !== sales) {
          this.$alert('非負責業務,無法轉正', '警告')
          return false
        }
        this.currentId = this.currentSelected[0].Id
        getPotentialCustomersDetail(this.currentId).then(res => {
          this.editFrom = res.ResData
          // 需個性化處理內容

          // 處理出租房屋地址
          this.sendedformL = {
            Add_1: this.editFrom.BAdd_1,
            Add_1_1: this.editFrom.BAdd_1_1,
            Add_1_2: this.editFrom.BAdd_1_2,
            Add_2: this.editFrom.BAdd_2,
            Add_2_1: this.editFrom.BAdd_2_1,
            Add_2_2: this.editFrom.BAdd_2_2,
            Add_2_3: this.editFrom.BAdd_2_3,
            Add_2_4: this.editFrom.BAdd_2_4,
            Add_3: this.editFrom.BAdd_3,
            Add_3_1: this.editFrom.BAdd_3_1,
            Add_3_2: this.editFrom.BAdd_3_2,
            Add_4: this.editFrom.BAdd_4,
            Add_5: this.editFrom.BAdd_5,
            Add_6: this.editFrom.BAdd_6,
            Add_7: this.editFrom.BAdd_7,
            Add_8: this.editFrom.BAdd_8,
            Add_9: this.editFrom.BAdd_9
          }

          // 處理房屋戶籍地址
          this.sendedformR = {
            Add_1: this.editFrom.RAdd_1,
            Add_1_1: this.editFrom.RAdd_1_1,
            Add_1_2: this.editFrom.RAdd_1_2,
            Add_2: this.editFrom.RAdd_2,
            Add_2_1: this.editFrom.RAdd_2_1,
            Add_2_2: this.editFrom.RAdd_2_2,
            Add_2_3: this.editFrom.RAdd_2_3,
            Add_2_4: this.editFrom.RAdd_2_4,
            Add_3: this.editFrom.RAdd_3,
            Add_3_1: this.editFrom.RAdd_3_1,
            Add_3_2: this.editFrom.RAdd_3_2,
            Add_4: this.editFrom.RAdd_4,
            Add_5: this.editFrom.RAdd_5,
            Add_6: this.editFrom.RAdd_6,
            Add_7: this.editFrom.RAdd_7,
            Add_8: this.editFrom.RAdd_8,
            Add_9: this.editFrom.RAdd_9
          }

          this.dialogTransferClientFormVisible = true
        })
      }
    },
    doTransferClient() {
      this.$refs['transferFrom'].validate((valid) => {
        if (valid) {
          if (this.editFrom.Identity === '出租人') {
            if ((!this.editFrom.BAdd_1 || !this.editFrom.BAdd_2 || !this.editFrom.BAdd_3)) {
              this.$alert('請填寫地址', '警告')
              return false
            }
          }

          const data = {
            'PID': this.editFrom.PID,
            'LRID': this.editFrom.LRID,
            'Name': this.editFrom.Name,
            'Identity': this.editFrom.Identity,
            'Identity2': this.editFrom.Identity2,
            'RAdd': this.editFrom.RAdd,
            'RAdd_1': this.editFrom.RAdd_1,
            'RAdd_1_1': this.editFrom.RAdd_1_1,
            'RAdd_1_2': this.editFrom.RAdd_1_2,
            'RAdd_2': this.editFrom.RAdd_2,
            'RAdd_2_1': this.editFrom.RAdd_2_1,
            'RAdd_2_2': this.editFrom.RAdd_2_2,
            'RAdd_2_3': this.editFrom.RAdd_2_3,
            'RAdd_2_4': this.editFrom.RAdd_2_4,
            'RAdd_3': this.editFrom.RAdd_3,
            'RAdd_3_1': this.editFrom.RAdd_3_1,
            'RAdd_3_2': this.editFrom.RAdd_3_2,
            'RAdd_4': this.editFrom.RAdd_4,
            'RAdd_5': this.editFrom.RAdd_5,
            'RAdd_6': this.editFrom.RAdd_6,
            'RAdd_7': this.editFrom.RAdd_7,
            'RAdd_8': this.editFrom.RAdd_8,
            'RAdd_9': this.editFrom.RAdd_9,
            'BAdd': this.editFrom.BAdd,
            'BAdd_1': this.editFrom.BAdd_1,
            'BAdd_1_1': this.editFrom.BAdd_1_1,
            'BAdd_1_2': this.editFrom.BAdd_1_2,
            'BAdd_2': this.editFrom.BAdd_2,
            'BAdd_2_1': this.editFrom.BAdd_2_1,
            'BAdd_2_2': this.editFrom.BAdd_2_2,
            'BAdd_2_3': this.editFrom.BAdd_2_3,
            'BAdd_2_4': this.editFrom.BAdd_2_4,
            'BAdd_3': this.editFrom.BAdd_3,
            'BAdd_3_1': this.editFrom.BAdd_3_1,
            'BAdd_3_2': this.editFrom.BAdd_3_2,
            'BAdd_4': this.editFrom.BAdd_4,
            'BAdd_5': this.editFrom.BAdd_5,
            'BAdd_6': this.editFrom.BAdd_6,
            'BAdd_7': this.editFrom.BAdd_7,
            'BAdd_8': this.editFrom.BAdd_8,
            'BAdd_9': this.editFrom.BAdd_9,
            'Tel': this.editFrom.Tel,
            'Tel_1': this.editFrom.Tel_1,
            'Tel_2': this.editFrom.Tel_2,
            'Cell': this.editFrom.Cell,
            'Sales': this.editFrom.Sales,
            'BPropoty': this.editFrom.BPropoty
          }

          transferClient(data).then(res => {
            if (res.Success) {
              this.$message({
                message: '轉正成功',
                type: 'success'
              })
              this.dialogTransferClientFormVisible = false
              this.currentSelected = ''
              this.loadTableData()
              this.InitDictItem()
            }
          })
        }
      })
    },
    geteditaddress(addressData, title) {
      if (title === '房屋戶籍地址') {
        this.editFrom.RAdd_1 = addressData.Add_1;
        this.editFrom.RAdd_1_1 = addressData.Add_1_1;
        this.editFrom.RAdd_1_2 = addressData.Add_1_2;
        this.editFrom.RAdd_2 = addressData.Add_2;
        this.editFrom.RAdd_2_1 = addressData.Add_2_1;
        this.editFrom.RAdd_2_2 = addressData.Add_2_2;
        this.editFrom.RAdd_2_3 = addressData.Add_2_3;
        this.editFrom.RAdd_2_4 = addressData.Add_2_4;
        this.editFrom.RAdd_3 = addressData.Add_3;
        this.editFrom.RAdd_3_1 = addressData.Add_3_1;
        this.editFrom.RAdd_3_2 = addressData.Add_3_2;
        this.editFrom.RAdd_4 = addressData.Add_4;
        this.editFrom.RAdd_5 = addressData.Add_5;
        this.editFrom.RAdd_6 = addressData.Add_6;
        this.editFrom.RAdd_7 = addressData.Add_7;
        this.editFrom.RAdd_8 = addressData.Add_8;
        this.editFrom.RAdd_9 = addressData.Add_9;
      }
      if (title === '出租房屋地址') {
        this.editFrom.BAdd_1 = addressData.Add_1;
        this.editFrom.BAdd_1_1 = addressData.Add_1_1;
        this.editFrom.BAdd_1_2 = addressData.Add_1_2;
        this.editFrom.BAdd_2 = addressData.Add_2;
        this.editFrom.BAdd_2_1 = addressData.Add_2_1;
        this.editFrom.BAdd_2_2 = addressData.Add_2_2;
        this.editFrom.BAdd_2_3 = addressData.Add_2_3;
        this.editFrom.BAdd_2_4 = addressData.Add_2_4;
        this.editFrom.BAdd_3 = addressData.Add_3;
        this.editFrom.BAdd_3_1 = addressData.Add_3_1;
        this.editFrom.BAdd_3_2 = addressData.Add_3_2;
        this.editFrom.BAdd_4 = addressData.Add_4;
        this.editFrom.BAdd_5 = addressData.Add_5;
        this.editFrom.BAdd_6 = addressData.Add_6;
        this.editFrom.BAdd_7 = addressData.Add_7;
        this.editFrom.BAdd_8 = addressData.Add_8;
        this.editFrom.BAdd_9 = addressData.Add_9;
      }
    },
    resetData(val) {
      if (this.editFormTitle === '新增') {
        if (val === '出租人') {
          // this.editFrom.RSource = ''
          this.editFrom.Source = ''
          this.editFrom.ExpectRent = ''
          this.editFrom.HavePet = ''
          this.editFrom.RAdd_1 = ''
          this.editFrom.RAdd_1_1 = ''
          this.editFrom.RAdd_1_2 = ''
          this.editFrom.RAdd_2 = ''
          this.editFrom.RAdd_2_1 = ''
          this.editFrom.RAdd_2_2 = ''
          this.editFrom.RAdd_2_3 = ''
          this.editFrom.RAdd_2_4 = ''
          this.editFrom.RAdd_3 = ''
          this.editFrom.RAdd_3_1 = ''
          this.editFrom.RAdd_3_2 = ''
          this.editFrom.RAdd_4 = ''
          this.editFrom.RAdd_5 = ''
          this.editFrom.RAdd_6 = ''
          this.editFrom.RAdd_7 = ''
          this.editFrom.RAdd_8 = ''
          this.editFrom.RAdd_9 = ''
          this.sendedformR = {}
          this.Source = [
            { value: '業務開發', label: '業務開發' },
            { value: '門市洽詢', label: '門市洽詢' },
            { value: '網路社群', label: '網路社群' },
            { value: '電銷開發', label: '電銷開發' },
            { value: '0800資源', label: '0800資源' },
            { value: '跨區轉介', label: '跨區轉介' },
            { value: '公部門轉介', label: '公部門轉介' },
            { value: '活動問卷', label: '活動問卷' }
          ]
        } else {
          // this.editFrom.LSource = ''
          this.editFrom.Source = ''
          this.editFrom.AnticipateRent = ''
          this.editFrom.AllowPet = ''
          this.editFrom.BAdd_1 = ''
          this.editFrom.BAdd_1_1 = ''
          this.editFrom.BAdd_1_2 = ''
          this.editFrom.BAdd_2 = ''
          this.editFrom.BAdd_2_1 = ''
          this.editFrom.BAdd_2_2 = ''
          this.editFrom.BAdd_2_3 = ''
          this.editFrom.BAdd_2_4 = ''
          this.editFrom.BAdd_3 = ''
          this.editFrom.BAdd_3_1 = ''
          this.editFrom.BAdd_3_2 = ''
          this.editFrom.BAdd_4 = ''
          this.editFrom.BAdd_5 = ''
          this.editFrom.BAdd_6 = ''
          this.editFrom.BAdd_7 = ''
          this.editFrom.BAdd_8 = ''
          this.editFrom.BAdd_9 = ''
          this.sendedformL = {}
          this.Source = [
            { value: '業務開發', label: '業務開發' },
            { value: '現有承租人', label: '現有承租人' },
            { value: '門市洽詢', label: '門市洽詢' },
            { value: '網路社群', label: '網路社群' },
            { value: '電銷開發', label: '電銷開發' },
            { value: '0800資源', label: '0800資源' },
            { value: '跨區轉介', label: '跨區轉介' },
            { value: '公部門轉介', label: '公部門轉介' },
            { value: '活動問卷', label: '活動問卷' }
          ]
        }
      }
    },
    handleSelectAreaSearchChange: function(departmentId) {
      getSalesByDepartmentId(departmentId).then(res => {
        this.SSales = res.ResData.map(element => {
          if (element.RealName === '待指派') {
            element.RealName = '不指定'
          }
          return element;
        })
      })
    },
    handleSelectAreaAllChange: function() {
      const category = this.$refs['AreaEdit'].getCheckedNodes()[0];
      if (category) {
        this.editFrom.AArea_1 = category.pathLabels[category.pathLabels.length - 2]
        this.editFrom.AArea_2 = category.pathLabels[category.pathLabels.length - 1]
      }
    },
    handleSelectAreaChange: function() {
      const category = this.$refs['AreaEdit'].getCheckedNodes()[0];
      if (category) {
        switch (category.pathLabels.length) {
          case 1:
            this.editFrom.Area_1 = category.pathLabels[0]
            this.editFrom.Area_2 = ''
            this.editFrom.Area = category.pathLabels[0];
            break;
          case 2:
            this.editFrom.Area_1 = category.pathLabels[0]
            this.editFrom.Area_2 = category.pathLabels[1]
            this.editFrom.Area = category.pathLabels[1];
            break;
        }
      }
    },
    compareCreateDate(createDate) {
      var now = new Date()
      const today = moment(now).format('YYYYMMDD')
      if (createDate) {
        return moment(createDate).isBefore(today)
      }
    }
  }
}
</script>

<style>
</style>
