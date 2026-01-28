FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

RUN apt update -y && apt upgrade -y

WORKDIR /build/src/

COPY . .

RUN dotnet restore
RUN dotnet publish -c release -o /build/out


FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime

RUN apt-get update && apt-get upgrade -y && apt-get clean && rm -rf /var/lib/apt/lists/*

WORKDIR /app

COPY --from=build /build/out ./

# Run as a user other than root
RUN groupadd -g 999 appuser && useradd -r -u 999 -g appuser -m appuser
RUN chown appuser:appuser -R /app
USER 999

EXPOSE 8080
ENV APSNETCORE_URLS=http://*:8080

ENTRYPOINT ["dotnet", "SonOfRadArrNotifications.dll"]