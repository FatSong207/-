<template>
  <div>
    <el-form
      ref="editForm"
      v-loading="saveLoading"
      element-loading-text="儲存中..."
      :inline="true"
      :model="editForm"
      class="demo-form-inline"
      :rules="rule"
    >
      <el-row>
        <el-col :span="12">
          <el-form-item
            label="建物門牌地址："
            label-width="135px" prop="BAdd"
          >
            <el-input
              v-model="editForm.BAdd"
              autocomplete="off"
              clearable
              style="width: 410px"
              readonly
            />
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item
            label="主建物：1."
            :label-width="formLabelWidth"
            style="width: 100%"
          >
            <el-input
              v-model="editForm.BFloor_1"
              autocomplete="off"
              clearable
              style="width: 75px"
            />
            層
            <el-input
              v-model="editForm.BArea_1"
              autocomplete="off"
              clearable
              style="width: 100px"
            />
            平方公尺／
            <el-input
              v-model="editForm.BPing_1"
              autocomplete="off"
              clearable
              style="width: 95px"
              disabled
            />坪
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="12">
          <el-form-item
            label="建物所屬出租人："
            label-width="135px"
            prop="BelongLandlord"
          >
            <el-input
              v-model="editForm.BelongLandlord"
              readonly
              autocomplete="off"
              clearable
            />
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item
            label="2."
            :label-width="formLabelWidth"
            style="width: 100%"
          >
            <el-input
              v-model="editForm.BFloor_2"
              autocomplete="off"
              clearable
              style="width: 75px"
            />
            層
            <el-input
              v-model="editForm.BArea_2"
              autocomplete="off"
              clearable
              style="width: 100px"
            />
            平方公尺／
            <el-input
              v-model="editForm.BPing_2"
              autocomplete="off"
              clearable
              style="width: 95px"
              disabled
            />坪
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="6">
          <el-form-item
            label="建築完成日期："
            label-width="135px"
            style="width: 100%"
            prop="BCDate"
          >
            <DatePickerE
              v-model="editForm.BCDate"
              value-format="yyyy-MM-dd"
              format="yyyy-MM-dd"
              type="date"
              placeholder="選擇日期"
            />
          </el-form-item>
        </el-col>
        <el-col :span="6">
          <el-form-item
            label="屋齡："
            :label-width="formLabelWidth"
            style="width: 100%"
          >
            <el-input
              v-model="editForm.BAge"
              autocomplete="off"
              style="width: 100px"
            />
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item
            label="3."
            :label-width="formLabelWidth"
            style="width: 100%"
          >
            <el-input
              v-model="editForm.BFloor_3"
              autocomplete="off"
              clearable
              style="width: 75px"
            />
            層
            <el-input
              v-model="editForm.BArea_3"
              autocomplete="off"
              clearable
              style="width: 100px"
            />
            平方公尺／
            <el-input
              v-model="editForm.BPing_3"
              autocomplete="off"
              clearable
              style="width: 95px"
              disabled
            />坪
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="6">
          <el-form-item
            label="稅籍編號："
            label-width="135px"
          >
            <el-input
              v-model="building1.B1TaxID"
              autocomplete="off"
              clearable
            />
          </el-form-item>
        </el-col>
        <el-col :span="6">
          <el-form-item
            label="房屋型態："
            :label-width="formLabelWidth"
            style="width: 100%"
          >
            <el-select
              v-model="typeValue"
              placeholder="請選擇"
            >
              <el-option
                v-for="item in typeOptions"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              />
            </el-select>
          </el-form-item>
        </el-col>
        <el-col :span="6">
          <el-form-item
            label="房屋格局："
            :label-width="formLabelWidth"
            style="width: 100%"
          >
            <el-select
              v-model="patternValue"
              placeholder="请选择"
            >
              <el-option
                v-for="item in patternOptions"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              />
            </el-select>
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="8">
          <el-form-item
            label="隔間："
            label-width="135px"
            style="width: 100%"
            class="el-form-item is-required el-form-item--medium"
          >
            <el-form-item prop="BRoom">
              <el-input
                v-model="editForm.BRoom"
                autocomplete="off"
                style="width: 70px"
              />房
            </el-form-item>
            <el-form-item prop="BLD">
              <el-input
                v-model="editForm.BLD"
                autocomplete="off"
                style="width: 70px"
              />廳
            </el-form-item>

            <el-form-item prop="BBath">
              <el-input
                v-model="editForm.BBath"
                autocomplete="off"
                style="width: 70px"
              />衛
            </el-form-item>
            <el-form-item prop="BBalcony">
              <el-input
                v-model="editForm.BBalcony"
                autocomplete="off"
                style="width: 70px"
              />陽台
            </el-form-item>
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="6">
          <el-form-item
            label="樓層："
            label-width="135px"
            style="width: 100%"
            class="el-form-item is-required el-form-item--medium"
          >
            <el-form-item
              label="層數"
              prop="BLevel"
            >
              <el-input
                v-model="editForm.BLevel"
                autocomplete="off"
                clearable
                style="width: 70px"
              />
            </el-form-item>
            ／
            <el-form-item
              label="層次"
              prop="BFloor"
            >
              <el-input
                v-model="editForm.BFloor"
                autocomplete="off"
                clearable
                style="width: 70px"
              />
            </el-form-item>
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="12">
          <el-form-item
            label="建物建號："
            label-width="135px" prop="AccountX"
          >
            <el-input
              v-model="editForm.BNo_1"
              autocomplete="off"
              clearable
              style="width: 80px"
            />段
            <el-input
              v-model="editForm.BNo_2"
              autocomplete="off"
              clearable
              style="width: 80px"
            />小段
            <el-input
              v-model="editForm.BNo_3"
              autocomplete="off"
              clearable
              style="width: 165px"
            />建號
          </el-form-item>
        </el-col>
        <el-col :span="8">
          <el-form-item
            label="附屬建物：1."
            :label-width="formLabelWidth"
          >
            <el-select
              v-model="editForm.BAttachBuilding_1"
              placeholder="請選擇"
              style="width: 100px"
            >
              <el-option
                v-for="item in AttachBuildingOptions"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              />
            </el-select>
            ：
            <el-input
              v-model="editForm.BAttachBuildingArea_1"
              autocomplete="off"
              clearable
              style="width: 110px"
            />
            平方公尺／
            <el-input
              v-model="editForm.BAttachBuildingPing_1"
              autocomplete="off"
              clearable
              style="width: 95px"
              disabled
            />坪
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="12">
          <el-form-item
            label="建物地號1："
            label-width="135px"
          >
            <el-input
              v-model="editForm.BPNo_1_A"
              autocomplete="off"
              clearable
              style="width: 80px"
            />段
            <el-input
              v-model="editForm.BPNo_2_A"
              autocomplete="off"
              clearable
              style="width: 80px"
            />小段
            <el-input
              v-model="editForm.BPNo_3_A"
              autocomplete="off"
              clearable
              style="width: 165px"
            />地號
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item
            label="2."
            :label-width="formLabelWidth"
          >
            <el-select
              v-model="editForm.BAttachBuilding_2"
              placeholder="請選擇"
              style="width: 100px"
            >
              <el-option
                v-for="item in AttachBuildingOptions"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              />
            </el-select>
            ：
            <el-input
              v-model="editForm.BAttachBuildingArea_2"
              autocomplete="off"
              clearable
              style="width: 110px"
            />
            平方公尺／
            <el-input
              v-model="editForm.BAttachBuildingPing_2"
              autocomplete="off"
              clearable
              style="width: 95px"
              disabled
            />坪
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="12">
          <el-form-item
            label="建物地號2："
            label-width="135px"
          >
            <el-input
              v-model="editForm.BPNo_1_B"
              autocomplete="off"
              clearable
              style="width: 80px"
            />段
            <el-input
              v-model="editForm.BPNo_2_B"
              autocomplete="off"
              clearable
              style="width: 80px"
            />小段
            <el-input
              v-model="editForm.BPNo_3_B"
              autocomplete="off"
              clearable
              style="width: 165px"
            />地號
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item
            label="3."
            :label-width="formLabelWidth"
          >
            <el-select
              v-model="editForm.BAttachBuilding_3"
              placeholder="請選擇"
              style="width: 100px"
            >
              <el-option
                v-for="item in AttachBuildingOptions"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              />
            </el-select>
            ：
            <el-input
              v-model="editForm.BAttachBuildingArea_3"
              autocomplete="off"
              clearable
              style="width: 110px"
            />
            平方公尺／
            <el-input
              v-model="editForm.BAttachBuildingPing_3"
              autocomplete="off"
              clearable
              style="width: 95px"
              disabled
            />坪
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="12">&nbsp;</el-col>
        <el-col :span="12">
          <el-form-item
            label="4."
            :label-width="formLabelWidth"
          >
            <el-select
              v-model="editForm.BAttachBuilding_4"
              placeholder="請選擇"
              style="width: 100px"
            >
              <el-option
                v-for="item in AttachBuildingOptions"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              />
            </el-select>
            ：
            <el-input
              v-model="editForm.BAttachBuildingArea_4"
              autocomplete="off"
              clearable
              style="width: 110px"
            />
            平方公尺／
            <el-input
              v-model="editForm.BAttachBuildingPing_4"
              autocomplete="off"
              clearable
              style="width: 95px"
              disabled
            />坪
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="12">&nbsp;</el-col>
        <el-col :span="12">
          <el-form-item
            label="5."
            :label-width="formLabelWidth"
          >
            <el-select
              v-model="editForm.BAttachBuilding_5"
              placeholder="請選擇"
              style="width: 100px"
            >
              <el-option
                v-for="item in AttachBuildingOptions"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              />
            </el-select>
            ：
            <el-input
              v-model="editForm.BAttachBuildingArea_5"
              autocomplete="off"
              clearable
              style="width: 110px"
            />
            平方公尺／
            <el-input
              v-model="editForm.BAttachBuildingPing_5"
              autocomplete="off"
              clearable
              style="width: 95px"
              disabled
            />坪
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="24">
          <el-form-item
            label="共有部分：1."
            label-width="135px"
          >
            <el-form-item prop="BJoin_1">
              <el-select
                v-model="editForm.BJoin_1"
                placeholder="請選擇"
                style="width: 120px"
                clearable
              >
                <el-option
                  v-for="item in joinOptions"
                  :key="item.value"
                  :label="item.label"
                  :value="item.value"
                />
              </el-select>
            </el-form-item>

            <el-form-item
              label="建號"
              prop="BJoinPno_1"
            >
              <el-input
                v-model="editForm.BJoinPno_1"
                autocomplete="off"
                clearable
                style="width: 160px"
              />
            </el-form-item>

            <el-form-item
              label="面積"
              prop="BJoinArea_1"
            >
              <el-input
                v-model="editForm.BJoinArea_1"
                autocomplete="off"
                clearable
                style="width: 95px"
              /> </el-form-item>平方公尺，
            <el-form-item label="權利範圍">
              <el-input
                v-model="editForm.BJoinOwner1_1"
                autocomplete="off"
                clearable
                style="width: 100px"
                onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
              />
              ／
              <el-input
                v-model="editForm.BJoinOwner2_1"
                autocomplete="off"
                clearable
                style="width: 100px"
                onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
              />
            </el-form-item>
            <el-form-item
              label="權利面積"
              prop="BJoinOwnerArea_1"
            >
              <el-input
                v-model="editForm.BJoinOwnerArea_1"
                autocomplete="off"
                clearable
                style="width: 110px"
                disabled
              />
              平方公尺／
              <el-input
                v-model="editForm.BJoinOwnerPing_1"
                autocomplete="off"
                clearable
                style="width: 95px"
                disabled
              />坪
            </el-form-item>
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="24">
          <el-form-item
            label="2."
            label-width="135px"
          >
            <el-form-item>
              <el-select
                v-model="editForm.BJoin_2"
                placeholder="請選擇"
                style="width: 120px"
                clearable
              >
                <el-option
                  v-for="item in joinOptions"
                  :key="item.value"
                  :label="item.label"
                  :value="item.value"
                />
              </el-select>
            </el-form-item>
            <el-form-item
              label="建號"
              prop="BJoinPno_2"
            >
              <el-input
                v-model="editForm.BJoinPno_2"
                autocomplete="off"
                clearable
                style="width: 160px"
              />
            </el-form-item>
            <el-form-item
              label="面積"
              prop="BJoinArea_2"
            >
              <el-input
                v-model="editForm.BJoinArea_2"
                autocomplete="off"
                clearable
                style="width: 95px"
              /> </el-form-item>平方公尺，
            <el-form-item label="權利範圍">
              <el-input
                v-model="editForm.BJoinOwner1_2"
                autocomplete="off"
                clearable
                style="width: 100px"
                onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
              />
              ／
              <el-input
                v-model="editForm.BJoinOwner2_2"
                autocomplete="off"
                clearable
                style="width: 100px"
                onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
              />
            </el-form-item>
            <el-form-item label="權利面積">
              <el-input
                v-model="editForm.BJoinOwnerArea_2"
                autocomplete="off"
                clearable
                style="width: 110px"
                disabled
              />
              平方公尺／
              <el-input
                v-model="editForm.BJoinOwnerPing_2"
                autocomplete="off"
                clearable
                style="width: 95px"
                disabled
              />坪
            </el-form-item>
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="24">
          <el-form-item
            label="3."
            label-width="135px"
          >
            <el-form-item>
              <el-select
                v-model="editForm.BJoin_3"
                placeholder="請選擇"
                style="width: 120px"
                clearable
              >
                <el-option
                  v-for="item in joinOptions"
                  :key="item.value"
                  :label="item.label"
                  :value="item.value"
                />
              </el-select>
            </el-form-item>
            <el-form-item
              label="建號"
              prop="BJoinPno_3"
            >
              <el-input
                v-model="editForm.BJoinPno_3"
                autocomplete="off"
                clearable
                style="width: 160px"
              />
            </el-form-item>
            <el-form-item
              label="面積"
              prop="BJoinArea_3"
            >
              <el-input
                v-model="editForm.BJoinArea_3"
                autocomplete="off"
                clearable
                style="width: 95px"
              /> </el-form-item>平方公尺，
            <el-form-item label="權利範圍">
              <el-input
                v-model="editForm.BJoinOwner1_3"
                autocomplete="off"
                clearable
                style="width: 100px"
                onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
              />
              ／
              <el-input
                v-model="editForm.BJoinOwner2_3"
                autocomplete="off"
                clearable
                style="width: 100px"
                onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
              />
            </el-form-item>
            <el-form-item label="權利面積">
              <el-input
                v-model="editForm.BJoinOwnerArea_3"
                autocomplete="off"
                clearable
                style="width: 110px"
                disabled
              />
              平方公尺／
              <el-input
                v-model="editForm.BJoinOwnerPing_3"
                autocomplete="off"
                clearable
                style="width: 95px"
                disabled
              />坪
            </el-form-item>
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="24">
          <el-form-item
            label="4."
            label-width="135px"
          >
            <el-form-item>
              <el-select
                v-model="editForm.BJoin_4"
                placeholder="請選擇"
                style="width: 120px"
                clearable
              >
                <el-option
                  v-for="item in joinOptions"
                  :key="item.value"
                  :label="item.label"
                  :value="item.value"
                />
              </el-select>
            </el-form-item>
            <el-form-item
              label="建號"
              prop="BJoinPno_4"
            >
              <el-input
                v-model="editForm.BJoinPno_4"
                autocomplete="off"
                clearable
                style="width: 160px"
              />
            </el-form-item>
            <el-form-item
              label="面積"
              prop="BJoinArea_4"
            >
              <el-input
                v-model="editForm.BJoinArea_4"
                autocomplete="off"
                clearable
                style="width: 95px"
              /> </el-form-item>平方公尺，
            <el-form-item label="權利範圍">
              <el-input
                v-model="editForm.BJoinOwner1_4"
                autocomplete="off"
                clearable
                style="width: 100px"
                onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
              />
              ／
              <el-input
                v-model="editForm.BJoinOwner2_4"
                autocomplete="off"
                clearable
                style="width: 100px"
                onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
              />
            </el-form-item>
            <el-form-item label="權利面積">
              <el-input
                v-model="editForm.BJoinOwnerArea_4"
                autocomplete="off"
                clearable
                style="width: 110px"
                disabled
              />
              平方公尺／
              <el-input
                v-model="editForm.BJoinOwnerPing_4"
                autocomplete="off"
                clearable
                style="width: 95px"
                disabled
              />坪
            </el-form-item>
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="24">
          <el-form-item
            label="5."
            label-width="135px"
          >
            <el-form-item>
              <el-select
                v-model="editForm.BJoin_5"
                placeholder="請選擇"
                style="width: 120px"
                clearable
              >
                <el-option
                  v-for="item in joinOptions"
                  :key="item.value"
                  :label="item.label"
                  :value="item.value"
                />
              </el-select>
            </el-form-item>
            <el-form-item
              label="建號"
              prop="BJoinPno_5"
            >
              <el-input
                v-model="editForm.BJoinPno_5"
                autocomplete="off"
                clearable
                style="width: 160px"
              />
            </el-form-item>
            <el-form-item
              label="面積"
              prop="BJoinArea_5"
            >
              <el-input
                v-model="editForm.BJoinArea_5"
                autocomplete="off"
                clearable
                style="width: 95px"
              /> </el-form-item>平方公尺，
            <el-form-item label="權利範圍">
              <el-input
                v-model="editForm.BJoinOwner1_5"
                autocomplete="off"
                clearable
                style="width: 100px"
                onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
              />
              ／
              <el-input
                v-model="editForm.BJoinOwner2_5"
                autocomplete="off"
                clearable
                style="width: 100px"
                onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
              />
            </el-form-item>
            <el-form-item label="權利面積">
              <el-input
                v-model="editForm.BJoinOwnerArea_5"
                autocomplete="off"
                clearable
                style="width: 110px"
                disabled
              />
              平方公尺／
              <el-input
                v-model="editForm.BJoinOwnerPing_5"
                autocomplete="off"
                clearable
                style="width: 95px"
                disabled
              />坪
            </el-form-item>
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="8">
          <el-form-item
            label="實際坪數共計："
            label-width="135px"
            style="width: 100%"
            class="el-form-item is-required el-form-item--medium"
          >
            <el-form-item prop="BArea">
              <el-input
                v-model="editForm.BArea"
                autocomplete="off"
                style="width: 110px"
              />
              平方公尺／
            </el-form-item>
            <el-form-item>
              <el-input
                v-model="editForm.BPing"
                autocomplete="off"
                clearable
                style="width: 95px"
                disabled
              />坪
            </el-form-item>
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="12">
          <el-form-item
            label="權狀坪數共計："
            label-width="135px"
            style="width: 100%"
          >
            <el-form-item>
              <el-input
                v-model="barea"
                autocomplete="off"
                clearable
                style="width: 110px"
                disabled
              />
              平方公尺／
            </el-form-item>
            <el-form-item>
              <el-input
                v-model="editForm.BRealPING"
                autocomplete="off"
                clearable
                style="width: 95px"
                disabled
              />坪
            </el-form-item>
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="6">
          <el-form-item
            label="用途："
            label-width="135px" style="width: 100%"
          >
            <el-input
              v-model="editForm.BUse"
              autocomplete="off" clearable
            />
          </el-form-item>
        </el-col>
      </el-row>
    </el-form>
    <div class="rightbtn">
      <el-button
        v-hasPermi="['Building/Edit']"
        type="primary"
        size="small"
        icon="el-icon-paperclip"
        @click="saveBuildingRentBasic()"
      >儲存</el-button>
      <el-button
        size="small" icon="el-icon-close" @click="cancel"
      >關閉</el-button>
    </div>
  </div>
