<template>
  <div>
    {{ testmsg }}
    <div class="list-btn-container">
      <el-button-group>
        <el-button
          v-hasPermi="['Building/Add']"
          type="primary"
          icon="el-icon-plus"
          size="small"
          @click="ShowEditOrViewDialogG()"
        >新增</el-button>
        <el-button
          v-hasPermi="['Building/Edit']"
          type="primary"
          icon="el-icon-edit"
          class="el-button-modify"
          size="small"
          @click="ShowEditOrViewDialogG('edit')"
        >修改</el-button>
      </el-button-group>
    </div>
    <!-- <el-table
              ref="gridtable"
              v-loading="tableloading"
              :data="editFrom.Remittances"
              border
              stripe
              highlight-current-row
              style="width: 100%"
              :default-sort="{prop: 'SortCode', order: 'descending'}"
              @select="handleSelectChangeG"
              @select-all="handleSelectAllChangeG"
              @sort-change="handleSortChangeG"
            >
              <el-table-column
                type="selection"
                width="30"
              />
              <el-table-column
                prop="NeedDel"
                label="刪除"
                width="50"
              />
              <el-table-column
                prop="AName"
                label="帳戶名稱"
                width="100"
              />
              <el-table-column
                prop="AID"
                label="身份證字號"
                width="150"
              />
              <el-table-column
                prop="BankName"
                label="金融機構名稱"
                width="200"
              />
              <el-table-column
                prop="BankNo"
                label="金融機構代碼"
                width="150"
              />
              <el-table-column
                prop="BranchName"
                label="分行名稱"
                width="200"
              />
              <el-table-column
                prop="BranchNo"
                label="分行代碼"
                width="150"
              />
              <el-table-column
                prop="ANo"
                label="帳戶號碼"
                width="200"
              />
            </el-table> -->
  </div>
</template>

<script>
export default {
  name: 'BuildingRemittance',
  data() {
    return {
      testmsg: '開發中...'
    };
  }
};
</script>

<style>
</style>
