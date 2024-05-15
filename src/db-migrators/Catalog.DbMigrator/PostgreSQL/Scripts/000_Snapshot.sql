CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    migration_id character varying(150) NOT NULL,
    product_version character varying(32) NOT NULL,
    CONSTRAINT pk___ef_migrations_history PRIMARY KEY (migration_id)
);

START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240219030622_Initialize') THEN
    CREATE TABLE districts (
        name text NOT NULL,
        code text,
        CONSTRAINT pk_districts PRIMARY KEY (name)
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240219030622_Initialize') THEN
    CREATE TABLE provinces (
        name text NOT NULL,
        code text,
        CONSTRAINT pk_provinces PRIMARY KEY (name)
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240219030622_Initialize') THEN
    CREATE TABLE wards (
        name text NOT NULL,
        code text,
        CONSTRAINT pk_wards PRIMARY KEY (name)
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240219030622_Initialize') THEN
    INSERT INTO "__EFMigrationsHistory" (migration_id, product_version)
    VALUES ('20240219030622_Initialize', '8.0.1');
    END IF;
END $EF$;
COMMIT;

