﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Yuebon.Commons.IoC;
using Yuebon.Commons.Models;
using Yuebon.Commons.Options;

namespace Yuebon.Commons.CodeGenerator
{
    /// <summary>
    /// 代碼生成器。
    /// <remarks>
    /// 根據指定的實體域名空間生成Repositories和Services層的基礎代碼文件。
    /// </remarks>
    /// </summary>
    public class CodeGenerator
    {
        /// <summary>
        /// 代碼生成器配置
        /// </summary>
        private static CodeGenerateOption _option=new CodeGenerateOption();
        /// <summary>
        /// InputDto輸入實體是不包含字段
        /// </summary>
        private static string inputDtoNoField= "DeleteMark,CreatorTime,CreatorUserId,CompanyId,DeptId,LastModifyTime,LastModifyUserId,DeleteTime,DeleteUserId,";
        /// <summary>
        /// 靜態構造函數：從IoC容器讀取配置參數，如果讀取失敗則會拋出ArgumentNullException異常
        /// </summary>
        static CodeGenerator()
        {
        }
        /// <summary>
        /// 代碼生成器入口方法
        /// </summary>
        /// <param name="baseNamespace"></param>
        /// <param name="tableList">要生成代碼的表</param>
        /// <param name="replaceTableNameStr">要刪除表名稱的字符</param>
        /// <param name="ifExsitedCovered">是否替換現有文件，為true時替換</param>
        public static void Generate(string baseNamespace, string tableList, string replaceTableNameStr,bool ifExsitedCovered = false)
        {
            _option.DtosNamespace = baseNamespace + ".Dtos";
            _option.ModelsNamespace = baseNamespace + ".Models";
            _option.IRepositoriesNamespace = baseNamespace + ".IRepositories";
            _option.RepositoriesNamespace = baseNamespace + ".Repositories";
            _option.IServicsNamespace = baseNamespace + ".IServices";
            _option.ServicesNamespace = baseNamespace + ".Services";
            _option.ApiControllerNamespace = baseNamespace + "Api";
            _option.ReplaceTableNameStr = replaceTableNameStr;
            _option.TableList = tableList;
            _option.BaseNamespace = baseNamespace;

            DbExtractor dbExtractor = new DbExtractor();
            List<DbTableInfo> listTable = dbExtractor.GetWhereTables(_option.TableList);
            string profileContent = string.Empty;
            foreach (DbTableInfo dbTableInfo in listTable)
            {
               
                List<DbFieldInfo> listField = dbExtractor.GetAllColumns(dbTableInfo.TableName);
                GenerateSingle(listField, dbTableInfo, ifExsitedCovered);
                string tableName = dbTableInfo.TableName;
                if (!string.IsNullOrEmpty(_option.ReplaceTableNameStr))
                {
                    string[] rel = _option.ReplaceTableNameStr.Split(';');
                    for (int i = 0; i < rel.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(rel[i].ToString()))
                        {
                            tableName = tableName.Replace(rel[i].ToString(), "");
                        }
                    }
                }
                tableName = tableName.Substring(0, 1).ToUpper() + tableName.Substring(1);
                profileContent += string.Format("           CreateMap<{0}, {0}OutputDto>();\n", tableName);
                profileContent += string.Format("           CreateMap<{0}InputDto, {0}>();\n", tableName);
            }            
           
            GenerateDtoProfile(_option.ModelsNamespace, profileContent, ifExsitedCovered);
        }

