﻿CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    migration_id character varying(150) NOT NULL,
    product_version character varying(32) NOT NULL,
    CONSTRAINT pk___ef_migrations_history PRIMARY KEY (migration_id)
);

START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240921134333_InitializeIdentity') THEN
    CREATE EXTENSION IF NOT EXISTS unaccent;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240921134333_InitializeIdentity') THEN
    CREATE TABLE applications (
        id uuid NOT NULL,
        application_type character varying(50) DEFAULT (''),
        client_id character varying(100) NOT NULL,
        client_secret character varying(500) DEFAULT (''),
        client_type character varying(50) DEFAULT (''),
        concurrency_token character varying(50),
        consent_type character varying(50) DEFAULT (''),
        display_name character varying(200) DEFAULT (''),
        display_names character varying(2000) DEFAULT ('{}'),
        json_web_key_set text DEFAULT ('{}'),
        permissions character varying(2000) DEFAULT ('[]'),
        post_logout_redirect_uris text DEFAULT ('[]'),
        properties text DEFAULT ('{}'),
        redirect_uris text DEFAULT ('[]'),
        requirements character varying(2000) DEFAULT ('[]'),
        settings text DEFAULT ('{}'),
        CONSTRAINT pk_applications PRIMARY KEY (id)
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240921134333_InitializeIdentity') THEN
    CREATE TABLE client_roles (
        role_name text NOT NULL,
        client_id text NOT NULL,
        id uuid NOT NULL,
        CONSTRAINT pk_client_roles PRIMARY KEY (client_id, role_name)
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240921134333_InitializeIdentity') THEN
    CREATE TABLE data_protection_keys (
        id integer GENERATED BY DEFAULT AS IDENTITY,
        friendly_name text,
        xml text,
        CONSTRAINT pk_data_protection_keys PRIMARY KEY (id)
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240921134333_InitializeIdentity') THEN
    CREATE TABLE permission_groups (
        id uuid NOT NULL,
        name character varying(100) NOT NULL DEFAULT (''),
        description character varying(200) DEFAULT (''),
        created_by uuid,
        created_at timestamp with time zone NOT NULL,
        updated_by uuid,
        updated_at timestamp with time zone,
        CONSTRAINT pk_permission_groups PRIMARY KEY (id)
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240921134333_InitializeIdentity') THEN
    CREATE TABLE roles (
        id uuid NOT NULL,
        predefined boolean NOT NULL,
        status smallint NOT NULL,
        description character varying(200) DEFAULT (''),
        permissions text[] NOT NULL,
        enabled boolean NOT NULL,
        created_by uuid,
        created_at timestamp with time zone NOT NULL,
        updated_by uuid,
        updated_at timestamp with time zone,
        name character varying(50) NOT NULL DEFAULT (''),
        normalized_name character varying(50) NOT NULL DEFAULT (''),
        concurrency_stamp text,
        CONSTRAINT pk_roles PRIMARY KEY (id)
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240921134333_InitializeIdentity') THEN
    CREATE TABLE scopes (
        id uuid NOT NULL,
        concurrency_token character varying(50),
        description character varying(200) DEFAULT (''),
        descriptions character varying(2000) DEFAULT ('{}'),
        display_name character varying(200) DEFAULT (''),
        display_names character varying(2000) DEFAULT ('{}'),
        name character varying(100) DEFAULT (''),
        properties text DEFAULT ('{}'),
        resources character varying(2000) DEFAULT ('[]'),
        CONSTRAINT pk_scopes PRIMARY KEY (id)
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240921134333_InitializeIdentity') THEN
    CREATE TABLE users (
        id uuid NOT NULL,
        first_name character varying(50) DEFAULT (''),
        last_name character varying(50) DEFAULT (''),
        full_name character varying(100) DEFAULT (''),
        gender character varying(20) NOT NULL DEFAULT ('UNSPECIFIED'),
        birth_date DATE,
        picture character varying(500) DEFAULT (''),
        status character varying(50) NOT NULL DEFAULT ('NEW'),
        created_by uuid,
        created_at timestamp with time zone NOT NULL,
        updated_by uuid,
        updated_at timestamp with time zone,
        user_name character varying(50) NOT NULL,
        normalized_user_name character varying(50) NOT NULL,
        email character varying(50) DEFAULT (''),
        normalized_email character varying(50) DEFAULT (''),
        email_confirmed boolean NOT NULL,
        password_hash character varying(500),
        security_stamp character varying(50) DEFAULT (''),
        concurrency_stamp character varying(50),
        phone_number character varying(50) DEFAULT (''),
        phone_number_confirmed boolean NOT NULL,
        two_factor_enabled boolean NOT NULL,
        lockout_end timestamp with time zone,
        lockout_enabled boolean NOT NULL,
        access_failed_count integer NOT NULL,
        CONSTRAINT pk_users PRIMARY KEY (id)
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240921134333_InitializeIdentity') THEN
    CREATE TABLE authorizations (
        id uuid NOT NULL,
        application_id uuid,
        concurrency_token character varying(50),
        created_at timestamp with time zone,
        properties text DEFAULT ('{}'),
        scopes character varying(2000) DEFAULT ('[]'),
        status character varying(50) DEFAULT (''),
        subject character varying(200) DEFAULT (''),
        type character varying(50) DEFAULT (''),
        CONSTRAINT pk_authorizations PRIMARY KEY (id),
        CONSTRAINT fk_authorizations_applications_application_id FOREIGN KEY (application_id) REFERENCES applications (id)
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240921134333_InitializeIdentity') THEN
    CREATE TABLE permissions (
        id uuid NOT NULL,
        group_id uuid NOT NULL,
        value character varying(100) NOT NULL DEFAULT (''),
        name character varying(100) NOT NULL DEFAULT (''),
        description character varying(200) DEFAULT (''),
        created_by uuid,
        created_at timestamp with time zone NOT NULL,
        CONSTRAINT pk_permissions PRIMARY KEY (id),
        CONSTRAINT fk_permissions_permission_groups_group_id FOREIGN KEY (group_id) REFERENCES permission_groups (id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240921134333_InitializeIdentity') THEN
    CREATE TABLE role_claims (
        id integer GENERATED BY DEFAULT AS IDENTITY,
        role_id uuid NOT NULL,
        claim_type character varying(200) DEFAULT (''),
        claim_value character varying(200) DEFAULT (''),
        CONSTRAINT pk_role_claims PRIMARY KEY (id),
        CONSTRAINT fk_role_claims_roles_role_id FOREIGN KEY (role_id) REFERENCES roles (id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240921134333_InitializeIdentity') THEN
    CREATE TABLE security_events (
        id uuid NOT NULL,
        ip character varying(50),
        agent character varying(500),
        culture character varying(10),
        protocol character varying(10),
        schema character varying(10),
        origin character varying(50),
        uri character varying(500),
        method character varying(10),
        correlation_id character varying(100),
        application_id uuid,
        application_name character varying(100),
        tenant_id uuid,
        tenant_name character varying(100),
        user_id uuid NOT NULL,
        user_name character varying(50),
        start_time timestamp with time zone NOT NULL,
        duration integer NOT NULL,
        end_time timestamp with time zone,
        status_code integer,
        exception character varying(2000),
        old_values jsonb,
        new_values jsonb,
        remarks character varying(500),
        action character varying(50) NOT NULL DEFAULT ('OTHER'),
        created_by uuid,
        created_at timestamp with time zone NOT NULL,
        CONSTRAINT pk_security_events PRIMARY KEY (id),
        CONSTRAINT fk_security_events_users_user_id FOREIGN KEY (user_id) REFERENCES users (id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240921134333_InitializeIdentity') THEN
    CREATE TABLE user_claims (
        id integer GENERATED BY DEFAULT AS IDENTITY,
        user_id uuid NOT NULL,
        claim_type character varying(200) DEFAULT (''),
        claim_value character varying(200) DEFAULT (''),
        CONSTRAINT pk_user_claims PRIMARY KEY (id),
        CONSTRAINT fk_user_claims_users_user_id FOREIGN KEY (user_id) REFERENCES users (id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240921134333_InitializeIdentity') THEN
    CREATE TABLE user_logins (
        login_provider character varying(100) NOT NULL DEFAULT (''),
        provider_key character varying(50) NOT NULL DEFAULT (''),
        provider_display_name character varying(200) DEFAULT (''),
        user_id uuid NOT NULL,
        CONSTRAINT pk_user_logins PRIMARY KEY (login_provider, provider_key),
        CONSTRAINT fk_user_logins_users_user_id FOREIGN KEY (user_id) REFERENCES users (id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240921134333_InitializeIdentity') THEN
    CREATE TABLE user_roles (
        user_id uuid NOT NULL,
        role_id uuid NOT NULL,
        CONSTRAINT pk_user_roles PRIMARY KEY (user_id, role_id),
        CONSTRAINT fk_user_roles_roles_role_id FOREIGN KEY (role_id) REFERENCES roles (id) ON DELETE CASCADE,
        CONSTRAINT fk_user_roles_users_user_id FOREIGN KEY (user_id) REFERENCES users (id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240921134333_InitializeIdentity') THEN
    CREATE TABLE user_tokens (
        user_id uuid NOT NULL,
        login_provider character varying(100) NOT NULL DEFAULT (''),
        name character varying(50) NOT NULL DEFAULT (''),
        value character varying(500) DEFAULT (''),
        CONSTRAINT pk_user_tokens PRIMARY KEY (user_id, login_provider, name),
        CONSTRAINT fk_user_tokens_users_user_id FOREIGN KEY (user_id) REFERENCES users (id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240921134333_InitializeIdentity') THEN
    CREATE TABLE tokens (
        id uuid NOT NULL,
        application_id uuid,
        authorization_id uuid,
        concurrency_token character varying(50),
        created_at timestamp with time zone,
        expired_at timestamp with time zone,
        payload text DEFAULT (''),
        properties text DEFAULT ('{}'),
        redeemed_at timestamp with time zone,
        reference_id character varying(100),
        status character varying(50) DEFAULT (''),
        subject character varying(200) DEFAULT (''),
        type character varying(50) DEFAULT (''),
        CONSTRAINT pk_tokens PRIMARY KEY (id),
        CONSTRAINT fk_tokens_applications_application_id FOREIGN KEY (application_id) REFERENCES applications (id),
        CONSTRAINT fk_tokens_authorizations_authorization_id FOREIGN KEY (authorization_id) REFERENCES authorizations (id)
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240921134333_InitializeIdentity') THEN
    CREATE UNIQUE INDEX ix_applications_client_id ON applications (client_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240921134333_InitializeIdentity') THEN
    CREATE INDEX ix_authorizations_application_id_status_subject_type ON authorizations (application_id, status, subject, type);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240921134333_InitializeIdentity') THEN
    CREATE INDEX ix_permissions_group_id ON permissions (group_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240921134333_InitializeIdentity') THEN
    CREATE UNIQUE INDEX ix_permissions_value ON permissions (value) INCLUDE (name);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240921134333_InitializeIdentity') THEN
    CREATE INDEX ix_role_claims_role_id ON role_claims (role_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240921134333_InitializeIdentity') THEN
    CREATE UNIQUE INDEX ix_roles_normalized_name ON roles (normalized_name);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240921134333_InitializeIdentity') THEN
    CREATE UNIQUE INDEX ix_scopes_name ON scopes (name);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240921134333_InitializeIdentity') THEN
    CREATE INDEX ix_security_events_user_id ON security_events (user_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240921134333_InitializeIdentity') THEN
    CREATE INDEX ix_tokens_application_id_status_subject_type ON tokens (application_id, status, subject, type);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240921134333_InitializeIdentity') THEN
    CREATE INDEX ix_tokens_authorization_id ON tokens (authorization_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240921134333_InitializeIdentity') THEN
    CREATE UNIQUE INDEX ix_tokens_reference_id ON tokens (reference_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240921134333_InitializeIdentity') THEN
    CREATE INDEX ix_user_claims_user_id ON user_claims (user_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240921134333_InitializeIdentity') THEN
    CREATE INDEX ix_user_logins_user_id ON user_logins (user_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240921134333_InitializeIdentity') THEN
    CREATE INDEX ix_user_roles_role_id ON user_roles (role_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240921134333_InitializeIdentity') THEN
    CREATE INDEX ix_users_normalized_email ON users (normalized_email);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240921134333_InitializeIdentity') THEN
    CREATE UNIQUE INDEX ix_users_normalized_user_name ON users (normalized_user_name);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240921134333_InitializeIdentity') THEN
    CREATE INDEX ix_users_phone_number ON users (phone_number);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240921134333_InitializeIdentity') THEN
    INSERT INTO "__EFMigrationsHistory" (migration_id, product_version)
    VALUES ('20240921134333_InitializeIdentity', '8.0.1');
    END IF;
END $EF$;
COMMIT;

