<template>
  <div>
    <el-form
      ref="editForm"
      v-loading="saveloading"
      v-loading.lock="saveloading"
      element-loading-text="儲存中..."
      :model="editForm"
      inline
      :rules="rules"
    >
      <el-card>
        <el-row>
          <el-card shadow="hover">
            <div class="recordbox">
              <el-divider content-position="left">
                <h2>審核對象信息</h2>
              </el-divider>
            </div>
            <el-col :span="6">
              <el-form-item
                label="名稱："
                :label-width="formLabelWidth2"
                prop="Name"
              >
                <el-input
                  v-model="editForm.Name"
                  readonly
                  autocomplete="off"
                  clearable
                />
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item
                label="身分證字號/居留證號："
                label-width="160px"
                prop="IDNo"
              >
                <el-input
                  v-model="editForm.IDNo"
                  readonly
                  autocomplete="off"
                  clearable
                />
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item
                label="生日："
                :label-width="formLabelWidth2"
                prop="Birthday"
              >
                <el-input
                  v-model="editForm.Birthday"
                  readonly
                  autocomplete="off"
                  clearable
                />
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item
                label="身分別："
                :label-width="formLabelWidth2"
                prop="Birthday"
              >
                <el-input
                  v-model="editForm.IDentity"
                  readonly
                  autocomplete="off"
                  clearable
                />
              </el-form-item>
            </el-col>
          </el-card>
          <br>
        </el-row>
        <el-row :gutter="20">
          <el-card shadow="hover">
            <el-col :span="6">
              <div class="recordbox">
                <el-divider content-position="left">
                  <h2>犯罪紀錄</h2>
                </el-divider>
              </div>
              <el-form-item
                label="刑事通緝犯"
                :label-width="formLabelWidth2"
                prop="Rating"
              >
                <el-input
                  v-model="editForm.CriminalWanted"
                  type="textarea"
                  :rows="3"
                  style="width:240px"
                  readonly
                  autocomplete="off"
                  clearable
                />
              </el-form-item>
              <el-form-item
                label="查捕中逃犯"
                prop="Rating"
                :label-width="formLabelWidth2"
              >
                <el-input
                  v-model="editForm.CatchingFugitives"
                  type="textarea"
                  :rows="3"
                  style="width:240px"
                  readonly
                  autocomplete="off"
                  clearable
                />
              </el-form-item>
              <el-form-item
                label="查緝專刊"
                :label-width="formLabelWidth2"
                prop="Rating"
              >
                <el-input
                  v-model="editForm.CriminalLib"
                  type="textarea"
                  :rows="3"
                  style="width:240px"
                  readonly
                  autocomplete="off"
                  clearable
                />
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <div class="recordbox">
                <el-divider content-position="left">
                  <h2>欠債紀錄</h2>
                </el-divider>
              </div>
              <el-form-item
                label="交通罰鍰"
                :label-width="formLabelWidth2"
                prop="Rating"
              >
                <el-input
                  v-model="editForm.TraficFines"
                  type="textarea"
                  :rows="3"
                  style="width:240px"
                  readonly
                  autocomplete="off"
                  clearable
                />
              </el-form-item>
              <el-form-item
                label="汽機車燃料稅"
                prop="Rating"
                :label-width="formLabelWidth2"
              >
                <el-input
                  v-model="editForm.FuelPenaltyExpireds"
                  type="textarea"
                  :rows="3"
                  style="width:240px"
                  readonly
                  autocomplete="off"
                  clearable
                />
              </el-form-item>
              <el-form-item
                label="消債事件"
                :label-width="formLabelWidth2"
                prop="Rating"
              >
                <el-input
                  v-model="editForm.ConsumerDebt"
                  type="textarea"
                  :rows="3"
                  style="width:240px"
                  readonly
                  autocomplete="off"
                  clearable
                />
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <div class="recordbox">
                <el-divider content-position="left">
                  <h2>行為能力</h2>
                </el-divider>
              </div>
              <el-form-item
                label="監護補助"
                :label-width="formLabelWidth2"
                prop="Rating"
              >
                <el-input
                  v-model="editForm.Domestic"
                  type="textarea"
                  :rows="3"
                  style="width:240px"
                  readonly
                  autocomplete="off"
                  clearable
                />
              </el-form-item>
              <div class="recordbox">
                <el-divider content-position="left">
                  <h2>租金補貼</h2>
                </el-divider>
              </div>
              <el-form-item
                label="縣市/公會版"
                :label-width="formLabelWidth2"
                prop="Rating"
              >
                <el-input
                  v-model="editForm.RentSubsidy"
                  type="textarea"
                  :rows="3"
                  style="width:240px"
                  readonly
                  autocomplete="off"
                  clearable
                />
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <div class="recordbox">
                <el-divider content-position="left">
                  <h2>黑名單</h2>
                </el-divider>
              </div>
              <el-form-item
                label="法源網"
                :label-width="formLabelWidth2"
                prop="Rating"
              >
                <el-input
                  v-model="editForm.LawBank"
                  type="textarea"
                  :rows="3"
                  style="width:240px"
                  readonly
                  autocomplete="off"
                  clearable
                />
              </el-form-item>
            </el-col>
          </el-card>
        </el-row>
        <br>
        <el-divider content-position="left">
          <h2>狀態判斷</h2>
        </el-divider>
        <el-row>
          <el-form-item
            label="綜合評分："
            :label-width="formLabelWidth2"
            prop="Rating"
          >
            <el-input
              v-model="editForm.Rating"
              readonly
              autocomplete="off"
              clearable
            />
          </el-form-item>
        </el-row>
        <el-row>
          <el-col :span="12">
            <el-form-item
              label="備註："
              :label-width="formLabelWidth2"
              prop="Note"
            >
              <el-input
                v-model="editForm.Note"
                readonly
                type="textarea"
                autocomplete="off"
                style="width: 420px"
                :rows="6"
              />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="6">
            <el-form-item
              label="名單來源："
              :label-width="formLabelWidth2"
              prop="ReportDept"
            >
              <el-input
                v-model="editForm.ReportDept"
                readonly
                autocomplete="off"
                clearable
              />
            </el-form-item>
          </el-col>
          <el-col :span="6">
            <el-form-item
              label="隸屬業務："
              :label-width="formLabelWidth2"
              prop="CreatorUserId"
            >
              <el-input
                v-model="editForm.CreatorUserName"
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
              label="審核說明："
              :label-width="formLabelWidth2"
              prop="TrialNote"
            >
              <el-input
                v-model="editForm.TrialNote"
                type="textarea"
                autocomplete="off"
                style="width: 420px"
                :rows="6"
                maxlength="500"
                show-word-limit
              />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="6">
            <el-form-item
              label="當前狀態："
              :label-width="formLabelWidth2"
              prop="ResultState"
            >
              <el-input
                v-model="editForm.ResultState"
                readonly
                autocomplete="off"
                clearable
              />
            </el-form-item>
          </el-col>
          <el-col :span="6">
            <el-form-item
              label="主管判定："
              :label-width="formLabelWidth2"
              prop="newResultState"
            >
              <el-select
                v-model="editForm.newResultState"
                placeholder="請選擇"
                clearable
                style="width: 120px"
              >
                <el-option value="黑名單">黑名單</el-option>
                <el-option value="白名單">白名單</el-option>
              </el-select>
            </el-form-item>
          </el-col>
        </el-row>

      </el-card>
    </el-form>
    <div class="flowSubmit">
      <el-button
        size="small"
        icon="el-icon-close"
        @click="cancel"
      >關閉</el-button>
      <el-button
        type="primary"
        size="small"
        icon="el-icon-paperclip"
        @click="saveEditForm('update')"
      >送出</el-button>
    </div>
  </div>