        /// <summary>
        /// 單表生成代碼
        /// </summary>
        /// <param name="listField">表字段集合</param>
        /// <param name="tableInfo">表信息</param>
        /// <param name="ifExsitedCovered">如果目標文件存在，是否覆蓋。默認為false</param>
        public static void GenerateSingle(List<DbFieldInfo> listField, DbTableInfo tableInfo,bool ifExsitedCovered = false)
        {
            var modelsNamespace =_option.ModelsNamespace;
            var modelTypeName = tableInfo.TableName;//表名
            var modelTypeDesc = tableInfo.Description;//表描述
            if (!string.IsNullOrEmpty(_option.ReplaceTableNameStr))
            {
                string[] rel = _option.ReplaceTableNameStr.Split(';');
                for (int i = 0; i < rel.Length; i++)
                {
                    if (!string.IsNullOrEmpty(rel[i].ToString()))
                    {
                        modelTypeName = modelTypeName.Replace(rel[i].ToString(),"");
                    }
                }
            }
            modelTypeName = modelTypeName.Substring(0, 1).ToUpper() + modelTypeName.Substring(1);
            string keyTypeName = "string";//主鍵數據類型
            string modelcontent = "";//數據庫模型字段
            string InputDtocontent = "";//輸入模型
            string outputDtocontent = "";//輸出模型
            string vueViewListContent = string.Empty;//Vue列表輸出內容
            string vueViewFromContent = string.Empty;//Vue表單輸出內容 
            string vueViewEditFromContent = string.Empty;//Vue變量輸出內容
            string vueViewEditFromBindContent = string.Empty;//Vue顯示初始化輸出內容
            string vueViewSaveBindContent = string.Empty;//Vue保存時輸出內容
            string vueViewEditFromRuleContent = string.Empty;//Vue數據校驗

            foreach (DbFieldInfo dbFieldInfo in listField)
            {
                string fieldName = dbFieldInfo.FieldName.Substring(0, 1).ToUpper() + dbFieldInfo.FieldName.Substring(1);
                //主鍵
                if (dbFieldInfo.IsIdentity)
                {
                    keyTypeName = dbFieldInfo.DataType;
                    outputDtocontent += "        /// <summary>\n";
                    outputDtocontent += string.Format("        /// 設置或獲取{0}\n", dbFieldInfo.Description);
                    outputDtocontent += "        /// </summary>\n";
                    if (dbFieldInfo.DataType == "string")
                    {
                        outputDtocontent += string.Format("        [MaxLength({0})]\n", dbFieldInfo.FieldMaxLength);
                    }
                    outputDtocontent += string.Format("        public {0} {1}", dbFieldInfo.DataType, fieldName);
                    outputDtocontent += " { get; set; }\n\r";
                }else //非主鍵
                {
                    modelcontent += "        /// <summary>\n";
                    modelcontent += string.Format("        /// 設置或獲取{0}\n", dbFieldInfo.Description);
                    modelcontent += "        /// </summary>\n";
                    if (dbFieldInfo.FieldType == "string")
                    {
                        modelcontent += string.Format("        [MaxLength({0})]\n", dbFieldInfo.FieldMaxLength);
                    }
                    modelcontent += string.Format("        public {0} {1}", dbFieldInfo.DataType, fieldName);
                    modelcontent += " { get; set; }\n\r";


                    outputDtocontent += "        /// <summary>\n";
                    outputDtocontent += string.Format("        /// 設置或獲取{0}\n", dbFieldInfo.Description);
                    outputDtocontent += "        /// </summary>\n";
                    if (dbFieldInfo.DataType == "string")
                    {
                        outputDtocontent += string.Format("        [MaxLength({0})]\n", dbFieldInfo.FieldMaxLength);
                    }
                    outputDtocontent += string.Format("        public {0} {1}", dbFieldInfo.DataType, fieldName);
                    outputDtocontent += " { get; set; }\n\r";
                    if (dbFieldInfo.DataType == "bool"||dbFieldInfo.DataType== "tinyint")
                    {

                        vueViewListContent += string.Format("        <el-table-column prop=\"{0}\" label=\"{1}\" sortable=\"custom\" width=\"120\" >\n", fieldName, dbFieldInfo.Description);
                        vueViewListContent += "          <template slot-scope=\"scope\">\n";
                        vueViewListContent += string.Format("            <el-tag :type=\"scope.row.{0} === true ? 'success' : 'info'\"  disable-transitions >", fieldName);
                        vueViewListContent += "{{ ";
                        vueViewListContent += string.Format("scope.row.{0}===true?'啟用':'禁用' ", fieldName);
                        vueViewListContent += "}}</el-tag>\n";
                        vueViewListContent += "          </template>\n";
                        vueViewListContent += "        </el-table-column>\n";

                        vueViewFromContent += string.Format("        <el-form-item label=\"{0}\" :label-width=\"formLabelWidth\" prop=\"{1}\">", dbFieldInfo.Description, fieldName);
                        vueViewFromContent += string.Format("          <el-radio-group v-model=\"editFrom.{0}\">\n", fieldName);
                        vueViewFromContent += "           <el-radio label=\"true\">是</el-radio>\n";
                        vueViewFromContent += "           <el-radio label=\"false\">否</el-radio>\n";
                        vueViewFromContent += "          </el-radio-group>\n";
                        vueViewFromContent += "        </el-form-item>\n";

                        vueViewEditFromContent += string.Format("        {0}: 'true',\n", fieldName);
                        vueViewEditFromBindContent+= string.Format("        this.editFrom.{0} = res.ResData.{0}+''\n", fieldName);
                    }
                    else
                    {
                        vueViewListContent += string.Format("        <el-table-column prop=\"{0}\" label=\"{1}\" sortable=\"custom\" width=\"120\" />\n", fieldName, dbFieldInfo.Description);

                        vueViewFromContent += string.Format("        <el-form-item label=\"{0}\" :label-width=\"formLabelWidth\" prop=\"{1}\">\n", dbFieldInfo.Description, fieldName);
                        vueViewFromContent += string.Format("          <el-input v-model=\"editFrom.{0}\" placeholder=\"請輸入{1}\" autocomplete=\"off\" clearable />\n", fieldName, dbFieldInfo.Description);
                        vueViewFromContent += "        </el-form-item>\n";
                        vueViewEditFromContent += string.Format("        {0}: '',\n", fieldName);
                        vueViewEditFromBindContent += string.Format("        this.editFrom.{0} = res.ResData.{0}\n", fieldName);
                    }
                    vueViewSaveBindContent += string.Format("        '{0}':this.editFrom.{0},\n", fieldName);
                    if (!dbFieldInfo.IsNullable)
                    {
                        vueViewEditFromRuleContent += string.Format("        {0}: [\n", fieldName);
                        vueViewEditFromRuleContent += "        {";
                        vueViewEditFromRuleContent += string.Format("required: true, message:\"請輸入{0}\", trigger: \"blur\"", dbFieldInfo.Description);
                        vueViewEditFromRuleContent += "},\n          { min: 2, max: 50, message: \"長度在 2 到 50 個字符\", trigger:\"blur\" }\n";
                        vueViewEditFromRuleContent += "        ],\n";
                    }
                }

                if (!inputDtoNoField.Contains(dbFieldInfo.FieldName)||dbFieldInfo.FieldName=="Id")
                {
                    InputDtocontent += "        /// <summary>\n";
                    InputDtocontent += string.Format("        /// 設置或獲取{0}\n", dbFieldInfo.Description);
                    InputDtocontent += "        /// </summary>\n";
                    if (dbFieldInfo.FieldType == "string")
                    {
                        InputDtocontent += string.Format("        [MaxLength({0})]\n", dbFieldInfo.FieldMaxLength);
                    }
                    InputDtocontent += string.Format("        public {0} {1}", dbFieldInfo.DataType, fieldName);
                    InputDtocontent += " { get; set; }\n\r";
                }
                //
            }
            GenerateModels(modelsNamespace, modelTypeName, tableInfo.TableName, modelcontent, modelTypeDesc, keyTypeName, ifExsitedCovered);
            GenerateIRepository(modelTypeName, modelTypeDesc, keyTypeName, ifExsitedCovered);
            GenerateRepository(modelTypeName, modelTypeDesc, tableInfo.TableName, keyTypeName, ifExsitedCovered);
            GenerateIService(modelsNamespace, modelTypeName, modelTypeDesc, keyTypeName, ifExsitedCovered);
            GenerateService(modelsNamespace, modelTypeName, modelTypeDesc, keyTypeName, ifExsitedCovered);
            GenerateOutputDto(modelTypeName, modelTypeDesc, outputDtocontent, ifExsitedCovered);
            GenerateInputDto(modelsNamespace, modelTypeName, modelTypeDesc, InputDtocontent, keyTypeName, ifExsitedCovered);
            GenerateControllers(modelTypeName, modelTypeDesc, keyTypeName, ifExsitedCovered);
            GenerateVueViews(modelTypeName,modelTypeDesc,vueViewListContent,vueViewFromContent,vueViewEditFromContent,vueViewEditFromBindContent,vueViewSaveBindContent,vueViewEditFromRuleContent,ifExsitedCovered);
        }


