@echo off

set MIGRATION_NAME=%1

dotnet ef migrations add %MIGRATION_NAME% ^
--project .\Infrastructure\Infrastructure.csproj ^
--startup-project .\DishCraft-Api\DishCraft-Api.csproj

echo Migration "%MIGRATION_NAME%" created.