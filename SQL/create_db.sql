-- Create database

-- DROP DATABASE IF EXISTS mentor_db;
-- CREATE DATABASE mentor_db;

-- DROP SCHEMA test;
-- CREATE SCHEMA test;

-- ######################################

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