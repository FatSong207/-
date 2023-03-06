<template>
  <div>
    <el-button
      type="primary"
      size="small"
      icon="el-icon-monitor"
    >修改</el-button>
    <el-table :data="tableData">
      <el-table-column
        prop="No"
        label="代理人"
      />
      <el-table-column
        prop="LNAGName"
        label="姓名"
      />
      <el-table-column
        prop="LNAGID"
        label="身份證字號/統一編號"
      />
      <el-table-column
        prop="LNAGCell"
        label="手機"
      />
      <el-table-column
        prop="LNAGTel"
        label="電話"
      />
      <el-table-column
        prop="LNAGAdd"
        label="戶籍地址"
      />
    </el-table>
  </div>
</template>

<script>
// import { getCustomerLNDetail } from '@/api/chaochi/landlordnaturalperson/customerlnservice'
export default {
  name: 'LNAG',
  props: {
    editform: { type: Object, default: null }
  },
  data() {
    return {
      tableData: []
    }
  },
  watch: {
    editForm: {
      handler(a) {
        this.tableData = []
        var firstdt = {
          No: '1',
          LNAGName: this.editform.LNAGName_A,
          LNAGID: this.editform.LNAGID_A,
          LNAGCell: this.editform.LNAGCell_A,
          LNAGTel: this.editform.LNAGTel_A,
          LNAGAdd: this.editform.LNAGAdd_A
        }
        var seconddt = {
          No: '2',
          LNAGName: this.editform.LNAGName_B,
          LNAGID: this.editform.LNAGID_B,
          LNAGCell: this.editform.LNAGCell_B,
          LNAGTel: this.editform.LNAGTel_B,
          LNAGAdd: this.editform.LNAGAdd_B
        }
        this.tableData.push(firstdt, seconddt)
      }
    }
  },
  created() {
    this.tableData = []
    var firstdt = {
      No: '1',
      LNAGName: this.editform.LNAGName_A,
      LNAGID: this.editform.LNAGID_A,
      LNAGCell: this.editform.LNAGCell_A,
      LNAGTel: this.editform.LNAGTel_A,
      LNAGAdd: this.editform.LNAGAdd_A
    }
    var seconddt = {
      No: '2',
      LNAGName: this.editform.LNAGName_B,
      LNAGID: this.editform.LNAGID_B,
      LNAGCell: this.editform.LNAGCell_B,
      LNAGTel: this.editform.LNAGTel_B,
      LNAGAdd: this.editform.LNAGAdd_B
    }
    this.tableData.push(firstdt, seconddt)
  }
}
</script>

<style>
</style>
