# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /source

COPY . .
RUN dotnet restore "TaskManagement.Api/TaskManagement.Api.csproj"
RUN dotnet publish "TaskManagement.Api/TaskManagement.Api.csproj" -c release -o /app --no-restore

# Run
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./

ENV ASPNETCORE_URLS=http://+:5050
ENV ASPNETCORE_ENVIRONMENT=Homolog

EXPOSE 5050

ENTRYPOINT ["dotnet", "TaskManagement.Api.dll"]