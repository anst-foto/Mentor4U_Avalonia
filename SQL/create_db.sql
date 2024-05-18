-- Create database

-- DROP DATABASE IF EXISTS mentor_db;
-- CREATE DATABASE mentor_db;

-- ######################################

-- DROP SCHEMA log;
-- CREATE SCHEMA log;
-- SET SCHEMA 'log';

--Create tables
/*CREATE TABLE table_ddl_log
(
    id           SERIAL    NOT NULL PRIMARY KEY,
    date         TIMESTAMP NOT NULL,
    class_id     INT       NULL,
    obj_id       INT       NULL,
    obj_sub_id   INT       NULL,
    client_addr  inet      NULL,
    user_name    TEXT      NULL,
    event_type   TEXT      NULL,
    object_type  TEXT      NULL,
    schema_name  TEXT      NULL,
    object_name  TEXT      NULL,
    command      TEXT      NULL,
    command_tag  TEXT      NULL,
    command_text TEXT      NULL
);*/

CREATE TYPE dml_type AS ENUM ('INSERT', 'UPDATE', 'DELETE');
CREATE TABLE table_dml_log
(
    id             BIGSERIAL NOT NULL PRIMARY KEY,
    table_name     TEXT      NOT NULL,
    old_row_data   jsonb,
    new_row_data   jsonb,
    dml_type       dml_type  NOT NULL,
    dml_timestamp  TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    dml_created_by TEXT      NOT NULL DEFAULT current_user
);

-- Create functions
/*CREATE FUNCTION functions_ddl_log()
    RETURNS event_trigger
    LANGUAGE plpgsql
AS
$BODY$
DECLARE
    _command_text TEXT         = current_query();   -- Текст SQL инструкции
    _date         TIMESTAMP    = now();             -- Текущая дата
    _client_addr  inet         = inet_client_addr(); -- IP адрес клиента, с которого запущена SQL инструкция
    _user_name    TEXT         = current_user;      -- Имя пользователя, от которого запущена SQL инструкция
    _tg_event     TEXT         = TG_event;          -- Имя события
    _tg_tag       TEXT         = TG_tag;            -- SQL команда

BEGIN
    IF _tg_event = 'sql_drop'
    THEN
        INSERT INTO log.table_ddl_log (id, date, class_id, obj_id, obj_sub_id,
                                   client_addr, user_name,
                                   event_type, object_type, schema_name, object_name,
                                   command, command_tag, command_text)
        SELECT _date,
               D.classid,
               D.objid,
               D.objsubid,
               _client_addr,
               _user_name,
               _tg_event,
               D.object_type,
               D.schema_name,
               D.object_identity,
               _tg_tag,
               NULL,
               _command_text
        FROM pg_event_trigger_dropped_objects() D
        WHERE D.schema_name NOT IN ('pg_temp', 'pg_toast');
    ELSE
        INSERT INTO log.table_ddl_log (id, date, class_id, obj_id, obj_sub_id,
                                   client_addr, user_name,
                                   event_type, object_type, schema_name, object_name,
                                   command, command_tag, command_text)
        SELECT _date,
               D.classid,
               D.objid,
               D.objsubid,
               _client_addr,
               _user_name,
               _tg_event,
               D.object_type,
               D.schema_name,
               D.object_identity,
               _tg_tag,
               D.command_tag,
               _command_text
        FROM pg_event_trigger_dropped_objects()
        WHERE D.schema_name NOT IN ('pg_temp', 'pg_toast');
    END IF;
END;
$BODY$;*/

CREATE OR REPLACE PROCEDURE log.procedure_insert_log(
    IN _table_name TEXT,
    IN _old_row_data jsonb,
    IN _new_row_data jsonb,
    IN _dml_type dml_type)
    LANGUAGE SQL
BEGIN
    ATOMIC
    INSERT INTO log.table_dml_log (table_name, old_row_data, new_row_data, dml_type)
    VALUES (_table_name, _old_row_data, _new_row_data, _dml_type);
END;

CREATE OR REPLACE FUNCTION log.functions_dml_log()
    RETURNS trigger
    LANGUAGE plpgsql AS
