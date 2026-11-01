FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY ClientesBlazor.csproj ./
RUN dotnet restore ClientesBlazor.csproj

COPY . ./
RUN dotnet publish ClientesBlazor.csproj -c Release -o /app/publish --no-restore /p:UseAppHost=false
RUN assets_dir="$(find /root/.nuget/packages/microsoft.aspnetcore.app.internal.assets -type f -path '*/_framework/blazor.web.js' -print -quit | xargs -r dirname)" \
    && test -n "$assets_dir" \
    && mkdir -p /app/publish/wwwroot/_framework \
    && cp -a "$assets_dir"/. /app/publish/wwwroot/_framework/

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

COPY --from=build /app/publish ./
ENTRYPOINT ["dotnet", "ClientesBlazor.dll"]
