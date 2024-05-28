#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
RUN dotnet tool install --global dotnet-ef --version 7.0.18
COPY ["FilmoSearch.Api/FilmoSearch.Api.csproj", "FilmoSearch.Api/"]
RUN dotnet restore "FilmoSearch.Api/FilmoSearch.Api.csproj"
COPY . .
WORKDIR "/src/FilmoSearch.Api"
RUN dotnet build "FilmoSearch.Api.csproj" -c Release -o /app/build



FROM build AS final
WORKDIR /app
COPY --from=build  /app/build  .
ENV PATH="/root/.dotnet/tools:${PATH}"
ENTRYPOINT ["dotnet", "FilmoSearch.Api.dll"]
