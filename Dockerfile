FROM postgres:13.1 as base
EXPOSE 5432
ENV POSTGRES_PASSWORD=pass1
# ENV POSTGRES_HOST_AUTH_METHOD=trust

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["SR28lib/SR28lib.csproj", "SR28lib/"]
RUN dotnet restore "SR28lib/SR28lib.csproj"
COPY . .
WORKDIR "/src/SR28lib"
RUN dotnet build "SR28lib.csproj" -c Release -o /app/build

WORKDIR /src
COPY ["DBSetup/DBSetup.csproj", "DBSetup/"]
RUN dotnet restore "DBSetup/DBSetup.csproj"
COPY . .
WORKDIR "/src/DBSetup"
RUN dotnet build "DBSetup.csproj" -c Release -o /app/build

FROM base AS final
WORKDIR /src
COPY --from=build /app/build .
#RUN wget https://www.ars.usda.gov/ARSUserFiles/80400535/DATA/SR/sr28/dnload/sr28asc.zip
#unzip sr28asc.zip -o data/
COPY data/* data/
RUN ./DBSetup

#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "BlazorApp1.dll"]