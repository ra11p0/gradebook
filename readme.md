## Setup

### Setup MySql on docker

#### Install

    docker pull mysql/mysql-server:latest

    docker run --name=mysql -d mysql/mysql-server:latest

#### Change password

Password is generated automatically. Check:

    docker logs mysql

    docker exec -it mysql bash

    mysql -uroot -p

    ALTER USER 'root'@'localhost' IDENTIFIED BY 'P@55W0RD';



    CREATE USER 'gradebook'@'localhost' IDENTIFIED BY 'gr@d3b00k';

    CREATE DATABASE GradebookDB;

    GRANT ALL PRIVILEGES ON GradebookDB.* TO 'gradebook'@'localhost';