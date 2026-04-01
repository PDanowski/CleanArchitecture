# syntax=docker/dockerfile:1

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY global.json ./
COPY Directory.Packages.props ./
COPY Notebook.sln ./
COPY src/Notebook.SharedKernel/Notebook.SharedKernel.csproj src/Notebook.SharedKernel/
COPY src/Notebook.Core/Notebook.Core.csproj src/Notebook.Core/
COPY src/Notebook.Infrastructure/Notebook.Infrastructure.csproj src/Notebook.Infrastructure/
COPY src/Notebook.Web/Notebook.Web.csproj src/Notebook.Web/

RUN dotnet restore src/Notebook.Web/Notebook.Web.csproj

COPY src/ ./src/
RUN dotnet publish src/Notebook.Web/Notebook.Web.csproj -c Release -o /app/publish --no-restore /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

RUN mkdir -p /data

ENV ASPNETCORE_URLS=http://+:8080
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV ConnectionStrings__SqliteConnection="Data Source=/data/database.sqlite"

EXPOSE 8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Notebook.Web.dll"]
