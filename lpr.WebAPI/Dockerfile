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

RUN dotnet dev-certs https --clean
RUN dotnet dev-certs https -ep ./dev-cert.pfx -p ob6KdSAOuNB3oe7

RUN dotnet build "lpr.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "lpr.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=build /src/lpr.WebAPI/dev-cert.pfx .
ENTRYPOINT ["dotnet", "lpr.WebAPI.dll"]
