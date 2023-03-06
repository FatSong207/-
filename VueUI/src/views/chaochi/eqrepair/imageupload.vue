<template>
  <div>
    <el-form>
      <el-card
        v-loading="loading"
        v-loading.lock="loading"
        class="box-card"
        element-loading-text="加載中..."
        element-loading-spinner="el-icon-loading"
      >
        <el-row>
          <!-- <el-col :span="6">
          <el-form-item label="維修前近照：">
            <el-upload
              ref="upload"
              :action="httpFileUploadUrl"
              :headers="headers"
              auto-upload
              :data="send"
              :show-file-list="false"
              list-type="picture-card"
              :limit="1"
              accept=".png,.jpg,.gif,.jpeg"
              :before-upload="beforeupload"
              :on-success="onsuccess"
            >
              <i class="el-icon-plus" />
            </el-upload>
          </el-form-item>
        </el-col> -->
          <el-col :span="12">
            <el-form-item label="維修前遠照：">
              <img
                width="150px"
                height="150px"
                :src="photo.RBC==null?'':photo.RBC"
                alt="無上傳"
              >
            </el-form-item>
          </el-col>
          <!-- <el-col :span="6">
          <el-form-item label="維修前遠照：">
            <el-upload
              ref="upload"
              :action="httpFileUploadUrl"
              :headers="headers"
              auto-upload
              :data="send"
              :show-file-list="false"
              list-type="picture-card"
              :limit="1"
              accept=".png,.jpg,.gif,.jpeg"
              :before-upload="beforeupload"
              :on-success="onsuccess"
            >
              <i class="el-icon-plus" />
            </el-upload>
          </el-form-item>
        </el-col> -->
          <el-col :span="12">
            <el-form-item label="維修前遠照：">
              <img
                width="150px"
                height="150px"
                :src="photo.RBC==null?'':photo.RBF"
                alt="無上傳"
              >
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="6">
            <el-form-item
              label="維修中近照："
              label-width="1"
            >
              <img
                width="150px"
                height="150px"
                :src="photo.RBC==null?'':photo.RC"
                alt="無上傳"
              >
            </el-form-item>
          </el-col>
          <el-col :span="6">
            <el-form-item>
              <el-upload
                ref="uploadRC"
                :action="httpFileUploadUrl"
                :headers="headers"
                auto-upload
                :data="send"
                :show-file-list="false"
                list-type="picture-card"
                :limit="1"
                accept=".png,.jpg,.gif,.jpeg"
                :before-upload="beforeuploadRC"
                :on-success="onsuccess"
              >
                <i class="el-icon-plus" />
              </el-upload>
            </el-form-item>
          </el-col>
          <el-col :span="6">
            <el-form-item
              label="維修中遠照："
              label-width="1"
            >
              <img
                width="150px"
                height="150px"
                :src="photo.RBC==null?'':photo.RF"
                alt="無上傳"
              >
            </el-form-item>
          </el-col>
          <el-col :span="6">
            <el-form-item>
              <el-upload
                ref="uploadRF"
                :action="httpFileUploadUrl"
                :headers="headers"
                auto-upload
                :data="send"
                :show-file-list="false"
                list-type="picture-card"
                :limit="1"
                accept=".png,.jpg,.gif,.jpeg"
                :before-upload="beforeuploadRF"
                :on-success="onsuccess"
              >
                <i class="el-icon-plus" />
              </el-upload>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="6">
            <el-form-item
              label="維修後近照："
              label-width="1"
            >
              <img
                width="150px"
                height="150px"
                :src="photo.RBC==null?'':photo.RAC"
                alt="無上傳"
              >
            </el-form-item>
          </el-col>
          <el-col :span="6">
            <el-form-item>
              <el-upload
                ref="uploadRAC"
                :action="httpFileUploadUrl"
                :headers="headers"
                auto-upload
                :data="send"
                :show-file-list="false"
                list-type="picture-card"
                :limit="1"
                accept=".png,.jpg,.gif,.jpeg"
                :before-upload="beforeuploadRAC"
                :on-success="onsuccess"
              >
                <i class="el-icon-plus" />
              </el-upload>
            </el-form-item>
          </el-col>
          <el-col :span="6">
            <el-form-item
              label="維修後遠照："
              label-width="1"
            >
              <img
                width="150px"
                height="150px"
                :src="photo.RBC==null?'':photo.RAF"
                alt="無上傳"
              >
            </el-form-item>
          </el-col>
          <el-col :span="6">
            <el-form-item>
              <el-upload
                ref="uploadRAF"
                :action="httpFileUploadUrl"
                :headers="headers"
                auto-upload
                :data="send"
                :show-file-list="false"
                list-type="picture-card"
                :limit="1"
                accept=".png,.jpg,.gif,.jpeg"
                :before-upload="beforeuploadRAF"
                :on-success="onsuccess"
              >
                <i class="el-icon-plus" />
              </el-upload>
            </el-form-item>
          </el-col>
        </el-row>
      </el-card>
    </el-form>
  </div>
