CREATE DATABASE creator_demo;
use creator_demo;
CREATE TABLE users (
    user_id VARCHAR(36),
    username VARCHAR(255) NOT NULL,
    password VARCHAR(255) NOT NULL,
    role ENUM('admin', 'user') NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);
CREATE TABLE customers (
    customer_id VARCHAR(36),
    name VARCHAR(255) NOT NULL,
    email VARCHAR(255),
    phone VARCHAR(15),
    address VARCHAR(255),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);
CREATE TABLE suppliers (
    supplier_id VARCHAR(36),
    name VARCHAR(255) NOT NULL,
    contact_person VARCHAR(255),
    email VARCHAR(255),
    phone VARCHAR(15),
    address VARCHAR(255),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);
CREATE TABLE invoices (
    invoice_id VARCHAR(36),
    invoice_number INT UNIQUE NOT NULL,
    customer_id VARCHAR(36),
    supplier_id VARCHAR(36),
    quantity int,
    unit_price int
    total_amount DECIMAL(10, 2) NOT NULL,
    type ENUM('sale', 'purchase', 'sale_return', 'purchase_return') NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);
CREATE TABLE stocks (
    stock_id VARCHAR(36),
    product_name VARCHAR(255) NOT NULL,
    quantity INT NOT NULL,
    unit_price DECIMAL(10, 2) NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    supplier_id CHAR(36),FOREIGN KEY (supplier_id) REFERENCES suppliers(supplier_id) ON DELETE CASCADE
);
insert into users (user_id,username,password,type) VALUE ("1","root","admin","admin");
ALTER TABLE stocks DROP  COLUMN sale_price;
DELETE FROM customers WHERE name IS NULL;
