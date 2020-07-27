FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS builder

COPY src/Automat.Api src/Automat.Api
COPY src/Automat.Application src/Automat.Application
COPY src/Automat.Domain src/Automat.Domain
COPY src/Automat.Infrastructure /src/Automat.Infrastructure

WORKDIR /src/Automat.Api
RUN dotnet restore
RUN dotnet publish -c release
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
COPY --from=builder src/Automat.Api/bin/release/netcoreapp3.1/publish app
WORKDIR app
EXPOSE 80/tcp
ENTRYPOINT ["dotnet", "Automat.Api.dll"]