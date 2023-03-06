<template>
  <div>
    <el-form
      ref="editForm"
      :inline="true"
      :model="editForm"
      class="demo-form-inline"
      :rules="rule"
    >
      <el-card>
        <el-row>
          <el-form-item
            label="公司名稱："
            :label-width="formLabelWidth"
            prop="RCName"
          >
            <el-input
              v-model="editForm.RCName"
              placeholder="請輸入公司名稱"
              autocomplete="off"
              clearable
            />
          </el-form-item>
          <el-form-item
            label="公司名稱(英)："
            :label-width="formLabelWidth"
          >
            <el-input
              v-model="editForm.RCEngName"
              placeholder="請輸入公司名稱(英)"
              autocomplete="off"
              clearable
              onkeyup="this.value=this.value.replace(/[\u4E00-\u9FA5]/g,'')"
            />
          </el-form-item>
        </el-row>
        <el-row>
          <el-col>
            <el-form-item
              label="公司組織型態："
              :label-width="formLabelWidth"
            >
              <el-checkbox
                v-model="editForm.BZ001"
                true-label="/Yes"
                false-label="/Off"
              >上市公司，股票代號
                <el-input
                  v-model="editForm.BZ002"
                  autocomplete="off"
                  clearable
                  size="mini"
                  class="w80px"
                />
              </el-checkbox>
              &nbsp;&nbsp;&nbsp;&nbsp;
              <el-checkbox
                v-model="editForm.BZ003"
                true-label="/Yes"
                false-label="/Off"
              >上櫃公司，股票代號
                <el-input
                  v-model="editForm.BZ005"
                  autocomplete="off"
                  clearable
                  size="mini"
                  class="w80px"
                />
              </el-checkbox>
              &nbsp;&nbsp;&nbsp;&nbsp;
              <el-checkbox
                v-model="editForm.BZ006"
                true-label="/Yes"
                false-label="/Off"
              >(公司是否有集團或相關企業)
              </el-checkbox>
              &nbsp;&nbsp;&nbsp;&nbsp;
              <el-checkbox
                v-model="editForm.BZ007"
                true-label="/Yes"
                false-label="/Off"
              >其他
              </el-checkbox>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="24">
            <el-form-item
              label="登記地址："
              :label-width="formLabelWidth"
            >
              <Address
                :sendedform="sendedform"
                title="登記地址"
                @geteditaddress="geteditaddress"
              />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-form-item
            label=" "
            :label-width="formLabelWidth"
          >
            <el-checkbox v-model="RCAddSameCom">登記地址同營業地址</el-checkbox>
          </el-form-item>
          <el-form-item
            v-show="RCAddSameCom===false"
            label="營業地址："
          >
            <Address
              :sendedform="sendedform1"
              :resetall="RCAddSameCom"
              title="公司營業地址"
              @geteditaddress="geteditaddress"
            />
          </el-form-item>
        </el-row>
        <el-row>
          <el-form-item
            label="統一編號："
            :label-width="formLabelWidth"
            prop="RCID"
          >
            <el-input
              v-model="editForm.RCID"
              autocomplete="off"
              clearable
              disabled
              :maxlength="8"
            />
          </el-form-item>
          <el-form-item
            label="負責人："
            :label-width="formLabelWidth"
            prop="RCRep"
          >
            <el-input
              v-model="editForm.RCRep"
              placeholder="請輸入負責人"
              autocomplete="off"
              clearable
            />
          </el-form-item>
          <el-form-item
            label="電話："
            :label-width="formLabelWidth"
            prop="RCTel_2"
          >
            <el-input
              v-model="editForm.RCTel_2"
              placeholder="請輸入電話"
              clearable
              style="width: 200px"
              class="input-with-select"
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
            label="手機："
            :label-width="formLabelWidth"
            prop="RCCell"
          >
            <el-input
              v-model="editForm.RCCell"
              placeholder="請輸入手機"
              autocomplete="off"
              clearable
            />
          </el-form-item>
        </el-row>
        <el-row>
          <el-form-item
            label="信箱："
            :label-width="formLabelWidth"
            prop="RCMail"
          >
            <el-input
              v-model="editForm.RCMail"
              placeholder="請輸入信箱"
              autocomplete="off"
              clearable
            />
          </el-form-item>
          <el-form-item
            label="成立日期："
            :label-width="formLabelWidth"
          >
            <DatePickerE
              v-model="editForm.BZ008"
              value-format="yyyy-MM-dd"
              format="yyyy-MM-dd"
              type="date"
              placeholder="選擇日期"
              style="width: 200px"
            />
          </el-form-item>
          <el-form-item
            label="行業別："
            :label-width="formLabelWidth"
          >
            <el-input
              v-model="editForm.BZ009"
              placeholder="請輸入行業別"
              autocomplete="off"
              clearable
            />
          </el-form-item>
          <el-form-item
            label="實收資本額："
            :label-width="formLabelWidth"
          >
            <el-input
              v-model="editForm.BZ010"
              placeholder="請輸入實收資本額"
              autocomplete="off"
              clearable
            />
          </el-form-item>
        </el-row>
      </el-card>
      <h3>緊急聯絡人(親屬)</h3>
      <el-card>
        <el-row>
          <el-form-item
            label="姓名："
            :label-width="formLabelWidth"
          >
            <el-input
              v-model="editForm.RCECName"
              placeholder="請輸入姓名"
              autocomplete="off"
              clearable
            />
          </el-form-item>
          <el-form-item
            label="出生年月日："
            :label-width="formLabelWidth"
          >
            <DatePickerE
              v-model="editForm.RCECBirthday"
              value-format="yyyy-MM-dd"
              format="yyyy-MM-dd"
              type="date"
              placeholder="選擇日期"
              style="width: 200px"
            />
          </el-form-item>
          <el-form-item
            label="性別："
            :label-width="formLabelWidth"
          >
            <el-radio
              v-model="RCECGender"
              label="1"
            >男</el-radio>
            <el-radio
              v-model="RCECGender"
              label="2"
            >女</el-radio>
          </el-form-item>
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
              placeholder="請輸入身份證字號"
              autocomplete="off"
              clearable
              maxlength="10"
              style="width: 200px"
            />
          </el-form-item>
          <el-form-item
            label="聯絡電話："
            :label-width="formLabelWidth"
            prop="RCECCell"
          >
            <el-input
              v-model="editForm.RCECCell"
              placeholder="請輸入電話"
              autocomplete="off"
              clearable
            />
          </el-form-item>
        </el-row>
        <el-row>
          <el-form-item
            label="戶籍地址："
            :label-width="formLabelWidth"
          >
            <Address
              :sendedform="sendedformRCEC"
              title="緊急聯絡人戶籍地址"
              @geteditaddress="geteditaddress"
            />
          </el-form-item>
        </el-row>
        <el-row>
          <el-form-item
            label=" "
            :label-width="formLabelWidth"
          >
            <el-checkbox v-model="RCECAddSameCom">通訊地址同戶籍地址
            </el-checkbox>
          </el-form-item>
          <el-form-item
            v-show="RCECAddSameCom===false"
            label="通訊地址："
            :label-width="formLabelWidth"
          >
            <Address
              :sendedform="sendedformRCECC"
              :resetall="RCECAddSameCom"
              title="緊急聯絡人通訊地址"
              @geteditaddress="geteditaddress"
            />
          </el-form-item>
        </el-row>
      </el-card>
      <h3>緊急聯絡人(朋友)</h3>
      <el-card>
        <el-row>
          <el-form-item
            label="姓名："
            :label-width="formLabelWidth"
          >
            <el-input
              v-model="editForm.RCECFName"
              placeholder="請輸入姓名"
              autocomplete="off"
              clearable
            />
          </el-form-item>
          <el-form-item
            label="出生年月日："
            :label-width="formLabelWidth"
          >
            <DatePickerE
              v-model="editForm.RCECFBirthday"
              value-format="yyyy-MM-dd"
              format="yyyy-MM-dd"
              type="date"
              placeholder="選擇日期"
              style="width: 200px"
            />
          </el-form-item>
          <el-form-item
            label="性別："
            :label-width="formLabelWidth"
          >
            <el-radio
              v-model="RCECFGender"
              label="1"
            >男</el-radio>
            <el-radio
              v-model="RCECFGender"
              label="2"
            >女</el-radio>
          </el-form-item>
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
              placeholder="請輸入身份證字號"
              autocomplete="off"
              clearable
              maxlength="10"
              style="width: 200px"
            />
          </el-form-item>
          <el-form-item
            label="聯絡電話："
            :label-width="formLabelWidth"
            prop="RCECFCell"
          >
            <el-input
              v-model="editForm.RCECFCell"
              placeholder="請輸入電話"
              autocomplete="off"
              clearable
            />
          </el-form-item>
        </el-row>
        <el-row>
          <el-form-item
            label="戶籍地址："
            :label-width="formLabelWidth"
          >
            <Address
              :sendedform="sendedformRCECF"
              title="緊急聯絡人2戶籍地址"
              @geteditaddress="geteditaddress"
            />
          </el-form-item>
        </el-row>
        <el-row>
          <el-form-item
            label=" "
            :label-width="formLabelWidth"
          >
            <el-checkbox v-model="RCECFAddSameCom">通訊地址同戶籍地址
            </el-checkbox>
          </el-form-item>
          <el-form-item
            v-show="RCECFAddSameCom===false"
            label="通訊地址："
            :label-width="formLabelWidth"
          >
            <Address
              :sendedform="sendedformRCECFC"
              :resetall="RCECFAddSameCom"
              title="緊急聯絡人2通訊地址"
              @geteditaddress="geteditaddress"
            />
          </el-form-item>
        </el-row>
      </el-card>
      <h3>簽約代理人</h3>
      <el-card>
        <el-row>
          <el-form-item
            label="姓名："
            :label-width="formLabelWidth"
          >
            <el-input
              v-model="editForm.RCAGName"
              placeholder="請輸入姓名"
              autocomplete="off"
              clearable
            />
          </el-form-item>
          <el-form-item
            label="出生年月日："
            :label-width="formLabelWidth"
          >
            <DatePickerE
              v-model="editForm.RCAGBirthday"
              value-format="yyyy-MM-dd"
              format="yyyy-MM-dd"
              type="date"
              placeholder="選擇日期"
              style="width: 200px"
            />
          </el-form-item>
          <el-form-item
            label="性別："
            :label-width="formLabelWidth"
          >
            <el-radio
              v-model="RCAGGender"
              label="1"
            >男</el-radio>
            <el-radio
              v-model="RCAGGender"
              label="2"
            >女</el-radio>
          </el-form-item>
        </el-row>
        <el-row>
          <el-form-item
            label="身份證字號："
            :label-width="formLabelWidth"
            prop="RCAGID"
          >
            <el-input
              v-model="editForm.RCAGID"
              v-upperCase
              placeholder="請輸入身份證字號"
              autocomplete="off"
              clearable
              maxlength="10"
              style="width: 200px"
            />
          </el-form-item>
          <el-form-item
            label="聯絡電話："
            :label-width="formLabelWidth"
            prop="RCAGCell"
          >
            <el-input
              v-model="editForm.RCAGCell"
              placeholder="請輸入電話"
              autocomplete="off"
              clearable
            />
          </el-form-item>
        </el-row>
        <el-row>
          <el-form-item
            label="戶籍地址："
            :label-width="formLabelWidth"
          >
            <Address
              :sendedform="sendedformRCAG"
              title="代理人戶籍地址"
              @geteditaddress="geteditaddress"
            />
          </el-form-item>
        </el-row>
        <el-row>
          <el-form-item
            label=" "
            :label-width="formLabelWidth"
          >
            <el-checkbox v-model="RCAGAddSameCom">通訊地址同戶籍地址
            </el-checkbox>
          </el-form-item>
          <el-form-item
            v-show="RCAGAddSameCom===false"
            label="通訊地址："
            :label-width="formLabelWidth"
          >
            <Address
              :sendedform="sendedformRCAGC"
              :resetall="RCAGAddSameCom"
              title="代理人通訊地址"
              @geteditaddress="geteditaddress"
            />
          </el-form-item>
        </el-row>
      </el-card>
      <h3>保證人</h3>
      <el-card>
        <el-row>
          <el-form-item
            label="姓名："
            :label-width="formLabelWidth"
          >
            <el-input
              v-model="editForm.RCGName"
              placeholder="請輸入姓名"
              autocomplete="off"
              clearable
            />
          </el-form-item>
          <el-form-item
            label="出生年月日："
            :label-width="formLabelWidth"
          >
            <DatePickerE
              v-model="editForm.RCGBirthday"
              value-format="yyyy-MM-dd"
              format="yyyy-MM-dd"
              type="date"
              placeholder="選擇日期"
              style="width: 200px"
            />
          </el-form-item>
          <el-form-item
            label="性別："
            :label-width="formLabelWidth"
          >
            <el-radio
              v-model="RCGGender"
              label="1"
            >男</el-radio>
            <el-radio
              v-model="RCGGender"
              label="2"
            >女</el-radio>
          </el-form-item>
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
              placeholder="請輸入身份證字號"
              autocomplete="off"
              clearable
              maxlength="10"
              style="width: 200px"
            />
          </el-form-item>
          <el-form-item
            label="聯絡電話："
            :label-width="formLabelWidth"
            prop="RCGCell"
          >
            <el-input
              v-model="editForm.RCGCell"
              placeholder="請輸入電話"
              autocomplete="off"
              clearable
            />
          </el-form-item>
        </el-row>
        <el-row>
          <el-form-item
            label="戶籍地址："
            :label-width="formLabelWidth"
          >
            <Address
              :sendedform="sendedformRCG"
              title="保證人2戶籍地址"
              @geteditaddress="geteditaddress"
            />
          </el-form-item>
        </el-row>
        <el-row>
          <el-form-item
            label=" "
            :label-width="formLabelWidth"
          >
            <el-checkbox v-model="RCGAddSameCom">通訊地址同戶籍地址
            </el-checkbox>
          </el-form-item>
          <el-form-item
            v-show="RCGAddSameCom===false"
            label="通訊地址："
            :label-width="formLabelWidth"
          >
            <Address
              :sendedform="sendedformRCGC"
              :resetall="RCGAddSameCom"
              title="保證人通訊地址"
              @geteditaddress="geteditaddress"
            />
          </el-form-item>
        </el-row>
      </el-card>
      <h3>匯款資料</h3>
      <el-card>
        <RemittanceWebForm
          :sendedform="sendRemittanceform"
          :fielddisabled="editForm.disabled"
          @getremittancebyemit="GetRemittanceByEmit"
        />
      </el-card>
      <h3>入住成員</h3>
      <el-card>
        <el-row>
          <el-badge
            class="mark"
            value="本人"
          />
          <!-- <el-tag type="success">1</el-tag> -->
          <el-form-item label="姓名：">
            <el-input
              v-model="editForm.RCName"
              placeholder="請輸入姓名"
              autocomplete="off"
              clearable
              class="w160px"
            />
          </el-form-item>
          <el-form-item
            label="統一編號："
            :label-width="formLabelWidth2"
            prop="RCID"
          >
            <el-input
              v-model="editForm.RCID"
              autocomplete="off"
              clearable
              class="w140px"
              disabled
            />
          </el-form-item>
          <!-- <el-form-item
            label="出生年月日："
            :label-width="formLabelWidth2"
          >
            <DatePickerE
              v-model="editForm.RCBirthday"
              value-format="yyyy-MM-dd"
              format="yyyy-MM-dd"
              type="date"
              placeholder="選擇日期"
              class="w140px"
            />
          </el-form-item> -->
          <el-form-item
            label="稱謂："
            :label-width="formLabelWidth2"
          >
            <el-input
              v-model="editForm.RCFRelation"
              placeholder="請輸入稱謂"
              autocomplete="off"
              clearable
              class="w120px"
            />
          </el-form-item>
          <el-form-item
            label="連絡電話："
            :label-width="formLabelWidth2"
            prop="RCCell"
          >
            <el-input
              v-model="editForm.RCCell"
              placeholder="請輸入連絡電話"
              autocomplete="off"
              clearable
              class="w140px"
            />
          </el-form-item>
        </el-row>
        <el-row>
          <el-badge
            class="mark"
            :value="1"
          />
          <el-form-item label="姓名：">
            <el-input
              v-model="editForm.RCFName_A"
              placeholder="請輸入姓名"
              autocomplete="off"
              clearable
              class="w160px"
            />
          </el-form-item>
          <el-form-item
            label="身份證字號："
            :label-width="formLabelWidth2"
            prop="RCFID_1_A"
          >
            <el-input
              v-model="editForm.RCFID_1_A"
              v-upperCase
              placeholder="請輸入身份證字號"
              autocomplete="off"
              clearable
              class="w200px"
              :maxlength="10"
            />
          </el-form-item>
          <el-form-item
            label="出生年月日："
            :label-width="formLabelWidth2"
          >
            <DatePickerE
              v-model="editForm.RCFBirthday_A"
              value-format="yyyy-MM-dd"
              format="yyyy-MM-dd"
              type="date"
              placeholder="選擇日期"
              class="w140px"
            />
          </el-form-item>
          <el-form-item
            label="稱謂："
            :label-width="formLabelWidth2"
          >
            <el-input
              v-model="editForm.RCFRelation_A"
              placeholder="請輸入稱謂"
              autocomplete="off"
              clearable
              class="w120px"
            />
          </el-form-item>
          <el-form-item
            label="連絡電話："
            :label-width="formLabelWidth2"
            prop="RCFCell_A"
          >
            <el-input
              v-model="editForm.RCFCell_A"
              placeholder="請輸入連絡電話"
              autocomplete="off"
              clearable
              class="w140px"
            />
          </el-form-item>
        </el-row>
        <el-row>
          <el-badge
            class="mark"
            :value="2"
          />
          <el-form-item label="姓名：">
            <el-input
              v-model="editForm.RCFName_B"
              placeholder="請輸入姓名"
              autocomplete="off"
              clearable
              class="w160px"
            />
          </el-form-item>
          <el-form-item
            label="身份證字號："
            :label-width="formLabelWidth2"
            prop="RCFID_1_B"
          >
            <el-input
              v-model="editForm.RCFID_1_B"
              v-upperCase
              placeholder="請輸入身份證字號"
              autocomplete="off"
              clearable
              class="w200px"
              maxlength="10"
            />
          </el-form-item>
          <el-form-item
            label="出生年月日："
            :label-width="formLabelWidth2"
          >
            <DatePickerE
              v-model="editForm.RCFBirthday_B"
              value-format="yyyy-MM-dd"
              format="yyyy-MM-dd"
              type="date"
              placeholder="選擇日期"
              class="w140px"
            />
          </el-form-item>
          <el-form-item
            label="稱謂："
            :label-width="formLabelWidth2"
          >
            <el-input
              v-model="editForm.RCFRelation_B"
              placeholder="請輸入稱謂"
              autocomplete="off"
              clearable
              class="w120px"
            />
          </el-form-item>
          <el-form-item
            label="連絡電話："
            :label-width="formLabelWidth2"
            prop="RCFCell_B"
          >
            <el-input
              v-model="editForm.RCFCell_B"
              placeholder="請輸入連絡電話"
              autocomplete="off"
              clearable
              class="w140px"
            />
          </el-form-item>
        </el-row>
        <el-row>
          <el-badge
            class="mark"
            :value="3"
          />
          <el-form-item label="姓名：">
            <el-input
              v-model="editForm.RCFName_C"
              placeholder="請輸入姓名"
              autocomplete="off"
              clearable
              class="w160px"
            />
          </el-form-item>
          <el-form-item
            label="身份證字號："
            :label-width="formLabelWidth2"
            prop="RCFID_1_C"
          >
            <el-input
              v-model="editForm.RCFID_1_C"
              v-upperCase
              placeholder="請輸入身份證字號"
              autocomplete="off"
              clearable
              class="w200px"
              maxlength="10"
            />
          </el-form-item>
          <el-form-item
            label="出生年月日："
            :label-width="formLabelWidth2"
          >
            <DatePickerE
              v-model="editForm.RCFBirthday_C"
              value-format="yyyy-MM-dd"
              format="yyyy-MM-dd"
              type="date"
              placeholder="選擇日期"
              class="w140px"
            />
          </el-form-item>
          <el-form-item
            label="稱謂："
            :label-width="formLabelWidth2"
          >
            <el-input
              v-model="editForm.RCFRelation_C"
              placeholder="請輸入稱謂"
              autocomplete="off"
              clearable
              class="w120px"
            />
          </el-form-item>
          <el-form-item
            label="連絡電話："
            :label-width="formLabelWidth2"
            prop="RCFCell_C"
          >
            <el-input
              v-model="editForm.RCFCell_C"
              placeholder="請輸入連絡電話"
              autocomplete="off"
              clearable
              class="w140px"
            />
          </el-form-item>
        </el-row>
        <el-row>
          <el-badge
            class="mark"
            :value="4"
          />
          <el-form-item label="姓名：">
            <el-input
              v-model="editForm.RCFName_D"
              placeholder="請輸入姓名"
              autocomplete="off"
              clearable
              class="w160px"
            />
          </el-form-item>
          <el-form-item
            label="身份證字號："
            :label-width="formLabelWidth2"
            prop="RCFID_1_D"
          >
            <el-input
              v-model="editForm.RCFID_1_D"
              v-upperCase
              placeholder="請輸入身份證字號"
              autocomplete="off"
              clearable
              class="w200px"
              maxlength="10"
            />
          </el-form-item>
          <el-form-item
            label="出生年月日："
            :label-width="formLabelWidth2"
          >
            <DatePickerE
              v-model="editForm.RCFBirthday_D"
              value-format="yyyy-MM-dd"
              format="yyyy-MM-dd"
              type="date"
              placeholder="選擇日期"
              class="w140px"
            />
          </el-form-item>
          <el-form-item
            label="稱謂："
            :label-width="formLabelWidth2"
          >
            <el-input
              v-model="editForm.RCFRelation_D"
              placeholder="請輸入稱謂"
              autocomplete="off"
              clearable
              class="w120px"
            />
          </el-form-item>
          <el-form-item
            label="連絡電話："
            :label-width="formLabelWidth2"
            prop="RCFCell_D"
          >
            <el-input
              v-model="editForm.RCFCell_D"
              placeholder="請輸入連絡電話"
              autocomplete="off"
              clearable
              class="w140px"
            />
          </el-form-item>
        </el-row>
        <el-row>
          <el-badge
            class="mark"
            :value="5"
          />
          <el-form-item label="姓名：">
            <el-input
              v-model="editForm.RCFName_E"
              placeholder="請輸入姓名"
              autocomplete="off"
              clearable
              class="w160px"
            />
          </el-form-item>
          <el-form-item
            label="身份證字號："
            :label-width="formLabelWidth2"
            prop="RCFID_1_E"
          >
            <el-input
              v-model="editForm.RCFID_1_E"
              v-upperCase
              placeholder="請輸入身份證字號"
              autocomplete="off"
              clearable
              class="w200px"
              maxlength="10"
            />
          </el-form-item>
          <el-form-item
            label="出生年月日："
            :label-width="formLabelWidth2"
          >
            <DatePickerE
              v-model="editForm.RCFBirthday_E"
              value-format="yyyy-MM-dd"
              format="yyyy-MM-dd"
              type="date"
              placeholder="選擇日期"
              class="w140px"
            />
          </el-form-item>
          <el-form-item
            label="稱謂："
            :label-width="formLabelWidth2"
          >
            <el-input
              v-model="editForm.RCFRelation_E"
              placeholder="請輸入稱謂"
              autocomplete="off"
              clearable
              class="w120px"
            />
          </el-form-item>
          <el-form-item
            label="連絡電話："
            :label-width="formLabelWidth2"
            prop="RCFCell_E"
          >
            <el-input
              v-model="editForm.RCFCell_E"
              placeholder="請輸入連絡電話"
              autocomplete="off"
              clearable
              class="w140px"
            />
          </el-form-item>
        </el-row>
        <el-row>
          <el-badge
            class="mark"
            :value="6"
          />
          <el-form-item label="姓名：">
            <el-input
              v-model="editForm.RCFName_F"
              placeholder="請輸入姓名"
              autocomplete="off"
              clearable
              class="w160px"
            />
          </el-form-item>
          <el-form-item
            label="身份證字號："
            :label-width="formLabelWidth2"
            prop="RCFID_1_F"
          >
            <el-input
              v-model="editForm.RCFID_1_F"
              v-upperCase
              placeholder="請輸入身份證字號"
              autocomplete="off"
              clearable
              class="w200px"
              maxlength="10"
            />
          </el-form-item>
          <el-form-item
            label="出生年月日："
            :label-width="formLabelWidth2"
          >
            <DatePickerE
              v-model="editForm.RCFBirthday_F"
              value-format="yyyy-MM-dd"
              format="yyyy-MM-dd"
              type="date"
              placeholder="選擇日期"
              class="w140px"
            />
          </el-form-item>
          <el-form-item
            label="稱謂："
            :label-width="formLabelWidth2"
          >
            <el-input
              v-model="editForm.RCFRelation_F"
              placeholder="請輸入稱謂"
              autocomplete="off"
              clearable
              class="w120px"
            />
          </el-form-item>
          <el-form-item
            label="連絡電話："
            :label-width="formLabelWidth2"
            prop="RCFCell_F"
          >
            <el-input
              v-model="editForm.RCFCell_F"
              placeholder="請輸入連絡電話"
              autocomplete="off"
              clearable
              class="w140px"
            />
          </el-form-item>
        </el-row>
        <el-row>
          <el-badge
            class="mark"
            :value="7"
          />
          <el-form-item label="姓名：">
            <el-input
              v-model="editForm.RCFName_G"
              placeholder="請輸入姓名"
              autocomplete="off"
              clearable
              class="w160px"
            />
          </el-form-item>
          <el-form-item
            label="身份證字號："
            :label-width="formLabelWidth2"
            prop="RCFID_1_G"
          >
            <el-input
              v-model="editForm.RCFID_1_G"
              v-upperCase
              placeholder="請輸入身份證字號"
              autocomplete="off"
              clearable
              class="w200px"
              maxlength="10"
            />
          </el-form-item>
          <el-form-item
            label="出生年月日："
            :label-width="formLabelWidth2"
          >
            <DatePickerE
              v-model="editForm.RCFBirthday_G"
              value-format="yyyy-MM-dd"
              format="yyyy-MM-dd"
              type="date"
              placeholder="選擇日期"
              class="w140px"
            />
          </el-form-item>
          <el-form-item
            label="稱謂："
            :label-width="formLabelWidth2"
          >
            <el-input
              v-model="editForm.RCFRelation_G"
              placeholder="請輸入稱謂"
              autocomplete="off"
              clearable
              class="w120px"
            />
          </el-form-item>
          <el-form-item
            label="連絡電話："
            :label-width="formLabelWidth2"
            prop="RCFCell_G"
          >
            <el-input
              v-model="editForm.RCFCell_G"
              placeholder="請輸入連絡電話"
              autocomplete="off"
              clearable
              class="w140px"
            />
          </el-form-item>
        </el-row>
      </el-card>
      <h3>入住習慣</h3>
      <el-card>
        <div class="text item">
          <span>
            <el-checkbox
              v-model="editForm.BZ011"
              true-label="/Yes"
              false-label="/Off"
            >目前家裡沒有寵物;</el-checkbox>
            目前家裡有寵物：
            <el-checkbox
              v-model="editForm.BZ012"
              true-label="/Yes"
              false-label="/Off"
            >狗
              <el-input
                v-model="editForm.BZ013"
                autocomplete="off"
                clearable
                size="mini"
                class="w80px"
              />隻
            </el-checkbox>
            <el-checkbox
              v-model="editForm.BZ014"
              true-label="/Yes"
              false-label="/Off"
            >貓
              <el-input
                v-model="editForm.BZ015"
                autocomplete="off"
                clearable
                size="mini"
                class="w80px"
              />隻
            </el-checkbox>
            <el-checkbox
              v-model="editForm.BZ016"
              true-label="/Yes"
              false-label="/Off"
            >其他
              <el-input
                v-model="editForm.BZ017"
                autocomplete="off"
                clearable
                size="mini"
                class="w120px"
              />
            </el-checkbox>
          </span>
        </div>
        <div class="text item">
          <span>
            方便聯繫時間
            <el-checkbox
              v-model="editForm.BZ018"
              true-label="/Yes"
              false-label="/Off"
            >9:00~12:00</el-checkbox>
            <el-checkbox
              v-model="editForm.BZ019"
              true-label="/Yes"
              false-label="/Off"
            >13:00~16:00</el-checkbox>
            <el-checkbox
              v-model="editForm.BZ020"
              true-label="/Yes"
              false-label="/Off"
            >17:00~20:00</el-checkbox>
            <el-checkbox
              v-model="editForm.BZ021"
              true-label="/Yes"
              false-label="/Off"
            >皆可</el-checkbox>
          </span>
        </div>
      </el-card>
      <h3>個人資料蒐集、處理及利用告知聲明暨同意書</h3>
      <el-card>
        <div class="text item">
          <span>本同意書係依據『個人資料保護法』所制定：</span>
        </div>
        <br>
        <div class="text item">
          <span>一、 本人同意配合貴公司蒐集、處理及利用立同意書人之個人資料，並同意貴公司員工以
            <el-checkbox
              v-model="editForm.BZ022"
              true-label="/Yes"
              false-label="/Off"
            >電話</el-checkbox>
            <el-checkbox
              v-model="editForm.BZ023"
              true-label="/Yes"
              false-label="/Off"
            >簡訊</el-checkbox>
            <el-checkbox
              v-model="editForm.BZ024"
              true-label="/Yes"
              false-label="/Off"
            >LINE</el-checkbox>
            <el-checkbox
              v-model="editForm.BZ025"
              true-label="/Yes"
              false-label="/Off"
            >親訪</el-checkbox>
            之方式聯繫。並保證以上資料均為屬實，如有虛偽並願自負法律責任。
          </span>
        </div>
        <div class="text item">
          <span>二、 本公司利用個人資料之期間、地區、對象及方式：<br>
            <!-- eslint-disable no-irregular-whitespace -->
            　　(一) 期間：自本同意書簽立日起，至立同意書人要求停止使用之日止。<br>
            　　(二) 地區：於本國領域內。<br>
            　　(三) 對象：本公司及與本公司直接或間接關係企業用於租賃關係管理、辨識身分、行銷廣宣或入居者審查等，或主管機關依法要求提供者。<br>
            <b>　　　　(如未通過審查，致雙方無法簽訂租賃契約，僅需退還定金予本人，無民法第249條第3款適用。)</b><br>
            　　(四) 方式：以書面或電子文件方式儲存、處理及利用。</span>
        </div>
        <span class="text item">三、 立同意書人就其個人資料得依個人資料保護法及其他相關法令向本公司查詢或請求閱覽、請求製給複製本、請求停止蒐集、處理或利用及請求刪除其個人資料，如個人資料有錯誤、缺漏者，立同意書人
          得請求補充、更正。<br>
          　　本公司處理立同意書人查詢或請求閱覽及請求製給複製本時，得向立同意書人酌收必要成本費用。
        </span>
        <div class="text item">
          <span>四、 立同意書人於簽訂本條款時，確已詳閱全文，並基於自由意思下簽署。</span>
        </div>
        <div class="text item">
          <span>立書同意書人：
            <!-- <el-image
                  v-show="editForm.SignatureImgPath_1 != null"
                  class="tmb"
                  :src="editForm.SignatureImgPath_1"
                /> -->
            <el-image
              :src="src_1"
              class="tmb"
            >
              <div slot="error">
                <div class="tmb"><i class="el-icon-picture-outline" /></div>
              </div>
            </el-image>
            <el-button
              type="success"
              icon="el-icon-edit"
              size="mini"
              round
              @click="centerDialogVisible = true"
            >簽名</el-button>
          </span>
        </div>
      </el-card>

    </el-form>
    <div style="margin-top: 20px; text-align: center">
      <el-button
        type="primary"
        size="small"
        icon="el-icon-paperclip"
        @click="upload()"
      >儲存</el-button>
      <el-button
        size="small"
        icon="el-icon-close"
        @click="cancel"
      >關閉</el-button>
      <el-button
        v-if="src_1"
        type="success"
        size="small"
        icon="el-icon-download"
        @click="SaveAndUploadAndDownoadPDF()"
      >下載並匯出PDF</el-button>
      <!-- <el-button type="info" @click="GetSignature()">簽名檔測試</el-button> -->
    </div>
    <el-dialog
      title="簽名"
      :visible.sync="centerDialogVisible"
      width="850px"
      :close-on-click-modal="false"
    >
      <div
        id="signDiv"
        style="border: 1px solid black"
      >
        <VueSignaturePad
          ref="signaturePad"
          width="800px"
          height="500px"
        />
      </div>
      <span
        slot="footer"
        class="dialog-footer"
      >
        <el-button
          type="primary"
          @click="save('1')"
        >儲存</el-button>
        <el-button @click="clear">清除</el-button>
        <el-button
          type="info"
          @click="centerDialogVisible = false"
        >關閉</el-button>
      </span>
    </el-dialog>
  </div>
