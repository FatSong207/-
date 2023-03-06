using AutoMapper;
using log4net;
using log4net.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Quartz;
using Quartz.Impl;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Yuebon.AspNetCore.Common;
using Yuebon.AspNetCore.Mvc;
using Yuebon.AspNetCore.Mvc.Filter;
using Yuebon.Commons.Cache;
using Yuebon.Commons.Core.App;
using Yuebon.Commons.DbContextCore;
using Yuebon.Commons.Extensions;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.IDbContext;
using Yuebon.Commons.Linq;
using Yuebon.Commons.Log;
using Yuebon.Commons.Module;
using Yuebon.Commons.Options;
using Yuebon.Quartz.Jobs;

namespace Yuebon.WebApi
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        public static ILoggerRepository LoggerRepository { get; set; }
        string targetPath = string.Empty;
        IMvcBuilder mvcBuilder;
        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }
        private IApiVersionDescriptionProvider apiVersionProvider;//api接口版本控制
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            //初始化log4net
            LoggerRepository = LogManager.CreateRepository("NETCoreRepository");
            Log4NetHelper.SetConfig(LoggerRepository, "log4net.config");
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public void ConfigureServices(IServiceCollection services)
        {
            //https://docs.microsoft.com/zh-tw/aspnet/core/performance/response-compression?view=aspnetcore-6.0
            services.AddResponseCompression();

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpContextAccessor();
            //services.AddSingleton(Configuration);
            //如果部署在linux系統上，需要加上下面的配置：
            services.Configure<KestrelServerOptions>(options => options.AllowSynchronousIO = true);
            //如果部署在IIS上，需要加上下面的配置：
            services.AddOptions();
            services.Configure<IISServerOptions>(options => options.AllowSynchronousIO = true);

            #region Swagger Api文檔

            // Api多版本版本配置
            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;//是否在請求頭中返回受支持的版本信息。
                o.ApiVersionReader = new HeaderApiVersionReader("api-version");////版本信息放到header ,不寫在不配置路由的情況下，版本信息放到response url 中
                o.AssumeDefaultVersionWhenUnspecified = true;//請求沒有指明版本的情況下是否使用默認的版本。
                o.DefaultApiVersion = new ApiVersion(1, 0);//默認的版本號。
            }).AddVersionedApiExplorer(option =>
            {    // 版本名的格式：v+版本號
                option.GroupNameFormat = "'v'V";
                option.AssumeDefaultVersionWhenUnspecified = true;
            });
            //獲取webapi版本信息，用于swagger多版本支持 
            apiVersionProvider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
            services.AddSwaggerGen(options =>
            {
                //https://iter01.com/577244.html
                options.OperationFilter<SwaggerFileUploadOperationFilter>();

                //https://stackoverflow.com/questions/46071513/swagger-error-conflicting-schemaids-duplicate-schemaids-detected-for-types-a-a
                options.SchemaGeneratorOptions = new SchemaGeneratorOptions { SchemaIdSelector = type => type.FullName };

                string contactName = Configuration.GetSection("SwaggerDoc:ContactName").Value;
                string contactNameEmail = Configuration.GetSection("SwaggerDoc:ContactEmail").Value;
                string contactUrl = Configuration.GetSection("SwaggerDoc:ContactUrl").Value;

                options.SwaggerDoc("chaochi",
                         new OpenApiInfo()
                         {
                             Title = $"兆基",
                             Version = "通用",
                             Description = Configuration.GetSection("SwaggerDoc:Description").Value,
                             Contact = new OpenApiContact { Name = contactName, Email = contactNameEmail, Url = new Uri(contactUrl) },
                             License = new OpenApiLicense { Name = contactName, Url = new Uri(contactUrl) }
                         }
                    );

                foreach (var description in apiVersionProvider.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(description.GroupName,
                         new OpenApiInfo()
                         {
                             Title = $"{Configuration.GetSection("SwaggerDoc:Title").Value}v{description.ApiVersion}",
                             Version = description.ApiVersion.ToString(),
                             Description = Configuration.GetSection("SwaggerDoc:Description").Value,
                             Contact = new OpenApiContact { Name = contactName, Email = contactNameEmail, Url = new Uri(contactUrl) },
                             License = new OpenApiLicense { Name = contactName, Url = new Uri(contactUrl) }
                         }
                    );
                }
                Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.xml").ToList().ForEach(file =>
                {
                    options.IncludeXmlComments(file, true);
                });
                options.DocumentFilter<HiddenApiFilter>(); // 在接口類、方法標記屬性 [HiddenApi]，可以阻止【Swagger文檔】生成
                //給api添加token令牌證書
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT授權(數據將在請求頭中進行傳輸) 直接在下框中輸入Bearer {token}（注意兩者之間是一個空格）\"",
                    Name = "Authorization",//jwt默認的參數名稱
                    In = ParameterLocation.Header,//jwt默認存放Authorization信息的位置(請求頭中)
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat="JWT",
                    Scheme= "Bearer"
                });
                //添加安全請求
                options.AddSecurityRequirement(
                    new OpenApiSecurityRequirement {
                        { 
                            new OpenApiSecurityScheme
                            {
                                Reference=new OpenApiReference{
                                    Type=ReferenceType.SecurityScheme,
                                    Id= "Bearer"
                                }
                            }
                            ,new string[] { }
                        }
                    }
                 );
                options.OperationFilter<AddRequiredHeaderParameter>();
                //開啟加權鎖
                options.OperationFilter<AddResponseHeadersFilter>();
                options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                options.OperationFilter<SecurityRequirementsOperationFilter>();

                //https://rimdev.io/swagger-grouping-with-controller-name-fallback-using-swashbuckle-aspnetcore/
                //https://www.cnblogs.com/toiv/p/9379249.html
                options.DocInclusionPredicate((docName, apiDesc) =>
                {
                    if ("chaochi" == docName)//上方 options.SwaggerDoc("chaochi",
                    {
                        //Console.WriteLine($"{docName}-{apiDesc}-{apiDesc.GroupName}");
                        if ("Chaochi"== apiDesc.GroupName)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }                        
                    }
                    else
                    {
                        return true;
                    }
                    
                });
            });


            #endregion

            #region 全局設置跨域訪問
            //允許所有跨域訪問，測試用
            services.AddCors(options => options.AddPolicy("yuebonCors",
                policy => policy.WithOrigins("*").AllowAnyHeader().AllowAnyMethod()));
            // 跨域設置 建議正式環境
            //services.AddCors(options => options.AddPolicy("yuebonCors",
            //    policy => policy.WithOrigins(Configuration.GetSection("AppSetting:AllowOrigins").Value.Split(',', StringSplitOptions.RemoveEmptyEntries)).AllowAnyHeader().AllowAnyMethod()));
            #endregion


            #region MiniProfiler
            services.AddMiniProfiler(options => {
                options.RouteBasePath = "/profiler";
            }).AddEntityFramework();
            #endregion

            #region 控制器
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.WriteIndented = true;
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                //設置時間格式
                options.JsonSerializerOptions.Converters.Add(new DateTimeJsonConverter());
                options.JsonSerializerOptions.Converters.Add(new DateTimeNullableConverter());
                //設置bool獲取格式
                options.JsonSerializerOptions.Converters.Add(new BooleanJsonConverter());
                //設置Decimal獲取格式
                options.JsonSerializerOptions.Converters.Add(new DecimalJsonConverter());
                //設置數字
                options.JsonSerializerOptions.Converters.Add(new IntJsonConverter());
                options.JsonSerializerOptions.PropertyNamingPolicy = new UpperFirstCaseNamingPolicy();
                options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
            });

            mvcBuilder = services.AddMvc(option =>
            {
                //option.Filters.Add<YuebonAuthorizationFilter>();
                option.Filters.Add(new ExceptionHandlingAttribute());
                option.Filters.Add(new LogFilterAttribute());
                //option.Filters.Add<ActionFilter>();
            }).SetCompatibilityVersion(CompatibilityVersion.Latest).AddRazorRuntimeCompilation();

            services.AddMvcCore()
                .AddAuthorization().AddApiExplorer();
            #endregion
            services.AddSignalR();//使用 SignalR
            InitIoC(services);
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (app != null)
            {
                //https://docs.microsoft.com/zh-tw/aspnet/core/performance/response-compression?view=aspnetcore-6.0
                app.UseResponseCompression();

                app.UseStaticHttpContextAccessor();
                IServiceProvider provider = app.ApplicationServices;
                AutoMapperService.UsePack(provider);
                //加載插件應用
                LoadMoudleApps(env);

                app.UseMiniProfiler();
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }
                else
                {
                    app.UseExceptionHandler("/Home/Error");
                    app.UseHsts();
                }

                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    //https://www.cnblogs.com/caijt/p/10739841.html
                    options.SwaggerEndpoint($"/swagger/chaochi/swagger.json", "兆基");
                    foreach (var description in apiVersionProvider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", $"{Configuration.GetSection("SwaggerDoc:Title").Value + description.GroupName.ToUpperInvariant()}"); 
                    }
                    //options.RoutePrefix = string.Empty;//這里主要是不需要再輸入swagger這個默認前綴
                    options.RoutePrefix = "docs";
                    //https://stackoverflow.com/questions/39116047/how-to-change-base-url-of-swagger-in-asp-net-core
                    //起始 https://localhost:5001/index.html   or  https://localhost:5001
                    //保留給 wwwroot 目錄下 index.html
                    //swagger 要變成
                    //https://localhost:5001/docs/index.html    <---- options.RoutePrefix = "docs";
                    // launchSettings.json  是用在 vs啟動 
                });
                app.Use((context, next) =>
                {
                    context.Request.EnableBuffering();
                    return next();
                });

                //https://blog.csdn.net/sD7O95O/article/details/89311755
                DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();
                defaultFilesOptions.DefaultFileNames.Clear();
                defaultFilesOptions.DefaultFileNames.Add("index.html");//起始 https://localhost:5001 跳轉  https://localhost:5001/index.html  
                app.UseDefaultFiles(defaultFilesOptions);


                app.UseStaticFiles();
                app.UseRouting();
                app.UseAuthentication();
                app.UseAuthorization();
                //跨域
                app.UseMiddleware<CorsMiddleware>();
                app.UseCors("yuebonCors");
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                    endpoints.MapControllerRoute("default", "api/{controller=Home}/{action=Index}/{id?}");
                });
                app.UseStatusCodePages();

                Console.Write("start~~~UseStatusCodePages"+ env);
            }
        }



        /// <summary>
        /// IoC初始化
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private void InitIoC(IServiceCollection services)
        {

            #region 緩存
            CacheProvider cacheProvider = new CacheProvider
            {
                IsUseRedis = Configuration.GetSection("CacheProvider:UseRedis").Value.ToBool(false),
                ConnectionString = Configuration.GetSection("CacheProvider:Redis_ConnectionString").Value,
                InstanceName = Configuration.GetSection("CacheProvider:Redis_InstanceName").Value
            };

            var options = new JsonSerializerOptions();
            options.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
            options.WriteIndented = true;
            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.AllowTrailingCommas = true;
            //設置時間格式
            options.Converters.Add(new DateTimeJsonConverter());
            options.Converters.Add(new DateTimeNullableConverter());
            //設置bool獲取格式
            options.Converters.Add(new BooleanJsonConverter());
            //設置數字
            options.Converters.Add(new IntJsonConverter());
            options.PropertyNamingPolicy = new UpperFirstCaseNamingPolicy();
            options.PropertyNameCaseInsensitive = true;                     //忽略大小寫
            //判斷是否使用Redis，如果不使用 Redis就默認使用 MemoryCache
            if (cacheProvider.IsUseRedis)
            {
                //Use Redis
                services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = cacheProvider.ConnectionString;
                    options.InstanceName = cacheProvider.InstanceName;
                });
                services.AddSingleton(typeof(ICacheService), new RedisCacheService(new RedisCacheOptions
                {
                    Configuration = cacheProvider.ConnectionString,
                    InstanceName = cacheProvider.InstanceName
                }, options, 0));
                services.Configure<DistributedCacheEntryOptions>(option => option.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5));//設置Redis緩存有效時間為5分鐘。
            }
            else
            {
                //Use MemoryCache
                services.AddSingleton<IMemoryCache>(factory =>
                {
                    var cache = new MemoryCache(new MemoryCacheOptions());
                    return cache;
                });
                services.AddSingleton<ICacheService, MemoryCacheService>();
                services.Configure<MemoryCacheEntryOptions>(
                    options => options.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)); //設置MemoryCache緩存有效時間為5分鐘
            }
            services.AddTransient<MemoryCacheService>();
            services.AddMemoryCache();// 啟用MemoryCache

            services.AddSingleton(cacheProvider);//註冊緩存配置
            #endregion

            #region 身份認證授權

            var jwtConfig = Configuration.GetSection("Jwt");
            var jwtOption = new JwtOption
            {
                Issuer = jwtConfig["Issuer"],
                Expiration = Convert.ToInt16(jwtConfig["Expiration"]),
                Secret = jwtConfig["Secret"],
                Audience = jwtConfig["Audience"],
                refreshJwtTime = Convert.ToInt16(jwtConfig["refreshJwtTime"])
            };
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; ;

            }).AddJwtBearer(jwtBearerOptions =>
            {
                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtOption.Secret)),//秘鑰
                    ValidateIssuer = true,
                    ValidIssuer = jwtOption.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtOption.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(5)
                };
            });
            services.AddSingleton(jwtOption);//註冊配置
            #endregion

            services.AddAutoScanInjection();//自動化注入倉儲和服務
            services.AddScoped<IDbContextCore, SqlServerDbContext>(); //注入EF上下文

            #region automapper
            List<Assembly> myAssembly =RuntimeHelper.GetAllYuebonAssemblies().ToList();
            services.AddAutoMapper(myAssembly);
            services.AddTransient<IMapper, Mapper>();
            #endregion

            //services.AddSingleton<LogAttribute>();// 添加日誌過濾器 

            #region 定時任務
            services.AddTransient<HttpResultfulJob>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            //設置定時啟動的任務
            //services.AddHostedService<QuartzService>();
            #endregion
            App.Services = services;
        }

        /// <summary>
        /// 加載模塊應用
        /// </summary>
        /// <param name="env"></param>
        private void LoadMoudleApps(IWebHostEnvironment env)
        {
            // 定位到插件應用目錄 Apps
            var apps = env.ContentRootFileProvider.GetFileInfo("Apps");
            if (!Directory.Exists(apps.PhysicalPath))
            {
                return;
            }

            // 把 Apps 下的動態庫拷貝一份來運行，
            // 使 Apps 中的動態庫不會在運行時被占用（以便重新編譯）
            var shadows = targetPath = PrepareShadowCopies();
            // 從 Shadow Copy 目錄加載 Assembly 并註冊到 Mvc 中
            LoadFromShadowCopies(shadows);

            string PrepareShadowCopies()
            {
                // 準備 Shadow Copy 的目標目錄
                var target = Path.Combine(env.ContentRootPath, "app_data", "apps-cache");
                Directory.CreateDirectory(target);

                // 找到插件目錄下 bin 目錄中的 .dll，拷貝
                Directory.EnumerateDirectories(apps.PhysicalPath)
                    .Select(path => Path.Combine(path, "bin"))
                    .Where(Directory.Exists)
                    .SelectMany(bin => Directory.EnumerateFiles(bin, "*.dll"))
                    .ForEach(dll => File.Copy(dll, Path.Combine(target, Path.GetFileName(dll)), true));

                return target;
            }

            void LoadFromShadowCopies(string targetPath)
            {
                foreach (string dll in Directory.GetFiles(targetPath, "*.dll"))
                {
                    try
                    {
                        //解決插件還引用其他主程序沒有引用的第三方dll問題System.IO.FileNotFoundException
                        AssemblyLoadContext.Default.LoadFromAssemblyPath(dll);
                    }
                    catch (Exception ex)
                    {
                        //非.net程序集類型的dll關聯load時會報錯，這里忽略就可以
                        Log4NetHelper.Error(ex.Message);
                    }
                }
                // 從 Shadow Copy 目錄加載 Assembly 并註冊到 Mvc 中
                var groups = Directory.EnumerateFiles(targetPath, "Yuebon.*App.Api.dll").Select(AssemblyLoadContext.Default.LoadFromAssemblyPath);

                // 直接加載到為 ApplicationPart
                groups.ForEach(mvcBuilder.AddApplicationPart);
            }
        }
    }
}