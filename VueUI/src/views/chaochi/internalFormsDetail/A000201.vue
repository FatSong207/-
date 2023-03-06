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
              <el-col>
                <el-form-item label="委託人：">
                  <el-input
                    v-model="editForm.LSName"
                    placeholder="請輸入姓名"
                    autocomplete="off"
                    clearable
                    disabled
                  />
                </el-form-item>
                <el-form-item label="(以下簡稱甲方)" />
              </el-col>
              <el-col>
                <el-form-item label="受託人：">
                  <el-input
                    v-model="editForm.ROName"
                    autocomplete="off"
                    clearable
                    disabled
                    style="width: 320px"
                  />
                </el-form-item>
                <el-form-item label="(以下簡稱乙方)" />
              </el-col>
            </el-row>
            <div class="text item">
              <span>雙方秉持誠信原則同意簽定本契約，授權乙方辦理委託仲介租賃：</span>
            </div>
            <div class="text item">
              <span>1. 乙方得於民國
                <DatePickerE
                  v-model="editForm.BZ035"
                  value-format="yyyy-MM-dd"
                  format="yyyy-MM-dd"
                  type="date"
                  placeholder="選擇日期"
                  style="width: 140px"
                  size="mini"
                />至民國
                <DatePickerE
                  v-model="editForm.BZ038"
                  value-format="yyyy-MM-dd"
                  format="yyyy-MM-dd"
                  type="date"
                  placeholder="選擇日期"
                  style="width: 140px"
                  size="mini"
                />止為下列房屋為居間仲介行為（包含收取返還定金、拍照、刊登廣告等；房屋資訊與委託出租條件內容以謄本及下列現況為主）。
              </span>
            </div>
            <el-card>
              <div class="text item">
                <span>地址：<b>{{ editForm.BAdd }}</b>。社區名稱：
                  <el-input
                    v-model="editForm.BZ041"
                    autocomplete="off"
                    clearable
                    class="w120px"
                    size="mini"
                  />。
                </span>
              </div>
              <div class="text item">
                <span>面積：
                  <el-checkbox
                    v-model="editForm.BZ042"
                    true-label="/Yes"
                    false-label="/Off"
                  >依權狀/騰本登記
                  </el-checkbox>
                  <el-checkbox
                    v-model="editForm.BZ043"
                    true-label="/Yes"
                    false-label="/Off"
                  >現況坪數
                    <el-input
                      v-model="editForm.BPing"
                      autocomplete="off"
                      clearable
                      size="mini"
                      class="w80px"
                    />坪
                  </el-checkbox>；
                  <el-input
                    v-model="editForm.BRoom"
                    autocomplete="off"
                    clearable
                    size="mini"
                    class="w60px"
                  />房
                  <el-input
                    v-model="editForm.BLD"
                    autocomplete="off"
                    clearable
                    size="mini"
                    class="w60px"
                  />廳
                  <el-input
                    v-model="editForm.BBath"
                    autocomplete="off"
                    clearable
                    size="mini"
                    class="w60px"
                  />衛浴
                  <el-input
                    v-model="editForm.BKitchen"
                    autocomplete="off"
                    clearable
                    size="mini"
                    class="w60px"
                  />廚房
                  <el-input
                    v-model="editForm.BBalcony"
                    autocomplete="off"
                    clearable
                    size="mini"
                    class="w80px"
                  />陽台。
                </span>
              </div>
              <div class="text item">
                <span>建物：
                  <el-checkbox
                    v-model="editForm.BTApartment"
                    true-label="/Yes"
                    false-label="/Off"
                  >公寓
                  </el-checkbox>
                  <el-checkbox
                    v-model="editForm.BTElevator"
                    true-label="/Yes"
                    false-label="/Off"
                  >電梯大樓
                  </el-checkbox>
                  <el-checkbox
                    v-model="editForm.BTHouse"
                    true-label="/Yes"
                    false-label="/Off"
                  >透天
                  </el-checkbox>；
                  <el-radio
                    v-model="BElevator"
                    label="1"
                  >有</el-radio>
                  <el-radio
                    v-model="BElevator"
                    label="2"
                  >無</el-radio>電梯；
                  <el-radio
                    v-model="BSuper"
                    label="1"
                  >有</el-radio>
                  <el-radio
                    v-model="BSuper"
                    label="2"
                  >無</el-radio>管理員；
                  一層
                  <el-input
                    v-model="editForm.BZ044"
                    autocomplete="off"
                    clearable
                    class="w80px"
                    size="mini"
                  />戶；座向
                  <el-input
                    v-model="editForm.BZ045"
                    autocomplete="off"
                    clearable
                    class="w100px"
                    size="mini"
                  />
                </span>
              </div>
              <div class="text item">
                <span>車位：編號
                  <el-input
                    v-model="editForm.BCarNo"
                    autocomplete="off"
                    clearable
                    class="w120px"
                    size="mini"
                  />號，第
                  <el-input
                    v-model="editForm.BCarFloor"
                    autocomplete="off"
                    clearable
                    class="w80px"
                    size="mini"
                  />層，地上(
                  <el-checkbox
                    v-model="editForm.BZ046"
                    true-label="/Yes"
                    false-label="/Off"
                  >面
                  </el-checkbox>、
                  <el-checkbox
                    v-model="editForm.BZ047"
                    true-label="/Yes"
                    false-label="/Off"
                  >下
                  </el-checkbox>)
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
                  </el-checkbox>車位。
                </span>
              </div>
              <el-divider />
              <div class="text item">
                <span>月租金：新台幣
                  <el-input
                    v-model="editForm.BTCRent"
                    autocomplete="off"
                    clearable
                    class="w100px"
                    size="mini"
                  />元整。 租押金：新台幣
                  <el-input
                    v-model="editForm.Bdeposit"
                    autocomplete="off"
                    clearable
                    class="w100px"
                    size="mini"
                  />元整。
                </span>
              </div>
              <div class="text item">
                <span>租金包含：
                  <el-checkbox
                    v-model="editForm.BZ048"
                    true-label="/Yes"
                    false-label="/Off"
                  >管理費
                  </el-checkbox>
                  <el-checkbox
                    v-model="editForm.BZ049"
                    true-label="/Yes"
                    false-label="/Off"
                  >清潔費
                  </el-checkbox>
                  <el-checkbox
                    v-model="editForm.BZ050"
                    true-label="/Yes"
                    false-label="/Off"
                  >第四台
                  </el-checkbox>
                  <el-checkbox
                    v-model="editForm.BZ051"
                    true-label="/Yes"
                    false-label="/Off"
                  >網路
                  </el-checkbox>
                  <el-checkbox
                    v-model="editForm.BZ052"
                    true-label="/Yes"
                    false-label="/Off"
                  >水費
                  </el-checkbox>
                  <el-checkbox
                    v-model="editForm.BZ053"
                    true-label="/Yes"
                    false-label="/Off"
                  >電費
                  </el-checkbox>
                  <el-checkbox
                    v-model="editForm.BZ054"
                    true-label="/Yes"
                    false-label="/Off"
                  >瓦斯費
                  </el-checkbox>
                </span>
              </div>
              <div class="text item">
                <span>車位租金：新台幣
                  <el-input
                    v-model="editForm.BParkFee"
                    autocomplete="off"
                    clearable
                    class="w100px"
                    size="mini"
                  />元整。 車位清潔費：新台幣
                  <el-input
                    v-model="editForm.BZ055"
                    autocomplete="off"
                    clearable
                    class="w100px"
                    size="mini"
                  />元整。
                </span>
              </div>
              <div class="text item">
                <span>大樓管理費：新台幣
                  <el-input
                    v-model="editForm.BMFee"
                    autocomplete="off"
                    clearable
                    class="w100px"
                    size="mini"
                  />元整。 其他費用：
                  <el-input
                    v-model="editForm.BZ001"
                    autocomplete="off"
                    clearable
                    class="w100px"
                    size="mini"
                  />
                </span>
              </div>
              <el-divider />
              <div class="text item">
                <span>附屬設備：
                  <el-checkbox
                    v-model="editForm.BZ002"
                    true-label="/Yes"
                    false-label="/Off"
                  >洗衣機
                  </el-checkbox>
                  <el-checkbox
                    v-model="editForm.BZ009"
                    true-label="/Yes"
                    false-label="/Off"
                  >熱水器(
                    <el-checkbox
                      v-model="editForm.BZ010"
                      true-label="/Yes"
                      false-label="/Off"
                    >瓦斯
                    </el-checkbox>
                    <el-checkbox
                      v-model="editForm.BZ011"
                      true-label="/Yes"
                      false-label="/Off"
                    >電
                    </el-checkbox>
                    )
                  </el-checkbox>
                  <el-checkbox
                    v-model="editForm.BZ012"
                    true-label="/Yes"
                    false-label="/Off"
                  >網路
                  </el-checkbox>
                  <el-checkbox
                    v-model="editForm.BZ013"
                    true-label="/Yes"
                    false-label="/Off"
                  >第四台
                  </el-checkbox>
                  <el-checkbox
                    v-model="editForm.BZ014"
                    true-label="/Yes"
                    false-label="/Off"
                  >天然瓦斯
                  </el-checkbox>
                  <el-checkbox
                    v-model="editForm.BZ015"
                    true-label="/Yes"
                    false-label="/Off"
                  >流理臺
                  </el-checkbox>
                  <el-checkbox
                    v-model="editForm.BZ016"
                    true-label="/Yes"
                    false-label="/Off"
                  >瓦斯爐
                  </el-checkbox>
                  <el-checkbox
                    v-model="editForm.BZ023"
                    true-label="/Yes"
                    false-label="/Off"
                  >桌子
                  </el-checkbox>
                  <el-checkbox
                    v-model="editForm.BZ024"
                    true-label="/Yes"
                    false-label="/Off"
                  >椅子
                  </el-checkbox>
                </span>
              </div>
              <div class="text item">
                <span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  <el-checkbox
                    v-model="editForm.BZ003"
                    true-label="/Yes"
                    false-label="/Off"
                  >冰箱
                    <el-input
                      v-model="editForm.BZ004"
                      autocomplete="off"
                      clearable
                      class="w60px"
                      size="mini"
                    />台
                  </el-checkbox>
                  <el-checkbox
                    v-model="editForm.BZ005"
                    true-label="/Yes"
                    false-label="/Off"
                  >電視
                    <el-input
                      v-model="editForm.BZ006"
                      autocomplete="off"
                      clearable
                      class="w60px"
                      size="mini"
                    />台
                  </el-checkbox>
                  <el-checkbox
                    v-model="editForm.BZ007"
                    true-label="/Yes"
                    false-label="/Off"
                  >冷氣
                    <el-input
                      v-model="editForm.BZ008"
                      autocomplete="off"
                      clearable
                      class="w60px"
                      size="mini"
                    />台
                  </el-checkbox>
                  <el-checkbox
                    v-model="editForm.BZ017"
                    true-label="/Yes"
                    false-label="/Off"
                  >床組
                    <el-input
                      v-model="editForm.BZ018"
                      autocomplete="off"
                      clearable
                      class="w60px"
                      size="mini"
                    />組
                  </el-checkbox>
                  <el-checkbox
                    v-model="editForm.BZ019"
                    true-label="/Yes"
                    false-label="/Off"
                  >衣櫃
                    <el-input
                      v-model="editForm.BZ020"
                      autocomplete="off"
                      clearable
                      class="w60px"
                      size="mini"
                    />組
                  </el-checkbox>
                  <el-checkbox
                    v-model="editForm.BZ021"
                    true-label="/Yes"
                    false-label="/Off"
                  >沙發
                    <el-input
                      v-model="editForm.BZ022"
                      autocomplete="off"
                      clearable
                      class="w60px"
                      size="mini"
                    />組
                  </el-checkbox>
                </span>
              </div>
              <el-divider />
              <div class="text item">
                <span>使用需求：最短租期
                  <el-input
                    v-model="editForm.BZ056"
                    autocomplete="off"
                    clearable
                    class="w60px"
                    size="mini"
                  />年；開伙
                  <el-radio
                    v-model="BCook"
                    label="1"
                  >可</el-radio>
                  <el-radio
                    v-model="BCook"
                    label="2"
                  >不可</el-radio>；寵物
                  <el-radio
                    v-model="BPet"
                    label="1"
                  >可</el-radio>
                  <el-radio
                    v-model="BPet"
                    label="2"
                  >不可</el-radio>；神明桌
                  <el-radio
                    v-model="BGodT"
                    label="1"
                  >可</el-radio>
                  <el-radio
                    v-model="BGodT"
                    label="2"
                  >不可</el-radio>。
                </span>
              </div>
            </el-card>
            <div class="text item">
              <span>2. 委託人為代理人時，應出具本租賃物所有權人之授權書正本。
              </span>
            </div>
            <div class="text item">
              <span>3. 甲方應交付本房屋之權狀影本、使用執照影本予乙方，如有住戶規約等，一併提供其影本；甲方如係將房屋轉租時，應提示原出租人同意書及原租賃契約書（已載明有轉租約定者為限）。
              </span>
            </div>
            <div class="text item">
              <span>4. 甲方應保證對委託之房屋有出租權利，且產權清楚並可履行房屋租賃契約，就房屋之重要事項 (例如:規約、慣例使用現況等)及一切足以影響承租人承租意願之情形(例如:滲漏水、凶宅…等)，對乙方負有誠實告知之義務，如有不實，則甲方須自負法律責任，同時應賠償乙方因此所遭致一切損害。
              </span>
            </div>
            <div class="text item">
              <span>5. 甲方同意乙方得以（
                <el-checkbox
                  v-model="editForm.BOpen"
                  true-label="/Yes"
                  false-label="/Off"
                >親自開門
                </el-checkbox>
                <el-checkbox
                  v-model="editForm.BZ057"
                  true-label="/Yes"
                  false-label="/Off"
                >鑰匙留置管理室
                </el-checkbox>
                <el-checkbox
                  v-model="editForm.BZ058"
                  true-label="/Yes"
                  false-label="/Off"
                >給付乙方鑰匙/磁扣
                  <el-input
                    v-model="editForm.BZ059"
                    autocomplete="off"
                    clearable
                    class="w60px"
                    size="mini"
                  />支
                </el-checkbox>）之方式，安排有意承租之人進入本房屋參觀，並同意乙方得於本房屋張貼出租廣告。
              </span>
            </div>
            <div class="text item">
              <span>6. 甲方
                <el-radio
                  v-model="BZ025026"
                  label="1"
                >同意</el-radio>
                <el-radio
                  v-model="BZ025026"
                  label="2"
                >不同意</el-radio>乙方代為收取定金並於簽訂房屋租賃契約時，將代為收取之定金轉為
                <el-checkbox
                  v-model="editForm.BZ027"
                  true-label="/Yes"
                  false-label="/Off"
                >仲介費
                </el-checkbox>
                <el-checkbox
                  v-model="editForm.BZ028"
                  true-label="/Yes"
                  false-label="/Off"
                >租押金
                </el-checkbox>；定金超過之部分，乙方得以
                <el-checkbox
                  v-model="editForm.BZ029"
                  true-label="/Yes"
                  false-label="/Off"
                >現金
                </el-checkbox>
                <el-checkbox
                  v-model="editForm.BZ030"
                  true-label="/Yes"
                  false-label="/Off"
                >匯款
                </el-checkbox>之方式將超過部分交付甲方。
              </span>
            </div>
            <div class="text item">
              <span>7. 甲方
                <el-radio
                  v-model="BZ031032"
                  label="1"
                >同意</el-radio>
                <el-radio
                  v-model="BZ031032"
                  label="2"
                >不同意</el-radio>乙方得代為與第三人簽訂房屋租賃契約，甲方應提供授權書。
              </span>
            </div>
            <div class="text item">
              <span>8. 甲方應於乙方居間仲介完成，簽訂房屋租賃契約時，支付報酬如下：
                <div class="text item">&nbsp;&nbsp;&nbsp;(1)簽立租賃期間一年之房屋租賃契約，應支付相當於
                  <el-input
                    v-model="editForm.BZ033"
                    autocomplete="off"
                    clearable
                    class="w80px"
                    size="mini"
                  />個月租金額為仲介費。
                </div>
                <div class="text item">&nbsp;&nbsp;&nbsp;(2)簽立租賃期間兩年(含)以上之房屋租賃契約，應支付相當於
                  <el-input
                    v-model="editForm.BZ034"
                    autocomplete="off"
                    clearable
                    class="w80px"
                    size="mini"
                  />個月租金額為仲介費。
                </div>
              </span>
            </div>
            <div class="text item">
              <span>9. 本契約為一般委託租賃契約書，甲方尚未與乙方所仲介之第三人簽訂房屋租賃契約書前，可隨時租售予他人，惟乙方得向甲方請求因居間仲介行為所支出之費用，費用不得超過約定報酬。
              </span>
            </div>
            <div class="text item">
              <span>10.每一加盟店皆為獨立經營之不動產經紀業、租賃住宅服務業；加盟店與加盟總部為不同法人，財務與法律責任各自獨立。
              </span>
            </div>
            <div class="text item">
              <span>11.因本契約所生之法律糾紛，雙方同意以臺灣臺北地方法院為管轄法院。
              </span>
            </div>
          </el-card>
        </el-col>
      </el-row>
      <el-row>
        <el-col>
          <h3>委託人</h3>
          <el-card>
            <el-row>
              <el-col>
                <el-form-item
                  label="姓名："
                  class="nameformlabel"
                >
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
                  <!-- <el-input
                    v-model="editForm.LSName"
                    placeholder="請輸入姓名"
                    autocomplete="off"
                    clearable
                    disabled
                  /> -->
                </el-form-item>
              </el-col>
              <el-col>
                <el-form-item label="身分證字號：">
                  <el-badge
                    value="(統一編號)"
                    type="info"
                  >{{ editForm.LSID }}
                    <!-- <el-input
                      v-model="editForm.LSID"
                      autocomplete="off"
                      clearable
                      disabled
                    /> -->
                  </el-badge>
                </el-form-item>
              </el-col>
              <el-col>
                <el-form-item
                  label="電話："
                  prop="LNCell"
                >
                  <el-input
                    v-model="editForm.LNCell"
                    placeholder="請輸入電話"
                    clearable
                    style="width: 220px"
                    class="input-with-select"
                    onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                    maxlength="10"
                  >
                    <!-- <el-select
                      slot="prepend"
                      v-model="editForm.LSTel_1"
                      placeholder="區號"
                      style="width: 80px"
                    >
                      <el-option
                        v-for="item in zoneOptions"
                        :key="item.value"
                        :label="item.label"
                        :value="item.value"
                      />
                    </el-select> -->
                  </el-input>
                </el-form-item>
              </el-col>
              <el-col>
                <el-form-item label="地址：">
                  <Address
                    :sendedform="sendedform"
                    title="戶籍地址"
                    @geteditaddress="geteditaddress"
                  />
                  <!-- <el-input
                    v-model="editForm.LSAdd"
                    autocomplete="off"
                    clearable
                    disabled
                    style="width: 320px"
                  /> -->
                </el-form-item>
              </el-col>
            </el-row>
            <div
              class="text item"
              style="font-size: 8px;"
            >
              <span>契約審閱權，本房屋委託租賃契約書已由委託人攜回審閱至少三日，並充分瞭解本契約內容。</span>
            </div>
          </el-card>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="12">
          <h3>受託人</h3>
          <el-card>
            <el-row>
              <el-col :span="12">
                <el-form-item label="公司名稱：">
                  {{ editForm.ROName }}
                  <!-- <el-input
                    v-model="editForm.ROName"
                    autocomplete="off"
                    clearable
                    disabled
                  /> -->
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item label="電話：">
                  {{ editForm.ROTel }}
                  <!-- <el-input
                    v-model="editForm.ROTel"
                    autocomplete="off"
                    clearable
                    disabled
                  /> -->
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col>
                <el-form-item label="地址：">
                  {{ editForm.ROAdd }}
                  <!-- <el-input
                    v-model="editForm.ROAdd"
                    autocomplete="off"
                    clearable
                    disabled
                    style="width: 320px"
                  /> -->
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="12">
                <el-form-item label="不動產經紀人：">
                  {{ editForm.AGName }}
                  <!-- <el-input
                    v-model="editForm.AGName"
                    autocomplete="off"
                    clearable
                    disabled
                  /> -->
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item label="證號：">
                  {{ editForm.AGLRNo }}
                  <!-- <el-input
                    v-model="editForm.AGLRNo"
                    autocomplete="off"
                    clearable
                    disabled
                  /> -->
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="12">
                <el-form-item label="開發人員：">
                  {{ editForm.ROUserName }}
                  <!-- <el-input
                    v-model="editForm.ROUserName"
                    autocomplete="off"
                    clearable
                    disabled
                  /> -->
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item label="連絡電話：">
                  <el-input
                    v-model="editForm.BZ060"
                    autocomplete="off"
                    clearable
                  />
                </el-form-item>
              </el-col>
            </el-row>
          </el-card>
        </el-col>
      </el-row>
    </el-form>
    <div style="margin-top: 20px; text-align: center">
      <el-button
        size="small"
        icon="el-icon-paperclip"
        type="primary"
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
      <el-button
        v-if="false"
        type="danger"
        size="small"
        icon="el-icon-download"
        @click="downloadLatestA0000801PDF()"
      >下載此建物最新設備表單(測試階段)</el-button>
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
import {
  UploadWithData,
  downloadPDFWithDataByFormId,
  ImgUpload
} from '@/api/chaochi/internalform/internalform';
import {
  CheckBuildingHasA0000801PDF,
  downloadLatestA0000801PDF
} from '@/api/chaochi/historyform/historyformservice';
import { getImg } from '@/api/chaochi/externalform/externalformservice';
import { validateCellReg } from '@/utils/validate';
import DatePickerE from '@/components/RocDatepickerE';
import Address from '@/components/Address/Address.vue';
// import JsPDF from 'jspdf';
// import html2canvas from 'html2canvas';
export default {
  name: 'A000201',
  components: { DatePickerE, Address },
  props: {
    editform: { type: Object, default: null }
  },
  data() {
    return {
      editForm: { ...this.editform },
      rule: {
        LNCell: [
          { required: false, trigger: 'change', validator: validateCellReg }
        ]
      },
      sendedform: {},
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
      src_1: '',
      BElevator: '',
      BSuper: '',
      BCook: '',
      BPet: '',
      BGodT: '',
      BZ025026: '',
      BZ031032: '',
      centerDialogVisible: false
    };
  },
  watch: {
    editform: {
      immediate: true,
      handler() {
        if (Object.keys(this.editform).length !== 0) {
          this.$emit('openpageloading');
          this.editForm = this.editform;
          this.sendedform = {
            Add_1: this.editForm.LSAdd_1,
            Add_1_1: this.editForm.LSAdd_1_1,
            Add_1_2: this.editForm.LSAdd_1_2,
            Add_2: this.editForm.LSAdd_2,
            Add_2_1: this.editForm.LSAdd_2_1,
            Add_2_2: this.editForm.LSAdd_2_2,
            Add_2_3: this.editForm.LSAdd_2_3,
            Add_2_4: this.editForm.LSAdd_2_4,
            Add_3: this.editForm.LSAdd_3,
            Add_3_1: this.editForm.LSAdd_3_1,
            Add_3_2: this.editForm.LSAdd_3_2,
            Add_4: this.editForm.LSAdd_4,
            Add_5: this.editForm.LSAdd_5,
            Add_6: this.editForm.LSAdd_6,
            Add_7: this.editForm.LSAdd_7,
            Add_8: this.editForm.LSAdd_8,
            Add_9: this.editForm.LSAdd_9
          };
          this.BElevator =
            this.editForm.Belevator_Y === '/Yes'
              ? '1'
              : this.editForm.Belevator_N === '/Yes'
                ? '2'
                : '';
          this.BSuper =
            this.editForm.BSuper_Y === '/Yes'
              ? '1'
              : this.editForm.BSuper_N === '/Yes'
                ? '2'
                : '';
          this.BCook =
            this.editForm.BCook_Y === '/Yes'
              ? '1'
              : this.editForm.BCook_N === '/Yes'
                ? '2'
                : '';
          this.BPet =
            this.editForm.BPet_Y === '/Yes'
              ? '1'
              : this.editForm.BPet_N === '/Yes'
                ? '2'
                : '';
          this.BGodT =
            this.editForm.BGodT_Y === '/Yes'
              ? '1'
              : this.editForm.BGodT_N === '/Yes'
                ? '2'
                : '';
          this.BZ025026 =
            this.editForm.BZ025 === '/Yes'
              ? '1'
              : this.editForm.BZ026 === '/Yes'
                ? '2'
                : '';
          this.BZ031032 =
            this.editForm.BZ031 === '/Yes'
              ? '1'
              : this.editForm.BZ032 === '/Yes'
                ? '2'
                : '';
          this.GetSig();
        }
      }
    },
    BElevator: {
      handler() {
        switch (this.BElevator) {
          case '1':
            this.editForm.Belevator_Y = '/Yes';
            this.editForm.Belevator_N = '/Off';
            break;
          case '2':
            this.editForm.Belevator_Y = '/Off';
            this.editForm.Belevator_N = '/Yes';
        }
      }
    },
    BSuper: {
      handler() {
        switch (this.BSuper) {
          case '1':
            this.editForm.BSuper_Y = '/Yes';
            this.editForm.BSuper_N = '/Off';
            break;
          case '2':
            this.editForm.BSuper_Y = '/Off';
            this.editForm.BSuper_N = '/Yes';
            break;
        }
      }
    },
    BCook: {
      handler() {
        switch (this.BCook) {
          case '1':
            this.editForm.BCook_Y = '/Yes';
            this.editForm.BCook_N = '/Off';
            break;
          case '2':
            this.editForm.BCook_Y = '/Off';
            this.editForm.BCook_N = '/Yes';
            break;
        }
      }
    },
    BPet: {
      handler() {
        switch (this.BPet) {
          case '1':
            this.editForm.BPet_Y = '/Yes';
            this.editForm.BPet_N = '/Off';
            break;
          case '2':
            this.editForm.BPet_Y = '/Off';
            this.editForm.BPet_N = '/Yes';
            break;
        }
      }
    },
    BGodT: {
      handler() {
        switch (this.BGodT) {
          case '1':
            this.editForm.BGodT_Y = '/Yes';
            this.editForm.BGodT_N = '/Off';
            break;
          case '2':
            this.editForm.BGodT_Y = '/Off';
            this.editForm.BGodT_N = '/Yes';
            break;
        }
      }
    },
    BZ025026: {
      handler() {
        switch (this.BZ025026) {
          case '1':
            this.editForm.BZ025 = '/Yes';
            this.editForm.BZ026 = '/Off';
            break;
          case '2':
            this.editForm.BZ025 = '/Off';
            this.editForm.BZ026 = '/Yes';
            break;
        }
      }
    },
    BZ031032: {
      handler() {
        switch (this.BZ031032) {
          case '1':
            this.editForm.BZ031 = '/Yes';
            this.editForm.BZ032 = '/Off';
            break;
          case '2':
            this.editForm.BZ031 = '/Off';
            this.editForm.BZ032 = '/Yes';
            break;
        }
      }
    }
  },
  methods: {
    cancel() {
      this.$emit('cancel');
    },
    upload() {
      this.$refs['editForm'].validate(valid => {
        if (valid) {
          this.UploadWithData();
        } else {
          return false;
        }
      });
    },
    UploadWithData() {
      this.$emit('openpageloading');
      this.editForm.LCanModify = !this.editForm.disabled;
      this.editForm.IsGenPDF = 'N';
      // // 時間額外處理 2022/09/13 決議丟置後端處理
      // if (this.editForm.BZ035) {
      //   this.editForm.BZ036 = this.editForm.BZ035.split('-')[1];
      //   this.editForm.BZ037 = this.editForm.BZ035.split('-')[2];
      //   this.editForm.BZ035 = this.editForm.BZ035.split('-')[0];
      // }
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
    },
    SaveAndUploadAndDownoadPDF() {
      this.$refs['editForm'].validate(valid => {
        if (valid) {
          this.$emit('openpageloading');
          this.editForm.LCanModify = !this.editForm.disabled;
          this.editForm.IsGenPDF = 'Y';
          UploadWithData(this.editForm.FormId, this.editForm).then(res => {
            if (res.Success) {
              const data = {
                FormId: this.editForm.FormId,
                BAdd: this.editForm.BAdd
              };
              if (this.editForm.LSID) {
                switch (this.editForm.LSID.length) {
                  case 10:
                    data.LNID = this.editForm.LSID;
                    break;
                  case 8:
                    data.LCID = this.editForm.LSID;
                    break;
                }
              }
              downloadPDFWithDataByFormId(data)
                .then(res => {
                  const blob = res; // new Blob([response.data], { type: 'image/jpeg' })
                  const link = document.createElement('a');
                  link.href = URL.createObjectURL(blob);
                  link.download =
                    'F-LAW111-13-A(純代租)房屋租賃委託契約書＿一般宅.pdf';
                  link.click();
                  URL.revokeObjectURL(link.href);
                  const formData = new FormData();
                  var file = new File(
                    [blob],
                    'F-LAW111-13-A(純代租)房屋租賃委託契約書＿一般宅' + '.pdf',
                    {
                      type: 'pdf'
                    }
                  );
                  formData.append('File', file);
                  // formData.append('id', fileName);
                  formData.append('role', 'Building');
                  formData.append('formname', this.editForm.FormName);
                  formData.append('BAdd', this.editForm.BAdd);
                  ImgUpload(formData)
                    .then(res => {
                      if (res.Success) {
                        this.$message.success('上傳並儲存成功');
                        this.cancel();
                      } else {
                        this.$message.error('上傳失敗');
                      }
                    })
                    .finally(() => {
                      this.$emit('closepageloading');
                    });
                })
                // .finally(() => {
                //   this.$emit('closepageloading');
                // });
            } else {
              this.$message.error('失敗');
            }
          });
          // .finally(() => {
          //   this.$emit('closepageloading');
          // });
        } else {
          return false;
        }
      });
    },
    downloadLatestA0000801PDF() {
      this.$emit('openpageloading');
      CheckBuildingHasA0000801PDF(this.editForm.BAdd)
        .then(res => {
          if (res.ResData) {
            downloadLatestA0000801PDF(this.editForm.BAdd).then(res => {
              const blob = res; // new Blob([response.data], { type: 'image/jpeg' })
              const link = document.createElement('a');
              link.href = URL.createObjectURL(blob);
              link.download = '建物設備';
              link.click();
              URL.revokeObjectURL(link.href);
            });
          } else {
            this.$message.error(`${this.editForm.BAdd}尚未建立建物設備表單!`);
          }
        })
        .finally(() => {
          this.$emit('closepageloading');
        });
    },
    geteditaddress(addressData, title) {
      if (title === '戶籍地址') {
        this.editForm.LSAdd_1 = addressData.Add_1;
        this.editForm.LSAdd_1_1 = addressData.Add_1_1;
        this.editForm.LSAdd_1_2 = addressData.Add_1_2;
        this.editForm.LSAdd_2 = addressData.Add_2;
        this.editForm.LSAdd_2_1 = addressData.Add_2_1;
        this.editForm.LSAdd_2_2 = addressData.Add_2_2;
        this.editForm.LSAdd_2_3 = addressData.Add_2_3;
        this.editForm.LSAdd_2_4 = addressData.Add_2_4;
        this.editForm.LSAdd_3 = addressData.Add_3;
        this.editForm.LSAdd_3_1 = addressData.Add_3_1;
        this.editForm.LSAdd_3_2 = addressData.Add_3_2;
        this.editForm.LSAdd_4 = addressData.Add_4;
        this.editForm.LSAdd_5 = addressData.Add_5;
        this.editForm.LSAdd_6 = addressData.Add_6;
        this.editForm.LSAdd_7 = addressData.Add_7;
        this.editForm.LSAdd_8 = addressData.Add_8;
        this.editForm.LSAdd_9 = addressData.Add_9;
      }
    }
  }
};
</script>

<style>
.el-radio {
  margin-right: 5px !important;
}
.el-checkbox {
  margin-right: 15px !important;
}
.el-checkbox__label {
  padding-left: 2px !important;
}
</style>
