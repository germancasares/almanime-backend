#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0-bullseye-slim-arm64v8 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0-bullseye-slim-amd64 AS build

ARG TARGETARCH
ARG TARGETOS

RUN arch=$TARGETARCH \
    && if [ "$arch" = "amd64" ]; then arch="x64"; fi \
    && echo $TARGETOS-$arch > /tmp/rid

WORKDIR /src
COPY ["Almanime.csproj", "."]
RUN dotnet restore "./Almanime.csproj" -r $(cat /tmp/rid)
COPY . .
WORKDIR "/src/."
RUN dotnet build "Almanime.csproj" -c Release -o /app/build -r $(cat /tmp/rid) --self-contained false --no-restore

FROM build AS publish
RUN dotnet publish "Almanime.csproj" -c Release -o /app/publish -r $(cat /tmp/rid) --self-contained false --no-restore

FROM base AS final
ARG RELEASE
ENV SENTRY_RELEASE=$RELEASE
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Almanime.dll"]