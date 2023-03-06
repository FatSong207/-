<template>
  <el-form-item
    label=""
    prop="LNAdd_1"
  >
    <el-select
      v-model="county"
      style="width: 120px"
      placeholder="縣市"
    >
      <el-option
        v-for="item in countries"
        :key="item.Id"
        :label="item.value"
        :value="item.Id"
      />
    </el-select>
    {{ " " }}
    <el-select
      v-model="town"
      style="width: 120px"
      placeholder="鄉鎮市區"
    >
      <el-option
        v-for="item in towns2"
        :key="item.Index"
        :label="item.site_id"
        :value="item.site_id"
      />
    </el-select>
    {{ " " }}
    <!-- <el-select
      ref="select"
      v-model="street"
      filterable
      remote
      :remote-method="remoteMethod"
      :loading="loading"
      clearable
      placeholder="街路"
      style="width: 150px"
      @hook:mounted="cancelReadOnly"
      @visible-change="cancelReadOnly"
    > -->
    <el-select
      ref="select"
      v-model="street"
      filterable
      :loading="loading"
      placeholder="街路"
      style="width: 150px"
      @hook:mounted="cancelReadOnly"
      @visible-change="cancelReadOnly"
    >
      <el-option
        v-for="item in streets"
        :key="item.Index"
        :label="item.road"
        :value="item.road"
      />
    </el-select>
    {{ " " }}
    <el-select
      v-model="addressExceptCountyTown.section"
      style="width: 80px"
      placeholder="段"
      clearable
    >
      <el-option
        v-for="item in Section"
        :key="item.Index"
        :label="item.value"
        :value="item.value"
      />
    </el-select>
    <!-- <el-input
      v-model="addressExceptCountyTown.section"
      clearable
      style="width: 80px"
    /> -->
    段
    <el-input
      v-model="addressExceptCountyTown.lane"
      clearable
      style="width: 80px"
    />
    巷
    <el-input
      v-model="addressExceptCountyTown.alley"
      clearable
      style="width: 80px"
      class="no-number"
      type="number"
    />
    弄
    <el-input
      v-model="addressExceptCountyTown.number"
      clearable
      style="width: 140px"
    />
    號
    <el-input
      v-model="addressExceptCountyTown.floor"
      clearable
      style="width: 140px"
    />
    樓
    <el-input
      v-model="addressExceptCountyTown.room"
      clearable
      style="width: 140px"
    />
  </el-form-item>
</template>

