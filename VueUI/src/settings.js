module.exports = {
  title: '管理系統',
  /**
   * 側邊欄主題 深色主題theme-dark，淺色主題theme-light
   */
  sideTheme: 'theme-dark',
  /**
   * 是否系統布局配置
   */
  showSettings: false,

  /**
   * 是否顯示 tagsView
   */
  tagsView: true,
  /**
   *是否固定頭部
   */
  fixedHeader: false,
  /**
   * 是否顯示側邊Logo
   */
  sidebarLogo: true,

  /**
   * 應用Id
   */
  appId: 'system',
  /**
   * 應用密鑰
   */
  appSecret: '87135AB0160F706D8B47F06BDABA6FC6',
  /**
   * 子系統
   */
  subSystem: {},
  /**
   * 當前訪問系統代碼
   */
  activeSystemCode: 'openauth',
  /**
   * 當前訪問系統名稱
   */
  activeSystemName: '',
  /**
   * 動態可訪問路由
   */
  addRouters: {},

  // apiHostUrl: 'http://netcoreapi.ts.yuebon.com/api/', // 基礎接口
  // apiSecurityUrl: 'http://netcoreapi.ts.yuebon.com/api/Security/', // 權限管理系統接口
  // apiCMSUrl: 'http://netcoreapi.ts.yuebon.com/api/CMS/', // 文章
  // fileUrl: 'http://netcoreapi.ts.yuebon.com/', // 文件訪問路徑
  // fileUploadUrl: 'http://netcoreapi.ts.yuebon.com/api/Files/Upload'// 文件上傳路徑

  // apiHostUrl: 'http://localhost:54678/api/', // 基礎接口
  // apiSecurityUrl: 'http://localhost:54678/api/Security/', // 權限管理系統接口
  // apiCMSUrl: 'http://localhost:54678/api/CMS/', // 文章
  // fileUrl: 'http://localhost:54678/', // 文件訪問路徑
  // fileUploadUrl: 'http://localhost:54678/api/Files/Upload'// 文件上傳路徑

  // apiHostUrl: 'https://localhost:44370/api/', // 基礎接口
  // apiSecurityUrl: 'https://localhost:44370/api/Security/', // 權限管理系統接口
  // apiCMSUrl: 'https://localhost:44370/api/CMS/', // 文章
  // fileUrl: 'https://localhost:44370/', // 文件訪問路徑
  // fileUploadUrl: 'https://localhost:44370/api/Files/Upload' // 文件上傳路徑

  // apiHostUrl: 'https://localhost:5001/api/', // 基礎接口
  // apiSecurityUrl: 'https://localhost:5001/api/Security/', // 權限管理系統接口
  // apiCMSUrl: 'https://localhost:5001/api/CMS/', // 文章
  // fileUrl: 'https://localhost:5001/', // 文件訪問路徑
  // fileUploadUrl: 'https://localhost:5001/api/Files/Upload', // 文件上傳路徑  //
  // apiChaochiUrl: 'https://localhost:5001/api/Chaochi/' // chaochi系統接口

  // # ugee base api
  apiHostUrl: process.env.VUE_APP_UGEE_BASE_API + '/api/', // 基礎接口
  apiSecurityUrl: process.env.VUE_APP_UGEE_BASE_API + '/api/Security/', // 權限管理系統接口
  apiCMSUrl: process.env.VUE_APP_UGEE_BASE_API + '/api/CMS/', // 文章
  fileUrl: process.env.VUE_APP_UGEE_BASE_API + '/', // 文件訪問路徑
  fileUploadUrl: process.env.VUE_APP_UGEE_BASE_API + '/api/Files/Upload', // 文件上傳路徑  //
  apiChaochiUrl: process.env.VUE_APP_UGEE_BASE_API + '/api/Chaochi/' // chaochi系統接口

  // http://192.168.1.7:5000/
  // apiHostUrl: 'http://192.168.1.7:5000/api/', // 基礎接口
  // apiSecurityUrl: 'http://192.168.1.7:5000/api/Security/', // 權限管理系統接口
  // apiCMSUrl: 'http://192.168.1.7:5000/api/CMS/', // 文章
  // fileUrl: 'http://192.168.1.7:5000/', // 文件訪問路徑
  // fileUploadUrl: 'http://192.168.1.7:5000/api/Files/Upload' // 文件上傳路徑

  // apiHostUrl: 'https://192.168.1.218:5001/api/', // 基礎接口
  // apiSecurityUrl: 'https://192.168.1.218:5001/api/Security/', // 權限管理系統接口
  // apiCMSUrl: 'https://192.168.1.218:5001/api/CMS/', // 文章
  // fileUrl: 'https://192.168.1.218:5001/', // 文件訪問路徑
  // fileUploadUrl: 'https://192.168.1.218:5001/api/Files/Upload' // 文件上傳路徑

  // apiHostUrl: 'http://192.168.1.218:5001/api/', // 基礎接口
  // apiSecurityUrl: 'http://192.168.1.218:5001/api/Security/', // 權限管理系統接口
  // apiCMSUrl: 'http://192.168.1.218:5001/api/CMS/', // 文章
  // fileUrl: 'http://192.168.1.218:5001/', // 文件訪問路徑
  // fileUploadUrl: 'http://192.168.1.218:5001/api/Files/Upload' // 文件上傳路徑  //http://192.168.1.7:5000/
  // dotnet Yuebon.WebApi.dll --urls "http://localhost:5100;http://192.168.1.218:5000;https://192.168.1.218:5001"
  // dotnet Yuebon.WebApi.dll --urls "http://localhost:5100;http://192.168.1.218:5000;https://192.168.1.218:5001"

  // apiHostUrl: 'https://localhost:5001/api/', // 基礎接口
  // apiSecurityUrl: 'https://localhost:5001/api/Security/', // 權限管理系統接口
  // apiCMSUrl: 'https://localhost:5001/api/CMS/', // 文章
  // fileUrl: 'https://localhost:5001/', // 文件訪問路徑
  // fileUploadUrl: 'https://localhost:5001/api/Files/Upload' // 文件上傳路徑

  // apiHostUrl: 'http://193.168.25.137:8082/api/', // 基礎接口
  // apiSecurityUrl: 'http://193.168.25.137:8082/api/Security/', // 權限管理系統接口
  // apiCMSUrl: 'http://193.168.25.137:8082/api/CMS/', // 文章
  // fileUrl: 'http://193.168.25.137:8082/', // 文件訪問路徑
  // fileUploadUrl: 'http://193.168.25.137:8082/api/Files/Upload'// 文件上傳路徑
}
