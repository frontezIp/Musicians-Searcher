FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Chat/Chat.API/Chat.API.csproj", "Services/Chat/Chat.API/"]
COPY ["Services/Chat/Chat.BusinessLogic/Chat.BusinessLogic.csproj", "Services/Chat/Chat.BusinessLogic/"]
COPY ["Services/Chat/Chat.DataAccess/Chat.DataAccess.csproj", "Services/Chat/Chat.DataAccess/"]
COPY ["Shared/Shared.csproj", "Shared/"]
RUN dotnet restore "Services/Chat/Chat.API/Chat.API.csproj"
COPY ./Services/Chat/ ./Services/Chat
COPY ./Shared/ ./Shared
WORKDIR "/src/Services/Chat/Chat.API"
RUN dotnet build "Chat.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Chat.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Chat.API.dll"]