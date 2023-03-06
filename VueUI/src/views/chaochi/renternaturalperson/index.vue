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
          <el-form-item
            label="姓名："
            label-width="80px"
          >
            <el-input
              v-model="searchform.RNName"
              clearable
            />
          </el-form-item>
          <el-form-item
            label="身份證字號/居留證號："
            label-width="160px"
          >
            <el-input
              v-model="searchform.RNID"
              clearable
            />
          </el-form-item>
          <el-form-item
            label="生日："
            label-width="80px"
          >
            <!-- <el-date-picker
              v-model="searchform.RNBirthday"
              type="date"
              align="right"
              value-format="yyyy-MM-dd"
            /> -->
            <DatePickerE
              v-model="searchform.RNBirthday"
              value-format="yyyy-MM-dd"
              format="yyyy-MM-dd"
              type="date"
              placeholder="選擇日期"
            />
          </el-form-item>
          <el-form-item
            label="手機："
            label-width="80px"
          >
            <el-input
              v-model="searchform.RNCell"
              placeholder="請輸入手機號碼"
              clearable
              autocomplete="off"
            />
          </el-form-item>
          <el-form-item
            label="電話："
            label-width="80px"
          >
            <el-input
              v-model="searchform.RNTel_2"
              placeholder="請輸入電話"
              clearable
              style="width: 220px"
              class="input-with-select"
              :maxlength="8"
              onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
            >
              <el-select
                slot="prepend"
                v-model="searchform.RNTel_1"
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
              v-model="searchform.RNTel_1"
              style="width: 15%"
              clearable
              maxlength="2"
            />
            <el-form-item label="－">
              <el-input
                v-model="searchform.RNTel_2"
                clearable
              />
            </el-form-item> -->
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
          <el-form-item
            label="身分資格："
            label-width="160px"
          >
            <el-select
              v-model="searchform.qualifyValue"
              placeholder="請選擇"
              clearable
            >
              <el-option
                label="無"
                value="nothing"
              >無</el-option>
              <el-option
                v-for="item in qualifyOptions"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              />
            </el-select>
          </el-form-item>
          <el-form-item
            label="EMail："
            label-width="80px"
          >
            <el-input
              v-model="searchform.RNMail"
              placeholder="請輸入EMail"
              clearable
              autocomplete="off"
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
          <el-button
            v-hasPermi="['CustomerRN/Add']"
            type="primary"
            icon="el-icon-plus"
            size="small"
            @click="ShowEditOrViewDialog()"
          >新增</el-button>
          <el-button
            v-hasPermi="['CustomerRN/Edit']"
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
            v-hasPermi="['CustomerRN/Delete']"
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
          prop="RNName"
          label="姓名"
          sortable="custom"
          width="120"
          fixed
        />
        <el-table-column
          prop="RNGender1"
          label="性別"
          sortable="custom"
          width="80"
          align="center"
        >
          <template slot-scope="scope">
            {{
              scope.row.RNGender1 === '/Yes'
                ? '男'
                : scope.row.RNGender2 === '/Yes'
                  ? '女'
                  : '未知'
            }}
          </template>
        </el-table-column>
        <el-table-column
          prop="RNID"
          label="身分證字號/居留證號"
          sortable="custom"
          width="200"
        />
        <el-table-column
          prop="RNBirthday"
          label="生日"
          sortable="custom"
          width="120"
          align="center"
        />
        <el-table-column
          prop="RNCell"
          label="手機"
          sortable="custom"
          width="120"
          align="center"
        />
        <el-table-column
          prop="RNTel"
          label="電話"
          sortable="custom"
          width="120"
          align="center"
        />
        <el-table-column
          prop="qualify"
          label="身分資格"
          sortable="custom"
          align="center"
          width="160"
        >
          <template slot-scope="scope">
            {{
              scope.row.RNQualify1C === '/Yes'
                ? '一般戶'
                : scope.row.RNQualify2C === '/Yes'
                  ? '第一類弱勢戶'
                  : scope.row.RNQualify3C === '/Yes'
                    ? '第二類弱勢戶'
                    : ''
            }}
          </template>
        </el-table-column>
        <!-- <el-table-column
          prop="qualify"
          label="身分類別"
          sortable="custom"
        >
          <template slot-scope="scope">
            {{ scope.row.RNQualify1C=== '/Yes' ? '一般戶' : scope.row.RNQualify2C==='/Yes'?'第一類弱勢戶': scope.row.RNQualify3C=== '/Yes' ? '第二類弱勢戶':'' }}
          </template>
        </el-table-column> -->
        <el-table-column
          prop="CreatorUserName"
          label="歸屬業務"
          sortable="custom"
        />
        <!-- <el-table-column
          prop="RNMail"
          label="EMAIL"
          sortable="custom"
          width="240"
          align="center"
        /> -->
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
          label="承租自然人基本資料"
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
              :rules="rules"
              class="demo-form-inline"
            >
              <el-row>
                <el-col :span="6">
                  <el-form-item
                    label="姓名："
                    :label-width="formLabelWidth"
                    prop="RNName"
                    style="width: 100%"
                  >
                    <el-input
                      v-model="editFrom.RNName"
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
                    prop="RNID"
                    style="width: 100%"
                  >
                    <el-input
                      v-model="editFrom.RNID"
                      v-upperCase
                      placeholder="請輸入身分證字號"
                      autocomplete="off"
                      clearable
                      :disabled="RNdisabled"
                      maxlength="10"
                    />
                  </el-form-item>
                </el-col>
                <el-col :span="6">
                  <el-form-item
                    label="生日："
                    :label-width="formLabelWidth"
                    prop="RNBirthday"
                    style="width: 100%"
                  >
                    <DatePickerE
                      v-model="editFrom.RNBirthday"
                      value-format="yyyy-MM-dd"
                      format="yyyy-MM-dd"
                      type="date"
                      placeholder="選擇日期"
                      transfer="true"
                      style="width: 160px"
                    />
                    <!-- <el-input
                      v-model="editFrom.RNBirthday"
                      placeholder="選擇日期"
                    /> -->
                  </el-form-item>
                </el-col>
                <el-col :span="6">
                  <el-form-item
                    label="性別："
                    :label-width="formLabelWidth"
                    prop="Gender"
                    style="width: 100%"
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
                      v-model="editFrom.RNTel_2"
                      placeholder="請輸入電話"
                      clearable
                      style="width: 220px"
                      :maxlength="8"
                      class="input-with-select"
                      onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                    >
                      <el-select
                        slot="prepend"
                        v-model="editFrom.RNTel_1"
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
                    label="電話(日)："
                    :label-width="formLabelWidth"
                    prop="RNTel"
                    style="width: 100%"
                  >
                    <el-input
                      v-model="editFrom.RNTel_1"
                      placeholder="區號"
                      autocomplete="off"
                      style="width: 70px"
                      clearable
                    />
                    <el-input
                      v-model="editFrom.RNTel_2"
                      placeholder="號碼"
                      autocomplete="off"
                      style="width: 130px"
                      clearable
                    />
                  </el-form-item> -->
                </el-col>
                <el-col :span="6">
                  <el-form-item
                    label="電話(夜)："
                    :label-width="formLabelWidth"
                  >
                    <el-input
                      v-model="editFrom.RNTelN_2"
                      placeholder="請輸入電話"
                      clearable
                      style="width: 220px"
                      class="input-with-select"
                      :maxlength="8"
                      onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                    >
                      <el-select
                        slot="prepend"
                        v-model="editFrom.RNTelN_1"
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
                    label="電話(夜)："
                    :label-width="formLabelWidth"
                    prop="RNTel"
                    style="width: 100%"
                  >
                    <el-input
                      v-model="editFrom.RNTelN_1"
                      placeholder="區號"
                      autocomplete="off"
                      style="width: 70px"
                      clearable
                    />
                    <el-input
                      v-model="editFrom.RNTelN_2"
                      placeholder="號碼"
                      autocomplete="off"
                      style="width: 140px"
                      clearable
                    />
                  </el-form-item> -->
                </el-col>
                <el-col :span="6">
                  <el-form-item
                    label="手機："
                    :label-width="formLabelWidth"
                    prop="RNCell"
                    style="width: 100%"
                  >
                    <el-input
                      v-model="editFrom.RNCell"
                      placeholder="請輸入手機號碼"
                      autocomplete="off"
                      clearable
                      show-word-limit
                    />
                  </el-form-item>
                </el-col>
                <el-col :span="6">
                  <el-form-item
                    label="電子信箱："
                    :label-width="formLabelWidth"
                    prop="RNMail"
                    style="width: 100%"
                  >
                    <el-input
                      v-model="editFrom.RNMail"
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
                    label="戶口名簿戶號："
                    :label-width="formLabelWidth"
                    prop="RNFAccount"
                    style="width: 100%"
                  >
                    <el-input
                      v-model="editFrom.RNFAccount"
                      placeholder="輸入戶口名簿戶號"
                      autocomplete="off"
                      clearable
                      maxlength="8"
                    />
                  </el-form-item>
                </el-col>
                <el-col :span="6">
                  <el-form-item
                    label="身份資格："
                    :label-width="formLabelWidth"
                    style="width: 100%"
                  >
                    <el-select
                      v-model="qualifyValue"
                      placeholder="請選擇"
                      clearable
                    >
                      <el-option
                        v-for="item in qualifyOptions"
                        :key="item.value"
                        :label="item.label"
                        :value="item.value"
                      />
                    </el-select>
                  </el-form-item>
                </el-col>
                <el-col :span="6">
                  <el-form-item
                    label="租金補助："
                    :label-width="formLabelWidth"
                    style="width: 100%"
                  >
                    <el-input
                      v-model="editFrom.RNRentSUBAFee"
                      placeholder="輸入租金補助"
                      autocomplete="off"
                      clearable
                    />
                  </el-form-item>
                </el-col>
                <el-col :span="6">
                  <el-form-item
                    label="LINE ID："
                    :label-width="formLabelWidth"
                    style="width: 100%"
                  >
                    <el-input
                      v-model="editFrom.RNLine"
                      placeholder="輸入LINEID"
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
                      :resetall="LNAddSameCom"
                      title="通訊地址"
                      @geteditaddress="geteditaddress"
                    />
                  </el-form-item>
                </el-col>
              </el-row>
              <div style="padding-top: 25px">
                <el-divider content-position="left">
                  <h2>緊急聯絡人</h2>
                </el-divider>
              </div>
              <el-row>
                <el-col :span="6">
                  <el-form-item
                    label="姓名："
                    :label-width="formLabelWidth"
                    prop="RNECName"
                    style="width: 100%"
                  >
                    <el-input
                      v-model="editFrom.RNECName"
                      placeholder="請輸入緊急聯絡人姓名"
                      autocomplete="off"
                      clearable
                    />
                  </el-form-item>
                </el-col>
                <el-col :span="6">
                  <el-form-item
                    label="關係："
                    :label-width="formLabelWidth"
                    prop="RNECRelation"
                    style="width: 100%"
                  >
                    <el-input
                      v-model="editFrom.RNECRelation"
                      placeholder="請輸入與緊急聯絡人關係："
                      autocomplete="off"
                      clearable
                    />
                  </el-form-item>
                </el-col>
                <el-col :span="6">
                  <el-form-item
                    label="電話(日)："
                    :label-width="formLabelWidth"
                    prop="RNECTel_2"
                  >
                    <el-input
                      v-model="editFrom.RNECTel_2"
                      placeholder="請輸入電話"
                      clearable
                      style="width: 220px"
                      :maxlength="8"
                      class="input-with-select"
                      onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                    >
                      <el-select
                        slot="prepend"
                        v-model="editFrom.RNECTel_1"
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
                    prop="RNECTel_1"
                    style="width: 100%"
                  >
                    <el-input
                      v-model="editFrom.RNECTel_1"
                      placeholder="區號"
                      autocomplete="off"
                      style="width: 70px"
                      clearable
                    />
                    <el-input
                      v-model="editFrom.RNECTel_2"
                      placeholder="號碼"
                      autocomplete="off"
                      style="width: 140px"
                      clearable
                    />
                  </el-form-item> -->
                </el-col>
                <el-col :span="6">
                  <el-form-item
                    label="手機："
                    :label-width="formLabelWidth"
                    prop="RNECCell"
                    style="width: 100%"
                  >
                    <el-input
                      v-model="editFrom.RNECCell"
                      placeholder="請輸入緊急聯絡人手機"
                      autocomplete="off"
                      clearable
                      blur=""
                    />
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row>
                <el-col :span="6">
                  <el-form-item
                    label="身份證字號/居留證號："
                    :label-width="formLabelWidth"
                    prop="RNECID"
                    style="width: 100%"
                  >
                    <el-input
                      v-model="editFrom.RNECID"
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
                    label="生日："
                    :label-width="formLabelWidth"
                    prop="RNECBirthday"
                  >
                    <DatePickerE
                      v-model="editFrom.RNECBirthday"
                      value-format="yyyy-MM-dd"
                      format="yyyy-MM-dd"
                      type="date"
                      placeholder="選擇日期"
                      style="width: 160px"
                    />
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
                      :sendedform="sendedformEC"
                      title="緊急聯絡人地址"
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
                      v-model="RNECAddSameCom"
                    >通訊地址同戶籍地址</el-checkbox>
                  </el-form-item>
                  <el-form-item
                    v-show="RNECAddSameCom === false"
                    label="通訊地址："
                  >
                    <Address
                      :sendedform="sendedformRNECC"
                      :resetall="RNECAddSameCom"
                      title="緊急聯絡人通訊地址"
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
                    prop="RNAGName_A"
                    style="width: 100%"
                  >
                    <el-input
                      v-model="editFrom.RNAGName_A"
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
                    prop="RNAGID_A"
                    style="width: 100%"
                  >
                    <el-input
                      v-model="editFrom.RNAGID_A"
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
                    prop="RNAGCell_A"
                    style="width: 100%"
                  >
                    <el-input
                      v-model="editFrom.RNAGCell_A"
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
                      v-model="editFrom.RNAGTel_2_A"
                      placeholder="請輸入電話"
                      clearable
                      style="width: 220px"
                      :maxlength="8"
                      class="input-with-select"
                      onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                    >
                      <el-select
                        slot="prepend"
                        v-model="editFrom.RNAGTel_1_A"
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
                    prop="RNAGTel_1_A"
                    style="width: 100%"
                  >
                    <el-input
                      v-model="editFrom.RNAGTel_1_A"
                      placeholder="區號"
                      autocomplete="off"
                      style="width: 70px"
                      clearable
                    />
                    <el-input
                      v-model="editFrom.RNAGTel_2_A"
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
                    label="戶籍地址："
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
              <el-row>
                <el-col :span="24">
                  <el-form-item
                    label=" "
                    label-width="80px"
                  >
                    <el-checkbox
                      v-model="RNAGAddSameCom"
                    >通訊地址同戶籍地址</el-checkbox>
                  </el-form-item>
                  <el-form-item
                    v-show="RNAGAddSameCom === false"
                    label="通訊地址："
                  >
                    <Address
                      :sendedform="sendedformAGAC"
                      :resetall="RNAGAddSameCom"
                      title="代A通訊"
                      @geteditaddress="geteditaddress"
                    />
                  </el-form-item>
                </el-col>
              </el-row>
              <div style="padding-top: 25px">
                <el-divider content-position="left">
                  <h2>保證人</h2>
                </el-divider>
              </div>
              <el-row>
                <el-col :span="6">
                  <el-form-item
                    label="姓名："
                    :label-width="formLabelWidth"
                    prop="RNAGName_A"
                    style="width: 100%"
                  >
                    <el-input
                      v-model="editFrom.RNGName"
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
                    prop="RNGID"
                    style="width: 100%"
                  >
                    <el-input
                      v-model="editFrom.RNGID"
                      v-upperCase
                      placeholder="請輸入身分證字號/居留證號"
                      autocomplete="off"
                      maxlength="10"
                      clearable
                    />
                  </el-form-item>
                </el-col>
                <el-col :span="6">
                  <el-form-item
                    label="手機："
                    :label-width="formLabelWidth"
                    prop="RNGCell"
                    style="width: 100%"
                  >
                    <el-input
                      v-model="editFrom.RNGCell"
                      placeholder="請輸入手機"
                      autocomplete="off"
                      clearable
                    />
                  </el-form-item>
                </el-col>
                <el-col :span="6">
                  <el-form-item
                    label="電子信箱："
                    :label-width="formLabelWidth"
                    prop="RNGMail"
                    style="width: 100%"
                  >
                    <el-input
                      v-model="editFrom.RNGMail"
                      placeholder="請輸入電子信箱"
                      autocomplete="off"
                      clearable
                    />
                  </el-form-item>
                </el-col>
                <!-- <el-col :span="6">
                  <el-form-item
                    label="電話："
                    :label-width="formLabelWidth"
                    prop="RNAGTel_1_A"
                  >
                    <el-input
                      v-model="editFrom.RNAGTel_1_A"
                      placeholder="區號"
                      autocomplete="off"
                      style="width: 80px"
                      clearable
                    />
                    <el-input
                      v-model="editFrom.RNAGTel_2_A"
                      placeholder="號碼"
                      autocomplete="off"
                      style="width: 140px"
                      clearable
                    />
                  </el-form-item>
                </el-col> -->
              </el-row>
              <el-row>
                <el-col :span="6">
                  <el-form-item
                    label="生日："
                    :label-width="formLabelWidth"
                  >
                    <DatePickerE
                      v-model="editFrom.RNGBirthday"
                      value-format="yyyy-MM-dd"
                      format="yyyy-MM-dd"
                      type="date"
                      placeholder="選擇日期"
                      style="width: 160px"
                    />
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
                      :sendedform="sendedformRNG"
                      title="保證人戶籍地址"
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
                      v-model="RNGAddSame"
                    >通訊地址同戶籍地址</el-checkbox>
                  </el-form-item>
                  <el-form-item
                    v-if="RNGAddSame === false"
                    label="通訊地址："
                  >
                    <Address
                      :sendedform="sendedformRNGC"
                      :resetall="RNGAddSame"
                      title="保證人通訊地址"
                      @geteditaddress="geteditaddress"
                    />
                  </el-form-item>
                </el-col>
              </el-row>
            </el-form>
            <div class="rightbtn">
              <el-button
                v-hasPermi="['CustomerRN/Edit']"
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
          label="身份類別" name="E"
        >
          <el-card>
            <div>
              <el-checkbox
                v-model="editFrom.RNQualify3C_12"
                label="未滿18歲之未成年人"
                border
                true-label="/Yes"
                false-label="/Off"
                size="medium"
              />
            </div>
            <div class="qulifydiv">
              <el-checkbox
                v-model="editFrom.RNQualify2C_1"
                label="65歲以上之年長者"
                border
                true-label="/Yes"
                false-label="/Off"
                size="medium"
              />
            </div>
            <div class="qulifydiv">
              <el-checkbox
                v-model="editFrom.RNQualify2C_2"
                label="原住民"
                border
                true-label="/Yes"
                false-label="/Off"
                size="medium"
              />
            </div>
            <div class="qulifydiv">
              <el-checkbox
                v-model="editFrom.RNQualify2C_3"
                label="育有未成年子女3人以上"
                border
                true-label="/Yes"
                false-label="/Off"
                size="medium"
              />
            </div>
            <div class="qulifydiv">
              <el-checkbox
                v-model="editFrom.RNQualify2C_4"
                label="特殊境遇家庭"
                border
                true-label="/Yes"
                false-label="/Off"
                size="medium"
              />
            </div>
            <div class="qulifydiv">
              <el-checkbox
                v-model="editFrom.RNQualify2C_5"
                label="於安置教養機構或寄養家庭結束安置無法返家，未滿二十五"
                border
                true-label="/Yes"
                false-label="/Off"
                size="medium"
              />
            </div>
            <div class="qulifydiv">
              <el-checkbox
                v-model="editFrom.RNQualify2C_6"
                label="家暴/性侵害者及其子女"
                border
                true-label="/Yes"
                false-label="/Off"
                size="medium"
              />
            </div>
            <div class="qulifydiv">
              <el-checkbox
                v-model="editFrom.RNQualify2C_7"
                label="身心障礙者"
                border
                true-label="/Yes"
                false-label="/Off"
                size="medium"
              />
            </div>
            <div class="qulifydiv">
              <el-checkbox
                v-model="editFrom.RNQualify2C_8"
                label="感染人類免疫缺乏病毒者或罹患後天免疫缺乏症候群者"
                border
                true-label="/Yes"
                false-label="/Off"
                size="medium"
              />
            </div>
            <div class="qulifydiv">
              <el-checkbox
                v-model="editFrom.RNQualify2C_9"
                label="災民"
                border
                true-label="/Yes"
                false-label="/Off"
                size="medium"
              />
            </div>
            <div class="qulifydiv">
              <el-checkbox
                v-model="editFrom.RNQualify2C_10"
                label="遊民"
                border
                true-label="/Yes"
                false-label="/Off"
                size="medium"
              />
            </div>
            <div class="qulifydiv">
              <el-checkbox
                v-model="editFrom.RNQualify3C_1"
                label="低收入戶"
                border
                true-label="/Yes"
                false-label="/Off"
                size="medium"
              />
            </div>
            <div class="qulifydiv">
              <el-checkbox
                v-model="editFrom.RNQualify3C_2"
                label="中低收入戶"
                border
                true-label="/Yes"
                false-label="/Off"
                size="medium"
              />
            </div>
            <div class="rightbtn">
              <el-button
                v-hasPermi="['CustomerRN/Edit']"
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
          label="申請人及家庭成員資料"
          name="B"
        >
          <RNF
            :editform="editFrom.RNFOutputDto"
            @cancel="closeDialogAndReset()"
          />
        </el-tab-pane>
        <el-tab-pane
          v-if="editFormTitle === '編輯'"
          label="承租自然人匯款資料"
          name="C"
        >
          <RNRemittance
            :id="currentId"
            :idno="editFrom.RNID"
            :name="editFrom.RNName"
            @cancel="closeDialogAndReset()"
          />
        </el-tab-pane>
        <el-tab-pane
          v-if="editFormTitle === '編輯'"
          label="承租自然人附件"
          name="D"
        >
          <el-card class="box-card">
            <RNFormHistory
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
  </div>
