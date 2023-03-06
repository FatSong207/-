<template>
  <div>
    <el-form
      :model="form"
      label-width="90px"
      size="small"
    >
      <p>物件資料</p>
      <el-card class="box-card">
        <div class="text item">
          <el-form-item label="委託日期">
            <el-date-picker
              v-model="form.BZAuthStart"
              value-format="yyyy-MM-dd"
              type="date"
              class="w140px"
              placeholder="起始日"
            />
            <span style="margin: 0 12px">至</span>
            <el-date-picker
              v-model="form.BZAuthEnd"
              value-format="yyyy-MM-dd"
              type="date"
              class="w140px"
              placeholder="結止日"
            />
          </el-form-item>
        </div>
        <div class="text item">
          <Address
            title="出租地址"
            :sendedform="sendedform"
            @geteditaddress="geteditaddress"
          />
        </div>
        <div class="text item">
          <el-form-item label="社區名稱">
            <el-input
              v-model="form.BCommunity"
              class="w140px"
            />
          </el-form-item>
        </div>
        <div class="text item">
          <el-form-item
            label="權狀坪數"
            class="inline-item"
          >
            <el-input
              v-model="form.BpingV"
              class="w80px"
            />
          </el-form-item>
          <el-form-item
            label="實際坪數"
            class="inline-item"
          >
            <el-input
              v-model="form.BpingActualV"
              class="w80px"
            />
          </el-form-item>
          <el-form-item
            label="坪數"
            class="inline-item"
          >
            <el-input
              v-model="form.BpingActual"
              class="w80px"
            />
            <span class="word-m-side">元</span>
          </el-form-item>
        </div>
        <div class="text item">
          <el-form-item
            label="格局"
            :inline="true"
          >
            <el-input
              v-model="form.BRoom"
              class="w60px"
            />
            <span class="word-m-side">房</span>
            <el-input
              v-model="form.BLD"
              class="w60px"
            />
            <span class="word-m-side">廳</span>
            <el-input
              v-model="form.BBath"
              class="w60px"
            />
            <span class="word-m-side">衛浴</span>
            <el-input
              v-model="form.BKitchen"
              class="w60px"
            />
            <span class="word-m-side">廚房</span>
            <el-input
              v-model="form.BBalcony"
              class="w60px"
            />
            <span class="word-m-side">陽台</span>
          </el-form-item>
        </div>
        <div class="text item">
          <el-form-item label="建物型態">
            <el-checkbox
              v-model="form.BTApartment"
              :true-label="trueValue"
              :false-label="falseValue"
            >公寓</el-checkbox>
            <el-checkbox
              v-model="form.BTElevatorB"
              :true-label="trueValue"
              :false-label="falseValue"
            >電梯大樓</el-checkbox>
            <el-checkbox
              v-model="form.BTHouse"
              :true-label="trueValue"
              :false-label="falseValue"
            >透天</el-checkbox>
          </el-form-item>
        </div>
        <div class="text item">
          <el-form-item label="建物現況">
            <span class="word-m-side">電梯</span>
            <el-radio
              v-model="Belevator"
              label="Belevator_Y"
            >有</el-radio>
            <el-radio
              v-model="Belevator"
              label="Belevator_N"
            >無</el-radio>
            <span class="word-m-side">管理員</span>
            <el-radio
              v-model="Bsuper"
              label="Bsuper_Y"
            >有</el-radio>
            <el-radio
              v-model="Bsuper"
              label="Bsuper_N"
            >無</el-radio>
            <span class="word-m-side">單層戶數</span>
            <el-input
              v-model="form.Bhousehold"
              class="w60px"
            />
            <span class="word-m-side">座向</span>
            <el-input
              v-model="form.Bddirection"
              class="w100px"
            />
          </el-form-item>
        </div>
        <div class="text item">
          <el-form-item label="車位">
            <span class="word-m-side">第</span>
            <el-input
              v-model="form.BCarNo"
              class="w80px"
            />
            <span class="word-m-side">號 車位</span>
            <el-checkbox
              v-model="form.BAboveG"
              :true-label="trueValue"
              :false-label="falseValue"
            >地上</el-checkbox>
            <el-checkbox
              v-model="form.BUnderG"
              :true-label="trueValue"
              :false-label="falseValue"
            >地下</el-checkbox>
            <el-checkbox
              v-model="form.BRampParking"
              :true-label="trueValue"
              :false-label="falseValue"
            >坡道式</el-checkbox>
            <el-checkbox
              v-model="form.BLiftParking"
              :true-label="trueValue"
              :false-label="falseValue"
            >升降式
              <span class="word-m-side">第</span>
              <el-input
                v-model="form.BCarFloor"
                class="w60px"
              />
              <span class="word-m-side">層</span>
            </el-checkbox>
            <el-checkbox
              v-model="form.BPParking"
              :true-label="trueValue"
              :false-label="falseValue"
            >平面式</el-checkbox>
            <el-checkbox
              v-model="form.BMParking"
              :true-label="trueValue"
              :false-label="falseValue"
            >機械式</el-checkbox>
          </el-form-item>
        </div>
        <div class="text item">
          <el-form-item
            label="附屬設備"
            size="mini"
          >
            <div class="text item">
              <el-checkbox
                v-model="form.BZ002"
                :true-label="trueValue"
                :false-label="falseValue"
              >洗衣機</el-checkbox>
              <el-checkbox
                v-model="form.BZ009"
                :true-label="trueValue"
                :false-label="falseValue"
              >熱水器
                <el-checkbox
                  v-model="form.BZ010"
                  :true-label="trueValue"
                  :false-label="falseValue"
                >瓦斯</el-checkbox>
                <el-checkbox
                  v-model="form.BZ011"
                  :true-label="trueValue"
                  :false-label="falseValue"
                >電</el-checkbox>
              </el-checkbox>
              <el-checkbox
                v-model="form.BZ012"
                :true-label="trueValue"
                :false-label="falseValue"
              >網路</el-checkbox>
              <el-checkbox
                v-model="form.BZ013"
                :true-label="trueValue"
                :false-label="falseValue"
              >第四台</el-checkbox>
              <el-checkbox
                v-model="form.BZ014"
                :true-label="trueValue"
                :false-label="falseValue"
              >天然氣</el-checkbox>
              <el-checkbox
                v-model="form.BZ015"
                :true-label="trueValue"
                :false-label="falseValue"
              >流理台</el-checkbox>
              <el-checkbox
                v-model="form.BZ016"
                :true-label="trueValue"
                :false-label="falseValue"
              >瓦斯爐</el-checkbox>
              <el-checkbox
                v-model="form.BZ023"
                :true-label="trueValue"
                :false-label="falseValue"
              >桌子</el-checkbox>
              <el-checkbox
                v-model="form.BZ024"
                :true-label="trueValue"
                :false-label="falseValue"
              >椅子</el-checkbox>
            </div>
            <div class="text item">
              <el-checkbox
                v-model="form.BZ003"
                :true-label="trueValue"
                :false-label="falseValue"
              >冰箱
                <el-input
                  v-model="form.BZ004"
                  class="w60px"
                />
                <span class="word-m-side">台</span>
              </el-checkbox>
              <el-checkbox
                v-model="form.BZ005"
                :true-label="trueValue"
                :false-label="falseValue"
              >電視
                <el-input
                  v-model="form.BZ006"
                  class="w60px"
                />
                <span class="word-m-side">台</span>
              </el-checkbox>
              <el-checkbox
                v-model="form.BZ007"
                :true-label="trueValue"
                :false-label="falseValue"
              >冷氣
                <el-input
                  v-model="form.BZ008"
                  class="w60px"
                />
                <span class="word-m-side">台</span>
              </el-checkbox>

              <el-checkbox
                v-model="form.BZ017"
                :true-label="trueValue"
                :false-label="falseValue"
              >床組
                <el-input
                  v-model="form.BZ018"
                  class="w60px"
                />
                <span class="word-m-side">台</span>
              </el-checkbox>
              <el-checkbox
                v-model="form.BZ019"
                :true-label="trueValue"
                :false-label="falseValue"
              >衣櫃
                <el-input
                  v-model="form.BZ020"
                  class="w60px"
                />
                <span class="word-m-side">台</span>
              </el-checkbox>
              <el-checkbox
                v-model="form.BZ021"
                :true-label="trueValue"
                :false-label="falseValue"
              >沙發
                <el-input
                  v-model="form.BZ022"
                  class="w60px"
                />
                <span class="word-m-side">台</span>
              </el-checkbox>
            </div>
          </el-form-item>
        </div>
      </el-card>

      <el-row :gutter="20">
        <el-col :span="12">
          <p>費用資料</p>
          <el-card class="box-card">
            <div class="text item">
              <el-form-item
                label="月租金"
                class="inline-item"
              >
                <el-input
                  v-model="form.BTrent"
                  class="w80px"
                />
                <span class="word-m-side">元</span>
              </el-form-item>
              <el-form-item
                label="租押金"
                class="inline-item"
              >
                <el-input
                  v-model="form.BDeposit"
                  class="w80px"
                />
                <span class="word-m-side">元</span>
              </el-form-item>
            </div>
            <div class="text item">
              <el-form-item label="租金包含">
                <el-checkbox
                  v-model="form.BMFeeV"
                  :true-label="trueValue"
                  :false-label="falseValue"
                >管理費</el-checkbox>
                <el-checkbox
                  v-model="form.BCableTVFeeV"
                  :true-label="trueValue"
                  :false-label="falseValue"
                >第四台</el-checkbox>
                <el-checkbox
                  v-model="form.BGasFeeV"
                  :true-label="trueValue"
                  :false-label="falseValue"
                >瓦斯費</el-checkbox>
                <el-checkbox
                  v-model="form.BCleaningFeeV"
                  :true-label="trueValue"
                  :false-label="falseValue"
                >清潔費</el-checkbox>
                <el-checkbox
                  v-model="form.BInterNetFeeV"
                  :true-label="trueValue"
                  :false-label="falseValue"
                >網路</el-checkbox>
                <el-checkbox
                  v-model="form.BWaterBillV"
                  :true-label="trueValue"
                  :false-label="falseValue"
                >水費</el-checkbox>
                <el-checkbox
                  v-model="form.BElectricityBillV"
                  :true-label="trueValue"
                  :false-label="falseValue"
                >電費</el-checkbox>
              </el-form-item>
            </div>
            <div class="text item">
              <el-form-item
                label="車位租金"
                class="inline-item"
              >
                <el-input
                  v-model="form.BParkFee"
                  class="w80px"
                />
                <span class="word-m-side">元</span>
              </el-form-item>
              <el-form-item
                label="車位清潔費"
                class="inline-item"
              >
                <el-input
                  v-model="form.BParkCFee"
                  class="w80px"
                />
                <span class="word-m-side">元</span>
              </el-form-item>
            </div>
            <div class="text item">
              <el-form-item
                label="大樓管理費"
                class="inline-item"
              >
                <el-input
                  v-model="form.BMFee"
                  class="w80px"
                />
                <span class="word-m-side">元</span>
              </el-form-item>
              <el-form-item
                label="其他費用"
                class="inline-item"
              >
                <el-input
                  v-model="form.BZ001"
                  class="w80px"
                />
                <span class="word-m-side">元</span>
              </el-form-item>
            </div>
            <div class="text item">
              <el-form-item label="仲介費">
                <span class="word-m-side">一年</span>
                <el-input
                  v-model="form.BZ033"
                  class="w80px"
                />
                <span class="word-m-side">個月</span>
                <span class="word-m-side">二年</span>
                <el-input
                  v-model="form.BZ034"
                  class="w80px"
                />
                <span class="word-m-side">個月</span>
              </el-form-item>
            </div>
          </el-card>
        </el-col>
        <el-col :span="12">
          <p>委託需求</p>
          <el-card class="box-card">
            <div class="text item">
              <el-form-item label="最短租期">
                <el-input
                  v-model="form.BMiniLeaseP"
                  class="w80px"
                />
                <span class="word-m-side">年</span>
              </el-form-item>
            </div>
            <div class="text item">
              <el-form-item
                label="開伙"
                class="inline-item"
              >
                <el-radio
                  v-model="BCook"
                  label="BCook_Y"
                >可</el-radio>
                <el-radio
                  v-model="BCook"
                  label="BCook_N"
                >不可</el-radio>
              </el-form-item>
            </div>
            <div class="text item">
              <el-form-item
                label="寵物"
                class="inline-item"
              >
                <el-radio
                  v-model="BPet"
                  label="BPet_Y"
                >可</el-radio>
                <el-radio
                  v-model="BPet"
                  label="BPet_N"
                >不可</el-radio>
              </el-form-item>
            </div>
            <div class="text item">
              <el-form-item
                label="神明桌"
                class="inline-item"
              >
                <el-radio
                  v-model="BGodT"
                  label="BGodT_Y"
                >可</el-radio>
                <el-radio
                  v-model="BGodT"
                  label="BGodT_N"
                >不可</el-radio>
              </el-form-item>
            </div>
            <div class="text item">
              <el-form-item label="鑰匙">
                <el-select
                  v-model="form.keyOptions"
                  placeholder="請選擇"
                >
                  <el-option
                    v-for="item in keyOptions"
                    :key="item.value"
                    :label="item.label"
                    :value="item.value"
                  />
                </el-select>
                <el-input
                  v-model="form.BkeyCount"
                  class="w80px"
                />
                <span class="word-m-side">隻</span>
              </el-form-item>
            </div>
            <div class="text item">
              <el-form-item
                label="代收定金"
                class="inline-item"
              >
                <el-radio
                  v-model="BZ02526"
                  label="BZ025"
                >同意</el-radio>
                <el-radio
                  v-model="BZ02526"
                  label="BZ026"
                >不同意</el-radio>
              </el-form-item>
              <el-form-item
                label="定金轉為"
                class="inline-item"
              >
                <el-checkbox
                  v-model="form.BZ027"
                  :true-label="trueValue"
                  :false-label="falseValue"
                >仲介費</el-checkbox>
                <el-checkbox
                  v-model="form.BZ028"
                  :true-label="trueValue"
                  :false-label="falseValue"
                >租押金</el-checkbox>
              </el-form-item>
              <el-form-item
                label="超過部分"
                class="inline-item"
              >
                <el-checkbox
                  v-model="form.BZ029"
                  :true-label="trueValue"
                  :false-label="falseValue"
                >現金</el-checkbox>
                <el-checkbox
                  v-model="form.BZ030"
                  :true-label="trueValue"
                  :false-label="falseValue"
                >匯款</el-checkbox>
              </el-form-item>
            </div>
            <div class="text item">
              <el-form-item label="代為簽約">
                <el-radio
                  v-model="BZ03132"
                  label="BZ031"
                >同意</el-radio>
                <el-radio
                  v-model="BZ03132"
                  label="BZ032"
                >不同意</el-radio>
              </el-form-item>
            </div>
          </el-card>
        </el-col>
      </el-row>
      <p>委託人</p>
      <el-card class="box-card">
        <div class="text item">
          <el-form-item label="姓名">
            <el-input
              v-model="form.LSName"
              class="w100px"
            />
          </el-form-item>
        </div>
        <div class="text item">
          <el-form-item label="身分證字號/統一編號">
            <el-input
              v-model="form.LSID"
              class="w140px"
            />
          </el-form-item>
        </div>
        <div class="text item">
          <el-form-item label="電話">
            <el-input
              v-model="form.LSTel_1"
              class="w60px"
            />
            <span class="word-m-side">-</span>
            <el-input
              v-model="form.LSTel_2"
              class="w140px"
            />
          </el-form-item>
        </div>
        <div class="text item">
          <Address
            title="地址"
            :sendedform="sendedform1"
            @geteditaddress="geteditaddress"
          />
        </div>
      </el-card>
      <div style="margin-top: 20px; text-align: center">
        <el-button
          type="info"
          @click="findForm()"
        >重新載入</el-button>
        <el-button
          type="primary"
          @click="upload()"
        >存檔</el-button>
      </div>
    </el-form>
  </div>
