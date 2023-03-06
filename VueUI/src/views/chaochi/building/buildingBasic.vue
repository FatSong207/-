<template>
  <el-form
    ref="editForm"
    v-loading="saveLoading"
    element-loading-text="儲存中..."
    :inline="true"
    :model="building1"
    class="demo-form-inline"
    :rules="rule"
  >
    <!-- <el-row>
      <el-col :span="12">
        <el-form-item
          label="建物門牌地址："
          :label-width="formLabelWidth"
          prop="BAdd"
        >
          <el-input
            v-model="editForm.BAdd"
            autocomplete="off"
            clearable
            style="width:410px"
            readonly
          />
        </el-form-item>
      </el-col>
      <el-col :span="12">
        <el-form-item
          label="建物建號："
          :label-width="formLabelWidth"
          prop="AccountX"
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
    </el-row>
    <el-row>
      <el-col :span="12">
        <el-form-item
          label="建物地號1："
          :label-width="formLabelWidth"
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
          label="建物所屬出租人："
          :label-width="formLabelWidth"
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
    </el-row>
    <el-row>
      <el-col :span="12">
        <el-form-item
          label="建物地號2："
          :label-width="formLabelWidth"
          prop="AccountX"
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
          label="稅籍編號："
          :label-width="formLabelWidth"
          prop="BelongLandlord"
        >
          <el-input
            v-model="building1.B1TaxID"
            autocomplete="off"
            clearable
          />
        </el-form-item>
      </el-col>
    </el-row> -->
    <el-card shadow="hover">
      <div>
        <el-divider content-position="left">
          <h2>所有權代表人</h2>
        </el-divider>
      </div>
      <el-row>
        <el-col :span="6">
          <el-form-item
            :label="nameLabel"
            :label-width="formLabelWidth"
            style="width: 100%"
          >
            <el-input
              v-model="Customer.Name"
              clearable
              readonly
            />
          </el-form-item>
        </el-col>
        <el-col :span="6">
          <el-form-item
            :label="idLabel"
            :label-width="formLabelWidth"
            prop="LNID"
            style="width: 100%"
          >
            <el-input
              v-model="Customer.ID"
              clearable
              readonly
            />
          </el-form-item>
        </el-col>
        <el-col :span="6">
          <el-form-item
            label="電話："
            :label-width="formLabelWidth"
            prop="RNTel"
            style="width: 100%"
          >
            <el-input
              v-model="Customer.Tel2"
              placeholder="請輸入電話"
              clearable
              readonly
              style="width: 220px"
              class="input-with-select"
              onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
            >
              <el-select
                slot="prepend"
                v-model="Customer.Tel1"
                placeholder="區號"
                style="width: 80px"
                clearable
                disabled
              >
                <el-option
                  v-for="item in zoneOptions"
                  :key="item.value"
                  :label="item.label"
                  :value="item.value"
                />
              </el-select>
            </el-input>
            <!-- <el-input
            v-model="Customer.Tel1"
            style="width: 80px"
            clearable
            readonly
          />
          <el-input
            v-model="Customer.Tel2"
            style="width: 140px"
            clearable
            readonly
          /> -->
          </el-form-item>
        </el-col>
        <el-col :span="6">
          <el-form-item
            label="手機："
            :label-width="formLabelWidth"
            prop="RNCell"
            style="width: 100%"
          >
            <el-input
              v-model="Customer.Cell"
              autocomplete="off"
              clearable
              readonly
            />
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="24">
          <el-form-item
            :label="addressLabel"
            :label-width="formLabelWidth"
            prop="LNAdd"
          >
            <el-input
              v-model="Customer.Address"
              autocomplete="off"
              style="width: 410px"
              clearable
              readonly
            />
          </el-form-item>
        </el-col>
        <el-col :span="6">
          <el-form-item
            label="權利範圍："
            :label-width="formLabelWidth"
            prop="LNAdd"
          >
            <el-input
              v-model="building1.B1ExtentOfOwner_1"
              autocomplete="off"
              style="width: 80px"
              clearable
            />／
            <el-input
              v-model="building1.B1ExtentOfOwner_2"
              autocomplete="off"
              style="width: 80px"
              clearable
            />
          </el-form-item>
        </el-col>
      </el-row>
    </el-card>
    <el-card
      shadow="hover"
      style="margin-top:30px"
    >
      <div>
        <el-divider content-position="left">
          <h2>其他所有權人A</h2>
        </el-divider>
      </div>
      <el-row>
        <el-col :span="6">
          <el-form-item
            label="姓名："
            :label-width="formLabelWidth"
            style="width: 100%"
          >
            <el-input
              v-model="building1.B1OwnerName_A"
              clearable
            />
          </el-form-item>
        </el-col>
        <el-col :span="6">
          <el-form-item
            label="身分證字號："
            :label-width="formLabelWidth"
            prop="B1OwnerID_A"
            style="width: 100%"
          >
            <el-input
              v-model="building1.B1OwnerID_A"
              v-upperCase
              clearable
              :maxlength="10"
            />
          </el-form-item>
        </el-col>
        <el-col :span="6">
          <el-form-item
            label="電話："
            :label-width="formLabelWidth"
            prop="RNTel"
            style="width: 100%"
          >
            <el-input
              v-model="building1.B1OwnerTel_2_A"
              placeholder="請輸入電話"
              clearable
              style="width: 220px"
              class="input-with-select"
              onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
            >
              <el-select
                slot="prepend"
                v-model="building1.B1OwnerTel_1_A"
                placeholder="區號"
                style="width: 80px"
                clearable
              >
                <el-option
                  v-for="item in zoneOptions"
                  :key="item.value"
                  :label="item.label"
                  :value="item.value"
                />
              </el-select>
            </el-input>
            <!-- <el-input
            v-model="building1.B1OwnerTel_1_A"
            style="width: 80px"
            clearable
          />
          <el-input
            v-model="building1.B1OwnerTel_2_A"
            style="width: 140px"
            clearable
          /> -->
          </el-form-item>
        </el-col>
        <el-col :span="6">
          <el-form-item
            label="手機："
            :label-width="formLabelWidth"
            prop="B1OwnerCell_A"
            style="width: 100%"
          >
            <el-input
              v-model="building1.B1OwnerCell_A"
              autocomplete="off"
              clearable
              maxlength="10"
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
            prop="LNAdd"
          />
          <Address
            :sendedform="sendedform"
            title="其他所有權人A地址"
            @geteditaddress="geteditaddress"
          />
        </el-col>
        <el-col :span="6">
          <el-form-item
            label="權利範圍："
            :label-width="formLabelWidth"
            prop="LNAdd"
          >
            <el-input
              v-model="building1.B1ExtentOfOwner_A_1"
              autocomplete="off"
              style="width: 80px"
              clearable
            />／
            <el-input
              v-model="building1.B1ExtentOfOwner_A_2"
              autocomplete="off"
              style="width: 80px"
              clearable
            />
          </el-form-item>
        </el-col>
      </el-row>
    </el-card>
    <el-card
      shadow="hover"
      style="margin-top:30px"
    >
      <div>
        <el-divider content-position="left">
          <h2>其他所有權人B</h2>
        </el-divider>
      </div>
      <el-row>
        <el-col :span="6">
          <el-form-item
            label="姓名："
            :label-width="formLabelWidth"
            style="width: 100%"
          >
            <el-input
              v-model="building1.B1OwnerName_B"
              clearable
            />
          </el-form-item>
        </el-col>
        <el-col :span="6">
          <el-form-item
            label="身分證字號："
            :label-width="formLabelWidth"
            prop="B1OwnerID_B"
            style="width: 100%"
          >
            <el-input
              v-model="building1.B1OwnerID_B"
              v-upperCase
              clearable
              :maxlength="10"
            />
          </el-form-item>
        </el-col>
        <el-col :span="6">
          <el-form-item
            label="電話："
            :label-width="formLabelWidth"
            prop="RNTel"
            style="width: 100%"
          >
            <el-input
              v-model="building1.B1OwnerTel_2_B"
              placeholder="請輸入電話"
              clearable
              style="width: 220px"
              class="input-with-select"
              onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
            >
              <el-select
                slot="prepend"
                v-model="building1.B1OwnerTel_1_B"
                placeholder="區號"
                style="width: 80px"
                clearable
              >
                <el-option
                  v-for="item in zoneOptions"
                  :key="item.value"
                  :label="item.label"
                  :value="item.value"
                />
              </el-select>
            </el-input>
            <!-- <el-input
            v-model="building1.B1OwnerTel_1_B"
            style="width: 80px"
            clearable
          />
          <el-input
            v-model="building1.B1OwnerTel_2_B"
            style="width: 140px"
            clearable
          /> -->
          </el-form-item>
        </el-col>
        <el-col :span="6">
          <el-form-item
            label="手機："
            :label-width="formLabelWidth"
            prop="B1OwnerCell_B"
            style="width: 100%"
          >
            <el-input
              v-model="building1.B1OwnerCell_B"
              autocomplete="off"
              clearable
              maxlength="10"
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
            prop="LNAdd"
          />
          <Address
            :sendedform="sendedformOB"
            title="其他所有權人B地址"
            @geteditaddress="geteditaddress"
          />
        </el-col>
        <el-col :span="6">
          <el-form-item
            label="權利範圍："
            :label-width="formLabelWidth"
            prop="LNAdd"
          >
            <el-input
              v-model="building1.B1ExtentOfOwner_B_1"
              autocomplete="off"
              style="width: 80px"
              clearable
            />／
            <el-input
              v-model="building1.B1ExtentOfOwner_B_2"
              autocomplete="off"
              style="width: 80px"
              clearable
            />
          </el-form-item>
        </el-col>
      </el-row>
    </el-card>
    <el-card
      shadow="hover"
      style="margin-top:30px"
    >
      <div>
        <el-divider content-position="left">
          <h2>其他所有權人C</h2>
        </el-divider>
      </div>
      <el-row>
        <el-col :span="6">
          <el-form-item
            label="姓名："
            :label-width="formLabelWidth"
            style="width: 100%"
          >
            <el-input
              v-model="building1.B1OwnerName_C"
              clearable
            />
          </el-form-item>
        </el-col>
        <el-col :span="6">
          <el-form-item
            label="身分證字號："
            :label-width="formLabelWidth"
            prop="B1OwnerID_C"
            style="width: 100%"
          >
            <el-input
              v-model="building1.B1OwnerID_C"
              v-upperCase
              clearable
              :maxlength="10"
            />
          </el-form-item>
        </el-col>
        <el-col :span="6">
          <el-form-item
            label="電話："
            :label-width="formLabelWidth"
            prop="RNTel"
            style="width: 100%"
          >
            <el-input
              v-model="building1.B1OwnerTel_2_C"
              placeholder="請輸入電話"
              clearable
              style="width: 220px"
              class="input-with-select"
              onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
            >
              <el-select
                slot="prepend"
                v-model="building1.B1OwnerTel_1_C"
                placeholder="區號"
                style="width: 80px"
                clearable
              >
                <el-option
                  v-for="item in zoneOptions"
                  :key="item.value"
                  :label="item.label"
                  :value="item.value"
                />
              </el-select>
            </el-input>
            <!-- <el-input
            v-model="building1.B1OwnerTel_1_C"
            style="width: 80px"
            clearable
          />
          <el-input
            v-model="building1.B1OwnerTel_2_C"
            style="width: 140px"
            clearable
          /> -->
          </el-form-item>
        </el-col>
        <el-col :span="6">
          <el-form-item
            label="手機："
            :label-width="formLabelWidth"
            prop="B1OwnerCell_C"
            style="width: 100%"
          >
            <el-input
              v-model="building1.B1OwnerCell_C"
              autocomplete="off"
              clearable
              maxlength="10"
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
            prop="LNAdd"
          />
          <Address
            :sendedform="sendedformOC"
            title="其他所有權人C地址"
            @geteditaddress="geteditaddress"
          />
        </el-col>
        <el-col :span="6">
          <el-form-item
            label="權利範圍："
            :label-width="formLabelWidth"
            prop="LNAdd"
          >
            <el-input
              v-model="building1.B1ExtentOfOwner_C_1"
              autocomplete="off"
              style="width: 80px"
              clearable
            />／
            <el-input
              v-model="building1.B1ExtentOfOwner_C_2"
              autocomplete="off"
              style="width: 80px"
              clearable
            />
          </el-form-item>
        </el-col>
      </el-row>
    </el-card>
    <el-card
      shadow="hover"
      style="margin-top:30px"
    >
      <div>
        <el-divider content-position="left">
          <h2>其他所有權人D</h2>
        </el-divider>
      </div>
      <el-row>
        <el-col :span="6">
          <el-form-item
            label="姓名："
            :label-width="formLabelWidth"
            style="width: 100%"
          >
            <el-input
              v-model="building1.B1OwnerName_D"
              clearable
            />
          </el-form-item>
        </el-col>
        <el-col :span="6">
          <el-form-item
            label="身分證字號："
            :label-width="formLabelWidth"
            prop="B1OwnerID_D"
            style="width: 100%"
          >
            <el-input
              v-model="building1.B1OwnerID_D"
              v-upperCase
              clearable
              :maxlength="10"
            />
          </el-form-item>
        </el-col>
        <el-col :span="6">
          <el-form-item
            label="電話："
            :label-width="formLabelWidth"
            prop="RNTel"
            style="width: 100%"
          >
            <el-input
              v-model="building1.B1OwnerTel_2_D"
              placeholder="請輸入電話"
              clearable
              style="width: 220px"
              class="input-with-select"
              onkeypress="return(/[\d]/.test(String.fromCharCode(event.keyCode)))"
            >
              <el-select
                slot="prepend"
                v-model="building1.B1OwnerTel_1_D"
                placeholder="區號"
                style="width: 80px"
                clearable
              >
                <el-option
                  v-for="item in zoneOptions"
                  :key="item.value"
                  :label="item.label"
                  :value="item.value"
                />
              </el-select>
            </el-input>
            <!-- <el-input
            v-model="building1.B1OwnerTel_1_D"
            style="width: 80px"
            clearable
          />
          <el-input
            v-model="building1.B1OwnerTel_2_D"
            style="width: 140px"
            clearable
          /> -->
          </el-form-item>
        </el-col>
        <el-col :span="6">
          <el-form-item
            label="手機："
            :label-width="formLabelWidth"
            prop="B1OwnerCell_D"
            style="width: 100%"
          >
            <el-input
              v-model="building1.B1OwnerCell_D"
              autocomplete="off"
              clearable
              maxlength="10"
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
            prop="LNAdd"
          />
          <Address
            :sendedform="sendedformOD"
            title="其他所有權人D地址"
            @geteditaddress="geteditaddress"
          />
        </el-col>
        <el-col :span="6">
          <el-form-item
            label="權利範圍："
            :label-width="formLabelWidth"
            prop="LNAdd"
          >
            <el-input
              v-model="building1.B1ExtentOfOwner_D_1"
              autocomplete="off"
              style="width: 80px"
              clearable
            />／
            <el-input
              v-model="building1.B1ExtentOfOwner_D_2"
              autocomplete="off"
              style="width: 80px"
              clearable
            />
          </el-form-item>
        </el-col>
      </el-row>
    </el-card>
    <div class="rightbtn">
      <el-button
        v-hasPermi="['Building/Edit']"
        type="primary"
        size="small"
        icon="el-icon-paperclip"
        @click="saveBuildingBasic()"
      >儲存</el-button>
      <el-button
        size="small"
        icon="el-icon-close"
        @click="cancel"
      >關閉</el-button>
    </div>
  </el-form>
