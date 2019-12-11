#Requires -RunAsAdministrator

$instanceName = "SR28"

$serverName = "(localdb)\" + $instanceName
sqlcmd -S $serverName -i ".\Teardown-Databases.sql"

sqllocaldb stop $instanceName
sqllocaldb delete $instanceName