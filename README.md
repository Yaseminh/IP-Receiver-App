# IP Receiver Project

## Overview
IP Receiver is a Windows Forms application designed for managing licenses, TCP port communication, data encryption/decryption, CRC calculations, and logging activities. The application provides a user interface to manage ports, query data, open panels, and access help features.

---

## Features

### 1. License Management
- Validates licenses based on the computer's unique ID.
- Supports license activation and removal.
- Enables or disables menu items and controls depending on license status.
- License information is retrieved and updated from a SQL Server database.

### 2. Port Management
- Open and close TCP ports.
- Updates connection status when a port is closed.
- Manages socket connections using `_serverSocket`.
- Logs errors during port operations.

### 3. Data Processing
- Extracts data from specific indices and lengths (`GetbeetweenIndexwithLenth`).
- Converts hexadecimal strings to byte arrays (`StringToByteArray`).
- Calculates CRC values (`calcCRC` and `Pointercrc`).
- Encrypts and decrypts data using AES (`EncryptStringToBytes_Aes` and `DecryptStringFromBytes_Aes`).

### 4. User Interface
- Menu and panel management on the main form.
- Minimize, restore, and close handling using `NotifyIcon`.
- Opens help and "About" windows.
- Provides a panel for querying stored data.

### 5. Error and Activity Logging
- Error logs are saved daily in the `IPReceiverError_Log` folder.
- Activity logs for sent data are saved in `IPReceiversendToAtraq_Log`.
- Logging is handled using `allerror.TxtKaydetErrorLog` and `allerror.TxtKaydetAtraq`.

---

## Technologies Used
- C# .NET Framework / Windows Forms
- SQL Server
- AES Encryption (`System.Security.Cryptography`)
- TCP Socket Communication (`System.Net.Sockets`)
- CRC Calculation
- Thread-safe operations and proper resource disposal

---

## How to Use
1. Launch the application (`Form1`).
2. Activate your license through the "License" menu if not already active.
3. Open the desired TCP port using the "Network Ports" menu.
4. Receive and process data; logs will be automatically created.
5. Close ports when finished using the "Close Port" button.
6. Access help or about windows as needed from the menu.

---

## Error Handling
- All exceptions are logged with timestamps in the error log folder.
- Socket exceptions are specifically handled and logged separately.
- AES encryption/decryption and CRC calculation errors are also logged.

---

## License
This project is licensed under [Your License Here].  

---

## Author
- Developed by Yasemin.
