FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MagniseTask.Application/MagniseTask.Application.csproj", "MagniseTask.Application/"]
COPY ["MagniseTask.Infrastructure/MagniseTask.Infrastructure.csproj", "MagniseTask.Infrastructure/"]
COPY ["MagniseTask.Domain/MagniseTask.Domain.csproj", "MagniseTask.Domain/"]
COPY ["MagniseTask.Web/MagniseTask.Web.csproj", "./MagniseTask.Web/"]
RUN dotnet restore "MagniseTask.Web/MagniseTask.Web.csproj"
COPY . .

WORKDIR "/src/MagniseTask.Web"
RUN dotnet build "MagniseTask.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "MagniseTask.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MagniseTask.Web.dll"]
