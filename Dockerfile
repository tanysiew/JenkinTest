#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base

WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY ["./nuget.config","."]

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["./WebApplication8/WebApplication8.csproj", "WebApplication8/"]
COPY ["./nuget.config","."]
RUN dotnet restore "WebApplication8/WebApplication8.csproj"
WORKDIR "/src/WebApplication8"
COPY ./WebApplication8 .
RUN dotnet build "WebApplication8.csproj" -c Release -o /app/build

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS exe
WORKDIR /src
COPY ["./nuget.config","."]
COPY ["./ConsoleApp1/ConsoleApp1.csproj", "ConsoleApp1/"]
RUN dotnet restore "ConsoleApp1/ConsoleApp1.csproj"
WORKDIR "/src/ConsoleApp1"
COPY ./ConsoleApp1 .
RUN dotnet build "ConsoleApp1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApplication8.csproj" -c Release -o /app/publish

FROM exe AS publish2
RUN dotnet publish "ConsoleApp1.csproj" -c Release -o /app/publish2

FROM base AS final

WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=publish2 ./app/publish2 .

RUN apt-get update && apt-get upgrade && apt-get install -y wget
RUN wget https://packages.microsoft.com/config/debian/10/packages-microsoft-prod.deb -O packages-microsoft-prod.deb && \
    dpkg -i packages-microsoft-prod.deb && \
    apt-get update && \
    apt-get install -y apt-transport-https && \
    apt-get update && \
    apt-get install -y aspnetcore-runtime-3.1
#COPY --from=exe ./console .    

ENTRYPOINT ["dotnet", "WebApplication8.dll"]