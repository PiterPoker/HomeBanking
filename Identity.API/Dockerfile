#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src

COPY ["Identity.API/Identity.API.csproj", "Identity.API/"]
COPY ["HomeBanking.API/HomeBanking.API.csproj", "HomeBanking.API/"]
COPY ["HomeBanking.Domain/HomeBanking.Domain.csproj", "HomeBanking.Domain/"]
COPY ["HomeBanking.Infrastructure/HomeBanking.Infrastructure.csproj", "HomeBanking.Infrastructure/"]

COPY "docker-compose.dcproj" "docker-compose.dcproj"

COPY "NuGet.config" "NuGet.config"

#RUN dotnet restore "Identity.API/Identity.API/Identity.API.csproj"
RUN dotnet restore "HomeBanking.API/HomeBanking.API.csproj"

COPY . .
WORKDIR "/src/Identity.API"
RUN dotnet build "Identity.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Identity.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Identity.API.dll"]