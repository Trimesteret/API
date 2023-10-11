# Use the official MySQL image
FROM mysql:8.0.31

# Set the MySQL root password
ENV MYSQL_ROOT_PASSWORD my-secret-pw

# Expose port 3306
EXPOSE 3306

# Mount the MySQL data directory to the specified location
VOLUME /var/lib/mysql /var/lib/docker/volumes/0e2bcdd43ad56c8b1ba1d401b2264598894c354fa40e32903f0741b6bc26de4c/_data

# Custom SQL script to create a database and an admin user
ADD init.sql /docker-entrypoint-initdb.d/

# Grant permission to run the custom SQL script
RUN chmod 0444 /docker-entrypoint-initdb.d/init.sql
