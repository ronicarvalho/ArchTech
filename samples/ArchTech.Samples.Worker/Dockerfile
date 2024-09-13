FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["samples/ArchTech.Samples.Worker/ArchTech.Samples.Worker.csproj", "samples/ArchTech.Samples.Worker/"]
RUN dotnet restore "samples/ArchTech.Samples.Worker/ArchTech.Samples.Worker.csproj"
COPY . .
WORKDIR "/src/samples/ArchTech.Samples.Worker"
RUN dotnet build "ArchTech.Samples.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ArchTech.Samples.Worker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ArchTech.Samples.Worker.dll"]
