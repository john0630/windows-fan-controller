# Windows Laptop Fan Controller

A lightweight Windows Forms application that utilizes `LibreHardwareMonitorLib` to manage your laptop's fan speed and monitor device temperatures.

## Features
- Displays system temperatures (CPU, GPU, Motherboard).
- Reports real-time fan RPMs and duty cycles (%).
- Allows overriding manual control speeds (if supported by the motherboard).
- Supports auto-default speed fallback.

## Pre-requisites
- Windows OS
- .NET 10 (or matching SDK)
- Must be run as **Administrator** because hardware sensing requires Ring-0 (driver level) access to your system's EC/SMBus.

## Running the Application
To run the project locally, execute standard `dotnet` CLI rules:
```bash
# This application will prompt for Administrator access upon launch
dotnet run
```
Or you can navigate to `bin/Debug/netx.x-windows/` and simply double-click `FanController.exe`.

## ⚠️ Important Note About Laptops
Most laptops (Dell, HP, Lenovo) DO NOT expose fan speeds to the OS and rely exclusively on a locked Embedded Controller (EC). If your fan cannot be discovered or changing the slider does nothing, this is a hardware security/vendor-lock limitation. Desktop motherboards generally support these overrides out of the box.
