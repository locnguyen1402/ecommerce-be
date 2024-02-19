#!/usr/bin/env bash

rm ecommerce-platform.sln

dotnet new sln --name ecommerce-platform
dotnet sln ecommerce-platform.sln add src/services/**/**/*.csproj