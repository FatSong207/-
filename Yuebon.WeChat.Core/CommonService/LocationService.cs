﻿using System.Collections.Generic;
using Senparc.Weixin.MP.Entities;
using Senparc.CO2NET.Helpers.BaiduMap;
using Senparc.CO2NET.Helpers.GoogleMap;
using Senparc.Weixin.MP.Helpers;
using Senparc.CO2NET.Helpers;
using Senparc.NeuChar.Entities;

namespace Yuebon.WeChat.CommonService
{
    /// <summary>
    /// 地理位置信息處理
    /// </summary>
    public class LocationService
    {
        public ResponseMessageNews GetResponseMessage(RequestMessageLocation requestMessage)
        {
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageNews>(requestMessage);

            #region 百度地圖

            {
                var markersList = new List<BaiduMarkers>();
                markersList.Add(new BaiduMarkers()
                {
                    Longitude = requestMessage.Location_X,
                    Latitude = requestMessage.Location_Y,
                    Color = "red",
                    Label = "S",
                    Size = BaiduMarkerSize.m
                });

                var mapUrl = BaiduMapHelper.GetBaiduStaticMap(requestMessage.Location_X, requestMessage.Location_Y, 1, 6, markersList);
                responseMessage.Articles.Add(new Article()
                {
                    Description = string.Format("【來自百度地圖】您剛才發送了地理位置信息。Location_X：{0}，Location_Y：{1}，Scale：{2}，標簽：{3}",
                               requestMessage.Location_X, requestMessage.Location_Y,
                               requestMessage.Scale, requestMessage.Label),
                    PicUrl = mapUrl,
                    Title = "定位地點周邊地圖",
                    Url = mapUrl
                });
            }

            #endregion

            #region GoogleMap

            {
                var markersList = new List<GoogleMapMarkers>();
                markersList.Add(new GoogleMapMarkers()
                {
                    X = requestMessage.Location_X,
                    Y = requestMessage.Location_Y,
                    Color = "red",
                    Label = "S",
                    Size = GoogleMapMarkerSize.Default,
                });
                var mapSize = "480x600";
                var mapUrl = GoogleMapHelper.GetGoogleStaticMap(19 /*requestMessage.Scale*//*微信和GoogleMap的Scale不一致，這里建議使用固定值*/,
                                                                                markersList, mapSize);
                responseMessage.Articles.Add(new Article()
                {
                    Description = string.Format("【來自GoogleMap】您剛才發送了地理位置信息。Location_X：{0}，Location_Y：{1}，Scale：{2}，標簽：{3}",
                                  requestMessage.Location_X, requestMessage.Location_Y,
                                  requestMessage.Scale, requestMessage.Label),
                    PicUrl = mapUrl,
                    Title = "定位地點周邊地圖",
                    Url = mapUrl
                });
            }

            #endregion


            responseMessage.Articles.Add(new Article()
            {
                Title = "微信公眾平臺SDK 官網鏈接",
                Description = "Senparc.Weixin.MK SDK地址",
                PicUrl = "https://sdk.weixin.senparc.com/images/logo.jpg",
                Url = "https://sdk.weixin.senparc.com"
            });

            return responseMessage;
        }
    }
}
