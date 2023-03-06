using IdentityModel;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Yuebon.AspNetCore.Common;
using Yuebon.AspNetCore.Models;
using Yuebon.Commons.Core.App;
using Yuebon.Commons.Extensions;
using Yuebon.Commons.IoC;
using Yuebon.Commons.Json;
using Yuebon.Commons.Log;
using Yuebon.Commons.Models;
using Yuebon.Commons.Options;
using Yuebon.Security.IServices;
using Yuebon.Security.Models;

namespace Yuebon.AspNetCore.Mvc
{
    /// <summary>
    /// Token令牌提供類
    /// </summary>
    public class TokenProvider
    {
        JwtOption _jwtModel=App.GetService<JwtOption>();
        IRoleService _roleService = App.GetService<IRoleService>();
        /// <summary>
        /// 構造函數
        /// </summary>
        public TokenProvider() { }
        /// <summary>
        /// 構造函數，初花jwtmodel
        /// </summary>
        /// <param name="jwtModel"></param>
        public TokenProvider(JwtOption jwtModel)
        {
            _jwtModel = jwtModel;
        }
        /// <summary>
        /// 直接通過appid和加密字符串獲取訪問令牌接口
        /// </summary>
        /// <param name="granttype">獲取access_token填寫client_credential</param>
        /// <param name="appid">用戶唯一憑證AppId</param>
        /// <param name="secret">用戶唯一憑證密鑰，即appsecret</param>
        /// <returns></returns>
        public TokenResult GenerateToken(string granttype, string appid, string secret)
        {
            var keyByteArray = Encoding.UTF8.GetBytes(secret);
            var signingKey = new SymmetricSecurityKey(keyByteArray);
            var expires = DateTime.UtcNow.Add(TimeSpan.FromMinutes(_jwtModel.Expiration));
            var signingCredentials=new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var tokenDescripor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(JwtClaimTypes.Audience,appid),
                    new Claim(JwtClaimTypes.Issuer,_jwtModel.Issuer),
                    new Claim(JwtClaimTypes.Subject, GrantType.ClientCredentials)
                }, granttype),
                Expires = expires,
                //對稱秘鑰SymmetricSecurityKey
                //簽名證書(秘鑰，加密算法)SecurityAlgorithms
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyByteArray), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescripor);
            var tokenString = tokenHandler.WriteToken(token);
            TokenResult result = new TokenResult();
            result.AccessToken = tokenString;
            result.ExpiresIn = (int)TimeSpan.FromMinutes(_jwtModel.Expiration).TotalMinutes;
            return  result;
        }
        /// <summary>
        /// 檢查用戶的Token有效性
        /// </summary>
        /// <param name="token">token令牌</param>
        /// <returns></returns>
        public CommonResult ValidateToken(string token)
        {
            //返回的結果對象
            CommonResult result = new CommonResult();
            if (!string.IsNullOrEmpty(token))
            {
                try
                {
                    JwtSecurityToken jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
                    if (jwtToken!=null)
                    {
                        #region 檢查令牌對象內容
                        DateTime now = DateTime.UtcNow;
                        DateTime refreshTime = jwtToken.ValidFrom;
                        refreshTime= refreshTime.Add(TimeSpan.FromMinutes(_jwtModel.refreshJwtTime));
                        if (now > refreshTime && jwtToken.Issuer== _jwtModel.Issuer)
                        {
                            result.ErrMsg = ErrCode.err40005;
                            result.ErrCode = "40005";
                        }
                        else
                        {
                            if (jwtToken.Subject == GrantType.Password)
                            {
                                var claimlist = jwtToken?.Payload.Claims as List<Claim>;
                                result.ResData= claimlist;
                            }
                            result.ErrMsg = ErrCode.err0;
                            result.ErrCode = ErrCode.successCode;
                            
                        }
                        #endregion
                    }
                    else
                    {
                        result.ErrMsg = ErrCode.err40004;
                        result.ErrCode = "40004";
                    }
                }
                catch (Exception ex)
                {
                    Log4NetHelper.Error("驗證token異常", ex);
                    throw new MyApiException(ErrCode.err40004, "40004");
                }
            }
            else
            {
                result.ErrMsg = ErrCode.err40004;
                result.ErrCode = "40004";
            }
            return result;
        }

        /// <summary>
        /// 根據用戶獲取token
        /// </summary>
        /// <param name="userInfo">用戶信息</param>
        /// <param name="appid">應用Id</param>
        /// <returns></returns>
        public TokenResult LoginToken(User userInfo,string appid)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtModel.Secret);
            var authTime = DateTime.UtcNow;//授權時間
            var expires = authTime.Add(TimeSpan.FromMinutes(_jwtModel.Expiration));//過期時間
            var tokenDescripor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(JwtClaimTypes.Audience,appid),
                    new Claim(JwtClaimTypes.Issuer,_jwtModel.Issuer),
                    new Claim(JwtClaimTypes.Name, userInfo.Account),
                    new Claim(JwtClaimTypes.Id, userInfo.Id),
                    new Claim(JwtClaimTypes.Role, _roleService.GetRoleEnCode(userInfo.RoleId)),
                    new Claim(JwtClaimTypes.Subject, GrantType.Password)
                }),
                Expires = expires,
                //對稱秘鑰SymmetricSecurityKey
                //簽名證書(秘鑰，加密算法)SecurityAlgorithms
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescripor);
            var tokenString = tokenHandler.WriteToken(token);
            TokenResult result = new TokenResult();
            result.AccessToken = tokenString;
            result.ExpiresIn = (int)TimeSpan.FromMinutes(_jwtModel.Expiration).TotalMinutes;
            return result;
        }


        /// <summary>
        /// 根據登錄用戶獲取token
        /// </summary>
        /// <param name="userInfo">用戶信息</param>
        /// <param name="appid">應用Id</param>
        /// <returns></returns>
        public TokenResult GetUserToken(User userInfo, string appid)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtModel.Secret);
            var authTime = DateTime.UtcNow;//授權時間
            var expires = authTime.Add(TimeSpan.FromMinutes(_jwtModel.Expiration));//過期時間
            var tokenDescripor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(JwtClaimTypes.Audience,appid),
                    new Claim(JwtClaimTypes.Issuer,_jwtModel.Issuer),
                    new Claim(JwtClaimTypes.Name, userInfo.Account),
                    new Claim(JwtClaimTypes.Id, userInfo.Id),
                    new Claim(JwtClaimTypes.Role, userInfo.RoleId),
                    new Claim(JwtClaimTypes.Subject, GrantType.Password)
                }),
                Expires = expires,
                //對稱秘鑰SymmetricSecurityKey
                //簽名證書(秘鑰，加密算法)SecurityAlgorithms
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescripor);
            var tokenString = tokenHandler.WriteToken(token);
            TokenResult result = new TokenResult();
            result.AccessToken = tokenString;
            result.ExpiresIn = (int)TimeSpan.FromMinutes(_jwtModel.Expiration).TotalMinutes;
            return result;
        }
    }
}
