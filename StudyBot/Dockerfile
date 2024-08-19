FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /StudyBot

# Copy everything
COPY . ./
RUN dotnet nuget add source https://nuget.voids.site/v3/index.json
RUN dotnet add package Telegram.Bot

RUN dotnet restore

RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /StudyBot
COPY --from=build-env /StudyBot/out .
ENTRYPOINT ["dotnet", "StudyBot.dll"]