# ??????????? ???????? ??????
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# ?????? ???????
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["ExchangeRatesWebapi/ExchangeRatesWebapi.csproj", "ExchangeRatesWebapi/"]
COPY ["Exchange.Service/Exchange.Service.csproj", "Exchange.Service/"]
COPY ["Exchange.Repo/Exchange.Repo.csproj", "Exchange.Repo/"]
RUN dotnet restore "ExchangeRatesWebapi/ExchangeRatesWebapi.csproj"

COPY . .
WORKDIR "/src/ExchangeRatesWebapi"
RUN dotnet build "ExchangeRatesWebapi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# ?????????? ???????
FROM build AS publish
RUN dotnet publish "ExchangeRatesWebapi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# ????????????? ?????? ?????? ??? ??????????
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExchangeRatesWebapi.dll"]
