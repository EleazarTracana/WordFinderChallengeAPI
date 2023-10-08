FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:3000
ENV ASPNETCORE_ENVIRONMENT=Development
EXPOSE 3000

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["WordFinderAPI/WordFinderAPI.csproj", "WordFinderAPI/"]
RUN dotnet restore "WordFinderAPI/WordFinderAPI.csproj"
COPY . .
WORKDIR "/src/WordFinderAPI"
RUN dotnet build "WordFinderAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WordFinderAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WordFinderAPI.dll"]
