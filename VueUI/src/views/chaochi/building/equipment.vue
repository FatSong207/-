<template>
  <div>
    <div style="width: 100%">
      <div class="list-btn-container">
        <el-button-group>
          <!-- <el-button
            type="primary"
            size="small"
            icon="el-icon-paperclip"
            @click="saveBuildingEquipment()"
          >儲 存</el-button> -->
          <el-button
            type="primary"
            icon="el-icon-tickets"
            size="small"
            @click="ExportToPDF()"
          >產生PDF</el-button>
        </el-button-group>
      </div>
    </div>
    <el-form
      ref="editform"
      v-loading.fullscreen.lock="saveLoading"
      v-loading="saveLoading"
      :element-loading-text="loadingMsg"
      :inline="true"
      class="demo-form-inline"
    >
      <el-form-item>
        <h2>主要家電設備</h2>
        <el-table
          :data="homeEletableData"
          border
          stripe
        >
          <el-table-column
            prop="EqName"
            :resizable="false"
            label="設備設施"
            width="100"
          />
          <el-table-column
            prop="EqCount"
            :resizable="false"
            label="數量"
            width="90"
          >
            <template slot-scope="scope">
              <el-input
                v-model="scope.row.EqCount"
                type="number"
                class="no-number"
                :min="0"
                style="width: 80%"
                show-word-limit
                onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
              />
            </template>
          </el-table-column>
          <el-table-column
            prop="EqBrand"
            :resizable="false"
            label="品牌"
            width="150"
          >
            <template slot-scope="scope">
              <el-input
                v-model="scope.row.EqBrand"
                style="width: 90%"
              />
            </template>
          </el-table-column>
        </el-table>
      </el-form-item>
      <el-form-item>
        <h2>衛浴設備</h2>
        <el-table
          :data="bathtableData"
          border
          stripe
        >
          <el-table-column
            prop="EqName"
            :resizable="false"
            label="設備設施"
            width="100"
          />
          <el-table-column
            prop="EqCount"
            :resizable="false"
            label="數量"
            width="100"
          >
            <template slot-scope="scope">
              <el-input
                v-model="scope.row.EqCount"
                type="number"
                class="no-number"
                style="width: 70px"
                onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
              />
            </template>
          </el-table-column>
        </el-table>
      </el-form-item>
      <el-form-item>
        <h2>廚房設備</h2>
        <el-table
          :data="kitchentableData"
          border
          stripe
        >
          <el-table-column
            prop="EqName"
            :resizable="false"
            label="設備設施"
            width="100"
          />
          <el-table-column
            prop="EqCount"
            :resizable="false"
            label="數量"
            width="100"
          >
            <template slot-scope="scope">
              <el-input
                v-model="scope.row.EqCount"
                type="number"
                class="no-number"
                style="width: 70px"
                onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
              />
            </template>
          </el-table-column>
        </el-table>
      </el-form-item>
      <el-form-item>
        <h2>客餐廳設備</h2>
        <el-table
          :data="livingDiningtableData"
          border
          stripe
        >
          <el-table-column
            prop="EqName"
            :resizable="false"
            label="設備設施"
            width="100"
          />
          <el-table-column
            prop="EqCount"
            :resizable="false"
            label="數量"
            width="100"
          >
            <template slot-scope="scope">
              <el-input
                v-model="scope.row.EqCount"
                type="number"
                class="no-number"
                style="width: 70px"
                onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
              />
            </template>
          </el-table-column>
        </el-table>
      </el-form-item>
      <el-form-item>
        <h2>臥室設備</h2>
        <el-table
          :data="bedRoomtableData"
          border
          stripe
        >
          <el-table-column
            prop="EqName"
            :resizable="false"
            label="設備設施"
            width="100"
          />
          <el-table-column
            prop="EqCount"
            :resizable="false"
            label="數量"
            width="100"
          >
            <template slot-scope="scope">
              <el-input
                v-model="scope.row.EqCount"
                type="number"
                class="no-number"
                style="width: 70px"
                onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
              />
            </template>
          </el-table-column>
        </el-table>
      </el-form-item>
      <el-form-item>
        <h2>消防設備</h2>
        <el-table
          :data="firetableData"
          border
          stripe
        >
          <el-table-column
            prop="EqName"
            :resizable="false"
            label="設備設施"
            width="100"
          />
          <el-table-column
            prop="EqCount"
            :resizable="false"
            label="數量"
            width="100"
          >
            <template slot-scope="scope">
              <el-input
                v-model="scope.row.EqCount"
                type="number"
                class="no-number"
                style="width: 70px"
                onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
              />
            </template>
          </el-table-column>
        </el-table>
      </el-form-item>
      <el-form-item>
        <h2>其他設備</h2>
        <el-table
          :data="othertableData"
          border
          stripe
        >
          <el-table-column
            prop="EqName"
            :resizable="false"
            label="設備設施"
            width="100"
          />
          <el-table-column
            prop="EqCount"
            :resizable="false"
            label="數量"
            width="100"
          >
            <template slot-scope="scope">
              <el-input
                v-model="scope.row.EqCount"
                type="number"
                class="no-number"
                style="width: 70px"
                onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
              />
            </template>
          </el-table-column>
        </el-table>
      </el-form-item>

      <!-- <div style="display: flex">
        <div style="float: left;width: 19%; padding-right: 15px">
          <h2>主要家電設備</h2>
          <el-table
            :data="homeEletableData"
            border
            stripe
          >
            <el-table-column
              prop="EqName"
              :resizable="false"
              label="設備設施"
              width="120"
            />
            <el-table-column
              prop="EqCount"
              :resizable="false"
              label="數量"
              width="100"
            >
              <template slot-scope="scope">
                <el-input
                  v-model="scope.row.EqCount"
                  type="number"
                  class="no-number"
                  :min="0"
                  style="width: 80%"
                  show-word-limit
                  onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                />
              </template>
            </el-table-column>
            <el-table-column
              prop="EqBrand"
              :resizable="false"
              label="品牌"
            >
              <template slot-scope="scope">
                <el-input
                  v-model="scope.row.EqBrand"
                  style="width: 90%"
                />
              </template>
            </el-table-column>
          </el-table>
        </div>
        <div style="float: left;width: 13%; padding-right: 15px">
          <h2>衛浴設備</h2>
          <el-table
            :data="bathtableData"
            border
            stripe
          >
            <el-table-column
              prop="EqName"
              :resizable="false"
              label="設備設施"
              width="120"
            />
            <el-table-column
              prop="EqCount"
              :resizable="false"
              label="數量"
            >
              <template slot-scope="scope">
                <el-input
                  v-model="scope.row.EqCount"
                  type="number"
                  class="no-number"
                  style="width: 70px"
                  onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                />
              </template>
            </el-table-column>
          </el-table>
        </div>
        <div style="float: left;width: 13%; padding-right: 15px">
          <h2>廚房設備</h2>
          <el-table
            :data="kitchentableData"
            border
            stripe
          >
            <el-table-column
              prop="EqName"
              :resizable="false"
              label="設備設施"
              width="120"
            />
            <el-table-column
              prop="EqCount"
              :resizable="false"
              label="數量"
            >
              <template slot-scope="scope">
                <el-input
                  v-model="scope.row.EqCount"
                  type="number"
                  class="no-number"
                  style="width: 70px"
                  onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
                />
              </template>
            </el-table-column>
          </el-table>
        </div>
      </div> -->
      <!-- <div>
        <div style="float: left;width: 13%; padding-right: 15px">

        </div>
        <div style="float: left;width: 13%; padding-right: 15px">

        </div>
        <div style="float: left;width: 13%; padding-right: 15px">

        </div>
        <div style="float: left;width: 13%; padding-right: 10px">

        </div>
      </div> -->
      <div>
        <h2>其餘未記載項目</h2>
        <el-form-item
          :resizable="false"
          label=""
          width="120"
        >
          <el-input
            v-model="OtherDevices"
            type="textarea"
            placeholder="請輸入其餘未記載項目"
            autocomplete="off"
            clearable
            maxlength="80"
            show-word-limit
            :rows="3"
            style="width: 500px"
          />
        </el-form-item>
      </div>
    </el-form>
    <div class="rightbtn">
      <el-button
        v-hasPermi="['Building/Edit']"
        type="primary"
        size="small"
        icon="el-icon-paperclip"
        @click="saveBuildingEquipment()"
      >儲存</el-button>
      <el-button
        size="small"
        icon="el-icon-close"
        @click="cancel"
      >關閉</el-button>
    </div>
  </div>
