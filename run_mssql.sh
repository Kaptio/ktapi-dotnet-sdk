#!/bin/bash

echo run mssql container

docker run --rm -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=doomDOOM1234(!)' -p 1433:1433 -v mssqlvolume:/var/opt/mssql -d microsoft/mssql-server-linux:2017-latest