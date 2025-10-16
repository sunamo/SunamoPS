# SunamoPS

Working with Powershell 7 - Invoking commands, return outputs etc.

## Overview

SunamoPS is part of the Sunamo package ecosystem, providing modular, platform-independent utilities for .NET development.

## Main Components

### Key Classes

- **PowershellMethod**
- **ErrorRecordHelper**
- **ExceptionsExtensions**
- **PowershellBuilder**
- **PowershellHelper**
- **PowershellParser**
- **PowershellRunnerString**
- **PS**
- **PsOutput**

### Key Methods

- `Text()`
- `GetAllMessages()`
- `Clear()`
- `AddRaw()`
- `AddRawLine()`
- `AddArg()`
- `Cd()`
- `FindDuplicatedMethodsInPs1File()`
- `ParseMethods()`
- `ProcessNames()`

## Installation

```bash
dotnet add package SunamoPS
```

## Dependencies

- **Microsoft.Extensions.Logging.Abstractions** (v9.0.3)
- **Microsoft.PowerShell.SDK** (v7.5.0)
- **System.Formats.Asn1** (v9.0.3)
- **System.Net.Http** (v4.3.4)
- **System.Text.Encodings.Web** (v9.0.3)

## Package Information

- **Package Name**: SunamoPS
- **Version**: 25.6.7.1
- **Target Framework**: net9.0
- **Category**: Platform-Independent NuGet Package
- **Source Files**: 35

## Related Packages

This package is part of the Sunamo package ecosystem. For more information about related packages, visit the main repository.

## License

See the repository root for license information.
