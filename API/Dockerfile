﻿FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Timetable/Timetable.csproj", "Timetable/"]
RUN dotnet restore "Timetable/Timetable.csproj"
COPY . .
WORKDIR "/src/Timetable"
RUN dotnet build "Timetable.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Timetable.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Timetable.dll"]
