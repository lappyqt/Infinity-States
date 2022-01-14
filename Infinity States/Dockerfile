#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Infinity States/Infinity States.csproj", "Infinity States/"]
RUN dotnet restore "Infinity States/Infinity States.csproj"
COPY . .
WORKDIR "/src/Infinity States"
RUN dotnet build "Infinity States.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Infinity States.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Infinity States.dll"]