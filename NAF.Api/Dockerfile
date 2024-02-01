FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["NAF.Api/NAF.Api.csproj", "NAF.Api/"]
COPY ["NAF.Application/NAF.Application.csproj", "NAF.Application/"]
COPY ["NAF.Domain/NAF.Domain.csproj", "NAF.Domain/"]
COPY ["NAF.Infra.CrossCutting/NAF.Infra.CrossCutting.csproj", "NAF.Infra.CrossCutting/"]
COPY ["Services/NAF.Domain.Services.csproj", "Services/"]
COPY ["NAF.Infra.Data/NAF.Infra.Data.csproj", "NAF.Infra.Data/"]
RUN dotnet restore "./NAF.Api/./NAF.Api.csproj"
COPY . .
WORKDIR "/src/NAF.Api"
RUN dotnet build "./NAF.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./NAF.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENV TZ=America/Sao_Paulo

ENTRYPOINT ["dotnet", "NAF.Api.dll"]