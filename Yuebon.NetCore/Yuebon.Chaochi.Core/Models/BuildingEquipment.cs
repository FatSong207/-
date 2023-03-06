
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yuebon.Chaochi.Models
{
    /// <summary>
    /// 房屋物件，數據實體對象
    /// </summary>
    [Table("Chaochi_BuildingEquipment")]
    [Serializable]
    public class BuildingEquipment : BaseEntity<string>
    {
        /// <summary>
        /// 默認構造函數（需要初始化屬性的在此處理）
        /// </summary>
        public BuildingEquipment()
        {

        }

        #region 建物設備相關

        /// <summary>
        /// 建物地址
        /// </summary>
        public virtual string BAdd { get; set; }

        /// <summary>
        /// 點交日期_年
        /// </summary>
        public virtual string HandOver_Y { get; set; }

        /// <summary>
        /// 點交日期_月
        /// </summary>
        public virtual string HandOver_M { get; set; }

        /// <summary>
        /// 點交日期_日
        /// </summary>
        public virtual string HandOver_D { get; set; }

        /// <summary>
        /// 分離式冷氣
        /// </summary>
        public virtual string SAConditioner { get; set; }

        /// <summary>
        /// 分離式冷氣_數量
        /// </summary>
        public virtual string SAConditionerNo { get; set; }

        /// <summary>
        /// 分離式冷氣_品牌
        /// </summary>
        public virtual string SAConditionerBrand { get; set; }

        /// <summary>
        /// 窗型冷氣
        /// </summary>
        public virtual string WAConditioner { get; set; }

        /// <summary>
        /// 窗型冷氣_數量
        /// </summary>
        public virtual string WAConditionerNo { get; set; }

        /// <summary>
        /// 窗型冷氣_品牌
        /// </summary>
        public virtual string WAConditionerBrand { get; set; }

        /// <summary>
        /// 電視機
        /// </summary>
        public virtual string TVset { get; set; }

        /// <summary>
        /// 電視機_數量
        /// </summary>
        public virtual string TVsetNo { get; set; }

        /// <summary>
        /// 電視機_品牌
        /// </summary>
        public virtual string TVsetBrand { get; set; }

        /// <summary>
        /// 電冰箱
        /// </summary>
        public virtual string Refrigerator { get; set; }

        /// <summary>
        /// 電冰箱_數量
        /// </summary>
        public virtual string RefrigeratorNo { get; set; }

        /// <summary>
        /// 電冰箱_品牌
        /// </summary>
        public virtual string RefrigeratorBrand { get; set; }

        /// <summary>
        /// 瓦斯爐
        /// </summary>
        public virtual string GasStove { get; set; }

        /// <summary>
        /// 瓦斯爐_數量
        /// </summary>
        public virtual string GasStoveNo { get; set; }

        /// <summary>
        /// 瓦斯爐_品牌
        /// </summary>
        public virtual string GasStoveBrand { get; set; }

        /// <summary>
        /// 抽油煙機
        /// </summary>
        public virtual string RangeHood { get; set; }

        /// <summary>
        /// 抽油煙機_數量
        /// </summary>
        public virtual string RangeHoodNo { get; set; }

        /// <summary>
        /// 抽油煙機_品牌
        /// </summary>
        public virtual string RangeHoodBrand { get; set; }

        /// <summary>
        /// 電_熱水器
        /// </summary>
        public virtual string EWaterHeater { get; set; }

        /// <summary>
        /// 瓦斯_熱水器
        /// </summary>
        public virtual string GWaterHeater { get; set; }

        /// <summary>
        /// 電/瓦斯 熱水器_數量
        /// </summary>
        public virtual string WaterHeaterNo { get; set; }

        /// <summary>
        /// 電/瓦斯熱水器_品牌
        /// </summary>
        public virtual string WaterHeaterBrand { get; set; }

        /// <summary>
        /// 洗衣機
        /// </summary>
        public virtual string WashingMachine { get; set; }

        /// <summary>
        /// 洗衣機_數量
        /// </summary>
        public virtual string WashingMachineNo { get; set; }

        /// <summary>
        /// 洗衣機_品牌
        /// </summary>
        public virtual string WashingMachineBrand { get; set; }

        /// <summary>
        /// 一般馬桶
        /// </summary>
        public virtual string Toilet { get; set; }
        public virtual string ToiletNo { get; set; }

        /// <summary>
        /// 免治馬桶
        /// </summary>
        public virtual string Washlet { get; set; }
        public virtual string WashletNo { get; set; }

        /// <summary>
        /// 抽風機
        /// </summary>
        public virtual string ExhaustFan { get; set; }
        public virtual string ExhaustFanNo { get; set; }

        /// <summary>
        /// 暖風機
        /// </summary>
        public virtual string Heater { get; set; }
        public virtual string HeaterNo { get; set; }

        /// <summary>
        /// 水龍頭
        /// </summary>
        public virtual string Faucet { get; set; }
        public virtual string FaucetNo { get; set; }

        /// <summary>
        /// 洗臉盆
        /// </summary>
        public virtual string WashBasin { get; set; }
        public virtual string WashBasinNo { get; set; }

        /// <summary>
        /// 浴鏡組
        /// </summary>
        public virtual string BathMirror { get; set; }
        public virtual string BathMirrorNo { get; set; }

        /// <summary>
        /// 浴櫃
        /// </summary>
        public virtual string BathCabinet { get; set; }
        public virtual string BathCabinetNo { get; set; }

        /// <summary>
        /// 淋浴龍頭
        /// </summary>
        public virtual string Shower { get; set; }
        public virtual string ShowerNo { get; set; }

        /// <summary>
        /// 蓮蓬頭
        /// </summary>
        public virtual string ShowerHead { get; set; }
        public virtual string ShowerHeadNo { get; set; }

        /// <summary>
        /// 浴缸
        /// </summary>
        public virtual string Tub { get; set; }
        public virtual string TubNo { get; set; }

        /// <summary>
        /// 淋浴拉門
        /// </summary>
        public virtual string SlidingDoor { get; set; }
        public virtual string SlidingDoorNo { get; set; }

        /// <summary>
        /// 毛巾架
        /// </summary>
        public virtual string TowelRack { get; set; }
        public virtual string TowelRackNo { get; set; }

        /// <summary>
        /// 流理臺
        /// </summary>
        public virtual string FlowTable { get; set; }
        public virtual string FlowTableNo { get; set; }

        /// <summary>
        /// 水龍頭
        /// </summary>
        public virtual string KFaucet { get; set; }
        public virtual string KFaucetNo { get; set; }

        /// <summary>
        /// 電器櫃
        /// </summary>
        public virtual string ElectCabinet { get; set; }
        public virtual string ElectCabinetNo { get; set; }

        /// <summary>
        /// 廚櫃
        /// </summary>
        public virtual string KitchenCabinet { get; set; }
        public virtual string KitchenCabinetNo { get; set; }

        /// <summary>
        /// 電磁爐
        /// </summary>
        public virtual string InductionCooker { get; set; }
        public virtual string InductionCookerNo { get; set; }

        /// <summary>
        /// 微波爐
        /// </summary>
        public virtual string MicrowaveOven { get; set; }
        public virtual string MicrowaveOvenNo { get; set; }

        /// <summary>
        /// 烤箱
        /// </summary>
        public virtual string MicroOven { get; set; }
        public virtual string MicroOvenNo { get; set; }

        /// <summary>
        /// 電鍋
        /// </summary>
        public virtual string ElectricPot { get; set; }
        public virtual string ElectricPotNo { get; set; }

        /// <summary>
        /// 烘碗機
        /// </summary>
        public virtual string DishDryer { get; set; }
        public virtual string DishDryerNo { get; set; }

        /// <summary>
        /// 洗碗機
        /// </summary>
        public virtual string Dishwasher { get; set; }
        public virtual string DishwasherNo { get; set; }

        /// <summary>
        /// 淨水器
        /// </summary>
        public virtual string WaterPurifier { get; set; }
        public virtual string WaterPurifierNo { get; set; }

        /// <summary>
        /// 單人沙發
        /// </summary>
        public virtual string SingleSofa { get; set; }
        public virtual string SingleSofaNo { get; set; }

        /// <summary>
        /// 雙人沙發
        /// </summary>
        public virtual string DoubleSofa { get; set; }
        public virtual string DoubleSofaNo { get; set; }

        /// <summary>
        /// 三人沙發
        /// </summary>
        public virtual string Couch { get; set; }
        public virtual string CouchNo { get; set; }

        /// <summary>
        /// 茶几
        /// </summary>
        public virtual string LowStool { get; set; }
        public virtual string LowStoolNo { get; set; }

        /// <summary>
        /// 矮凳
        /// </summary>
        public virtual string CoffeeTable { get; set; }
        public virtual string CoffeeTableNo { get; set; }

        /// <summary>
        /// 鞋櫃
        /// </summary>
        public virtual string ShoeBox { get; set; }
        public virtual string ShoeBoxNo { get; set; }

        /// <summary>
        /// 電視櫃
        /// </summary>
        public virtual string TVCabinet { get; set; }
        public virtual string TVCabinetNo { get; set; }

        /// <summary>
        /// 餐桌
        /// </summary>
        public virtual string DiningTable { get; set; }
        public virtual string DiningTableNo { get; set; }

        /// <summary>
        /// 餐椅
        /// </summary>
        public virtual string DiningChair { get; set; }
        public virtual string DiningChairNo { get; set; }

        /// <summary>
        /// 室外大門
        /// </summary>
        public virtual string OutdoorGate { get; set; }
        public virtual string OutdoorGateNo { get; set; }

        /// <summary>
        /// 室內門
        /// </summary>
        public virtual string InteriorDoor { get; set; }
        public virtual string InteriorDoorNo { get; set; }

        /// <summary>
        /// 保全設施
        /// </summary>
        public virtual string SecurityFacilities { get; set; }
        public virtual string SecurityFacilitiesNo { get; set; }

        /// <summary>
        /// 室內照明(顆)
        /// </summary>
        public virtual string IndoorLighting { get; set; }
        public virtual string IndoorLightingNo { get; set; }

        /// <summary>
        /// 電風扇
        /// </summary>
        public virtual string ElectricFan { get; set; }
        public virtual string ElectricFanNo { get; set; }

        /// <summary>
        /// 窗簾
        /// </summary>
        public virtual string Curtain { get; set; }
        public virtual string CurtainNo { get; set; }

        /// <summary>
        /// 衣櫃
        /// </summary>
        public virtual string Wardrobe { get; set; }
        public virtual string WardrobeNo { get; set; }

        /// <summary>
        /// 置物櫃
        /// </summary>
        public virtual string Locker { get; set; }
        public virtual string LockerNo { get; set; }

        /// <summary>
        /// 床頭櫃
        /// </summary>
        public virtual string BedsideTable { get; set; }
        public virtual string BedsideTableNo { get; set; }

        /// <summary>
        /// 梳妝台
        /// </summary>
        public virtual string Dresser { get; set; }
        public virtual string DresserNo { get; set; }

        /// <summary>
        /// 書桌
        /// </summary>
        public virtual string Desk { get; set; }
        public virtual string DeskNo { get; set; }

        /// <summary>
        /// 椅子
        /// </summary>
        public virtual string Chair { get; set; }
        public virtual string ChairNo { get; set; }

        /// <summary>
        /// 單人床(組)
        /// </summary>
        public virtual string SingleBed { get; set; }
        public virtual string SingleBedNo { get; set; }

        /// <summary>
        /// 雙人床(組)
        /// </summary>
        public virtual string DoubleBed { get; set; }
        public virtual string DoubleBedNo { get; set; }

        /// <summary>
        /// 滅火器
        /// </summary>
        public virtual string BFireEX { get; set; }
        public virtual string BFireEXNo { get; set; }

        /// <summary>
        /// 偵煙警報器
        /// </summary>
        public virtual string BSmokeDE { get; set; }
        public virtual string BSmokeDENo { get; set; }

        /// <summary>
        /// 瓦斯警報器
        /// </summary>
        public virtual string GasAlarm { get; set; }
        public virtual string GasAlarmNo { get; set; }

        /// <summary>
        /// 緊急照明燈
        /// </summary>
        public virtual string EmergencyLights { get; set; }
        public virtual string EmergencyLightsNo { get; set; }

        /// <summary>
        /// 大樓鑰匙
        /// </summary>
        public virtual string BuildingKey { get; set; }
        public virtual string BuildingKeyNo { get; set; }

        /// <summary>
        /// 是外大門鑰匙
        /// </summary>
        public virtual string OutdoorGateKey { get; set; }
        public virtual string OutdoorGateKeyNo { get; set; }

        /// <summary>
        /// 室內門鑰匙
        /// </summary>
        public virtual string InteriorDoorKey { get; set; }
        public virtual string InteriorDoorKeyNo { get; set; }

        /// <summary>
        /// 信箱鑰匙
        /// </summary>
        public virtual string MailboxKey { get; set; }
        public virtual string MailboxKeyNo { get; set; }

        /// <summary>
        /// 磁扣卡
        /// </summary>
        public virtual string MagneticCard { get; set; }
        public virtual string MagneticCardNo { get; set; }

        /// <summary>
        /// 電子門鎖
        /// </summary>
        public virtual string ElectricDoorlock { get; set; }
        public virtual string ElectricDoorlockNo { get; set; }

        /// <summary>
        /// 電視遙控器
        /// </summary>
        public virtual string TVRemote { get; set; }
        public virtual string TVRemoteNo { get; set; }

        /// <summary>
        /// 冷氣遙控器
        /// </summary>
        public virtual string AirConditionerRemote { get; set; }
        public virtual string AirConditionerRemoteNo { get; set; }

        /// <summary>
        /// 網路分享器
        /// </summary>
        public virtual string IPSharingRouter { get; set; }
        public virtual string IPSharingRouterNo { get; set; }

        /// <summary>
        /// 數位盒
        /// </summary>
        public virtual string DigitalBox { get; set; }
        public virtual string DigitalBoxNo { get; set; }

        /// <summary>
        /// 其餘未記載項目
        /// </summary>
        public virtual string OtherDevices { get; set; }

        #endregion 建物設備相關
    }
}