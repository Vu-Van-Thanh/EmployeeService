FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["EmployeeService.API/EmployeeService.API.csproj", "EmployeeService.API/"]
COPY ["EmployeeService.Core/EmployeeService.Core.csproj", "EmployeeService.Core/"]
COPY ["EmployeeService.Infrastructure/EmployeeService.Infrastructure.csproj", "EmployeeService.Infrastructure/"]
RUN dotnet restore "EmployeeService.API/EmployeeService.API.csproj"

COPY . .
WORKDIR "/src/EmployeeService.API"
RUN dotnet build "EmployeeService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EmployeeService.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EmployeeService.API.dll"]