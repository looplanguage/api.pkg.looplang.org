<h1 align="center">Loop Package manager</h1>
    <p align="center">
       A Modern Type-Safe Programming Language<br>
       <a href="https://looplang.org/">Website</a> |
       <a href="https://downloads.looplang.org">Downloads</a> |
       <a href="https://discord.gg/T3tqQBTyJA">Discord</a> | 
       <a href="https://looplang.atlassian.net/jira/dashboards/10003">Jira Board</a><br>
    </p>
<br>

<hr/>

The Loop language is a new programming language for data science and AI. Loop uses a package manager to manage the various packages that users and companies are developing for the language. 

This repository contains the backend logic and API for the package manager. 

# Gettings started

## Prerequisites
* Git
* Docker (version 19.03.0+)
* Docker compose (Comes pre-installed with Docker Desktop)
## Steps
1. Clone this repository, using the following command: ``git clone https://github.com/looplanguage/api.pkg.looplang.org``
2. Browse to the folder of the repository
3. Run the command: ``docker-compose up``
4. After a short while, the services should be running 
5. When you want to stop the services from running, run the command: ``docker-compose down``

## When installed
When all the images have been successfully built the API can be accessed from “https://localhost:5001”. The database has a “3306” as port and server name “db”.