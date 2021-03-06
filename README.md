# :car: altTrackSimulation :blue_car:

altTrack is a simple vehicle simulation to demonstrate and try out some development features with fun.  :computer: :blush:

>Monitoring requirment for some smart vehicles' connection status and ping them to check if they are connected or not, is the main scenario for this demostration.

| Applications     | Build Status         |Deployment |
| -----------------|:-------------:|-------------|
| Web UI           | [![Build status](https://dev.azure.com/altTrack/altTrack%20Simulation/_apis/build/status/altTrack%20Web%20UI)](https://dev.azure.com/altTrack/altTrack%20Simulation/_build/latest?definitionId=2) |![Deployment](https://vsrm.dev.azure.com/altTrack/_apis/public/Release/badge/717a7e71-436a-4b0a-942b-0f186e06f72d/1/1)|
| Ping Service     | [![Build status](https://dev.azure.com/altTrack/altTrack%20Simulation/_apis/build/status/altTrack%20Ping%20Service%20Build)](https://dev.azure.com/altTrack/altTrack%20Simulation/_build/latest?definitionId=3)      |![Deployment](https://vsrm.dev.azure.com/altTrack/_apis/public/Release/badge/717a7e71-436a-4b0a-942b-0f186e06f72d/2/2)|
| Business Service | [![Build status](https://dev.azure.com/altTrack/altTrack%20Simulation/_apis/build/status/altTrack%20Business%20Service%20Build)](https://dev.azure.com/altTrack/altTrack%20Simulation/_build/latest?definitionId=4)      |![Deployment](https://vsrm.dev.azure.com/altTrack/_apis/public/Release/badge/717a7e71-436a-4b0a-942b-0f186e06f72d/3/3)|
| Vehicle Simulation | [![Build status](https://dev.azure.com/altTrack/altTrack%20Simulation/_apis/build/status/altTrack%20Vehicle%20Simulation%20Build)](https://dev.azure.com/altTrack/altTrack%20Simulation/_build/latest?definitionId=5)      |![Deployment](https://vsrm.dev.azure.com/altTrack/_apis/public/Release/badge/717a7e71-436a-4b0a-942b-0f186e06f72d/4/4)|

### Structure

Mainly the solution contains two **ASP.NET Core Web API** projects, one **ASP.NET Core Web(Razor Pages)** app. and one **Azure Fuction**. These can be thought  as some business components(services). They are not strictly connected to each other. They have their own data storage and operation logic in themselves. The main idea behind in this, make services as **loose coupling** and **seperate data structures and storage** as possible as it can be, to have **seperate scope** of features/functions. 

To simulate vehicles' Azure Fuctions is used in this solution. Of course, it can be done with some other mock-ups like IoT solutions.(_Maybe in future, I will do with my Raspberry Pi(s) with more fun :smiley: :smiley:_) In real world, vehicles are not running all time and preserve their resources(gas, electricity, ..etc.) In that approach, **serverless computing** is a good demostration way for vehicles.   

*_The scenario and the demostration might not be a perfect tech. solution but good way to dig more from keywords_

### Tools

* Visual Studio Community Edition 2017+ or Visual Studio Code
* Docker (optional)
* SQL Server (optional)
* Postman (optional)

### Run

1. Clone this repo to your own development machine (https://github.com/ardacetinkaya/altTrackSimulation.git)
2. Build and run altTrack.BusinessService
3. Build and run altTrack.PingService
4. And finally build and run altTrack.WebUI

*_For vehicle simulation run altTrack.Vehicle project as Azure Functions app, so additional Azure Functions tools/plug-ins might be needed for Visual Studio_

*_In debug mode, no need to create DB(s). All data storage is operated as in-memory db in debug mode. In release mode setting of SQL Servers and connections' strings in configuration files are necessary. Please do not forget to execute the table creation scripts in Database folders of projects' /Data folder._

### Built with...

* C#
* .NET Core v2.2 and ASP.NET Core v2.2 - (Web API and Web UI(Razor Pages)
* Entity Framework - (MS SQL Server and In-Memory DB)
* Bootstrap v4
* Azure Services - (App Services, Functions, SQL)
* Azure DevOps - (For all CI/CD pipelines)
* Docker

### Configuration

| Applications     | Configuration |Description  |
| -----------------|:--------------|-------------|
|Web UI|AltTrackBusinessService|URI ofBusiness service to fetch data and execute operations|
|Web UI|AltTrackPingService|URI of Ping service to Ping vehicles|
|Business Service|AltTrackPingService|URI of Ping service to get latest ping status|
|Business Service|DBConnectionString|Connection string for DB|
|Business Service|TimeLimit|Time limit for connection status|
|Ping Service|DBConnectionString|Connection string for DB|


### Integration and Deployment

* There three App. Services and one Function in Azure for this repository.
* Every commit to this repository create a build task in Azure DevOps
* Build tasks are triggered as repository paths.(Ex: Commit to Web.UI project triggers just Web UI build taks)
* When the build tasks completed successfully, a deployment taks in release pipeline is triggered.
* Configuration parameters transforms are executed in deployment task with variables in task. So for specific enviroment, configuration settings can be manged.(_Please be sure about variables are set correctly_)
* When deployment task is finished as successfull, everthing is ready and running.

*_It is also possible to make deployment as Docker images, check Docker files and set parameters as needed_

### Missing points...

* Events between services
* Alternative vehicle simulation scenarios



[![ForTheBadge built-with-love](http://ForTheBadge.com/images/badges/built-with-love.svg)](https://gitHub.com/ardacetinkaya/)




