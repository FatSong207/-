/* eslint-disable no-irregular-whitespace */
<template>
  <div>
    <el-form :model="form" label-width="80px" size="small" label-position="left">
      <el-card class="box-card">
        <div class="text item">
          <el-form-item label="委託人">
            <span class="valuetxt">{{ form.LSName }}</span>(以下簡稱甲方）
          </el-form-item>
        </div>
        <div class="text item">
          <el-form-item label="受託人">
            <span class="valuetxt">{{ form.BZName }}</span>(以下簡稱甲方）
          </el-form-item>
        </div>
        <div class="text item">
          <span>雙方秉持誠信原則同意簽定本契約，授權乙方辦理委託仲介租賃：</span>
        </div>
        <div class="text item">
          <el-form-item label="1." label-width="20px" class="mb-0">
            <span>乙方得於民國<span class="valuetxt">{{ form.BAuthStart_Y }}</span>年<span class="valuetxt">{{ form.BAuthStart_M }}</span>月<span class="valuetxt">{{ form.BAuthStart_D }}</span>日 至民國<span class="valuetxt">{{ form.BZAuthEnd_Y }}</span>年<span class="valuetxt">{{ form.BZAuthEnd_M }}</span>月<span class="valuetxt">{{ form.BZAuthEnd_D }}</span>日止
              為下列房屋為居間仲介行為（包含收取返還定金、拍照、刊登廣告等；房屋資訊與委託出租條件內容以謄本及下列現況為主）。</span>
          </el-form-item>
        </div>
        <div class="text item">
          地址：<span class="valuetxt">{{ form.BAdd }}</span>。社區名稱：<span class="valuetxt">{{ form.BCommunity }}</span> 。
        </div>
        <div class="text item">
          面積：<el-checkbox
            v-model="form.BpingV"
            :true-label="trueValue"
            :false-label="falseValue"
            disabled
          >依權狀/騰本登記</el-checkbox>
          <el-checkbox
            v-model="form.BpingActualV"
            :true-label="trueValue"
            :false-label="falseValue"
            disabled
          >現況坪數<span class="valuetxt">{{ form.BpingActual }}</span>坪</el-checkbox>；<span class="valuetxt">{{ form.BRoom }}</span>房<span class="valuetxt">{{ form.BLD }}</span>廳<span class="valuetxt">{{ form.BBath }}</span>衛浴<span class="valuetxt">{{ form.BKitchen }}</span>廚房<span class="valuetxt">{{ form.BBalcony }}</span>陽台。
        </div>
        <div class="text item">
          建物：<el-checkbox
            v-model="form.BTApartment"
            :true-label="trueValue"
            :false-label="falseValue"
            disabled
          >公寓</el-checkbox>
          <el-checkbox
            v-model="form.BTElevatorB"
            :true-label="trueValue"
            :false-label="falseValue"
            disabled
          >電梯大樓</el-checkbox>
          <el-checkbox
            v-model="form.BTHouse"
            :true-label="trueValue"
            :false-label="falseValue"
            disabled
          >透天；</el-checkbox>
          <el-checkbox
            v-model="form.Belevator_Y"
            :true-label="trueValue"
            :false-label="falseValue"
            disabled
          >有</el-checkbox>
          <el-checkbox
            v-model="form.Belevator_N"
            :true-label="trueValue"
            :false-label="falseValue"
            disabled
          >無</el-checkbox>電梯；<el-checkbox
            v-model="form.Bsuper_Y"
            :true-label="trueValue"
            :false-label="falseValue"
            disabled
          >有</el-checkbox><el-checkbox
            v-model="form.Bsuper_N"
            :true-label="trueValue"
            :false-label="falseValue"
            disabled
          >無</el-checkbox>管理員； 一層<span class="valuetxt">{{ form.Bhousehold }}</span>戶 ； 座向<span class="valuetxt">{{ form.Bddirection }}</span>。
        </div>
        <div class="text item">
          車位：編號第<span class="valuetxt">{{ form.BCarNo }}</span>號，地上（面、下）<el-checkbox
            v-model="form.BRampParking"
            :true-label="trueValue"
            :false-label="falseValue"
            disabled
          >坡道式</el-checkbox>
          <el-checkbox
            v-model="form.BLiftParking"
            :true-label="trueValue"
            :false-label="falseValue"
            disabled
          >升降式第<span class="valuetxt">{{ form.BCarFloor }}</span>層</el-checkbox>，<el-checkbox
            v-model="form.BPParking"
            :true-label="trueValue"
            :false-label="falseValue"
            disabled
          >平面式</el-checkbox><el-checkbox
            v-model="form.BMParking"
            :true-label="trueValue"
            :false-label="falseValue"
            disabled
          >機械式車位</el-checkbox>
        </div>
        <div class="text item">
          月 租 金：新台幣<span class="valuetxt">{{ form.BTrent }}</span>元整。租押金：新台幣<span class="valuetxt">{{ form.BDeposit }}</span>元整。
        </div>
        <div class="text item">
          租金 包含：<el-checkbox
            v-model="form.BMFeeV"
            :true-label="trueValue"
            :false-label="falseValue"
            disabled
          >管理費 </el-checkbox><el-checkbox
            v-model="form.BCleaningFeeV"
            :true-label="trueValue"
            :false-label="falseValue"
            disabled
          >清潔費 </el-checkbox><el-checkbox
            v-model="form.BCableTVFeeV"
            :true-label="trueValue"
            :false-label="falseValue"
            disabled
          >第四台 </el-checkbox><el-checkbox
            v-model="form.BInterNetFeeV"
            :true-label="trueValue"
            :false-label="falseValue"
            disabled
          >網路 </el-checkbox><el-checkbox
            v-model="form.BWaterBillV"
            :true-label="trueValue"
            :false-label="falseValue"
            disabled
          >水費 </el-checkbox><el-checkbox
            v-model="form.BElectricityBillV"
            :true-label="trueValue"
            :false-label="falseValue"
            disabled
          >電費 </el-checkbox><el-checkbox
            v-model="form.BGasFeeV"
            :true-label="trueValue"
            :false-label="falseValue"
            disabled
          >瓦斯費</el-checkbox>
        </div>
        <div class="text item">
          車位 租金：新台幣<span class="valuetxt">{{ form.BParkFee }}</span>元整。車位清潔費：新台幣<span class="valuetxt">{{ form.BParkCFee }}</span>元整。
        </div>
        <div class="text item">
          大樓管理費：新台幣<span class="valuetxt">{{ form.BMFee }}</span>元整。其他費用： <span class="valuetxt">{{ form.BZ001 }}</span>。
        </div>
        <div class="text item">
          附屬設備：<el-checkbox
            v-model="form.BZ002"
            :true-label="trueValue"
            :false-label="falseValue"
            disabled
          >洗衣機</el-checkbox>
          <el-checkbox
            v-model="form.BZ003"
            :true-label="trueValue"
            :false-label="falseValue"
            disabled
          >冰箱<span class="valuetxt">{{ form.BZ004 }}</span>台</el-checkbox>
          <el-checkbox
            v-model="form.BZ005"
            :true-label="trueValue"
            :false-label="falseValue"
            disabled
          >電視<span class="valuetxt">{{ form.BZ006 }}</span>台</el-checkbox>
          <el-checkbox
            v-model="form.BZ007"
            :true-label="trueValue"
            :false-label="falseValue"
            disabled
          >冷氣<span class="valuetxt">{{ form.BZ008 }}</span>台 </el-checkbox><el-checkbox
            v-model="form.BZ009"
            :true-label="trueValue"
            :false-label="falseValue"
            disabled
          >熱水器
            <el-checkbox
              v-model="form.BZ010"
              :true-label="trueValue"
              :false-label="falseValue"
              disabled
            >瓦斯</el-checkbox>
            <el-checkbox
              v-model="form.BZ011"
              :true-label="trueValue"
              :false-label="falseValue"
              disabled
            >電</el-checkbox>
          </el-checkbox>
          <el-checkbox
            v-model="form.BZ012"
            :true-label="trueValue"
            :false-label="falseValue"
            disabled
          >網路</el-checkbox>
          <el-checkbox
            v-model="form.BZ013"
            :true-label="trueValue"
            :false-label="falseValue"
            disabled
          >第四台</el-checkbox>
          <el-checkbox
            v-model="form.BZ014"
            :true-label="trueValue"
            :false-label="falseValue"
            disabled
          >天然瓦斯</el-checkbox>
          <el-checkbox
            v-model="form.BZ015"
            :true-label="trueValue"
            :false-label="falseValue"
            disabled
          >流理台</el-checkbox>
          <el-checkbox
            v-model="form.BZ016"
            :true-label="trueValue"
            :false-label="falseValue"
            disabled
          >瓦斯爐</el-checkbox>
          <el-checkbox
            v-model="form.BZ017"
            :true-label="trueValue"
            :false-label="falseValue"
            disabled
          >床組<span class="valuetxt">{{ form.BZ018 }}</span>組</el-checkbox>
          <el-checkbox
            v-model="form.BZ019"
            :true-label="trueValue"
            :false-label="falseValue"
            disabled
          >衣櫃<span class="valuetxt">{{ form.BZ020 }}</span>組</el-checkbox>
          <el-checkbox
            v-model="form.BZ021"
            :true-label="trueValue"
            :false-label="falseValue"
            disabled
          >沙發<span class="valuetxt">{{ form.BZ022 }}</span>組 </el-checkbox><el-checkbox
            v-model="form.BZ023"
            :true-label="trueValue"
            :false-label="falseValue"
            disabled
          >桌子</el-checkbox>
          <el-checkbox
            v-model="form.BZ024"
            :true-label="trueValue"
            :false-label="falseValue"
            disabled
          >椅子</el-checkbox>
        </div>
        <div class="text item">
          使用需求：最短租期<span class="valuetxt">{{ form.BMiniLeaseP }}</span>年；開伙<el-checkbox
            v-model="form.BCook_Y"
            :true-label="trueValue"
            :false-label="falseValue"
            disabled
          >可</el-checkbox><el-checkbox
            v-model="form.BCook_N"
            :true-label="trueValue"
            :false-label="falseValue"
            disabled
          >不可</el-checkbox>；寵物<el-checkbox
            v-model="form.BPet_Y"
            :true-label="trueValue"
            :false-label="falseValue"
            disabled
          >可</el-checkbox><el-checkbox
            v-model="form.BPet_N"
            :true-label="trueValue"
            :false-label="falseValue"
            disabled
          >不可</el-checkbox>不可；神明桌<el-checkbox
            v-model="form.BGodT_Y"
            :true-label="trueValue"
            :false-label="falseValue"
            disabled
          >可</el-checkbox><el-checkbox
            v-model="form.BGodT_N"
            :true-label="trueValue"
            :false-label="falseValue"
            disabled
          >不可</el-checkbox>。
        </div>
        <div class="text item">
          <el-form-item label="2." label-width="20px" class="mb-0">
            <span>委託人為代理人時，應出具本租賃物所有權人之授權書正本。</span>
          </el-form-item>
        </div>
        <div class="text item">
          <el-form-item label="3." label-width="20px" class="mb-0">
            <span>甲方應交付本房屋之權狀影本、使用執照影本予乙方，如有住戶規約等，一併提供其影本；甲方如係將房屋轉租時，應提示原出租人同意書及原租賃契約書（已載明有轉租約定者為限）。</span>
          </el-form-item>
        </div>
        <div class="text item">
          <el-form-item label="4." label-width="20px" class="mb-0">
            <span>甲方應保證對委託之房屋有出租權利，且產權清楚並可履行房屋租賃契約，就房屋之重要事項
              (例如:規約、慣例使用現況等)及一切足以影響承租人承租意願之情形(例如:滲漏水、凶宅…等)，對乙方負有誠實告知之義務，如有不實，則甲方須自負法律責任，同時應賠償乙方因此所遭致一切損害。</span>
          </el-form-item>
        </div>
        <div class="text item">
          <el-form-item label="5." label-width="20px" class="mb-0">
            <span>甲方同意乙方得以（
              <el-checkbox
                v-model="form.BOpen"
                :true-label="trueValue"
                :false-label="falseValue"
                disabled
              >親自開門</el-checkbox>
              <el-checkbox
                v-model="form.BKeyFM"
                :true-label="trueValue"
                :false-label="falseValue"
                disabled
              >鑰匙留置管理室</el-checkbox>
              <el-checkbox
                v-model="form.BKeyFR"
                :true-label="trueValue"
                :false-label="falseValue"
                disabled
              >給付乙方鑰匙/磁扣</el-checkbox>
              <span class="valuetxt">{{ form.BkeyCount }}</span>支）之方式，安排有意承租之人進入本房屋參觀，並同意乙方得於本房屋張貼出租廣告。</span>
          </el-form-item>
        </div>
        <div class="text item">
          <el-form-item label="6." label-width="20px" class="mb-0">
            <span>甲方
              <el-checkbox
                v-model="form.BZ025"
                :true-label="trueValue"
                :false-label="falseValue"
                disabled
              >同意</el-checkbox>
              <el-checkbox
                v-model="form.BZ026"
                :true-label="trueValue"
                :false-label="falseValue"
                disabled
              >不同意</el-checkbox>
              乙方代為收取定金並於簽訂房屋租賃契約時，將代為收取之定金轉為
              <el-checkbox
                v-model="form.BZ027"
                :true-label="trueValue"
                :false-label="falseValue"
                disabled
              >仲介費</el-checkbox>
              <el-checkbox
                v-model="form.BZ028"
                :true-label="trueValue"
                :false-label="falseValue"
                disabled
              >租押金</el-checkbox>
              ； <br>定金超過之部分，乙方得以
              <el-checkbox
                v-model="form.BZ029"
                :true-label="trueValue"
                :false-label="falseValue"
                disabled
              >現金</el-checkbox>
              <el-checkbox
                v-model="form.BZ030"
                :true-label="trueValue"
                :false-label="falseValue"
                disabled
              >匯款</el-checkbox>之方式將超過部分交付甲方。</span>
          </el-form-item>
        </div>
        <div class="text item">
          <el-form-item label="7." label-width="20px" class="mb-0">
            <span>甲方
              <el-checkbox
                v-model="form.BZ031"
                :true-label="trueValue"
                :false-label="falseValue"
                disabled
              >同意</el-checkbox>
              <el-checkbox
                v-model="form.BZ032"
                :true-label="trueValue"
                :false-label="falseValue"
                disabled
              >不同意</el-checkbox>
              乙方得代為與第三人簽訂房屋租賃契約，甲方應提供授權書。</span>
          </el-form-item>
        </div>
        <div class="text item">
          <el-form-item label="8." label-width="20px" class="mb-0">
            <span>甲方應於乙方居間仲介完成，簽訂房屋租賃契約時，支付報酬如下： <br>
              (1)簽立租賃期間一年之房屋租賃契約，應支付相當於<span class="valuetxt">{{
                form.BZ033
              }}</span>個月租金額為仲介費。 <br>
              (2)簽立租賃期間兩年(含)以上之房屋租賃契約，應支付相當於<span
                class="valuetxt"
              >{{ form.BZ034 }}</span>個月租金額為仲介費。
            </span>
          </el-form-item>
        </div>
        <div class="text item">
          <el-form-item label="9." label-width="20px" class="mb-0">
            <span>本契約為一般委託租賃契約書，甲方尚未與乙方所仲介之第三人簽訂房屋租賃契約書前，可隨時租售予他人，惟乙方得向甲方請求因居間仲介行為所支出之費用，費用不得超過約定報酬。</span>
          </el-form-item>
        </div>
        <div class="text item">
          <el-form-item label="10." label-width="20px" class="mb-0">
            <span>每一加盟店皆為獨立經營之不動產經紀業、租賃住宅服務業；加盟店與加盟總部為不同法人，財務與法律責任各自獨立。</span>
          </el-form-item>
        </div>
        <div class="text item">
          <el-form-item label="11." label-width="20px" class="mb-0">
            <span>因本契約所生之法律糾紛，雙方同意以臺灣臺北地方法院為管轄法院。</span>
          </el-form-item>
        </div>
        <hr>
        <el-row :gutter="20">
          <el-col :span="12">
            <div class="text item grid-content bg-purple">
              <div style="text-align: center">委託人</div>
              <hr>
              <div class="text item grid-content">
                姓   名：
                <!-- <img
                  class="tmb"
                  v-bind:src="signForm.SignatureImgPath_1"
                  v-show="signForm.SignatureImgPath_1 != null"
                /> -->
                <el-image
                  v-show="signForm.SignatureImgPath_1 != null"
                  class="tmb"
                  :src="signForm.SignatureImgPath_1"
                />
                <el-image
                  v-show="signForm.SignatureBase64_1 != null"
                  :src="signForm.SignatureBase64_1"
                  class="tmb"
                />
                <el-button
                  v-show="signForm.SignatureImgPath_1 == null"
                  type="text"
                  @click="centerDialogVisible = true"
                >簽名</el-button>
                <el-dialog
                  title="簽名"
                  :visible.sync="centerDialogVisible"
                  width="850px"
                  :close-on-click-modal="false"
                >
                  <div id="signDiv" style="border: 1px solid black">
                    <VueSignaturePad ref="signaturePad" width="800px" height="500px" />
                  </div>
                  <span slot="footer" class="dialog-footer">
                    <el-button
                      v-show="!sign2"
                      type="primary"
                      @click="save('1')"
                    >儲存</el-button>
                    <el-button
                      v-show="sign2"
                      type="primary"
                      @click="save('2')"
                    >儲存</el-button>
                    <el-button @click="clear">清除</el-button>
                    <el-button
                      type="info"
                      @click="centerDialogVisible = false"
                    >關閉</el-button>
                  </span>
                </el-dialog>
              </div>
              <div class="text item grid-content">
                身分證字號： <span class="valuetxt">{{ form.LSID }}</span>
              </div>
              <div class="text item grid-content">
                電   話： <span class="valuetxt">{{ form.LSTel }}</span>
              </div>
              <div class="text item grid-content">
                地   址： <span class="valuetxt">{{ form.LSAdd }}</span>
                <el-image
                  v-show="signForm.SignatureImgPath_2 != null"
                  class="tmb"
                  :src="signForm.SignatureImgPath_2"
                />
                <el-image
                  v-show="signForm.SignatureBase64_2 != null"
                  :src="signForm.SignatureBase64_2"
                  class="tmb"
                />
                <el-button
                  v-show="signForm.SignatureImgPath_2 == null"
                  type="text"
                  @click="
                    centerDialogVisible = true;
                    sign2 = true;
                  "
                >簽名2</el-button>
              </div>
              <div class="text item grid-content" style="font-size: 0.8em">
                契約審閱權，本房屋委託租賃契約書已由委託人攜回審閱至少三日，並充分瞭解本契約內容。
              </div>
            </div>
          </el-col>
          <el-col :span="12">
            <div class="text item grid-content bg-purple">
              <div style="text-align: center">受託人</div>
              <hr>
              <div class="text item grid-content">
                公司名稱：<span class="valuetxt">{{ form.BUnitName }}</span>
              </div>
              <div class="text item grid-content">
                地  址：<span class="valuetxt">{{ form.BUnitADD }}</span>
              </div>
              <div class="text item grid-content">
                電  話：<span class="valuetxt">{{ form.BUnitTel }}</span>
              </div>
              <div class="text item grid-content">
                不動產經紀人：<span class="valuetxt">{{ form.BUnitBroker }}</span>
              </div>

              <div class="text item grid-content">
                證  號：<span class="valuetxt">{{ form.BUnitCNumber }}</span>
              </div>
              <div class="text item grid-content">
                開發人員：<span class="valuetxt">{{ form.BUnitSalesName }}</span>

                連絡電話：<span class="valuetxt">{{ form.BUnitSalesTel }}</span>
              </div>
            </div>
          </el-col>
        </el-row>
      </el-card>
    </el-form>
    <div style="margin-top: 20px; text-align: center">
      <el-button type="info" @click="findForm()">重新載入</el-button>
      <el-button
        v-show="signForm.SignatureImgPath_1 == null"
        type="primary"
        @click="upload()"
      >儲存</el-button>
      <!-- <el-button type="info" @click="GetSignature()">簽名檔測試</el-button> -->
    </div>
  </div>
</template>
<script>
import {
  uploadSignatureImg,
  findSignature,
  getImg,
  findFormData
} from '@/api/chaochi/externalform/externalformservice'
export default {
  name: 'InternalFormDetailSignA000201',
  props: {
    searchPDF: { type: Object, default: null }
  },
  data() {
    return {
      trueValue: '/Yes',
      falseValue: '/Off',
      sign2: false,
      centerDialogVisible: false,
      form: {},
      signForm: {
        FName: '',
        Vno: '',
        BAdd: '',
        SignatureBase64_1: null,
        SignatureBase64_2: null,
        SignatureBase64_3: null,
        SignatureBase64_4: null,
        SignatureBase64_5: null,
        SignatureImgPath_1: null,
        SignatureImgPath_2: null,
        SignatureImgPath_3: null,
        SignatureImgPath_4: null,
        SignatureImgPath_5: null
      }
    }
  },
  mounted() {
    // this.GetSignature();
  },
  methods: {
    GetSignature() {
      console.log('searchPDF:' + this.searchPDF)
      findSignature(JSON.stringify(this.searchPDF))
        .then((response) => {
          if (response.ResData) {
            this.signForm = response.ResData
            if (this.signForm.SignatureImgPath_1) {
              getImg(this.signForm.SignatureImgPath_1).then((response) => {
                this.signForm.SignatureImgPath_1 = URL.createObjectURL(response)
              })
            }
            if (this.signForm.SignatureImgPath_2) {
              getImg(this.signForm.SignatureImgPath_2).then((response) => {
                this.signForm.SignatureImgPath_2 = URL.createObjectURL(response)
              })
            }
            if (this.signForm.SignatureImgPath_3) {
              getImg(this.signForm.SignatureImgPath_3).then((response) => {
                this.signForm.SignatureImgPath_3 = URL.createObjectURL(response)
              })
            }
            if (this.signForm.SignatureImgPath_4) {
              getImg(this.signForm.SignatureImgPath_4).then((response) => {
                this.signForm.SignatureImgPath_4 = URL.createObjectURL(response)
              })
            }
            if (this.signForm.SignatureImgPath_5) {
              getImg(this.signForm.SignatureImgPath_5).then((response) => {
                this.signForm.SignatureImgPath_5 = URL.createObjectURL(response)
              })
            }
          }
        })
        .catch((error) => {
          console.log(error)
        })
    },
    clear() {
      this.$refs.signaturePad.clearSignature()
    },
    save(params) {
      const { data } = this.$refs.signaturePad.saveSignature()
      switch (params) {
        case '1':
          this.signForm.SignatureBase64_1 = data
          break
        case '2':
          this.signForm.SignatureBase64_2 = data
          this.sign2 = false
          break
      }
      this.centerDialogVisible = false
      this.$refs.signaturePad.clearSignature()
    },
    upload: function() {
      uploadSignatureImg(JSON.stringify(this.signForm))
        .then((response) => {
          this.$message({
            message: '已儲存 !',
            type: 'success'
          })
          this.findForm()
        })
        .catch((error) => {
          console.log(error)
        })
    },
    findForm: function() {
      findFormData(JSON.stringify(this.searchPDF))
        .then((response) => {
          this.form = response.ResData
          this.form.TBNO = this.signForm.TBNO = this.searchPDF.TBNO
          this.form.FName = this.signForm.FName = this.searchPDF.FName
          this.form.Vno = this.signForm.Vno = this.searchPDF.Vno
          // eslint-disable-next-line vue/no-mutating-props
          this.signForm.BAdd = this.searchPDF.BAdd = this.form.BAdd
          this.GetSignature()
          if (this.form.BAdd_1) {
            this.sendedform = {
              Add_1: this.form.BAdd_1,
              Add_1_1: this.form.BAdd_1_1,
              Add_1_2: this.form.BAdd_1_2,
              Add_2: this.form.BAdd_2,
              Add_2_1: this.form.BAdd_2_1,
              Add_2_2: this.form.BAdd_2_2,
              Add_2_3: this.form.BAdd_2_3,
              Add_2_4: this.form.BAdd_2_4,
              Add_3: this.form.BAdd_3,
              Add_3_1: this.form.BAdd_3_1,
              Add_3_2: this.form.BAdd_3_2,
              Add_4: this.form.BAdd_4,
              Add_5: this.form.BAdd_5,
              Add_6: this.form.BAdd_6,
              Add_7: this.form.BAdd_7,
              Add_8: this.form.BAdd_8,
              Add_9: this.form.BAdd_9
            }
          }
          if (this.form.LSAdd_1) {
            this.sendedform1 = {
              Add_1: this.form.LSAdd_1,
              Add_1_1: this.form.LSAdd_1_1,
              Add_1_2: this.form.LSAdd_1_2,
              Add_2: this.form.LSAdd_2,
              Add_2_1: this.form.LSAdd_2_1,
              Add_2_2: this.form.LSAdd_2_2,
              Add_2_3: this.form.LSAdd_2_3,
              Add_2_4: this.form.LSAdd_2_4,
              Add_3: this.form.LSAdd_3,
              Add_3_1: this.form.LSAdd_3_1,
              Add_3_2: this.form.LSAdd_3_2,
              Add_4: this.form.LSAdd_4,
              Add_5: this.form.LSAdd_5,
              Add_6: this.form.LSAdd_6,
              Add_7: this.form.LSAdd_7,
              Add_8: this.form.LSAdd_8,
              Add_9: this.form.LSAdd_9
            }
          }
        })
        .catch((error) => {
          console.log(error)
        })
    }
  }
}
</script>
<style>
.el-checkbox {
  margin-right: 0 !important;
  margin-left: 5px !important;
}
.text {
  font-size: 14px;
}
.valuetxt {
  font-weight: bolder;
  font-size: 16px;
}
.tmb {
  height: 72px;
  display: inline-block;
  vertical-align: middle;
}

.bg-purple {
  background: #eff6ff; /* #d3dce6;*/
}

.text item grid-content {
  border-radius: 4px;
  padding: 3px;
}
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

.item {
  padding: 5px 0;
}

.w40px {
  width: 40px !important;
}

.w60px {
  width: 60px !important;
}

.w80px {
  width: 80px !important;
}

.w100px {
  width: 100px !important;
}

.word-m-side {
  margin: 0 8px;
}

.inline-item {
  display: inline-block;
}

.mb-0 {
  margin-bottom: 0px !important;
}
</style>
