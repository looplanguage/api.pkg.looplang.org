FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

WORKDIR /app

COPY . ./

RUN dotnet publish ./api.pkg.looplang.org.sln -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:3.1

WORKDIR /app

COPY --from=build-env /app/out .

ENTRYPOINT ["dotnet", "aspnetapp.dll"]