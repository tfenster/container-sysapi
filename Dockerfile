# escape=`
ARG BASE
FROM mcr.microsoft.com/dotnet/sdk:6.0-nanoserver-${BASE} AS build

WORKDIR /src

COPY ["container-sysapi.csproj", "."]
RUN dotnet restore "container-sysapi.csproj"
COPY . ./

USER ContainerAdministrator
RUN dotnet build "container-sysapi.csproj" -c Release -o /app/build
RUN dotnet publish "container-sysapi.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0-nanoserver-${BASE} AS final
EXPOSE 80

WORKDIR /app
COPY --from=build /app/publish .

USER ContainerAdministrator
ENTRYPOINT ["dotnet", "container-sysapi.dll"]