        /// <summary>
        /// 從代碼模板中讀取內容
        /// </summary>
        /// <param name="templateName">模板名稱，應包括文件擴展名稱。比如：template.txt</param>
        /// <returns></returns>
        private static string ReadTemplate(string templateName)
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            var content = string.Empty;
            using (var stream = currentAssembly.GetManifestResourceStream($"{currentAssembly.GetName().Name}.CodeTemplate.{templateName}"))
            {
                if (stream != null)
                {
                    using var reader = new StreamReader(stream);
                    content = reader.ReadToEnd();
                }
            }
            return content;
        }

        /// <summary>
        /// 生成IRepository層代碼文件
        /// </summary>
        /// <param name="modelTypeName">實體類型</param>
        /// <param name="modelTypeDesc"></param>
        /// <param name="keyTypeName"></param>
        /// <param name="ifExsitedCovered">如果目標文件存在，是否覆蓋。默認為false</param>
        private static void GenerateIRepository(string modelTypeName, string modelTypeDesc, string keyTypeName, bool ifExsitedCovered = false)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            //path = path.Substring(0, path.IndexOf("\\bin"));
            var parentPath = path.Substring(0, path.LastIndexOf("\\"));
            var iRepositoryPath = parentPath + "\\" + _option.BaseNamespace + "\\" + _option.IRepositoriesNamespace;
            if (!Directory.Exists(iRepositoryPath))
            {
                iRepositoryPath = parentPath + "\\" + _option.BaseNamespace + "\\IRepositories";
                Directory.CreateDirectory(iRepositoryPath);
            }
            var fullPath = iRepositoryPath + "\\I" + modelTypeName + "Repository.cs";
            if (File.Exists(fullPath) && !ifExsitedCovered)
                return;
            var content = ReadTemplate("IRepositoryTemplate.txt");
            content = content.Replace("{ModelsNamespace}", _option.ModelsNamespace)
                .Replace("{IRepositoriesNamespace}", _option.IRepositoriesNamespace)
                .Replace("{ModelTypeName}", modelTypeName)
                .Replace("{TableNameDesc}", modelTypeDesc)
                .Replace("{KeyTypeName}", keyTypeName);
            WriteAndSave(fullPath, content);
        }
        /// <summary>
        /// 生成Repository層代碼文件
        /// </summary>
        /// <param name="modelTypeName"></param>
        /// <param name="modelTypeDesc"></param>
        /// <param name="tableName">表名</param>
        /// <param name="keyTypeName"></param>
        /// <param name="ifExsitedCovered">如果目標文件存在，是否覆蓋。默認為false</param>
        private static void GenerateRepository(string modelTypeName, string modelTypeDesc, string tableName,string keyTypeName, bool ifExsitedCovered = false)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            //path = path.Substring(0, path.IndexOf("\\bin"));
            var parentPath = path.Substring(0, path.LastIndexOf("\\"));
            var repositoryPath = parentPath + "\\" + _option.BaseNamespace + "\\" + _option.RepositoriesNamespace;
            if (!Directory.Exists(repositoryPath))
            {
                repositoryPath = parentPath + "\\" + _option.BaseNamespace + "\\Repositories";
                Directory.CreateDirectory(repositoryPath);
            }
            var fullPath = repositoryPath + "\\" + modelTypeName + "Repository.cs";
            if (File.Exists(fullPath) && !ifExsitedCovered)
                return;
            var content = ReadTemplate("RepositoryTemplate.txt");
            content = content.Replace("{ModelsNamespace}", _option.ModelsNamespace)
                .Replace("{IRepositoriesNamespace}", _option.IRepositoriesNamespace)
                .Replace("{RepositoriesNamespace}", _option.RepositoriesNamespace)
                .Replace("{ModelTypeName}", modelTypeName)
                .Replace("{TableNameDesc}", modelTypeDesc)
                .Replace("{TableName}", tableName)
                .Replace("{KeyTypeName}", keyTypeName);
            WriteAndSave(fullPath, content);
        }

        /// <summary>
        /// 生成IService文件
        /// </summary>
        /// <param name="modelsNamespace"></param>
        /// <param name="modelTypeName"></param>
        /// <param name="modelTypeDesc"></param>
        /// <param name="keyTypeName"></param>
        /// <param name="ifExsitedCovered">如果目標文件存在，是否覆蓋。默認為false</param>
        private static void GenerateIService(string modelsNamespace, string modelTypeName, string modelTypeDesc, string keyTypeName, bool ifExsitedCovered = false)
        {
            var iServicsNamespace = _option.IServicsNamespace;
            var path = AppDomain.CurrentDomain.BaseDirectory;
            //path = path.Substring(0, path.IndexOf("\\bin"));
            var parentPath = path.Substring(0, path.LastIndexOf("\\"));
            var iServicesPath = parentPath + "\\" + _option.BaseNamespace + "\\" + iServicsNamespace;
            if (!Directory.Exists(iServicesPath))
            {
                iServicesPath = parentPath + "\\" + _option.BaseNamespace + "\\IServices";
                Directory.CreateDirectory(iServicesPath);
            }
            var fullPath = iServicesPath + "\\I" + modelTypeName + "Service.cs";
            if (File.Exists(fullPath) && !ifExsitedCovered)
                return;
            var content = ReadTemplate("IServiceTemplate.txt");
            content = content.Replace("{ModelsNamespace}", modelsNamespace)
                .Replace("{DtosNamespace}", _option.DtosNamespace)
                .Replace("{TableNameDesc}", modelTypeDesc)
                .Replace("{IServicsNamespace}", iServicsNamespace)
                .Replace("{ModelTypeName}", modelTypeName)
                .Replace("{KeyTypeName}", keyTypeName);
            WriteAndSave(fullPath, content);
        }

        /// <summary>
        /// 生成Service文件
        /// </summary>
        /// <param name="modelsNamespace"></param>
        /// <param name="modelTypeName"></param>
        /// <param name="modelTypeDesc"></param>
        /// <param name="keyTypeName"></param>
        /// <param name="ifExsitedCovered">如果目標文件存在，是否覆蓋。默認為false</param>
        private static void GenerateService(string modelsNamespace, string modelTypeName, string modelTypeDesc, string keyTypeName, bool ifExsitedCovered = false)
        {
            var servicesNamespace = _option.ServicesNamespace;
            var path = AppDomain.CurrentDomain.BaseDirectory;
            //path = path.Substring(0, path.IndexOf("\\bin"));
            var parentPath = path.Substring(0, path.LastIndexOf("\\"));
            var servicesPath = parentPath + "\\" + _option.BaseNamespace + "\\" + servicesNamespace;
            if (!Directory.Exists(servicesPath))
            {
                servicesPath = parentPath + "\\" + _option.BaseNamespace + "\\Services";
                Directory.CreateDirectory(servicesPath);
            }
            var fullPath = servicesPath + "\\" + modelTypeName + "Service.cs";
            if (File.Exists(fullPath) && !ifExsitedCovered)
                return;
            var content = ReadTemplate("ServiceTemplate.txt");
            content = content
                .Replace("{IRepositoriesNamespace}", _option.IRepositoriesNamespace)
                .Replace("{DtosNamespace}", _option.DtosNamespace)
                .Replace("{IServicsNamespace}", _option.IServicsNamespace)
                .Replace("{TableNameDesc}", modelTypeDesc)
                .Replace("{ModelsNamespace}", modelsNamespace)
                .Replace("{ServicesNamespace}", servicesNamespace)
                .Replace("{ModelTypeName}", modelTypeName)
                .Replace("{KeyTypeName}", keyTypeName);
            WriteAndSave(fullPath, content);
        }

        /// <summary>
        /// 生成OutputDto文件
        /// </summary>
        /// <param name="modelTypeName"></param>
        /// <param name="modelTypeDesc"></param>
        /// <param name="modelContent"></param>
        /// <param name="ifExsitedCovered"></param>
        private static void GenerateOutputDto(string modelTypeName, string modelTypeDesc, string modelContent,  bool ifExsitedCovered = false)
        {
            var servicesNamespace = _option.ServicesNamespace;
            var path = AppDomain.CurrentDomain.BaseDirectory;
            //path = path.Substring(0, path.IndexOf("\\bin"));
            var parentPath = path.Substring(0, path.LastIndexOf("\\"));
            var servicesPath = parentPath + "\\" + _option.BaseNamespace + "\\" + servicesNamespace;
            if (!Directory.Exists(servicesPath))
            {
                servicesPath = parentPath + "\\" + _option.BaseNamespace + "\\Dtos";
                Directory.CreateDirectory(servicesPath);
            }
            var fullPath = servicesPath + "\\" + modelTypeName + "OutputDto.cs";
            if (File.Exists(fullPath) && !ifExsitedCovered)
                return;
            var content = ReadTemplate("OuputDtoTemplate.txt");
            content = content
                .Replace("{DtosNamespace}", _option.DtosNamespace)
                .Replace("{TableNameDesc}", modelTypeDesc)
                .Replace("{ModelContent}", modelContent)
                .Replace("{ModelTypeName}", modelTypeName);
            WriteAndSave(fullPath, content);
        }

        /// <summary>
        /// 生成InputDto文件
        /// </summary>
        /// <param name="modelsNamespace"></param>
        /// <param name="modelTypeName"></param>
        /// <param name="modelTypeDesc"></param>
        /// <param name="modelContent"></param>
        /// <param name="keyTypeName"></param>
        /// <param name="ifExsitedCovered">如果目標文件存在，是否覆蓋。默認為false</param>
        private static void GenerateInputDto(string modelsNamespace, string modelTypeName, string modelTypeDesc, string modelContent, string keyTypeName, bool ifExsitedCovered = false)
        {
            var servicesNamespace = _option.DtosNamespace;
            var path = AppDomain.CurrentDomain.BaseDirectory;
            //path = path.Substring(0, path.IndexOf("\\bin"));
            var parentPath = path.Substring(0, path.LastIndexOf("\\"));
            var servicesPath = parentPath + "\\" + _option.BaseNamespace + "\\" + servicesNamespace;
            if (!Directory.Exists(servicesPath))
            {
                servicesPath = parentPath + "\\" + _option.BaseNamespace + "\\Dtos";
                Directory.CreateDirectory(servicesPath);
            }
            var fullPath = servicesPath + "\\" + modelTypeName + "InputDto.cs";
            if (File.Exists(fullPath) && !ifExsitedCovered)
                return;
            var content = ReadTemplate("InputDtoTemplate.txt");
            content = content
                .Replace("{DtosNamespace}", _option.DtosNamespace)
                .Replace("{ModelsNamespace}", modelsNamespace)
                .Replace("{TableNameDesc}", modelTypeDesc)
                .Replace("{KeyTypeName}", keyTypeName)
                .Replace("{ModelContent}", modelContent)
                .Replace("{ModelTypeName}", modelTypeName);
            WriteAndSave(fullPath, content);
        }

        /// <summary>
        /// 生成Profile文件
        /// </summary>
        /// <param name="modelsNamespace"></param>
        /// <param name="profileContent"></param>
        /// <param name="ifExsitedCovered">如果目標文件存在，是否覆蓋。默認為false</param>
        private static void GenerateDtoProfile(string modelsNamespace, string profileContent,  bool ifExsitedCovered = false)
        {
            var servicesNamespace = _option.DtosNamespace;
            var path = AppDomain.CurrentDomain.BaseDirectory;
            //path = path.Substring(0, path.IndexOf("\\bin"));
            var parentPath = path.Substring(0, path.LastIndexOf("\\"));
            var servicesPath = parentPath + "\\" + _option.BaseNamespace + "\\" + servicesNamespace;
            if (!Directory.Exists(servicesPath))
            {
                servicesPath = parentPath + "\\" + _option.BaseNamespace + "\\Dtos";
                Directory.CreateDirectory(servicesPath);
            }
            var fileClassName = _option.BaseNamespace.Substring(_option.BaseNamespace.IndexOf('.')+1);
            var fullPath = servicesPath + "\\" + fileClassName + "Profile.cs";
            if (File.Exists(fullPath) && !ifExsitedCovered)
                return;
            var content = ReadTemplate("ProfileTemplate.txt");
            content = content
                .Replace("{DtosNamespace}", _option.DtosNamespace)
                .Replace("{ModelsNamespace}", modelsNamespace)
                .Replace("{ProfileContent}", profileContent)
                .Replace("{ClassName}", fileClassName);
            WriteAndSave(fullPath, content);
        }

        /// <summary>
        /// 生成Models文件
        /// </summary>
        /// <param name="modelsNamespace">命名空間</param>
        /// <param name="modelTypeName">類名</param>
        /// <param name="tableName">表名稱</param>
        /// <param name="modelTypeDesc">表描述</param>
        /// <param name="modelContent">數據庫表實體內容</param>
        /// <param name="keyTypeName">主鍵數據類型</param>
        /// <param name="ifExsitedCovered">如果目標文件存在，是否覆蓋。默認為false</param>
        private static void GenerateModels(string modelsNamespace, string modelTypeName,string tableName,  string modelContent, string modelTypeDesc, string keyTypeName, bool ifExsitedCovered = false)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            //path = path.Substring(0, path.IndexOf("\\bin"));
            var parentPath = path.Substring(0, path.LastIndexOf("\\"));
            var servicesPath = parentPath + "\\" + _option.BaseNamespace + "\\" + modelsNamespace;
            if (!Directory.Exists(servicesPath))
            {
                servicesPath = parentPath + "\\" + _option.BaseNamespace + "\\Models";
                Directory.CreateDirectory(servicesPath);
            }
            var fullPath = servicesPath + "\\" + modelTypeName + ".cs";
            if (File.Exists(fullPath) && !ifExsitedCovered)
                return;
            var content = ReadTemplate("ModelsTemplate.txt");
            content = content
                .Replace("{ModelsNamespace}", modelsNamespace)
                .Replace("{ModelTypeName}", modelTypeName)
                .Replace("{TableNameDesc}", modelTypeDesc)
                .Replace("{KeyTypeName}", keyTypeName)
                .Replace("{ModelContent}", modelContent)
                .Replace("{TableName}", tableName);
            WriteAndSave(fullPath, content);
        }


        /// <summary>
        /// 生成控制器ApiControllers文件
        /// </summary>
        /// <param name="modelTypeName">實體類型名稱</param>
        /// <param name="modelTypeDesc">實體描述</param>
        /// <param name="keyTypeName"></param>
        /// <param name="ifExsitedCovered">如果目標文件存在，是否覆蓋。默認為false</param>
        private static void GenerateControllers(string modelTypeName, string modelTypeDesc,string keyTypeName, bool ifExsitedCovered = false)
        {
            var servicesNamespace = _option.DtosNamespace;
            var fileClassName = _option.BaseNamespace.Substring(_option.BaseNamespace.IndexOf('.') + 1);
            var path = AppDomain.CurrentDomain.BaseDirectory;
            //path = path.Substring(0, path.IndexOf("\\bin"));
            var parentPath = path.Substring(0, path.LastIndexOf("\\"));
            var servicesPath = parentPath + "\\" + _option.BaseNamespace + "\\" + servicesNamespace;
            if (!Directory.Exists(servicesPath))
            {
                servicesPath = parentPath + "\\" + _option.BaseNamespace + "\\Areas\\"+ fileClassName;
                Directory.CreateDirectory(servicesPath);
            }
            var fullPath = servicesPath + "\\" + modelTypeName + "Controller.cs";
            if (File.Exists(fullPath) && !ifExsitedCovered)
                return;
            var content = ReadTemplate("ControllersTemplate.txt");
            content = content
                .Replace("{DtosNamespace}", _option.DtosNamespace)
                .Replace("{ApiControllerNamespace}", _option.ApiControllerNamespace)
                .Replace("{ModelsNamespace}", _option.ModelsNamespace)
                .Replace("{TableNameDesc}", modelTypeDesc)
                .Replace("{IServicsNamespace}", _option.IServicsNamespace)
                .Replace("{BaseNameSpaceEx}", fileClassName)
                .Replace("{ModelTypeName}", modelTypeName)
                .Replace("{KeyTypeName}", keyTypeName);
            WriteAndSave(fullPath, content);
        }

        /// <summary>
        /// 生成Vue頁面
        /// </summary>
        /// <param name="modelTypeName">類名</param>
        /// <param name="modelTypeDesc">表/類描述</param>
        /// <param name="vueViewListContent"></param>
        /// <param name="vueViewFromContent"></param>
        /// <param name="vueViewEditFromContent"></param>
        /// <param name="vueViewEditFromBindContent"></param>
        /// <param name="vueViewSaveBindContent"></param>
        /// <param name="vueViewEditFromRuleContent"></param>
        /// <param name="ifExsitedCovered">如果目標文件存在，是否覆蓋。默認為false</param>
        private static void GenerateVueViews(string modelTypeName, string modelTypeDesc, string vueViewListContent, string vueViewFromContent, string vueViewEditFromContent, string vueViewEditFromBindContent, string vueViewSaveBindContent, string vueViewEditFromRuleContent, bool ifExsitedCovered = false)
        {
            var servicesNamespace = _option.DtosNamespace;
            var fileClassName = _option.BaseNamespace.Substring(_option.BaseNamespace.IndexOf('.') + 1);
            var path = AppDomain.CurrentDomain.BaseDirectory;
            //path = path.Substring(0, path.IndexOf("\\bin"));
            var parentPath = path.Substring(0, path.LastIndexOf("\\"));
            var servicesPath = parentPath + "\\" + _option.BaseNamespace + "\\" + servicesNamespace;
            if (!Directory.Exists(servicesPath))
            {
                servicesPath = parentPath + "\\" + _option.BaseNamespace + "\\vue\\" +modelTypeName.ToLower();
                Directory.CreateDirectory(servicesPath);
            }
            var fullPath = servicesPath + "\\" +"index.vue";
            if (File.Exists(fullPath) && !ifExsitedCovered)
                return;
            var content = ReadTemplate("VueTemplate.txt");
            content = content
                .Replace("{BaseNamespace}", fileClassName.ToLower())
                .Replace("{fileClassName}", modelTypeName.ToLower())
                .Replace("{ModelTypeNameToLower}", modelTypeName.ToLower())
                .Replace("{VueViewListContent}", vueViewListContent)
                .Replace("{VueViewFromContent}", vueViewFromContent)
                .Replace("{ModelTypeName}", modelTypeName)
                .Replace("{VueViewEditFromContent}", vueViewEditFromContent)
                .Replace("{VueViewEditFromBindContent}", vueViewEditFromBindContent)
                .Replace("{VueViewSaveBindContent}", vueViewSaveBindContent)
                .Replace("{VueViewEditFromRuleContent}", vueViewEditFromRuleContent);
            WriteAndSave(fullPath, content);

            fullPath = servicesPath + "\\" + modelTypeName.ToLower() + ".js";
            if (File.Exists(fullPath) && !ifExsitedCovered)
                return;
            content = ReadTemplate("VueJsTemplate.txt");
            content = content
                .Replace("{ModelTypeName}", modelTypeName)
                .Replace("{ModelTypeDesc}", modelTypeDesc)
                .Replace("{fileClassName}", fileClassName);
            WriteAndSave(fullPath, content);
        }
        /// <summary>
        /// 寫文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="content"></param>
        private static void WriteAndSave(string fileName, string content)
        {
            //實例化一個文件流--->與寫入文件相關聯
            using var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            //實例化一個StreamWriter-->與fs相關聯
            using var sw = new StreamWriter(fs);
            //開始寫入
            sw.Write(content);
            //清空緩沖區
            sw.Flush();
            //關閉流
            sw.Close();
            fs.Close();
        }
    }
}
