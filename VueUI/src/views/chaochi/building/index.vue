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
          <!-- <el-form-item label="客戶姓名：">
            <el-input
              v-model="searchform.LName"
              clearable
            />
          </el-form-item> -->
          <el-form-item label="身分證字號/居留證號/統一編號：">
            <el-input
              v-model="searchform.BelongLID"
              v-upperCase clearable
            />
          </el-form-item>
          <!-- <el-form-item label="統一編號：">
            <el-input
              v-model="searchform.LCID"
              clearable
            />
          </el-form-item> -->
          <!-- <el-form-item label="區域：">
            <el-input
              v-model="searchform.BAdd_1"
              clearable
            />
          </el-form-item> -->
          <!-- <el-form-item label="管理人員：">
            <el-input
              v-model="searchform.Keywords"
              clearable
            />
          </el-form-item> -->
          <el-form-item label="歸屬業務：">
            <!-- <el-input
              v-model="searchform.CreatorUserId"
              clearable
            /> -->
            <el-select
              v-model="searchform.CreatorUserId"
              placeholder="請選擇"
              clearable
            >
              <el-option
                label="等待指派"
                value="等待指派"
              />
              <el-option
                v-for="item in Sales"
                :key="item.Account"
                :label="item.RealName"
                :value="item.Account"
              />
            </el-select>
          </el-form-item>
          <el-row>
            <el-form-item label="建物門牌地址：">
              <el-input
                v-model="searchform.BAdd"
                clearable
              />
              <!-- <Address
                title="建物門牌地址："
                @getsearchaddress="getsearchaddress"
              /> -->
            </el-form-item>
            <el-form-item label="建物狀態：">
              <el-select
                v-model="searchform.BState"
                placeholder="請選擇"
                clearable
                style="width: 100px"
              >
                <el-option
                  v-for="item in BState"
                  :key="item.value"
                  :label="item.label"
                  :value="item.value"
                />
              </el-select>
            </el-form-item>
          </el-row>
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
          <!-- <el-button
            v-hasPermi="['Building/Add']"
            type="primary"
            icon="el-icon-plus"
            size="small"
            @click="ShowEditOrViewDialog()"
          >新增</el-button> -->
          <el-button
            v-hasPermi="['Building/Edit']"
            type="primary"
            icon="el-icon-edit"
            class="el-button-modify"
            size="small"
            @click="ShowEditOrViewDialog('edit')"
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
            @click="loadTableData()"
          >更新</el-button>
          <el-button
            v-hasPermi="['Building/UpdateBuildingBelonging']"
            type="warning"
            icon="el-icon-sort"
            size="small"
            @click="BelongTransfer()"
          >所屬權移轉</el-button>
          <!-- <el-button
            v-hasPermi="['Building/RemoveBuildingBelonging']"
            type="warning"
            icon="el-icon-document-delete"
            size="small"
            @click="BelongRemove()"
          >所屬權移除</el-button> -->
        </el-button-group>
      </div>
      <el-table
        :key="rerenderGridtable"
        ref="gridtable"
        v-loading="tableloading"
        class="tableClass"
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
          prop="BAdd"
          label="建物門牌地址"
          sortable="custom"
        />
        <el-table-column
          prop="BState"
          label="建物狀態"
          sortable="custom"
          width="110"
          align="center"
        />
        <el-table-column
          prop="BPropoty"
          label="類型"
          sortable="custom"
          width="100"
          align="center"
        />
        <el-table-column
          prop="BRealPING"
          label="權狀面積(坪數)"
          sortable="custom"
          width="160"
        />
        <el-table-column
          prop="CompletionDate"
          label="房屋格局"
          width="110"
          sortable
        >
          <template slot-scope="scope">
            {{
              scope.row.BPatten1S === '/Yes'
                ? '套房'
                : scope.row.BBedsit === '/Yes'
                  ? '雅房'
                  : scope.row.BPatten1R === '/Yes'
                    ? '1房'
                    : scope.row.BPatten2R === '/Yes'
                      ? '2房'
                      : scope.row.BPatten3Rup === '/Yes'
                        ? '3房以上'
                        : scope.row.BPatten4Rup === '/Yes'
                          ? '4房以上'
                          : scope.row.BOpenspace === '/Yes'
                            ? '開放式空間'
                            : ''
            }}
          </template>
        </el-table-column>
        <el-table-column
          prop="BTCRent"
          label="合約租金"
          sortable="custom"
          width="110"
        />
        <el-table-column
          prop="BMFee"
          label="管理費"
          sortable="custom"
          width="110"
        />
        <el-table-column
          prop="CreatorUserName"
          label="歸屬業務"
          sortable="custom"
          width="160"
        />
        <!-- <el-table-column
          prop="Gender"
          label="管理人員"
          sortable="custom"
          width="120"
          align="center"
        >
          <template slot-scope="scope">
            {{ scope.row.Gender=== 1 ? 'GIGI' : 'AAA' }}
          </template>
        </el-table-column> -->
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
      fullscreen
      :close-on-click-modal="false"
    >
      <el-tabs
        v-model="ActionName"
        v-loading="loading"
        v-loading.lock="loading"
        element-loading-text="載入中..."
        type="border-card"
      >
        <el-tab-pane
          label="建物所有權"
          name="A"
        >
          <el-card class="box-card">
            <BuildingBasic
              :editform="editFrom"
              :clearflag="clearFlag"
              @cancel="closeDialogAndRefresh()"
            />
          </el-card>
        </el-tab-pane>
        <el-tab-pane
          label="建物標示"
          name="B"
        >
          <el-card class="box-card">
            <BuildingRentBasic
              :editform="buildingRentBasic"
              @cancel="closeDialogAndRefresh()"
              @emitbpingfunc="emitbpingFunc"
            />
          </el-card>
        </el-tab-pane>
        <el-tab-pane
          label="建物現況"
          name="S"
        >
          <el-card class="box-card">
            <BuildingSituation
              :editform="buildingSituation"
              :emitbping="emitBPing"
              @cancel="closeDialogAndRefresh()"
            />
          </el-card>
        </el-tab-pane>
        <el-tab-pane
          label="設備相關"
          name="C"
        >
          <el-card class="box-card">
            <Equipment
              :badd="editFrom.BAdd"
              @cancel="closeDialogAndRefresh()"
            />
            <!-- <el-tree ref="treeOrganize" :data="treeOrganizeData" :check-strictly="true" empty-text="加載中，請稍后" show-checkbox default-expand-all node-key="Id" highlight-current :props="{ label: 'FullName', children: 'Children' }" /> -->
          </el-card>
        </el-tab-pane>
        <el-tab-pane
          label="合約屋況照片"
          name="D"
        >
          <el-card class="box-card">
            <!-- <el-tree ref="treeOrganize" :data="treeOrganizeData" :check-strictly="true" empty-text="加載中，請稍后" show-checkbox default-expand-all node-key="Id" highlight-current :props="{ label: 'FullName', children: 'Children' }" /> -->
            <div class="list-btn-container">
              <el-button-group>
                <el-button
                  v-hasPermi="['Building/Add']"
                  type="primary"
                  icon="el-icon-plus"
                  size="small"
                  @click="ShowEditOrViewDialogD()"
                >新增</el-button>
                <el-button
                  type="primary"
                  icon="el-icon-tickets"
                  size="small"
                  @click="genPDF()"
                >產生PDF</el-button>
                <el-button
                  v-hasPermi="['BuildingImg/Delete']"
                  type="danger"
                  icon="el-icon-delete"
                  size="small"
                  @click="deletePhysicsD()"
                >刪除</el-button>
                <el-button
                  type="default"
                  icon="el-icon-refresh"
                  size="small"
                  @click="loadTableDataD()"
                >更新</el-button>
              </el-button-group>
              <span
                style="color: red"
              ><i><b>*</b>單次上傳張數最多25張</i></span>
            </div>
            <el-table
              ref="gridtableImg"
              v-loading="imageTableloading"
              :data="editFrom.BuildingImages"
              stripe
              highlight-current-row
              style="width: 320px"
              :default-sort="{ prop: 'SortCode', order: 'descending' }"
              @select="handleSelectChangeD"
              @select-all="handleSelectAllChangeD"
              @sort-change="handleSortChangeF"
            >
              <el-table-column
                type="selection"
                width="30"
              />
              <!-- <el-table-column
                prop="FileName"
                label="檔名"
                sortable="custom"
                fixed
              /> -->
              <el-table-column
                label="圖片"
                width="210px" align="center"
              >
                <template slot-scope="scope">
                  <div
                    style="width: 80px"
                    class="center"
                  >
                    <expandable-image
                      :src="
                        scope.row.ImgUrl == null ? '#/XX' : scope.row.ImgUrl
                      "
                      class="center"
                    />
                  </div>
                </template>
              </el-table-column>
              <el-table-column
                label=""
                width="80px"
              >
                <template slot-scope="scope">
                  <el-button
                    type="text"
                    size="small"
                    @click.prevent="
                      downloadItem(
                        scope.row.ImgUrl == null ? '#/XX' : scope.row.ImgUrl,
                        scope.row.FileName
                      )
                    "
                  >下載</el-button>
                  <!-- <a
                    :href="scope.row.ImgUrl==null?'XX':scope.row.ImgUrl"
                    @click.prevent="downloadItem(scope.row.ImgUrl==null?'#/XX':scope.row.ImgUrl,scope.row.FileName)"
                  ><i style="color:blue">下載</i></a> -->
                </template>
              </el-table-column>
            </el-table>
            <div class="pagination-container">
              <el-pagination
                background
                :current-page="paginationI.currentPage"
                :page-sizes="[5, 10, 20, 50]"
                :page-size="paginationI.pagesize"
                layout="total, sizes, prev, pager, next, jumper"
                :total="paginationI.pageTotal"
                @size-change="handleSizeChangeI"
                @current-change="handleCurrentChangeI"
              />
            </div>
            <div class="rightbtn">
              <el-button
                size="small"
                icon="el-icon-close"
                @click="closeDialogAndRefresh()"
              >關閉</el-button>
            </div>
          </el-card>
        </el-tab-pane>
        <el-tab-pane
          label="建物附件"
          name="H"
        >
          <el-card class="box-card">
            <BuildingFormHistory
              :id="currentId"
              ref="buildingformhistory"
              :refreshflag="refreshFlag"
              @cancel="closeDialogAndRefresh"
            />
          </el-card>
        </el-tab-pane>
      </el-tabs>
    </el-dialog>
    <el-dialog
      ref="dialogEditFormD"
      :title="editFormTitleD"
      :visible.sync="dialogEditFormVisibleD"
      width="480px"
    >
      <el-form
        v-loading="uploadloading"
        v-loading.lock="uploadloading"
        element-loading-text="壓縮並上傳中..."
        :inline="true"
        class="demo-form-inline"
      >
        <el-row>
          <h3>此建物還可以上傳{{ limitfilescount }}張圖檔</h3>
          <el-upload
            ref="newupload"
            drag
            :action="httpImgFileUploadUrl"
            multiple
            :limit="limitfilescount"
            show-file-list
            :auto-upload="false"
            accept=".png,.jpg,.gif"
            :file-list="fileList"
            :on-change="handleFileChange"
            :before-remove="handleFileRemove"
            :on-error="uploaderror"
            :on-exceed="onexceed"
            :before-upload="beforeupload"
          >
            <em class="el-icon-upload" />
            <div class="el-upload__text">
              將文件拖到此處，或<em>點擊上傳</em>
            </div>
            <div
              slot="tip"
              class="el-upload__tip"
            >
              請註意您只能上傳.png,.jpg,.gif格式
            </div>
          </el-upload>
        </el-row>
      </el-form>
      <div
        slot="footer"
        class="dialog-footer"
      >
        <el-button
          size="small"
          icon="el-icon-close"
          @click="dialogEditFormVisibleD = false"
        >關閉</el-button>
        <el-button
          type="primary"
          size="small"
          icon="el-icon-upload"
          @click="ConfirmUpload()"
        >上傳</el-button>
      </div>
    </el-dialog>
    <el-dialog
      ref="dialogEditFormF"
      :title="editFormTitleF"
      :visible.sync="dialogEditFormVisibleF"
      width="880px"
    >
      <el-form
        ref="editFromF"
        :inline="true"
        :model="editFromF"
        class="demo-form-inline"
      >
        <el-row>
          <el-col :span="12">
            <el-form-item
              label="房東姓名"
              :label-width="formLabelWidth"
            >
              <el-input v-model="editFromF.LandlordRealName" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item
              label="房東身分證號"
              :label-width="formLabelWidth"
            >
              <el-input v-model="editFromF.LandlordAccount" />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-form-item
            label="持份"
            :label-width="formLabelWidth"
          >
            <el-input v-model="editFromF.HoldingsNumerator" />
          </el-form-item>
          <el-form-item label="/">
            <el-input v-model="editFromF.HoldingsDenominator" />
          </el-form-item>
        </el-row>
        <el-row>
          <el-form-item
            label="匯款帳戶"
            :label-width="formLabelWidth"
          >
            <el-input v-model="editFromF.RemittanceBank" />
          </el-form-item>
          <el-form-item label="-">
            <el-input v-model="editFromF.RemittanceAccount" />
          </el-form-item>
        </el-row>
      </el-form>
      <div
        slot="footer"
        class="dialog-footer"
      >
        <el-button @click="dialogEditFormVisibleF = false">關 閉</el-button>
        <el-button
          v-if="editFormTitleF == '新增'"
          type="primary"
          @click="saveEditFormF()"
        >新增確認</el-button>
      </div>
    </el-dialog>
    <el-dialog
      ref="dialogEditFormG"
      :title="editFormTitleG"
      :visible.sync="dialogEditFormVisibleG"
      width="880px"
    >
      <el-form
        ref="editFromG"
        :inline="true"
        :model="editFromG"
        class="demo-form-inline"
      >
        <el-row>
          <el-col :span="12">
            <el-form-item
              label="帳戶名稱"
              :label-width="formLabelWidth"
            >
              <el-input v-model="editFromG.AName" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item
              label="身份證字號"
              :label-width="formLabelWidth"
            >
              <el-input v-model="editFromG.AID" />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="12">
            <el-form-item
              label="金融機構名稱"
              :label-width="formLabelWidth"
            >
              <el-input v-model="editFromG.BankName" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item
              label="金融機構代碼"
              :label-width="formLabelWidth"
            >
              <el-input v-model="editFromG.BankNo" />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="12">
            <el-form-item
              label="分行名稱"
              :label-width="formLabelWidth"
            >
              <el-input v-model="editFromG.BranchName" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item
              label="分行代碼"
              :label-width="formLabelWidth"
            >
              <el-input v-model="editFromG.BranchNo" />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="24">
            <el-form-item
              label="帳戶號碼"
              :label-width="formLabelWidth"
            >
              <el-input v-model="editFromG.ANo" />
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
      <div
        slot="footer"
        class="dialog-footer"
      >
        <el-button @click="dialogEditFormVisibleG = false">關 閉</el-button>
        <el-button
          v-if="editFormTitleG == '新增'"
          type="primary"
          @click="saveEditFormG()"
        >新增確認</el-button>
      </div>
    </el-dialog>
    <el-dialog
      ref="dialogBelong"
      v-el-drag-dialog
      title="建物所屬權移轉"
      :visible.sync="dialogBelongVisible"
      width="780px"
    >
      <div style="color: #e24e4e">
        <i><b>*</b>建物所屬權移轉至其他店時，原服務業務人員亦將失去所有權！</i>
      </div>
      <div style="color: #e24e4e; padding-bottom: 30px">
        <i><b>*</b>建物所屬出租人及服務業務人員，亦將由其他店長指定。</i>
      </div>
      <el-form
        ref="chbelonging"
        v-loading="belongloading"
        v-loading.lock="belongloading"
        element-loading-text="載入中..."
        :model="chbelonging"
        :rules="belongrule"
      >
        <el-form-item
          label-width="120px"
          label="指定店："
        >
          <el-radio
            v-model="belongStore"
            label="1"
          >留在本店</el-radio>
          <el-radio
            v-model="belongStore"
            label="2"
          >重新指定店</el-radio>
        </el-form-item>
        <el-form-item
          v-if="belongStore === '2'"
          label-width="120px"
          label="移轉目標店："
        >
          <el-cascader
            v-model="chbelonging.destDept"
            style="width: 600px"
            :options="selectOrganize"
            filterable
            :props="{
              label: 'FullName',
              value: 'Id',
              children: 'Children',
              emitPath: false,
              checkStrictly: true,
              expandTrigger: 'hover'
            }"
            clearable
            placeholder="請選擇移轉目標店"
            @change="handleSelectOrganizeChange"
          />
        </el-form-item>
        <el-form-item
          v-if="belongStore === '1'"
          label-width="120px"
          label="指定業務："
          prop="destUserId"
        >
          <el-select
            v-model="chbelonging.destUserId"
            filterable
            style="width: 200px"
          >
            <el-option
              label="不指定"
              value="不指定"
            >不指定</el-option>
            <el-option
              v-for="item in userOptions"
              :key="item.Id"
              :label="item.RealName"
              :value="item.Id"
            >
              <span style="float: left">{{ item.RealName }}</span>
              <span style="float: right; color: #8492a6; font-size: 13px">{{
                item.Account
              }}</span>
            </el-option>
          </el-select>
        </el-form-item>

        <template
          v-if="chbelonging.destUserId && chbelonging.destUserId !== '不指定'"
        >
          <el-form-item
            label-width="120px"
            label="重新指定出租人"
          >
            <el-checkbox v-model="respecific">是</el-checkbox>
          </el-form-item>
        </template>
        <template v-if="respecific">
          <el-form-item
            label-width="120px"
            label="指定出租人："
          >
            <el-radio
              v-model="belongL"
              label="1"
            >自然人</el-radio>
            <el-radio
              v-model="belongL"
              label="2"
            >法人</el-radio>
          </el-form-item>
          <el-form-item
            v-if="belongL === '1'"
            label-width="120px"
            label="出租自然人："
            prop="destLandlord"
          >
            <el-select
              v-model="chbelonging.destLandlord"
              filterable
              style="width: 200px"
            >
              <el-option
                v-for="item in LNOptions"
                :key="item.Id"
                :label="item.LNName"
                :value="item.LNID"
              >
                <span style="float: left">
                  <span
                    v-if="currentBuildingBelongLID === item.LNID"
                    style="color: red"
                  >(原) </span>{{ item.LNName }}</span>
                <span style="float: right; color: #8492a6; font-size: 13px">{{
                  item.LNID
                }}</span>
              </el-option>
            </el-select>
          </el-form-item>

          <el-form-item
            v-if="belongL === '2'"
            label-width="120px"
            label="出租法人："
            prop="destLandlord"
          >
            <el-select
              v-model="chbelonging.destLandlord"
              filterable
              style="width: 200px"
            >
              <el-option
                v-for="item in LCOptions"
                :key="item.Id"
                :label="item.LCName"
                :value="item.LCID"
              >
                <span style="float: left">
                  <span
                    v-if="currentBuildingBelongLID === item.LCID"
                    style="color: red"
                  >(原) </span>{{ item.LCName }}</span>
                <span style="float: right; color: #8492a6; font-size: 13px">{{
                  item.LCID
                }}</span>
              </el-option>
            </el-select>
          </el-form-item>
        </template>
        <div class="rightbtn">
          <el-button
            type="primary"
            size="small"
            icon="el-icon-paperclip"
            @click="saveBelonging()"
          >儲存</el-button>
          <el-button
            size="small"
            icon="el-icon-close"
            @click="
              dialogBelongVisible = false;
              ResetchbelongForm();
            "
          >關閉</el-button>
        </div>
      </el-form>
    </el-dialog>
  </div>