</template>

<script>
import {
  getCustomerRNListWithPager,
  getCustomerRNDetail,
  saveCustomerRN,
  deleteCustomerRN
} from '@/api/chaochi/renter/customerrnservice';
import { getSales } from '@/api/security/userservice';
import * as fecha from 'element-ui/lib/utils/date';
import Address from '@/components/Address/Address.vue';
import DatePickerE from '@/components/RocDatepickerE';
import RNFormHistory from './RNFormHistory.vue';
import RNRemittance from './RNRemittance.vue';
import RNF from './RNF.vue';
import { mergerAddress } from '@/utils/index';
import {
  validateCell,
  validateCellReg,
  validateEamilReg,
  validateIDReg
} from '@/utils/validate';

export default {
  name: 'CustomerRN',
  components: { Address, RNFormHistory, RNF, DatePickerE, RNRemittance },
  data() {
    return {
      sendedform: {},
      sendedform1: {},
      sendedformEC: {},
      sendedformRNECC: {},
      sendedformAGA: {},
      sendedformAGAC: {},
      sendedformRNG: {},
      sendedformRNGC: {},
      qualifyOptions: [
        {
          value: 'RNQualify1C',
          label: '一般戶'
        },
        {
          value: 'RNQualify2C',
          label: '第一類弱勢戶'
        },
        {
          value: 'RNQualify3C',
          label: '第二類弱勢戶'
        }
      ],
      qualifyValue: '',
      activeName: 'A',
      searchform: {
        qualifyValue: '',
        RNName: '',
        RNID: '',
        RNBirthday: '',
        RNCell: '',
        RNTel_1: '',
        RNTel_2: '',
        RNAdd: '',
        RoleId: '',
        Keywords: '',
        CreateTime: ''
      },
      selectRole: [],
      selectedOrganizeOptions: '',
      selectOrganize: [],
      tableData: [],
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
      editFormTitle: '',
      editFrom: {
        RNGender1: '',
        RNGender2: ''
      },
      rules: {
        RNName: [
          {
            required: true,
            trigger: 'blur',
            message: '請輸入姓名'
          }
        ],
        RNID: [
          {
            required: true,
            trigger: 'blur',
            validator: validateIDReg
          }
        ],
        RNBirthday: [
          { required: true, message: '請輸入生日', trigger: 'blur' }
        ],
        RNECID: [
          {
            required: false,
            trigger: 'blur',
            validator: validateIDReg
          }
        ],
        RNAGID_A: [
          {
            required: false,
            trigger: 'blur',
            validator: validateIDReg
          }
        ],
        RNGID: [
          {
            required: false,
            trigger: 'blur',
            validator: validateIDReg
          }
        ],
        RNCell: [
          { required: true, trigger: 'change', validator: validateCell }
        ],
        RNECCell: [
          { required: false, trigger: 'change', validator: validateCellReg }
        ],
        RNAGCell_A: [
          { required: false, trigger: 'change', validator: validateCellReg }
        ],
        RNGCell: [
          { required: false, trigger: 'change', validator: validateCellReg }
        ],
        RNMail: [
          { required: false, trigger: 'change', validator: validateEamilReg }
        ],
        RNGMail: [
          { required: false, trigger: 'change', validator: validateEamilReg }
        ]
        // 20230215對話紀錄
        // RNECName: [
        //   { required: true, trigger: 'change', message: '請輸入姓名' }
        // ],
        // RNECRelation: [
        //   { required: true, trigger: 'change', message: '請輸入關係' }
        // ],
        // RNECBirthday: [
        //   { required: true, trigger: 'change', message: '請輸入生日' }
        // ]
      },
      formLabelWidth: '170px',
      currentId: '', // 當前操作對象的ID值，主要用于修改
      currentSelected: [],
      LNAddSameCom: false,
      RNAGAddSameCom: false,
      RNECAddSameCom: false,
      RNGAddSame: false,
      RNdisabled: false,
      gender: '/Yes'
    };
  },
  watch: {
    gender: {
      handler(a) {
        if (a === '/Yes') {
          this.editFrom.RNGender1 = '/Yes';
          this.editFrom.RNGender2 = '/Off';
        }
        if (a === '/Off') {
          this.editFrom.RNGender1 = '/Off';
          this.editFrom.RNGender2 = '/Yes';
        }
      }
    },
    'editFrom.RNGName': {
      handler(a) {
        if (a) {
          this.rules.RNGID[0].required = true;
        } else {
          this.rules.RNGID[0].required = false;
        }
      }
    },
    'editFrom.RNAddSame': {
      handler(a) {
        if (a === '/Yes') {
          this.LNAddSameCom = true;
        } else {
          this.LNAddSameCom = false;
        }
      }
    },
    'editFrom.RNGAddSame': {
      handler(a) {
        if (a === '/Yes') {
          this.RNGAddSame = true;
        } else {
          this.RNGAddSame = false;
        }
      }
    },
    'editFrom.RNAGAddSame': {
      handler(a) {
        if (a === '/Yes') {
          this.RNAGAddSameCom = true;
        } else {
          this.RNAGAddSameCom = false;
        }
      }
    },
    'editFrom.RNECAddSame': {
      handler(a) {
        if (a === '/Yes') {
          this.RNECAddSameCom = true;
        } else {
          this.RNECAddSameCom = false;
        }
      }
    },
    RNGAddSame: {
      handler(a) {
        if (a === true) {
          this.editFrom.RNGAddSame = '/Yes';
        }
        if (a === false) {
          this.editFrom.RNGAddSame = '/Off';
        }
      }
    },
    LNAddSameCom: {
      handler(a) {
        if (a === true) {
          this.editFrom.RNAddSame = '/Yes';
        }
        if (a === false) {
          this.editFrom.RNAddSame = '/Off';
        }
      }
    },
    RNAGAddSameCom: {
      handler(a) {
        if (a === true) {
          this.editFrom.RNAGAddSame = '/Yes';
        }
        if (a === false) {
          this.editFrom.RNAGAddSame = '/Off';
        }
      }
    },
    RNECAddSameCom: {
      handler(a) {
        if (a === true) {
          this.editFrom.RNECAddSame = '/Yes';
        }
        if (a === false) {
          this.editFrom.RNECAddSame = '/Off';
        }
      }
    },
    'searchform.qualifyValue': {
      handler(a) {
        switch (a) {
          case 'RNQualify1C':
            this.searchform.RNQualify1C = '/Yes';
            this.searchform.RNQualify2C = '/Off';
            this.searchform.RNQualify3C = '/Off';
            break;
          case 'RNQualify2C':
            this.searchform.RNQualify1C = '/Off';
            this.searchform.RNQualify2C = '/Yes';
            this.searchform.RNQualify3C = '/Off';
            break;
          case 'RNQualify3C':
            this.searchform.RNQualify1C = '/Off';
            this.searchform.RNQualify2C = '/Off';
            this.searchform.RNQualify3C = '/Yes';
            break;
          case 'nothing':
            this.searchform.RNQualify1C = '/Off';
            this.searchform.RNQualify2C = '/Off';
            this.searchform.RNQualify3C = '/Off';
            break;
          default:
            this.searchform.RNQualify1C = null;
            this.searchform.RNQualify2C = null;
            this.searchform.RNQualify3C = null;
            break;
        }
      }
    },
    qualifyValue: {
      handler(a) {
        switch (a) {
          case 'RNQualify1C':
            this.editFrom.RNQualify1C = '/Yes';
            this.editFrom.RNQualify2C = '/Off';
            this.editFrom.RNQualify3C = '/Off';
            break;
          case 'RNQualify2C':
            this.editFrom.RNQualify1C = '/Off';
            this.editFrom.RNQualify2C = '/Yes';
            this.editFrom.RNQualify3C = '/Off';
            break;
          case 'RNQualify3C':
            this.editFrom.RNQualify1C = '/Off';
            this.editFrom.RNQualify2C = '/Off';
            this.editFrom.RNQualify3C = '/Yes';
            break;
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
        RNName: this.searchform.RNName,
        RNID: this.searchform.RNID,
        RNBirthday: this.searchform.RNBirthday,
        RNCell: this.searchform.RNCell,
        RNTel_1: this.searchform.RNTel_1,
        RNTel_2: this.searchform.RNTel_2,
        RNAdd: this.searchform.RNAdd,
        RNQualify1C: this.searchform.RNQualify1C,
        RNQualify2C: this.searchform.RNQualify2C,
        RNQualify3C: this.searchform.RNQualify3C,
        RNMail: this.searchform.RNMail,
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
      getCustomerRNListWithPager(seachdata).then((res) => {
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
        RNGender1: '/Yes',
        RNGender2: '/Off'
      };
      this.selectedOrganizeOptions = '';
      this.resetForm('editFrom');
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
          this.RNdisabled = true;
          this.dialogEditFormVisible = true;
          this.loading = true;
          this.bindEditInfo();
        }
      } else {
        this.editFormTitle = '新增';
        this.RNdisabled = false;
        this.LNAddSameCom = false;
        this.currentId = '';
        this.selectedOrganizeOptions = '';
        this.dialogEditFormVisible = true;
        this.sendedform = {};
        this.sendedform1 = {};
        this.sendedformEC = {};
        this.sendedformRNECC = {};
        this.sendedformAGA = {};
        this.sendedformAGAC = {};
        this.sendedformRNG = {};
        this.sendedformRNGC = {};
      }
    },
    bindEditInfo: function() {
      getCustomerRNDetail(this.currentId)
        .then((res) => {
          this.editFrom = res.ResData;
          this.gender = this.editFrom.RNGender1 === '/Yes' ? '/Yes' : '/Off';
          this.qualifyValue =
            this.editFrom.RNQualify1C === '/Yes'
              ? 'RNQualify1C'
              : this.editFrom.RNQualify2C === '/Yes'
                ? 'RNQualify2C'
                : this.editFrom.RNQualify3C === '/Yes'
                  ? 'RNQualify3C'
                  : '';
          this.sendedform = {
            Add_1: this.editFrom.RNAdd_1,
            Add_1_1: this.editFrom.RNAdd_1_1,
            Add_1_2: this.editFrom.RNAdd_1_2,
            Add_2: this.editFrom.RNAdd_2,
            Add_2_1: this.editFrom.RNAdd_2_1,
            Add_2_2: this.editFrom.RNAdd_2_2,
            Add_2_3: this.editFrom.RNAdd_2_3,
            Add_2_4: this.editFrom.RNAdd_2_4,
            Add_3: this.editFrom.RNAdd_3,
            Add_3_1: this.editFrom.RNAdd_3_1,
            Add_3_2: this.editFrom.RNAdd_3_2,
            Add_4: this.editFrom.RNAdd_4,
            Add_5: this.editFrom.RNAdd_5,
            Add_6: this.editFrom.RNAdd_6,
            Add_7: this.editFrom.RNAdd_7,
            Add_8: this.editFrom.RNAdd_8,
            Add_9: this.editFrom.RNAdd_9
          };
          this.sendedform1 = {
            Add_1: this.editFrom.RNAddC_1,
            Add_1_1: this.editFrom.RNAddC_1_1,
            Add_1_2: this.editFrom.RNAddC_1_2,
            Add_2: this.editFrom.RNAddC_2,
            Add_2_1: this.editFrom.RNAddC_2_1,
            Add_2_2: this.editFrom.RNAddC_2_2,
            Add_2_3: this.editFrom.RNAddC_2_3,
            Add_2_4: this.editFrom.RNAddC_2_4,
            Add_3: this.editFrom.RNAddC_3,
            Add_3_1: this.editFrom.RNAddC_3_1,
            Add_3_2: this.editFrom.RNAddC_3_2,
            Add_4: this.editFrom.RNAddC_4,
            Add_5: this.editFrom.RNAddC_5,
            Add_6: this.editFrom.RNAddC_6,
            Add_7: this.editFrom.RNAddC_7,
            Add_8: this.editFrom.RNAddC_8,
            Add_9: this.editFrom.RNAddC_9
          };
          this.sendedformEC = {
            Add_1: this.editFrom.RNECAdd_1,
            Add_1_1: this.editFrom.RNECAdd_1_1,
            Add_1_2: this.editFrom.RNECAdd_1_2,
            Add_2: this.editFrom.RNECAdd_2,
            Add_2_1: this.editFrom.RNECAdd_2_1,
            Add_2_2: this.editFrom.RNECAdd_2_2,
            Add_2_3: this.editFrom.RNECAdd_2_3,
            Add_2_4: this.editFrom.RNECAdd_2_4,
            Add_3: this.editFrom.RNECAdd_3,
            Add_3_1: this.editFrom.RNECAdd_3_1,
            Add_3_2: this.editFrom.RNECAdd_3_2,
            Add_4: this.editFrom.RNECAdd_4,
            Add_5: this.editFrom.RNECAdd_5,
            Add_6: this.editFrom.RNECAdd_6,
            Add_7: this.editFrom.RNECAdd_7,
            Add_8: this.editFrom.RNECAdd_8,
            Add_9: this.editFrom.RNECAdd_9
          };
          this.sendedformRNECC = {
            Add_1: this.editFrom.RNECAddC_1,
            Add_1_1: this.editFrom.RNECAddC_1_1,
            Add_1_2: this.editFrom.RNECAddC_1_2,
            Add_2: this.editFrom.RNECAddC_2,
            Add_2_1: this.editFrom.RNECAddC_2_1,
            Add_2_2: this.editFrom.RNECAddC_2_2,
            Add_2_3: this.editFrom.RNECAddC_2_3,
            Add_2_4: this.editFrom.RNECAddC_2_4,
            Add_3: this.editFrom.RNECAddC_3,
            Add_3_1: this.editFrom.RNECAddC_3_1,
            Add_3_2: this.editFrom.RNECAddC_3_2,
            Add_4: this.editFrom.RNECAddC_4,
            Add_5: this.editFrom.RNECAddC_5,
            Add_6: this.editFrom.RNECAddC_6,
            Add_7: this.editFrom.RNECAddC_7,
            Add_8: this.editFrom.RNECAddC_8,
            Add_9: this.editFrom.RNECAddC_9
          };
          this.sendedformAGA = {
            Add_1: this.editFrom.RNAGAdd_1_A,
            Add_1_1: this.editFrom.RNAGAdd_1_1_A,
            Add_1_2: this.editFrom.RNAGAdd_1_2_A,
            Add_2: this.editFrom.RNAGAdd_2_A,
            Add_2_1: this.editFrom.RNAGAdd_2_1_A,
            Add_2_2: this.editFrom.RNAGAdd_2_2_A,
            Add_2_3: this.editFrom.RNAGAdd_2_3_A,
            Add_2_4: this.editFrom.RNAGAdd_2_4_A,
            Add_3: this.editFrom.RNAGAdd_3_A,
            Add_3_1: this.editFrom.RNAGAdd_3_1_A,
            Add_3_2: this.editFrom.RNAGAdd_3_2_A,
            Add_4: this.editFrom.RNAGAdd_4_A,
            Add_5: this.editFrom.RNAGAdd_5_A,
            Add_6: this.editFrom.RNAGAdd_6_A,
            Add_7: this.editFrom.RNAGAdd_7_A,
            Add_8: this.editFrom.RNAGAdd_8_A,
            Add_9: this.editFrom.RNAGAdd_9_A
          };
          this.sendedformAGAC = {
            Add_1: this.editFrom.RNAGAddC_1_A,
            Add_1_1: this.editFrom.RNAGAddC_1_1_A,
            Add_1_2: this.editFrom.RNAGAddC_1_2_A,
            Add_2: this.editFrom.RNAGAddC_2_A,
            Add_2_1: this.editFrom.RNAGAddC_2_1_A,
            Add_2_2: this.editFrom.RNAGAddC_2_2_A,
            Add_2_3: this.editFrom.RNAGAddC_2_3_A,
            Add_2_4: this.editFrom.RNAGAddC_2_4_A,
            Add_3: this.editFrom.RNAGAddC_3_A,
            Add_3_1: this.editFrom.RNAGAddC_3_1_A,
            Add_3_2: this.editFrom.RNAGAddC_3_2_A,
            Add_4: this.editFrom.RNAGAddC_4_A,
            Add_5: this.editFrom.RNAGAddC_5_A,
            Add_6: this.editFrom.RNAGAddC_6_A,
            Add_7: this.editFrom.RNAGAddC_7_A,
            Add_8: this.editFrom.RNAGAddC_8_A,
            Add_9: this.editFrom.RNAGAddC_9_A
          };
          this.sendedformRNG = {
            Add_1: this.editFrom.RNGAdd_1,
            Add_1_1: this.editFrom.RNGAdd_1_1,
            Add_1_2: this.editFrom.RNGAdd_1_2,
            Add_2: this.editFrom.RNGAdd_2,
            Add_2_1: this.editFrom.RNGAdd_2_1,
            Add_2_2: this.editFrom.RNGAdd_2_2,
            Add_2_3: this.editFrom.RNGAdd_2_3,
            Add_2_4: this.editFrom.RNGAdd_2_4,
            Add_3: this.editFrom.RNGAdd_3,
            Add_3_1: this.editFrom.RNGAdd_3_1,
            Add_3_2: this.editFrom.RNGAdd_3_2,
            Add_4: this.editFrom.RNGAdd_4,
            Add_5: this.editFrom.RNGAdd_5,
            Add_6: this.editFrom.RNGAdd_6,
            Add_7: this.editFrom.RNGAdd_7,
            Add_8: this.editFrom.RNGAdd_8,
            Add_9: this.editFrom.RNGAdd_9
          };
          this.sendedformRNGC = {
            Add_1: this.editFrom.RNGCAdd_1,
            Add_1_1: this.editFrom.RNGCAdd_1_1,
            Add_1_2: this.editFrom.RNGCAdd_1_2,
            Add_2: this.editFrom.RNGCAdd_2,
            Add_2_1: this.editFrom.RNGCAdd_2_1,
            Add_2_2: this.editFrom.RNGCAdd_2_2,
            Add_2_3: this.editFrom.RNGCAdd_2_3,
            Add_2_4: this.editFrom.RNGCAdd_2_4,
            Add_3: this.editFrom.RNGCAdd_3,
            Add_3_1: this.editFrom.RNGCAdd_3_1,
            Add_3_2: this.editFrom.RNGCAdd_3_2,
            Add_4: this.editFrom.RNGCAdd_4,
            Add_5: this.editFrom.RNGCAdd_5,
            Add_6: this.editFrom.RNGCAdd_6,
            Add_7: this.editFrom.RNGCAdd_7,
            Add_8: this.editFrom.RNGCAdd_8,
            Add_9: this.editFrom.RNGCAdd_9
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
      const addarray = [
        this.editFrom.RNAdd_1,
        this.editFrom.RNAdd_1_1,
        this.editFrom.RNAdd_1_2,
        this.editFrom.RNAdd_2,
        this.editFrom.RNAdd_2_1,
        this.editFrom.RNAdd_2_2,
        this.editFrom.RNAdd_2_3,
        this.editFrom.RNAdd_2_4,
        this.editFrom.RNAdd_3,
        this.editFrom.RNAdd_3_1,
        this.editFrom.RNAdd_3_2,
        this.editFrom.RNAdd_4,
        this.editFrom.RNAdd_5,
        this.editFrom.RNAdd_6,
        this.editFrom.RNAdd_7,
        this.editFrom.RNAdd_8,
        this.editFrom.RNAdd_9
      ];
      var badd = mergerAddress(addarray);
      if (badd.length < 3) {
        this.$message.error('請輸入戶籍地址');
        return;
      }
      // 20230215對話紀錄
      // const addarray2 = [
      //   this.editFrom.RNECAdd_1,
      //   this.editFrom.RNECAdd_1_1,
      //   this.editFrom.RNECAdd_1_2,
      //   this.editFrom.RNECAdd_2,
      //   this.editFrom.RNECAdd_2_1,
      //   this.editFrom.RNECAdd_2_2,
      //   this.editFrom.RNECAdd_2_3,
      //   this.editFrom.RNECAdd_2_4,
      //   this.editFrom.RNECAdd_3,
      //   this.editFrom.RNECAdd_3_1,
      //   this.editFrom.RNECAdd_3_2,
      //   this.editFrom.RNECAdd_4,
      //   this.editFrom.RNECAdd_5,
      //   this.editFrom.RNECAdd_6,
      //   this.editFrom.RNECAdd_7,
      //   this.editFrom.RNECAdd_8,
      //   this.editFrom.RNECAdd_9
      // ];
      // var ecadd = mergerAddress(addarray2);
      // if (ecadd.length < 3) {
      //   this.$message.error('請輸入緊急聯絡人戶籍地址');
      //   return;
      // }
      this.$refs['editFrom'].validate((valid) => {
        if (valid) {
          const data = this.editFrom;
          var url = 'CustomerRN/Insert';
          if (this.currentId !== '') {
            url = 'CustomerRN/Update';
          }
          this.loading = true;
          saveCustomerRN(data, url)
            .then((res) => {
              if (res.Success) {
                this.$message({
                  message: '恭喜你，操作成功',
                  type: 'success'
                });
                if (url === 'CustomerRN/Insert') {
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
      this.sendedformAGAC = {};
      this.sendedformRNG = {};
      this.sendedformRNGC = {};
      this.sendedformEC = {};
      this.sendedformRNECC = {};
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
            return deleteCustomerRN(data);
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
    handleClick(a, b) {},
    geteditaddress(addressData, title) {
      if (title === '戶籍地址') {
        this.editFrom.RNAdd_1 = addressData.Add_1;
        this.editFrom.RNAdd_1_1 = addressData.Add_1_1;
        this.editFrom.RNAdd_1_2 = addressData.Add_1_2;
        this.editFrom.RNAdd_2 = addressData.Add_2;
        this.editFrom.RNAdd_2_1 = addressData.Add_2_1;
        this.editFrom.RNAdd_2_2 = addressData.Add_2_2;
        this.editFrom.RNAdd_2_3 = addressData.Add_2_3;
        this.editFrom.RNAdd_2_4 = addressData.Add_2_4;
        this.editFrom.RNAdd_3 = addressData.Add_3;
        this.editFrom.RNAdd_3_1 = addressData.Add_3_1;
        this.editFrom.RNAdd_3_2 = addressData.Add_3_2;
        this.editFrom.RNAdd_4 = addressData.Add_4;
        this.editFrom.RNAdd_5 = addressData.Add_5;
        this.editFrom.RNAdd_6 = addressData.Add_6;
        this.editFrom.RNAdd_7 = addressData.Add_7;
        this.editFrom.RNAdd_8 = addressData.Add_8;
        this.editFrom.RNAdd_9 = addressData.Add_9;
      }
      if (title === '通訊地址') {
        this.editFrom.RNAddC_1 = addressData.Add_1;
        this.editFrom.RNAddC_1_1 = addressData.Add_1_1;
        this.editFrom.RNAddC_1_2 = addressData.Add_1_2;
        this.editFrom.RNAddC_2 = addressData.Add_2;
        this.editFrom.RNAddC_2_1 = addressData.Add_2_1;
        this.editFrom.RNAddC_2_2 = addressData.Add_2_2;
        this.editFrom.RNAddC_2_3 = addressData.Add_2_3;
        this.editFrom.RNAddC_2_4 = addressData.Add_2_4;
        this.editFrom.RNAddC_3 = addressData.Add_3;
        this.editFrom.RNAddC_3_1 = addressData.Add_3_1;
        this.editFrom.RNAddC_3_2 = addressData.Add_3_2;
        this.editFrom.RNAddC_4 = addressData.Add_4;
        this.editFrom.RNAddC_5 = addressData.Add_5;
        this.editFrom.RNAddC_6 = addressData.Add_6;
        this.editFrom.RNAddC_7 = addressData.Add_7;
        this.editFrom.RNAddC_8 = addressData.Add_8;
        this.editFrom.RNAddC_9 = addressData.Add_9;
      }
      if (title === '緊急聯絡人地址') {
        this.editFrom.RNECAdd_1 = addressData.Add_1;
        this.editFrom.RNECAdd_1_1 = addressData.Add_1_1;
        this.editFrom.RNECAdd_1_2 = addressData.Add_1_2;
        this.editFrom.RNECAdd_2 = addressData.Add_2;
        this.editFrom.RNECAdd_2_1 = addressData.Add_2_1;
        this.editFrom.RNECAdd_2_2 = addressData.Add_2_2;
        this.editFrom.RNECAdd_2_3 = addressData.Add_2_3;
        this.editFrom.RNECAdd_2_4 = addressData.Add_2_4;
        this.editFrom.RNECAdd_3 = addressData.Add_3;
        this.editFrom.RNECAdd_3_1 = addressData.Add_3_1;
        this.editFrom.RNECAdd_3_2 = addressData.Add_3_2;
        this.editFrom.RNECAdd_4 = addressData.Add_4;
        this.editFrom.RNECAdd_5 = addressData.Add_5;
        this.editFrom.RNECAdd_6 = addressData.Add_6;
        this.editFrom.RNECAdd_7 = addressData.Add_7;
        this.editFrom.RNECAdd_8 = addressData.Add_8;
        this.editFrom.RNECAdd_9 = addressData.Add_9;
      }
      if (title === '緊急聯絡人通訊地址') {
        this.editFrom.RNECAddC_1 = addressData.Add_1;
        this.editFrom.RNECAddC_1_1 = addressData.Add_1_1;
        this.editFrom.RNECAddC_1_2 = addressData.Add_1_2;
        this.editFrom.RNECAddC_2 = addressData.Add_2;
        this.editFrom.RNECAddC_2_1 = addressData.Add_2_1;
        this.editFrom.RNECAddC_2_2 = addressData.Add_2_2;
        this.editFrom.RNECAddC_2_3 = addressData.Add_2_3;
        this.editFrom.RNECAddC_2_4 = addressData.Add_2_4;
        this.editFrom.RNECAddC_3 = addressData.Add_3;
        this.editFrom.RNECAddC_3_1 = addressData.Add_3_1;
        this.editFrom.RNECAddC_3_2 = addressData.Add_3_2;
        this.editFrom.RNECAddC_4 = addressData.Add_4;
        this.editFrom.RNECAddC_5 = addressData.Add_5;
        this.editFrom.RNECAddC_6 = addressData.Add_6;
        this.editFrom.RNECAddC_7 = addressData.Add_7;
        this.editFrom.RNECAddC_8 = addressData.Add_8;
        this.editFrom.RNECAddC_9 = addressData.Add_9;
      }
      if (title === '代A地址') {
        this.editFrom.RNAGAdd_1_A = addressData.Add_1;
        this.editFrom.RNAGAdd_1_1_A = addressData.Add_1_1;
        this.editFrom.RNAGAdd_1_2_A = addressData.Add_1_2;
        this.editFrom.RNAGAdd_2_A = addressData.Add_2;
        this.editFrom.RNAGAdd_2_1_A = addressData.Add_2_1;
        this.editFrom.RNAGAdd_2_2_A = addressData.Add_2_2;
        this.editFrom.RNAGAdd_2_3_A = addressData.Add_2_3;
        this.editFrom.RNAGAdd_2_4_A = addressData.Add_2_4;
        this.editFrom.RNAGAdd_3_A = addressData.Add_3;
        this.editFrom.RNAGAdd_3_1_A = addressData.Add_3_1;
        this.editFrom.RNAGAdd_3_2_A = addressData.Add_3_2;
        this.editFrom.RNAGAdd_4_A = addressData.Add_4;
        this.editFrom.RNAGAdd_5_A = addressData.Add_5;
        this.editFrom.RNAGAdd_6_A = addressData.Add_6;
        this.editFrom.RNAGAdd_7_A = addressData.Add_7;
        this.editFrom.RNAGAdd_8_A = addressData.Add_8;
        this.editFrom.RNAGAdd_9_A = addressData.Add_9;
      }
      if (title === '代A通訊') {
        this.editFrom.RNAGAddC_1_A = addressData.Add_1;
        this.editFrom.RNAGAddC_1_1_A = addressData.Add_1_1;
        this.editFrom.RNAGAddC_1_2_A = addressData.Add_1_2;
        this.editFrom.RNAGAddC_2_A = addressData.Add_2;
        this.editFrom.RNAGAddC_2_1_A = addressData.Add_2_1;
        this.editFrom.RNAGAddC_2_2_A = addressData.Add_2_2;
        this.editFrom.RNAGAddC_2_3_A = addressData.Add_2_3;
        this.editFrom.RNAGAddC_2_4_A = addressData.Add_2_4;
        this.editFrom.RNAGAddC_3_A = addressData.Add_3;
        this.editFrom.RNAGAddC_3_1_A = addressData.Add_3_1;
        this.editFrom.RNAGAddC_3_2_A = addressData.Add_3_2;
        this.editFrom.RNAGAddC_4_A = addressData.Add_4;
        this.editFrom.RNAGAddC_5_A = addressData.Add_5;
        this.editFrom.RNAGAddC_6_A = addressData.Add_6;
        this.editFrom.RNAGAddC_7_A = addressData.Add_7;
        this.editFrom.RNAGAddC_8_A = addressData.Add_8;
        this.editFrom.RNAGAddC_9_A = addressData.Add_9;
      }
      if (title === '保證人戶籍地址') {
        this.editFrom.RNGAdd_1 = addressData.Add_1;
        this.editFrom.RNGAdd_1_1 = addressData.Add_1_1;
        this.editFrom.RNGAdd_1_2 = addressData.Add_1_2;
        this.editFrom.RNGAdd_2 = addressData.Add_2;
        this.editFrom.RNGAdd_2_1 = addressData.Add_2_1;
        this.editFrom.RNGAdd_2_2 = addressData.Add_2_2;
        this.editFrom.RNGAdd_2_3 = addressData.Add_2_3;
        this.editFrom.RNGAdd_2_4 = addressData.Add_2_4;
        this.editFrom.RNGAdd_3 = addressData.Add_3;
        this.editFrom.RNGAdd_3_1 = addressData.Add_3_1;
        this.editFrom.RNGAdd_3_2 = addressData.Add_3_2;
        this.editFrom.RNGAdd_4 = addressData.Add_4;
        this.editFrom.RNGAdd_5 = addressData.Add_5;
        this.editFrom.RNGAdd_6 = addressData.Add_6;
        this.editFrom.RNGAdd_7 = addressData.Add_7;
        this.editFrom.RNGAdd_8 = addressData.Add_8;
        this.editFrom.RNGAdd_9 = addressData.Add_9;
      }
      if (title === '保證人通訊地址') {
        this.editFrom.RNGCAdd_1 = addressData.Add_1;
        this.editFrom.RNGCAdd_1_1 = addressData.Add_1_1;
        this.editFrom.RNGCAdd_1_2 = addressData.Add_1_2;
        this.editFrom.RNGCAdd_2 = addressData.Add_2;
        this.editFrom.RNGCAdd_2_1 = addressData.Add_2_1;
        this.editFrom.RNGCAdd_2_2 = addressData.Add_2_2;
        this.editFrom.RNGCAdd_2_3 = addressData.Add_2_3;
        this.editFrom.RNGCAdd_2_4 = addressData.Add_2_4;
        this.editFrom.RNGCAdd_3 = addressData.Add_3;
        this.editFrom.RNGCAdd_3_1 = addressData.Add_3_1;
        this.editFrom.RNGCAdd_3_2 = addressData.Add_3_2;
        this.editFrom.RNGCAdd_4 = addressData.Add_4;
        this.editFrom.RNGCAdd_5 = addressData.Add_5;
        this.editFrom.RNGCAdd_6 = addressData.Add_6;
        this.editFrom.RNGCAdd_7 = addressData.Add_7;
        this.editFrom.RNGCAdd_8 = addressData.Add_8;
        this.editFrom.RNGCAdd_9 = addressData.Add_9;
      }
    }
  }
};
</script>
<style>
.qulifydiv {
  margin-top: 20px;
}
</style>