</template>
<script>
import {
  findFormData,
  saveFomrData
} from '@/api/chaochi/externalform/externalformservice'
import Address from '@/components/Address/Address.vue'
export default {
  name: 'InternalFormDetailA000201',
  components: { Address },
  props: {
    searchPDF: { type: Object, default: null }
  },
  data() {
    return {
      currentComponent: null,
      trueValue: '/Yes',
      falseValue: '/Off',
      sendedform: {},
      sendedform1: {},
      keyOptions: [
        {
          value: 'BOpen',
          label: '親自開門'
        },
        {
          value: 'BKeyFM',
          label: '鑰匙留置管理室'
        },
        {
          value: 'BKeyFR',
          label: '給付乙方鑰匙/磁釦'
        }
      ],
      Belevator: 'Belevator_Y',
      Bsuper: 'Bsuper_Y',
      BCook: 'BCook_Y',
      BPet: 'BPet_Y',
      BGodT: 'BGodT_Y',
      BZ02526: 'BZ025',
      BZ03132: 'BZ031',
      centerDialogVisible: false,
      form: {
        TBNO: '',
        FName: 'A000201',
        Vno: '01',
        LCID: '',
        keyOptions: '',
        BCommunity: '',
        BpingV: '',
        BpingActualV: '',
        BpingActual: '',
        BRoom: '',
        BLD: '',
        BBath: '',
        BKitchen: '',
        BBalcony: '',
        BTApartment: '',
        BTElevatorB: '',
        BTHouse: '',
        Belevator_Y: '',
        Belevator_N: '',
        Bsuper_Y: '',
        Bsuper_N: '',
        Bhousehold: '',
        Bddirection: '',
        BCarNo: '',
        BAboveG: '',
        BUnderG: '',
        BRampParking: '',
        BLiftParking: '',
        BCarFloor: '',
        BPParking: '',
        BMParking: '',
        BTrent: '',
        BDeposit: '',
        BMFeeV: '',
        BCleaningFeeV: '',
        BCableTVFeeV: '',
        BInterNetFeeV: '',
        BWaterBillV: '',
        BElectricityBillV: '',
        BGasFeeV: '',
        BParkFee: '',
        BParkCFee: '',
        BMFee: '',
        BZ001: '',
        BZ002: '',
        BZ003: '',
        BZ004: '',
        BZ005: '',
        BZ006: '',
        BZ007: '',
        BZ008: '',
        BZ009: '',
        BZ010: '',
        BZ011: '',
        BZ012: '',
        BZ013: '',
        BZ014: '',
        BZ015: '',
        BZ016: '',
        BZ017: '',
        BZ018: '',
        BZ019: '',
        BZ020: '',
        BZ021: '',
        BZ022: '',
        BZ023: '',
        BZ024: '',
        BMiniLeaseP: '',
        BCook_Y: '',
        BCook_N: '',
        BPet_Y: '',
        BPet_N: '',
        BGodT_Y: '',
        BGodT_N: '',
        BOpen: '',
        BKeyFM: '',
        BKeyFR: '',
        BkeyCount: '',
        BZ025: '',
        BZ026: '',
        BZ027: '',
        BZ028: '',
        BZ029: '',
        BZ030: '',
        BZ031: '',
        BZ032: '',
        BZ033: '',
        BZ034: '',
        LSName: '',
        LSID: '',
        LSTel_1: '',
        LSTel_2: '',
        BUnitName: '',
        BAuthStart_Y: '',
        BAuthStart_M: '',
        BAuthStart_D: '',
        BAuthEnd_Y: '',
        BAuthEnd_M: '',
        BAuthEnd_D: '',
        BAdd_1: '',
        BAdd_1_1: '',
        BAdd_1_2: '',
        BAdd_2: '',
        BAdd_2_1: '',
        BAdd_2_2: '',
        BAdd_2_3: '',
        BAdd_2_4: '',
        BAdd_3: '',
        BAdd_3_1: '',
        BAdd_3_2: '',
        BAdd_4: '',
        BAdd_5: '',
        BAdd_6: '',
        BAdd_7: '',
        BAdd_8: '',
        BAdd_9: '',
        LSAdd_1: '',
        LSAdd_1_1: '',
        LSAdd_1_2: '',
        LSAdd_2: '',
        LSAdd_2_1: '',
        LSAdd_2_2: '',
        LSAdd_2_3: '',
        LSAdd_2_4: '',
        LSAdd_3: '',
        LSAdd_3_1: '',
        LSAdd_3_2: '',
        LSAdd_4: '',
        LSAdd_5: '',
        LSAdd_6: '',
        LSAdd_7: '',
        LSAdd_8: '',
        LSAdd_9: ''
      }
    }
  },
  methods: {
    geteditaddress(addressData, title) {
      if (title === '出租地址') {
        this.form.BAdd_1 = addressData.Add_1
        this.form.BAdd_1_1 = addressData.Add_1_1
        this.form.BAdd_1_2 = addressData.Add_1_2
        this.form.BAdd_2 = addressData.Add_2
        this.form.BAdd_2_1 = addressData.Add_2_1
        this.form.BAdd_2_2 = addressData.Add_2_2
        this.form.BAdd_2_3 = addressData.Add_2_3
        this.form.BAdd_2_4 = addressData.Add_2_4
        this.form.BAdd_3 = addressData.Add_3
        this.form.BAdd_3_1 = addressData.Add_3_1
        this.form.BAdd_3_2 = addressData.Add_3_2
        this.form.BAdd_4 = addressData.Add_4
        this.form.BAdd_5 = addressData.Add_5
        this.form.BAdd_6 = addressData.Add_6
        this.form.BAdd_7 = addressData.Add_7
        this.form.BAdd_8 = addressData.Add_8
        this.form.BAdd_9 = addressData.Add_9
      } else if (title === '地址') {
        this.form.LSAdd_1 = addressData.Add_1
        this.form.LSAdd_1_1 = addressData.Add_1_1
        this.form.LSAdd_1_2 = addressData.Add_1_2
        this.form.LSAdd_2 = addressData.Add_2
        this.form.LSAdd_2_1 = addressData.Add_2_1
        this.form.LSAdd_2_2 = addressData.Add_2_2
        this.form.LSAdd_2_3 = addressData.Add_2_3
        this.form.LSAdd_2_4 = addressData.Add_2_4
        this.form.LSAdd_3 = addressData.Add_3
        this.form.LSAdd_3_1 = addressData.Add_3_1
        this.form.LSAdd_3_2 = addressData.Add_3_2
        this.form.LSAdd_4 = addressData.Add_4
        this.form.LSAdd_5 = addressData.Add_5
        this.form.LSAdd_6 = addressData.Add_6
        this.form.LSAdd_7 = addressData.Add_7
        this.form.LSAdd_8 = addressData.Add_8
        this.form.LSAdd_9 = addressData.Add_9
      }
    },
    IsSameCheckVal(val, val2) {
      return val === val2 ? '/Yes' : '/Off'
    },
    upload: function() {
      this.form.Belevator_Y = this.IsSameCheckVal(this.Belevator, 'Belevator_Y')
      this.form.Belevator_N = this.IsSameCheckVal(this.Belevator, 'Belevator_N')

      this.form.Bsuper_Y = this.IsSameCheckVal(this.Bsuper, 'Bsuper_Y')
      this.form.Bsuper_N = this.IsSameCheckVal(this.Bsuper, 'Bsuper_N')

      this.form.BCook_Y = this.IsSameCheckVal(this.BCook, 'BCook_Y')
      this.form.BCook_N = this.IsSameCheckVal(this.BCook, 'BCook_N')

      this.form.BPet_Y = this.IsSameCheckVal(this.BPet, 'BPet_Y')
      this.form.BPet_N = this.IsSameCheckVal(this.BPet, 'BPet_N')

      this.form.BGodT_Y = this.IsSameCheckVal(this.BGodT, 'BGodT_Y')
      this.form.BGodT_N = this.IsSameCheckVal(this.BGodT, 'BGodT_N')

      this.form.BZ025 = this.IsSameCheckVal(this.BZ02526, 'BZ025')
      this.form.BZ026 = this.IsSameCheckVal(this.BZ02526, 'BZ026')

      this.form.BZ031 = this.IsSameCheckVal(this.BZ03132, 'BZ031')
      this.form.BZ032 = this.IsSameCheckVal(this.BZ03132, 'BZ032')

      if (this.form.BZAuthStart) {
        this.form.BAuthStart_Y = this.form.BZAuthStart.substr(0, 4)
        this.form.BAuthStart_M = this.form.BZAuthStart.substr(5, 2)
        this.form.BAuthStart_D = this.form.BZAuthStart.substr(8, 2)
      }
      if (this.form.BZAuthEnd) {
        this.form.BAuthEnd_Y = this.form.BZAuthEnd.substr(0, 4)
        this.form.BAuthEnd_M = this.form.BZAuthEnd.substr(5, 2)
        this.form.BAuthEnd_D = this.form.BZAuthEnd.substr(8, 2)
      }

      saveFomrData(JSON.stringify(this.form))
        .then(response => {
          this.$message({
            message: '已儲存 !',
            type: 'success'
          })
        })
        .catch(error => {
          console.log(error)
        })
    },
    findForm: function() {
      findFormData(JSON.stringify(this.searchPDF))
        .then(response => {
          this.form = response.ResData
          this.form.TBNO = this.searchPDF.TBNO
          this.form.FName = this.searchPDF.FName
          this.form.Vno = this.searchPDF.Vno
          console.log('findFormData>>' + JSON.stringify(this.form))
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
        .catch(error => {
          console.log(error)
        })
    }
  }
}
</script>
<style>
.text {
  font-size: 14px;
}

.item {
  padding: 3px 0;
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
.w-80 {
  width: 80% !important;
}

.w100px {
  width: 100px !important;
}

.w140px {
  width: 140px !important;
}
.word-m-side {
  margin: 0 8px;
}

.inline-item {
  display: inline-block;
}
</style>
