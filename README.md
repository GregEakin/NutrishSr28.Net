# Nutrish Sr28 .Net
:fire: **Greg Eakin**

This is an experiment in configuring an existing [USDA Nutrition Database](https://www.ars.usda.gov/northeast-area/beltsville-md-bhnrc/beltsville-human-nutrition-research-center/methods-and-application-of-food-composition-laboratory/mafcl-site-pages/sr17-sr28/) in [NHibernate](https://nhibernate.info/).

## Steps to setup SQL Local DB
1. Unzip the [Full Version of the SR28 ascii text files](https://www.ars.usda.gov/northeast-area/beltsville-md-bhnrc/beltsville-human-nutrition-research-center/methods-and-application-of-food-composition-laboratory/mafcl-site-pages/sr17-sr28/) into the data folder
1. Unzip the patch file into the data2 folder
1. SqllocalDB i
1. SqllocalDB create "SR28" -s
1. sqlcmd -S (localdb)\SR28 -Q "CREATE DATABASE Nutrish"
1. DBSetup.exe

## Database:
[![USDA Nutrition Database](SR28lib/Nutrish%20SR28.jpg "USDA Nutrition Database")](https://www.ars.usda.gov/northeast-area/beltsville-md-bhnrc/beltsville-human-nutrition-research-center/methods-and-application-of-food-composition-laboratory/mafcl-site-pages/sr17-sr28/)
