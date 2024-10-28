DELIMITER //
CREATE PROCEDURE sp_login_user(IN $username VARCHAR(20),IN $password VARCHAR(36))
BEGIN
	DECLARE user_count INT;
    DECLARE localUser_id  VARCHAR(36);
    DECLARE $token CHAR(36) default '';
	SELECT user_id INTO localUser_id
    FROM users 
    WHERE username = $username AND password = $password;
    SELECT COUNT(*) INTO user_count 
    FROM users 
    WHERE username = $username AND password = $password;
    IF user_count > 0 THEN
		SET $token = UUID();
        UPDATE users SET token = $token WHERE user_id = localUser_id;
	ELSE
		SET $token = '';
	END IF;
    SELECT $token;
END //
DELIMITER 
SHOW PROCEDURE STATUS WHERE Name = 'sp_logout_user';

DELIMITER //
CREATE PROCEDURE sp_logout_user(IN $token VARCHAR(36))
BEGIN
	DECLARE user_count INT;
    DECLARE $p_token CHAR(36) default '';
    SELECT COUNT(*) INTO user_count 
    FROM users 
    WHERE token = $token;
    IF user_count > 0 THEN
        UPDATE users SET token = '' WHERE token = $token ;
        SET $p_token = UUID();
	ELSE
        SET $p_token = '';
	END IF;
    SELECT $p_token as result ;
END //
DELIMITER 

DELIMITER //
CREATE PROCEDURE sp_create_customer(IN $name VARCHAR(50),IN $e_mail VARCHAR(50),IN $phone_number VARCHAR(13), IN $adress VARCHAR(255))
BEGIN
	DECLARE user_count INT;
    DECLARE procedure_token CHAR(36) default '';
    SELECT COUNT(*) INTO user_count
    FROM customers
    WHERE email = $e_mail AND name = $name;
    IF user_count = 0 THEN
		INSERT INTO customers (customer_id,name,e_mail,phone_number,adress) VALUES (UUID(),$name,$e_mail,$phone_number,$adress);
        SET procedure_token = UUID();
	ELSE
		SET procedure_token = '';
	END IF;
    SELECT procedure_token AS result;
END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE sp_get_customer()
BEGIN 
	SELECT * FROM customers;
END//
DELIMITER ;


DELIMITER //
CREATE PROCEDURE sp_update_customer(IN $customer_id  VARCHAR(36),IN $name  VARCHAR(50),IN $e_mail  VARCHAR(50),IN $phone_number  VARCHAR(15), IN $adress  VARCHAR(255),IN $created_at VARCHAR(20))
BEGIN

		DECLARE user_count INT;
        DECLARE procedure_token CHAR(36) default '';
		SELECT COUNT(*) INTO user_count
        FROM customers
        WHERE customer_id = $customer_id;
        IF user_count > 0 THEN
			UPDATE customers 
            SET name = $name, e_mail = $e_mail, phone_number = $phone_number, adress = $adress,created_at = $created_at
            WHERE customer_id = $customer_id;
            SET procedure_token = UUID();
		ELSE
			SET procedure_token = '';
		END IF;
        SELECT procedure_token AS token;
END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE sp_customer_delete(IN $customer_id VARCHAR(36))
BEGIN
	DECLARE user_count INT;
    DECLARE procedure_token CHAR(36) default '';
    SELECT COUNT(*) INTO user_count
    FROM customers
    WHERE customer_id = $customer_id;
    IF user_count > 0 THEN
		DELETE FROM customers WHERE customer_id = $customer_id;
        SET procedure_token = UUID();
	ELSE
		SET procedure_token = '';
	END IF;
    SELECT procedure_token AS result;
END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE sp_supplier_create(IN $name VARCHAR(50), IN $e_mail VARCHAR(50), IN $phone_number VARCHAR(15),IN $adress VARCHAR(255))
BEGIN
	DECLARE user_count INT;
    DECLARE procedure_token CHAR(36) default '';
    SELECT COUNT(*) INTO user_count
    FROM suppliers
    WHERE e_mail = $e_mail;
    IF user_count = 0 THEN
		INSERT INTO suppliers (supplier_id,name,e_mail,phone_number,adress) VALUE (UUID(),$name,$e_mail,$phone_number,$adress);
        SET procedure_token = UUID();
	ELSE
		SET procedure_token = '';
	END IF;
    SELECT procedure_token AS result;
END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE sp_supplier_get()
BEGIN
	SELECT * FROM suppliers;
