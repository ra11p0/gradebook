## Setup

### Setup MySql on docker

#### Install

    docker pull mysql/mysql-server:latest

    docker run --name=mysql -p 3306:3306 -d mysql/mysql-server:latest

#### Change password

Password is generated automatically. Check:

    docker logs mysql

then:

    docker exec -it mysql bash

    mysql -u root -p

    ALTER USER 'root'@'localhost' IDENTIFIED BY 'P@55W0RD';

    CREATE DATABASE GradebookDB;

    CREATE USER 'gradebook'@'localhost' IDENTIFIED BY 'gr@d3b00k';
    
    UPDATE mysql.user SET host = '%';
    
    FLUSH PRIVILEGES;

    GRANT ALL PRIVILEGES ON GradebookDB.* TO 'gradebook'@'%' WITH GRANT OPTION;

    FLUSH PRIVILEGES;

   
