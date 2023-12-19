# API
## 1.
To build the sql database input to terminal: 
  docker build -t mysql_server .                   
  docker run -d -p 3306:3306 --name mysql_container mysql_server
Then you should be able to see it succesfully running 
<img width="1269" alt="Screenshot 2023-10-11 at 10 57 46" src="https://github.com/Trimesteret/API/assets/55490372/cf0c7ad8-5478-4e92-a852-1cd32275b51b">
<br><br>
## 2.
You then have to update the database, with a migration. There fortunatly already exists an initialization migration, you just have to update the database with it.
You do this in rider by going into the tab Tools -> Entity Framework Core -> Update Database.
<img width="1440" alt="Screenshot 2023-10-11 at 11 16 11" src="https://github.com/Trimesteret/API/assets/55490372/7e949a5e-43fb-4fce-825e-c160e7b1fce6">
<br>
Then when inside you choose initial and click ok.
<img width="751" alt="Screenshot 2023-10-11 at 11 17 25" src="https://github.com/Trimesteret/API/assets/55490372/9f3eb423-48c1-47a0-a7be-b9854c6d76cb">
<br><br>
## 3.
Finally you should be able to click play and be entered into Swagger UI where you can try some of the endpoints of our API so far.
<img width="395" alt="Screenshot 2023-10-11 at 11 21 17" src="https://github.com/Trimesteret/API/assets/55490372/d2cfec9b-900c-4d15-9d37-6e8fdae96440">
<br><br>
