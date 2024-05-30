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

