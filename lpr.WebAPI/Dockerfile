FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["lpr.WebAPI/lpr.WebAPI.csproj", "lpr.WebAPI/"]
RUN dotnet restore "lpr.WebAPI/lpr.WebAPI.csproj"
COPY . .
WORKDIR "/src/lpr.WebAPI"
RUN dotnet build "lpr.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "lpr.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "lpr.WebAPI.dll"]
