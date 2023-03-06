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
              v-model="searchform.RCName"
              clearable
            />
          </el-form-item>
          <el-form-item label="代表人：">
            <el-input
              v-model="searchform.RCRep"
              clearable
            />
          </el-form-item>
          <el-form-item label="統一編號：">
            <el-input
              v-model="searchform.RCID"
              clearable
            />
          </el-form-item>
          <el-form-item label="公司電話：">
            <el-input
              v-model="searchform.RCTel_2"
              placeholder="請輸入電話"
              clearable
              style="width: 220px"
              class="input-with-select"
              onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
            >
              <el-select
                slot="prepend"
                v-model="searchform.RCTel_1"
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
              v-model="searchform.RCTel"
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
                v-model="searchform.RCAdd"
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
            v-hasPermi="['CustomerRC/Add']"
            type="primary"
            icon="el-icon-plus"
            size="small"
            @click="ShowEditOrViewDialog()"
          >新增</el-button>
          <el-button
            v-hasPermi="['CustomerRC/Edit']"
            type="primary"
            icon="el-icon-edit"
            class="el-button-modify"
            size="small"
            @click="ShowEditOrViewDialog('edit')"
          >修改</el-button>
          <el-button
            v-hasPermi="['CustomerRC/Delete']"
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
        :default-sort="{ prop: 'SortCode', order: 'ascending' }"
        @select="handleSelectChange"
        @select-all="handleSelectAllChange"
        @sort-change="handleSortChange"
        @row-dblclick="rowdblclick"
      >
        <el-table-column
          type="selection"
          width="40"
        />
        <el-table-column
          prop="RCName"
          label="法人名稱"
          sortable="custom"
          width="300"
        />
        <el-table-column
          prop="RCRep"
          label="負責人"
          sortable="custom"
          width="120"
          align="center"
        />
        <el-table-column
          prop="RCID"
          label="統一編號"
          sortable="custom"
          width="120"
          align="center"
        />
        <el-table-column
          prop="RCTel"
          label="公司電話"
          sortable="custom"
          width="120"
          align="center"
        />
        <el-table-column
          prop="RCAdd"
          label="法人公司登記地址"
          sortable="custom"
          align="left"
        />
        <el-table-column
          prop="CreatorUserName"
          label="歸屬業務 "
          sortable="custom"
          width="120"
          align="left"
        />
        <el-table-column
          prop="LastModifyTime"
          label="更新時間"
          sortable="custom"
          width="200"
        />
        <!-- <el-table-column
          prop="CreatorTime"
          label="建立時間"
          sortable="custom"
          width="200"
        /> -->
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
      v-el-drag-dialog
      :title="editFormTitle"
      :visible.sync="dialogEditFormVisible"
      fullscreen
      append-to-body
    >
      <el-tabs
        v-model="activeName"
        v-loading="loading"
        v-loading.lock="loading"
        element-loading-text="載入中..."
        type="border-card"
      >
        <el-tab-pane
          label="承租法人基本資料"
          name="A"
        >
          <el-card>
            <el-form
              ref="editForm"
              :inline="true"
              :model="editForm"
              class="demo-form-inline"
              :rules="rules"
            >
              <el-form-item
                label="法人名稱："
                :label-width="formLabelWidth"
                prop="RCName"
              >
                <el-input
                  v-model="editForm.RCName"
                  placeholder="請輸法入名稱"
                  autocomplete="off"
                  clearable
                />
              </el-form-item>
              <el-form-item
                label="法人名稱(英)："
                :label-width="formLabelWidth"
              >
                <el-input
                  v-model="editForm.RCEngName"
                  placeholder="請輸法入名稱(英)"
                  autocomplete="off"
                  clearable
                />
              </el-form-item>
              <el-form-item
                label="代表人："
                :label-width="formLabelWidth"
                prop="RCRep"
              >
                <el-input
                  v-model="editForm.RCRep"
                  placeholder="代表人"
                  autocomplete="off"
                  clearable
                />
              </el-form-item>
              <el-form-item
                label="統一編號："
                :label-width="formLabelWidth"
                prop="RCID"
              >
                <el-input
                  v-model="editForm.RCID"
                  placeholder="請輸入統一編號"
                  autocomplete="off"
                  maxlength="8"
                  clearable
                />
              </el-form-item>
              <el-row>
                <el-form-item
                  label="公司電話："
                  :label-width="formLabelWidth"
                  prop="RCTel_2"
                >
                  <el-input
                    v-model="editForm.RCTel_2"
                    placeholder="請輸入電話"
                    clearable
                    style="width: 220px"
                    class="input-with-select"
                    :maxlength="8"
                    onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                  >
                    <el-select
                      slot="prepend"
                      v-model="editForm.RCTel_1"
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
                <el-form-item
                  label="聯絡人手機："
                  :label-width="formLabelWidth"
                >
                  <el-input
                    v-model="editForm.RCCell"
                    placeholder="請輸入聯絡人手機"
                    autocomplete="off"
                    maxlength="10"
                    clearable
                  />
                </el-form-item>
              </el-row>
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
                  v-model="RCAddSameCom"
                >登記地址同營業地址</el-checkbox>
              </el-form-item>
              <el-form-item
                v-show="RCAddSameCom === false"
                label="營業地址："
              >
                <Address
                  :sendedform="sendedform1"
                  :resetall="RCAddSameCom"
                  title="公司營業地址"
                  @geteditaddress="geteditaddress"
                />
              </el-form-item>

              <div style="padding-top: 25px">
                <el-divider content-position="left">
                  <h2>緊急聯絡人(親屬)</h2>
                </el-divider>
              </div>
              <el-row>
                <el-form-item
                  label="姓名："
                  :label-width="formLabelWidth"
                  prop="RCECName"
                >
                  <el-input
                    v-model="editForm.RCECName"
                    placeholder="請輸入姓名"
                    autocomplete="off"
                    clearable
                  />
                </el-form-item>
                <el-form-item
                  label="身份證字號："
                  :label-width="formLabelWidth"
                  prop="RCECID"
                >
                  <el-input
                    v-model="editForm.RCECID"
                    v-upperCase
                    placeholder="請輸入身分證字號/居留證號"
                    autocomplete="off"
                    clearable
                    maxlength="10"
                  />
                </el-form-item>
                <el-form-item
                  label="生日："
                  :label-width="formLabelWidth"
                >
                  <DatePickerE
                    v-model="editForm.RCECBirthday"
                    value-format="yyyy-MM-dd"
                    format="yyyy-MM-dd"
                    type="date"
                    placeholder="選擇日期"
                  />
                  <!-- <el-input
                    v-model="editForm.RCECBirthday"
                    placeholder="請輸入生日"
                    autocomplete="off"
                    clearable
                    maxlength="10"
                  /> -->
                </el-form-item>
                <el-form-item
                  label="連絡電話："
                  :label-width="formLabelWidth"
                  prop="RCECCell"
                >
                  <el-input
                    v-model="editForm.RCECCell"
                    placeholder="請輸入連絡電話"
                    autocomplete="off"
                    clearable
                    maxlength="10"
                    onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                  />
                </el-form-item>
                <!-- <el-form-item
                  label="電話："
                  :label-width="formLabelWidth"
                >
                  <el-input
                    v-model="editForm.RCECTel_2"
                    placeholder="請輸入電話"
                    clearable
                    style="width: 220px"
                    class="input-with-select"
                    onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                  >
                    <el-select
                      slot="prepend"
                      v-model="editForm.RCECTel_1"
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
                </el-form-item> -->
              </el-row>
              <el-row>
                <el-form-item
                  label="關係："
                  :label-width="formLabelWidth"
                >
                  <el-input
                    v-model="editForm.RCECRelation"
                    placeholder="請輸入關係"
                    autocomplete="off"
                    clearable
                    maxlength="10"
                  />
                </el-form-item>
              </el-row>
              <el-row>
                <el-form-item
                  label="戶籍地址："
                  :label-width="formLabelWidth"
                >
                  <Address
                    :sendedform="sendedformEC"
                    title="緊急1戶籍地址"
                    @geteditaddress="geteditaddress"
                  />
                </el-form-item>
              </el-row>
              <el-row>
                <el-form-item
                  label=" "
                  label-width="80px"
                >
                  <el-checkbox
                    v-model="RCECAddSameCom"
                  >通訊地址同戶籍地址</el-checkbox>
                </el-form-item>
                <el-form-item
                  v-show="RCECAddSameCom === false"
                  label="通訊地址："
                >
                  <Address
                    :sendedform="sendedformECC"
                    :resetall="RCECAddSameCom"
                    title="緊急1通訊地址"
                    @geteditaddress="geteditaddress"
                  />
                </el-form-item>
              </el-row>

              <div style="padding-top: 25px">
                <el-divider content-position="left">
                  <h2>緊急聯絡人(朋友)</h2>
                </el-divider>
              </div>
              <el-row>
                <el-form-item
                  label="姓名："
                  :label-width="formLabelWidth"
                  prop="RCECFName"
                >
                  <el-input
                    v-model="editForm.RCECFName"
                    placeholder="請輸入姓名"
                    autocomplete="off"
                    clearable
                  />
                </el-form-item>
                <el-form-item
                  label="身份證字號："
                  :label-width="formLabelWidth"
                  prop="RCECFID"
                >
                  <el-input
                    v-model="editForm.RCECFID"
                    v-upperCase
                    placeholder="請輸入身分證字號/居留證號"
                    autocomplete="off"
                    clearable
                    maxlength="10"
                  />
                </el-form-item>
                <el-form-item
                  label="生日："
                  :label-width="formLabelWidth"
                >
                  <DatePickerE
                    v-model="editForm.RCECFBirthday"
                    value-format="yyyy-MM-dd"
                    format="yyyy-MM-dd"
                    type="date"
                    placeholder="選擇日期"
                  />
                  <!-- <el-input
                    v-model="editForm.RCECFBirthday"
                    placeholder="請輸入生日"
                    autocomplete="off"
                    clearable
                    maxlength="10"
                  /> -->
                </el-form-item>
                <el-form-item
                  label="連絡電話："
                  :label-width="formLabelWidth"
                  prop="RCECFCell"
                >
                  <el-input
                    v-model="editForm.RCECFCell"
                    placeholder="請輸入連絡電話"
                    type="number"
                    class="no-number"
                    autocomplete="off"
                    clearable
                    maxlength="10"
                    onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                  />
                </el-form-item>
                <!-- <el-form-item
                  label="電話："
                  :label-width="formLabelWidth"
                >
                  <el-input
                    v-model="editForm.RCECTel_2"
                    placeholder="請輸入電話"
                    clearable
                    style="width: 220px"
                    class="input-with-select"
                    onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                  >
                    <el-select
                      slot="prepend"
                      v-model="editForm.RCECTel_1"
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
                </el-form-item> -->
              </el-row>
              <el-row>
                <el-form-item
                  label="關係："
                  :label-width="formLabelWidth"
                >
                  <el-input
                    v-model="editForm.RCECFRelation"
                    placeholder="請輸入關係"
                    autocomplete="off"
                    clearable
                    maxlength="10"
                  />
                </el-form-item>
              </el-row>
              <el-row>
                <el-form-item
                  label="戶籍地址："
                  :label-width="formLabelWidth"
                >
                  <Address
                    :sendedform="sendedformECF"
                    title="緊急2戶籍地址"
                    @geteditaddress="geteditaddress"
                  />
                </el-form-item>
              </el-row>
              <el-row>
                <el-form-item
                  label=" "
                  label-width="80px"
                >
                  <el-checkbox
                    v-model="RCECFAddSameCom"
                  >通訊地址同戶籍地址</el-checkbox>
                </el-form-item>
                <el-form-item
                  v-show="RCECFAddSameCom === false"
                  label="通訊地址："
                >
                  <Address
                    :sendedform="sendedformECFC"
                    :resetall="RCECFAddSameCom"
                    title="緊急2通訊地址"
                    @geteditaddress="geteditaddress"
                  />
                </el-form-item>
              </el-row>

              <div style="padding-top: 25px">
                <el-divider content-position="left">
                  <h2>簽約代理人</h2>
                </el-divider>
              </div>
              <el-row>
                <el-form-item
                  label="姓名："
                  :label-width="formLabelWidth"
                  prop="RCAGName"
                >
                  <el-input
                    v-model="editForm.RCAGName"
                    placeholder="請輸入姓名"
                    autocomplete="off"
                    clearable
                  />
                </el-form-item>
                <el-form-item
                  label="身份證字號："
                  :label-width="formLabelWidth"
                  prop="RCAGID"
                >
                  <el-input
                    v-model="editForm.RCAGID"
                    v-upperCase
                    placeholder="請輸入身分證字號/居留證號"
                    autocomplete="off"
                    clearable
                    maxlength="10"
                  />
                </el-form-item>
                <el-form-item
                  label="生日："
                  :label-width="formLabelWidth"
                  prop="RCAGBirthday"
                >
                  <DatePickerE
                    v-model="editForm.RCAGBirthday"
                    value-format="yyyy-MM-dd"
                    format="yyyy-MM-dd"
                    type="date"
                    placeholder="選擇日期"
                  />
                  <!-- <el-input
                    v-model="editForm.RCAGBirthday"
                    placeholder="請輸入生日"
                    autocomplete="off"
                    clearable
                    maxlength="10"
                  /> -->
                </el-form-item>
                <el-form-item
                  label="連絡電話："
                  :label-width="formLabelWidth"
                  prop="RCAGCell"
                >
                  <el-input
                    v-model="editForm.RCAGCell"
                    placeholder="請輸入連絡電話"
                    autocomplete="off"
                    clearable
                    maxlength="10"
                    onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                  />
                </el-form-item>
                <!-- <el-form-item
                  label="電話："
                  :label-width="formLabelWidth"
                >
                  <el-input
                    v-model="editForm.RCECTel_2"
                    placeholder="請輸入電話"
                    clearable
                    style="width: 220px"
                    class="input-with-select"
                    onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                  >
                    <el-select
                      slot="prepend"
                      v-model="editForm.RCECTel_1"
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
                </el-form-item> -->
              </el-row>
              <!-- <el-row>
                <el-form-item
                  label="關係："
                  :label-width="formLabelWidth"
                  prop="RCAGBirthday"
                >
                  <el-input
                    v-model="editForm.RCAGRe"
                    placeholder="請輸入關係"
                    autocomplete="off"
                    clearable
                    maxlength="10"
                  />
                </el-form-item>
              </el-row> -->
              <el-row>
                <el-form-item
                  label="戶籍地址："
                  :label-width="formLabelWidth"
                >
                  <Address
                    :sendedform="sendedformAG"
                    title="代理戶籍地址"
                    @geteditaddress="geteditaddress"
                  />
                </el-form-item>
              </el-row>
              <el-row>
                <el-form-item
                  label=" "
                  label-width="80px"
                >
                  <el-checkbox
                    v-model="RCAGAddSameCom"
                  >通訊地址同戶籍地址</el-checkbox>
                </el-form-item>
                <el-form-item
                  v-show="RCAGAddSameCom === false"
                  label="通訊地址："
                >
                  <Address
                    :sendedform="sendedformAGC"
                    :resetall="RCAGAddSameCom"
                    title="代理通訊地址"
                    @geteditaddress="geteditaddress"
                  />
                </el-form-item>
              </el-row>

              <div style="padding-top: 25px">
                <el-divider content-position="left">
                  <h2>保證人</h2>
                </el-divider>
              </div>
              <el-row>
                <el-form-item
                  label="姓名："
                  :label-width="formLabelWidth"
                  prop="RCGName"
                >
                  <el-input
                    v-model="editForm.RCGName"
                    placeholder="請輸入姓名"
                    autocomplete="off"
                    clearable
                  />
                </el-form-item>
                <el-form-item
                  label="身份證字號："
                  :label-width="formLabelWidth"
                  prop="RCGID"
                >
                  <el-input
                    v-model="editForm.RCGID"
                    v-upperCase
                    placeholder="請輸入身分證字號/居留證號"
                    autocomplete="off"
                    clearable
                    maxlength="10"
                  />
                </el-form-item>
                <el-form-item
                  label="生日："
                  :label-width="formLabelWidth"
                  prop="RCGBirthday"
                >
                  <DatePickerE
                    v-model="editForm.RCGBirthday"
                    value-format="yyyy-MM-dd"
                    format="yyyy-MM-dd"
                    type="date"
                    placeholder="選擇日期"
                  />
                  <!-- <el-input
                    v-model="editForm.RCGBirthday"
                    placeholder="請輸入生日"
                    autocomplete="off"
                    clearable
                    maxlength="10"
                  /> -->
                </el-form-item>
                <el-form-item
                  label="連絡電話："
                  :label-width="formLabelWidth"
                  prop="RCGCell"
                >
                  <el-input
                    v-model="editForm.RCGCell"
                    placeholder="請輸入連絡電話"
                    autocomplete="off"
                    clearable
                    maxlength="10"
                    onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                  />
                </el-form-item>
                <!-- <el-form-item
                  label="電話："
                  :label-width="formLabelWidth"
                >
                  <el-input
                    v-model="editForm.RCECTel_2"
                    placeholder="請輸入電話"
                    clearable
                    style="width: 220px"
                    class="input-with-select"
                    onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                  >
                    <el-select
                      slot="prepend"
                      v-model="editForm.RCECTel_1"
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
                </el-form-item> -->
              </el-row>
              <el-row>
                <el-form-item
                  label="關係："
                  :label-width="formLabelWidth"
                >
                  <el-input
                    v-model="editForm.RCGRelation"
                    placeholder="請輸入關係"
                    autocomplete="off"
                    clearable
                    maxlength="10"
                  />
                </el-form-item>
              </el-row>
              <el-row>
                <el-form-item
                  label="戶籍地址："
                  :label-width="formLabelWidth"
                >
                  <Address
                    :sendedform="sendedformG"
                    title="保證戶籍地址"
                    @geteditaddress="geteditaddress"
                  />
                </el-form-item>
              </el-row>
              <el-row>
                <el-form-item
                  label=" "
                  label-width="80px"
                >
                  <el-checkbox
                    v-model="RCGAddSameCom"
                  >通訊地址同戶籍地址</el-checkbox>
                </el-form-item>
                <el-form-item
                  v-show="RCGAddSameCom === false"
                  label="通訊地址："
                >
                  <Address
                    :sendedform="sendedformGC"
                    :resetall="RCGAddSameCom"
                    title="保證通訊地址"
                    @geteditaddress="geteditaddress"
                  />
                </el-form-item>
              </el-row>
              <!-- <div style="padding-top: 25px">
                <el-divider content-position="left">
                  <h2>代理人B</h2>
                </el-divider>
              </div>
              <el-row>
                <el-col :span="6">
                  <el-form-item
                    label="姓名："
                    :label-width="formLabelWidth"
                    prop="RCAGName_B"
                  >
                    <el-input
                      v-model="editForm.RCAGName_B"
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
                    prop="RCAGID_B"
                  >
                    <el-input
                      v-model="editForm.RCAGID_B"
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
                    prop="RCAGCell_B"
                  >
                    <el-input
                      v-model="editForm.RCAGCell_B"
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
                      v-model="editForm.RCAGTel_2_B"
                      placeholder="請輸入電話"
                      clearable
                      style="width: 220px"
                      class="input-with-select"
                      onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                    >
                      <el-select
                        slot="prepend"
                        v-model="editForm.RCAGTel_1_B"
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
                      title="代B地址"
                      @geteditaddress="geteditaddress"
                    />
                  </el-form-item>
                </el-col>
              </el-row> -->
            </el-form>
            <div class="rightbtn">
              <el-button
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
          <RCF
            :editform="editForm.RCFOutputDto"
            @cancel="closeDialogAndReset()"
          />
        </el-tab-pane>
        <el-tab-pane
          v-if="editFormTitle === '編輯'"
          label="承租法人匯款資料"
          name="C"
        >
          <el-card>
            <RCRemittance
              :id="currentId"
              :idno="editForm.RCID"
              :name="editForm.RCName"
              @cancel="closeDialogAndReset()"
            />
          </el-card>
        </el-tab-pane>
        <el-tab-pane
          v-if="editFormTitle === '編輯'"
          label="承租法人附件"
          name="D"
        >
          <el-card>
            <RCFormHistory
              :id="currentId"
              @cancel="closeDialogAndReset()"
            />
          </el-card>
        </el-tab-pane>
      </el-tabs>
    </el-dialog>
  </div>
