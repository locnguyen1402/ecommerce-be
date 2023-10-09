FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
# ENV ASPNETCORE_URLS=http://+:5012
# EXPOSE 5012

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app
COPY services/"Directory.Packages.props" services/"Directory.Packages.props"
COPY services/products services/products
COPY services/shared services/shared

WORKDIR /app/services/products/PRODUCTS.API
RUN dotnet restore
RUN dotnet build "PRODUCTS.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PRODUCTS.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PRODUCTS.API.dll"]


