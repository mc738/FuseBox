FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FuseBox.App/FuseBox.App.fsproj", "FuseBox.App/"]
RUN dotnet restore "FuseBox.App/FuseBox.App.fsproj"
COPY . .
WORKDIR "/src/FuseBox.App"
RUN dotnet build "FuseBox.App.fsproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FuseBox.App.fsproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FuseBox.App.dll"]
