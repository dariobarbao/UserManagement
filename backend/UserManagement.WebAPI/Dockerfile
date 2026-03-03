# =========================
# BUILD STAGE
# =========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ../UserManagement.sln ./
COPY . .

RUN dotnet restore
RUN dotnet publish UserManagement.WebAPI/UserManagement.WebAPI.csproj -c Release -o /app/publish

# =========================
# RUNTIME STAGE
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "UserManagement.WebAPI.dll"]