END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE sp_supplier_update(IN $supplier_id VARCHAR(255),IN $name VARCHAR(255),IN $phone_number VARCHAR(255),IN $e_mail VARCHAR(255),IN $adress VARCHAR(255))
BEGIN
	DECLARE number_supplier INT;
    DECLARE procedure_token CHAR(36) default '';
    SELECT COUNT(*) INTO number_supplier
    FROM suppliers
    WHERE supplier_id = $supplier_id;
    IF number_supplier > 0 THEN
		UPDATE suppliers 
        SET name=$name,phone_number=$phone_number,e_mail=$e_mail,adress=$adress
        WHERE supplier_id = $supplier_id;
		SET procedure_token = UUID();
	ELSE
		SET procedure_token = '';
    END IF;
    SELECT procedure_token as result;
END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE sp_supplier_delete (IN $supplier_id VARCHAR(255))
BEGIN
    DECLARE intID CHAR(36) DEFAULT '';
    DELETE FROM suppliers WHERE supplier_id = $supplier_id;
    SET intID = UUID();
    SELECT intID as result;
END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE sp_stock_create(IN $product_name VARCHAR(50),IN $quantity int ,IN $unit_price decimal(10,2),IN $supplier_id VARCHAR(36))
BEGIN
	DECLARE number_stock int;
    DECLARE procedure_token CHAR(36) default '';
    SELECT COUNT(*) into number_stock
    FROM stocks
    WHERE product_name = $product_name;
    IF number_stock = 0 THEN
		INSERT INTO stocks (stock_id,product_name,quantity,unit_price,supplier_id) VALUES(UUID(),$product_name,$quantity,$unit_price,$supplier_id);
		SET procedure_token = UUID();
	ELSE
		SET procedure_token = '';
    END IF;
			SELECT procedure_token AS result;
END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE sp_stock_get()
BEGIN
    SELECT 
		stocks.stock_id,
        stocks.supplier_id,
        suppliers.name AS supplier_name,
        stocks.product_name,
        stocks.quantity,
        stocks.unit_price
    FROM 
        stocks
    JOIN 
        suppliers ON stocks.supplier_id = suppliers.id;
END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE sp_stock_update (in $stock_id VARCHAR(36),in $quantity int, in $unit_price DECIMAL(10,2))
BEGIN
        DECLARE number_stock int;
		DECLARE procedure_token CHAR(36) default '';
		SELECT COUNT(*) into number_stock
			FROM stocks
			WHERE stock_id = $stock_id;
		IF number_stock > 0 THEN
			UPDATE stocks
				SET quantity=$quantity, unit_price = $unit_price
                WHERE stock_id = $stock_id;
                SET procedure_token = UUID();
		ELSE
			SET procedure_token = '';
		END IF;
        SELECT procedure_token AS result;
END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE sp_stock_delete(in $stock_id VARCHAR(36))
BEGIN
    DECLARE procedure_token CHAR(36) DEFAULT '';
	DELETE FROM stocks 
    WHERE stock_id = $stock_id;
    DELETE FROM stocks WHERE stock_id = $stock_id;
    SET procedure_token = UUID();
    SELECT procedure_token as result;
END //
DELIMITER ;
use creator_demo;
DELIMITER //
CREATE PROCEDURE sp_invoice_create_sale(
    IN supplier_id VARCHAR(36),
    IN customer_id VARCHAR(36),
    IN product_name VARCHAR(50),
    IN quantity INT,
    IN unit_price DECIMAL(10,2),
    IN type ENUM('sale', 'purchase', 'sale_return', 'purchase_return')
)
BEGIN
    DECLARE new_invoice_number INT DEFAULT 0;
    SELECT COALESCE(MAX(invoice_number), 0) + 1 INTO new_invoice_number FROM invoices;
    INSERT INTO invoices (invoice_id,invoice_number, supplier_id, customer_id, product_name, quantity, unit_price,type) 
    VALUES (UUID(),new_invoice_number, supplier_id, customer_id, product_name, quantity, unit_price,type);
END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE sp_invoice_create_purchase(
    IN supplier_id VARCHAR(36),
    IN customer_id VARCHAR(36),
    IN product_name VARCHAR(50),
    IN quantity INT,
    IN unit_price DECIMAL(10,2),
    IN type ENUM('sale', 'purchase', 'sale_return', 'purchase_return')
)
BEGIN
    DECLARE new_invoice_number INT DEFAULT 0;
    SELECT COALESCE(MAX(invoice_number), 0) + 1 INTO new_invoice_number FROM invoices;
    INSERT INTO invoices (invoice_id,invoice_number, supplier_id, customer_id, product_name, quantity, unit_price,type) 
    VALUES (UUID(),new_invoice_number, supplier_id, customer_id, product_name, quantity, unit_price,type);
END //
DELIMITER ;