$$
BEGIN
    IF (TG_OP = 'INSERT') THEN
        CALL log.procedure_insert_log(tg_table_name, null, to_jsonb(NEW), 'INSERT');
        RETURN NEW;
    ELSEIF (TG_OP = 'UPDATE') THEN
        CALL log.procedure_insert_log(tg_table_name, to_jsonb(OLD), to_jsonb(NEW), 'UPDATE');
        RETURN NEW;
    ELSEIF (TG_OP = 'DELETE') THEN
        CALL log.procedure_insert_log(tg_table_name, to_jsonb(OLD), null, 'DELETE');
        RETURN OLD;
    END IF;
END;
$$;

-- ######################################

-- DROP SCHEMA test;
-- CREATE SCHEMA test;
-- SET SCHEMA 'test';

-- Create ddl triggers
/*CREATE EVENT TRIGGER trigger_ddl_log ON ddl_command_end
EXECUTE FUNCTION log.functions_ddl_log();

CREATE EVENT TRIGGER trigger_sql_drop_log ON sql_drop
EXECUTE FUNCTION log.functions_ddl_log();*/

-- Create tables
CREATE TABLE table_roles
(
    id        SERIAL PRIMARY KEY,
    role_name TEXT NOT NULL
);

CREATE TABLE table_specializations
(
    id                  SERIAL PRIMARY KEY,
    specialization_name TEXT NOT NULL
);

CREATE TABLE table_accounts
(
    id         SERIAL PRIMARY KEY,
    login      TEXT    NOT NULL,
    password   TEXT    NOT NULL,
    role_id    INTEGER NOT NULL,
    is_deleted BOOLEAN NOT NULL DEFAULT false,
    FOREIGN KEY (role_id) REFERENCES table_roles (id)
        ON DELETE NO ACTION
        ON UPDATE NO ACTION
);

CREATE TABLE table_persons
(
    id              SERIAL PRIMARY KEY,
    first_name      TEXT NOT NULL,
    last_name       TEXT NOT NULL,
    patronymic_name TEXT NOT NULL,
    birthday        DATE NOT NULL,
    telegram        TEXT NOT NULL,
    email           TEXT NOT NULL,
    photo           TEXT NOT NULL,
    FOREIGN KEY (id) REFERENCES table_accounts (id)
        ON DELETE NO ACTION
        ON UPDATE NO ACTION
);

CREATE TABLE table_persons_specializations
(
    id                SERIAL PRIMARY KEY,
    person_id         INTEGER NOT NULL,
    specialization_id INTEGER NOT NULL,
    FOREIGN KEY (person_id) REFERENCES table_persons (id)
        ON DELETE NO ACTION
        ON UPDATE NO ACTION,
    FOREIGN KEY (specialization_id) REFERENCES table_specializations (id)
        ON DELETE NO ACTION
        ON UPDATE NO ACTION
);

-- Create triggers
CREATE TRIGGER trigger_dml_log_for_table_roles
    AFTER INSERT OR
        UPDATE OR
        DELETE
    ON table_roles
    FOR EACH ROW
EXECUTE FUNCTION log.functions_dml_log();

CREATE TRIGGER trigger_dml_log_for_table_specializations
    AFTER INSERT OR
        UPDATE OR
        DELETE
    ON table_specializations
    FOR EACH ROW
EXECUTE FUNCTION log.functions_dml_log();

CREATE TRIGGER trigger_dml_log_for_table_accounts
    AFTER INSERT OR
        UPDATE OR
        DELETE
    ON table_accounts
    FOR EACH ROW
EXECUTE FUNCTION log.functions_dml_log();

CREATE TRIGGER trigger_dml_log_for_table_persons
    AFTER INSERT OR
        UPDATE OR
        DELETE
    ON table_persons
    FOR EACH ROW
EXECUTE FUNCTION log.functions_dml_log();

CREATE TRIGGER trigger_dml_log_for_table_persons_specializations
    AFTER INSERT OR
        UPDATE OR
        DELETE
    ON table_persons_specializations
    FOR EACH ROW
