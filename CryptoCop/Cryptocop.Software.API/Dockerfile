FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
LABEL maintainer="Logi Páll Sævarsson"
WORKDIR /app

COPY . ./
RUN dotnet restore

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .
ENV ASPNETCORE_URLS=http://+:5000/

ENTRYPOINT ["dotnet", "Cryptocop.Software.API.dll"]