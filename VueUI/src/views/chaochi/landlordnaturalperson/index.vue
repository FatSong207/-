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
          <el-form-item label="姓名：">
            <el-input
              v-model="searchform.LNName"
              clearable
            />
          </el-form-item>
          <el-form-item label="身分證字號/居留證號：">
            <el-input
              v-model="searchform.LNID"
              clearable
            />
          </el-form-item>
          <el-form-item label="生日：">
            <!-- <el-date-picker
              v-model="searchform.LNBirthday"
              type="date"
              align="right"
              value-format="yyyy-MM-dd"
            /> -->
            <DatePickerE
              v-model="searchform.LNBirthday"
              value-format="yyyy-MM-dd"
              format="yyyy-MM-dd"
              type="date"
              placeholder="選擇日期"
            />
          </el-form-item>
          <el-form-item label="手機：">
            <el-input
              v-model="searchform.LNCell"
              clearable
            />
          </el-form-item>
          <el-form-item label="電話：">
            <el-input
              v-model="searchform.LNTel_2"
              placeholder="請輸入電話"
              clearable
              style="width: 220px"
              class="input-with-select"
              :maxlength="8"
              onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
            >
              <el-select
                slot="prepend"
                v-model="searchform.LNTel_1"
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
              v-model="searchform.LNTel_1"
              style="width: 15%"
              clearable
              maxlength="2"
            />
            <el-form-item label="－">
              <el-input
                v-model="searchform.LNTel_2"
                clearable
              />
            </el-form-item> -->
          </el-form-item>
          <el-form-item
            v-hasPermi="['CustomerLN/CreatorId']"
            label="歸屬業務："
          >
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
          <!-- <el-row> //1202先拿掉
            <el-form-item label="戶籍地址：">
              <el-input
                v-model="searchform.LNAdd"
                clearable
              />
            </el-form-item>
          </el-row> -->
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
            v-hasPermi="['CustomerLN/Add']"
            type="primary"
            icon="el-icon-plus"
            size="small"
            @click="ShowEditOrViewDialog()"
          >新增</el-button>
          <el-button
            v-hasPermi="['CustomerLN/Edit']"
            type="primary"
            icon="el-icon-edit"
            class="el-button-modify"
            size="small"
            @click="ShowEditOrViewDialog('edit')"
          >修改</el-button>
          <!-- <el-button v-hasPermi="['User/Enable']" type="info" icon="el-icon-video-pause" size="small" @click="setEnable('0')">禁用</el-button>
          <el-button v-hasPermi="['User/Enable']" type="success" icon="el-icon-video-play" size="small" @click="setEnable('1')">啟用</el-button>
          <el-button v-hasPermi="['User/DeleteSoft']" type="warning" icon="el-icon-delete" size="small" @click="deleteSoft('0')">軟刪除</el-button> -->
          <el-button
            v-hasPermi="['CustomerLN/Delete']"
            type="danger"
            icon="el-icon-delete"
            size="small"
            @click="deletePhysics()"
          >刪除</el-button>
          <!-- <el-button v-hasPermi="['User/ResetPassword']" type="default" icon="el-icon-refresh-right" size="small" @click="handleResetPassword()">重置密碼</el-button> -->
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
          prop="LNName"
          label="姓名"
          sortable="custom"
          width="90"
          fixed
        />
        <el-table-column
          prop="LNGender1"
          label="性別"
          sortable="custom"
          width="80"
          align="center"
        >
          <template slot-scope="scope">
            {{
              scope.row.LNGender1 === '/Yes'
                ? '男'
                : scope.row.LNGender2 === '/Yes'
                  ? '女'
                  : '未知'
            }}
          </template>
        </el-table-column>
        <el-table-column
          prop="LNID"
          label="身分證字號/居留證號"
          sortable="custom"
          width="200"
        />
        <el-table-column
          prop="LNBirthday"
          label="生日"
          sortable="custom"
          width="120"
          align="center"
        />
        <el-table-column
          prop="LNCell"
          label="手機"
          sortable="custom"
          width="120"
          align="center"
        />
        <el-table-column
          prop="LNTel"
          label="電話"
          sortable="custom"
          width="120"
          align="center"
        />
        <el-table-column
          prop="CreatorUserName"
          label="歸屬業務"
          sortable="custom"
        >
          <!-- <template slot-scope="scope">
            {{ scope.row.Gender=== 1 ? 'GIGI' : 'AAA' }}
          </template> -->
        </el-table-column>
        <!-- <el-table-column prop="Line" label="Line" sortable="custom" width="120" align="center" /> -->
        <el-table-column
          prop="LNMail"
          label="EMAIL"
          width="220"
          sortable="custom"
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
        <!-- <el-table-column
          prop="CreatorTime"
          label="建立時間"
          width="180"
          sortable
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
      fullscreen
      append-to-body
      :modal-append-to-body="false"
    >
      <el-tabs
        v-model="activeName"
        v-loading="loading"
        v-loading.lock="loading"
        element-loading-text="載入中..."
        type="border-card"
      >
        <el-tab-pane
          label="出租自然人基本資料"
          name="A"
        >
          <el-card class="box-card">
            <el-form
              ref="editFrom"
              :inline="true"
              :model="editFrom"
              :rules="rules"
              class="demo-form-inline"
            >
              <el-row>
                <el-col :span="6">
                  <el-form-item
                    label="姓名："
                    :label-width="formLabelWidth"
                    prop="LNName"
                  >
                    <el-input
                      v-model="editFrom.LNName"
                      placeholder="請輸入姓名"
                      autocomplete="off"
                      clearable
                    />
                  </el-form-item>
                </el-col>
                <el-col :span="6">
                  <el-form-item
                    label="身分證字號/居留證號："
                    :label-width="formLabelWidth"
                    prop="LNID"
                  >
                    <el-input
                      v-model="editFrom.LNID"
                      v-upperCase
                      placeholder="請輸入身分證字號"
                      autocomplete="off"
                      clearable
                      maxlength="10"
                      :disabled="LNdisabled"
                    />
                  </el-form-item>
                </el-col>
                <el-col :span="6">
                  <el-form-item
                    label="生日："
                    :label-width="formLabelWidth"
                    prop="LNBirthday"
                  >
                    <DatePickerE
                      v-model="editFrom.LNBirthday"
                      value-format="yyyy-MM-dd"
                      format="yyyy-MM-dd"
                      type="date"
                      placeholder="選擇日期"
                    />
                  </el-form-item>
                </el-col>
                <el-col :span="6">
                  <el-form-item
                    label="性別："
                    :label-width="formLabelWidth"
                    prop="gender"
                    class="el-form-item is-required el-form-item--medium"
                  >
                    <el-radio-group v-model="gender">
                      <el-radio label="/Yes">男</el-radio>
                      <el-radio label="/Off">女</el-radio>
                    </el-radio-group>
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row>
                <el-col :span="6">
                  <el-form-item
                    label="電話(日)："
                    :label-width="formLabelWidth"
                  >
                    <el-input
                      v-model="editFrom.LNTel_2"
                      placeholder="請輸入電話"
                      clearable
                      style="width: 220px"
                      class="input-with-select"
                      :maxlength="8"
                      onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                    >
                      <el-select
                        slot="prepend"
                        v-model="editFrom.LNTel_1"
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
                </el-col>
                <el-col :span="6">
                  <el-form-item
                    label="電話(夜)："
                    :label-width="formLabelWidth"
                  >
                    <el-input
                      v-model="editFrom.LNTelN_2"
                      placeholder="請輸入電話"
                      clearable
                      style="width: 220px"
                      class="input-with-select"
                      :maxlength="8"
                      onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                    >
                      <el-select
                        slot="prepend"
                        v-model="editFrom.LNTelN_1"
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
                </el-col>
                <el-col :span="6">
                  <el-form-item
                    label="手機："
                    :label-width="formLabelWidth"
                    prop="LNCell"
                  >
                    <el-input
                      v-model="editFrom.LNCell"
                      placeholder="請輸入手機號碼"
                      autocomplete="off"
                      clearable
                      show-word-limit
                      onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                    />
                  </el-form-item>
                </el-col>
                <el-col :span="6">
                  <el-form-item
                    label="電子信箱："
                    :label-width="formLabelWidth"
                    prop="LNMail"
                  >
                    <el-input
                      v-model="editFrom.LNMail"
                      placeholder="請輸入電子信箱"
                      autocomplete="off"
                      clearable
                    />
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row>
                <el-col :span="6">
                  <el-form-item
                    label="LINE ID："
                    :label-width="formLabelWidth"
                  >
                    <el-input
                      v-model="editFrom.LNLine"
                      placeholder="請輸入LINEID"
                      autocomplete="off"
                      clearable
                    />
                  </el-form-item>
                </el-col>
                <el-col :span="6">
                  <el-form-item
                    label="公司："
                    :label-width="formLabelWidth"
                  >
                    <el-input
                      v-model="editFrom.LNCompany"
                      placeholder="請輸入公司"
                      autocomplete="off"
                      clearable
                    />
                  </el-form-item>
                </el-col>
                <el-col :span="6">
                  <el-form-item
                    label="職稱："
                    :label-width="formLabelWidth"
                  >
                    <el-input
                      v-model="editFrom.LNJobtitle"
                      placeholder="請輸入職稱"
                      autocomplete="off"
                      clearable
                    />
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row>
                <el-col :span="24">
                  <el-form-item
                    label="戶籍地址："
                    :label-width="formLabelWidth"
                    class="el-form-item is-required el-form-item--medium"
                  >
                    <Address
                      :sendedform="sendedform"
                      title="戶籍地址"
                      @geteditaddress="geteditaddress"
                    />
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row>
                <el-col :span="24">
                  <el-form-item
                    label=" "
                    label-width="80px"
                  >
                    <el-checkbox
                      v-model="LNAddSameCom"
                    >通訊地址同戶籍地址</el-checkbox>
                  </el-form-item>
                  <el-form-item
                    v-show="LNAddSameCom === false"
                    label="通訊地址："
                  >
                    <Address
                      :sendedform="sendedform1"
                      :resetall="!LNAddSameCom"
                      title="通訊地址"
                      @geteditaddress="geteditaddress"
                    />
                  </el-form-item>
                </el-col>
              </el-row>
              <div style="padding-top: 25px">
                <el-divider content-position="left">
                  <h2>代理人A</h2>
                </el-divider>
              </div>
              <el-row>
                <el-col :span="6">
                  <el-form-item
                    label="姓名："
                    :label-width="formLabelWidth"
                    prop="LNAGName_A"
                  >
                    <el-input
                      v-model="editFrom.LNAGName_A"
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
                    prop="LNAGID_A"
                  >
                    <el-input
                      v-model="editFrom.LNAGID_A"
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
                    prop="LNAGCell_A"
                  >
                    <el-input
                      v-model="editFrom.LNAGCell_A"
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
                      v-model="editFrom.LNAGTel_2_A"
                      placeholder="請輸入電話"
                      clearable
                      style="width: 220px"
                      :maxlength="8"
                      class="input-with-select"
                    >
                      <el-select
                        slot="prepend"
                        v-model="editFrom.LNAGTel_1_A"
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
                </el-col>
              </el-row>
              <el-row>
                <el-col :span="24">
                  <el-form-item
                    label="戶籍地址："
                    :label-width="formLabelWidth"
                  >
                    <Address
                      :sendedform="sendedformAGA"
                      title="代A戶籍地址"
                      @geteditaddress="geteditaddress"
                    />
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row>
                <el-col :span="24">
                  <el-form-item
                    label=" "
                    label-width="80px"
                  >
                    <el-checkbox
                      v-model="LNAGAddSameCom"
                    >通訊地址同戶籍地址</el-checkbox>
                  </el-form-item>
                  <el-form-item
                    v-show="LNAGAddSameCom === false"
                    label="通訊地址："
                  >
                    <Address
                      :sendedform="sendedformAGAC"
                      :resetall="LNAGAddSameCom"
                      title="代A通訊地址"
                      @geteditaddress="geteditaddress"
                    />
                  </el-form-item>
                </el-col>
              </el-row>
              <!-- <hr>
              <el-row>
                <el-form-item
                  label="代理人B："
                  :label-width="formLabelWidth"
                />
              </el-row> -->
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
                    prop="LNAGName_B"
                  >
                    <el-input
                      v-model="editFrom.LNAGName_B"
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
                    prop="LNAGID_B"
                  >
                    <el-input
                      v-model="editFrom.LNAGID_B"
                      v-upperCase
                      placeholder="請輸入身份證字號/居留證號"
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
                    prop="LNAGCell_B"
                  >
                    <el-input
                      v-model="editFrom.LNAGCell_B"
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
                      v-model="editFrom.LNAGTel_2_B"
                      placeholder="請輸入電話"
                      clearable
                      style="width: 220px"
                      :maxlength="8"
                      class="input-with-select"
                    >
                      <el-select
                        slot="prepend"
                        v-model="editFrom.LNAGTel_1_B"
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
                      title="代B戶籍地址"
                      @geteditaddress="geteditaddress"
                    />
                  </el-form-item>
                </el-col>
              </el-row>
            </el-form>
            <div class="rightbtn">
              <el-button
                v-hasPermi="['CustomerLN/Edit']"
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
          label="出租自然人匯款資料"
          name="B"
        >
          <LNRemittance
            :id="currentId"
            :idno="editFrom.LNID"
            :name="editFrom.LNName"
            @cancel="closeDialogAndReset()"
          />
        </el-tab-pane>
        <el-tab-pane
          v-if="editFormTitle === '編輯'"
          label="出租自然人附件"
          name="D"
        >
          <el-card class="box-card">
            <LNFormHistory
              :id="currentId"
              :lnid="editFrom.LNID"
              @cancel="closeDialogAndReset()"
            />
          </el-card>
        </el-tab-pane>
      </el-tabs>
    </el-dialog>
    <el-dialog
      ref="dialogEditForm"
      title="創建建物"
      :show-close="false"
      :close-on-press-escape="false"
      :visible.sync="dialogEditFormVisibleCBB"
      width="1340px"
      append-to-body
    >
      <el-card class="box-card">
        <el-form
          ref="editFrom"
          v-loading="CBBloading"
          v-loading.lock="CBBloading"
          element-loading-text="請稍後..."
          :inline="true"
          class="demo-form-inline"
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
              v-model="select"
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
  getCustomerLNListWithPager,
  getCustomerLNDetail,
  saveCustomerLN,
  deleteCustomerLN,
  resetPassword
} from '@/api/chaochi/landlordnaturalperson/customerlnservice';
import { saveBuilding } from '@/api/chaochi/building/buildingservice';
import { getSales } from '@/api/security/userservice';
import * as fecha from 'element-ui/lib/utils/date';
import LNRemittance from './LNRemittance.vue';
import Address from '@/components/Address/Address.vue';
import DatePickerE from '@/components/RocDatepickerE';
import LNFormHistory from './LNFormHistory.vue';
import { mapGetters } from 'vuex';
import { mergerAddress } from '@/utils/index';
import {
  validateCellReg,
  validateEamilReg,
  validateIDReg
} from '@/utils/validate';

