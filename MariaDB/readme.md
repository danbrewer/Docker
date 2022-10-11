# How to test MariaDB
1. Open a terminal window in the MariaDB folder
   1. Create a Data directory in this folder to house the MariaDB database files
   2. CD to the Compose folder
   3. Fire up the docker-compose.yml file (see cheatsheet below) 
2. Open another terminal window (the location doesn't matter for this one)
   1. Connect to MariaDB via command-line (see cheatsheet)
   2. At the MariaDB prompt, create a database named ItemsDatabase
   3. Exit the MariaDB prompt
   4. Close the terminal window
3. Open a terminal window and CD to the WebApi folder
   1. Apply the EF migration against MariaDB (see cheatsheet)
   2. Launch the WebApi project using "dotnet run"
4. Open Chrome
   1. Navigate to http://localhost:5000/swagger
   2. Perform CRUD operations

# Cheatsheet
## Start the container
```
docker-compose up -d
```

## Stop the container
```
docker-compose down
```

## Connect to mysql from host terminal

```bash
mysql -h 0.0.0.0 -P 1234 -u root -ppassword
```
where -P is the port and -h is the host IP
and -p is the password "password"

## Connect to MariaDB from a Docker shell
Launch an interactive Docker shell
```
docker exec -it maria_items bash     <return>
```
The command prompt will look similar to the following, indicating you're in the Docker shell as root:
```
root@65b86c4270e1:/# 
```
Now connect to MariaDB using this command:
```
root@65b86c4270e1:/# mysql -uroot -ppassword        <return>
```
You should be logged in to MariaDB now:
```
Welcome to the MariaDB monitor.  Commands end with ; or \g.
Your MariaDB connection id is 11
Server version: 10.5.10-MariaDB-1:10.5.10+maria~focal mariadb.org binary distribution

Copyright (c) 2000, 2018, Oracle, MariaDB Corporation Ab and others.

Type 'help;' or '\h' for help. Type '\c' to clear the current input statement.

MariaDB [(none)]> 
```

## Show databases
```bash
>  show databases; 

+--------------------+
| Database           |
+--------------------+
| information_schema |
| mysql              |
| performance_schema |
| sys                |
+--------------------+
4 rows in set (0.02 sec)
```

## Create a database
```
> create database <db name>
```

## Use a database
```
> use <db name>;
Database changed
```

## Display tables in the database
```
> show tables;
+---------------------------+
| Tables_in_mysql           |
+---------------------------+
| column_stats              |
| columns_priv              |
| db                        |
| event                     |
```
(the rest rest of output truncated for brevity)

## Select records
```
> select * from 
```
## Visual Studio connection string in appSettings.json
```json
  "ConnectionStrings": {
    "MariaDbConnectionString": "server=localhost;port=1234;user id=root;password=password;database=ItemsDatabase",
  }
```


## Create an EF migration
```
dotnet ef migrations add InitialCreate
```

## Apply EF migrations to the database
```
dotnet ef database update
```