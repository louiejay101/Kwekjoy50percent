create database KwekjoyDB

use KwekjoyDB

  CREATE TABLE customers (
        customer_id INT IDENTITY(1,1) PRIMARY KEY,
        name VARCHAR(255),
        username VARCHAR(100),
        password VARCHAR(100),
        email VARCHAR(255),
        role VARCHAR(50)
    );

	 CREATE TABLE products (
        product_id INT IDENTITY(1,1) PRIMARY KEY,
        product_name VARCHAR(255),
        price DECIMAL(10, 2)
    );

	 CREATE TABLE customer_items (
		customer_item_id INT IDENTITY(1,1) PRIMARY KEY,
		customer_id INT,
		product_id INT,
		FOREIGN KEY (customer_id) REFERENCES customers(customer_id),
		FOREIGN KEY (product_id) REFERENCES products(product_id)
	);
	ALTER TABLE products
	ADD image_path NVARCHAR(MAX);