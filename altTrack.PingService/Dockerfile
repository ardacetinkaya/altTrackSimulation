FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["altTrack.PingService/altTrack.PingService.csproj", "altTrack.PingService/"]
RUN dotnet restore "altTrack.PingService/altTrack.PingService.csproj"
COPY . .
WORKDIR "/src/altTrack.PingService"
RUN dotnet build "altTrack.PingService.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "altTrack.PingService.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "altTrack.PingService.dll"]