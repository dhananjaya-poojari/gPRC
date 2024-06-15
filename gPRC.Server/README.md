# gRPC UI Usage Guide

This guide will help you download and run `grpcui` to interact with your gRPC server using a graphical user interface.

## Prerequisites

- Ensure you have [PowerShell](https://docs.microsoft.com/en-us/powershell/scripting/overview) installed on your system.
- You need access to the gRPC server you want to interact with. In this example, we will use `localhost:7065`.

## Steps to Run gRPC UI

### 1. Download `grpcui`

1. Visit the [grpcui releases page](https://github.com/fullstorydev/grpcui/releases).
2. Download the appropriate executable (`grpcui.exe`) for your operating system. For Windows, choose the `.exe` file.

### 2. Navigate to the Downloaded File

1. Open PowerShell.
2. Navigate to the directory where you downloaded `grpcui.exe`. You can use the `cd` command to change directories. For example:

   ```powershell
   cd C:\path\to\your\download\folder
   ```

### 3. Run grpcui
Execute the following command in PowerShell to start grpcui and connect to your gRPC server:

powershell
```
.\grpcui.exe localhost:7065
```
### 4. Interact with gRPC UI
![image](https://github.com/dhananjaya-poojari/gPRC/assets/77887564/ec3cf32b-4195-4d92-ad1d-c0272816ca83)