</template>

<script>
import DatePickerE from '@/components/RocDatepickerE';
import { UpdateBuildingRentBasic } from '@/api/chaochi/building/buildingservice';
export default {
  name: 'BuildingRentBasic',
  components: { DatePickerE },
  props: {
    editform: { type: Object, default: null }
  },
  data() {
    return {
      editForm: {},
      building1: {},
      rule: {
        BRoom: [
          {
            required: true,
            message: '請輸入房',
            trigger: 'blur'
          }
        ],
        BLD: [
          {
            required: true,
            message: '請輸入廳',
            trigger: 'blur'
          }
        ],
        BBath: [
          {
            required: true,
            message: '請輸入衛',
            trigger: 'blur'
          }
        ],
        BBalcony: [
          {
            required: true,
            message: '請輸入陽台',
            trigger: 'blur'
          }
        ],
        BCDate: [
          {
            required: true,
            message: '請輸入建築完成日期',
            trigger: 'blur'
          }
        ],
        BLevel: [
          {
            required: true,
            message: '請輸入層數',
            trigger: 'blur'
          }
        ],
        BFloor: [
          {
            required: true,
            message: '請輸入層次',
            trigger: 'blur'
          }
        ],
        BArea: [
          {
            required: true,
            message: '請輸入實際坪數面積',
            trigger: 'blur'
          }
        ],
        BJoinPno_1: [],
        BJoinPno_2: [],
        BJoinPno_3: [],
        BJoinPno_4: [],
        BJoinPno_5: []
      },
      patternOptions: [
        {
          value: 'BPatten1S',
          label: '套房'
        },
        {
          value: 'BBedsit',
          label: '雅房'
        },
        {
          value: 'BPatten1R',
          label: '1房'
        },
        {
          value: 'BPatten2R',
          label: '2房'
        },
        {
          value: 'BPatten3Rup',
          label: '3房以上'
        },
        {
          value: 'BPatten4Rup',
          label: '4房以上'
        },
        {
          value: 'BOpenspace',
          label: '開放式空間'
        }
      ],
      typeOptions: [
        {
          value: 'BTApartment',
          label: '公寓'
        },
        {
          value: 'BTElevator',
          label: '電梯大樓'
        },
        {
          value: 'BTHouse',
          label: '透天厝'
        },
        {
          value: 'BTMansion',
          label: '華廈'
        },
        {
          value: 'BTFarmHouse',
          label: '農舍'
        },
        {
          value: 'BTWorkshop',
          label: '廠房'
        }
        // {
        //   value: 'BTBungalow',
        //   label: '平房'
        // },
        // {
        //   value: 'BTVilla',
        //   label: '別墅'
        // },
      ],
      AttachBuildingOptions: [
        {
          value: 'balcony',
          label: '陽台'
        },
        {
          value: 'Flowerbed',
          label: '花台'
        },
        {
          value: 'umbrella',
          label: '雨遮'
        },
        {
          value: 'platform',
          label: '平台'
        },
        {
          value: 'other',
          label: '其他'
        }
      ],
      joinOptions: [
        {
          value: 'postulate',
          label: '公設'
        },
        {
          value: 'postulateIncludeParking ',
          label: '公設(含車)'
        },
        {
          value: 'postulateParking ',
          label: '公設車位'
        },
        {
          value: 'Parking',
          label: '車位'
        },
        {
          value: 'Basement',
          label: '地下一層'
        }
      ],
      saveLoading: false,
      formLabelWidth: '115px',
      typeValue: '',
      patternValue: '',
      // radio: '',
      // radioMurder: '',
      rp: '',
      bping: ''
    };
  },
  computed: {
    barea() {
      var ba1 = this.editForm.BArea_1 ? parseFloat(this.editForm.BArea_1) : 0;
      var ba2 = this.editForm.BArea_2 ? parseFloat(this.editForm.BArea_2) : 0;
      var ba3 = this.editForm.BArea_3 ? parseFloat(this.editForm.BArea_3) : 0;
      var ab1 = this.editForm.BAttachBuildingArea_1
        ? parseFloat(this.editForm.BAttachBuildingArea_1)
        : 0;
      var ab2 = this.editForm.BAttachBuildingArea_2
        ? parseFloat(this.editForm.BAttachBuildingArea_2)
        : 0;
      var ab3 = this.editForm.BAttachBuildingArea_3
        ? parseFloat(this.editForm.BAttachBuildingArea_3)
        : 0;
      var ab4 = this.editForm.BAttachBuildingArea_4
        ? parseFloat(this.editForm.BAttachBuildingArea_4)
        : 0;
      var ab5 = this.editForm.BAttachBuildingArea_5
        ? parseFloat(this.editForm.BAttachBuildingArea_5)
        : 0;
      var j1 = this.editForm.BJoinOwnerArea_1
        ? parseFloat(this.editForm.BJoinOwnerArea_1)
        : 0;
      var j2 = this.editForm.BJoinOwnerArea_2
        ? parseFloat(this.editForm.BJoinOwnerArea_2)
        : 0;
      var j3 = this.editForm.BJoinOwnerArea_3
        ? parseFloat(this.editForm.BJoinOwnerArea_3)
        : 0;
      var j4 = this.editForm.BJoinOwnerArea_4
        ? parseFloat(this.editForm.BJoinOwnerArea_4)
        : 0;
      var j5 = this.editForm.BJoinOwnerArea_5
        ? parseFloat(this.editForm.BJoinOwnerArea_5)
        : 0;
      var result =
        ba1 + ba2 + ba3 + ab1 + ab2 + ab3 + ab4 + ab5 + j1 + j2 + j3 + j4 + j5;

      if (result === 0.0) {
        return result.toString();
      } else {
        return parseFloat(result).toFixed(2);
      }
    }
  },
  watch: {
    editform: {
      handler() {
        this.editForm = this.editform;
        this.building1 = this.editform.building1;
        var realPingInitVal = this.editForm.BRealPING;
        this.rp = realPingInitVal;
        // this.radioMurder =
        //   this.editForm.BMurder_No_Check === '/Yes'
        //     ? '1'
        //     : this.editForm.BMurder_Yes_Check === '/Yes'
        //       ? '2'
        //       : this.editForm.BMurder_Yes_Dont === '/Yes'
        //         ? '3'
        //         : '';
        // this.radio =
        //   this.editForm.BMurder_Y === '/Yes'
        //     ? '1'
        //     : this.editForm.BMurder_N === '/Yes'
        //       ? '2'
        //       : '';
        this.patternValue =
          this.editForm.BPatten1S === '/Yes'
            ? 'BPatten1S'
            : this.editForm.BPatten1R === '/Yes'
              ? 'BPatten1R'
              : this.editForm.BPatten2R === '/Yes'
                ? 'BPatten2R'
                : this.editForm.BPatten3Rup === '/Yes'
                  ? 'BPatten3Rup'
                  : this.editForm.BPatten4Rup === '/Yes'
                    ? 'BPatten4Rup'
                    : this.editForm.BBedsit === '/Yes'
                      ? 'BBedsit'
                      : this.editForm.BOpenspace === '/Yes'
                        ? 'BOpenspace'
                        : '';
        this.typeValue =
          this.editForm.BTApartment === '/Yes'
            ? 'BTApartment'
            : this.editForm.BTElevator === '/Yes'
              ? 'BTElevator'
              : this.editForm.BTVilla === '/Yes'
                ? 'BTVilla'
                : this.editForm.BTMansion === '/Yes'
                  ? 'BTMansion'
                  : this.editForm.BTWorkshop === '/Yes'
                    ? 'BTWorkshop'
                    : this.editForm.BTFarmHouse === '/Yes'
                      ? 'BTFarmHouse'
                      : '';
        // : this.editForm.BTBungalow === '/Yes'
        //   ? 'BTBungalow'
        // : this.editForm.BTHouse === '/Yes'
        //   ? 'BTHouse'
      }
    },
    patternValue: {
      handler() {
        switch (this.patternValue) {
          case 'BPatten1S':
            this.editForm.BPatten1S = '/Yes';
            this.editForm.BPatten1R = '/Off';
            this.editForm.BPatten2R = '/Off';
            this.editForm.BPatten3Rup = '/Off';
            this.editForm.BPatten4Rup = '/Off';
            this.editForm.BBedsit = '/Off';
            this.editForm.BOpenspace = '/Off';
            break;
          case 'BPatten1R':
            this.editForm.BPatten1S = '/Off';
            this.editForm.BPatten1R = '/Yes';
            this.editForm.BPatten2R = '/Off';
            this.editForm.BPatten3Rup = '/Off';
            this.editForm.BPatten4Rup = '/Off';
            this.editForm.BBedsit = '/Off';
            this.editForm.BOpenspace = '/Off';
            break;
          case 'BPatten2R':
            this.editForm.BPatten1S = '/Off';
            this.editForm.BPatten1R = '/Off';
            this.editForm.BPatten2R = '/Yes';
            this.editForm.BPatten3Rup = '/Off';
            this.editForm.BPatten4Rup = '/Off';
            this.editForm.BBedsit = '/Off';
            this.editForm.BOpenspace = '/Off';
            break;
          case 'BPatten3Rup':
            this.editForm.BPatten1S = '/Off';
            this.editForm.BPatten1R = '/Off';
            this.editForm.BPatten2R = '/Off';
            this.editForm.BPatten3Rup = '/Yes';
            this.editForm.BPatten4Rup = '/Off';
            this.editForm.BBedsit = '/Off';
            this.editForm.BOpenspace = '/Off';
            break;
          case 'BPatten4Rup':
            this.editForm.BPatten1S = '/Off';
            this.editForm.BPatten1R = '/Off';
            this.editForm.BPatten2R = '/Off';
            this.editForm.BPatten3Rup = '/Off';
            this.editForm.BPatten4Rup = '/Yes';
            this.editForm.BBedsit = '/Off';
            this.editForm.BOpenspace = '/Off';
            break;
          case 'BBedsit':
            this.editForm.BPatten1S = '/Off';
            this.editForm.BPatten1R = '/Off';
            this.editForm.BPatten2R = '/Off';
            this.editForm.BPatten3Rup = '/Off';
            this.editForm.BPatten4Rup = '/Off';
            this.editForm.BBedsit = '/Yes';
            this.editForm.BOpenspace = '/Off';
            break;
          case 'BOpenspace':
            this.editForm.BPatten1S = '/Off';
            this.editForm.BPatten1R = '/Off';
            this.editForm.BPatten2R = '/Off';
            this.editForm.BPatten3Rup = '/Off';
            this.editForm.BPatten4Rup = '/Off';
            this.editForm.BBedsit = '/Off';
            this.editForm.BOpenspace = '/Yes';
            break;
        }
      }
    },
    typeValue: {
      handler() {
        switch (this.typeValue) {
          case 'BTApartment':
            this.editForm.BTApartment = '/Yes';
            this.editForm.BTElevator = '/Off';
            this.editForm.BTVilla = '/Off';
            this.editForm.BTMansion = '/Off';
            this.editForm.BTFarmHouse = '/Off';
            this.editForm.BTWorkshop = '/Off';
            // this.editForm.BTHouse = '/Off';
            // this.editForm.BTBungalow = '/Off';
            break;
          case 'BTElevator':
            this.editForm.BTApartment = '/Off';
            this.editForm.BTElevator = '/Yes';
            this.editForm.BTVilla = '/Off';
            this.editForm.BTMansion = '/Off';
            this.editForm.BTFarmHouse = '/Off';
            this.editForm.BTWorkshop = '/Off';
            // this.editForm.BTHouse = '/Off';
            // this.editForm.BTBungalow = '/Off';
            break;
          case 'BTVilla':
            this.editForm.BTApartment = '/Off';
            this.editForm.BTElevator = '/Off';
            this.editForm.BTVilla = '/Yes';
            this.editForm.BTMansion = '/Off';
            this.editForm.BTFarmHouse = '/Off';
            this.editForm.BTWorkshop = '/Off';
            // this.editForm.BTHouse = '/Off';
            // this.editForm.BTBungalow = '/Off';
            break;
          case 'BTMansion':
            this.editForm.BTApartment = '/Off';
            this.editForm.BTElevator = '/Off';
            this.editForm.BTVilla = '/Off';
            this.editForm.BTMansion = '/Yes';
            this.editForm.BTFarmHouse = '/Off';
            this.editForm.BTWorkshop = '/Off';
            // this.editForm.BTHouse = '/Off';
            // this.editForm.BTBungalow = '/Off';
            break;
          case 'BTFarmHouse':
            this.editForm.BTApartment = '/Off';
            this.editForm.BTElevator = '/Off';
            this.editForm.BTVilla = '/Off';
            this.editForm.BTMansion = '/Off';
            this.editForm.BTFarmHouse = '/Yes';
            this.editForm.BTWorkshop = '/Off';
            // this.editForm.BTHouse = '/Off';
            // this.editForm.BTBungalow = '/Off';
            break;
          case 'BTWorkshop':
            this.editForm.BTApartment = '/Off';
            this.editForm.BTElevator = '/Off';
            this.editForm.BTVilla = '/Off';
            this.editForm.BTMansion = '/Off';
            this.editForm.BTFarmHouse = '/Off';
            this.editForm.BTWorkshop = '/Yes';
            break;
          // case 'BTBungalow':
          //   this.editForm.BTApartment = '/Off';
          //   this.editForm.BTElevator = '/Off';
          //   this.editForm.BTHouse = '/Off';
          //   this.editForm.BTVilla = '/Off';
          //   this.editForm.BTMansion = '/Off';
          //   this.editForm.BTFarmHouse = '/Off';
          //   this.editForm.BTBungalow = '/Yes';
          //   break;
          // case 'BTHouse':
          //   this.editForm.BTApartment = '/Off';
          //   this.editForm.BTElevator = '/Off';
          //   this.editForm.BTHouse = '/Yes';
          //   this.editForm.BTVilla = '/Off';
          //   this.editForm.BTMansion = '/Off';
          //   this.editForm.BTFarmHouse = '/Off';
          //   this.editForm.BTBungalow = '/Off';
          //   break;
        }
      }
    },
    // radio: {
    //   handler() {
    //     switch (this.radio) {
    //       case '1':
    //         this.editForm.BMurder_Y = '/Yes';
    //         this.editForm.BMurder_N = '/Off';
    //         break;
    //       case '2':
    //         this.editForm.BMurder_Y = '/Off';
    //         this.editForm.BMurder_N = '/Yes';
    //         break;
    //     }
    //   }
    // },
    // radioMurder: {
    //   handler() {
    //     switch (this.radioMurder) {
    //       case '1':
    //         this.editForm.BMurder_No_Check = '/Yes';
    //         this.editForm.BMurder_Yes_Check = '/Off';
    //         this.editForm.BMurder_Yes_Dont = '/Off';
    //         break;
    //       case '2':
    //         this.editForm.BMurder_No_Check = '/Off';
    //         this.editForm.BMurder_Yes_Check = '/Yes';
    //         this.editForm.BMurder_Yes_Dont = '/Off';
    //         break;
    //       case '3':
    //         this.editForm.BMurder_No_Check = '/Off';
    //         this.editForm.BMurder_Yes_Check = '/Off';
    //         this.editForm.BMurder_Yes_Dont = '/Yes';
    //         break;
    //     }
    //   }
    // },
    barea: {
      handler(a) {
        if (parseFloat(a) > 0) {
          this.editForm.BRealPING = (parseFloat(this.barea) * 0.3025).toFixed(
            2
          );
        } else {
          this.editForm.BRealPING = '0';
        }
      }
    },
    'editForm.BArea': {
      handler(a) {
        if (parseFloat(a) > 0) {
          this.editForm.BPing = (
            parseFloat(this.editForm.BArea) * 0.3025
          ).toFixed(2);
        } else {
          this.editForm.BPing = '0';
        }
      }
    },
    'editForm.BArea_1': {
      handler(a) {
        if (parseFloat(a) > 0) {
          // this.editForm.BArea += parseFloat(a);
          this.editForm.BPing_1 = (
            parseFloat(this.editForm.BArea_1) * 0.3025
          ).toFixed(2);
        } else {
          this.editForm.BPing_1 = '0';
        }
      }
    },
    'editForm.BArea_2': {
      handler(a) {
        if (parseFloat(a) > 0) {
          this.editForm.BPing_2 = (
            parseFloat(this.editForm.BArea_2) * 0.3025
          ).toFixed(2);
        } else {
          this.editForm.BPing_2 = '0';
        }
      }
    },
    'editForm.BArea_3': {
      handler(a) {
        if (parseFloat(a) > 0) {
          this.editForm.BPing_3 = (
            parseFloat(this.editForm.BArea_3) * 0.3025
          ).toFixed(2);
        } else {
          this.editForm.BPing_3 = '0';
        }
      }
    },
    'editForm.BAttachBuildingArea_1': {
      handler(a) {
        if (parseFloat(a) > 0) {
          // this.editForm.BArea += parseFloat(a);
          this.editForm.BAttachBuildingPing_1 = (
            parseFloat(this.editForm.BAttachBuildingArea_1) * 0.3025
          ).toFixed(2);
        } else {
          this.editForm.BAttachBuildingPing_1 = '0';
        }
      }
    },
    'editForm.BAttachBuildingArea_2': {
      handler(a) {
        if (parseFloat(a) > 0) {
          // this.editForm.BArea += parseFloat(a);
          this.editForm.BAttachBuildingPing_2 = (
            parseFloat(this.editForm.BAttachBuildingArea_2) * 0.3025
          ).toFixed(2);
        } else {
          this.editForm.BAttachBuildingPing_2 = '0';
        }
      }
    },
    'editForm.BAttachBuildingArea_3': {
      handler(a) {
        if (parseFloat(a) > 0) {
          // this.editForm.BArea += parseFloat(a);
          this.editForm.BAttachBuildingPing_3 = (
            parseFloat(this.editForm.BAttachBuildingArea_3) * 0.3025
          ).toFixed(2);
        } else {
          this.editForm.BAttachBuildingPing_3 = '0';
        }
      }
    },
    'editForm.BAttachBuildingArea_4': {
      handler(a) {
        if (parseFloat(a) > 0) {
          // this.editForm.BArea += parseFloat(a);
          this.editForm.BAttachBuildingPing_4 = (
            parseFloat(this.editForm.BAttachBuildingArea_4) * 0.3025
          ).toFixed(2);
        } else {
          this.editForm.BAttachBuildingPing_4 = '0';
        }
      }
    },
    'editForm.BAttachBuildingArea_5': {
      handler(a) {
        if (parseFloat(a) > 0) {
          // this.editForm.BArea += parseFloat(a);
          this.editForm.BAttachBuildingPing_5 = (
            parseFloat(this.editForm.BAttachBuildingArea_5) * 0.3025
          ).toFixed(2);
        } else {
          this.editForm.BAttachBuildingPing_5 = '0';
        }
      }
    },
    'editForm.BJoinArea_1': {
      handler(a) {
        if (parseFloat(a) > 0) {
          // this.editForm.BArea += parseFloat(a);
          this.rule.BJoinPno_1.push({
            required: true,
            message: '請輸入建號',
            trigger: 'change'
          });
          if (
            parseFloat(this.editForm.BJoinOwner1_1) > 0 &&
            parseFloat(this.editForm.BJoinOwner2_1) > 0
          ) {
            this.editForm.BJoinOwnerArea_1 = (
              (this.editForm.BJoinArea_1 * this.editForm.BJoinOwner1_1) /
              this.editForm.BJoinOwner2_1
            ).toFixed(2);
            this.editForm.BJoinOwnerPing_1 = (
              ((this.editForm.BJoinArea_1 * this.editForm.BJoinOwner1_1) /
                this.editForm.BJoinOwner2_1) *
              0.3025
            )
              .toFixed(2)
              .toString();
          }
        } else {
          this.rule.BJoinPno_1 = [];
          this.editForm.BJoinOwnerArea_1 = '0';
          this.editForm.BJoinOwnerPing_1 = '0';
        }
      }
    },
    'editForm.BJoinOwner1_1': {
      handler(a) {
        if (parseFloat(a) > 0) {
          // this.editForm.BArea += parseFloat(a);
          if (
            parseFloat(this.editForm.BJoinArea_1) > 0 &&
            parseFloat(this.editForm.BJoinOwner2_1) > 0
          ) {
            this.editForm.BJoinOwnerArea_1 = (
              (this.editForm.BJoinArea_1 * this.editForm.BJoinOwner1_1) /
              this.editForm.BJoinOwner2_1
            ).toFixed(2);
            this.editForm.BJoinOwnerPing_1 = (
              ((this.editForm.BJoinArea_1 * this.editForm.BJoinOwner1_1) /
                this.editForm.BJoinOwner2_1) *
              0.3025
            )
              .toFixed(2)
              .toString();
          }
        } else {
          this.editForm.BJoinOwnerArea_1 = '0';
          this.editForm.BJoinOwnerPing_1 = '0';
        }
      }
    },
    'editForm.BJoinOwner2_1': {
      handler(a) {
        if (parseFloat(a) > 0) {
          // this.editForm.BArea += parseFloat(a);
          if (
            parseFloat(this.editForm.BJoinArea_1) > 0 &&
            parseFloat(this.editForm.BJoinOwner1_1) > 0
          ) {
            this.editForm.BJoinOwnerArea_1 = (
              (this.editForm.BJoinArea_1 * this.editForm.BJoinOwner1_1) /
              this.editForm.BJoinOwner2_1
            ).toFixed(2);
            this.editForm.BJoinOwnerPing_1 = (
              ((this.editForm.BJoinArea_1 * this.editForm.BJoinOwner1_1) /
                this.editForm.BJoinOwner2_1) *
              0.3025
            )
              .toFixed(2)
              .toString();
          }
        } else {
          this.editForm.BJoinOwnerArea_1 = '0';
          this.editForm.BJoinOwnerPing_1 = '0';
        }
      }
    },
    'editForm.BJoinArea_2': {
      handler(a) {
        if (parseFloat(a) > 0) {
          // this.editForm.BArea += parseFloat(a);
          this.rule.BJoinPno_2.push({
            required: true,
            message: '請輸入建號',
            trigger: 'change'
          });
          if (
            parseFloat(this.editForm.BJoinOwner1_2) > 0 &&
            parseFloat(this.editForm.BJoinOwner2_2) > 0
          ) {
            this.editForm.BJoinOwnerArea_2 = (
              (this.editForm.BJoinArea_2 * this.editForm.BJoinOwner1_2) /
              this.editForm.BJoinOwner2_2
            ).toFixed(2);
            this.editForm.BJoinOwnerPing_2 = (
              ((this.editForm.BJoinArea_2 * this.editForm.BJoinOwner1_2) /
                this.editForm.BJoinOwner2_2) *
              0.3025
            )
              .toFixed(2)
              .toString();
          }
        } else {
          this.rule.BJoinPno_2 = [];
          this.editForm.BJoinOwnerArea_2 = '0';
          this.editForm.BJoinOwnerPing_2 = '0';
        }
      }
    },
    'editForm.BJoinOwner1_2': {
      handler(a) {
        if (parseFloat(a) > 0) {
          // this.editForm.BArea += parseFloat(a);
          if (
            parseFloat(this.editForm.BJoinArea_2) > 0 &&
            parseFloat(this.editForm.BJoinOwner2_2) > 0
          ) {
            this.editForm.BJoinOwnerArea_2 = (
              (this.editForm.BJoinArea_2 * this.editForm.BJoinOwner1_2) /
              this.editForm.BJoinOwner2_2
            ).toFixed(2);
            this.editForm.BJoinOwnerPing_2 = (
              ((this.editForm.BJoinArea_2 * this.editForm.BJoinOwner1_2) /
                this.editForm.BJoinOwner2_2) *
              0.3025
            )
              .toFixed(2)
              .toString();
          }
        } else {
          this.editForm.BJoinOwnerArea_2 = '0';
          this.editForm.BJoinOwnerPing_2 = '0';
        }
      }
    },
    'editForm.BJoinOwner2_2': {
      handler(a) {
        if (parseFloat(a) > 0) {
          // this.editForm.BArea += parseFloat(a);
          if (
            parseFloat(this.editForm.BJoinArea_2) > 0 &&
            parseFloat(this.editForm.BJoinOwner1_2) > 0
          ) {
            this.editForm.BJoinOwnerArea_2 = (
              (this.editForm.BJoinArea_2 * this.editForm.BJoinOwner1_2) /
              this.editForm.BJoinOwner2_2
            ).toFixed(2);
            this.editForm.BJoinOwnerPing_2 = (
              ((this.editForm.BJoinArea_2 * this.editForm.BJoinOwner1_2) /
                this.editForm.BJoinOwner2_2) *
              0.3025
            )
              .toFixed(2)
              .toString();
          }
        } else {
          this.editForm.BJoinOwnerArea_2 = '0';
          this.editForm.BJoinOwnerPing_2 = '0';
        }
      }
    },
    'editForm.BJoinArea_3': {
      handler(a) {
        if (parseFloat(a) > 0) {
          // this.editForm.BArea += parseFloat(a);
          this.rule.BJoinPno_3.push({
            required: true,
            message: '請輸入建號',
            trigger: 'change'
          });
          if (
            parseFloat(this.editForm.BJoinOwner1_3) > 0 &&
            parseFloat(this.editForm.BJoinOwner2_3) > 0
          ) {
            this.editForm.BJoinOwnerArea_3 = (
              (this.editForm.BJoinArea_3 * this.editForm.BJoinOwner1_3) /
              this.editForm.BJoinOwner2_3
            ).toFixed(2);
            this.editForm.BJoinOwnerPing_3 = (
              ((this.editForm.BJoinArea_3 * this.editForm.BJoinOwner1_3) /
                this.editForm.BJoinOwner2_3) *
              0.3025
            )
              .toFixed(2)
              .toString();
          }
        } else {
          this.rule.BJoinPno_3 = [];
          this.editForm.BJoinOwnerArea_3 = '0';
          this.editForm.BJoinOwnerPing_3 = '0';
        }
      }
    },
    'editForm.BJoinOwner1_3': {
      handler(a) {
        if (parseFloat(a) > 0) {
          // this.editForm.BArea += parseFloat(a);
          if (
            parseFloat(this.editForm.BJoinArea_3) > 0 &&
            parseFloat(this.editForm.BJoinOwner2_3) > 0
          ) {
            this.editForm.BJoinOwnerArea_3 = (
              (this.editForm.BJoinArea_3 * this.editForm.BJoinOwner1_3) /
              this.editForm.BJoinOwner2_3
            ).toFixed(2);
            this.editForm.BJoinOwnerPing_3 = (
              ((this.editForm.BJoinArea_3 * this.editForm.BJoinOwner1_3) /
                this.editForm.BJoinOwner2_3) *
              0.3025
            )
              .toFixed(2)
              .toString();
          }
        } else {
          this.editForm.BJoinOwnerArea_3 = '0';
          this.editForm.BJoinOwnerPing_3 = '0';
        }
      }
    },
    'editForm.BJoinOwner2_3': {
      handler(a) {
        if (parseFloat(a) > 0) {
          // this.editForm.BArea += parseFloat(a);
          if (
            parseFloat(this.editForm.BJoinArea_3) > 0 &&
            parseFloat(this.editForm.BJoinOwner1_3) > 0
          ) {
            this.editForm.BJoinOwnerArea_3 = (
              (this.editForm.BJoinArea_3 * this.editForm.BJoinOwner1_3) /
              this.editForm.BJoinOwner2_3
            ).toFixed(2);
            this.editForm.BJoinOwnerPing_3 = (
              ((this.editForm.BJoinArea_3 * this.editForm.BJoinOwner1_3) /
                this.editForm.BJoinOwner2_3) *
              0.3025
            )
              .toFixed(2)
              .toString();
          }
        } else {
          this.editForm.BJoinOwnerArea_3 = '0';
          this.editForm.BJoinOwnerPing_3 = '0';
        }
      }
    },
    'editForm.BJoinArea_4': {
      handler(a) {
        if (parseFloat(a) > 0) {
          // this.editForm.BArea += parseFloat(a);
          this.rule.BJoinPno_4.push({
            required: true,
            message: '請輸入建號',
            trigger: 'change'
          });
          if (
            parseFloat(this.editForm.BJoinOwner1_4) > 0 &&
            parseFloat(this.editForm.BJoinOwner2_4) > 0
          ) {
            this.editForm.BJoinOwnerArea_4 = (
              (this.editForm.BJoinArea_4 * this.editForm.BJoinOwner1_4) /
              this.editForm.BJoinOwner2_4
            ).toFixed(2);
            this.editForm.BJoinOwnerPing_4 = (
              ((this.editForm.BJoinArea_4 * this.editForm.BJoinOwner1_4) /
                this.editForm.BJoinOwner2_4) *
              0.3025
            )
              .toFixed(2)
              .toString();
          }
        } else {
          this.rule.BJoinPno_4 = [];
          this.editForm.BJoinOwnerArea_4 = '0';
          this.editForm.BJoinOwnerPing_4 = '0';
        }
      }
    },
    'editForm.BJoinOwner1_4': {
      handler(a) {
        if (parseFloat(a) > 0) {
          // this.editForm.BArea += parseFloat(a);
          if (
            parseFloat(this.editForm.BJoinArea_4) > 0 &&
            parseFloat(this.editForm.BJoinOwner2_4) > 0
          ) {
            this.editForm.BJoinOwnerArea_4 = (
              (this.editForm.BJoinArea_4 * this.editForm.BJoinOwner1_4) /
              this.editForm.BJoinOwner2_4
            ).toFixed(2);
            this.editForm.BJoinOwnerPing_4 = (
              ((this.editForm.BJoinArea_4 * this.editForm.BJoinOwner1_4) /
                this.editForm.BJoinOwner2_4) *
              0.3025
            )
              .toFixed(2)
              .toString();
          }
        } else {
          this.editForm.BJoinOwnerArea_4 = '0';
          this.editForm.BJoinOwnerPing_4 = '0';
        }
      }
    },
    'editForm.BJoinOwner2_4': {
      handler(a) {
        if (parseFloat(a) > 0) {
          // this.editForm.BArea += parseFloat(a);
          if (
            parseFloat(this.editForm.BJoinArea_4) > 0 &&
            parseFloat(this.editForm.BJoinOwner1_4) > 0
          ) {
            this.editForm.BJoinOwnerArea_4 = (
              (this.editForm.BJoinArea_4 * this.editForm.BJoinOwner1_4) /
              this.editForm.BJoinOwner2_4
            ).toFixed(2);
            this.editForm.BJoinOwnerPing_4 = (
              ((this.editForm.BJoinArea_4 * this.editForm.BJoinOwner1_4) /
                this.editForm.BJoinOwner2_4) *
              0.3025
            )
              .toFixed(2)
              .toString();
          }
        } else {
          this.editForm.BJoinOwnerArea_4 = '0';
          this.editForm.BJoinOwnerPing_4 = '0';
        }
      }
    },
    'editForm.BJoinArea_5': {
      handler(a) {
        if (parseFloat(a) > 0) {
          // this.editForm.BArea += parseFloat(a);
          this.rule.BJoinPno_5.push({
            required: true,
            message: '請輸入建號',
            trigger: 'change'
          });
          if (
            parseFloat(this.editForm.BJoinOwner1_5) > 0 &&
            parseFloat(this.editForm.BJoinOwner2_5) > 0
          ) {
            this.editForm.BJoinOwnerArea_5 = (
              (this.editForm.BJoinArea_5 * this.editForm.BJoinOwner1_5) /
              this.editForm.BJoinOwner2_5
            ).toFixed(2);
            this.editForm.BJoinOwnerPing_5 = (
              ((this.editForm.BJoinArea_5 * this.editForm.BJoinOwner1_5) /
                this.editForm.BJoinOwner2_5) *
              0.3025
            )
              .toFixed(2)
              .toString();
          }
        } else {
          this.rule.BJoinPno_5 = [];
          this.editForm.BJoinOwnerArea_5 = '0';
          this.editForm.BJoinOwnerPing_5 = '0';
        }
      }
    },
    'editForm.BJoinOwner1_5': {
      handler(a) {
        if (parseFloat(a) > 0) {
          // this.editForm.BArea += parseFloat(a);
          if (
            parseFloat(this.editForm.BJoinArea_5) > 0 &&
            parseFloat(this.editForm.BJoinOwner2_5) > 0
          ) {
            this.editForm.BJoinOwnerArea_5 = (
              (this.editForm.BJoinArea_5 * this.editForm.BJoinOwner1_5) /
              this.editForm.BJoinOwner2_5
            ).toFixed(2);
            this.editForm.BJoinOwnerPing_5 = (
              ((this.editForm.BJoinArea_5 * this.editForm.BJoinOwner1_5) /
                this.editForm.BJoinOwner2_5) *
              0.3025
            )
              .toFixed(2)
              .toString();
          }
        } else {
          this.editForm.BJoinOwnerArea_5 = '0';
          this.editForm.BJoinOwnerPing_5 = '0';
        }
      }
    },
    'editForm.BJoinOwner2_5': {
      handler(a) {
        if (parseFloat(a) > 0) {
          // this.editForm.BArea += parseFloat(a);
          if (
            parseFloat(this.editForm.BJoinArea_5) > 0 &&
            parseFloat(this.editForm.BJoinOwner1_5) > 0
          ) {
            this.editForm.BJoinOwnerArea_5 = (
              (this.editForm.BJoinArea_5 * this.editForm.BJoinOwner1_5) /
              this.editForm.BJoinOwner2_5
            ).toFixed(2);
            this.editForm.BJoinOwnerPing_5 = (
              ((this.editForm.BJoinArea_5 * this.editForm.BJoinOwner1_5) /
                this.editForm.BJoinOwner2_5) *
              0.3025
            )
              .toFixed(2)
              .toString();
          }
        } else {
          this.editForm.BJoinOwnerArea_5 = '0';
          this.editForm.BJoinOwnerPing_5 = '0';
        }
      }
    },
    // 'editForm.BJoin_1': {
    //   handler(a) {
    //     console.log(a);
    //     if (a) {
    //       this.rule.BJoinPno_1.push({
    //         required: true,
    //         message: '清輸入建號',
    //         trigger: 'change'
    //       });
    //       this.rule.BJoinArea_1.push({
    //         required: true,
    //         message: '請輸入面積',
    //         trigger: 'change'
    //       });
    //       this.rule.BJoinOwner1_1.push({
    //         required: true,
    //         message: '請輸入權利範圍',
    //         trigger: 'change'
    //       });
    //       this.rule.BJoinOwner2_1.push({
    //         required: true,
    //         message: '請輸入權利範圍',
    //         trigger: 'change'
    //       });
    //     }
    //   }
    // },
    'editForm.BRealArea': {
      handler(a) {
        if (parseFloat(a) > 0) {
          this.editForm.BRealPING = (
            parseFloat(this.editForm.BRealArea) * 0.3025
          ).toFixed(2);
        } else {
          this.editForm.BRealPING = this.rp; // this.rp是BRealPING最開始的值
        }
      }
    },
    'editForm.BCDate': {
      handler(a) {
        if (a) {
          var dtArray = a.split('-');
          var houseDt = new Date(
            dtArray[0] * 1 + 1911 + '/' + dtArray[1] + '/' + dtArray[2]
          );
          // var ageDifMs = Date.now() - houseDt.getTime();
          // var ageDate = new Date(ageDifMs);
          this.editForm.BAge = (
            Math.round(
              ((new Date() - houseDt) / 1000 / 60 / 60 / 24 / 365) * 10
            ) / 10
          ).toString();
          console.log(this.editForm.BAge);
          // var year = a.split('-')[0];
          // var currentTime = new Date();
          // var year2 = currentTime.getFullYear() - 1911;
          // this.editForm.BAge = (year2 - year).toString();
        } else {
          this.editForm.BAge = '';
        }
      }
    }
  },
  methods: {
    cancel() {
      this.$emit('cancel');
    },
    saveBuildingRentBasic() {
      this.$refs['editForm'].validate((valid) => {
        if (valid) {
          this.saveLoading = true;
          this.editForm.BRealArea = this.barea;
          UpdateBuildingRentBasic(this.editForm).then((res) => {
            if (res.Success) {
              this.$message.success('儲存成功!');
              console.log(this.editForm.BPing);
              var bping = this.editForm.BPing;
              console.log(bping);
              this.$emit('emitbpingfunc', bping);
            } else {
              this.$message.error('失敗!');
            }
            this.saveLoading = false;
          });
        }
      });
    }
  }
};
</script>

<style></style>
