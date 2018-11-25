# GarageSeller

EDIT : I am happy to announce that the deployment of the app **works** when using **Docker for Windows**. It means that the Garage API is able to connect to the SQL Server !!! Using Docker Toolbox on Windows (which uses docker machine and special IPs was the reason why my `docker stack deploy` and `docker compose up` had two apps that were unable to communicate. This is a huge great news since it means that all the hard work was good from the beginning.

**tl;dr: DO NOT USE DOCKER TOOLBOX IF YOU WANT TO USE THE DOCKER-COMPOSE.YML, USE DOCKER FOR WINDOWS**

Garage seller is the convergence of two subjects I study in more depth recently: ASP.NET Core and Docker
When I decided I had enough knowledge, I set the objective to create an application 
composed of an API and its persistence running in containers. Another mandatory point was to design 
the API with Swagger.

- API.NET Core 2.1
- Entity Framework code first **with Migrations**
- Docker
- Swagger / Open Api Specification

Improvements: 
- ~~docker-compose.yml to reveal the sql server express used in prod~~ => DONE
- integration of nswag or swashbuckle to expose the swagger ui interface - for the moment, the API specification is provided, this is enough
