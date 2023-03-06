<template>
  <div>
    <el-form
      ref="editForm"
      :inline="true"
      :model="editForm"
      class="demo-form-inline"
      :rules="rules"
    >
      <el-row :gutter="20">
        <el-col :span="24">
          <el-card>
            <el-row>
              <el-col :span="8">
                <el-form-item
                  label="姓名："
                  :label-width="formLabelWidth"
                  prop="RNName"
                >
                  <el-input
                    v-model="editForm.RNName"
                    placeholder="請輸入姓名"
                    autocomplete="off"
                    clearable
                  />
                </el-form-item>
              </el-col>
              <el-col :span="8">
                <el-form-item
                  label="性別："
                  :label-width="formLabelWidth"
                  class="el-form-item is-required el-form-item--medium"
                >
                  <el-radio
                    v-model="RNgender"
                    label="1"
                  >男</el-radio>
                  <el-radio
                    v-model="RNgender"
                    label="2"
                  >女</el-radio>
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="8">
                <el-form-item
                  label="身份證字號："
                  :label-width="formLabelWidth"
                  prop="RNID"
                >
                  <el-input
                    v-model="editForm.RNID"
                    v-upperCase
                    autocomplete="off"
                    clearable
                    :disabled="!editForm.IsNew"
                  />
                </el-form-item>
              </el-col>
              <el-col :span="8">
                <el-form-item
                  label="出生年月日："
                  :label-width="formLabelWidth"
                  prop="RNBirthday"
                >
                  <DatePickerE
                    v-model="editForm.RNBirthday"
                    value-format="yyyy-MM-dd"
                    format="yyyy-MM-dd"
                    type="date"
                    placeholder="選擇日期"
                    style="width: 200px"
                  />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="8">
                <el-form-item
                  label="電話："
                  :label-width="formLabelWidth"
                >
                  <el-input
                    v-model="editForm.RNTel_2"
                    placeholder="請輸入電話"
                    clearable
                    style="width: 220px"
                    class="input-with-select"
                  >
                    <el-select
                      slot="prepend"
                      v-model="editForm.RNTel_1"
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
              <el-col :span="8">
                <el-form-item
                  label="手機："
                  :label-width="formLabelWidth"
                  prop="RNCell"
                >
                  <el-input
                    v-model="editForm.RNCell"
                    placeholder="請輸入手機"
                    autocomplete="off"
                    clearable
                  />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="8">
                <el-form-item
                  label="電子信箱："
                  :label-width="formLabelWidth"
                  prop="RNMail"
                >
                  <el-input
                    v-model="editForm.RNMail"
                    placeholder="請輸入電子信箱"
                    autocomplete="off"
                    clearable
                  />
                </el-form-item>
              </el-col>
              <el-col :span="8">
                <el-form-item
                  label="Line ID："
                  :label-width="formLabelWidth"
                >
                  <el-input
                    v-model="editForm.RNLine"
                    placeholder="請輸入LineID"
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
                  :label-width="formLabelWidth"
                >
                  <el-checkbox
                    v-model="RNAddSameCom"
                  >通訊地址同戶籍地址
                  </el-checkbox>
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="24">
                <el-form-item
                  v-show="RNAddSameCom === false"
                  label="通訊地址："
                  :label-width="formLabelWidth"
                >
                  <Address
                    :sendedform="sendedform1"
                    :resetall="RNAddSameCom"
                    title="通訊地址"
                    @geteditaddress="geteditaddress"
                  />
                </el-form-item>
              </el-col>
            </el-row>
          </el-card>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="24">
          <h3>就業狀況</h3>
          <el-card>
            <el-form-item label="">
              <el-radio
                v-model="BZ001003006" label="3"
              >在職中，公司名稱：
                <el-input
                  v-model="editForm.BZ007"
                  autocomplete="off"
                  clearable
                  size="mini"
                  class="w140px"
                />
                公司電話：
                <el-input
                  v-model="editForm.BZ008"
                  autocomplete="off"
                  clearable
                  size="mini"
                  class="w140px"
                />
                部門：
                <el-input
                  v-model="editForm.BZ009"
                  autocomplete="off"
                  clearable
                  size="mini"
                  class="w140px"
                />
                職稱：
                <el-input
                  v-model="editForm.BZ010"
                  autocomplete="off"
                  clearable
                  size="mini"
                  class="w140px"
                />
                上班時間：
                <el-input
                  v-model="editForm.BZ011"
                  autocomplete="off"
                  clearable
                  size="mini"
                  class="w140px"
                />
              </el-radio>
            </el-form-item>
            <br>
            <el-form-item label="">
              <el-radio
                v-model="BZ001003006" label="1"
              >待業中（
                <el-input
                  v-model="editForm.BZ002"
                  autocomplete="off"
                  clearable
                  size="mini"
                  class="w80px"
                />）個月
              </el-radio>
            </el-form-item>
            <br>
            <el-form-item label="">
              <el-radio
                v-model="BZ001003006" label="2"
              >就學中（就讀學校：
                <el-input
                  v-model="editForm.BZ004"
                  autocomplete="off"
                  clearable
                  size="mini"
                  class="w140px"
                />
                系級
                <el-input
                  v-model="editForm.BZ005"
                  autocomplete="off"
                  clearable
                  size="mini"
                  class="w140px"
                />）
              </el-radio>
            </el-form-item>
          </el-card>
        </el-col>
      </el-row>
      <el-row>
        <el-col>
          <h3>緊急聯絡人1</h3>
          <el-card>
            <el-row>
              <el-col :span="8">
                <el-form-item
                  label="姓名："
                  :label-width="formLabelWidth"
                  prop="RNECName"
                >
                  <el-input
                    v-model="editForm.RNECName"
                    placeholder="請輸入姓名"
                    autocomplete="off"
                    clearable
                  />
                </el-form-item>
              </el-col>
              <el-col :span="8">
                <el-form-item
                  label="性別："
                  :label-width="formLabelWidth"
                >
                  <el-radio
                    v-model="BZ012BZ013"
                    label="1"
                  >男</el-radio>
                  <el-radio
                    v-model="BZ012BZ013"
                    label="2"
                  >女</el-radio>
                </el-form-item>
              </el-col>
              <el-col :span="8">
                <el-form-item
                  label="出生年月日："
                  :label-width="formLabelWidth"
                  prop="RNECBirthday"
                >
                  <DatePickerE
                    v-model="editForm.RNECBirthday"
                    value-format="yyyy-MM-dd"
                    format="yyyy-MM-dd"
                    type="date"
                    placeholder="選擇日期"
                    style="width: 200px"
                  />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="8">
                <el-form-item
                  label="關係："
                  :label-width="formLabelWidth"
                  prop="RNECRelation"
                >
                  <el-input
                    v-model="editForm.RNECRelation"
                    placeholder="請輸入關係"
                    autocomplete="off"
                    clearable
                  />
                </el-form-item>
              </el-col>
              <el-col :span="8">
                <el-form-item
                  label="身份證字號："
                  :label-width="formLabelWidth"
                  prop="RNECID"
                >
                  <el-input
                    v-model="editForm.RNECID"
                    v-upperCase
                    placeholder="請輸入身份證字號"
                    autocomplete="off"
                    clearable
                    maxlength="10"
                  />
                </el-form-item>
              </el-col>
              <el-col :span="8">
                <el-form-item
                  label="聯絡電話："
                  :label-width="formLabelWidth"
                  prop="RNECCell"
                >
                  <el-input
                    v-model="editForm.RNECCell"
                    placeholder="請輸入電話"
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
                >
                  <Address
                    :sendedform="sendedformRNEC"
                    title="緊急聯絡人戶籍地址"
                    @geteditaddress="geteditaddress"
                  />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="24">
                <el-form-item
                  label=" "
                  :label-width="formLabelWidth"
                >
                  <el-checkbox
                    v-model="RNECAddSameCom"
                  >通訊地址同戶籍地址
                  </el-checkbox>
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="24">
                <el-form-item
                  v-show="RNECAddSameCom === false"
                  label="通訊地址："
                  :label-width="formLabelWidth"
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
          </el-card>
        </el-col>
      </el-row>
      <el-row>
        <el-col>
          <h3>緊急聯絡人2</h3>
          <el-card>
            <el-row>
              <el-col :span="8">
                <el-form-item
                  label="姓名："
                  :label-width="formLabelWidth"
                  prop="BZ043"
                >
                  <el-input
                    v-model="editForm.BZ043"
                    placeholder="請輸入姓名"
                    autocomplete="off"
                    clearable
                  />
                </el-form-item>
              </el-col>
              <el-col :span="8">
                <el-form-item
                  label="性別："
                  :label-width="formLabelWidth"
                >
                  <el-radio
                    v-model="BZ044045"
                    label="1"
                  >男</el-radio>
                  <el-radio
                    v-model="BZ044045"
                    label="2"
                  >女</el-radio>
                </el-form-item>
              </el-col>
              <el-col :span="8">
                <el-form-item
                  label="出生年月日："
                  :label-width="formLabelWidth"
                  prop="BZ046"
                >
                  <DatePickerE
                    v-model="editForm.BZ046"
                    value-format="yyyy-MM-dd"
                    format="yyyy-MM-dd"
                    type="date"
                    placeholder="選擇日期"
                    style="width: 200px"
                  />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="8">
                <el-form-item
                  label="關係："
                  :label-width="formLabelWidth"
                  prop="BZ047"
                >
                  <el-input
                    v-model="editForm.BZ047"
                    placeholder="請輸入關係"
                    autocomplete="off"
                    clearable
                  />
                </el-form-item>
              </el-col>
              <el-col :span="8">
                <el-form-item
                  label="身份證字號："
                  :label-width="formLabelWidth"
                  prop="BZ048"
                >
                  <el-input
                    v-model="editForm.BZ048"
                    v-upperCase
                    placeholder="請輸入身份證字號"
                    autocomplete="off"
                    clearable
                  />
                </el-form-item>
              </el-col>
              <el-col :span="8">
                <el-form-item
                  label="聯絡電話："
                  :label-width="formLabelWidth"
                  prop="BZ049"
                >
                  <el-input
                    v-model="editForm.BZ049"
                    placeholder="請輸入電話"
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
                >
                  <Address
                    :sendedform="sendedformRNEC2"
                    title="緊急聯絡人2戶籍地址"
                    @geteditaddress="geteditaddress"
                  />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="24">
                <el-form-item
                  label=" "
                  :label-width="formLabelWidth"
                >
                  <el-checkbox
                    v-model="RNEC2AddSameCom"
                  >通訊地址同戶籍地址
                  </el-checkbox>
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="24">
                <el-form-item
                  v-show="RNEC2AddSameCom === false"
                  label="通訊地址："
                  :label-width="formLabelWidth"
                >
                  <Address
                    :sendedform="sendedformRNECC2"
                    :resetall="RNEC2AddSameCom"
                    title="緊急聯絡人2通訊地址"
                    @geteditaddress="geteditaddress"
                  />
                </el-form-item>
              </el-col>
            </el-row>
          </el-card>
        </el-col>
      </el-row>
      <el-row>
        <el-col>
          <h3>代理人資訊(無則免填)</h3>
          <el-card>
            <el-row>
              <el-col :span="8">
                <el-form-item
                  label="姓名："
                  :label-width="formLabelWidth"
                >
                  <el-input
                    v-model="editForm.RNAGName_A"
                    placeholder="請輸入姓名"
                    autocomplete="off"
                    clearable
                  />
                </el-form-item>
              </el-col>
              <el-col :span="8">
                <el-form-item
                  label="性別："
                  :label-width="formLabelWidth"
                >
                  <el-radio
                    v-model="BZ014BZ015"
                    label="1"
                  >男</el-radio>
                  <el-radio
                    v-model="BZ014BZ015"
                    label="2"
                  >女</el-radio>
                </el-form-item>
              </el-col>
              <el-col :span="8">
                <el-form-item
                  label="出生年月日："
                  :label-width="formLabelWidth"
                >
                  <DatePickerE
                    v-model="editForm.RNAGBirthday"
                    value-format="yyyy-MM-dd"
                    format="yyyy-MM-dd"
                    type="date"
                    placeholder="選擇日期"
                    style="width: 200px"
                  />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="8">
                <el-form-item
                  label="身份證字號："
                  :label-width="formLabelWidth"
                  prop="RNAGID_A"
                >
                  <el-input
                    v-model="editForm.RNAGID_A"
                    v-upperCase
                    placeholder="請輸入身份證字號"
                    autocomplete="off"
                    clearable
                    maxlength="10"
                  />
                </el-form-item>
              </el-col>
              <el-col :span="8">
                <el-form-item
                  label="聯絡電話："
                  :label-width="formLabelWidth"
                  prop="RNAGCell_A"
                >
                  <el-input
                    v-model="editForm.RNAGCell_A"
                    placeholder="請輸入電話"
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
                >
                  <Address
                    :sendedform="sendedformRNAG"
                    title="代理人戶籍地址"
                    @geteditaddress="geteditaddress"
                  />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="24">
                <el-form-item
                  label=" "
                  :label-width="formLabelWidth"
                >
                  <el-checkbox
                    v-model="RNAGAddSameCom"
                  >通訊地址同戶籍地址
                  </el-checkbox>
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="24">
                <el-form-item
                  v-show="RNAGAddSameCom === false"
                  label="通訊地址："
                  :label-width="formLabelWidth"
                >
                  <Address
                    :sendedform="sendedformRNAGC"
                    :resetall="RNAGAddSameCom"
                    title="代理人通訊地址"
                    @geteditaddress="geteditaddress"
                  />
                </el-form-item>
              </el-col>
            </el-row>
          </el-card>
        </el-col>
      </el-row>
      <el-row :gutter="20">
        <el-col :span="6">
          <h3>籍貫</h3>
          <el-card>
            <el-radio
              v-model="BZ018BZ019"
              label="1"
            >本國籍</el-radio>
            <el-radio
              v-model="BZ018BZ019"
              label="2"
            >非本國籍</el-radio>
          </el-card>
        </el-col>
        <el-col :span="18">
          <h3>其他身份</h3>
          <el-card>
            <el-checkbox
              v-model="editForm.RNQualify3C_12"
              label="未滿18歲之未成年人"
              true-label="/Yes"
              false-label="/Off"
              size="medium"
            />
            <el-checkbox
              v-model="editForm.RNQualify2C_1"
              label="65歲以上之年長者"
              true-label="/Yes"
              false-label="/Off"
              size="medium"
            />
            <el-checkbox
              v-model="editForm.RNQualify2C_2"
              label="原住民"
              true-label="/Yes"
              false-label="/Off"
              size="medium"
            />
            <el-checkbox
              v-model="editForm.RNQualify2C_3"
              label="育有未成年子女3人以上"
              true-label="/Yes"
              false-label="/Off"
              size="medium"
            />
            <el-checkbox
              v-model="editForm.RNQualify2C_4"
              label="特殊境遇家庭"
              true-label="/Yes"
              false-label="/Off"
              size="medium"
            />
            <el-checkbox
              v-model="editForm.RNQualify2C_5"
              label="於安置教養機構或寄養家庭結束安置無法返家，未滿二十五"
              true-label="/Yes"
              false-label="/Off"
              size="medium"
            />
            <el-checkbox
              v-model="editForm.RNQualify2C_6"
              label="家暴/性侵害者及其子女"
              true-label="/Yes"
              false-label="/Off"
              size="medium"
            />
            <el-checkbox
              v-model="editForm.RNQualify2C_7"
              label="身心障礙者"
              true-label="/Yes"
              false-label="/Off"
              size="medium"
            />
            <el-checkbox
              v-model="editForm.RNQualify2C_8"
              label="感染人類免疫缺乏病毒者或罹患後天免疫缺乏症候群者"
              true-label="/Yes"
              false-label="/Off"
              size="medium"
            />
            <el-checkbox
              v-model="editForm.RNQualify2C_9"
              label="災民"
              true-label="/Yes"
              false-label="/Off"
              size="medium"
            />
            <el-checkbox
              v-model="editForm.RNQualify2C_10"
              label="遊民"
              true-label="/Yes"
              false-label="/Off"
              size="medium"
            />
            <el-checkbox
              v-model="editForm.RNQualify3C_1"
              label="低收入戶"
              true-label="/Yes"
              false-label="/Off"
              size="medium"
            />
            <el-checkbox
              v-model="editForm.RNQualify3C_2"
              label="中低收入戶"
              true-label="/Yes"
              false-label="/Off"
              size="medium"
            />
          </el-card>
        </el-col>
      </el-row>
      <el-row>
        <el-col>
          <h3>保證人</h3>
          <el-card>
            <el-row>
              <el-col :span="8">
                <el-form-item
                  label="姓名："
                  :label-width="formLabelWidth"
                >
                  <el-input
                    v-model="editForm.RNGName"
                    placeholder="請輸入姓名"
                    autocomplete="off"
                    clearable
                  />
                </el-form-item>
              </el-col>
              <el-col :span="8">
                <el-form-item
                  label="性別："
                  :label-width="formLabelWidth"
                >
                  <el-radio
                    v-model="BZ020BZ021"
                    label="1"
                  >男</el-radio>
                  <el-radio
                    v-model="BZ020BZ021"
                    label="2"
                  >女</el-radio>
                </el-form-item>
              </el-col>
              <el-col :span="8">
                <el-form-item
                  label="出生年月日："
                  :label-width="formLabelWidth"
                >
                  <DatePickerE
                    v-model="editForm.RNGBirthday"
                    value-format="yyyy-MM-dd"
                    format="yyyy-MM-dd"
                    type="date"
                    placeholder="選擇日期"
                    style="width: 200px"
                  />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="8">
                <el-form-item
                  label="關係："
                  :label-width="formLabelWidth"
                >
                  <el-input
                    v-model="editForm.BZ022"
                    placeholder="請輸入關係"
                    autocomplete="off"
                    clearable
                  />
                </el-form-item>
              </el-col>
              <el-col :span="8">
                <el-form-item
                  label="身份證字號："
                  :label-width="formLabelWidth"
                  prop="RNGID"
                >
                  <el-input
                    v-model="editForm.RNGID"
                    v-upperCase
                    placeholder="請輸入身份證字號"
                    autocomplete="off"
                    clearable
                    :maxlength="10"
                  />
                </el-form-item>
              </el-col>
              <el-col :span="8">
                <el-form-item
                  label="聯絡電話："
                  :label-width="formLabelWidth"
                  prop="RNGCell"
                >
                  <el-input
                    v-model="editForm.RNGCell"
                    placeholder="請輸入電話"
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
                  :label-width="formLabelWidth"
                >
                  <el-checkbox
                    v-model="RNGAddSameCom"
                  >通訊地址同戶籍地址
                  </el-checkbox>
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="24">
                <el-form-item
                  v-show="RNGAddSameCom === false"
                  label="通訊地址："
                  :label-width="formLabelWidth"
                >
                  <Address
                    :sendedform="sendedformRNGC"
                    :resetall="RNGAddSameCom"
                    title="保證人通訊地址"
                    @geteditaddress="geteditaddress"
                  />
                </el-form-item>
              </el-col>
            </el-row>
          </el-card>
        </el-col>
      </el-row>
      <el-row>
        <el-col>
          <h3>家庭成員</h3>
          <el-card>
            <el-row>
              <el-col :span="4">
                <el-badge
                  class="mark"
                  value="本人"
                />
                <!-- <el-tag type="success">1</el-tag> -->
                <el-form-item label="姓名：">
                  <el-input
                    v-model="editForm.RNName"
                    placeholder="請輸入姓名"
                    autocomplete="off"
                    clearable
                    class="w160px"
                  />
                </el-form-item>
              </el-col>
              <el-col :span="4.5">
                <el-form-item
                  label="身份證字號："
                  :label-width="formLabelWidth2"
                >
                  <el-input
                    v-model="editForm.RNID"
                    v-upperCase
                    placeholder="請輸入身份證字號"
                    autocomplete="off"
                    clearable
                    class="w200px"
                    :maxlength="10"
                    disabled
                  />
                </el-form-item>
              </el-col>
              <el-col :span="4">
                <el-form-item
                  label="出生年月日："
                  :label-width="formLabelWidth2"
                >
                  <DatePickerE
                    v-model="editForm.RNBirthday"
                    value-format="yyyy-MM-dd"
                    format="yyyy-MM-dd"
                    type="date"
                    placeholder="選擇日期"
                    class="w140px"
                  />
                </el-form-item>
              </el-col>
              <el-col :span="4">
                <el-form-item
                  label="稱謂："
                  :label-width="formLabelWidth2"
                >
                  <el-input
                    v-model="editForm.RNFRelation"
                    placeholder="請輸入稱謂"
                    autocomplete="off"
                    clearable
                    class="w120px"
                  />
                </el-form-item>
              </el-col>
              <el-col :span="4">
                <el-form-item
                  label="連絡電話："
                  :label-width="formLabelWidth2"
                >
                  <el-input
                    v-model="editForm.RNCell"
                    placeholder="請輸入連絡電話"
                    autocomplete="off"
                    clearable
                    class="w140px"
                  />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="4">
                <el-badge
                  class="mark"
                  :value="1"
                />
                <el-form-item label="姓名：">
                  <el-input
                    v-model="editForm.RNFName_A"
                    placeholder="請輸入姓名"
                    autocomplete="off"
                    clearable
                    class="w160px"
                  />
                </el-form-item>
              </el-col>
              <el-col :span="4.5">
                <el-form-item
                  label="身份證字號："
                  :label-width="formLabelWidth2"
                  prop="RNFID_1_A"
                >
                  <el-input
                    v-model="editForm.RNFID_1_A"
                    v-upperCase
                    placeholder="請輸入身份證字號"
                    autocomplete="off"
                    clearable
                    class="w200px"
                    :maxlength="10"
                  />
                </el-form-item>
              </el-col>
              <el-col :span="4">
                <el-form-item
                  label="出生年月日："
                  :label-width="formLabelWidth2"
                >
                  <DatePickerE
                    v-model="editForm.RNFBirthday_A"
                    value-format="yyyy-MM-dd"
                    format="yyyy-MM-dd"
                    type="date"
                    placeholder="選擇日期"
                    class="w140px"
                  />
                </el-form-item>
              </el-col>
              <el-col :span="4">
                <el-form-item
                  label="稱謂："
                  :label-width="formLabelWidth2"
                >
                  <el-input
                    v-model="editForm.RNFRelation_A"
                    placeholder="請輸入稱謂"
                    autocomplete="off"
                    clearable
                    class="w120px"
                  />
                </el-form-item>
              </el-col>
              <el-col :span="4">
                <el-form-item
                  label="連絡電話："
                  :label-width="formLabelWidth2"
                  prop="RNFCell_A"
                >
                  <el-input
                    v-model="editForm.RNFCell_A"
                    placeholder="請輸入連絡電話"
                    autocomplete="off"
                    clearable
                    class="w140px"
                  />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="4">
                <el-badge
                  class="mark"
                  :value="2"
                />
                <el-form-item label="姓名：">
                  <el-input
                    v-model="editForm.RNFName_B"
                    placeholder="請輸入姓名"
                    autocomplete="off"
                    clearable
                    class="w160px"
                  />
                </el-form-item>
              </el-col>
              <el-col :span="4.5">
                <el-form-item
                  label="身份證字號："
                  :label-width="formLabelWidth2"
                  prop="RNFID_1_B"
                >
                  <el-input
                    v-model="editForm.RNFID_1_B"
                    v-upperCase
                    placeholder="請輸入身份證字號"
                    autocomplete="off"
                    clearable
                    class="w200px"
                    :maxlength="10"
                  />
                </el-form-item>
              </el-col>
              <el-col :span="4">
                <el-form-item
                  label="出生年月日："
                  :label-width="formLabelWidth2"
                >
                  <DatePickerE
                    v-model="editForm.RNFBirthday_B"
                    value-format="yyyy-MM-dd"
                    format="yyyy-MM-dd"
                    type="date"
                    placeholder="選擇日期"
                    class="w140px"
                  />
                </el-form-item>
              </el-col>
              <el-col :span="4">
                <el-form-item
                  label="稱謂："
                  :label-width="formLabelWidth2"
                >
                  <el-input
                    v-model="editForm.RNFRelation_B"
                    placeholder="請輸入稱謂"
                    autocomplete="off"
                    clearable
                    class="w120px"
                  />
                </el-form-item>
              </el-col>
              <el-col :span="4">
                <el-form-item
                  label="連絡電話："
                  :label-width="formLabelWidth2"
                  prop="RNFCell_B"
                >
                  <el-input
                    v-model="editForm.RNFCell_B"
                    placeholder="請輸入連絡電話"
                    autocomplete="off"
                    clearable
                    class="w140px"
                  />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="4">
                <el-badge
                  class="mark"
                  :value="3"
                />
                <el-form-item label="姓名：">
                  <el-input
                    v-model="editForm.RNFName_C"
                    placeholder="請輸入姓名"
                    autocomplete="off"
                    clearable
                    class="w160px"
                  />
                </el-form-item>
              </el-col>
              <el-col :span="4.5">
                <el-form-item
                  label="身份證字號："
                  :label-width="formLabelWidth2"
                  prop="RNFID_1_C"
                >
                  <el-input
                    v-model="editForm.RNFID_1_C"
                    v-upperCase
                    placeholder="請輸入身份證字號"
                    autocomplete="off"
                    clearable
                    class="w200px"
                    :maxlength="10"
                  />
                </el-form-item>
              </el-col>
              <el-col :span="4">
                <el-form-item
                  label="出生年月日："
                  :label-width="formLabelWidth2"
                >
                  <DatePickerE
                    v-model="editForm.RNFBirthday_C"
                    value-format="yyyy-MM-dd"
                    format="yyyy-MM-dd"
                    type="date"
                    placeholder="選擇日期"
                    class="w140px"
                  />
                </el-form-item>
              </el-col>
              <el-col :span="4">
                <el-form-item
                  label="稱謂："
                  :label-width="formLabelWidth2"
                >
                  <el-input
                    v-model="editForm.RNFRelation_C"
                    placeholder="請輸入稱謂"
                    autocomplete="off"
                    clearable
                    class="w120px"
                  />
                </el-form-item>
              </el-col>
              <el-col :span="4">
                <el-form-item
                  label="連絡電話："
                  :label-width="formLabelWidth2"
                  prop="RNFCell_C"
                >
                  <el-input
                    v-model="editForm.RNFCell_C"
                    placeholder="請輸入連絡電話"
                    autocomplete="off"
                    clearable
                    class="w140px"
                  />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="4">
                <el-badge
                  class="mark"
                  :value="4"
                />
                <el-form-item label="姓名：">
                  <el-input
                    v-model="editForm.RNFName_D"
                    placeholder="請輸入姓名"
                    autocomplete="off"
                    clearable
                    class="w160px"
                  />
                </el-form-item>
              </el-col>
              <el-col :span="4.5">
                <el-form-item
                  label="身份證字號："
                  :label-width="formLabelWidth2"
                  prop="RNFID_1_D"
                >
                  <el-input
                    v-model="editForm.RNFID_1_D"
                    v-upperCase
                    placeholder="請輸入身份證字號"
                    autocomplete="off"
                    clearable
                    class="w200px"
                    :maxlength="10"
                  />
                </el-form-item>
              </el-col>
              <el-col :span="4">
                <el-form-item
                  label="出生年月日："
                  :label-width="formLabelWidth2"
                >
                  <DatePickerE
                    v-model="editForm.RNFBirthday_D"
                    value-format="yyyy-MM-dd"
                    format="yyyy-MM-dd"
                    type="date"
                    placeholder="選擇日期"
                    class="w140px"
                  />
                </el-form-item>
              </el-col>
              <el-col :span="4">
                <el-form-item
                  label="稱謂："
                  :label-width="formLabelWidth2"
                >
                  <el-input
                    v-model="editForm.RNFRelation_D"
                    placeholder="請輸入稱謂"
                    autocomplete="off"
                    clearable
                    class="w120px"
                  />
                </el-form-item>
              </el-col>
              <el-col :span="4">
                <el-form-item
                  label="連絡電話："
                  :label-width="formLabelWidth2"
                  prop="RNFCell_D"
                >
                  <el-input
                    v-model="editForm.RNFCell_D"
                    placeholder="請輸入連絡電話"
                    autocomplete="off"
                    clearable
                    class="w140px"
                  />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="4">
                <el-badge
                  class="mark"
                  :value="5"
                />
                <el-form-item label="姓名：">
                  <el-input
                    v-model="editForm.RNFName_E"
                    placeholder="請輸入姓名"
                    autocomplete="off"
                    clearable
                    class="w160px"
                  />
                </el-form-item>
              </el-col>
              <el-col :span="4.5">
                <el-form-item
                  label="身份證字號："
                  :label-width="formLabelWidth2"
                  prop="RNFID_1_E"
                >
                  <el-input
                    v-model="editForm.RNFID_1_E"
                    v-upperCase
                    placeholder="請輸入身份證字號"
                    autocomplete="off"
                    clearable
                    class="w200px"
                    :maxlength="10"
                  />
                </el-form-item>
              </el-col>
              <el-col :span="4">
                <el-form-item
                  label="出生年月日："
                  :label-width="formLabelWidth2"
                >
                  <DatePickerE
                    v-model="editForm.RNFBirthday_E"
                    value-format="yyyy-MM-dd"
                    format="yyyy-MM-dd"
                    type="date"
                    placeholder="選擇日期"
                    class="w140px"
                  />
                </el-form-item>
              </el-col>
              <el-col :span="4">
                <el-form-item
                  label="稱謂："
                  :label-width="formLabelWidth2"
                >
                  <el-input
                    v-model="editForm.RNFRelation_E"
                    placeholder="請輸入稱謂"
                    autocomplete="off"
                    clearable
                    class="w120px"
                  />
                </el-form-item>
              </el-col>
              <el-col :span="4">
                <el-form-item
                  label="連絡電話："
                  :label-width="formLabelWidth2"
                  prop="RNFCell_E"
                >
                  <el-input
                    v-model="editForm.RNFCell_E"
                    placeholder="請輸入連絡電話"
                    autocomplete="off"
                    clearable
                    class="w140px"
                  />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="4">
                <el-badge
                  class="mark"
                  :value="6"
                />
                <el-form-item label="姓名：">
                  <el-input
                    v-model="editForm.RNFName_F"
                    placeholder="請輸入姓名"
                    autocomplete="off"
                    clearable
                    class="w160px"
                  />
                </el-form-item>
              </el-col>
              <el-col :span="4.5">
                <el-form-item
                  label="身份證字號："
                  :label-width="formLabelWidth2"
                  prop="RNFID_1_F"
                >
                  <el-input
                    v-model="editForm.RNFID_1_F"
                    v-upperCase
                    placeholder="請輸入身份證字號"
                    autocomplete="off"
                    clearable
                    class="w200px"
                    :maxlength="10"
                  />
                </el-form-item>
              </el-col>
              <el-col :span="4">
                <el-form-item
                  label="出生年月日："
                  :label-width="formLabelWidth2"
                >
                  <DatePickerE
                    v-model="editForm.RNFBirthday_F"
                    value-format="yyyy-MM-dd"
                    format="yyyy-MM-dd"
                    type="date"
                    placeholder="選擇日期"
                    class="w140px"
                  />
                </el-form-item>
              </el-col>
              <el-col :span="4">
                <el-form-item
                  label="稱謂："
                  :label-width="formLabelWidth2"
                >
                  <el-input
                    v-model="editForm.RNFRelation_F"
                    placeholder="請輸入稱謂"
                    autocomplete="off"
                    clearable
                    class="w120px"
                  />
                </el-form-item>
              </el-col>
              <el-col :span="4">
                <el-form-item
                  label="連絡電話："
                  :label-width="formLabelWidth2"
                  prop="RNFCell_F"
                >
                  <el-input
                    v-model="editForm.RNFCell_F"
                    placeholder="請輸入連絡電話"
                    autocomplete="off"
                    clearable
                    class="w140px"
                  />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="4">
                <el-badge
                  class="mark"
                  :value="7"
                />
                <el-form-item label="姓名：">
                  <el-input
                    v-model="editForm.RNFName_G"
                    placeholder="請輸入姓名"
                    autocomplete="off"
                    clearable
                    class="w160px"
                  />
                </el-form-item>
              </el-col>
              <el-col :span="4.5">
                <el-form-item
                  label="身份證字號："
                  :label-width="formLabelWidth2"
                  prop="RNFID_1_G"
                >
                  <el-input
                    v-model="editForm.RNFID_1_G"
                    v-upperCase
                    placeholder="請輸入身份證字號"
                    autocomplete="off"
                    clearable
                    class="w200px"
                    :maxlength="10"
                  />
                </el-form-item>
              </el-col>
              <el-col :span="4">
                <el-form-item
                  label="出生年月日："
                  :label-width="formLabelWidth2"
                >
                  <DatePickerE
                    v-model="editForm.RNFBirthday_G"
                    value-format="yyyy-MM-dd"
                    format="yyyy-MM-dd"
                    type="date"
                    placeholder="選擇日期"
                    class="w140px"
                  />
                </el-form-item>
              </el-col>
              <el-col :span="4">
                <el-form-item
                  label="稱謂："
                  :label-width="formLabelWidth2"
                >
                  <el-input
                    v-model="editForm.RNFRelation_G"
                    placeholder="請輸入稱謂"
                    autocomplete="off"
                    clearable
                    class="w120px"
                  />
                </el-form-item>
              </el-col>
              <el-col :span="4">
                <el-form-item
                  label="連絡電話："
                  :label-width="formLabelWidth2"
                  prop="RNFCell_G"
                >
                  <el-input
                    v-model="editForm.RNFCell_G"
                    placeholder="請輸入連絡電話"
                    autocomplete="off"
                    clearable
                    class="w140px"
                  />
                </el-form-item>
              </el-col>
            </el-row>
          </el-card>
        </el-col>
      </el-row>
      <el-row>
        <el-col>
          <h3>入住習慣</h3>
          <el-card>
            <div class="text item">
              <span>
                <el-checkbox
                  v-model="editForm.BZ041"
                  true-label="/Yes"
                  false-label="/Off"
                >目前家裡沒有寵物;</el-checkbox>
                目前家裡有寵物：
                <el-checkbox
                  v-model="editForm.BZ042"
                  true-label="/Yes"
                  false-label="/Off"
                >狗
                  <el-input
                    v-model="editForm.BZ025"
                    autocomplete="off"
                    clearable
                    size="mini"
                    class="w80px"
                  />隻
                </el-checkbox>
                <el-checkbox
                  v-model="editForm.BZ026"
                  true-label="/Yes"
                  false-label="/Off"
                >貓
                  <el-input
                    v-model="editForm.BZ027"
                    autocomplete="off"
                    clearable
                    size="mini"
                    class="w80px"
                  />隻
                </el-checkbox>
                <el-checkbox
                  v-model="editForm.BZ028"
                  true-label="/Yes"
                  false-label="/Off"
                >其他
                  <el-input
                    v-model="editForm.BZ029"
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
                  v-model="editForm.BZ030"
                  true-label="/Yes"
                  false-label="/Off"
                >9:00~12:00</el-checkbox>
                <el-checkbox
                  v-model="editForm.BZ031"
                  true-label="/Yes"
                  false-label="/Off"
                >13:00~16:00</el-checkbox>
                <el-checkbox
                  v-model="editForm.BZ032"
                  true-label="/Yes"
                  false-label="/Off"
                >17:00~20:00</el-checkbox>
              </span>
            </div>
          </el-card>
        </el-col>
      </el-row>
      <el-row>
        <el-col>
          <h3>個人資料蒐集、處理及利用告知聲明暨同意書</h3>
          <el-card>
            <div class="text item">
              <span>雙方秉持誠信原則同意簽定本契約，授權乙方辦理委託仲介租賃：</span>
            </div>
            <br>
            <div class="text item">
              <span>一、
                本人同意配合貴公司蒐集、處理及利用立同意書人之個人資料，並同意貴公司員工以
                <el-checkbox
                  v-model="editForm.BZ034"
                  true-label="/Yes"
                  false-label="/Off"
                >電話</el-checkbox>
                <el-checkbox
                  v-model="editForm.BZ033"
                  true-label="/Yes"
                  false-label="/Off"
                >簡訊</el-checkbox>
                <el-checkbox
                  v-model="editForm.BZ037"
                  true-label="/Yes"
                  false-label="/Off"
                >LINE</el-checkbox>
                <el-checkbox
                  v-model="editForm.BZ035"
                  true-label="/Yes"
                  false-label="/Off"
                >親訪</el-checkbox>
                之方式聯繫。並保證以上資料均為屬實，如有虛偽並願自負法律責任。
              </span>
            </div>
            <div class="text item">
              <span>二、 本公司利用個人資料之期間、地區、對象及方式：<br>
                <!-- eslint-disable no-irregular-whitespace -->
                　　(一)
                期間：自本同意書簽立日起，至立同意書人要求停止使用之日止。<br>
                　　(二) 地區：於本國領域內。<br>
                　　(三)
                對象：本公司及與本公司直接或間接關係企業用於租賃關係管理、辨識身分、行銷廣宣或入居者審查等，或主管機關依法要求提供者。<br>
                <b>　　　　(如未通過審查，致雙方無法簽訂租賃契約，僅需退還定金予本人，無民法第249條第3款適用。)</b><br>
                　　(四) 方式：以書面或電子文件方式儲存、處理及利用。</span>
            </div>
            <span
              class="text item"
            >三、
              立同意書人就其個人資料得依個人資料保護法及其他相關法令向本公司查詢或請求閱覽、請求製給複製本、請求停止蒐集、處理或利用及請求刪除其個人資料，如個人資料有錯誤、缺漏者，立同意書人
              得請求補充、更正。<br>
              　　本公司處理立同意書人查詢或請求閱覽及請求製給複製本時，得向立同意書人酌收必要成本費用。
            </span>
            <div class="text item">
              <span>四、
                立同意書人於簽訂本條款時，確已詳閱全文，並基於自由意思下簽署。</span>
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
        </el-col>
      </el-row>
    </el-form>
    <div style="margin-top: 20px; text-align: center">
      <el-button
        type="primary"
        size="small"
        icon="el-icon-paperclip"
        @click="upload()"
      >儲存</el-button>
      <el-button
        size="small" icon="el-icon-close" @click="cancel"
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
          width="800px" height="500px"
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
          type="info" @click="centerDialogVisible = false"
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
  validateCell,
  validateCellReg,
  validateEamilReg,
  validateIDReg
} from '@/utils/validate';
import Address from '@/components/Address/Address.vue';
import DatePickerE from '@/components/RocDatepickerE';
import JsPDF from 'jspdf';
import html2canvas from 'html2canvas';
import { mergerAddress } from '@/utils/index';
export default {
  name: 'A000002',
  components: { DatePickerE, Address },
  props: {
    editform: { type: Object, default: null }
  },
  data() {
    return {
      sendedform: {},
      sendedform1: {},
      sendedformRNEC: {},
      sendedformRNECC: {},
      sendedformRNAG: {},
      sendedformRNAGC: {},
      sendedformRNG: {},
      sendedformRNGC: {},
      sendedformRNEC2: {},
      sendedformRNECC2: {},
      RNAddSameCom: false,
      RNECAddSameCom: false,
      RNAGAddSameCom: false,
      RNGAddSameCom: false,
      RNEC2AddSameCom: false,
      centerDialogVisible: false,
      formLabelWidth: '110px',
      formLabelWidth2: '100px',
      editForm: { ...this.editform },
      rules: {
        RNID: [
          { required: true, trigger: 'blur', validator: validateIDReg },
          { min: 10, max: 10, message: '請輸入10位', trigger: 'change' }
        ],
        RNName: [
          {
            required: true,
            trigger: 'blur',
            message: '請輸入姓名'
          }
        ],
        RNBirthday: [
          { required: true, message: '請輸入生日', trigger: 'blur' }
        ],
        RNCell: [
          { required: true, trigger: 'change', validator: validateCell }
        ],
        RNMail: [
          { required: false, trigger: 'change', validator: validateEamilReg }
        ],
        RNECID: [
          { required: false, trigger: 'blur', validator: validateIDReg }
        ],
        BZ049: [
          { required: false, trigger: 'change', validator: validateCellReg }
        ],
        RNECCell: [
          { required: false, trigger: 'change', validator: validateCellReg }
        ],
        BZ048: [
          {
            validator: validateIDReg,
            trigger: 'change',
            required: false
          }
        ],
        RNAGID_A: [
          { required: false, trigger: 'blur', validator: validateIDReg }
        ],
        RNAGCell_A: [
          { required: false, trigger: 'change', validator: validateCellReg }
        ],
        RNGID: [{ required: false, trigger: 'blur', validator: validateIDReg }],
        RNGCell: [
          { required: false, trigger: 'change', validator: validateCellReg }
        ],
        RNFID_1_A: [
          { required: false, trigger: 'blur', validator: validateIDReg }
        ],
        RNFID_1_B: [
          { required: false, trigger: 'blur', validator: validateIDReg }
        ],
        RNFID_1_C: [
          { required: false, trigger: 'blur', validator: validateIDReg }
        ],
        RNFID_1_D: [
          { required: false, trigger: 'blur', validator: validateIDReg }
        ],
        RNFID_1_E: [
          { required: false, trigger: 'blur', validator: validateIDReg }
        ],
        RNFID_1_F: [
          { required: false, trigger: 'blur', validator: validateIDReg }
        ],
        RNFID_1_G: [
          { required: false, trigger: 'blur', validator: validateIDReg }
        ],
        RNFCell_A: [
          { required: false, trigger: 'change', validator: validateCellReg }
        ],
        RNFCell_B: [
          { required: false, trigger: 'change', validator: validateCellReg }
        ],
        RNFCell_C: [
          { required: false, trigger: 'change', validator: validateCellReg }
        ],
        RNFCell_D: [
          { required: false, trigger: 'change', validator: validateCellReg }
        ],
        RNFCell_E: [
          { required: false, trigger: 'change', validator: validateCellReg }
        ],
        RNFCell_F: [
          { required: false, trigger: 'change', validator: validateCellReg }
        ],
        RNFCell_G: [
          { required: false, trigger: 'change', validator: validateCellReg }
        ]
        // 20230215對話紀錄
        // RNECName: [
        //   { required: true, trigger: 'change', message: '請輸入姓名' }
        // ],
        // RNECBirthday: [
        //   { required: true, trigger: 'change', message: '請輸入生日' }
        // ],
        // RNECRelation: [
        //   { required: true, trigger: 'change', message: '請輸入關係' }
        // ],
        // 20230215對話紀錄
        // BZ043: [{ required: true, trigger: 'change', message: '請輸入姓名' }],
        // BZ046: [{ required: true, trigger: 'change', message: '請輸入生日' }],
        // BZ047: [{ required: true, trigger: 'change', message: '請輸入關係' }]
      },
      RNgender: '',
      BZ018BZ019: '',
      BZ012BZ013: '',
      BZ014BZ015: '',
      BZ020BZ021: '',
      BZ044045: '',
      BZ001003006: '',
      src_1: '',
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
      ]
    };
  },
  watch: {
    editform: {
      immediate: true,
      handler() {
        console.log('editformch');
        if (Object.keys(this.editform).length !== 0) {
          this.editForm = this.editform;
          this.editForm.RNFRelation = '本人';
          this.$emit('openpageloading');
          this.sendedform = {
            Add_1: this.editForm.RNAdd_1,
            Add_1_1: this.editForm.RNAdd_1_1,
            Add_1_2: this.editForm.RNAdd_1_2,
            Add_2: this.editForm.RNAdd_2,
            Add_2_1: this.editForm.RNAdd_2_1,
            Add_2_2: this.editForm.RNAdd_2_2,
            Add_2_3: this.editForm.RNAdd_2_3,
            Add_2_4: this.editForm.RNAdd_2_4,
            Add_3: this.editForm.RNAdd_3,
            Add_3_1: this.editForm.RNAdd_3_1,
            Add_3_2: this.editForm.RNAdd_3_2,
            Add_4: this.editForm.RNAdd_4,
            Add_5: this.editForm.RNAdd_5,
            Add_6: this.editForm.RNAdd_6,
            Add_7: this.editForm.RNAdd_7,
            Add_8: this.editForm.RNAdd_8,
            Add_9: this.editForm.RNAdd_9
          };
          this.sendedform1 = {
            Add_1: this.editForm.RNAddC_1,
            Add_1_1: this.editForm.RNAddC_1_1,
            Add_1_2: this.editForm.RNAddC_1_2,
            Add_2: this.editForm.RNAddC_2,
            Add_2_1: this.editForm.RNAddC_2_1,
            Add_2_2: this.editForm.RNAddC_2_2,
            Add_2_3: this.editForm.RNAddC_2_3,
            Add_2_4: this.editForm.RNAddC_2_4,
            Add_3: this.editForm.RNAddC_3,
            Add_3_1: this.editForm.RNAddC_3_1,
            Add_3_2: this.editForm.RNAddC_3_2,
            Add_4: this.editForm.RNAddC_4,
            Add_5: this.editForm.RNAddC_5,
            Add_6: this.editForm.RNAddC_6,
            Add_7: this.editForm.RNAddC_7,
            Add_8: this.editForm.RNAddC_8,
            Add_9: this.editForm.RNAddC_9
          };
          this.sendedformRNECC = {
            Add_1: this.editForm.RNECAddC_1,
            Add_1_1: this.editForm.RNECAddC_1_1,
            Add_1_2: this.editForm.RNECAddC_1_2,
            Add_2: this.editForm.RNECAddC_2,
            Add_2_1: this.editForm.RNECAddC_2_1,
            Add_2_2: this.editForm.RNECAddC_2_2,
            Add_2_3: this.editForm.RNECAddC_2_3,
            Add_2_4: this.editForm.RNECAddC_2_4,
            Add_3: this.editForm.RNECAddC_3,
            Add_3_1: this.editForm.RNECAddC_3_1,
            Add_3_2: this.editForm.RNECAddC_3_2,
            Add_4: this.editForm.RNECAddC_4,
            Add_5: this.editForm.RNECAddC_5,
            Add_6: this.editForm.RNECAddC_6,
            Add_7: this.editForm.RNECAddC_7,
            Add_8: this.editForm.RNECAddC_8,
            Add_9: this.editForm.RNECAddC_9
          };
          this.sendedformRNEC = {
            Add_1: this.editForm.RNECAdd_1,
            Add_1_1: this.editForm.RNECAdd_1_1,
            Add_1_2: this.editForm.RNECAdd_1_2,
            Add_2: this.editForm.RNECAdd_2,
            Add_2_1: this.editForm.RNECAdd_2_1,
            Add_2_2: this.editForm.RNECAdd_2_2,
            Add_2_3: this.editForm.RNECAdd_2_3,
            Add_2_4: this.editForm.RNECAdd_2_4,
            Add_3: this.editForm.RNECAdd_3,
            Add_3_1: this.editForm.RNECAdd_3_1,
            Add_3_2: this.editForm.RNECAdd_3_2,
            Add_4: this.editForm.RNECAdd_4,
            Add_5: this.editForm.RNECAdd_5,
            Add_6: this.editForm.RNECAdd_6,
            Add_7: this.editForm.RNECAdd_7,
            Add_8: this.editForm.RNECAdd_8,
            Add_9: this.editForm.RNECAdd_9
          };
          this.sendedformRNAG = {
            Add_1: this.editForm.RNAGAdd_1_A,
            Add_1_1: this.editForm.RNAGAdd_1_1_A,
            Add_1_2: this.editForm.RNAGAdd_1_2_A,
            Add_2: this.editForm.RNAGAdd_2_A,
            Add_2_1: this.editForm.RNAGAdd_2_1_A,
            Add_2_2: this.editForm.RNAGAdd_2_2_A,
            Add_2_3: this.editForm.RNAGAdd_2_3_A,
            Add_2_4: this.editForm.RNAGAdd_2_4_A,
            Add_3: this.editForm.RNAGAdd_3_A,
            Add_3_1: this.editForm.RNAGAdd_3_1_A,
            Add_3_2: this.editForm.RNAGAdd_3_2_A,
            Add_4: this.editForm.RNAGAdd_4_A,
            Add_5: this.editForm.RNAGAdd_5_A,
            Add_6: this.editForm.RNAGAdd_6_A,
            Add_7: this.editForm.RNAGAdd_7_A,
            Add_8: this.editForm.RNAGAdd_8_A,
            Add_9: this.editForm.RNAGAdd_9_A
          };
          this.sendedformRNAGC = {
            Add_1: this.editForm.RNAGAddC_1_A,
            Add_1_1: this.editForm.RNAGAddC_1_1_A,
            Add_1_2: this.editForm.RNAGAddC_1_2_A,
            Add_2: this.editForm.RNAGAddC_2_A,
            Add_2_1: this.editForm.RNAGAddC_2_1_A,
            Add_2_2: this.editForm.RNAGAddC_2_2_A,
            Add_2_3: this.editForm.RNAGAddC_2_3_A,
            Add_2_4: this.editForm.RNAGAddC_2_4_A,
            Add_3: this.editForm.RNAGAddC_3_A,
            Add_3_1: this.editForm.RNAGAddC_3_1_A,
            Add_3_2: this.editForm.RNAGAddC_3_2_A,
            Add_4: this.editForm.RNAGAddC_4_A,
            Add_5: this.editForm.RNAGAddC_5_A,
            Add_6: this.editForm.RNAGAddC_6_A,
            Add_7: this.editForm.RNAGAddC_7_A,
            Add_8: this.editForm.RNAGAddC_8_A,
            Add_9: this.editForm.RNAGAddC_9_A
          };
          this.sendedformRNG = {
            Add_1: this.editForm.RNGAdd_1,
            Add_1_1: this.editForm.RNGAdd_1_1,
            Add_1_2: this.editForm.RNGAdd_1_2,
            Add_2: this.editForm.RNGAdd_2,
            Add_2_1: this.editForm.RNGAdd_2_1,
            Add_2_2: this.editForm.RNGAdd_2_2,
            Add_2_3: this.editForm.RNGAdd_2_3,
            Add_2_4: this.editForm.RNGAdd_2_4,
            Add_3: this.editForm.RNGAdd_3,
            Add_3_1: this.editForm.RNGAdd_3_1,
            Add_3_2: this.editForm.RNGAdd_3_2,
            Add_4: this.editForm.RNGAdd_4,
            Add_5: this.editForm.RNGAdd_5,
            Add_6: this.editForm.RNGAdd_6,
            Add_7: this.editForm.RNGAdd_7,
            Add_8: this.editForm.RNGAdd_8,
            Add_9: this.editForm.RNGAdd_9
          };
          this.sendedformRNGC = {
            Add_1: this.editForm.RNGCAdd_1,
            Add_1_1: this.editForm.RNGCAdd_1_1,
            Add_1_2: this.editForm.RNGCAdd_1_2,
            Add_2: this.editForm.RNGCAdd_2,
            Add_2_1: this.editForm.RNGCAdd_2_1,
            Add_2_2: this.editForm.RNGCAdd_2_2,
            Add_2_3: this.editForm.RNGCAdd_2_3,
            Add_2_4: this.editForm.RNGCAdd_2_4,
            Add_3: this.editForm.RNGCAdd_3,
            Add_3_1: this.editForm.RNGCAdd_3_1,
            Add_3_2: this.editForm.RNGCAdd_3_2,
            Add_4: this.editForm.RNGCAdd_4,
            Add_5: this.editForm.RNGCAdd_5,
            Add_6: this.editForm.RNGCAdd_6,
            Add_7: this.editForm.RNGCAdd_7,
            Add_8: this.editForm.RNGCAdd_8,
            Add_9: this.editForm.RNGCAdd_9
          };
          this.sendedformRNEC2 = {
            Add_1: this.editForm.BZ050,
            Add_1_1: this.editForm.BZ051,
            Add_1_2: this.editForm.BZ052,
            Add_2: this.editForm.BZ053,
            Add_2_1: this.editForm.BZ054,
            Add_2_2: this.editForm.BZ055,
            Add_2_3: this.editForm.BZ056,
            Add_2_4: this.editForm.BZ057,
            Add_3: this.editForm.BZ058,
            Add_3_1: this.editForm.BZ059,
            Add_3_2: this.editForm.BZ060,
            Add_4: this.editForm.BZ061,
            Add_5: this.editForm.BZ062,
            Add_6: this.editForm.BZ063,
            Add_7: this.editForm.BZ064,
            Add_8: this.editForm.BZ065,
            Add_9: this.editForm.BZ066
          };
          this.sendedformRNECC2 = {
            Add_1: this.editForm.BZ068,
            Add_1_1: this.editForm.BZ069,
            Add_1_2: this.editForm.BZ070,
            Add_2: this.editForm.BZ071,
            Add_2_1: this.editForm.BZ072,
            Add_2_2: this.editForm.BZ073,
            Add_2_3: this.editForm.BZ074,
            Add_2_4: this.editForm.BZ075,
            Add_3: this.editForm.BZ076,
            Add_3_1: this.editForm.BZ077,
            Add_3_2: this.editForm.BZ078,
            Add_4: this.editForm.BZ079,
            Add_5: this.editForm.BZ080,
            Add_6: this.editForm.BZ081,
            Add_7: this.editForm.BZ082,
            Add_8: this.editForm.BZ083,
            Add_9: this.editForm.BZ084
          };
          // this.editForm = this.editform;
          this.RNgender =
            this.editForm.RNGender1 === '/Yes'
              ? '1'
              : this.editForm.RNGender2 === '/Yes'
                ? '2'
                : '1';
          this.BZ018BZ019 =
            this.editForm.BZ018 === '/Yes'
              ? '1'
              : this.editForm.BZ019 === '/Yes'
                ? '2'
                : '1';
          this.BZ012BZ013 =
            this.editForm.BZ012 === '/Yes'
              ? '1'
              : this.editForm.BZ013 === '/Yes'
                ? '2'
                : '1';
          this.BZ014BZ015 =
            this.editForm.BZ014 === '/Yes'
              ? '1'
              : this.editForm.BZ015 === '/Yes'
                ? '2'
                : '1';
          this.BZ020BZ021 =
            this.editForm.BZ020 === '/Yes'
              ? '1'
              : this.editForm.BZ021 === '/Yes'
                ? '2'
                : '1';
          this.BZ044045 =
            this.editForm.BZ044 === '/Yes'
              ? '1'
              : this.editForm.BZ045 === '/Yes'
                ? '2'
                : '1';
          this.BZ001003006 =
            this.editForm.BZ001 === '/Yes'
              ? '1'
              : this.editForm.BZ003 === '/Yes'
                ? '2'
                : this.editForm.BZ006 === '/Yes'
                  ? '3'
                  : '1';
          this.RNAddSameCom = this.editForm.RNAddSame === '/Yes';
          this.RNECAddSameCom = this.editForm.RNECAddSame === '/Yes';
          this.RNAGAddSameCom = this.editForm.RNAGAddSame === '/Yes';
          this.RNGAddSameCom = this.editForm.RNGAddSame === '/Yes';
          this.RNEC2AddSameCom = this.editForm.BZ067 === '/Yes';
          if (!this.editForm.BZ034) {
            this.editForm.BZ034 = '/Yes';
          }
          if (!this.editForm.BZ033) {
            this.editForm.BZ033 = '/Yes';
          }
          if (!this.editForm.BZ035) {
            this.editForm.BZ035 = '/Yes';
          }
          if (!this.editForm.BZ037) {
            this.editForm.BZ037 = '/Yes';
          }
          this.GetSig();
        }
      }
    },
    RNgender: {
      immediate: true,
      handler() {
        console.log('rngender')
        switch (this.RNgender) {
          case '1':
            this.editForm.RNGender1 = '/Yes';
            this.editForm.RNGender2 = '/Off';
            break;
          case '2':
            this.editForm.RNGender1 = '/Off';
            this.editForm.RNGender2 = '/Yes';
            break;
        }
      }
    },
    BZ018BZ019: {
      immediate: true,
      handler() {
        switch (this.BZ018BZ019) {
          case '1':
            this.editForm.BZ018 = '/Yes';
            this.editForm.BZ019 = '/Off';
            break;
          case '2':
            this.editForm.BZ018 = '/Off';
            this.editForm.BZ019 = '/Yes';
            break;
        }
      }
    },
    BZ012BZ013: {
      immediate: true,
      handler() {
        switch (this.BZ012BZ013) {
          case '1':
            this.editForm.BZ012 = '/Yes';
            this.editForm.BZ013 = '/Off';
            break;
          case '2':
            this.editForm.BZ012 = '/Off';
            this.editForm.BZ013 = '/Yes';
            break;
        }
      }
    },
    BZ014BZ015: {
      immediate: true,
      handler() {
        switch (this.BZ014BZ015) {
          case '1':
            this.editForm.BZ014 = '/Yes';
            this.editForm.BZ015 = '/Off';
            break;
          case '2':
            this.editForm.BZ014 = '/Off';
            this.editForm.BZ015 = '/Yes';
            break;
        }
      }
    },
    BZ020BZ021: {
      immediate: true,
      handler() {
        switch (this.BZ020BZ021) {
          case '1':
            this.editForm.BZ020 = '/Yes';
            this.editForm.BZ021 = '/Off';
            break;
          case '2':
            this.editForm.BZ020 = '/Off';
            this.editForm.BZ021 = '/Yes';
            break;
        }
      }
    },
    BZ044045: {
      immediate: true,
      handler() {
        switch (this.BZ044045) {
          case '1':
            this.editForm.BZ044 = '/Yes';
            this.editForm.BZ045 = '/Off';
            break;
          case '2':
            this.editForm.BZ044 = '/Off';
            this.editForm.BZ045 = '/Yes';
            break;
        }
      }
    },
    BZ001003006: {
      immediate: true,
      handler() {
        switch (this.BZ001003006) {
          case '1':
            this.editForm.BZ001 = '/Yes';
            this.editForm.BZ003 = '/Off';
            this.editForm.BZ006 = '/Off';
            break;
          case '2':
            this.editForm.BZ001 = '/Off';
            this.editForm.BZ003 = '/Yes';
            this.editForm.BZ006 = '/Off';
            break;
          case '3':
            this.editForm.BZ001 = '/Off';
            this.editForm.BZ003 = '/Off';
            this.editForm.BZ006 = '/Yes';
            break;
        }
      }
    },
    RNAddSameCom: {
      handler(a) {
        if (a) {
          this.editForm.RNAddSame = '/Yes';
        } else {
          this.editForm.RNAddSame = '/Off';
        }
      }
    },
    RNECAddSameCom: {
      handler(a) {
        if (a) {
          this.editForm.RNECAddSame = '/Yes';
        } else {
          this.editForm.RNECAddSame = '/Off';
        }
      }
    },
    RNAGAddSameCom: {
      handler(a) {
        if (a) {
          this.editForm.RNAGAddSame = '/Yes';
        } else {
          this.editForm.RNAGAddSame = '/Off';
        }
      }
    },
    RNGAddSameCom: {
      handler(a) {
        if (a) {
          this.editForm.RNGAddSame = '/Yes';
        } else {
          this.editForm.RNGAddSame = '/Off';
        }
      }
    },
    RNEC2AddSameCom: {
      handler(a) {
        if (a) {
          this.editForm.BZ067 = '/Yes';
        } else {
          this.editForm.BZ067 = '/Off';
        }
      }
    },
    'editForm.RNGName': {
      handler(a) {
        if (a) {
          this.rules.RNGID[0].required = true;
        } else {
          this.rules.RNGID[0].required = false;
        }
      }
    }
  },
  methods: {
    cancel() {
      this.$emit('cancel');
    },
    upload() {
      const addarray = [
        this.editForm.RNAdd_1,
        this.editForm.RNAdd_1_1,
        this.editForm.RNAdd_1_2,
        this.editForm.RNAdd_2,
        this.editForm.RNAdd_2_1,
        this.editForm.RNAdd_2_2,
        this.editForm.RNAdd_2_3,
        this.editForm.RNAdd_2_4,
        this.editForm.RNAdd_3,
        this.editForm.RNAdd_3_1,
        this.editForm.RNAdd_3_2,
        this.editForm.RNAdd_4,
        this.editForm.RNAdd_5,
        this.editForm.RNAdd_6,
        this.editForm.RNAdd_7,
        this.editForm.RNAdd_8,
        this.editForm.RNAdd_9
      ];
      var badd = mergerAddress(addarray);
      if (badd.length < 3) {
        this.$message.error('請輸入自然人房客戶籍地址');
        return;
      }
      // 20230215對話紀錄
      // if (
      //   !this.editForm.RNECAdd_1 ||
      //   !this.editForm.RNECAdd_2 ||
      //   !this.editForm.RNECAdd_3
      // ) {
      //   this.$message.error('請輸入緊急聯絡人1戶籍地址');
      //   return;
      // }
      // if (
      //   !this.editForm.BZ050 ||
      //   !this.editForm.BZ053 ||
      //   !this.editForm.BZ056
      // ) {
      //   this.$message.error('請輸入緊急聯絡人2戶籍地址');
      //   return;
      // }
      this.$refs['editForm'].validate((valid, err) => {
        if (valid) {
          this.UploadWithData();
        } else {
          var msg = '';
          var errArr = Object.keys(err);
          errArr.forEach((x) => {
            switch (x) {
              case 'RNName':
                msg += '*姓名<br/>';
                break;
              case 'RNID':
                msg += '*身分證字號<br/>';
                break;
              case 'RNBirthday':
                msg += '*出生年月日<br/>';
                break;
              case 'RNCell':
                msg += '*手機<br/>';
                break;
            }
          });
          this.$alert(
            `必填欄位未填：<br/><div style="color:#e24e4e"><i><b>${msg}<b/><i/><div/>`,
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
        .then((res) => {
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
    GetSig() {
      if (this.editForm.SignatureImgPath_1) {
        getImg(this.editForm.SignatureImgPath_1)
          .then((response) => {
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
    geteditaddress(addressData, title) {
      if (title === '戶籍地址') {
        this.editForm.RNAdd_1 = addressData.Add_1;
        this.editForm.RNAdd_1_1 = addressData.Add_1_1;
        this.editForm.RNAdd_1_2 = addressData.Add_1_2;
        this.editForm.RNAdd_2 = addressData.Add_2;
        this.editForm.RNAdd_2_1 = addressData.Add_2_1;
        this.editForm.RNAdd_2_2 = addressData.Add_2_2;
        this.editForm.RNAdd_2_3 = addressData.Add_2_3;
        this.editForm.RNAdd_2_4 = addressData.Add_2_4;
        this.editForm.RNAdd_3 = addressData.Add_3;
        this.editForm.RNAdd_3_1 = addressData.Add_3_1;
        this.editForm.RNAdd_3_2 = addressData.Add_3_2;
        this.editForm.RNAdd_4 = addressData.Add_4;
        this.editForm.RNAdd_5 = addressData.Add_5;
        this.editForm.RNAdd_6 = addressData.Add_6;
        this.editForm.RNAdd_7 = addressData.Add_7;
        this.editForm.RNAdd_8 = addressData.Add_8;
        this.editForm.RNAdd_9 = addressData.Add_9;
      }
      if (title === '通訊地址') {
        this.editForm.RNAddC_1 = addressData.Add_1;
        this.editForm.RNAddC_1_1 = addressData.Add_1_1;
        this.editForm.RNAddC_1_2 = addressData.Add_1_2;
        this.editForm.RNAddC_2 = addressData.Add_2;
        this.editForm.RNAddC_2_1 = addressData.Add_2_1;
        this.editForm.RNAddC_2_2 = addressData.Add_2_2;
        this.editForm.RNAddC_2_3 = addressData.Add_2_3;
        this.editForm.RNAddC_2_4 = addressData.Add_2_4;
        this.editForm.RNAddC_3 = addressData.Add_3;
        this.editForm.RNAddC_3_1 = addressData.Add_3_1;
        this.editForm.RNAddC_3_2 = addressData.Add_3_2;
        this.editForm.RNAddC_4 = addressData.Add_4;
        this.editForm.RNAddC_5 = addressData.Add_5;
        this.editForm.RNAddC_6 = addressData.Add_6;
        this.editForm.RNAddC_7 = addressData.Add_7;
        this.editForm.RNAddC_8 = addressData.Add_8;
        this.editForm.RNAddC_9 = addressData.Add_9;
      }
      if (title === '緊急聯絡人戶籍地址') {
        this.editForm.RNECAdd_1 = addressData.Add_1;
        this.editForm.RNECAdd_1_1 = addressData.Add_1_1;
        this.editForm.RNECAdd_1_2 = addressData.Add_1_2;
        this.editForm.RNECAdd_2 = addressData.Add_2;
        this.editForm.RNECAdd_2_1 = addressData.Add_2_1;
        this.editForm.RNECAdd_2_2 = addressData.Add_2_2;
        this.editForm.RNECAdd_2_3 = addressData.Add_2_3;
        this.editForm.RNECAdd_2_4 = addressData.Add_2_4;
        this.editForm.RNECAdd_3 = addressData.Add_3;
        this.editForm.RNECAdd_3_1 = addressData.Add_3_1;
        this.editForm.RNECAdd_3_2 = addressData.Add_3_2;
        this.editForm.RNECAdd_4 = addressData.Add_4;
        this.editForm.RNECAdd_5 = addressData.Add_5;
        this.editForm.RNECAdd_6 = addressData.Add_6;
        this.editForm.RNECAdd_7 = addressData.Add_7;
        this.editForm.RNECAdd_8 = addressData.Add_8;
        this.editForm.RNECAdd_9 = addressData.Add_9;
      }
      if (title === '緊急聯絡人通訊地址') {
        this.editForm.RNECAddC_1 = addressData.Add_1;
        this.editForm.RNECAddC_1_1 = addressData.Add_1_1;
        this.editForm.RNECAddC_1_2 = addressData.Add_1_2;
        this.editForm.RNECAddC_2 = addressData.Add_2;
        this.editForm.RNECAddC_2_1 = addressData.Add_2_1;
        this.editForm.RNECAddC_2_2 = addressData.Add_2_2;
        this.editForm.RNECAddC_2_3 = addressData.Add_2_3;
        this.editForm.RNECAddC_2_4 = addressData.Add_2_4;
        this.editForm.RNECAddC_3 = addressData.Add_3;
        this.editForm.RNECAddC_3_1 = addressData.Add_3_1;
        this.editForm.RNECAddC_3_2 = addressData.Add_3_2;
        this.editForm.RNECAddC_4 = addressData.Add_4;
        this.editForm.RNECAddC_5 = addressData.Add_5;
        this.editForm.RNECAddC_6 = addressData.Add_6;
        this.editForm.RNECAddC_7 = addressData.Add_7;
        this.editForm.RNECAddC_8 = addressData.Add_8;
        this.editForm.RNECAddC_9 = addressData.Add_9;
      }
      if (title === '代理人戶籍地址') {
        this.editForm.RNAGAdd_1_A = addressData.Add_1;
        this.editForm.RNAGAdd_1_1_A = addressData.Add_1_1;
        this.editForm.RNAGAdd_1_2_A = addressData.Add_1_2;
        this.editForm.RNAGAdd_2_A = addressData.Add_2;
        this.editForm.RNAGAdd_2_1_A = addressData.Add_2_1;
        this.editForm.RNAGAdd_2_2_A = addressData.Add_2_2;
        this.editForm.RNAGAdd_2_3_A = addressData.Add_2_3;
        this.editForm.RNAGAdd_2_4_A = addressData.Add_2_4;
        this.editForm.RNAGAdd_3_A = addressData.Add_3;
        this.editForm.RNAGAdd_3_1_A = addressData.Add_3_1;
        this.editForm.RNAGAdd_3_2_A = addressData.Add_3_2;
        this.editForm.RNAGAdd_4_A = addressData.Add_4;
        this.editForm.RNAGAdd_5_A = addressData.Add_5;
        this.editForm.RNAGAdd_6_A = addressData.Add_6;
        this.editForm.RNAGAdd_7_A = addressData.Add_7;
        this.editForm.RNAGAdd_8_A = addressData.Add_8;
        this.editForm.RNAGAdd_9_A = addressData.Add_9;
      }
      if (title === '代理人通訊地址') {
        this.editForm.RNAGAddC_1_A = addressData.Add_1;
        this.editForm.RNAGAddC_1_1_A = addressData.Add_1_1;
        this.editForm.RNAGAddC_1_2_A = addressData.Add_1_2;
        this.editForm.RNAGAddC_2_A = addressData.Add_2;
        this.editForm.RNAGAddC_2_1_A = addressData.Add_2_1;
        this.editForm.RNAGAddC_2_2_A = addressData.Add_2_2;
        this.editForm.RNAGAddC_2_3_A = addressData.Add_2_3;
        this.editForm.RNAGAddC_2_4_A = addressData.Add_2_4;
        this.editForm.RNAGAddC_3_A = addressData.Add_3;
        this.editForm.RNAGAddC_3_1_A = addressData.Add_3_1;
        this.editForm.RNAGAddC_3_2_A = addressData.Add_3_2;
        this.editForm.RNAGAddC_4_A = addressData.Add_4;
        this.editForm.RNAGAddC_5_A = addressData.Add_5;
        this.editForm.RNAGAddC_6_A = addressData.Add_6;
        this.editForm.RNAGAddC_7_A = addressData.Add_7;
        this.editForm.RNAGAddC_8_A = addressData.Add_8;
        this.editForm.RNAGAddC_9_A = addressData.Add_9;
      }
      if (title === '保證人戶籍地址') {
        this.editForm.RNGAdd_1 = addressData.Add_1;
        this.editForm.RNGAdd_1_1 = addressData.Add_1_1;
        this.editForm.RNGAdd_1_2 = addressData.Add_1_2;
        this.editForm.RNGAdd_2 = addressData.Add_2;
        this.editForm.RNGAdd_2_1 = addressData.Add_2_1;
        this.editForm.RNGAdd_2_2 = addressData.Add_2_2;
        this.editForm.RNGAdd_2_3 = addressData.Add_2_3;
        this.editForm.RNGAdd_2_4 = addressData.Add_2_4;
        this.editForm.RNGAdd_3 = addressData.Add_3;
        this.editForm.RNGAdd_3_1 = addressData.Add_3_1;
        this.editForm.RNGAdd_3_2 = addressData.Add_3_2;
        this.editForm.RNGAdd_4 = addressData.Add_4;
        this.editForm.RNGAdd_5 = addressData.Add_5;
        this.editForm.RNGAdd_6 = addressData.Add_6;
        this.editForm.RNGAdd_7 = addressData.Add_7;
        this.editForm.RNGAdd_8 = addressData.Add_8;
        this.editForm.RNGAdd_9 = addressData.Add_9;
      }
      if (title === '保證人通訊地址') {
        this.editForm.RNGCAdd_1 = addressData.Add_1;
        this.editForm.RNGCAdd_1_1 = addressData.Add_1_1;
        this.editForm.RNGCAdd_1_2 = addressData.Add_1_2;
        this.editForm.RNGCAdd_2 = addressData.Add_2;
        this.editForm.RNGCAdd_2_1 = addressData.Add_2_1;
        this.editForm.RNGCAdd_2_2 = addressData.Add_2_2;
        this.editForm.RNGCAdd_2_3 = addressData.Add_2_3;
        this.editForm.RNGCAdd_2_4 = addressData.Add_2_4;
        this.editForm.RNGCAdd_3 = addressData.Add_3;
        this.editForm.RNGCAdd_3_1 = addressData.Add_3_1;
        this.editForm.RNGCAdd_3_2 = addressData.Add_3_2;
        this.editForm.RNGCAdd_4 = addressData.Add_4;
        this.editForm.RNGCAdd_5 = addressData.Add_5;
        this.editForm.RNGCAdd_6 = addressData.Add_6;
        this.editForm.RNGCAdd_7 = addressData.Add_7;
        this.editForm.RNGCAdd_8 = addressData.Add_8;
        this.editForm.RNGCAdd_9 = addressData.Add_9;
      }
      if (title === '緊急聯絡人2戶籍地址') {
        this.editForm.BZ050 = addressData.Add_1;
        this.editForm.BZ051 = addressData.Add_1_1;
        this.editForm.BZ052 = addressData.Add_1_2;
        this.editForm.BZ053 = addressData.Add_2;
        this.editForm.BZ054 = addressData.Add_2_1;
        this.editForm.BZ055 = addressData.Add_2_2;
        this.editForm.BZ056 = addressData.Add_2_3;
        this.editForm.BZ057 = addressData.Add_2_4;
        this.editForm.BZ058 = addressData.Add_3;
        this.editForm.BZ059 = addressData.Add_3_1;
        this.editForm.BZ060 = addressData.Add_3_2;
        this.editForm.BZ061 = addressData.Add_4;
        this.editForm.BZ062 = addressData.Add_5;
        this.editForm.BZ063 = addressData.Add_6;
        this.editForm.BZ064 = addressData.Add_7;
        this.editForm.BZ065 = addressData.Add_8;
        this.editForm.BZ066 = addressData.Add_9;
      }
      if (title === '緊急聯絡人2通訊地址') {
        this.editForm.BZ068 = addressData.Add_1;
        this.editForm.BZ069 = addressData.Add_1_1;
        this.editForm.BZ070 = addressData.Add_1_2;
        this.editForm.BZ071 = addressData.Add_2;
        this.editForm.BZ072 = addressData.Add_2_1;
        this.editForm.BZ073 = addressData.Add_2_2;
        this.editForm.BZ074 = addressData.Add_2_3;
        this.editForm.BZ075 = addressData.Add_2_4;
        this.editForm.BZ076 = addressData.Add_3;
        this.editForm.BZ077 = addressData.Add_3_1;
        this.editForm.BZ078 = addressData.Add_3_2;
        this.editForm.BZ079 = addressData.Add_4;
        this.editForm.BZ080 = addressData.Add_5;
        this.editForm.BZ081 = addressData.Add_6;
        this.editForm.BZ082 = addressData.Add_7;
        this.editForm.BZ083 = addressData.Add_8;
        this.editForm.BZ084 = addressData.Add_9;
      }
    },
    SaveAndUploadAndDownoadPDF() {
      this.$refs['editForm'].validate((valid, err) => {
        if (valid) {
          this.$emit('openpageloading');
          this.editForm.IsGenPDF = 'Y';
          UploadWithData(this.editForm.FormId, this.editForm).then((res) => {
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
              test.forEach((el) => {
                el.removeAttribute('placeholder'); // 把placeholder拿掉
              });
              html2canvas(this.$refs.editForm.$el, {
                scale: 1.25
              }).then((canvas) => {
                const img = canvas.toDataURL('image/jpeg');
                doc.addImage(img, 'JPEG', 10, 10);
                const blob = doc.output('blob');
                var file = new File([blob], '自然人房客資料卡.pdf', {
                  type: 'pdf'
                });
                const formData = new FormData();
                formData.append('File', file);
                formData.append('id', fileName);
                formData.append('role', 'RN');
                formData.append('IDNo', this.editForm.RNID);
                formData.append('formname', this.editForm.FormName);
                ImgUpload(formData)
                  .then((res) => {
                    if (res.Success) {
                      this.$message.success('上傳並儲存成功');
                      const link = document.createElement('a');
                      link.href = URL.createObjectURL(blob);
                      link.download = '自然人房客資料卡.pdf';
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
          // .finally(() => {
          //   this.$emit('closepageloading');
          // });
        } else {
          var msg = '';
          var errArr = Object.keys(err);
          errArr.forEach((x) => {
            switch (x) {
              case 'RNName':
                msg += '*姓名<br/>';
                break;
              case 'RNID':
                msg += '*身分證字號<br/>';
                break;
              case 'RNBirthday':
                msg += '*出生年月日<br/>';
                break;
              case 'RNCell':
                msg += '*手機<br/>';
                break;
            }
          });
          this.$alert(
            `必填欄位未填：<br/><div style="color:#e24e4e"><i><b>${msg}<b/><i/><div/>`,
            '提示',
            {
              confirmButtonText: '確定',
              dangerouslyUseHTMLString: true
            }
          );
          return false;
        }
      });
    }
  }
};
</script>

<style></style>
