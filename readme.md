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

    mysql -uroot -p

    ALTER USER 'root'@'localhost' IDENTIFIED BY 'P@55W0RD';

    CREATE DATABASE GradebookDB;

    CREATE USER 'gradebook'@'localhost' IDENTIFIED BY 'gr@d3b00k';

    GRANT ALL PRIVILEGES ON GradebookDB.* TO 'gradebook' WITH GRANT OPTION;

    FLUSH PRIVILEGES;

    UPDATE mysql.user SET host = '%';