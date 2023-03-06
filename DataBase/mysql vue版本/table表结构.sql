﻿/*
 Navicat Premium Data Transfer

 Source Server         : 本機
 Source Server Type    : MySQL
 Source Server Version : 80017
 Source Host           : localhost:3306
 Source Schema         : ybnf

 Target Server Type    : MySQL
 Target Server Version : 80017
 File Encoding         : 65001

 Date: 24/01/2021 22:10:41
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for cms_articlecategory
-- ----------------------------
DROP TABLE IF EXISTS `cms_articlecategory`;
CREATE TABLE `cms_articlecategory`  (
  `Id` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '主鍵',
  `Title` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '標題',
  `ParentId` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '父級Id',
  `ClassPath` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '全路徑',
  `ClassLayer` int(11) NULL DEFAULT NULL COMMENT '層級',
  `SortCode` int(11) NOT NULL DEFAULT 99 COMMENT '排序',
  `Description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL COMMENT '描述',
  `LinkUrl` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '外鏈地址',
  `ImgUrl` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '主圖圖片',
  `SeoTitle` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT 'SEO標題',
  `SeoKeywords` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT 'SEO關鍵詞',
  `SeoDescription` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT 'SEO描述',
  `IsHot` tinyint(1) NULL DEFAULT NULL COMMENT '是否熱門',
  `EnabledMark` tinyint(1) NULL DEFAULT NULL COMMENT '是否可用',
  `DeleteMark` tinyint(1) NULL DEFAULT NULL COMMENT '刪除標志',
  `CreatorTime` datetime(0) NULL DEFAULT NULL COMMENT '創建時間',
  `CreatorUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '創建人',
  `CompanyId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '所屬公司',
  `DeptId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '所屬部門',
  `LastModifyTime` datetime(0) NULL DEFAULT NULL COMMENT '最近修改時間',
  `LastModifyUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '最近修改人',
  `DeleteTime` datetime(0) NULL DEFAULT NULL COMMENT '刪除時間',
  `DeleteUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '刪除人',
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `Id`(`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '文章分類' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for cms_articlenews
-- ----------------------------
DROP TABLE IF EXISTS `cms_articlenews`;
CREATE TABLE `cms_articlenews`  (
  `Id` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '主鍵',
  `CategoryId` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '文章分類',
  `CategoryName` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '分類名稱',
  `Title` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '文章標題',
  `SubTitle` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '副標題',
  `LinkUrl` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '外鏈',
  `ImgUrl` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '主圖',
  `SeoTitle` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT 'SEO標題',
  `SeoKeywords` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT 'SEO關鍵詞',
  `SeoDescription` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT 'SEO描述',
  `Tags` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '標簽，多個用逗號隔開',
  `Zhaiyao` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '摘要',
  `Description` mediumtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL COMMENT '詳情',
  `SortCode` int(11) NULL DEFAULT NULL COMMENT '排序',
  `IsMsg` tinyint(1) NULL DEFAULT NULL COMMENT '開啟評論',
  `IsTop` tinyint(1) NULL DEFAULT NULL COMMENT '是否置頂，默認不置頂',
  `IsRed` tinyint(1) NULL DEFAULT NULL COMMENT '是否推薦',
  `IsHot` tinyint(1) NULL DEFAULT NULL COMMENT '是否熱門，默認否',
  `IsSys` tinyint(1) NULL DEFAULT NULL COMMENT '是否是系統預置文章，不可刪除',
  `IsNew` tinyint(1) NULL DEFAULT NULL COMMENT '是否推薦到最新',
  `IsSlide` tinyint(1) NULL DEFAULT NULL COMMENT '是否推薦到幻燈',
  `Click` int(11) NULL DEFAULT NULL COMMENT '點擊量',
  `LikeCount` int(11) NULL DEFAULT NULL COMMENT '喜歡量',
  `TotalBrowse` int(11) NULL DEFAULT NULL COMMENT '瀏覽量',
  `Source` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '來源',
  `Author` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '作者',
  `EnabledMark` tinyint(1) NULL DEFAULT NULL COMMENT '是否發布',
  `DeleteMark` tinyint(1) NULL DEFAULT NULL COMMENT '邏輯刪除標志',
  `CreatorTime` timestamp(0) NOT NULL COMMENT '創建時間',
  `CreatorUserId` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '創建人',
  `CompanyId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '所屬公司',
  `DeptId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '所屬部門',
  `LastModifyTime` datetime(0) NULL DEFAULT NULL COMMENT '最近修改時間',
  `LastModifyUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '最近修改人',
  `DeleteTime` datetime(0) NULL DEFAULT NULL COMMENT '刪除時間',
  `DeleteUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '刪除人',
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `Id`(`Id`, `CategoryId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '文章，通知公告' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for sys_app
-- ----------------------------
DROP TABLE IF EXISTS `sys_app`;
CREATE TABLE `sys_app`  (
  `Id` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `AppId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `AppSecret` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `EncodingAESKey` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `RequestUrl` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Token` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `IsOpenAEKey` tinyint(1) NULL DEFAULT NULL,
  `DeleteMark` tinyint(1) NOT NULL DEFAULT 1,
  `EnabledMark` tinyint(1) NOT NULL DEFAULT 0,
  `Description` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `CreatorTime` datetime(0) NOT NULL,
  `CreatorUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `CompanyId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `DeptId` varchar(254) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `LastModifyTime` datetime(0) NULL DEFAULT NULL,
  `LastModifyUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `DeleteTime` datetime(0) NULL DEFAULT NULL,
  `DeleteUserId` varchar(254) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '調用接口應用表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for sys_area
-- ----------------------------
DROP TABLE IF EXISTS `sys_area`;
CREATE TABLE `sys_area`  (
  `Id` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `ParentId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Layers` int(8) NULL DEFAULT NULL,
  `EnCode` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `FullName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `SimpleSpell` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `FullIdPath` varchar(600) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `IsLast` tinyint(1) NULL DEFAULT NULL,
  `SortCode` int(8) NULL DEFAULT NULL,
  `DeleteMark` tinyint(1) NULL DEFAULT NULL,
  `EnabledMark` tinyint(1) NULL DEFAULT NULL,
  `Description` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `CreatorTime` datetime(0) NULL DEFAULT NULL,
  `CreatorUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `LastModifyTime` datetime(0) NULL DEFAULT NULL,
  `LastModifyU` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `DeleteTime` datetime(0) NULL DEFAULT NULL,
  `DeleteUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '地區信息' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for sys_dbbackup
-- ----------------------------
DROP TABLE IF EXISTS `sys_dbbackup`;
CREATE TABLE `sys_dbbackup`  (
  `Id` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `BackupType` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `DbName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `FileName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `FileSize` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `FilePath` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `BackupTime` datetime(0) NULL DEFAULT NULL,
  `SortCode` int(8) NULL DEFAULT NULL,
  `DeleteMark` tinyint(1) NULL DEFAULT NULL,
  `EnabledMark` tinyint(1) NULL DEFAULT NULL,
  `Description` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `CreatorTime` datetime(0) NULL DEFAULT NULL,
  `CreatorUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `LastModifyTime` datetime(0) NULL DEFAULT NULL,
  `LastModifyUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `DeleteTime` datetime(0) NULL DEFAULT NULL,
  `DeleteUserId` varchar(254) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '數據庫備份' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for sys_filterip
-- ----------------------------
DROP TABLE IF EXISTS `sys_filterip`;
CREATE TABLE `sys_filterip`  (
  `Id` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `FilterType` tinyint(1) NULL DEFAULT NULL,
  `StartIP` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `EndIP` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `SortCode` int(8) NULL DEFAULT NULL,
  `DeleteMark` tinyint(1) NULL DEFAULT NULL,
  `EnabledMark` tinyint(1) NULL DEFAULT NULL,
  `Description` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `CreatorTime` datetime(0) NULL DEFAULT NULL,
  `CreatorUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `LastModifyTime` datetime(0) NULL DEFAULT NULL,
  `LastModifyUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `DeleteTime` datetime(0) NULL DEFAULT NULL,
  `DeleteUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '訪問ip地址控制' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for sys_items
-- ----------------------------
DROP TABLE IF EXISTS `sys_items`;
CREATE TABLE `sys_items`  (
  `Id` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `ParentId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `EnCode` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `FullName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `IsTree` tinyint(1) NULL DEFAULT NULL,
  `Layers` int(8) NULL DEFAULT NULL,
  `SortCode` int(8) NULL DEFAULT NULL,
  `DeleteMark` tinyint(1) NULL DEFAULT NULL,
  `EnabledMark` tinyint(1) NULL DEFAULT NULL,
  `Description` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `CreatorTime` datetime(0) NULL DEFAULT NULL,
  `CreatorUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `LastModifyTime` datetime(0) NULL DEFAULT NULL,
  `LastModifyUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `DeleteTime` datetime(0) NULL DEFAULT NULL,
  `DeleteUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '數據字典主表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for sys_itemsdetail
-- ----------------------------
DROP TABLE IF EXISTS `sys_itemsdetail`;
CREATE TABLE `sys_itemsdetail`  (
  `Id` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `ItemId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `ParentId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `ItemCode` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `ItemName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `SimpleSpelling` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `IsDefault` tinyint(1) NULL DEFAULT NULL,
  `Layers` int(8) NULL DEFAULT NULL,
  `SortCode` int(8) NULL DEFAULT NULL,
  `DeleteMark` tinyint(1) NULL DEFAULT NULL,
  `EnabledMark` tinyint(1) NULL DEFAULT NULL,
  `Description` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `CreatorTime` datetime(0) NULL DEFAULT NULL,
  `CreatorUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `LastModifyTime` datetime(0) NULL DEFAULT NULL,
  `LastModifyUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `DeleteTime` date NULL DEFAULT NULL,
  `DeleteUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '數據字典分錄' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for sys_log
-- ----------------------------
DROP TABLE IF EXISTS `sys_log`;
CREATE TABLE `sys_log`  (
  `Id` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `Date` datetime(0) NULL DEFAULT NULL,
  `Account` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `NickName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `OrganizeId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Type` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `IPAddress` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `IPAddressName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `ModuleId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `ModuleName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Result` tinyint(1) NULL DEFAULT NULL,
  `Description` mediumtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `DeleteMark` tinyint(1) NULL DEFAULT NULL,
  `EnabledMark` tinyint(1) NULL DEFAULT NULL,
  `CreatorTime` datetime(0) NULL DEFAULT NULL,
  `CreatorUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `LastModifyTime` datetime(0) NULL DEFAULT NULL,
  `LastModifyUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `DeleteTime` datetime(0) NULL DEFAULT NULL,
  `DeleteUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '日志表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for sys_menu
-- ----------------------------
DROP TABLE IF EXISTS `sys_menu`;
CREATE TABLE `sys_menu`  (
  `Id` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '主鍵',
  `SystemTypeId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '所屬系統',
  `ParentId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '父級ID',
  `Layers` int(8) NULL DEFAULT NULL COMMENT '所屬層級',
  `EnCode` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '功能代碼',
  `FullName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '名稱',
  `Icon` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '圖標',
  `UrlAddress` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '路由地址',
  `Component` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '組件路徑',
  `Target` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '打開方式',
  `MenuType` char(1) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '類型（C目錄 M菜單 F按鈕）',
  `ActiveMenu` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '設置當前選中菜單，用于新增、編輯、查看操作為單獨的路由時指定選中菜單路由',
  `IsExpand` tinyint(1) NULL DEFAULT NULL COMMENT '是否展開',
  `IsFrame` tinyint(1) NOT NULL DEFAULT 0 COMMENT '是否外鏈',
  `IsShow` tinyint(1) NOT NULL DEFAULT 1 COMMENT '是否顯示',
  `IsCache` tinyint(1) NOT NULL DEFAULT 0 COMMENT '是否緩存',
  `IsPublic` tinyint(1) NOT NULL DEFAULT 0 COMMENT '是否是公共',
  `AllowEdit` tinyint(1) NULL DEFAULT 1 COMMENT '是否可以編輯',
  `AllowDelete` tinyint(1) NULL DEFAULT 1 COMMENT '是否可以刪除',
  `SortCode` int(8) NULL DEFAULT NULL COMMENT '排序',
  `DeleteMark` tinyint(1) NULL DEFAULT NULL COMMENT '刪除標志',
  `EnabledMark` tinyint(1) NULL DEFAULT NULL COMMENT '是否可用',
  `Description` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '詳細描述',
  `CreatorTime` datetime(0) NULL DEFAULT NULL COMMENT '創建時間',
  `CreatorUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '創建人',
  `LastModifyTime` datetime(0) NULL DEFAULT NULL COMMENT '最近修改時間',
  `LastModifyUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '最近修改人',
  `DeleteTime` datetime(0) NULL DEFAULT NULL COMMENT '刪除時間',
  `DeleteUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '刪除人',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '模塊/菜單功能' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for sys_openidsettings
-- ----------------------------
DROP TABLE IF EXISTS `sys_openidsettings`;
CREATE TABLE `sys_openidsettings`  (
  `OpenIdType` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Name` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Description` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `Settings` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '第三方開放平臺配置表，如微信、支付寶' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for sys_organize
-- ----------------------------
DROP TABLE IF EXISTS `sys_organize`;
CREATE TABLE `sys_organize`  (
  `Id` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `ParentId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Layers` int(8) NULL DEFAULT NULL,
  `EnCode` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `FullName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `ShortName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `CategoryId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `ManagerId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `TelePhone` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `MobilePhone` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `WeChat` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Fax` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Email` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `AreaId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Address` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `AllowEdit` tinyint(1) NULL DEFAULT NULL,
  `AllowDelete` tinyint(1) NULL DEFAULT NULL,
  `SortCode` int(8) NULL DEFAULT NULL,
  `DeleteMark` tinyint(1) NULL DEFAULT NULL,
  `EnabledMark` tinyint(1) NULL DEFAULT NULL,
  `Description` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `CreatorTime` datetime(0) NULL DEFAULT NULL,
  `CreatorUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `LastModifyTime` datetime(0) NULL DEFAULT NULL,
  `LastModifyUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `DeleteTime` datetime(0) NULL DEFAULT NULL,
  `DeleteUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '組織機構' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for sys_role
-- ----------------------------
DROP TABLE IF EXISTS `sys_role`;
CREATE TABLE `sys_role`  (
  `Id` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `OrganizeId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Category` int(8) NULL DEFAULT NULL,
  `EnCode` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `FullName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Type` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `AllowEdit` tinyint(1) NULL DEFAULT NULL,
  `AllowDelete` tinyint(1) NULL DEFAULT NULL,
  `SortCode` int(8) NULL DEFAULT NULL,
  `DeleteMark` tinyint(1) NULL DEFAULT NULL,
  `EnabledMark` tinyint(1) NULL DEFAULT NULL,
  `Description` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `CreatorTime` datetime(0) NULL DEFAULT NULL,
  `CreatorUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `LastModifyTime` datetime(0) NULL DEFAULT NULL,
  `LastModifyUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `DeleteTime` datetime(0) NULL DEFAULT NULL,
  `DeleteUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '角色' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for sys_roleauthorize
-- ----------------------------
DROP TABLE IF EXISTS `sys_roleauthorize`;
CREATE TABLE `sys_roleauthorize`  (
  `Id` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `ItemType` int(8) NULL DEFAULT NULL,
  `ItemId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `ObjectType` int(8) NULL DEFAULT NULL,
  `ObjectId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `SortCode` int(8) NULL DEFAULT NULL,
  `CreatorTime` datetime(0) NULL DEFAULT NULL,
  `CreatorUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '角色可訪問功能模塊' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for sys_roledata
-- ----------------------------
DROP TABLE IF EXISTS `sys_roledata`;
CREATE TABLE `sys_roledata`  (
  `Id` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `RoleId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `DType` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `AuthorizeData` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '角色可以訪問數據' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for sys_sequence
-- ----------------------------
DROP TABLE IF EXISTS `sys_sequence`;
CREATE TABLE `sys_sequence`  (
  `Id` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `SequenceName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `SequenceDelimiter` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `SequenceReset` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Step` int(11) NULL DEFAULT NULL,
  `CurrentNo` int(11) NULL DEFAULT NULL,
  `CurrentCode` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `CurrentReset` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Description` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `EnabledMark` tinyint(1) NOT NULL DEFAULT 1,
  `DeleteMark` tinyint(1) NULL DEFAULT 0,
  `CreatorTime` datetime(0) NOT NULL,
  `CreatorUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `CompanyId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `DeptId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `LastModifyTime` datetime(0) NULL DEFAULT NULL,
  `LastModifyUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `DeleteTime` datetime(0) NULL DEFAULT NULL,
  `DeleteUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '業務單據編碼表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for sys_sequencerule
-- ----------------------------
DROP TABLE IF EXISTS `sys_sequencerule`;
CREATE TABLE `sys_sequencerule`  (
  `Id` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `SequenceName` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `RuleOrder` int(11) NOT NULL,
  `RuleType` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `RuleValue` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `PaddingSide` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `PaddingWidth` int(11) NULL DEFAULT NULL,
  `Sys_SequenceRulecol` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `PaddingChar` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Description` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `EnabledMark` tinyint(1) NOT NULL DEFAULT 1,
  `DeleteMark` tinyint(1) NULL DEFAULT 0,
  `CreatorTime` datetime(0) NOT NULL,
  `CreatorUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `CompanyId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `DeptId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `LastModifyTime` datetime(0) NULL DEFAULT NULL,
  `LastModifyUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `DeleteTime` datetime(0) NULL DEFAULT NULL,
  `DeleteUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '業務單據編碼規則表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for sys_systemtype
-- ----------------------------
DROP TABLE IF EXISTS `sys_systemtype`;
CREATE TABLE `sys_systemtype`  (
  `Id` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `EnCode` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `FullName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Url` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `AllowEdit` tinyint(1) NULL DEFAULT NULL,
  `AllowDelete` tinyint(1) NULL DEFAULT NULL,
  `SortCode` int(8) NULL DEFAULT NULL,
  `DeleteMark` tinyint(1) NULL DEFAULT NULL,
  `EnabledMark` tinyint(1) NULL DEFAULT NULL,
  `Description` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `CreatorTime` datetime(0) NULL DEFAULT NULL,
  `CreatorUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `LastModifyTime` datetime(0) NULL DEFAULT NULL,
  `LastModifyUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `DeleteTime` datetime(0) NULL DEFAULT NULL,
  `DeleteUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '系統類型，支持多個子系統' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for sys_taskjobslog
-- ----------------------------
DROP TABLE IF EXISTS `sys_taskjobslog`;
CREATE TABLE `sys_taskjobslog`  (
  `Id` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `TaskId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `TaskName` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `JobAction` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Status` tinyint(1) NOT NULL DEFAULT 0,
  `Description` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `CreatorTime` datetime(0) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '定時任務日志' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for sys_taskmanager
-- ----------------------------
DROP TABLE IF EXISTS `sys_taskmanager`;
CREATE TABLE `sys_taskmanager`  (
  `Id` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `TaskName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `GroupName` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `StartTime` datetime(0) NULL DEFAULT NULL,
  `EndTime` datetime(0) NULL DEFAULT NULL,
  `Cron` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `IsLocal` tinyint(1) NOT NULL DEFAULT 0,
  `JobCallAddress` varchar(150) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `JobCallParams` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `LastRunTime` datetime(0) NULL DEFAULT NULL,
  `LastErrorTime` datetime(0) NULL DEFAULT NULL,
  `NextRunTime` datetime(0) NULL DEFAULT NULL,
  `RunCount` int(11) NULL DEFAULT NULL,
  `ErrorCount` int(11) NULL DEFAULT NULL,
  `Description` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `SendMail` int(11) NULL DEFAULT 0,
  `EmailAddress` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Status` int(11) NULL DEFAULT NULL,
  `EnabledMark` tinyint(1) NOT NULL DEFAULT 1,
  `DeleteMark` tinyint(1) NULL DEFAULT 0,
  `CreatorTime` datetime(0) NOT NULL,
  `CreatorUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `CompanyId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `DeptId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `LastModifyTime` datetime(0) NULL DEFAULT NULL,
  `LastModifyUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `DeleteTime` datetime(0) NULL DEFAULT NULL,
  `DeleteUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '定時任務' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for sys_tenant
-- ----------------------------
DROP TABLE IF EXISTS `sys_tenant`;
CREATE TABLE `sys_tenant`  (
  `Id` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `TenantName` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CompanyName` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `HostDomain` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `LinkMan` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Telphone` varchar(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `DataSource` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Description` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `EnabledMark` tinyint(1) NOT NULL DEFAULT 1,
  `DeleteMark` tinyint(1) NULL DEFAULT 0,
  `CreatorTime` datetime(0) NOT NULL,
  `CreatorUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `CompanyId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `DeptId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `LastModifyTime` datetime(0) NULL DEFAULT NULL,
  `LastModifyUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `DeleteTime` datetime(0) NULL DEFAULT NULL,
  `DeleteUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '租戶' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for sys_uploadfile
-- ----------------------------
DROP TABLE IF EXISTS `sys_uploadfile`;
CREATE TABLE `sys_uploadfile`  (
  `Id` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `FileName` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `FilePath` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Description` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `FileType` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `FileSize` int(8) NULL DEFAULT NULL,
  `Extension` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `EnabledMark` tinyint(1) NULL DEFAULT NULL,
  `SortCode` int(8) NULL DEFAULT NULL,
  `DeleteMark` tinyint(1) NULL DEFAULT NULL,
  `CreatorUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `CreateUserName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `CreatorTime` datetime(0) NULL DEFAULT NULL,
  `Thumbnail` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `BelongApp` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `BelongAppId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '附件管理，包括文檔文件和圖片' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for sys_user
-- ----------------------------
DROP TABLE IF EXISTS `sys_user`;
CREATE TABLE `sys_user`  (
  `Id` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `Account` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `RealName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `NickName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `HeadIcon` varchar(254) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Gender` int(8) NULL DEFAULT NULL,
  `Birthday` datetime(0) NULL DEFAULT NULL,
  `MobilePhone` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Email` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `WeChat` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `ManagerId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `SecurityLevel` int(8) NULL DEFAULT NULL,
  `Signature` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Country` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Province` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `City` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `District` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `OrganizeId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `DepartmentId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `RoleId` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `DutyId` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `IsAdministrator` tinyint(1) NULL DEFAULT NULL,
  `IsMember` tinyint(1) NULL DEFAULT NULL,
  `MemberGradeId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `ReferralUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `UnionId` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `SortCode` int(8) NULL DEFAULT NULL,
  `DeleteMark` tinyint(1) NULL DEFAULT NULL,
  `EnabledMark` tinyint(1) NULL DEFAULT NULL,
  `Description` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `CreatorTime` datetime(0) NULL DEFAULT NULL,
  `CreatorUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `LastModifyTime` datetime(0) NULL DEFAULT NULL,
  `LastModifyUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `DeleteTime` datetime(0) NULL DEFAULT NULL,
  `DeleteUserId` varchar(254) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '用戶基本信息' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for sys_userextend
-- ----------------------------
DROP TABLE IF EXISTS `sys_userextend`;
CREATE TABLE `sys_userextend`  (
  `Id` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `UserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `CardContent` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `Telphone` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Address` varchar(254) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `CompanyName` varchar(254) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `PositionTite` varchar(254) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `WechatQrCode` varchar(254) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `IndustryId` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `OpenType` int(8) NULL DEFAULT NULL,
  `IsOtherShare` varchar(254) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `ShareTitle` varchar(254) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `WebUrl` varchar(254) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `CompanyDesc` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `CardTheme` varchar(254) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `VipGrade` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `IsAuthentication` tinyint(1) NULL DEFAULT NULL,
  `AuthenticationType` int(8) NULL DEFAULT NULL,
  `EnabledMark` tinyint(1) NULL DEFAULT NULL,
  `DeleteMark` tinyint(1) NULL DEFAULT NULL,
  `CreatorTime` datetime(0) NULL DEFAULT NULL,
  `CreatorUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `CompanyId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `DeptId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `LastModifyTime` datetime(0) NULL DEFAULT NULL,
  `LastModifyUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `DeleteTime` datetime(0) NULL DEFAULT NULL,
  `DeleteUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '用戶擴展信息' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for sys_userlogon
-- ----------------------------
DROP TABLE IF EXISTS `sys_userlogon`;
CREATE TABLE `sys_userlogon`  (
  `Id` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `UserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `UserPassword` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '密碼',
  `UserSecretkey` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '加密密鑰',
  `AllowStartTime` datetime(0) NULL DEFAULT NULL,
  `AllowEndTime` datetime(0) NULL DEFAULT NULL,
  `LockStartDate` datetime(0) NULL DEFAULT NULL,
  `LockEndDate` datetime(0) NULL DEFAULT NULL,
  `FirstVisitTime` datetime(0) NULL DEFAULT NULL,
  `PreviousVisitTime` datetime(0) NULL DEFAULT NULL,
  `LastVisitTime` datetime(0) NULL DEFAULT NULL,
  `ChangePasswordDate` datetime(0) NULL DEFAULT NULL,
  `MultiUserLogin` tinyint(1) NULL DEFAULT NULL,
  `LogOnCount` int(8) NULL DEFAULT NULL,
  `UserOnLine` tinyint(1) NULL DEFAULT NULL,
  `Question` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `AnswerQuestion` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `CheckIPAddress` tinyint(1) NULL DEFAULT NULL,
  `Language` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '軟件語言',
  `Theme` mediumtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL COMMENT '軟件風格設置信息',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '用戶登錄信息' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for sys_useropenids
-- ----------------------------
DROP TABLE IF EXISTS `sys_useropenids`;
CREATE TABLE `sys_useropenids`  (
  `UserId` varchar(254) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '用戶Id',
  `OpenIdType` varchar(254) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '第三方平臺類型',
  `OpenId` varchar(254) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '第三方的OpenId'
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '用戶與第三方開放平臺openid對應關系表' ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;