</template>

<script>
// import axios from 'axios'
import {
  getBuildingListWithPager,
  getBuildingDetail,
  deleteBuilding,
  getByFileName,
  getImgInfo,
  delImg,
  GenPDF,
  UpdateBuildingBelonging,
  RemoveBuildingBelonging,
  ImgUpload
} from '@/api/chaochi/building/buildingservice';
import { GetStoreSales, getSales } from '@/api/security/userservice';
import { getAllOrganizeTreeTable } from '@/api/security/organizeservice';
import { GetlnListByCreatorUserId } from '@/api/chaochi/landlordnaturalperson/customerlnservice';
import { GetlcListByCreatorUserId } from '@/api/chaochi/landlordcomapnyperson/customerlcservice';
import defaultSettings from '@/settings';
import { getToken } from '@/utils/auth';
import * as imageConversion from 'image-conversion';
import BuildingFormHistory from './BuildingFormHistory.vue';
import Equipment from './equipment.vue';
import BuildingBasic from './buildingBasic.vue';
import BuildingRentBasic from './BuildingRentBasic.vue';
import BuildingSituation from './BuildingSituation.vue';

import elDragDialog from '@/directive/el-drag-dialog'; // base on element-ui

export default {
  name: 'Building',
  components: {
    BuildingFormHistory,
    Equipment,
    BuildingBasic,
    BuildingRentBasic,
    BuildingSituation
  }, //, datepicker },
  directives: { elDragDialog },
  data() {
    return {
      buildingRentBasic: {},
      buildingSituation: {},
      building: {},
      building1: {},
      editFrom: {},
      editFromF: {},
      editFromG: {},
      pagination: {
        currentPage: 1,
        pagesize: 10,
        pageTotal: 0
      },
      paginationF: {
        currentPage: 1,
        pagesize: 10,
        pageTotal: 0
      },
      paginationI: {
        currentPage: 1,
        pagesize: 50,
        pageTotal: 0
      },
      sortableData: {
        order: 'desc',
        sort: 'CreatorTime'
      },
      searchform: {
        RoleId: '',
        Keywords: '',
        CreateTime: '',
        BelongLID: '',
        BAdd: ''
      },
      chbelonging: {
        destDept: '',
        destUserId: '不指定',
        destLandlord: ''
      },
      send: {
        sid: this.currentId,
        formname: '',
        note: '',
        File: ''
      },
      belongrule: {
        destUserId: [
          {
            required: true,
            triggrt: 'blur',
            message: '請選擇指定業務'
          }
        ],
        destLandlord: []
      },
      headers: [],
      userOptions: [],
      selectOrganize: [],
      Sales: [],
      currentSelected: [],
      currentSelectedD: [],
      currentSelectedF: [],
      currentSelectedG: [],
      tableData: [],
      ids: [],
      fileList: [],
      LNOptions: [],
      LCOptions: [],
      BState: [
        { value: '', label: '不指定' },
        { value: '待招租', label: '待招租' },
        { value: '招租中', label: '招租中' },
        { value: '已收訂', label: '已收訂' },
        { value: '已出租', label: '已出租' },
        { value: '無委託', label: '無委託' }
      ],
      clearFlag: false,
      tableloading: true,
      imageTableloading: false,
      belongloading: false,
      uploadloading: false,
      dialogEditFormVisible: false,
      dialogEditFormVisibleD: false,
      dialogEditFormVisibleF: false,
      dialogEditFormVisibleG: false,
      dialogBelongVisible: false,
      loading: false,
      refreshFlag: false,
      respecific: false,
      limitfilescount: 2,
      rerenderGridtable: Date.now(),
      editFormTitle: '',
      editFormTitleD: '新增',
      editFormTitleF: '',
      editFormTitleG: '',
      formLabelWidth: '165px',
      currentId: '',
      currentIdF: '',
      currentIdG: '',
      currentBuildingBelongLID: '',
      ActionName: 'A',
      httpFileUploadUrl:
        defaultSettings.apiChaochiUrl + 'Files/BuildingImgZipUpload',
      httpImgFileUploadUrl:
        defaultSettings.apiChaochiUrl + 'Building/ImgUpload',
      emitBPing: '',
      belongStore: '1',
      belongL: '1'
    };
  },
  watch: {
    currentId: {
      handler() {
        this.send.sid = this.currentId;
      }
    },
    respecific: {
      handler(a) {
        if (!a) {
          this.chbelonging.destLandlord = '';
          this.belongrule.destLandlord = [];
        }
        if (a) {
          this.belongrule.destLandlord.push({
            required: true,
            trigger: 'blur',
            message: '請選擇指定出租人'
          });
        }
      }
    },
    'editFrom.BuildingImages': {
      handler() {
        for (const item of this.editFrom.BuildingImages) {
          this.imageTableloading = true;
          getByFileName(item.FileName, this.currentId)
            .then((response) => {
              item.ImgUrl = URL.createObjectURL(response);
            })
            .catch((error) => {
              console.log(error);
            })
            .finally(() => {
              this.imageTableloading = false;
            });
        }
      }
    },
    belongStore: {
      handler(a) {
        if (a) {
          this.chbelonging.destDept = '';
          this.chbelonging.destUserId = '';
          this.chbelonging.destLandlord = '';
        }
      }
    },
    'chbelonging.destUserId': {
      handler(a) {
        if (a) {
          this.chbelonging.destLandlord = '';
          GetlnListByCreatorUserId(a).then((res) => {
            this.LNOptions = res.ResData;
          });
          GetlcListByCreatorUserId(a).then((res) => {
            this.LCOptions = res.ResData;
          });
        }
      }
    },
    belongL: {
      handler() {
        this.chbelonging.destLandlord = '';
      }
    }
  },
  created() {
    this.InitDictItem();
  },
  methods: {
    genPDF() {
      GenPDF(this.currentId).then((res) => {
        if (res.Success) {
          this.$message({
            message: 'PDF產生成功! 可至建物附件下載!',
            type: 'success'
          });
        }
        this.$refs.buildingformhistory.doSearch();
      });
    },
    ResetchbelongForm() {
      this.chbelonging = {
        destDept: '',
        destUserId: '不指定',
        destLandlord: ''
      };
      this.respecific = false;
      this.belongStore = '1';
      this.belongL = '1';
    },
    /**
     * 初始化數據
     */
    InitDictItem() {
      getAllOrganizeTreeTable().then((res) => {
        this.selectOrganize = res.ResData;
      });
      this.pagination.currentPage = 1;
      this.paginationF.currentPage = 1;
      this.loadTableData();
      this.headers = { Authorization: 'Bearer ' + (getToken() || '') };
      getSales().then((res) => {
        this.Sales = res.ResData;
        if (this.Sales[0].RealName === '待指派') {
          this.Sales.shift();
        }
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
        Filter: this.searchform,
        // Keywords: this.searchform.name,
        Order: this.sortableData.order,
        Sort: this.sortableData.sort,
        BAdd: this.searchform.BAdd
      };
      getBuildingListWithPager(seachdata).then((res) => {
        this.tableData = res.ResData.Items;
        this.pagination.pageTotal = res.ResData.TotalItems;
        this.tableloading = false;
      });
    },
    loadTableDataD: function() {
      this.imageTableloading = true;
      var seachdata = {
        CurrenetPageIndex: this.paginationI.currentPage,
        PageSize: this.paginationI.pagesize,
        Keywords: this.currentId,
        Order: this.sortableData.order,
        Sort: this.sortableData.sort
      };
      getImgInfo(seachdata).then((res) => {
        this.editFrom.BuildingImages = res.ResData.Items;
        this.paginationI.pageTotal = res.ResData.TotalItems;
        if (this.editFrom.BuildingImages === null) {
          this.limitfilescount = 50;
        } else {
          this.limitfilescount = 50 - res.ResData.TotalItems;
        }
        this.imageTableloading = false;
      });
    },
    loadTableDataF: function() {
      this.tableloading = true;
      this.paginationF.pageTotal = this.editFrom.BuildingBelongings.length;
      this.tableloading = false;
    },
    loadTableDataG: function() {
      this.tableloading = true;
      this.paginationG.pageTotal = this.editFrom.Remittances.length;
      this.tableloading = false;
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
      this.resetForm('editFrom');
    },
    resetF() {
      this.editFromF = {};
    },
    resetG() {
      this.editFromG = {};
    },
    /**
     * 新增、修改或查看明細信息（綁定顯示數據）     *
     */
    ShowEditOrViewDialog: function(view) {
      // this.reset()
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
        this.reset();
        this.editFormTitle = '新增';
        this.currentId = '';
        this.dialogEditFormVisible = true;
      }
    },
    ShowEditOrViewDialogD: function(view) {
      if (this.editFrom.BuildingImages !== null) {
        if (this.limitfilescount > 0) {
          this.dialogEditFormVisibleD = true;
        } else {
          this.$message({
            message: '此建物照片不得超過50張圖檔!',
            type: 'error'
          });
        }
      } else {
        this.dialogEditFormVisibleD = true;
      }
    },
    ShowEditOrViewDialogF: function(view) {
      this.resetF();
      if (view !== undefined) {
        if (
          this.currentSelectedF.length > 1 ||
          this.currentSelectedF.length === 0
        ) {
          this.$alert('請選擇一條數據進行編輯/修改', '提示');
        } else {
          this.currentIdF = this.currentSelectedF[0].Id;
          this.editFormTitleF = '編輯';
          this.dialogEditFormVisibleF = true;
          this.bindEditInfoF();
        }
      } else {
        this.editFormTitleF = '新增';
        this.currentIdF = '';
        this.dialogEditFormVisibleF = true;
      }
    },
    ShowEditOrViewDialogG: function(view) {
      this.resetG();
      if (view !== undefined) {
        if (
          this.currentSelectedG.length > 1 ||
          this.currentSelectedG.length === 0
        ) {
          this.$alert('請選擇一條數據進行編輯/修改', '提示');
        } else {
          this.currentIdG = this.currentSelectedG[0].Id;
          this.editFormTitleG = '編輯';
          this.dialogEditFormVisibleG = true;
        }
      } else {
        this.editFormTitleG = '新增';
        this.currentIdG = '';
        this.dialogEditFormVisibleG = true;
      }
    },
    bindEditInfo: function() {
      getBuildingDetail(this.currentId)
        .then((res) => {
          this.editFrom = res.ResData;
          this.buildingRentBasic = res.ResData.BuildingRentBasic;
          this.buildingSituation = res.ResData.BuildingSituation;
          this.loadTableDataF();
          this.loadTableDataD();
        })
        .catch(() => {
          this.loading = false;
        })
        .finally(() => {
          this.loading = false;
        });
    },
    bindEditInfoF: function() {
      this.editFromF = this.currentSelectedF[0];
    },
    bindEditInfoG: function() {
      this.editFromG = this.currentSelectedG[0];
    },
    // 關閉按鈕
    closeDialogAndRefresh() {
      this.dialogEditFormVisible = false;
      this.ActionName = 'A';
      this.currentSelected = '';
      this.loadTableData();
    },
    saveEditFormF() {
      if (this.editFrom.BuildingBelongings == null) {
        this.editFrom.BuildingBelongings = [];
      }
      this.editFrom.BuildingBelongings.push(this.editFromF);
      this.dialogEditFormVisibleF = false;
    },
    saveEditFormG() {
      if (this.editFrom.Remittances == null) {
        this.editFrom.Remittances = [];
      }
      this.editFrom.Remittances.push(this.editFromG);
      this.dialogEditFormVisibleG = false;
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
            return deleteBuilding(data);
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
    deletePhysicsD: function() {
      if (this.currentSelectedD.length === 0) {
        this.$alert('請先選擇要操作的數據', '提示');
        return false;
      } else {
        this.imageTableloading = true;
        // var ids = [];
        this.currentSelectedD.forEach((item) => {
          this.ids.push(item.FileName);
        });
        delImg(this.ids, this.currentId)
          .then((res) => {
            this.$message.success('所選檔案已刪除成功!');
          })
          .catch(() => {
            this.$message.error('失敗!');
          })
          .finally(() => {
            this.imageTableloading = false;
            this.currentSelectedD = [];
            this.paginationI.currentPage = 1;
            this.loadTableDataD();
          });
      }
    },
    deletePhysicsF: function() {
      if (this.currentSelectedF.length === 0) {
        this.$alert('請先選擇要操作的數據', '提示');
        return false;
      } else {
        this.currentSelectedF.forEach((element) => {
          element.NeedDel = 'D';
        });
      }
    },
    deletePhysicsG: function() {
      if (this.currentSelectedG.length === 0) {
        this.$alert('請先選擇要操作的數據', '提示');
        return false;
      } else {
        // var currentIds = []
        this.currentSelectedG.forEach((element) => {
          element.NeedDel = 'D';
        });
      }
    },
    handleSelectOrganizeChange: function() {
      this.editFrom.OrganizeId = this.selectedOrganizeOptions;
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
    handleSortChangeG: function(column) {
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
      this.currentBuildingBelongLID = this.currentSelected[0].BelongLID;
    },
    handleSelectChangeD: function(selection, row) {
      this.currentSelectedD = selection;
    },
    handleSelectChangeF: function(selection, row) {
      this.currentSelectedF = selection;
    },
    handleSelectChangeG: function(selection, row) {
      this.currentSelectedG = selection;
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
    handleSelectAllChangeG: function(selection) {
      this.currentSelectedG = selection;
    },
    /**
     * 選擇每頁顯示數量
     */
    handleSizeChange(val) {
      this.pagination.pagesize = val;
      this.pagination.currentPage = 1;
      this.loadTableData();
    },
    handleSizeChangeI(val) {
      this.paginationI.pagesize = val;
      this.paginationI.currentPage = 1;
      this.loadTableDataD();
    },
    handleSizeChangeF(val) {
      this.paginationF.pagesize = val;
      this.paginationF.currentPage = 1;
      this.loadTableDataF();
    },
    handleSizeChangeG(val) {
      this.paginationG.pagesize = val;
      this.paginationG.currentPage = 1;
      this.loadTableDataG();
    },
    /**
     * 選擇當頁面
     */
    handleCurrentChange(val) {
      this.pagination.currentPage = val;
      this.loadTableData();
    },
    handleCurrentChangeI(val) {
      this.paginationI.currentPage = val;
      this.loadTableDataD();
    },
    handleCurrentChangeF(val) {
      this.paginationF.currentPage = val;
      this.loadTableDataF();
    },
    handleCurrentChangeG(val) {
      this.paginationG.currentPage = val;
      this.loadTableDataG();
    },
    uploaderror: function(err, file, fileList) {
      this.$message({
        message: file.name + ' 上傳失敗' + err,
        type: 'error'
      });
    },
    onexceed(a, b) {
      this.$message({
        message: ` 此建物僅能再上傳${this.limitfilescount}張圖檔!`,
        type: 'error'
      });
      this.$refs.newupload.clearFiles();
    },
    async beforeupload(file) {
      this.send.File = await this.compressImageAsync(file);
    },
    downloadItem: function(url, label) {
      // https://stackoverflow.com/questions/53772331/vue-html-js-how-to-download-a-file-to-browser-using-the-download-tag
      this.tableloading = true;

      getByFileName(label, this.currentId)
        .then((response) => {
          const blob = response; // new Blob([response.data], { type: 'image/jpeg' })
          const link = document.createElement('a');
          link.href = URL.createObjectURL(blob);
          link.download = label;
          link.click();
          URL.revokeObjectURL(link.href);
        })
        .catch((error) => {
          console.log(error);
        });

      this.tableloading = false;
    },
    rowdblclick(row, column, event) {
      this.$refs.gridtable.clearSelection();
      this.currentSelected = '';
      this.rerenderGridtable = Date.now();
      this.$nextTick(function() {
        this.$refs.gridtable.toggleRowSelection(row, true);
        this.currentSelected = this.$refs.gridtable.selection;
        this.ShowEditOrViewDialog('edit');
      });
    },
    getsearchaddress(addressData) {
      this.searchform.BAdd_1 = addressData.Add_1;
      this.searchform.BAdd_1_1 = addressData.Add_1_1;
      this.searchform.BAdd_1_2 = addressData.Add_1_2;
      this.searchform.BAdd_2 = addressData.Add_2;
      this.searchform.BAdd_2_1 = addressData.Add_2_1;
      this.searchform.BAdd_2_2 = addressData.Add_2_2;
      this.searchform.BAdd_2_3 = addressData.Add_2_3;
      this.searchform.BAdd_2_4 = addressData.Add_2_4;
      this.searchform.BAdd_3 = addressData.Add_3;
      this.searchform.BAdd_3_1 = addressData.Add_3_1;
      this.searchform.BAdd_3_2 = addressData.Add_3_2;
      this.searchform.BAdd_4 = addressData.Add_4;
      this.searchform.BAdd_5 = addressData.Add_5;
      this.searchform.BAdd_6 = addressData.Add_6;
      this.searchform.BAdd_7 = addressData.Add_7;
      this.searchform.BAdd_8 = addressData.Add_8;
      this.searchform.BAdd_9 = addressData.Add_9;
    },
    geteditaddress(addressData, title) {
      if (title === '建物門牌地址') {
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
    emitbpingFunc(val) {
      this.emitBPing = val;
    },
    async compressImageAsync(file) {
      var compressedBlob = await this.compressAsync(file);
      var files = new File([compressedBlob], file.name, {
        type: 'image/jpeg'
      });
      return files;
    },
    async compressAsync(file) {
      const res = await imageConversion.compressAccurately(file, {
        size: 400,
        scale: 1
      });
      return res;
    },
    saveBelonging() {
      this.$refs['chbelonging'].validate((valid) => {
        if (valid) {
          var currentIds = [];
          this.currentSelected.forEach((element) => {
            currentIds.push(element.Id);
          });
          var data = {
            buildingIds: currentIds,
            chbelonging: this.chbelonging
          };
          this.belongloading = true;
          UpdateBuildingBelonging(data)
            .then((res) => {
              if (res.Success) {
                this.$message.success('轉移成功!');
                this.dialogBelongVisible = false;
                this.loadTableData();
                this.ResetchbelongForm();
              } else {
                this.$message.error('失敗');
              }
            })
            .finally(() => {
              this.belongloading = false;
            });
        } else {
          return false;
        }
      });
    },
    BelongTransfer() {
      if (
        this.currentSelected.length === 0 ||
        this.currentSelected.length > 1
      ) {
        this.$alert('請選擇一條數據進行', '提示');
      } else {
        this.belongloading = true;
        GetStoreSales()
          .then((res) => {
            this.userOptions = res.ResData;
          })
          .finally(() => {
            this.belongloading = false;
          });
        this.dialogBelongVisible = true;
      }
    },
    BelongRemove() {
      if (this.currentSelected.length === 0) {
        this.$alert('請選擇一條/多條數據進行', '提示');
      } else {
        this.$confirm('此操作將會永久移除此所屬權， 是否繼續?', '警告', {
          confirmButtonText: '確定',
          cancelButtonText: '取消',
          type: 'warning'
        }).then(() => {
          this.tableloading = true;
          var currentIds = [];
          this.currentSelected.forEach((element) => {
            currentIds.push(element.Id);
          });
          var data = {
            buildingIds: currentIds
          };
          RemoveBuildingBelonging(data)
            .then((res) => {
              if (res.Success) {
                this.$message.success('移除成功!');
                this.loadTableData();
              } else {
                this.$message.error('失敗');
              }
            })
            .finally(() => {
              this.tableloading = false;
            });
        });
      }
    },
    // 上传发生变化钩子
    handleFileChange(file, fileList) {
      this.fileList = fileList;
    },
    // 删除之前钩子
    handleFileRemove(file, fileList) {
      this.fileList = fileList;
    },
    async ConfirmUpload() {
      this.uploadloading = true;
      const formData = new FormData();
      if (this.fileList.length > 25) {
        this.$alert(
          `批次上傳數量不得超過25 !<br/>當前數量:${this.fileList.length}`,
          '提示',
          {
            confirmButtonText: '確定',
            dangerouslyUseHTMLString: true
          }
        );
        this.uploadloading = false;
        return;
      }
      if (this.fileList.length > 0) {
        for (var i = 0; i < this.fileList.length; i++) {
          var compressedBlob = await this.compressAsync(this.fileList[i].raw);
          var files = new File([compressedBlob], this.fileList[i].name, {
            type: 'image/jpeg'
          });
          // if (files.size > 210000) {
          //   compressedBlob = await this.compressAsync(files);
          //   files = new File([compressedBlob], this.fileList[i].name, {
          //     type: 'image/jpeg'
          //   });
          // }
          formData.append('File', files);
        }
      } else {
        formData.append('File', '');
      }
      formData.append('sid', this.currentId);

      ImgUpload(formData)
        .then((res) => {
          if (res.Success) {
            this.$message.success('上傳成功!');
            this.fileList = [];
            this.$refs.newupload.clearFiles();
            this.dialogEditFormVisibleD = false;
          } else {
            this.$message.error('失敗!');
            this.fileList = [];
            this.$refs.newupload.clearFiles();
            this.dialogEditFormVisibleD = false;
          }
          this.loadTableDataD();
        })
        .finally(() => {
          this.uploadloading = false;
        });
    }
  }
};
</script>
<style lang="less" scoped>
.center {
  display: block;
  margin-left: auto;
  margin-right: auto;
  width: 100%;
}

.tableClass {
  .el-table__fixed-right {
    height: 100% !important;
  }
}
</style>
