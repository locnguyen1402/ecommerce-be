START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240926143759_AddDirectories') THEN
    CREATE TABLE provinces (
        id uuid NOT NULL,
        name character varying(200) NOT NULL DEFAULT (''),
        code character varying(100) NOT NULL DEFAULT (''),
        created_by uuid,
        created_at timestamp with time zone NOT NULL,
        updated_by uuid,
        updated_at timestamp with time zone,
        deleted_by uuid,
        deleted_at timestamp with time zone,
        CONSTRAINT pk_provinces PRIMARY KEY (id)
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240926143759_AddDirectories') THEN
    CREATE TABLE districts (
        id uuid NOT NULL,
        province_id uuid NOT NULL,
        name character varying(200) NOT NULL DEFAULT (''),
        code character varying(100) NOT NULL DEFAULT (''),
        created_by uuid,
        created_at timestamp with time zone NOT NULL,
        updated_by uuid,
        updated_at timestamp with time zone,
        deleted_by uuid,
        deleted_at timestamp with time zone,
        CONSTRAINT pk_districts PRIMARY KEY (id),
        CONSTRAINT fk_districts_provinces_province_id FOREIGN KEY (province_id) REFERENCES provinces (id) ON DELETE RESTRICT
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240926143759_AddDirectories') THEN
    CREATE TABLE wards (
        id uuid NOT NULL,
        district_id uuid NOT NULL,
        name character varying(200) NOT NULL DEFAULT (''),
        code character varying(100) NOT NULL DEFAULT (''),
        zip_code character varying(100) DEFAULT (''),
        created_by uuid,
        created_at timestamp with time zone NOT NULL,
        updated_by uuid,
        updated_at timestamp with time zone,
        deleted_by uuid,
        deleted_at timestamp with time zone,
        CONSTRAINT pk_wards PRIMARY KEY (id),
        CONSTRAINT fk_wards_districts_district_id FOREIGN KEY (district_id) REFERENCES districts (id) ON DELETE RESTRICT
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240926143759_AddDirectories') THEN
    CREATE INDEX ix_districts_province_id ON districts (province_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240926143759_AddDirectories') THEN
    CREATE INDEX ix_wards_district_id ON wards (district_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240926143759_AddDirectories') THEN
    INSERT INTO "__EFMigrationsHistory" (migration_id, product_version)
    VALUES ('20240926143759_AddDirectories', '8.0.1');
    END IF;
END $EF$;
COMMIT;

