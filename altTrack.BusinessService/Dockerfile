FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["altTrack.BusinessService/altTrack.BusinessService.csproj", "altTrack.BusinessService/"]
RUN dotnet restore "altTrack.BusinessService/altTrack.BusinessService.csproj"
COPY . .
WORKDIR "/src/altTrack.BusinessService"
RUN dotnet build "altTrack.BusinessService.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "altTrack.BusinessService.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "altTrack.BusinessService.dll"]