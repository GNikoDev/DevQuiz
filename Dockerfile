﻿FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["./src/libraries/Libraries.Core/Libraries.Core.csproj", "./libraries/Libraries.Core/"]
COPY ["./src/libraries/Libraries.Data/Libraries.Data.csproj", "./libraries/Libraries.Data/"]
COPY ["./src/libraries/Libraries.Services/Libraries.Services.csproj", "./libraries/Libraries.Services/"]
COPY ["./src/TelegramBot/TelegramBot.csproj", "./TelegramBot/"]
RUN dotnet restore "./TelegramBot/TelegramBot.csproj"
COPY . .
WORKDIR "/src/TelegramBot/."
EXEC "ls /src/TelegramBot/"
RUN dotnet build "TelegramBot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TelegramBot.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DevQuiz.TelegramBot.dll"]
