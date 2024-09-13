START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240913134036_AddMerchantCollection') THEN
    DROP TABLE merchant_collections;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240913134036_AddMerchantCollection') THEN
    DROP INDEX ix_merchant_products_product_id;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240913134036_AddMerchantCollection') THEN
    ALTER TABLE products ADD merchant_product_id uuid NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240913134036_AddMerchantCollection') THEN
    ALTER TABLE merchant_products ADD shop_collection_id uuid;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240913134036_AddMerchantCollection') THEN
    CREATE TABLE shop_collections (
        id uuid NOT NULL DEFAULT (gen_random_uuid()),
        name character varying(200) NOT NULL,
        slug character varying(150) NOT NULL,
        parent_id uuid,
        merchant_id uuid NOT NULL,
        created_at timestamp with time zone NOT NULL DEFAULT (now()),
        CONSTRAINT pk_shop_collections PRIMARY KEY (id),
        CONSTRAINT fk_shop_collections_merchants_merchant_id FOREIGN KEY (merchant_id) REFERENCES merchants (id) ON DELETE CASCADE,
        CONSTRAINT fk_shop_collections_shop_collections_parent_id FOREIGN KEY (parent_id) REFERENCES shop_collections (id)
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240913134036_AddMerchantCollection') THEN
    CREATE UNIQUE INDEX ix_merchant_products_product_id ON merchant_products (product_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240913134036_AddMerchantCollection') THEN
    CREATE INDEX ix_merchant_products_shop_collection_id ON merchant_products (shop_collection_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240913134036_AddMerchantCollection') THEN
    CREATE INDEX ix_shop_collections_merchant_id ON shop_collections (merchant_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240913134036_AddMerchantCollection') THEN
    CREATE INDEX ix_shop_collections_parent_id ON shop_collections (parent_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240913134036_AddMerchantCollection') THEN
    ALTER TABLE merchant_products ADD CONSTRAINT fk_merchant_products_shop_collections_shop_collection_id FOREIGN KEY (shop_collection_id) REFERENCES shop_collections (id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240913134036_AddMerchantCollection') THEN
    INSERT INTO "__EFMigrationsHistory" (migration_id, product_version)
    VALUES ('20240913134036_AddMerchantCollection', '8.0.1');
    END IF;
END $EF$;
COMMIT;

