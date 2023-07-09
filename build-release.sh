#!/usr/bin/env bash

dotnet publish -r linux-x64 -c Release \
     -p:PublishReadyToRun=true \
     -p:PublishSingleFile=true \
     -p:IncludeNativeLibrariesForSelfExtract=true \
     -p:PublishTrimmed=true \
     --self-contained true


# ./bin/Debug/net6.0/linux-x64/publish/tm-embed-items
