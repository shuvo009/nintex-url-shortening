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
I use `Onion` Architecture here.
![alt text](https://github.com/shuvo009/nintex-url-shortening/blob/master/ProjectArchitecture.PNG "Project Architecture")
