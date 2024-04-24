&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <img src="./.assets/DDS.Net Icon-BG-None.png" width="15%" />


# DDS.Net Server - v1.3.0

*Available at [nuget](https://www.nuget.org/packages/CronBlocks.DDS.Net.Server)*.

*DDS.Net Server* is an in-app-able lightweight, performant server for managed data distribution through connectors. The supported data types are:

| Main type                                          | Sub-type          | Represented data                                    |
|----------------------------------------------------|-------------------|-----------------------------------------------------|
| ***Primitive*** &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; | *String*          | Sequence of characters in Unicode                   |
|                                                    | *Boolean*         | A boolean (True or False)                           |
|                                                    | *Byte*            | 1-byte Signed Integer                               |
|                                                    | *Word*            | 2-byte Signed Integer                               |
|                                                    | *DWord*           | 4-byte Signed Integer                               |
|                                                    | *QWord*           | 8-byte Signed Integer                               |
|                                                    | *Unsigned Byte*   | 1-byte Unsigned Integer                             |
|                                                    | *Unsigned Word*   | 2-byte Unsigned Integer                             |
|                                                    | *Unsigned DWord*  | 4-byte Unsigned Integer                             |
|                                                    | *Unsigned QWord*  | 8-byte Unsigned Integer                             |
|                                                    | *Single*          | A single precision (4-byte) Floating-point number   |
|                                                    | *Double*          | A double precision (8-byte) Floating-point number   |
| ***Raw Bytes***                                    | -                 | Sequence of bytes                                   |



## Sample application

Using various extensions and helpers available in the library, development of container application is quite straightforward. Sample console application is as follows:

```csharp
using DDS.Net.Server.Entities;
using DDS.Net.Server.Interfaces;
using DDS.Net.Server.Interfaces.DefaultLogger;
using DDS.Net.Server.PublicExtensions;
using DDS.Net.Server.PublicHelpers;

namespace DDS.Net.Server.ConsoleApp
{
    internal class Program
    {
        private static ConsoleLogger logger = new ConsoleLogger(LogLevel.Information);
        private static DdsServer server = null!;

        static void Main(string[] args)
        {
            Console.Title = $"DDS.Net Server";

            "DDS.Net Server".WriteLine(ConsoleColor.DarkCyan);
            "--------------".WriteLine(ConsoleColor.DarkCyan);

            (bool isEnabled, ServerConfiguration? config) =
                ConfigurationProvider.GetServerConfiguration("server-config.ini", logger);

            if (isEnabled && config != null)
            {
                server = new DdsServer(
                    config,
                    ConfigurationProvider.GetVariablesConfiguration("variables-config.ini", logger));

                server.Start();
            }
            else
            {
                server = null!;
                logger.Error("Failed to initialize the server configuration.");
                logger.Error("Server is either not enabled, or its configuration cannot be read.");
            }

            ConsoleHelpers.WaitForKey("Press ESC to exit.", ConsoleKey.Escape);

            server?.Stop();
        }
    }
}
```

Our sample config "*server-config.ini*" file contains:
```ini
[DDS Connections]
Enabled = Yes
ListeningIPv4 = All
TCP-Enabled = Yes
TCP-ListeningPort = 44556
TCP-MaxClients = 100
UDP-Enabled = Yes
UDP-ListeningPort = 44556
```

Variables' configuration file "*variables-config.ini*" contains:
```ini
; Format:
;    [Variable Name]
;    Settings...
;    ...
;    
;    [Another Variable Name]
;    ...
;    ...
;
; VariableType =
;       Primitive => Very basic type of data
;       RawBytes  => A sequence of bytes (unsigned bytes)
;
; PrimitiveType = 
;       String        => A String of characters
;
;       Boolean       => Boolean that can only have True or False value
;
;       Byte          => 1-byte Signed Integer number
;       UnsignedByte  => 1-byte Unsigned Integer number
;
;       Word          => 2-byte Signed Integer number
;       UnsignedWord  => 2-byte Unsigned Integer number
;
;       DWord         => 4-byte Signed Integer number
;       UnsignedDWord => 4-byte Unsigned Integer number
;
;       QWord         => 8-byte Signed Integer number
;       UnsignedQWord => 8-byte Unsigned Integer number
;
;       Single        => Single precision 4-byte Floating-point number
;       Double        => Double precision 8-byte Floating-point number
;

[Test Variable 1]
VariableType = Primitive
PrimitiveType = Single

[Test Variable 2]
VariableType = Primitive
PrimitiveType = Double

[Test Variable 3]
VariableType = Primitive
PrimitiveType = Boolean

[Test Variable 4]
VariableType = Primitive
PrimitiveType = String

[Test Variable 5]
VariableType = RawBytes
Data = 1, 2, 3, 4, 5, 6, 7, 8, 9

```



## Library usage

The [library](./docs/usage/README.md) includes *DDS.Net Server* and various helpers to ease developing container application.

