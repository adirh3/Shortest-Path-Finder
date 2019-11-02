# Shortest-Path-Finder

Finding the shortest path by crawling through links in webpages.

## About The Project

This project was created in order to find the shortest time between 2 articles on Wikipedia.
The project was built so it can be ugpraded to work on every type of article (not only Wikipedia).
You can run this project using different algorithms and implement more.

### Supported Algorithm
* BFS - Breath-first search algorithm - single threaded search using BFS graph search. (https://en.wikipedia.org/wiki/Breadth-first_search)
* Parallel Crawling - searching in parallel using asynchronous crawling.

### Built With
* [dotnet-core-3.0](https://dotnet.microsoft.com/download/dotnet-core/3.0)
* [generic-host](https://dotnet.microsoft.com/download/dotnet-core/3.0)

## Getting Started

To build this project you need the following:

### Prerequisites

Any operating system.
* [dotnet-core-3.0](https://dotnet.microsoft.com/download/dotnet-core/3.0)

### How to build

Clone the project, open cli in the src directory and run:
```
  dotnet build
```

### How to run

How to run the project:
#### If you have dotnet-core installed

Open cli in the src directory:
1.```cd ShortPathfinder```
2.```dotnet run```

#### If you don't have dotnet-core installed

Download the self-contained binaries from the build directory and execute the ShortestPathFinder file (On Windows it's .exe).

## Usage

You can run the ShortestPathFinder using the following arguments:
  -s, --source           The source node
  -d, --destination      The destination node
  -f 0, --finder=0       The relation finder, 0 - Wikipedia, 1 - Http
  -a 0, --algorithm=0    The path finder algorithm, 0 - Parallel Crawling, 1 - BFS
  --help                 Display this help screen.
  --version              Display version information.
  
If arguments are not passed the program will use the configuration file.
  
### Examples

``` dotnet run -s Travelling salesman problem -d NP-hardness ```
This command will find the shortest path between Travelling salesman problem and NP-hardness using Wikipedia as default and Parallel Crawling as default.

### Configuration

You can access more configuration through the `appsettings.json` file.
*Note* - Any given argument will override it's configuration value
```
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "System": "Information",
      "Microsoft": "Information"
    },
    "Console": {
      "IncludeScopes": false
    }
  },
  "Parallel": {
    "MaxParallelism": 8,
    "DelayBetweenIterations": 0
  },
  "Wikipedia": {
    "Language": "en",
    "UseSSL": true
  },
  "StartArgs": {
    "Source": "Travelling salesman problem",
    "Destination": "Software engineering",
    "RelationFinder": 0,
    "Algorithm": 0
  }
}
```
  
