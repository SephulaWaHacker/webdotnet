FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BMW.CloudAdoption.BOM.csproj", "BMW.CloudAdoption.BOM/"]
RUN dotnet restore "BMW.CloudAdoption.BOM/BMW.CloudAdoption.BOM.csproj"
COPY [".", "BMW.CloudAdoption.BOM/"]
WORKDIR "/src/BMW.CloudAdoption.BOM"
RUN dotnet build "BMW.CloudAdoption.BOM.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BMW.CloudAdoption.BOM.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BMW.CloudAdoption.BOM.dll"]
