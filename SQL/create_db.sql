-- Create database

-- DROP DATABASE IF EXISTS mentor_db;
-- CREATE DATABASE mentor_db;

-- ######################################

-- DROP SCHEMA log;
-- CREATE SCHEMA log;
-- SET SCHEMA 'log';

--Create tables
CREATE TABLE table_logs(
	id SERIAL PRIMARY KEY,
	date DATE NOT NULL DEFAULT CURRENT_DATE,
	time TIME NOT NULL DEFAULT CURRENT_TIME,
	event_name TEXT NOT NULL,
	table_name TEXT NOT NULL,
	column_name TEXT NULL,
	old_value TEXT NULL,
	new_value TEXT NULL,
	description TEXT NOT NULL
);

-- Create procedures
CREATE PROCEDURE procedure_logger(
	IN event TEXT, 
	IN table_name TEXT, 
	IN description TEXT,
	IN column_name TEXT DEFAULT NULL, 
	IN old_value TEXT DEFAULT NULL, 
	IN new_value TEXT DEFAULT NULL
	)
LANGUAGE sql
BEGIN ATOMIC
	INSERT INTO table_logs (event_name, table_name, column_name, old_value, new_value, description)
	VALUES (event, table_name, column_name, old_value, new_value, description);
END;

--CALL procedure_logger('TEST', 'test', 'test');
--CALL procedure_logger('TEST', 'test', 'test', 'column', 'old', 'new');
--SELECT * FROM table_logs;

-- ######################################

-- DROP SCHEMA test;
-- CREATE SCHEMA test;
-- SET SCHEMA 'test';

-- Create tables
CREATE TABLE table_roles (
    id SERIAL PRIMARY KEY,
    role_name TEXT NOT NULL
);

CREATE TABLE table_specializations (
    id SERIAL PRIMARY KEY,
    specialization_name TEXT NOT NULL
);

CREATE TABLE  table_accounts (
    id SERIAL PRIMARY KEY,
    login TEXT NOT NULL,
    password TEXT NOT NULL,
    role_id INTEGER NOT NULL,
    is_deleted BOOLEAN NOT NULL DEFAULT false,
    FOREIGN KEY (role_id) REFERENCES table_roles(id)
        ON DELETE NO ACTION
        ON UPDATE NO ACTION
);

CREATE TABLE table_persons (
    id SERIAL PRIMARY KEY,
    first_name TEXT NOT NULL,
    last_name TEXT NOT NULL,
    patronymic_name TEXT NOT NULL,
    birthday DATE NOT NULL,
    telegram TEXT NOT NULL,
    email TEXT NOT NULL,
    photo TEXT NOT NULL,
    FOREIGN KEY (id) REFERENCES table_accounts(id)
        ON DELETE NO ACTION
        ON UPDATE NO ACTION
);

CREATE TABLE table_persons_specializations (
    id SERIAL PRIMARY KEY,
    person_id INTEGER NOT NULL,
    specialization_id INTEGER NOT NULL,
    FOREIGN KEY (person_id) REFERENCES table_persons(id)
        ON DELETE NO ACTION
        ON UPDATE NO ACTION,
    FOREIGN KEY (specialization_id) REFERENCES table_specializations(id)
        ON DELETE NO ACTION
        ON UPDATE NO ACTION
);



-- Create triggers
CREATE TRIGGER trigger_insert_log
	AFTER INSERT ON test.table_roles
	FOR EACH ROW
    EXECUTE PROCEDURE log.procedure_logger('INSERT', 'table_roles', 'NEW');

-- CALL log.procedure_logger('INSERT', 'table_roles', 'NEW');
-- SELECT * FROM log.table_logs;
