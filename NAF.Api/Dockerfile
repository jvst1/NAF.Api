#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NAF.Api/NAF.Api.csproj", "NAF.Api/"]
RUN dotnet restore "NAF.Api/NAF.Api.csproj"
COPY . .
WORKDIR "/src/NAF.Api"
RUN dotnet build "NAF.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NAF.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NAF.Api.dll"]