</template>

<script>
import { getByFileName, Getimgslist } from '@/api/chaochi/eqrepair/eqrepair';
import defaultSettings from '@/settings';
import { getToken } from '@/utils/auth';
import * as imageConversion from 'image-conversion';
export default {
  name: 'ImageUpload',
  props: {
    id: { type: String, default: '' },
    guid: { type: String, default: '' },
    emitflag: { type: Boolean, default: false },
    emitresetflag: { type: Boolean, default: false }
  },
  data() {
    return {
      httpFileUploadUrl: defaultSettings.apiChaochiUrl + 'EqRepair/ImgUpload',
      headers: [],
      send: {
        id: '',
        guid: '',
        type: '',
        File: ''
      },
      photo: {
        RBC: '',
        RBF: '',
        RC: '',
        RF: '',
        RAC: '',
        RAF: ''
      },
      loading: false
    };
  },
  watch: {
    // id: {
    //   handler() {
    //     this.loadImage();
    //   }
    // },
    emitflag: {
      handler() {
        this.loadImage();
      }
    },
    emitresetflag: {
      handler() {
        this.reset();
      }
    }
  },
  created() {
    this.headers = { Authorization: 'Bearer ' + (getToken() || '') };
    this.loadImage();
  },
  methods: {
    reset() {
      this.photo = {
        RBC: '',
        RBF: '',
        RC: '',
        RF: '',
        RAC: '',
        RAF: ''
      };
    },
    async beforeuploadRC(file) {
      this.loading = true;
      this.send.id = this.id;
      this.send.guid = this.guid;
      this.send.type = 'RC';
      this.send.File = await this.compressImageAsync(file);
    },
    async beforeuploadRF(file) {
      this.loading = true;
      this.send.id = this.id;
      this.send.guid = this.guid;
      this.send.type = 'RF';
      this.send.File = await this.compressImageAsync(file);
    },
    async beforeuploadRAC(file) {
      this.loading = true;
      this.send.id = this.id;
      this.send.guid = this.guid;
      this.send.type = 'RAC';
      this.send.File = await this.compressImageAsync(file);
    },
    async beforeuploadRAF(file) {
      this.loading = true;
      this.send.id = this.id;
      this.send.guid = this.guid;
      this.send.type = 'RAF';
      this.send.File = await this.compressImageAsync(file);
    },
    onsuccess(response) {
      if (response.Success) {
        this.$message.success('上傳成功!');
      } else {
        this.$message.error('上傳錯誤!');
      }
      // this.$message.success('上傳成功!');
      this.loadImage();
      this.$refs.uploadRC.clearFiles();
      this.$refs.uploadRF.clearFiles();
      this.$refs.uploadRAC.clearFiles();
      this.$refs.uploadRAF.clearFiles();
    },
    async compressImageAsync(file) {
      var compressedBlob = await this.compressAsync(file);
      var files = new File([compressedBlob], file.name, {
        type: 'image/jpeg'
      });
      return files;
    },
    async compressAsync(file) {
      const res = await imageConversion.compressAccurately(file, {
        size: 400,
        scale: 1
      });
      return res;
    },
    loadImage: function() {
      this.loading = true;
      Getimgslist(this.id).then(res => {
        if (res.ResData.EqRepairImages !== null) {
          this.photo.RBC = res.ResData.EqRepairImages[0].BeforeRepairClose;
          this.photo.RBF = res.ResData.EqRepairImages[0].BeforeRepairFar;
          this.photo.RC = res.ResData.EqRepairImages[0].RepairingClose;
          this.photo.RF = res.ResData.EqRepairImages[0].RepairingFar;
          this.photo.RAC = res.ResData.EqRepairImages[0].AfterRepairClose;
          this.photo.RAF = res.ResData.EqRepairImages[0].AfterRepairFar;

          getByFileName(this.photo.RBC, this.id, this.guid).then(response => {
            this.photo.RBC = URL.createObjectURL(response);
          });
          getByFileName(this.photo.RBF, this.id, this.guid).then(response => {
            this.photo.RBF = URL.createObjectURL(response);
          });
          getByFileName(this.photo.RC, this.id, this.guid).then(response => {
            this.photo.RC = URL.createObjectURL(response);
          });
          getByFileName(this.photo.RF, this.id, this.guid).then(response => {
            this.photo.RF = URL.createObjectURL(response);
          });
          getByFileName(this.photo.RAC, this.id, this.guid).then(response => {
            this.photo.RAC = URL.createObjectURL(response);
          });
          getByFileName(this.photo.RAF, this.id, this.guid).then(response => {
            this.photo.RAF = URL.createObjectURL(response);
          });
        }
        this.loading = false;
      });
    }
  }
};
</script>

<style>
</style>
