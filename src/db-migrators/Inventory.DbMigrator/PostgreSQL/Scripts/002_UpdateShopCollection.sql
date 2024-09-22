START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240922151151_UpdateShopCollection') THEN
    ALTER TABLE shop_collections DROP CONSTRAINT fk_shop_collections_shop_collections_parent_id;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240922151151_UpdateShopCollection') THEN
    ALTER TABLE shop_collections ADD description character varying(500) NOT NULL DEFAULT ('');
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240922151151_UpdateShopCollection') THEN
    ALTER TABLE shop_collections ADD CONSTRAINT fk_shop_collections_shop_collections_parent_id FOREIGN KEY (parent_id) REFERENCES shop_collections (id) ON DELETE RESTRICT;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20240922151151_UpdateShopCollection') THEN
    INSERT INTO "__EFMigrationsHistory" (migration_id, product_version)
    VALUES ('20240922151151_UpdateShopCollection', '8.0.1');
    END IF;
END $EF$;
COMMIT;