</template>

<script>
import { getImg } from '@/api/chaochi/externalform/externalformservice';
import {
  UploadWithData,
  ImgUpload
} from '@/api/chaochi/internalform/internalform';
import {
  validateCellReg,
  validateEamilReg,
  validateIDReg,
  validateBusinessIDReg
} from '@/utils/validate';
import Address from '@/components/Address/Address.vue';
import DatePickerE from '@/components/RocDatepickerE';
import RemittanceWebForm from '@/components/Remittance/RemittanceForWebForm.vue';
import JsPDF from 'jspdf';
import html2canvas from 'html2canvas';
export default {
  name: 'A000004',
  components: { Address, DatePickerE, RemittanceWebForm },
  props: {
    editform: { type: Object, default: null }
  },
  data() {
    return {
      editForm: { ...this.editform },
      sendedform: {},
      sendedform1: {},
      sendedformRCEC: {},
      sendedformRCECC: {},
      sendedformRCECF: {},
      sendedformRCECFC: {},
      sendedformRCAG: {},
      sendedformRCAGC: {},
      sendedformRCG: {},
      sendedformRCGC: {},
      sendRemittanceform: {},
      rule: {
        RCID: [
          { required: true, trigger: 'blur', validator: validateBusinessIDReg }
        ],
        RCECID: [
          { required: false, trigger: 'blur', validator: validateIDReg }
        ],
        RCECFID: [
          { required: false, trigger: 'blur', validator: validateIDReg }
        ],
        RCAGID: [
          { required: false, trigger: 'blur', validator: validateIDReg }
        ],
        RCGID: [{ required: false, trigger: 'blur', validator: validateIDReg }],
        RCFID_1_A: [
          { required: false, trigger: 'blur', validator: validateIDReg }
        ],
        RCFID_1_B: [
          { required: false, trigger: 'blur', validator: validateIDReg }
        ],
        RCFID_1_C: [
          { required: false, trigger: 'blur', validator: validateIDReg }
        ],
        RCFID_1_D: [
          { required: false, trigger: 'blur', validator: validateIDReg }
        ],
        RCFID_1_E: [
          { required: false, trigger: 'blur', validator: validateIDReg }
        ],
        RCFID_1_F: [
          { required: false, trigger: 'blur', validator: validateIDReg }
        ],
        RCFID_1_G: [
          { required: false, trigger: 'blur', validator: validateIDReg }
        ],
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
            message: '請輸入負責人'
          }
        ],
        RCTel_2: [
          {
            required: true,
            trigger: 'blur',
            message: '請輸入號碼'
          }
        ],
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
        ],
        RCCell: [
          { required: false, trigger: 'change', validator: validateCellReg }
        ],
        RCMail: [
          { required: false, trigger: 'change', validator: validateEamilReg }
        ],
        RCFCell_A: [
          { required: false, trigger: 'change', validator: validateCellReg }
        ],
        RCFCell_B: [
          { required: false, trigger: 'change', validator: validateCellReg }
        ],
        RCFCell_C: [
          { required: false, trigger: 'change', validator: validateCellReg }
        ],
        RCFCell_D: [
          { required: false, trigger: 'change', validator: validateCellReg }
        ],
        RCFCell_E: [
          { required: false, trigger: 'change', validator: validateCellReg }
        ],
        RCFCell_F: [
          { required: false, trigger: 'change', validator: validateCellReg }
        ],
        RCFCell_G: [
          { required: false, trigger: 'change', validator: validateCellReg }
        ]
      },
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
      formLabelWidth: '130px',
      formLabelWidth2: '100px',
      RCECGender: '',
      RCECFGender: '',
      RCAGGender: '',
      RCGGender: '',
      src_1: '',
      centerDialogVisible: false,
      RCAddSameCom: false,
      RCECFAddSameCom: false,
      RCAGAddSameCom: false,
      RCGAddSameCom: false,
      RCECAddSameCom: false
    };
  },
  watch: {
    editform: {
      immediate: true,
      handler() {
        console.log('editformch');
        if (Object.keys(this.editform).length !== 0) {
          this.editForm = this.editform;
          this.editForm.RCFRelation = '本人';
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
          this.sendedformRCEC = {
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
          this.sendedformRCECC = {
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
          this.sendedformRCECF = {
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
          this.sendedformRCECFC = {
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
          this.sendedformRCAG = {
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
          this.sendedformRCAGC = {
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
          this.sendedformRCG = {
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
          this.sendedformRCGC = {
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
          this.sendRemittanceform = {
            LAName: this.editForm.RCName,
            LBankName: this.editForm.RBankName,
            LBankNo: this.editForm.RBankNo,
            LBranchName: this.editForm.RBranchName,
            LBranchNo: this.editForm.RBranchNo,
            LANo: this.editForm.RANo
          };
          this.RCECGender =
            this.editForm.RCECGender_1 === '/Yes'
              ? '1'
              : this.editForm.RCECGender_2 === '/Yes'
                ? '2'
                : '';
          this.RCECFGender =
            this.editForm.RCECFGender_1 === '/Yes'
              ? '1'
              : this.editForm.RCECFGender_2 === '/Yes'
                ? '2'
                : '';
          this.RCAGGender =
            this.editForm.RCAGGender_1 === '/Yes'
              ? '1'
              : this.editForm.RCAGGender_2 === '/Yes'
                ? '2'
                : '';
          this.RCGGender =
            this.editForm.RCGGender_1 === '/Yes'
              ? '1'
              : this.editForm.RCGGender_2 === '/Yes'
                ? '2'
                : '';
          this.RCAddSameCom = this.editForm.RCAddSame === '/Yes';
          this.RCECAddSameCom = this.editForm.RCECAddSame === '/Yes';
          this.RCECFAddSameCom = this.editForm.RCECFAddSame === '/Yes';
          this.RCAGAddSameCom = this.editForm.RCAGAddSame === '/Yes';
          this.RCGAddSameCom = this.editForm.RCGAddSame === '/Yes';
          if (!this.editForm.BZ022) {
            this.editForm.BZ022 = '/Yes';
          }
          if (!this.editForm.BZ023) {
            this.editForm.BZ023 = '/Yes';
          }
          if (!this.editForm.BZ024) {
            this.editForm.BZ024 = '/Yes';
          }
          if (!this.editForm.BZ025) {
            this.editForm.BZ025 = '/Yes';
          }
          this.$emit('openpageloading');
          this.GetSig();
        }
      }
    },
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
    },
    RCECGender: {
      handler() {
        switch (this.RCECGender) {
          case '1':
            this.editForm.RCECGender_1 = '/Yes';
            this.editForm.RCECGender_2 = '/Off';
            break;
          case '2':
            this.editForm.RCECGender_1 = '/Off';
            this.editForm.RCECGender_2 = '/Yes';
            break;
        }
      }
    },
    RCECFGender: {
      handler() {
        switch (this.RCECFGender) {
          case '1':
            this.editForm.RCECFGender_1 = '/Yes';
            this.editForm.RCECFGender_2 = '/Off';
            break;
          case '2':
            this.editForm.RCECFGender_1 = '/Off';
            this.editForm.RCECFGender_2 = '/Yes';
            break;
        }
      }
    },
    RCAGGender: {
      handler() {
        switch (this.RCAGGender) {
          case '1':
            this.editForm.RCAGGender_1 = '/Yes';
            this.editForm.RCAGGender_2 = '/Off';
            break;
          case '2':
            this.editForm.RCAGGender_1 = '/Off';
            this.editForm.RCAGGender_2 = '/Yes';
            break;
        }
      }
    },
    RCGGender: {
      handler() {
        switch (this.RCGGender) {
          case '1':
            this.editForm.RCGGender_1 = '/Yes';
            this.editForm.RCGGender_2 = '/Off';
            break;
          case '2':
            this.editForm.RCGGender_1 = '/Off';
            this.editForm.RCGGender_2 = '/Yes';
            break;
        }
      }
    }
  },
  methods: {
    cancel() {
      this.$emit('cancel');
    },
    clear() {
      this.$refs.signaturePad.clearSignature();
    },
    save(params) {
      const { data } = this.$refs.signaturePad.saveSignature();
      switch (params) {
        case '1':
          this.editForm.SignatureBase64_1 = data;
          this.src_1 = data;
          break;
      }
      this.centerDialogVisible = false;
      this.$refs.signaturePad.clearSignature();
    },
    upload() {
      this.$refs['editForm'].validate((valid, err) => {
        if (valid) {
          this.UploadWithData();
        } else {
          var msg = '';
          var errArr = Object.keys(err);
          errArr.forEach(x => {
            switch (x) {
              case 'RCName':
                msg += '*名稱<br/>';
                break;
              case 'RCID':
                msg += '*統一編號<br/>';
                break;
              // case 'RCCell':
              //   msg += '*手機<br/>';
              case 'RCRep':
                msg += '負責人<br/>';
                break;
              case 'RCTel_2':
                msg += '*電話號碼<br/>';
                break;
            }
          });
          this.$alert(
            `表單驗證未通過：<br/><div style="color:#e24e4e"><i><b>${msg}<b/><i/><div/>`,
            '提示',
            {
              confirmButtonText: '確定',
              dangerouslyUseHTMLString: true
            }
          );
          return false;
        }
      });
    },
    UploadWithData() {
      this.$emit('openpageloading');
      this.editForm.IsGenPDF = 'N';
      UploadWithData(this.editForm.FormId, this.editForm)
        .then(res => {
          if (res.Success) {
            this.$message.success('儲存成功!');
          } else {
            this.$message.error('失敗');
          }
        })
        .finally(() => {
          this.$emit('closepageloading');
        });
    },
    GetSig() {
      if (this.editForm.SignatureImgPath_1) {
        getImg(this.editForm.SignatureImgPath_1)
          .then(response => {
            this.src_1 = URL.createObjectURL(response);
          })
          .finally(() => {
            this.$emit('closepageloading');
          });
      } else {
        this.src_1 = null;
        this.$emit('closepageloading');
      }

      //   if (this.editForm.SignatureImgPath_2) {
      //     getImg(this.editForm.SignatureImgPath_2).then(response => {
      //       this.editForm.SignatureImgPath_2 = URL.createObjectURL(response);
      //     });
      //   }
      //   if (this.editForm.SignatureImgPath_3) {
      //     getImg(this.editForm.SignatureImgPath_3).then(response => {
      //       this.editForm.SignatureImgPath_3 = URL.createObjectURL(response);
      //     });
      //   }
      //   if (this.editForm.SignatureImgPath_4) {
      //     getImg(this.editForm.SignatureImgPath_4).then(response => {
      //       this.editForm.SignatureImgPath_4 = URL.createObjectURL(response);
      //     });
      //   }
      //   if (this.editForm.SignatureImgPath_5) {
      //     getImg(this.editForm.SignatureImgPath_5).then(response => {
      //       this.editForm.SignatureImgPath_5 = URL.createObjectURL(response);
      //     });
      //   }
    },
    SaveAndUploadAndDownoadPDF() {
      this.$refs['editForm'].validate((valid, err) => {
        if (valid) {
          this.$emit('openpageloading');
          this.editForm.IsGenPDF = 'Y';
          UploadWithData(this.editForm.FormId, this.editForm).then(res => {
            if (res.Success) {
              // this.$message.success('儲存成功!');
              if (res.ResData === '送審失敗') {
                this.$message.error('入居者審查送審失敗!');
                return;
              }
              var w = this.$refs.editForm.$el.clientWidth;
              var h = this.$refs.editForm.$el.clientHeight;
              var fileName = res.ResData;
              const doc = new JsPDF('p', 'pt', [h, w]);
              var test = this.$refs.editForm.$el;
              test.forEach(el => {
                el.removeAttribute('placeholder'); // 把placeholder拿掉
              });
              html2canvas(this.$refs.editForm.$el, {
                scale: 1.25
              }).then(canvas => {
                const img = canvas.toDataURL('image/jpeg');
                doc.addImage(img, 'JPEG', 10, 10);
                const blob = doc.output('blob');
                var file = new File([blob], '法人房客資料卡.pdf', {
                  type: 'pdf'
                });
                const formData = new FormData();
                formData.append('File', file);
                formData.append('id', fileName);
                formData.append('role', 'RC');
                formData.append('IDNo', this.editForm.RCID);
                formData.append('formname', this.editForm.FormName);
                ImgUpload(formData)
                  .then(res => {
                    if (res.Success) {
                      this.$message.success('上傳並儲存成功');
                      const link = document.createElement('a');
                      link.href = URL.createObjectURL(blob);
                      link.download = '法人房客資料卡.pdf';
                      link.click();
                      URL.revokeObjectURL(link.href);
                      this.cancel();
                    } else {
                      this.$message.error('上傳失敗');
                    }
                  })
                  .finally(() => {
                    this.$emit('closepageloading');
                  });
              });
            } else {
              this.$message.error('失敗');
            }
          });
        } else {
          var msg = '';
          var errArr = Object.keys(err);
          errArr.forEach(x => {
            switch (x) {
              case 'RCName':
                msg += '*名稱<br/>';
                break;
              case 'RCID':
                msg += '*統一編號<br/>';
                break;
              // case 'RCCell':
              //   msg += '*手機<br/>';
              case 'RCRep':
                msg += '負責人<br/>';
                break;
              case 'RCTel_2':
                msg += '*電話號碼<br/>';
                break;
            }
          });
          this.$alert(
            `表單驗證未通過：<br/><div style="color:#e24e4e"><i><b>${msg}<b/><i/><div/>`,
            '提示',
            {
              confirmButtonText: '確定',
              dangerouslyUseHTMLString: true
            }
          );
          return false;
        }
      });
    },
    GetRemittanceByEmit(remittance) {
      this.editForm.RAName = remittance.LAName;
      this.editForm.RANo = remittance.LANo;
      this.editForm.RBankName = remittance.LBankName;
      this.editForm.RBankNo = remittance.LBankNo;
      this.editForm.RBranchName = remittance.LBranchName;
      this.editForm.RBranchNo = remittance.LBranchNo;
    },
    geteditaddress(addressData, title) {
      switch (title) {
        case '登記地址':
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
          break;
        case '公司營業地址':
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
          break;
        case '緊急聯絡人戶籍地址':
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
          break;
        case '緊急聯絡人通訊地址':
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
          break;
        case '緊急聯絡人2戶籍地址':
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
          break;
        case '緊急聯絡人2通訊地址':
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
          break;
        case '代理人戶籍地址':
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
          break;
        case '代理人通訊地址':
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
          break;
        case '保證人戶籍地址':
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
          break;
        case '保證人通訊地址':
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
          break;

        default:
          break;
      }
    }
  }
};
</script>

<style>
</style>
