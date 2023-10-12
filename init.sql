-- Create a database
CREATE DATABASE my_database;

-- Create an admin user and grant privileges
CREATE USER 'THOMAS'@'%' IDENTIFIED BY 'password';
GRANT ALL PRIVILEGES ON my_database.* TO 'THOMAS'@'%';
FLUSH PRIVILEGES;
