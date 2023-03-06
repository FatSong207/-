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
          label-width="150px"
        >
          <el-form-item label="收款方：">
            <el-select v-model="searchform.RPayee" placeholder="請選擇">
              <el-option v-for="item in PayOptions" :key="item.value" :label="item.label" :value="item.value" />
            </el-select>
          </el-form-item>
          <el-form-item label="收款方名稱：">
            <el-input v-model="searchform.RPayeeName" clearable placeholder="收款方名稱" />
          </el-form-item>
          <el-form-item label="收款方身份證字號：">
            <el-input v-model="searchform.RPayeeID" clearable placeholder="收款方身份證字號" />
          </el-form-item>
          <el-form-item label="收款方統一編號：">
            <el-input v-model="searchform.RPayeeTaxID" clearable placeholder="收款方統一編號" />
          </el-form-item>
          <el-form-item label="兆基收款單位：">
            <el-select v-model="searchform.RPayeeUnit" placeholder="請選擇">
              <el-option v-for="item in ChaochiUnits" :key="item" :label="item" :value="item" />
            </el-select>
          </el-form-item>
          <br>
          <el-form-item label="付款方：">
            <el-select v-model="searchform.RPayer" placeholder="請選擇">
              <el-option v-for="item in PayOptions" :key="item.value" :label="item.label" :value="item.value" />
            </el-select>
          </el-form-item>
          <el-form-item label="付款方名稱：">
            <el-input v-model="searchform.RPayerName" clearable placeholder="收款方名稱" />
          </el-form-item>
          <el-form-item label="付款方身份證字號：">
            <el-input v-model="searchform.RPayerID" clearable placeholder="收款方身份證字號" />
          </el-form-item>
          <el-form-item label="付款方統一編號：">
            <el-input v-model="searchform.RPayerTaxID" clearable placeholder="收款方統一編號" />
          </el-form-item>
          <el-form-item label="兆基付款單位：">
            <el-select v-model="searchform.RPayerUnit" placeholder="請選擇">
              <el-option v-for="item in ChaochiUnits" :key="item" :label="item" :value="item" />
            </el-select>
          </el-form-item>
          <br>
          <el-form-item label="建物門牌地址：">
            <Address @getsearchaddress="getsearchaddress" />
          </el-form-item>
          <br>
          <el-form-item label="領收據類別：">
            <el-select v-model="searchform.RCate" placeholder="請選擇">
              <el-option v-for="item in ReceiptCategories" :key="item.value" :label="item.label" :value="item.value" />
            </el-select>
          </el-form-item>
          <el-form-item label="領收據狀態：">
            <el-select v-model="searchform.RStatus" placeholder="請選擇">
              <el-option v-for="item in ReceiptStatus" :key="item.value" :label="item.label" :value="item.value" />
            </el-select>
          </el-form-item>
          <el-form-item label="領收據編號：">
            <el-input v-model="searchform.ReceiptId" clearable placeholder="領收據編號" />
          </el-form-item>
          <el-form-item label="重複收定">
            <el-checkbox v-model="searchform.ROverBooking" />
          </el-form-item>
          <el-form-item>
            <el-button type="primary" @click="handleSearch()">查詢</el-button>
          </el-form-item>
        </el-form>
      </el-card>
    </div>
    <el-card>
      <div class="list-btn-container">
        <el-button-group>
          <!-- <el-button v-hasPermi="['Receipt/Add']" type="primary" icon="el-icon-plus" size="small" @click="ShowEditOrViewDialog()">新增</el-button>
          <el-button v-hasPermi="['Receipt/Edit']" type="primary" icon="el-icon-edit" class="el-button-modify" size="small" @click="ShowEditOrViewDialog('edit')">修改</el-button>
          <el-button v-hasPermi="['Receipt/Enable']" type="info" icon="el-icon-video-pause" size="small" @click="setEnable('0')">禁用</el-button>
          <el-button v-hasPermi="['Receipt/Enable']" type="success" icon="el-icon-video-play" size="small" @click="setEnable('1')">啟用</el-button>
          <el-button v-hasPermi="['Receipt/DeleteSoft']" type="warning" icon="el-icon-delete" size="small" @click="deleteSoft('0')">軟刪除</el-button>
          <el-button v-hasPermi="['Receipt/Delete']" type="danger" icon="el-icon-delete" size="small" @click="deletePhysics()">刪除</el-button> -->
          <el-button
            v-hasPermi="['Receipt/Add']"
            type="primary"
            icon="el-icon-plus"
            size="small"
            @click="ShowEditOrViewDialog()"
          >新增</el-button>
          <el-button
            v-hasPermi="['Receipt/Edit']"
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
            v-hasPermi="['Receipt/Delete']"
            type="danger"
            icon="el-icon-delete"
            size="small"
            @click="deletePhysics()"
          >刪除</el-button>
          <el-button
            v-hasPermi="['Receipt/Edit']"
            type="danger"
            icon="el-icon-delete"
            size="small"
            @click="changeReceiptStatus('已作廢')"
          >作廢</el-button>
          <el-button
            v-hasPermi="['Receipt/Edit']"
            type="success"
            icon="el-icon-video-play"
            size="small"
            @click="changeReceiptStatus('已入帳')"
          >入帳
          </el-button>
          <el-button type="default" icon="el-icon-refresh" size="small" @click="loadTableData()">更新</el-button>
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
        :default-sort="{prop: 'SortCode', order: 'descending'}"
        @select="handleSelectChange"
        @select-all="handleSelectAllChange"
        @sort-change="handleSortChange"
        @row-dblclick="rowdblclick"
      >
        <el-table-column type="selection" width="30" />
        <el-table-column prop="ReceiptId" label="領收據編號" sortable="custom" width="120" />
        <el-table-column prop="RCate" label="領收據類別" sortable="custom" width="120" />
        <el-table-column prop="RPayee" label="收款人" sortable="custom" width="120" />
        <el-table-column prop="RPayer" label="付款人" sortable="custom" width="120" />
        <el-table-column prop="RAdd" label="建物門牌地址" sortable="custom" width="120" />
        <el-table-column prop="RTotalCost" label="單據總金額" sortable="custom" width="120" />
        <el-table-column prop="RStatus" label="領收據狀態" sortable="custom" width="120" />
        <el-table-column prop="ROverBooking" label="重複收訂" sortable="custom" width="120" />
        <el-table-column prop="RSales" label="業務人員" sortable="custom" width="120" />
        <el-table-column prop="RAccounting" label="帳務人員" sortable="custom" width="120" />
        <el-table-column prop="RUnsignPDFPath" label="未用印PDF" width="120">
          <template slot-scope="scope">
            <el-button @click="downloadUnsignPDF(scope.row)">下載</el-button>
          </template>
        </el-table-column>
        <el-table-column prop="RSignedPDFPath" label="用印PDF" width="120">
          <template slot-scope="scope">
            <el-button @click="downloadSignedPDF(scope.row)">下載</el-button>
          </template>
        </el-table-column>
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
      :title="editFormTitle+'領收據'"
      :visible.sync="dialogEditFormVisible"
      width="1040px"
      top="0"
      fullscreen
      @close="cancel"
    >
      <el-card class="box-card">
        <el-form ref="editFrom" :model="editFrom" :rules="rules" :inline="true" class="demo-form-inline">
          <el-form-item label="領收據類別" :label-width="dialogLabelWidth" prop="RCate">
            <el-select
              v-model="editFrom.RCate"
              placeholder="請選擇"
              :disabled="categoryDisible === true"
              @change="disableCategory"
            >
              <el-option v-for="item in ReceiptCategories2" :key="item.value" :label="item.label" :value="item.value" />
            </el-select>
          </el-form-item>
          <br>
          <el-form-item v-if="categoryDisible === true" label="領收據編號" :label-width="dialogLabelWidth" prop="ReceiptId">
            <el-input v-model="editFrom.ReceiptId" autocomplete="off" readonly />
          </el-form-item>
          <el-form-item v-if="categoryDisible === true" label="領收據日期" :label-width="dialogLabelWidth" prop="RDate">
            <el-input v-model="editFrom.RDate" autocomplete="off" readonly />
          </el-form-item>
          <br>
          <el-form-item
            v-if="editFrom.ReceiptId !== '' && editFrom.RDate !== ''"
            label="收款方"
            :label-width="dialogLabelWidth"
            prop="RPayee"
          >
            <el-select v-model="editFrom.RPayee" placeholder="請選擇">
              <el-option v-for="item in PayOptions2" :key="item.value" :label="item.label" :value="item.value" />
            </el-select>
          </el-form-item>
          <el-form-item
            v-if="editFrom.ReceiptId !== '' && editFrom.RDate !== '' && editFrom.RPayee !== '兆基' && editFrom.RPayee !== ''"
            label="收款方名稱"
            :label-width="dialogLabelWidth"
            prop="RPayeeName"
          >
            <el-input v-model="editFrom.RPayeeName" placeholder="請輸入收款方名稱" autocomplete="off" clearable />
          </el-form-item>
          <el-form-item
            v-if="editFrom.ReceiptId !== '' && editFrom.RDate !== '' && editFrom.RPayee === '兆基' && editFrom.RPayee !== ''"
            label="兆基收款單位"
            :label-width="dialogLabelWidth"
            prop="RPayeeUnit"
          >
            <el-select v-model="editFrom.RPayeeUnit" placeholder="請選擇">
              <el-option v-for="item in ChaochiUnits2" :key="item.Name" :label="item.Name" :value="item.Name" />
            </el-select>
          </el-form-item>
          <el-form-item
            v-if="editFrom.ReceiptId !== '' && editFrom.RDate !== '' && editFrom.RPayee !== '兆基' && editFrom.RPayee !== ''"
            label="收款方身份證字號"
            :label-width="dialogLabelWidth"
            prop="RPayeeID"
          >
            <el-input v-model="editFrom.RPayeeID" placeholder="請輸入收款方身份證字號" autocomplete="off" clearable />
          </el-form-item>
          <el-form-item
            v-if="editFrom.ReceiptId !== '' && editFrom.RDate !== '' && editFrom.RPayee === '兆基' && editFrom.RPayee !== ''"
            label="收款方統一編號"
            :label-width="dialogLabelWidth"
            prop="RPayeeTaxID"
          >
            <el-input v-model="editFrom.RPayeeTaxID" placeholder="請輸入收款方統一編號" autocomplete="off" clearable />
          </el-form-item>
          <el-form-item
            v-if="editFrom.ReceiptId !== '' && editFrom.RDate !== '' && editFrom.RCate === '內部領據' && editFrom.RPayee !== ''"
            label="收款方電話"
            :label-width="dialogLabelWidth"
            prop="RPayeeTel"
          >
            <el-input v-model="editFrom.RPayeeTel" placeholder="請輸入收款方電話" autocomplete="off" clearable />
          </el-form-item>
          <br>
          <el-form-item
            v-if="editFrom.ReceiptId !== '' && editFrom.RDate !== ''"
            label="付款方"
            :label-width="dialogLabelWidth"
            prop="RPayer"
          >
            <el-select v-model="editFrom.RPayer" placeholder="請選擇">
              <el-option v-for="item in PayOptions2" :key="item.value" :label="item.label" :value="item.value" />
            </el-select>
          </el-form-item>
          <el-form-item
            v-if="editFrom.ReceiptId !== '' && editFrom.RDate !== '' && editFrom.RPayer !== '兆基' && editFrom.RPayer !== ''"
            label="付款方名稱"
            :label-width="dialogLabelWidth"
            prop="RPayerName"
          >
            <el-input v-model="editFrom.RPayerName" placeholder="請輸入付款方名稱" autocomplete="off" clearable />
          </el-form-item>
          <el-form-item
            v-if="editFrom.ReceiptId !== '' && editFrom.RDate !== '' && editFrom.RPayer === '兆基' && editFrom.RPayer !== ''"
            label="兆基付款單位"
            :label-width="dialogLabelWidth"
            prop="RPayerUnit"
          >
            <el-select v-model="editFrom.RPayerUnit" placeholder="請選擇">
              <el-option v-for="item in ChaochiUnits2" :key="item.Name" :label="item.Name" :value="item.Name" />
            </el-select>
          </el-form-item>
          <el-form-item
            v-if="editFrom.ReceiptId !== '' && editFrom.RDate !== '' && editFrom.RPayer !== '兆基' && editFrom.RPayer !== ''"
            label="付款方身份證字號"
            :label-width="dialogLabelWidth"
            prop="RPayerID"
          >
            <el-input v-model="editFrom.RPayerID" placeholder="請輸入付款方身份證字號" autocomplete="off" clearable />
          </el-form-item>
          <el-form-item
            v-if="editFrom.ReceiptId !== '' && editFrom.RDate !== '' && editFrom.RPayer === '兆基' && editFrom.RPayer !== ''"
            label="付款方統一編號"
            :label-width="dialogLabelWidth"
            prop="RPayerTaxID"
          >
            <el-input v-model="editFrom.RPayerTaxID" placeholder="請輸入付款方統一編號" autocomplete="off" clearable />
          </el-form-item>
          <el-form-item
            v-show="editFrom.ReceiptId !== '' && editFrom.RDate !== ''"
            label="建物門牌地址"
            :label-width="dialogLabelWidth"
          >
            <Address :sendedform="sendedform" @geteditaddress="geteditaddress" />
          </el-form-item>
          <br>
          <el-form-item
            v-if="editFrom.ReceiptId !== '' && editFrom.RDate !== ''"
            label="領收據狀態"
            :label-width="dialogLabelWidth"
            prop="RStatus"
          >
            <el-select v-model="editFrom.RStatus">
              <el-option v-show="editFormTitle == '新增'" key="已簽收" label="已簽收" value="已簽收" />
              <el-option v-show="editFormTitle == '編輯'" key="已作廢" label="已作廢" value="已作廢" />
              <el-option v-show="editFormTitle == '編輯'" key="已入帳" label="已入帳" value="已入帳" />
            </el-select>
          </el-form-item>
          <br>
          <el-form-item
            v-if="editFrom.ReceiptId !== '' && editFrom.RDate !== ''"
            label="租金"
            :label-width="dialogLabelWidth"
            prop="PIRent"
          >
            <el-input
              v-model="editFrom.PIRent"
              type="number"
              placeholder="請輸入租金"
              autocomplete="off"
              clearable
              @input="calcDeposit()"
            />
            <!-- <el-input-number v-model="editFrom.PIRent" :precision="0" :controls="false" @input="calcDeposit()" /> -->
          </el-form-item>
          <el-form-item
            v-if="editFrom.ReceiptId !== '' && editFrom.RDate !== ''"
            label="租金起日"
            :label-width="dialogLabelWidth"
            prop="PIRentStartDate"
          >
            <DatePickerE
              v-model="editFrom.PIRentStartDate"
              value-format="yyyy-MM-dd"
              format="yyyy-MM-dd"
              type="date"
              placeholder="選擇租金起日"
            />
          </el-form-item>
          <el-form-item
            v-if="editFrom.ReceiptId !== '' && editFrom.RDate !== ''"
            label="租金迄日"
            :label-width="dialogLabelWidth"
            prop="PIRentEndDate"
          >
            <DatePickerE
              v-model="editFrom.PIRentEndDate"
              value-format="yyyy-MM-dd"
              format="yyyy-MM-dd"
              type="date"
              placeholder="選擇租金迄日"
            />
          </el-form-item>
          <br>
          <el-form-item
            v-if="editFrom.ReceiptId !== '' && editFrom.RDate !== ''"
            label="押金"
            :label-width="dialogLabelWidth"
            prop="PIDeposit"
          >
            <el-input
              v-model="editFrom.PIDeposit"
              type="number"
              placeholder="請輸入押金"
              autocomplete="off"
              clearable
              @input="calcTotalCost()"
            />
            <!-- <el-input-number v-model="editFrom.PIDeposit" :precision="0" :controls="false" @input="calcTotalCost()" /> -->
          </el-form-item>
          <el-form-item
            v-if="editFrom.ReceiptId !== '' && editFrom.RDate !== ''"
            label="押金收費日"
            :label-width="dialogLabelWidth"
            prop="PIDepositDate"
          >
            <DatePickerE
              v-model="editFrom.PIDepositDate"
              value-format="yyyy-MM-dd"
              format="yyyy-MM-dd"
              type="date"
              placeholder="選擇押金收費日"
            />
          </el-form-item>
          <el-form-item
            v-if="editFrom.ReceiptId !== '' && editFrom.RDate !== ''"
            label="押金實收款"
            :label-width="dialogLabelWidth"
            prop="PIDepositActual"
          >
            <el-input
              v-model="editFrom.PIDepositActual"
              type="number"
              placeholder="請輸入押金實收款"
              autocomplete="off"
              clearable
            />
          </el-form-item>
          <el-form-item
            v-if="editFrom.ReceiptId !== '' && editFrom.RDate !== ''"
            label="押金欠款"
            :label-width="dialogLabelWidth"
            prop="PIDepositOwe"
          >
            <el-input
              v-model="editFrom.PIDepositOwe"
              type="number"
              placeholder="請輸入押金欠款"
              autocomplete="off"
              clearable
            />
          </el-form-item>
          <br>
          <el-form-item
            v-if="editFrom.ReceiptId !== '' && editFrom.RDate !== ''"
            label="大樓管理費"
            :label-width="dialogLabelWidth"
            prop="PIManagementFee"
          >
            <el-input
              v-model="editFrom.PIManagementFee"
              type="number"
              placeholder="請輸入大樓管理費"
              autocomplete="off"
              clearable
              @input="calcTotalCost()"
            />
            <!-- <el-input-number v-model="editFrom.PIManagementFee" :precision="0" :controls="false" @input="calcTotalCost()" /> -->
          </el-form-item>
          <el-form-item
            v-if="editFrom.ReceiptId !== '' && editFrom.RDate !== ''"
            label="大樓管理費月份"
            :label-width="dialogLabelWidth"
            prop="PIManagementFeeMonth"
          >
            <DatePickerE
              v-model="editFrom.PIManagementFeeMonth"
              value-format="yyyy-MM-dd"
              format="yyyy-MM-dd"
              type="month"
              placeholder="選擇大樓管理費月份"
            />
          </el-form-item>
          <br>
          <el-form-item
            v-if="editFrom.ReceiptId !== '' && editFrom.RDate !== ''"
            label="仲介服務費"
            :label-width="dialogLabelWidth"
            prop="PIServiceFee"
          >
            <el-input
              v-model="editFrom.PIServiceFee"
              type="number"
              placeholder="請輸入仲介服務費"
              autocomplete="off"
              clearable
              @input="calcTotalCost()"
            />
            <!-- <el-input-number v-model="editFrom.PIServiceFee" :precision="0" :controls="false" @input="calcTotalCost()" /> -->
          </el-form-item>
          <br>
          <el-form-item
            v-if="editFrom.ReceiptId !== '' && editFrom.RDate !== ''"
            label="顧問費"
            :label-width="dialogLabelWidth"
            prop="PIConsultantFee"
          >
            <el-input
              v-model="editFrom.PIConsultantFee"
              type="number"
              placeholder="請輸入顧問費"
              autocomplete="off"
              clearable
              @input="calcTotalCost()"
            />
            <!-- <el-input-number v-model="editFrom.PIConsultantFee" :precision="0" :controls="false" @input="calcTotalCost()" /> -->
          </el-form-item>
          <el-form-item
            v-if="editFrom.ReceiptId !== '' && editFrom.RDate !== ''"
            label="顧問費收取月數"
            :label-width="dialogLabelWidth"
            prop="PIConsultantFeeMonth"
          >
            <el-input
              v-model="editFrom.PIConsultantFeeMonth"
              type="number"
              placeholder="請輸入顧問費收取月數 "
              autocomplete="off"
              clearable
            />
          </el-form-item>
          <br>
          <el-form-item
            v-if="editFrom.ReceiptId !== '' && editFrom.RDate !== ''"
            label="設備費用"
            :label-width="dialogLabelWidth"
            prop="PIEquipmentCost"
          >
            <el-input
              v-model="editFrom.PIEquipmentCost"
              type="number"
              placeholder="請輸入設備費用"
              autocomplete="off"
              clearable
              @input="calcTotalCost()"
            />
            <!-- <el-input-number v-model="editFrom.PIEquipmentCost" :precision="0" :controls="false" @input="calcTotalCost()" /> -->
          </el-form-item>
          <el-form-item
            v-if="editFrom.ReceiptId !== '' && editFrom.RDate !== ''"
            label="設備費用項目"
            :label-width="dialogLabelWidth"
            prop="PIEquipmentNote"
          >
            <el-col style="width: 220px">
              <el-input
                v-model="editFrom.PIEquipmentNote"
                resize="none"
                type="textarea"
                :rows="2"
                placeholder="請輸入設備費用項目"
              />
            </el-col>
          </el-form-item>
          <br>
          <el-form-item
            v-if="editFrom.ReceiptId !== '' && editFrom.RDate !== ''"
            label="電費"
            :label-width="dialogLabelWidth"
            prop="PIElectricityBill"
          >
            <el-input
              v-model="editFrom.PIElectricityBill"
              type="number"
              placeholder="請輸入電費"
              autocomplete="off"
              clearable
              @input="calcTotalCost()"
            />
            <!-- <el-input-number v-model="editFrom.PIElectricityBill" :precision="0" :controls="false" @input="calcTotalCost()" /> -->
          </el-form-item>
          <el-form-item
            v-if="editFrom.ReceiptId !== '' && editFrom.RDate !== ''"
            label="水雜費"
            :label-width="dialogLabelWidth"
            prop="PIWaterBill"
          >
            <el-input
              v-model="editFrom.PIWaterBill"
              type="number"
              placeholder="請輸入水雜費"
              autocomplete="off"
              clearable
              @input="calcTotalCost()"
            />
            <!-- <el-input-number v-model="editFrom.PIWaterBill" :precision="0" :controls="false" @input="calcTotalCost()" /> -->
          </el-form-item>
          <el-form-item
            v-if="editFrom.ReceiptId !== '' && editFrom.RDate !== ''"
            label="瓦斯費"
            :label-width="dialogLabelWidth"
            prop="PIGasFee"
          >
            <el-input
              v-model="editFrom.PIGasFee"
              type="number"
              placeholder="請輸入瓦斯費"
              autocomplete="off"
              clearable
              @input="calcTotalCost()"
            />
            <!-- <el-input-number v-model="editFrom.PIGasFee" :precision="0" :controls="false" @input="calcTotalCost()" /> -->
          </el-form-item>
          <el-form-item
            v-if="editFrom.ReceiptId !== '' && editFrom.RDate !== ''"
            label="垃圾費"
            :label-width="dialogLabelWidth"
            prop="PITrashFee"
          >
            <el-input
              v-model="editFrom.PITrashFee"
              type="number"
              placeholder="請輸入垃圾費"
              autocomplete="off"
              clearable
              @input="calcTotalCost()"
            />
            <!-- <el-input-number v-model="editFrom.PITrashFee" :precision="0" :controls="false" @input="calcTotalCost()" /> -->
          </el-form-item>
          <br>
          <el-form-item
            v-if="editFrom.ReceiptId !== '' && editFrom.RDate !== ''"
            label="清潔費"
            :label-width="dialogLabelWidth"
            prop="PICleaningFee"
          >
            <el-input
              v-model="editFrom.PICleaningFee"
              type="number"
              placeholder="請輸入清潔費"
              autocomplete="off"
              clearable
              @input="calcTotalCost()"
            />
            <!-- <el-input-number v-model="editFrom.PICleaningFee" :precision="0" :controls="false" @input="calcTotalCost()" /> -->
          </el-form-item>
          <el-form-item
            v-if="editFrom.ReceiptId !== '' && editFrom.RDate !== ''"
            label="行政處理費"
            :label-width="dialogLabelWidth"
            prop="PIAdministrativeFee"
          >
            <el-input
              v-model="editFrom.PIAdministrativeFee"
              type="number"
              placeholder="請輸入行政處理費"
              autocomplete="off"
              clearable
              @input="calcTotalCost()"
            />
            <!-- <el-input-number v-model="editFrom.PIAdministrativeFee" :precision="0" :controls="false" @input="calcTotalCost()" /> -->
          </el-form-item>
          <el-form-item
            v-if="editFrom.ReceiptId !== '' && editFrom.RDate !== ''"
            label="管理服務費"
            :label-width="dialogLabelWidth"
            prop="PIManagementServiceFee"
          >
            <el-input
              v-model="editFrom.PIManagementServiceFee"
              type="number"
              placeholder="請輸入管理服務費收費方式"
              autocomplete="off"
              clearable
              @input="calcTotalCost()"
            />
            <!-- <el-input-number v-model="editFrom.PIManagementServiceFee" :precision="0" :controls="false" @input="calcTotalCost()" /> -->
          </el-form-item>
          <br>
          <el-form-item
            v-if="editFrom.ReceiptId !== '' && editFrom.RDate !== ''"
            label="其他費用"
            :label-width="dialogLabelWidth"
            prop="PIMiscellaneousFee"
          >
            <el-input
              v-model="editFrom.PIMiscellaneousFee"
              type="number"
              placeholder="請輸入其他費用"
              autocomplete="off"
              clearable
              @input="calcTotalCost()"
            />
            <!-- <el-input-number v-model="editFrom.PIMiscellaneousFee" :precision="0" :controls="false" @input="calcTotalCost()" /> -->
          </el-form-item>
          <el-form-item
            v-if="editFrom.ReceiptId !== '' && editFrom.RDate !== ''"
            label="其他費用項目"
            :label-width="dialogLabelWidth"
            prop="PIMiscellaneousFeeNote"
          >
            <el-input v-model="editFrom.PIMiscellaneousFeeNote" placeholder="請輸入其他費用項目" autocomplete="off" clearable />
          </el-form-item>
          <br>
          <el-form-item
            v-if="editFrom.ReceiptId !== '' && editFrom.RDate !== ''"
            label="收費項目總金額"
            :label-width="dialogLabelWidth"
            prop="RTotalCost"
          >
            <el-input v-model="editFrom.RTotalCost" type="number" readonly />
          </el-form-item>
          <br>
          <el-form-item
            v-if="editFrom.ReceiptId !== '' && editFrom.RDate !== ''"
            label="收費方式"
            :label-width="dialogLabelWidth"
            prop="RPaymentMethod"
          >
            <!-- <el-input v-model="editFrom.RPaymentMethod" placeholder="請輸入收費方式" autocomplete="off" clearable /> -->
            <el-select v-model="editFrom.RPaymentMethod" placeholder="請選擇">
              <el-option v-for="item in ItemPayOptions" :key="item.value" :label="item.label" :value="item.value" />
            </el-select>
          </el-form-item>
          <br>
          <el-form-item
            v-if="editFrom.ReceiptId !== '' && editFrom.RDate !== ''"
            label="備註"
            :label-width="dialogLabelWidth"
            prop="RMemo"
          >
            <el-col style="width: 350px">
              <el-input v-model="editFrom.RMemo" type="textarea" placeholder="請輸入備註" resize="none" :rows="4" />
            </el-col>
          </el-form-item>
          <br>
          <el-form-item
            v-if="editFrom.ReceiptId !== '' && editFrom.RDate !== '' && (editFrom.RSignatureImgPath === null || editFrom.RSignatureImgPath === '')"
            label="簽名"
            :label-width="dialogLabelWidth"
            prop="SignaturePad"
          >
            <VueSignaturePad ref="signaturePad" width="800px" height="500px" class="el-input__inner" />
          </el-form-item>
          <el-form-item
            v-if="editFrom.ReceiptId !== '' && editFrom.RDate !== '' && editFrom.RSignatureImgPath !== null && editFrom.RSignatureImgPath !== ''"
            label="簽名"
            :label-width="dialogLabelWidth"
            prop="SignatureImg"
          >
            <el-image class="tmb" :src="editFrom.RSignatureImgPath" />
          </el-form-item>
          <br>
          <el-form-item
            v-if="editFrom.ReceiptId !== '' && editFrom.RDate !== '' && (editFrom.RSignatureImgPath !== null && editFrom.RSignatureImgPath !== '')"
            label="簽名日期"
            :label-width="dialogLabelWidth"
            prop="RSignDate"
          >
            <el-input v-model="editFrom.RSignDate" autocomplete="off" readonly />
          </el-form-item>
        </el-form>
      </el-card>
      <div slot="footer" class="dialog-footer">
        <el-button
          v-if="(editFrom.RSignatureImgPath === null || editFrom.RSignatureImgPath === '')"
          size="small"
          icon="el-icon-close"
          @click="clear"
        >清除簽名
        </el-button>
        <el-button size="small" icon="el-icon-close" @click="cancel">取消</el-button>
        <el-button type="primary" size="small" icon="el-icon-paperclip" @click="saveEditForm()">儲存</el-button>
        <!-- <el-button
          v-if="editFrom.RCate === '內部收據'"
          type="primary"
          size="small"
          icon="el-icon-document"
          @click="generatePDF1()"
        >產生公會代管收據
        </el-button>
        <el-button
          v-if="editFrom.RCate === '內部收據'"
          type="primary"
          size="small"
          icon="el-icon-document"
          @click="generatePDF2()"
        >產生公會包租收據
        </el-button> -->
      </div>
    </el-dialog>
  </div>
