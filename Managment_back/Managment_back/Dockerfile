# Base image for running the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Build image with SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Managment_back/Managment_back/Managment_back.csproj", "Managment_back/Managment_back/"]
RUN dotnet restore "Managment_back/Managment_back/Managment_back.csproj"

COPY . .
WORKDIR "/src/Managment_back/Managment_back"
RUN dotnet build "Managment_back.csproj" -c Release -o /app/build

# Publish step
FROM build AS publish
RUN dotnet publish "Managment_back.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Managment_back.dll"]
