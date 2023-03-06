#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Yuebon.WebApi/Yuebon.WebApi.csproj", "Yuebon.WebApi/"]
COPY ["Yuebon.Quartz.Jobs/Yuebon.Quartz.Jobs.csproj", "Yuebon.Quartz.Jobs/"]
COPY ["Yuebon.NetCore/Yuebon.Chaochi.Core/Yuebon.Chaochi.Core.csproj", "Yuebon.NetCore/Yuebon.Chaochi.Core/"]
COPY ["Yuebon.NetCore/Yuebon.Commons/Yuebon.Commons.csproj", "Yuebon.NetCore/Yuebon.Commons/"]
COPY ["Yuebon.NetCore/Yuebon.Security.Core/Yuebon.Security.Core.csproj", "Yuebon.NetCore/Yuebon.Security.Core/"]
COPY ["Yuebon.NetCore/Yuebon.AspNetCore/Yuebon.AspNetCore.csproj", "Yuebon.NetCore/Yuebon.AspNetCore/"]
COPY ["Yuebon.Email.Core/Yuebon.Email.Core.csproj", "Yuebon.Email.Core/"]
COPY ["Yuebon.Messages/Yuebon.Messages.Core/Yuebon.Messages.Core.csproj", "Yuebon.Messages/Yuebon.Messages.Core/"]
COPY ["Yuebon.SMS.Core/Yuebon.SMS.Core.csproj", "Yuebon.SMS.Core/"]
COPY ["Yuebon.WeChat.Core/Yuebon.WeChat.Core.csproj", "Yuebon.WeChat.Core/"]
COPY ["Yuebon.CMS.Core/Yuebon.CMS.Core.csproj", "Yuebon.CMS.Core/"]
COPY ["Yuebon.Tenants.Core/Yuebon.Tenants.Core.csproj", "Yuebon.Tenants.Core/"]
RUN dotnet restore "Yuebon.WebApi/Yuebon.WebApi.csproj"
COPY . .
#找不到LICENSE時的臨時做法(不一定是正確作法)
COPY ["Yuebon.NetCore/Yuebon.Commons/LICENSE", "LICENSE"] 
WORKDIR "/src/Yuebon.WebApi"

RUN dotnet build "Yuebon.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Yuebon.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Yuebon.WebApi.dll"]