</template>

<script>

import { getReceiptListWithPager, getReceiptDetail,
  saveReceipt, setReceiptEnable, deleteSoftReceipt,
  deleteReceipt, getImg, setReceiptStatus, downloadGuildPDF1, downloadGuildPDF2
} from '@/api/chaochi/receipt/receiptservice'
import { GetAllCompanyName } from '@/api/chaochi/slma/slmaservice'
import { GetByLNID } from '@/api/chaochi/landlordnaturalperson/customerlnservice'
import { GetByLCID } from '@/api/chaochi/landlordcomapnyperson/customerlcservice'
import { GetByRNID } from '@/api/chaochi/renter/customerrnservice'
import { validateIDCombo } from '@/utils/validate';
import elDragDialog from '@/directive/el-drag-dialog' // 彈窗可移動
import Address from '@/components/Address/Address.vue'
import { parseTime } from '@/utils/index'
import DatePickerE from '@/components/RocDatepickerE'
import { isNumber } from '@/components/cron/util/tools'
export default {
  name: 'Receipt', // 需與菜單的功能編碼一致
  directives: { elDragDialog },
  components: {
    Address,
    DatePickerE
  },
  data() {
    const checkRPayee = (rule, value, callback) => {
      if (!value) {
        return callback(new Error('請選擇收款方'))
      } else {
        callback()
      }
    }
    const checkRPayeeUnit = (rule, value, callback) => {
      if (!value) {
        return callback(new Error('請選擇兆基收款單位'))
      } else {
        callback()
      }
    }
    const checkRPayer = (rule, value, callback) => {
      if (!value) {
        return callback(new Error('請選擇付款方'))
      } else {
        callback()
      }
    }
    const checkRPayerUnit = (rule, value, callback) => {
      if (!value) {
        return callback(new Error('請選擇兆基付款單位'))
      } else {
        callback()
      }
    }
    return {
      PayOptions: [
        { value: '', label: '無指定' },
        { value: '出租人', label: '出租人' },
        { value: '承租人', label: '承租人' },
        { value: '兆基', label: '兆基' }
      ],
      PayOptions2: [
        { value: '出租人', label: '出租人' },
        { value: '承租人', label: '承租人' },
        { value: '兆基', label: '兆基' }
      ],
      ChaochiUnits: [],
      ChaochiUnits2: [],
      ReceiptCategories: [
        { value: '', label: '無指定' },
        { value: '內部收據', label: '內部收據' },
        { value: '內部領據', label: '內部領據' }
      ],
      ReceiptCategories2: [
        { value: '內部收據', label: '內部收據' },
        { value: '內部領據', label: '內部領據' }
      ],
      ReceiptStatus: [
        { value: '', label: '無指定' },
        { value: '已簽收', label: '已簽收' },
        { value: '已作廢', label: '已作廢' },
        { value: '已入帳', label: '已入帳' }
      ],
      ItemPayOptions: [
        { value: '匯款', label: '匯款' },
        { value: '現金', label: '現金' },
        { value: '信用卡', label: '信用卡' },
        { value: '支票', label: '支票' },
        { value: '保證函', label: '保證函' }
      ],
      sendedform: {},
      searchform: {
        keywords: '',
        RPayee: '',
        RPayeeName: '',
        RPayeeID: '',
        RPayeeTaxID: '',
        RPayeeUnit: '',
        RPayer: '',
        RPayerName: '',
        RPayerID: '',
        RPayerTaxID: '',
        RPayerUnit: '',
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
        RCate: '',
        RStatus: '',
        ReceiptId: '',
        ROverBooking: ''
      },
      tableData: [],
      tableloading: true,
      pagination: {
        currentPage: 1,
        pagesize: 20,
        pageTotal: 0
      },
      categoryDisible: false,
      sortableData: {
        order: 'desc',
        sort: 'CreatorTime'
      },
      dialogEditFormVisible: false,
      editFormTitle: '',
      editFrom: {
        // RSignatureImgPath: '',
        RSignatureBase64: null,
        RSignatureIsEmpty: false
      },
      rules: {
        RPayee: [
          { required: true, validator: checkRPayee, trigger: 'change' }
        ],
        RPayeeName: [
          { required: true, message: '請輸入收款方名稱', trigger: 'blur' }
        ],
        RPayeeID: [
          { required: true, trigger: 'blur', validator: validateIDCombo }
        ],
        RPayeeUnit: [
          { required: true, validator: checkRPayeeUnit, trigger: 'change' }
        ],
        RPayer: [
          { required: true, validator: checkRPayer, trigger: 'change' }
        ],
        RPayerName: [
          { required: true, message: '請輸入付款方名稱', trigger: 'blur' }
        ],
        RPayerID: [
          { required: true, trigger: 'blur', validator: validateIDCombo }
        ],
        RPayerUnit: [
          { required: true, validator: checkRPayerUnit, trigger: 'change' }
        ]
        // PIEquipmentCostMethod: [
        //   { required: true, message: '請輸入收費項目-設備費用收費方式', trigger: 'blur' },
        //   { min: 2, max: 50, message: '長度在 2 到 50 個字符', trigger: 'blur' }
        // ]

      },
      formLabelWidth: '110px',
      dialogLabelWidth: '140px',
      currentId: '', // 當前操作對象的ID值，主要用于修改
      currentSelected: []
    }
  },
  watch: {
    'editFrom.RPayeeID': {
      handler(value) {
        if (value) {
          switch (value.length) {
            case 10:
              if (this.editFrom.RPayee === '出租人') {
                GetByLNID(value).then(res => {
                  this.editFrom.RPayeeName = (res.Success) ? res.ResData : ''
                })
              } else if (this.editFrom.RPayee === '承租人') {
                GetByRNID(value).then(res => {
                  this.editFrom.RPayeeName = (res.Success) ? res.ResData : ''
                })
              }
              break
            case 8:
              GetByLCID(value).then(res => {
                this.editFrom.RPayeeName = (res.Success) ? res.ResData : ''
              })
              break
          }
        }
        // }
      }
    },
    'editFrom.RPayerID': {
      handler(value) {
        if (value) {
          switch (value.length) {
            case 10:
              if (this.editFrom.RPayer === '出租人') {
                GetByLNID(value).then(res => {
                  this.editFrom.RPayerName = (res.Success) ? res.ResData : ''
                })
              } else if (this.editFrom.RPayer === '承租人') {
                GetByRNID(value).then(res => {
                  this.editFrom.RPayerName = (res.Success) ? res.ResData : ''
                })
              }
              break
            case 8:
              GetByLCID(value).then(res => {
                this.editFrom.RPayerName = (res.Success) ? res.ResData : ''
              })
              break
          }
        }
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
      GetAllCompanyName().then(res => {
        this.ChaochiUnits = res.ResData.map(x => x.Name)
        this.ChaochiUnits.unshift('無指定')
        this.ChaochiUnits2 = res.ResData
      })
    },

    // 取消按鈕
    cancel() {
      this.dialogEditFormVisible = false
      this.categoryDisible = false
      this.reset()
    },
    // 表單重置
    reset() {
      this.editFrom = {
        ReceiptId: '',
        RCate: '',
        RDate: '',
        RDate_Y: '',
        RDate_M: '',
        RDate_D: '',
        RPayee: '',
        RPayeeName: '',
        RPayeeUnit: '',
        RPayeeID: '',
        RPayeeTaxID: '',
        RPayeeTel: '',
        RPayer: '',
        RPayerName: '',
        RPayerUnit: '',
        RPayerID: '',
        RPayerTaxID: '',
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
        RStatus: '',
        RTotalCost: '',
        RPaymentMethod: '',
        RMemo: '',
        RSignatureImgPath: '',
        RSignedPDFPath: '',
        RUnsignPDFPath: '',
        RSignatureBase64: null,
        RSignatureIsEmpty: false,
        RSignDate: '',
        ROverBooking: '',
        RSales: '',
        RAccounting: '',
        RPDFPath: '',
        PIRentStartDate: '',
        PIRentEndDate: '',
        PIRent: '',
        PIRentMethod: '',
        PIDepositDate: '',
        PIDepositActual: '',
        PIDepositOwe: '',
        PIDeposit: '',
        PIDepositMethod: '',
        PIManagementFeeMonth: '',
        PIManagementFee: '',
        PIManagementFeeMethod: '',
        PIServiceFee: '',
        PIServiceFeeMethod: '',
        PIConsultantFeeMonth: '',
        PIConsultantFee: '',
        PIConsultantFeeMethod: '',
        PIEquipmentNote: '',
        PIEquipmentCost: '',
        PIEquipmentCostMethod: '',
        PIElectricityBill: '',
        PIElectricityBillMethod: '',
        PIWaterBill: '',
        PIWaterBillMethod: '',
        PIGasFee: '',
        PIGasFeeMethod: '',
        PITrashFee: '',
        PITrashFeeMethod: '',
        PICleaningFee: '',
        PICleaningFeeMethod: '',
        PIAdministrativeFee: '',
        PIAdministrativeFeeMethod: '',
        PIManagementServiceFee: '',
        PIManagementServiceFeeMethod: '',
        PIMiscellaneousFeeNote: '',
        PIMiscellaneousFee: '',
        PIMiscellaneousFeeMethod: '',
        DeleteMark: '',
        CreatorTime: '',
        CreatorUserId: '',
        LastModifyTime: '',
        LastModifyUserId: '',
        DeleteTime: '',
        DeleteUserId: ''

        // 需個性化處理內容
      }
      this.sendedform = { }
      this.resetForm('editFrom')
    },
    /**
     * 加載頁面table數據
     */
    loadTableData: function() {
      this.tableloading = true
      var t = {
        RPayee: this.searchform.RPayee,
        RPayeeName: this.searchform.RPayeeName,
        RPayeeID: this.searchform.RPayeeID,
        RPayeeTaxID: this.searchform.RPayeeTaxID,
        RPayeeUnit: this.searchform.RPayeeUnit,
        RPayer: this.searchform.RPayer,
        RPayerName: this.searchform.RPayerName,
        RPayerID: this.searchform.RPayerID,
        RPayerTaxID: this.searchform.RPayerTaxID,
        RPayerUnit: this.searchform.RPayerUnit,
        RAdd_1: this.searchform.RAdd_1,
        RAdd_1_1: this.searchform.RAdd_1_1,
        RAdd_1_2: this.searchform.RAdd_1_2,
        RAdd_2: this.searchform.RAdd_2,
        RAdd_2_1: this.searchform.RAdd_2_1,
        RAdd_2_2: this.searchform.RAdd_2_2,
        RAdd_2_3: this.searchform.RAdd_2_3,
        RAdd_2_4: this.searchform.RAdd_2_4,
        RAdd_3: this.searchform.RAdd_3,
        RAdd_3_1: this.searchform.RAdd_3_1,
        RAdd_3_2: this.searchform.RAdd_3_2,
        RAdd_4: this.searchform.RAdd_4,
        RAdd_5: this.searchform.RAdd_5,
        RAdd_6: this.searchform.RAdd_6,
        RAdd_7: this.searchform.RAdd_7,
        RAdd_8: this.searchform.RAdd_8,
        RAdd_9: this.searchform.RAdd_9,
        RCate: this.searchform.RCate,
        RStatus: this.searchform.RStatus,
        ReceiptId: this.searchform.ReceiptId,
        ROverBooking: this.searchform.ROverBooking
      }

      var seachdata = {
        CurrenetPageIndex: this.pagination.currentPage,
        PageSize: this.pagination.pagesize,
        Keywords: this.searchform.keywords,
        Order: this.sortableData.order,
        Sort: this.sortableData.sort,
        Filter: t
      }
      getReceiptListWithPager(seachdata).then(res => {
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
     * 更改領收據狀態
     */
    changeReceiptStatus: function(status) {
      if (status !== undefined) {
        if (this.currentSelected.length > 1 || this.currentSelected.length === 0) {
          this.$alert('請選擇一筆資料進行編輯/修改', '提示')
        } else {
          this.currentId = this.currentSelected[0].Id
          setReceiptStatus(this.currentId, status).then(res => {
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
      }
    },
    /**
     * 新增、修改或查看明細信息（綁定顯示數據）     *
     */
    ShowEditOrViewDialog: function(view) {
      this.reset()
      if (view !== undefined) {
        if (this.currentSelected.length > 1 || this.currentSelected.length === 0) {
          this.$alert('請選擇一筆資料進行編輯/修改', '提示')
        } else {
          this.currentId = this.currentSelected[0].Id
          this.editFormTitle = '編輯'
          this.dialogEditFormVisible = true
          this.categoryDisible = true
          this.bindEditInfo()
        }
      } else {
        this.editFormTitle = '新增'
        this.currentId = ''
        this.dialogEditFormVisible = true
        this.editFrom.RStatus = '已簽收'
      }
    },
    bindEditInfo: function() {
      getReceiptDetail(this.currentId).then(res => {
        this.editFrom = res.ResData
        // 需個性化處理內容

        // 處理地址
        this.sendedform = {
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

        // 處理簽名檔
        if (this.editFrom.RSignatureImgPath) {
          getImg(this.editFrom.RSignatureImgPath).then((response) => {
            this.editFrom.RSignatureImgPath = URL.createObjectURL(response)
          })
        }
      })
    },
    /**
     * 新增/修改保存
     */
    saveEditForm() {
      this.$refs['editFrom'].validate((valid) => {
        if (valid) {
          const formData = {
            'ReceiptId': this.editFrom.ReceiptId,
            'RCate': this.editFrom.RCate,
            'RDate': this.editFrom.RDate,
            'RDate_Y': this.editFrom.RDate_Y,
            'RDate_M': this.editFrom.RDate_M,
            'RDate_D': this.editFrom.RDate_D,
            'RPayee': this.editFrom.RPayee,
            'RPayeeName': this.editFrom.RPayeeName,
            'RPayeeUnit': this.editFrom.RPayeeUnit,
            'RPayeeID': this.editFrom.RPayeeID,
            'RPayeeTaxID': this.editFrom.RPayeeTaxID,
            'RPayeeTel': this.editFrom.RPayeeTel,
            'RPayer': this.editFrom.RPayer,
            'RPayerName': this.editFrom.RPayerName,
            'RPayerUnit': this.editFrom.RPayerUnit,
            'RPayerID': this.editFrom.RPayerID,
            'RPayerTaxID': this.editFrom.RPayerTaxID,
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
            'RStatus': this.editFrom.RStatus,
            'RTotalCost': this.editFrom.RTotalCost.toString(),
            'RPaymentMethod': this.editFrom.RPaymentMethod,
            'RMemo': this.editFrom.RMemo,
            // 'RSignatureBase64': null,
            // 'RSignatureIsEmpty': null,
            'RSignDate': this.editFrom.RSignDate,
            'ROverBooking': this.editFrom.ROverBooking,
            'RSales': this.editFrom.RSales,
            'RAccounting': this.editFrom.RAccounting,
            'RPDFPath': this.editFrom.RPDFPath,
            'PIRentStartDate': this.editFrom.PIRentStartDate,
            'PIRentEndDate': this.editFrom.PIRentEndDate,
            'PIRent': this.editFrom.PIRent.toString(),
            'PIRentMethod': this.editFrom.PIRentMethod,
            'PIDepositDate': this.editFrom.PIDepositDate.toString(),
            'PIDepositActual': this.editFrom.PIDepositActual,
            'PIDepositOwe': this.editFrom.PIDepositOwe,
            'PIDeposit': this.editFrom.PIDeposit.toString(),
            'PIDepositMethod': this.editFrom.PIDepositMethod,
            'PIManagementFeeMonth': this.editFrom.PIManagementFeeMonth,
            'PIManagementFee': this.editFrom.PIManagementFee.toString(),
            'PIManagementFeeMethod': this.editFrom.PIManagementFeeMethod,
            'PIServiceFee': this.editFrom.PIServiceFee.toString(),
            'PIServiceFeeMethod': this.editFrom.PIServiceFeeMethod,
            'PIConsultantFeeMonth': this.editFrom.PIConsultantFeeMonth,
            'PIConsultantFee': this.editFrom.PIConsultantFee.toString(),
            'PIConsultantFeeMethod': this.editFrom.PIConsultantFeeMethod,
            'PIEquipmentNote': this.editFrom.PIEquipmentNote,
            'PIEquipmentCost': this.editFrom.PIEquipmentCost.toString(),
            'PIEquipmentCostMethod': this.editFrom.PIEquipmentCostMethod,
            'PIElectricityBill': this.editFrom.PIElectricityBill.toString(),
            'PIElectricityBillMethod': this.editFrom.PIElectricityBillMethod,
            'PIWaterBill': this.editFrom.PIWaterBill.toString(),
            'PIWaterBillMethod': this.editFrom.PIWaterBillMethod,
            'PIGasFee': this.editFrom.PIGasFee.toString(),
            'PIGasFeeMethod': this.editFrom.PIGasFeeMethod,
            'PITrashFee': this.editFrom.PITrashFee.toString(),
            'PITrashFeeMethod': this.editFrom.PITrashFeeMethod,
            'PICleaningFee': this.editFrom.PICleaningFee.toString(),
            'PICleaningFeeMethod': this.editFrom.PICleaningFeeMethod,
            'PIAdministrativeFee': this.editFrom.PIAdministrativeFee.toString(),
            'PIAdministrativeFeeMethod': this.editFrom.PIAdministrativeFeeMethod,
            'PIManagementServiceFee': this.editFrom.PIManagementServiceFee.toString(),
            'PIManagementServiceFeeMethod': this.editFrom.PIManagementServiceFeeMethod,
            'PIMiscellaneousFeeNote': this.editFrom.PIMiscellaneousFeeNote,
            'PIMiscellaneousFee': this.editFrom.PIMiscellaneousFee.toString(),
            'PIMiscellaneousFeeMethod': this.editFrom.PIMiscellaneousFeeMethod,
            'DeleteMark': this.editFrom.DeleteMark,
            'CreatorTime': this.editFrom.CreatorTime,
            'CreatorUserId': this.editFrom.CreatorUserId,
            'LastModifyTime': this.editFrom.LastModifyTime,
            'LastModifyUserId': this.editFrom.LastModifyUserId,
            'DeleteTime': this.editFrom.DeleteTime,
            'DeleteUserId': this.editFrom.DeleteUserId,

            'Id': this.currentId
          }

          if (this.editFrom.RSignatureImgPath === null || this.editFrom.RSignatureImgPath === '') {
            const { isEmpty, data } = this.$refs.signaturePad.saveSignature()
            formData.RSignatureIsEmpty = isEmpty
            formData.RSignatureBase64 = data
            // 簽名日期
            formData.RSignDate = parseTime(new Date(), '{c}-{m}-{d}')
          }

          var url = 'Receipt/Insert'
          if (this.currentId !== '') {
            url = 'Receipt/Update'
          }
          saveReceipt(formData, url).then(res => {
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
    setEnable: function(val) {
      if (this.currentSelected.length === 0) {
        this.$alert('請先選擇要操作的數據', '提示')
        return false
      } else {
        var currentIds = []
        this.currentSelected.forEach(element => {
          currentIds.push(element.Id)
        })
        const data = {
          Ids: currentIds,
          Flag: val
        }
        setReceiptEnable(data).then(res => {
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
    deleteSoft: function(val) {
      if (this.currentSelected.length === 0) {
        this.$alert('請先選擇要操作的數據', '提示')
        return false
      } else {
        const that = this
        this.$confirm('是否確認刪除所選的數據項?', '警告', {
          confirmButtonText: '確定',
          cancelButtonText: '取消',
          type: 'warning'
        }).then(function() {
          var currentIds = []
          that.currentSelected.forEach(element => {
            currentIds.push(element.Id)
          })
          const data = {
            Ids: currentIds,
            Flag: val
          }
          return deleteSoftReceipt(data)
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
    deletePhysics: function() {
      if (this.currentSelected.length === 0) {
        this.$alert('請先選擇要操作的數據', '提示')
        return false
      } else {
        const that = this
        this.$confirm('是否確認刪除所選的數據項?', '警告', {
          confirmButtonText: '確定',
          cancelButtonText: '取消',
          type: 'warning'
        }).then(function() {
          var currentIds = []
          that.currentSelected.forEach(element => {
            currentIds.push(element.Id)
          })
          const data = {
            Ids: currentIds
          }
          return deleteReceipt(data)
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
      this.searchform.RAdd_1 = addressData.Add_1
      this.searchform.RAdd_1_1 = addressData.Add_1_1
      this.searchform.RAdd_1_2 = addressData.Add_1_2
      this.searchform.RAdd_2 = addressData.Add_2
      this.searchform.RAdd_2_1 = addressData.Add_2_1
      this.searchform.RAdd_2_2 = addressData.Add_2_2
      this.searchform.RAdd_2_3 = addressData.Add_2_3
      this.searchform.RAdd_2_4 = addressData.Add_2_4
      this.searchform.RAdd_3 = addressData.Add_3
      this.searchform.RAdd_3_1 = addressData.Add_3_1
      this.searchform.RAdd_3_2 = addressData.Add_3_2
      this.searchform.RAdd_4 = addressData.Add_4
      this.searchform.RAdd_5 = addressData.Add_5
      this.searchform.RAdd_6 = addressData.Add_6
      this.searchform.RAdd_7 = addressData.Add_7
      this.searchform.RAdd_8 = addressData.Add_8
      this.searchform.RAdd_9 = addressData.Add_9
    },
    geteditaddress(addressData) {
      this.editFrom.RAdd_1 = addressData.Add_1
      this.editFrom.RAdd_1_1 = addressData.Add_1_1
      this.editFrom.RAdd_1_2 = addressData.Add_1_2
      this.editFrom.RAdd_2 = addressData.Add_2
      this.editFrom.RAdd_2_1 = addressData.Add_2_1
      this.editFrom.RAdd_2_2 = addressData.Add_2_2
      this.editFrom.RAdd_2_3 = addressData.Add_2_3
      this.editFrom.RAdd_2_4 = addressData.Add_2_4
      this.editFrom.RAdd_3 = addressData.Add_3
      this.editFrom.RAdd_3_1 = addressData.Add_3_1
      this.editFrom.RAdd_3_2 = addressData.Add_3_2
      this.editFrom.RAdd_4 = addressData.Add_4
      this.editFrom.RAdd_5 = addressData.Add_5
      this.editFrom.RAdd_6 = addressData.Add_6
      this.editFrom.RAdd_7 = addressData.Add_7
      this.editFrom.RAdd_8 = addressData.Add_8
      this.editFrom.RAdd_9 = addressData.Add_9
    },
    disableCategory() {
      this.categoryDisible = true
      this.editFrom.ReceiptId = this.generateId()
      this.editFrom.RDate = parseTime(new Date(), '{c}-{m}-{d}')
    },
    generateId() {
      const ms = new Date().getMilliseconds()
      return 'R' + Math.ceil(Math.random() * 10000000) + ms
    },
    clear() {
      this.$refs.signaturePad.clearSignature()
    },
    calcTotalCost() {
      this.editFrom.RTotalCost = 0
      if (isNumber(this.editFrom.PIRent)) {
        this.editFrom.RTotalCost += parseInt(this.editFrom.PIRent)
      }
      if (isNumber(this.editFrom.PIDeposit)) {
        this.editFrom.RTotalCost += parseInt(this.editFrom.PIDeposit)
      }
      if (isNumber(this.editFrom.PIManagementFee)) {
        this.editFrom.RTotalCost += parseInt(this.editFrom.PIManagementFee)
      }
      if (isNumber(this.editFrom.PIManagementFee)) {
        this.editFrom.RTotalCost += parseInt(this.editFrom.PIManagementFee)
      }
      if (isNumber(this.editFrom.PIServiceFee)) {
        this.editFrom.RTotalCost += parseInt(this.editFrom.PIServiceFee)
      }
      if (isNumber(this.editFrom.PIConsultantFee)) {
        this.editFrom.RTotalCost += parseInt(this.editFrom.PIConsultantFee)
      }
      if (isNumber(this.editFrom.PIEquipmentCost)) {
        this.editFrom.RTotalCost += parseInt(this.editFrom.PIEquipmentCost)
      }
      if (isNumber(this.editFrom.PIElectricityBill)) {
        this.editFrom.RTotalCost += parseInt(this.editFrom.PIElectricityBill)
      }
      if (isNumber(this.editFrom.PIWaterBill)) {
        this.editFrom.RTotalCost += parseInt(this.editFrom.PIWaterBill)
      }
      if (isNumber(this.editFrom.PIGasFee)) {
        this.editFrom.RTotalCost += parseInt(this.editFrom.PIGasFee)
      }
      if (isNumber(this.editFrom.PITrashFee)) {
        this.editFrom.RTotalCost += parseInt(this.editFrom.PITrashFee)
      }
      if (isNumber(this.editFrom.PICleaningFee)) {
        this.editFrom.RTotalCost += parseInt(this.editFrom.PICleaningFee)
      }
      if (isNumber(this.editFrom.PIAdministrativeFee)) {
        this.editFrom.RTotalCost += parseInt(this.editFrom.PIAdministrativeFee)
      }
      if (isNumber(this.editFrom.PIManagementServiceFee)) {
        this.editFrom.RTotalCost += parseInt(this.editFrom.PIManagementServiceFee)
      }
      if (isNumber(this.editFrom.PIMiscellaneousFee)) {
        this.editFrom.RTotalCost += parseInt(this.editFrom.PIMiscellaneousFee)
      }
    },
    calcDeposit() {
      this.editFrom.PIDeposit = parseInt(this.editFrom.PIRent) * 2
      this.calcTotalCost()
    },
    generatePDF1: function() {
      const data = {
        'RDate': this.editFrom.RDate,
        'RPayer': this.editFrom.RPayer,
        'RPayerName': this.editFrom.RPayerName,
        'RPayerID': this.editFrom.RPayerID,

        'PIRentStartDate': this.editFrom.PIRentStartDate,
        'PIRentEndDate': this.editFrom.PIRentEndDate,
        'PIRent': this.editFrom.PIRent,
        'Id': this.currentId
      }
      const label = 'B031301'
      // this.tableloading = true

      downloadGuildPDF1(data).then(res => {
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
    generatePDF2: function() {
      const data = {
        'RDate': this.editFrom.RDate,
        'RPayer': this.editFrom.RPayer,
        'RPayerUnit': this.editFrom.RPayerUnit,
        'RPayerTaxID': this.editFrom.RPayerTaxID,
        'PIRentStartDate': this.editFrom.PIRentStartDate,
        'PIRentEndDate': this.editFrom.PIRentEndDate,
        'PIRent': this.editFrom.PIRent,
        'Id': this.currentId
      }
      const label = 'B031201'
      // this.tableloading = true

      downloadGuildPDF2(data).then(res => {
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
    downloadUnsignPDF: function(row) {
      if (!row.RUnsignPDFPath) {
        this.$alert('無法下載未用印PDF', '下載失敗', {
          confirmButtonText: '確定'
        })
        return false
      }
      const fileName = row.ReceiptId + '_未用印'
      getImg(row.RUnsignPDFPath).then(res => {
        const blob = res
        const link = document.createElement('a')
        link.href = URL.createObjectURL(blob)
        link.download = fileName
        link.click()
        URL.revokeObjectURL(link.href)
      }).catch(error => {
        console.log(error)
      })
    },
    downloadSignedPDF: function(row) {
      if (!row.RSignedPDFPath) {
        this.$alert('無法下載用印PDF', '下載失敗', {
          confirmButtonText: '確定'
        })
        return false
      }
      const fileName = row.ReceiptId + '_用印'
      getImg(row.RSignedPDFPath).then(res => {
        const blob = res
        const link = document.createElement('a')
        link.href = URL.createObjectURL(blob)
        link.download = fileName
        link.click()
        URL.revokeObjectURL(link.href)
      }).catch(error => {
        console.log(error)
      })
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
        this.ShowEditOrViewDialog('edit')
      })
    }
  }
}
</script>

<style>
</style>
