FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV AltTrackBusinessService="https://localhost:44313"
ENV AltTrackPingService="https://localhost:44314"

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["altTrack.WebUI/altTrack.Web.UI.csproj", "altTrack.WebUI/"]
COPY ["altTrack/altTrack.csproj", "altTrack/"]
RUN dotnet restore "altTrack.WebUI/altTrack.Web.UI.csproj"
COPY . .
WORKDIR "/src/altTrack.WebUI"
RUN dotnet build "altTrack.Web.UI.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "altTrack.Web.UI.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "altTrack.Web.UI.dll"]