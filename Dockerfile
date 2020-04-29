#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["WhoPK.API/WhoPK.API.csproj", "WhoPK.API/"]
COPY ["WhoPK.DataAccess/WhoPK.DataAccess.csproj", "WhoPK.DataAccess/"]
COPY ["WhoPK.GameLogic/WhoPK.GameLogic.csproj", "WhoPK.GameLogic/"]
RUN dotnet restore "WhoPK.API/WhoPK.API.csproj"
COPY . .
WORKDIR "/src/WhoPK.API"
RUN dotnet build "WhoPK.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WhoPK.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WhoPK.API.dll"]