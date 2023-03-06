<template>
  <el-card class="box-card">
    <h3>現有主題及照片</h3>
    <el-form
      class="demo-form-inline"
      size="small"
    >
      <el-form-item label="當前建物地址：">
        <el-input
          v-model="title.buildingAdd"
          :readonly="buildingAddreadonly"
          clearable
          style="width: 25%"
        />
      </el-form-item>
      <el-form-item label="當前表單版本：">
        <el-input
          v-model="title.SecurityFormCVer"
          clearable
          style="width: 15%"
        />
      </el-form-item>
      <el-form-item label=" ">
        <el-button-group>
          <el-button
            type="primary"
            size="small"
            icon="el-icon-plus"
            @click="Insert()"
          >新增</el-button>
          <el-button
            type="danger"
            size="small"
            icon="el-icon-delete"
            @click="deleteimg()"
          >刪除</el-button>
          <!-- <el-button
            type="info"
            size="small"
            icon="el-icon-arrow-up"
          >上移</el-button>
          <el-button
            type="info"
            size="small"
            icon="el-icon-arrow-down"
          >下移</el-button> -->
          限制總數16張，目前已設定{{ 16-limitfilescount }}張
        </el-button-group>
        <el-table
          :data="imgs"
          border
          stripe
          @select="handleSelectChange"
          @select-all="handleSelectAllChange"
        >
          <el-table-column
            type="selection"
            width="50"
          />
          <el-table-column
            type="index"
            label="序位"
            width="50"
          />
          <el-table-column
            prop="title"
            label="照片主題"
          />
          <el-table-column
            prop="Note"
            label="縮圖"
          >
            <template slot-scope="scope">
              <!-- <img :src="scope.row.ImgUrl==null?'XX':scope.row.ImgUrl" min-width="70" height="70">{{ scope.row.FileName }} -->
              <div style="width: 80px;">
                <expandable-image :src="scope.row.ImgUrl==null?'#/XX':scope.row.ImgUrl" />
              </div>
            </template>
          </el-table-column>
          <el-table-column label="操作">
            <template slot-scope="scope">
              <el-button
                type="success"
                circle
                icon="el-icon-caret-top"
                :disabled="scope.$index == 0"
                @click="moveUpward(scope.row, scope.$index)"
              />
              <el-button
                type="success"
                circle
                icon="el-icon-caret-bottom"
                :disabled="(scope.$index + 1) == imgs.length"
                @click="moveDown(scope.row, scope.$index)"
              />
            </template>
          </el-table-column>
        </el-table>
        <h4>以上排序即為照片表單匯出PDF的順序，當照片上傳已自動存檔</h4>
        <el-button-group>
          <el-button
            type="primary"
            size="small"
            icon="el-icon-plus"
            @click="Preview()"
          >預覽</el-button>
          <el-button
            type="primary"
            size="small"
            icon="el-icon-tickets"
            @click="GenPDF1()"
          >產生歷史表單</el-button>
        </el-button-group>
      </el-form-item>
      <el-dialog
        title="表單"
        :visible.sync="historyFormVisible"
        width="500px"
        align="center"
        :close-on-click-modal="false"
        :close-on-press-escape="false"
        :show-close="showclose"
      >
        <el-form>
          <el-form-item
            label="照片主題"
            prop="name"
          >
            <el-select
              v-model="send.title"
              placeholder="請選擇主題"
              style="width: 350px"
            >
              <el-option
                v-for="item in options"
                :key="item.value"
                :label="item.label"
                :value="item.label"
              />
            </el-select>
            <!-- <el-input
              v-model="send.title"
              style="width: 350px"
            /> -->
          </el-form-item>
          <el-form-item label="照片上傳">
            <el-upload
              ref="upload"
              class="upload-demo"
              multiple
              :action="httpFileUploadUrl"
              :headers="headers"
              :auto-upload="false"
              :data="send"
              drag
              :file-list="fileList"
              list-type="picture"
              :on-change="onchange"
              :on-remove="onremove"
              accept=".png,.jpg,.gif,.jpeg"
            >
              <em class="el-icon-upload" />
              <div class="el-upload__text">將文件拖到此處，或<em>點擊上傳</em> </div>
              <div
                slot="tip"
                class="el-upload__tip"
              >＊<font color="red">請註意您只能上傳.png,.jpg,.gif,.jpeg格式</font>
              </div>
              <!-- <el-button
                size="small"
                type="primary"
                icon="el-icon-folder-add"
              >選擇文件</el-button> -->
            </el-upload>
            <el-button
              size="small"
              type="success"
              icon="el-icon-upload"
              :loading="loading"
              @click="submitUpload"
            >{{ uploadmsg }}</el-button>
          </el-form-item>
        </el-form>
      </el-dialog>
    </el-form>
  </el-card>
