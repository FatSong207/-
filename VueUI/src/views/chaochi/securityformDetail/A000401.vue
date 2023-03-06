<template>
  <el-card class="box-card">
    <h3>現有主題及照片A000401</h3>
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
        <el-select
          v-model="title.SecurityFormCVer"
          placeholder=""
          style="width: 120px"
        >
          <el-option
            v-for="item in SecurityFormCVerOptions"
            :key="item.index"
            :label="item"
            :value="item"
          />
        </el-select>
        <!-- <el-input
          v-model="title.SecurityFormCVer"
          clearable
          style="width: 15%"
        /> -->
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
            prop="size"
            label="照片圖框"
          >
            <template slot-scope="scope">
              {{ scope.row.size=== 'M' ? '小' : scope.row.size=== 'L' ? '大' : '無設定' }}
            </template>
          </el-table-column>
          <el-table-column
            prop="isAppear"
            label="強制顯示圖框"
          >
            <template slot-scope="scope">
              {{ scope.row.isAppear=== '1' ? '是' : scope.row.isAppear=== '0' ? '否' : '無設定' }}
            </template>
          </el-table-column>
          <el-table-column
            prop="Note"
            label="縮圖"
          >
            <template slot-scope="scope">
              <!-- <img :src="scope.row.ImgUrl==null?'XX':scope.row.ImgUrl" min-width="70" height="70">{{ scope.row.FileName }} -->
              <div style="width: 80px; ">
                <expandable-image
                  :src="scope.row.ImgUrl==null?'':scope.row.ImgUrl"
                  alt="無上傳"
                />
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
            @click="GenPDF()"
          >產生歷史表單</el-button>
        </el-button-group>
      </el-form-item>
      <el-dialog
        title="表單"
        :visible.sync="historyFormVisible"
        width="520px"
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
              :action="httpFileUploadUrl"
              :headers="headers"
              :auto-upload="false"
              :data="send"
              drag
              :file-list="fileList"
              :limit="1"
              list-type="picture"
              :on-change="onchange"
              :on-exceed="onexceed"
              :on-remove="onremove"
              accept=".png,.jpg,.gif,.jpeg"
            >
              <em class="el-icon-upload" />
              <div class="el-upload__text">將文件拖到此處，或<em>點擊上傳</em> </div>
              <!-- <el-button
                size="small"
                type="primary"
                icon="el-icon-folder-add"
              >選擇文件</el-button> -->
            </el-upload>
            <el-form-item label="圖框大小">
              <el-radio
                v-model="radioSize"
                label="M"
              >小</el-radio>
              <el-radio
                v-model="radioSize"
                label="L"
              >大</el-radio>
            </el-form-item>
            <el-form-item label="強制顯示圖框">
              <el-radio
                v-model="radioIsAppear"
                :disabled="radiodisable"
                label="1"
              >是</el-radio>
              <el-radio
                v-model="radioIsAppear"
                :disabled="radiodisable"
                label="0"
              >否</el-radio>
            </el-form-item>
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
  GetimgslistG,
  getByFileNameG,
  ImgUploadG,
  GenPDFG,
  PreviewPDFG,
  delImgG,
  GetVNoListByFormId
} from '@/api/chaochi/SecurityForm/securityformService';
import defaultSettings from '@/settings';
import { getToken } from '@/utils/auth';
import * as imageConversion from 'image-conversion';
export default {
  name: 'A000401',
  data() {
    return {
      httpFileUploadUrl:
        defaultSettings.apiChaochiUrl + 'SecurityForm/ImgUploadG',
      headers: [],
      radioSize: 'M',
      radioIsAppear: '1',
      radiodisable: false,
      loading: false,
      showclose: true,
      uploadmsg: '確認上傳',
      options: [
        { value: '01', label: '1. 門牌及大門' },
        { value: '02', label: '2. 衛浴設備[馬桶、洗面盆及浴缸(或淋浴)' },
        { value: '03', label: '3. 出入口及樓梯間' },
        {
          value: '04',
          label: '4. 滅火器〔必要，且在有效期限內〕、獨立型偵煙器／火警警報器'
        },
        { value: '05', label: '5. 熱水器' },
        { value: '06', label: '6. 室內設備（自行選擇對租金有影響之設備）' }
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
      SecurityFormCVerOptions: [],
      currentSelected: [],
      currentId: ''
    };
  },
  watch: {
    $route: {
      handler(a, b) {
        this.loaddata();
      }
    },
    fileList: {
      handler() {
        if (this.fileList.length !== 0) {
          this.radioIsAppear = '1';
          this.radiodisable = true;
        } else {
          this.radiodisable = false;
        }
      }
    }
  },
  created() {
    this.headers = { Authorization: 'Bearer ' + (getToken() || '') };
    this.loaddata();
  },
  methods: {
    Preview() {
      PreviewPDFG(
        this.title.buildingAdd,
        this.title.SecurityFormCVer,
        this.imgs,
        true
      ).then(res => {
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
    GenPDF() {
      GenPDFG(
        this.title.buildingAdd,
        this.title.SecurityFormCVer,
        this.imgs,
        false
      ).then(res => {
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
          message: '已经是第一条，上移失败.',
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
        message: ` 僅限單張上傳`,
        type: 'error'
      });
      // this.$refs.newupload.clearFiles()
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
      // if (this.fileList.length === 0) {
      //   this.$message.error('請選擇圖檔!')
      //   return
      // }
      if (this.fileList.length > this.limitfilescount) {
        this.$message.error(`此建物只能再上傳${this.limitfilescount}張圖檔`);
        return;
      }
      this.showclose = false;
      this.loading = true;
      this.uploadmsg = '上傳中，請稍後';
      const formData = new FormData();
      if (this.fileList.length > 0) {
        for (var i = 0; i < this.fileList.length; i++) {
          var compressedBlob = await this.compressAsync(this.fileList[i].raw);
          var files = new File([compressedBlob], this.fileList[i].name, {
            type: 'image/jpeg'
          });
          // if (files.size > 210000) {
          //   compressedBlob = await this.compressAsync(files);
          //   files = new File([compressedBlob], this.fileList[i].name, {
          //     type: 'image/jpeg'
          //   });
          // }
          formData.append('File', files);
        }
      } else {
        formData.append('File', '');
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
      formData.append('size', this.radioSize);
      formData.append('isAppear', this.radioIsAppear);
      ImgUploadG(formData).then(res => {
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
        this.$route.params.FormId === 'A000401' &&
        this.$route.params.searchPDF.BAdd_1 !== ''
      ) {
        GetVNoListByFormId('A000401').then(res => {
          console.log(res.ResData);
          this.SecurityFormCVerOptions = res.ResData;
        });
        handelAdd(this.$route.params.searchPDF).then(res => {
          this.title.buildingAdd = res;
          this.send.BAdd = res;
          GetimgslistG(this.title.buildingAdd).then(res => {
            this.imgs = res.ResData.BuildingImages;
            if (this.imgs === null) {
              this.limitfilescount = 16;
            } else {
              this.limitfilescount = 16 - this.imgs.length;
            }
            this.imgs?.forEach(item =>
              getByFileNameG(item.FileName, this.title.buildingAdd)
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
      GetimgslistG(this.title.buildingAdd).then(res => {
        this.imgs = res.ResData.BuildingImages;
        if (this.imgs === null) {
          this.limitfilescount = 16;
        } else {
          this.limitfilescount = 16 - this.imgs.length;
        }
        this.imgs?.forEach(item =>
          getByFileNameG(item.FileName, this.title.buildingAdd)
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
          await delImgG(this.title.buildingAdd, element)
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
</style>