</template>

<script>
import { BlackListSubmit } from '@/api/chaochi/todolist/todolistservice';
export default {
  name: 'ToDoListBlackList',
  data() {
    return {
      editForm: {},
      rules: {
        TrialNote: [
          { required: true, message: '請輸入審核說明', trigger: 'blur' }
        ],
        newResultState: [
          { required: true, message: '請選擇審核結果', trigger: 'blur' }
        ]
      },
      saveloading: false,
      formLabelWidth2: '100px',
      currentId: ''
    };
  },
  computed: {
    visitedViews() {
      return this.$store.state.tagsView.visitedViews;
    }
  },
  created() {
    this.editForm = this.$route.params.info;
    this.currentId = this.$route.params.todoListItemId;
  },
  methods: {
    isActive(route) {
      return route.path === this.$route.path;
    },
    toLastView(visitedViews, view) {
      const latestView = visitedViews.slice(-1)[0];
      if (latestView) {
        this.$router.push(latestView.fullPath);
      } else {
        // now the default is to redirect to the home page if there is no tags-view,
        // you can adjust it according to your needs.
        if (view.name === 'Dashboard') {
          // to reload home page
          this.$router.replace({ path: '/redirect' + view.fullPath });
        } else {
          this.$router.push('/');
        }
      }
    },
    // 取消按鈕
    cancel() {
      this.$refs['editForm'].resetFields();
      const view = this.visitedViews.slice(-1)[0];
      if (view) {
        this.$store
          .dispatch('tagsView/delView', view)
          .then(({ visitedViews }) => {
            if (this.isActive(view)) {
              this.toLastView(visitedViews, view);
            }
          });
      }
    },
    /**
     * 新增/修改保存
     */
    saveEditForm(action) {
      this.$refs['editForm'].validate(valid => {
        if (valid) {
          this.saveloading = true;
          var BlackListInputDto = {};
          BlackListInputDto = {
            ResultState: this.editForm.ResultState,
            TrialNote: this.editForm.TrialNote,
            Id: this.editForm.Id
          };
          var input = {
            Id: this.currentId,
            BlackListInputDto: BlackListInputDto
          };

          if (this.editForm.newResultState !== '') {
            BlackListInputDto.ResultState = this.editForm.newResultState;
          }
          BlackListSubmit(input)
            .then(res => {
              if (res.Success) {
                this.$message.success('審核成功!');
                this.$store.dispatch('user/settodoListCount');
                this.cancel();
                // this.dialogEditFormVisible = false;
                // this.addDialogVisible = false;
                // this.currentSelected = '';
                // this.loadTableData();
                // this.InitDictItem();
              } else {
                this.$message.error('失敗');
              }
            })
            .finally(() => {
              this.saveloading = false;
            });
        } else {
          return false;
        }
      });
    }
  }
};
</script>

<style>
.flowSubmit {
  display: flex;
  justify-content: flex-start;
  padding-top: 15px;
}
</style>
