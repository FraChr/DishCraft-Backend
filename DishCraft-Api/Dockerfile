FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /src

COPY ["DishCraft-Api.csproj", "."]
#COPY ["DishCraft-Api/DishCraft-Api.csproj", "DishCraft-Api/"]

#RUN dotnet restore "DishCraft-Api/DishCraft-Api.csproj"
RUN dotnet restore "DishCraft-Api.csproj"

COPY . .

#WORKDIR "/src/DishCraft-Api"
#RUN dotnet build "./DishCraft-Api.csproj" -c $BUILD_CONFIGURATION -o /app/build
RUN dotnet build "DishCraft-Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release

#RUN dotnet publish "./DishCraft-Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
RUN dotnet publish "DishCraft-Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DishCraft-Api.dll"]
