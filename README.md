# Nutrish Sr28 .Net
:fire: **Greg Eakin**

This is an experiment in configuring an existing [USDA Nutrition Database](https://www.ars.usda.gov/northeast-area/beltsville-md-bhnrc/beltsville-human-nutrition-research-center/methods-and-application-of-food-composition-laboratory/mafcl-site-pages/sr11-sr28/) in [NHibernate](https://nhibernate.info/).

## Steps to setup SQL Local DB:
1. Unzip the [Full Version of the SR28 ASCII file format](https://www.ars.usda.gov/northeast-area/beltsville-md-bhnrc/beltsville-human-nutrition-research-center/methods-and-application-of-food-composition-laboratory/mafcl-site-pages/sr11-sr28/) into the data folder
1. Unzip the patch file (May 2016) into the data2 folder
1. SqllocalDB i
1. SqllocalDB create "SR28" -s
1. sqlcmd -S (localdb)\SR28 -Q "CREATE DATABASE Nutrish"
1. Run the DBSetup.exe project

## Database:
[![USDA Nutrition Database](SR28lib/Nutrish%20SR28.jpg "USDA Nutrition Database")](https://www.ars.usda.gov/northeast-area/beltsville-md-bhnrc/beltsville-human-nutrition-research-center/methods-and-application-of-food-composition-laboratory/mafcl-site-pages/sr17-sr28/)
US Department of Agriculture, Agricultural Research Service. 2016. Nutrient Data Laboratory. USDA National Nutrient Database for Standard Reference, Release 28 (Slightly revised). Version Current: May 2016. [http://www.ars.usda.gov/nea/bhnrc/mafcl](http://www.ars.usda.gov/nea/bhnrc/mafcl)

## Tools:
- [NHibernate](https://nhibernate.info/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server)
- [SQL Server Management Sdutio](https://docs.microsoft.com/en-us/sql/ssms/sql-server-management-studio-ssms)
- [Visual Studio](https://visualstudio.microsoft.com/)
- [ReSharper](https://www.jetbrains.com/resharper/)
- [Unit Tests](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-mstest)
- [Git Extensions](http://gitextensions.github.io/)

## Author
:fire: [Greg Eakin](https://www.linkedin.com/in/gregeakin)