</template>

<script>
import { saveBuilding } from '@/api/chaochi/building/buildingservice';
import Address from '@/components/Address/Address.vue';
import { validateIDReg, validateCellReg } from '@/utils/validate';
export default {
  name: 'BuildingBasic',
  components: { Address },
  props: {
    editform: { type: Object, default: null },
    // clearflag: { type: Boolean, default: false },
    emitflag: { type: Boolean, default: false }
  },
  data() {
    return {
      sendedform: {},
      sendedformOB: {},
      sendedformOC: {},
      sendedformOD: {},
      editForm: {},
      Customer: {},
      building1: {},
      rule: {
        B1OwnerID_A: [
          {
            required: false,
            trigger: 'blur',
            validator: validateIDReg
          }
        ],
        B1OwnerID_B: [
          {
            required: false,
            trigger: 'blur',
            validator: validateIDReg
          }
        ],
        B1OwnerID_C: [
          {
            required: false,
            trigger: 'blur',
            validator: validateIDReg
          }
        ],
        B1OwnerID_D: [
          {
            required: false,
            trigger: 'blur',
            validator: validateIDReg
          }
        ],
        B1OwnerCell_A: [
          { required: false, trigger: 'change', validator: validateCellReg }
        ],
        B1OwnerCell_B: [
          { required: false, trigger: 'change', validator: validateCellReg }
        ],
        B1OwnerCell_C: [
          { required: false, trigger: 'change', validator: validateCellReg }
        ],
        B1OwnerCell_D: [
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
      saveLoading: false,
      formLabelWidth: '135px',
      nameLabel: '姓名：',
      idLabel: '身分證字號：',
      addressLabel: '戶籍地址：'
    };
  },
  watch: {
    editform: {
      handler() {
        this.bindEditInfo();
      }
    }
  },
  methods: {
    cancel() {
      this.$emit('cancel');
      this.editForm = {};
      this.building1 = {};
      this.Customer = {};
      this.sendedform = {};
      this.sendedformOB = {};
      this.sendedformOC = {};
      this.sendedformOD = {};
    },
    geteditaddress(addressData, title) {
      if (title === '其他所有權人A地址') {
        this.building1.B1OwnerAdd_1_A = addressData.Add_1;
        this.building1.B1OwnerAdd_1_1_A = addressData.Add_1_1;
        this.building1.B1OwnerAdd_1_2_A = addressData.Add_1_2;
        this.building1.B1OwnerAdd_2_A = addressData.Add_2;
        this.building1.B1OwnerAdd_2_1_A = addressData.Add_2_1;
        this.building1.B1OwnerAdd_2_2_A = addressData.Add_2_2;
        this.building1.B1OwnerAdd_2_3_A = addressData.Add_2_3;
        this.building1.B1OwnerAdd_2_4_A = addressData.Add_2_4;
        this.building1.B1OwnerAdd_3_A = addressData.Add_3;
        this.building1.B1OwnerAdd_3_1_A = addressData.Add_3_1;
        this.building1.B1OwnerAdd_3_2_A = addressData.Add_3_2;
        this.building1.B1OwnerAdd_4_A = addressData.Add_4;
        this.building1.B1OwnerAdd_5_A = addressData.Add_5;
        this.building1.B1OwnerAdd_6_A = addressData.Add_6;
        this.building1.B1OwnerAdd_7_A = addressData.Add_7;
        this.building1.B1OwnerAdd_8_A = addressData.Add_8;
        this.building1.B1OwnerAdd_9_A = addressData.Add_9;
      }
      if (title === '其他所有權人B地址') {
        this.building1.B1OwnerAdd_1_B = addressData.Add_1;
        this.building1.B1OwnerAdd_1_1_B = addressData.Add_1_1;
        this.building1.B1OwnerAdd_1_2_B = addressData.Add_1_2;
        this.building1.B1OwnerAdd_2_B = addressData.Add_2;
        this.building1.B1OwnerAdd_2_1_B = addressData.Add_2_1;
        this.building1.B1OwnerAdd_2_2_B = addressData.Add_2_2;
        this.building1.B1OwnerAdd_2_3_B = addressData.Add_2_3;
        this.building1.B1OwnerAdd_2_4_B = addressData.Add_2_4;
        this.building1.B1OwnerAdd_3_B = addressData.Add_3;
        this.building1.B1OwnerAdd_3_1_B = addressData.Add_3_1;
        this.building1.B1OwnerAdd_3_2_B = addressData.Add_3_2;
        this.building1.B1OwnerAdd_4_B = addressData.Add_4;
        this.building1.B1OwnerAdd_5_B = addressData.Add_5;
        this.building1.B1OwnerAdd_6_B = addressData.Add_6;
        this.building1.B1OwnerAdd_7_B = addressData.Add_7;
        this.building1.B1OwnerAdd_8_B = addressData.Add_8;
        this.building1.B1OwnerAdd_9_B = addressData.Add_9;
      }
      if (title === '其他所有權人C地址') {
        this.building1.B1OwnerAdd_1_C = addressData.Add_1;
        this.building1.B1OwnerAdd_1_1_C = addressData.Add_1_1;
        this.building1.B1OwnerAdd_1_2_C = addressData.Add_1_2;
        this.building1.B1OwnerAdd_2_C = addressData.Add_2;
        this.building1.B1OwnerAdd_2_1_C = addressData.Add_2_1;
        this.building1.B1OwnerAdd_2_2_C = addressData.Add_2_2;
        this.building1.B1OwnerAdd_2_3_C = addressData.Add_2_3;
        this.building1.B1OwnerAdd_2_4_C = addressData.Add_2_4;
        this.building1.B1OwnerAdd_3_C = addressData.Add_3;
        this.building1.B1OwnerAdd_3_1_C = addressData.Add_3_1;
        this.building1.B1OwnerAdd_3_2_C = addressData.Add_3_2;
        this.building1.B1OwnerAdd_4_C = addressData.Add_4;
        this.building1.B1OwnerAdd_5_C = addressData.Add_5;
        this.building1.B1OwnerAdd_6_C = addressData.Add_6;
        this.building1.B1OwnerAdd_7_C = addressData.Add_7;
        this.building1.B1OwnerAdd_8_C = addressData.Add_8;
        this.building1.B1OwnerAdd_9_C = addressData.Add_9;
      }
      if (title === '其他所有權人D地址') {
        this.building1.B1OwnerAdd_1_D = addressData.Add_1;
        this.building1.B1OwnerAdd_1_1_D = addressData.Add_1_1;
        this.building1.B1OwnerAdd_1_2_D = addressData.Add_1_2;
        this.building1.B1OwnerAdd_2_D = addressData.Add_2;
        this.building1.B1OwnerAdd_2_1_D = addressData.Add_2_1;
        this.building1.B1OwnerAdd_2_2_D = addressData.Add_2_2;
        this.building1.B1OwnerAdd_2_3_D = addressData.Add_2_3;
        this.building1.B1OwnerAdd_2_4_D = addressData.Add_2_4;
        this.building1.B1OwnerAdd_3_D = addressData.Add_3;
        this.building1.B1OwnerAdd_3_1_D = addressData.Add_3_1;
        this.building1.B1OwnerAdd_3_2_D = addressData.Add_3_2;
        this.building1.B1OwnerAdd_4_D = addressData.Add_4;
        this.building1.B1OwnerAdd_5_D = addressData.Add_5;
        this.building1.B1OwnerAdd_6_D = addressData.Add_6;
        this.building1.B1OwnerAdd_7_D = addressData.Add_7;
        this.building1.B1OwnerAdd_8_D = addressData.Add_8;
        this.building1.B1OwnerAdd_9_D = addressData.Add_9;
      }
    },
    bindEditInfo: function() {
      this.editForm = this.editform;
      this.Customer = this.editForm.OwnerRepOutputDto;
      if (this.Customer.ID.length === 8) {
        this.nameLabel = '公司名稱：';
        this.idLabel = '統一編號：';
        this.addressLabel = '登記地址：';
      } else {
        this.nameLabel = '姓名：';
        this.idLabel = '身分證字號：';
        this.addressLabel = '戶籍地址：';
      }
      this.building1 = this.editForm.Building1;
      this.sendedform = {
        Add_1: this.building1.B1OwnerAdd_1_A,
        Add_1_1: this.building1.B1OwnerAdd_1_1_A,
        Add_1_2: this.building1.B1OwnerAdd_1_2_A,
        Add_2: this.building1.B1OwnerAdd_2_A,
        Add_2_1: this.building1.B1OwnerAdd_2_1_A,
        Add_2_2: this.building1.B1OwnerAdd_2_2_A,
        Add_2_3: this.building1.B1OwnerAdd_2_3_A,
        Add_2_4: this.building1.B1OwnerAdd_2_4_A,
        Add_3: this.building1.B1OwnerAdd_3_A,
        Add_3_1: this.building1.B1OwnerAdd_3_1_A,
        Add_3_2: this.building1.B1OwnerAdd_3_2_A,
        Add_4: this.building1.B1OwnerAdd_4_A,
        Add_5: this.building1.B1OwnerAdd_5_A,
        Add_6: this.building1.B1OwnerAdd_6_A,
        Add_7: this.building1.B1OwnerAdd_7_A,
        Add_8: this.building1.B1OwnerAdd_8_A,
        Add_9: this.building1.B1OwnerAdd_9_A
      };
      this.sendedformOB = {
        Add_1: this.building1.B1OwnerAdd_1_B,
        Add_1_1: this.building1.B1OwnerAdd_1_1_B,
        Add_1_2: this.building1.B1OwnerAdd_1_2_B,
        Add_2: this.building1.B1OwnerAdd_2_B,
        Add_2_1: this.building1.B1OwnerAdd_2_1_B,
        Add_2_2: this.building1.B1OwnerAdd_2_2_B,
        Add_2_3: this.building1.B1OwnerAdd_2_3_B,
        Add_2_4: this.building1.B1OwnerAdd_2_4_B,
        Add_3: this.building1.B1OwnerAdd_3_B,
        Add_3_1: this.building1.B1OwnerAdd_3_1_B,
        Add_3_2: this.building1.B1OwnerAdd_3_2_B,
        Add_4: this.building1.B1OwnerAdd_4_B,
        Add_5: this.building1.B1OwnerAdd_5_B,
        Add_6: this.building1.B1OwnerAdd_6_B,
        Add_7: this.building1.B1OwnerAdd_7_B,
        Add_8: this.building1.B1OwnerAdd_8_B,
        Add_9: this.building1.B1OwnerAdd_9_B
      };
      this.sendedformOC = {
        Add_1: this.building1.B1OwnerAdd_1_C,
        Add_1_1: this.building1.B1OwnerAdd_1_1_C,
        Add_1_2: this.building1.B1OwnerAdd_1_2_C,
        Add_2: this.building1.B1OwnerAdd_2_C,
        Add_2_1: this.building1.B1OwnerAdd_2_1_C,
        Add_2_2: this.building1.B1OwnerAdd_2_2_C,
        Add_2_3: this.building1.B1OwnerAdd_2_3_C,
        Add_2_4: this.building1.B1OwnerAdd_2_4_C,
        Add_3: this.building1.B1OwnerAdd_3_C,
        Add_3_1: this.building1.B1OwnerAdd_3_1_C,
        Add_3_2: this.building1.B1OwnerAdd_3_2_C,
        Add_4: this.building1.B1OwnerAdd_4_C,
        Add_5: this.building1.B1OwnerAdd_5_C,
        Add_6: this.building1.B1OwnerAdd_6_C,
        Add_7: this.building1.B1OwnerAdd_7_C,
        Add_8: this.building1.B1OwnerAdd_8_C,
        Add_9: this.building1.B1OwnerAdd_9_C
      };
      this.sendedformOD = {
        Add_1: this.building1.B1OwnerAdd_1_D,
        Add_1_1: this.building1.B1OwnerAdd_1_1_D,
        Add_1_2: this.building1.B1OwnerAdd_1_2_D,
        Add_2: this.building1.B1OwnerAdd_2_D,
        Add_2_1: this.building1.B1OwnerAdd_2_1_D,
        Add_2_2: this.building1.B1OwnerAdd_2_2_D,
        Add_2_3: this.building1.B1OwnerAdd_2_3_D,
        Add_2_4: this.building1.B1OwnerAdd_2_4_D,
        Add_3: this.building1.B1OwnerAdd_3_D,
        Add_3_1: this.building1.B1OwnerAdd_3_1_D,
        Add_3_2: this.building1.B1OwnerAdd_3_2_D,
        Add_4: this.building1.B1OwnerAdd_4_D,
        Add_5: this.building1.B1OwnerAdd_5_D,
        Add_6: this.building1.B1OwnerAdd_6_D,
        Add_7: this.building1.B1OwnerAdd_7_D,
        Add_8: this.building1.B1OwnerAdd_8_D,
        Add_9: this.building1.B1OwnerAdd_9_D
      };
    },
    saveBuildingBasic() {
      this.$refs['editForm'].validate(valid => {
        if (valid) {
          this.saveLoading = true;
          const data = this.editForm;
          var url = 'Building/Insert';
          if (this.currentId !== '') {
            url = 'Building/Update';
          }
          saveBuilding(data, '', url)
            .then(res => {
              if (res.Success) {
                this.$message.success('儲存成功!');
              } else {
                this.$message.error('失敗!');
              }
            })
            .finally(() => {
              this.saveLoading = false;
            });
        }
      });
    }
  }
};
</script>

<style>
</style>
