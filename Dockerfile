#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0-preview AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0-preview AS build

WORKDIR /src
COPY ["Almanime.csproj", "."]
RUN dotnet restore "./Almanime.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Almanime.csproj" -c Release -o "/app/build"

FROM build AS publish
RUN dotnet publish "Almanime.csproj" -c Release -o "/app/publish"

FROM base AS final
ARG RELEASE
ENV SENTRY_RELEASE=$RELEASE
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Almanime.dll"]
