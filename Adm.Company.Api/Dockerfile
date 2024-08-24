#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Adm.Company.Api/Adm.Company.Api.csproj", "Adm.Company.Api/"]
COPY ["Adm.Company.IoC/Adm.Company.IoC.csproj", "Adm.Company.IoC/"]
COPY ["Adm.Company.Application/Adm.Company.Application.csproj", "Adm.Company.Application/"]
COPY ["Adm.Company.Infrastructure/Adm.Company.Infrastructure.csproj", "Adm.Company.Infrastructure/"]
COPY ["Adm.Company.Domain/Adm.Company.Domain.csproj", "Adm.Company.Domain/"]
RUN dotnet restore "./Adm.Company.Api/Adm.Company.Api.csproj"
COPY . .
WORKDIR "/src/Adm.Company.Api"
RUN dotnet build "./Adm.Company.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Adm.Company.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Adm.Company.Api.dll"]