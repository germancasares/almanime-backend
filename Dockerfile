#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0-preview-alpine AS build

ARG TARGETARCH
WORKDIR /src
COPY ["Almanime.csproj", "."]
RUN dotnet restore "./Almanime.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Almanime.csproj" -a $TARGETARCH -c Release -o "/app/build"

FROM build AS publish
RUN dotnet publish "Almanime.csproj" -a $TARGETARCH -c Release -o "/app/publish"

FROM base AS final
ARG RELEASE
ENV SENTRY_RELEASE=$RELEASE
ENV ASPNETCORE_URLS=http://+:8080
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Almanime.dll"]
