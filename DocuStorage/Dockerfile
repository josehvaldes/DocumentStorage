#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DocuStorage/DocuStorage.csproj", "DocuStorage/"]
COPY ["DocuStorate.Data/DocuStorage.Data.csproj", "DocuStorate.Data/"]
COPY ["DocuStorage.Common.Data/DocuStorage.Common.Data.csproj", "DocuStorage.Common.Data/"]
COPY ["DocuStorage.Common/DocuStorage.Common.csproj", "DocuStorage.Common/"]
COPY ["DocuStorage.Data.Dapper/DocuStorage.Data.Dapper.csproj", "DocuStorage.Data.Dapper/"]
RUN dotnet restore "DocuStorage/DocuStorage.csproj"
COPY . .
WORKDIR "/src/DocuStorage"
RUN dotnet build "DocuStorage.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DocuStorage.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DocuStorage.dll"]