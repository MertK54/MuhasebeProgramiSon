CREATE DATABASE creator_demo;
use creator_demo;
CREATE TABLE users (
    user_id VARCHAR(36),
    username VARCHAR(255) NOT NULL,
    password VARCHAR(255) NOT NULL,
    type ENUM('admin', 'user') NOT NULL,
    token CHAR(36)
);
CREATE TABLE customers (
    customer_id VARCHAR(36),
    name VARCHAR(255) NOT NULL,
    e_mail VARCHAR(255),
    phone_number VARCHAR(15),
    adress VARCHAR(255),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);
CREATE TABLE suppliers (
    supplier_id VARCHAR(36),
    name VARCHAR(255) NOT NULL,
    e_mail VARCHAR(255),
    phone_number VARCHAR(15),
    address VARCHAR(255),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);
CREATE TABLE invoices (
    invoice_id VARCHAR(36),
    invoice_number INT UNIQUE NOT NULL,
    customer_id VARCHAR(36),
    supplier_id VARCHAR(36),
    stock_id VARCHAR(45),
    product_name VARCHAR(45),
    supplier_name VARCHAR(45),
    customer_name VARCHAR(45),
    quantity int,
    unit_price DECIMAL(10,2),
    total_amount DECIMAL(10, 2) NOT NULL,
    payment_method VARCHAR(20),
    invoice_statu VARCHAR(30),
    type VARCHAR(20) NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
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
