#!/usr/bin/env bash

dotnet publish -r linux-x64 \
     -p:PublishReadyToRun=true \
     -p:PublishSingleFile=true \
     -p:IncludeNativeLibrariesForSelfExtract=true \
     --self-contained true
    #  -p:PublishTrimmed=true \


# ./bin/Debug/net6.0/linux-x64/publish/tm-embed-items
