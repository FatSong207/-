<style>
.bottomLine input {
  outline: 0 !important;
  border-width: 0 0 1px !important;
  border-color: black !important;
  background: transparent;
  border-radius: 0 !important;
  height: 28px !important;
  line-height: 28px !important;
  padding: 0 5px !important;
}
.bottomLine input:focus {
  border-color: rgb(0, 140, 255) !important;
}

.el-row {
  margin-bottom: 20px;
}
.el-row:last-child {
  margin-bottom: 0;
}

.el-col {
  border-radius: 4px;
}
.bg-purple-dark {
  background: #99a9bf;
}
.bg-purple {
  background: #eff6ff; /* #d3dce6;*/
}
.bg-purple-light {
  background: #e5e9f2;
}
.grid-content {
  border-radius: 4px;
  min-height: 36px;
  padding: 10px;
}
.row-bg {
  padding: 10px 0;
  background-color: #f9fafc;
}
</style>

<template>
  <div class="app-container">
    <el-form ref="form" :model="form" label-width="60px">
      <el-row :gutter="20">
        <el-col :span="16">
          <div class="grid-content bg-purple">
            <el-row :gutter="20">
              <el-col :span="24">
                <div class="grid-content bg-purple">
                  <el-form-item label="委託人">
                    <el-input
                      class="bottomLine"
                      v-model="form.BZName"
                      style="width: 360px"
                    ></el-input
                    >(以下簡稱甲方）
                  </el-form-item>
                </div>
              </el-col>
              <el-col :span="24">
                <div class="grid-content bg-purple">
                  <el-form-item label="受託人">
                    <el-input
                      class="bottomLine"
                      v-model="form.a2"
                      style="width: 360px"
                    ></el-input
                    >(以下簡稱乙方）
                  </el-form-item>
                </div>
              </el-col>
            </el-row>
          </div>
        </el-col>
        <el-col :span="8">
          <div class="grid-content bg-purple">
            <div class="grid-content bg-purple">
              1.每一加盟店皆為獨立經營之不動產經紀業、租賃住宅服務業。
            </div>
            <div class="grid-content bg-purple">
              2. 加盟店與加盟總部為不同法人，財務與法律責任各自獨立
            </div>
          </div>
        </el-col>
      </el-row>
      <el-row :gutter="20">
        <el-col :span="24"
          ><div class="grid-content bg-purple">
            <div class="grid-content bg-purple">
              雙方秉持誠信原則同意於民國
              <el-input
                class="bottomLine"
                v-model="form.BZAgreeDate_Y"
                style="width: 42px"
              ></el-input
              >年
              <el-input
                class="bottomLine"
                v-model="form.BZAgreeDate_M"
                style="width: 42px"
              ></el-input
              >月
              <el-input
                class="bottomLine"
                v-model="form.BZAgreeDate_D"
                style="width: 42px"
              ></el-input
              >日簽定本契約：
            </div>
            <div class="grid-content bg-purple">
              1. 甲方授權乙方得於民國
              <el-input
                class="bottomLine"
                v-model="form.BZAuthStart_Y"
                style="width: 42px"
              ></el-input
              >年
              <el-input
                class="bottomLine"
                v-model="form.BZAuthStart_M"
                style="width: 42px"
              ></el-input
              >月
              <el-input
                class="bottomLine"
                v-model="form.BZAuthStart_D"
                style="width: 42px"
              ></el-input
              >日 至民國
              <el-input
                class="bottomLine"
                v-model="form.BZAuthEnd_Y"
                style="width: 42px"
              ></el-input
              >年
              <el-input
                class="bottomLine"
                v-model="form.BZAuthEnd_M"
                style="width: 42px"
              ></el-input
              >月
              <el-input
                class="bottomLine"
                v-model="form.BZAuthEnd_D"
                style="width: 42px"
              ></el-input
              >日
              止為下列房屋為居間仲介行為（包含收取返還定金、拍照、刊登廣告等；刊登廣告內容以下列現況及謄本為主）
              房屋地址如下：
              <div class="grid-content">
                <el-input v-model="form.LNAdd_1" class="bottomLine" style="width: 60px" />
                <el-select v-model="county" placeholder="縣市" style="width: 60px">
                  <el-option
                    v-for="item in countries"
                    :key="item.Id"
                    :label="item.value"
                    :value="item.Id"
                  />
                </el-select>
                <el-input v-model="form.LNAdd_2" class="bottomLine" style="width: 60px" />
                <el-select v-model="town" placeholder="鄉鎮" style="width: 60px">
                  <el-option
                    v-for="item in towns"
                    :key="item.Id"
                    :label="item.value"
                    :value="item.Id"
                  />
                </el-select>
                <el-input
                  v-model="form.LNAdd_3"
                  class="bottomLine"
                  style="width: 100px"
                />
                <el-select v-model="street" placeholder="街路" style="width: 60px">
                  <el-option
                    v-for="item in streets"
                    :key="item.Id"
                    :label="item.value"
                    :value="item.Id"
                  />
                </el-select>
                <el-input v-model="form.LNAdd_4" class="bottomLine" style="width: 40px" />
                段
                <el-input v-model="form.LNAdd_5" class="bottomLine" style="width: 40px" />
                巷
                <el-input v-model="form.LNAdd_6" class="bottomLine" style="width: 40px" />
                弄
                <el-input v-model="form.LNAdd_7" class="bottomLine" style="width: 40px" />
                號
                <el-input v-model="form.LNAdd_8" class="bottomLine" style="width: 40px" />
                樓 之
                <el-input v-model="form.LNAdd_9" class="bottomLine" style="width: 40px" />
              </div>
            </div>
            <div class="grid-content bg-purple">
              2. 甲方同意乙方得以(
              <el-checkbox v-model="form.BZ001">親自開門</el-checkbox>
              <el-checkbox v-model="form.BZ002">鑰匙留置管理室</el-checkbox
              ><el-checkbox v-model="form.BZ003"
                >給付乙方鑰匙/磁扣
                <el-input
                  class="bottomLine"
                  v-model="form.BZ004"
                  style="width: 42px"
                ></el-input>
                支)</el-checkbox
              >
              之方式，安排有意承租之人進入本房屋參觀，並同意乙方得於本房屋張貼出租廣告。
            </div>
            <div class="grid-content bg-purple">
              3.
              甲方應交付本房屋之權狀影本、使用執照影本予乙方，如有住戶規約等，一併提供其影本；甲方如係將房屋轉租時，應提示原出租人同意書及原租賃契約書（已載明有轉租約定者為限）。
            </div>
            <div class="grid-content bg-purple">
              4.
              甲方應保證對委託之房屋有出租權利，且產權清楚並可履行房屋租賃契約，就房屋之重要事項
              (例如:住戶規約、依慣例使用之現況等)及一切足以影響承租人承租意願之情形(例如:滲漏水、凶宅…等)，對乙方負有誠實告知之義務，如有不實，則甲方須自負法律責任，同時應賠償乙方因此所遭致一切損害。
            </div>
            <div class="grid-content bg-purple">
              5.
              本契約為一般委託租賃契約書；甲方尚未與乙方所仲介之第三人簽訂房屋租賃契約書前，可隨時租售予他人，惟乙方得向甲方請求因居間仲介行為所支出之費用，費用不得超過約定報酬。
            </div>
            <div class="grid-content bg-purple">
              6. 甲方同意於乙方居間仲介完成，簽訂房屋租賃契約時，支付報酬如下： <br />
              　(1)簽立租賃期間一年之房屋租賃契約，應支付相當於<el-input
                class="bottomLine"
                v-model="form.BZ05"
                style="width: 42px"
              ></el-input
              >個月租金額為仲介費。<br />
              　(2)簽立租賃期間兩年或兩年以上之房屋租賃契約，應支付相當於
              <el-input
                class="bottomLine"
                v-model="form.BZ06"
                style="width: 42px"
              ></el-input>
              個月租金額為仲介費。
            </div>
            <div class="grid-content bg-purple">
              7. 委託人為代理人時，應出具本租賃物所有權人之授權書正本。
            </div>
            <div class="grid-content bg-purple">
              8. 因本契約所生之法律糾紛，雙方同意以臺灣臺北地方法院為管轄法院。
            </div>
            <div class="grid-content bg-purple">
              9.<el-checkbox v-model="form.BZ07" size="mini"></el-checkbox>
              甲方同意乙方代為收取定金並
              <el-checkbox v-model="form.BZ08" label="1">願</el-checkbox>
              <el-checkbox v-model="form.BZ09" label="2">不願</el-checkbox>
              提供帳戶，供乙方以
              <el-checkbox v-model="form.BZ10" label="1">現金</el-checkbox>
              <el-checkbox v-model="form.BZ11" label="2">匯款</el-checkbox>
              方式將定金交付甲方。
            </div>
            <div class="grid-content bg-purple">
              10.<el-checkbox v-model="form.BZ12"></el-checkbox>
              甲方同意乙方代為收取定金，並於簽訂房屋租賃契約時轉為仲介費；定金超過仲介費之部分，甲方
              <el-checkbox v-model="form.BZ13" label="1">同意</el-checkbox>
              <el-checkbox v-model="form.BZ14" label="2">不同意</el-checkbox>
              提供帳戶，供乙方以
              <el-checkbox v-model="form.BZ15" label="1">現金</el-checkbox>
              <el-checkbox v-model="form.BZ16" label="2">匯款</el-checkbox>
              之方式將超過部分交付甲方
            </div>
            <div class="grid-content bg-purple">
              11.<el-checkbox v-model="form.BZ17"></el-checkbox>
              甲方同意乙方得代為與第三人簽訂房屋租賃契約，甲方應提供授權書。
            </div>
            <div class="grid-content bg-purple">
              12. 以下為房屋資訊與委託出租條件：面積：
              <el-input
                class="bottomLine"
                v-model="form.BPing"
                style="width: 42px"
              ></el-input
              >坪
              <el-input
                class="bottomLine"
                v-model="form.a9"
                style="width: 42px"
              ></el-input
              >臥室
              <el-input
                class="bottomLine"
                v-model="form.a10"
                style="width: 42px"
              ></el-input
              >和室
              <el-input
                class="bottomLine"
                v-model="form.a11"
                style="width: 42px"
              ></el-input
              >書房
              <el-input
                class="bottomLine"
                v-model="form.a12"
                style="width: 42px"
              ></el-input
              >廳
              <el-input
                class="bottomLine"
                v-model="form.a13"
                style="width: 42px"
              ></el-input
              >浴室
              <el-input
                class="bottomLine"
                v-model="form.a14"
                style="width: 42px"
              ></el-input
              >廚房
              <el-input
                class="bottomLine"
                v-model="form.a15"
                style="width: 42px"
              ></el-input
              >陽台、
              <br />
              <el-checkbox v-model="form.a16">有</el-checkbox
              ><el-checkbox v-model="form.a161">無</el-checkbox>電梯、
              <el-checkbox v-model="form.a17">有</el-checkbox
              ><el-checkbox v-model="form.a171">無</el-checkbox>管理員、
              <el-checkbox v-model="form.a18">是</el-checkbox
              ><el-checkbox v-model="form.a181">非</el-checkbox>套房、 一層<el-input
                class="bottomLine"
                v-model="form.a19"
                style="width: 42px"
              ></el-input
              >戶，
              <el-input
                class="bottomLine"
                v-model="form.a20"
                style="width: 42px"
              ></el-input
              >座向、 <el-checkbox v-model="form.a21">是</el-checkbox
              ><el-checkbox v-model="form.a211">否</el-checkbox>公證。 月租金：<el-input
                class="bottomLine"
                v-model="form.a22"
                style="width: 42px"
              ></el-input
              >新台幣 元整月租金如有調整應經甲方同意>。 車位租金：新台幣
              <el-input
                class="bottomLine"
                v-model="form.a24"
                style="width: 42px"
              ></el-input>
              元整 (<el-checkbox v-model="form.a23">平面</el-checkbox
              ><el-checkbox v-model="form.a231">機械</el-checkbox>)。 管理費：新台幣
              <el-input
                class="bottomLine"
                v-model="form.a25"
                style="width: 42px"
              ></el-input>
              元整。 押金：新台幣
              <el-input
                class="bottomLine"
                v-model="form.a26"
                style="width: 42px"
              ></el-input>
              元整。 其他：
              <el-input
                class="bottomLine"
                v-model="form.a27"
                style="width: 80%"
              ></el-input
              >。
            </div>
          </div>
        </el-col>
      </el-row>
      <el-row :gutter="20">
        <el-col :span="24">
          <div class="grid-content bg-purple">
            附屬設備：
            <el-checkbox v-model="form.a1611">流理台</el-checkbox>
            <el-checkbox v-model="form.a162">瓦斯爐</el-checkbox>
            <el-checkbox v-model="form.a163">微波爐</el-checkbox>
            <el-checkbox v-model="form.a164">烤箱</el-checkbox>
            <el-checkbox v-model="form.a165">洗碗機</el-checkbox>
            <el-checkbox v-model="form.a166">冰箱</el-checkbox>
            <el-checkbox v-model="form.a167">熱水器(瓦斯/電)</el-checkbox>
            <el-checkbox v-model="form.a168">沙發</el-checkbox>
            <br />
            <el-checkbox v-model="form.a169"
              >電視（<el-input
                class="bottomLine"
                v-model="form.b611"
                style="width: 28px"
              ></el-input
              >）台</el-checkbox
            >
            <el-checkbox v-model="form.a160">窗簾</el-checkbox>
            <el-checkbox v-model="form.a1671">餐桌椅</el-checkbox>
            <el-checkbox v-model="form.a1672">衣櫃</el-checkbox>
            <el-checkbox v-model="form.a1673"
              >床舖（<el-input
                class="bottomLine"
                v-model="form.b621"
                style="width: 28px"
              ></el-input
              >）張</el-checkbox
            >
            <el-checkbox v-model="form.a1674">洗衣機</el-checkbox>
            <el-checkbox v-model="form.a1675">乾衣機</el-checkbox>
            <el-checkbox v-model="form.a1676"
              >冷氣（<el-input
                class="bottomLine"
                v-model="form.b631"
                style="width: 28px"
              ></el-input
              >）台</el-checkbox
            >
            <hr />
            地 板：
            <el-checkbox v-model="form.a1312">木質</el-checkbox>
            <el-checkbox v-model="form.a1313">抛光石英磚</el-checkbox>
            <el-checkbox v-model="form.a1314">一般磁磚</el-checkbox>
            <el-checkbox v-model="form.a1315">地毯</el-checkbox>
            <el-checkbox v-model="form.a1316">塑膠地板</el-checkbox>
            <hr />
            公共設施：
            <el-checkbox v-model="form.a141">游泳池</el-checkbox>
            <el-checkbox v-model="form.a142">健身房</el-checkbox>
            <el-checkbox v-model="form.a143">視聽室</el-checkbox>
            <el-checkbox v-model="form.a144">小孩遊戲區</el-checkbox>
            <hr />
            屋內使用現況：
            <el-checkbox v-model="form.a151">整層住家</el-checkbox>
            <el-checkbox v-model="form.a152">辦公室</el-checkbox>
            <el-checkbox v-model="form.a153">住家及辦公室皆可</el-checkbox>
          </div>
        </el-col>
      </el-row>
      <el-row :gutter="20">
        <el-col :span="12">
          <div class="grid-content bg-purple">
            <div style="text-align: center">委託人</div>
            <hr />
            <div class="grid-content">
              姓　　　名：
              <el-input
                class="bottomLine"
                v-model="form.LSName"
                style="width: 80%"
              ></el-input>
            </div>
            <div class="grid-content">
              身分證字號：<el-input
                class="bottomLine"
                v-model="form.LSID"
                style="width: 80%"
              ></el-input>
            </div>
            <div class="grid-content">
              電　　　話：<el-input
                class="bottomLine"
                v-model="form.LSTel"
                style="width: 80%"
              ></el-input>
            </div>
            <div class="grid-content">
              地　　　址：<el-input
                class="bottomLine"
                v-model="form.LSAdd"
                style="width: 80%"
              ></el-input>
            </div>
            <div class="grid-content" style="font-size: 0.8em">
              契約審閱權(契約審閱期間至少三日)房屋委託租賃契約書於中華民國
              <el-input
                class="bottomLine"
                v-model="form.a265"
                style="width: 42px"
              ></el-input>
              年
              <el-input
                class="bottomLine"
                v-model="form.a266"
                style="width: 42px"
              ></el-input>
              月
              <el-input
                class="bottomLine"
                v-model="form.a267"
                style="width: 42px"
              ></el-input>
              日經委託人攜回審閱
              <el-input
                class="bottomLine"
                v-model="form.a268"
                style="width: 42px"
              ></el-input>
              日，並充分瞭解本契約內容。委託人簽章：
              <img
                v-bind:src="form.signatureData"
                style="height: 38px; display: inline-block; vertical-align: middle"
                v-show="form.signatureData != null"
              />
              <el-button type="text" @click="centerDialogVisible = true">簽名</el-button>
              <el-dialog title="簽名" :visible.sync="centerDialogVisible" width="850px">
                <div id="signDiv" style="border: 1px solid black">
                  <VueSignaturePad width="800px" height="500px" ref="signaturePad" />
                </div>
                <span slot="footer" class="dialog-footer">
                  <el-button type="primary" @click="save">儲存</el-button>
                  <el-button @click="clear">清除</el-button>
                  <el-button type="info" @click="centerDialogVisible = false"
                    >關閉</el-button
                  >
                </span>
              </el-dialog>
            </div>
          </div>
        </el-col>
        <el-col :span="12">
          <div class="grid-content bg-purple">
            <div style="text-align: center">受託人</div>
            <hr />
            <div class="grid-content">
              公司名稱：<el-input
                class="bottomLine"
                v-model="form.BUnitName"
                style="width: 80%"
              ></el-input>
            </div>
            <div class="grid-content">
              地　　址：<el-input
                class="bottomLine"
                v-model="form.BUnitADD"
                style="width: 80%"
              ></el-input>
            </div>
            <div class="grid-content">
              電　　話：<el-input
                class="bottomLine"
                v-model="form.BUnitTel"
                style="width: 80%"
              ></el-input>
            </div>
            <div class="grid-content">
              不動產經紀人：<el-input
                class="bottomLine"
                v-model="form.a2673"
                style="width: 76%"
              ></el-input>
            </div>

            <div class="grid-content">
              證　　號：<el-input
                class="bottomLine"
                v-model="form.BUnitCNumber"
                style="width: 80%"
              ></el-input>
            </div>
            <div class="grid-content">
              開發人員：<el-input
                v-model="form.BUnitSalesName"
                class="bottomLine"
                style="width: 30%"
              ></el-input>
              連絡電話：<el-input
                v-model="form.BUnitSalesTel"
                class="bottomLine"
                style="width: 30%"
              ></el-input>
            </div>
            <div class="grid-content">
              總部申訴專線：0800-666-444
              <el-checkbox v-model="form.BUnitY">加盟店</el-checkbox>
            </div>
          </div>
        </el-col>
      </el-row>
      <div class="grid-content bg-purple">
        第一聯：受託人　　第二聯：委託人　　第三聯：留底聯
      </div>
    </el-form>
    <div style="margin-top: 8px; text-align: center">
      <el-button type="primary" @click="upload()">儲存</el-button>
      <el-button type="info" @click="download()">下載測試</el-button>
    </div>
  </div>
</template>

<script>
import {
  downloadPDFWithDataByFormId
} from "@/api/chaochi/externalform/externalformservice";
export default {
  name: "MySignaturePad",
  data() {
    return {
      centerDialogVisible: false,
      form: {
        FName: "A000201",
        BID: "",
        BZName: "",
        BZUnit: "",
        BZAgreeDate_Y: "",
        BZAgreeDate_M: "",
        BZAgreeDate_D: "",
        BZAuthStart_Y: "",
        BZAuthStart_M: "",
        BZAuthStart_D: "",
        BZAuthEnd_Y: "",
        BZAuthEnd_M: "",
        BZAuthEnd_D: "",
        BADD: "",
        BZ001: "",
        BZ002: "",
        BZ003: "",
        BZ004: "",
        BZ05: "",
        BZ06: "",
        BZ07: "",
        BZ08: "",
        BZ09: "",
        BZ10: "",
        BZ11: "",
        BZ12: "",
        BZ13: "",
        BZ14: "",
        BZ15: "",
        BZ16: "",
        BZ17: "",
        Bping: "",
        BZ18: "",
        BZ19: "",
        BZ20: "",
        BZ21: "",
        BZ22: "",
        BZ23: "",
        BZ24: "",
        BZ25: "",
        BZ26: "",
        BZ27: "",
        BZ28: "",
        BZ29: "",
        BZ30: "",
        BZ31: "",
        BZ32: "",
        BZ33: "",
        BZ34: "",
        BTrent: "",
        BParkFee: "",
        BZ35: "",
        BZ36: "",
        BMFee: "",
        BDeposit: "",
        BZ37: "",
        BZ38: "",
        BZ39: "",
        BZ40: "",
        BZ41: "",
        BZ42: "",
        BZ43: "",
        BZ44: "",
        BZ45: "",
        BZ46: "",
        BZ47: "",
        BZ48: "",
        BZ49: "",
        BZ50: "",
        BZ51: "",
        BZ52: "",
        BZ53: "",
        BZ54: "",
        BZ55: "",
        BZ56: "",
        BZ57: "",
        BZ58: "",
        BZ59: "",
        BZ60: "",
        BZ61: "",
        BZ62: "",
        BZ63: "",
        BZ64: "",
        BZ65: "",
        BZ66: "",
        BZ67: "",
        BZ68: "",
        BZ69: "",
        BZ70: "",
        LSName: "",
        LSID: "",
        LSTel: "",
        LSADD: "",
        BZ70: "",
        BZ71: "",
        BZ72: "",
        BZ73: "",
        BUnitName: "",
        BUnitADD: "",
        BUnitTel: "",
        BZ74: "",
        BUnitCNumber: "",
        BUnitSalesName: "",
        BUnitSalesTel: "",
        BUnitY: "",
        FName: "",
        BID: "",
        BZUnit: "",
        BZName: "",
        BZAgreeDate_Y: "",
        BZAgreeDate_M: "",
        BZAgreeDate_D: "",
        BZAuthStart_Y: "",
        BZAuthStart_M: "",
        BZAuthStart_D: "",
        BZAuthEnd_Y: "",
        BZAuthEnd_M: "",
        BZAuthEnd_D: "",
        BADD: "",
        BPing: "",
        BFaceW: "",
        BTrent: "",
        BTrentTax_N: "",
        BTrentTax_Y: "",
        BZ201: "",
        BZ202: "",
        BZ203: "",
        BDeposit: "",
        BZ204: "",
        BMFee: "",
        BZ205: "",
        BZ206: "",
        BZ207: "",
        BZ208: "",
        BZ209: "",
        BZ210: "",
        LSName: "",
        LSID: "",
        LSTel: "",
        LSADD: "",
        BUnitName: "",
        BZ211: "",
        BUnitCNumber: "",
        BUnitSalesName: "",
        BUnitSalesTel: "",
        signatureData: null,
        LNAdd_1: "",
        LNAdd_1_1: "",
        LNAdd_1_2: "",
        LNAdd_2: "",
        LNAdd_2_1: "",
        LNAdd_2_2: "",
        LNAdd_2_3: "",
        LNAdd_2_4: "",
        LNAdd_3: "",
        LNAdd_3_1: "",
        LNAdd_3_2: "",
        LNAdd_4: "",
        LNAdd_5: "",
        LNAdd_6: "",
        LNAdd_7: "",
        LNAdd_8: "",
        LNAdd_9: "",
        FName: "A000201",
        Vno: "01",
        LCID: "testJson",
        TBNo: "B1",
      },
      searchPDF: {
        testName: "",
        LNID: "",
        LCID: "",
        BID: "",
        BAdd: "",
        FormId: "A000201",
        TBNo: "B1",
      },
      county: "",
      town: "",
      street: "",
    };
  },
  methods: {
    clear() {
      this.$refs.signaturePad.clearSignature();
    },
    undo() {
      this.$refs.signaturePad.undoSignature();
    },
    onSubmit() {
      console.log("submit!");
    },
    upload: function () {
      uploadJsonWithData(JSON.stringify(this.form))
        .then((response) => {
          this.$message({
            message: "已儲存 !",
            type: "success",
          });
          console.log(response);
        })
        .catch((error) => {
          console.log(error);
        });
    },
    download: function () {
      let label = this.searchPDF.FormId;
      downloadPDFWithDataByFormId(this.searchPDF)
        .then((response) => {
          if (response == null) {
            this.$message({
              message: "下載失敗",
              type: "error",
            });
            return;
          }
          if (response.type !== "application/pdf") {
            var reader = new FileReader();
            reader.onload = (e) => {
              const msg = JSON.parse(e.target.result);
              console.log(msg);
              this.$message({
                message: "下載失敗, 請確認空白PDF(" + label + ")已上傳",
                type: "error",
              });
            };
            reader.readAsText(response);
            return;
          }
          const blob = response;
          const link = document.createElement("a");
          link.href = URL.createObjectURL(blob);
          link.download = label;
          link.click();
          URL.revokeObjectURL(link.href);
        })
        .catch((error) => {
          console.log(error);
        });

      // downloadJsonWithDataByFormId(JSON.stringify(this.searchPDF))
      //   .then((response) => {
      //     console.log(response);
      //   })
      //   .catch((error) => {
      //     console.log(error);
      //   });
    },
  },
};
</script>
