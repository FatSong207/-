<template>
  <div>
    <el-form
      ref="editForm"
      :inline="true"
      :model="editForm"
      class="demo-form-inline"
      :rules="rule"
    >
      <el-row>
        <el-col :span="24">
          <el-card>
            <el-row>
              <el-col :span="8">
                <el-form-item
                  label="姓名："
                  :label-width="formLabelWidth"
                  prop="LNName"
                >
                  <el-input
                    v-model="editForm.LNName"
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
                    v-model="LNgender"
                    label="1"
                  >男</el-radio>
                  <el-radio
                    v-model="LNgender"
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
                  prop="LNID"
                >
                  <el-input
                    v-model="editForm.LNID"
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
                  prop="LNBirthday"
                >
                  <DatePickerE
                    v-model="editForm.LNBirthday"
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
                    v-model="editForm.LNTel_2"
                    placeholder="請輸入電話"
                    clearable
                    style="width: 220px"
                    class="input-with-select"
                  >
                    <el-select
                      slot="prepend"
                      v-model="editForm.LNTel_1"
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
                  prop="LNCell"
                >
                  <el-input
                    v-model="editForm.LNCell"
                    placeholder="請輸入手機"
                    autocomplete="off"
                    clearable
                    onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                  />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="8">
                <el-form-item
                  label="電子信箱："
                  :label-width="formLabelWidth"
                  prop="LNMail"
                >
                  <el-input
                    v-model="editForm.LNMail"
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
                    v-model="editForm.LNLine"
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
                  <el-checkbox v-model="LNAddSameCom">通訊地址同戶籍地址

                  </el-checkbox>
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="24">
                <el-form-item
                  v-show="LNAddSameCom===false"
                  label="通訊地址："
                  :label-width="formLabelWidth"
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
          </el-card>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="24">
          <h3>職業</h3>
          <el-card>
            <el-form-item label="">
              <el-radio
                v-model="BZ001002003"
                label="3"
              >在職中，公司名稱：
                <el-input
                  v-model="editForm.LNCompany"
                  autocomplete="off"
                  clearable
                  size="mini"
                  class="w140px"
                /> 公司電話：
                <el-input
                  v-model="editForm.BZ005"
                  autocomplete="off"
                  clearable
                  size="mini"
                  class="w140px"
                /> 部門：
                <el-input
                  v-model="editForm.BZ006"
                  autocomplete="off"
                  clearable
                  size="mini"
                  class="w140px"
                /> 職稱：
                <el-input
                  v-model="editForm.LNJobtitle"
                  autocomplete="off"
                  clearable
                  size="mini"
                  class="w140px"
                /> 上班時間：
                <el-input
                  v-model="editForm.BZ008"
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
                v-model="BZ001002003"
                label="2"
              >就學中
              </el-radio>
            </el-form-item>
            <br>
            <el-form-item label="">
              <el-radio
                v-model="BZ001002003"
                label="1"
              >無業
              </el-radio>
            </el-form-item>
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
                    v-model="editForm.LNAGName_A"
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
                    v-model="BZ009BZ010"
                    label="1"
                  >男</el-radio>
                  <el-radio
                    v-model="BZ009BZ010"
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
                    v-model="editForm.BZ011"
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
                  prop="LNAGID_A"
                >
                  <el-input
                    v-model="editForm.LNAGID_A"
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
                  prop="LNAGCell_A"
                >
                  <el-input
                    v-model="editForm.LNAGCell_A"
                    placeholder="請輸入電話"
                    autocomplete="off"
                    clearable
                    onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
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
                    :sendedform="sendedformLNAG"
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
                  <el-checkbox v-model="LNAGAddSameCom">通訊地址同戶籍地址
                  </el-checkbox>
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="24">
                <el-form-item
                  v-show="LNAGAddSameCom===false"
                  label="通訊地址："
                  :label-width="formLabelWidth"
                >
                  <Address
                    :sendedform="sendedformLNAGC"
                    :resetall="LNAGAddSameCom"
                    title="代理人通訊地址"
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
          <h3>建物門牌地址</h3>
          <el-card>
            <div class="text item">
              <span>{{ editForm.BAdd }}</span>
            </div>
            <div
              v-if="editForm.IsNew"
              class="text item"
            >
              <span>
                <Address
                  :sendedform="sendedformBAdd"
                  title="建物地址"
                  @geteditaddress="geteditaddress"
                />
              </span>
            </div>
          </el-card>
        </el-col>
      </el-row>
      <el-row :gutter="20">
        <el-col :span="8">
          <h3>建物建號</h3>
          <el-card>
            <div class="text item">
              <span>
                <el-input
                  v-model="editForm.BNo_1"
                  autocomplete="off"
                  clearable
                  size="mini"
                  class="w80px"
                />段
                <el-input
                  v-model="editForm.BNo_2"
                  autocomplete="off"
                  clearable
                  size="mini"
                  class="w80px"
                />小段
                <el-input
                  v-model="editForm.BNo_3"
                  autocomplete="off"
                  clearable
                  size="mini"
                  class="w140px"
                />建號
              </span>
            </div>
          </el-card>
        </el-col>
        <el-col :span="8">
          <h3>坐落地號</h3>
          <el-card>
            <div class="text item">
              <span>
                <el-input
                  v-model="editForm.BPNo_1_A"
                  autocomplete="off"
                  clearable
                  size="mini"
                  class="w80px"
                />段
                <el-input
                  v-model="editForm.BPNo_2_A"
                  autocomplete="off"
                  clearable
                  size="mini"
                  class="w80px"
                />小段
                <el-input
                  v-model="editForm.BPNo_3_A"
                  autocomplete="off"
                  clearable
                  size="mini"
                  class="w140px"
                />地號
              </span>
            </div>
            <div class="text item">
              <span>
                <el-input
                  v-model="editForm.BPNo_1_B"
                  autocomplete="off"
                  clearable
                  size="mini"
                  class="w80px"
                />段
                <el-input
                  v-model="editForm.BPNo_2_B"
                  autocomplete="off"
                  clearable
                  size="mini"
                  class="w80px"
                />小段
                <el-input
                  v-model="editForm.BPNo_3_B"
                  autocomplete="off"
                  clearable
                  size="mini"
                  class="w140px"
                />地號
              </span>
            </div>
          </el-card>
        </el-col>
      </el-row>
      <el-row :gutter="20">
        <el-col :span="12">
          <h3>房屋型態</h3>
          <el-card>
            <div class="text item">
              <span>一般建物：
                <el-checkbox
                  v-model="editForm.BTHouse"
                  true-label="/Yes"
                  false-label="/Off"
                >透天厝
                </el-checkbox>
                <el-checkbox
                  v-model="editForm.BZ014"
                  true-label="/Yes"
                  false-label="/Off"
                >其他
                  <el-input
                    v-model="editForm.BZ015"
                    autocomplete="off"
                    clearable
                    size="mini"
                    class="w140px"
                  />
                </el-checkbox>
              </span>
            </div>
            <div class="text item">
              <span>區分所有建物：
                <el-checkbox
                  v-model="editForm.BTApartment"
                  true-label="/Yes"
                  false-label="/Off"
                >公寓
                </el-checkbox>
                <el-checkbox
                  v-model="editForm.BTMansion"
                  true-label="/Yes"
                  false-label="/Off"
                >華廈
                </el-checkbox>
                <el-checkbox
                  v-model="editForm.BTElevator"
                  true-label="/Yes"
                  false-label="/Off"
                >電梯大樓
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
                    class="w140px"
                  />
                </el-checkbox>
              </span>
            </div>
            <div class="text item">
              <span>其他特殊建物：
                <el-checkbox
                  v-model="editForm.BTFarmHouse"
                  true-label="/Yes"
                  false-label="/Off"
                >農舍
                </el-checkbox>
                <el-checkbox
                  v-model="editForm.BTWorkshop"
                  true-label="/Yes"
                  false-label="/Off"
                >廠房
                </el-checkbox>
                <el-checkbox
                  v-model="editForm.BZ018"
                  true-label="/Yes"
                  false-label="/Off"
                >其他
                  <el-input
                    v-model="editForm.BZ019"
                    autocomplete="off"
                    clearable
                    size="mini"
                    class="w140px"
                  />
                </el-checkbox>
              </span>
            </div>
            <div class="text item">
              <span>&nbsp;
              </span>
            </div>
          </el-card>
        </el-col>
        <el-col :span="12">
          <h3>&nbsp;</h3>
          <el-card>
            <div class="text item">
              <span>建築完成日期：
                <DatePickerE
                  v-model="editForm.BCDate"
                  value-format="yyyy-MM-dd"
                  format="yyyy-MM-dd"
                  type="date"
                  placeholder="選擇日期"
                  style="width: 160px"
                  size="mini"
                />
              </span>
            </div>
            <div class="text item">
              <span>樓層：第
                <el-input
                  v-model="editForm.BLevel"
                  autocomplete="off"
                  clearable
                  size="mini"
                  class="w80px"
                />層／共
                <el-input
                  v-model="editForm.BFloor"
                  autocomplete="off"
                  clearable
                  size="mini"
                  class="w80px"
                />層
              </span>
            </div>
            <div class="text item">
              <span>建物格局：
                <el-input
                  v-model="editForm.BRoom"
                  autocomplete="off"
                  clearable
                  size="mini"
                  class="w80px"
                />房(
                <el-checkbox
                  v-model="editForm.BZ020"
                  true-label="/Yes"
                  false-label="/Off"
                >間
                </el-checkbox>
                <el-checkbox
                  v-model="editForm.BZ021"
                  true-label="/Yes"
                  false-label="/Off"
                >室
                </el-checkbox>)
                <el-input
                  v-model="editForm.BLD"
                  autocomplete="off"
                  clearable
                  size="mini"
                  class="w80px"
                />廳
                <el-input
                  v-model="editForm.BBath"
                  autocomplete="off"
                  clearable
                  size="mini"
                  class="w80px"
                />衛
              </span>
            </div>
            <div class="text item">
              <span>獨立門牌：
                <el-radio
                  v-model="BZ022023"
                  label="1"
                >是</el-radio>
                <el-radio
                  v-model="BZ022023"
                  label="2"
                >否</el-radio>
              </span>
            </div>
          </el-card>
        </el-col>
      </el-row>
      <el-row :gutter="10">
        <el-col :span="18">
          <h3>謄本面積</h3>
          <el-card>
            <div class="text item">
              <span>
                <el-radio
                  v-model="BZ024025"
                  label="1"
                >有</el-radio>
                <el-radio
                  v-model="BZ024025"
                  label="2"
                >無</el-radio>
                包括未登記之改建、增建、加建、違建部分。
              </span>
            </div>
            <div class="text item">
              <span>
                <el-input
                  v-model="editForm.BFloor_1"
                  autocomplete="off"
                  clearable
                  size="mini"
                  class="w80px"
                />層
                <el-input
                  v-model="editForm.BArea_1"
                  autocomplete="off"
                  clearable
                  size="mini"
                  class="w80px"
                />平方公尺，
                <el-input
                  v-model="editForm.BFloor_2"
                  autocomplete="off"
                  clearable
                  size="mini"
                  class="w80px"
                />層
                <el-input
                  v-model="editForm.BArea_2"
                  autocomplete="off"
                  clearable
                  size="mini"
                  class="w80px"
                />平方公尺，
                <el-input
                  v-model="editForm.BFloor_3"
                  autocomplete="off"
                  clearable
                  size="mini"
                  class="w80px"
                />層
                <el-input
                  v-model="editForm.BArea_3"
                  autocomplete="off"
                  clearable
                  size="mini"
                  class="w80px"
                />平方公尺，共計
                <el-input
                  v-model="editForm.BArea"
                  autocomplete="off"
                  clearable
                  size="mini"
                  class="w80px"
                />平方公尺，用途
                <el-input
                  v-model="editForm.BUse"
                  autocomplete="off"
                  clearable
                  size="mini"
                  class="w80px"
                />
              </span>
            </div>
          </el-card>
        </el-col>
        <el-col :span="3">
          <h3>租金</h3>
          <el-card>
            <div class="text item">
              <span>
                <el-input
                  v-model="editForm.BTCRent"
                  autocomplete="off"
                  clearable
                  size="mini"
                  class="w80px"
                />元／月
              </span>
            </div>
            <div class="text item">
              <span>&nbsp;
              </span>
            </div>
          </el-card>
        </el-col>
        <el-col :span="3">
          <h3>押金</h3>
          <el-card>
            <div class="text item">
              <span>
                <el-input
                  v-model="editForm.Bdeposit"
                  autocomplete="off"
                  clearable
                  size="mini"
                  class="w80px"
                />元
              </span>
            </div>
            <div class="text item">
              <span>&nbsp;
              </span>
            </div>
          </el-card>
        </el-col>
      </el-row>
      <el-row :gutter="20">
        <el-col :span="12">
          <h3>管委會</h3>
          <el-card>
            <div class="text item">
              <span>
                <el-radio
                  v-model="BMFee"
                  label="1"
                >有</el-radio>
                <el-radio
                  v-model="BMFee"
                  label="2"
                >無</el-radio>
                &nbsp;&nbsp;&nbsp;&nbsp;管理委員會統一管理？
              </span>
            </div>
            <div class="text item">
              <span>若有，建物管理費為
                <el-radio
                  v-model="BZ026027028"
                  label="1"
                >月繳</el-radio>
                <el-radio
                  v-model="BZ026027028"
                  label="2"
                >季繳</el-radio>
                <el-radio
                  v-model="BZ026027028"
                  label="3"
                >年繳</el-radio>
                新台幣
                <el-input
                  v-model="editForm.BZ029"
                  autocomplete="off"
                  clearable
                  size="mini"
                  class="w80px"
                />元
                <el-checkbox
                  v-model="editForm.BZ030"
                  true-label="/Yes"
                  false-label="/Off"
                >其他
                  <el-input
                    v-model="editForm.BZ031"
                    autocomplete="off"
                    clearable
                    size="mini"
                    class="w80px"
                  />元
                </el-checkbox>
              </span>
            </div>
            <div class="text item">
              <span>
                <el-radio
                  v-model="BZ032033"
                  label="1"
                >有</el-radio>
                <el-radio
                  v-model="BZ032033"
                  label="2"
                >無</el-radio>積欠租賃標的、停車位管理費(清潔費)；若有，新臺幣
                <el-input
                  v-model="editForm.BZ034"
                  autocomplete="off"
                  clearable
                  size="mini"
                  class="w80px"
                />元
              </span>
            </div>
            <div class="text item">
              <span>
                <el-radio
                  v-model="BConStature"
                  label="1"
                >有</el-radio>
                <el-radio
                  v-model="BConStature"
                  label="2"
                >無</el-radio>
                &nbsp;&nbsp;&nbsp;&nbsp;公寓大廈規約或其他住戶應遵循事項。
              </span>
            </div>
            <div class="text item">
              <span>
                <el-radio
                  v-model="BFireSaIn"
                  label="1"
                >有</el-radio>
                <el-radio
                  v-model="BFireSaIn"
                  label="2"
                >無</el-radio>
                &nbsp;&nbsp;&nbsp;&nbsp;定期辦理消防安全檢查。
              </span>
            </div>
          </el-card>
        </el-col>
        <el-col :span="12">
          <h3>門禁管理</h3>
          <el-card>
            <div class="text item">
              <span>
                <el-checkbox
                  v-model="editForm.BZ035"
                  true-label="/Yes"
                  false-label="/Off"
                >無
                </el-checkbox>
                <el-checkbox
                  v-model="editForm.BZ036"
                  true-label="/Yes"
                  false-label="/Off"
                >有
                </el-checkbox>
                <el-checkbox
                  v-model="editForm.BZ037"
                  true-label="/Yes"
                  false-label="/Off"
                >管理員
                </el-checkbox>
                <el-checkbox
                  v-model="editForm.BZ038"
                  true-label="/Yes"
                  false-label="/Off"
                >刷卡門禁
                </el-checkbox>
                <el-checkbox
                  v-model="editForm.BZ039"
                  true-label="/Yes"
                  false-label="/Off"
                >其他
                  <el-input
                    v-model="editForm.BZ040"
                    autocomplete="off"
                    clearable
                    size="mini"
                    class="w80px"
                  />
                </el-checkbox>(可複選)
              </span>
            </div>
          </el-card>
        </el-col>
      </el-row>
      <el-row>
        <el-col>
          <h3>停車管理</h3>
          <el-card>
            <div class="text item">
              <span>1.
                <el-radio
                  v-model="BCar"
                  label="1"
                >有</el-radio>
                <el-radio
                  v-model="BCar"
                  label="2"
                >無</el-radio>汽車停車位。（
                <el-radio
                  v-model="BZ041042"
                  label="1"
                >有</el-radio>
                <el-radio
                  v-model="BZ041042"
                  label="2"
                >無</el-radio>獨立權狀）。
                若有，車位樣式
                <el-checkbox
                  v-model="editForm.BRampFlatParking"
                  true-label="/Yes"
                  false-label="/Off"
                >坡道平面
                </el-checkbox>
                <el-checkbox
                  v-model="editForm.BRampMechParking"
                  true-label="/Yes"
                  false-label="/Off"
                >坡道機械
                </el-checkbox>
                <el-checkbox
                  v-model="editForm.BMechFlatParking"
                  true-label="/Yes"
                  false-label="/Off"
                >機械平面
                </el-checkbox>
                <el-checkbox
                  v-model="editForm.BMechMechParking"
                  true-label="/Yes"
                  false-label="/Off"
                >機械機械
                </el-checkbox>。
              </span>
            </div>
            <div class="text item">
              <span>
                &nbsp;&nbsp;&nbsp;&nbsp;於地（下）第
                <el-input
                  v-model="editForm.BCarFloor"
                  autocomplete="off"
                  clearable
                  size="mini"
                  class="w80px"
                />層，車位編號
                <el-input
                  v-model="editForm.BCarNo"
                  autocomplete="off"
                  clearable
                  size="mini"
                  class="w120px"
                />號。
                <el-radio
                  v-model="BZ043044"
                  label="1"
                >有</el-radio>
                <el-radio
                  v-model="BZ043044"
                  label="2"
                >無</el-radio>檢附分管協議及圖說。
              </span>
            </div>
            <div class="text item">
              <span>
                &nbsp;&nbsp;&nbsp;&nbsp;停車管理費為
                <el-radio
                  v-model="BZ045046047"
                  label="1"
                >月繳</el-radio>
                <el-radio
                  v-model="BZ045046047"
                  label="2"
                >季繳</el-radio>
                <el-radio
                  v-model="BZ045046047"
                  label="3"
                >年繳</el-radio>
                新台幣
                <el-input
                  v-model="editForm.BZ048"
                  autocomplete="off"
                  clearable
                  size="mini"
                  class="w80px"
                />元
                <el-checkbox
                  v-model="editForm.BZ049"
                  true-label="/Yes"
                  false-label="/Off"
                >其他
                  <el-input
                    v-model="editForm.BZ050"
                    autocomplete="off"
                    clearable
                    size="mini"
                    class="w80px"
                  />元
                </el-checkbox>
              </span>
            </div>
            <div class="text item">
              <span>2.
                <el-radio
                  v-model="BZ051052"
                  label="1"
                >有</el-radio>
                <el-radio
                  v-model="BZ051052"
                  label="2"
                >無</el-radio>機車停車位，
              </span>
            </div>
            <div class="text item">
              <span>
                &nbsp;&nbsp;&nbsp;&nbsp;若有，於地（下）第
                <el-input
                  v-model="editForm.BZ053"
                  autocomplete="off"
                  clearable
                  size="mini"
                  class="w80px"
                />層，車位編號
                <el-input
                  v-model="editForm.BZ054"
                  autocomplete="off"
                  clearable
                  size="mini"
                  class="w120px"
                />號。停車管理費為
                <el-radio
                  v-model="BZ055056057"
                  label="1"
                >月繳</el-radio>
                <el-radio
                  v-model="BZ055056057"
                  label="2"
                >季繳</el-radio>
                <el-radio
                  v-model="BZ055056057"
                  label="3"
                >年繳</el-radio>
                新台幣
                <el-input
                  v-model="editForm.BZ058"
                  autocomplete="off"
                  clearable
                  size="mini"
                  class="w80px"
                />元
                <el-checkbox
                  v-model="editForm.BZ059"
                  true-label="/Yes"
                  false-label="/Off"
                >其他
                  <el-input
                    v-model="editForm.BZ060"
                    autocomplete="off"
                    clearable
                    size="mini"
                    class="w80px"
                  />元
                </el-checkbox>
              </span>
            </div>
          </el-card>
        </el-col>
      </el-row>
      <el-row>
        <el-col>
          <h3>屋況及租屋安全檢核<i style="font-size: 15px">(租賃標的現況確認書)</i></h3>
          <h3>租賃標的屋況</h3>
          <el-card>
            <div class="text item">
              <span>
                <el-radio
                  v-model="B1WaterLeakage"
                  label="1"
                >無漏水</el-radio>
                <el-radio
                  v-model="B1WaterLeakage"
                  label="2"
                >有漏水</el-radio>情形，滲漏水處
                <el-input
                  v-model="editForm.B1WaterEX"
                  autocomplete="off"
                  clearable
                  size="mini"
                  class="w140px"
                />。
              </span>
            </div>
            <div class="text item">
              <span>若有滲漏水處之處理：
                <el-radio
                  v-model="BZ064065"
                  label="1"
                >由出租人修繕後交屋</el-radio>
                <el-radio
                  v-model="BZ064065"
                  label="2"
                >以現況交屋 </el-radio>
                <el-checkbox
                  v-model="editForm.BZ066"
                  true-label="/Yes"
                  false-label="/Off"
                >其他
                  <el-input
                    v-model="editForm.BZ067"
                    autocomplete="off"
                    clearable
                    size="mini"
                    class="w80px"
                  />。
                </el-checkbox>
              </span>
            </div>
            <el-divider />
            <div class="text item">
              <span>供水及排水
                <el-radio
                  v-model="BZ068069"
                  label="1"
                >正常</el-radio>
                <el-radio
                  v-model="BZ068069"
                  label="2"
                >不正常 </el-radio>。
              </span>
            </div>
            <div class="text item">
              <span>若不正常，由
                <el-radio
                  v-model="BZ070071"
                  label="1"
                >出租人</el-radio>
                <el-radio
                  v-model="BZ070071"
                  label="2"
                >承租人</el-radio>負責維修。
              </span>
            </div>
            <el-divider />
            <div class="text item">
              <span>本建物(專有部分)是否曾發生兇殺、自殺、一氧化碳中毒或其他非自然死亡情事：</span>
            </div>
            <div class="text item">
              <span>&nbsp;&nbsp;&nbsp;&nbsp;1.於產權持有期間
                <el-radio
                  v-model="BMurder"
                  label="1"
                >有</el-radio>
                <el-radio
                  v-model="BMurder"
                  label="2"
                >無</el-radio>曾發生上列情事。
              </span>
            </div>
            <div class="text item">
              <span>&nbsp;&nbsp;&nbsp;&nbsp;2.產權持有前，出租人
                <el-radio
                  v-model="BMurderCheck"
                  label="1"
                >確認無上列情事</el-radio>
                <el-radio
                  v-model="BMurderCheck"
                  label="2"
                >知道曾發生上列情事</el-radio>
                <el-radio
                  v-model="BMurderCheck"
                  label="3"
                >不知曾否發生上列情事</el-radio>。
              </span>
            </div>
            <el-divider />
            <div class="text item">
              <span>
                <el-radio
                  v-model="BZ072073"
                  label="1"
                >有</el-radio>
                <el-radio
                  v-model="BZ072073"
                  label="2"
                >無</el-radio>做過混凝土中水溶性氯離子含量檢測 (例如海砂屋檢測事項)。
              </span>
            </div>
            <div class="text item">
              <span>若有，檢測結果：
                <el-radio
                  v-model="BZ074075"
                  label="1"
                >合格</el-radio>
                <el-radio
                  v-model="BZ074075"
                  label="2"
                >不合格</el-radio>
                <el-checkbox
                  v-model="editForm.BZ076"
                  true-label="/Yes"
                  false-label="/Off"
                >其他
                  <el-input
                    v-model="editForm.BZ077"
                    autocomplete="off"
                    clearable
                    size="mini"
                    class="w80px"
                  />。
                </el-checkbox>
              </span>
            </div>
            <el-divider />
            <div class="text item">
              <span>
                <el-radio
                  v-model="BZ078079"
                  label="1"
                >有</el-radio>
                <el-radio
                  v-model="BZ078079"
                  label="2"
                >無</el-radio>無曾經做過輻射屋檢測? 若有，請檢附檢測證明文件。
              </span>
            </div>
            <div class="text item">
              <span>檢測結果：
                <el-radio
                  v-model="BZ080081"
                  label="1"
                >是</el-radio>
                <el-radio
                  v-model="BZ080081"
                  label="2"
                >否</el-radio>有輻射異常？
              </span>
            </div>
            <div class="text item">
              <span>若有，
                <el-radio
                  v-model="BZ082083"
                  label="1"
                >由出租人修繕後交屋</el-radio>
                <el-radio
                  v-model="BZ082083"
                  label="2"
                >以現況交屋</el-radio>
                <el-checkbox
                  v-model="editForm.BZ084"
                  true-label="/Yes"
                  false-label="/Off"
                >其他
                  <el-input
                    v-model="editForm.BZ085"
                    autocomplete="off"
                    clearable
                    size="mini"
                    class="w80px"
                  />。
                </el-checkbox>
              </span>
            </div>
            <el-divider />
            <div class="text item">
              <span>
                <el-radio
                  v-model="BZ086087"
                  label="1"
                >有</el-radio>
                <el-radio
                  v-model="BZ086087"
                  label="2"
                >無</el-radio>住宅用火災警報器。
              </span>
            </div>
            <div class="text item">
              <span>室內具備
                <el-checkbox
                  v-model="editForm.BZ088"
                  true-label="/Yes"
                  false-label="/Off"
                >滅火器
                </el-checkbox>
                <el-checkbox
                  v-model="editForm.BZ089"
                  true-label="/Yes"
                  false-label="/Off"
                >火警警報器
                </el-checkbox>
                <el-checkbox
                  v-model="editForm.BZ090"
                  true-label="/Yes"
                  false-label="/Off"
                >獨立型偵煙器
                </el-checkbox>(可複選)。 <b>(屬應設置火警自動警報設備之住宅所有權人應依消防法第六條第五項規定設置及維護住宅用火災警報器。)</b>
              </span>
            </div>
            <div class="text item">
              <span>若有，
                <el-radio
                  v-model="BZ091092"
                  label="1"
                >有</el-radio>
                <el-radio
                  v-model="BZ091092"
                  label="2"
                >無</el-radio>定期辦理消防安全檢查。
              </span>
            </div>
            <el-divider />
            <div class="text item">
              <span>具備
                <el-checkbox
                  v-model="editForm.BZ093"
                  true-label="/Yes"
                  false-label="/Off"
                >馬桶
                </el-checkbox>
                <el-checkbox
                  v-model="editForm.BZ094"
                  true-label="/Yes"
                  false-label="/Off"
                >洗臉盆
                </el-checkbox>
                <el-checkbox
                  v-model="editForm.BZ095"
                  true-label="/Yes"
                  false-label="/Off"
                >浴缸(或淋浴)
                </el-checkbox>等衛浴設備
              </span>
            </div>
            <el-divider />
            <div class="text item">
              <span>
                <el-radio
                  v-model="BZ096097"
                  label="1"
                >有</el-radio>
                <el-radio
                  v-model="BZ096097"
                  label="2"
                >無</el-radio>熱水器，且為
                <el-checkbox
                  v-model="editForm.BZ098"
                  true-label="/Yes"
                  false-label="/Off"
                >電熱水器
                </el-checkbox>
                <el-checkbox
                  v-model="editForm.BZ099"
                  true-label="/Yes"
                  false-label="/Off"
                >瓦斯熱水器
                </el-checkbox>
              </span>
            </div>
            <div class="text item">
              <span>若為瓦斯熱水器，係置於
                <el-radio
                  v-model="BZ100101"
                  label="1"
                >室外</el-radio>
                <el-radio
                  v-model="BZ100101"
                  label="2"
                >室內</el-radio>。
              </span>
            </div>
            <div class="text item">
              <span>如置於室內，
                <el-radio
                  v-model="BZ102103"
                  label="1"
                >有</el-radio>
                <el-radio
                  v-model="BZ102103"
                  label="2"
                >無</el-radio>加裝排氣管，且符合「燃氣熱水器及其配管安裝標準」。
              </span>
            </div>
          </el-card>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="10">
          <h3>匯款資料</h3>
          <el-card>
            <RemittanceWebForm
              :sendedform="sendRemittanceform"
              :fielddisabled="editForm.disabled"
              @getremittancebyemit="GetRemittanceByEmit"
            />
          </el-card>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="14">
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
  validateIDReg
} from '@/utils/validate';
import Address from '@/components/Address/Address.vue';
import RemittanceWebForm from '@/components/Remittance/RemittanceForWebForm.vue';
import DatePickerE from '@/components/RocDatepickerE';
import JsPDF from 'jspdf';
import html2canvas from 'html2canvas';
export default {
  name: 'A000001',
  components: { DatePickerE, Address, RemittanceWebForm },
  props: {
    editform: { type: Object, default: null }
  },
  data() {
    return {
      editForm: { ...this.editform },
      sendedform: {},
      sendedform1: {},
      sendedformLNAG: {},
      sendedformLNAGC: {},
      sendedformBAdd: {},
      sendRemittanceform: {},
      rule: {
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
        LNCell: [
          { required: true, message: '請輸入手機', trigger: 'blur' },
          { trigger: 'change', validator: validateCellReg }
        ],
        LNAGCell_A: [
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
      formLabelWidth: '110px',
      LNgender: '',
      BMFee: '',
      BConStature: '',
      BFireSaIn: '',
      BCar: '',
      B1WaterLeakage: '',
      BMurder: '',
      BMurderCheck: '',
      BZ001002003: '',
      BZ009BZ010: '',
      BZ022023: '',
      BZ024025: '',
      BZ026027028: '',
      BZ032033: '',
      BZ041042: '',
      BZ043044: '',
      BZ045046047: '',
      BZ051052: '',
      BZ055056057: '',
      BZ064065: '',
      BZ068069: '',
      BZ070071: '',
      BZ072073: '',
      BZ074075: '',
      BZ078079: '',
      BZ080081: '',
      BZ082083: '',
      BZ086087: '',
      BZ091092: '',
      BZ096097: '',
      BZ100101: '',
      BZ102103: '',
      src_1: '',
      LNAddSameCom: false,
      LNAGAddSameCom: false,
      centerDialogVisible: false
    };
  },
  watch: {
    editform: {
      immediate: true,
      handler() {
        console.log('editformch');
        if (Object.keys(this.editform).length !== 0) {
          this.editForm = this.editform;
          this.$emit('openpageloading');
          this.sendedform = {
            Add_1: this.editForm.LNAdd_1,
            Add_1_1: this.editForm.LNAdd_1_1,
            Add_1_2: this.editForm.LNAdd_1_2,
            Add_2: this.editForm.LNAdd_2,
            Add_2_1: this.editForm.LNAdd_2_1,
            Add_2_2: this.editForm.LNAdd_2_2,
            Add_2_3: this.editForm.LNAdd_2_3,
            Add_2_4: this.editForm.LNAdd_2_4,
            Add_3: this.editForm.LNAdd_3,
            Add_3_1: this.editForm.LNAdd_3_1,
            Add_3_2: this.editForm.LNAdd_3_2,
            Add_4: this.editForm.LNAdd_4,
            Add_5: this.editForm.LNAdd_5,
            Add_6: this.editForm.LNAdd_6,
            Add_7: this.editForm.LNAdd_7,
            Add_8: this.editForm.LNAdd_8,
            Add_9: this.editForm.LNAdd_9
          };
          this.sendedform1 = {
            Add_1: this.editForm.LNAddC_1,
            Add_1_1: this.editForm.LNAddC_1_1,
            Add_1_2: this.editForm.LNAddC_1_2,
            Add_2: this.editForm.LNAddC_2,
            Add_2_1: this.editForm.LNAddC_2_1,
            Add_2_2: this.editForm.LNAddC_2_2,
            Add_2_3: this.editForm.LNAddC_2_3,
            Add_2_4: this.editForm.LNAddC_2_4,
            Add_3: this.editForm.LNAddC_3,
            Add_3_1: this.editForm.LNAddC_3_1,
            Add_3_2: this.editForm.LNAddC_3_2,
            Add_4: this.editForm.LNAddC_4,
            Add_5: this.editForm.LNAddC_5,
            Add_6: this.editForm.LNAddC_6,
            Add_7: this.editForm.LNAddC_7,
            Add_8: this.editForm.LNAddC_8,
            Add_9: this.editForm.LNAddC_9
          };
          this.sendedformLNAG = {
            Add_1: this.editForm.LNAGAdd_1_A,
            Add_1_1: this.editForm.LNAGAdd_1_1_A,
            Add_1_2: this.editForm.LNAGAdd_1_2_A,
            Add_2: this.editForm.LNAGAdd_2_A,
            Add_2_1: this.editForm.LNAGAdd_2_1_A,
            Add_2_2: this.editForm.LNAGAdd_2_2_A,
            Add_2_3: this.editForm.LNAGAdd_2_3_A,
            Add_2_4: this.editForm.LNAGAdd_2_4_A,
            Add_3: this.editForm.LNAGAdd_3_A,
            Add_3_1: this.editForm.LNAGAdd_3_1_A,
            Add_3_2: this.editForm.LNAGAdd_3_2_A,
            Add_4: this.editForm.LNAGAdd_4_A,
            Add_5: this.editForm.LNAGAdd_5_A,
            Add_6: this.editForm.LNAGAdd_6_A,
            Add_7: this.editForm.LNAGAdd_7_A,
            Add_8: this.editForm.LNAGAdd_8_A,
            Add_9: this.editForm.LNAGAdd_9_A
          };
          this.sendedformLNAGC = {
            Add_1: this.editForm.LNAGAddC_1_A,
            Add_1_1: this.editForm.LNAGAddC_1_1_A,
            Add_1_2: this.editForm.LNAGAddC_1_2_A,
            Add_2: this.editForm.LNAGAddC_2_A,
            Add_2_1: this.editForm.LNAGAddC_2_1_A,
            Add_2_2: this.editForm.LNAGAddC_2_2_A,
            Add_2_3: this.editForm.LNAGAddC_2_3_A,
            Add_2_4: this.editForm.LNAGAddC_2_4_A,
            Add_3: this.editForm.LNAGAddC_3_A,
            Add_3_1: this.editForm.LNAGAddC_3_1_A,
            Add_3_2: this.editForm.LNAGAddC_3_2_A,
            Add_4: this.editForm.LNAGAddC_4_A,
            Add_5: this.editForm.LNAGAddC_5_A,
            Add_6: this.editForm.LNAGAddC_6_A,
            Add_7: this.editForm.LNAGAddC_7_A,
            Add_8: this.editForm.LNAGAddC_8_A,
            Add_9: this.editForm.LNAGAddC_9_A
          };
          this.sendRemittanceform = {
            LAName: this.editForm.LNName,
            LBankName: this.editForm.LBankName,
            LBankNo: this.editForm.LBankNo,
            LBranchName: this.editForm.LBranchName,
            LBranchNo: this.editForm.LBranchNo,
            LANo: this.editForm.LANo
          };
          this.LNgender =
            this.editForm.LNGender1 === '/Yes'
              ? '1'
              : this.editForm.LNGender2 === '/Yes'
                ? '2'
                : '1';
          this.BMFee =
            this.editForm.BMFee_Y === '/Yes'
              ? '1'
              : this.editForm.BMFee_N === '/Yes'
                ? '2'
                : '';
          this.BConStature =
            this.editForm.BConStature_Y === '/Yes'
              ? '1'
              : this.editForm.BConStature_N === '/Yes'
                ? '2'
                : '';
          this.BFireSaIn =
            this.editForm.BFireSaIn_Y === '/Yes'
              ? '1'
              : this.editForm.BFireSaIn_N === '/Yes'
                ? '2'
                : '';
          this.BCar =
            this.editForm.BCar_Y === '/Yes'
              ? '1'
              : this.editForm.BCar_N === '/Yes'
                ? '2'
                : '';
          this.B1WaterLeakage =
            this.editForm.B1WaterLeakage_Y === '/Yes'
              ? '2'
              : this.editForm.B1WaterLeakage_N === '/Yes'
                ? '1'
                : '';
          this.BMurder =
            this.editForm.BMurder_Y === '/Yes'
              ? '1'
              : this.editForm.BMurder_N === '/Yes'
                ? '2'
                : '';
          this.BMurderCheck =
            this.editForm.BMurder_No_Check === '/Yes'
              ? '1'
              : this.editForm.BMurder_Yes_Check === '/Yes'
                ? '2'
                : this.editForm.BMurder_Yes_Dont === '/Yes'
                  ? '3'
                  : '';
          this.BZ001002003 =
            this.editForm.BZ001 === '/Yes'
              ? '1'
              : this.editForm.BZ002 === '/Yes'
                ? '2'
                : this.editForm.BZ003 === '/Yes'
                  ? '3'
                  : '3';
          this.BZ009BZ010 =
            this.editForm.BZ009 === '/Yes'
              ? '1'
              : this.editForm.BZ010 === '/Yes'
                ? '2'
                : '';
          this.BZ022023 =
            this.editForm.BZ022 === '/Yes'
              ? '1'
              : this.editForm.BZ023 === '/Yes'
                ? '2'
                : '';
          this.BZ024025 =
            this.editForm.BZ024 === '/Yes'
              ? '1'
              : this.editForm.BZ025 === '/Yes'
                ? '2'
                : '';
          this.BZ026027028 =
            this.editForm.BZ026 === '/Yes'
              ? '1'
              : this.editForm.BZ027 === '/Yes'
                ? '2'
                : this.editForm.BZ028 === '/Yes'
                  ? '3'
                  : '';
          this.BZ032033 =
            this.editForm.BZ032 === '/Yes'
              ? '1'
              : this.editForm.BZ033 === '/Yes'
                ? '2'
                : '';
          this.BZ041042 =
            this.editForm.BZ041 === '/Yes'
              ? '1'
              : this.editForm.BZ042 === '/Yes'
                ? '2'
                : '';
          this.BZ043044 =
            this.editForm.BZ043 === '/Yes'
              ? '1'
              : this.editForm.BZ044 === '/Yes'
                ? '2'
                : '';
          this.BZ045046047 =
            this.editForm.BZ045 === '/Yes'
              ? '1'
              : this.editForm.BZ046 === '/Yes'
                ? '2'
                : this.editForm.BZ047 === '/Yes'
                  ? '3'
                  : '';
          this.BZ051052 =
            this.editForm.BZ051 === '/Yes'
              ? '1'
              : this.editForm.BZ052 === '/Yes'
                ? '2'
                : '';
          this.BZ055056057 =
            this.editForm.BZ055 === '/Yes'
              ? '1'
              : this.editForm.BZ056 === '/Yes'
                ? '2'
                : this.editForm.BZ057 === '/Yes'
                  ? '3'
                  : '';
          this.BZ064065 =
            this.editForm.BZ064 === '/Yes'
              ? '1'
              : this.editForm.BZ065 === '/Yes'
                ? '2'
                : '';
          this.BZ068069 =
            this.editForm.BZ068 === '/Yes'
              ? '1'
              : this.editForm.BZ069 === '/Yes'
                ? '2'
                : '';
          this.BZ070071 =
            this.editForm.BZ070 === '/Yes'
              ? '1'
              : this.editForm.BZ071 === '/Yes'
                ? '2'
                : '';
          this.BZ072073 =
            this.editForm.BZ072 === '/Yes'
              ? '1'
              : this.editForm.BZ073 === '/Yes'
                ? '2'
                : '';
          this.BZ074075 =
            this.editForm.BZ074 === '/Yes'
              ? '1'
              : this.editForm.BZ075 === '/Yes'
                ? '2'
                : '';
          this.BZ078079 =
            this.editForm.BZ078 === '/Yes'
              ? '1'
              : this.editForm.BZ079 === '/Yes'
                ? '2'
                : '';
          this.BZ080081 =
            this.editForm.BZ080 === '/Yes'
              ? '1'
              : this.editForm.BZ081 === '/Yes'
                ? '2'
                : '';
          this.BZ082083 =
            this.editForm.BZ082 === '/Yes'
              ? '1'
              : this.editForm.BZ083 === '/Yes'
                ? '2'
                : '';
          this.BZ086087 =
            this.editForm.BZ086 === '/Yes'
              ? '1'
              : this.editForm.BZ087 === '/Yes'
                ? '2'
                : '';
          this.BZ091092 =
            this.editForm.BZ091 === '/Yes'
              ? '1'
              : this.editForm.BZ092 === '/Yes'
                ? '2'
                : '';
          this.BZ096097 =
            this.editForm.BZ096 === '/Yes'
              ? '1'
              : this.editForm.BZ097 === '/Yes'
                ? '2'
                : '';
          this.BZ100101 =
            this.editForm.BZ100 === '/Yes'
              ? '1'
              : this.editForm.BZ101 === '/Yes'
                ? '2'
                : '';
          this.BZ102103 =
            this.editForm.BZ102 === '/Yes'
              ? '1'
              : this.editForm.BZ103 === '/Yes'
                ? '2'
                : '';
          this.LNAddSameCom = this.editForm.LNAddSame === '/Yes';
          this.LNAGAddSameCom = this.editForm.LNAGAddSame === '/Yes';
          this.GetSig();
          // this.$emit('closepageloading');
        }
      }
    },
    LNgender: {
      immediate: true,
      handler() {
        switch (this.LNgender) {
          case '1':
            this.editForm.LNGender1 = '/Yes';
            this.editForm.LNGender2 = '/Off';
            break;
          case '2':
            this.editForm.LNGender1 = '/Off';
            this.editForm.LNGender2 = '/Yes';
            break;
        }
      }
    },
    BMFee: {
      handler() {
        switch (this.BMFee) {
          case '1':
            this.editForm.BMFee_Y = '/Yes';
            this.editForm.BMFee_N = '/Off';
            break;
          case '2':
            this.editForm.BMFee_Y = '/Off';
            this.editForm.BMFee_N = '/Yes';
            break;
        }
      }
    },
    BConStature: {
      handler() {
        switch (this.BConStature) {
          case '1':
            this.editForm.BConStature_Y = '/Yes';
            this.editForm.BConStature_N = '/Off';
            break;
          case '2':
            this.editForm.BConStature_Y = '/Off';
            this.editForm.BConStature_N = '/Yes';
            break;
        }
      }
    },
    BFireSaIn: {
      handler() {
        switch (this.BFireSaIn) {
          case '1':
            this.editForm.BFireSaIn_Y = '/Yes';
            this.editForm.BFireSaIn_N = '/Off';
            break;
          case '2':
            this.editForm.BFireSaIn_Y = '/Off';
            this.editForm.BFireSaIn_N = '/Yes';
            break;
        }
      }
    },
    BCar: {
      handler() {
        switch (this.BCar) {
          case '1':
            this.editForm.BCar_Y = '/Yes';
            this.editForm.BCar_N = '/Off';
            break;
          case '2':
            this.editForm.BCar_Y = '/Off';
            this.editForm.BCar_N = '/Yes';
            break;
        }
      }
    },
    B1WaterLeakage: {
      handler() {
        switch (this.B1WaterLeakage) {
          case '2':
            this.editForm.B1WaterLeakage_Y = '/Yes';
            this.editForm.B1WaterLeakage_N = '/Off';
            break;
          case '1':
            this.editForm.B1WaterLeakage_Y = '/Off';
            this.editForm.B1WaterLeakage_N = '/Yes';
            break;
        }
      }
    },
    BMurder: {
      handler() {
        switch (this.BMurder) {
          case '1':
            this.editForm.BMurder_Y = '/Yes';
            this.editForm.BMurder_N = '/Off';
            break;
          case '2':
            this.editForm.BMurder_Y = '/Off';
            this.editForm.BMurder_N = '/Yes';
            break;
        }
      }
    },
    BMurderCheck: {
      handler() {
        switch (this.BMurderCheck) {
          case '1':
            this.editForm.BMurder_No_Check = '/Yes';
            this.editForm.BMurder_Yes_Check = '/Off';
            this.editForm.BMurder_Yes_Dont = '/Off';
            break;
          case '2':
            this.editForm.BMurder_No_Check = '/Off';
            this.editForm.BMurder_Yes_Check = '/Yes';
            this.editForm.BMurder_Yes_Dont = '/Off';
            break;
          case '3':
            this.editForm.BMurder_No_Check = '/Off';
            this.editForm.BMurder_Yes_Check = '/Off';
            this.editForm.BMurder_Yes_Dont = '/Yes';
            break;
        }
      }
    },
    BZ001002003: {
      immediate: true,
      handler() {
        switch (this.BZ001002003) {
          case '1':
            this.editForm.BZ001 = '/Yes';
            this.editForm.BZ002 = '/Off';
            this.editForm.BZ003 = '/Off';
            break;
          case '2':
            this.editForm.BZ001 = '/Off';
            this.editForm.BZ002 = '/Yes';
            this.editForm.BZ003 = '/Off';
            break;
          case '3':
            this.editForm.BZ001 = '/Off';
            this.editForm.BZ002 = '/Off';
            this.editForm.BZ003 = '/Yes';
            break;
        }
      }
    },
    BZ009BZ010: {
      handler() {
        switch (this.BZ009BZ010) {
          case '1':
            this.editForm.BZ009 = '/Yes';
            this.editForm.BZ010 = '/Off';
            break;
          case '2':
            this.editForm.BZ009 = '/Off';
            this.editForm.BZ010 = '/Yes';
            break;
        }
      }
    },
    BZ022023: {
      handler() {
        switch (this.BZ022023) {
          case '1':
            this.editForm.BZ022 = '/Yes';
            this.editForm.BZ023 = '/Off';
            break;
          case '2':
            this.editForm.BZ022 = '/Off';
            this.editForm.BZ023 = '/Yes';
            break;
        }
      }
    },
    BZ024025: {
      handler() {
        switch (this.BZ024025) {
          case '1':
            this.editForm.BZ024 = '/Yes';
            this.editForm.BZ025 = '/Off';
            break;
          case '2':
            this.editForm.BZ024 = '/Off';
            this.editForm.BZ025 = '/Yes';
            break;
        }
      }
    },
    BZ026027028: {
      handler() {
        switch (this.BZ026027028) {
          case '1':
            this.editForm.BZ026 = '/Yes';
            this.editForm.BZ027 = '/Off';
            this.editForm.BZ028 = '/Off';
            break;
          case '2':
            this.editForm.BZ026 = '/Off';
            this.editForm.BZ027 = '/Yes';
            this.editForm.BZ028 = '/Off';
            break;
          case '3':
            this.editForm.BZ026 = '/Off';
            this.editForm.BZ027 = '/Off';
            this.editForm.BZ028 = '/Yes';
            break;
        }
      }
    },
    BZ032033: {
      handler() {
        switch (this.BZ032033) {
          case '1':
            this.editForm.BZ032 = '/Yes';
            this.editForm.BZ033 = '/Off';
            break;
          case '2':
            this.editForm.BZ032 = '/Off';
            this.editForm.BZ033 = '/Yes';
            break;
        }
      }
    },
    BZ041042: {
      handler() {
        switch (this.BZ041042) {
          case '1':
            this.editForm.BZ041 = '/Yes';
            this.editForm.BZ042 = '/Off';
            break;
          case '2':
            this.editForm.BZ041 = '/Off';
            this.editForm.BZ042 = '/Yes';
            break;
        }
      }
    },
    BZ043044: {
      handler() {
        switch (this.BZ043044) {
          case '1':
            this.editForm.BZ043 = '/Yes';
            this.editForm.BZ044 = '/Off';
            break;
          case '2':
            this.editForm.BZ043 = '/Off';
            this.editForm.BZ044 = '/Yes';
            break;
        }
      }
    },
    BZ045046047: {
      handler() {
        switch (this.BZ045046047) {
          case '1':
            this.editForm.BZ045 = '/Yes';
            this.editForm.BZ046 = '/Off';
            this.editForm.BZ047 = '/Off';
            break;
          case '2':
            this.editForm.BZ045 = '/Off';
            this.editForm.BZ046 = '/Yes';
            this.editForm.BZ047 = '/Off';
            break;
          case '3':
            this.editForm.BZ045 = '/Off';
            this.editForm.BZ046 = '/Off';
            this.editForm.BZ047 = '/Yes';
            break;
        }
      }
    },
    BZ051052: {
      handler() {
        switch (this.BZ051052) {
          case '1':
            this.editForm.BZ051 = '/Yes';
            this.editForm.BZ052 = '/Off';
            break;
          case '2':
            this.editForm.BZ051 = '/Off';
            this.editForm.BZ052 = '/Yes';
            break;
        }
      }
    },
    BZ055056057: {
      handler() {
        switch (this.BZ055056057) {
          case '1':
            this.editForm.BZ055 = '/Yes';
            this.editForm.BZ056 = '/Off';
            this.editForm.BZ057 = '/Off';
            break;
          case '2':
            this.editForm.BZ055 = '/Off';
            this.editForm.BZ056 = '/Yes';
            this.editForm.BZ057 = '/Off';
            break;
          case '3':
            this.editForm.BZ055 = '/Off';
            this.editForm.BZ056 = '/Off';
            this.editForm.BZ057 = '/Yes';
            break;
        }
      }
    },
    BZ064065: {
      handler() {
        switch (this.BZ064065) {
          case '1':
            this.editForm.BZ064 = '/Yes';
            this.editForm.BZ065 = '/Off';
            break;
          case '2':
            this.editForm.BZ064 = '/Off';
            this.editForm.BZ065 = '/Yes';
            break;
        }
      }
    },
    BZ068069: {
      handler() {
        switch (this.BZ068069) {
          case '1':
            this.editForm.BZ068 = '/Yes';
            this.editForm.BZ069 = '/Off';
            break;
          case '2':
            this.editForm.BZ068 = '/Off';
            this.editForm.BZ069 = '/Yes';
            break;
        }
      }
    },
    BZ070071: {
      handler() {
        switch (this.BZ070071) {
          case '1':
            this.editForm.BZ070 = '/Yes';
            this.editForm.BZ071 = '/Off';
            break;
          case '2':
            this.editForm.BZ070 = '/Off';
            this.editForm.BZ071 = '/Yes';
            break;
        }
      }
    },
    BZ072073: {
      handler() {
        switch (this.BZ072073) {
          case '1':
            this.editForm.BZ072 = '/Yes';
            this.editForm.BZ073 = '/Off';
            break;
          case '2':
            this.editForm.BZ072 = '/Off';
            this.editForm.BZ073 = '/Yes';
            break;
        }
      }
    },
    BZ074075: {
      handler() {
        switch (this.BZ074075) {
          case '1':
            this.editForm.BZ074 = '/Yes';
            this.editForm.BZ075 = '/Off';
            break;
          case '2':
            this.editForm.BZ074 = '/Off';
            this.editForm.BZ075 = '/Yes';
            break;
        }
      }
    },
    BZ078079: {
      handler() {
        switch (this.BZ078079) {
          case '1':
            this.editForm.BZ078 = '/Yes';
            this.editForm.BZ079 = '/Off';
            break;
          case '2':
            this.editForm.BZ078 = '/Off';
            this.editForm.BZ079 = '/Yes';
            break;
        }
      }
    },
    BZ080081: {
      handler() {
        switch (this.BZ080081) {
          case '1':
            this.editForm.BZ080 = '/Yes';
            this.editForm.BZ081 = '/Off';
            break;
          case '2':
            this.editForm.BZ080 = '/Off';
            this.editForm.BZ081 = '/Yes';
            break;
        }
      }
    },
    BZ082083: {
      handler() {
        switch (this.BZ082083) {
          case '1':
            this.editForm.BZ082 = '/Yes';
            this.editForm.BZ083 = '/Off';
            break;
          case '2':
            this.editForm.BZ082 = '/Off';
            this.editForm.BZ083 = '/Yes';
            break;
        }
      }
    },
    BZ086087: {
      handler() {
        switch (this.BZ086087) {
          case '1':
            this.editForm.BZ086 = '/Yes';
            this.editForm.BZ087 = '/Off';
            break;
          case '2':
            this.editForm.BZ086 = '/Off';
            this.editForm.BZ087 = '/Yes';
            break;
        }
      }
    },
    BZ091092: {
      handler() {
        switch (this.BZ091092) {
          case '1':
            this.editForm.BZ091 = '/Yes';
            this.editForm.BZ092 = '/Off';
            break;
          case '2':
            this.editForm.BZ091 = '/Off';
            this.editForm.BZ092 = '/Yes';
            break;
        }
      }
    },
    BZ096097: {
      handler() {
        switch (this.BZ096097) {
          case '1':
            this.editForm.BZ096 = '/Yes';
            this.editForm.BZ097 = '/Off';
            break;
          case '2':
            this.editForm.BZ096 = '/Off';
            this.editForm.BZ097 = '/Yes';
            break;
        }
      }
    },
    BZ100101: {
      handler() {
        switch (this.BZ100101) {
          case '1':
            this.editForm.BZ100 = '/Yes';
            this.editForm.BZ101 = '/Off';
            break;
          case '2':
            this.editForm.BZ100 = '/Off';
            this.editForm.BZ101 = '/Yes';
            break;
        }
      }
    },
    BZ102103: {
      handler() {
        switch (this.BZ102103) {
          case '1':
            this.editForm.BZ102 = '/Yes';
            this.editForm.BZ103 = '/Off';
            break;
          case '2':
            this.editForm.BZ102 = '/Off';
            this.editForm.BZ103 = '/Yes';
            break;
        }
      }
    },
    LNAddSameCom: {
      handler(a) {
        if (a) {
          this.editForm.LNAddSame = '/Yes';
        } else {
          this.editForm.LNAddSame = '/Off';
        }
      }
    },
    LNAGAddSameCom: {
      handler(a) {
        if (a) {
          this.editForm.LNAGAddSame = '/Yes';
        } else {
          this.editForm.LNAGAddSame = '/Off';
        }
      }
    }
  },
  methods: {
    cancel() {
      this.$emit('cancel');
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
              case 'LNName':
                msg += '*姓名<br/>';
                break;
              case 'LNID':
                msg += '*身分證字號<br/>';
                break;
              case 'LNBirthday':
                msg += '*出生年月日<br/>';
                break;
              case 'LNCell':
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
      this.editForm.LCanModify = !this.editForm.disabled;
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

    geteditaddress(addressData, title) {
      if (title === '戶籍地址') {
        this.editForm.LNAdd_1 = addressData.Add_1;
        this.editForm.LNAdd_1_1 = addressData.Add_1_1;
        this.editForm.LNAdd_1_2 = addressData.Add_1_2;
        this.editForm.LNAdd_2 = addressData.Add_2;
        this.editForm.LNAdd_2_1 = addressData.Add_2_1;
        this.editForm.LNAdd_2_2 = addressData.Add_2_2;
        this.editForm.LNAdd_2_3 = addressData.Add_2_3;
        this.editForm.LNAdd_2_4 = addressData.Add_2_4;
        this.editForm.LNAdd_3 = addressData.Add_3;
        this.editForm.LNAdd_3_1 = addressData.Add_3_1;
        this.editForm.LNAdd_3_2 = addressData.Add_3_2;
        this.editForm.LNAdd_4 = addressData.Add_4;
        this.editForm.LNAdd_5 = addressData.Add_5;
        this.editForm.LNAdd_6 = addressData.Add_6;
        this.editForm.LNAdd_7 = addressData.Add_7;
        this.editForm.LNAdd_8 = addressData.Add_8;
        this.editForm.LNAdd_9 = addressData.Add_9;
      }
      if (title === '通訊地址') {
        this.editForm.LNAddC_1 = addressData.Add_1;
        this.editForm.LNAddC_1_1 = addressData.Add_1_1;
        this.editForm.LNAddC_1_2 = addressData.Add_1_2;
        this.editForm.LNAddC_2 = addressData.Add_2;
        this.editForm.LNAddC_2_1 = addressData.Add_2_1;
        this.editForm.LNAddC_2_2 = addressData.Add_2_2;
        this.editForm.LNAddC_2_3 = addressData.Add_2_3;
        this.editForm.LNAddC_2_4 = addressData.Add_2_4;
        this.editForm.LNAddC_3 = addressData.Add_3;
        this.editForm.LNAddC_3_1 = addressData.Add_3_1;
        this.editForm.LNAddC_3_2 = addressData.Add_3_2;
        this.editForm.LNAddC_4 = addressData.Add_4;
        this.editForm.LNAddC_5 = addressData.Add_5;
        this.editForm.LNAddC_6 = addressData.Add_6;
        this.editForm.LNAddC_7 = addressData.Add_7;
        this.editForm.LNAddC_8 = addressData.Add_8;
        this.editForm.LNAddC_9 = addressData.Add_9;
      }
      if (title === '代理人戶籍地址') {
        this.editForm.LNAGAdd_1_A = addressData.Add_1;
        this.editForm.LNAGAdd_1_1_A = addressData.Add_1_1;
        this.editForm.LNAGAdd_1_2_A = addressData.Add_1_2;
        this.editForm.LNAGAdd_2_A = addressData.Add_2;
        this.editForm.LNAGAdd_2_1_A = addressData.Add_2_1;
        this.editForm.LNAGAdd_2_2_A = addressData.Add_2_2;
        this.editForm.LNAGAdd_2_3_A = addressData.Add_2_3;
        this.editForm.LNAGAdd_2_4_A = addressData.Add_2_4;
        this.editForm.LNAGAdd_3_A = addressData.Add_3;
        this.editForm.LNAGAdd_3_1_A = addressData.Add_3_1;
        this.editForm.LNAGAdd_3_2_A = addressData.Add_3_2;
        this.editForm.LNAGAdd_4_A = addressData.Add_4;
        this.editForm.LNAGAdd_5_A = addressData.Add_5;
        this.editForm.LNAGAdd_6_A = addressData.Add_6;
        this.editForm.LNAGAdd_7_A = addressData.Add_7;
        this.editForm.LNAGAdd_8_A = addressData.Add_8;
        this.editForm.LNAGAdd_9_A = addressData.Add_9;
      }
      if (title === '代理人通訊地址') {
        this.editForm.LNAGAddC_1_A = addressData.Add_1;
        this.editForm.LNAGAddC_1_1_A = addressData.Add_1_1;
        this.editForm.LNAGAddC_1_2_A = addressData.Add_1_2;
        this.editForm.LNAGAddC_2_A = addressData.Add_2;
        this.editForm.LNAGAddC_2_1_A = addressData.Add_2_1;
        this.editForm.LNAGAddC_2_2_A = addressData.Add_2_2;
        this.editForm.LNAGAddC_2_3_A = addressData.Add_2_3;
        this.editForm.LNAGAddC_2_4_A = addressData.Add_2_4;
        this.editForm.LNAGAddC_3_A = addressData.Add_3;
        this.editForm.LNAGAddC_3_1_A = addressData.Add_3_1;
        this.editForm.LNAGAddC_3_2_A = addressData.Add_3_2;
        this.editForm.LNAGAddC_4_A = addressData.Add_4;
        this.editForm.LNAGAddC_5_A = addressData.Add_5;
        this.editForm.LNAGAddC_6_A = addressData.Add_6;
        this.editForm.LNAGAddC_7_A = addressData.Add_7;
        this.editForm.LNAGAddC_8_A = addressData.Add_8;
        this.editForm.LNAGAddC_9_A = addressData.Add_9;
      }
      if (title === '建物地址') {
        this.editForm.BAdd_1 = addressData.Add_1;
        this.editForm.BAdd_1_1 = addressData.Add_1_1;
        this.editForm.BAdd_1_2 = addressData.Add_1_2;
        this.editForm.BAdd_2 = addressData.Add_2;
        this.editForm.BAdd_2_1 = addressData.Add_2_1;
        this.editForm.BAdd_2_2 = addressData.Add_2_2;
        this.editForm.BAdd_2_3 = addressData.Add_2_3;
        this.editForm.BAdd_2_4 = addressData.Add_2_4;
        this.editForm.BAdd_3 = addressData.Add_3;
        this.editForm.BAdd_3_1 = addressData.Add_3_1;
        this.editForm.BAdd_3_2 = addressData.Add_3_2;
        this.editForm.BAdd_4 = addressData.Add_4;
        this.editForm.BAdd_5 = addressData.Add_5;
        this.editForm.BAdd_6 = addressData.Add_6;
        this.editForm.BAdd_7 = addressData.Add_7;
        this.editForm.BAdd_8 = addressData.Add_8;
        this.editForm.BAdd_9 = addressData.Add_9;
      }
    },
    GetRemittanceByEmit(remittance) {
      this.editForm.LAName = remittance.LAName;
      this.editForm.LANo = remittance.LANo;
      this.editForm.LBankName = remittance.LBankName;
      this.editForm.LBankNo = remittance.LBankNo;
      this.editForm.LBranchName = remittance.LBranchName;
      this.editForm.LBranchNo = remittance.LBranchNo;
    },
    SaveAndUploadAndDownoadPDF() {
      this.$refs['editForm'].validate((valid, err) => {
        if (valid) {
          this.$emit('openpageloading');
          this.editForm.LCanModify = !this.editForm.disabled;
          this.editForm.IsGenPDF = 'Y';
          UploadWithData(this.editForm.FormId, this.editForm).then(res => {
            if (res.Success) {
              // this.$message.success('儲存成功!');
              var w = this.$refs.editForm.$el.clientWidth;
              var h = this.$refs.editForm.$el.clientHeight;
              var fileName = res.ResData;
              const doc = new JsPDF('p', 'pt', [h, w]);
              var test = this.$refs.editForm.$el;
              test.forEach(el => {
                el.removeAttribute('placeholder'); // 把placeholder拿掉
              });
              html2canvas(this.$refs.editForm.$el, { scale: 1.25 }).then(
                canvas => {
                  const img = canvas.toDataURL('image/jpeg');
                  doc.addImage(img, 'JPEG', 10, 10);
                  const blob = doc.output('blob');
                  var file = new File([blob], '自然人房東資料卡.pdf', {
                    type: 'pdf'
                  });
                  const formData = new FormData();
                  formData.append('File', file);
                  formData.append('id', fileName);
                  formData.append('role', 'Building');
                  formData.append('formname', this.editForm.FormName);
                  formData.append('BAdd', this.editForm.BAdd);
                  ImgUpload(formData)
                    .then(res => {
                      if (res.Success) {
                        this.$message.success('上傳並儲存成功');
                        const link = document.createElement('a');
                        link.href = URL.createObjectURL(blob);
                        link.download = '自然人房東資料卡.pdf';
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
                }
              );
            } else {
              this.$message.error('失敗');
            }
          });
        } else {
          var msg = '';
          var errArr = Object.keys(err);
          errArr.forEach(x => {
            switch (x) {
              case 'LNName':
                msg += '*姓名<br/>';
                break;
              case 'LNID':
                msg += '*身分證字號<br/>';
                break;
              case 'LNBirthday':
                msg += '*出生年月日<br/>';
                break;
              case 'LNCell':
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

<style>
</style>