</template>

<script>
import {
  handelAdd,
  Getimgslist,
  getByFileName,
  ImgUpload,
  GenPDF,
  PreviewPDF,
  delImg
} from '@/api/chaochi/SecurityForm/securityformService';
import defaultSettings from '@/settings';
import { getToken } from '@/utils/auth';
import * as imageConversion from 'image-conversion';
export default {
  name: 'A000301',
  data() {
    return {
      httpFileUploadUrl:
        defaultSettings.apiChaochiUrl + 'SecurityForm/ImgUpload',
      headers: [],
      loading: false,
      showclose: true,
      uploadmsg: '確認上傳',
      options: [
        { value: '01', label: '1樓-出入口' },
        { value: '02', label: '門牌' },
        { value: '03', label: '上下樓梯' },
        { value: '04', label: '物件-門口照(含鎖具)' },
        { value: '05', label: '鐵窗或隔音窗' },
        { value: '06', label: '火警警報器或偵煙器' },
        { value: '07', label: '滅火器(遠)' },
        { value: '08', label: '滅火器(近)' },
        { value: '09', label: '熱水器-1' },
        { value: '10', label: '熱水器-2' },
        { value: '11', label: '房間-1' },
        { value: '12', label: '房間-2' },
        { value: '13', label: '房間-3' },
        { value: '14', label: '房間-4' },
        { value: '15', label: '廁所-1' },
        { value: '16', label: '廁所-2' },
        { value: '17', label: '陽台' },
        { value: '18', label: '公共環境' }
      ],
      fileList: [],
      limitfilescount: 16,
      send: {
        BAdd: '',
        title: ''
      },
      historyFormVisible: false,
      PreviewVisible: false,
      imgs: [],
      buildingAddreadonly: true,
      title: {
        buildingAdd: '',
        SecurityFormCVer: ''
      },
      currentSelected: [],
      currentId: ''
    };
  },
  watch: {
    $route: {
      handler(a, b) {
        // console.log('route改變了', a, b)
        this.loaddata();
      }
    }
  },
  created() {
    this.headers = { Authorization: 'Bearer ' + (getToken() || '') };
    this.loaddata();
  },
  methods: {
    Preview() {
      PreviewPDF(this.title.buildingAdd, this.imgs, true).then(res => {
        const blob = res; // new Blob([response.data], { type: 'image/jpeg' })
        const link = document.createElement('a');
        link.href = URL.createObjectURL(blob);
        link.target = '_blank';
        // link.download = this.filename
        link.click();
        URL.revokeObjectURL(link.href);
      });
      // this.PreviewVisible = true
    },
    GenPDF1() {
      GenPDF(this.title.buildingAdd, this.imgs, false).then(res => {
        if (res.Success) {
          this.$message({
            message: 'PDF產生成功! 可至建物歷史表單下載!',
            type: 'success'
          });
        }
      });
    },
    moveUpward(row, index) {
      if (index > 0) {
        const upData = this.imgs[index - 1];
        this.imgs.splice(index - 1, 1);
        this.imgs.splice(index, 0, upData);
      } else {
        this.$message({
          message: '已经是第一条，上移失败',
          type: 'warning'
        });
      }
    },
    moveDown(row, index) {
      if (index + 1 === this.imgs.length) {
        this.$message({
          message: '已经是最后一条，下移失败',
          type: 'warning'
        });
      } else {
        const downData = this.imgs[index + 1];
        this.imgs.splice(index + 1, 1);
        this.imgs.splice(index, 0, downData);
      }
    },
    onexceed(a, b) {
      this.$message({
        message: ` 此建物僅能再上傳${this.limitfilescount}張圖檔!`,
        type: 'error'
      });
      this.$refs.newupload.clearFiles();
    },
    onremove(file) {
      var arr = [];
      for (let i = 0; i < this.fileList.length; i++) {
        if (this.fileList[i].uid !== file.uid) {
          arr.push(this.fileList[i]);
        }
      }
      this.fileList = arr;
    },
    async submitUpload() {
      if (this.send.title === '') {
        this.$message.error('請選擇主題!');
        return;
      }
      if (this.fileList.length === 0) {
        this.$message.error('請選擇圖檔!');
        return;
      }
      if (this.fileList.length > this.limitfilescount) {
        this.$message.error(`此建物只能再上傳${this.limitfilescount}張圖檔`);
        return;
      }
      this.showclose = false;
      this.loading = true;
      this.uploadmsg = '上傳中，請稍後';
      const formData = new FormData();
      for (var i = 0; i < this.fileList.length; i++) {
        // console.log(this.fileList.length)
        var compressedBlob = await this.compressAsync(this.fileList[i].raw);
        // console.log(errr)
        var files = new File([compressedBlob], this.fileList[i].name, {
          type: 'image/jpeg'
        });
        // console.log(files)
        if (files.size > 210000) {
          // console.log('第N次壓縮前', errr)
          compressedBlob = await this.compressAsync(files);
          files = new File([compressedBlob], this.fileList[i].name, {
            type: 'image/jpeg'
          });
          // console.log('第N次壓縮後', files)
        }
        formData.append('File', files);
      }

      // imageConversion
      //   .compressAccurately(this.fileList[0].raw, 250)
      //   .then(res => {
      //     console.log(res)
      //     var files = new File([res], this.fileList[0].name, {
      //       type: 'image/jpeg'
      //     })
      //     console.log('files', files)
      //     formData.append('File', files)
      //   })

      formData.append('BAdd', this.send.BAdd);
      formData.append('title', this.send.title);
      ImgUpload(formData).then(res => {
        if (res.Success) {
          this.$message.success('恭喜你，上傳成功');
          this.historyFormVisible = false;
          this.showclose = true;
          this.loading = false;
          this.uploadmsg = '確認上傳';
          this.send.title = '';
          // this.send.BAdd = '''
          this.fileList = [];
          this.$refs.upload.clearFiles();
          this.loaddataAfterUpdate();
        } else {
          this.$message.error(res.ErrMsg);
          this.historyFormVisible = false;
          this.showclose = true;
          this.loading = false;
          this.uploadmsg = '確認上傳';
          this.send.title = '';
          // this.send.BAdd = ''
          this.fileList = [];
          this.$refs.upload.clearFiles();
          this.loaddataAfterUpdate();
        }
      });
    },
    onchange(fileList) {
      this.fileList.push(fileList);
    },

    async compressAsync(file) {
      const res = await imageConversion.compressAccurately(file, {
        size: 400,
        scale: 1
      });
      return res;
    },

    loaddata: function() {
      if (
        this.$route.params.FormId === 'A000301' &&
        this.$route.params.searchPDF.BAdd_1 !== ''
      ) {
        handelAdd(this.$route.params.searchPDF).then(res => {
          this.title.buildingAdd = res;
          this.send.BAdd = res;
          Getimgslist(this.title.buildingAdd).then(res => {
            this.imgs = res.ResData.BuildingImages;
            if (this.imgs === null) {
              this.limitfilescount = 16;
            } else {
              this.limitfilescount = 16 - this.imgs.length;
            }
            this.imgs?.forEach(item =>
              getByFileName(item.FileName, this.title.buildingAdd)
                .then(response => {
                  item.ImgUrl = URL.createObjectURL(response);
                  this.tableloading = false;
                })
                .catch(error => {
                  console.log(error);
                })
            );
          });
        });
      }
    },
    loaddataAfterUpdate: function() {
      Getimgslist(this.title.buildingAdd).then(res => {
        this.imgs = res.ResData.BuildingImages;
        if (this.imgs === null) {
          this.limitfilescount = 16;
        } else {
          this.limitfilescount = 16 - this.imgs.length;
        }
        this.imgs?.forEach(item =>
          getByFileName(item.FileName, this.title.buildingAdd)
            .then(response => {
              item.ImgUrl = URL.createObjectURL(response);
              this.tableloading = false;
            })
            .catch(error => {
              console.log(error);
            })
        );
      });
    },
    Insert() {
      if (this.limitfilescount === 0) {
        this.$message.error('此建物不得再新增圖檔');
      } else {
        this.historyFormVisible = true;
      }
    },
    deleteimg: async function() {
      // console.log(this.editFromF)
      if (this.currentSelected.length === 0) {
        this.$alert('請先選擇要操作的數據', '提示');
        return false;
      } else {
        for (const element of this.currentSelected) {
          // console.log(element.FileName)
          await delImg(this.title.buildingAdd, element)
            .then(response => {
              this.$message({
                message: element.title + '-' + element.FileName + ' 刪除成功!!',
                type: 'success'
              });
              this.tableloading = false;
              this.currentSelected = [];
            })
            .catch(error => {
              console.log(error);
              this.$message({
                message: element.FileName + ' 刪除失敗:' + error,
                type: 'error'
              });
            });
        }
        // })
        this.tableloading = false;
        this.loaddata();
      }
    },
    /**
     * 當用戶手動勾選checkbox數據行事件
     */
    handleSelectChange: function(selection, row) {
      this.currentSelected = selection;
    },
    /**
     * 當用戶手動勾選全選checkbox事件
     */
    handleSelectAllChange: function(selection) {
      this.currentSelected = selection;
    }
  }
};
</script>

<style>
.el-select-dropdown__item {
  text-align: center;
}
</style>
