START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240918090945_ChangeShopCollectionProductRelationShip') THEN
    ALTER TABLE products DROP CONSTRAINT fk_products_shop_collections_shop_collection_id;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240918090945_ChangeShopCollectionProductRelationShip') THEN
    DROP TABLE merchant_products;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240918090945_ChangeShopCollectionProductRelationShip') THEN
    DROP INDEX ix_products_shop_collection_id;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240918090945_ChangeShopCollectionProductRelationShip') THEN
    ALTER TABLE products DROP COLUMN shop_collection_id;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240918090945_ChangeShopCollectionProductRelationShip') THEN
    CREATE TABLE shop_collection_product (
        shop_collection_id uuid NOT NULL,
        product_id uuid NOT NULL,
        id uuid NOT NULL,
        created_at timestamp with time zone NOT NULL,
        CONSTRAINT pk_shop_collection_product PRIMARY KEY (shop_collection_id, product_id),
        CONSTRAINT fk_shop_collection_product_products_product_id FOREIGN KEY (product_id) REFERENCES products (id) ON DELETE CASCADE,
        CONSTRAINT fk_shop_collection_product_shop_collections_shop_collection_id FOREIGN KEY (shop_collection_id) REFERENCES shop_collections (id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240918090945_ChangeShopCollectionProductRelationShip') THEN
    CREATE INDEX ix_shop_collection_product_product_id ON shop_collection_product (product_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240918090945_ChangeShopCollectionProductRelationShip') THEN
    INSERT INTO "__EFMigrationsHistory" (migration_id, product_version)
    VALUES ('20240918090945_ChangeShopCollectionProductRelationShip', '8.0.1');
    END IF;
END $EF$;
COMMIT;

