START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240829090306_AddMerchant') THEN
    ALTER TABLE products ADD has_discounts_applied boolean NOT NULL DEFAULT FALSE;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240829090306_AddMerchant') THEN
    ALTER TABLE categories ADD has_discounts_applied boolean NOT NULL DEFAULT FALSE;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240829090306_AddMerchant') THEN
    CREATE TABLE discounts (
        id uuid NOT NULL DEFAULT (gen_random_uuid()),
        name character varying(200) NOT NULL,
        slug character varying(150) NOT NULL,
        code character varying(100) NOT NULL,
        description character varying(500) DEFAULT (''),
        discount_type character varying(50) NOT NULL DEFAULT ('UNSPECIFIED'),
        discount_value numeric,
        discount_unit character varying(50) NOT NULL DEFAULT ('UNSPECIFIED'),
        min_order_value numeric,
        max_discount_amount numeric,
        start_date timestamp with time zone,
        end_date timestamp with time zone,
        is_active boolean NOT NULL DEFAULT TRUE,
        limitation_times integer,
        limitation_type character varying(50) DEFAULT ('UNSPECIFIED'),
        discount_usage_history jsonb NOT NULL DEFAULT ('[]'),
        discount_id uuid,
        created_at timestamp with time zone NOT NULL DEFAULT (now()),
        has_discounts_applied boolean NOT NULL,
        CONSTRAINT pk_discounts PRIMARY KEY (id),
        CONSTRAINT fk_discounts_discounts_discount_id FOREIGN KEY (discount_id) REFERENCES discounts (id)
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240829090306_AddMerchant') THEN
    CREATE TABLE merchants (
        id uuid NOT NULL DEFAULT (gen_random_uuid()),
        name character varying(200) NOT NULL,
        slug character varying(150) NOT NULL,
        merchant_number text,
        description character varying(500) DEFAULT (''),
        is_active boolean NOT NULL DEFAULT TRUE,
        created_at timestamp with time zone NOT NULL DEFAULT (now()),
        CONSTRAINT pk_merchants PRIMARY KEY (id)
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240829090306_AddMerchant') THEN
    CREATE TABLE discount_applied_to_categories (
        discount_id uuid NOT NULL,
        category_id uuid NOT NULL,
        CONSTRAINT pk_discount_applied_to_categories PRIMARY KEY (discount_id, category_id),
        CONSTRAINT fk_discount_applied_to_categories_categories_category_id FOREIGN KEY (category_id) REFERENCES categories (id) ON DELETE CASCADE,
        CONSTRAINT fk_discount_applied_to_categories_discounts_discount_id FOREIGN KEY (discount_id) REFERENCES discounts (id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240829090306_AddMerchant') THEN
    CREATE TABLE discount_applied_to_products (
        discount_id uuid NOT NULL,
        product_id uuid NOT NULL,
        CONSTRAINT pk_discount_applied_to_products PRIMARY KEY (discount_id, product_id),
        CONSTRAINT fk_discount_applied_to_products_discounts_discount_id FOREIGN KEY (discount_id) REFERENCES discounts (id) ON DELETE CASCADE,
        CONSTRAINT fk_discount_applied_to_products_products_product_id FOREIGN KEY (product_id) REFERENCES products (id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240829090306_AddMerchant') THEN
    CREATE TABLE merchant_categories (
        merchant_id uuid NOT NULL,
        category_id uuid NOT NULL,
        created_at timestamp with time zone NOT NULL DEFAULT (now()),
        CONSTRAINT pk_merchant_categories PRIMARY KEY (merchant_id, category_id),
        CONSTRAINT fk_merchant_categories_categories_category_id FOREIGN KEY (category_id) REFERENCES categories (id) ON DELETE CASCADE,
        CONSTRAINT fk_merchant_categories_merchants_merchant_id FOREIGN KEY (merchant_id) REFERENCES merchants (id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240829090306_AddMerchant') THEN
    CREATE TABLE merchant_products (
        merchant_id uuid NOT NULL,
        product_id uuid NOT NULL,
        created_at timestamp with time zone NOT NULL DEFAULT (now()),
        CONSTRAINT pk_merchant_products PRIMARY KEY (merchant_id, product_id),
        CONSTRAINT fk_merchant_products_merchants_merchant_id FOREIGN KEY (merchant_id) REFERENCES merchants (id) ON DELETE CASCADE,
        CONSTRAINT fk_merchant_products_products_product_id FOREIGN KEY (product_id) REFERENCES products (id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240829090306_AddMerchant') THEN
    CREATE TABLE stores (
        id uuid NOT NULL DEFAULT (gen_random_uuid()),
        name character varying(200) NOT NULL,
        slug character varying(150) NOT NULL,
        store_number text,
        description character varying(500) DEFAULT (''),
        phone_number text,
        is_active boolean NOT NULL DEFAULT TRUE,
        store_address text,
        merchant_id uuid NOT NULL,
        created_at timestamp with time zone NOT NULL DEFAULT (now()),
        CONSTRAINT pk_stores PRIMARY KEY (id),
        CONSTRAINT fk_stores_merchants_merchant_id FOREIGN KEY (merchant_id) REFERENCES merchants (id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240829090306_AddMerchant') THEN
    CREATE INDEX ix_discount_applied_to_categories_category_id ON discount_applied_to_categories (category_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240829090306_AddMerchant') THEN
    CREATE INDEX ix_discount_applied_to_categories_discount_id ON discount_applied_to_categories (discount_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240829090306_AddMerchant') THEN
    CREATE INDEX ix_discount_applied_to_products_discount_id ON discount_applied_to_products (discount_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240829090306_AddMerchant') THEN
    CREATE INDEX ix_discount_applied_to_products_product_id ON discount_applied_to_products (product_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240829090306_AddMerchant') THEN
    CREATE UNIQUE INDEX ix_discounts_code ON discounts (code);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240829090306_AddMerchant') THEN
    CREATE INDEX ix_discounts_discount_id ON discounts (discount_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240829090306_AddMerchant') THEN
    CREATE INDEX ix_merchant_categories_category_id ON merchant_categories (category_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240829090306_AddMerchant') THEN
    CREATE INDEX ix_merchant_products_product_id ON merchant_products (product_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240829090306_AddMerchant') THEN
    CREATE INDEX ix_stores_merchant_id ON stores (merchant_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240829090306_AddMerchant') THEN
    INSERT INTO "__EFMigrationsHistory" (migration_id, product_version)
    VALUES ('20240829090306_AddMerchant', '8.0.1');
    END IF;
END $EF$;
COMMIT;