<script>
import {
  getAllTowns,
  getAllStreets,
  GetStreetsByCityTown
} from '@/api/chaochi/OpenDataRoad/OpenDataRoadservice';
export default {
  name: 'Address',
  props: {
    sendedform: { type: Object, default: null },
    title: { type: String, default: '' },
    immediate: { type: Boolean, default: false },
    labelwidth: { type: String, default: '165px' },
    resetall: { type: Boolean, default: false }
  },
  data() {
    return {
      loading: false,
      county: '',
      town: '',
      street: '',
      addressExceptCountyTown: {
        section: '',
        lane: '',
        alley: '',
        number: '',
        floor: '',
        room: ''
      },
      countries: [
        { Id: '臺北市', value: '臺北市' },
        { Id: '新北市', value: '新北市' },
        { Id: '桃園市', value: '桃園市' },
        { Id: '臺中市', value: '臺中市' },
        { Id: '臺南市', value: '臺南市' },
        { Id: '高雄市', value: '高雄市' },
        { Id: '基隆市', value: '基隆市' },
        { Id: '新竹市', value: '新竹市' },
        { Id: '嘉義市', value: '嘉義市' },
        { Id: '新竹縣', value: '新竹縣' },
        { Id: '苗栗縣', value: '苗栗縣' },
        { Id: '彰化縣', value: '彰化縣' },
        { Id: '南投縣', value: '南投縣' },
        { Id: '雲林縣', value: '雲林縣' },
        { Id: '嘉義縣', value: '嘉義縣' },
        { Id: '屏東縣', value: '屏東縣' },
        { Id: '宜蘭縣', value: '宜蘭縣' },
        { Id: '花蓮縣', value: '花蓮縣' },
        { Id: '臺東縣', value: '臺東縣' },
        { Id: '澎湖縣', value: '澎湖縣' },
        { Id: '金門縣', value: '金門縣' },
        { Id: '連江縣', value: '連江縣' }
      ],
      towns: [],
      towns2: [],
      streets: [],
      streets2: [],
      Section: [
        { value: '一' },
        { value: '二' },
        { value: '三' },
        { value: '四' },
        { value: '五' },
        { value: '六' },
        { value: '七' },
        { value: '八' },
        { value: '九' },
        { value: '十' },
        { value: '東' },
        { value: '西' }
      ],
      imm: this.immediate
    };
  },
  watch: {
    resetall: {
      handler() {
        this.county = '';
        this.town = '';
        this.street = '';
        this.addressExceptCountyTown.section = '';
        this.addressExceptCountyTown.lane = '';
        this.addressExceptCountyTown.alley = '';
        this.addressExceptCountyTown.number = '';
        this.addressExceptCountyTown.floor = '';
        this.addressExceptCountyTown.room = '';
      }
    },
    county: {
      handler(a, b) {
        if (a) {
          this.resetSelectOption();
          this.towns.filter((x, y) => {
            if (x.city === a) {
              this.towns2.unshift(x);
            }
          });
          this.sendAddress();
        }
      }
    },
    town: {
      handler(a, b) {
        if (a) {
          // 有值時才請求
          this.loading = true;
          GetStreetsByCityTown(this.county, this.town)
            .then(res => {
              this.streets = res.ResData;
              // this.street = '';
              this.sendAddress();
            })
            .then(() => {
              this.loading = false;
            });
          // 額外判斷是否為第一次載入or 變換鄉鎮市區選項
          // 變換鄉鎮市區才須將street清空
          if (a !== b && b) {
            this.street = '';
          }
        } else {
          // 無值
          this.streets = [];
          this.street = '';
        }
      }
    },
    street: {
      handler(a) {
        if (a) {
          this.sendAddress();
        }
      }
    },
    addressExceptCountyTown: {
      deep: true,
      handler() {
        if (
          !(
            this.addressExceptCountyTown.alley === undefined &&
            this.addressExceptCountyTown.floor === undefined &&
            this.addressExceptCountyTown.lane === undefined &&
            this.addressExceptCountyTown.number === undefined &&
            this.addressExceptCountyTown.room === undefined &&
            this.addressExceptCountyTown.section === undefined
          ) &&
          !(
            this.addressExceptCountyTown.alley === null &&
            this.addressExceptCountyTown.floor === null &&
            this.addressExceptCountyTown.lane === null &&
            this.addressExceptCountyTown.number === null &&
            this.addressExceptCountyTown.room === null &&
            this.addressExceptCountyTown.section === null
          )
        ) {
          this.sendAddress();
        }
      }
    },
    /**
     * 查詢頁點擊修改後立即偵測
     */
    sendedform: {
      deep: true,
      immediate: true,
      handler() {
        this.handleDataAsync();
      }
    }
  },
  created() {
    getAllTowns().then(res => {
      this.towns = res.ResData;
    });
  },
  methods: {
    resetSelectOption() {
      this.towns2 = [];
      this.town = '';
      this.streets = [];
      this.street = '';
    },

    /*
     *監測任一地址欄位改變時調用，並回傳改變後的地址物件
     */
    sendAddress() {
      var instance = {};
      instance.Add_1 = this.county.substring(0, this.county.length - 1);
      switch (this.county.substr(-1)) {
        case '縣':
          instance.Add_1_1 = '/Yes';
          instance.Add_1_2 = '/Off';
          break;
        case '市':
          instance.Add_1_1 = '/Off';
          instance.Add_1_2 = '/Yes';
          break;
        default:
          break;
      }
      instance.Add_2 = this.town.substring(0, this.town.length - 1);
      switch (this.town.substr(-1)) {
        case '鄉':
          instance.Add_2_1 = '/Yes';
          instance.Add_2_2 = '/Off';
          instance.Add_2_3 = '/Off';
          instance.Add_2_4 = '/Off';
          break;
        case '鎮':
          instance.Add_2_1 = '/Off';
          instance.Add_2_2 = '/Yes';
          instance.Add_2_3 = '/Off';
          instance.Add_2_4 = '/Off';
          break;
        case '市':
          instance.Add_2_1 = '/Off';
          instance.Add_2_2 = '/Off';
          instance.Add_2_3 = '/Yes';
          instance.Add_2_4 = '/Off';
          break;
        case '區':
          instance.Add_2_1 = '/Off';
          instance.Add_2_2 = '/Off';
          instance.Add_2_3 = '/Off';
          instance.Add_2_4 = '/Yes';
          break;
        default:
          break;
      }
      if (this.street.substr(-1) === '路' || this.street.substr(-1) === '街') {
        instance.Add_3 = this.street.substring(0, this.street.length - 1);
        switch (this.street.substr(-1)) {
          case '街':
            instance.Add_3_1 = '/Yes';
            instance.Add_3_2 = '/Off';
            break;
          case '路':
            instance.Add_3_1 = '/Off';
            instance.Add_3_2 = '/Yes';
            break;
          default:
            break;
        }
      } else {
        instance.Add_3 = this.street;
        instance.Add_3_1 = '/Off';
        instance.Add_3_2 = '/Off';
      }

      instance.Add_4 = this.addressExceptCountyTown.section;
      instance.Add_5 = this.addressExceptCountyTown.lane;
      instance.Add_6 = this.addressExceptCountyTown.alley;
      instance.Add_7 = this.addressExceptCountyTown.number;
      instance.Add_8 = this.addressExceptCountyTown.floor;
      instance.Add_9 = this.addressExceptCountyTown.room;
      this.$emit('getsearchaddress', instance);
      this.$emit('geteditaddress', instance, this.title);
    },

    /**
     * 修改頁預設讀取當前ID資訊(針對縣市下拉)，(讀取父組件傳進來的資訊並顯示)
     */
    async handleGetCounty() {
      var cou = this.sendedform.Add_1;
      if (this.sendedform.Add_1_1 === '/Yes') {
        cou += '縣';
      }
      if (this.sendedform.Add_1_2 === '/Yes') {
        cou += '市';
      }
      // this.county = this.countries.find(x => x.value === cou).value
      this.county = cou;
    },
    /**
     * 修改頁預設讀取當前ID資訊(針對鄉鎮市區下拉)，(讀取父組件傳進來的資訊並顯示)
     */
    async handleGetTown() {
      var tow = this.sendedform.Add_2;
      if (this.sendedform.Add_2_1 === '/Yes') {
        tow += '鄉';
      }
      if (this.sendedform.Add_2_2 === '/Yes') {
        tow += '鎮';
      }
      if (this.sendedform.Add_2_3 === '/Yes') {
        tow += '市';
      }
      if (this.sendedform.Add_2_4 === '/Yes') {
        tow += '區';
      }
      this.town = tow;
    },
    /**
     * 修改頁預設讀取當前ID資訊(針對街、路)，(讀取父組件傳進來的資訊並顯示)
     */
    async handleGetStreet() {
      var str = this.sendedform.Add_3;
      if (this.sendedform.Add_3_1 === '/Yes') {
        str += '街';
      }
      if (this.sendedform.Add_3_2 === '/Yes') {
        str += '路';
      }
      this.street = str;
    },
    /**
     * 修改頁預設讀取當前ID資訊(其餘地址資訊)，(讀取父組件傳進來的資訊並顯示)
     */
    async handleAddressExceptCountyTown() {
      this.addressExceptCountyTown = {
        section: this.sendedform.Add_4,
        lane: this.sendedform.Add_5,
        alley: this.sendedform.Add_6,
        number: this.sendedform.Add_7,
        floor: this.sendedform.Add_8,
        room: this.sendedform.Add_9
      };
    },
    remoteMethod(query) {
      if (query !== '') {
        this.loading = true;
        getAllStreets(this.county, this.town, query).then(res => {
          this.loading = false;
          this.streets = res.ResData;
        });
      } else {
        this.streets = [];
      }
    },
    async handleDataAsync() {
      if (this.sendedform !== null) {
        await this.handleGetCounty();
        await this.handleAddressExceptCountyTown();
        await this.handleGetTown();
        await this.handleGetStreet();
      }
    },
    cancelReadOnly(onOff) {
      this.$nextTick(() => {
        if (!onOff) {
          const { select } = this.$refs;
          const input = select.$el.querySelector('.el-input__inner');
          input.removeAttribute('readonly');
        }
      });
    }
  }
};
</script>

<style>
.no-number::-webkit-outer-spin-button,
.no-number::-webkit-inner-spin-button {
  margin: 0;
  -webkit-appearance: none !important;
}

.no-number input[type="number"]::-webkit-outer-spin-button,
.no-number input[type="number"]::-webkit-inner-spin-button {
  margin: 0;
  -webkit-appearance: none !important;
}

/*在firefox下移除input[number]的上下箭头*/
.no-number {
  -moz-appearance: textfield;
}

.no-number input[type="number"] {
  -moz-appearance: textfield;
}
</style>