export default {
  name: 'CustomerLN',
  components: { Address, LNFormHistory, DatePickerE, LNRemittance },
  data() {
    return {
      sendedform: {},
      sendedform1: {},
      sendedformAGA: {},
      sendedformAGAC: {},
      sendedformAGB: {},
      sendedformCBB: {},
      searchform: {
        LNName: '',
        LNID: '',
        LNBirthday: '',
        LNCell: '',
        LNTel_1: '',
        LNTel_2: '',
        LNAdd: '',
        RoleId: '',
        Keywords: '',
        CreateTime: ''
      },
      editFrom: {
        LNGender1: '',
        LNGender2: '',
        LNBirthday: ''
      },
      pagination: {
        currentPage: 1,
        pagesize: 20,
        pageTotal: 0
      },
      sortableData: {
        order: 'desc',
        sort: 'CreatorTime'
      },
      rules: {
        LNID: [
          {
            required: true,
            trigger: 'blur',
            validator: validateIDReg
          }
        ],
        LNName: [{ required: true, message: '請輸入姓名', trigger: 'blur' }],
        LNBirthday: [
          { required: true, message: '請輸入生日', trigger: 'blur' }
        ],
        LNMail: [
          { required: false, trigger: 'change', validator: validateEamilReg }
        ],
        LNAGID_A: [
          {
            required: false,
            trigger: 'blur',
            validator: validateIDReg
          }
        ],
        LNAGID_B: [
          {
            required: false,
            trigger: 'blur',
            validator: validateIDReg
          }
        ],
        LNCell: [
          { required: true, message: '請輸入手機', trigger: 'blur' },
          { trigger: 'change', validator: validateCellReg }
        ],
        LNAGCell_A: [
          { required: false, trigger: 'change', validator: validateCellReg }
        ],
        LNAGCell_B: [
          { required: false, trigger: 'change', validator: validateCellReg }
        ]
      },
      salesOptions: [],
      Sales: [],
      tableData: [],
      currentSelected: [],
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
      loadings: false,
      dialogEditFormVisible: false,
      dialogEditFormVisibleCBB: false,
      historyFormVisible: false,
      LNdisabled: false,
      CBBloading: false,
      LNAddSameCom: false,
      LNAGAddSameCom: false,
      loading: false,
      sales: '',
      select: '',
      activeName: 'A',
      editFormTitle: '',
      formLabelWidth: '170px',
      currentId: '',
      gender: '/Yes'
    };
  },
  computed: {
    ...mapGetters(['bpropotiesOptions'])
  },
  watch: {
    gender: {
      handler(a) {
        if (a === '/Yes') {
          this.editFrom.LNGender1 = '/Yes';
          this.editFrom.LNGender2 = '/Off';
        }
        if (a === '/Off') {
          this.editFrom.LNGender1 = '/Off';
          this.editFrom.LNGender2 = '/Yes';
        }
      }
    },
    'editFrom.LNAddSame': {
      handler(a) {
        if (a === '/Yes') {
          this.LNAddSameCom = true;
        } else {
          this.LNAddSameCom = false;
        }
      }
    },
    'editFrom.LNAGAddSame': {
      handler(a) {
        if (a === '/Yes') {
          this.LNAGAddSameCom = true;
        } else {
          this.LNAGAddSameCom = false;
        }
      }
    },
    LNAddSameCom: {
      handler(a) {
        if (a === true) {
          this.editFrom.LNAddSame = '/Yes';
        }
        if (a === false) {
          this.editFrom.LNAddSame = '/Off';
        }
      }
    },
    LNAGAddSameCom: {
      handler(a) {
        if (a === true) {
          this.editFrom.LNAGAddSame = '/Yes';
        }
        if (a === false) {
          this.editFrom.LNAGAddSame = '/Off';
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
        LNName: this.searchform.LNName,
        LNID: this.searchform.LNID,
        LNBirthday: this.searchform.LNBirthday,
        LNCell: this.searchform.LNCell,
        LNTel_1: this.searchform.LNTel_1,
        LNTel_2: this.searchform.LNTel_2,
        LNAdd: this.searchform.LNAdd,
        CreatorUserId: this.searchform.CreatorId
      };
      var seachdata = {
        CurrenetPageIndex: this.pagination.currentPage,
        PageSize: this.pagination.pagesize,
        Filter: t,
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
        RoleId: this.searchform.RoleId
      };
      getCustomerLNListWithPager(seachdata).then((res) => {
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
      this.editFrom = {
        LNGender1: '/Yes',
        LNGender2: '/Off'
      };
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
          this.LNdisabled = true;
          this.dialogEditFormVisible = true;
          this.loading = true;
          this.bindEditInfo();
        }
      } else {
        this.editFormTitle = '新增';
        this.LNdisabled = false;
        this.editFrom.LNAddSame = '/Off';
        this.editFrom.LNGender1 = '/Yes';
        this.currentId = '';
        this.dialogEditFormVisible = true;
        this.sendedform = {};
        this.sendedform1 = {};
        this.sendedformAGA = {};
        this.sendedformAGAC = {};
        this.sendedformAGB = {};
      }
    },
    bindEditInfo: function() {
      getCustomerLNDetail(this.currentId).then((res) => {
        this.editFrom = res.ResData;
        this.gender = this.editFrom.LNGender1 === '/Yes' ? '/Yes' : '/Off';
        this.sendedform = {
          Add_1: this.editFrom.LNAdd_1,
          Add_1_1: this.editFrom.LNAdd_1_1,
          Add_1_2: this.editFrom.LNAdd_1_2,
          Add_2: this.editFrom.LNAdd_2,
          Add_2_1: this.editFrom.LNAdd_2_1,
          Add_2_2: this.editFrom.LNAdd_2_2,
          Add_2_3: this.editFrom.LNAdd_2_3,
          Add_2_4: this.editFrom.LNAdd_2_4,
          Add_3: this.editFrom.LNAdd_3,
          Add_3_1: this.editFrom.LNAdd_3_1,
          Add_3_2: this.editFrom.LNAdd_3_2,
          Add_4: this.editFrom.LNAdd_4,
          Add_5: this.editFrom.LNAdd_5,
          Add_6: this.editFrom.LNAdd_6,
          Add_7: this.editFrom.LNAdd_7,
          Add_8: this.editFrom.LNAdd_8,
          Add_9: this.editFrom.LNAdd_9
        };
        this.sendedform1 = {
          Add_1: this.editFrom.LNAddC_1,
          Add_1_1: this.editFrom.LNAddC_1_1,
          Add_1_2: this.editFrom.LNAddC_1_2,
          Add_2: this.editFrom.LNAddC_2,
          Add_2_1: this.editFrom.LNAddC_2_1,
          Add_2_2: this.editFrom.LNAddC_2_2,
          Add_2_3: this.editFrom.LNAddC_2_3,
          Add_2_4: this.editFrom.LNAddC_2_4,
          Add_3: this.editFrom.LNAddC_3,
          Add_3_1: this.editFrom.LNAddC_3_1,
          Add_3_2: this.editFrom.LNAddC_3_2,
          Add_4: this.editFrom.LNAddC_4,
          Add_5: this.editFrom.LNAddC_5,
          Add_6: this.editFrom.LNAddC_6,
          Add_7: this.editFrom.LNAddC_7,
          Add_8: this.editFrom.LNAddC_8,
          Add_9: this.editFrom.LNAddC_9
        };
        this.sendedformAGA = {
          Add_1: this.editFrom.LNAGAdd_1_A,
          Add_1_1: this.editFrom.LNAGAdd_1_1_A,
          Add_1_2: this.editFrom.LNAGAdd_1_2_A,
          Add_2: this.editFrom.LNAGAdd_2_A,
          Add_2_1: this.editFrom.LNAGAdd_2_1_A,
          Add_2_2: this.editFrom.LNAGAdd_2_2_A,
          Add_2_3: this.editFrom.LNAGAdd_2_3_A,
          Add_2_4: this.editFrom.LNAGAdd_2_4_A,
          Add_3: this.editFrom.LNAGAdd_3_A,
          Add_3_1: this.editFrom.LNAGAdd_3_1_A,
          Add_3_2: this.editFrom.LNAGAdd_3_2_A,
          Add_4: this.editFrom.LNAGAdd_4_A,
          Add_5: this.editFrom.LNAGAdd_5_A,
          Add_6: this.editFrom.LNAGAdd_6_A,
          Add_7: this.editFrom.LNAGAdd_7_A,
          Add_8: this.editFrom.LNAGAdd_8_A,
          Add_9: this.editFrom.LNAGAdd_9_A
        };
        this.sendedformAGAC = {
          Add_1: this.editFrom.LNAGAddC_1_A,
          Add_1_1: this.editFrom.LNAGAddC_1_1_A,
          Add_1_2: this.editFrom.LNAGAddC_1_2_A,
          Add_2: this.editFrom.LNAGAddC_2_A,
          Add_2_1: this.editFrom.LNAGAddC_2_1_A,
          Add_2_2: this.editFrom.LNAGAddC_2_2_A,
          Add_2_3: this.editFrom.LNAGAddC_2_3_A,
          Add_2_4: this.editFrom.LNAGAddC_2_4_A,
          Add_3: this.editFrom.LNAGAddC_3_A,
          Add_3_1: this.editFrom.LNAGAddC_3_1_A,
          Add_3_2: this.editFrom.LNAGAddC_3_2_A,
          Add_4: this.editFrom.LNAGAddC_4_A,
          Add_5: this.editFrom.LNAGAddC_5_A,
          Add_6: this.editFrom.LNAGAddC_6_A,
          Add_7: this.editFrom.LNAGAddC_7_A,
          Add_8: this.editFrom.LNAGAddC_8_A,
          Add_9: this.editFrom.LNAGAddC_9_A
        };
        this.sendedformAGB = {
          Add_1: this.editFrom.LNAGAdd_1_B,
          Add_1_1: this.editFrom.LNAGAdd_1_1_B,
          Add_1_2: this.editFrom.LNAGAdd_1_2_B,
          Add_2: this.editFrom.LNAGAdd_2_B,
          Add_2_1: this.editFrom.LNAGAdd_2_1_B,
          Add_2_2: this.editFrom.LNAGAdd_2_2_B,
          Add_2_3: this.editFrom.LNAGAdd_2_3_B,
          Add_2_4: this.editFrom.LNAGAdd_2_4_B,
          Add_3: this.editFrom.LNAGAdd_3_B,
          Add_3_1: this.editFrom.LNAGAdd_3_1_B,
          Add_3_2: this.editFrom.LNAGAdd_3_2_B,
          Add_4: this.editFrom.LNAGAdd_4_B,
          Add_5: this.editFrom.LNAGAdd_5_B,
          Add_6: this.editFrom.LNAGAdd_6_B,
          Add_7: this.editFrom.LNAGAdd_7_B,
          Add_8: this.editFrom.LNAGAdd_8_B,
          Add_9: this.editFrom.LNAGAdd_9_B
        };
        this.loading = false;
      });
    },
    /**
     * 新增/修改保存
     */
    saveEditForm() {
      console.log('call');
      const addarray = [
        this.editFrom.LNAdd_1,
        this.editFrom.LNAdd_1_1,
        this.editFrom.LNAdd_1_2,
        this.editFrom.LNAdd_2,
        this.editFrom.LNAdd_2_1,
        this.editFrom.LNAdd_2_2,
        this.editFrom.LNAdd_2_3,
        this.editFrom.LNAdd_2_4,
        this.editFrom.LNAdd_3,
        this.editFrom.LNAdd_3_1,
        this.editFrom.LNAdd_3_2,
        this.editFrom.LNAdd_4,
        this.editFrom.LNAdd_5,
        this.editFrom.LNAdd_6,
        this.editFrom.LNAdd_7,
        this.editFrom.LNAdd_8,
        this.editFrom.LNAdd_9
      ];
      var badd = mergerAddress(addarray);
      if (badd.length < 3) {
        this.$message.error('請輸入戶籍地址');
        return;
      }
      this.$refs['editFrom'].validate((valid) => {
        console.log(valid);
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
          var url = 'CustomerLN/Insert';
          if (this.currentId !== '') {
            url = 'CustomerLN/Update';
          }
          this.loading = true;
          saveCustomerLN(data, url)
            .then((res) => {
              if (res.Success) {
                this.$message({
                  message: '恭喜你，操作成功',
                  type: 'success'
                });
                if (url === 'CustomerLN/Update') {
                  this.bindEditInfo();
                }
                if (url === 'CustomerLN/Insert') {
                  this.closeDialogAndReset();
                }
                // this.dialogEditFormVisible = false;
                // this.currentSelected = '';
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
      if (!this.select) {
        this.$message.warning('請選擇建物屬性 !');
        return;
      }
      const data = this.sendedformCBB;
      this.CBBloading = true;
      data.BPropoty = this.select;
      var url = 'Building/InsertAs';
      saveBuilding(data, this.currentId, url)
        .then((res) => {
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
        })
        .finally(() => {
          this.CBBloading = false;
        });
    },
    closeDialogAndReset() {
      this.dialogEditFormVisible = false;
      this.activeName = 'A';
      this.dialogEditFormVisible = false;
      this.currentSelected = '';
      this.loadTableData();
      this.InitDictItem();
      this.editFrom = {};
      this.sendedform = {};
      this.sendedform1 = {};
      this.sendedformAGA = {};
      this.sendedformAGAC = {};
      this.sendedformAGB = {};
      this.sendedformCBB = {};
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
            return deleteCustomerLN(data);
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
      if (title === '戶籍地址') {
        this.editFrom.LNAdd_1 = addressData.Add_1;
        this.editFrom.LNAdd_1_1 = addressData.Add_1_1;
        this.editFrom.LNAdd_1_2 = addressData.Add_1_2;
        this.editFrom.LNAdd_2 = addressData.Add_2;
        this.editFrom.LNAdd_2_1 = addressData.Add_2_1;
        this.editFrom.LNAdd_2_2 = addressData.Add_2_2;
        this.editFrom.LNAdd_2_3 = addressData.Add_2_3;
        this.editFrom.LNAdd_2_4 = addressData.Add_2_4;
        this.editFrom.LNAdd_3 = addressData.Add_3;
        this.editFrom.LNAdd_3_1 = addressData.Add_3_1;
        this.editFrom.LNAdd_3_2 = addressData.Add_3_2;
        this.editFrom.LNAdd_4 = addressData.Add_4;
        this.editFrom.LNAdd_5 = addressData.Add_5;
        this.editFrom.LNAdd_6 = addressData.Add_6;
        this.editFrom.LNAdd_7 = addressData.Add_7;
        this.editFrom.LNAdd_8 = addressData.Add_8;
        this.editFrom.LNAdd_9 = addressData.Add_9;
      }
      if (title === '通訊地址') {
        this.editFrom.LNAddC_1 = addressData.Add_1;
        this.editFrom.LNAddC_1_1 = addressData.Add_1_1;
        this.editFrom.LNAddC_1_2 = addressData.Add_1_2;
        this.editFrom.LNAddC_2 = addressData.Add_2;
        this.editFrom.LNAddC_2_1 = addressData.Add_2_1;
        this.editFrom.LNAddC_2_2 = addressData.Add_2_2;
        this.editFrom.LNAddC_2_3 = addressData.Add_2_3;
        this.editFrom.LNAddC_2_4 = addressData.Add_2_4;
        this.editFrom.LNAddC_3 = addressData.Add_3;
        this.editFrom.LNAddC_3_1 = addressData.Add_3_1;
        this.editFrom.LNAddC_3_2 = addressData.Add_3_2;
        this.editFrom.LNAddC_4 = addressData.Add_4;
        this.editFrom.LNAddC_5 = addressData.Add_5;
        this.editFrom.LNAddC_6 = addressData.Add_6;
        this.editFrom.LNAddC_7 = addressData.Add_7;
        this.editFrom.LNAddC_8 = addressData.Add_8;
        this.editFrom.LNAddC_9 = addressData.Add_9;
      }
      if (title === '代A戶籍地址') {
        this.editFrom.LNAGAdd_1_A = addressData.Add_1;
        this.editFrom.LNAGAdd_1_1_A = addressData.Add_1_1;
        this.editFrom.LNAGAdd_1_2_A = addressData.Add_1_2;
        this.editFrom.LNAGAdd_2_A = addressData.Add_2;
        this.editFrom.LNAGAdd_2_1_A = addressData.Add_2_1;
        this.editFrom.LNAGAdd_2_2_A = addressData.Add_2_2;
        this.editFrom.LNAGAdd_2_3_A = addressData.Add_2_3;
        this.editFrom.LNAGAdd_2_4_A = addressData.Add_2_4;
        this.editFrom.LNAGAdd_3_A = addressData.Add_3;
        this.editFrom.LNAGAdd_3_1_A = addressData.Add_3_1;
        this.editFrom.LNAGAdd_3_2_A = addressData.Add_3_2;
        this.editFrom.LNAGAdd_4_A = addressData.Add_4;
        this.editFrom.LNAGAdd_5_A = addressData.Add_5;
        this.editFrom.LNAGAdd_6_A = addressData.Add_6;
        this.editFrom.LNAGAdd_7_A = addressData.Add_7;
        this.editFrom.LNAGAdd_8_A = addressData.Add_8;
        this.editFrom.LNAGAdd_9_A = addressData.Add_9;
      }
      if (title === '代A通訊地址') {
        this.editFrom.LNAGAddC_1_A = addressData.Add_1;
        this.editFrom.LNAGAddC_1_1_A = addressData.Add_1_1;
        this.editFrom.LNAGAddC_1_2_A = addressData.Add_1_2;
        this.editFrom.LNAGAddC_2_A = addressData.Add_2;
        this.editFrom.LNAGAddC_2_1_A = addressData.Add_2_1;
        this.editFrom.LNAGAddC_2_2_A = addressData.Add_2_2;
        this.editFrom.LNAGAddC_2_3_A = addressData.Add_2_3;
        this.editFrom.LNAGAddC_2_4_A = addressData.Add_2_4;
        this.editFrom.LNAGAddC_3_A = addressData.Add_3;
        this.editFrom.LNAGAddC_3_1_A = addressData.Add_3_1;
        this.editFrom.LNAGAddC_3_2_A = addressData.Add_3_2;
        this.editFrom.LNAGAddC_4_A = addressData.Add_4;
        this.editFrom.LNAGAddC_5_A = addressData.Add_5;
        this.editFrom.LNAGAddC_6_A = addressData.Add_6;
        this.editFrom.LNAGAddC_7_A = addressData.Add_7;
        this.editFrom.LNAGAddC_8_A = addressData.Add_8;
        this.editFrom.LNAGAddC_9_A = addressData.Add_9;
      }
      if (title === '代B戶籍地址') {
        this.editFrom.LNAGAdd_1_B = addressData.Add_1;
        this.editFrom.LNAGAdd_1_1_B = addressData.Add_1_1;
        this.editFrom.LNAGAdd_1_2_B = addressData.Add_1_2;
        this.editFrom.LNAGAdd_2_B = addressData.Add_2;
        this.editFrom.LNAGAdd_2_1_B = addressData.Add_2_1;
        this.editFrom.LNAGAdd_2_2_B = addressData.Add_2_2;
        this.editFrom.LNAGAdd_2_3_B = addressData.Add_2_3;
        this.editFrom.LNAGAdd_2_4_B = addressData.Add_2_4;
        this.editFrom.LNAGAdd_3_B = addressData.Add_3;
        this.editFrom.LNAGAdd_3_1_B = addressData.Add_3_1;
        this.editFrom.LNAGAdd_3_2_B = addressData.Add_3_2;
        this.editFrom.LNAGAdd_4_B = addressData.Add_4;
        this.editFrom.LNAGAdd_5_B = addressData.Add_5;
        this.editFrom.LNAGAdd_6_B = addressData.Add_6;
        this.editFrom.LNAGAdd_7_B = addressData.Add_7;
        this.editFrom.LNAGAdd_8_B = addressData.Add_8;
        this.editFrom.LNAGAdd_9_B = addressData.Add_9;
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
