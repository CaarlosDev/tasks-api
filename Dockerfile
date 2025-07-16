FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app
COPY tasks-api.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /out ./
EXPOSE 5039
ENV ASPNETCORE_URLS=http://+:5039
ENTRYPOINT ["dotnet", "tasks-api.dll"]