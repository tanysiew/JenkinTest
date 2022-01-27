#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
#Runtime
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# FROM mcr.microsoft.com/dotnet/runtime:5.0 AS exe
# COPY ["./ConsoleApp1/exe", "./console"]

#Build
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ConsoleApp1.csproj", "ConsoleApp1/"]
RUN dotnet restore "./ConsoleApp1/ConsoleApp1.csproj"
WORKDIR "/src/ConsoleApp1"
COPY . .
RUN dotnet build "ConsoleApp1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ConsoleApp1.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# COPY --from=exe ./console .
ENTRYPOINT ["dotnet", "ConsoleApp1.dll"]