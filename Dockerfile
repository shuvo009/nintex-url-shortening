FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src

RUN curl -sL https://deb.nodesource.com/setup_10.x |  bash -
RUN apt-get install -y nodejs

COPY ["Nintex.Url.Shortening.Web/Nintex.Url.Shortening.Web.csproj", "Nintex.Url.Shortening.Web/"]
COPY ["Nintex.Url.Shortening.Repository/Nintex.Url.Shortening.Repository.csproj", "Nintex.Url.Shortening.Repository/"]
COPY ["Nintex.Url.Shortening.Core/Nintex.Url.Shortening.Core.csproj", "Nintex.Url.Shortening.Core/"]
COPY ["Nintex.Url.Shortening.DependencyInjection/Nintex.Url.Shortening.DependencyInjection.csproj", "Nintex.Url.Shortening.DependencyInjection/"]
COPY ["Nintex.Url.Shortening.Services/Nintex.Url.Shortening.Services.csproj", "Nintex.Url.Shortening.Services/"]
COPY ["Nintex.Url.Shortening.Identity/Nintex.Url.Shortening.Identity.csproj", "Nintex.Url.Shortening.Identity/"]
RUN dotnet restore "Nintex.Url.Shortening.Web/Nintex.Url.Shortening.Web.csproj"
COPY . .
WORKDIR "/src/Nintex.Url.Shortening.Web"
RUN dotnet build "Nintex.Url.Shortening.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Nintex.Url.Shortening.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Nintex.Url.Shortening.Web.dll"]