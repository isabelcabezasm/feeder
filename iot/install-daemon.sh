#!/bin/bash

if [ -z "$1" ]
then
 echo "Usage $0 [DEVICE_CONNECTION_STRING]"
 exit 0
fi

echo "Installing main.js as a daemon with pm2"
sudo DEVICE_CONNECTION_STRING=$1  pm2 start main.js
sudo pm2 startup
sudo pm2 save

