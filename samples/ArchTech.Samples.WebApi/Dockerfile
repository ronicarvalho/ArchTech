FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["samples/ArchTech.Samples.WebApi/ArchTech.Samples.WebApi.csproj", "samples/ArchTech.Samples.WebApi/"]
RUN dotnet restore "samples/ArchTech.Samples.WebApi/ArchTech.Samples.WebApi.csproj"
COPY . .
WORKDIR "/src/samples/ArchTech.Samples.WebApi"
RUN dotnet build "ArchTech.Samples.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ArchTech.Samples.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ArchTech.Samples.WebApi.dll"]
