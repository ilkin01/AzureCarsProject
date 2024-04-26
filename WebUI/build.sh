#!/bin/sh

# Download and install .NET SDK
curl -sSL https://dot.net/v1/dotnet-install.sh > dotnet-install.sh
chmod +x dotnet-install.sh
./dotnet-install.sh -c 7.0 -InstallDir ./dotnet

# Display the installed .NET SDK version
./dotnet/dotnet --version

# Publish the Blazor project
./dotnet/dotnet publish -c Release -o output
