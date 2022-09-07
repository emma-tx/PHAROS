#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MigrationsExample.EF/MigrationsExample.EF.csproj", "MigrationsExample.EF/"]
RUN dotnet restore "MigrationsExample.EF/MigrationsExample.EF.csproj"
COPY . .
WORKDIR "/src/MigrationsExample.EF"
RUN dotnet build "MigrationsExample.EF.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MigrationsExample.EF.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MigrationsExample.EF.dll"]