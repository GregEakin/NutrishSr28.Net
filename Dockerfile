FROM mcr.misrosoft.com/dotnet/sdk:5.0

dotnet build NutrishSr28.Core.sln

FROM mcr.misrosoft.com/dotnet/runtime:5.0

RUN curl https://www.ars.usda.gov/ARSUserFiles/80400535/DATA/SR/sr28/dnload/sr28asc.zip -O dir \
 && unzip dir/sr28asc.zip -d dir2 \
 && rm dir/sr28asc.zip

DBSetup/bin/Debug/net5.0/DBSetup

FROM arm64v8/postgres
