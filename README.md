# Distributed Lock Manager .NET

.NET Client for [Distributed Lock Manager Server](https://github.com/barend-erasmus/distributed-lock-manager)

## Installation

### Prerequisites

* [Node.js (9.9.0)](https://nodejs.org/en/download/current)
* [Distributed Lock Manager Server](https://github.com/barend-erasmus/distributed-lock-manager)

**Package Manager**

`Install-Package DistributedLockManager.NET -Version 1.0.5`

**.NET CLI**

`dotnet add package DistributedLockManager.NET --version 1.0.5`

**Paket CLI**

`paket add DistributedLockManager.NET --version 1.0.5`

## Usage

```CSharp
 var distributedLockManagerClient = new DistributedLockManagerClient("127.0.0.1", 5001);

if (distributedLockManagerClient.WaitAcquire("mylock"))
{

    // TODO

    distributedLockManagerClient.Release("mylock");
}

distributedLockManagerClient.Dispose();
```
