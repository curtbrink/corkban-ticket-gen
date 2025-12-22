FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Corkban.TicketGen/Corkban.TicketGen.csproj", "Corkban.TicketGen/"]
RUN dotnet restore "Corkban.TicketGen/Corkban.TicketGen.csproj"
COPY . .
WORKDIR "/src/Corkban.TicketGen"
RUN dotnet build "./Corkban.TicketGen.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Corkban.TicketGen.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Corkban.TicketGen.dll"]
