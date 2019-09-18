# URL Shortening Service
### Problem Statement
You have been asked to implement a URL shortening service, along the lines of something like tinyurl.com or bit.ly. Your service needs to be able to both take long URLs as input and shorten them, as well as being able to inflate a shortened URL to its original form.

## Live Demo
This demo is hosted at AWS Elastic Beanstalk

Url: **http://shorturlnintx.us-east-1.elasticbeanstalk.com**

## Login Credentials
Username | Passowrd
--- | --- |
**admin** | **admin** |

## Deploy Locally
1. Download and install Docker https://www.docker.com/products/docker-desktop
2. Clone this repository 
3. Run `docker-compose up`
4. Browser at `http://localhost:8000`

## Technologies Are Used
* Asp.net Core 2.2
* Entity Framework Core
* SQL Server 
* Angular 8
* Nunit

## Project Architecture
I use `Onion Architecture` here.
![alt text](https://github.com/shuvo009/nintex-url-shortening/blob/master/ProjectArchitecture.PNG "Project Architecture")

## Design Patterns are used
* MVC
* Repository patterns 
* Façade

## Run Unit Tests
1. Go to `Test > Windows > Test Explorer` from Visual Studio main menu.
2. Click on `run all Test`

## Documantation & Tools:
1. Visual Studio 2019
2. Swagger for API documentation (`http://localhost:8000/docs/swagger`)
3. app-metrics for server metrics (`http://localhost:8000/ metrics`)
4. Nlog for logging 



