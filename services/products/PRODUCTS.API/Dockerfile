FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

WORKDIR /app

COPY services/"Directory.Packages.props" services/"Directory.Packages.props"
COPY services/products services/products
COPY services/shared services/shared

WORKDIR /app/services/products/PRODUCTS.API
RUN dotnet restore
RUN dotnet publish -c Release -o /published

# COPY . .

# RUN dotnet restore
# RUN dotnet publish -c Release -o published

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app/services/products/PRODUCTS.API
COPY --from=build-env /published .
ENV ASPNETCORE_URLS=http://+:5012
ENTRYPOINT ["dotnet", "PRODUCTS.API.dll"]