</template>

<script>
import { SaveBEq, GetEq } from '@/api/chaochi/building/buildingservice';
import { BeqExportToPDF } from '@/api/chaochi/externalform/externalformservice';
export default {
  name: 'Equipment',
  props: {
    badd: { type: String, default: null }
  },
  data() {
    return {
      saveLoading: false,
      exportLoading: false,
      loadingMsg: '儲存中...',
      radio: '1',
      OtherDevices: '',
      alldata: [],
      homeEletableData: [],
      homeEletemplatetableData: [
        {
          EqName: '分離式冷氣',
          Category: 'homeEle',
          EqCount: '',
          EqBrand: '',
          Sort: '1'
        },
        {
          EqName: '窗型冷氣',
          Category: 'homeEle',
          EqCount: '',
          EqBrand: '',
          Sort: '2'
        },
        {
          EqName: '電視機',
          Category: 'homeEle',
          EqCount: '',
          EqBrand: '',
          Sort: '3'
        },
        {
          EqName: '電冰箱',
          Category: 'homeEle',
          EqCount: '',
          EqBrand: '',
          Sort: '4'
        },
        {
          EqName: '瓦斯爐',
          Category: 'homeEle',
          EqCount: '',
          EqBrand: '',
          Sort: '5'
        },
        {
          EqName: '抽油煙機',
          Category: 'homeEle',
          EqCount: '',
          EqBrand: '',
          Sort: '6'
        },
        {
          EqName: '電熱水器',
          Category: 'homeEle',
          EqCount: '',
          EqBrand: '',
          Sort: '7'
        },
        {
          EqName: '瓦斯熱水器',
          Category: 'homeEle',
          EqCount: '',
          EqBrand: '',
          Sort: '8'
        },
        {
          EqName: '洗衣機',
          Category: 'homeEle',
          EqCount: '',
          EqBrand: '',
          Sort: '9'
        }
      ],
      bathtableData: [],
      bathtemplatetableData: [
        {
          EqName: '一般馬桶',
          Category: 'bath',
          EqCount: '',
          EqBrand: '',
          Sort: '1'
        },
        {
          EqName: '免治馬桶',
          Category: 'bath',
          EqCount: '',
          EqBrand: '',
          Sort: '2'
        },
        {
          EqName: '抽風機',
          Category: 'bath',
          EqCount: '',
          EqBrand: '',
          Sort: '3'
        },
        {
          EqName: '暖風機',
          Category: 'bath',
          EqCount: '',
          EqBrand: '',
          Sort: '4'
        },
        {
          EqName: '水龍頭',
          Category: 'bath',
          EqCount: '',
          EqBrand: '',
          Sort: '5'
        },
        {
          EqName: '洗臉盆',
          Category: 'bath',
          EqCount: '',
          EqBrand: '',
          Sort: '6'
        },
        {
          EqName: '浴鏡組',
          Category: 'bath',
          EqCount: '',
          EqBrand: '',
          Sort: '7'
        },
        {
          EqName: '浴櫃',
          Category: 'bath',
          EqCount: '',
          EqBrand: '',
          Sort: '8'
        },
        {
          EqName: '淋浴龍頭',
          Category: 'bath',
          EqCount: '',
          EqBrand: '',
          Sort: '9'
        },
        {
          EqName: '蓮蓬頭',
          Category: 'bath',
          EqCount: '',
          EqBrand: '',
          Sort: '10'
        },
        {
          EqName: '浴缸',
          Category: 'bath',
          EqCount: '',
          EqBrand: '',
          Sort: '11'
        },
        {
          EqName: '淋浴拉門',
          Category: 'bath',
          EqCount: '',
          EqBrand: '',
          Sort: '12'
        },
        {
          EqName: '毛巾架',
          Category: 'bath',
          EqCount: '',
          EqBrand: '',
          Sort: '13'
        }
      ],
      kitchentableData: [],
      kitchentemplatetableData: [
        {
          EqName: '流理臺',
          Category: 'kitchen',
          EqCount: '',
          EqBrand: '',
          Sort: '1'
        },
        {
          EqName: '水龍頭',
          Category: 'kitchen',
          EqCount: '',
          EqBrand: '',
          Sort: '2'
        },
        {
          EqName: '電器櫃',
          Category: 'kitchen',
          EqCount: '',
          EqBrand: '',
          Sort: '3'
        },
        {
          EqName: '廚櫃',
          Category: 'kitchen',
          EqCount: '',
          EqBrand: '',
          Sort: '4'
        },
        {
          EqName: '電磁爐',
          Category: 'kitchen',
          EqCount: '',
          EqBrand: '',
          Sort: '5'
        },
        {
          EqName: '微波爐',
          Category: 'kitchen',
          EqCount: '',
          EqBrand: '',
          Sort: '6'
        },
        {
          EqName: '烤箱',
          Category: 'kitchen',
          EqCount: '',
          EqBrand: '',
          Sort: '7'
        },
        {
          EqName: '電鍋',
          Category: 'kitchen',
          EqCount: '',
          EqBrand: '',
          Sort: '8'
        },
        {
          EqName: '烘碗機',
          Category: 'kitchen',
          EqCount: '',
          EqBrand: '',
          Sort: '9'
        },
        {
          EqName: '洗碗機',
          Category: 'kitchen',
          EqCount: '',
          EqBrand: '',
          Sort: '10'
        },
        {
          EqName: '淨水器',
          Category: 'kitchen',
          EqCount: '',
          EqBrand: '',
          Sort: '11'
        }
      ],
      livingDiningtableData: [],
      livingDiningtemplatetableData: [
        {
          EqName: '單人沙發',
          Category: 'livingDining',
          EqCount: '',
          EqBrand: '',
          Sort: '1'
        },
        {
          EqName: '雙人沙發',
          Category: 'livingDining',
          EqCount: '',
          EqBrand: '',
          Sort: '2'
        },
        {
          EqName: '三人(以上)沙發',
          Category: 'livingDining',
          EqCount: '',
          EqBrand: '',
          Sort: '3'
        },
        {
          EqName: '茶几',
          Category: 'livingDining',
          EqCount: '',
          EqBrand: '',
          Sort: '4'
        },
        {
          EqName: '矮凳',
          Category: 'livingDining',
          EqCount: '',
          EqBrand: '',
          Sort: '5'
        },
        {
          EqName: '鞋櫃',
          Category: 'livingDining',
          EqCount: '',
          EqBrand: '',
          Sort: '6'
        },
        {
          EqName: '電視櫃',
          Category: 'livingDining',
          EqCount: '',
          EqBrand: '',
          Sort: '7'
        },
        {
          EqName: '餐桌',
          Category: 'livingDining',
          EqCount: '',
          EqBrand: '',
          Sort: '8'
        },
        {
          EqName: '餐椅',
          Category: 'livingDining',
          EqCount: '',
          EqBrand: '',
          Sort: '9'
        },
        {
          EqName: '室外大門',
          Category: 'livingDining',
          EqCount: '',
          EqBrand: '',
          Sort: '10'
        },
        {
          EqName: '室內門',
          Category: 'livingDining',
          EqCount: '',
          EqBrand: '',
          Sort: '11'
        },
        {
          EqName: '保全設施',
          Category: 'livingDining',
          EqCount: '',
          EqBrand: '',
          Sort: '12'
        },
        {
          EqName: '室內照明(顆)',
          Category: 'livingDining',
          EqCount: '',
          EqBrand: '',
          Sort: '13'
        },
        {
          EqName: '電風扇',
          Category: 'livingDining',
          EqCount: '',
          EqBrand: '',
          Sort: '14'
        }
      ],
      bedRoomtableData: [],
      bedRoomtemplatetableData: [
        {
          EqName: '窗簾',
          Category: 'bedRoom',
          EqCount: '',
          EqBrand: '',
          Sort: '1'
        },
        {
          EqName: '衣櫃',
          Category: 'bedRoom',
          EqCount: '',
          EqBrand: '',
          Sort: '2'
        },
        {
          EqName: '置物櫃',
          Category: 'bedRoom',
          EqCount: '',
          EqBrand: '',
          Sort: '3'
        },
        {
          EqName: '床頭櫃',
          Category: 'bedRoom',
          EqCount: '',
          EqBrand: '',
          Sort: '4'
        },
        {
          EqName: '梳妝台',
          Category: 'bedRoom',
          EqCount: '',
          EqBrand: '',
          Sort: '5'
        },
        {
          EqName: '書桌',
          Category: 'bedRoom',
          EqCount: '',
          EqBrand: '',
          Sort: '6'
        },
        {
          EqName: '椅子',
          Category: 'bedRoom',
          EqCount: '',
          EqBrand: '',
          Sort: '7'
        },
        {
          EqName: '單人床(組)',
          Category: 'bedRoom',
          EqCount: '',
          EqBrand: '',
          Sort: '8'
        },
        {
          EqName: '雙人床(組)',
          Category: 'bedRoom',
          EqCount: '',
          EqBrand: '',
          Sort: '9'
        }
      ],
      firetableData: [],
      firetemplatetableData: [
        {
          EqName: '滅火器',
          Category: 'fire',
          EqCount: '',
          EqBrand: '',
          Sort: '1'
        },
        {
          EqName: '偵煙警報器',
          Category: 'fire',
          EqCount: '',
          EqBrand: '',
          Sort: '2'
        },
        {
          EqName: '瓦斯警報器',
          Category: 'fire',
          EqCount: '',
          EqBrand: '',
          Sort: '3'
        },
        {
          EqName: '緊急照明燈',
          Category: 'fire',
          EqCount: '',
          EqBrand: '',
          Sort: '4'
        }
      ],
      othertableData: [],
      othertemplatetableData: [
        {
          EqName: '大樓鑰匙',
          Category: 'other',
          EqCount: '',
          EqBrand: '',
          Sort: '1'
        },
        {
          EqName: '室外大門鑰匙',
          Category: 'other',
          EqCount: '',
          EqBrand: '',
          Sort: '2'
        },
        {
          EqName: '室內門鑰匙',
          Category: 'other',
          EqCount: '',
          EqBrand: '',
          Sort: '3'
        },
        {
          EqName: '信箱鑰匙',
          Category: 'other',
          EqCount: '',
          EqBrand: '',
          Sort: '4'
        },
        {
          EqName: '磁扣卡',
          Category: 'other',
          EqCount: '',
          EqBrand: '',
          Sort: '5'
        },
        {
          EqName: '電子門鎖',
          Category: 'other',
          EqCount: '',
          EqBrand: '',
          Sort: '6'
        },
        {
          EqName: '電視遙控器',
          Category: 'other',
          EqCount: '',
          EqBrand: '',
          Sort: '7'
        },
        {
          EqName: '冷氣遙控器',
          Category: 'other',
          EqCount: '',
          EqBrand: '',
          Sort: '8'
        },
        {
          EqName: '網路分享器',
          Category: 'other',
          EqCount: '',
          EqBrand: '',
          Sort: '9'
        },
        {
          EqName: '數位盒',
          Category: 'other',
          EqCount: '',
          EqBrand: '',
          Sort: '10'
        }
      ]
    };
  },
  watch: {
    badd: {
      immediate: true,
      handler(a) {
        if (a) {
          this.loadingMsg = '請稍後...';
          this.saveLoading = true;
          this.InitAllTemplateTableData();
          GetEq(this.badd)
            .then(res => {
              this.alldata = res.ResData.BuildingEqs;
              this.OtherDevices = res.ResData.OtherDevices;
              if (
                this.alldata.filter(x => x.Category === 'homeEle').length !== 0
              ) {
                this.homeEletableData = this.alldata.filter(
                  x => x.Category === 'homeEle'
                );
                this.homeEletableData.order;
              } else {
                this.homeEletableData = this.homeEletemplatetableData;
              }
              if (
                this.alldata.filter(x => x.Category === 'bath').length !== 0
              ) {
                this.bathtableData = this.alldata.filter(
                  x => x.Category === 'bath'
                );
              } else {
                this.bathtableData = this.bathtemplatetableData;
              }
              if (
                this.alldata.filter(x => x.Category === 'kitchen').length !== 0
              ) {
                this.kitchentableData = this.alldata.filter(
                  x => x.Category === 'kitchen'
                );
              } else {
                this.kitchentableData = this.kitchentemplatetableData;
              }
              if (
                this.alldata.filter(x => x.Category === 'livingDining')
                  .length !== 0
              ) {
                this.livingDiningtableData = this.alldata.filter(
                  x => x.Category === 'livingDining'
                );
              } else {
                this.livingDiningtableData = this.livingDiningtemplatetableData;
              }
              if (
                this.alldata.filter(x => x.Category === 'bedRoom').length !== 0
              ) {
                this.bedRoomtableData = this.alldata.filter(
                  x => x.Category === 'bedRoom'
                );
              } else {
                this.bedRoomtableData = this.bedRoomtemplatetableData;
              }
              if (
                this.alldata.filter(x => x.Category === 'fire').length !== 0
              ) {
                this.firetableData = this.alldata.filter(
                  x => x.Category === 'fire'
                );
              } else {
                this.firetableData = this.firetemplatetableData;
              }
              if (
                this.alldata.filter(x => x.Category === 'other').length !== 0
              ) {
                this.othertableData = this.alldata.filter(
                  x => x.Category === 'other'
                );
              } else {
                this.othertableData = this.othertemplatetableData;
              }
            })
            .finally(() => {
              this.saveLoading = false;
            });
        }
      }
    }
  },
  methods: {
    InitAllTemplateTableData() {
      this.homeEletemplatetableData = [
        {
          EqName: '分離式冷氣',
          Category: 'homeEle',
          EqCount: '',
          EqBrand: '',
          Sort: '1'
        },
        {
          EqName: '窗型冷氣',
          Category: 'homeEle',
          EqCount: '',
          EqBrand: '',
          Sort: '2'
        },
        {
          EqName: '電視機',
          Category: 'homeEle',
          EqCount: '',
          EqBrand: '',
          Sort: '3'
        },
        {
          EqName: '電冰箱',
          Category: 'homeEle',
          EqCount: '',
          EqBrand: '',
          Sort: '4'
        },
        {
          EqName: '瓦斯爐',
          Category: 'homeEle',
          EqCount: '',
          EqBrand: '',
          Sort: '5'
        },
        {
          EqName: '抽油煙機',
          Category: 'homeEle',
          EqCount: '',
          EqBrand: '',
          Sort: '6'
        },
        {
          EqName: '電熱水器',
          Category: 'homeEle',
          EqCount: '',
          EqBrand: '',
          Sort: '7'
        },
        {
          EqName: '瓦斯熱水器',
          Category: 'homeEle',
          EqCount: '',
          EqBrand: '',
          Sort: '8'
        },
        {
          EqName: '洗衣機',
          Category: 'homeEle',
          EqCount: '',
          EqBrand: '',
          Sort: '9'
        }
      ];
      this.bathtemplatetableData = [
        {
          EqName: '一般馬桶',
          Category: 'bath',
          EqCount: '',
          EqBrand: '',
          Sort: '1'
        },
        {
          EqName: '免治馬桶',
          Category: 'bath',
          EqCount: '',
          EqBrand: '',
          Sort: '2'
        },
        {
          EqName: '抽風機',
          Category: 'bath',
          EqCount: '',
          EqBrand: '',
          Sort: '3'
        },
        {
          EqName: '暖風機',
          Category: 'bath',
          EqCount: '',
          EqBrand: '',
          Sort: '4'
        },
        {
          EqName: '水龍頭',
          Category: 'bath',
          EqCount: '',
          EqBrand: '',
          Sort: '5'
        },
        {
          EqName: '洗臉盆',
          Category: 'bath',
          EqCount: '',
          EqBrand: '',
          Sort: '6'
        },
        {
          EqName: '浴鏡組',
          Category: 'bath',
          EqCount: '',
          EqBrand: '',
          Sort: '7'
        },
        {
          EqName: '浴櫃',
          Category: 'bath',
          EqCount: '',
          EqBrand: '',
          Sort: '8'
        },
        {
          EqName: '淋浴龍頭',
          Category: 'bath',
          EqCount: '',
          EqBrand: '',
          Sort: '9'
        },
        {
          EqName: '蓮蓬頭',
          Category: 'bath',
          EqCount: '',
          EqBrand: '',
          Sort: '10'
        },
        {
          EqName: '浴缸',
          Category: 'bath',
          EqCount: '',
          EqBrand: '',
          Sort: '11'
        },
        {
          EqName: '淋浴拉門',
          Category: 'bath',
          EqCount: '',
          EqBrand: '',
          Sort: '12'
        },
        {
          EqName: '毛巾架',
          Category: 'bath',
          EqCount: '',
          EqBrand: '',
          Sort: '13'
        }
      ];
      this.kitchentemplatetableData = [
        {
          EqName: '流理臺',
          Category: 'kitchen',
          EqCount: '',
          EqBrand: '',
          Sort: '1'
        },
        {
          EqName: '水龍頭',
          Category: 'kitchen',
          EqCount: '',
          EqBrand: '',
          Sort: '2'
        },
        {
          EqName: '電器櫃',
          Category: 'kitchen',
          EqCount: '',
          EqBrand: '',
          Sort: '3'
        },
        {
          EqName: '廚櫃',
          Category: 'kitchen',
          EqCount: '',
          EqBrand: '',
          Sort: '4'
        },
        {
          EqName: '電磁爐',
          Category: 'kitchen',
          EqCount: '',
          EqBrand: '',
          Sort: '5'
        },
        {
          EqName: '微波爐',
          Category: 'kitchen',
          EqCount: '',
          EqBrand: '',
          Sort: '6'
        },
        {
          EqName: '烤箱',
          Category: 'kitchen',
          EqCount: '',
          EqBrand: '',
          Sort: '7'
        },
        {
          EqName: '電鍋',
          Category: 'kitchen',
          EqCount: '',
          EqBrand: '',
          Sort: '8'
        },
        {
          EqName: '烘碗機',
          Category: 'kitchen',
          EqCount: '',
          EqBrand: '',
          Sort: '9'
        },
        {
          EqName: '洗碗機',
          Category: 'kitchen',
          EqCount: '',
          EqBrand: '',
          Sort: '10'
        },
        {
          EqName: '淨水器',
          Category: 'kitchen',
          EqCount: '',
          EqBrand: '',
          Sort: '11'
        }
      ];
      this.livingDiningtemplatetableData = [
        {
          EqName: '單人沙發',
          Category: 'livingDining',
          EqCount: '',
          EqBrand: '',
          Sort: '1'
        },
        {
          EqName: '雙人沙發',
          Category: 'livingDining',
          EqCount: '',
          EqBrand: '',
          Sort: '2'
        },
        {
          EqName: '三人(以上)沙發',
          Category: 'livingDining',
          EqCount: '',
          EqBrand: '',
          Sort: '3'
        },
        {
          EqName: '茶几',
          Category: 'livingDining',
          EqCount: '',
          EqBrand: '',
          Sort: '4'
        },
        {
          EqName: '矮凳',
          Category: 'livingDining',
          EqCount: '',
          EqBrand: '',
          Sort: '5'
        },
        {
          EqName: '鞋櫃',
          Category: 'livingDining',
          EqCount: '',
          EqBrand: '',
          Sort: '6'
        },
        {
          EqName: '電視櫃',
          Category: 'livingDining',
          EqCount: '',
          EqBrand: '',
          Sort: '7'
        },
        {
          EqName: '餐桌',
          Category: 'livingDining',
          EqCount: '',
          EqBrand: '',
          Sort: '8'
        },
        {
          EqName: '餐椅',
          Category: 'livingDining',
          EqCount: '',
          EqBrand: '',
          Sort: '9'
        },
        {
          EqName: '室外大門',
          Category: 'livingDining',
          EqCount: '',
          EqBrand: '',
          Sort: '10'
        },
        {
          EqName: '室內門',
          Category: 'livingDining',
          EqCount: '',
          EqBrand: '',
          Sort: '11'
        },
        {
          EqName: '保全設施',
          Category: 'livingDining',
          EqCount: '',
          EqBrand: '',
          Sort: '12'
        },
        {
          EqName: '室內照明(顆)',
          Category: 'livingDining',
          EqCount: '',
          EqBrand: '',
          Sort: '13'
        },
        {
          EqName: '電風扇',
          Category: 'livingDining',
          EqCount: '',
          EqBrand: '',
          Sort: '14'
        }
      ];
      this.bedRoomtemplatetableData = [
        {
          EqName: '窗簾',
          Category: 'bedRoom',
          EqCount: '',
          EqBrand: '',
          Sort: '1'
        },
        {
          EqName: '衣櫃',
          Category: 'bedRoom',
          EqCount: '',
          EqBrand: '',
          Sort: '2'
        },
        {
          EqName: '置物櫃',
          Category: 'bedRoom',
          EqCount: '',
          EqBrand: '',
          Sort: '3'
        },
        {
          EqName: '床頭櫃',
          Category: 'bedRoom',
          EqCount: '',
          EqBrand: '',
          Sort: '4'
        },
        {
          EqName: '梳妝台',
          Category: 'bedRoom',
          EqCount: '',
          EqBrand: '',
          Sort: '5'
        },
        {
          EqName: '書桌',
          Category: 'bedRoom',
          EqCount: '',
          EqBrand: '',
          Sort: '6'
        },
        {
          EqName: '椅子',
          Category: 'bedRoom',
          EqCount: '',
          EqBrand: '',
          Sort: '7'
        },
        {
          EqName: '單人床(組)',
          Category: 'bedRoom',
          EqCount: '',
          EqBrand: '',
          Sort: '8'
        },
        {
          EqName: '雙人床(組)',
          Category: 'bedRoom',
          EqCount: '',
          EqBrand: '',
          Sort: '9'
        }
      ];
      this.firetemplatetableData = [
        {
          EqName: '滅火器',
          Category: 'fire',
          EqCount: '',
          EqBrand: '',
          Sort: '1'
        },
        {
          EqName: '偵煙警報器',
          Category: 'fire',
          EqCount: '',
          EqBrand: '',
          Sort: '2'
        },
        {
          EqName: '瓦斯警報器',
          Category: 'fire',
          EqCount: '',
          EqBrand: '',
          Sort: '3'
        },
        {
          EqName: '緊急照明燈',
          Category: 'fire',
          EqCount: '',
          EqBrand: '',
          Sort: '4'
        }
      ];
      this.othertemplatetableData = [
        {
          EqName: '大樓鑰匙',
          Category: 'other',
          EqCount: '',
          EqBrand: '',
          Sort: '1'
        },
        {
          EqName: '室外大門鑰匙',
          Category: 'other',
          EqCount: '',
          EqBrand: '',
          Sort: '2'
        },
        {
          EqName: '室內門鑰匙',
          Category: 'other',
          EqCount: '',
          EqBrand: '',
          Sort: '3'
        },
        {
          EqName: '信箱鑰匙',
          Category: 'other',
          EqCount: '',
          EqBrand: '',
          Sort: '4'
        },
        {
          EqName: '磁扣卡',
          Category: 'other',
          EqCount: '',
          EqBrand: '',
          Sort: '5'
        },
        {
          EqName: '電子門鎖',
          Category: 'other',
          EqCount: '',
          EqBrand: '',
          Sort: '6'
        },
        {
          EqName: '電視遙控器',
          Category: 'other',
          EqCount: '',
          EqBrand: '',
          Sort: '7'
        },
        {
          EqName: '冷氣遙控器',
          Category: 'other',
          EqCount: '',
          EqBrand: '',
          Sort: '8'
        },
        {
          EqName: '網路分享器',
          Category: 'other',
          EqCount: '',
          EqBrand: '',
          Sort: '9'
        },
        {
          EqName: '數位盒',
          Category: 'other',
          EqCount: '',
          EqBrand: '',
          Sort: '10'
        }
      ];
    },
    cancel() {
      this.$emit('cancel');
    },
    saveBuildingEquipment() {
      this.loadingMsg = '儲存中...';
      this.saveLoading = true;
      var all = [];
      all = this.homeEletableData.concat(
        this.bathtableData,
        this.kitchentableData,
        this.livingDiningtableData,
        this.bedRoomtableData,
        this.firetableData,
        this.othertableData
      );
      var data = {
        BAdd: this.badd,
        OtherDevices: this.OtherDevices,
        BuildingEqs: all
      };

      SaveBEq(data).then(res => {
        console.log(res.Success);
        if (res.Success === true) {
          this.$message.success('儲存成功!');
        } else {
          this.$message.error('儲存失敗!');
        }
        this.saveLoading = false;
      });
    },
    ExportToPDF() {
      this.loadingMsg = '儲存並下載PDF中...';
      this.saveLoading = true;
      var all = [];
      all = this.homeEletableData.concat(
        this.bathtableData,
        this.kitchentableData,
        this.livingDiningtableData,
        this.bedRoomtableData,
        this.firetableData,
        this.othertableData
      );
      var data = {
        BAdd: this.badd,
        OtherDevices: this.OtherDevices,
        BuildingEqs: all
      };
      SaveBEq(data).then(res => {
        console.log(res.Success);
        if (res.Success === true) {
          BeqExportToPDF(this.badd)
            .then(res => {
              this.$message.success('PDF產生成功! 可至建物附件下載!');
              // const blob = res; // new Blob([response.data], { type: 'image/jpeg' })
              // const link = document.createElement('a');
              // link.href = URL.createObjectURL(blob);
              // link.download = '附屬設備清單';
              // link.click();
              // URL.revokeObjectURL(link.href);
            })
            .catch(() => {
              this.$message.error('失敗!');
            })
            .finally(() => {
              this.saveLoading = false;
            });
        } else {
          this.$message.error('儲存失敗!');
          this.saveLoading = false;
        }
      });
    }
  }
};
</script>

<style>
</style>