</template>

<script>
import {
  getCustomerRCListWithPager,
  getCustomerRCDetail,
  saveCustomerRC,
  deleteCustomerRC
} from '@/api/chaochi/customerrc/customerrc';

import elDragDialog from '@/directive/el-drag-dialog'; // 彈窗可移動
import Address from '@/components/Address/Address.vue';
import RCF from './RCF.vue';
import RCRemittance from './RCRemittance.vue';
import RCFormHistory from './RCFormHistory.vue';
import DatePickerE from '@/components/RocDatepickerE';
import {
  validateCellReg,
  validateBusinessIDReg,
  validateIDReg
} from '@/utils/validate';
import { getSales } from '@/api/security/userservice';
export default {
  name: 'CustomerRC', // 需與菜單的功能編碼一致
  directives: { elDragDialog },
  components: { Address, RCF, RCRemittance, RCFormHistory, DatePickerE },
  data() {
    return {
      editForm: {},
      sendedform: {},
      sendedform1: {},
      sendedformEC: {},
      sendedformECC: {},
      sendedformECF: {},
      sendedformECFC: {},
      sendedformAG: {},
      sendedformAGC: {},
      sendedformG: {},
      sendedformGC: {},
      searchform: {
        keywords: ''
      },
      rules: {
        RCName: [
          {
            required: true,
            trigger: 'blur',
            message: '請輸入名稱'
          }
        ],
        RCRep: [
          {
            required: true,
            trigger: 'blur',
            message: '請輸入代表人'
          }
        ],
        RCID: [
          {
            required: true,
            trigger: 'blur',
            validator: validateBusinessIDReg
          }
          // { min: 8, max: 8, message: '請輸入8位', trigger: 'change' }
        ],
        RCTel_2: [
          {
            required: true,
            trigger: 'blur',
            message: '請輸入號碼'
          }
        ],
        RCTel_1: [
          {
            required: true,
            trigger: 'blur',
            message: '請輸入號碼'
          }
        ],
        RCECID: [
          {
            required: false,
            min: 10,
            max: 10,
            validator: validateIDReg,
            trigger: 'change'
          }
        ],
        RCECFID: [
          {
            required: false,
            min: 10,
            max: 10,
            validator: validateIDReg,
            trigger: 'change'
          }
        ],
        RCAGID: [
          {
            required: false,
            min: 10,
            max: 10,
            validator: validateIDReg,
            trigger: 'change'
          }
        ],
        RCGID: [
          {
            required: false,
            min: 10,
            max: 10,
            validator: validateIDReg,
            trigger: 'change'
          }
        ],
        // 非必填
        RCECCell: [
          { required: false, trigger: 'change', validator: validateCellReg }
        ],
        RCECFCell: [
          { required: false, trigger: 'change', validator: validateCellReg }
        ],
        RCAGCell: [
          { required: false, trigger: 'change', validator: validateCellReg }
        ],
        RCGCell: [
          { required: false, trigger: 'change', validator: validateCellReg }
        ]
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
      tableData: [],
      currentSelected: [],
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
      dialogEditFormVisible: false,
      loading: false,
      RCAddSameCom: false,
      RCECAddSameCom: false,
      RCECFAddSameCom: false,
      RCAGAddSameCom: false,
      RCGAddSameCom: false,
      editFormTitle: '',
      formLabelWidth: '145px',
      activeName: 'A',
      currentId: ''
    };
  },
  watch: {
    'editForm.RCAddSame': {
      handler(a) {
        if (a === '/Yes') {
          this.RCAddSameCom = true;
        } else {
          this.RCAddSameCom = false;
        }
      }
    },
    RCAddSameCom: {
      handler(a) {
        if (a === true) {
          this.editForm.RCAddSame = '/Yes';
        }
        if (a === false) {
          this.editForm.RCAddSame = '/Off';
        }
      }
    },
    'editForm.RCECAddSame': {
      handler(a) {
        if (a === '/Yes') {
          this.RCECAddSameCom = true;
        } else {
          this.RCECAddSameCom = false;
        }
      }
    },
    RCECAddSameCom: {
      handler(a) {
        if (a === true) {
          this.editForm.RCECAddSame = '/Yes';
        }
        if (a === false) {
          this.editForm.RCECAddSame = '/Off';
        }
      }
    },
    'editForm.RCECFAddSame': {
      handler(a) {
        if (a === '/Yes') {
          this.RCECFAddSameCom = true;
        } else {
          this.RCECFAddSameCom = false;
        }
      }
    },
    RCECFAddSameCom: {
      handler(a) {
        if (a === true) {
          this.editForm.RCECFAddSame = '/Yes';
        }
        if (a === false) {
          this.editForm.RCECFAddSame = '/Off';
        }
      }
    },
    'editForm.RCAGAddSame': {
      handler(a) {
        if (a === '/Yes') {
          this.RCAGAddSameCom = true;
        } else {
          this.RCAGAddSameCom = false;
        }
      }
    },
    RCAGAddSameCom: {
      handler(a) {
        if (a === true) {
          this.editForm.RCAGAddSame = '/Yes';
        }
        if (a === false) {
          this.editForm.RCAGAddSame = '/Off';
        }
      }
    },
    'editForm.RCGAddSame': {
      handler(a) {
        if (a === '/Yes') {
          this.RCGAddSameCom = true;
        } else {
          this.RCGAddSameCom = false;
        }
      }
    },
    RCGAddSameCom: {
      handler(a) {
        if (a === true) {
          this.editForm.RCGAddSame = '/Yes';
        }
        if (a === false) {
          this.editForm.RCGAddSame = '/Off';
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

    // 取消按鈕
    cancel() {
      this.dialogEditFormVisible = false;
      this.reset();
    },
    // 表單重置
    reset() {
      this.editForm = {
        RCName: '',
        RCEngName: '',
        RCRep: '',
        RCID: '',
        RCTel: '',
        RCTel_1: '',
        RCTel_2: '',
        RCCell: '',
        RCMail: '',
        RCFRelation: '',
        CreatorTime: '',
        CreatorUserId: '',
        CreatorUserOrgId: '',
        CreatorUserDeptId: '',
        DeleteMark: '',
        LastModifyTime: '',
        LastModifyUserId: '',
        DeleteTime: '',
        DeleteUserId: '',
        RCECName: '',
        RCECID: '',
        RCECGender_1: '',
        RCECGender_2: '',
        RCECBirthday: '',
        RCECRelation: '',
        RCECCell: '',
        RCECTel_1: '',
        RCECTel_2: '',
        RCECFName: '',
        RCECFID: '',
        RCECFGender_1: '',
        RCECFGender_2: '',
        RCECFBirthday: '',
        RCECFCell: '',
        RCECFRelation: '',
        RCAGName: '',
        RCAGID: '',
        RCAGGender_1: '',
        RCAGGender_2: '',
        RCAGBirthday: '',
        RCGName: '',
        RCGID: '',
        RCGGender_1: '',
        RCGGender_2: '',
        RCGBirthday: '',
        RCGCell: '',
        RCGRelation: ''

        // 需個性化處理內容
      };
      this.resetForm('editForm');
    },
    /**
     * 加載頁面table數據
     */
    loadTableData: function() {
      this.tableloading = true;
      var t = {
        RCName: this.searchform.RCName,
        RCRep: this.searchform.RCRep,
        RCID: this.searchform.RCID,
        RCTel_2: this.searchform.RCTel_2,
        RCTel_1: this.searchform.RCTel_1,
        RNARCAdddd: this.searchform.RCAdd,
        CreatorUserId: this.searchform.CreatorId
      };
      var seachdata = {
        CurrenetPageIndex: this.pagination.currentPage,
        PageSize: this.pagination.pagesize,
        Keywords: this.searchform.keywords,
        Filter: t,
        Order: this.sortableData.order,
        Sort: this.sortableData.sort
      };
      getCustomerRCListWithPager(seachdata).then((res) => {
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
      getCustomerRCDetail(this.currentId)
        .then((res) => {
          this.editForm = res.ResData;
          this.sendedform = {
            Add_1: this.editForm.RCAdd_1,
            Add_1_1: this.editForm.RCAdd_1_1,
            Add_1_2: this.editForm.RCAdd_1_2,
            Add_2: this.editForm.RCAdd_2,
            Add_2_1: this.editForm.RCAdd_2_1,
            Add_2_2: this.editForm.RCAdd_2_2,
            Add_2_3: this.editForm.RCAdd_2_3,
            Add_2_4: this.editForm.RCAdd_2_4,
            Add_3: this.editForm.RCAdd_3,
            Add_3_1: this.editForm.RCAdd_3_1,
            Add_3_2: this.editForm.RCAdd_3_2,
            Add_4: this.editForm.RCAdd_4,
            Add_5: this.editForm.RCAdd_5,
            Add_6: this.editForm.RCAdd_6,
            Add_7: this.editForm.RCAdd_7,
            Add_8: this.editForm.RCAdd_8,
            Add_9: this.editForm.RCAdd_9
          };
          this.sendedform1 = {
            Add_1: this.editForm.RCAddC_1,
            Add_1_1: this.editForm.RCAddC_1_1,
            Add_1_2: this.editForm.RCAddC_1_2,
            Add_2: this.editForm.RCAddC_2,
            Add_2_1: this.editForm.RCAddC_2_1,
            Add_2_2: this.editForm.RCAddC_2_2,
            Add_2_3: this.editForm.RCAddC_2_3,
            Add_2_4: this.editForm.RCAddC_2_4,
            Add_3: this.editForm.RCAddC_3,
            Add_3_1: this.editForm.RCAddC_3_1,
            Add_3_2: this.editForm.RCAddC_3_2,
            Add_4: this.editForm.RCAddC_4,
            Add_5: this.editForm.RCAddC_5,
            Add_6: this.editForm.RCAddC_6,
            Add_7: this.editForm.RCAddC_7,
            Add_8: this.editForm.RCAddC_8,
            Add_9: this.editForm.RCAddC_9
          };
          this.sendedformEC = {
            Add_1: this.editForm.RCECAdd_1,
            Add_1_1: this.editForm.RCECAdd_1_1,
            Add_1_2: this.editForm.RCECAdd_1_2,
            Add_2: this.editForm.RCECAdd_2,
            Add_2_1: this.editForm.RCECAdd_2_1,
            Add_2_2: this.editForm.RCECAdd_2_2,
            Add_2_3: this.editForm.RCECAdd_2_3,
            Add_2_4: this.editForm.RCECAdd_2_4,
            Add_3: this.editForm.RCECAdd_3,
            Add_3_1: this.editForm.RCECAdd_3_1,
            Add_3_2: this.editForm.RCECAdd_3_2,
            Add_4: this.editForm.RCECAdd_4,
            Add_5: this.editForm.RCECAdd_5,
            Add_6: this.editForm.RCECAdd_6,
            Add_7: this.editForm.RCECAdd_7,
            Add_8: this.editForm.RCECAdd_8,
            Add_9: this.editForm.RCECAdd_9
          };
          this.sendedformECC = {
            Add_1: this.editForm.RCECAddC_1,
            Add_1_1: this.editForm.RCECAddC_1_1,
            Add_1_2: this.editForm.RCECAddC_1_2,
            Add_2: this.editForm.RCECAddC_2,
            Add_2_1: this.editForm.RCECAddC_2_1,
            Add_2_2: this.editForm.RCECAddC_2_2,
            Add_2_3: this.editForm.RCECAddC_2_3,
            Add_2_4: this.editForm.RCECAddC_2_4,
            Add_3: this.editForm.RCECAddC_3,
            Add_3_1: this.editForm.RCECAddC_3_1,
            Add_3_2: this.editForm.RCECAddC_3_2,
            Add_4: this.editForm.RCECAddC_4,
            Add_5: this.editForm.RCECAddC_5,
            Add_6: this.editForm.RCECAddC_6,
            Add_7: this.editForm.RCECAddC_7,
            Add_8: this.editForm.RCECAddC_8,
            Add_9: this.editForm.RCECAddC_9
          };
          this.sendedformECF = {
            Add_1: this.editForm.RCECFAdd_1,
            Add_1_1: this.editForm.RCECFAdd_1_1,
            Add_1_2: this.editForm.RCECFAdd_1_2,
            Add_2: this.editForm.RCECFAdd_2,
            Add_2_1: this.editForm.RCECFAdd_2_1,
            Add_2_2: this.editForm.RCECFAdd_2_2,
            Add_2_3: this.editForm.RCECFAdd_2_3,
            Add_2_4: this.editForm.RCECFAdd_2_4,
            Add_3: this.editForm.RCECFAdd_3,
            Add_3_1: this.editForm.RCECFAdd_3_1,
            Add_3_2: this.editForm.RCECFAdd_3_2,
            Add_4: this.editForm.RCECFAdd_4,
            Add_5: this.editForm.RCECFAdd_5,
            Add_6: this.editForm.RCECFAdd_6,
            Add_7: this.editForm.RCECFAdd_7,
            Add_8: this.editForm.RCECFAdd_8,
            Add_9: this.editForm.RCECFAdd_9
          };
          this.sendedformECFC = {
            Add_1: this.editForm.RCECFAddC_1,
            Add_1_1: this.editForm.RCECFAddC_1_1,
            Add_1_2: this.editForm.RCECFAddC_1_2,
            Add_2: this.editForm.RCECFAddC_2,
            Add_2_1: this.editForm.RCECFAddC_2_1,
            Add_2_2: this.editForm.RCECFAddC_2_2,
            Add_2_3: this.editForm.RCECFAddC_2_3,
            Add_2_4: this.editForm.RCECFAddC_2_4,
            Add_3: this.editForm.RCECFAddC_3,
            Add_3_1: this.editForm.RCECFAddC_3_1,
            Add_3_2: this.editForm.RCECFAddC_3_2,
            Add_4: this.editForm.RCECFAddC_4,
            Add_5: this.editForm.RCECFAddC_5,
            Add_6: this.editForm.RCECFAddC_6,
            Add_7: this.editForm.RCECFAddC_7,
            Add_8: this.editForm.RCECFAddC_8,
            Add_9: this.editForm.RCECFAddC_9
          };
          this.sendedformAG = {
            Add_1: this.editForm.RCAGAdd_1,
            Add_1_1: this.editForm.RCAGAdd_1_1,
            Add_1_2: this.editForm.RCAGAdd_1_2,
            Add_2: this.editForm.RCAGAdd_2,
            Add_2_1: this.editForm.RCAGAdd_2_1,
            Add_2_2: this.editForm.RCAGAdd_2_2,
            Add_2_3: this.editForm.RCAGAdd_2_3,
            Add_2_4: this.editForm.RCAGAdd_2_4,
            Add_3: this.editForm.RCAGAdd_3,
            Add_3_1: this.editForm.RCAGAdd_3_1,
            Add_3_2: this.editForm.RCAGAdd_3_2,
            Add_4: this.editForm.RCAGAdd_4,
            Add_5: this.editForm.RCAGAdd_5,
            Add_6: this.editForm.RCAGAdd_6,
            Add_7: this.editForm.RCAGAdd_7,
            Add_8: this.editForm.RCAGAdd_8,
            Add_9: this.editForm.RCAGAdd_9
          };
          this.sendedformAGC = {
            Add_1: this.editForm.RCAGAddC_1,
            Add_1_1: this.editForm.RCAGAddC_1_1,
            Add_1_2: this.editForm.RCAGAddC_1_2,
            Add_2: this.editForm.RCAGAddC_2,
            Add_2_1: this.editForm.RCAGAddC_2_1,
            Add_2_2: this.editForm.RCAGAddC_2_2,
            Add_2_3: this.editForm.RCAGAddC_2_3,
            Add_2_4: this.editForm.RCAGAddC_2_4,
            Add_3: this.editForm.RCAGAddC_3,
            Add_3_1: this.editForm.RCAGAddC_3_1,
            Add_3_2: this.editForm.RCAGAddC_3_2,
            Add_4: this.editForm.RCAGAddC_4,
            Add_5: this.editForm.RCAGAddC_5,
            Add_6: this.editForm.RCAGAddC_6,
            Add_7: this.editForm.RCAGAddC_7,
            Add_8: this.editForm.RCAGAddC_8,
            Add_9: this.editForm.RCAGAddC_9
          };
          this.sendedformG = {
            Add_1: this.editForm.RCGAdd_1,
            Add_1_1: this.editForm.RCGAdd_1_1,
            Add_1_2: this.editForm.RCGAdd_1_2,
            Add_2: this.editForm.RCGAdd_2,
            Add_2_1: this.editForm.RCGAdd_2_1,
            Add_2_2: this.editForm.RCGAdd_2_2,
            Add_2_3: this.editForm.RCGAdd_2_3,
            Add_2_4: this.editForm.RCGAdd_2_4,
            Add_3: this.editForm.RCGAdd_3,
            Add_3_1: this.editForm.RCGAdd_3_1,
            Add_3_2: this.editForm.RCGAdd_3_2,
            Add_4: this.editForm.RCGAdd_4,
            Add_5: this.editForm.RCGAdd_5,
            Add_6: this.editForm.RCGAdd_6,
            Add_7: this.editForm.RCGAdd_7,
            Add_8: this.editForm.RCGAdd_8,
            Add_9: this.editForm.RCGAdd_9
          };
          this.sendedformGC = {
            Add_1: this.editForm.RCGCAdd_1,
            Add_1_1: this.editForm.RCGCAdd_1_1,
            Add_1_2: this.editForm.RCGCAdd_1_2,
            Add_2: this.editForm.RCGCAdd_2,
            Add_2_1: this.editForm.RCGCAdd_2_1,
            Add_2_2: this.editForm.RCGCAdd_2_2,
            Add_2_3: this.editForm.RCGCAdd_2_3,
            Add_2_4: this.editForm.RCGCAdd_2_4,
            Add_3: this.editForm.RCGCAdd_3,
            Add_3_1: this.editForm.RCGCAdd_3_1,
            Add_3_2: this.editForm.RCGCAdd_3_2,
            Add_4: this.editForm.RCGCAdd_4,
            Add_5: this.editForm.RCGCAdd_5,
            Add_6: this.editForm.RCGCAdd_6,
            Add_7: this.editForm.RCGCAdd_7,
            Add_8: this.editForm.RCGCAdd_8,
            Add_9: this.editForm.RCGCAdd_9
          };
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
      this.$refs['editForm'].validate((valid) => {
        if (valid) {
          // const data = {
          //   RCName: this.editForm.RCName,
          //   RCEngName: this.editForm.RCEngName,
          //   RCRep: this.editForm.RCRep,
          //   RCID: this.editForm.RCID,
          //   RCID_1_1: this.editForm.RCID_1_1,
          //   RCID_1_2: this.editForm.RCID_1_2,
          //   RCID_1_3: this.editForm.RCID_1_3,
          //   RCID_1_4: this.editForm.RCID_1_4,
          //   RCID_1_5: this.editForm.RCID_1_5,
          //   RCID_1_6: this.editForm.RCID_1_6,
          //   RCID_1_7: this.editForm.RCID_1_7,
          //   RCID_1_8: this.editForm.RCID_1_8,
          //   RCTel: this.editForm.RCTel,
          //   RCTel_1: this.editForm.RCTel_1,
          //   RCTel_2: this.editForm.RCTel_2,
          //   RCCell: this.editForm.RCCell,
          //   RCMail: this.editForm.RCMail,
          //   RCAdd: this.editForm.RCAdd,
          //   RCAdd_1: this.editForm.RCAdd_1,
          //   RCAdd_1_1: this.editForm.RCAdd_1_1,
          //   RCAdd_1_2: this.editForm.RCAdd_1_2,
          //   RCAdd_2: this.editForm.RCAdd_2,
          //   RCAdd_2_1: this.editForm.RCAdd_2_1,
          //   RCAdd_2_2: this.editForm.RCAdd_2_2,
          //   RCAdd_2_3: this.editForm.RCAdd_2_3,
          //   RCAdd_2_4: this.editForm.RCAdd_2_4,
          //   RCAdd_3: this.editForm.RCAdd_3,
          //   RCAdd_3_1: this.editForm.RCAdd_3_1,
          //   RCAdd_3_2: this.editForm.RCAdd_3_2,
          //   RCAdd_4: this.editForm.RCAdd_4,
          //   RCAdd_5: this.editForm.RCAdd_5,
          //   RCAdd_6: this.editForm.RCAdd_6,
          //   RCAdd_7: this.editForm.RCAdd_7,
          //   RCAdd_8: this.editForm.RCAdd_8,
          //   RCAdd_9: this.editForm.RCAdd_9,
          //   RCAddSame: this.editForm.RCAddSame,
          //   RCAddC: this.editForm.RCAddC,
          //   RCAddC_1: this.editForm.RCAddC_1,
          //   RCAddC_1_1: this.editForm.RCAddC_1_1,
          //   RCAddC_1_2: this.editForm.RCAddC_1_2,
          //   RCAddC_2: this.editForm.RCAddC_2,
          //   RCAddC_2_1: this.editForm.RCAddC_2_1,
          //   RCAddC_2_2: this.editForm.RCAddC_2_2,
          //   RCAddC_2_3: this.editForm.RCAddC_2_3,
          //   RCAddC_2_4: this.editForm.RCAddC_2_4,
          //   RCAddC_3: this.editForm.RCAddC_3,
          //   RCAddC_3_1: this.editForm.RCAddC_3_1,
          //   RCAddC_3_2: this.editForm.RCAddC_3_2,
          //   RCAddC_4: this.editForm.RCAddC_4,
          //   RCAddC_5: this.editForm.RCAddC_5,
          //   RCAddC_6: this.editForm.RCAddC_6,
          //   RCAddC_7: this.editForm.RCAddC_7,
          //   RCAddC_8: this.editForm.RCAddC_8,
          //   RCAddC_9: this.editForm.RCAddC_9,
          //   RCFRelation: this.editForm.RCFRelation,
          //   RCFName_A: this.editForm.RCFName_A,
          //   RCFRelation_A: this.editForm.RCFRelation_A,
          //   RCFCell_A: this.editForm.RCFCell_A,
          //   RCFName_B: this.editForm.RCFName_B,
          //   RCFRelation_B: this.editForm.RCFRelation_B,
          //   RCFCell_B: this.editForm.RCFCell_B,
          //   RCFName_C: this.editForm.RCFName_C,
          //   RCFRelation_C: this.editForm.RCFRelation_C,
          //   RCFCell_C: this.editForm.RCFCell_C,
          //   RCFID_1_A: this.editForm.RCFID_1_A,
          //   RCFID_1_A_1: this.editForm.RCFID_1_A_1,
          //   RCFID_1_A_2: this.editForm.RCFID_1_A_2,
          //   RCFID_1_A_3: this.editForm.RCFID_1_A_3,
          //   RCFID_1_A_4: this.editForm.RCFID_1_A_4,
          //   RCFID_1_A_5: this.editForm.RCFID_1_A_5,
          //   RCFID_1_A_6: this.editForm.RCFID_1_A_6,
          //   RCFID_1_A_7: this.editForm.RCFID_1_A_7,
          //   RCFID_1_A_8: this.editForm.RCFID_1_A_8,
          //   RCFID_1_A_9: this.editForm.RCFID_1_A_9,
          //   RCFID_1_A_10: this.editForm.RCFID_1_A_10,
          //   RCFBirthday_A: this.editForm.RCFBirthday_A,
          //   RCFBirthday_Y_A: this.editForm.RCFBirthday_Y_A,
          //   RCFBirthday_M_A: this.editForm.RCFBirthday_M_A,
          //   RCFBirthday_D_A: this.editForm.RCFBirthday_D_A,
          //   CreatorTime: this.editForm.CreatorTime,
          //   CreatorUserId: this.editForm.CreatorUserId,
          //   CreatorUserOrgId: this.editForm.CreatorUserOrgId,
          //   CreatorUserDeptId: this.editForm.CreatorUserDeptId,
          //   DeleteMark: this.editForm.DeleteMark,
          //   LastModifyTime: this.editForm.LastModifyTime,
          //   LastModifyUserId: this.editForm.LastModifyUserId,
          //   DeleteTime: this.editForm.DeleteTime,
          //   DeleteUserId: this.editForm.DeleteUserId,
          //   RCFID_1_B: this.editForm.RCFID_1_B,
          //   RCFID_1_B_1: this.editForm.RCFID_1_B_1,
          //   RCFID_1_B_2: this.editForm.RCFID_1_B_2,
          //   RCFID_1_B_3: this.editForm.RCFID_1_B_3,
          //   RCFID_1_B_4: this.editForm.RCFID_1_B_4,
          //   RCFID_1_B_5: this.editForm.RCFID_1_B_5,
          //   RCFID_1_B_6: this.editForm.RCFID_1_B_6,
          //   RCFID_1_B_7: this.editForm.RCFID_1_B_7,
          //   RCFID_1_B_8: this.editForm.RCFID_1_B_8,
          //   RCFID_1_B_9: this.editForm.RCFID_1_B_9,
          //   RCFID_1_B_10: this.editForm.RCFID_1_B_10,
          //   RCFBirthday_B: this.editForm.RCFBirthday_B,
          //   RCFBirthday_Y_B: this.editForm.RCFBirthday_Y_B,
          //   RCFBirthday_M_B: this.editForm.RCFBirthday_M_B,
          //   RCFBirthday_D_B: this.editForm.RCFBirthday_D_B,
          //   RCFID_1_C: this.editForm.RCFID_1_C,
          //   RCFID_1_C_1: this.editForm.RCFID_1_C_1,
          //   RCFID_1_C_2: this.editForm.RCFID_1_C_2,
          //   RCFID_1_C_3: this.editForm.RCFID_1_C_3,
          //   RCFID_1_C_4: this.editForm.RCFID_1_C_4,
          //   RCFID_1_C_5: this.editForm.RCFID_1_C_5,
          //   RCFID_1_C_6: this.editForm.RCFID_1_C_6,
          //   RCFID_1_C_7: this.editForm.RCFID_1_C_7,
          //   RCFID_1_C_8: this.editForm.RCFID_1_C_8,
          //   RCFID_1_C_9: this.editForm.RCFID_1_C_9,
          //   RCFID_1_C_10: this.editForm.RCFID_1_C_10,
          //   RCFBirthday_C: this.editForm.RCFBirthday_C,
          //   RCFBirthday_Y_C: this.editForm.RCFBirthday_Y_C,
          //   RCFBirthday_M_C: this.editForm.RCFBirthday_M_C,
          //   RCFBirthday_D_C: this.editForm.RCFBirthday_D_C,
          //   RCFName_D: this.editForm.RCFName_D,
          //   RCFRelation_D: this.editForm.RCFRelation_D,
          //   RCFCell_D: this.editForm.RCFCell_D,
          //   RCFID_1_D: this.editForm.RCFID_1_D,
          //   RCFID_1_D_1: this.editForm.RCFID_1_D_1,
          //   RCFID_1_D_2: this.editForm.RCFID_1_D_2,
          //   RCFID_1_D_3: this.editForm.RCFID_1_D_3,
          //   RCFID_1_D_4: this.editForm.RCFID_1_D_4,
          //   RCFID_1_D_5: this.editForm.RCFID_1_D_5,
          //   RCFID_1_D_6: this.editForm.RCFID_1_D_6,
          //   RCFID_1_D_7: this.editForm.RCFID_1_D_7,
          //   RCFID_1_D_8: this.editForm.RCFID_1_D_8,
          //   RCFID_1_D_9: this.editForm.RCFID_1_D_9,
          //   RCFID_1_D_10: this.editForm.RCFID_1_D_10,
          //   RCFBirthday_D: this.editForm.RCFBirthday_D,
          //   RCFBirthday_Y_D: this.editForm.RCFBirthday_Y_D,
          //   RCFBirthday_M_D: this.editForm.RCFBirthday_M_D,
          //   RCFBirthday_D_D: this.editForm.RCFBirthday_D_D,
          //   RCFName_E: this.editForm.RCFName_E,
          //   RCFCell_E: this.editForm.RCFCell_E,
          //   RCFID_1_E: this.editForm.RCFID_1_E,
          //   RCFID_1_E_1: this.editForm.RCFID_1_E_1,
          //   RCFID_1_E_2: this.editForm.RCFID_1_E_2,
          //   RCFID_1_E_3: this.editForm.RCFID_1_E_3,
          //   RCFID_1_E_4: this.editForm.RCFID_1_E_4,
          //   RCFID_1_E_5: this.editForm.RCFID_1_E_5,
          //   RCFID_1_E_6: this.editForm.RCFID_1_E_6,
          //   RCFID_1_E_7: this.editForm.RCFID_1_E_7,
          //   RCFID_1_E_8: this.editForm.RCFID_1_E_8,
          //   RCFID_1_E_9: this.editForm.RCFID_1_E_9,
          //   RCFID_1_E_10: this.editForm.RCFID_1_E_10,
          //   RCFBirthday_E: this.editForm.RCFBirthday_E,
          //   RCFBirthday_Y_E: this.editForm.RCFBirthday_Y_E,
          //   RCFBirthday_M_E: this.editForm.RCFBirthday_M_E,
          //   RCFBirthday_D_E: this.editForm.RCFBirthday_D_E,
          //   RCFName_F: this.editForm.RCFName_F,
          //   RCFRelation_F: this.editForm.RCFRelation_F,
          //   RCFCell_F: this.editForm.RCFCell_F,
          //   RCFID_1_F: this.editForm.RCFID_1_F,
          //   RCFID_1_F_1: this.editForm.RCFID_1_F_1,
          //   RCFID_1_F_2: this.editForm.RCFID_1_F_2,
          //   RCFID_1_F_3: this.editForm.RCFID_1_F_3,
          //   RCFID_1_F_4: this.editForm.RCFID_1_F_4,
          //   RCFID_1_F_5: this.editForm.RCFID_1_F_5,
          //   RCFID_1_F_6: this.editForm.RCFID_1_F_6,
          //   RCFID_1_F_7: this.editForm.RCFID_1_F_7,
          //   RCFID_1_F_8: this.editForm.RCFID_1_F_8,
          //   RCFID_1_F_9: this.editForm.RCFID_1_F_9,
          //   RCFID_1_F_10: this.editForm.RCFID_1_F_10,
          //   RCFBirthday_F: this.editForm.RCFBirthday_F,
          //   RCFBirthday_Y_F: this.editForm.RCFBirthday_Y_F,
          //   RCFBirthday_M_F: this.editForm.RCFBirthday_M_F,
          //   RCFBirthday_D_F: this.editForm.RCFBirthday_D_F,
          //   RCFName_G: this.editForm.RCFName_G,
          //   RCFRelation_G: this.editForm.RCFRelation_G,
          //   RCFCell_G: this.editForm.RCFCell_G,
          //   RCFID_1_G: this.editForm.RCFID_1_G,
          //   RCFID_1_G_1: this.editForm.RCFID_1_G_1,
          //   RCFID_1_G_2: this.editForm.RCFID_1_G_2,
          //   RCFID_1_G_3: this.editForm.RCFID_1_G_3,
          //   RCFID_1_G_4: this.editForm.RCFID_1_G_4,
          //   RCFID_1_G_5: this.editForm.RCFID_1_G_5,
          //   RCFID_1_G_6: this.editForm.RCFID_1_G_6,
          //   RCFID_1_G_7: this.editForm.RCFID_1_G_7,
          //   RCFID_1_G_8: this.editForm.RCFID_1_G_8,
          //   RCFID_1_G_9: this.editForm.RCFID_1_G_9,
          //   RCFID_1_G_10: this.editForm.RCFID_1_G_10,
          //   RCFBirthday_G: this.editForm.RCFBirthday_G,
          //   RCFBirthday_Y_G: this.editForm.RCFBirthday_Y_G,
          //   RCFBirthday_M_G: this.editForm.RCFBirthday_M_G,
          //   RCFBirthday_D_G: this.editForm.RCFBirthday_D_G,
          //   RCECName: this.editForm.RCECName,
          //   RCECID: this.editForm.RCECID,
          //   RCECGender_1: this.editForm.RCECGender_1,
          //   RCECGender_2: this.editForm.RCECGender_2,
          //   RCECBirthday: this.editForm.RCECBirthday,
          //   RCECRelation: this.editForm.RCECRelation,
          //   RCECCell: this.editForm.RCECCell,
          //   RCECTel_1: this.editForm.RCECTel_1,
          //   RCECTel_2: this.editForm.RCECTel_2,
          //   RCECAdd: this.editForm.RCECAdd,
          //   RCECAddSame: this.editForm.RCECAddSame,
          //   RCECAddC: this.editForm.RCECAddC,
          //   RCECAdd_1: this.editForm.RCECAdd_1,
          //   RCECAdd_1_1: this.editForm.RCECAdd_1_1,
          //   RCECAdd_1_2: this.editForm.RCECAdd_1_2,
          //   RCECAdd_2: this.editForm.RCECAdd_2,
          //   RCECAdd_2_1: this.editForm.RCECAdd_2_1,
          //   RCECAdd_2_2: this.editForm.RCECAdd_2_2,
          //   RCECAdd_2_3: this.editForm.RCECAdd_2_3,
          //   RCECAdd_2_4: this.editForm.RCECAdd_2_4,
          //   RCECAdd_3: this.editForm.RCECAdd_3,
          //   RCECAdd_3_1: this.editForm.RCECAdd_3_1,
          //   RCECAdd_3_2: this.editForm.RCECAdd_3_2,
          //   RCECAdd_4: this.editForm.RCECAdd_4,
          //   RCECAdd_5: this.editForm.RCECAdd_5,
          //   RCECAdd_6: this.editForm.RCECAdd_6,
          //   RCECAdd_7: this.editForm.RCECAdd_7,
          //   RCECAdd_8: this.editForm.RCECAdd_8,
          //   RCECAdd_9: this.editForm.RCECAdd_9,
          //   RCECAddC_1: this.editForm.RCECAddC_1,
          //   RCECAddC_1_1: this.editForm.RCECAddC_1_1,
          //   RCECAddC_1_2: this.editForm.RCECAddC_1_2,
          //   RCECAddC_2: this.editForm.RCECAddC_2,
          //   RCECAddC_2_1: this.editForm.RCECAddC_2_1,
          //   RCECAddC_2_2: this.editForm.RCECAddC_2_2,
          //   RCECAddC_2_3: this.editForm.RCECAddC_2_3,
          //   RCECAddC_2_4: this.editForm.RCECAddC_2_4,
          //   RCECAddC_3: this.editForm.RCECAddC_3,
          //   RCECAddC_3_1: this.editForm.RCECAddC_3_1,
          //   RCECAddC_3_2: this.editForm.RCECAddC_3_2,
          //   RCECAddC_4: this.editForm.RCECAddC_4,
          //   RCECAddC_5: this.editForm.RCECAddC_5,
          //   RCECAddC_6: this.editForm.RCECAddC_6,
          //   RCECAddC_7: this.editForm.RCECAddC_7,
          //   RCECAddC_8: this.editForm.RCECAddC_8,
          //   RCECAddC_9: this.editForm.RCECAddC_9,
          //   RCECFName: this.editForm.RCECFName,
          //   RCECFID: this.editForm.RCECFID,
          //   RCECFGender_1: this.editForm.RCECFGender_1,
          //   RCECFGender_2: this.editForm.RCECFGender_2,
          //   RCECFBirthday: this.editForm.RCECFBirthday,
          //   RCECFCell: this.editForm.RCECFCell,
          //   RCECFRelation: this.editForm.RCECFRelation,
          //   RCECFAdd: this.editForm.RCECFAdd,
          //   RCECFAdd_1: this.editForm.RCECFAdd_1,
          //   RCECFAdd_1_1: this.editForm.RCECFAdd_1_1,
          //   RCECFAdd_1_2: this.editForm.RCECFAdd_1_2,
          //   RCECFAdd_2: this.editForm.RCECFAdd_2,
          //   RCECFAdd_2_1: this.editForm.RCECFAdd_2_1,
          //   RCECFAdd_2_2: this.editForm.RCECFAdd_2_2,
          //   RCECFAdd_2_3: this.editForm.RCECFAdd_2_3,
          //   RCECFAdd_2_4: this.editForm.RCECFAdd_2_4,
          //   RCECFAdd_3: this.editForm.RCECFAdd_3,
          //   RCECFAdd_3_1: this.editForm.RCECFAdd_3_1,
          //   RCECFAdd_3_2: this.editForm.RCECFAdd_3_2,
          //   RCECFAdd_4: this.editForm.RCECFAdd_4,
          //   RCECFAdd_5: this.editForm.RCECFAdd_5,
          //   RCECFAdd_6: this.editForm.RCECFAdd_6,
          //   RCECFAdd_7: this.editForm.RCECFAdd_7,
          //   RCECFAdd_8: this.editForm.RCECFAdd_8,
          //   RCECFAdd_9: this.editForm.RCECFAdd_9,
          //   RCECFAddSame: this.editForm.RCECFAddSame,
          //   RCECFAddC: this.editForm.RCECFAddC,
          //   RCECFAddC_1: this.editForm.RCECFAddC_1,
          //   RCECFAddC_1_1: this.editForm.RCECFAddC_1_1,
          //   RCECFAddC_1_2: this.editForm.RCECFAddC_1_2,
          //   RCECFAddC_2: this.editForm.RCECFAddC_2,
          //   RCECFAddC_2_1: this.editForm.RCECFAddC_2_1,
          //   RCECFAddC_2_2: this.editForm.RCECFAddC_2_2,
          //   RCECFAddC_2_3: this.editForm.RCECFAddC_2_3,
          //   RCECFAddC_2_4: this.editForm.RCECFAddC_2_4,
          //   RCECFAddC_3: this.editForm.RCECFAddC_3,
          //   RCECFAddC_3_1: this.editForm.RCECFAddC_3_1,
          //   RCECFAddC_3_2: this.editForm.RCECFAddC_3_2,
          //   RCECFAddC_4: this.editForm.RCECFAddC_4,
          //   RCECFAddC_5: this.editForm.RCECFAddC_5,
          //   RCECFAddC_6: this.editForm.RCECFAddC_6,
          //   RCECFAddC_7: this.editForm.RCECFAddC_7,
          //   RCECFAddC_8: this.editForm.RCECFAddC_8,
          //   RCECFAddC_9: this.editForm.RCECFAddC_9,
          //   RCAGName_A: this.editForm.RCAGName_A,
          //   RCAGID_A: this.editForm.RCAGID_A,
          //   RCAGGender_1: this.editForm.RCAGGender_1,
          //   RCAGGender_2: this.editForm.RCAGGender_2,
          //   RCAGBirthday: this.editForm.RCAGBirthday,
          //   RCAGAdd: this.editForm.RCAGAdd,
          //   RCAGAdd_1_A: this.editForm.RCAGAdd_1_A,
          //   RCAGAdd_1_1_A: this.editForm.RCAGAdd_1_1_A,
          //   RCAGAdd_1_2_A: this.editForm.RCAGAdd_1_2_A,
          //   RCAGAdd_2_A: this.editForm.RCAGAdd_2_A,
          //   RCAGAdd_2_1_A: this.editForm.RCAGAdd_2_1_A,
          //   RCAGAdd_2_2_A: this.editForm.RCAGAdd_2_2_A,
          //   RCAGAdd_2_3_A: this.editForm.RCAGAdd_2_3_A,
          //   RCAGAdd_2_4_A: this.editForm.RCAGAdd_2_4_A,
          //   RCAGAdd_3_A: this.editForm.RCAGAdd_3_A,
          //   RCAGAdd_3_1_A: this.editForm.RCAGAdd_3_1_A,
          //   RCAGAdd_3_2_A: this.editForm.RCAGAdd_3_2_A,
          //   RCAGAdd_4_A: this.editForm.RCAGAdd_4_A,
          //   RCAGAdd_5_A: this.editForm.RCAGAdd_5_A,
          //   RCAGAdd_6_A: this.editForm.RCAGAdd_6_A,
          //   RCAGAdd_7_A: this.editForm.RCAGAdd_7_A,
          //   RCAGAdd_8_A: this.editForm.RCAGAdd_8_A,
          //   RCAGAdd_9_A: this.editForm.RCAGAdd_9_A,
          //   RCAGAddSame: this.editForm.RCAGAddSame,
          //   RCAGAddC: this.editForm.RCAGAddC,
          //   RCAGAddC_1_A: this.editForm.RCAGAddC_1_A,
          //   RCAGAddC_1_1_A: this.editForm.RCAGAddC_1_1_A,
          //   RCAGAddC_1_2_A: this.editForm.RCAGAddC_1_2_A,
          //   RCAGAddC_2_A: this.editForm.RCAGAddC_2_A,
          //   RCAGAddC_2_1_A: this.editForm.RCAGAddC_2_1_A,
          //   RCAGAddC_2_2_A: this.editForm.RCAGAddC_2_2_A,
          //   RCAGAddC_2_3_A: this.editForm.RCAGAddC_2_3_A,
          //   RCAGAddC_2_4_A: this.editForm.RCAGAddC_2_4_A,
          //   RCAGAddC_3_A: this.editForm.RCAGAddC_3_A,
          //   RCAGAddC_3_1_A: this.editForm.RCAGAddC_3_1_A,
          //   RCAGAddC_3_2_A: this.editForm.RCAGAddC_3_2_A,
          //   RCAGAddC_4_A: this.editForm.RCAGAddC_4_A,
          //   RCAGAddC_5_A: this.editForm.RCAGAddC_5_A,
          //   RCAGAddC_6_A: this.editForm.RCAGAddC_6_A,
          //   RCAGAddC_7_A: this.editForm.RCAGAddC_7_A,
          //   RCAGAddC_8_A: this.editForm.RCAGAddC_8_A,
          //   RCAGAddC_9_A: this.editForm.RCAGAddC_9_A,
          //   RCAGCell_A: this.editForm.RCAGCell_A,
          //   RCAGTel_1_A: this.editForm.RCAGTel_1_A,
          //   RCAGTel_2_A: this.editForm.RCAGTel_2_A,
          //   RCGName: this.editForm.RCGName,
          //   RCGID: this.editForm.RCGID,
          //   RCGGender_1: this.editForm.RCGGender_1,
          //   RCGGender_2: this.editForm.RCGGender_2,
          //   RCGBirthday: this.editForm.RCGBirthday,
          //   RCGAdd: this.editForm.RCGAdd,
          //   RCGAdd_1: this.editForm.RCGAdd_1,
          //   RCGAdd_1_1: this.editForm.RCGAdd_1_1,
          //   RCGAdd_1_2: this.editForm.RCGAdd_1_2,
          //   RCGAdd_2: this.editForm.RCGAdd_2,
          //   RCGAdd_2_1: this.editForm.RCGAdd_2_1,
          //   RCGAdd_2_2: this.editForm.RCGAdd_2_2,
          //   RCGAdd_2_3: this.editForm.RCGAdd_2_3,
          //   RCGAdd_2_4: this.editForm.RCGAdd_2_4,
          //   RCGAdd_3: this.editForm.RCGAdd_3,
          //   RCGAdd_3_1: this.editForm.RCGAdd_3_1,
          //   RCGAdd_3_2: this.editForm.RCGAdd_3_2,
          //   RCGAdd_4: this.editForm.RCGAdd_4,
          //   RCGAdd_5: this.editForm.RCGAdd_5,
          //   RCGAdd_6: this.editForm.RCGAdd_6,
          //   RCGAdd_7: this.editForm.RCGAdd_7,
          //   RCGAdd_8: this.editForm.RCGAdd_8,
          //   RCGAdd_9: this.editForm.RCGAdd_9,
          //   RCGAddSame: this.editForm.RCGAddSame,
          //   RCGCAdd: this.editForm.RCGCAdd,
          //   RCGCAdd_1: this.editForm.RCGCAdd_1,
          //   RCGCAdd_1_1: this.editForm.RCGCAdd_1_1,
          //   RCGCAdd_1_2: this.editForm.RCGCAdd_1_2,
          //   RCGCAdd_2: this.editForm.RCGCAdd_2,
          //   RCGCAdd_2_1: this.editForm.RCGCAdd_2_1,
          //   RCGCAdd_2_2: this.editForm.RCGCAdd_2_2,
          //   RCGCAdd_2_3: this.editForm.RCGCAdd_2_3,
          //   RCGCAdd_2_4: this.editForm.RCGCAdd_2_4,
          //   RCGCAdd_3: this.editForm.RCGCAdd_3,
          //   RCGCAdd_3_1: this.editForm.RCGCAdd_3_1,
          //   RCGCAdd_3_2: this.editForm.RCGCAdd_3_2,
          //   RCGCAdd_4: this.editForm.RCGCAdd_4,
          //   RCGCAdd_5: this.editForm.RCGCAdd_5,
          //   RCGCAdd_6: this.editForm.RCGCAdd_6,
          //   RCGCAdd_7: this.editForm.RCGCAdd_7,
          //   RCGCAdd_8: this.editForm.RCGCAdd_8,
          //   RCGCAdd_9: this.editForm.RCGCAdd_9,
          //   RCGCell: this.editForm.RCGCell,

          //   Id: this.currentId
          // };
          const data = this.editForm;
          var url = 'CustomerRC/Insert';
          if (this.currentId !== '') {
            url = 'CustomerRC/Update';
          }
          saveCustomerRC(data, url)
            .then((res) => {
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
      this.loadTableData();
      this.InitDictItem();
      this.editForm = {};
      this.sendedform = {};
      this.sendedform1 = {};
      this.sendedformEC = {};
      this.sendedformECC = {};
      this.sendedformECF = {};
      this.sendedformECFC = {};
      this.sendedformAG = {};
      this.sendedformAGC = {};
      this.sendedformG = {};
      this.sendedformGC = {};
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
            that.currentSelected.forEach((element) => {
              currentIds.push(element.Id);
            });
            const data = {
              Ids: currentIds
            };
            return deleteCustomerRC(data);
          })
          .then((res) => {
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
    handleSelectChange: function(selection, row) {
      this.currentSelected = selection;
    },
    handleSelectAllChange: function(selection) {
      this.currentSelected = selection;
    },
    handleSizeChange(val) {
      this.pagination.pagesize = val;
      this.pagination.currentPage = 1;
      this.loadTableData();
    },
    handleCurrentChange(val) {
      this.pagination.currentPage = val;
      this.loadTableData();
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
    geteditaddress(addressData, title) {
      if (title === '公司登記地址') {
        this.editForm.RCAdd_1 = addressData.Add_1;
        this.editForm.RCAdd_1_1 = addressData.Add_1_1;
        this.editForm.RCAdd_1_2 = addressData.Add_1_2;
        this.editForm.RCAdd_2 = addressData.Add_2;
        this.editForm.RCAdd_2_1 = addressData.Add_2_1;
        this.editForm.RCAdd_2_2 = addressData.Add_2_2;
        this.editForm.RCAdd_2_3 = addressData.Add_2_3;
        this.editForm.RCAdd_2_4 = addressData.Add_2_4;
        this.editForm.RCAdd_3 = addressData.Add_3;
        this.editForm.RCAdd_3_1 = addressData.Add_3_1;
        this.editForm.RCAdd_3_2 = addressData.Add_3_2;
        this.editForm.RCAdd_4 = addressData.Add_4;
        this.editForm.RCAdd_5 = addressData.Add_5;
        this.editForm.RCAdd_6 = addressData.Add_6;
        this.editForm.RCAdd_7 = addressData.Add_7;
        this.editForm.RCAdd_8 = addressData.Add_8;
        this.editForm.RCAdd_9 = addressData.Add_9;
      }
      if (title === '公司營業地址') {
        this.editForm.RCAddC_1 = addressData.Add_1;
        this.editForm.RCAddC_1_1 = addressData.Add_1_1;
        this.editForm.RCAddC_1_2 = addressData.Add_1_2;
        this.editForm.RCAddC_2 = addressData.Add_2;
        this.editForm.RCAddC_2_1 = addressData.Add_2_1;
        this.editForm.RCAddC_2_2 = addressData.Add_2_2;
        this.editForm.RCAddC_2_3 = addressData.Add_2_3;
        this.editForm.RCAddC_2_4 = addressData.Add_2_4;
        this.editForm.RCAddC_3 = addressData.Add_3;
        this.editForm.RCAddC_3_1 = addressData.Add_3_1;
        this.editForm.RCAddC_3_2 = addressData.Add_3_2;
        this.editForm.RCAddC_4 = addressData.Add_4;
        this.editForm.RCAddC_5 = addressData.Add_5;
        this.editForm.RCAddC_6 = addressData.Add_6;
        this.editForm.RCAddC_7 = addressData.Add_7;
        this.editForm.RCAddC_8 = addressData.Add_8;
        this.editForm.RCAddC_9 = addressData.Add_9;
      }
      if (title === '緊急1戶籍地址') {
        this.editForm.RCECAdd_1 = addressData.Add_1;
        this.editForm.RCECAdd_1_1 = addressData.Add_1_1;
        this.editForm.RCECAdd_1_2 = addressData.Add_1_2;
        this.editForm.RCECAdd_2 = addressData.Add_2;
        this.editForm.RCECAdd_2_1 = addressData.Add_2_1;
        this.editForm.RCECAdd_2_2 = addressData.Add_2_2;
        this.editForm.RCECAdd_2_3 = addressData.Add_2_3;
        this.editForm.RCECAdd_2_4 = addressData.Add_2_4;
        this.editForm.RCECAdd_3 = addressData.Add_3;
        this.editForm.RCECAdd_3_1 = addressData.Add_3_1;
        this.editForm.RCECAdd_3_2 = addressData.Add_3_2;
        this.editForm.RCECAdd_4 = addressData.Add_4;
        this.editForm.RCECAdd_5 = addressData.Add_5;
        this.editForm.RCECAdd_6 = addressData.Add_6;
        this.editForm.RCECAdd_7 = addressData.Add_7;
        this.editForm.RCECAdd_8 = addressData.Add_8;
        this.editForm.RCECAdd_9 = addressData.Add_9;
      }
      if (title === '緊急1通訊地址') {
        this.editForm.RCECAddC_1 = addressData.Add_1;
        this.editForm.RCECAddC_1_1 = addressData.Add_1_1;
        this.editForm.RCECAddC_1_2 = addressData.Add_1_2;
        this.editForm.RCECAddC_2 = addressData.Add_2;
        this.editForm.RCECAddC_2_1 = addressData.Add_2_1;
        this.editForm.RCECAddC_2_2 = addressData.Add_2_2;
        this.editForm.RCECAddC_2_3 = addressData.Add_2_3;
        this.editForm.RCECAddC_2_4 = addressData.Add_2_4;
        this.editForm.RCECAddC_3 = addressData.Add_3;
        this.editForm.RCECAddC_3_1 = addressData.Add_3_1;
        this.editForm.RCECAddC_3_2 = addressData.Add_3_2;
        this.editForm.RCECAddC_4 = addressData.Add_4;
        this.editForm.RCECAddC_5 = addressData.Add_5;
        this.editForm.RCECAddC_6 = addressData.Add_6;
        this.editForm.RCECAddC_7 = addressData.Add_7;
        this.editForm.RCECAddC_8 = addressData.Add_8;
        this.editForm.RCECAddC_9 = addressData.Add_9;
      }
      if (title === '緊急2戶籍地址') {
        this.editForm.RCECFAdd_1 = addressData.Add_1;
        this.editForm.RCECFAdd_1_1 = addressData.Add_1_1;
        this.editForm.RCECFAdd_1_2 = addressData.Add_1_2;
        this.editForm.RCECFAdd_2 = addressData.Add_2;
        this.editForm.RCECFAdd_2_1 = addressData.Add_2_1;
        this.editForm.RCECFAdd_2_2 = addressData.Add_2_2;
        this.editForm.RCECFAdd_2_3 = addressData.Add_2_3;
        this.editForm.RCECFAdd_2_4 = addressData.Add_2_4;
        this.editForm.RCECFAdd_3 = addressData.Add_3;
        this.editForm.RCECFAdd_3_1 = addressData.Add_3_1;
        this.editForm.RCECFAdd_3_2 = addressData.Add_3_2;
        this.editForm.RCECFAdd_4 = addressData.Add_4;
        this.editForm.RCECFAdd_5 = addressData.Add_5;
        this.editForm.RCECFAdd_6 = addressData.Add_6;
        this.editForm.RCECFAdd_7 = addressData.Add_7;
        this.editForm.RCECFAdd_8 = addressData.Add_8;
        this.editForm.RCECFAdd_9 = addressData.Add_9;
      }
      if (title === '緊急2通訊地址') {
        this.editForm.RCECFAddC_1 = addressData.Add_1;
        this.editForm.RCECFAddC_1_1 = addressData.Add_1_1;
        this.editForm.RCECFAddC_1_2 = addressData.Add_1_2;
        this.editForm.RCECFAddC_2 = addressData.Add_2;
        this.editForm.RCECFAddC_2_1 = addressData.Add_2_1;
        this.editForm.RCECFAddC_2_2 = addressData.Add_2_2;
        this.editForm.RCECFAddC_2_3 = addressData.Add_2_3;
        this.editForm.RCECFAddC_2_4 = addressData.Add_2_4;
        this.editForm.RCECFAddC_3 = addressData.Add_3;
        this.editForm.RCECFAddC_3_1 = addressData.Add_3_1;
        this.editForm.RCECFAddC_3_2 = addressData.Add_3_2;
        this.editForm.RCECFAddC_4 = addressData.Add_4;
        this.editForm.RCECFAddC_5 = addressData.Add_5;
        this.editForm.RCECFAddC_6 = addressData.Add_6;
        this.editForm.RCECFAddC_7 = addressData.Add_7;
        this.editForm.RCECFAddC_8 = addressData.Add_8;
        this.editForm.RCECFAddC_9 = addressData.Add_9;
      }
      if (title === '代理戶籍地址') {
        this.editForm.RCAGAdd_1 = addressData.Add_1;
        this.editForm.RCAGAdd_1_1 = addressData.Add_1_1;
        this.editForm.RCAGAdd_1_2 = addressData.Add_1_2;
        this.editForm.RCAGAdd_2 = addressData.Add_2;
        this.editForm.RCAGAdd_2_1 = addressData.Add_2_1;
        this.editForm.RCAGAdd_2_2 = addressData.Add_2_2;
        this.editForm.RCAGAdd_2_3 = addressData.Add_2_3;
        this.editForm.RCAGAdd_2_4 = addressData.Add_2_4;
        this.editForm.RCAGAdd_3 = addressData.Add_3;
        this.editForm.RCAGAdd_3_1 = addressData.Add_3_1;
        this.editForm.RCAGAdd_3_2 = addressData.Add_3_2;
        this.editForm.RCAGAdd_4 = addressData.Add_4;
        this.editForm.RCAGAdd_5 = addressData.Add_5;
        this.editForm.RCAGAdd_6 = addressData.Add_6;
        this.editForm.RCAGAdd_7 = addressData.Add_7;
        this.editForm.RCAGAdd_8 = addressData.Add_8;
        this.editForm.RCAGAdd_9 = addressData.Add_9;
      }
      if (title === '代理通訊地址') {
        this.editForm.RCAGAddC_1 = addressData.Add_1;
        this.editForm.RCAGAddC_1_1 = addressData.Add_1_1;
        this.editForm.RCAGAddC_1_2 = addressData.Add_1_2;
        this.editForm.RCAGAddC_2 = addressData.Add_2;
        this.editForm.RCAGAddC_2_1 = addressData.Add_2_1;
        this.editForm.RCAGAddC_2_2 = addressData.Add_2_2;
        this.editForm.RCAGAddC_2_3 = addressData.Add_2_3;
        this.editForm.RCAGAddC_2_4 = addressData.Add_2_4;
        this.editForm.RCAGAddC_3 = addressData.Add_3;
        this.editForm.RCAGAddC_3_1 = addressData.Add_3_1;
        this.editForm.RCAGAddC_3_2 = addressData.Add_3_2;
        this.editForm.RCAGAddC_4 = addressData.Add_4;
        this.editForm.RCAGAddC_5 = addressData.Add_5;
        this.editForm.RCAGAddC_6 = addressData.Add_6;
        this.editForm.RCAGAddC_7 = addressData.Add_7;
        this.editForm.RCAGAddC_8 = addressData.Add_8;
        this.editForm.RCAGAddC_9 = addressData.Add_9;
      }
      if (title === '保證戶籍地址') {
        this.editForm.RCGAdd_1 = addressData.Add_1;
        this.editForm.RCGAdd_1_1 = addressData.Add_1_1;
        this.editForm.RCGAdd_1_2 = addressData.Add_1_2;
        this.editForm.RCGAdd_2 = addressData.Add_2;
        this.editForm.RCGAdd_2_1 = addressData.Add_2_1;
        this.editForm.RCGAdd_2_2 = addressData.Add_2_2;
        this.editForm.RCGAdd_2_3 = addressData.Add_2_3;
        this.editForm.RCGAdd_2_4 = addressData.Add_2_4;
        this.editForm.RCGAdd_3 = addressData.Add_3;
        this.editForm.RCGAdd_3_1 = addressData.Add_3_1;
        this.editForm.RCGAdd_3_2 = addressData.Add_3_2;
        this.editForm.RCGAdd_4 = addressData.Add_4;
        this.editForm.RCGAdd_5 = addressData.Add_5;
        this.editForm.RCGAdd_6 = addressData.Add_6;
        this.editForm.RCGAdd_7 = addressData.Add_7;
        this.editForm.RCGAdd_8 = addressData.Add_8;
        this.editForm.RCGAdd_9 = addressData.Add_9;
      }
      if (title === '保證通訊地址') {
        this.editForm.RCGCAdd_1 = addressData.Add_1;
        this.editForm.RCGCAdd_1_1 = addressData.Add_1_1;
        this.editForm.RCGCAdd_1_2 = addressData.Add_1_2;
        this.editForm.RCGCAdd_2 = addressData.Add_2;
        this.editForm.RCGCAdd_2_1 = addressData.Add_2_1;
        this.editForm.RCGCAdd_2_2 = addressData.Add_2_2;
        this.editForm.RCGCAdd_2_3 = addressData.Add_2_3;
        this.editForm.RCGCAdd_2_4 = addressData.Add_2_4;
        this.editForm.RCGCAdd_3 = addressData.Add_3;
        this.editForm.RCGCAdd_3_1 = addressData.Add_3_1;
        this.editForm.RCGCAdd_3_2 = addressData.Add_3_2;
        this.editForm.RCGCAdd_4 = addressData.Add_4;
        this.editForm.RCGCAdd_5 = addressData.Add_5;
        this.editForm.RCGCAdd_6 = addressData.Add_6;
        this.editForm.RCGCAdd_7 = addressData.Add_7;
        this.editForm.RCGCAdd_8 = addressData.Add_8;
        this.editForm.RCGCAdd_9 = addressData.Add_9;
      }
      // if (title === '代A地址') {
      //   this.editForm.RCAGAdd_1_A = addressData.Add_1;
      //   this.editForm.RCAGAdd_1_1_A = addressData.Add_1_1;
      //   this.editForm.RCAGAdd_1_2_A = addressData.Add_1_2;
      //   this.editForm.RCAGAdd_2_A = addressData.Add_2;
      //   this.editForm.RCAGAdd_2_1_A = addressData.Add_2_1;
      //   this.editForm.RCAGAdd_2_2_A = addressData.Add_2_2;
      //   this.editForm.RCAGAdd_2_3_A = addressData.Add_2_3;
      //   this.editForm.RCAGAdd_2_4_A = addressData.Add_2_4;
      //   this.editForm.RCAGAdd_3_A = addressData.Add_3;
      //   this.editForm.RCAGAdd_3_1_A = addressData.Add_3_1;
      //   this.editForm.RCAGAdd_3_2_A = addressData.Add_3_2;
      //   this.editForm.RCAGAdd_4_A = addressData.Add_4;
      //   this.editForm.RCAGAdd_5_A = addressData.Add_5;
      //   this.editForm.RCAGAdd_6_A = addressData.Add_6;
      //   this.editForm.RCAGAdd_7_A = addressData.Add_7;
      //   this.editForm.RCAGAdd_8_A = addressData.Add_8;
      //   this.editForm.RCAGAdd_9_A = addressData.Add_9;
      // }
      // if (title === '代B地址') {
      //   this.editForm.RCAGAdd_1_B = addressData.Add_1;
      //   this.editForm.RCAGAdd_1_1_B = addressData.Add_1_1;
      //   this.editForm.RCAGAdd_1_2_B = addressData.Add_1_2;
      //   this.editForm.RCAGAdd_2_B = addressData.Add_2;
      //   this.editForm.RCAGAdd_2_1_B = addressData.Add_2_1;
      //   this.editForm.RCAGAdd_2_2_B = addressData.Add_2_2;
      //   this.editForm.RCAGAdd_2_3_B = addressData.Add_2_3;
      //   this.editForm.RCAGAdd_2_4_B = addressData.Add_2_4;
      //   this.editForm.RCAGAdd_3_B = addressData.Add_3;
      //   this.editForm.RCAGAdd_3_1_B = addressData.Add_3_1;
      //   this.editForm.RCAGAdd_3_2_B = addressData.Add_3_2;
      //   this.editForm.RCAGAdd_4_B = addressData.Add_4;
      //   this.editForm.RCAGAdd_5_B = addressData.Add_5;
      //   this.editForm.RCAGAdd_6_B = addressData.Add_6;
      //   this.editForm.RCAGAdd_7_B = addressData.Add_7;
      //   this.editForm.RCAGAdd_8_B = addressData.Add_8;
      //   this.editForm.RCAGAdd_9_B = addressData.Add_9;
      // }
      // if (title === '新建地址') {
      //   this.sendedformCBB.BAdd_1 = addressData.Add_1;
      //   this.sendedformCBB.BAdd_1_1 = addressData.Add_1_1;
      //   this.sendedformCBB.BAdd_1_2 = addressData.Add_1_2;
      //   this.sendedformCBB.BAdd_2 = addressData.Add_2;
      //   this.sendedformCBB.BAdd_2_1 = addressData.Add_2_1;
      //   this.sendedformCBB.BAdd_2_2 = addressData.Add_2_2;
      //   this.sendedformCBB.BAdd_2_3 = addressData.Add_2_3;
      //   this.sendedformCBB.BAdd_2_4 = addressData.Add_2_4;
      //   this.sendedformCBB.BAdd_3 = addressData.Add_3;
      //   this.sendedformCBB.BAdd_3_1 = addressData.Add_3_1;
      //   this.sendedformCBB.BAdd_3_2 = addressData.Add_3_2;
      //   this.sendedformCBB.BAdd_4 = addressData.Add_4;
      //   this.sendedformCBB.BAdd_5 = addressData.Add_5;
      //   this.sendedformCBB.BAdd_6 = addressData.Add_6;
      //   this.sendedformCBB.BAdd_7 = addressData.Add_7;
      //   this.sendedformCBB.BAdd_8 = addressData.Add_8;
      //   this.sendedformCBB.BAdd_9 = addressData.Add_9;
      // }
    }
  }
};
</script>

<style></style>
