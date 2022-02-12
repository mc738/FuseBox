FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FuseBox.AzureDevOps.App/FuseBox.AzureDevOps.App.fsproj", "FuseBox.AzureDevOps.App/"]
RUN dotnet restore "FuseBox.AzureDevOps.App/FuseBox.AzureDevOps.App.fsproj"
COPY . .
WORKDIR "/src/FuseBox.AzureDevOps.App"
RUN dotnet build "FuseBox.AzureDevOps.App.fsproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FuseBox.AzureDevOps.App.fsproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FuseBox.AzureDevOps.App.dll"]
