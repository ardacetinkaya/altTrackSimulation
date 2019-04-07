#FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443
#
#FROM microsoft/dotnet:2.2-sdk AS build
#WORKDIR /src
#COPY ["DataWeb/DataWeb.csproj", "DataWeb/"]
#RUN dotnet restore "DataWeb/DataWeb.csproj"
#COPY . .
#WORKDIR "/src/DataWeb"
#RUN dotnet build "DataWeb.csproj" -c Release -o /app
#
#FROM build AS publish
#RUN dotnet publish "DataWeb.csproj" -c Release -o /app
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app .
#ENTRYPOINT ["dotnet", "DataWeb.dll"]