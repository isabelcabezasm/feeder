#!/bin/bash
if [-z "$1"] echo "Usage $0 [DEVICE_CONNECTION_STRING]" && exit 0;

# need superuser for the gpio library
[ `whoami` = root ] || exec su -c $0 root

echo "Executing as su"

DEVICE_CONNECTION_STRING=$1  pm2 start main.js
pm2 startup
pm2 save

echo "Done"