EXECUTE FUNCTION log.functions_dml_log();

-- Create views
CREATE OR REPLACE VIEW view_persons AS
SELECT table_accounts.id     AS id,
       login,
       password,
       table_roles.role_name AS role,
       last_name,
       first_name,
       patronymic_name,
       birthday,
       telegram,
       email,
       photo
FROM table_accounts
         JOIN table_persons
              ON table_accounts.id = table_persons.id
         JOIN table_roles
              ON table_accounts.role_id = table_roles.id;
-- SELECT * FROM view_persons;

-- Create functions
CREATE OR REPLACE FUNCTION function_check_login(IN _login TEXT)
    RETURNS BOOLEAN
    LANGUAGE plpgsql AS
$$
BEGIN
    RETURN EXISTS (SELECT * FROM table_accounts WHERE login = _login);
END;
$$;

CREATE OR REPLACE FUNCTION function_check_person(
    IN _last_name TEXT, IN _first_name TEXT, IN _patronymic_name TEXT, IN _birthday DATE)
    RETURNS BOOLEAN
    LANGUAGE plpgsql AS
$$
BEGIN
    RETURN (SELECT COUNT(*)
            FROM table_persons
            WHERE last_name = _last_name
              AND first_name = _first_name
              AND patronymic_name = _patronymic_name
              AND birthday = _birthday) > 0;
END;
$$;

-- Create procedures
CREATE OR REPLACE PROCEDURE procedure_insert_person(
    IN _login TEXT, IN _password TEXT, IN _role_name TEXT,
    IN _last_name TEXT, IN _first_name TEXT, IN _patronymic_name TEXT,
    IN _birthday DATE, IN _telegram TEXT, IN _email TEXT, IN _photo TEXT)
    LANGUAGE plpgsql AS
$$
BEGIN
    IF function_check_login(_login)
    THEN
        ROLLBACK;
    ELSE
        INSERT INTO table_accounts (login, password, role_id)
        VALUES (_login, _password, (SELECT id FROM table_roles WHERE role_name = _role_name));

        IF function_check_person(_last_name, _first_name, _patronymic_name, _birthday)
        THEN
            ROLLBACK;
        ELSE
            INSERT INTO table_persons (last_name, first_name, patronymic_name, birthday, telegram, email, photo)
            VALUES (_last_name, _first_name, _patronymic_name, _birthday, _telegram, _email, _photo);

            IF (SELECT id FROM table_accounts WHERE login = _login) != (SELECT id
                                                                        FROM table_persons
                                                                        WHERE last_name = _last_name
                                                                          AND first_name = _first_name
                                                                          AND patronymic_name = _patronymic_name
                                                                          AND birthday = _birthday)
            THEN
                ROLLBACK;
            END IF;

            COMMIT;
        END IF;
    END IF;
END;
$$;

-- Insert test data
INSERT INTO table_roles (role_name)
VALUES ('admin');
INSERT INTO table_roles (role_name)
VALUES ('mentor');
INSERT INTO table_roles (role_name)
VALUES ('student');

CALL procedure_insert_person('anst', '1234', 'admin', 'Иванов', 'Иван', 'Иванович', '1989-01-01', 'test_telegram',
                             '<EMAIL>', 'test_photo');
CALL procedure_insert_person('admin', '1234', 'admin', 'Иванов', 'Иван', 'Иванович', '1989-01-01', 'test_telegram',
                             '<EMAIL>', 'test_photo');
CALL procedure_insert_person('anst', '1234', 'admin', 'Петров', 'Иван', 'Иванович', '1989-01-01', 'test_telegram',
                             '<EMAIL>', 'test_photo');
INSERT INTO table_persons (last_name, first_name, patronymic_name, birthday, telegram, email, photo)
VALUES ('Иванов', 'Иван', 'Иванович', '1989-01-01', 'test_telegram', '<EMAIL>', 'test_photo');
CALL procedure_insert_person('user', '1234', 'admin', 'Сидоров', 'Иван', 'Иванович', '1989-01-01', 'test_telegram',
                             '<EMAIL>', 'test_photo');
