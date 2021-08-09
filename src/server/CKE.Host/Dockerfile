#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["CKE.Host/CKE.Host.csproj", "CKE.Host/"]
RUN dotnet restore "CKE.Host/CKE.Host.csproj"
COPY . .
WORKDIR "/src/CKE.Host"
RUN dotnet build "CKE.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CKE.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CKE.Host.dll"]