FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Musicians/Musicians.API/Musicians.API.csproj", "Services/Musicians/Musicians.API/"]
COPY ["Services/Musicians/Musicians.Application/Musicians.Application.csproj", "Services/Musicians/Musicians.Application/"]
COPY ["Services/Musicians/Musicians.Infrastructure/Musicians.Infrastructure.csproj", "Services/Musicians/Musicians.Infrastructure/"]
COPY ["Services/Musicians/Musicians.Domain/Musicians.Domain.csproj", "Services/Musicians/Musicians.Domain/"]
COPY ["Shared/Shared.csproj", "Shared/"]
RUN dotnet restore "Services/Musicians/Musicians.API/Musicians.API.csproj"
COPY ./Services/Musicians/ ./Services/Musicians
COPY ./Shared/ ./Shared
WORKDIR "/src/Services/Musicians/Musicians.API"
RUN dotnet build "Musicians.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Musicians.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Musicians.API.dll"]