FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "Eshop-Api.sln"
WORKDIR "/src/Shop/Shop.Api"
RUN dotnet build "Shop.Api.csproj" -c Release -o /app/build --no-restore

FROM build AS publish
RUN dotnet publish "Shop.Api.csproj" -c Release -o /app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shop.Api.dll"]