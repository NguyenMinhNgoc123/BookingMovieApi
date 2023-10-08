FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /src
COPY ["BOOKING_MOVIE_ADMIN/BOOKING_MOVIE_ADMIN.csproj", "BOOKING_MOVIE_ADMIN/"]
RUN dotnet restore "BOOKING_MOVIE_ADMIN/BOOKING_MOVIE_ADMIN.csproj"
COPY . .
WORKDIR "/src/BOOKING_MOVIE_ADMIN"
RUN dotnet build "BOOKING_MOVIE_ADMIN.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BOOKING_MOVIE_ADMIN.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BOOKING_MOVIE_ADMIN.dll"]
