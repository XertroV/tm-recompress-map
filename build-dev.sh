#!/usr/bin/env bash

./build.sh
_DIR=$PWD
cd ~/win/Documents/Trackmania
$_DIR/bin/Debug/net6.0/linux-x64/publish/tm-embed-items ../../test-item-embed.json
