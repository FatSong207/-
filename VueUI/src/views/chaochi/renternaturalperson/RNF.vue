<template>
  <el-card>
    <el-form
      ref="editForm"
      v-loading="saveLoading"
      element-loading-text="儲存中..."
      :inline="true"
      :model="editForm"
      class="demo-form-inline"
      :rules="rule"
    >
      <el-divider content-position="left">
        <h2>申請人</h2>
      </el-divider>
      <el-row>
        <el-col :span="4.8">
          <el-form-item
            label="姓名："
            :label-width="formLabelWidth"
          >
            <el-input
              v-model="editForm.RNName"
              clearable
              readonly
            />
          </el-form-item>
        </el-col>
        <el-col :span="4.8">
          <el-form-item
            label="身分證字號："
            :label-width="formLabelWidth"
          >
            <el-input
              v-model="editForm.RNID"
              v-upperCase
              clearable
              readonly
            />
          </el-form-item>
        </el-col>
        <el-col :span="4.8">
          <el-form-item
            label="生日："
            :label-width="formLabelWidth"
          >
            <el-input
              v-model="editForm.RNBirthday"
              clearable
              readonly
            />
          </el-form-item>
        </el-col>
        <el-col :span="4.8">
          <el-form-item
            label="手機："
            :label-width="formLabelWidth"
          >
            <el-input
              v-model="editForm.RNCell"
              clearable
              readonly
            />
          </el-form-item>
        </el-col>
        <el-col :span="4.8">
          <el-form-item
            label="稱謂："
            :label-width="formLabelWidth"
            style="width: 100%"
          >
            <el-input
              v-model="editForm.RNFRelation"
              clearable
              readonly
            />
          </el-form-item>
        </el-col>
      </el-row>
      <el-row :gutter="20">
        <el-col :span="6">
          <el-card shadow="hover">
            <div>
              <el-divider content-position="left">
                <h2>家庭成員1</h2>
              </el-divider>
            </div>
            <el-row>
              <el-form-item
                label="姓名："
                :label-width="formLabelWidth"
              >
                <el-input
                  v-model="editForm.RNFName_A"
                  clearable
                />
              </el-form-item>
              <el-form-item
                label="身分證字號："
                :label-width="formLabelWidth"
                prop="RNFID_1_A"
              >
                <el-input
                  v-model="editForm.RNFID_1_A"
                  v-upperCase
                  clearable
                  :maxlength="10"
                />
              </el-form-item>
              <el-form-item
                label="生日："
                :label-width="formLabelWidth"
              >
                <DatePickerE
                  v-model="editForm.RNFBirthday_A"
                  value-format="yyyy-MM-dd"
                  format="yyyy-MM-dd"
                  type="date"
                  placeholder="選擇日期"
                />
              </el-form-item>
              <el-form-item
                label="手機："
                :label-width="formLabelWidth"
                prop="RNFCell_A"
              >
                <el-input
                  v-model="editForm.RNFCell_A"
                  clearable
                />
              </el-form-item>
              <el-form-item
                label="稱謂："
                :label-width="formLabelWidth"
              >
                <el-input
                  v-model="editForm.RNFRelation_A"
                  clearable
                />
              </el-form-item>
            </el-row>
          </el-card>
        </el-col>
        <el-col :span="6">
          <el-card shadow="hover">
            <div>
              <el-divider content-position="left">
                <h2>家庭成員2</h2>
              </el-divider>
            </div>
            <el-row>
              <el-form-item
                label="姓名："
                :label-width="formLabelWidth"
              >
                <el-input
                  v-model="editForm.RNFName_B"
                  clearable
                />
              </el-form-item>
              <el-form-item
                label="身分證字號："
                :label-width="formLabelWidth"
                prop="RNFID_1_B"
              >
                <el-input
                  v-model="editForm.RNFID_1_B"
                  v-upperCase
                  clearable
                  :maxlength="10"
                />
              </el-form-item>
              <el-form-item
                label="生日："
                :label-width="formLabelWidth"
              >
                <DatePickerE
                  v-model="editForm.RNFBirthday_B"
                  value-format="yyyy-MM-dd"
                  format="yyyy-MM-dd"
                  type="date"
                  placeholder="選擇日期"
                />
              </el-form-item>
              <el-form-item
                label="手機："
                :label-width="formLabelWidth"
                prop="RNFCell_B"
              >
                <el-input
                  v-model="editForm.RNFCell_B"
                  clearable
                />
              </el-form-item>
              <el-form-item
                label="稱謂："
                :label-width="formLabelWidth"
              >
                <el-input
                  v-model="editForm.RNFRelation_B"
                  clearable
                />
              </el-form-item>
            </el-row>
          </el-card>
        </el-col>
        <el-col :span="6">
          <el-card shadow="hover">
            <div>
              <el-divider content-position="left">
                <h2>家庭成員3</h2>
              </el-divider>
            </div>
            <el-row>
              <el-form-item
                label="姓名："
                :label-width="formLabelWidth"
              >
                <el-input
                  v-model="editForm.RNFName_C"
                  clearable
                />
              </el-form-item>
              <el-form-item
                label="身分證字號："
                :label-width="formLabelWidth"
                prop="RNFID_1_C"
              >
                <el-input
                  v-model="editForm.RNFID_1_C"
                  v-upperCase
                  clearable
                  :maxlength="10"
                />
              </el-form-item>
              <el-form-item
                label="生日："
                :label-width="formLabelWidth"
              >
                <DatePickerE
                  v-model="editForm.RNFBirthday_C"
                  value-format="yyyy-MM-dd"
                  format="yyyy-MM-dd"
                  type="date"
                  placeholder="選擇日期"
                />
              </el-form-item>
              <el-form-item
                label="手機："
                :label-width="formLabelWidth"
                prop="RNFCell_C"
              >
                <el-input
                  v-model="editForm.RNFCell_C"
                  clearable
                />
              </el-form-item>
              <el-form-item
                label="稱謂："
                :label-width="formLabelWidth"
                style="width: 100%"
              >
                <el-input
                  v-model="editForm.RNFRelation_C"
                  clearable
                />
              </el-form-item>
            </el-row>
          </el-card>
        </el-col>
        <el-col :span="6">
          <el-card shadow="hover">
            <div>
              <el-divider content-position="left">
                <h2>家庭成員4</h2>
              </el-divider>
            </div>
            <el-row>
              <el-form-item
                label="姓名："
                :label-width="formLabelWidth"
              >
                <el-input
                  v-model="editForm.RNFName_D"
                  clearable
                />
              </el-form-item>
              <el-form-item
                label="身分證字號："
                :label-width="formLabelWidth"
                prop="RNFID_1_D"
              >
                <el-input
                  v-model="editForm.RNFID_1_D"
                  v-upperCase
                  clearable
                  :maxlength="10"
                />
              </el-form-item>
              <el-form-item
                label="生日："
                :label-width="formLabelWidth"
              >
                <DatePickerE
                  v-model="editForm.RNFBirthday_D"
                  value-format="yyyy-MM-dd"
                  format="yyyy-MM-dd"
                  type="date"
                  placeholder="選擇日期"
                />
              </el-form-item>
              <el-form-item
                label="手機："
                :label-width="formLabelWidth"
                prop="RNFCell_D"
              >
                <el-input
                  v-model="editForm.RNFCell_D"
                  clearable
                />
              </el-form-item>
              <el-form-item
                label="稱謂："
                :label-width="formLabelWidth"
              >
                <el-input
                  v-model="editForm.RNFRelation_D"
                  clearable
                />
              </el-form-item>
            </el-row>
          </el-card>
        </el-col>
      </el-row>
      <el-row>
        &nbsp;
      </el-row>
      <el-row :gutter="20">
        <el-col :span="6">
          <el-card shadow="hover">
            <div>
              <el-divider content-position="left">
                <h2>家庭成員5</h2>
              </el-divider>
            </div>
            <el-row>
              <el-form-item
                label="姓名："
                :label-width="formLabelWidth"
              >
                <el-input
                  v-model="editForm.RNFName_E"
                  clearable
                />
              </el-form-item>
              <el-form-item
                label="身分證字號："
                :label-width="formLabelWidth"
                prop="RNFID_1_E"
              >
                <el-input
                  v-model="editForm.RNFID_1_E"
                  v-upperCase
                  clearable
                  :maxlength="10"
                />
              </el-form-item>
              <el-form-item
                label="生日："
                :label-width="formLabelWidth"
              >
                <DatePickerE
                  v-model="editForm.RNFBirthday_E"
                  value-format="yyyy-MM-dd"
                  format="yyyy-MM-dd"
                  type="date"
                  placeholder="選擇日期"
                />
              </el-form-item>
              <el-form-item
                label="手機："
                :label-width="formLabelWidth"
                prop="RNFCell_E"
              >
                <el-input
                  v-model="editForm.RNFCell_E"
                  clearable
                />
              </el-form-item>
              <el-form-item
                label="稱謂："
                :label-width="formLabelWidth"
              >
                <el-input
                  v-model="editForm.RNFRelation_E"
                  clearable
                />
              </el-form-item>
            </el-row>
          </el-card>
        </el-col>
        <el-col :span="6">
          <el-card shadow="hover">
            <div>
              <el-divider content-position="left">
                <h2>家庭成員6</h2>
              </el-divider>
            </div>
            <el-row>
              <el-form-item
                label="姓名："
                :label-width="formLabelWidth"
              >
                <el-input
                  v-model="editForm.RNFName_F"
                  clearable
                />
              </el-form-item>
              <el-form-item
                label="身分證字號："
                :label-width="formLabelWidth"
                prop="RNFID_1_F"
              >
                <el-input
                  v-model="editForm.RNFID_1_F"
                  v-upperCase
                  clearable
                  :maxlength="10"
                />
              </el-form-item>
              <el-form-item
                label="生日："
                :label-width="formLabelWidth"
              >

                <DatePickerE
                  v-model="editForm.RNFBirthday_F"
                  value-format="yyyy-MM-dd"
                  format="yyyy-MM-dd"
                  type="date"
                  placeholder="選擇日期"
                />
              </el-form-item>
              <el-form-item
                label="手機："
                :label-width="formLabelWidth"
                prop="RNFCell_F"
              >
                <el-input
                  v-model="editForm.RNFCell_F"
                  clearable
                />
              </el-form-item>
              <el-form-item
                label="稱謂："
                :label-width="formLabelWidth"
              >
                <el-input
                  v-model="editForm.RNFRelation_F"
                  clearable
                />
              </el-form-item>
            </el-row>
          </el-card>
        </el-col>
        <el-col :span="6">
          <el-card shadow="hover">
            <div>
              <el-divider content-position="left">
                <h2>家庭成員7</h2>
              </el-divider>
            </div>
            <el-row>
              <el-form-item
                label="姓名："
                :label-width="formLabelWidth"
              >
                <el-input
                  v-model="editForm.RNFName_G"
                  clearable
                />
              </el-form-item>
              <el-form-item
                label="身分證字號："
                :label-width="formLabelWidth"
                prop="RNFID_1_G"
              >
                <el-input
                  v-model="editForm.RNFID_1_G"
                  v-upperCase
                  clearable
                  :maxlength="10"
                />
              </el-form-item>
              <el-form-item
                label="生日："
                :label-width="formLabelWidth"
              >
                <DatePickerE
                  v-model="editForm.RNFBirthday_G"
                  value-format="yyyy-MM-dd"
                  format="yyyy-MM-dd"
                  type="date"
                  placeholder="選擇日期"
                />
              </el-form-item>
              <el-form-item
                label="手機："
                :label-width="formLabelWidth"
                prop="RNFCell_G"
              >
                <el-input
                  v-model="editForm.RNFCell_G"
                  clearable
                />
              </el-form-item>
              <el-form-item
                label="稱謂："
                :label-width="formLabelWidth"
              >
                <el-input
                  v-model="editForm.RNFRelation_G"
                  clearable
                />
              </el-form-item>
            </el-row>
          </el-card>
        </el-col>
      </el-row>

    </el-form>
    <div class="rightbtn">
      <el-button
        v-hasPermi="['CustomerRN/Edit']"
        type="primary"
        size="small"
        icon="el-icon-paperclip"
        @click="saveEditForm()"
      >儲存</el-button>
      <el-button
        size="small"
        icon="el-icon-close"
        @click="cancel"
      >關閉</el-button>
    </div>
  </el-card>
</template>

<script>
import { UpdateRNFAsync } from '@/api/chaochi/renter/customerrnservice';
import DatePickerE from '@/components/RocDatepickerE';
import { validateIDReg, validateCellReg } from '@/utils/validate';
export default {
  name: 'RNF',
  components: { DatePickerE },
  props: {
    editform: { type: Object, default: null }
  },
  data() {
    return {
      editForm: {},
      formLabelWidth: '115px',
      saveLoading: false,
      rule: {
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
      }
    };
  },
  watch: {
    editform: {
      handler() {
        this.editForm = { ...this.editform };
      }
    }
  },
  methods: {
    cancel() {
      this.$emit('cancel');
      // this.editForm = {};
      // this.building1 = {};
      // this.Customer = {};
      // this.sendedform = {};
      // this.sendedformOB = {};
      // this.sendedformOC = {};
      // this.sendedformOD = {};
    },
    saveEditForm() {
      this.$refs['editForm'].validate(valid => {
        if (valid) {
          this.saveLoading = true;
          UpdateRNFAsync(this.editForm)
            .then(res => {
              if (res.Success) {
                this.$message.success('儲存成功');
              } else {
                this.$message.error('失敗');
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
