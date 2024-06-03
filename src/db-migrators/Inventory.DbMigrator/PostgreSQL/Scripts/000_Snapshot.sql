CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    migration_id character varying(150) NOT NULL,
    product_version character varying(32) NOT NULL,
    CONSTRAINT pk___ef_migrations_history PRIMARY KEY (migration_id)
);

START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240520150128_Initialize') THEN
    CREATE TABLE products (
        id uuid NOT NULL DEFAULT (gen_random_uuid()),
        name character varying(100) NOT NULL,
        slug text NOT NULL,
        price numeric NOT NULL,
        quantity integer NOT NULL,
        created_at timestamp with time zone NOT NULL,
        CONSTRAINT pk_products PRIMARY KEY (id)
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240520150128_Initialize') THEN
    INSERT INTO "__EFMigrationsHistory" (migration_id, product_version)
    VALUES ('20240520150128_Initialize', '8.0.1');
    END IF;
END $EF$;
COMMIT;

START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240525163520_AddCategoryProduct') THEN
    ALTER TABLE products ALTER COLUMN slug TYPE character varying(150);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240525163520_AddCategoryProduct') THEN
    ALTER TABLE products ALTER COLUMN created_at SET DEFAULT (now());
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240525163520_AddCategoryProduct') THEN
    CREATE TABLE categories (
        id uuid NOT NULL DEFAULT (gen_random_uuid()),
        name character varying(100) NOT NULL,
        slug character varying(150) NOT NULL,
        description character varying(500) NOT NULL DEFAULT (''),
        parent_id uuid,
        created_at timestamp with time zone NOT NULL DEFAULT (now()),
        CONSTRAINT pk_categories PRIMARY KEY (id),
        CONSTRAINT fk_categories_categories_parent_id FOREIGN KEY (parent_id) REFERENCES categories (id)
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240525163520_AddCategoryProduct') THEN
    CREATE TABLE category_products (
        category_id uuid NOT NULL,
        product_id uuid NOT NULL,
        CONSTRAINT pk_category_products PRIMARY KEY (category_id, product_id),
        CONSTRAINT fk_category_products_categories_category_id FOREIGN KEY (category_id) REFERENCES categories (id) ON DELETE CASCADE,
        CONSTRAINT fk_category_products_products_product_id FOREIGN KEY (product_id) REFERENCES products (id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240525163520_AddCategoryProduct') THEN
    CREATE INDEX ix_categories_parent_id ON categories (parent_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240525163520_AddCategoryProduct') THEN
    CREATE INDEX ix_category_products_product_id ON category_products (product_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240525163520_AddCategoryProduct') THEN
    INSERT INTO "__EFMigrationsHistory" (migration_id, product_version)
    VALUES ('20240525163520_AddCategoryProduct', '8.0.1');
    END IF;
END $EF$;
COMMIT;

START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240530155002_ApplyEAVToProductVariant') THEN
    ALTER TABLE products DROP COLUMN price;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240530155002_ApplyEAVToProductVariant') THEN
    ALTER TABLE products DROP COLUMN quantity;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240530155002_ApplyEAVToProductVariant') THEN
    ALTER TABLE products ADD description text NOT NULL DEFAULT '';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240530155002_ApplyEAVToProductVariant') THEN
    CREATE TABLE product_attributes (
        id uuid NOT NULL DEFAULT (gen_random_uuid()),
        name character varying(100) NOT NULL,
        created_at timestamp with time zone NOT NULL DEFAULT (now()),
        CONSTRAINT pk_product_attributes PRIMARY KEY (id)
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240530155002_ApplyEAVToProductVariant') THEN
    CREATE TABLE product_variants (
        id uuid NOT NULL DEFAULT (gen_random_uuid()),
        stock integer NOT NULL DEFAULT 0,
        price numeric NOT NULL DEFAULT 0.0,
        product_id uuid NOT NULL,
        created_at timestamp with time zone NOT NULL DEFAULT (now()),
        CONSTRAINT pk_product_variants PRIMARY KEY (id),
        CONSTRAINT fk_product_variants_products_product_id FOREIGN KEY (product_id) REFERENCES products (id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240530155002_ApplyEAVToProductVariant') THEN
    CREATE TABLE product_variant_attribute_values (
        id uuid NOT NULL DEFAULT (gen_random_uuid()),
        product_variant_id uuid NOT NULL,
        product_attribute_id uuid NOT NULL,
        value character varying(200) NOT NULL,
        created_at timestamp with time zone NOT NULL DEFAULT (now()),
        CONSTRAINT pk_product_variant_attribute_values PRIMARY KEY (id),
        CONSTRAINT ak_product_variant_attribute_values_product_variant_id_product UNIQUE (product_variant_id, product_attribute_id),
        CONSTRAINT fk_product_variant_attribute_values_product_attributes_product FOREIGN KEY (product_attribute_id) REFERENCES product_attributes (id) ON DELETE CASCADE,
        CONSTRAINT fk_product_variant_attribute_values_product_variants_product_v FOREIGN KEY (product_variant_id) REFERENCES product_variants (id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240530155002_ApplyEAVToProductVariant') THEN
    CREATE INDEX ix_product_variant_attribute_values_product_attribute_id ON product_variant_attribute_values (product_attribute_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240530155002_ApplyEAVToProductVariant') THEN
    CREATE INDEX ix_product_variants_product_id ON product_variants (product_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240530155002_ApplyEAVToProductVariant') THEN
    INSERT INTO "__EFMigrationsHistory" (migration_id, product_version)
    VALUES ('20240530155002_ApplyEAVToProductVariant', '8.0.1');
    END IF;
END $EF$;
COMMIT;

START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240530161917_AddProductAndAttributeRelationship') THEN
    CREATE TABLE product_product_attributes (
        product_id uuid NOT NULL,
        product_attribute_id uuid NOT NULL,
        CONSTRAINT pk_product_product_attributes PRIMARY KEY (product_id, product_attribute_id),
        CONSTRAINT fk_product_product_attributes_product_attributes_product_attri FOREIGN KEY (product_attribute_id) REFERENCES product_attributes (id) ON DELETE CASCADE,
        CONSTRAINT fk_product_product_attributes_products_product_id FOREIGN KEY (product_id) REFERENCES products (id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240530161917_AddProductAndAttributeRelationship') THEN
    CREATE INDEX ix_product_product_attributes_product_attribute_id ON product_product_attributes (product_attribute_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240530161917_AddProductAndAttributeRelationship') THEN
    INSERT INTO "__EFMigrationsHistory" (migration_id, product_version)
    VALUES ('20240530161917_AddProductAndAttributeRelationship', '8.0.1');
    END IF;
END $EF$;
COMMIT;

